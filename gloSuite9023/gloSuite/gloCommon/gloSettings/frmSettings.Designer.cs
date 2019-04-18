namespace gloSettings
{
    partial class frmSettings
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
                    if (clDlg != null)
                    {

                        clDlg.Dispose();
                        clDlg = null;
                    }
                }
                catch
                {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tb_Settings = new System.Windows.Forms.TabControl();
            this.tbpg_GeneralSettings = new System.Windows.Forms.TabPage();
            this.panel33 = new System.Windows.Forms.Panel();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.trvPatientColumns = new System.Windows.Forms.TreeView();
            this.panel31 = new System.Windows.Forms.Panel();
            this.label66 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.pnlPatientSearch = new System.Windows.Forms.Panel();
            this.trvPatientSearch = new System.Windows.Forms.TreeView();
            this.panel25 = new System.Windows.Forms.Panel();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.trvDemographics = new System.Windows.Forms.TreeView();
            this.panel32 = new System.Windows.Forms.Panel();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.panel36 = new System.Windows.Forms.Panel();
            this.panel35 = new System.Windows.Forms.Panel();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.btnBrowseAlertColor = new System.Windows.Forms.Button();
            this.lblAlertColor = new System.Windows.Forms.Label();
            this.txtAlertColor = new System.Windows.Forms.TextBox();
            this.chkShowBlinkAlert = new System.Windows.Forms.CheckBox();
            this.label158 = new System.Windows.Forms.Label();
            this.label159 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.cmbDefaultProvider = new System.Windows.Forms.ComboBox();
            this.label139 = new System.Windows.Forms.Label();
            this.cmbSearchColumns = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.numPatientCodeIncrement = new System.Windows.Forms.NumericUpDown();
            this.label73 = new System.Windows.Forms.Label();
            this.txtPatientCodePrefix = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.gbRemotePrintSetting = new System.Windows.Forms.Panel();
            this.label85 = new System.Windows.Forms.Label();
            this.cmbNoTemplatesJob = new System.Windows.Forms.ComboBox();
            this.chkZipMetadata = new System.Windows.Forms.CheckBox();
            this.pnlPrintImages = new System.Windows.Forms.Panel();
            this.rbPrintImagesEMF = new System.Windows.Forms.RadioButton();
            this.Label209 = new System.Windows.Forms.Label();
            this.rbPrintImagesPNG = new System.Windows.Forms.RadioButton();
            this.pnlPrintClaims = new System.Windows.Forms.Panel();
            this.rbPrintClaimsEMF = new System.Windows.Forms.RadioButton();
            this.Label210 = new System.Windows.Forms.Label();
            this.rbPrintClaimsPDF = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rbPrintSSRSReportEMF = new System.Windows.Forms.RadioButton();
            this.Label208 = new System.Windows.Forms.Label();
            this.rbPrintSSRSReportPDF = new System.Windows.Forms.RadioButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.rbPrintWordDocEMF = new System.Windows.Forms.RadioButton();
            this.Label207 = new System.Windows.Forms.Label();
            this.rbPrintWordDocPDF = new System.Windows.Forms.RadioButton();
            this.cmbNoPagesSplit = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.Label205 = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.chkAddFooterService = new System.Windows.Forms.CheckBox();
            this.chkEnableLocalPrinter = new System.Windows.Forms.CheckBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnClearPatientContext = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.panel38 = new System.Windows.Forms.Panel();
            this.label164 = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.chk_EnableErrorLogs = new System.Windows.Forms.CheckBox();
            this.chk_EnableApplicationLogs = new System.Windows.Forms.CheckBox();
            this.panel24 = new System.Windows.Forms.Panel();
            this.grpOutboundSettings = new System.Windows.Forms.Panel();
            this.chkSendPatientDetails = new System.Windows.Forms.CheckBox();
            this.chkHL7 = new System.Windows.Forms.CheckBox();
            this.label133 = new System.Windows.Forms.Label();
            this.chkSendAppointmentDetails = new System.Windows.Forms.CheckBox();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.chkGenerateOutboundMsg = new System.Windows.Forms.CheckBox();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.panel37 = new System.Windows.Forms.Panel();
            this.chkUseDefaultPrinter = new System.Windows.Forms.CheckBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.txt_Path = new System.Windows.Forms.TextBox();
            this.chbox_ExptoDefaultlocation = new System.Windows.Forms.CheckBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.chkAutoApplicationLock = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.num_LockScreen = new System.Windows.Forms.NumericUpDown();
            this.label104 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.tbpg_ExchangeSettings = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.txtExchangeDomain = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.txtExchangeURL = new System.Windows.Forms.TextBox();
            this.tbpg_EMRDBSettings = new System.Windows.Forms.TabPage();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.txt_EMRDB_ServerName = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.cmbAuthentication = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.txt_EMRDB_Database = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_EMRDB_Password = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txt_EMRDB_UserName = new System.Windows.Forms.TextBox();
            this.tbpg_ProviderSettings = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label68 = new System.Windows.Forms.Label();
            this.c1Providers = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.tbpg_BillingSettings = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label90 = new System.Windows.Forms.Label();
            this.lblModifiers = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.numModifiers = new System.Windows.Forms.NumericUpDown();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.numDiagnosis = new System.Windows.Forms.NumericUpDown();
            this.label93 = new System.Windows.Forms.Label();
            this.tbpg_AppointmentSettings = new System.Windows.Forms.TabPage();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.label140 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.rbFollowupFromToday = new System.Windows.Forms.RadioButton();
            this.rbFolloupFromDate = new System.Windows.Forms.RadioButton();
            this.label146 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label78 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.num_NoofColOnCalndr = new System.Windows.Forms.NumericUpDown();
            this.lblCalCol = new System.Windows.Forms.Label();
            this.chkShowTemplate = new System.Windows.Forms.CheckBox();
            this.label126 = new System.Windows.Forms.Label();
            this.num_NoofApptInaSlot = new System.Windows.Forms.NumericUpDown();
            this.label127 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.treeView4 = new System.Windows.Forms.TreeView();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.label37 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label40 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.c1FlexGrid2 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.label45 = new System.Windows.Forms.Label();
            this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
            this.label46 = new System.Windows.Forms.Label();
            this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.treeView5 = new System.Windows.Forms.TreeView();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
            this.label47 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.treeView6 = new System.Windows.Forms.TreeView();
            this.label50 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label52 = new System.Windows.Forms.Label();
            this.numericUpDown13 = new System.Windows.Forms.NumericUpDown();
            this.label53 = new System.Windows.Forms.Label();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.c1FlexGrid3 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.label61 = new System.Windows.Forms.Label();
            this.numericUpDown14 = new System.Windows.Forms.NumericUpDown();
            this.label62 = new System.Windows.Forms.Label();
            this.numericUpDown15 = new System.Windows.Forms.NumericUpDown();
            this.clDlg = new System.Windows.Forms.ColorDialog();
            this.pnlMain.SuspendLayout();
            this.tb_Settings.SuspendLayout();
            this.tbpg_GeneralSettings.SuspendLayout();
            this.panel33.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.panel31.SuspendLayout();
            this.pnlPatientSearch.SuspendLayout();
            this.panel25.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel36.SuspendLayout();
            this.panel35.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPatientCodeIncrement)).BeginInit();
            this.gbRemotePrintSetting.SuspendLayout();
            this.pnlPrintImages.SuspendLayout();
            this.pnlPrintClaims.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel38.SuspendLayout();
            this.panel24.SuspendLayout();
            this.grpOutboundSettings.SuspendLayout();
            this.panel37.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LockScreen)).BeginInit();
            this.tbpg_ExchangeSettings.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tbpg_EMRDBSettings.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel22.SuspendLayout();
            this.tbpg_ProviderSettings.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Providers)).BeginInit();
            this.tbpg_BillingSettings.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModifiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiagnosis)).BeginInit();
            this.tbpg_AppointmentSettings.SuspendLayout();
            this.panel29.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NoofColOnCalndr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NoofApptInaSlot)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).BeginInit();
            this.tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
            this.tabPage11.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
            this.groupBox19.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).BeginInit();
            this.tabPage12.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tabPage14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid3)).BeginInit();
            this.tabPage15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.tb_Settings);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(602, 734);
            this.pnlMain.TabIndex = 0;
            // 
            // tb_Settings
            // 
            this.tb_Settings.Controls.Add(this.tbpg_GeneralSettings);
            this.tb_Settings.Controls.Add(this.tbpg_ExchangeSettings);
            this.tb_Settings.Controls.Add(this.tbpg_EMRDBSettings);
            this.tb_Settings.Controls.Add(this.tbpg_ProviderSettings);
            this.tb_Settings.Controls.Add(this.tbpg_BillingSettings);
            this.tb_Settings.Controls.Add(this.tbpg_AppointmentSettings);
            this.tb_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Settings.Location = new System.Drawing.Point(0, 3);
            this.tb_Settings.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Settings.Name = "tb_Settings";
            this.tb_Settings.SelectedIndex = 0;
            this.tb_Settings.Size = new System.Drawing.Size(602, 731);
            this.tb_Settings.TabIndex = 0;
            // 
            // tbpg_GeneralSettings
            // 
            this.tbpg_GeneralSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_GeneralSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_GeneralSettings.Controls.Add(this.panel33);
            this.tbpg_GeneralSettings.Controls.Add(this.panel36);
            this.tbpg_GeneralSettings.Controls.Add(this.panel1);
            this.tbpg_GeneralSettings.Controls.Add(this.gbRemotePrintSetting);
            this.tbpg_GeneralSettings.Controls.Add(this.panel11);
            this.tbpg_GeneralSettings.Controls.Add(this.panel38);
            this.tbpg_GeneralSettings.Controls.Add(this.panel24);
            this.tbpg_GeneralSettings.Controls.Add(this.panel37);
            this.tbpg_GeneralSettings.Controls.Add(this.panel17);
            this.tbpg_GeneralSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_GeneralSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tbpg_GeneralSettings.Name = "tbpg_GeneralSettings";
            this.tbpg_GeneralSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tbpg_GeneralSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_GeneralSettings.TabIndex = 1;
            this.tbpg_GeneralSettings.Text = "General Settings";
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.pnlDashboard);
            this.panel33.Controls.Add(this.pnlPatientSearch);
            this.panel33.Controls.Add(this.pnl_Base);
            this.panel33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel33.Location = new System.Drawing.Point(2, 590);
            this.panel33.Name = "panel33";
            this.panel33.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel33.Size = new System.Drawing.Size(590, 112);
            this.panel33.TabIndex = 7;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDashboard.Controls.Add(this.trvPatientColumns);
            this.pnlDashboard.Controls.Add(this.panel31);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDashboard.Location = new System.Drawing.Point(188, 3);
            this.pnlDashboard.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnlDashboard.Size = new System.Drawing.Size(210, 109);
            this.pnlDashboard.TabIndex = 1;
            // 
            // trvPatientColumns
            // 
            this.trvPatientColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvPatientColumns.CheckBoxes = true;
            this.trvPatientColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvPatientColumns.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvPatientColumns.ForeColor = System.Drawing.Color.Black;
            this.trvPatientColumns.Indent = 20;
            this.trvPatientColumns.ItemHeight = 20;
            this.trvPatientColumns.Location = new System.Drawing.Point(3, 23);
            this.trvPatientColumns.Name = "trvPatientColumns";
            this.trvPatientColumns.Size = new System.Drawing.Size(207, 86);
            this.trvPatientColumns.TabIndex = 0;
            // 
            // panel31
            // 
            this.panel31.BackColor = System.Drawing.Color.Transparent;
            this.panel31.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel31.Controls.Add(this.label66);
            this.panel31.Controls.Add(this.label145);
            this.panel31.Controls.Add(this.label151);
            this.panel31.Controls.Add(this.label150);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel31.Location = new System.Drawing.Point(3, 0);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(207, 23);
            this.panel31.TabIndex = 9;
            // 
            // label66
            // 
            this.label66.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Location = new System.Drawing.Point(1, 1);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(205, 22);
            this.label66.TabIndex = 0;
            this.label66.Text = "Patient Columns on Dashboard  ";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label145
            // 
            this.label145.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label145.Dock = System.Windows.Forms.DockStyle.Left;
            this.label145.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(0, 1);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(1, 22);
            this.label145.TabIndex = 7;
            this.label145.Text = "label4";
            // 
            // label151
            // 
            this.label151.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label151.Dock = System.Windows.Forms.DockStyle.Top;
            this.label151.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(0, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(206, 1);
            this.label151.TabIndex = 5;
            this.label151.Text = "label1";
            // 
            // label150
            // 
            this.label150.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label150.Dock = System.Windows.Forms.DockStyle.Right;
            this.label150.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label150.Location = new System.Drawing.Point(206, 0);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(1, 23);
            this.label150.TabIndex = 6;
            this.label150.Text = "label3";
            // 
            // pnlPatientSearch
            // 
            this.pnlPatientSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatientSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientSearch.Controls.Add(this.trvPatientSearch);
            this.pnlPatientSearch.Controls.Add(this.panel25);
            this.pnlPatientSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPatientSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPatientSearch.Location = new System.Drawing.Point(0, 3);
            this.pnlPatientSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlPatientSearch.Name = "pnlPatientSearch";
            this.pnlPatientSearch.Size = new System.Drawing.Size(188, 109);
            this.pnlPatientSearch.TabIndex = 0;
            // 
            // trvPatientSearch
            // 
            this.trvPatientSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvPatientSearch.CheckBoxes = true;
            this.trvPatientSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvPatientSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvPatientSearch.ForeColor = System.Drawing.Color.Black;
            this.trvPatientSearch.Indent = 20;
            this.trvPatientSearch.ItemHeight = 20;
            this.trvPatientSearch.Location = new System.Drawing.Point(0, 23);
            this.trvPatientSearch.Name = "trvPatientSearch";
            this.trvPatientSearch.Size = new System.Drawing.Size(188, 86);
            this.trvPatientSearch.TabIndex = 0;
            // 
            // panel25
            // 
            this.panel25.BackColor = System.Drawing.Color.Transparent;
            this.panel25.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel25.Controls.Add(this.label137);
            this.panel25.Controls.Add(this.label138);
            this.panel25.Controls.Add(this.label163);
            this.panel25.Controls.Add(this.label166);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel25.Location = new System.Drawing.Point(0, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(188, 23);
            this.panel25.TabIndex = 9;
            // 
            // label137
            // 
            this.label137.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label137.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label137.Location = new System.Drawing.Point(1, 1);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(186, 22);
            this.label137.TabIndex = 0;
            this.label137.Text = "Patient Search";
            this.label137.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label138.Dock = System.Windows.Forms.DockStyle.Left;
            this.label138.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(0, 1);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(1, 22);
            this.label138.TabIndex = 7;
            this.label138.Text = "label4";
            // 
            // label163
            // 
            this.label163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label163.Dock = System.Windows.Forms.DockStyle.Top;
            this.label163.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label163.Location = new System.Drawing.Point(0, 0);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(187, 1);
            this.label163.TabIndex = 5;
            this.label163.Text = "label1";
            // 
            // label166
            // 
            this.label166.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label166.Dock = System.Windows.Forms.DockStyle.Right;
            this.label166.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label166.Location = new System.Drawing.Point(187, 0);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(1, 23);
            this.label166.TabIndex = 6;
            this.label166.Text = "label3";
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.trvDemographics);
            this.pnl_Base.Controls.Add(this.panel32);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(398, 3);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pnl_Base.Size = new System.Drawing.Size(192, 109);
            this.pnl_Base.TabIndex = 2;
            // 
            // trvDemographics
            // 
            this.trvDemographics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvDemographics.CheckBoxes = true;
            this.trvDemographics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDemographics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvDemographics.ForeColor = System.Drawing.Color.Black;
            this.trvDemographics.Indent = 20;
            this.trvDemographics.ItemHeight = 20;
            this.trvDemographics.Location = new System.Drawing.Point(3, 22);
            this.trvDemographics.Name = "trvDemographics";
            this.trvDemographics.ShowNodeToolTips = true;
            this.trvDemographics.Size = new System.Drawing.Size(189, 87);
            this.trvDemographics.TabIndex = 0;
            // 
            // panel32
            // 
            this.panel32.BackColor = System.Drawing.Color.Transparent;
            this.panel32.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel32.Controls.Add(this.lbl_RightBrd);
            this.panel32.Controls.Add(this.lbl_LeftBrd);
            this.panel32.Controls.Add(this.lbl_TopBrd);
            this.panel32.Controls.Add(this.label67);
            this.panel32.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel32.Location = new System.Drawing.Point(3, 0);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(189, 22);
            this.panel32.TabIndex = 6;
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(188, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(189, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Location = new System.Drawing.Point(0, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(189, 22);
            this.label67.TabIndex = 5;
            this.label67.Text = "Patient Demographic ";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel36
            // 
            this.panel36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel36.Controls.Add(this.panel35);
            this.panel36.Controls.Add(this.btnBrowseAlertColor);
            this.panel36.Controls.Add(this.lblAlertColor);
            this.panel36.Controls.Add(this.txtAlertColor);
            this.panel36.Controls.Add(this.chkShowBlinkAlert);
            this.panel36.Controls.Add(this.label158);
            this.panel36.Controls.Add(this.label159);
            this.panel36.Controls.Add(this.label160);
            this.panel36.Controls.Add(this.label162);
            this.panel36.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel36.Location = new System.Drawing.Point(2, 531);
            this.panel36.Name = "panel36";
            this.panel36.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel36.Size = new System.Drawing.Size(590, 59);
            this.panel36.TabIndex = 6;
            // 
            // panel35
            // 
            this.panel35.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel35.Controls.Add(this.label152);
            this.panel35.Controls.Add(this.label153);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel35.Location = new System.Drawing.Point(1, 4);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(588, 24);
            this.panel35.TabIndex = 0;
            // 
            // label152
            // 
            this.label152.BackColor = System.Drawing.Color.Transparent;
            this.label152.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label152.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label152.Location = new System.Drawing.Point(0, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(588, 23);
            this.label152.TabIndex = 9;
            this.label152.Text = "  Patient Alerts";
            this.label152.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label153
            // 
            this.label153.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label153.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label153.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label153.Location = new System.Drawing.Point(0, 23);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(588, 1);
            this.label153.TabIndex = 8;
            this.label153.Text = "label2";
            // 
            // btnBrowseAlertColor
            // 
            this.btnBrowseAlertColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseAlertColor.BackgroundImage")));
            this.btnBrowseAlertColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseAlertColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseAlertColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAlertColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseAlertColor.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseAlertColor.Image")));
            this.btnBrowseAlertColor.Location = new System.Drawing.Point(555, 32);
            this.btnBrowseAlertColor.Name = "btnBrowseAlertColor";
            this.btnBrowseAlertColor.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseAlertColor.TabIndex = 1;
            this.btnBrowseAlertColor.UseVisualStyleBackColor = true;
            this.btnBrowseAlertColor.Click += new System.EventHandler(this.btnBrowseAppColor_Click);
            // 
            // lblAlertColor
            // 
            this.lblAlertColor.AutoSize = true;
            this.lblAlertColor.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertColor.Location = new System.Drawing.Point(447, 36);
            this.lblAlertColor.Name = "lblAlertColor";
            this.lblAlertColor.Size = new System.Drawing.Size(72, 14);
            this.lblAlertColor.TabIndex = 24;
            this.lblAlertColor.Text = "Alert Color :";
            this.lblAlertColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAlertColor
            // 
            this.txtAlertColor.BackColor = System.Drawing.Color.White;
            this.txtAlertColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlertColor.ForeColor = System.Drawing.Color.Black;
            this.txtAlertColor.Location = new System.Drawing.Point(522, 32);
            this.txtAlertColor.Name = "txtAlertColor";
            this.txtAlertColor.ReadOnly = true;
            this.txtAlertColor.Size = new System.Drawing.Size(26, 22);
            this.txtAlertColor.TabIndex = 1;
            this.txtAlertColor.TabStop = false;
            // 
            // chkShowBlinkAlert
            // 
            this.chkShowBlinkAlert.AutoSize = true;
            this.chkShowBlinkAlert.Location = new System.Drawing.Point(24, 34);
            this.chkShowBlinkAlert.Name = "chkShowBlinkAlert";
            this.chkShowBlinkAlert.Size = new System.Drawing.Size(168, 18);
            this.chkShowBlinkAlert.TabIndex = 0;
            this.chkShowBlinkAlert.Text = "Show Blinking Copay Alert";
            this.chkShowBlinkAlert.UseVisualStyleBackColor = true;
            // 
            // label158
            // 
            this.label158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label158.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label158.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label158.Location = new System.Drawing.Point(1, 58);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(588, 1);
            this.label158.TabIndex = 4;
            this.label158.Text = "label2";
            // 
            // label159
            // 
            this.label159.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label159.Dock = System.Windows.Forms.DockStyle.Left;
            this.label159.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label159.Location = new System.Drawing.Point(0, 4);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(1, 55);
            this.label159.TabIndex = 3;
            this.label159.Text = "label4";
            // 
            // label160
            // 
            this.label160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label160.Dock = System.Windows.Forms.DockStyle.Right;
            this.label160.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label160.Location = new System.Drawing.Point(589, 4);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(1, 55);
            this.label160.TabIndex = 2;
            this.label160.Text = "label3";
            // 
            // label162
            // 
            this.label162.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label162.Dock = System.Windows.Forms.DockStyle.Top;
            this.label162.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.Location = new System.Drawing.Point(0, 3);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(590, 1);
            this.label162.TabIndex = 0;
            this.label162.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.cmbDefaultProvider);
            this.panel1.Controls.Add(this.label139);
            this.panel1.Controls.Add(this.cmbSearchColumns);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label71);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label72);
            this.panel1.Controls.Add(this.numPatientCodeIncrement);
            this.panel1.Controls.Add(this.label73);
            this.panel1.Controls.Add(this.txtPatientCodePrefix);
            this.panel1.Controls.Add(this.label74);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(2, 468);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(590, 63);
            this.panel1.TabIndex = 5;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label76);
            this.panel8.Controls.Add(this.label77);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(1, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(588, 24);
            this.panel8.TabIndex = 0;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label76.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Location = new System.Drawing.Point(0, 0);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(588, 23);
            this.label76.TabIndex = 9;
            this.label76.Text = "  Patient Data";
            this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label77.Location = new System.Drawing.Point(0, 23);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(588, 1);
            this.label77.TabIndex = 8;
            this.label77.Text = "label2";
            // 
            // cmbDefaultProvider
            // 
            this.cmbDefaultProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDefaultProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbDefaultProvider.FormattingEnabled = true;
            this.cmbDefaultProvider.Location = new System.Drawing.Point(126, 33);
            this.cmbDefaultProvider.Name = "cmbDefaultProvider";
            this.cmbDefaultProvider.Size = new System.Drawing.Size(458, 22);
            this.cmbDefaultProvider.TabIndex = 0;
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label139.Location = new System.Drawing.Point(19, 37);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(102, 14);
            this.label139.TabIndex = 0;
            this.label139.Text = "Default Provider :";
            // 
            // cmbSearchColumns
            // 
            this.cmbSearchColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchColumns.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchColumns.ForeColor = System.Drawing.Color.Black;
            this.cmbSearchColumns.FormattingEnabled = true;
            this.cmbSearchColumns.Items.AddRange(new object[] {
            "Code",
            "First Name",
            "Last Name"});
            this.cmbSearchColumns.Location = new System.Drawing.Point(157, 81);
            this.cmbSearchColumns.Name = "cmbSearchColumns";
            this.cmbSearchColumns.Size = new System.Drawing.Size(219, 22);
            this.cmbSearchColumns.TabIndex = 0;
            this.cmbSearchColumns.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Default Search Column :";
            this.label4.Visible = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(418, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 14);
            this.label7.TabIndex = 4;
            this.label7.Text = "Patient Code Increment :";
            this.label7.Visible = false;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label71.Location = new System.Drawing.Point(1, 62);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(588, 1);
            this.label71.TabIndex = 4;
            this.label71.Text = "label2";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(485, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "Patient Code Prefix :";
            this.label6.Visible = false;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Left;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(0, 4);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(1, 59);
            this.label72.TabIndex = 3;
            this.label72.Text = "label4";
            // 
            // numPatientCodeIncrement
            // 
            this.numPatientCodeIncrement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPatientCodeIncrement.ForeColor = System.Drawing.Color.Black;
            this.numPatientCodeIncrement.Location = new System.Drawing.Point(458, 82);
            this.numPatientCodeIncrement.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPatientCodeIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPatientCodeIncrement.Name = "numPatientCodeIncrement";
            this.numPatientCodeIncrement.Size = new System.Drawing.Size(23, 22);
            this.numPatientCodeIncrement.TabIndex = 2;
            this.numPatientCodeIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPatientCodeIncrement.Visible = false;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Right;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label73.Location = new System.Drawing.Point(589, 4);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1, 59);
            this.label73.TabIndex = 2;
            this.label73.Text = "label3";
            // 
            // txtPatientCodePrefix
            // 
            this.txtPatientCodePrefix.BackColor = System.Drawing.Color.White;
            this.txtPatientCodePrefix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCodePrefix.ForeColor = System.Drawing.Color.Black;
            this.txtPatientCodePrefix.Location = new System.Drawing.Point(505, 82);
            this.txtPatientCodePrefix.Margin = new System.Windows.Forms.Padding(2);
            this.txtPatientCodePrefix.Name = "txtPatientCodePrefix";
            this.txtPatientCodePrefix.Size = new System.Drawing.Size(17, 22);
            this.txtPatientCodePrefix.TabIndex = 1;
            this.txtPatientCodePrefix.Visible = false;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Dock = System.Windows.Forms.DockStyle.Top;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(0, 3);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(590, 1);
            this.label74.TabIndex = 0;
            this.label74.Text = "label1";
            // 
            // gbRemotePrintSetting
            // 
            this.gbRemotePrintSetting.Controls.Add(this.label85);
            this.gbRemotePrintSetting.Controls.Add(this.cmbNoTemplatesJob);
            this.gbRemotePrintSetting.Controls.Add(this.chkZipMetadata);
            this.gbRemotePrintSetting.Controls.Add(this.pnlPrintImages);
            this.gbRemotePrintSetting.Controls.Add(this.pnlPrintClaims);
            this.gbRemotePrintSetting.Controls.Add(this.panel7);
            this.gbRemotePrintSetting.Controls.Add(this.panel9);
            this.gbRemotePrintSetting.Controls.Add(this.cmbNoPagesSplit);
            this.gbRemotePrintSetting.Controls.Add(this.label63);
            this.gbRemotePrintSetting.Controls.Add(this.Label205);
            this.gbRemotePrintSetting.Controls.Add(this.panel26);
            this.gbRemotePrintSetting.Controls.Add(this.chkAddFooterService);
            this.gbRemotePrintSetting.Controls.Add(this.chkEnableLocalPrinter);
            this.gbRemotePrintSetting.Controls.Add(this.label102);
            this.gbRemotePrintSetting.Controls.Add(this.label103);
            this.gbRemotePrintSetting.Controls.Add(this.label167);
            this.gbRemotePrintSetting.Controls.Add(this.label168);
            this.gbRemotePrintSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRemotePrintSetting.Location = new System.Drawing.Point(2, 305);
            this.gbRemotePrintSetting.Name = "gbRemotePrintSetting";
            this.gbRemotePrintSetting.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gbRemotePrintSetting.Size = new System.Drawing.Size(590, 163);
            this.gbRemotePrintSetting.TabIndex = 4;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Location = new System.Drawing.Point(264, 60);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(134, 14);
            this.label85.TabIndex = 48;
            this.label85.Text = "No. of Templates/Job :";
            // 
            // cmbNoTemplatesJob
            // 
            this.cmbNoTemplatesJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNoTemplatesJob.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNoTemplatesJob.ForeColor = System.Drawing.Color.Black;
            this.cmbNoTemplatesJob.FormattingEnabled = true;
            this.cmbNoTemplatesJob.Location = new System.Drawing.Point(401, 56);
            this.cmbNoTemplatesJob.Name = "cmbNoTemplatesJob";
            this.cmbNoTemplatesJob.Size = new System.Drawing.Size(183, 22);
            this.cmbNoTemplatesJob.TabIndex = 47;
            // 
            // chkZipMetadata
            // 
            this.chkZipMetadata.AutoSize = true;
            this.chkZipMetadata.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkZipMetadata.Location = new System.Drawing.Point(24, 58);
            this.chkZipMetadata.Name = "chkZipMetadata";
            this.chkZipMetadata.Size = new System.Drawing.Size(97, 18);
            this.chkZipMetadata.TabIndex = 44;
            this.chkZipMetadata.Text = "Zip Metadata";
            this.chkZipMetadata.UseVisualStyleBackColor = true;
            this.chkZipMetadata.CheckedChanged += new System.EventHandler(this.chkZipMetadata_CheckedChanged);
            // 
            // pnlPrintImages
            // 
            this.pnlPrintImages.Controls.Add(this.rbPrintImagesEMF);
            this.pnlPrintImages.Controls.Add(this.Label209);
            this.pnlPrintImages.Controls.Add(this.rbPrintImagesPNG);
            this.pnlPrintImages.Location = new System.Drawing.Point(276, 110);
            this.pnlPrintImages.Name = "pnlPrintImages";
            this.pnlPrintImages.Size = new System.Drawing.Size(288, 27);
            this.pnlPrintImages.TabIndex = 43;
            // 
            // rbPrintImagesEMF
            // 
            this.rbPrintImagesEMF.AutoSize = true;
            this.rbPrintImagesEMF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintImagesEMF.Location = new System.Drawing.Point(205, 4);
            this.rbPrintImagesEMF.Name = "rbPrintImagesEMF";
            this.rbPrintImagesEMF.Size = new System.Drawing.Size(47, 18);
            this.rbPrintImagesEMF.TabIndex = 1;
            this.rbPrintImagesEMF.TabStop = true;
            this.rbPrintImagesEMF.Text = "EMF";
            this.rbPrintImagesEMF.UseVisualStyleBackColor = true;
            this.rbPrintImagesEMF.CheckedChanged += new System.EventHandler(this.rbPrintImagesEMF_CheckedChanged);
            // 
            // Label209
            // 
            this.Label209.AutoSize = true;
            this.Label209.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label209.Location = new System.Drawing.Point(38, 6);
            this.Label209.Name = "Label209";
            this.Label209.Size = new System.Drawing.Size(83, 14);
            this.Label209.TabIndex = 39;
            this.Label209.Text = "Print Images :";
            // 
            // rbPrintImagesPNG
            // 
            this.rbPrintImagesPNG.AutoSize = true;
            this.rbPrintImagesPNG.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintImagesPNG.Location = new System.Drawing.Point(129, 4);
            this.rbPrintImagesPNG.Name = "rbPrintImagesPNG";
            this.rbPrintImagesPNG.Size = new System.Drawing.Size(48, 18);
            this.rbPrintImagesPNG.TabIndex = 0;
            this.rbPrintImagesPNG.TabStop = true;
            this.rbPrintImagesPNG.Text = "PNG";
            this.rbPrintImagesPNG.UseVisualStyleBackColor = true;
            this.rbPrintImagesPNG.CheckedChanged += new System.EventHandler(this.rbPrintImagesPNG_CheckedChanged);
            // 
            // pnlPrintClaims
            // 
            this.pnlPrintClaims.Controls.Add(this.rbPrintClaimsEMF);
            this.pnlPrintClaims.Controls.Add(this.Label210);
            this.pnlPrintClaims.Controls.Add(this.rbPrintClaimsPDF);
            this.pnlPrintClaims.Location = new System.Drawing.Point(12, 110);
            this.pnlPrintClaims.Name = "pnlPrintClaims";
            this.pnlPrintClaims.Size = new System.Drawing.Size(262, 27);
            this.pnlPrintClaims.TabIndex = 42;
            // 
            // rbPrintClaimsEMF
            // 
            this.rbPrintClaimsEMF.AutoSize = true;
            this.rbPrintClaimsEMF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintClaimsEMF.Location = new System.Drawing.Point(202, 4);
            this.rbPrintClaimsEMF.Name = "rbPrintClaimsEMF";
            this.rbPrintClaimsEMF.Size = new System.Drawing.Size(47, 18);
            this.rbPrintClaimsEMF.TabIndex = 1;
            this.rbPrintClaimsEMF.TabStop = true;
            this.rbPrintClaimsEMF.Text = "EMF";
            this.rbPrintClaimsEMF.UseVisualStyleBackColor = true;
            this.rbPrintClaimsEMF.CheckedChanged += new System.EventHandler(this.rbPrintClaimsEMF_CheckedChanged);
            // 
            // Label210
            // 
            this.Label210.AutoSize = true;
            this.Label210.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label210.Location = new System.Drawing.Point(67, 6);
            this.Label210.Name = "Label210";
            this.Label210.Size = new System.Drawing.Size(76, 14);
            this.Label210.TabIndex = 36;
            this.Label210.Text = "Print Claims :";
            // 
            // rbPrintClaimsPDF
            // 
            this.rbPrintClaimsPDF.AutoSize = true;
            this.rbPrintClaimsPDF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintClaimsPDF.Location = new System.Drawing.Point(147, 4);
            this.rbPrintClaimsPDF.Name = "rbPrintClaimsPDF";
            this.rbPrintClaimsPDF.Size = new System.Drawing.Size(46, 18);
            this.rbPrintClaimsPDF.TabIndex = 0;
            this.rbPrintClaimsPDF.TabStop = true;
            this.rbPrintClaimsPDF.Text = "PDF";
            this.rbPrintClaimsPDF.UseVisualStyleBackColor = true;
            this.rbPrintClaimsPDF.CheckedChanged += new System.EventHandler(this.rbPrintClaimsPDF_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rbPrintSSRSReportEMF);
            this.panel7.Controls.Add(this.Label208);
            this.panel7.Controls.Add(this.rbPrintSSRSReportPDF);
            this.panel7.Location = new System.Drawing.Point(276, 82);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(288, 27);
            this.panel7.TabIndex = 3;
            // 
            // rbPrintSSRSReportEMF
            // 
            this.rbPrintSSRSReportEMF.AutoSize = true;
            this.rbPrintSSRSReportEMF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintSSRSReportEMF.Location = new System.Drawing.Point(204, 4);
            this.rbPrintSSRSReportEMF.Name = "rbPrintSSRSReportEMF";
            this.rbPrintSSRSReportEMF.Size = new System.Drawing.Size(47, 18);
            this.rbPrintSSRSReportEMF.TabIndex = 1;
            this.rbPrintSSRSReportEMF.TabStop = true;
            this.rbPrintSSRSReportEMF.Text = "EMF";
            this.rbPrintSSRSReportEMF.UseVisualStyleBackColor = true;
            this.rbPrintSSRSReportEMF.CheckedChanged += new System.EventHandler(this.rbPrintSSRSReportEMF_CheckedChanged);
            // 
            // Label208
            // 
            this.Label208.AutoSize = true;
            this.Label208.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label208.Location = new System.Drawing.Point(3, 6);
            this.Label208.Name = "Label208";
            this.Label208.Size = new System.Drawing.Size(118, 14);
            this.Label208.TabIndex = 39;
            this.Label208.Text = "Print SSRS Reports :";
            // 
            // rbPrintSSRSReportPDF
            // 
            this.rbPrintSSRSReportPDF.AutoSize = true;
            this.rbPrintSSRSReportPDF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintSSRSReportPDF.Location = new System.Drawing.Point(128, 4);
            this.rbPrintSSRSReportPDF.Name = "rbPrintSSRSReportPDF";
            this.rbPrintSSRSReportPDF.Size = new System.Drawing.Size(46, 18);
            this.rbPrintSSRSReportPDF.TabIndex = 0;
            this.rbPrintSSRSReportPDF.TabStop = true;
            this.rbPrintSSRSReportPDF.Text = "PDF";
            this.rbPrintSSRSReportPDF.UseVisualStyleBackColor = true;
            this.rbPrintSSRSReportPDF.CheckedChanged += new System.EventHandler(this.rbPrintSSRSReportPDF_CheckedChanged);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.rbPrintWordDocEMF);
            this.panel9.Controls.Add(this.Label207);
            this.panel9.Controls.Add(this.rbPrintWordDocPDF);
            this.panel9.Location = new System.Drawing.Point(12, 82);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(262, 27);
            this.panel9.TabIndex = 2;
            // 
            // rbPrintWordDocEMF
            // 
            this.rbPrintWordDocEMF.AutoSize = true;
            this.rbPrintWordDocEMF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintWordDocEMF.Location = new System.Drawing.Point(201, 4);
            this.rbPrintWordDocEMF.Name = "rbPrintWordDocEMF";
            this.rbPrintWordDocEMF.Size = new System.Drawing.Size(47, 18);
            this.rbPrintWordDocEMF.TabIndex = 1;
            this.rbPrintWordDocEMF.TabStop = true;
            this.rbPrintWordDocEMF.Text = "EMF";
            this.rbPrintWordDocEMF.UseVisualStyleBackColor = true;
            this.rbPrintWordDocEMF.CheckedChanged += new System.EventHandler(this.rbPrintWordDocEMF_CheckedChanged);
            // 
            // Label207
            // 
            this.Label207.AutoSize = true;
            this.Label207.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label207.Location = new System.Drawing.Point(11, 6);
            this.Label207.Name = "Label207";
            this.Label207.Size = new System.Drawing.Size(133, 14);
            this.Label207.TabIndex = 36;
            this.Label207.Text = "Print word Document :";
            // 
            // rbPrintWordDocPDF
            // 
            this.rbPrintWordDocPDF.AutoSize = true;
            this.rbPrintWordDocPDF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrintWordDocPDF.Location = new System.Drawing.Point(146, 4);
            this.rbPrintWordDocPDF.Name = "rbPrintWordDocPDF";
            this.rbPrintWordDocPDF.Size = new System.Drawing.Size(46, 18);
            this.rbPrintWordDocPDF.TabIndex = 0;
            this.rbPrintWordDocPDF.TabStop = true;
            this.rbPrintWordDocPDF.Text = "PDF";
            this.rbPrintWordDocPDF.UseVisualStyleBackColor = true;
            this.rbPrintWordDocPDF.CheckedChanged += new System.EventHandler(this.rbPrintWordDocPDF_CheckedChanged);
            // 
            // cmbNoPagesSplit
            // 
            this.cmbNoPagesSplit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNoPagesSplit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNoPagesSplit.ForeColor = System.Drawing.Color.Black;
            this.cmbNoPagesSplit.FormattingEnabled = true;
            this.cmbNoPagesSplit.Location = new System.Drawing.Point(401, 31);
            this.cmbNoPagesSplit.Name = "cmbNoPagesSplit";
            this.cmbNoPagesSplit.Size = new System.Drawing.Size(183, 22);
            this.cmbNoPagesSplit.TabIndex = 1;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(270, 35);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(128, 14);
            this.label63.TabIndex = 5;
            this.label63.Text = "No. of Pages to Split :";
            // 
            // Label205
            // 
            this.Label205.AutoSize = true;
            this.Label205.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label205.Location = new System.Drawing.Point(8, 143);
            this.Label205.Name = "Label205";
            this.Label205.Size = new System.Drawing.Size(536, 14);
            this.Label205.TabIndex = 35;
            this.Label205.Text = "Note:  If Local printer setting is enabled, default printer will be used as per s" +
    "ervice configuration.";
            // 
            // panel26
            // 
            this.panel26.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel26.Controls.Add(this.label83);
            this.panel26.Controls.Add(this.label84);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel26.Location = new System.Drawing.Point(1, 4);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(588, 24);
            this.panel26.TabIndex = 9;
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.Transparent;
            this.label83.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label83.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Location = new System.Drawing.Point(0, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(588, 23);
            this.label83.TabIndex = 10;
            this.label83.Text = "  Remote Printer Settings";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label84.Location = new System.Drawing.Point(0, 23);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(588, 1);
            this.label84.TabIndex = 8;
            this.label84.Text = "label2";
            // 
            // chkAddFooterService
            // 
            this.chkAddFooterService.AutoSize = true;
            this.chkAddFooterService.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddFooterService.Location = new System.Drawing.Point(24, 184);
            this.chkAddFooterService.Name = "chkAddFooterService";
            this.chkAddFooterService.Size = new System.Drawing.Size(144, 18);
            this.chkAddFooterService.TabIndex = 1;
            this.chkAddFooterService.Text = "Add Footer in Service";
            this.chkAddFooterService.UseVisualStyleBackColor = true;
            this.chkAddFooterService.Visible = false;
            // 
            // chkEnableLocalPrinter
            // 
            this.chkEnableLocalPrinter.AutoSize = true;
            this.chkEnableLocalPrinter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLocalPrinter.Location = new System.Drawing.Point(24, 33);
            this.chkEnableLocalPrinter.Name = "chkEnableLocalPrinter";
            this.chkEnableLocalPrinter.Size = new System.Drawing.Size(133, 18);
            this.chkEnableLocalPrinter.TabIndex = 0;
            this.chkEnableLocalPrinter.Text = "Enable Local Printer";
            this.chkEnableLocalPrinter.UseVisualStyleBackColor = true;
            this.chkEnableLocalPrinter.CheckedChanged += new System.EventHandler(this.chkEnableLocalPrinter_CheckedChanged);
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Dock = System.Windows.Forms.DockStyle.Top;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(1, 3);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(588, 1);
            this.label102.TabIndex = 8;
            this.label102.Text = "label1";
            // 
            // label103
            // 
            this.label103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Dock = System.Windows.Forms.DockStyle.Right;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label103.Location = new System.Drawing.Point(589, 3);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(1, 159);
            this.label103.TabIndex = 7;
            this.label103.Text = "label3";
            // 
            // label167
            // 
            this.label167.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label167.Dock = System.Windows.Forms.DockStyle.Left;
            this.label167.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label167.Location = new System.Drawing.Point(0, 3);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(1, 159);
            this.label167.TabIndex = 6;
            this.label167.Text = "label4";
            // 
            // label168
            // 
            this.label168.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label168.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label168.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label168.Location = new System.Drawing.Point(0, 162);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(590, 1);
            this.label168.TabIndex = 5;
            this.label168.Text = "label2";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.btnClearPatientContext);
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Controls.Add(this.label64);
            this.panel11.Controls.Add(this.label79);
            this.panel11.Controls.Add(this.label80);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(2, 247);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(590, 58);
            this.panel11.TabIndex = 4;
            // 
            // btnClearPatientContext
            // 
            this.btnClearPatientContext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearPatientContext.Location = new System.Drawing.Point(20, 28);
            this.btnClearPatientContext.Name = "btnClearPatientContext";
            this.btnClearPatientContext.Size = new System.Drawing.Size(200, 23);
            this.btnClearPatientContext.TabIndex = 11;
            this.btnClearPatientContext.Text = "Clear Patient Context Setting";
            this.btnClearPatientContext.UseVisualStyleBackColor = true;
            this.btnClearPatientContext.Click += new System.EventHandler(this.btnClearPatientContext_Click);
            // 
            // panel12
            // 
            this.panel12.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.label96);
            this.panel12.Controls.Add(this.label97);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(1, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(588, 24);
            this.panel12.TabIndex = 10;
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.Color.Transparent;
            this.label96.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Location = new System.Drawing.Point(0, 0);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(588, 23);
            this.label96.TabIndex = 10;
            this.label96.Text = "  Patient Context Settings";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label97
            // 
            this.label97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label97.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label97.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label97.Location = new System.Drawing.Point(0, 23);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(588, 1);
            this.label97.TabIndex = 8;
            this.label97.Text = "label2";
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Right;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label64.Location = new System.Drawing.Point(589, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(1, 57);
            this.label64.TabIndex = 7;
            this.label64.Text = "label3";
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Left;
            this.label79.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(0, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1, 57);
            this.label79.TabIndex = 6;
            this.label79.Text = "label4";
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label80.Location = new System.Drawing.Point(0, 57);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(590, 1);
            this.label80.TabIndex = 5;
            this.label80.Text = "label2";
            // 
            // panel38
            // 
            this.panel38.Controls.Add(this.label164);
            this.panel38.Controls.Add(this.label161);
            this.panel38.Controls.Add(this.label157);
            this.panel38.Controls.Add(this.chk_EnableErrorLogs);
            this.panel38.Controls.Add(this.chk_EnableApplicationLogs);
            this.panel38.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel38.Location = new System.Drawing.Point(2, 217);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(590, 30);
            this.panel38.TabIndex = 3;
            // 
            // label164
            // 
            this.label164.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label164.Dock = System.Windows.Forms.DockStyle.Right;
            this.label164.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label164.Location = new System.Drawing.Point(589, 0);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(1, 29);
            this.label164.TabIndex = 7;
            this.label164.Text = "label3";
            // 
            // label161
            // 
            this.label161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label161.Dock = System.Windows.Forms.DockStyle.Left;
            this.label161.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(0, 0);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(1, 29);
            this.label161.TabIndex = 6;
            this.label161.Text = "label4";
            // 
            // label157
            // 
            this.label157.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label157.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label157.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label157.Location = new System.Drawing.Point(0, 29);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(590, 1);
            this.label157.TabIndex = 5;
            this.label157.Text = "label2";
            // 
            // chk_EnableErrorLogs
            // 
            this.chk_EnableErrorLogs.AutoSize = true;
            this.chk_EnableErrorLogs.Location = new System.Drawing.Point(275, 6);
            this.chk_EnableErrorLogs.Name = "chk_EnableErrorLogs";
            this.chk_EnableErrorLogs.Size = new System.Drawing.Size(121, 18);
            this.chk_EnableErrorLogs.TabIndex = 2;
            this.chk_EnableErrorLogs.Text = "Enable Error Logs";
            this.chk_EnableErrorLogs.UseVisualStyleBackColor = true;
            // 
            // chk_EnableApplicationLogs
            // 
            this.chk_EnableApplicationLogs.AutoSize = true;
            this.chk_EnableApplicationLogs.Location = new System.Drawing.Point(21, 6);
            this.chk_EnableApplicationLogs.Name = "chk_EnableApplicationLogs";
            this.chk_EnableApplicationLogs.Size = new System.Drawing.Size(154, 18);
            this.chk_EnableApplicationLogs.TabIndex = 1;
            this.chk_EnableApplicationLogs.Text = "Enable Application Logs";
            this.chk_EnableApplicationLogs.UseVisualStyleBackColor = true;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.grpOutboundSettings);
            this.panel24.Controls.Add(this.chkGenerateOutboundMsg);
            this.panel24.Controls.Add(this.label130);
            this.panel24.Controls.Add(this.label131);
            this.panel24.Controls.Add(this.label132);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel24.Location = new System.Drawing.Point(2, 137);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(590, 80);
            this.panel24.TabIndex = 2;
            // 
            // grpOutboundSettings
            // 
            this.grpOutboundSettings.Controls.Add(this.chkSendPatientDetails);
            this.grpOutboundSettings.Controls.Add(this.chkHL7);
            this.grpOutboundSettings.Controls.Add(this.label133);
            this.grpOutboundSettings.Controls.Add(this.chkSendAppointmentDetails);
            this.grpOutboundSettings.Controls.Add(this.label134);
            this.grpOutboundSettings.Controls.Add(this.label135);
            this.grpOutboundSettings.Controls.Add(this.label136);
            this.grpOutboundSettings.Location = new System.Drawing.Point(42, 26);
            this.grpOutboundSettings.Name = "grpOutboundSettings";
            this.grpOutboundSettings.Size = new System.Drawing.Size(422, 48);
            this.grpOutboundSettings.TabIndex = 2;
            // 
            // chkSendPatientDetails
            // 
            this.chkSendPatientDetails.AutoSize = true;
            this.chkSendPatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendPatientDetails.Location = new System.Drawing.Point(54, 23);
            this.chkSendPatientDetails.Name = "chkSendPatientDetails";
            this.chkSendPatientDetails.Size = new System.Drawing.Size(136, 18);
            this.chkSendPatientDetails.TabIndex = 2;
            this.chkSendPatientDetails.Text = "Send Patient Details";
            this.chkSendPatientDetails.UseVisualStyleBackColor = true;
            // 
            // chkHL7
            // 
            this.chkHL7.AutoSize = true;
            this.chkHL7.Location = new System.Drawing.Point(16, 5);
            this.chkHL7.Name = "chkHL7";
            this.chkHL7.Size = new System.Drawing.Size(47, 18);
            this.chkHL7.TabIndex = 1;
            this.chkHL7.Text = "HL7";
            this.chkHL7.UseVisualStyleBackColor = true;
            // 
            // label133
            // 
            this.label133.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label133.Dock = System.Windows.Forms.DockStyle.Top;
            this.label133.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(1, 0);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(420, 1);
            this.label133.TabIndex = 8;
            this.label133.Text = "label1";
            // 
            // chkSendAppointmentDetails
            // 
            this.chkSendAppointmentDetails.AutoSize = true;
            this.chkSendAppointmentDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendAppointmentDetails.Location = new System.Drawing.Point(232, 23);
            this.chkSendAppointmentDetails.Name = "chkSendAppointmentDetails";
            this.chkSendAppointmentDetails.Size = new System.Drawing.Size(169, 18);
            this.chkSendAppointmentDetails.TabIndex = 3;
            this.chkSendAppointmentDetails.Text = "Send Appointment Details";
            this.chkSendAppointmentDetails.UseVisualStyleBackColor = true;
            // 
            // label134
            // 
            this.label134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label134.Dock = System.Windows.Forms.DockStyle.Right;
            this.label134.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label134.Location = new System.Drawing.Point(421, 0);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(1, 47);
            this.label134.TabIndex = 7;
            this.label134.Text = "label3";
            // 
            // label135
            // 
            this.label135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label135.Dock = System.Windows.Forms.DockStyle.Left;
            this.label135.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(0, 0);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(1, 47);
            this.label135.TabIndex = 6;
            this.label135.Text = "label4";
            // 
            // label136
            // 
            this.label136.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label136.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label136.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label136.Location = new System.Drawing.Point(0, 47);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(422, 1);
            this.label136.TabIndex = 5;
            this.label136.Text = "label2";
            // 
            // chkGenerateOutboundMsg
            // 
            this.chkGenerateOutboundMsg.AutoSize = true;
            this.chkGenerateOutboundMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGenerateOutboundMsg.Location = new System.Drawing.Point(22, 6);
            this.chkGenerateOutboundMsg.Name = "chkGenerateOutboundMsg";
            this.chkGenerateOutboundMsg.Size = new System.Drawing.Size(204, 18);
            this.chkGenerateOutboundMsg.TabIndex = 1;
            this.chkGenerateOutboundMsg.Text = "Generate Outbound Message";
            this.chkGenerateOutboundMsg.UseVisualStyleBackColor = true;
            this.chkGenerateOutboundMsg.CheckedChanged += new System.EventHandler(this.chkGenerateOutboundMsg_CheckedChanged);
            // 
            // label130
            // 
            this.label130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label130.Dock = System.Windows.Forms.DockStyle.Right;
            this.label130.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label130.Location = new System.Drawing.Point(589, 0);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(1, 79);
            this.label130.TabIndex = 7;
            this.label130.Text = "label3";
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label131.Dock = System.Windows.Forms.DockStyle.Left;
            this.label131.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(0, 0);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(1, 79);
            this.label131.TabIndex = 6;
            this.label131.Text = "label4";
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label132.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label132.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label132.Location = new System.Drawing.Point(0, 79);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(590, 1);
            this.label132.TabIndex = 5;
            this.label132.Text = "label2";
            // 
            // panel37
            // 
            this.panel37.Controls.Add(this.chkUseDefaultPrinter);
            this.panel37.Controls.Add(this.panel10);
            this.panel37.Controls.Add(this.label87);
            this.panel37.Controls.Add(this.label65);
            this.panel37.Controls.Add(this.btn_Clear);
            this.panel37.Controls.Add(this.btn_Browse);
            this.panel37.Controls.Add(this.txt_Path);
            this.panel37.Controls.Add(this.chbox_ExptoDefaultlocation);
            this.panel37.Controls.Add(this.label89);
            this.panel37.Controls.Add(this.label88);
            this.panel37.Controls.Add(this.label86);
            this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel37.Location = new System.Drawing.Point(2, 57);
            this.panel37.Name = "panel37";
            this.panel37.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel37.Size = new System.Drawing.Size(590, 80);
            this.panel37.TabIndex = 1;
            // 
            // chkUseDefaultPrinter
            // 
            this.chkUseDefaultPrinter.AutoSize = true;
            this.chkUseDefaultPrinter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseDefaultPrinter.Location = new System.Drawing.Point(21, 56);
            this.chkUseDefaultPrinter.Name = "chkUseDefaultPrinter";
            this.chkUseDefaultPrinter.Size = new System.Drawing.Size(154, 18);
            this.chkUseDefaultPrinter.TabIndex = 41;
            this.chkUseDefaultPrinter.Text = "Default Printer Settings";
            this.chkUseDefaultPrinter.UseVisualStyleBackColor = true;
            // 
            // panel10
            // 
            this.panel10.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.label81);
            this.panel10.Controls.Add(this.label82);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(1, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(588, 24);
            this.panel10.TabIndex = 0;
            // 
            // label81
            // 
            this.label81.BackColor = System.Drawing.Color.Transparent;
            this.label81.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Location = new System.Drawing.Point(0, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(588, 23);
            this.label81.TabIndex = 10;
            this.label81.Text = "  Other Settings";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label82.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label82.Location = new System.Drawing.Point(0, 23);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(588, 1);
            this.label82.TabIndex = 8;
            this.label82.Text = "label2";
            // 
            // label87
            // 
            this.label87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label87.Dock = System.Windows.Forms.DockStyle.Left;
            this.label87.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label87.Location = new System.Drawing.Point(0, 4);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(1, 75);
            this.label87.TabIndex = 39;
            this.label87.Text = "label3";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(257, 35);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(85, 14);
            this.label65.TabIndex = 38;
            this.label65.Text = " Report Path :";
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Clear.BackgroundImage")));
            this.btn_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_Clear.Image")));
            this.btn_Clear.Location = new System.Drawing.Point(555, 31);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(22, 22);
            this.btn_Clear.TabIndex = 3;
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Browse
            // 
            this.btn_Browse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Browse.BackgroundImage")));
            this.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Browse.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Image = ((System.Drawing.Image)(resources.GetObject("btn_Browse.Image")));
            this.btn_Browse.Location = new System.Drawing.Point(530, 31);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(22, 22);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // txt_Path
            // 
            this.txt_Path.BackColor = System.Drawing.Color.White;
            this.txt_Path.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Path.ForeColor = System.Drawing.Color.Black;
            this.txt_Path.Location = new System.Drawing.Point(344, 31);
            this.txt_Path.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Path.Name = "txt_Path";
            this.txt_Path.ReadOnly = true;
            this.txt_Path.Size = new System.Drawing.Size(181, 22);
            this.txt_Path.TabIndex = 2;
            // 
            // chbox_ExptoDefaultlocation
            // 
            this.chbox_ExptoDefaultlocation.AutoSize = true;
            this.chbox_ExptoDefaultlocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbox_ExptoDefaultlocation.Location = new System.Drawing.Point(21, 35);
            this.chbox_ExptoDefaultlocation.Name = "chbox_ExptoDefaultlocation";
            this.chbox_ExptoDefaultlocation.Size = new System.Drawing.Size(215, 18);
            this.chbox_ExptoDefaultlocation.TabIndex = 0;
            this.chbox_ExptoDefaultlocation.Text = "Export Report To Default Location";
            this.chbox_ExptoDefaultlocation.UseVisualStyleBackColor = true;
            this.chbox_ExptoDefaultlocation.CheckedChanged += new System.EventHandler(this.chbox_ExptoDefaultlocation_CheckedChanged);
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label89.Dock = System.Windows.Forms.DockStyle.Top;
            this.label89.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(0, 3);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(589, 1);
            this.label89.TabIndex = 30;
            this.label89.Text = "label1";
            // 
            // label88
            // 
            this.label88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label88.Dock = System.Windows.Forms.DockStyle.Right;
            this.label88.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label88.Location = new System.Drawing.Point(589, 3);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(1, 76);
            this.label88.TabIndex = 29;
            this.label88.Text = "label3";
            // 
            // label86
            // 
            this.label86.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label86.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label86.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label86.Location = new System.Drawing.Point(0, 79);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(590, 1);
            this.label86.TabIndex = 27;
            this.label86.Text = "label2";
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel17.Controls.Add(this.panel16);
            this.panel17.Controls.Add(this.chkAutoApplicationLock);
            this.panel17.Controls.Add(this.label2);
            this.panel17.Controls.Add(this.num_LockScreen);
            this.panel17.Controls.Add(this.label104);
            this.panel17.Controls.Add(this.label1);
            this.panel17.Controls.Add(this.label105);
            this.panel17.Controls.Add(this.label106);
            this.panel17.Controls.Add(this.label107);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel17.Location = new System.Drawing.Point(2, 2);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(590, 55);
            this.panel17.TabIndex = 0;
            // 
            // panel16
            // 
            this.panel16.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.label99);
            this.panel16.Controls.Add(this.label100);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(1, 1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(588, 24);
            this.panel16.TabIndex = 0;
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.Color.Transparent;
            this.label99.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Location = new System.Drawing.Point(0, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(588, 23);
            this.label99.TabIndex = 10;
            this.label99.Text = "   Lock Screen Settings";
            this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label100
            // 
            this.label100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label100.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label100.Location = new System.Drawing.Point(0, 23);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(588, 1);
            this.label100.TabIndex = 8;
            this.label100.Text = "label2";
            // 
            // chkAutoApplicationLock
            // 
            this.chkAutoApplicationLock.AutoSize = true;
            this.chkAutoApplicationLock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoApplicationLock.Location = new System.Drawing.Point(20, 30);
            this.chkAutoApplicationLock.Name = "chkAutoApplicationLock";
            this.chkAutoApplicationLock.Size = new System.Drawing.Size(145, 18);
            this.chkAutoApplicationLock.TabIndex = 1;
            this.chkAutoApplicationLock.Text = "Auto Application Lock";
            this.chkAutoApplicationLock.UseVisualStyleBackColor = true;
            this.chkAutoApplicationLock.CheckedChanged += new System.EventHandler(this.chkAutoApplicationLock_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(240, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "(In Minutes)";
            // 
            // num_LockScreen
            // 
            this.num_LockScreen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_LockScreen.ForeColor = System.Drawing.Color.Black;
            this.num_LockScreen.Location = new System.Drawing.Point(168, 28);
            this.num_LockScreen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_LockScreen.Name = "num_LockScreen";
            this.num_LockScreen.Size = new System.Drawing.Size(63, 22);
            this.num_LockScreen.TabIndex = 2;
            this.num_LockScreen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_LockScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.num_LockScreen_KeyDown);
            this.num_LockScreen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_LockScreen_KeyPress);
            // 
            // label104
            // 
            this.label104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label104.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label104.Location = new System.Drawing.Point(1, 54);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(588, 1);
            this.label104.TabIndex = 4;
            this.label104.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application Lock Time :";
            this.label1.Visible = false;
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Dock = System.Windows.Forms.DockStyle.Left;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(0, 1);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(1, 54);
            this.label105.TabIndex = 3;
            this.label105.Text = "label4";
            // 
            // label106
            // 
            this.label106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label106.Dock = System.Windows.Forms.DockStyle.Right;
            this.label106.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label106.Location = new System.Drawing.Point(589, 1);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(1, 54);
            this.label106.TabIndex = 2;
            this.label106.Text = "label3";
            // 
            // label107
            // 
            this.label107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label107.Dock = System.Windows.Forms.DockStyle.Top;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(0, 0);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(590, 1);
            this.label107.TabIndex = 0;
            this.label107.Text = "label1";
            // 
            // tbpg_ExchangeSettings
            // 
            this.tbpg_ExchangeSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_ExchangeSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_ExchangeSettings.Controls.Add(this.panel20);
            this.tbpg_ExchangeSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_ExchangeSettings.Name = "tbpg_ExchangeSettings";
            this.tbpg_ExchangeSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_ExchangeSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_ExchangeSettings.TabIndex = 2;
            this.tbpg_ExchangeSettings.Text = "Exchange Server Settings";
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel20.Controls.Add(this.panel19);
            this.panel20.Controls.Add(this.txtExchangeDomain);
            this.panel20.Controls.Add(this.label113);
            this.panel20.Controls.Add(this.label12);
            this.panel20.Controls.Add(this.label114);
            this.panel20.Controls.Add(this.label13);
            this.panel20.Controls.Add(this.label115);
            this.panel20.Controls.Add(this.txtExchangeURL);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel20.Location = new System.Drawing.Point(3, 3);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(588, 698);
            this.panel20.TabIndex = 19;
            // 
            // panel19
            // 
            this.panel19.BackgroundImage = global::gloSettings.Properties.Resources.Img_Button;
            this.panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel19.Controls.Add(this.label108);
            this.panel19.Controls.Add(this.label109);
            this.panel19.Controls.Add(this.label112);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel19.Location = new System.Drawing.Point(1, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(586, 24);
            this.panel19.TabIndex = 0;
            // 
            // label108
            // 
            this.label108.BackColor = System.Drawing.Color.Transparent;
            this.label108.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label108.Location = new System.Drawing.Point(0, 1);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(586, 22);
            this.label108.TabIndex = 10;
            this.label108.Text = "  Exchange Server Settings";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label109
            // 
            this.label109.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label109.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label109.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label109.Location = new System.Drawing.Point(0, 23);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(586, 1);
            this.label109.TabIndex = 8;
            this.label109.Text = "label2";
            // 
            // label112
            // 
            this.label112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label112.Dock = System.Windows.Forms.DockStyle.Top;
            this.label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(0, 0);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(586, 1);
            this.label112.TabIndex = 5;
            this.label112.Text = "label1";
            // 
            // txtExchangeDomain
            // 
            this.txtExchangeDomain.BackColor = System.Drawing.Color.White;
            this.txtExchangeDomain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExchangeDomain.ForeColor = System.Drawing.Color.Black;
            this.txtExchangeDomain.Location = new System.Drawing.Point(245, 44);
            this.txtExchangeDomain.Margin = new System.Windows.Forms.Padding(2);
            this.txtExchangeDomain.Name = "txtExchangeDomain";
            this.txtExchangeDomain.Size = new System.Drawing.Size(188, 22);
            this.txtExchangeDomain.TabIndex = 0;
            // 
            // label113
            // 
            this.label113.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label113.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label113.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label113.Location = new System.Drawing.Point(1, 697);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(586, 1);
            this.label113.TabIndex = 4;
            this.label113.Text = "label2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(148, 76);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 14);
            this.label12.TabIndex = 72;
            this.label12.Text = "Exchange URL :";
            // 
            // label114
            // 
            this.label114.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label114.Dock = System.Windows.Forms.DockStyle.Left;
            this.label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(0, 0);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(1, 698);
            this.label114.TabIndex = 3;
            this.label114.Text = "label4";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(129, 48);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 14);
            this.label13.TabIndex = 74;
            this.label13.Text = "Exchange Domain :";
            // 
            // label115
            // 
            this.label115.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label115.Dock = System.Windows.Forms.DockStyle.Right;
            this.label115.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label115.Location = new System.Drawing.Point(587, 0);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(1, 698);
            this.label115.TabIndex = 2;
            this.label115.Text = "label3";
            // 
            // txtExchangeURL
            // 
            this.txtExchangeURL.BackColor = System.Drawing.Color.White;
            this.txtExchangeURL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExchangeURL.ForeColor = System.Drawing.Color.Black;
            this.txtExchangeURL.Location = new System.Drawing.Point(245, 72);
            this.txtExchangeURL.Margin = new System.Windows.Forms.Padding(2);
            this.txtExchangeURL.Name = "txtExchangeURL";
            this.txtExchangeURL.Size = new System.Drawing.Size(188, 22);
            this.txtExchangeURL.TabIndex = 1;
            // 
            // tbpg_EMRDBSettings
            // 
            this.tbpg_EMRDBSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_EMRDBSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_EMRDBSettings.Controls.Add(this.panel23);
            this.tbpg_EMRDBSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_EMRDBSettings.Name = "tbpg_EMRDBSettings";
            this.tbpg_EMRDBSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_EMRDBSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_EMRDBSettings.TabIndex = 3;
            this.tbpg_EMRDBSettings.Text = "gloEMR Database Settings";
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel23.Controls.Add(this.panel22);
            this.panel23.Controls.Add(this.label31);
            this.panel23.Controls.Add(this.label122);
            this.panel23.Controls.Add(this.txt_EMRDB_ServerName);
            this.panel23.Controls.Add(this.label123);
            this.panel23.Controls.Add(this.cmbAuthentication);
            this.panel23.Controls.Add(this.label32);
            this.panel23.Controls.Add(this.label124);
            this.panel23.Controls.Add(this.txt_EMRDB_Database);
            this.panel23.Controls.Add(this.label33);
            this.panel23.Controls.Add(this.txt_EMRDB_Password);
            this.panel23.Controls.Add(this.label34);
            this.panel23.Controls.Add(this.label35);
            this.panel23.Controls.Add(this.txt_EMRDB_UserName);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel23.Location = new System.Drawing.Point(3, 3);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(588, 698);
            this.panel23.TabIndex = 19;
            // 
            // panel22
            // 
            this.panel22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel22.BackgroundImage")));
            this.panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel22.Controls.Add(this.label117);
            this.panel22.Controls.Add(this.label118);
            this.panel22.Controls.Add(this.label121);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Location = new System.Drawing.Point(1, 0);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(586, 24);
            this.panel22.TabIndex = 0;
            // 
            // label117
            // 
            this.label117.BackColor = System.Drawing.Color.Transparent;
            this.label117.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label117.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(0, 1);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(586, 22);
            this.label117.TabIndex = 10;
            this.label117.Text = "  gloEMR Database Settings";
            this.label117.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label118
            // 
            this.label118.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label118.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label118.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label118.Location = new System.Drawing.Point(0, 23);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(586, 1);
            this.label118.TabIndex = 8;
            this.label118.Text = "label2";
            // 
            // label121
            // 
            this.label121.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label121.Dock = System.Windows.Forms.DockStyle.Top;
            this.label121.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(0, 0);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(586, 1);
            this.label121.TabIndex = 5;
            this.label121.Text = "label1";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(79, 44);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(111, 14);
            this.label31.TabIndex = 8;
            this.label31.Text = "SQL Server Name :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label122
            // 
            this.label122.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label122.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label122.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label122.Location = new System.Drawing.Point(1, 697);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(586, 1);
            this.label122.TabIndex = 4;
            this.label122.Text = "label2";
            // 
            // txt_EMRDB_ServerName
            // 
            this.txt_EMRDB_ServerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EMRDB_ServerName.ForeColor = System.Drawing.Color.Black;
            this.txt_EMRDB_ServerName.Location = new System.Drawing.Point(197, 42);
            this.txt_EMRDB_ServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EMRDB_ServerName.Name = "txt_EMRDB_ServerName";
            this.txt_EMRDB_ServerName.Size = new System.Drawing.Size(273, 22);
            this.txt_EMRDB_ServerName.TabIndex = 0;
            // 
            // label123
            // 
            this.label123.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label123.Dock = System.Windows.Forms.DockStyle.Left;
            this.label123.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(0, 0);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(1, 698);
            this.label123.TabIndex = 3;
            this.label123.Text = "label4";
            // 
            // cmbAuthentication
            // 
            this.cmbAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthentication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAuthentication.ForeColor = System.Drawing.Color.Black;
            this.cmbAuthentication.FormattingEnabled = true;
            this.cmbAuthentication.Location = new System.Drawing.Point(197, 104);
            this.cmbAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAuthentication.Name = "cmbAuthentication";
            this.cmbAuthentication.Size = new System.Drawing.Size(273, 22);
            this.cmbAuthentication.TabIndex = 2;
            this.cmbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(90, 75);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(100, 14);
            this.label32.TabIndex = 9;
            this.label32.Text = "Database Name :";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label124
            // 
            this.label124.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label124.Dock = System.Windows.Forms.DockStyle.Right;
            this.label124.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label124.Location = new System.Drawing.Point(587, 0);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(1, 698);
            this.label124.TabIndex = 2;
            this.label124.Text = "label3";
            // 
            // txt_EMRDB_Database
            // 
            this.txt_EMRDB_Database.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EMRDB_Database.ForeColor = System.Drawing.Color.Black;
            this.txt_EMRDB_Database.Location = new System.Drawing.Point(197, 73);
            this.txt_EMRDB_Database.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EMRDB_Database.Name = "txt_EMRDB_Database";
            this.txt_EMRDB_Database.Size = new System.Drawing.Size(273, 22);
            this.txt_EMRDB_Database.TabIndex = 1;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(94, 106);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(96, 14);
            this.label33.TabIndex = 0;
            this.label33.Text = "Authentication :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_EMRDB_Password
            // 
            this.txt_EMRDB_Password.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EMRDB_Password.ForeColor = System.Drawing.Color.Black;
            this.txt_EMRDB_Password.Location = new System.Drawing.Point(197, 166);
            this.txt_EMRDB_Password.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EMRDB_Password.Name = "txt_EMRDB_Password";
            this.txt_EMRDB_Password.PasswordChar = '*';
            this.txt_EMRDB_Password.Size = new System.Drawing.Size(273, 22);
            this.txt_EMRDB_Password.TabIndex = 4;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Location = new System.Drawing.Point(116, 137);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(74, 14);
            this.label34.TabIndex = 1;
            this.label34.Text = "User Name :";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(124, 168);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(66, 14);
            this.label35.TabIndex = 2;
            this.label35.Text = "Password :";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_EMRDB_UserName
            // 
            this.txt_EMRDB_UserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EMRDB_UserName.ForeColor = System.Drawing.Color.Black;
            this.txt_EMRDB_UserName.Location = new System.Drawing.Point(197, 135);
            this.txt_EMRDB_UserName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EMRDB_UserName.Name = "txt_EMRDB_UserName";
            this.txt_EMRDB_UserName.Size = new System.Drawing.Size(273, 22);
            this.txt_EMRDB_UserName.TabIndex = 3;
            // 
            // tbpg_ProviderSettings
            // 
            this.tbpg_ProviderSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_ProviderSettings.Controls.Add(this.panel5);
            this.tbpg_ProviderSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_ProviderSettings.Name = "tbpg_ProviderSettings";
            this.tbpg_ProviderSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_ProviderSettings.TabIndex = 4;
            this.tbpg_ProviderSettings.Text = "Provider Settings";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel5.Controls.Add(this.label68);
            this.panel5.Controls.Add(this.c1Providers);
            this.panel5.Controls.Add(this.label69);
            this.panel5.Controls.Add(this.label70);
            this.panel5.Controls.Add(this.label75);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(594, 704);
            this.panel5.TabIndex = 13;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label68.Location = new System.Drawing.Point(1, 703);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(592, 1);
            this.label68.TabIndex = 4;
            this.label68.Text = "label2";
            // 
            // c1Providers
            // 
            this.c1Providers.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Providers.AllowEditing = false;
            this.c1Providers.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Providers.AutoGenerateColumns = false;
            this.c1Providers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Providers.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Providers.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1Providers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Providers.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1Providers.Location = new System.Drawing.Point(1, 1);
            this.c1Providers.Name = "c1Providers";
            this.c1Providers.Rows.Count = 1;
            this.c1Providers.Rows.DefaultSize = 21;
            this.c1Providers.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Providers.Size = new System.Drawing.Size(592, 703);
            this.c1Providers.StyleInfo = resources.GetString("c1Providers.StyleInfo");
            this.c1Providers.TabIndex = 12;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Left;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(0, 1);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1, 703);
            this.label69.TabIndex = 3;
            this.label69.Text = "label4";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(593, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 703);
            this.label70.TabIndex = 2;
            this.label70.Text = "label3";
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Top;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(0, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(594, 1);
            this.label75.TabIndex = 0;
            this.label75.Text = "label1";
            // 
            // tbpg_BillingSettings
            // 
            this.tbpg_BillingSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_BillingSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_BillingSettings.Controls.Add(this.panel6);
            this.tbpg_BillingSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_BillingSettings.Name = "tbpg_BillingSettings";
            this.tbpg_BillingSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_BillingSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_BillingSettings.TabIndex = 5;
            this.tbpg_BillingSettings.Tag = "BillingSettings";
            this.tbpg_BillingSettings.Text = "Billing Settings";
            this.tbpg_BillingSettings.ToolTipText = "Billing Settings";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel6.Controls.Add(this.label90);
            this.panel6.Controls.Add(this.lblModifiers);
            this.panel6.Controls.Add(this.label91);
            this.panel6.Controls.Add(this.numModifiers);
            this.panel6.Controls.Add(this.lblDiagnosis);
            this.panel6.Controls.Add(this.label92);
            this.panel6.Controls.Add(this.numDiagnosis);
            this.panel6.Controls.Add(this.label93);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(588, 698);
            this.panel6.TabIndex = 23;
            // 
            // label90
            // 
            this.label90.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label90.Location = new System.Drawing.Point(1, 697);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(586, 1);
            this.label90.TabIndex = 4;
            this.label90.Text = "label2";
            // 
            // lblModifiers
            // 
            this.lblModifiers.AutoSize = true;
            this.lblModifiers.Location = new System.Drawing.Point(181, 58);
            this.lblModifiers.Name = "lblModifiers";
            this.lblModifiers.Size = new System.Drawing.Size(96, 14);
            this.lblModifiers.TabIndex = 22;
            this.lblModifiers.Text = "No of Modifiers :";
            // 
            // label91
            // 
            this.label91.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label91.Dock = System.Windows.Forms.DockStyle.Left;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(0, 1);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(1, 697);
            this.label91.TabIndex = 3;
            this.label91.Text = "label4";
            // 
            // numModifiers
            // 
            this.numModifiers.Location = new System.Drawing.Point(283, 55);
            this.numModifiers.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numModifiers.Name = "numModifiers";
            this.numModifiers.ReadOnly = true;
            this.numModifiers.Size = new System.Drawing.Size(47, 22);
            this.numModifiers.TabIndex = 1;
            this.numModifiers.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Location = new System.Drawing.Point(179, 25);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(98, 14);
            this.lblDiagnosis.TabIndex = 20;
            this.lblDiagnosis.Text = "No of Diagnosis :";
            // 
            // label92
            // 
            this.label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label92.Dock = System.Windows.Forms.DockStyle.Right;
            this.label92.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label92.Location = new System.Drawing.Point(587, 1);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(1, 697);
            this.label92.TabIndex = 2;
            this.label92.Text = "label3";
            // 
            // numDiagnosis
            // 
            this.numDiagnosis.Location = new System.Drawing.Point(284, 22);
            this.numDiagnosis.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numDiagnosis.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numDiagnosis.Name = "numDiagnosis";
            this.numDiagnosis.ReadOnly = true;
            this.numDiagnosis.Size = new System.Drawing.Size(47, 22);
            this.numDiagnosis.TabIndex = 0;
            this.numDiagnosis.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label93
            // 
            this.label93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Dock = System.Windows.Forms.DockStyle.Top;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(0, 0);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(588, 1);
            this.label93.TabIndex = 0;
            this.label93.Text = "label1";
            // 
            // tbpg_AppointmentSettings
            // 
            this.tbpg_AppointmentSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_AppointmentSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_AppointmentSettings.Controls.Add(this.panel29);
            this.tbpg_AppointmentSettings.Controls.Add(this.panel14);
            this.tbpg_AppointmentSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_AppointmentSettings.Name = "tbpg_AppointmentSettings";
            this.tbpg_AppointmentSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_AppointmentSettings.Size = new System.Drawing.Size(594, 704);
            this.tbpg_AppointmentSettings.TabIndex = 6;
            this.tbpg_AppointmentSettings.Tag = "Appointment Settings ";
            this.tbpg_AppointmentSettings.Text = "Appointment Settings ";
            this.tbpg_AppointmentSettings.ToolTipText = "Appointment Settings ";
            // 
            // panel29
            // 
            this.panel29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel29.Controls.Add(this.panel28);
            this.panel29.Controls.Add(this.rbFollowupFromToday);
            this.panel29.Controls.Add(this.rbFolloupFromDate);
            this.panel29.Controls.Add(this.label146);
            this.panel29.Controls.Add(this.label147);
            this.panel29.Controls.Add(this.label148);
            this.panel29.Controls.Add(this.label149);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel29.Location = new System.Drawing.Point(3, 93);
            this.panel29.Name = "panel29";
            this.panel29.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel29.Size = new System.Drawing.Size(588, 608);
            this.panel29.TabIndex = 3;
            // 
            // panel28
            // 
            this.panel28.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel28.BackgroundImage")));
            this.panel28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel28.Controls.Add(this.label140);
            this.panel28.Controls.Add(this.label141);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(1, 4);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(586, 24);
            this.panel28.TabIndex = 0;
            // 
            // label140
            // 
            this.label140.BackColor = System.Drawing.Color.Transparent;
            this.label140.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label140.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(0, 0);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(586, 23);
            this.label140.TabIndex = 10;
            this.label140.Text = "   Followup";
            this.label140.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label141
            // 
            this.label141.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label141.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label141.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label141.Location = new System.Drawing.Point(0, 23);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(586, 1);
            this.label141.TabIndex = 8;
            this.label141.Text = "label2";
            // 
            // rbFollowupFromToday
            // 
            this.rbFollowupFromToday.AutoSize = true;
            this.rbFollowupFromToday.Checked = true;
            this.rbFollowupFromToday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFollowupFromToday.Location = new System.Drawing.Point(223, 46);
            this.rbFollowupFromToday.Name = "rbFollowupFromToday";
            this.rbFollowupFromToday.Size = new System.Drawing.Size(131, 18);
            this.rbFollowupFromToday.TabIndex = 1;
            this.rbFollowupFromToday.TabStop = true;
            this.rbFollowupFromToday.Text = "Start from Today";
            this.rbFollowupFromToday.UseVisualStyleBackColor = true;
            this.rbFollowupFromToday.CheckedChanged += new System.EventHandler(this.rbFollowupFromToday_CheckedChanged);
            // 
            // rbFolloupFromDate
            // 
            this.rbFolloupFromDate.AutoSize = true;
            this.rbFolloupFromDate.Location = new System.Drawing.Point(35, 46);
            this.rbFolloupFromDate.Name = "rbFolloupFromDate";
            this.rbFolloupFromDate.Size = new System.Drawing.Size(167, 18);
            this.rbFolloupFromDate.TabIndex = 0;
            this.rbFolloupFromDate.Text = "Start from Followup Date ";
            this.rbFolloupFromDate.UseVisualStyleBackColor = true;
            this.rbFolloupFromDate.CheckedChanged += new System.EventHandler(this.rbFolloupFromDate_CheckedChanged);
            // 
            // label146
            // 
            this.label146.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label146.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label146.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label146.Location = new System.Drawing.Point(1, 607);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(586, 1);
            this.label146.TabIndex = 4;
            this.label146.Text = "label2";
            // 
            // label147
            // 
            this.label147.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label147.Dock = System.Windows.Forms.DockStyle.Left;
            this.label147.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label147.Location = new System.Drawing.Point(0, 4);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(1, 604);
            this.label147.TabIndex = 3;
            this.label147.Text = "label4";
            // 
            // label148
            // 
            this.label148.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label148.Dock = System.Windows.Forms.DockStyle.Right;
            this.label148.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label148.Location = new System.Drawing.Point(587, 4);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(1, 604);
            this.label148.TabIndex = 2;
            this.label148.Text = "label3";
            // 
            // label149
            // 
            this.label149.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label149.Dock = System.Windows.Forms.DockStyle.Top;
            this.label149.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(0, 3);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(588, 1);
            this.label149.TabIndex = 0;
            this.label149.Text = "label1";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel14.Controls.Add(this.panel13);
            this.panel14.Controls.Add(this.label3);
            this.panel14.Controls.Add(this.num_NoofColOnCalndr);
            this.panel14.Controls.Add(this.lblCalCol);
            this.panel14.Controls.Add(this.chkShowTemplate);
            this.panel14.Controls.Add(this.label126);
            this.panel14.Controls.Add(this.num_NoofApptInaSlot);
            this.panel14.Controls.Add(this.label127);
            this.panel14.Controls.Add(this.label5);
            this.panel14.Controls.Add(this.label128);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(588, 90);
            this.panel14.TabIndex = 1;
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel13.BackgroundImage")));
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.label78);
            this.panel13.Controls.Add(this.label94);
            this.panel13.Controls.Add(this.label95);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(1, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(586, 24);
            this.panel13.TabIndex = 0;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Top;
            this.label78.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label78.Location = new System.Drawing.Point(0, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(586, 1);
            this.label78.TabIndex = 11;
            this.label78.Text = "label2";
            // 
            // label94
            // 
            this.label94.BackColor = System.Drawing.Color.Transparent;
            this.label94.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(0, 0);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(586, 23);
            this.label94.TabIndex = 10;
            this.label94.Text = "  Appointments";
            this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label95.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label95.Location = new System.Drawing.Point(0, 23);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(586, 1);
            this.label95.TabIndex = 8;
            this.label95.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(274, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "Calendar columns";
            // 
            // num_NoofColOnCalndr
            // 
            this.num_NoofColOnCalndr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_NoofColOnCalndr.ForeColor = System.Drawing.Color.Black;
            this.num_NoofColOnCalndr.Location = new System.Drawing.Point(215, 59);
            this.num_NoofColOnCalndr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NoofColOnCalndr.Name = "num_NoofColOnCalndr";
            this.num_NoofColOnCalndr.Size = new System.Drawing.Size(45, 22);
            this.num_NoofColOnCalndr.TabIndex = 9;
            this.num_NoofColOnCalndr.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_NoofColOnCalndr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.num_NoofColOnCalndr_KeyDown);
            this.num_NoofColOnCalndr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_NoofColOnCalndr_KeyPress);
            // 
            // lblCalCol
            // 
            this.lblCalCol.AutoSize = true;
            this.lblCalCol.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalCol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCalCol.Location = new System.Drawing.Point(105, 63);
            this.lblCalCol.Name = "lblCalCol";
            this.lblCalCol.Size = new System.Drawing.Size(104, 14);
            this.lblCalCol.TabIndex = 8;
            this.lblCalCol.Text = "My screen can fit ";
            // 
            // chkShowTemplate
            // 
            this.chkShowTemplate.AutoSize = true;
            this.chkShowTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowTemplate.Location = new System.Drawing.Point(274, 34);
            this.chkShowTemplate.Name = "chkShowTemplate";
            this.chkShowTemplate.Size = new System.Drawing.Size(113, 18);
            this.chkShowTemplate.TabIndex = 1;
            this.chkShowTemplate.Text = "Show Template";
            this.chkShowTemplate.UseVisualStyleBackColor = true;
            // 
            // label126
            // 
            this.label126.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label126.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label126.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label126.Location = new System.Drawing.Point(1, 89);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(586, 1);
            this.label126.TabIndex = 4;
            this.label126.Text = "label2";
            // 
            // num_NoofApptInaSlot
            // 
            this.num_NoofApptInaSlot.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_NoofApptInaSlot.ForeColor = System.Drawing.Color.Black;
            this.num_NoofApptInaSlot.Location = new System.Drawing.Point(215, 32);
            this.num_NoofApptInaSlot.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NoofApptInaSlot.Name = "num_NoofApptInaSlot";
            this.num_NoofApptInaSlot.Size = new System.Drawing.Size(45, 22);
            this.num_NoofApptInaSlot.TabIndex = 0;
            this.num_NoofApptInaSlot.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NoofApptInaSlot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.num_NoofApptInaSlot_KeyDown);
            this.num_NoofApptInaSlot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_NoofApptInaSlot_KeyPress);
            // 
            // label127
            // 
            this.label127.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label127.Dock = System.Windows.Forms.DockStyle.Left;
            this.label127.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label127.Location = new System.Drawing.Point(0, 0);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(1, 90);
            this.label127.TabIndex = 3;
            this.label127.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(20, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "No. of Appointments in an hour :";
            // 
            // label128
            // 
            this.label128.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label128.Dock = System.Windows.Forms.DockStyle.Right;
            this.label128.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label128.Location = new System.Drawing.Point(587, 0);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(1, 90);
            this.label128.TabIndex = 2;
            this.label128.Text = "label3";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtons.BackgroundImage = global::gloSettings.Properties.Resources.ImgSearchBackground;
            this.pnlButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Location = new System.Drawing.Point(282, 2);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(2);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(12, 10);
            this.pnlButtons.TabIndex = 24;
            this.pnlButtons.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(456, 4);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(63, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(528, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls);
            this.pnl_tlspTOP.Controls.Add(this.pnlButtons);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(602, 53);
            this.pnl_tlspTOP.TabIndex = 1;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnOk,
            this.ts_btnCancel});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(602, 53);
            this.tls.TabIndex = 0;
            this.tls.TabStop = true;
            this.tls.Text = "toolStrip1";
            // 
            // ts_btnOk
            // 
            this.ts_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOk.Image")));
            this.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOk.Name = "ts_btnOk";
            this.ts_btnOk.Size = new System.Drawing.Size(66, 50);
            this.ts_btnOk.Tag = "OK";
            this.ts_btnOk.Text = "&Save&&Cls";
            this.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnOk.ToolTipText = "Save and Close";
            this.ts_btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Cancel";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(439, 480);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "General Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(16, 363);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 114);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patient Demographics";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Location = new System.Drawing.Point(118, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 86);
            this.panel2.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ForeColor = System.Drawing.Color.Black;
            this.treeView1.LineColor = System.Drawing.Color.Empty;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(200, 86);
            this.treeView1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox3.Location = new System.Drawing.Point(16, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(397, 50);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Appointments";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(278, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(113, 18);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Show Template";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown1.Location = new System.Drawing.Point(219, 16);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 22);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(9, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(204, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "No. of Appointments in Single Slot :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.treeView2);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox4.Location = new System.Drawing.Point(16, 186);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(397, 171);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Patient Data";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(9, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "Patient Code Increment :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(9, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Patient Code Prefix :";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown2.Location = new System.Drawing.Point(157, 127);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(131, 96);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(89, 22);
            this.textBox1.TabIndex = 1;
            // 
            // treeView2
            // 
            this.treeView2.CheckBoxes = true;
            this.treeView2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView2.ForeColor = System.Drawing.Color.Black;
            this.treeView2.LineColor = System.Drawing.Color.Empty;
            this.treeView2.Location = new System.Drawing.Point(118, 17);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(198, 71);
            this.treeView2.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(9, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "Patient Columns :";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox5.Location = new System.Drawing.Point(16, 70);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(397, 50);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Patient Search";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Code",
            "First Name",
            "Last Name"});
            this.comboBox1.Location = new System.Drawing.Point(149, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(219, 22);
            this.comboBox1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(9, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(139, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "Default Search Column :";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.numericUpDown3);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox6.Location = new System.Drawing.Point(16, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(397, 50);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " Lock Screen Settings";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(214, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 14);
            this.label15.TabIndex = 2;
            this.label15.Text = "(In Minutes)";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown3.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown3.Location = new System.Drawing.Point(149, 18);
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown3.TabIndex = 0;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(14, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 14);
            this.label16.TabIndex = 0;
            this.label16.Text = "Application Lock Time :";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 480);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Exchange Server Settings";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox2);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.textBox3);
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox7.Location = new System.Drawing.Point(16, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(349, 99);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Exchange Server Settings";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(129, 30);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(188, 22);
            this.textBox2.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(32, 62);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 14);
            this.label17.TabIndex = 72;
            this.label17.Text = "Exchange URL :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(13, 34);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(112, 14);
            this.label18.TabIndex = 74;
            this.label18.Text = "Exchange Domain :";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(129, 58);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(188, 22);
            this.textBox3.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 480);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "gloEMR Database Settings";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.textBox4);
            this.groupBox8.Controls.Add(this.comboBox2);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.textBox5);
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Controls.Add(this.label22);
            this.groupBox8.Controls.Add(this.textBox6);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.textBox7);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox8.Location = new System.Drawing.Point(16, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(349, 182);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "gloEMR Database Settings";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Location = new System.Drawing.Point(10, 25);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 14);
            this.label19.TabIndex = 8;
            this.label19.Text = "SQL Server Name :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.Black;
            this.textBox4.Location = new System.Drawing.Point(125, 22);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(187, 22);
            this.textBox4.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.Color.Black;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(125, 84);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(187, 22);
            this.comboBox2.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(21, 56);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 14);
            this.label20.TabIndex = 9;
            this.label20.Text = "Database Name :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.Black;
            this.textBox5.Location = new System.Drawing.Point(125, 53);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(187, 22);
            this.textBox5.TabIndex = 4;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(25, 87);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 14);
            this.label21.TabIndex = 0;
            this.label21.Text = "Authentication :";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(47, 118);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(74, 14);
            this.label22.TabIndex = 1;
            this.label22.Text = "User Name :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.Black;
            this.textBox6.Location = new System.Drawing.Point(125, 115);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(187, 22);
            this.textBox6.TabIndex = 6;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Location = new System.Drawing.Point(55, 149);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 14);
            this.label23.TabIndex = 2;
            this.label23.Text = "Password :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.Black;
            this.textBox7.Location = new System.Drawing.Point(125, 146);
            this.textBox7.Margin = new System.Windows.Forms.Padding(2);
            this.textBox7.Name = "textBox7";
            this.textBox7.PasswordChar = '*';
            this.textBox7.Size = new System.Drawing.Size(187, 22);
            this.textBox7.TabIndex = 7;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage4.Controls.Add(this.c1FlexGrid1);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(439, 480);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Provider Settings";
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1FlexGrid1.AutoGenerateColumns = false;
            this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGrid1.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 19;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1FlexGrid1.Size = new System.Drawing.Size(439, 480);
            this.c1FlexGrid1.StyleInfo = resources.GetString("c1FlexGrid1.StyleInfo");
            this.c1FlexGrid1.TabIndex = 12;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage5.Controls.Add(this.label24);
            this.tabPage5.Controls.Add(this.numericUpDown4);
            this.tabPage5.Controls.Add(this.label25);
            this.tabPage5.Controls.Add(this.numericUpDown5);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(439, 480);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Tag = "BillingSettings";
            this.tabPage5.Text = "Billing Settings";
            this.tabPage5.ToolTipText = "Billing Settings";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(18, 82);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(86, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "No Of Modifiers :";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(122, 80);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.ReadOnly = true;
            this.numericUpDown4.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown4.TabIndex = 21;
            this.numericUpDown4.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(90, 13);
            this.label25.TabIndex = 20;
            this.label25.Text = "No Of Diagnosis :";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(124, 33);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.ReadOnly = true;
            this.numericUpDown5.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown5.TabIndex = 19;
            this.numericUpDown5.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage6.Controls.Add(this.groupBox9);
            this.tabPage6.Controls.Add(this.groupBox10);
            this.tabPage6.Controls.Add(this.groupBox11);
            this.tabPage6.Controls.Add(this.groupBox12);
            this.tabPage6.Controls.Add(this.groupBox13);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(439, 480);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "General Settings";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.panel3);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox9.Location = new System.Drawing.Point(16, 363);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(397, 114);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Patient Demographics";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeView3);
            this.panel3.Location = new System.Drawing.Point(118, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 86);
            this.panel3.TabIndex = 2;
            // 
            // treeView3
            // 
            this.treeView3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView3.CheckBoxes = true;
            this.treeView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView3.ForeColor = System.Drawing.Color.Black;
            this.treeView3.LineColor = System.Drawing.Color.Empty;
            this.treeView3.Location = new System.Drawing.Point(0, 0);
            this.treeView3.Name = "treeView3";
            this.treeView3.ShowNodeToolTips = true;
            this.treeView3.Size = new System.Drawing.Size(200, 86);
            this.treeView3.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.checkBox2);
            this.groupBox10.Controls.Add(this.numericUpDown6);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox10.Location = new System.Drawing.Point(16, 128);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(397, 50);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Appointments";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(278, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(113, 18);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Show Template";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown6.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown6.Location = new System.Drawing.Point(219, 16);
            this.numericUpDown6.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(45, 22);
            this.numericUpDown6.TabIndex = 0;
            this.numericUpDown6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(9, 20);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(204, 14);
            this.label26.TabIndex = 0;
            this.label26.Text = "No. of Appointments in Single Slot :";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label27);
            this.groupBox11.Controls.Add(this.label28);
            this.groupBox11.Controls.Add(this.numericUpDown7);
            this.groupBox11.Controls.Add(this.textBox8);
            this.groupBox11.Controls.Add(this.treeView4);
            this.groupBox11.Controls.Add(this.label29);
            this.groupBox11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox11.Location = new System.Drawing.Point(16, 186);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(397, 171);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Patient Data";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(9, 131);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(147, 14);
            this.label27.TabIndex = 4;
            this.label27.Text = "Patient Code Increment :";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(9, 100);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(120, 14);
            this.label28.TabIndex = 3;
            this.label28.Text = "Patient Code Prefix :";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown7.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown7.Location = new System.Drawing.Point(157, 127);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown7.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown7.TabIndex = 2;
            this.numericUpDown7.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.White;
            this.textBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.Black;
            this.textBox8.Location = new System.Drawing.Point(131, 96);
            this.textBox8.Margin = new System.Windows.Forms.Padding(2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(89, 22);
            this.textBox8.TabIndex = 1;
            // 
            // treeView4
            // 
            this.treeView4.CheckBoxes = true;
            this.treeView4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView4.ForeColor = System.Drawing.Color.Black;
            this.treeView4.LineColor = System.Drawing.Color.Empty;
            this.treeView4.Location = new System.Drawing.Point(118, 17);
            this.treeView4.Name = "treeView4";
            this.treeView4.Size = new System.Drawing.Size(198, 71);
            this.treeView4.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(9, 17);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(103, 14);
            this.label29.TabIndex = 0;
            this.label29.Text = "Patient Columns :";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.comboBox3);
            this.groupBox12.Controls.Add(this.label30);
            this.groupBox12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox12.Location = new System.Drawing.Point(16, 70);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(397, 50);
            this.groupBox12.TabIndex = 3;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Patient Search";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.ForeColor = System.Drawing.Color.Black;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Code",
            "First Name",
            "Last Name"});
            this.comboBox3.Location = new System.Drawing.Point(149, 18);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(219, 22);
            this.comboBox3.TabIndex = 0;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(9, 22);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(139, 14);
            this.label30.TabIndex = 0;
            this.label30.Text = "Default Search Column :";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label36);
            this.groupBox13.Controls.Add(this.numericUpDown8);
            this.groupBox13.Controls.Add(this.label37);
            this.groupBox13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox13.Location = new System.Drawing.Point(16, 12);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(397, 50);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = " Lock Screen Settings";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Location = new System.Drawing.Point(214, 22);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(74, 14);
            this.label36.TabIndex = 2;
            this.label36.Text = "(In Minutes)";
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown8.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown8.Location = new System.Drawing.Point(149, 18);
            this.numericUpDown8.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown8.TabIndex = 0;
            this.numericUpDown8.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(14, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(134, 14);
            this.label37.TabIndex = 0;
            this.label37.Text = "Application Lock Time :";
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage7.Controls.Add(this.groupBox14);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(439, 480);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Exchange Server Settings";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.textBox9);
            this.groupBox14.Controls.Add(this.label38);
            this.groupBox14.Controls.Add(this.label39);
            this.groupBox14.Controls.Add(this.textBox10);
            this.groupBox14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox14.Location = new System.Drawing.Point(16, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(349, 99);
            this.groupBox14.TabIndex = 2;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Exchange Server Settings";
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.White;
            this.textBox9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.ForeColor = System.Drawing.Color.Black;
            this.textBox9.Location = new System.Drawing.Point(129, 30);
            this.textBox9.Margin = new System.Windows.Forms.Padding(2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(188, 22);
            this.textBox9.TabIndex = 0;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(32, 62);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(93, 14);
            this.label38.TabIndex = 72;
            this.label38.Text = "Exchange URL :";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(13, 34);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(112, 14);
            this.label39.TabIndex = 74;
            this.label39.Text = "Exchange Domain :";
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.White;
            this.textBox10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.ForeColor = System.Drawing.Color.Black;
            this.textBox10.Location = new System.Drawing.Point(129, 58);
            this.textBox10.Margin = new System.Windows.Forms.Padding(2);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(188, 22);
            this.textBox10.TabIndex = 1;
            // 
            // tabPage8
            // 
            this.tabPage8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage8.Controls.Add(this.groupBox15);
            this.tabPage8.Location = new System.Drawing.Point(4, 23);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(439, 480);
            this.tabPage8.TabIndex = 3;
            this.tabPage8.Text = "gloEMR Database Settings";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label40);
            this.groupBox15.Controls.Add(this.textBox11);
            this.groupBox15.Controls.Add(this.comboBox4);
            this.groupBox15.Controls.Add(this.label41);
            this.groupBox15.Controls.Add(this.textBox12);
            this.groupBox15.Controls.Add(this.label42);
            this.groupBox15.Controls.Add(this.label43);
            this.groupBox15.Controls.Add(this.textBox13);
            this.groupBox15.Controls.Add(this.label44);
            this.groupBox15.Controls.Add(this.textBox14);
            this.groupBox15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox15.Location = new System.Drawing.Point(16, 12);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(349, 182);
            this.groupBox15.TabIndex = 0;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "gloEMR Database Settings";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(10, 25);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(111, 14);
            this.label40.TabIndex = 8;
            this.label40.Text = "SQL Server Name :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox11
            // 
            this.textBox11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.ForeColor = System.Drawing.Color.Black;
            this.textBox11.Location = new System.Drawing.Point(125, 22);
            this.textBox11.Margin = new System.Windows.Forms.Padding(2);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(187, 22);
            this.textBox11.TabIndex = 3;
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox4.ForeColor = System.Drawing.Color.Black;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(125, 84);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(187, 22);
            this.comboBox4.TabIndex = 5;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Location = new System.Drawing.Point(21, 56);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(100, 14);
            this.label41.TabIndex = 9;
            this.label41.Text = "Database Name :";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox12
            // 
            this.textBox12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.ForeColor = System.Drawing.Color.Black;
            this.textBox12.Location = new System.Drawing.Point(125, 53);
            this.textBox12.Margin = new System.Windows.Forms.Padding(2);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(187, 22);
            this.textBox12.TabIndex = 4;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(25, 87);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(96, 14);
            this.label42.TabIndex = 0;
            this.label42.Text = "Authentication :";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(47, 118);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(74, 14);
            this.label43.TabIndex = 1;
            this.label43.Text = "User Name :";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox13
            // 
            this.textBox13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.ForeColor = System.Drawing.Color.Black;
            this.textBox13.Location = new System.Drawing.Point(125, 115);
            this.textBox13.Margin = new System.Windows.Forms.Padding(2);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(187, 22);
            this.textBox13.TabIndex = 6;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(55, 149);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(66, 14);
            this.label44.TabIndex = 2;
            this.label44.Text = "Password :";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox14
            // 
            this.textBox14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.Black;
            this.textBox14.Location = new System.Drawing.Point(125, 146);
            this.textBox14.Margin = new System.Windows.Forms.Padding(2);
            this.textBox14.Name = "textBox14";
            this.textBox14.PasswordChar = '*';
            this.textBox14.Size = new System.Drawing.Size(187, 22);
            this.textBox14.TabIndex = 7;
            // 
            // tabPage9
            // 
            this.tabPage9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage9.Controls.Add(this.c1FlexGrid2);
            this.tabPage9.Location = new System.Drawing.Point(4, 23);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(439, 480);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "Provider Settings";
            // 
            // c1FlexGrid2
            // 
            this.c1FlexGrid2.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid2.AllowEditing = false;
            this.c1FlexGrid2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1FlexGrid2.AutoGenerateColumns = false;
            this.c1FlexGrid2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGrid2.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid2.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1FlexGrid2.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid2.Name = "c1FlexGrid2";
            this.c1FlexGrid2.Rows.Count = 1;
            this.c1FlexGrid2.Rows.DefaultSize = 19;
            this.c1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1FlexGrid2.Size = new System.Drawing.Size(439, 480);
            this.c1FlexGrid2.StyleInfo = resources.GetString("c1FlexGrid2.StyleInfo");
            this.c1FlexGrid2.TabIndex = 12;
            // 
            // tabPage10
            // 
            this.tabPage10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage10.Controls.Add(this.label45);
            this.tabPage10.Controls.Add(this.numericUpDown9);
            this.tabPage10.Controls.Add(this.label46);
            this.tabPage10.Controls.Add(this.numericUpDown10);
            this.tabPage10.Location = new System.Drawing.Point(4, 23);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(439, 480);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Tag = "BillingSettings";
            this.tabPage10.Text = "Billing Settings";
            this.tabPage10.ToolTipText = "Billing Settings";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(18, 82);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(86, 13);
            this.label45.TabIndex = 22;
            this.label45.Text = "No Of Modifiers :";
            // 
            // numericUpDown9
            // 
            this.numericUpDown9.Location = new System.Drawing.Point(122, 80);
            this.numericUpDown9.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown9.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.ReadOnly = true;
            this.numericUpDown9.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown9.TabIndex = 21;
            this.numericUpDown9.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(18, 35);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(90, 13);
            this.label46.TabIndex = 20;
            this.label46.Text = "No Of Diagnosis :";
            // 
            // numericUpDown10
            // 
            this.numericUpDown10.Location = new System.Drawing.Point(124, 33);
            this.numericUpDown10.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown10.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown10.Name = "numericUpDown10";
            this.numericUpDown10.ReadOnly = true;
            this.numericUpDown10.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown10.TabIndex = 19;
            this.numericUpDown10.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // tabPage11
            // 
            this.tabPage11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage11.Controls.Add(this.groupBox16);
            this.tabPage11.Controls.Add(this.groupBox17);
            this.tabPage11.Controls.Add(this.groupBox18);
            this.tabPage11.Controls.Add(this.groupBox19);
            this.tabPage11.Controls.Add(this.groupBox20);
            this.tabPage11.Location = new System.Drawing.Point(4, 23);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage11.Size = new System.Drawing.Size(439, 480);
            this.tabPage11.TabIndex = 1;
            this.tabPage11.Text = "General Settings";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.panel4);
            this.groupBox16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox16.Location = new System.Drawing.Point(16, 363);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(397, 114);
            this.groupBox16.TabIndex = 6;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Patient Demographics";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.treeView5);
            this.panel4.Location = new System.Drawing.Point(118, 19);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 86);
            this.panel4.TabIndex = 2;
            // 
            // treeView5
            // 
            this.treeView5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView5.CheckBoxes = true;
            this.treeView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView5.ForeColor = System.Drawing.Color.Black;
            this.treeView5.LineColor = System.Drawing.Color.Empty;
            this.treeView5.Location = new System.Drawing.Point(0, 0);
            this.treeView5.Name = "treeView5";
            this.treeView5.ShowNodeToolTips = true;
            this.treeView5.Size = new System.Drawing.Size(200, 86);
            this.treeView5.TabIndex = 0;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.checkBox3);
            this.groupBox17.Controls.Add(this.numericUpDown11);
            this.groupBox17.Controls.Add(this.label47);
            this.groupBox17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox17.Location = new System.Drawing.Point(16, 128);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(397, 50);
            this.groupBox17.TabIndex = 5;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Appointments";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.Location = new System.Drawing.Point(278, 19);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(113, 18);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Show Template";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // numericUpDown11
            // 
            this.numericUpDown11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown11.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown11.Location = new System.Drawing.Point(219, 16);
            this.numericUpDown11.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown11.Name = "numericUpDown11";
            this.numericUpDown11.Size = new System.Drawing.Size(45, 22);
            this.numericUpDown11.TabIndex = 0;
            this.numericUpDown11.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(9, 20);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(204, 14);
            this.label47.TabIndex = 0;
            this.label47.Text = "No. of Appointments in Single Slot :";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label48);
            this.groupBox18.Controls.Add(this.label49);
            this.groupBox18.Controls.Add(this.numericUpDown12);
            this.groupBox18.Controls.Add(this.textBox15);
            this.groupBox18.Controls.Add(this.treeView6);
            this.groupBox18.Controls.Add(this.label50);
            this.groupBox18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox18.Location = new System.Drawing.Point(16, 186);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(397, 171);
            this.groupBox18.TabIndex = 4;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Patient Data";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(9, 131);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(147, 14);
            this.label48.TabIndex = 4;
            this.label48.Text = "Patient Code Increment :";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(9, 100);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(120, 14);
            this.label49.TabIndex = 3;
            this.label49.Text = "Patient Code Prefix :";
            // 
            // numericUpDown12
            // 
            this.numericUpDown12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown12.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown12.Location = new System.Drawing.Point(157, 127);
            this.numericUpDown12.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown12.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown12.Name = "numericUpDown12";
            this.numericUpDown12.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown12.TabIndex = 2;
            this.numericUpDown12.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.Color.White;
            this.textBox15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.ForeColor = System.Drawing.Color.Black;
            this.textBox15.Location = new System.Drawing.Point(131, 96);
            this.textBox15.Margin = new System.Windows.Forms.Padding(2);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(89, 22);
            this.textBox15.TabIndex = 1;
            // 
            // treeView6
            // 
            this.treeView6.CheckBoxes = true;
            this.treeView6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView6.ForeColor = System.Drawing.Color.Black;
            this.treeView6.LineColor = System.Drawing.Color.Empty;
            this.treeView6.Location = new System.Drawing.Point(118, 17);
            this.treeView6.Name = "treeView6";
            this.treeView6.Size = new System.Drawing.Size(198, 71);
            this.treeView6.TabIndex = 0;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(9, 17);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(103, 14);
            this.label50.TabIndex = 0;
            this.label50.Text = "Patient Columns :";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.comboBox5);
            this.groupBox19.Controls.Add(this.label51);
            this.groupBox19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox19.Location = new System.Drawing.Point(16, 70);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(397, 50);
            this.groupBox19.TabIndex = 3;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Patient Search";
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox5.ForeColor = System.Drawing.Color.Black;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "Code",
            "First Name",
            "Last Name"});
            this.comboBox5.Location = new System.Drawing.Point(149, 18);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(219, 22);
            this.comboBox5.TabIndex = 0;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(9, 22);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(139, 14);
            this.label51.TabIndex = 0;
            this.label51.Text = "Default Search Column :";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label52);
            this.groupBox20.Controls.Add(this.numericUpDown13);
            this.groupBox20.Controls.Add(this.label53);
            this.groupBox20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox20.Location = new System.Drawing.Point(16, 12);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(397, 50);
            this.groupBox20.TabIndex = 1;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = " Lock Screen Settings";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(214, 22);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(74, 14);
            this.label52.TabIndex = 2;
            this.label52.Text = "(In Minutes)";
            // 
            // numericUpDown13
            // 
            this.numericUpDown13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown13.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown13.Location = new System.Drawing.Point(149, 18);
            this.numericUpDown13.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown13.Name = "numericUpDown13";
            this.numericUpDown13.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown13.TabIndex = 0;
            this.numericUpDown13.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(14, 22);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(134, 14);
            this.label53.TabIndex = 0;
            this.label53.Text = "Application Lock Time :";
            // 
            // tabPage12
            // 
            this.tabPage12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage12.Controls.Add(this.groupBox21);
            this.tabPage12.Location = new System.Drawing.Point(4, 23);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(439, 480);
            this.tabPage12.TabIndex = 2;
            this.tabPage12.Text = "Exchange Server Settings";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.textBox16);
            this.groupBox21.Controls.Add(this.label54);
            this.groupBox21.Controls.Add(this.label55);
            this.groupBox21.Controls.Add(this.textBox17);
            this.groupBox21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox21.Location = new System.Drawing.Point(16, 12);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(349, 99);
            this.groupBox21.TabIndex = 2;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Exchange Server Settings";
            // 
            // textBox16
            // 
            this.textBox16.BackColor = System.Drawing.Color.White;
            this.textBox16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.ForeColor = System.Drawing.Color.Black;
            this.textBox16.Location = new System.Drawing.Point(129, 30);
            this.textBox16.Margin = new System.Windows.Forms.Padding(2);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(188, 22);
            this.textBox16.TabIndex = 0;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(32, 62);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(93, 14);
            this.label54.TabIndex = 72;
            this.label54.Text = "Exchange URL :";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Location = new System.Drawing.Point(13, 34);
            this.label55.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(112, 14);
            this.label55.TabIndex = 74;
            this.label55.Text = "Exchange Domain :";
            // 
            // textBox17
            // 
            this.textBox17.BackColor = System.Drawing.Color.White;
            this.textBox17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox17.ForeColor = System.Drawing.Color.Black;
            this.textBox17.Location = new System.Drawing.Point(129, 58);
            this.textBox17.Margin = new System.Windows.Forms.Padding(2);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(188, 22);
            this.textBox17.TabIndex = 1;
            // 
            // tabPage13
            // 
            this.tabPage13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage13.Controls.Add(this.groupBox22);
            this.tabPage13.Location = new System.Drawing.Point(4, 23);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(439, 480);
            this.tabPage13.TabIndex = 3;
            this.tabPage13.Text = "gloEMR Database Settings";
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.label56);
            this.groupBox22.Controls.Add(this.textBox18);
            this.groupBox22.Controls.Add(this.comboBox6);
            this.groupBox22.Controls.Add(this.label57);
            this.groupBox22.Controls.Add(this.textBox19);
            this.groupBox22.Controls.Add(this.label58);
            this.groupBox22.Controls.Add(this.label59);
            this.groupBox22.Controls.Add(this.textBox20);
            this.groupBox22.Controls.Add(this.label60);
            this.groupBox22.Controls.Add(this.textBox21);
            this.groupBox22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox22.Location = new System.Drawing.Point(16, 12);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(349, 182);
            this.groupBox22.TabIndex = 0;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "gloEMR Database Settings";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(10, 25);
            this.label56.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(111, 14);
            this.label56.TabIndex = 8;
            this.label56.Text = "SQL Server Name :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox18
            // 
            this.textBox18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox18.ForeColor = System.Drawing.Color.Black;
            this.textBox18.Location = new System.Drawing.Point(125, 22);
            this.textBox18.Margin = new System.Windows.Forms.Padding(2);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(187, 22);
            this.textBox18.TabIndex = 3;
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox6.ForeColor = System.Drawing.Color.Black;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(125, 84);
            this.comboBox6.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(187, 22);
            this.comboBox6.TabIndex = 5;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(21, 56);
            this.label57.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(100, 14);
            this.label57.TabIndex = 9;
            this.label57.Text = "Database Name :";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox19
            // 
            this.textBox19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox19.ForeColor = System.Drawing.Color.Black;
            this.textBox19.Location = new System.Drawing.Point(125, 53);
            this.textBox19.Margin = new System.Windows.Forms.Padding(2);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(187, 22);
            this.textBox19.TabIndex = 4;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(25, 87);
            this.label58.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(96, 14);
            this.label58.TabIndex = 0;
            this.label58.Text = "Authentication :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(47, 118);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(74, 14);
            this.label59.TabIndex = 1;
            this.label59.Text = "User Name :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox20
            // 
            this.textBox20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox20.ForeColor = System.Drawing.Color.Black;
            this.textBox20.Location = new System.Drawing.Point(125, 115);
            this.textBox20.Margin = new System.Windows.Forms.Padding(2);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(187, 22);
            this.textBox20.TabIndex = 6;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Location = new System.Drawing.Point(55, 149);
            this.label60.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(66, 14);
            this.label60.TabIndex = 2;
            this.label60.Text = "Password :";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox21
            // 
            this.textBox21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox21.ForeColor = System.Drawing.Color.Black;
            this.textBox21.Location = new System.Drawing.Point(125, 146);
            this.textBox21.Margin = new System.Windows.Forms.Padding(2);
            this.textBox21.Name = "textBox21";
            this.textBox21.PasswordChar = '*';
            this.textBox21.Size = new System.Drawing.Size(187, 22);
            this.textBox21.TabIndex = 7;
            // 
            // tabPage14
            // 
            this.tabPage14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage14.Controls.Add(this.c1FlexGrid3);
            this.tabPage14.Location = new System.Drawing.Point(4, 23);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Size = new System.Drawing.Size(439, 480);
            this.tabPage14.TabIndex = 4;
            this.tabPage14.Text = "Provider Settings";
            // 
            // c1FlexGrid3
            // 
            this.c1FlexGrid3.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGrid3.AllowEditing = false;
            this.c1FlexGrid3.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1FlexGrid3.AutoGenerateColumns = false;
            this.c1FlexGrid3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FlexGrid3.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGrid3.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1FlexGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid3.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1FlexGrid3.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid3.Name = "c1FlexGrid3";
            this.c1FlexGrid3.Rows.Count = 1;
            this.c1FlexGrid3.Rows.DefaultSize = 19;
            this.c1FlexGrid3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1FlexGrid3.Size = new System.Drawing.Size(439, 480);
            this.c1FlexGrid3.StyleInfo = resources.GetString("c1FlexGrid3.StyleInfo");
            this.c1FlexGrid3.TabIndex = 12;
            // 
            // tabPage15
            // 
            this.tabPage15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage15.Controls.Add(this.label61);
            this.tabPage15.Controls.Add(this.numericUpDown14);
            this.tabPage15.Controls.Add(this.label62);
            this.tabPage15.Controls.Add(this.numericUpDown15);
            this.tabPage15.Location = new System.Drawing.Point(4, 23);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(439, 480);
            this.tabPage15.TabIndex = 5;
            this.tabPage15.Tag = "BillingSettings";
            this.tabPage15.Text = "Billing Settings";
            this.tabPage15.ToolTipText = "Billing Settings";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(18, 82);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(86, 13);
            this.label61.TabIndex = 22;
            this.label61.Text = "No Of Modifiers :";
            // 
            // numericUpDown14
            // 
            this.numericUpDown14.Location = new System.Drawing.Point(122, 80);
            this.numericUpDown14.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown14.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown14.Name = "numericUpDown14";
            this.numericUpDown14.ReadOnly = true;
            this.numericUpDown14.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown14.TabIndex = 21;
            this.numericUpDown14.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(18, 35);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(90, 13);
            this.label62.TabIndex = 20;
            this.label62.Text = "No Of Diagnosis :";
            // 
            // numericUpDown15
            // 
            this.numericUpDown15.Location = new System.Drawing.Point(124, 33);
            this.numericUpDown15.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown15.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown15.Name = "numericUpDown15";
            this.numericUpDown15.ReadOnly = true;
            this.numericUpDown15.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown15.TabIndex = 19;
            this.numericUpDown15.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(602, 787);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.pnlMain.ResumeLayout(false);
            this.tb_Settings.ResumeLayout(false);
            this.tbpg_GeneralSettings.ResumeLayout(false);
            this.panel33.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.panel31.ResumeLayout(false);
            this.pnlPatientSearch.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.pnl_Base.ResumeLayout(false);
            this.panel32.ResumeLayout(false);
            this.panel36.ResumeLayout(false);
            this.panel36.PerformLayout();
            this.panel35.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPatientCodeIncrement)).EndInit();
            this.gbRemotePrintSetting.ResumeLayout(false);
            this.gbRemotePrintSetting.PerformLayout();
            this.pnlPrintImages.ResumeLayout(false);
            this.pnlPrintImages.PerformLayout();
            this.pnlPrintClaims.ResumeLayout(false);
            this.pnlPrintClaims.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel38.ResumeLayout(false);
            this.panel38.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.grpOutboundSettings.ResumeLayout(false);
            this.grpOutboundSettings.PerformLayout();
            this.panel37.ResumeLayout(false);
            this.panel37.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_LockScreen)).EndInit();
            this.tbpg_ExchangeSettings.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.tbpg_EMRDBSettings.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.tbpg_ProviderSettings.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Providers)).EndInit();
            this.tbpg_BillingSettings.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numModifiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiagnosis)).EndInit();
            this.tbpg_AppointmentSettings.ResumeLayout(false);
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_NoofColOnCalndr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NoofApptInaSlot)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid2)).EndInit();
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
            this.tabPage11.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).EndInit();
            this.tabPage12.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid3)).EndInit();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tb_Settings;
        private System.Windows.Forms.TabPage tbpg_GeneralSettings;
        private System.Windows.Forms.NumericUpDown num_LockScreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSearchColumns;
        private System.Windows.Forms.TreeView trvPatientColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown num_NoofApptInaSlot;
        private System.Windows.Forms.TabPage tbpg_ExchangeSettings;
        internal System.Windows.Forms.TextBox txtExchangeDomain;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txtExchangeURL;
        private System.Windows.Forms.TabPage tbpg_EMRDBSettings;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txt_EMRDB_ServerName;
        private System.Windows.Forms.ComboBox cmbAuthentication;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_EMRDB_Database;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_EMRDB_UserName;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txt_EMRDB_Password;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numPatientCodeIncrement;
        internal System.Windows.Forms.TextBox txtPatientCodePrefix;
        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.CheckBox chkShowTemplate;
        private System.Windows.Forms.TabPage tbpg_ProviderSettings;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Providers;
        private System.Windows.Forms.TabPage tbpg_BillingSettings;
        private System.Windows.Forms.Label lblModifiers;
        private System.Windows.Forms.NumericUpDown numModifiers;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.NumericUpDown numDiagnosis;
        private System.Windows.Forms.TreeView trvDemographics;
        private System.Windows.Forms.TabPage tbpg_AppointmentSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox7;
        internal System.Windows.Forms.TextBox textBox2;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TabPage tabPage4;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        internal System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TreeView treeView4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.GroupBox groupBox14;
        internal System.Windows.Forms.TextBox textBox9;
        internal System.Windows.Forms.Label label38;
        internal System.Windows.Forms.Label label39;
        internal System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TabPage tabPage9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid2;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.NumericUpDown numericUpDown10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TreeView treeView5;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown11;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.NumericUpDown numericUpDown12;
        internal System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TreeView treeView6;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.NumericUpDown numericUpDown13;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.GroupBox groupBox21;
        internal System.Windows.Forms.TextBox textBox16;
        internal System.Windows.Forms.Label label54;
        internal System.Windows.Forms.Label label55;
        internal System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TabPage tabPage14;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid3;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.NumericUpDown numericUpDown14;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.NumericUpDown numericUpDown15;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.ComboBox cmbDefaultProvider;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.RadioButton rbFollowupFromToday;
        private System.Windows.Forms.RadioButton rbFolloupFromDate;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Label label149;
        internal System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.Panel panel31;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.Panel panel36;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Panel panel35;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.CheckBox chkShowBlinkAlert;
        private System.Windows.Forms.Button btnBrowseAlertColor;
        private System.Windows.Forms.Label lblAlertColor;
        private System.Windows.Forms.TextBox txtAlertColor;
        private System.Windows.Forms.ColorDialog clDlg;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.CheckBox chkAutoApplicationLock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown num_NoofColOnCalndr;
        private System.Windows.Forms.Label lblCalCol;
        private System.Windows.Forms.Panel panel38;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.CheckBox chk_EnableErrorLogs;
        private System.Windows.Forms.CheckBox chk_EnableApplicationLogs;
        private System.Windows.Forms.Panel panel37;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Browse;
        internal System.Windows.Forms.TextBox txt_Path;
        private System.Windows.Forms.CheckBox chbox_ExptoDefaultlocation;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.CheckBox chkSendPatientDetails;
        private System.Windows.Forms.CheckBox chkHL7;
        private System.Windows.Forms.CheckBox chkSendAppointmentDetails;
        private System.Windows.Forms.CheckBox chkGenerateOutboundMsg;
        private System.Windows.Forms.Panel grpOutboundSettings;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label136;
        internal System.Windows.Forms.Panel pnlPatientSearch;
        private System.Windows.Forms.TreeView trvPatientSearch;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.Label label166;
        internal System.Windows.Forms.Label Label205;
        internal System.Windows.Forms.CheckBox chkAddFooterService;
        internal System.Windows.Forms.CheckBox chkEnableLocalPrinter;
        private System.Windows.Forms.Panel gbRemotePrintSetting;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cmbNoPagesSplit;
        private System.Windows.Forms.Label label63;
        internal System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.RadioButton rbPrintSSRSReportEMF;
        internal System.Windows.Forms.Label Label208;
        internal System.Windows.Forms.RadioButton rbPrintSSRSReportPDF;
        internal System.Windows.Forms.Panel panel9;
        internal System.Windows.Forms.RadioButton rbPrintWordDocEMF;
        internal System.Windows.Forms.Label Label207;
        internal System.Windows.Forms.RadioButton rbPrintWordDocPDF;
        internal System.Windows.Forms.Panel pnlPrintImages;
        internal System.Windows.Forms.RadioButton rbPrintImagesEMF;
        internal System.Windows.Forms.Label Label209;
        internal System.Windows.Forms.RadioButton rbPrintImagesPNG;
        internal System.Windows.Forms.Panel pnlPrintClaims;
        internal System.Windows.Forms.RadioButton rbPrintClaimsEMF;
        internal System.Windows.Forms.Label Label210;
        internal System.Windows.Forms.RadioButton rbPrintClaimsPDF;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Button btnClearPatientContext;
        internal System.Windows.Forms.CheckBox chkZipMetadata;
        internal System.Windows.Forms.CheckBox chkUseDefaultPrinter;
        private System.Windows.Forms.ComboBox cmbNoTemplatesJob;
        private System.Windows.Forms.Label label85;
    }
}