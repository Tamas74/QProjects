namespace gloAppointmentScheduling
{
    partial class frmSetupAppointment
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpApp_DateTime_EndDate, dtpApp_DateTime_EndTime, dtpApp_DateTime_StartTime, dtpApp_DateTime_StartDate, dtpRec_Range_EndBy, dtpRec_Range_StartDate, dtpRec_DateTime_EndTime, dtpRec_DateTime_StartTime };
            System.Windows.Forms.Control[] cntControls = { dtpApp_DateTime_EndDate, dtpApp_DateTime_EndDate, dtpApp_DateTime_EndTime, dtpApp_DateTime_StartTime, dtpApp_DateTime_StartDate, dtpRec_Range_EndBy, dtpRec_Range_StartDate, dtpRec_DateTime_EndTime, dtpRec_DateTime_StartTime };
 
            if (disposing && (components != null))
            {


                components.Dispose();
                base.Dispose(disposing);
                if (_SetAppointmentParameter != null)
                {
                    _SetAppointmentParameter.Dispose();
                }
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
                if (dtpControls != null)
                {
                    if (dtpControls.Length > 0)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers( ref dtpControls);
                       
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
            
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAppointment));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlAppointment = new System.Windows.Forms.Panel();
            this.chkPARequired = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRemove_PriorAuthorization = new System.Windows.Forms.Button();
            this.btnAdd_PriorAuthorization = new System.Windows.Forms.Button();
            this.txtPriorAuthorizationNo = new System.Windows.Forms.TextBox();
            this.txtApp_Notes = new System.Windows.Forms.RichTextBox();
            this.lblPatientBalance = new System.Windows.Forms.Label();
            this.pnlApp_DateTime = new System.Windows.Forms.Panel();
            this.pnlApp_DateTimeContainer = new System.Windows.Forms.Panel();
            this.chkApp_DateTime_IsAllDayEvent = new System.Windows.Forms.CheckBox();
            this.numApp_DateTime_Duration = new System.Windows.Forms.NumericUpDown();
            this.dtpApp_DateTime_StartDate = new System.Windows.Forms.DateTimePicker();
            this.lblApp_DateTime_Time = new System.Windows.Forms.Label();
            this.lblDurationUnit = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lblApp_DateTime_Date = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Duration = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeBottomBrd = new System.Windows.Forms.Label();
            this.chkRecurring = new System.Windows.Forms.CheckBox();
            this.lbl_pnlApp_DateTimeTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlApp_DateTimeLeftBrd = new System.Windows.Forms.Label();
            this.lblApp_Recurrence_Time = new System.Windows.Forms.Label();
            this.lblFinishDate = new System.Windows.Forms.Label();
            this.btnApp_ClearReferralDoctor = new System.Windows.Forms.Button();
            this.btnApp_ClearPatient = new System.Windows.Forms.Button();
            this.lblFinishTime = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpApp_DateTime_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lblApp_DateTime_ColorContainer = new System.Windows.Forms.Label();
            this.btnApp_ClearProvider = new System.Windows.Forms.Button();
            this.btnApp_ClearInsurance = new System.Windows.Forms.Button();
            this.btnApp_ClearResources = new System.Windows.Forms.Button();
            this.btnApp_ClearDateTime_Color = new System.Windows.Forms.Button();
            this.btnApp_ClearProcedures = new System.Windows.Forms.Button();
            this.lblApp_DateTime_Color = new System.Windows.Forms.Label();
            this.pnlCriteria_ProviderProblemType = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd = new System.Windows.Forms.Label();
            this.c1ProviderProblemType = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_ProviderProblemType_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd = new System.Windows.Forms.Label();
            this.lblCriteria_ProviderProblemType_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd = new System.Windows.Forms.Label();
            this.pnlCriteria_Resources = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_ResourcesBottomBrd = new System.Windows.Forms.Label();
            this.c1Resources = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria_Resources_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCriteria_Resources_Header = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlCriteria_ResourcesTopBrd = new System.Windows.Forms.Label();
            this.btnApp_DateTime_Color = new System.Windows.Forms.Button();
            this.pnlAppointmentHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlAppointmentHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblAppointment = new System.Windows.Forms.Label();
            this.lblApp_Divider3 = new System.Windows.Forms.Label();
            this.lblApp_Divider2 = new System.Windows.Forms.Label();
            this.lblApp_Divider1 = new System.Windows.Forms.Label();
            this.btnApp_ReferralDoctor = new System.Windows.Forms.Button();
            this.btnApp_Insurance = new System.Windows.Forms.Button();
            this.btnApp_Patient = new System.Windows.Forms.Button();
            this.btnApp_Provider = new System.Windows.Forms.Button();
            this.cmbApp_ReferralDoctor = new System.Windows.Forms.ComboBox();
            this.txtApp_Patient = new System.Windows.Forms.TextBox();
            this.lblApp_ReferralDoctor = new System.Windows.Forms.Label();
            this.lblApp_Status = new System.Windows.Forms.Label();
            this.btnApp_Procedures = new System.Windows.Forms.Button();
            this.lblApp_Patient = new System.Windows.Forms.Label();
            this.cmbApp_Status = new System.Windows.Forms.ComboBox();
            this.btnApp_Resources = new System.Windows.Forms.Button();
            this.lblAuthorizaionName = new System.Windows.Forms.Label();
            this.cmbApp_AppointmentType = new System.Windows.Forms.ComboBox();
            this.lblPatientBalanceName = new System.Windows.Forms.Label();
            this.lblApp_AppointmentType = new System.Windows.Forms.Label();
            this.lblApp_Notes = new System.Windows.Forms.Label();
            this.lblApp_Resources = new System.Windows.Forms.Label();
            this.cmbApp_Department = new System.Windows.Forms.ComboBox();
            this.lblApp_Department = new System.Windows.Forms.Label();
            this.cmbApp_Provider = new System.Windows.Forms.ComboBox();
            this.cmbApp_Location = new System.Windows.Forms.ComboBox();
            this.lblApp_Coverage = new System.Windows.Forms.Label();
            this.lblApp_Location = new System.Windows.Forms.Label();
            this.cmbApp_Coverage = new System.Windows.Forms.ComboBox();
            this.lblApp_Recurrence = new System.Windows.Forms.Label();
            this.lblApp_DateTime = new System.Windows.Forms.Label();
            this.lblApp_Procedure = new System.Windows.Forms.Label();
            this.lbl_pnlAppointmentLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAppointmentRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAppointmentTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlAppointmentBottomBrd = new System.Windows.Forms.Label();
            this.lblApp_Provider = new System.Windows.Forms.Label();
            this.pnlRecurring = new System.Windows.Forms.Panel();
            this.pnlRecurring_Appointments = new System.Windows.Forms.Panel();
            this.lvwRec_Apointments = new System.Windows.Forms.ListView();
            this.pnlRecurring_Appointments_Header = new System.Windows.Forms.Panel();
            this.lblRecurring_Appointments_Header = new System.Windows.Forms.Label();
            this.pnlRecurring_Appointments_Exception = new System.Windows.Forms.Panel();
            this.lvwRec_Exception = new System.Windows.Forms.ListView();
            this.btnRec_RemoveException = new System.Windows.Forms.Button();
            this.btnRec_AddException = new System.Windows.Forms.Button();
            this.lbl_pnlRecurring_AppointmentsLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_AppointmentsRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_AppointmentsTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_AppointmentsBottomBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Range = new System.Windows.Forms.Panel();
            this.numRec_Range_EndAfterOccurence = new System.Windows.Forms.NumericUpDown();
            this.lbl_pnlRecurring_RangeBottomBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Range_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_Range_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.cmbRec_Range_NoEndDateYear = new System.Windows.Forms.ComboBox();
            this.lblRecurring_Range_Header = new System.Windows.Forms.Label();
            this.rbRec_Range_NoEndDate = new System.Windows.Forms.RadioButton();
            this.lblRec_Range_EndDate = new System.Windows.Forms.Label();
            this.dtpRec_Range_StartDate = new System.Windows.Forms.DateTimePicker();
            this.rbRec_Range_EndBy = new System.Windows.Forms.RadioButton();
            this.lblRec_Range_StartDate = new System.Windows.Forms.Label();
            this.rbRec_Range_EndAfterOccurence = new System.Windows.Forms.RadioButton();
            this.dtpRec_Range_EndBy = new System.Windows.Forms.DateTimePicker();
            this.lblRec_Range_Occurence = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_RangeTopBrd = new System.Windows.Forms.Label();
            this.pnlRecurring_Pattern = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_PatternBootomBrd = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Yearly = new System.Windows.Forms.RadioButton();
            this.pnlRecurring_Pattern_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblRecurring_Pattern_Header = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Monthly = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Weekly = new System.Windows.Forms.RadioButton();
            this.lbl_pnlRecurring_PatternLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_PatternRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_PatternTopBrd = new System.Windows.Forms.Label();
            this.pnlRec_Pattern_Yearly = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.numRec_Pattern_Yearly_Every_MonthDay = new System.Windows.Forms.NumericUpDown();
            this.cmbRec_Pattern_Yearly_Criteria_Month = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Every_Month = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday = new System.Windows.Forms.ComboBox();
            this.cmbRec_Pattern_Yearly_Criteria_FstLst = new System.Windows.Forms.ComboBox();
            this.lblRec_Pattern_Yearly_Criteria_Of = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Yearly_Criteria = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Yearly_EveryMonthDay = new System.Windows.Forms.RadioButton();
            this.pnlRec_Pattern_Daily = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_DailyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_DailyLeftBrd = new System.Windows.Forms.Label();
            this.numRec_Pattern_Daily_EveryDay = new System.Windows.Forms.NumericUpDown();
            this.lblRec_Pattern_Daily_Days = new System.Windows.Forms.Label();
            this.rbRec_Pattern_Daily_EveryWeekday = new System.Windows.Forms.RadioButton();
            this.rbRec_Pattern_Daily_EveryDay = new System.Windows.Forms.RadioButton();
            this.pnlRec_Pattern_Monthly = new System.Windows.Forms.Panel();
            this.lbl_pnlRec_Pattern_MonthlyRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRec_Pattern_MonthlyTopBrd = new System.Windows.Forms.Label();
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
            this.pnlRecurring_DateTime = new System.Windows.Forms.Panel();
            this.chkRec_DateTime_IsAllDayEvent = new System.Windows.Forms.CheckBox();
            this.lbl_pnlRecurring_DateTimeBottomBrd = new System.Windows.Forms.Label();
            this.lblRec_DateTime_ColorCode = new System.Windows.Forms.Label();
            this.btnRec_DateTime_Color = new System.Windows.Forms.Button();
            this.pnlRecurring_DateTime_Header = new System.Windows.Forms.Panel();
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblRecurring_DateTime_Header = new System.Windows.Forms.Label();
            this.numRec_DateTime_Duration = new System.Windows.Forms.NumericUpDown();
            this.dtpRec_DateTime_EndTime = new System.Windows.Forms.DateTimePicker();
            this.lblRec_DateTime_EndDate = new System.Windows.Forms.Label();
            this.dtpRec_DateTime_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_pnlRecurring_DateTimeLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_DateTimeRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurring_DateTimeTopBrd = new System.Windows.Forms.Label();
            this.lblRec_DateTime_StartDate = new System.Windows.Forms.Label();
            this.lblRec_DateTime_Duration = new System.Windows.Forms.Label();
            this.lblRec_DateTime_ColorContainer = new System.Windows.Forms.Label();
            this.pnlRecurringHeader = new System.Windows.Forms.Panel();
            this.lblRecurring = new System.Windows.Forms.Label();
            this.lbl_pnlRecurringHeaderLeftBrd = new System.Windows.Forms.Label();
            this.btnRec_Save = new System.Windows.Forms.Button();
            this.btnRec_Close = new System.Windows.Forms.Button();
            this.lbl_pnlRecurringHeaderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurringHeaderTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlRecurringHeaderBottomBrdBrd = new System.Windows.Forms.Label();
            this.lbl_Recurrence_DTL = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pnl_toolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_RegPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Fax = new System.Windows.Forms.ToolStripButton();
            this.tsb_Email = new System.Windows.Forms.ToolStripButton();
            this.tsb_Recurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_RemoveRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_ApplyRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_Search = new System.Windows.Forms.ToolStripButton();
            this.tsb_Help = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_CancelRecurrence = new System.Windows.Forms.ToolStripButton();
            this.pnlAlerts = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlgloEMRAlerts = new System.Windows.Forms.Panel();
            this.c1EMRAlerts = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlEMRCaption = new System.Windows.Forms.Panel();
            this.btnUpEMR = new System.Windows.Forms.Button();
            this.btnDownEMR = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlgloPMAlerts = new System.Windows.Forms.Panel();
            this.c1EligibilityCheck = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1PatientAlerts = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label31 = new System.Windows.Forms.Label();
            this.c1GlobalPeriod = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1CopayAlert = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlPMCaption = new System.Windows.Forms.Panel();
            this.btnUpPM = new System.Windows.Forms.Button();
            this.btnDownPM = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlCommanAlerts = new System.Windows.Forms.Panel();
            this.c1SystemAlert = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlMainNToolStrip = new System.Windows.Forms.Panel();
            this.tmrCopayAlertBlink = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlAppointment.SuspendLayout();
            this.pnlApp_DateTime.SuspendLayout();
            this.pnlApp_DateTimeContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).BeginInit();
            this.pnlCriteria_ProviderProblemType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderProblemType)).BeginInit();
            this.pnlCriteria_ProviderProblemType_Header.SuspendLayout();
            this.pnlCriteria_Resources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Resources)).BeginInit();
            this.pnlCriteria_Resources_Header.SuspendLayout();
            this.pnlAppointmentHeader.SuspendLayout();
            this.pnlRecurring.SuspendLayout();
            this.pnlRecurring_Appointments.SuspendLayout();
            this.pnlRecurring_Appointments_Header.SuspendLayout();
            this.pnlRecurring_Appointments_Exception.SuspendLayout();
            this.pnlRecurring_Range.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).BeginInit();
            this.pnlRecurring_Range_Header.SuspendLayout();
            this.pnlRecurring_Pattern.SuspendLayout();
            this.pnlRecurring_Pattern_Header.SuspendLayout();
            this.pnlRec_Pattern_Yearly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).BeginInit();
            this.pnlRec_Pattern_Daily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).BeginInit();
            this.pnlRec_Pattern_Monthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).BeginInit();
            this.pnlRec_Pattern_Weekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).BeginInit();
            this.pnlRecurring_DateTime.SuspendLayout();
            this.pnlRecurring_DateTime_Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_DateTime_Duration)).BeginInit();
            this.pnlRecurringHeader.SuspendLayout();
            this.pnl_toolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlAlerts.SuspendLayout();
            this.pnlgloEMRAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1EMRAlerts)).BeginInit();
            this.pnlEMRCaption.SuspendLayout();
            this.pnlgloPMAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1EligibilityCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GlobalPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CopayAlert)).BeginInit();
            this.pnlPMCaption.SuspendLayout();
            this.pnlCommanAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SystemAlert)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlMainNToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlAppointment);
            this.pnlMain.Controls.Add(this.pnlRecurring);
            this.pnlMain.Controls.Add(this.lbl_Recurrence_DTL);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(604, 463);
            this.pnlMain.TabIndex = 65;
            // 
            // pnlAppointment
            // 
            this.pnlAppointment.AutoSize = true;
            this.pnlAppointment.Controls.Add(this.chkPARequired);
            this.pnlAppointment.Controls.Add(this.label2);
            this.pnlAppointment.Controls.Add(this.label1);
            this.pnlAppointment.Controls.Add(this.label7);
            this.pnlAppointment.Controls.Add(this.btnRemove_PriorAuthorization);
            this.pnlAppointment.Controls.Add(this.btnAdd_PriorAuthorization);
            this.pnlAppointment.Controls.Add(this.txtPriorAuthorizationNo);
            this.pnlAppointment.Controls.Add(this.txtApp_Notes);
            this.pnlAppointment.Controls.Add(this.lblPatientBalance);
            this.pnlAppointment.Controls.Add(this.pnlApp_DateTime);
            this.pnlAppointment.Controls.Add(this.lblFinishDate);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearReferralDoctor);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearPatient);
            this.pnlAppointment.Controls.Add(this.lblFinishTime);
            this.pnlAppointment.Controls.Add(this.dtpApp_DateTime_EndDate);
            this.pnlAppointment.Controls.Add(this.dtpApp_DateTime_EndTime);
            this.pnlAppointment.Controls.Add(this.lblApp_DateTime_ColorContainer);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearProvider);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearInsurance);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearResources);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearDateTime_Color);
            this.pnlAppointment.Controls.Add(this.btnApp_ClearProcedures);
            this.pnlAppointment.Controls.Add(this.lblApp_DateTime_Color);
            this.pnlAppointment.Controls.Add(this.pnlCriteria_ProviderProblemType);
            this.pnlAppointment.Controls.Add(this.pnlCriteria_Resources);
            this.pnlAppointment.Controls.Add(this.btnApp_DateTime_Color);
            this.pnlAppointment.Controls.Add(this.pnlAppointmentHeader);
            this.pnlAppointment.Controls.Add(this.lblApp_Divider3);
            this.pnlAppointment.Controls.Add(this.lblApp_Divider2);
            this.pnlAppointment.Controls.Add(this.lblApp_Divider1);
            this.pnlAppointment.Controls.Add(this.btnApp_ReferralDoctor);
            this.pnlAppointment.Controls.Add(this.btnApp_Insurance);
            this.pnlAppointment.Controls.Add(this.btnApp_Patient);
            this.pnlAppointment.Controls.Add(this.btnApp_Provider);
            this.pnlAppointment.Controls.Add(this.cmbApp_ReferralDoctor);
            this.pnlAppointment.Controls.Add(this.txtApp_Patient);
            this.pnlAppointment.Controls.Add(this.lblApp_ReferralDoctor);
            this.pnlAppointment.Controls.Add(this.lblApp_Status);
            this.pnlAppointment.Controls.Add(this.btnApp_Procedures);
            this.pnlAppointment.Controls.Add(this.lblApp_Patient);
            this.pnlAppointment.Controls.Add(this.cmbApp_Status);
            this.pnlAppointment.Controls.Add(this.btnApp_Resources);
            this.pnlAppointment.Controls.Add(this.lblAuthorizaionName);
            this.pnlAppointment.Controls.Add(this.cmbApp_AppointmentType);
            this.pnlAppointment.Controls.Add(this.lblPatientBalanceName);
            this.pnlAppointment.Controls.Add(this.lblApp_AppointmentType);
            this.pnlAppointment.Controls.Add(this.lblApp_Notes);
            this.pnlAppointment.Controls.Add(this.lblApp_Resources);
            this.pnlAppointment.Controls.Add(this.cmbApp_Department);
            this.pnlAppointment.Controls.Add(this.lblApp_Department);
            this.pnlAppointment.Controls.Add(this.cmbApp_Provider);
            this.pnlAppointment.Controls.Add(this.cmbApp_Location);
            this.pnlAppointment.Controls.Add(this.lblApp_Coverage);
            this.pnlAppointment.Controls.Add(this.lblApp_Location);
            this.pnlAppointment.Controls.Add(this.cmbApp_Coverage);
            this.pnlAppointment.Controls.Add(this.lblApp_Recurrence);
            this.pnlAppointment.Controls.Add(this.lblApp_DateTime);
            this.pnlAppointment.Controls.Add(this.lblApp_Procedure);
            this.pnlAppointment.Controls.Add(this.lbl_pnlAppointmentLeftBrd);
            this.pnlAppointment.Controls.Add(this.lbl_pnlAppointmentRightBrd);
            this.pnlAppointment.Controls.Add(this.lbl_pnlAppointmentTopBrd);
            this.pnlAppointment.Controls.Add(this.lbl_pnlAppointmentBottomBrd);
            this.pnlAppointment.Controls.Add(this.lblApp_Provider);
            this.pnlAppointment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAppointment.Location = new System.Drawing.Point(0, 0);
            this.pnlAppointment.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAppointment.Name = "pnlAppointment";
            this.pnlAppointment.Padding = new System.Windows.Forms.Padding(2);
            this.pnlAppointment.Size = new System.Drawing.Size(604, 463);
            this.pnlAppointment.TabIndex = 0;
            this.pnlAppointment.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAppointment_Paint);
            // 
            // chkPARequired
            // 
            this.chkPARequired.AutoSize = true;
            this.chkPARequired.Location = new System.Drawing.Point(123, 145);
            this.chkPARequired.Name = "chkPARequired";
            this.chkPARequired.Size = new System.Drawing.Size(150, 18);
            this.chkPARequired.TabIndex = 178;
            this.chkPARequired.Text = "Authorization Required";
            this.chkPARequired.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(57, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 177;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(373, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 176;
            this.label1.Text = "*";
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
            this.label7.Location = new System.Drawing.Point(51, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 175;
            this.label7.Text = "*";
            this.label7.Visible = false;
            // 
            // btnRemove_PriorAuthorization
            // 
            this.btnRemove_PriorAuthorization.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove_PriorAuthorization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove_PriorAuthorization.BackgroundImage")));
            this.btnRemove_PriorAuthorization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove_PriorAuthorization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemove_PriorAuthorization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove_PriorAuthorization.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove_PriorAuthorization.Image")));
            this.btnRemove_PriorAuthorization.Location = new System.Drawing.Point(338, 86);
            this.btnRemove_PriorAuthorization.Name = "btnRemove_PriorAuthorization";
            this.btnRemove_PriorAuthorization.Size = new System.Drawing.Size(22, 22);
            this.btnRemove_PriorAuthorization.TabIndex = 11;
            this.btnRemove_PriorAuthorization.UseVisualStyleBackColor = false;
            this.btnRemove_PriorAuthorization.Click += new System.EventHandler(this.btnRemove_PriorAuthorization_Click);
            this.btnRemove_PriorAuthorization.MouseLeave += new System.EventHandler(this.btnRemove_PriorAuthorization_MouseLeave);
            this.btnRemove_PriorAuthorization.MouseHover += new System.EventHandler(this.btnRemove_PriorAuthorization_MouseHover);
            // 
            // btnAdd_PriorAuthorization
            // 
            this.btnAdd_PriorAuthorization.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd_PriorAuthorization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd_PriorAuthorization.BackgroundImage")));
            this.btnAdd_PriorAuthorization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd_PriorAuthorization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdd_PriorAuthorization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd_PriorAuthorization.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd_PriorAuthorization.Image")));
            this.btnAdd_PriorAuthorization.Location = new System.Drawing.Point(312, 86);
            this.btnAdd_PriorAuthorization.Name = "btnAdd_PriorAuthorization";
            this.btnAdd_PriorAuthorization.Size = new System.Drawing.Size(22, 22);
            this.btnAdd_PriorAuthorization.TabIndex = 10;
            this.btnAdd_PriorAuthorization.UseVisualStyleBackColor = false;
            this.btnAdd_PriorAuthorization.Click += new System.EventHandler(this.btnAdd_PriorAuthorization_Click);
            this.btnAdd_PriorAuthorization.MouseLeave += new System.EventHandler(this.btnAdd_PriorAuthorization_MouseLeave);
            this.btnAdd_PriorAuthorization.MouseHover += new System.EventHandler(this.btnAdd_PriorAuthorization_MouseHover);
            // 
            // txtPriorAuthorizationNo
            // 
            this.txtPriorAuthorizationNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txtPriorAuthorizationNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPriorAuthorizationNo.ForeColor = System.Drawing.Color.Black;
            this.txtPriorAuthorizationNo.Location = new System.Drawing.Point(125, 86);
            this.txtPriorAuthorizationNo.Name = "txtPriorAuthorizationNo";
            this.txtPriorAuthorizationNo.ReadOnly = true;
            this.txtPriorAuthorizationNo.Size = new System.Drawing.Size(182, 22);
            this.txtPriorAuthorizationNo.TabIndex = 9;
            // 
            // txtApp_Notes
            // 
            this.txtApp_Notes.ForeColor = System.Drawing.Color.Black;
            this.txtApp_Notes.Location = new System.Drawing.Point(101, 225);
            this.txtApp_Notes.Margin = new System.Windows.Forms.Padding(2);
            this.txtApp_Notes.MaxLength = 1000;
            this.txtApp_Notes.Name = "txtApp_Notes";
            this.txtApp_Notes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtApp_Notes.Size = new System.Drawing.Size(353, 46);
            this.txtApp_Notes.TabIndex = 174;
            this.txtApp_Notes.Text = "";
            this.txtApp_Notes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApp_Notes_KeyPress);
            // 
            // lblPatientBalance
            // 
            this.lblPatientBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientBalance.AutoEllipsis = true;
            this.lblPatientBalance.AutoSize = true;
            this.lblPatientBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPatientBalance.ForeColor = System.Drawing.Color.Maroon;
            this.lblPatientBalance.Location = new System.Drawing.Point(452, 117);
            this.lblPatientBalance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPatientBalance.Name = "lblPatientBalance";
            this.lblPatientBalance.Size = new System.Drawing.Size(35, 14);
            this.lblPatientBalance.TabIndex = 16;
            this.lblPatientBalance.Text = "0.00";
            this.lblPatientBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPatientBalance.Visible = false;
            // 
            // pnlApp_DateTime
            // 
            this.pnlApp_DateTime.Controls.Add(this.pnlApp_DateTimeContainer);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeBottomBrd);
            this.pnlApp_DateTime.Controls.Add(this.chkRecurring);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeTopBrd);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeRightBrd);
            this.pnlApp_DateTime.Controls.Add(this.lbl_pnlApp_DateTimeLeftBrd);
            this.pnlApp_DateTime.Controls.Add(this.lblApp_Recurrence_Time);
            this.pnlApp_DateTime.Location = new System.Drawing.Point(101, 173);
            this.pnlApp_DateTime.Margin = new System.Windows.Forms.Padding(2);
            this.pnlApp_DateTime.Name = "pnlApp_DateTime";
            this.pnlApp_DateTime.Size = new System.Drawing.Size(490, 47);
            this.pnlApp_DateTime.TabIndex = 17;
            // 
            // pnlApp_DateTimeContainer
            // 
            this.pnlApp_DateTimeContainer.Controls.Add(this.chkApp_DateTime_IsAllDayEvent);
            this.pnlApp_DateTimeContainer.Controls.Add(this.numApp_DateTime_Duration);
            this.pnlApp_DateTimeContainer.Controls.Add(this.dtpApp_DateTime_StartDate);
            this.pnlApp_DateTimeContainer.Controls.Add(this.lblApp_DateTime_Time);
            this.pnlApp_DateTimeContainer.Controls.Add(this.lblDurationUnit);
            this.pnlApp_DateTimeContainer.Controls.Add(this.dtpApp_DateTime_StartTime);
            this.pnlApp_DateTimeContainer.Controls.Add(this.lblApp_DateTime_Date);
            this.pnlApp_DateTimeContainer.Controls.Add(this.lblApp_DateTime_Duration);
            this.pnlApp_DateTimeContainer.Location = new System.Drawing.Point(2, 6);
            this.pnlApp_DateTimeContainer.Margin = new System.Windows.Forms.Padding(2);
            this.pnlApp_DateTimeContainer.Name = "pnlApp_DateTimeContainer";
            this.pnlApp_DateTimeContainer.Size = new System.Drawing.Size(484, 33);
            this.pnlApp_DateTimeContainer.TabIndex = 117;
            // 
            // chkApp_DateTime_IsAllDayEvent
            // 
            this.chkApp_DateTime_IsAllDayEvent.BackColor = System.Drawing.Color.Transparent;
            this.chkApp_DateTime_IsAllDayEvent.Location = new System.Drawing.Point(419, 8);
            this.chkApp_DateTime_IsAllDayEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkApp_DateTime_IsAllDayEvent.Name = "chkApp_DateTime_IsAllDayEvent";
            this.chkApp_DateTime_IsAllDayEvent.Size = new System.Drawing.Size(63, 18);
            this.chkApp_DateTime_IsAllDayEvent.TabIndex = 86;
            this.chkApp_DateTime_IsAllDayEvent.Text = "All Day";
            this.chkApp_DateTime_IsAllDayEvent.UseVisualStyleBackColor = false;
            this.chkApp_DateTime_IsAllDayEvent.CheckedChanged += new System.EventHandler(this.chkApp_DateTime_IsAllDayEvent_CheckedChanged);
            // 
            // numApp_DateTime_Duration
            // 
            this.numApp_DateTime_Duration.ForeColor = System.Drawing.Color.Black;
            this.numApp_DateTime_Duration.Location = new System.Drawing.Point(330, 5);
            this.numApp_DateTime_Duration.Margin = new System.Windows.Forms.Padding(2);
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
            this.numApp_DateTime_Duration.Size = new System.Drawing.Size(42, 22);
            this.numApp_DateTime_Duration.TabIndex = 63;
            this.numApp_DateTime_Duration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.ValueChanged += new System.EventHandler(this.numApp_DateTime_Duration_ValueChanged);
            this.numApp_DateTime_Duration.Leave += new System.EventHandler(this.numApp_DateTime_Duration_Leave);
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
            this.dtpApp_DateTime_StartDate.Location = new System.Drawing.Point(43, 5);
            this.dtpApp_DateTime_StartDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpApp_DateTime_StartDate.Name = "dtpApp_DateTime_StartDate";
            this.dtpApp_DateTime_StartDate.Size = new System.Drawing.Size(93, 22);
            this.dtpApp_DateTime_StartDate.TabIndex = 52;
            this.dtpApp_DateTime_StartDate.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_StartDate_ValueChanged);
            this.dtpApp_DateTime_StartDate.Leave += new System.EventHandler(this.dtpApp_DateTime_StartDate_Leave);
            // 
            // lblApp_DateTime_Time
            // 
            this.lblApp_DateTime_Time.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Time.Location = new System.Drawing.Point(138, 9);
            this.lblApp_DateTime_Time.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime_Time.Name = "lblApp_DateTime_Time";
            this.lblApp_DateTime_Time.Size = new System.Drawing.Size(42, 14);
            this.lblApp_DateTime_Time.TabIndex = 59;
            this.lblApp_DateTime_Time.Text = "Time :";
            // 
            // lblDurationUnit
            // 
            this.lblDurationUnit.Location = new System.Drawing.Point(375, 9);
            this.lblDurationUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDurationUnit.Name = "lblDurationUnit";
            this.lblDurationUnit.Size = new System.Drawing.Size(41, 14);
            this.lblDurationUnit.TabIndex = 87;
            this.lblDurationUnit.Text = "(mins)";
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
            this.dtpApp_DateTime_StartTime.Location = new System.Drawing.Point(182, 5);
            this.dtpApp_DateTime_StartTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpApp_DateTime_StartTime.Name = "dtpApp_DateTime_StartTime";
            this.dtpApp_DateTime_StartTime.ShowUpDown = true;
            this.dtpApp_DateTime_StartTime.Size = new System.Drawing.Size(82, 22);
            this.dtpApp_DateTime_StartTime.TabIndex = 55;
            this.dtpApp_DateTime_StartTime.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_StartTime_ValueChanged);
            this.dtpApp_DateTime_StartTime.Leave += new System.EventHandler(this.dtpApp_DateTime_StartTime_Leave);
            // 
            // lblApp_DateTime_Date
            // 
            this.lblApp_DateTime_Date.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Date.Location = new System.Drawing.Point(1, 9);
            this.lblApp_DateTime_Date.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime_Date.Name = "lblApp_DateTime_Date";
            this.lblApp_DateTime_Date.Size = new System.Drawing.Size(41, 14);
            this.lblApp_DateTime_Date.TabIndex = 42;
            this.lblApp_DateTime_Date.Text = "Date :";
            this.lblApp_DateTime_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApp_DateTime_Duration
            // 
            this.lblApp_DateTime_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Duration.Location = new System.Drawing.Point(267, 9);
            this.lblApp_DateTime_Duration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime_Duration.Name = "lblApp_DateTime_Duration";
            this.lblApp_DateTime_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblApp_DateTime_Duration.TabIndex = 44;
            this.lblApp_DateTime_Duration.Text = "Duration :";
            // 
            // lbl_pnlApp_DateTimeBottomBrd
            // 
            this.lbl_pnlApp_DateTimeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlApp_DateTimeBottomBrd.Location = new System.Drawing.Point(1, 46);
            this.lbl_pnlApp_DateTimeBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlApp_DateTimeBottomBrd.Name = "lbl_pnlApp_DateTimeBottomBrd";
            this.lbl_pnlApp_DateTimeBottomBrd.Size = new System.Drawing.Size(488, 1);
            this.lbl_pnlApp_DateTimeBottomBrd.TabIndex = 116;
            // 
            // chkRecurring
            // 
            this.chkRecurring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRecurring.AutoSize = true;
            this.chkRecurring.BackColor = System.Drawing.Color.Transparent;
            this.chkRecurring.Location = new System.Drawing.Point(340, 2);
            this.chkRecurring.Margin = new System.Windows.Forms.Padding(2);
            this.chkRecurring.Name = "chkRecurring";
            this.chkRecurring.Size = new System.Drawing.Size(114, 18);
            this.chkRecurring.TabIndex = 36;
            this.chkRecurring.Text = "Setup Recurring";
            this.chkRecurring.UseVisualStyleBackColor = false;
            this.chkRecurring.Visible = false;
            // 
            // lbl_pnlApp_DateTimeTopBrd
            // 
            this.lbl_pnlApp_DateTimeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlApp_DateTimeTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlApp_DateTimeTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlApp_DateTimeTopBrd.Name = "lbl_pnlApp_DateTimeTopBrd";
            this.lbl_pnlApp_DateTimeTopBrd.Size = new System.Drawing.Size(488, 1);
            this.lbl_pnlApp_DateTimeTopBrd.TabIndex = 115;
            // 
            // lbl_pnlApp_DateTimeRightBrd
            // 
            this.lbl_pnlApp_DateTimeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlApp_DateTimeRightBrd.Location = new System.Drawing.Point(489, 0);
            this.lbl_pnlApp_DateTimeRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlApp_DateTimeRightBrd.Name = "lbl_pnlApp_DateTimeRightBrd";
            this.lbl_pnlApp_DateTimeRightBrd.Size = new System.Drawing.Size(1, 47);
            this.lbl_pnlApp_DateTimeRightBrd.TabIndex = 114;
            this.lbl_pnlApp_DateTimeRightBrd.Text = "label7";
            // 
            // lbl_pnlApp_DateTimeLeftBrd
            // 
            this.lbl_pnlApp_DateTimeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlApp_DateTimeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlApp_DateTimeLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlApp_DateTimeLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlApp_DateTimeLeftBrd.Name = "lbl_pnlApp_DateTimeLeftBrd";
            this.lbl_pnlApp_DateTimeLeftBrd.Size = new System.Drawing.Size(1, 47);
            this.lbl_pnlApp_DateTimeLeftBrd.TabIndex = 113;
            this.lbl_pnlApp_DateTimeLeftBrd.Text = "label6";
            // 
            // lblApp_Recurrence_Time
            // 
            this.lblApp_Recurrence_Time.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Recurrence_Time.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp_Recurrence_Time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblApp_Recurrence_Time.Location = new System.Drawing.Point(3, 13);
            this.lblApp_Recurrence_Time.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Recurrence_Time.Name = "lblApp_Recurrence_Time";
            this.lblApp_Recurrence_Time.Size = new System.Drawing.Size(480, 20);
            this.lblApp_Recurrence_Time.TabIndex = 60;
            this.lblApp_Recurrence_Time.Text = "Time :";
            // 
            // lblFinishDate
            // 
            this.lblFinishDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinishDate.AutoEllipsis = true;
            this.lblFinishDate.AutoSize = true;
            this.lblFinishDate.BackColor = System.Drawing.Color.Transparent;
            this.lblFinishDate.Location = new System.Drawing.Point(343, 468);
            this.lblFinishDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFinishDate.Name = "lblFinishDate";
            this.lblFinishDate.Size = new System.Drawing.Size(56, 14);
            this.lblFinishDate.TabIndex = 43;
            this.lblFinishDate.Text = "E. Date :";
            this.lblFinishDate.Visible = false;
            // 
            // btnApp_ClearReferralDoctor
            // 
            this.btnApp_ClearReferralDoctor.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearReferralDoctor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearReferralDoctor.BackgroundImage")));
            this.btnApp_ClearReferralDoctor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearReferralDoctor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearReferralDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearReferralDoctor.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearReferralDoctor.Image")));
            this.btnApp_ClearReferralDoctor.Location = new System.Drawing.Point(338, 113);
            this.btnApp_ClearReferralDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearReferralDoctor.Name = "btnApp_ClearReferralDoctor";
            this.btnApp_ClearReferralDoctor.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearReferralDoctor.TabIndex = 15;
            this.btnApp_ClearReferralDoctor.UseVisualStyleBackColor = false;
            this.btnApp_ClearReferralDoctor.Click += new System.EventHandler(this.btnApp_ClearReferralDoctor_Click);
            this.btnApp_ClearReferralDoctor.MouseLeave += new System.EventHandler(this.btnApp_ClearReferralDoctor_MouseLeave);
            this.btnApp_ClearReferralDoctor.MouseHover += new System.EventHandler(this.btnApp_ClearReferralDoctor_MouseHover);
            // 
            // btnApp_ClearPatient
            // 
            this.btnApp_ClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearPatient.BackgroundImage")));
            this.btnApp_ClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearPatient.Image")));
            this.btnApp_ClearPatient.Location = new System.Drawing.Point(338, 59);
            this.btnApp_ClearPatient.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearPatient.Name = "btnApp_ClearPatient";
            this.btnApp_ClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearPatient.TabIndex = 7;
            this.btnApp_ClearPatient.UseVisualStyleBackColor = false;
            this.btnApp_ClearPatient.Click += new System.EventHandler(this.btnApp_ClearPatient_Click);
            this.btnApp_ClearPatient.MouseLeave += new System.EventHandler(this.btnApp_ClearPatient_MouseLeave);
            this.btnApp_ClearPatient.MouseHover += new System.EventHandler(this.btnApp_ClearPatient_MouseHover);
            // 
            // lblFinishTime
            // 
            this.lblFinishTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinishTime.AutoEllipsis = true;
            this.lblFinishTime.AutoSize = true;
            this.lblFinishTime.BackColor = System.Drawing.Color.Transparent;
            this.lblFinishTime.Location = new System.Drawing.Point(455, 470);
            this.lblFinishTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFinishTime.Name = "lblFinishTime";
            this.lblFinishTime.Size = new System.Drawing.Size(57, 14);
            this.lblFinishTime.TabIndex = 60;
            this.lblFinishTime.Text = "E. Time :";
            this.lblFinishTime.Visible = false;
            // 
            // dtpApp_DateTime_EndDate
            // 
            this.dtpApp_DateTime_EndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_EndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_EndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_EndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_EndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_EndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApp_DateTime_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_EndDate.Location = new System.Drawing.Point(402, 466);
            this.dtpApp_DateTime_EndDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpApp_DateTime_EndDate.Name = "dtpApp_DateTime_EndDate";
            this.dtpApp_DateTime_EndDate.Size = new System.Drawing.Size(49, 22);
            this.dtpApp_DateTime_EndDate.TabIndex = 53;
            this.dtpApp_DateTime_EndDate.Visible = false;
            // 
            // dtpApp_DateTime_EndTime
            // 
            this.dtpApp_DateTime_EndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_EndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_EndTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_EndTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_EndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpApp_DateTime_EndTime.Location = new System.Drawing.Point(507, 465);
            this.dtpApp_DateTime_EndTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpApp_DateTime_EndTime.Name = "dtpApp_DateTime_EndTime";
            this.dtpApp_DateTime_EndTime.Size = new System.Drawing.Size(44, 22);
            this.dtpApp_DateTime_EndTime.TabIndex = 56;
            this.dtpApp_DateTime_EndTime.Visible = false;
            // 
            // lblApp_DateTime_ColorContainer
            // 
            this.lblApp_DateTime_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lblApp_DateTime_ColorContainer.Location = new System.Drawing.Point(502, 231);
            this.lblApp_DateTime_ColorContainer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime_ColorContainer.Name = "lblApp_DateTime_ColorContainer";
            this.lblApp_DateTime_ColorContainer.Size = new System.Drawing.Size(34, 22);
            this.lblApp_DateTime_ColorContainer.TabIndex = 85;
            // 
            // btnApp_ClearProvider
            // 
            this.btnApp_ClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProvider.BackgroundImage")));
            this.btnApp_ClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProvider.Image")));
            this.btnApp_ClearProvider.Location = new System.Drawing.Point(338, 32);
            this.btnApp_ClearProvider.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearProvider.Name = "btnApp_ClearProvider";
            this.btnApp_ClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearProvider.TabIndex = 3;
            this.btnApp_ClearProvider.UseVisualStyleBackColor = false;
            this.btnApp_ClearProvider.Visible = false;
            this.btnApp_ClearProvider.Click += new System.EventHandler(this.btnApp_ClearProvider_Click);
            this.btnApp_ClearProvider.MouseLeave += new System.EventHandler(this.btnApp_ClearProvider_MouseLeave);
            this.btnApp_ClearProvider.MouseHover += new System.EventHandler(this.btnApp_ClearProvider_MouseHover);
            // 
            // btnApp_ClearInsurance
            // 
            this.btnApp_ClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearInsurance.BackgroundImage")));
            this.btnApp_ClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearInsurance.Image")));
            this.btnApp_ClearInsurance.Location = new System.Drawing.Point(319, 465);
            this.btnApp_ClearInsurance.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearInsurance.Name = "btnApp_ClearInsurance";
            this.btnApp_ClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearInsurance.TabIndex = 171;
            this.btnApp_ClearInsurance.UseVisualStyleBackColor = false;
            this.btnApp_ClearInsurance.Visible = false;
            this.btnApp_ClearInsurance.Click += new System.EventHandler(this.btnApp_ClearInsurance_Click);
            this.btnApp_ClearInsurance.MouseLeave += new System.EventHandler(this.btnApp_ClearInsurance_MouseLeave);
            this.btnApp_ClearInsurance.MouseHover += new System.EventHandler(this.btnApp_ClearInsurance_MouseHover);
            // 
            // btnApp_ClearResources
            // 
            this.btnApp_ClearResources.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearResources.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearResources.BackgroundImage")));
            this.btnApp_ClearResources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearResources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearResources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearResources.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearResources.Image")));
            this.btnApp_ClearResources.Location = new System.Drawing.Point(569, 368);
            this.btnApp_ClearResources.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearResources.Name = "btnApp_ClearResources";
            this.btnApp_ClearResources.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearResources.TabIndex = 24;
            this.btnApp_ClearResources.UseVisualStyleBackColor = false;
            this.btnApp_ClearResources.Click += new System.EventHandler(this.btnApp_ClearResources_Click);
            this.btnApp_ClearResources.MouseLeave += new System.EventHandler(this.btnApp_ClearResources_MouseLeave);
            this.btnApp_ClearResources.MouseHover += new System.EventHandler(this.btnApp_ClearResources_MouseHover);
            // 
            // btnApp_ClearDateTime_Color
            // 
            this.btnApp_ClearDateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearDateTime_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearDateTime_Color.BackgroundImage")));
            this.btnApp_ClearDateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearDateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearDateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearDateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearDateTime_Color.Image")));
            this.btnApp_ClearDateTime_Color.Location = new System.Drawing.Point(569, 231);
            this.btnApp_ClearDateTime_Color.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearDateTime_Color.Name = "btnApp_ClearDateTime_Color";
            this.btnApp_ClearDateTime_Color.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearDateTime_Color.TabIndex = 19;
            this.btnApp_ClearDateTime_Color.UseVisualStyleBackColor = false;
            this.btnApp_ClearDateTime_Color.Click += new System.EventHandler(this.btnApp_ClearDateTime_Color_Click);
            this.btnApp_ClearDateTime_Color.MouseLeave += new System.EventHandler(this.btnApp_ClearDateTime_Color_MouseLeave);
            this.btnApp_ClearDateTime_Color.MouseHover += new System.EventHandler(this.btnApp_ClearDateTime_Color_MouseHover);
            // 
            // btnApp_ClearProcedures
            // 
            this.btnApp_ClearProcedures.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearProcedures.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProcedures.BackgroundImage")));
            this.btnApp_ClearProcedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearProcedures.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearProcedures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearProcedures.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProcedures.Image")));
            this.btnApp_ClearProcedures.Location = new System.Drawing.Point(569, 282);
            this.btnApp_ClearProcedures.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ClearProcedures.Name = "btnApp_ClearProcedures";
            this.btnApp_ClearProcedures.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearProcedures.TabIndex = 170;
            this.btnApp_ClearProcedures.UseVisualStyleBackColor = false;
            this.btnApp_ClearProcedures.Click += new System.EventHandler(this.btnApp_ClearProcedures_Click);
            this.btnApp_ClearProcedures.MouseLeave += new System.EventHandler(this.btnApp_ClearProcedures_MouseLeave);
            this.btnApp_ClearProcedures.MouseHover += new System.EventHandler(this.btnApp_ClearProcedures_MouseHover);
            // 
            // lblApp_DateTime_Color
            // 
            this.lblApp_DateTime_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_DateTime_Color.AutoEllipsis = true;
            this.lblApp_DateTime_Color.AutoSize = true;
            this.lblApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Color.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp_DateTime_Color.Location = new System.Drawing.Point(458, 234);
            this.lblApp_DateTime_Color.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime_Color.Name = "lblApp_DateTime_Color";
            this.lblApp_DateTime_Color.Size = new System.Drawing.Size(42, 14);
            this.lblApp_DateTime_Color.TabIndex = 81;
            this.lblApp_DateTime_Color.Text = "Color :";
            this.lblApp_DateTime_Color.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCriteria_ProviderProblemType
            // 
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.c1ProviderProblemType);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.pnlCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeRightBrd);
            this.pnlCriteria_ProviderProblemType.Controls.Add(this.lbl_pnlCriteria_ProviderProblemTypeTopBrd);
            this.pnlCriteria_ProviderProblemType.Location = new System.Drawing.Point(101, 282);
            this.pnlCriteria_ProviderProblemType.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCriteria_ProviderProblemType.Name = "pnlCriteria_ProviderProblemType";
            this.pnlCriteria_ProviderProblemType.Size = new System.Drawing.Size(434, 83);
            this.pnlCriteria_ProviderProblemType.TabIndex = 169;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeBottomBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Location = new System.Drawing.Point(1, 82);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeBottomBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.Size = new System.Drawing.Size(432, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeBottomBrd.TabIndex = 143;
            // 
            // c1ProviderProblemType
            // 
            this.c1ProviderProblemType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProviderProblemType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProviderProblemType.ColumnInfo = resources.GetString("c1ProviderProblemType.ColumnInfo");
            this.c1ProviderProblemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProviderProblemType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProviderProblemType.Location = new System.Drawing.Point(1, 19);
            this.c1ProviderProblemType.Margin = new System.Windows.Forms.Padding(2);
            this.c1ProviderProblemType.Name = "c1ProviderProblemType";
            this.c1ProviderProblemType.Rows.Count = 1;
            this.c1ProviderProblemType.Rows.DefaultSize = 21;
            this.c1ProviderProblemType.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProviderProblemType.Size = new System.Drawing.Size(432, 64);
            this.c1ProviderProblemType.StyleInfo = resources.GetString("c1ProviderProblemType.StyleInfo");
            this.c1ProviderProblemType.TabIndex = 21;
            // 
            // pnlCriteria_ProviderProblemType_Header
            // 
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlCriteria_ProviderProblemType_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd);
            this.pnlCriteria_ProviderProblemType_Header.Controls.Add(this.lblCriteria_ProviderProblemType_Header);
            this.pnlCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_ProviderProblemType_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCriteria_ProviderProblemType_Header.Name = "pnlCriteria_ProviderProblemType_Header";
            this.pnlCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(432, 18);
            this.pnlCriteria_ProviderProblemType_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.Location = new System.Drawing.Point(0, 17);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.Name = "lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd";
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.Size = new System.Drawing.Size(432, 1);
            this.lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd.TabIndex = 141;
            // 
            // lblCriteria_ProviderProblemType_Header
            // 
            this.lblCriteria_ProviderProblemType_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_ProviderProblemType_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_ProviderProblemType_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_ProviderProblemType_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_ProviderProblemType_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_ProviderProblemType_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCriteria_ProviderProblemType_Header.Name = "lblCriteria_ProviderProblemType_Header";
            this.lblCriteria_ProviderProblemType_Header.Size = new System.Drawing.Size(432, 18);
            this.lblCriteria_ProviderProblemType_Header.TabIndex = 0;
            this.lblCriteria_ProviderProblemType_Header.Text = " Problem Types";
            this.lblCriteria_ProviderProblemType_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ProviderProblemTypeLeftBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeLeftBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.TabIndex = 139;
            this.lbl_pnlCriteria_ProviderProblemTypeLeftBrd.Text = "label10";
            // 
            // lbl_pnlCriteria_ProviderProblemTypeRightBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Location = new System.Drawing.Point(433, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeRightBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.TabIndex = 140;
            this.lbl_pnlCriteria_ProviderProblemTypeRightBrd.Text = "label11";
            // 
            // lbl_pnlCriteria_ProviderProblemTypeTopBrd
            // 
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Name = "lbl_pnlCriteria_ProviderProblemTypeTopBrd";
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.Size = new System.Drawing.Size(434, 1);
            this.lbl_pnlCriteria_ProviderProblemTypeTopBrd.TabIndex = 142;
            // 
            // pnlCriteria_Resources
            // 
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesBottomBrd);
            this.pnlCriteria_Resources.Controls.Add(this.c1Resources);
            this.pnlCriteria_Resources.Controls.Add(this.pnlCriteria_Resources_Header);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesLeftBrd);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesRightBrd);
            this.pnlCriteria_Resources.Controls.Add(this.lbl_pnlCriteria_ResourcesTopBrd);
            this.pnlCriteria_Resources.Location = new System.Drawing.Point(101, 368);
            this.pnlCriteria_Resources.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCriteria_Resources.Name = "pnlCriteria_Resources";
            this.pnlCriteria_Resources.Size = new System.Drawing.Size(434, 83);
            this.pnlCriteria_Resources.TabIndex = 168;
            // 
            // lbl_pnlCriteria_ResourcesBottomBrd
            // 
            this.lbl_pnlCriteria_ResourcesBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_ResourcesBottomBrd.Location = new System.Drawing.Point(1, 82);
            this.lbl_pnlCriteria_ResourcesBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ResourcesBottomBrd.Name = "lbl_pnlCriteria_ResourcesBottomBrd";
            this.lbl_pnlCriteria_ResourcesBottomBrd.Size = new System.Drawing.Size(432, 1);
            this.lbl_pnlCriteria_ResourcesBottomBrd.TabIndex = 144;
            // 
            // c1Resources
            // 
            this.c1Resources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Resources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Resources.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Resources.ColumnInfo = resources.GetString("c1Resources.ColumnInfo");
            this.c1Resources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Resources.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Resources.Location = new System.Drawing.Point(1, 19);
            this.c1Resources.Margin = new System.Windows.Forms.Padding(2);
            this.c1Resources.Name = "c1Resources";
            this.c1Resources.Rows.Count = 1;
            this.c1Resources.Rows.DefaultSize = 21;
            this.c1Resources.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Resources.Size = new System.Drawing.Size(432, 64);
            this.c1Resources.StyleInfo = resources.GetString("c1Resources.StyleInfo");
            this.c1Resources.TabIndex = 23;
            // 
            // pnlCriteria_Resources_Header
            // 
            this.pnlCriteria_Resources_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlCriteria_Resources_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCriteria_Resources_Header.Controls.Add(this.lbl_pnlCriteria_Resources_HeaderBottomBrd);
            this.pnlCriteria_Resources_Header.Controls.Add(this.lblCriteria_Resources_Header);
            this.pnlCriteria_Resources_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria_Resources_Header.Location = new System.Drawing.Point(1, 1);
            this.pnlCriteria_Resources_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCriteria_Resources_Header.Name = "pnlCriteria_Resources_Header";
            this.pnlCriteria_Resources_Header.Size = new System.Drawing.Size(432, 18);
            this.pnlCriteria_Resources_Header.TabIndex = 137;
            // 
            // lbl_pnlCriteria_Resources_HeaderBottomBrd
            // 
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Location = new System.Drawing.Point(0, 17);
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Name = "lbl_pnlCriteria_Resources_HeaderBottomBrd";
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.Size = new System.Drawing.Size(432, 1);
            this.lbl_pnlCriteria_Resources_HeaderBottomBrd.TabIndex = 145;
            // 
            // lblCriteria_Resources_Header
            // 
            this.lblCriteria_Resources_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblCriteria_Resources_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCriteria_Resources_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria_Resources_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCriteria_Resources_Header.Location = new System.Drawing.Point(0, 0);
            this.lblCriteria_Resources_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCriteria_Resources_Header.Name = "lblCriteria_Resources_Header";
            this.lblCriteria_Resources_Header.Size = new System.Drawing.Size(432, 18);
            this.lblCriteria_Resources_Header.TabIndex = 0;
            this.lblCriteria_Resources_Header.Text = " Resources";
            this.lblCriteria_Resources_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlCriteria_ResourcesLeftBrd
            // 
            this.lbl_pnlCriteria_ResourcesLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlCriteria_ResourcesLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlCriteria_ResourcesLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlCriteria_ResourcesLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ResourcesLeftBrd.Name = "lbl_pnlCriteria_ResourcesLeftBrd";
            this.lbl_pnlCriteria_ResourcesLeftBrd.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlCriteria_ResourcesLeftBrd.TabIndex = 140;
            this.lbl_pnlCriteria_ResourcesLeftBrd.Text = "label55";
            // 
            // lbl_pnlCriteria_ResourcesRightBrd
            // 
            this.lbl_pnlCriteria_ResourcesRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlCriteria_ResourcesRightBrd.Location = new System.Drawing.Point(433, 1);
            this.lbl_pnlCriteria_ResourcesRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ResourcesRightBrd.Name = "lbl_pnlCriteria_ResourcesRightBrd";
            this.lbl_pnlCriteria_ResourcesRightBrd.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlCriteria_ResourcesRightBrd.TabIndex = 141;
            this.lbl_pnlCriteria_ResourcesRightBrd.Text = "label56";
            // 
            // lbl_pnlCriteria_ResourcesTopBrd
            // 
            this.lbl_pnlCriteria_ResourcesTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlCriteria_ResourcesTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlCriteria_ResourcesTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlCriteria_ResourcesTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlCriteria_ResourcesTopBrd.Name = "lbl_pnlCriteria_ResourcesTopBrd";
            this.lbl_pnlCriteria_ResourcesTopBrd.Size = new System.Drawing.Size(434, 1);
            this.lbl_pnlCriteria_ResourcesTopBrd.TabIndex = 146;
            // 
            // btnApp_DateTime_Color
            // 
            this.btnApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_DateTime_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.BackgroundImage")));
            this.btnApp_DateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_DateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_DateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_DateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.Image")));
            this.btnApp_DateTime_Color.Location = new System.Drawing.Point(543, 231);
            this.btnApp_DateTime_Color.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_DateTime_Color.Name = "btnApp_DateTime_Color";
            this.btnApp_DateTime_Color.Size = new System.Drawing.Size(22, 22);
            this.btnApp_DateTime_Color.TabIndex = 18;
            this.btnApp_DateTime_Color.UseVisualStyleBackColor = false;
            this.btnApp_DateTime_Color.Click += new System.EventHandler(this.btnApp_DateTime_Color_Click);
            this.btnApp_DateTime_Color.MouseLeave += new System.EventHandler(this.btnApp_DateTime_Color_MouseLeave);
            this.btnApp_DateTime_Color.MouseHover += new System.EventHandler(this.btnApp_DateTime_Color_MouseHover);
            // 
            // pnlAppointmentHeader
            // 
            this.pnlAppointmentHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlAppointmentHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAppointmentHeader.Controls.Add(this.lbl_pnlAppointmentHeaderBottomBrd);
            this.pnlAppointmentHeader.Controls.Add(this.lblAppointment);
            this.pnlAppointmentHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppointmentHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlAppointmentHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAppointmentHeader.Name = "pnlAppointmentHeader";
            this.pnlAppointmentHeader.Size = new System.Drawing.Size(598, 23);
            this.pnlAppointmentHeader.TabIndex = 89;
            // 
            // lbl_pnlAppointmentHeaderBottomBrd
            // 
            this.lbl_pnlAppointmentHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAppointmentHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlAppointmentHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlAppointmentHeaderBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlAppointmentHeaderBottomBrd.Name = "lbl_pnlAppointmentHeaderBottomBrd";
            this.lbl_pnlAppointmentHeaderBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlAppointmentHeaderBottomBrd.TabIndex = 116;
            this.lbl_pnlAppointmentHeaderBottomBrd.Text = "label4";
            // 
            // lblAppointment
            // 
            this.lblAppointment.BackColor = System.Drawing.Color.Transparent;
            this.lblAppointment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointment.ForeColor = System.Drawing.Color.White;
            this.lblAppointment.Location = new System.Drawing.Point(0, 0);
            this.lblAppointment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAppointment.Name = "lblAppointment";
            this.lblAppointment.Size = new System.Drawing.Size(598, 23);
            this.lblAppointment.TabIndex = 0;
            this.lblAppointment.Text = " Appointments";
            this.lblAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApp_Divider3
            // 
            this.lblApp_Divider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lblApp_Divider3.Location = new System.Drawing.Point(4, 477);
            this.lblApp_Divider3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Divider3.Name = "lblApp_Divider3";
            this.lblApp_Divider3.Size = new System.Drawing.Size(597, 1);
            this.lblApp_Divider3.TabIndex = 102;
            this.lblApp_Divider3.Visible = false;
            // 
            // lblApp_Divider2
            // 
            this.lblApp_Divider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lblApp_Divider2.Location = new System.Drawing.Point(3, 275);
            this.lblApp_Divider2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Divider2.Name = "lblApp_Divider2";
            this.lblApp_Divider2.Size = new System.Drawing.Size(598, 1);
            this.lblApp_Divider2.TabIndex = 101;
            // 
            // lblApp_Divider1
            // 
            this.lblApp_Divider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lblApp_Divider1.Location = new System.Drawing.Point(3, 167);
            this.lblApp_Divider1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Divider1.Name = "lblApp_Divider1";
            this.lblApp_Divider1.Size = new System.Drawing.Size(598, 1);
            this.lblApp_Divider1.TabIndex = 100;
            // 
            // btnApp_ReferralDoctor
            // 
            this.btnApp_ReferralDoctor.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ReferralDoctor.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnApp_ReferralDoctor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ReferralDoctor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ReferralDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ReferralDoctor.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ReferralDoctor.Image")));
            this.btnApp_ReferralDoctor.Location = new System.Drawing.Point(312, 113);
            this.btnApp_ReferralDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_ReferralDoctor.Name = "btnApp_ReferralDoctor";
            this.btnApp_ReferralDoctor.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ReferralDoctor.TabIndex = 14;
            this.btnApp_ReferralDoctor.UseVisualStyleBackColor = false;
            this.btnApp_ReferralDoctor.Click += new System.EventHandler(this.btnApp_ReferralDoctor_Click);
            this.btnApp_ReferralDoctor.MouseLeave += new System.EventHandler(this.btnApp_ReferralDoctor_MouseLeave);
            this.btnApp_ReferralDoctor.MouseHover += new System.EventHandler(this.btnApp_ReferralDoctor_MouseHover);
            // 
            // btnApp_Insurance
            // 
            this.btnApp_Insurance.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Insurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_Insurance.BackgroundImage")));
            this.btnApp_Insurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Insurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Insurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Insurance.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Insurance.Image")));
            this.btnApp_Insurance.Location = new System.Drawing.Point(293, 465);
            this.btnApp_Insurance.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_Insurance.Name = "btnApp_Insurance";
            this.btnApp_Insurance.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Insurance.TabIndex = 87;
            this.btnApp_Insurance.UseVisualStyleBackColor = false;
            this.btnApp_Insurance.Visible = false;
            this.btnApp_Insurance.Click += new System.EventHandler(this.btnApp_Coverage_Click);
            this.btnApp_Insurance.MouseLeave += new System.EventHandler(this.btnApp_Coverage_MouseLeave);
            this.btnApp_Insurance.MouseHover += new System.EventHandler(this.btnApp_Coverage_MouseHover);
            // 
            // btnApp_Patient
            // 
            this.btnApp_Patient.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Patient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_Patient.BackgroundImage")));
            this.btnApp_Patient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Patient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Patient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Patient.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Patient.Image")));
            this.btnApp_Patient.Location = new System.Drawing.Point(312, 59);
            this.btnApp_Patient.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_Patient.Name = "btnApp_Patient";
            this.btnApp_Patient.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Patient.TabIndex = 6;
            this.btnApp_Patient.UseVisualStyleBackColor = false;
            this.btnApp_Patient.Click += new System.EventHandler(this.btnApp_Patient_Click);
            this.btnApp_Patient.MouseLeave += new System.EventHandler(this.btnApp_Patient_MouseLeave);
            this.btnApp_Patient.MouseHover += new System.EventHandler(this.btnApp_Patient_MouseHover);
            // 
            // btnApp_Provider
            // 
            this.btnApp_Provider.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Provider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_Provider.BackgroundImage")));
            this.btnApp_Provider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Provider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Provider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Provider.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Provider.Image")));
            this.btnApp_Provider.Location = new System.Drawing.Point(312, 32);
            this.btnApp_Provider.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_Provider.Name = "btnApp_Provider";
            this.btnApp_Provider.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Provider.TabIndex = 2;
            this.btnApp_Provider.UseVisualStyleBackColor = false;
            this.btnApp_Provider.Visible = false;
            this.btnApp_Provider.Click += new System.EventHandler(this.btnApp_Provider_Click);
            this.btnApp_Provider.MouseLeave += new System.EventHandler(this.btnApp_Provider_MouseLeave);
            this.btnApp_Provider.MouseHover += new System.EventHandler(this.btnApp_Provider_MouseHover);
            // 
            // cmbApp_ReferralDoctor
            // 
            this.cmbApp_ReferralDoctor.BackColor = System.Drawing.SystemColors.Window;
            this.cmbApp_ReferralDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_ReferralDoctor.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_ReferralDoctor.FormattingEnabled = true;
            this.cmbApp_ReferralDoctor.Location = new System.Drawing.Point(125, 113);
            this.cmbApp_ReferralDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_ReferralDoctor.Name = "cmbApp_ReferralDoctor";
            this.cmbApp_ReferralDoctor.Size = new System.Drawing.Size(182, 22);
            this.cmbApp_ReferralDoctor.TabIndex = 13;
            this.cmbApp_ReferralDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbApp_ReferralDoctor_SelectedIndexChanged);
            // 
            // txtApp_Patient
            // 
            this.txtApp_Patient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtApp_Patient.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtApp_Patient.Location = new System.Drawing.Point(125, 59);
            this.txtApp_Patient.Margin = new System.Windows.Forms.Padding(2);
            this.txtApp_Patient.Name = "txtApp_Patient";
            this.txtApp_Patient.ReadOnly = true;
            this.txtApp_Patient.Size = new System.Drawing.Size(182, 22);
            this.txtApp_Patient.TabIndex = 5;
            this.txtApp_Patient.TextChanged += new System.EventHandler(this.txtApp_Patient_TextChanged);
            // 
            // lblApp_ReferralDoctor
            // 
            this.lblApp_ReferralDoctor.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_ReferralDoctor.Location = new System.Drawing.Point(11, 117);
            this.lblApp_ReferralDoctor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_ReferralDoctor.Name = "lblApp_ReferralDoctor";
            this.lblApp_ReferralDoctor.Size = new System.Drawing.Size(112, 14);
            this.lblApp_ReferralDoctor.TabIndex = 79;
            this.lblApp_ReferralDoctor.Text = "Referring Provider :";
            this.lblApp_ReferralDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApp_Status
            // 
            this.lblApp_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Status.AutoEllipsis = true;
            this.lblApp_Status.AutoSize = true;
            this.lblApp_Status.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Status.Location = new System.Drawing.Point(47, 468);
            this.lblApp_Status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Status.Name = "lblApp_Status";
            this.lblApp_Status.Size = new System.Drawing.Size(50, 14);
            this.lblApp_Status.TabIndex = 45;
            this.lblApp_Status.Text = "Status :";
            this.lblApp_Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApp_Status.Visible = false;
            // 
            // btnApp_Procedures
            // 
            this.btnApp_Procedures.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Procedures.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_Procedures.BackgroundImage")));
            this.btnApp_Procedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Procedures.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Procedures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Procedures.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Procedures.Image")));
            this.btnApp_Procedures.Location = new System.Drawing.Point(543, 282);
            this.btnApp_Procedures.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_Procedures.Name = "btnApp_Procedures";
            this.btnApp_Procedures.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Procedures.TabIndex = 20;
            this.btnApp_Procedures.UseVisualStyleBackColor = false;
            this.btnApp_Procedures.Click += new System.EventHandler(this.btnApp_Procedures_Click);
            this.btnApp_Procedures.MouseLeave += new System.EventHandler(this.btnApp_Procedures_MouseLeave);
            this.btnApp_Procedures.MouseHover += new System.EventHandler(this.btnApp_Procedures_MouseHover);
            // 
            // lblApp_Patient
            // 
            this.lblApp_Patient.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Patient.Location = new System.Drawing.Point(69, 63);
            this.lblApp_Patient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Patient.Name = "lblApp_Patient";
            this.lblApp_Patient.Size = new System.Drawing.Size(54, 14);
            this.lblApp_Patient.TabIndex = 40;
            this.lblApp_Patient.Text = "Patient :";
            this.lblApp_Patient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Status
            // 
            this.cmbApp_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Status.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Status.FormattingEnabled = true;
            this.cmbApp_Status.Location = new System.Drawing.Point(101, 465);
            this.cmbApp_Status.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_Status.Name = "cmbApp_Status";
            this.cmbApp_Status.Size = new System.Drawing.Size(63, 22);
            this.cmbApp_Status.TabIndex = 54;
            this.cmbApp_Status.Visible = false;
            // 
            // btnApp_Resources
            // 
            this.btnApp_Resources.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Resources.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnApp_Resources.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Resources.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Resources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Resources.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Resources.Image")));
            this.btnApp_Resources.Location = new System.Drawing.Point(543, 368);
            this.btnApp_Resources.Margin = new System.Windows.Forms.Padding(2);
            this.btnApp_Resources.Name = "btnApp_Resources";
            this.btnApp_Resources.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Resources.TabIndex = 22;
            this.btnApp_Resources.UseVisualStyleBackColor = false;
            this.btnApp_Resources.Click += new System.EventHandler(this.btnApp_Resources_Click);
            this.btnApp_Resources.MouseLeave += new System.EventHandler(this.btnApp_Resources_MouseLeave);
            this.btnApp_Resources.MouseHover += new System.EventHandler(this.btnApp_Resources_MouseHover);
            // 
            // lblAuthorizaionName
            // 
            this.lblAuthorizaionName.AutoSize = true;
            this.lblAuthorizaionName.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthorizaionName.Location = new System.Drawing.Point(8, 90);
            this.lblAuthorizaionName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAuthorizaionName.Name = "lblAuthorizaionName";
            this.lblAuthorizaionName.Size = new System.Drawing.Size(115, 14);
            this.lblAuthorizaionName.TabIndex = 73;
            this.lblAuthorizaionName.Text = "Prior Authorization :";
            this.lblAuthorizaionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_AppointmentType
            // 
            this.cmbApp_AppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_AppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_AppointmentType.FormattingEnabled = true;
            this.cmbApp_AppointmentType.Location = new System.Drawing.Point(449, 86);
            this.cmbApp_AppointmentType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_AppointmentType.Name = "cmbApp_AppointmentType";
            this.cmbApp_AppointmentType.Size = new System.Drawing.Size(145, 22);
            this.cmbApp_AppointmentType.TabIndex = 12;
            this.cmbApp_AppointmentType.SelectedIndexChanged += new System.EventHandler(this.cmbApp_AppointmentType_SelectedIndexChanged);
            this.cmbApp_AppointmentType.SelectionChangeCommitted += new System.EventHandler(this.cmbApp_AppointmentType_SelectionChangeCommitted);
            // 
            // lblPatientBalanceName
            // 
            this.lblPatientBalanceName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientBalanceName.AutoEllipsis = true;
            this.lblPatientBalanceName.AutoSize = true;
            this.lblPatientBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientBalanceName.Location = new System.Drawing.Point(367, 117);
            this.lblPatientBalanceName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPatientBalanceName.Name = "lblPatientBalanceName";
            this.lblPatientBalanceName.Size = new System.Drawing.Size(80, 14);
            this.lblPatientBalanceName.TabIndex = 73;
            this.lblPatientBalanceName.Text = "Patient Due :";
            this.lblPatientBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPatientBalanceName.Visible = false;
            // 
            // lblApp_AppointmentType
            // 
            this.lblApp_AppointmentType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_AppointmentType.AutoEllipsis = true;
            this.lblApp_AppointmentType.AutoSize = true;
            this.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_AppointmentType.Location = new System.Drawing.Point(404, 90);
            this.lblApp_AppointmentType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_AppointmentType.Name = "lblApp_AppointmentType";
            this.lblApp_AppointmentType.Size = new System.Drawing.Size(43, 14);
            this.lblApp_AppointmentType.TabIndex = 73;
            this.lblApp_AppointmentType.Text = "Type :";
            this.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApp_Notes
            // 
            this.lblApp_Notes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Notes.AutoEllipsis = true;
            this.lblApp_Notes.AutoSize = true;
            this.lblApp_Notes.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Notes.Location = new System.Drawing.Point(52, 231);
            this.lblApp_Notes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Notes.Name = "lblApp_Notes";
            this.lblApp_Notes.Size = new System.Drawing.Size(47, 14);
            this.lblApp_Notes.TabIndex = 61;
            this.lblApp_Notes.Text = "Notes :";
            this.lblApp_Notes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApp_Resources
            // 
            this.lblApp_Resources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Resources.AutoEllipsis = true;
            this.lblApp_Resources.AutoSize = true;
            this.lblApp_Resources.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Resources.Location = new System.Drawing.Point(34, 371);
            this.lblApp_Resources.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Resources.Name = "lblApp_Resources";
            this.lblApp_Resources.Size = new System.Drawing.Size(65, 14);
            this.lblApp_Resources.TabIndex = 71;
            this.lblApp_Resources.Text = "Resource :";
            this.lblApp_Resources.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Department
            // 
            this.cmbApp_Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Department.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Department.FormattingEnabled = true;
            this.cmbApp_Department.Location = new System.Drawing.Point(449, 59);
            this.cmbApp_Department.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_Department.Name = "cmbApp_Department";
            this.cmbApp_Department.Size = new System.Drawing.Size(145, 22);
            this.cmbApp_Department.TabIndex = 8;
            // 
            // lblApp_Department
            // 
            this.lblApp_Department.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Department.AutoEllipsis = true;
            this.lblApp_Department.AutoSize = true;
            this.lblApp_Department.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Department.Location = new System.Drawing.Point(366, 63);
            this.lblApp_Department.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Department.Name = "lblApp_Department";
            this.lblApp_Department.Size = new System.Drawing.Size(81, 14);
            this.lblApp_Department.TabIndex = 69;
            this.lblApp_Department.Text = "Department :";
            this.lblApp_Department.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Provider
            // 
            this.cmbApp_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Provider.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Provider.FormattingEnabled = true;
            this.cmbApp_Provider.Location = new System.Drawing.Point(125, 32);
            this.cmbApp_Provider.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_Provider.Name = "cmbApp_Provider";
            this.cmbApp_Provider.Size = new System.Drawing.Size(182, 22);
            this.cmbApp_Provider.TabIndex = 1;
            this.cmbApp_Provider.SelectedIndexChanged += new System.EventHandler(this.cmbApp_Provider_SelectedIndexChanged);
            // 
            // cmbApp_Location
            // 
            this.cmbApp_Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Location.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Location.FormattingEnabled = true;
            this.cmbApp_Location.Location = new System.Drawing.Point(449, 32);
            this.cmbApp_Location.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_Location.Name = "cmbApp_Location";
            this.cmbApp_Location.Size = new System.Drawing.Size(145, 22);
            this.cmbApp_Location.TabIndex = 4;
            this.cmbApp_Location.SelectedIndexChanged += new System.EventHandler(this.cmbApp_Location_SelectedIndexChanged);
            // 
            // lblApp_Coverage
            // 
            this.lblApp_Coverage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Coverage.AutoEllipsis = true;
            this.lblApp_Coverage.AutoSize = true;
            this.lblApp_Coverage.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Coverage.Location = new System.Drawing.Point(171, 468);
            this.lblApp_Coverage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Coverage.Name = "lblApp_Coverage";
            this.lblApp_Coverage.Size = new System.Drawing.Size(68, 14);
            this.lblApp_Coverage.TabIndex = 65;
            this.lblApp_Coverage.Text = "Insurance :";
            this.lblApp_Coverage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApp_Coverage.Visible = false;
            // 
            // lblApp_Location
            // 
            this.lblApp_Location.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Location.AutoEllipsis = true;
            this.lblApp_Location.AutoSize = true;
            this.lblApp_Location.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Location.Location = new System.Drawing.Point(386, 36);
            this.lblApp_Location.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Location.Name = "lblApp_Location";
            this.lblApp_Location.Size = new System.Drawing.Size(61, 14);
            this.lblApp_Location.TabIndex = 67;
            this.lblApp_Location.Text = "Location :";
            this.lblApp_Location.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Coverage
            // 
            this.cmbApp_Coverage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Coverage.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Coverage.FormattingEnabled = true;
            this.cmbApp_Coverage.Location = new System.Drawing.Point(241, 465);
            this.cmbApp_Coverage.Margin = new System.Windows.Forms.Padding(2);
            this.cmbApp_Coverage.Name = "cmbApp_Coverage";
            this.cmbApp_Coverage.Size = new System.Drawing.Size(44, 22);
            this.cmbApp_Coverage.TabIndex = 66;
            this.cmbApp_Coverage.Visible = false;
            // 
            // lblApp_Recurrence
            // 
            this.lblApp_Recurrence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Recurrence.AutoEllipsis = true;
            this.lblApp_Recurrence.AutoSize = true;
            this.lblApp_Recurrence.Location = new System.Drawing.Point(22, 176);
            this.lblApp_Recurrence.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Recurrence.Name = "lblApp_Recurrence";
            this.lblApp_Recurrence.Size = new System.Drawing.Size(77, 14);
            this.lblApp_Recurrence.TabIndex = 106;
            this.lblApp_Recurrence.Text = "Recurrence :";
            // 
            // lblApp_DateTime
            // 
            this.lblApp_DateTime.AutoSize = true;
            this.lblApp_DateTime.Location = new System.Drawing.Point(15, 176);
            this.lblApp_DateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_DateTime.Name = "lblApp_DateTime";
            this.lblApp_DateTime.Size = new System.Drawing.Size(84, 14);
            this.lblApp_DateTime.TabIndex = 105;
            this.lblApp_DateTime.Text = "Date && Time :";
            // 
            // lblApp_Procedure
            // 
            this.lblApp_Procedure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Procedure.AutoEllipsis = true;
            this.lblApp_Procedure.AutoSize = true;
            this.lblApp_Procedure.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Procedure.Location = new System.Drawing.Point(8, 285);
            this.lblApp_Procedure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Procedure.Name = "lblApp_Procedure";
            this.lblApp_Procedure.Size = new System.Drawing.Size(91, 14);
            this.lblApp_Procedure.TabIndex = 76;
            this.lblApp_Procedure.Text = "Problem Type :";
            this.lblApp_Procedure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_pnlAppointmentLeftBrd
            // 
            this.lbl_pnlAppointmentLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAppointmentLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlAppointmentLeftBrd.Location = new System.Drawing.Point(2, 3);
            this.lbl_pnlAppointmentLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlAppointmentLeftBrd.Name = "lbl_pnlAppointmentLeftBrd";
            this.lbl_pnlAppointmentLeftBrd.Size = new System.Drawing.Size(1, 457);
            this.lbl_pnlAppointmentLeftBrd.TabIndex = 112;
            this.lbl_pnlAppointmentLeftBrd.Text = "label1";
            // 
            // lbl_pnlAppointmentRightBrd
            // 
            this.lbl_pnlAppointmentRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlAppointmentRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlAppointmentRightBrd.Location = new System.Drawing.Point(601, 3);
            this.lbl_pnlAppointmentRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlAppointmentRightBrd.Name = "lbl_pnlAppointmentRightBrd";
            this.lbl_pnlAppointmentRightBrd.Size = new System.Drawing.Size(1, 457);
            this.lbl_pnlAppointmentRightBrd.TabIndex = 113;
            this.lbl_pnlAppointmentRightBrd.Text = "label2";
            // 
            // lbl_pnlAppointmentTopBrd
            // 
            this.lbl_pnlAppointmentTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlAppointmentTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlAppointmentTopBrd.Location = new System.Drawing.Point(2, 2);
            this.lbl_pnlAppointmentTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlAppointmentTopBrd.Name = "lbl_pnlAppointmentTopBrd";
            this.lbl_pnlAppointmentTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlAppointmentTopBrd.TabIndex = 114;
            this.lbl_pnlAppointmentTopBrd.Text = "label3";
            // 
            // lbl_pnlAppointmentBottomBrd
            // 
            this.lbl_pnlAppointmentBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlAppointmentBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlAppointmentBottomBrd.Location = new System.Drawing.Point(2, 460);
            this.lbl_pnlAppointmentBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlAppointmentBottomBrd.Name = "lbl_pnlAppointmentBottomBrd";
            this.lbl_pnlAppointmentBottomBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlAppointmentBottomBrd.TabIndex = 115;
            this.lbl_pnlAppointmentBottomBrd.Text = "label4";
            // 
            // lblApp_Provider
            // 
            this.lblApp_Provider.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Provider.Location = new System.Drawing.Point(64, 36);
            this.lblApp_Provider.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApp_Provider.Name = "lblApp_Provider";
            this.lblApp_Provider.Size = new System.Drawing.Size(59, 14);
            this.lblApp_Provider.TabIndex = 41;
            this.lblApp_Provider.Text = "Provider :";
            this.lblApp_Provider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlRecurring
            // 
            this.pnlRecurring.Controls.Add(this.pnlRecurring_Appointments);
            this.pnlRecurring.Controls.Add(this.pnlRecurring_Range);
            this.pnlRecurring.Controls.Add(this.pnlRecurring_Pattern);
            this.pnlRecurring.Controls.Add(this.pnlRecurring_DateTime);
            this.pnlRecurring.Controls.Add(this.pnlRecurringHeader);
            this.pnlRecurring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecurring.Location = new System.Drawing.Point(0, 0);
            this.pnlRecurring.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring.Name = "pnlRecurring";
            this.pnlRecurring.Padding = new System.Windows.Forms.Padding(2);
            this.pnlRecurring.Size = new System.Drawing.Size(604, 463);
            this.pnlRecurring.TabIndex = 66;
            // 
            // pnlRecurring_Appointments
            // 
            this.pnlRecurring_Appointments.Controls.Add(this.lvwRec_Apointments);
            this.pnlRecurring_Appointments.Controls.Add(this.pnlRecurring_Appointments_Header);
            this.pnlRecurring_Appointments.Controls.Add(this.pnlRecurring_Appointments_Exception);
            this.pnlRecurring_Appointments.Controls.Add(this.lbl_pnlRecurring_AppointmentsLeftBrd);
            this.pnlRecurring_Appointments.Controls.Add(this.lbl_pnlRecurring_AppointmentsRightBrd);
            this.pnlRecurring_Appointments.Controls.Add(this.lbl_pnlRecurring_AppointmentsTopBrd);
            this.pnlRecurring_Appointments.Controls.Add(this.lbl_pnlRecurring_AppointmentsBottomBrd);
            this.pnlRecurring_Appointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecurring_Appointments.Location = new System.Drawing.Point(2, 257);
            this.pnlRecurring_Appointments.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Appointments.Name = "pnlRecurring_Appointments";
            this.pnlRecurring_Appointments.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.pnlRecurring_Appointments.Size = new System.Drawing.Size(600, 204);
            this.pnlRecurring_Appointments.TabIndex = 118;
            // 
            // lvwRec_Apointments
            // 
            this.lvwRec_Apointments.BackColor = System.Drawing.Color.White;
            this.lvwRec_Apointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRec_Apointments.ForeColor = System.Drawing.Color.Black;
            this.lvwRec_Apointments.Location = new System.Drawing.Point(1, 26);
            this.lvwRec_Apointments.Margin = new System.Windows.Forms.Padding(2);
            this.lvwRec_Apointments.Name = "lvwRec_Apointments";
            this.lvwRec_Apointments.Size = new System.Drawing.Size(479, 177);
            this.lvwRec_Apointments.TabIndex = 118;
            this.lvwRec_Apointments.UseCompatibleStateImageBehavior = false;
            this.lvwRec_Apointments.View = System.Windows.Forms.View.Details;
            // 
            // pnlRecurring_Appointments_Header
            // 
            this.pnlRecurring_Appointments_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Appointments_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Appointments_Header.Controls.Add(this.lblRecurring_Appointments_Header);
            this.pnlRecurring_Appointments_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Appointments_Header.Location = new System.Drawing.Point(1, 3);
            this.pnlRecurring_Appointments_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Appointments_Header.Name = "pnlRecurring_Appointments_Header";
            this.pnlRecurring_Appointments_Header.Size = new System.Drawing.Size(479, 23);
            this.pnlRecurring_Appointments_Header.TabIndex = 5;
            // 
            // lblRecurring_Appointments_Header
            // 
            this.lblRecurring_Appointments_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Appointments_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Appointments_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Appointments_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Appointments_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_Appointments_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurring_Appointments_Header.Name = "lblRecurring_Appointments_Header";
            this.lblRecurring_Appointments_Header.Size = new System.Drawing.Size(479, 23);
            this.lblRecurring_Appointments_Header.TabIndex = 0;
            this.lblRecurring_Appointments_Header.Text = " Appointments";
            this.lblRecurring_Appointments_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRecurring_Appointments_Exception
            // 
            this.pnlRecurring_Appointments_Exception.Controls.Add(this.lvwRec_Exception);
            this.pnlRecurring_Appointments_Exception.Controls.Add(this.btnRec_RemoveException);
            this.pnlRecurring_Appointments_Exception.Controls.Add(this.btnRec_AddException);
            this.pnlRecurring_Appointments_Exception.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRecurring_Appointments_Exception.Location = new System.Drawing.Point(480, 3);
            this.pnlRecurring_Appointments_Exception.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Appointments_Exception.Name = "pnlRecurring_Appointments_Exception";
            this.pnlRecurring_Appointments_Exception.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pnlRecurring_Appointments_Exception.Size = new System.Drawing.Size(119, 200);
            this.pnlRecurring_Appointments_Exception.TabIndex = 107;
            this.pnlRecurring_Appointments_Exception.Visible = false;
            // 
            // lvwRec_Exception
            // 
            this.lvwRec_Exception.BackColor = System.Drawing.Color.White;
            this.lvwRec_Exception.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRec_Exception.ForeColor = System.Drawing.Color.Black;
            this.lvwRec_Exception.Location = new System.Drawing.Point(2, 23);
            this.lvwRec_Exception.Margin = new System.Windows.Forms.Padding(2);
            this.lvwRec_Exception.Name = "lvwRec_Exception";
            this.lvwRec_Exception.Size = new System.Drawing.Size(115, 154);
            this.lvwRec_Exception.TabIndex = 119;
            this.lvwRec_Exception.UseCompatibleStateImageBehavior = false;
            // 
            // btnRec_RemoveException
            // 
            this.btnRec_RemoveException.BackColor = System.Drawing.Color.Transparent;
            this.btnRec_RemoveException.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnRec_RemoveException.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRec_RemoveException.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRec_RemoveException.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRec_RemoveException.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRec_RemoveException.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec_RemoveException.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRec_RemoveException.Location = new System.Drawing.Point(2, 177);
            this.btnRec_RemoveException.Margin = new System.Windows.Forms.Padding(2);
            this.btnRec_RemoveException.Name = "btnRec_RemoveException";
            this.btnRec_RemoveException.Size = new System.Drawing.Size(115, 23);
            this.btnRec_RemoveException.TabIndex = 107;
            this.btnRec_RemoveException.Text = "Remove Exception";
            this.btnRec_RemoveException.UseVisualStyleBackColor = false;
            // 
            // btnRec_AddException
            // 
            this.btnRec_AddException.BackColor = System.Drawing.Color.Transparent;
            this.btnRec_AddException.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnRec_AddException.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRec_AddException.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRec_AddException.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRec_AddException.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRec_AddException.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec_AddException.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRec_AddException.Location = new System.Drawing.Point(2, 0);
            this.btnRec_AddException.Margin = new System.Windows.Forms.Padding(2);
            this.btnRec_AddException.Name = "btnRec_AddException";
            this.btnRec_AddException.Size = new System.Drawing.Size(115, 23);
            this.btnRec_AddException.TabIndex = 106;
            this.btnRec_AddException.Text = "Add Exception";
            this.btnRec_AddException.UseVisualStyleBackColor = false;
            // 
            // lbl_pnlRecurring_AppointmentsLeftBrd
            // 
            this.lbl_pnlRecurring_AppointmentsLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_AppointmentsLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_AppointmentsLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlRecurring_AppointmentsLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_AppointmentsLeftBrd.Name = "lbl_pnlRecurring_AppointmentsLeftBrd";
            this.lbl_pnlRecurring_AppointmentsLeftBrd.Size = new System.Drawing.Size(1, 200);
            this.lbl_pnlRecurring_AppointmentsLeftBrd.TabIndex = 119;
            // 
            // lbl_pnlRecurring_AppointmentsRightBrd
            // 
            this.lbl_pnlRecurring_AppointmentsRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_AppointmentsRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_AppointmentsRightBrd.Location = new System.Drawing.Point(599, 3);
            this.lbl_pnlRecurring_AppointmentsRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_AppointmentsRightBrd.Name = "lbl_pnlRecurring_AppointmentsRightBrd";
            this.lbl_pnlRecurring_AppointmentsRightBrd.Size = new System.Drawing.Size(1, 200);
            this.lbl_pnlRecurring_AppointmentsRightBrd.TabIndex = 120;
            // 
            // lbl_pnlRecurring_AppointmentsTopBrd
            // 
            this.lbl_pnlRecurring_AppointmentsTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_AppointmentsTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_AppointmentsTopBrd.Location = new System.Drawing.Point(0, 2);
            this.lbl_pnlRecurring_AppointmentsTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_AppointmentsTopBrd.Name = "lbl_pnlRecurring_AppointmentsTopBrd";
            this.lbl_pnlRecurring_AppointmentsTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurring_AppointmentsTopBrd.TabIndex = 121;
            // 
            // lbl_pnlRecurring_AppointmentsBottomBrd
            // 
            this.lbl_pnlRecurring_AppointmentsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_AppointmentsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_AppointmentsBottomBrd.Location = new System.Drawing.Point(0, 203);
            this.lbl_pnlRecurring_AppointmentsBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_AppointmentsBottomBrd.Name = "lbl_pnlRecurring_AppointmentsBottomBrd";
            this.lbl_pnlRecurring_AppointmentsBottomBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurring_AppointmentsBottomBrd.TabIndex = 122;
            // 
            // pnlRecurring_Range
            // 
            this.pnlRecurring_Range.Controls.Add(this.numRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeBottomBrd);
            this.pnlRecurring_Range.Controls.Add(this.pnlRecurring_Range_Header);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_EndDate);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_StartDate);
            this.pnlRecurring_Range.Controls.Add(this.rbRec_Range_EndAfterOccurence);
            this.pnlRecurring_Range.Controls.Add(this.dtpRec_Range_EndBy);
            this.pnlRecurring_Range.Controls.Add(this.lblRec_Range_Occurence);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeLeftBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd);
            this.pnlRecurring_Range.Controls.Add(this.lbl_pnlRecurring_RangeTopBrd);
            this.pnlRecurring_Range.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Range.Location = new System.Drawing.Point(2, 200);
            this.pnlRecurring_Range.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Range.Name = "pnlRecurring_Range";
            this.pnlRecurring_Range.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlRecurring_Range.Size = new System.Drawing.Size(600, 57);
            this.pnlRecurring_Range.TabIndex = 115;
            // 
            // numRec_Range_EndAfterOccurence
            // 
            this.numRec_Range_EndAfterOccurence.BackColor = System.Drawing.Color.White;
            this.numRec_Range_EndAfterOccurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Range_EndAfterOccurence.ForeColor = System.Drawing.Color.Black;
            this.numRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(314, 28);
            this.numRec_Range_EndAfterOccurence.Margin = new System.Windows.Forms.Padding(2);
            this.numRec_Range_EndAfterOccurence.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numRec_Range_EndAfterOccurence.Name = "numRec_Range_EndAfterOccurence";
            this.numRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(45, 22);
            this.numRec_Range_EndAfterOccurence.TabIndex = 13;
            this.numRec_Range_EndAfterOccurence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Range_EndAfterOccurence.ValueChanged += new System.EventHandler(this.numRec_Range_EndAfterOccurence_ValueChanged);
            // 
            // lbl_pnlRecurring_RangeBottomBrd
            // 
            this.lbl_pnlRecurring_RangeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_RangeBottomBrd.Location = new System.Drawing.Point(1, 54);
            this.lbl_pnlRecurring_RangeBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_RangeBottomBrd.Name = "lbl_pnlRecurring_RangeBottomBrd";
            this.lbl_pnlRecurring_RangeBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_RangeBottomBrd.TabIndex = 120;
            // 
            // pnlRecurring_Range_Header
            // 
            this.pnlRecurring_Range_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_Range_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_Range_Header.Controls.Add(this.lbl_pnlRecurring_Range_HeaderBottomBrd);
            this.pnlRecurring_Range_Header.Controls.Add(this.cmbRec_Range_NoEndDateYear);
            this.pnlRecurring_Range_Header.Controls.Add(this.lblRecurring_Range_Header);
            this.pnlRecurring_Range_Header.Controls.Add(this.rbRec_Range_NoEndDate);
            this.pnlRecurring_Range_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Range_Header.Location = new System.Drawing.Point(1, 3);
            this.pnlRecurring_Range_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Range_Header.Name = "pnlRecurring_Range_Header";
            this.pnlRecurring_Range_Header.Size = new System.Drawing.Size(598, 23);
            this.pnlRecurring_Range_Header.TabIndex = 5;
            // 
            // lbl_pnlRecurring_Range_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Name = "lbl_pnlRecurring_Range_HeaderBottomBrd";
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_Range_HeaderBottomBrd.TabIndex = 121;
            // 
            // cmbRec_Range_NoEndDateYear
            // 
            this.cmbRec_Range_NoEndDateYear.BackColor = System.Drawing.Color.White;
            this.cmbRec_Range_NoEndDateYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Range_NoEndDateYear.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmbRec_Range_NoEndDateYear.FormattingEnabled = true;
            this.cmbRec_Range_NoEndDateYear.Location = new System.Drawing.Point(514, 1);
            this.cmbRec_Range_NoEndDateYear.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Range_NoEndDateYear.Name = "cmbRec_Range_NoEndDateYear";
            this.cmbRec_Range_NoEndDateYear.Size = new System.Drawing.Size(58, 21);
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
            this.lblRecurring_Range_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurring_Range_Header.Name = "lblRecurring_Range_Header";
            this.lblRecurring_Range_Header.Size = new System.Drawing.Size(598, 23);
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
            this.rbRec_Range_NoEndDate.Location = new System.Drawing.Point(438, 2);
            this.rbRec_Range_NoEndDate.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Range_NoEndDate.Name = "rbRec_Range_NoEndDate";
            this.rbRec_Range_NoEndDate.Size = new System.Drawing.Size(86, 17);
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
            this.lblRec_Range_EndDate.Location = new System.Drawing.Point(172, 32);
            this.lblRec_Range_EndDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.dtpRec_Range_StartDate.Location = new System.Drawing.Point(79, 28);
            this.dtpRec_Range_StartDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRec_Range_StartDate.Name = "dtpRec_Range_StartDate";
            this.dtpRec_Range_StartDate.Size = new System.Drawing.Size(90, 22);
            this.dtpRec_Range_StartDate.TabIndex = 10;
            this.dtpRec_Range_StartDate.ValueChanged += new System.EventHandler(this.dtpRec_Range_StartDate_ValueChanged);
            this.dtpRec_Range_StartDate.Leave += new System.EventHandler(this.dtpRec_Range_StartDate_Leave);
            // 
            // rbRec_Range_EndBy
            // 
            this.rbRec_Range_EndBy.AutoSize = true;
            this.rbRec_Range_EndBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_EndBy.Location = new System.Drawing.Point(435, 30);
            this.rbRec_Range_EndBy.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Range_EndBy.Name = "rbRec_Range_EndBy";
            this.rbRec_Range_EndBy.Size = new System.Drawing.Size(63, 18);
            this.rbRec_Range_EndBy.TabIndex = 16;
            this.rbRec_Range_EndBy.Text = "End by";
            this.rbRec_Range_EndBy.UseVisualStyleBackColor = true;
            // 
            // lblRec_Range_StartDate
            // 
            this.lblRec_Range_StartDate.AutoSize = true;
            this.lblRec_Range_StartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_StartDate.Location = new System.Drawing.Point(6, 32);
            this.lblRec_Range_StartDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Range_StartDate.Name = "lblRec_Range_StartDate";
            this.lblRec_Range_StartDate.Size = new System.Drawing.Size(72, 14);
            this.lblRec_Range_StartDate.TabIndex = 9;
            this.lblRec_Range_StartDate.Text = "Start Date :";
            // 
            // rbRec_Range_EndAfterOccurence
            // 
            this.rbRec_Range_EndAfterOccurence.AutoSize = true;
            this.rbRec_Range_EndAfterOccurence.Checked = true;
            this.rbRec_Range_EndAfterOccurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Range_EndAfterOccurence.Location = new System.Drawing.Point(239, 30);
            this.rbRec_Range_EndAfterOccurence.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Range_EndAfterOccurence.Name = "rbRec_Range_EndAfterOccurence";
            this.rbRec_Range_EndAfterOccurence.Size = new System.Drawing.Size(76, 18);
            this.rbRec_Range_EndAfterOccurence.TabIndex = 15;
            this.rbRec_Range_EndAfterOccurence.TabStop = true;
            this.rbRec_Range_EndAfterOccurence.Text = "End after";
            this.rbRec_Range_EndAfterOccurence.UseVisualStyleBackColor = true;
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
            this.dtpRec_Range_EndBy.Location = new System.Drawing.Point(502, 28);
            this.dtpRec_Range_EndBy.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRec_Range_EndBy.Name = "dtpRec_Range_EndBy";
            this.dtpRec_Range_EndBy.Size = new System.Drawing.Size(93, 22);
            this.dtpRec_Range_EndBy.TabIndex = 11;
            this.dtpRec_Range_EndBy.ValueChanged += new System.EventHandler(this.dtpRec_Range_EndBy_ValueChanged);
            this.dtpRec_Range_EndBy.Leave += new System.EventHandler(this.dtpRec_Range_EndBy_Leave);
            // 
            // lblRec_Range_Occurence
            // 
            this.lblRec_Range_Occurence.AutoSize = true;
            this.lblRec_Range_Occurence.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Range_Occurence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Range_Occurence.Location = new System.Drawing.Point(358, 32);
            this.lblRec_Range_Occurence.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Range_Occurence.Name = "lblRec_Range_Occurence";
            this.lblRec_Range_Occurence.Size = new System.Drawing.Size(75, 14);
            this.lblRec_Range_Occurence.TabIndex = 12;
            this.lblRec_Range_Occurence.Text = "Occurrences";
            // 
            // lbl_pnlRecurring_RangeLeftBrd
            // 
            this.lbl_pnlRecurring_RangeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_RangeLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlRecurring_RangeLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_RangeLeftBrd.Name = "lbl_pnlRecurring_RangeLeftBrd";
            this.lbl_pnlRecurring_RangeLeftBrd.Size = new System.Drawing.Size(1, 52);
            this.lbl_pnlRecurring_RangeLeftBrd.TabIndex = 87;
            // 
            // lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd
            // 
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.Location = new System.Drawing.Point(599, 3);
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.Name = "lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd";
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.Size = new System.Drawing.Size(1, 52);
            this.lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd.TabIndex = 88;
            // 
            // lbl_pnlRecurring_RangeTopBrd
            // 
            this.lbl_pnlRecurring_RangeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_RangeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_RangeTopBrd.Location = new System.Drawing.Point(0, 2);
            this.lbl_pnlRecurring_RangeTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_RangeTopBrd.Name = "lbl_pnlRecurring_RangeTopBrd";
            this.lbl_pnlRecurring_RangeTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurring_RangeTopBrd.TabIndex = 122;
            // 
            // pnlRecurring_Pattern
            // 
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternBootomBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Yearly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRecurring_Pattern_Header);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Monthly);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Daily);
            this.pnlRecurring_Pattern.Controls.Add(this.rbRec_Pattern_Weekly);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternLeftBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternRightBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.lbl_pnlRecurring_PatternTopBrd);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Yearly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Daily);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Monthly);
            this.pnlRecurring_Pattern.Controls.Add(this.pnlRec_Pattern_Weekly);
            this.pnlRecurring_Pattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_Pattern.Location = new System.Drawing.Point(2, 86);
            this.pnlRecurring_Pattern.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Pattern.Name = "pnlRecurring_Pattern";
            this.pnlRecurring_Pattern.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlRecurring_Pattern.Size = new System.Drawing.Size(600, 114);
            this.pnlRecurring_Pattern.TabIndex = 113;
            // 
            // lbl_pnlRecurring_PatternBootomBrd
            // 
            this.lbl_pnlRecurring_PatternBootomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternBootomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_PatternBootomBrd.Location = new System.Drawing.Point(1, 111);
            this.lbl_pnlRecurring_PatternBootomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_PatternBootomBrd.Name = "lbl_pnlRecurring_PatternBootomBrd";
            this.lbl_pnlRecurring_PatternBootomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_PatternBootomBrd.TabIndex = 120;
            // 
            // rbRec_Pattern_Yearly
            // 
            this.rbRec_Pattern_Yearly.AutoSize = true;
            this.rbRec_Pattern_Yearly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Yearly.Location = new System.Drawing.Point(11, 88);
            this.rbRec_Pattern_Yearly.Margin = new System.Windows.Forms.Padding(2);
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
            this.pnlRecurring_Pattern_Header.Location = new System.Drawing.Point(1, 3);
            this.pnlRecurring_Pattern_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_Pattern_Header.Name = "pnlRecurring_Pattern_Header";
            this.pnlRecurring_Pattern_Header.Size = new System.Drawing.Size(598, 23);
            this.pnlRecurring_Pattern_Header.TabIndex = 5;
            // 
            // lbl_pnlRecurring_Pattern_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Name = "lbl_pnlRecurring_Pattern_HeaderBottomBrd";
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_Pattern_HeaderBottomBrd.TabIndex = 121;
            // 
            // lblRecurring_Pattern_Header
            // 
            this.lblRecurring_Pattern_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_Pattern_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_Pattern_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_Pattern_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_Pattern_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_Pattern_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurring_Pattern_Header.Name = "lblRecurring_Pattern_Header";
            this.lblRecurring_Pattern_Header.Size = new System.Drawing.Size(598, 23);
            this.lblRecurring_Pattern_Header.TabIndex = 0;
            this.lblRecurring_Pattern_Header.Text = " Recurrence Pattern";
            this.lblRecurring_Pattern_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbRec_Pattern_Monthly
            // 
            this.rbRec_Pattern_Monthly.AutoSize = true;
            this.rbRec_Pattern_Monthly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Monthly.Location = new System.Drawing.Point(11, 70);
            this.rbRec_Pattern_Monthly.Margin = new System.Windows.Forms.Padding(2);
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
            this.rbRec_Pattern_Daily.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Daily.Location = new System.Drawing.Point(11, 34);
            this.rbRec_Pattern_Daily.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Pattern_Daily.Name = "rbRec_Pattern_Daily";
            this.rbRec_Pattern_Daily.Size = new System.Drawing.Size(49, 18);
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
            this.rbRec_Pattern_Weekly.Location = new System.Drawing.Point(11, 52);
            this.rbRec_Pattern_Weekly.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Pattern_Weekly.Name = "rbRec_Pattern_Weekly";
            this.rbRec_Pattern_Weekly.Size = new System.Drawing.Size(65, 18);
            this.rbRec_Pattern_Weekly.TabIndex = 2;
            this.rbRec_Pattern_Weekly.Text = "Weekly";
            this.rbRec_Pattern_Weekly.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Weekly.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Weekly_CheckedChanged);
            // 
            // lbl_pnlRecurring_PatternLeftBrd
            // 
            this.lbl_pnlRecurring_PatternLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_PatternLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlRecurring_PatternLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_PatternLeftBrd.Name = "lbl_pnlRecurring_PatternLeftBrd";
            this.lbl_pnlRecurring_PatternLeftBrd.Size = new System.Drawing.Size(1, 109);
            this.lbl_pnlRecurring_PatternLeftBrd.TabIndex = 87;
            // 
            // lbl_pnlRecurring_PatternRightBrd
            // 
            this.lbl_pnlRecurring_PatternRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_PatternRightBrd.Location = new System.Drawing.Point(599, 3);
            this.lbl_pnlRecurring_PatternRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_PatternRightBrd.Name = "lbl_pnlRecurring_PatternRightBrd";
            this.lbl_pnlRecurring_PatternRightBrd.Size = new System.Drawing.Size(1, 109);
            this.lbl_pnlRecurring_PatternRightBrd.TabIndex = 88;
            // 
            // lbl_pnlRecurring_PatternTopBrd
            // 
            this.lbl_pnlRecurring_PatternTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_PatternTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_PatternTopBrd.Location = new System.Drawing.Point(0, 2);
            this.lbl_pnlRecurring_PatternTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_PatternTopBrd.Name = "lbl_pnlRecurring_PatternTopBrd";
            this.lbl_pnlRecurring_PatternTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurring_PatternTopBrd.TabIndex = 122;
            // 
            // pnlRec_Pattern_Yearly
            // 
            this.pnlRec_Pattern_Yearly.Controls.Add(this.label29);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.label28);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.label27);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.label26);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.numRec_Pattern_Yearly_Every_MonthDay);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_Month);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Every_Month);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_DayWeekday);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.cmbRec_Pattern_Yearly_Criteria_FstLst);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.lblRec_Pattern_Yearly_Criteria_Of);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.rbRec_Pattern_Yearly_Criteria);
            this.pnlRec_Pattern_Yearly.Controls.Add(this.rbRec_Pattern_Yearly_EveryMonthDay);
            this.pnlRec_Pattern_Yearly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Yearly.Location = new System.Drawing.Point(82, 31);
            this.pnlRec_Pattern_Yearly.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRec_Pattern_Yearly.Name = "pnlRec_Pattern_Yearly";
            this.pnlRec_Pattern_Yearly.Size = new System.Drawing.Size(484, 72);
            this.pnlRec_Pattern_Yearly.TabIndex = 8;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(483, 1);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 70);
            this.label29.TabIndex = 87;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 1);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 70);
            this.label28.TabIndex = 86;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Location = new System.Drawing.Point(0, 71);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(484, 1);
            this.label27.TabIndex = 85;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(484, 1);
            this.label26.TabIndex = 84;
            // 
            // numRec_Pattern_Yearly_Every_MonthDay
            // 
            this.numRec_Pattern_Yearly_Every_MonthDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Yearly_Every_MonthDay.Location = new System.Drawing.Point(160, 13);
            this.numRec_Pattern_Yearly_Every_MonthDay.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Yearly_Every_MonthDay.Size = new System.Drawing.Size(38, 22);
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
            this.cmbRec_Pattern_Yearly_Criteria_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Yearly_Criteria_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_Month.Location = new System.Drawing.Point(272, 42);
            this.cmbRec_Pattern_Yearly_Criteria_Month.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Yearly_Criteria_Month.Name = "cmbRec_Pattern_Yearly_Criteria_Month";
            this.cmbRec_Pattern_Yearly_Criteria_Month.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Yearly_Criteria_Month.TabIndex = 25;
            this.cmbRec_Pattern_Yearly_Criteria_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Every_Month
            // 
            this.cmbRec_Pattern_Yearly_Every_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Every_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Yearly_Every_Month.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Every_Month.Location = new System.Drawing.Point(66, 13);
            this.cmbRec_Pattern_Yearly_Every_Month.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Yearly_Every_Month.Name = "cmbRec_Pattern_Yearly_Every_Month";
            this.cmbRec_Pattern_Yearly_Every_Month.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Yearly_Every_Month.TabIndex = 24;
            this.cmbRec_Pattern_Yearly_Every_Month.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Every_Month_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_DayWeekday
            // 
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Location = new System.Drawing.Point(159, 42);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Yearly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.TabIndex = 23;
            this.cmbRec_Pattern_Yearly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Yearly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Location = new System.Drawing.Point(66, 42);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Name = "cmbRec_Pattern_Yearly_Criteria_FstLst";
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.TabIndex = 22;
            this.cmbRec_Pattern_Yearly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Yearly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Yearly_Criteria_Of
            // 
            this.lblRec_Pattern_Yearly_Criteria_Of.AutoSize = true;
            this.lblRec_Pattern_Yearly_Criteria_Of.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Yearly_Criteria_Of.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Yearly_Criteria_Of.Location = new System.Drawing.Point(250, 46);
            this.lblRec_Pattern_Yearly_Criteria_Of.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Yearly_Criteria_Of.Name = "lblRec_Pattern_Yearly_Criteria_Of";
            this.lblRec_Pattern_Yearly_Criteria_Of.Size = new System.Drawing.Size(18, 14);
            this.lblRec_Pattern_Yearly_Criteria_Of.TabIndex = 19;
            this.lblRec_Pattern_Yearly_Criteria_Of.Text = "of";
            // 
            // rbRec_Pattern_Yearly_Criteria
            // 
            this.rbRec_Pattern_Yearly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Yearly_Criteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Yearly_Criteria.Location = new System.Drawing.Point(11, 44);
            this.rbRec_Pattern_Yearly_Criteria.Margin = new System.Windows.Forms.Padding(2);
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
            this.rbRec_Pattern_Yearly_EveryMonthDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Yearly_EveryMonthDay.Location = new System.Drawing.Point(10, 15);
            this.rbRec_Pattern_Yearly_EveryMonthDay.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Pattern_Yearly_EveryMonthDay.Name = "rbRec_Pattern_Yearly_EveryMonthDay";
            this.rbRec_Pattern_Yearly_EveryMonthDay.Size = new System.Drawing.Size(55, 18);
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabIndex = 2;
            this.rbRec_Pattern_Yearly_EveryMonthDay.TabStop = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.Text = "Every";
            this.rbRec_Pattern_Yearly_EveryMonthDay.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Yearly_EveryMonthDay.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Yearly_EveryMonthDay_CheckedChanged);
            // 
            // pnlRec_Pattern_Daily
            // 
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyBottomBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyTopBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyRightBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lbl_pnlRec_Pattern_DailyLeftBrd);
            this.pnlRec_Pattern_Daily.Controls.Add(this.numRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Controls.Add(this.lblRec_Pattern_Daily_Days);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryWeekday);
            this.pnlRec_Pattern_Daily.Controls.Add(this.rbRec_Pattern_Daily_EveryDay);
            this.pnlRec_Pattern_Daily.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Daily.Location = new System.Drawing.Point(82, 31);
            this.pnlRec_Pattern_Daily.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRec_Pattern_Daily.Name = "pnlRec_Pattern_Daily";
            this.pnlRec_Pattern_Daily.Size = new System.Drawing.Size(484, 72);
            this.pnlRec_Pattern_Daily.TabIndex = 5;
            this.pnlRec_Pattern_Daily.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRec_Pattern_Daily_Paint);
            // 
            // lbl_pnlRec_Pattern_DailyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_DailyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Location = new System.Drawing.Point(1, 71);
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Name = "lbl_pnlRec_Pattern_DailyBottomBrd";
            this.lbl_pnlRec_Pattern_DailyBottomBrd.Size = new System.Drawing.Size(482, 1);
            this.lbl_pnlRec_Pattern_DailyBottomBrd.TabIndex = 21;
            // 
            // lbl_pnlRec_Pattern_DailyTopBrd
            // 
            this.lbl_pnlRec_Pattern_DailyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_DailyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_DailyTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_DailyTopBrd.Name = "lbl_pnlRec_Pattern_DailyTopBrd";
            this.lbl_pnlRec_Pattern_DailyTopBrd.Size = new System.Drawing.Size(482, 1);
            this.lbl_pnlRec_Pattern_DailyTopBrd.TabIndex = 20;
            // 
            // lbl_pnlRec_Pattern_DailyRightBrd
            // 
            this.lbl_pnlRec_Pattern_DailyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_DailyRightBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlRec_Pattern_DailyRightBrd.Location = new System.Drawing.Point(483, 0);
            this.lbl_pnlRec_Pattern_DailyRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_DailyRightBrd.Name = "lbl_pnlRec_Pattern_DailyRightBrd";
            this.lbl_pnlRec_Pattern_DailyRightBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlRec_Pattern_DailyRightBrd.TabIndex = 19;
            // 
            // lbl_pnlRec_Pattern_DailyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_DailyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Name = "lbl_pnlRec_Pattern_DailyLeftBrd";
            this.lbl_pnlRec_Pattern_DailyLeftBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlRec_Pattern_DailyLeftBrd.TabIndex = 18;
            // 
            // numRec_Pattern_Daily_EveryDay
            // 
            this.numRec_Pattern_Daily_EveryDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Daily_EveryDay.ForeColor = System.Drawing.Color.Black;
            this.numRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(67, 13);
            this.numRec_Pattern_Daily_EveryDay.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Daily_EveryDay.Size = new System.Drawing.Size(38, 22);
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
            this.lblRec_Pattern_Daily_Days.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Daily_Days.Location = new System.Drawing.Point(112, 16);
            this.lblRec_Pattern_Daily_Days.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Daily_Days.Name = "lblRec_Pattern_Daily_Days";
            this.lblRec_Pattern_Daily_Days.Size = new System.Drawing.Size(41, 14);
            this.lblRec_Pattern_Daily_Days.TabIndex = 14;
            this.lblRec_Pattern_Daily_Days.Text = "day(s)";
            // 
            // rbRec_Pattern_Daily_EveryWeekday
            // 
            this.rbRec_Pattern_Daily_EveryWeekday.AutoSize = true;
            this.rbRec_Pattern_Daily_EveryWeekday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Daily_EveryWeekday.Location = new System.Drawing.Point(16, 42);
            this.rbRec_Pattern_Daily_EveryWeekday.Margin = new System.Windows.Forms.Padding(2);
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
            this.rbRec_Pattern_Daily_EveryDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Daily_EveryDay.Location = new System.Drawing.Point(16, 14);
            this.rbRec_Pattern_Daily_EveryDay.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Pattern_Daily_EveryDay.Name = "rbRec_Pattern_Daily_EveryDay";
            this.rbRec_Pattern_Daily_EveryDay.Size = new System.Drawing.Size(55, 18);
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
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyBottomBrd);
            this.pnlRec_Pattern_Monthly.Controls.Add(this.lbl_pnlRec_Pattern_MonthlyTopBrd);
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
            this.pnlRec_Pattern_Monthly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Monthly.Location = new System.Drawing.Point(82, 31);
            this.pnlRec_Pattern_Monthly.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRec_Pattern_Monthly.Name = "pnlRec_Pattern_Monthly";
            this.pnlRec_Pattern_Monthly.Size = new System.Drawing.Size(484, 72);
            this.pnlRec_Pattern_Monthly.TabIndex = 7;
            // 
            // lbl_pnlRec_Pattern_MonthlyRightBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Location = new System.Drawing.Point(483, 1);
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Name = "lbl_pnlRec_Pattern_MonthlyRightBrd";
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.Size = new System.Drawing.Size(1, 70);
            this.lbl_pnlRec_Pattern_MonthlyRightBrd.TabIndex = 88;
            // 
            // lbl_pnlRec_Pattern_MonthlyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Name = "lbl_pnlRec_Pattern_MonthlyLeftBrd";
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.Size = new System.Drawing.Size(1, 70);
            this.lbl_pnlRec_Pattern_MonthlyLeftBrd.TabIndex = 87;
            // 
            // lbl_pnlRec_Pattern_MonthlyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Location = new System.Drawing.Point(0, 71);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Name = "lbl_pnlRec_Pattern_MonthlyBottomBrd";
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.Size = new System.Drawing.Size(484, 1);
            this.lbl_pnlRec_Pattern_MonthlyBottomBrd.TabIndex = 86;
            // 
            // lbl_pnlRec_Pattern_MonthlyTopBrd
            // 
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Name = "lbl_pnlRec_Pattern_MonthlyTopBrd";
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.Size = new System.Drawing.Size(484, 1);
            this.lbl_pnlRec_Pattern_MonthlyTopBrd.TabIndex = 85;
            // 
            // numRec_Pattern_Monthly_Criteria_Month
            // 
            this.numRec_Pattern_Monthly_Criteria_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(295, 41);
            this.numRec_Pattern_Monthly_Criteria_Month.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(38, 22);
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
            this.numRec_Pattern_Monthly_Day_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(151, 12);
            this.numRec_Pattern_Monthly_Day_Month.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Monthly_Day_Month.Size = new System.Drawing.Size(38, 22);
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
            this.numRec_Pattern_Monthly_Day_Day.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_Pattern_Monthly_Day_Day.Location = new System.Drawing.Point(58, 12);
            this.numRec_Pattern_Monthly_Day_Day.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Monthly_Day_Day.Size = new System.Drawing.Size(38, 22);
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
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.FormattingEnabled = true;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Location = new System.Drawing.Point(151, 41);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Name = "cmbRec_Pattern_Monthly_Criteria_DayWeekday";
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.TabIndex = 23;
            this.cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_DayWeekday_SelectedIndexChanged);
            // 
            // cmbRec_Pattern_Monthly_Criteria_FstLst
            // 
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.FormattingEnabled = true;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Location = new System.Drawing.Point(58, 41);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Name = "cmbRec_Pattern_Monthly_Criteria_FstLst";
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.Size = new System.Drawing.Size(87, 22);
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.TabIndex = 22;
            this.cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedIndexChanged += new System.EventHandler(this.cmbRec_Pattern_Monthly_Criteria_FstLst_SelectedIndexChanged);
            // 
            // lblRec_Pattern_Monthly_Criteria_Month
            // 
            this.lblRec_Pattern_Monthly_Criteria_Month.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Month.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Monthly_Criteria_Month.Location = new System.Drawing.Point(338, 44);
            this.lblRec_Pattern_Monthly_Criteria_Month.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Monthly_Criteria_Month.Name = "lblRec_Pattern_Monthly_Criteria_Month";
            this.lblRec_Pattern_Monthly_Criteria_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Criteria_Month.TabIndex = 21;
            this.lblRec_Pattern_Monthly_Criteria_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Criteria_Every
            // 
            this.lblRec_Pattern_Monthly_Criteria_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Criteria_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Criteria_Every.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Monthly_Criteria_Every.Location = new System.Drawing.Point(246, 44);
            this.lblRec_Pattern_Monthly_Criteria_Every.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Monthly_Criteria_Every.Name = "lblRec_Pattern_Monthly_Criteria_Every";
            this.lblRec_Pattern_Monthly_Criteria_Every.Size = new System.Drawing.Size(56, 14);
            this.lblRec_Pattern_Monthly_Criteria_Every.TabIndex = 19;
            this.lblRec_Pattern_Monthly_Criteria_Every.Text = "of every ";
            // 
            // rbRec_Pattern_Monthly_Criteria
            // 
            this.rbRec_Pattern_Monthly_Criteria.AutoSize = true;
            this.rbRec_Pattern_Monthly_Criteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRec_Pattern_Monthly_Criteria.Location = new System.Drawing.Point(13, 42);
            this.rbRec_Pattern_Monthly_Criteria.Margin = new System.Windows.Forms.Padding(2);
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
            this.lblRec_Pattern_Monthly_Day_Month.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Monthly_Day_Month.Location = new System.Drawing.Point(195, 15);
            this.lblRec_Pattern_Monthly_Day_Month.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Monthly_Day_Month.Name = "lblRec_Pattern_Monthly_Day_Month";
            this.lblRec_Pattern_Monthly_Day_Month.Size = new System.Drawing.Size(58, 14);
            this.lblRec_Pattern_Monthly_Day_Month.TabIndex = 16;
            this.lblRec_Pattern_Monthly_Day_Month.Text = "month(s)";
            // 
            // lblRec_Pattern_Monthly_Day_Every
            // 
            this.lblRec_Pattern_Monthly_Day_Every.AutoSize = true;
            this.lblRec_Pattern_Monthly_Day_Every.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Monthly_Day_Every.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Monthly_Day_Every.Location = new System.Drawing.Point(102, 15);
            this.lblRec_Pattern_Monthly_Day_Every.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.rbRec_Pattern_Monthly_Day.Location = new System.Drawing.Point(12, 14);
            this.rbRec_Pattern_Monthly_Day.Margin = new System.Windows.Forms.Padding(2);
            this.rbRec_Pattern_Monthly_Day.Name = "rbRec_Pattern_Monthly_Day";
            this.rbRec_Pattern_Monthly_Day.Size = new System.Drawing.Size(45, 18);
            this.rbRec_Pattern_Monthly_Day.TabIndex = 2;
            this.rbRec_Pattern_Monthly_Day.TabStop = true;
            this.rbRec_Pattern_Monthly_Day.Text = "Day";
            this.rbRec_Pattern_Monthly_Day.UseVisualStyleBackColor = true;
            this.rbRec_Pattern_Monthly_Day.CheckedChanged += new System.EventHandler(this.rbRec_Pattern_Monthly_Day_CheckedChanged);
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
            this.pnlRec_Pattern_Weekly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRec_Pattern_Weekly.Location = new System.Drawing.Point(82, 31);
            this.pnlRec_Pattern_Weekly.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRec_Pattern_Weekly.Name = "pnlRec_Pattern_Weekly";
            this.pnlRec_Pattern_Weekly.Size = new System.Drawing.Size(484, 72);
            this.pnlRec_Pattern_Weekly.TabIndex = 6;
            // 
            // lbl_pnlRec_Pattern_WeeklyBottomBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Location = new System.Drawing.Point(1, 71);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Name = "lbl_pnlRec_Pattern_WeeklyBottomBrd";
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.Size = new System.Drawing.Size(482, 1);
            this.lbl_pnlRec_Pattern_WeeklyBottomBrd.TabIndex = 84;
            // 
            // lbl_pnlRec_Pattern_WeeklyTopBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Name = "lbl_pnlRec_Pattern_WeeklyTopBrd";
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.Size = new System.Drawing.Size(482, 1);
            this.lbl_pnlRec_Pattern_WeeklyTopBrd.TabIndex = 83;
            // 
            // lbl_pnlRec_Pattern_WeeklyRightBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Location = new System.Drawing.Point(483, 0);
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Name = "lbl_pnlRec_Pattern_WeeklyRightBrd";
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlRec_Pattern_WeeklyRightBrd.TabIndex = 82;
            // 
            // lbl_pnlRec_Pattern_WeeklyLeftBrd
            // 
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Name = "lbl_pnlRec_Pattern_WeeklyLeftBrd";
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.Size = new System.Drawing.Size(1, 72);
            this.lbl_pnlRec_Pattern_WeeklyLeftBrd.TabIndex = 81;
            // 
            // lblRec_Pattern_Weekly_WeekOn
            // 
            this.lblRec_Pattern_Weekly_WeekOn.AutoSize = true;
            this.lblRec_Pattern_Weekly_WeekOn.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_Pattern_Weekly_WeekOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(114, 10);
            this.lblRec_Pattern_Weekly_WeekOn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            this.lblRec_Pattern_Weekly_RecurEvery.Location = new System.Drawing.Point(1, 10);
            this.lblRec_Pattern_Weekly_RecurEvery.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_Pattern_Weekly_RecurEvery.Name = "lblRec_Pattern_Weekly_RecurEvery";
            this.lblRec_Pattern_Weekly_RecurEvery.Size = new System.Drawing.Size(72, 14);
            this.lblRec_Pattern_Weekly_RecurEvery.TabIndex = 14;
            this.lblRec_Pattern_Weekly_RecurEvery.Text = "Recur every";
            // 
            // ChkRec_Pattern_Weekly_Saturday
            // 
            this.ChkRec_Pattern_Weekly_Saturday.AutoSize = true;
            this.ChkRec_Pattern_Weekly_Saturday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRec_Pattern_Weekly_Saturday.Location = new System.Drawing.Point(318, 42);
            this.ChkRec_Pattern_Weekly_Saturday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Friday.Location = new System.Drawing.Point(255, 42);
            this.ChkRec_Pattern_Weekly_Friday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Sunday.Location = new System.Drawing.Point(184, 9);
            this.ChkRec_Pattern_Weekly_Sunday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Tuesday.Location = new System.Drawing.Point(318, 9);
            this.ChkRec_Pattern_Weekly_Tuesday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Wednesday.Location = new System.Drawing.Point(391, 9);
            this.ChkRec_Pattern_Weekly_Wednesday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Thursday.Location = new System.Drawing.Point(184, 42);
            this.ChkRec_Pattern_Weekly_Thursday.Margin = new System.Windows.Forms.Padding(2);
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
            this.ChkRec_Pattern_Weekly_Monday.Location = new System.Drawing.Point(255, 9);
            this.ChkRec_Pattern_Weekly_Monday.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Weekly_WeekOn.Location = new System.Drawing.Point(74, 7);
            this.numRec_Pattern_Weekly_WeekOn.Margin = new System.Windows.Forms.Padding(2);
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
            this.numRec_Pattern_Weekly_WeekOn.Size = new System.Drawing.Size(38, 22);
            this.numRec_Pattern_Weekly_WeekOn.TabIndex = 80;
            this.numRec_Pattern_Weekly_WeekOn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_Pattern_Weekly_WeekOn.ValueChanged += new System.EventHandler(this.numRec_Pattern_Weekly_WeekOn_ValueChanged);
            // 
            // pnlRecurring_DateTime
            // 
            this.pnlRecurring_DateTime.Controls.Add(this.chkRec_DateTime_IsAllDayEvent);
            this.pnlRecurring_DateTime.Controls.Add(this.lbl_pnlRecurring_DateTimeBottomBrd);
            this.pnlRecurring_DateTime.Controls.Add(this.lblRec_DateTime_ColorCode);
            this.pnlRecurring_DateTime.Controls.Add(this.btnRec_DateTime_Color);
            this.pnlRecurring_DateTime.Controls.Add(this.pnlRecurring_DateTime_Header);
            this.pnlRecurring_DateTime.Controls.Add(this.numRec_DateTime_Duration);
            this.pnlRecurring_DateTime.Controls.Add(this.dtpRec_DateTime_EndTime);
            this.pnlRecurring_DateTime.Controls.Add(this.lblRec_DateTime_EndDate);
            this.pnlRecurring_DateTime.Controls.Add(this.dtpRec_DateTime_StartTime);
            this.pnlRecurring_DateTime.Controls.Add(this.lbl_pnlRecurring_DateTimeLeftBrd);
            this.pnlRecurring_DateTime.Controls.Add(this.lbl_pnlRecurring_DateTimeRightBrd);
            this.pnlRecurring_DateTime.Controls.Add(this.lbl_pnlRecurring_DateTimeTopBrd);
            this.pnlRecurring_DateTime.Controls.Add(this.lblRec_DateTime_StartDate);
            this.pnlRecurring_DateTime.Controls.Add(this.lblRec_DateTime_Duration);
            this.pnlRecurring_DateTime.Controls.Add(this.lblRec_DateTime_ColorContainer);
            this.pnlRecurring_DateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_DateTime.Location = new System.Drawing.Point(2, 25);
            this.pnlRecurring_DateTime.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_DateTime.Name = "pnlRecurring_DateTime";
            this.pnlRecurring_DateTime.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.pnlRecurring_DateTime.Size = new System.Drawing.Size(600, 61);
            this.pnlRecurring_DateTime.TabIndex = 111;
            // 
            // chkRec_DateTime_IsAllDayEvent
            // 
            this.chkRec_DateTime_IsAllDayEvent.AutoSize = true;
            this.chkRec_DateTime_IsAllDayEvent.BackColor = System.Drawing.Color.Transparent;
            this.chkRec_DateTime_IsAllDayEvent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRec_DateTime_IsAllDayEvent.Location = new System.Drawing.Point(477, 35);
            this.chkRec_DateTime_IsAllDayEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkRec_DateTime_IsAllDayEvent.Name = "chkRec_DateTime_IsAllDayEvent";
            this.chkRec_DateTime_IsAllDayEvent.Size = new System.Drawing.Size(111, 18);
            this.chkRec_DateTime_IsAllDayEvent.TabIndex = 89;
            this.chkRec_DateTime_IsAllDayEvent.Text = "Is All Day Event";
            this.chkRec_DateTime_IsAllDayEvent.UseVisualStyleBackColor = false;
            this.chkRec_DateTime_IsAllDayEvent.CheckedChanged += new System.EventHandler(this.chkRec_DateTime_IsAllDayEvent_CheckedChanged);
            // 
            // lbl_pnlRecurring_DateTimeBottomBrd
            // 
            this.lbl_pnlRecurring_DateTimeBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_DateTimeBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_DateTimeBottomBrd.Location = new System.Drawing.Point(1, 58);
            this.lbl_pnlRecurring_DateTimeBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_DateTimeBottomBrd.Name = "lbl_pnlRecurring_DateTimeBottomBrd";
            this.lbl_pnlRecurring_DateTimeBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_DateTimeBottomBrd.TabIndex = 120;
            // 
            // lblRec_DateTime_ColorCode
            // 
            this.lblRec_DateTime_ColorCode.AutoSize = true;
            this.lblRec_DateTime_ColorCode.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_DateTime_ColorCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_DateTime_ColorCode.Location = new System.Drawing.Point(513, 36);
            this.lblRec_DateTime_ColorCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_DateTime_ColorCode.Name = "lblRec_DateTime_ColorCode";
            this.lblRec_DateTime_ColorCode.Size = new System.Drawing.Size(42, 14);
            this.lblRec_DateTime_ColorCode.TabIndex = 86;
            this.lblRec_DateTime_ColorCode.Text = "Color :";
            this.lblRec_DateTime_ColorCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRec_DateTime_ColorCode.Visible = false;
            // 
            // btnRec_DateTime_Color
            // 
            this.btnRec_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnRec_DateTime_Color.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.btnRec_DateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRec_DateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnRec_DateTime_Color.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnRec_DateTime_Color.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnRec_DateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRec_DateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnRec_DateTime_Color.Image")));
            this.btnRec_DateTime_Color.Location = new System.Drawing.Point(551, 36);
            this.btnRec_DateTime_Color.Margin = new System.Windows.Forms.Padding(2);
            this.btnRec_DateTime_Color.Name = "btnRec_DateTime_Color";
            this.btnRec_DateTime_Color.Size = new System.Drawing.Size(22, 15);
            this.btnRec_DateTime_Color.TabIndex = 87;
            this.btnRec_DateTime_Color.UseVisualStyleBackColor = false;
            this.btnRec_DateTime_Color.Visible = false;
            this.btnRec_DateTime_Color.Click += new System.EventHandler(this.btnRec_DateTime_Color_Click);
            this.btnRec_DateTime_Color.MouseLeave += new System.EventHandler(this.btnRec_DateTime_Color_MouseLeave);
            this.btnRec_DateTime_Color.MouseHover += new System.EventHandler(this.btnRec_DateTime_Color_MouseHover);
            // 
            // pnlRecurring_DateTime_Header
            // 
            this.pnlRecurring_DateTime_Header.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlRecurring_DateTime_Header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurring_DateTime_Header.Controls.Add(this.lbl_pnlRecurring_DateTime_HeaderBottomBrd);
            this.pnlRecurring_DateTime_Header.Controls.Add(this.lblRecurring_DateTime_Header);
            this.pnlRecurring_DateTime_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurring_DateTime_Header.ForeColor = System.Drawing.Color.Black;
            this.pnlRecurring_DateTime_Header.Location = new System.Drawing.Point(1, 3);
            this.pnlRecurring_DateTime_Header.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurring_DateTime_Header.Name = "pnlRecurring_DateTime_Header";
            this.pnlRecurring_DateTime_Header.Size = new System.Drawing.Size(598, 23);
            this.pnlRecurring_DateTime_Header.TabIndex = 5;
            // 
            // lbl_pnlRecurring_DateTime_HeaderBottomBrd
            // 
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.Name = "lbl_pnlRecurring_DateTime_HeaderBottomBrd";
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.Size = new System.Drawing.Size(598, 1);
            this.lbl_pnlRecurring_DateTime_HeaderBottomBrd.TabIndex = 121;
            // 
            // lblRecurring_DateTime_Header
            // 
            this.lblRecurring_DateTime_Header.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring_DateTime_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring_DateTime_Header.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring_DateTime_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRecurring_DateTime_Header.Location = new System.Drawing.Point(0, 0);
            this.lblRecurring_DateTime_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurring_DateTime_Header.Name = "lblRecurring_DateTime_Header";
            this.lblRecurring_DateTime_Header.Size = new System.Drawing.Size(598, 23);
            this.lblRecurring_DateTime_Header.TabIndex = 0;
            this.lblRecurring_DateTime_Header.Text = " Appointment Time";
            this.lblRecurring_DateTime_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numRec_DateTime_Duration
            // 
            this.numRec_DateTime_Duration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRec_DateTime_Duration.ForeColor = System.Drawing.Color.Black;
            this.numRec_DateTime_Duration.Location = new System.Drawing.Point(381, 31);
            this.numRec_DateTime_Duration.Margin = new System.Windows.Forms.Padding(2);
            this.numRec_DateTime_Duration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numRec_DateTime_Duration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_DateTime_Duration.Name = "numRec_DateTime_Duration";
            this.numRec_DateTime_Duration.Size = new System.Drawing.Size(46, 22);
            this.numRec_DateTime_Duration.TabIndex = 16;
            this.numRec_DateTime_Duration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRec_DateTime_Duration.ValueChanged += new System.EventHandler(this.numRec_DateTime_Duration_ValueChanged);
            // 
            // dtpRec_DateTime_EndTime
            // 
            this.dtpRec_DateTime_EndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRec_DateTime_EndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_DateTime_EndTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRec_DateTime_EndTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRec_DateTime_EndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRec_DateTime_EndTime.CustomFormat = "hh:mm tt";
            this.dtpRec_DateTime_EndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRec_DateTime_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_DateTime_EndTime.Location = new System.Drawing.Point(236, 31);
            this.dtpRec_DateTime_EndTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRec_DateTime_EndTime.Name = "dtpRec_DateTime_EndTime";
            this.dtpRec_DateTime_EndTime.ShowUpDown = true;
            this.dtpRec_DateTime_EndTime.Size = new System.Drawing.Size(80, 22);
            this.dtpRec_DateTime_EndTime.TabIndex = 14;
            this.dtpRec_DateTime_EndTime.ValueChanged += new System.EventHandler(this.dtpRec_DateTime_EndTime_ValueChanged);
            // 
            // lblRec_DateTime_EndDate
            // 
            this.lblRec_DateTime_EndDate.AutoSize = true;
            this.lblRec_DateTime_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_DateTime_EndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_DateTime_EndDate.Location = new System.Drawing.Point(166, 35);
            this.lblRec_DateTime_EndDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_DateTime_EndDate.Name = "lblRec_DateTime_EndDate";
            this.lblRec_DateTime_EndDate.Size = new System.Drawing.Size(67, 14);
            this.lblRec_DateTime_EndDate.TabIndex = 13;
            this.lblRec_DateTime_EndDate.Text = "End Time :";
            // 
            // dtpRec_DateTime_StartTime
            // 
            this.dtpRec_DateTime_StartTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRec_DateTime_StartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRec_DateTime_StartTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRec_DateTime_StartTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRec_DateTime_StartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRec_DateTime_StartTime.CustomFormat = "hh:mm tt";
            this.dtpRec_DateTime_StartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRec_DateTime_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec_DateTime_StartTime.Location = new System.Drawing.Point(82, 31);
            this.dtpRec_DateTime_StartTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRec_DateTime_StartTime.Name = "dtpRec_DateTime_StartTime";
            this.dtpRec_DateTime_StartTime.ShowUpDown = true;
            this.dtpRec_DateTime_StartTime.Size = new System.Drawing.Size(80, 22);
            this.dtpRec_DateTime_StartTime.TabIndex = 10;
            this.dtpRec_DateTime_StartTime.ValueChanged += new System.EventHandler(this.dtpRec_DateTime_StartTime_ValueChanged);
            // 
            // lbl_pnlRecurring_DateTimeLeftBrd
            // 
            this.lbl_pnlRecurring_DateTimeLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_DateTimeLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurring_DateTimeLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlRecurring_DateTimeLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_DateTimeLeftBrd.Name = "lbl_pnlRecurring_DateTimeLeftBrd";
            this.lbl_pnlRecurring_DateTimeLeftBrd.Size = new System.Drawing.Size(1, 56);
            this.lbl_pnlRecurring_DateTimeLeftBrd.TabIndex = 90;
            // 
            // lbl_pnlRecurring_DateTimeRightBrd
            // 
            this.lbl_pnlRecurring_DateTimeRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_DateTimeRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurring_DateTimeRightBrd.Location = new System.Drawing.Point(599, 3);
            this.lbl_pnlRecurring_DateTimeRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_DateTimeRightBrd.Name = "lbl_pnlRecurring_DateTimeRightBrd";
            this.lbl_pnlRecurring_DateTimeRightBrd.Size = new System.Drawing.Size(1, 56);
            this.lbl_pnlRecurring_DateTimeRightBrd.TabIndex = 91;
            // 
            // lbl_pnlRecurring_DateTimeTopBrd
            // 
            this.lbl_pnlRecurring_DateTimeTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurring_DateTimeTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurring_DateTimeTopBrd.Location = new System.Drawing.Point(0, 2);
            this.lbl_pnlRecurring_DateTimeTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurring_DateTimeTopBrd.Name = "lbl_pnlRecurring_DateTimeTopBrd";
            this.lbl_pnlRecurring_DateTimeTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurring_DateTimeTopBrd.TabIndex = 122;
            // 
            // lblRec_DateTime_StartDate
            // 
            this.lblRec_DateTime_StartDate.AutoSize = true;
            this.lblRec_DateTime_StartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_DateTime_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_DateTime_StartDate.Location = new System.Drawing.Point(7, 35);
            this.lblRec_DateTime_StartDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_DateTime_StartDate.Name = "lblRec_DateTime_StartDate";
            this.lblRec_DateTime_StartDate.Size = new System.Drawing.Size(73, 14);
            this.lblRec_DateTime_StartDate.TabIndex = 9;
            this.lblRec_DateTime_StartDate.Text = "Start Time :";
            // 
            // lblRec_DateTime_Duration
            // 
            this.lblRec_DateTime_Duration.AutoSize = true;
            this.lblRec_DateTime_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblRec_DateTime_Duration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_DateTime_Duration.Location = new System.Drawing.Point(319, 35);
            this.lblRec_DateTime_Duration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_DateTime_Duration.Name = "lblRec_DateTime_Duration";
            this.lblRec_DateTime_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblRec_DateTime_Duration.TabIndex = 15;
            this.lblRec_DateTime_Duration.Text = "Duration :";
            // 
            // lblRec_DateTime_ColorContainer
            // 
            this.lblRec_DateTime_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lblRec_DateTime_ColorContainer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec_DateTime_ColorContainer.Location = new System.Drawing.Point(537, 36);
            this.lblRec_DateTime_ColorContainer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRec_DateTime_ColorContainer.Name = "lblRec_DateTime_ColorContainer";
            this.lblRec_DateTime_ColorContainer.Size = new System.Drawing.Size(32, 15);
            this.lblRec_DateTime_ColorContainer.TabIndex = 88;
            this.lblRec_DateTime_ColorContainer.Visible = false;
            // 
            // pnlRecurringHeader
            // 
            this.pnlRecurringHeader.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.pnlRecurringHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRecurringHeader.Controls.Add(this.lblRecurring);
            this.pnlRecurringHeader.Controls.Add(this.lbl_pnlRecurringHeaderLeftBrd);
            this.pnlRecurringHeader.Controls.Add(this.btnRec_Save);
            this.pnlRecurringHeader.Controls.Add(this.btnRec_Close);
            this.pnlRecurringHeader.Controls.Add(this.lbl_pnlRecurringHeaderRightBrd);
            this.pnlRecurringHeader.Controls.Add(this.lbl_pnlRecurringHeaderTopBrd);
            this.pnlRecurringHeader.Controls.Add(this.lbl_pnlRecurringHeaderBottomBrdBrd);
            this.pnlRecurringHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecurringHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlRecurringHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRecurringHeader.Name = "pnlRecurringHeader";
            this.pnlRecurringHeader.Size = new System.Drawing.Size(600, 23);
            this.pnlRecurringHeader.TabIndex = 4;
            // 
            // lblRecurring
            // 
            this.lblRecurring.BackColor = System.Drawing.Color.Transparent;
            this.lblRecurring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecurring.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecurring.ForeColor = System.Drawing.Color.White;
            this.lblRecurring.Location = new System.Drawing.Point(1, 1);
            this.lblRecurring.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecurring.Name = "lblRecurring";
            this.lblRecurring.Size = new System.Drawing.Size(560, 21);
            this.lblRecurring.TabIndex = 0;
            this.lblRecurring.Text = " Recurring";
            this.lblRecurring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlRecurringHeaderLeftBrd
            // 
            this.lbl_pnlRecurringHeaderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurringHeaderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlRecurringHeaderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlRecurringHeaderLeftBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurringHeaderLeftBrd.Name = "lbl_pnlRecurringHeaderLeftBrd";
            this.lbl_pnlRecurringHeaderLeftBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_pnlRecurringHeaderLeftBrd.TabIndex = 123;
            // 
            // btnRec_Save
            // 
            this.btnRec_Save.BackColor = System.Drawing.Color.Transparent;
            this.btnRec_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRec_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRec_Save.FlatAppearance.BorderSize = 0;
            this.btnRec_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRec_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRec_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRec_Save.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec_Save.Image = ((System.Drawing.Image)(resources.GetObject("btnRec_Save.Image")));
            this.btnRec_Save.Location = new System.Drawing.Point(561, 1);
            this.btnRec_Save.Margin = new System.Windows.Forms.Padding(2);
            this.btnRec_Save.Name = "btnRec_Save";
            this.btnRec_Save.Size = new System.Drawing.Size(19, 21);
            this.btnRec_Save.TabIndex = 2;
            this.btnRec_Save.UseVisualStyleBackColor = false;
            this.btnRec_Save.Visible = false;
            this.btnRec_Save.MouseLeave += new System.EventHandler(this.btnRec_Save_MouseLeave);
            this.btnRec_Save.MouseHover += new System.EventHandler(this.btnRec_Save_MouseHover);
            // 
            // btnRec_Close
            // 
            this.btnRec_Close.BackColor = System.Drawing.Color.Transparent;
            this.btnRec_Close.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_CloseBlue;
            this.btnRec_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRec_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRec_Close.FlatAppearance.BorderSize = 0;
            this.btnRec_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRec_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRec_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRec_Close.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRec_Close.Location = new System.Drawing.Point(580, 1);
            this.btnRec_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btnRec_Close.Name = "btnRec_Close";
            this.btnRec_Close.Size = new System.Drawing.Size(19, 21);
            this.btnRec_Close.TabIndex = 1;
            this.btnRec_Close.UseVisualStyleBackColor = false;
            this.btnRec_Close.Visible = false;
            this.btnRec_Close.MouseLeave += new System.EventHandler(this.btnRec_Close_MouseLeave);
            this.btnRec_Close.MouseHover += new System.EventHandler(this.btnRec_Close_MouseHover);
            // 
            // lbl_pnlRecurringHeaderRightBrd
            // 
            this.lbl_pnlRecurringHeaderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurringHeaderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRecurringHeaderRightBrd.Location = new System.Drawing.Point(599, 1);
            this.lbl_pnlRecurringHeaderRightBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurringHeaderRightBrd.Name = "lbl_pnlRecurringHeaderRightBrd";
            this.lbl_pnlRecurringHeaderRightBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_pnlRecurringHeaderRightBrd.TabIndex = 124;
            // 
            // lbl_pnlRecurringHeaderTopBrd
            // 
            this.lbl_pnlRecurringHeaderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurringHeaderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlRecurringHeaderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlRecurringHeaderTopBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurringHeaderTopBrd.Name = "lbl_pnlRecurringHeaderTopBrd";
            this.lbl_pnlRecurringHeaderTopBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurringHeaderTopBrd.TabIndex = 122;
            // 
            // lbl_pnlRecurringHeaderBottomBrdBrd
            // 
            this.lbl_pnlRecurringHeaderBottomBrdBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRecurringHeaderBottomBrdBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlRecurringHeaderBottomBrdBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlRecurringHeaderBottomBrdBrd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_pnlRecurringHeaderBottomBrdBrd.Name = "lbl_pnlRecurringHeaderBottomBrdBrd";
            this.lbl_pnlRecurringHeaderBottomBrdBrd.Size = new System.Drawing.Size(600, 1);
            this.lbl_pnlRecurringHeaderBottomBrdBrd.TabIndex = 125;
            // 
            // lbl_Recurrence_DTL
            // 
            this.lbl_Recurrence_DTL.AutoSize = true;
            this.lbl_Recurrence_DTL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Recurrence_DTL.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_Recurrence_DTL.Location = new System.Drawing.Point(0, 0);
            this.lbl_Recurrence_DTL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Recurrence_DTL.Name = "lbl_Recurrence_DTL";
            this.lbl_Recurrence_DTL.Size = new System.Drawing.Size(72, 13);
            this.lbl_Recurrence_DTL.TabIndex = 90;
            this.lbl_Recurrence_DTL.Text = "Recurrence";
            this.lbl_Recurrence_DTL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Recurrence_DTL.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(583, 21);
            this.menuStrip1.TabIndex = 66;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // pnl_toolStrip
            // 
            this.pnl_toolStrip.Controls.Add(this.ts_Commands);
            this.pnl_toolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_toolStrip.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_toolStrip.Name = "pnl_toolStrip";
            this.pnl_toolStrip.Size = new System.Drawing.Size(814, 55);
            this.pnl_toolStrip.TabIndex = 68;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ts_Commands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_RegPatient,
            this.tsb_Print,
            this.tsb_Fax,
            this.tsb_Email,
            this.tsb_Recurrence,
            this.tsb_RemoveRecurrence,
            this.tsb_ShowRecurrence,
            this.tsb_ApplyRecurrence,
            this.tsb_Search,
            this.tsb_Help,
            this.tsb_OK,
            this.tsb_Cancel,
            this.tsb_CancelRecurrence});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(814, 53);
            this.ts_Commands.TabIndex = 8;
            this.ts_Commands.Text = "Appointment";
            // 
            // tsb_RegPatient
            // 
            this.tsb_RegPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RegPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RegPatient.Image")));
            this.tsb_RegPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RegPatient.Name = "tsb_RegPatient";
            this.tsb_RegPatient.Size = new System.Drawing.Size(85, 50);
            this.tsb_RegPatient.Tag = "AddPatient";
            this.tsb_RegPatient.Text = "&Add Patient";
            this.tsb_RegPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RegPatient.ToolTipText = "Add Patient";
            this.tsb_RegPatient.Click += new System.EventHandler(this.tsb_RegPatient_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_Fax
            // 
            this.tsb_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Fax.Image")));
            this.tsb_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Fax.Name = "tsb_Fax";
            this.tsb_Fax.Size = new System.Drawing.Size(36, 50);
            this.tsb_Fax.Tag = "Fax";
            this.tsb_Fax.Text = "&Fax";
            this.tsb_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Fax.Visible = false;
            this.tsb_Fax.Click += new System.EventHandler(this.tsb_Fax_Click);
            // 
            // tsb_Email
            // 
            this.tsb_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Email.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Email.Image")));
            this.tsb_Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Email.Name = "tsb_Email";
            this.tsb_Email.Size = new System.Drawing.Size(42, 50);
            this.tsb_Email.Tag = "Email";
            this.tsb_Email.Text = "&Email";
            this.tsb_Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Email.Visible = false;
            this.tsb_Email.Click += new System.EventHandler(this.tsb_Email_Click);
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
            this.tsb_Recurrence.Click += new System.EventHandler(this.tsb_Recurrence_Click);
            // 
            // tsb_RemoveRecurrence
            // 
            this.tsb_RemoveRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RemoveRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RemoveRecurrence.Image")));
            this.tsb_RemoveRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RemoveRecurrence.Name = "tsb_RemoveRecurrence";
            this.tsb_RemoveRecurrence.Size = new System.Drawing.Size(60, 50);
            this.tsb_RemoveRecurrence.Tag = "RemoveRecurrence";
            this.tsb_RemoveRecurrence.Text = "Re&move";
            this.tsb_RemoveRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RemoveRecurrence.ToolTipText = "Remove Recurrence";
            this.tsb_RemoveRecurrence.Click += new System.EventHandler(this.tsb_RemoveRecurrence_Click);
            // 
            // tsb_ShowRecurrence
            // 
            this.tsb_ShowRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowRecurrence.Image")));
            this.tsb_ShowRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowRecurrence.Name = "tsb_ShowRecurrence";
            this.tsb_ShowRecurrence.Size = new System.Drawing.Size(46, 50);
            this.tsb_ShowRecurrence.Tag = "ShowRecurrence";
            this.tsb_ShowRecurrence.Text = "&Show";
            this.tsb_ShowRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowRecurrence.ToolTipText = "Show Recurrence";
            this.tsb_ShowRecurrence.Click += new System.EventHandler(this.tsb_ShowRecurrence_Click);
            // 
            // tsb_ApplyRecurrence
            // 
            this.tsb_ApplyRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ApplyRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ApplyRecurrence.Image")));
            this.tsb_ApplyRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ApplyRecurrence.Name = "tsb_ApplyRecurrence";
            this.tsb_ApplyRecurrence.Size = new System.Drawing.Size(46, 50);
            this.tsb_ApplyRecurrence.Tag = "ApplyRecurrence";
            this.tsb_ApplyRecurrence.Text = "Appl&y";
            this.tsb_ApplyRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ApplyRecurrence.ToolTipText = "Apply Recurrence";
            this.tsb_ApplyRecurrence.Click += new System.EventHandler(this.tsb_ApplyRecurrence_Click);
            // 
            // tsb_Search
            // 
            this.tsb_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Search.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Search.Image")));
            this.tsb_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Search.Name = "tsb_Search";
            this.tsb_Search.Size = new System.Drawing.Size(52, 50);
            this.tsb_Search.Tag = "SearchAppointment";
            this.tsb_Search.Text = "Sear&ch";
            this.tsb_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Search.Visible = false;
            this.tsb_Search.Click += new System.EventHandler(this.tsb_Search_Click);
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
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
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
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
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
            this.tsb_CancelRecurrence.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlAlerts
            // 
            this.pnlAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAlerts.Controls.Add(this.label16);
            this.pnlAlerts.Controls.Add(this.pnlgloEMRAlerts);
            this.pnlAlerts.Controls.Add(this.pnlEMRCaption);
            this.pnlAlerts.Controls.Add(this.pnlgloPMAlerts);
            this.pnlAlerts.Controls.Add(this.pnlPMCaption);
            this.pnlAlerts.Controls.Add(this.pnlCommanAlerts);
            this.pnlAlerts.Controls.Add(this.panel2);
            this.pnlAlerts.Controls.Add(this.label20);
            this.pnlAlerts.Controls.Add(this.label13);
            this.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlAlerts.Location = new System.Drawing.Point(604, 55);
            this.pnlAlerts.Name = "pnlAlerts";
            this.pnlAlerts.Padding = new System.Windows.Forms.Padding(2);
            this.pnlAlerts.Size = new System.Drawing.Size(210, 463);
            this.pnlAlerts.TabIndex = 179;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(3, 460);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(204, 1);
            this.label16.TabIndex = 121;
            this.label16.Text = "label4";
            // 
            // pnlgloEMRAlerts
            // 
            this.pnlgloEMRAlerts.BackColor = System.Drawing.Color.White;
            this.pnlgloEMRAlerts.Controls.Add(this.c1EMRAlerts);
            this.pnlgloEMRAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloEMRAlerts.Location = new System.Drawing.Point(3, 429);
            this.pnlgloEMRAlerts.Name = "pnlgloEMRAlerts";
            this.pnlgloEMRAlerts.Size = new System.Drawing.Size(204, 32);
            this.pnlgloEMRAlerts.TabIndex = 3;
            // 
            // c1EMRAlerts
            // 
            this.c1EMRAlerts.AllowEditing = false;
            this.c1EMRAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1EMRAlerts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1EMRAlerts.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1EMRAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1EMRAlerts.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1EMRAlerts.ExtendLastCol = true;
            this.c1EMRAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1EMRAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1EMRAlerts.Location = new System.Drawing.Point(0, 0);
            this.c1EMRAlerts.Name = "c1EMRAlerts";
            this.c1EMRAlerts.Rows.Count = 5;
            this.c1EMRAlerts.Rows.DefaultSize = 19;
            this.c1EMRAlerts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1EMRAlerts.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1EMRAlerts.ShowCellLabels = true;
            this.c1EMRAlerts.Size = new System.Drawing.Size(204, 32);
            this.c1EMRAlerts.StyleInfo = resources.GetString("c1EMRAlerts.StyleInfo");
            this.c1EMRAlerts.TabIndex = 9;
            this.c1EMRAlerts.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1EMRAlerts.Tree.NodeImageCollapsed")));
            this.c1EMRAlerts.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1EMRAlerts.Tree.NodeImageExpanded")));
            // 
            // pnlEMRCaption
            // 
            this.pnlEMRCaption.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlEMRCaption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlEMRCaption.Controls.Add(this.btnUpEMR);
            this.pnlEMRCaption.Controls.Add(this.btnDownEMR);
            this.pnlEMRCaption.Controls.Add(this.label34);
            this.pnlEMRCaption.Controls.Add(this.label17);
            this.pnlEMRCaption.Controls.Add(this.label18);
            this.pnlEMRCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEMRCaption.Location = new System.Drawing.Point(3, 403);
            this.pnlEMRCaption.Margin = new System.Windows.Forms.Padding(2);
            this.pnlEMRCaption.Name = "pnlEMRCaption";
            this.pnlEMRCaption.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlEMRCaption.Size = new System.Drawing.Size(204, 26);
            this.pnlEMRCaption.TabIndex = 90;
            // 
            // btnUpEMR
            // 
            this.btnUpEMR.BackColor = System.Drawing.Color.Transparent;
            this.btnUpEMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpEMR.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpEMR.FlatAppearance.BorderSize = 0;
            this.btnUpEMR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUpEMR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUpEMR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpEMR.Image = global::gloAppointmentScheduling.Properties.Resources.UP;
            this.btnUpEMR.Location = new System.Drawing.Point(156, 4);
            this.btnUpEMR.Name = "btnUpEMR";
            this.btnUpEMR.Size = new System.Drawing.Size(24, 21);
            this.btnUpEMR.TabIndex = 125;
            this.btnUpEMR.UseVisualStyleBackColor = false;
            this.btnUpEMR.Visible = false;
            this.btnUpEMR.Click += new System.EventHandler(this.btnUpEMR_Click);
            this.btnUpEMR.MouseLeave += new System.EventHandler(this.btnUpEMR_MouseLeave);
            this.btnUpEMR.MouseHover += new System.EventHandler(this.btnUpEMR_MouseHover);
            // 
            // btnDownEMR
            // 
            this.btnDownEMR.BackColor = System.Drawing.Color.Transparent;
            this.btnDownEMR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownEMR.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDownEMR.FlatAppearance.BorderSize = 0;
            this.btnDownEMR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDownEMR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDownEMR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownEMR.Image = global::gloAppointmentScheduling.Properties.Resources.Down;
            this.btnDownEMR.Location = new System.Drawing.Point(180, 4);
            this.btnDownEMR.Name = "btnDownEMR";
            this.btnDownEMR.Size = new System.Drawing.Size(24, 21);
            this.btnDownEMR.TabIndex = 126;
            this.btnDownEMR.UseVisualStyleBackColor = false;
            this.btnDownEMR.Visible = false;
            this.btnDownEMR.Click += new System.EventHandler(this.btnDownEMR_Click);
            this.btnDownEMR.MouseLeave += new System.EventHandler(this.btnDownEMR_MouseLeave);
            this.btnDownEMR.MouseHover += new System.EventHandler(this.btnDownEMR_MouseHover);
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Location = new System.Drawing.Point(0, 3);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(204, 1);
            this.label34.TabIndex = 122;
            this.label34.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Location = new System.Drawing.Point(0, 25);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(204, 1);
            this.label17.TabIndex = 116;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(0, 3);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(204, 23);
            this.label18.TabIndex = 0;
            this.label18.Text = "Q EMR Alerts";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlgloPMAlerts
            // 
            this.pnlgloPMAlerts.BackColor = System.Drawing.Color.White;
            this.pnlgloPMAlerts.Controls.Add(this.c1EligibilityCheck);
            this.pnlgloPMAlerts.Controls.Add(this.c1PatientAlerts);
            this.pnlgloPMAlerts.Controls.Add(this.label31);
            this.pnlgloPMAlerts.Controls.Add(this.c1GlobalPeriod);
            this.pnlgloPMAlerts.Controls.Add(this.c1CopayAlert);
            this.pnlgloPMAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlgloPMAlerts.Location = new System.Drawing.Point(3, 107);
            this.pnlgloPMAlerts.Name = "pnlgloPMAlerts";
            this.pnlgloPMAlerts.Size = new System.Drawing.Size(204, 296);
            this.pnlgloPMAlerts.TabIndex = 1;
            // 
            // c1EligibilityCheck
            // 
            this.c1EligibilityCheck.AllowEditing = false;
            this.c1EligibilityCheck.BackColor = System.Drawing.Color.White;
            this.c1EligibilityCheck.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1EligibilityCheck.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1EligibilityCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1EligibilityCheck.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1EligibilityCheck.ExtendLastCol = true;
            this.c1EligibilityCheck.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid;
            this.c1EligibilityCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1EligibilityCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1EligibilityCheck.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1EligibilityCheck.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1EligibilityCheck.Location = new System.Drawing.Point(0, 94);
            this.c1EligibilityCheck.Name = "c1EligibilityCheck";
            this.c1EligibilityCheck.Rows.Count = 0;
            this.c1EligibilityCheck.Rows.DefaultSize = 19;
            this.c1EligibilityCheck.Rows.Fixed = 0;
            this.c1EligibilityCheck.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1EligibilityCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1EligibilityCheck.ShowCellLabels = true;
            this.c1EligibilityCheck.Size = new System.Drawing.Size(204, 25);
            this.c1EligibilityCheck.StyleInfo = resources.GetString("c1EligibilityCheck.StyleInfo");
            this.c1EligibilityCheck.TabIndex = 5;
            this.c1EligibilityCheck.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1EligibilityCheck.Tree.NodeImageCollapsed")));
            this.c1EligibilityCheck.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1EligibilityCheck.Tree.NodeImageExpanded")));
            // 
            // c1PatientAlerts
            // 
            this.c1PatientAlerts.AllowEditing = false;
            this.c1PatientAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PatientAlerts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PatientAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientAlerts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientAlerts.ColumnInfo = resources.GetString("c1PatientAlerts.ColumnInfo");
            this.c1PatientAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1PatientAlerts.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientAlerts.ExtendLastCol = true;
            this.c1PatientAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientAlerts.Location = new System.Drawing.Point(0, 39);
            this.c1PatientAlerts.Name = "c1PatientAlerts";
            this.c1PatientAlerts.Rows.Count = 1;
            this.c1PatientAlerts.Rows.DefaultSize = 19;
            this.c1PatientAlerts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1PatientAlerts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1PatientAlerts.ShowCellLabels = true;
            this.c1PatientAlerts.Size = new System.Drawing.Size(204, 55);
            this.c1PatientAlerts.StyleInfo = resources.GetString("c1PatientAlerts.StyleInfo");
            this.c1PatientAlerts.TabIndex = 125;
            this.c1PatientAlerts.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientAlerts.Tree.NodeImageCollapsed")));
            this.c1PatientAlerts.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientAlerts.Tree.NodeImageExpanded")));
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Location = new System.Drawing.Point(0, 295);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(204, 1);
            this.label31.TabIndex = 124;
            this.label31.Text = "label4";
            // 
            // c1GlobalPeriod
            // 
            this.c1GlobalPeriod.AllowEditing = false;
            this.c1GlobalPeriod.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1GlobalPeriod.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1GlobalPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1GlobalPeriod.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1GlobalPeriod.ColumnInfo = resources.GetString("c1GlobalPeriod.ColumnInfo");
            this.c1GlobalPeriod.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1GlobalPeriod.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1GlobalPeriod.ExtendLastCol = true;
            this.c1GlobalPeriod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1GlobalPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1GlobalPeriod.Location = new System.Drawing.Point(0, 20);
            this.c1GlobalPeriod.Name = "c1GlobalPeriod";
            this.c1GlobalPeriod.Rows.Count = 0;
            this.c1GlobalPeriod.Rows.DefaultSize = 19;
            this.c1GlobalPeriod.Rows.Fixed = 0;
            this.c1GlobalPeriod.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1GlobalPeriod.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1GlobalPeriod.ShowCellLabels = true;
            this.c1GlobalPeriod.Size = new System.Drawing.Size(204, 19);
            this.c1GlobalPeriod.StyleInfo = resources.GetString("c1GlobalPeriod.StyleInfo");
            this.c1GlobalPeriod.TabIndex = 126;
            this.c1GlobalPeriod.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1GlobalPeriod.Tree.NodeImageCollapsed")));
            this.c1GlobalPeriod.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1GlobalPeriod.Tree.NodeImageExpanded")));
            // 
            // c1CopayAlert
            // 
            this.c1CopayAlert.AllowEditing = false;
            this.c1CopayAlert.BackColor = System.Drawing.Color.White;
            this.c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CopayAlert.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1CopayAlert.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1CopayAlert.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1CopayAlert.ExtendLastCol = true;
            this.c1CopayAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1CopayAlert.Location = new System.Drawing.Point(0, 0);
            this.c1CopayAlert.Name = "c1CopayAlert";
            this.c1CopayAlert.Rows.Count = 5;
            this.c1CopayAlert.Rows.DefaultSize = 19;
            this.c1CopayAlert.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1CopayAlert.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1CopayAlert.ShowCellLabels = true;
            this.c1CopayAlert.Size = new System.Drawing.Size(204, 20);
            this.c1CopayAlert.StyleInfo = resources.GetString("c1CopayAlert.StyleInfo");
            this.c1CopayAlert.TabIndex = 8;
            this.c1CopayAlert.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1CopayAlert.Tree.NodeImageCollapsed")));
            this.c1CopayAlert.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1CopayAlert.Tree.NodeImageExpanded")));
            // 
            // pnlPMCaption
            // 
            this.pnlPMCaption.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Button;
            this.pnlPMCaption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPMCaption.Controls.Add(this.btnUpPM);
            this.pnlPMCaption.Controls.Add(this.btnDownPM);
            this.pnlPMCaption.Controls.Add(this.label25);
            this.pnlPMCaption.Controls.Add(this.label11);
            this.pnlPMCaption.Controls.Add(this.label12);
            this.pnlPMCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPMCaption.Location = new System.Drawing.Point(3, 81);
            this.pnlPMCaption.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPMCaption.Name = "pnlPMCaption";
            this.pnlPMCaption.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlPMCaption.Size = new System.Drawing.Size(204, 26);
            this.pnlPMCaption.TabIndex = 90;
            // 
            // btnUpPM
            // 
            this.btnUpPM.BackColor = System.Drawing.Color.Transparent;
            this.btnUpPM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUpPM.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpPM.FlatAppearance.BorderSize = 0;
            this.btnUpPM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUpPM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUpPM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpPM.Image = global::gloAppointmentScheduling.Properties.Resources.UP;
            this.btnUpPM.Location = new System.Drawing.Point(156, 4);
            this.btnUpPM.Name = "btnUpPM";
            this.btnUpPM.Size = new System.Drawing.Size(24, 21);
            this.btnUpPM.TabIndex = 123;
            this.btnUpPM.UseVisualStyleBackColor = false;
            this.btnUpPM.Visible = false;
            this.btnUpPM.Click += new System.EventHandler(this.btnUpPM_Click);
            this.btnUpPM.MouseLeave += new System.EventHandler(this.btnUpPM_MouseLeave);
            this.btnUpPM.MouseHover += new System.EventHandler(this.btnUpPM_MouseHover);
            // 
            // btnDownPM
            // 
            this.btnDownPM.BackColor = System.Drawing.Color.Transparent;
            this.btnDownPM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDownPM.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDownPM.FlatAppearance.BorderSize = 0;
            this.btnDownPM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDownPM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDownPM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownPM.Image = global::gloAppointmentScheduling.Properties.Resources.Down;
            this.btnDownPM.Location = new System.Drawing.Point(180, 4);
            this.btnDownPM.Name = "btnDownPM";
            this.btnDownPM.Size = new System.Drawing.Size(24, 21);
            this.btnDownPM.TabIndex = 124;
            this.btnDownPM.UseVisualStyleBackColor = false;
            this.btnDownPM.Visible = false;
            this.btnDownPM.Click += new System.EventHandler(this.btnDownPM_Click);
            this.btnDownPM.MouseLeave += new System.EventHandler(this.btnDownPM_MouseLeave);
            this.btnDownPM.MouseHover += new System.EventHandler(this.btnDownPM_MouseHover);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Location = new System.Drawing.Point(0, 3);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(204, 1);
            this.label25.TabIndex = 122;
            this.label25.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(204, 1);
            this.label11.TabIndex = 116;
            this.label11.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(204, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "  Q PM Alerts";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCommanAlerts
            // 
            this.pnlCommanAlerts.Controls.Add(this.c1SystemAlert);
            this.pnlCommanAlerts.Controls.Add(this.label9);
            this.pnlCommanAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCommanAlerts.Location = new System.Drawing.Point(3, 25);
            this.pnlCommanAlerts.Name = "pnlCommanAlerts";
            this.pnlCommanAlerts.Size = new System.Drawing.Size(204, 56);
            this.pnlCommanAlerts.TabIndex = 0;
            // 
            // c1SystemAlert
            // 
            this.c1SystemAlert.AllowEditing = false;
            this.c1SystemAlert.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SystemAlert.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1SystemAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SystemAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SystemAlert.ColumnInfo = resources.GetString("c1SystemAlert.ColumnInfo");
            this.c1SystemAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SystemAlert.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1SystemAlert.ExtendLastCol = true;
            this.c1SystemAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SystemAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SystemAlert.Location = new System.Drawing.Point(0, 0);
            this.c1SystemAlert.Name = "c1SystemAlert";
            this.c1SystemAlert.Rows.Count = 1;
            this.c1SystemAlert.Rows.DefaultSize = 19;
            this.c1SystemAlert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1SystemAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1SystemAlert.ShowCellLabels = true;
            this.c1SystemAlert.Size = new System.Drawing.Size(204, 55);
            this.c1SystemAlert.StyleInfo = resources.GetString("c1SystemAlert.StyleInfo");
            this.c1SystemAlert.TabIndex = 121;
            this.c1SystemAlert.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1SystemAlert.Tree.NodeImageCollapsed")));
            this.c1SystemAlert.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1SystemAlert.Tree.NodeImageExpanded")));
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(0, 55);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(204, 1);
            this.label9.TabIndex = 120;
            this.label9.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 23);
            this.panel2.TabIndex = 91;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 1);
            this.label5.TabIndex = 122;
            this.label5.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 1);
            this.label3.TabIndex = 116;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "  Patient Alerts";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(207, 2);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 459);
            this.label20.TabIndex = 122;
            this.label20.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(2, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 459);
            this.label13.TabIndex = 123;
            this.label13.Text = "label2";
            // 
            // pnlMainNToolStrip
            // 
            this.pnlMainNToolStrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMainNToolStrip.Controls.Add(this.pnlMain);
            this.pnlMainNToolStrip.Controls.Add(this.pnlAlerts);
            this.pnlMainNToolStrip.Controls.Add(this.pnl_toolStrip);
            this.pnlMainNToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainNToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlMainNToolStrip.Name = "pnlMainNToolStrip";
            this.pnlMainNToolStrip.Size = new System.Drawing.Size(814, 518);
            this.pnlMainNToolStrip.TabIndex = 180;
            // 
            // tmrCopayAlertBlink
            // 
            this.tmrCopayAlertBlink.Interval = 500;
            this.tmrCopayAlertBlink.Tick += new System.EventHandler(this.tmrCopayAlertBlink_Tick);
            // 
            // frmSetupAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(814, 518);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlMainNToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupAppointment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appointment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupAppointment_FormClosing);
            this.Load += new System.EventHandler(this.frmSetupAppointment_Load);
            this.Shown += new System.EventHandler(this.frmSetupAppointment_Shown);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlAppointment.ResumeLayout(false);
            this.pnlAppointment.PerformLayout();
            this.pnlApp_DateTime.ResumeLayout(false);
            this.pnlApp_DateTime.PerformLayout();
            this.pnlApp_DateTimeContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).EndInit();
            this.pnlCriteria_ProviderProblemType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderProblemType)).EndInit();
            this.pnlCriteria_ProviderProblemType_Header.ResumeLayout(false);
            this.pnlCriteria_Resources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Resources)).EndInit();
            this.pnlCriteria_Resources_Header.ResumeLayout(false);
            this.pnlAppointmentHeader.ResumeLayout(false);
            this.pnlRecurring.ResumeLayout(false);
            this.pnlRecurring_Appointments.ResumeLayout(false);
            this.pnlRecurring_Appointments_Header.ResumeLayout(false);
            this.pnlRecurring_Appointments_Exception.ResumeLayout(false);
            this.pnlRecurring_Range.ResumeLayout(false);
            this.pnlRecurring_Range.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Range_EndAfterOccurence)).EndInit();
            this.pnlRecurring_Range_Header.ResumeLayout(false);
            this.pnlRecurring_Range_Header.PerformLayout();
            this.pnlRecurring_Pattern.ResumeLayout(false);
            this.pnlRecurring_Pattern.PerformLayout();
            this.pnlRecurring_Pattern_Header.ResumeLayout(false);
            this.pnlRec_Pattern_Yearly.ResumeLayout(false);
            this.pnlRec_Pattern_Yearly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Yearly_Every_MonthDay)).EndInit();
            this.pnlRec_Pattern_Daily.ResumeLayout(false);
            this.pnlRec_Pattern_Daily.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Daily_EveryDay)).EndInit();
            this.pnlRec_Pattern_Monthly.ResumeLayout(false);
            this.pnlRec_Pattern_Monthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Criteria_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Month)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Monthly_Day_Day)).EndInit();
            this.pnlRec_Pattern_Weekly.ResumeLayout(false);
            this.pnlRec_Pattern_Weekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRec_Pattern_Weekly_WeekOn)).EndInit();
            this.pnlRecurring_DateTime.ResumeLayout(false);
            this.pnlRecurring_DateTime.PerformLayout();
            this.pnlRecurring_DateTime_Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRec_DateTime_Duration)).EndInit();
            this.pnlRecurringHeader.ResumeLayout(false);
            this.pnl_toolStrip.ResumeLayout(false);
            this.pnl_toolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlAlerts.ResumeLayout(false);
            this.pnlgloEMRAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1EMRAlerts)).EndInit();
            this.pnlEMRCaption.ResumeLayout(false);
            this.pnlgloPMAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1EligibilityCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GlobalPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CopayAlert)).EndInit();
            this.pnlPMCaption.ResumeLayout(false);
            this.pnlCommanAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SystemAlert)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlMainNToolStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Email;
        internal System.Windows.Forms.ToolStripButton tsb_Fax;
        internal System.Windows.Forms.ToolStripButton tsb_Help;
        internal System.Windows.Forms.Label lblFinishDate;
        internal System.Windows.Forms.CheckBox chkRecurring;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_EndDate;
        internal System.Windows.Forms.Label lblFinishTime;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_EndTime;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlRecurring;
        private System.Windows.Forms.Panel pnlRecurringHeader;
        private System.Windows.Forms.Button btnRec_Close;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Weekly;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Saturday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Friday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Tuesday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Thursday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Monday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Wednesday;
        private System.Windows.Forms.CheckBox ChkRec_Pattern_Weekly_Sunday;
        private System.Windows.Forms.Panel pnlAppointment;
        private System.Windows.Forms.Panel pnlAppointmentHeader;
        private System.Windows.Forms.Label lblAppointment;
        internal System.Windows.Forms.Button btnApp_ReferralDoctor;
        internal System.Windows.Forms.Button btnApp_Insurance;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartTime;
        internal System.Windows.Forms.Button btnApp_Patient;
        internal System.Windows.Forms.Button btnApp_DateTime_Color;
        internal System.Windows.Forms.Label lblApp_DateTime_Duration;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartDate;
        private System.Windows.Forms.Label lblApp_DateTime_Color;
        internal System.Windows.Forms.Button btnApp_Provider;
        internal System.Windows.Forms.TextBox txtApp_Patient;
        internal System.Windows.Forms.Label lblApp_ReferralDoctor;
        internal System.Windows.Forms.Label lblApp_Status;
        internal System.Windows.Forms.Button btnApp_Procedures;
        internal System.Windows.Forms.Label lblApp_Patient;
        internal System.Windows.Forms.Label lblApp_Provider;
        internal System.Windows.Forms.Label lblApp_Procedure;
        internal System.Windows.Forms.ComboBox cmbApp_Status;
        internal System.Windows.Forms.Button btnApp_Resources;
        internal System.Windows.Forms.Label lblApp_DateTime_Date;
        internal System.Windows.Forms.Label lblApp_AppointmentType;
        internal System.Windows.Forms.Label lblApp_DateTime_Time;
        internal System.Windows.Forms.Label lblApp_Notes;
        internal System.Windows.Forms.Label lblApp_Resources;
        private System.Windows.Forms.ComboBox cmbApp_Department;
        internal System.Windows.Forms.Label lblApp_Department;
        private System.Windows.Forms.ComboBox cmbApp_Location;
        internal System.Windows.Forms.Label lblApp_Coverage;
        internal System.Windows.Forms.Label lblApp_Location;
        private System.Windows.Forms.ComboBox cmbApp_Coverage;
        private System.Windows.Forms.Panel pnlApp_DateTime;
        internal System.Windows.Forms.Label lblApp_Divider3;
        internal System.Windows.Forms.Label lblApp_Divider2;
        internal System.Windows.Forms.Label lblApp_Divider1;
        private System.Windows.Forms.RadioButton rbRec_Range_EndBy;
        private System.Windows.Forms.RadioButton rbRec_Range_EndAfterOccurence;
        private System.Windows.Forms.RadioButton rbRec_Range_NoEndDate;
        private System.Windows.Forms.Label lblRec_Range_Occurence;
        private System.Windows.Forms.NumericUpDown numRec_Range_EndAfterOccurence;
        private System.Windows.Forms.DateTimePicker dtpRec_Range_EndBy;
        private System.Windows.Forms.DateTimePicker dtpRec_Range_StartDate;
        private System.Windows.Forms.Label lblRec_Range_StartDate;
        private System.Windows.Forms.Label lblRec_DateTime_Duration;
        private System.Windows.Forms.NumericUpDown numRec_DateTime_Duration;
        private System.Windows.Forms.DateTimePicker dtpRec_DateTime_EndTime;
        private System.Windows.Forms.Label lblRec_DateTime_EndDate;
        private System.Windows.Forms.DateTimePicker dtpRec_DateTime_StartTime;
        private System.Windows.Forms.Label lblRec_DateTime_StartDate;
        private System.Windows.Forms.Panel pnlRec_Pattern_Daily;
        private System.Windows.Forms.Label lblRec_Pattern_Daily_Days;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryWeekday;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Daily_EveryDay;
        private System.Windows.Forms.Panel pnlRec_Pattern_Weekly;
        private System.Windows.Forms.Label lblRec_Pattern_Weekly_WeekOn;
        private System.Windows.Forms.Label lblRec_Pattern_Weekly_RecurEvery;
        private System.Windows.Forms.Panel pnlRec_Pattern_Monthly;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Day_Month;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Day_Every;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly_Day;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Monthly_Criteria_DayWeekday;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Monthly_Criteria_FstLst;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Criteria_Month;
        private System.Windows.Forms.Label lblRec_Pattern_Monthly_Criteria_Every;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Monthly_Criteria;
        private System.Windows.Forms.Panel pnlRec_Pattern_Yearly;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_Month;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Every_Month;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_DayWeekday;
        private System.Windows.Forms.ComboBox cmbRec_Pattern_Yearly_Criteria_FstLst;
        private System.Windows.Forms.Label lblRec_Pattern_Yearly_Criteria_Of;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly_Criteria;
        private System.Windows.Forms.RadioButton rbRec_Pattern_Yearly_EveryMonthDay;
        private System.Windows.Forms.Label lblRec_Range_EndDate;
        internal System.Windows.Forms.Label lblApp_Recurrence_Time;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripButton tsb_Recurrence;
        private System.Windows.Forms.Button btnRec_Save;
        private System.Windows.Forms.ComboBox cmbRec_Range_NoEndDateYear;
        private System.Windows.Forms.Panel pnlRecurring_DateTime;
        private System.Windows.Forms.Panel pnlRecurring_DateTime_Header;
        private System.Windows.Forms.Label lblRecurring_DateTime_Header;
        private System.Windows.Forms.Panel pnlRecurring_Pattern;
        private System.Windows.Forms.Panel pnlRecurring_Pattern_Header;
        private System.Windows.Forms.Label lblRecurring_Pattern_Header;
        private System.Windows.Forms.Panel pnlRecurring_Range;
        private System.Windows.Forms.Panel pnlRecurring_Range_Header;
        private System.Windows.Forms.Label lblRecurring_Range_Header;
        private System.Windows.Forms.Panel pnlRecurring_Appointments;
        private System.Windows.Forms.Panel pnlRecurring_Appointments_Header;
        private System.Windows.Forms.Label lblRecurring_Appointments_Header;
        private System.Windows.Forms.ListView lvwRec_Apointments;
        private System.Windows.Forms.Panel pnlRecurring_Appointments_Exception;
        private System.Windows.Forms.ListView lvwRec_Exception;
        private System.Windows.Forms.Button btnRec_RemoveException;
        private System.Windows.Forms.Button btnRec_AddException;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Daily_EveryDay;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Weekly_WeekOn;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Yearly_Every_MonthDay;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Day_Month;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Day_Day;
        private System.Windows.Forms.NumericUpDown numRec_Pattern_Monthly_Criteria_Month;
        private System.Windows.Forms.Label lblApp_Recurrence;
        private System.Windows.Forms.Label lblApp_DateTime;
        private System.Windows.Forms.Label lblRec_DateTime_ColorContainer;
        private System.Windows.Forms.Label lblRec_DateTime_ColorCode;
        internal System.Windows.Forms.Button btnRec_DateTime_Color;
        internal System.Windows.Forms.CheckBox chkApp_DateTime_IsAllDayEvent;
        internal System.Windows.Forms.CheckBox chkRec_DateTime_IsAllDayEvent;
        private System.Windows.Forms.Label lbl_Recurrence_DTL;
        private System.Windows.Forms.Label lblDurationUnit;
        internal System.Windows.Forms.ComboBox cmbApp_AppointmentType;
        internal System.Windows.Forms.NumericUpDown numApp_DateTime_Duration;
        internal System.Windows.Forms.ComboBox cmbApp_Provider;
        internal System.Windows.Forms.Label lblApp_DateTime_ColorContainer;
        internal System.Windows.Forms.ToolStripButton tsb_RemoveRecurrence;
        private System.Windows.Forms.Panel pnl_toolStrip;
        private System.Windows.Forms.Label lbl_pnlAppointmentLeftBrd;
        private System.Windows.Forms.Label lbl_pnlAppointmentRightBrd;
        private System.Windows.Forms.Label lbl_pnlAppointmentTopBrd;
        private System.Windows.Forms.Label lbl_pnlAppointmentBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlApp_DateTimeBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlApp_DateTimeTopBrd;
        private System.Windows.Forms.Label lbl_pnlApp_DateTimeRightBrd;
        private System.Windows.Forms.Label lbl_pnlApp_DateTimeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_DailyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_MonthlyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyTopBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyRightBrd;
        private System.Windows.Forms.Label lbl_pnlRec_Pattern_WeeklyLeftBrd;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lbl_pnlRecurring_DateTimeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_DateTimeRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_AppointmentsLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_AppointmentsRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_AppointmentsTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Range_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeLeftBrd;
        private System.Windows.Forms.Label lbl_lbl_pnlRecurring_RangeBottomBrdRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_RangeTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_Pattern_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternBootomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_PatternTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_DateTimeTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_DateTime_HeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_DateTimeBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurringHeaderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlRecurringHeaderRightBrd;
        private System.Windows.Forms.Label lbl_pnlRecurringHeaderTopBrd;
        private System.Windows.Forms.Label lbl_pnlRecurring_AppointmentsBottomBrd;
        private System.Windows.Forms.Label lbl_pnlRecurringHeaderBottomBrdBrd;
        internal System.Windows.Forms.ToolStripButton tsb_ApplyRecurrence;
        internal System.Windows.Forms.ToolStripButton tsb_ShowRecurrence;
        internal System.Windows.Forms.ToolStripButton tsb_CancelRecurrence;
        internal System.Windows.Forms.ToolStripButton tsb_Search;
        private System.Windows.Forms.Panel pnlCriteria_Resources;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Resources;
        private System.Windows.Forms.Panel pnlCriteria_Resources_Header;
        private System.Windows.Forms.Label lblCriteria_Resources_Header;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProviderProblemType;
        private System.Windows.Forms.Panel pnlCriteria_ProviderProblemType_Header;
        private System.Windows.Forms.Label lblCriteria_ProviderProblemType_Header;
        internal System.Windows.Forms.Button btnApp_ClearResources;
        internal System.Windows.Forms.Button btnApp_ClearProcedures;
        private System.Windows.Forms.ToolStripButton tsb_RegPatient;
        internal System.Windows.Forms.Button btnApp_ClearReferralDoctor;
        internal System.Windows.Forms.Button btnApp_ClearPatient;
        internal System.Windows.Forms.Button btnApp_ClearProvider;
        internal System.Windows.Forms.Button btnApp_ClearInsurance;
        internal System.Windows.Forms.Button btnApp_ClearDateTime_Color;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemType_HeaderBootmBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeLeftBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ProviderProblemTypeTopBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_Resources_HeaderBottomBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ResourcesBottomBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ResourcesLeftBrd;
        private System.Windows.Forms.Label lbl_pnlCriteria_ResourcesRightBrd;
        internal System.Windows.Forms.Label lbl_pnlCriteria_ResourcesTopBrd;
        private System.Windows.Forms.Panel pnlApp_DateTimeContainer;
        private System.Windows.Forms.Label lbl_pnlAppointmentHeaderBottomBrd;
        private System.Windows.Forms.Label lblPatientBalance;
        private System.Windows.Forms.RichTextBox txtApp_Notes;
        internal System.Windows.Forms.Label lblAuthorizaionName;
        internal System.Windows.Forms.Label lblPatientBalanceName;
        internal System.Windows.Forms.Button btnRemove_PriorAuthorization;
        internal System.Windows.Forms.Button btnAdd_PriorAuthorization;
        private System.Windows.Forms.TextBox txtPriorAuthorizationNo;
        private System.Windows.Forms.Label lblRecurring;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkPARequired;
        internal System.Windows.Forms.ComboBox cmbApp_ReferralDoctor;
        private System.Windows.Forms.Panel pnlAlerts;
        private System.Windows.Forms.Panel pnlMainNToolStrip;
        private System.Windows.Forms.Panel pnlCommanAlerts;
        private System.Windows.Forms.Panel pnlgloEMRAlerts;
        private System.Windows.Forms.Panel pnlEMRCaption;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlgloPMAlerts;
        private System.Windows.Forms.Panel pnlPMCaption;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CopayAlert;
        private C1.Win.C1FlexGrid.C1FlexGrid c1EligibilityCheck;
        private System.Windows.Forms.Timer tmrCopayAlertBlink;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SystemAlert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Button btnUpEMR;
        internal System.Windows.Forms.Button btnDownEMR;
        internal System.Windows.Forms.Button btnUpPM;
        internal System.Windows.Forms.Button btnDownPM;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label13;
        private C1.Win.C1FlexGrid.C1FlexGrid c1EMRAlerts;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientAlerts;
        private C1.Win.C1FlexGrid.C1FlexGrid c1GlobalPeriod;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
