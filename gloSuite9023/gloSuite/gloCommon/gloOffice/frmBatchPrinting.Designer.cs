namespace gloOffice
{
    partial class frmBatchPrinting
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpChkInDate, dtp_ToDate, dateTimePicker3, dateTimePicker4, dateTimePicker5, dateTimePicker6, dtpApptLetterToDate, dtpApptLetterFromDate, dtpChkInApptStartTimeTo, dtpChkInApptStartTimeFrom, dtpApptLetterApptStartTimeTo, dtpApptLetterApptStartTimeFrom };
            System.Windows.Forms.Control[] cntControls = { dtpChkInDate, dtp_ToDate, dateTimePicker3, dateTimePicker4, dateTimePicker5, dateTimePicker6, dtpApptLetterToDate, dtpApptLetterFromDate, dtpChkInApptStartTimeTo, dtpChkInApptStartTimeFrom, dtpApptLetterApptStartTimeTo, dtpApptLetterApptStartTimeFrom };
 
            if (disposing && (components != null))
            {
            
                components.Dispose();
                try
                {
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }

                }
                catch
                {
                }
                //for (int i = dtpControls.Length-1; i >=0; i--)
                //{
                //    System.Windows.Forms.DateTimePicker dtpDate = dtpControls[i];

                //    try
                //    {
                //       if (dtpDate != null)
                //        {
                //            try
                //            {
                //                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDate);
                //            }
                //            catch
                //            {
                //            }
                //            dtpDate.Dispose();
                //            dtpDate = null;
                //        }
                //    }
                //    catch
                //    {
                //    }

               // }

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
             
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
                }
                try
                {
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchPrinting));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbChkInProvider = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls_OverDue = new gloGlobal.gloToolStripIgnoreFocus();
           // this.tls_OverDue = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.ts_btnPrint = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.dtpChkInApptStartTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dtpChkInApptStartTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chkChkInIncludeOneAppt = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rbtnChkInPlan = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbtnChkInCompany = new System.Windows.Forms.RadioButton();
            this.label40 = new System.Windows.Forms.Label();
            this.btnChkInBrowseProvider = new System.Windows.Forms.Button();
            this.cmbChkInPatient = new System.Windows.Forms.ComboBox();
            this.btnChkInClearProvider = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.cmbChkInInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.btnChkInBrowsePatient = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnChkInClearPatient = new System.Windows.Forms.Button();
            this.btnChkInBrowseInsurance = new System.Windows.Forms.Button();
            this.cmbChkInApptTypeType = new System.Windows.Forms.ComboBox();
            this.btnChkInClearInsurance = new System.Windows.Forms.Button();
            this.btnChkInClearApptTypeType = new System.Windows.Forms.Button();
            this.dtpChkInDate = new System.Windows.Forms.DateTimePicker();
            this.btnChkInBrowseApptTypeType = new System.Windows.Forms.Button();
            this.label45 = new System.Windows.Forms.Label();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmbChkInApptType = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnChkInClearApptType = new System.Windows.Forms.Button();
            this.btnChkInClearLocation = new System.Windows.Forms.Button();
            this.btnChkInBrowseApptType = new System.Windows.Forms.Button();
            this.btnChkInBrowseLocation = new System.Windows.Forms.Button();
            this.cmbChkInReosurce = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.cmbChkInLocation = new System.Windows.Forms.ComboBox();
            this.btnChkInBrowseResource = new System.Windows.Forms.Button();
            this.btnChkInClearResource = new System.Windows.Forms.Button();
            this.cmbChkInInsurancePlan = new System.Windows.Forms.ComboBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.dtp_ToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.cmbDiagnosisCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseDiagnosisCode = new System.Windows.Forms.Button();
            this.btnClearDiagnosisCode = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.pnlChkInGrid = new System.Windows.Forms.Panel();
            this.c1ChkInPatients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label28 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.Panel22 = new System.Windows.Forms.Panel();
            this.btnChkInSelectAll = new System.Windows.Forms.Button();
            this.btnChkInClearAll = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pnl_Prgsbar = new System.Windows.Forms.Panel();
            this.prgBar_Print = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.trvTemplates = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnSelectAllClearAll = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel11 = new System.Windows.Forms.Panel();
            this.tbBatchPrint = new System.Windows.Forms.TabControl();
            this.tbCheckInPre = new System.Windows.Forms.TabPage();
            this.panel39 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.trvChkInTemplate = new System.Windows.Forms.TreeView();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnChkInSelectAllTreeNode = new System.Windows.Forms.Button();
            this.btnChkInClearAllTreeNode = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.tbAppLetters = new System.Windows.Forms.TabPage();
            this.panel40 = new System.Windows.Forms.Panel();
            this.panel41 = new System.Windows.Forms.Panel();
            this.trvApptLetterPrintTemplate = new System.Windows.Forms.TreeView();
            this.label136 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.panel42 = new System.Windows.Forms.Panel();
            this.panel43 = new System.Windows.Forms.Panel();
            this.btnApptLetterSelectAllTreeNode = new System.Windows.Forms.Button();
            this.btnApptLetterClearAllTreeNode = new System.Windows.Forms.Button();
            this.label142 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label144 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.panel35 = new System.Windows.Forms.Panel();
            this.pnlApptLetterGrid = new System.Windows.Forms.Panel();
            this.c1ApptLetterPatients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.panel37 = new System.Windows.Forms.Panel();
            this.panel38 = new System.Windows.Forms.Panel();
            this.btnApptLetterSelectAll = new System.Windows.Forms.Button();
            this.btnApptLetterClearAll = new System.Windows.Forms.Button();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.panel31 = new System.Windows.Forms.Panel();
            this.panel32 = new System.Windows.Forms.Panel();
            this.dtpApptLetterApptStartTimeTo = new System.Windows.Forms.DateTimePicker();
            this.dtpApptLetterApptStartTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rbtnApptLetterCancelled = new System.Windows.Forms.RadioButton();
            this.rbtnApptLetterOpen = new System.Windows.Forms.RadioButton();
            this.rbtnApptLetterNoShow = new System.Windows.Forms.RadioButton();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.dtpApptLetterToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpApptLetterFromDate = new System.Windows.Forms.DateTimePicker();
            this.cmbApptLetterDateRange = new System.Windows.Forms.ComboBox();
            this.chkApptLetterIncludeOneAppt = new System.Windows.Forms.CheckBox();
            this.label113 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.rbtnApptLetterPlan = new System.Windows.Forms.RadioButton();
            this.label107 = new System.Windows.Forms.Label();
            this.rbtnApptLetterCompany = new System.Windows.Forms.RadioButton();
            this.cmbApptLetterProvider = new System.Windows.Forms.ComboBox();
            this.btnApptLetterBrowseProvider = new System.Windows.Forms.Button();
            this.cmbApptLetterPatient = new System.Windows.Forms.ComboBox();
            this.btnApptLetterClearProvider = new System.Windows.Forms.Button();
            this.label109 = new System.Windows.Forms.Label();
            this.cmbApptLetterInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.btnApptLetterBrowsePatient = new System.Windows.Forms.Button();
            this.label110 = new System.Windows.Forms.Label();
            this.btnApptLetterClearPatient = new System.Windows.Forms.Button();
            this.btnApptLetterBrowseInsurance = new System.Windows.Forms.Button();
            this.cmbApptLetterApptTypeType = new System.Windows.Forms.ComboBox();
            this.btnApptLetterClearInsurance = new System.Windows.Forms.Button();
            this.btnApptLetterClearApptTypeType = new System.Windows.Forms.Button();
            this.btnApptLetterBrowseApptTypeType = new System.Windows.Forms.Button();
            this.label126 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.cmbApptLetterApptType = new System.Windows.Forms.ComboBox();
            this.label114 = new System.Windows.Forms.Label();
            this.btnApptLetterClearApptType = new System.Windows.Forms.Button();
            this.btnApptLetterClearLocation = new System.Windows.Forms.Button();
            this.btnApptLetterBrowseApptType = new System.Windows.Forms.Button();
            this.btnApptLetterBrowseLocation = new System.Windows.Forms.Button();
            this.cmbApptLetterResource = new System.Windows.Forms.ComboBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.cmbApptLetterLocation = new System.Windows.Forms.ComboBox();
            this.btnApptLetterBrowseResource = new System.Windows.Forms.Button();
            this.btnApptLetterClearResource = new System.Windows.Forms.Button();
            this.cmbApptLetterInsurancePlan = new System.Windows.Forms.ComboBox();
            this.panel33 = new System.Windows.Forms.Panel();
            this.panel34 = new System.Windows.Forms.Panel();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.button50 = new System.Windows.Forms.Button();
            this.button51 = new System.Windows.Forms.Button();
            this.label124 = new System.Windows.Forms.Label();
            this.comboBox24 = new System.Windows.Forms.ComboBox();
            this.comboBox25 = new System.Windows.Forms.ComboBox();
            this.label125 = new System.Windows.Forms.Label();
            this.button52 = new System.Windows.Forms.Button();
            this.button53 = new System.Windows.Forms.Button();
            this.panel18 = new System.Windows.Forms.Panel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.panel27 = new System.Windows.Forms.Panel();
            this.panel28 = new System.Windows.Forms.Panel();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label84 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label85 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label86 = new System.Windows.Forms.Label();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.button18 = new System.Windows.Forms.Button();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.button19 = new System.Windows.Forms.Button();
            this.label87 = new System.Windows.Forms.Label();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.button20 = new System.Windows.Forms.Button();
            this.label88 = new System.Windows.Forms.Label();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
            this.button25 = new System.Windows.Forms.Button();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label92 = new System.Windows.Forms.Label();
            this.button26 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.button30 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
            this.label101 = new System.Windows.Forms.Label();
            this.button32 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.label102 = new System.Windows.Forms.Label();
            this.comboBox14 = new System.Windows.Forms.ComboBox();
            this.comboBox15 = new System.Windows.Forms.ComboBox();
            this.label103 = new System.Windows.Forms.Label();
            this.button34 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls_OverDue.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlChkInGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ChkInPatients)).BeginInit();
            this.pnlProviderHeader.SuspendLayout();
            this.Panel22.SuspendLayout();
            this.pnl_Prgsbar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tbBatchPrint.SuspendLayout();
            this.tbCheckInPre.SuspendLayout();
            this.panel39.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.tbAppLetters.SuspendLayout();
            this.panel40.SuspendLayout();
            this.panel41.SuspendLayout();
            this.panel42.SuspendLayout();
            this.panel43.SuspendLayout();
            this.panel35.SuspendLayout();
            this.pnlApptLetterGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ApptLetterPatients)).BeginInit();
            this.panel37.SuspendLayout();
            this.panel38.SuspendLayout();
            this.panel31.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel33.SuspendLayout();
            this.panel34.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel29.SuspendLayout();
            this.panel30.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbChkInProvider
            // 
            this.cmbChkInProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInProvider.FormattingEnabled = true;
            this.cmbChkInProvider.Location = new System.Drawing.Point(341, 37);
            this.cmbChkInProvider.Name = "cmbChkInProvider";
            this.cmbChkInProvider.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInProvider.TabIndex = 6;
            this.cmbChkInProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(427, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "   Patient ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatAppearance.BorderSize = 0;
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(555, 1);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(25, 23);
            this.btnClearPatient.TabIndex = 3;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Visible = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            this.btnClearPatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearPatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatAppearance.BorderSize = 0;
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(530, 1);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(25, 23);
            this.btnBrowsePatient.TabIndex = 2;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Visible = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            this.btnBrowsePatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowsePatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls_OverDue);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(1284, 53);
            this.pnl_tlspTOP.TabIndex = 210;
            // 
            // tls_OverDue
            // 
            this.tls_OverDue.BackColor = System.Drawing.Color.Transparent;
            this.tls_OverDue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_OverDue.BackgroundImage")));
            this.tls_OverDue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_OverDue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls_OverDue.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_OverDue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateBatch,
            this.ts_btnRefresh,
            this.ts_btnPrint,
            this.ts_btnClose});
            this.tls_OverDue.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_OverDue.Location = new System.Drawing.Point(0, 0);
            this.tls_OverDue.Name = "tls_OverDue";
            this.tls_OverDue.Size = new System.Drawing.Size(1284, 53);
            this.tls_OverDue.TabIndex = 0;
            this.tls_OverDue.Text = "toolStrip1";
            // 
            // tsb_GenerateBatch
            // 
            this.tsb_GenerateBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateBatch.Image")));
            this.tsb_GenerateBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateBatch.Name = "tsb_GenerateBatch";
            this.tsb_GenerateBatch.Size = new System.Drawing.Size(105, 50);
            this.tsb_GenerateBatch.Tag = "Generate Batch";
            this.tsb_GenerateBatch.Text = "&Generate Batch";
            this.tsb_GenerateBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateBatch.ToolTipText = "Generate Batch";
            this.tsb_GenerateBatch.Click += new System.EventHandler(this.tsb_GenerateBatch_Click);
            // 
            // ts_btnRefresh
            // 
            this.ts_btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRefresh.Image")));
            this.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRefresh.Name = "ts_btnRefresh";
            this.ts_btnRefresh.Size = new System.Drawing.Size(58, 50);
            this.ts_btnRefresh.Tag = "Refresh";
            this.ts_btnRefresh.Text = "&Refresh";
            this.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRefresh.Visible = false;
            this.ts_btnRefresh.Click += new System.EventHandler(this.ts_btnRefresh_Click);
            // 
            // ts_btnPrint
            // 
            this.ts_btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPrint.Image")));
            this.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPrint.Name = "ts_btnPrint";
            this.ts_btnPrint.Size = new System.Drawing.Size(41, 50);
            this.ts_btnPrint.Tag = "Print";
            this.ts_btnPrint.Text = "&Print";
            this.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPrint.Click += new System.EventHandler(this.ts_btnPrint_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlControl.Location = new System.Drawing.Point(431, 294);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlControl.Size = new System.Drawing.Size(440, 10);
            this.pnlControl.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.panel14);
            this.panel3.Controls.Add(this.panel12);
            this.panel3.Controls.Add(this.cmb_datefilter);
            this.panel3.Controls.Add(this.dtp_ToDate);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnClearCPT);
            this.panel3.Controls.Add(this.btnBrowseCPT);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cmbCPT);
            this.panel3.Controls.Add(this.cmbDiagnosisCode);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.btnBrowseDiagnosisCode);
            this.panel3.Controls.Add(this.btnClearDiagnosisCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1270, 147);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.dtpChkInApptStartTimeTo);
            this.panel14.Controls.Add(this.dtpChkInApptStartTimeFrom);
            this.panel14.Controls.Add(this.label16);
            this.panel14.Controls.Add(this.label13);
            this.panel14.Controls.Add(this.chkChkInIncludeOneAppt);
            this.panel14.Controls.Add(this.label15);
            this.panel14.Controls.Add(this.rbtnChkInPlan);
            this.panel14.Controls.Add(this.label14);
            this.panel14.Controls.Add(this.rbtnChkInCompany);
            this.panel14.Controls.Add(this.cmbChkInProvider);
            this.panel14.Controls.Add(this.label40);
            this.panel14.Controls.Add(this.btnChkInBrowseProvider);
            this.panel14.Controls.Add(this.cmbChkInPatient);
            this.panel14.Controls.Add(this.btnChkInClearProvider);
            this.panel14.Controls.Add(this.label46);
            this.panel14.Controls.Add(this.cmbChkInInsuranceCompany);
            this.panel14.Controls.Add(this.btnChkInBrowsePatient);
            this.panel14.Controls.Add(this.label6);
            this.panel14.Controls.Add(this.btnChkInClearPatient);
            this.panel14.Controls.Add(this.btnChkInBrowseInsurance);
            this.panel14.Controls.Add(this.cmbChkInApptTypeType);
            this.panel14.Controls.Add(this.btnChkInClearInsurance);
            this.panel14.Controls.Add(this.btnChkInClearApptTypeType);
            this.panel14.Controls.Add(this.dtpChkInDate);
            this.panel14.Controls.Add(this.btnChkInBrowseApptTypeType);
            this.panel14.Controls.Add(this.label1);
            this.panel14.Controls.Add(this.label45);
            this.panel14.Controls.Add(this.lbl_datefilter);
            this.panel14.Controls.Add(this.cmbChkInApptType);
            this.panel14.Controls.Add(this.label26);
            this.panel14.Controls.Add(this.btnChkInClearApptType);
            this.panel14.Controls.Add(this.btnChkInClearLocation);
            this.panel14.Controls.Add(this.btnChkInBrowseApptType);
            this.panel14.Controls.Add(this.btnChkInBrowseLocation);
            this.panel14.Controls.Add(this.cmbChkInReosurce);
            this.panel14.Controls.Add(this.label42);
            this.panel14.Controls.Add(this.label41);
            this.panel14.Controls.Add(this.cmbChkInLocation);
            this.panel14.Controls.Add(this.btnChkInBrowseResource);
            this.panel14.Controls.Add(this.btnChkInClearResource);
            this.panel14.Controls.Add(this.cmbChkInInsurancePlan);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 24);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel14.Size = new System.Drawing.Size(1270, 123);
            this.panel14.TabIndex = 0;
            // 
            // dtpChkInApptStartTimeTo
            // 
            this.dtpChkInApptStartTimeTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpChkInApptStartTimeTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpChkInApptStartTimeTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpChkInApptStartTimeTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpChkInApptStartTimeTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpChkInApptStartTimeTo.CustomFormat = "hh:mm tt";
            this.dtpChkInApptStartTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChkInApptStartTimeTo.Location = new System.Drawing.Point(126, 67);
            this.dtpChkInApptStartTimeTo.Name = "dtpChkInApptStartTimeTo";
            this.dtpChkInApptStartTimeTo.ShowCheckBox = true;
            this.dtpChkInApptStartTimeTo.ShowUpDown = true;
            this.dtpChkInApptStartTimeTo.Size = new System.Drawing.Size(111, 22);
            this.dtpChkInApptStartTimeTo.TabIndex = 2;
            // 
            // dtpChkInApptStartTimeFrom
            // 
            this.dtpChkInApptStartTimeFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpChkInApptStartTimeFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpChkInApptStartTimeFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpChkInApptStartTimeFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpChkInApptStartTimeFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpChkInApptStartTimeFrom.CustomFormat = "hh:mm tt";
            this.dtpChkInApptStartTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChkInApptStartTimeFrom.Location = new System.Drawing.Point(126, 37);
            this.dtpChkInApptStartTimeFrom.Name = "dtpChkInApptStartTimeFrom";
            this.dtpChkInApptStartTimeFrom.ShowCheckBox = true;
            this.dtpChkInApptStartTimeFrom.ShowUpDown = true;
            this.dtpChkInApptStartTimeFrom.Size = new System.Drawing.Size(111, 22);
            this.dtpChkInApptStartTimeFrom.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(1, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1268, 1);
            this.label16.TabIndex = 262;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(1, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1268, 1);
            this.label13.TabIndex = 220;
            this.label13.Text = "label2";
            // 
            // chkChkInIncludeOneAppt
            // 
            this.chkChkInIncludeOneAppt.AutoSize = true;
            this.chkChkInIncludeOneAppt.Location = new System.Drawing.Point(661, 98);
            this.chkChkInIncludeOneAppt.Name = "chkChkInIncludeOneAppt";
            this.chkChkInIncludeOneAppt.Size = new System.Drawing.Size(258, 18);
            this.chkChkInIncludeOneAppt.TabIndex = 26;
            this.chkChkInIncludeOneAppt.Text = "Include only one Appointment per Patient";
            this.chkChkInIncludeOneAppt.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(1269, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 120);
            this.label15.TabIndex = 219;
            this.label15.Text = "label3";
            // 
            // rbtnChkInPlan
            // 
            this.rbtnChkInPlan.AutoSize = true;
            this.rbtnChkInPlan.Location = new System.Drawing.Point(1095, 39);
            this.rbtnChkInPlan.Name = "rbtnChkInPlan";
            this.rbtnChkInPlan.Size = new System.Drawing.Size(47, 18);
            this.rbtnChkInPlan.TabIndex = 22;
            this.rbtnChkInPlan.Text = "Plan";
            this.rbtnChkInPlan.UseVisualStyleBackColor = true;
            this.rbtnChkInPlan.CheckedChanged += new System.EventHandler(this.rbtnChkInPlan_CheckedChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 120);
            this.label14.TabIndex = 218;
            this.label14.Text = "label4";
            // 
            // rbtnChkInCompany
            // 
            this.rbtnChkInCompany.AutoSize = true;
            this.rbtnChkInCompany.Checked = true;
            this.rbtnChkInCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnChkInCompany.Location = new System.Drawing.Point(984, 39);
            this.rbtnChkInCompany.Name = "rbtnChkInCompany";
            this.rbtnChkInCompany.Size = new System.Drawing.Size(82, 18);
            this.rbtnChkInCompany.TabIndex = 21;
            this.rbtnChkInCompany.TabStop = true;
            this.rbtnChkInCompany.Text = "Company";
            this.rbtnChkInCompany.UseVisualStyleBackColor = true;
            this.rbtnChkInCompany.CheckedChanged += new System.EventHandler(this.rbtnChkInCompany_CheckedChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(20, 12);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(14, 14);
            this.label40.TabIndex = 234;
            this.label40.Text = "*";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnChkInBrowseProvider
            // 
            this.btnChkInBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseProvider.BackgroundImage")));
            this.btnChkInBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseProvider.Image")));
            this.btnChkInBrowseProvider.Location = new System.Drawing.Point(502, 37);
            this.btnChkInBrowseProvider.Name = "btnChkInBrowseProvider";
            this.btnChkInBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseProvider.TabIndex = 7;
            this.btnChkInBrowseProvider.UseVisualStyleBackColor = false;
            this.btnChkInBrowseProvider.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbChkInPatient
            // 
            this.cmbChkInPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInPatient.FormattingEnabled = true;
            this.cmbChkInPatient.Location = new System.Drawing.Point(984, 8);
            this.cmbChkInPatient.Name = "cmbChkInPatient";
            this.cmbChkInPatient.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInPatient.TabIndex = 18;
            // 
            // btnChkInClearProvider
            // 
            this.btnChkInClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearProvider.BackgroundImage")));
            this.btnChkInClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearProvider.Image")));
            this.btnChkInClearProvider.Location = new System.Drawing.Point(527, 37);
            this.btnChkInClearProvider.Name = "btnChkInClearProvider";
            this.btnChkInClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearProvider.TabIndex = 8;
            this.btnChkInClearProvider.UseVisualStyleBackColor = false;
            this.btnChkInClearProvider.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(927, 12);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(54, 14);
            this.label46.TabIndex = 256;
            this.label46.Text = "Patient :";
            // 
            // cmbChkInInsuranceCompany
            // 
            this.cmbChkInInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInInsuranceCompany.FormattingEnabled = true;
            this.cmbChkInInsuranceCompany.Location = new System.Drawing.Point(984, 67);
            this.cmbChkInInsuranceCompany.Name = "cmbChkInInsuranceCompany";
            this.cmbChkInInsuranceCompany.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInInsuranceCompany.TabIndex = 23;
            // 
            // btnChkInBrowsePatient
            // 
            this.btnChkInBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowsePatient.BackgroundImage")));
            this.btnChkInBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowsePatient.Image")));
            this.btnChkInBrowsePatient.Location = new System.Drawing.Point(1145, 8);
            this.btnChkInBrowsePatient.Name = "btnChkInBrowsePatient";
            this.btnChkInBrowsePatient.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowsePatient.TabIndex = 19;
            this.btnChkInBrowsePatient.UseVisualStyleBackColor = false;
            this.btnChkInBrowsePatient.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(913, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 14);
            this.label6.TabIndex = 224;
            this.label6.Text = "Insurance :";
            // 
            // btnChkInClearPatient
            // 
            this.btnChkInClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearPatient.BackgroundImage")));
            this.btnChkInClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearPatient.Image")));
            this.btnChkInClearPatient.Location = new System.Drawing.Point(1170, 8);
            this.btnChkInClearPatient.Name = "btnChkInClearPatient";
            this.btnChkInClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearPatient.TabIndex = 20;
            this.btnChkInClearPatient.UseVisualStyleBackColor = false;
            this.btnChkInClearPatient.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnChkInBrowseInsurance
            // 
            this.btnChkInBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseInsurance.BackgroundImage")));
            this.btnChkInBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseInsurance.Image")));
            this.btnChkInBrowseInsurance.Location = new System.Drawing.Point(1145, 67);
            this.btnChkInBrowseInsurance.Name = "btnChkInBrowseInsurance";
            this.btnChkInBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseInsurance.TabIndex = 24;
            this.btnChkInBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnChkInBrowseInsurance.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbChkInApptTypeType
            // 
            this.cmbChkInApptTypeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInApptTypeType.FormattingEnabled = true;
            this.cmbChkInApptTypeType.Location = new System.Drawing.Point(661, 37);
            this.cmbChkInApptTypeType.Name = "cmbChkInApptTypeType";
            this.cmbChkInApptTypeType.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInApptTypeType.TabIndex = 12;
            // 
            // btnChkInClearInsurance
            // 
            this.btnChkInClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearInsurance.BackgroundImage")));
            this.btnChkInClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearInsurance.Image")));
            this.btnChkInClearInsurance.Location = new System.Drawing.Point(1170, 67);
            this.btnChkInClearInsurance.Name = "btnChkInClearInsurance";
            this.btnChkInClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearInsurance.TabIndex = 25;
            this.btnChkInClearInsurance.UseVisualStyleBackColor = false;
            this.btnChkInClearInsurance.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnChkInClearApptTypeType
            // 
            this.btnChkInClearApptTypeType.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearApptTypeType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearApptTypeType.BackgroundImage")));
            this.btnChkInClearApptTypeType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearApptTypeType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearApptTypeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearApptTypeType.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearApptTypeType.Image")));
            this.btnChkInClearApptTypeType.Location = new System.Drawing.Point(847, 37);
            this.btnChkInClearApptTypeType.Name = "btnChkInClearApptTypeType";
            this.btnChkInClearApptTypeType.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearApptTypeType.TabIndex = 14;
            this.btnChkInClearApptTypeType.UseVisualStyleBackColor = false;
            this.btnChkInClearApptTypeType.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // dtpChkInDate
            // 
            this.dtpChkInDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpChkInDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpChkInDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpChkInDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpChkInDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpChkInDate.CustomFormat = "MM/dd/yyyy";
            this.dtpChkInDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpChkInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChkInDate.Location = new System.Drawing.Point(126, 8);
            this.dtpChkInDate.Name = "dtpChkInDate";
            this.dtpChkInDate.Size = new System.Drawing.Size(111, 22);
            this.dtpChkInDate.TabIndex = 0;
            this.dtpChkInDate.ValueChanged += new System.EventHandler(this.dtp_FromDate_ValueChanged);
            // 
            // btnChkInBrowseApptTypeType
            // 
            this.btnChkInBrowseApptTypeType.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseApptTypeType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseApptTypeType.BackgroundImage")));
            this.btnChkInBrowseApptTypeType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseApptTypeType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseApptTypeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseApptTypeType.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseApptTypeType.Image")));
            this.btnChkInBrowseApptTypeType.Location = new System.Drawing.Point(822, 37);
            this.btnChkInBrowseApptTypeType.Name = "btnChkInBrowseApptTypeType";
            this.btnChkInBrowseApptTypeType.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseApptTypeType.TabIndex = 13;
            this.btnChkInBrowseApptTypeType.UseVisualStyleBackColor = false;
            this.btnChkInBrowseApptTypeType.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(585, 41);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(74, 14);
            this.label45.TabIndex = 246;
            this.label45.Text = "Appt Type :";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(31, 12);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(93, 14);
            this.lbl_datefilter.TabIndex = 231;
            this.lbl_datefilter.Text = "Check-In Date :";
            // 
            // cmbChkInApptType
            // 
            this.cmbChkInApptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInApptType.FormattingEnabled = true;
            this.cmbChkInApptType.Location = new System.Drawing.Point(661, 67);
            this.cmbChkInApptType.Name = "cmbChkInApptType";
            this.cmbChkInApptType.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInApptType.TabIndex = 15;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(15, 41);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(109, 14);
            this.label26.TabIndex = 232;
            this.label26.Text = "Appt Start Times :";
            // 
            // btnChkInClearApptType
            // 
            this.btnChkInClearApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearApptType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearApptType.BackgroundImage")));
            this.btnChkInClearApptType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearApptType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearApptType.Image")));
            this.btnChkInClearApptType.Location = new System.Drawing.Point(847, 67);
            this.btnChkInClearApptType.Name = "btnChkInClearApptType";
            this.btnChkInClearApptType.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearApptType.TabIndex = 17;
            this.btnChkInClearApptType.UseVisualStyleBackColor = false;
            this.btnChkInClearApptType.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnChkInClearLocation
            // 
            this.btnChkInClearLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearLocation.BackgroundImage")));
            this.btnChkInClearLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearLocation.Image")));
            this.btnChkInClearLocation.Location = new System.Drawing.Point(527, 8);
            this.btnChkInClearLocation.Name = "btnChkInClearLocation";
            this.btnChkInClearLocation.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearLocation.TabIndex = 5;
            this.btnChkInClearLocation.UseVisualStyleBackColor = false;
            this.btnChkInClearLocation.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnChkInBrowseApptType
            // 
            this.btnChkInBrowseApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseApptType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseApptType.BackgroundImage")));
            this.btnChkInBrowseApptType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseApptType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseApptType.Image")));
            this.btnChkInBrowseApptType.Location = new System.Drawing.Point(822, 67);
            this.btnChkInBrowseApptType.Name = "btnChkInBrowseApptType";
            this.btnChkInBrowseApptType.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseApptType.TabIndex = 16;
            this.btnChkInBrowseApptType.UseVisualStyleBackColor = false;
            this.btnChkInBrowseApptType.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btnChkInBrowseLocation
            // 
            this.btnChkInBrowseLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseLocation.BackgroundImage")));
            this.btnChkInBrowseLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseLocation.Image")));
            this.btnChkInBrowseLocation.Location = new System.Drawing.Point(502, 8);
            this.btnChkInBrowseLocation.Name = "btnChkInBrowseLocation";
            this.btnChkInBrowseLocation.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseLocation.TabIndex = 4;
            this.btnChkInBrowseLocation.UseVisualStyleBackColor = false;
            this.btnChkInBrowseLocation.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbChkInReosurce
            // 
            this.cmbChkInReosurce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInReosurce.FormattingEnabled = true;
            this.cmbChkInReosurce.Location = new System.Drawing.Point(661, 8);
            this.cmbChkInReosurce.Name = "cmbChkInReosurce";
            this.cmbChkInReosurce.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInReosurce.TabIndex = 9;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(273, 12);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(66, 14);
            this.label42.TabIndex = 241;
            this.label42.Text = "Locations :";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(589, 12);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(70, 14);
            this.label41.TabIndex = 245;
            this.label41.Text = "Resources :";
            // 
            // cmbChkInLocation
            // 
            this.cmbChkInLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInLocation.FormattingEnabled = true;
            this.cmbChkInLocation.Location = new System.Drawing.Point(341, 8);
            this.cmbChkInLocation.Name = "cmbChkInLocation";
            this.cmbChkInLocation.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInLocation.TabIndex = 3;
            // 
            // btnChkInBrowseResource
            // 
            this.btnChkInBrowseResource.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInBrowseResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseResource.BackgroundImage")));
            this.btnChkInBrowseResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInBrowseResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInBrowseResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInBrowseResource.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInBrowseResource.Image")));
            this.btnChkInBrowseResource.Location = new System.Drawing.Point(822, 8);
            this.btnChkInBrowseResource.Name = "btnChkInBrowseResource";
            this.btnChkInBrowseResource.Size = new System.Drawing.Size(22, 22);
            this.btnChkInBrowseResource.TabIndex = 10;
            this.btnChkInBrowseResource.UseVisualStyleBackColor = false;
            this.btnChkInBrowseResource.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btnChkInClearResource
            // 
            this.btnChkInClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChkInClearResource.BackgroundImage")));
            this.btnChkInClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnChkInClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearResource.Image")));
            this.btnChkInClearResource.Location = new System.Drawing.Point(847, 8);
            this.btnChkInClearResource.Name = "btnChkInClearResource";
            this.btnChkInClearResource.Size = new System.Drawing.Size(22, 22);
            this.btnChkInClearResource.TabIndex = 11;
            this.btnChkInClearResource.UseVisualStyleBackColor = false;
            this.btnChkInClearResource.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // cmbChkInInsurancePlan
            // 
            this.cmbChkInInsurancePlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChkInInsurancePlan.FormattingEnabled = true;
            this.cmbChkInInsurancePlan.Location = new System.Drawing.Point(984, 67);
            this.cmbChkInInsurancePlan.Name = "cmbChkInInsurancePlan";
            this.cmbChkInInsurancePlan.Size = new System.Drawing.Size(158, 22);
            this.cmbChkInInsurancePlan.TabIndex = 263;
            this.cmbChkInInsurancePlan.Visible = false;
            // 
            // panel12
            // 
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1270, 24);
            this.panel12.TabIndex = 262;
            this.panel12.TabStop = true;
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.label48);
            this.panel13.Controls.Add(this.label49);
            this.panel13.Controls.Add(this.label52);
            this.panel13.Controls.Add(this.label54);
            this.panel13.Controls.Add(this.label39);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1270, 24);
            this.panel13.TabIndex = 1;
            this.panel13.TabStop = true;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Location = new System.Drawing.Point(1269, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 22);
            this.label48.TabIndex = 98;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label49.Location = new System.Drawing.Point(1, 23);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1269, 1);
            this.label49.TabIndex = 97;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Location = new System.Drawing.Point(1, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1269, 1);
            this.label52.TabIndex = 99;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Left;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(0, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 24);
            this.label54.TabIndex = 100;
            this.label54.Text = "label4";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(5, 5);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(452, 14);
            this.label39.TabIndex = 233;
            this.label39.Text = "1. Specify Appointment filters and options, then select Generate Batch :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(751, 193);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(108, 22);
            this.cmb_datefilter.TabIndex = 1;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // dtp_ToDate
            // 
            this.dtp_ToDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtp_ToDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtp_ToDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtp_ToDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtp_ToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtp_ToDate.CustomFormat = "MM/dd/yyyy";
            this.dtp_ToDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ToDate.Location = new System.Drawing.Point(44, 171);
            this.dtp_ToDate.Name = "dtp_ToDate";
            this.dtp_ToDate.Size = new System.Drawing.Size(108, 22);
            this.dtp_ToDate.TabIndex = 9;
            this.dtp_ToDate.ValueChanged += new System.EventHandler(this.dtp_ToDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(668, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 212;
            this.label4.Text = "Date Range :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(502, 157);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(23, 23);
            this.btnClearCPT.TabIndex = 8;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(473, 157);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseCPT.TabIndex = 7;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(272, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 14);
            this.label7.TabIndex = 228;
            this.label7.Text = "CPT :";
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(315, 157);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(149, 22);
            this.cmbCPT.TabIndex = 6;
            // 
            // cmbDiagnosisCode
            // 
            this.cmbDiagnosisCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosisCode.FormattingEnabled = true;
            this.cmbDiagnosisCode.Location = new System.Drawing.Point(315, 188);
            this.cmbDiagnosisCode.Name = "cmbDiagnosisCode";
            this.cmbDiagnosisCode.Size = new System.Drawing.Size(149, 22);
            this.cmbDiagnosisCode.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(213, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 14);
            this.label5.TabIndex = 220;
            this.label5.Text = "Diagnosis Code :";
            // 
            // btnBrowseDiagnosisCode
            // 
            this.btnBrowseDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.BackgroundImage")));
            this.btnBrowseDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.Image")));
            this.btnBrowseDiagnosisCode.Location = new System.Drawing.Point(473, 188);
            this.btnBrowseDiagnosisCode.Name = "btnBrowseDiagnosisCode";
            this.btnBrowseDiagnosisCode.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseDiagnosisCode.TabIndex = 11;
            this.btnBrowseDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnBrowseDiagnosisCode.Click += new System.EventHandler(this.btnBrowseDiagnosisCode_Click);
            // 
            // btnClearDiagnosisCode
            // 
            this.btnClearDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.BackgroundImage")));
            this.btnClearDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.Image")));
            this.btnClearDiagnosisCode.Location = new System.Drawing.Point(502, 188);
            this.btnClearDiagnosisCode.Name = "btnClearDiagnosisCode";
            this.btnClearDiagnosisCode.Size = new System.Drawing.Size(23, 23);
            this.btnClearDiagnosisCode.TabIndex = 12;
            this.btnClearDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnClearDiagnosisCode.Click += new System.EventHandler(this.btnClearDiagnosisCode_Click);
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.pnlChkInGrid);
            this.pnlProvider.Controls.Add(this.pnlProviderHeader);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(3, 150);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlProvider.Size = new System.Drawing.Size(1270, 292);
            this.pnlProvider.TabIndex = 1;
            // 
            // pnlChkInGrid
            // 
            this.pnlChkInGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlChkInGrid.Controls.Add(this.c1ChkInPatients);
            this.pnlChkInGrid.Controls.Add(this.label28);
            this.pnlChkInGrid.Controls.Add(this.label8);
            this.pnlChkInGrid.Controls.Add(this.label22);
            this.pnlChkInGrid.Controls.Add(this.label27);
            this.pnlChkInGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChkInGrid.Location = new System.Drawing.Point(0, 27);
            this.pnlChkInGrid.Name = "pnlChkInGrid";
            this.pnlChkInGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlChkInGrid.Size = new System.Drawing.Size(1270, 262);
            this.pnlChkInGrid.TabIndex = 3;
            // 
            // c1ChkInPatients
            // 
            this.c1ChkInPatients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ChkInPatients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ChkInPatients.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1ChkInPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ChkInPatients.ExtendLastCol = true;
            this.c1ChkInPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ChkInPatients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ChkInPatients.Location = new System.Drawing.Point(1, 4);
            this.c1ChkInPatients.Name = "c1ChkInPatients";
            this.c1ChkInPatients.Rows.Count = 1;
            this.c1ChkInPatients.Rows.DefaultSize = 19;
            this.c1ChkInPatients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ChkInPatients.Size = new System.Drawing.Size(1268, 257);
            this.c1ChkInPatients.StyleInfo = resources.GetString("c1ChkInPatients.StyleInfo");
            this.c1ChkInPatients.TabIndex = 0;
            this.c1ChkInPatients.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ChkInPatients_AfterEdit);
            this.c1ChkInPatients.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ChkInPatients_MouseMove);
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 4);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 257);
            this.label28.TabIndex = 100;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1269, 1);
            this.label8.TabIndex = 102;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(1269, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 258);
            this.label22.TabIndex = 101;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Location = new System.Drawing.Point(0, 261);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1270, 1);
            this.label27.TabIndex = 103;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.Panel22);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(1270, 24);
            this.pnlProviderHeader.TabIndex = 2;
            // 
            // Panel22
            // 
            this.Panel22.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel22.Controls.Add(this.btnChkInSelectAll);
            this.Panel22.Controls.Add(this.btnChkInClearAll);
            this.Panel22.Controls.Add(this.label44);
            this.Panel22.Controls.Add(this.label43);
            this.Panel22.Controls.Add(this.label30);
            this.Panel22.Controls.Add(this.label32);
            this.Panel22.Controls.Add(this.label31);
            this.Panel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel22.Location = new System.Drawing.Point(0, 0);
            this.Panel22.Name = "Panel22";
            this.Panel22.Size = new System.Drawing.Size(1270, 24);
            this.Panel22.TabIndex = 1;
            // 
            // btnChkInSelectAll
            // 
            this.btnChkInSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInSelectAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChkInSelectAll.FlatAppearance.BorderSize = 0;
            this.btnChkInSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChkInSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInSelectAll.Image")));
            this.btnChkInSelectAll.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChkInSelectAll.Location = new System.Drawing.Point(1211, 1);
            this.btnChkInSelectAll.Name = "btnChkInSelectAll";
            this.btnChkInSelectAll.Size = new System.Drawing.Size(29, 22);
            this.btnChkInSelectAll.TabIndex = 0;
            this.btnChkInSelectAll.UseVisualStyleBackColor = false;
            this.btnChkInSelectAll.Visible = false;
            this.btnChkInSelectAll.Click += new System.EventHandler(this.btnChkInSelectAll_Click);
            // 
            // btnChkInClearAll
            // 
            this.btnChkInClearAll.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChkInClearAll.FlatAppearance.BorderSize = 0;
            this.btnChkInClearAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChkInClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearAll.Image")));
            this.btnChkInClearAll.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChkInClearAll.Location = new System.Drawing.Point(1240, 1);
            this.btnChkInClearAll.Name = "btnChkInClearAll";
            this.btnChkInClearAll.Size = new System.Drawing.Size(29, 22);
            this.btnChkInClearAll.TabIndex = 1;
            this.btnChkInClearAll.UseVisualStyleBackColor = false;
            this.btnChkInClearAll.Click += new System.EventHandler(this.btnChkInClearAll_Click);
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(1, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1268, 22);
            this.label44.TabIndex = 0;
            this.label44.Text = "  2. Confirm Appointments for print";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Location = new System.Drawing.Point(1, 23);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1268, 1);
            this.label43.TabIndex = 97;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Location = new System.Drawing.Point(1, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1268, 1);
            this.label30.TabIndex = 99;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(1269, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 24);
            this.label32.TabIndex = 101;
            this.label32.Text = "label4";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 24);
            this.label31.TabIndex = 100;
            this.label31.Text = "label4";
            // 
            // pnl_Prgsbar
            // 
            this.pnl_Prgsbar.Controls.Add(this.prgBar_Print);
            this.pnl_Prgsbar.Location = new System.Drawing.Point(0, 626);
            this.pnl_Prgsbar.Name = "pnl_Prgsbar";
            this.pnl_Prgsbar.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnl_Prgsbar.Size = new System.Drawing.Size(841, 20);
            this.pnl_Prgsbar.TabIndex = 3;
            // 
            // prgBar_Print
            // 
            this.prgBar_Print.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgBar_Print.Location = new System.Drawing.Point(3, 1);
            this.prgBar_Print.Name = "prgBar_Print";
            this.prgBar_Print.Size = new System.Drawing.Size(835, 16);
            this.prgBar_Print.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(587, 23);
            this.panel1.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 18);
            this.label10.TabIndex = 217;
            this.label10.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(3, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(580, 1);
            this.label9.TabIndex = 218;
            this.label9.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(583, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 19);
            this.label11.TabIndex = 216;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(581, 1);
            this.label12.TabIndex = 215;
            this.label12.Text = "label1";
            // 
            // trvTemplates
            // 
            this.trvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTemplates.CheckBoxes = true;
            this.trvTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTemplates.ForeColor = System.Drawing.Color.Black;
            this.trvTemplates.HideSelection = false;
            this.trvTemplates.Location = new System.Drawing.Point(7, 4);
            this.trvTemplates.Name = "trvTemplates";
            this.trvTemplates.Size = new System.Drawing.Size(486, 17);
            this.trvTemplates.TabIndex = 0;
            this.trvTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTemplates_AfterCheck);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel2.Location = new System.Drawing.Point(342, 541);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(587, 51);
            this.panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(587, 28);
            this.panel5.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.btnSelectAllClearAll);
            this.panel9.Controls.Add(this.panel8);
            this.panel9.Controls.Add(this.btnBrowsePatient);
            this.panel9.Controls.Add(this.btnClearPatient);
            this.panel9.Controls.Add(this.label33);
            this.panel9.Controls.Add(this.label34);
            this.panel9.Controls.Add(this.label35);
            this.panel9.Controls.Add(this.label36);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel9.Location = new System.Drawing.Point(3, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(581, 25);
            this.panel9.TabIndex = 1;
            // 
            // btnSelectAllClearAll
            // 
            this.btnSelectAllClearAll.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.btnSelectAllClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectAllClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAllClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAllClearAll.Location = new System.Drawing.Point(428, 1);
            this.btnSelectAllClearAll.Name = "btnSelectAllClearAll";
            this.btnSelectAllClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllClearAll.TabIndex = 0;
            this.btnSelectAllClearAll.Tag = "Select";
            this.btnSelectAllClearAll.Text = "&Select All";
            this.btnSelectAllClearAll.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(503, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(27, 23);
            this.panel8.TabIndex = 217;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label33.Location = new System.Drawing.Point(1, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(579, 1);
            this.label33.TabIndex = 8;
            this.label33.Text = "label2";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(0, 1);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 24);
            this.label34.TabIndex = 7;
            this.label34.Text = "label4";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label35.Location = new System.Drawing.Point(580, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 24);
            this.label35.TabIndex = 6;
            this.label35.Text = "label3";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(581, 1);
            this.label36.TabIndex = 5;
            this.label36.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel4.Controls.Add(this.trvTemplates);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(0, 28);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(497, 25);
            this.panel4.TabIndex = 0;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.White;
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(7, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(486, 3);
            this.label25.TabIndex = 221;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(4, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(3, 20);
            this.label24.TabIndex = 220;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(4, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(489, 1);
            this.label17.TabIndex = 218;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 21);
            this.label18.TabIndex = 217;
            this.label18.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(493, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 21);
            this.label19.TabIndex = 216;
            this.label19.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(491, 1);
            this.label20.TabIndex = 215;
            this.label20.Text = "label1";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(497, 28);
            this.panel6.TabIndex = 219;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel10.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.label3);
            this.panel10.Controls.Add(this.label21);
            this.panel10.Controls.Add(this.label23);
            this.panel10.Controls.Add(this.label37);
            this.panel10.Controls.Add(this.label38);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel10.Location = new System.Drawing.Point(3, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(491, 25);
            this.panel10.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(489, 23);
            this.label3.TabIndex = 15;
            this.label3.Text = "   Templates ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(1, 24);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(489, 1);
            this.label21.TabIndex = 8;
            this.label21.Text = "label2";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 24);
            this.label23.TabIndex = 7;
            this.label23.Text = "label4";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label37.Location = new System.Drawing.Point(490, 1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 24);
            this.label37.TabIndex = 6;
            this.label37.Text = "label3";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(491, 1);
            this.label38.TabIndex = 5;
            this.label38.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.panel4);
            this.panel11.Controls.Add(this.panel6);
            this.panel11.Location = new System.Drawing.Point(448, 629);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(497, 53);
            this.panel11.TabIndex = 2;
            // 
            // tbBatchPrint
            // 
            this.tbBatchPrint.Controls.Add(this.tbCheckInPre);
            this.tbBatchPrint.Controls.Add(this.tbAppLetters);
            this.tbBatchPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBatchPrint.Location = new System.Drawing.Point(0, 53);
            this.tbBatchPrint.Name = "tbBatchPrint";
            this.tbBatchPrint.SelectedIndex = 0;
            this.tbBatchPrint.Size = new System.Drawing.Size(1284, 672);
            this.tbBatchPrint.TabIndex = 211;
            this.tbBatchPrint.SelectedIndexChanged += new System.EventHandler(this.tbBatchPrint_SelectedIndexChanged);
            // 
            // tbCheckInPre
            // 
            this.tbCheckInPre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbCheckInPre.Controls.Add(this.panel39);
            this.tbCheckInPre.Controls.Add(this.pnlProvider);
            this.tbCheckInPre.Controls.Add(this.pnlControl);
            this.tbCheckInPre.Controls.Add(this.panel3);
            this.tbCheckInPre.Location = new System.Drawing.Point(4, 23);
            this.tbCheckInPre.Name = "tbCheckInPre";
            this.tbCheckInPre.Padding = new System.Windows.Forms.Padding(3);
            this.tbCheckInPre.Size = new System.Drawing.Size(1276, 645);
            this.tbCheckInPre.TabIndex = 0;
            this.tbCheckInPre.Text = "Check-In";
            // 
            // panel39
            // 
            this.panel39.Controls.Add(this.panel17);
            this.panel39.Controls.Add(this.panel15);
            this.panel39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel39.Location = new System.Drawing.Point(3, 442);
            this.panel39.Name = "panel39";
            this.panel39.Size = new System.Drawing.Size(1270, 200);
            this.panel39.TabIndex = 2;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel17.Controls.Add(this.trvChkInTemplate);
            this.panel17.Controls.Add(this.label58);
            this.panel17.Controls.Add(this.label59);
            this.panel17.Controls.Add(this.label60);
            this.panel17.Controls.Add(this.label61);
            this.panel17.Controls.Add(this.label62);
            this.panel17.Controls.Add(this.label63);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel17.Location = new System.Drawing.Point(0, 24);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel17.Size = new System.Drawing.Size(1270, 176);
            this.panel17.TabIndex = 1;
            // 
            // trvChkInTemplate
            // 
            this.trvChkInTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvChkInTemplate.CheckBoxes = true;
            this.trvChkInTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvChkInTemplate.ForeColor = System.Drawing.Color.Black;
            this.trvChkInTemplate.HideSelection = false;
            this.trvChkInTemplate.Location = new System.Drawing.Point(4, 7);
            this.trvChkInTemplate.Name = "trvChkInTemplate";
            this.trvChkInTemplate.Size = new System.Drawing.Size(1265, 168);
            this.trvChkInTemplate.TabIndex = 0;
            this.trvChkInTemplate.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvChkInTemplate_AfterCheck);
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.White;
            this.label58.Dock = System.Windows.Forms.DockStyle.Top;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label58.Location = new System.Drawing.Point(4, 4);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(1265, 3);
            this.label58.TabIndex = 221;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.White;
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(1, 4);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(3, 171);
            this.label59.TabIndex = 220;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.Location = new System.Drawing.Point(1, 175);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1268, 1);
            this.label60.TabIndex = 218;
            this.label60.Text = "label2";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Left;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(0, 4);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 172);
            this.label61.TabIndex = 217;
            this.label61.Text = "label4";
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Right;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.Location = new System.Drawing.Point(1269, 4);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(1, 172);
            this.label62.TabIndex = 216;
            this.label62.Text = "label3";
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Top;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(0, 3);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(1270, 1);
            this.label63.TabIndex = 215;
            this.label63.Text = "label1";
            // 
            // panel15
            // 
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(1270, 24);
            this.panel15.TabIndex = 0;
            // 
            // panel16
            // 
            this.panel16.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.btnChkInSelectAllTreeNode);
            this.panel16.Controls.Add(this.btnChkInClearAllTreeNode);
            this.panel16.Controls.Add(this.label29);
            this.panel16.Controls.Add(this.label47);
            this.panel16.Controls.Add(this.label55);
            this.panel16.Controls.Add(this.label56);
            this.panel16.Controls.Add(this.label57);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1270, 24);
            this.panel16.TabIndex = 0;
            // 
            // btnChkInSelectAllTreeNode
            // 
            this.btnChkInSelectAllTreeNode.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAllTreeNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInSelectAllTreeNode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChkInSelectAllTreeNode.FlatAppearance.BorderSize = 0;
            this.btnChkInSelectAllTreeNode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAllTreeNode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChkInSelectAllTreeNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInSelectAllTreeNode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChkInSelectAllTreeNode.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInSelectAllTreeNode.Image")));
            this.btnChkInSelectAllTreeNode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChkInSelectAllTreeNode.Location = new System.Drawing.Point(1211, 1);
            this.btnChkInSelectAllTreeNode.Name = "btnChkInSelectAllTreeNode";
            this.btnChkInSelectAllTreeNode.Size = new System.Drawing.Size(29, 22);
            this.btnChkInSelectAllTreeNode.TabIndex = 1;
            this.btnChkInSelectAllTreeNode.UseVisualStyleBackColor = false;
            this.btnChkInSelectAllTreeNode.Visible = false;
            this.btnChkInSelectAllTreeNode.Click += new System.EventHandler(this.btnChkInSelectAllTreeNode_Click);
            // 
            // btnChkInClearAllTreeNode
            // 
            this.btnChkInClearAllTreeNode.BackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAllTreeNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChkInClearAllTreeNode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnChkInClearAllTreeNode.FlatAppearance.BorderSize = 0;
            this.btnChkInClearAllTreeNode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAllTreeNode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChkInClearAllTreeNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkInClearAllTreeNode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChkInClearAllTreeNode.Image = ((System.Drawing.Image)(resources.GetObject("btnChkInClearAllTreeNode.Image")));
            this.btnChkInClearAllTreeNode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChkInClearAllTreeNode.Location = new System.Drawing.Point(1240, 1);
            this.btnChkInClearAllTreeNode.Name = "btnChkInClearAllTreeNode";
            this.btnChkInClearAllTreeNode.Size = new System.Drawing.Size(29, 22);
            this.btnChkInClearAllTreeNode.TabIndex = 2;
            this.btnChkInClearAllTreeNode.UseVisualStyleBackColor = false;
            this.btnChkInClearAllTreeNode.Click += new System.EventHandler(this.btnChkInClearAllTreeNode_Click);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(1, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1268, 22);
            this.label29.TabIndex = 0;
            this.label29.Text = "  3. Select Word Template(s) to Print ";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Location = new System.Drawing.Point(1, 23);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1268, 1);
            this.label47.TabIndex = 97;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Top;
            this.label55.Location = new System.Drawing.Point(1, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1268, 1);
            this.label55.TabIndex = 99;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(1269, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 24);
            this.label56.TabIndex = 101;
            this.label56.Text = "label4";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Left;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(0, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1, 24);
            this.label57.TabIndex = 100;
            this.label57.Text = "label4";
            // 
            // tbAppLetters
            // 
            this.tbAppLetters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbAppLetters.Controls.Add(this.panel40);
            this.tbAppLetters.Controls.Add(this.panel35);
            this.tbAppLetters.Controls.Add(this.panel31);
            this.tbAppLetters.Location = new System.Drawing.Point(4, 23);
            this.tbAppLetters.Name = "tbAppLetters";
            this.tbAppLetters.Padding = new System.Windows.Forms.Padding(3);
            this.tbAppLetters.Size = new System.Drawing.Size(1276, 645);
            this.tbAppLetters.TabIndex = 1;
            this.tbAppLetters.Text = "Appointment Letters";
            // 
            // panel40
            // 
            this.panel40.Controls.Add(this.panel41);
            this.panel40.Controls.Add(this.panel42);
            this.panel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel40.Location = new System.Drawing.Point(3, 447);
            this.panel40.Name = "panel40";
            this.panel40.Size = new System.Drawing.Size(1270, 195);
            this.panel40.TabIndex = 2;
            // 
            // panel41
            // 
            this.panel41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel41.Controls.Add(this.trvApptLetterPrintTemplate);
            this.panel41.Controls.Add(this.label136);
            this.panel41.Controls.Add(this.label137);
            this.panel41.Controls.Add(this.label138);
            this.panel41.Controls.Add(this.label139);
            this.panel41.Controls.Add(this.label140);
            this.panel41.Controls.Add(this.label141);
            this.panel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel41.Location = new System.Drawing.Point(0, 24);
            this.panel41.Name = "panel41";
            this.panel41.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel41.Size = new System.Drawing.Size(1270, 171);
            this.panel41.TabIndex = 1;
            // 
            // trvApptLetterPrintTemplate
            // 
            this.trvApptLetterPrintTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvApptLetterPrintTemplate.CheckBoxes = true;
            this.trvApptLetterPrintTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvApptLetterPrintTemplate.ForeColor = System.Drawing.Color.Black;
            this.trvApptLetterPrintTemplate.HideSelection = false;
            this.trvApptLetterPrintTemplate.Location = new System.Drawing.Point(4, 7);
            this.trvApptLetterPrintTemplate.Name = "trvApptLetterPrintTemplate";
            this.trvApptLetterPrintTemplate.Size = new System.Drawing.Size(1265, 163);
            this.trvApptLetterPrintTemplate.TabIndex = 0;
            this.trvApptLetterPrintTemplate.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvApptLetterPrintTemplate_AfterCheck);
            // 
            // label136
            // 
            this.label136.BackColor = System.Drawing.Color.White;
            this.label136.Dock = System.Windows.Forms.DockStyle.Top;
            this.label136.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label136.Location = new System.Drawing.Point(4, 4);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(1265, 3);
            this.label136.TabIndex = 221;
            // 
            // label137
            // 
            this.label137.BackColor = System.Drawing.Color.White;
            this.label137.Dock = System.Windows.Forms.DockStyle.Left;
            this.label137.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(1, 4);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(3, 166);
            this.label137.TabIndex = 220;
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label138.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label138.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label138.Location = new System.Drawing.Point(1, 170);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(1268, 1);
            this.label138.TabIndex = 218;
            this.label138.Text = "label2";
            // 
            // label139
            // 
            this.label139.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label139.Dock = System.Windows.Forms.DockStyle.Left;
            this.label139.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(0, 4);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(1, 167);
            this.label139.TabIndex = 217;
            this.label139.Text = "label4";
            // 
            // label140
            // 
            this.label140.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label140.Dock = System.Windows.Forms.DockStyle.Right;
            this.label140.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label140.Location = new System.Drawing.Point(1269, 4);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(1, 167);
            this.label140.TabIndex = 216;
            this.label140.Text = "label3";
            // 
            // label141
            // 
            this.label141.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label141.Dock = System.Windows.Forms.DockStyle.Top;
            this.label141.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(0, 3);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(1270, 1);
            this.label141.TabIndex = 215;
            this.label141.Text = "label1";
            // 
            // panel42
            // 
            this.panel42.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel42.Controls.Add(this.panel43);
            this.panel42.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel42.Location = new System.Drawing.Point(0, 0);
            this.panel42.Name = "panel42";
            this.panel42.Size = new System.Drawing.Size(1270, 24);
            this.panel42.TabIndex = 0;
            // 
            // panel43
            // 
            this.panel43.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel43.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel43.Controls.Add(this.btnApptLetterSelectAllTreeNode);
            this.panel43.Controls.Add(this.btnApptLetterClearAllTreeNode);
            this.panel43.Controls.Add(this.label142);
            this.panel43.Controls.Add(this.label143);
            this.panel43.Controls.Add(this.label144);
            this.panel43.Controls.Add(this.label145);
            this.panel43.Controls.Add(this.label146);
            this.panel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel43.Location = new System.Drawing.Point(0, 0);
            this.panel43.Name = "panel43";
            this.panel43.Size = new System.Drawing.Size(1270, 24);
            this.panel43.TabIndex = 1;
            // 
            // btnApptLetterSelectAllTreeNode
            // 
            this.btnApptLetterSelectAllTreeNode.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAllTreeNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterSelectAllTreeNode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApptLetterSelectAllTreeNode.FlatAppearance.BorderSize = 0;
            this.btnApptLetterSelectAllTreeNode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAllTreeNode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAllTreeNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterSelectAllTreeNode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApptLetterSelectAllTreeNode.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterSelectAllTreeNode.Image")));
            this.btnApptLetterSelectAllTreeNode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnApptLetterSelectAllTreeNode.Location = new System.Drawing.Point(1211, 1);
            this.btnApptLetterSelectAllTreeNode.Name = "btnApptLetterSelectAllTreeNode";
            this.btnApptLetterSelectAllTreeNode.Size = new System.Drawing.Size(29, 22);
            this.btnApptLetterSelectAllTreeNode.TabIndex = 0;
            this.btnApptLetterSelectAllTreeNode.UseVisualStyleBackColor = false;
            this.btnApptLetterSelectAllTreeNode.Visible = false;
            this.btnApptLetterSelectAllTreeNode.Click += new System.EventHandler(this.btnApptLetterSelectAllTreeNode_Click);
            // 
            // btnApptLetterClearAllTreeNode
            // 
            this.btnApptLetterClearAllTreeNode.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAllTreeNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearAllTreeNode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApptLetterClearAllTreeNode.FlatAppearance.BorderSize = 0;
            this.btnApptLetterClearAllTreeNode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAllTreeNode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAllTreeNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearAllTreeNode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApptLetterClearAllTreeNode.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearAllTreeNode.Image")));
            this.btnApptLetterClearAllTreeNode.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnApptLetterClearAllTreeNode.Location = new System.Drawing.Point(1240, 1);
            this.btnApptLetterClearAllTreeNode.Name = "btnApptLetterClearAllTreeNode";
            this.btnApptLetterClearAllTreeNode.Size = new System.Drawing.Size(29, 22);
            this.btnApptLetterClearAllTreeNode.TabIndex = 1;
            this.btnApptLetterClearAllTreeNode.UseVisualStyleBackColor = false;
            this.btnApptLetterClearAllTreeNode.Click += new System.EventHandler(this.btnApptLetterClearAllTreeNode_Click);
            // 
            // label142
            // 
            this.label142.BackColor = System.Drawing.Color.Transparent;
            this.label142.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label142.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label142.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label142.Location = new System.Drawing.Point(1, 1);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(1268, 22);
            this.label142.TabIndex = 0;
            this.label142.Text = "  3. Select Word Template(s) to Print ";
            this.label142.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label143
            // 
            this.label143.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label143.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label143.Location = new System.Drawing.Point(1, 23);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(1268, 1);
            this.label143.TabIndex = 97;
            // 
            // label144
            // 
            this.label144.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label144.Dock = System.Windows.Forms.DockStyle.Top;
            this.label144.Location = new System.Drawing.Point(1, 0);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(1268, 1);
            this.label144.TabIndex = 99;
            // 
            // label145
            // 
            this.label145.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label145.Dock = System.Windows.Forms.DockStyle.Right;
            this.label145.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(1269, 0);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(1, 24);
            this.label145.TabIndex = 101;
            this.label145.Text = "label4";
            // 
            // label146
            // 
            this.label146.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label146.Dock = System.Windows.Forms.DockStyle.Left;
            this.label146.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(0, 0);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(1, 24);
            this.label146.TabIndex = 100;
            this.label146.Text = "label4";
            // 
            // panel35
            // 
            this.panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel35.Controls.Add(this.pnlApptLetterGrid);
            this.panel35.Controls.Add(this.panel37);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel35.Location = new System.Drawing.Point(3, 155);
            this.panel35.Name = "panel35";
            this.panel35.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel35.Size = new System.Drawing.Size(1270, 292);
            this.panel35.TabIndex = 1;
            // 
            // pnlApptLetterGrid
            // 
            this.pnlApptLetterGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlApptLetterGrid.Controls.Add(this.c1ApptLetterPatients);
            this.pnlApptLetterGrid.Controls.Add(this.label127);
            this.pnlApptLetterGrid.Controls.Add(this.label128);
            this.pnlApptLetterGrid.Controls.Add(this.label129);
            this.pnlApptLetterGrid.Controls.Add(this.label130);
            this.pnlApptLetterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlApptLetterGrid.Location = new System.Drawing.Point(0, 27);
            this.pnlApptLetterGrid.Name = "pnlApptLetterGrid";
            this.pnlApptLetterGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlApptLetterGrid.Size = new System.Drawing.Size(1270, 262);
            this.pnlApptLetterGrid.TabIndex = 1;
            // 
            // c1ApptLetterPatients
            // 
            this.c1ApptLetterPatients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ApptLetterPatients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ApptLetterPatients.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1ApptLetterPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ApptLetterPatients.ExtendLastCol = true;
            this.c1ApptLetterPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ApptLetterPatients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ApptLetterPatients.Location = new System.Drawing.Point(1, 4);
            this.c1ApptLetterPatients.Name = "c1ApptLetterPatients";
            this.c1ApptLetterPatients.Rows.Count = 1;
            this.c1ApptLetterPatients.Rows.DefaultSize = 19;
            this.c1ApptLetterPatients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ApptLetterPatients.Size = new System.Drawing.Size(1268, 257);
            this.c1ApptLetterPatients.StyleInfo = resources.GetString("c1ApptLetterPatients.StyleInfo");
            this.c1ApptLetterPatients.TabIndex = 0;
            this.c1ApptLetterPatients.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ApptLetterPatients_AfterEdit);
            this.c1ApptLetterPatients.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ApptLetterPatients_MouseMove);
            // 
            // label127
            // 
            this.label127.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label127.Dock = System.Windows.Forms.DockStyle.Left;
            this.label127.Location = new System.Drawing.Point(0, 4);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(1, 257);
            this.label127.TabIndex = 100;
            // 
            // label128
            // 
            this.label128.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label128.Dock = System.Windows.Forms.DockStyle.Top;
            this.label128.Location = new System.Drawing.Point(0, 3);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(1269, 1);
            this.label128.TabIndex = 102;
            // 
            // label129
            // 
            this.label129.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label129.Dock = System.Windows.Forms.DockStyle.Right;
            this.label129.Location = new System.Drawing.Point(1269, 3);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(1, 258);
            this.label129.TabIndex = 101;
            // 
            // label130
            // 
            this.label130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label130.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label130.Location = new System.Drawing.Point(0, 261);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(1270, 1);
            this.label130.TabIndex = 103;
            // 
            // panel37
            // 
            this.panel37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel37.Controls.Add(this.panel38);
            this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel37.Location = new System.Drawing.Point(0, 3);
            this.panel37.Name = "panel37";
            this.panel37.Size = new System.Drawing.Size(1270, 24);
            this.panel37.TabIndex = 0;
            // 
            // panel38
            // 
            this.panel38.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel38.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel38.Controls.Add(this.btnApptLetterSelectAll);
            this.panel38.Controls.Add(this.btnApptLetterClearAll);
            this.panel38.Controls.Add(this.label131);
            this.panel38.Controls.Add(this.label132);
            this.panel38.Controls.Add(this.label133);
            this.panel38.Controls.Add(this.label134);
            this.panel38.Controls.Add(this.label135);
            this.panel38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel38.Location = new System.Drawing.Point(0, 0);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(1270, 24);
            this.panel38.TabIndex = 1;
            // 
            // btnApptLetterSelectAll
            // 
            this.btnApptLetterSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterSelectAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApptLetterSelectAll.FlatAppearance.BorderSize = 0;
            this.btnApptLetterSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApptLetterSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterSelectAll.Image")));
            this.btnApptLetterSelectAll.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnApptLetterSelectAll.Location = new System.Drawing.Point(1211, 1);
            this.btnApptLetterSelectAll.Name = "btnApptLetterSelectAll";
            this.btnApptLetterSelectAll.Size = new System.Drawing.Size(29, 22);
            this.btnApptLetterSelectAll.TabIndex = 0;
            this.btnApptLetterSelectAll.UseVisualStyleBackColor = false;
            this.btnApptLetterSelectAll.Visible = false;
            this.btnApptLetterSelectAll.Click += new System.EventHandler(this.btnApptLetterSelectAll_Click);
            // 
            // btnApptLetterClearAll
            // 
            this.btnApptLetterClearAll.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApptLetterClearAll.FlatAppearance.BorderSize = 0;
            this.btnApptLetterClearAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApptLetterClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearAll.Image")));
            this.btnApptLetterClearAll.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnApptLetterClearAll.Location = new System.Drawing.Point(1240, 1);
            this.btnApptLetterClearAll.Name = "btnApptLetterClearAll";
            this.btnApptLetterClearAll.Size = new System.Drawing.Size(29, 22);
            this.btnApptLetterClearAll.TabIndex = 1;
            this.btnApptLetterClearAll.UseVisualStyleBackColor = false;
            this.btnApptLetterClearAll.Click += new System.EventHandler(this.btnApptLetterClearAll_Click);
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.Color.Transparent;
            this.label131.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label131.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label131.Location = new System.Drawing.Point(1, 1);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(1268, 22);
            this.label131.TabIndex = 0;
            this.label131.Text = "  2. Confirm Appointments for print";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label132.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label132.Location = new System.Drawing.Point(1, 23);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(1268, 1);
            this.label132.TabIndex = 97;
            // 
            // label133
            // 
            this.label133.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label133.Dock = System.Windows.Forms.DockStyle.Top;
            this.label133.Location = new System.Drawing.Point(1, 0);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(1268, 1);
            this.label133.TabIndex = 99;
            // 
            // label134
            // 
            this.label134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label134.Dock = System.Windows.Forms.DockStyle.Right;
            this.label134.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(1269, 0);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(1, 24);
            this.label134.TabIndex = 101;
            this.label134.Text = "label4";
            // 
            // label135
            // 
            this.label135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label135.Dock = System.Windows.Forms.DockStyle.Left;
            this.label135.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(0, 0);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(1, 24);
            this.label135.TabIndex = 100;
            this.label135.Text = "label4";
            // 
            // panel31
            // 
            this.panel31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel31.Controls.Add(this.panel32);
            this.panel31.Controls.Add(this.panel33);
            this.panel31.Controls.Add(this.button50);
            this.panel31.Controls.Add(this.button51);
            this.panel31.Controls.Add(this.label124);
            this.panel31.Controls.Add(this.comboBox24);
            this.panel31.Controls.Add(this.comboBox25);
            this.panel31.Controls.Add(this.label125);
            this.panel31.Controls.Add(this.button52);
            this.panel31.Controls.Add(this.button53);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel31.Location = new System.Drawing.Point(3, 3);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(1270, 152);
            this.panel31.TabIndex = 0;
            // 
            // panel32
            // 
            this.panel32.Controls.Add(this.dtpApptLetterApptStartTimeTo);
            this.panel32.Controls.Add(this.dtpApptLetterApptStartTimeFrom);
            this.panel32.Controls.Add(this.panel7);
            this.panel32.Controls.Add(this.label104);
            this.panel32.Controls.Add(this.label105);
            this.panel32.Controls.Add(this.dtpApptLetterToDate);
            this.panel32.Controls.Add(this.dtpApptLetterFromDate);
            this.panel32.Controls.Add(this.cmbApptLetterDateRange);
            this.panel32.Controls.Add(this.chkApptLetterIncludeOneAppt);
            this.panel32.Controls.Add(this.label113);
            this.panel32.Controls.Add(this.label108);
            this.panel32.Controls.Add(this.label123);
            this.panel32.Controls.Add(this.label106);
            this.panel32.Controls.Add(this.rbtnApptLetterPlan);
            this.panel32.Controls.Add(this.label107);
            this.panel32.Controls.Add(this.rbtnApptLetterCompany);
            this.panel32.Controls.Add(this.cmbApptLetterProvider);
            this.panel32.Controls.Add(this.btnApptLetterBrowseProvider);
            this.panel32.Controls.Add(this.cmbApptLetterPatient);
            this.panel32.Controls.Add(this.btnApptLetterClearProvider);
            this.panel32.Controls.Add(this.label109);
            this.panel32.Controls.Add(this.cmbApptLetterInsuranceCompany);
            this.panel32.Controls.Add(this.btnApptLetterBrowsePatient);
            this.panel32.Controls.Add(this.label110);
            this.panel32.Controls.Add(this.btnApptLetterClearPatient);
            this.panel32.Controls.Add(this.btnApptLetterBrowseInsurance);
            this.panel32.Controls.Add(this.cmbApptLetterApptTypeType);
            this.panel32.Controls.Add(this.btnApptLetterClearInsurance);
            this.panel32.Controls.Add(this.btnApptLetterClearApptTypeType);
            this.panel32.Controls.Add(this.btnApptLetterBrowseApptTypeType);
            this.panel32.Controls.Add(this.label126);
            this.panel32.Controls.Add(this.label111);
            this.panel32.Controls.Add(this.label112);
            this.panel32.Controls.Add(this.cmbApptLetterApptType);
            this.panel32.Controls.Add(this.label114);
            this.panel32.Controls.Add(this.btnApptLetterClearApptType);
            this.panel32.Controls.Add(this.btnApptLetterClearLocation);
            this.panel32.Controls.Add(this.btnApptLetterBrowseApptType);
            this.panel32.Controls.Add(this.btnApptLetterBrowseLocation);
            this.panel32.Controls.Add(this.cmbApptLetterResource);
            this.panel32.Controls.Add(this.label115);
            this.panel32.Controls.Add(this.label116);
            this.panel32.Controls.Add(this.cmbApptLetterLocation);
            this.panel32.Controls.Add(this.btnApptLetterBrowseResource);
            this.panel32.Controls.Add(this.btnApptLetterClearResource);
            this.panel32.Controls.Add(this.cmbApptLetterInsurancePlan);
            this.panel32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel32.Location = new System.Drawing.Point(0, 24);
            this.panel32.Name = "panel32";
            this.panel32.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel32.Size = new System.Drawing.Size(1270, 128);
            this.panel32.TabIndex = 263;
            // 
            // dtpApptLetterApptStartTimeTo
            // 
            this.dtpApptLetterApptStartTimeTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApptLetterApptStartTimeTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApptLetterApptStartTimeTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApptLetterApptStartTimeTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApptLetterApptStartTimeTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApptLetterApptStartTimeTo.CustomFormat = "hh:mm tt";
            this.dtpApptLetterApptStartTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApptLetterApptStartTimeTo.Location = new System.Drawing.Point(1123, 38);
            this.dtpApptLetterApptStartTimeTo.Name = "dtpApptLetterApptStartTimeTo";
            this.dtpApptLetterApptStartTimeTo.ShowCheckBox = true;
            this.dtpApptLetterApptStartTimeTo.ShowUpDown = true;
            this.dtpApptLetterApptStartTimeTo.Size = new System.Drawing.Size(94, 22);
            this.dtpApptLetterApptStartTimeTo.TabIndex = 29;
            // 
            // dtpApptLetterApptStartTimeFrom
            // 
            this.dtpApptLetterApptStartTimeFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApptLetterApptStartTimeFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApptLetterApptStartTimeFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApptLetterApptStartTimeFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApptLetterApptStartTimeFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApptLetterApptStartTimeFrom.CustomFormat = "hh:mm tt";
            this.dtpApptLetterApptStartTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApptLetterApptStartTimeFrom.Location = new System.Drawing.Point(1123, 9);
            this.dtpApptLetterApptStartTimeFrom.Name = "dtpApptLetterApptStartTimeFrom";
            this.dtpApptLetterApptStartTimeFrom.ShowCheckBox = true;
            this.dtpApptLetterApptStartTimeFrom.ShowUpDown = true;
            this.dtpApptLetterApptStartTimeFrom.Size = new System.Drawing.Size(94, 22);
            this.dtpApptLetterApptStartTimeFrom.TabIndex = 28;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rbtnApptLetterCancelled);
            this.panel7.Controls.Add(this.rbtnApptLetterOpen);
            this.panel7.Controls.Add(this.rbtnApptLetterNoShow);
            this.panel7.Location = new System.Drawing.Point(273, 67);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(257, 27);
            this.panel7.TabIndex = 10;
            // 
            // rbtnApptLetterCancelled
            // 
            this.rbtnApptLetterCancelled.AutoSize = true;
            this.rbtnApptLetterCancelled.Location = new System.Drawing.Point(76, 3);
            this.rbtnApptLetterCancelled.Name = "rbtnApptLetterCancelled";
            this.rbtnApptLetterCancelled.Size = new System.Drawing.Size(76, 18);
            this.rbtnApptLetterCancelled.TabIndex = 1;
            this.rbtnApptLetterCancelled.Text = "Cancelled";
            this.rbtnApptLetterCancelled.UseVisualStyleBackColor = true;
            this.rbtnApptLetterCancelled.CheckedChanged += new System.EventHandler(this.rbtnApptLetterCancelled_CheckedChanged);
            // 
            // rbtnApptLetterOpen
            // 
            this.rbtnApptLetterOpen.AutoSize = true;
            this.rbtnApptLetterOpen.Checked = true;
            this.rbtnApptLetterOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnApptLetterOpen.Location = new System.Drawing.Point(4, 3);
            this.rbtnApptLetterOpen.Name = "rbtnApptLetterOpen";
            this.rbtnApptLetterOpen.Size = new System.Drawing.Size(57, 18);
            this.rbtnApptLetterOpen.TabIndex = 0;
            this.rbtnApptLetterOpen.TabStop = true;
            this.rbtnApptLetterOpen.Text = "Open";
            this.rbtnApptLetterOpen.UseVisualStyleBackColor = true;
            this.rbtnApptLetterOpen.CheckedChanged += new System.EventHandler(this.rbtnApptLetterOpen_CheckedChanged);
            // 
            // rbtnApptLetterNoShow
            // 
            this.rbtnApptLetterNoShow.AutoSize = true;
            this.rbtnApptLetterNoShow.Location = new System.Drawing.Point(166, 3);
            this.rbtnApptLetterNoShow.Name = "rbtnApptLetterNoShow";
            this.rbtnApptLetterNoShow.Size = new System.Drawing.Size(75, 18);
            this.rbtnApptLetterNoShow.TabIndex = 2;
            this.rbtnApptLetterNoShow.Text = "No-Show";
            this.rbtnApptLetterNoShow.UseVisualStyleBackColor = true;
            this.rbtnApptLetterNoShow.CheckedChanged += new System.EventHandler(this.rbtnApptLetterNoShow_CheckedChanged);
            // 
            // label104
            // 
            this.label104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label104.Dock = System.Windows.Forms.DockStyle.Top;
            this.label104.Location = new System.Drawing.Point(1, 3);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(1268, 1);
            this.label104.TabIndex = 262;
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label105.Location = new System.Drawing.Point(1, 127);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(1268, 1);
            this.label105.TabIndex = 220;
            this.label105.Text = "label2";
            // 
            // dtpApptLetterToDate
            // 
            this.dtpApptLetterToDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApptLetterToDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApptLetterToDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApptLetterToDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApptLetterToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApptLetterToDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApptLetterToDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpApptLetterToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApptLetterToDate.Location = new System.Drawing.Point(89, 67);
            this.dtpApptLetterToDate.Name = "dtpApptLetterToDate";
            this.dtpApptLetterToDate.Size = new System.Drawing.Size(111, 22);
            this.dtpApptLetterToDate.TabIndex = 3;
            // 
            // dtpApptLetterFromDate
            // 
            this.dtpApptLetterFromDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApptLetterFromDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApptLetterFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApptLetterFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApptLetterFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApptLetterFromDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApptLetterFromDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpApptLetterFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApptLetterFromDate.Location = new System.Drawing.Point(89, 38);
            this.dtpApptLetterFromDate.Name = "dtpApptLetterFromDate";
            this.dtpApptLetterFromDate.Size = new System.Drawing.Size(111, 22);
            this.dtpApptLetterFromDate.TabIndex = 2;
            // 
            // cmbApptLetterDateRange
            // 
            this.cmbApptLetterDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterDateRange.FormattingEnabled = true;
            this.cmbApptLetterDateRange.Location = new System.Drawing.Point(89, 9);
            this.cmbApptLetterDateRange.Name = "cmbApptLetterDateRange";
            this.cmbApptLetterDateRange.Size = new System.Drawing.Size(111, 22);
            this.cmbApptLetterDateRange.TabIndex = 1;
            this.cmbApptLetterDateRange.SelectedIndexChanged += new System.EventHandler(this.cmbApptLetterDateRange_SelectedIndexChanged);
            // 
            // chkApptLetterIncludeOneAppt
            // 
            this.chkApptLetterIncludeOneAppt.AutoSize = true;
            this.chkApptLetterIncludeOneAppt.Location = new System.Drawing.Point(542, 98);
            this.chkApptLetterIncludeOneAppt.Name = "chkApptLetterIncludeOneAppt";
            this.chkApptLetterIncludeOneAppt.Size = new System.Drawing.Size(258, 18);
            this.chkApptLetterIncludeOneAppt.TabIndex = 30;
            this.chkApptLetterIncludeOneAppt.Text = "Include only one Appointment per Patient";
            this.chkApptLetterIncludeOneAppt.UseVisualStyleBackColor = true;
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(27, 71);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(60, 14);
            this.label113.TabIndex = 212;
            this.label113.Text = "To Date :";
            this.label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(15, 42);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(72, 14);
            this.label108.TabIndex = 212;
            this.label108.Text = "From Date :";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(8, 13);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(79, 14);
            this.label123.TabIndex = 212;
            this.label123.Text = "Date Range :";
            this.label123.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label106
            // 
            this.label106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label106.Dock = System.Windows.Forms.DockStyle.Right;
            this.label106.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label106.Location = new System.Drawing.Point(1269, 3);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(1, 125);
            this.label106.TabIndex = 219;
            this.label106.Text = "label3";
            // 
            // rbtnApptLetterPlan
            // 
            this.rbtnApptLetterPlan.AutoSize = true;
            this.rbtnApptLetterPlan.Location = new System.Drawing.Point(921, 38);
            this.rbtnApptLetterPlan.Name = "rbtnApptLetterPlan";
            this.rbtnApptLetterPlan.Size = new System.Drawing.Size(47, 18);
            this.rbtnApptLetterPlan.TabIndex = 24;
            this.rbtnApptLetterPlan.Text = "Plan";
            this.rbtnApptLetterPlan.UseVisualStyleBackColor = true;
            this.rbtnApptLetterPlan.CheckedChanged += new System.EventHandler(this.rbtnApptLetterPlan_CheckedChanged);
            // 
            // label107
            // 
            this.label107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label107.Dock = System.Windows.Forms.DockStyle.Left;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(0, 3);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(1, 125);
            this.label107.TabIndex = 218;
            this.label107.Text = "label4";
            // 
            // rbtnApptLetterCompany
            // 
            this.rbtnApptLetterCompany.AutoSize = true;
            this.rbtnApptLetterCompany.Checked = true;
            this.rbtnApptLetterCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnApptLetterCompany.Location = new System.Drawing.Point(818, 38);
            this.rbtnApptLetterCompany.Name = "rbtnApptLetterCompany";
            this.rbtnApptLetterCompany.Size = new System.Drawing.Size(82, 18);
            this.rbtnApptLetterCompany.TabIndex = 23;
            this.rbtnApptLetterCompany.TabStop = true;
            this.rbtnApptLetterCompany.Text = "Company";
            this.rbtnApptLetterCompany.UseVisualStyleBackColor = true;
            this.rbtnApptLetterCompany.CheckedChanged += new System.EventHandler(this.rbtnApptLetterCompany_CheckedChanged);
            // 
            // cmbApptLetterProvider
            // 
            this.cmbApptLetterProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterProvider.FormattingEnabled = true;
            this.cmbApptLetterProvider.Location = new System.Drawing.Point(275, 38);
            this.cmbApptLetterProvider.Name = "cmbApptLetterProvider";
            this.cmbApptLetterProvider.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterProvider.TabIndex = 7;
            // 
            // btnApptLetterBrowseProvider
            // 
            this.btnApptLetterBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseProvider.BackgroundImage")));
            this.btnApptLetterBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseProvider.Image")));
            this.btnApptLetterBrowseProvider.Location = new System.Drawing.Point(410, 38);
            this.btnApptLetterBrowseProvider.Name = "btnApptLetterBrowseProvider";
            this.btnApptLetterBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseProvider.TabIndex = 8;
            this.btnApptLetterBrowseProvider.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseProvider.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbApptLetterPatient
            // 
            this.cmbApptLetterPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterPatient.FormattingEnabled = true;
            this.cmbApptLetterPatient.Location = new System.Drawing.Point(813, 9);
            this.cmbApptLetterPatient.Name = "cmbApptLetterPatient";
            this.cmbApptLetterPatient.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterPatient.TabIndex = 20;
            // 
            // btnApptLetterClearProvider
            // 
            this.btnApptLetterClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearProvider.BackgroundImage")));
            this.btnApptLetterClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearProvider.Image")));
            this.btnApptLetterClearProvider.Location = new System.Drawing.Point(435, 38);
            this.btnApptLetterClearProvider.Name = "btnApptLetterClearProvider";
            this.btnApptLetterClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearProvider.TabIndex = 9;
            this.btnApptLetterClearProvider.UseVisualStyleBackColor = false;
            this.btnApptLetterClearProvider.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(756, 13);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(54, 14);
            this.label109.TabIndex = 256;
            this.label109.Text = "Patient :";
            // 
            // cmbApptLetterInsuranceCompany
            // 
            this.cmbApptLetterInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterInsuranceCompany.FormattingEnabled = true;
            this.cmbApptLetterInsuranceCompany.Location = new System.Drawing.Point(813, 67);
            this.cmbApptLetterInsuranceCompany.Name = "cmbApptLetterInsuranceCompany";
            this.cmbApptLetterInsuranceCompany.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterInsuranceCompany.TabIndex = 25;
            // 
            // btnApptLetterBrowsePatient
            // 
            this.btnApptLetterBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowsePatient.BackgroundImage")));
            this.btnApptLetterBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowsePatient.Image")));
            this.btnApptLetterBrowsePatient.Location = new System.Drawing.Point(949, 9);
            this.btnApptLetterBrowsePatient.Name = "btnApptLetterBrowsePatient";
            this.btnApptLetterBrowsePatient.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowsePatient.TabIndex = 21;
            this.btnApptLetterBrowsePatient.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowsePatient.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(736, 71);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(74, 14);
            this.label110.TabIndex = 224;
            this.label110.Text = "Patient Ins :";
            // 
            // btnApptLetterClearPatient
            // 
            this.btnApptLetterClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearPatient.BackgroundImage")));
            this.btnApptLetterClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearPatient.Image")));
            this.btnApptLetterClearPatient.Location = new System.Drawing.Point(974, 9);
            this.btnApptLetterClearPatient.Name = "btnApptLetterClearPatient";
            this.btnApptLetterClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearPatient.TabIndex = 22;
            this.btnApptLetterClearPatient.UseVisualStyleBackColor = false;
            this.btnApptLetterClearPatient.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnApptLetterBrowseInsurance
            // 
            this.btnApptLetterBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseInsurance.BackgroundImage")));
            this.btnApptLetterBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseInsurance.Image")));
            this.btnApptLetterBrowseInsurance.Location = new System.Drawing.Point(949, 67);
            this.btnApptLetterBrowseInsurance.Name = "btnApptLetterBrowseInsurance";
            this.btnApptLetterBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseInsurance.TabIndex = 26;
            this.btnApptLetterBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseInsurance.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbApptLetterApptTypeType
            // 
            this.cmbApptLetterApptTypeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterApptTypeType.FormattingEnabled = true;
            this.cmbApptLetterApptTypeType.Location = new System.Drawing.Point(542, 38);
            this.cmbApptLetterApptTypeType.Name = "cmbApptLetterApptTypeType";
            this.cmbApptLetterApptTypeType.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterApptTypeType.TabIndex = 14;
            // 
            // btnApptLetterClearInsurance
            // 
            this.btnApptLetterClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearInsurance.BackgroundImage")));
            this.btnApptLetterClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearInsurance.Image")));
            this.btnApptLetterClearInsurance.Location = new System.Drawing.Point(974, 67);
            this.btnApptLetterClearInsurance.Name = "btnApptLetterClearInsurance";
            this.btnApptLetterClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearInsurance.TabIndex = 27;
            this.btnApptLetterClearInsurance.UseVisualStyleBackColor = false;
            this.btnApptLetterClearInsurance.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnApptLetterClearApptTypeType
            // 
            this.btnApptLetterClearApptTypeType.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearApptTypeType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearApptTypeType.BackgroundImage")));
            this.btnApptLetterClearApptTypeType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearApptTypeType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearApptTypeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearApptTypeType.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearApptTypeType.Image")));
            this.btnApptLetterClearApptTypeType.Location = new System.Drawing.Point(703, 38);
            this.btnApptLetterClearApptTypeType.Name = "btnApptLetterClearApptTypeType";
            this.btnApptLetterClearApptTypeType.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearApptTypeType.TabIndex = 16;
            this.btnApptLetterClearApptTypeType.UseVisualStyleBackColor = false;
            this.btnApptLetterClearApptTypeType.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnApptLetterBrowseApptTypeType
            // 
            this.btnApptLetterBrowseApptTypeType.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseApptTypeType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseApptTypeType.BackgroundImage")));
            this.btnApptLetterBrowseApptTypeType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseApptTypeType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseApptTypeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseApptTypeType.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseApptTypeType.Image")));
            this.btnApptLetterBrowseApptTypeType.Location = new System.Drawing.Point(678, 38);
            this.btnApptLetterBrowseApptTypeType.Name = "btnApptLetterBrowseApptTypeType";
            this.btnApptLetterBrowseApptTypeType.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseApptTypeType.TabIndex = 15;
            this.btnApptLetterBrowseApptTypeType.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseApptTypeType.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(223, 71);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(50, 14);
            this.label126.TabIndex = 0;
            this.label126.Text = "Status :";
            this.label126.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(214, 42);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(59, 14);
            this.label111.TabIndex = 0;
            this.label111.Text = "Provider :";
            this.label111.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(466, 42);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(74, 14);
            this.label112.TabIndex = 246;
            this.label112.Text = "Appt Type :";
            this.label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApptLetterApptType
            // 
            this.cmbApptLetterApptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterApptType.FormattingEnabled = true;
            this.cmbApptLetterApptType.Location = new System.Drawing.Point(542, 67);
            this.cmbApptLetterApptType.Name = "cmbApptLetterApptType";
            this.cmbApptLetterApptType.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterApptType.TabIndex = 17;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(1010, 13);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(109, 14);
            this.label114.TabIndex = 232;
            this.label114.Text = "Appt Start Times :";
            // 
            // btnApptLetterClearApptType
            // 
            this.btnApptLetterClearApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearApptType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearApptType.BackgroundImage")));
            this.btnApptLetterClearApptType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearApptType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearApptType.Image")));
            this.btnApptLetterClearApptType.Location = new System.Drawing.Point(703, 67);
            this.btnApptLetterClearApptType.Name = "btnApptLetterClearApptType";
            this.btnApptLetterClearApptType.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearApptType.TabIndex = 19;
            this.btnApptLetterClearApptType.UseVisualStyleBackColor = false;
            this.btnApptLetterClearApptType.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnApptLetterClearLocation
            // 
            this.btnApptLetterClearLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearLocation.BackgroundImage")));
            this.btnApptLetterClearLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearLocation.Image")));
            this.btnApptLetterClearLocation.Location = new System.Drawing.Point(435, 9);
            this.btnApptLetterClearLocation.Name = "btnApptLetterClearLocation";
            this.btnApptLetterClearLocation.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearLocation.TabIndex = 6;
            this.btnApptLetterClearLocation.UseVisualStyleBackColor = false;
            this.btnApptLetterClearLocation.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btnApptLetterBrowseApptType
            // 
            this.btnApptLetterBrowseApptType.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseApptType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseApptType.BackgroundImage")));
            this.btnApptLetterBrowseApptType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseApptType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseApptType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseApptType.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseApptType.Image")));
            this.btnApptLetterBrowseApptType.Location = new System.Drawing.Point(678, 67);
            this.btnApptLetterBrowseApptType.Name = "btnApptLetterBrowseApptType";
            this.btnApptLetterBrowseApptType.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseApptType.TabIndex = 18;
            this.btnApptLetterBrowseApptType.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseApptType.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btnApptLetterBrowseLocation
            // 
            this.btnApptLetterBrowseLocation.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseLocation.BackgroundImage")));
            this.btnApptLetterBrowseLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseLocation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseLocation.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseLocation.Image")));
            this.btnApptLetterBrowseLocation.Location = new System.Drawing.Point(410, 9);
            this.btnApptLetterBrowseLocation.Name = "btnApptLetterBrowseLocation";
            this.btnApptLetterBrowseLocation.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseLocation.TabIndex = 5;
            this.btnApptLetterBrowseLocation.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseLocation.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // cmbApptLetterResource
            // 
            this.cmbApptLetterResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterResource.FormattingEnabled = true;
            this.cmbApptLetterResource.Location = new System.Drawing.Point(542, 9);
            this.cmbApptLetterResource.Name = "cmbApptLetterResource";
            this.cmbApptLetterResource.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterResource.TabIndex = 11;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(207, 13);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(66, 14);
            this.label115.TabIndex = 241;
            this.label115.Text = "Locations :";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(470, 13);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(70, 14);
            this.label116.TabIndex = 245;
            this.label116.Text = "Resources :";
            // 
            // cmbApptLetterLocation
            // 
            this.cmbApptLetterLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterLocation.FormattingEnabled = true;
            this.cmbApptLetterLocation.Location = new System.Drawing.Point(275, 9);
            this.cmbApptLetterLocation.Name = "cmbApptLetterLocation";
            this.cmbApptLetterLocation.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterLocation.TabIndex = 4;
            // 
            // btnApptLetterBrowseResource
            // 
            this.btnApptLetterBrowseResource.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterBrowseResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseResource.BackgroundImage")));
            this.btnApptLetterBrowseResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterBrowseResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterBrowseResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterBrowseResource.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterBrowseResource.Image")));
            this.btnApptLetterBrowseResource.Location = new System.Drawing.Point(678, 9);
            this.btnApptLetterBrowseResource.Name = "btnApptLetterBrowseResource";
            this.btnApptLetterBrowseResource.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterBrowseResource.TabIndex = 12;
            this.btnApptLetterBrowseResource.UseVisualStyleBackColor = false;
            this.btnApptLetterBrowseResource.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btnApptLetterClearResource
            // 
            this.btnApptLetterClearResource.BackColor = System.Drawing.Color.Transparent;
            this.btnApptLetterClearResource.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearResource.BackgroundImage")));
            this.btnApptLetterClearResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApptLetterClearResource.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApptLetterClearResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApptLetterClearResource.Image = ((System.Drawing.Image)(resources.GetObject("btnApptLetterClearResource.Image")));
            this.btnApptLetterClearResource.Location = new System.Drawing.Point(703, 9);
            this.btnApptLetterClearResource.Name = "btnApptLetterClearResource";
            this.btnApptLetterClearResource.Size = new System.Drawing.Size(22, 22);
            this.btnApptLetterClearResource.TabIndex = 13;
            this.btnApptLetterClearResource.UseVisualStyleBackColor = false;
            this.btnApptLetterClearResource.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // cmbApptLetterInsurancePlan
            // 
            this.cmbApptLetterInsurancePlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApptLetterInsurancePlan.FormattingEnabled = true;
            this.cmbApptLetterInsurancePlan.Location = new System.Drawing.Point(813, 67);
            this.cmbApptLetterInsurancePlan.Name = "cmbApptLetterInsurancePlan";
            this.cmbApptLetterInsurancePlan.Size = new System.Drawing.Size(132, 22);
            this.cmbApptLetterInsurancePlan.TabIndex = 264;
            this.cmbApptLetterInsurancePlan.Visible = false;
            // 
            // panel33
            // 
            this.panel33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel33.Controls.Add(this.panel34);
            this.panel33.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel33.Location = new System.Drawing.Point(0, 0);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(1270, 24);
            this.panel33.TabIndex = 262;
            this.panel33.TabStop = true;
            // 
            // panel34
            // 
            this.panel34.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel34.Controls.Add(this.label117);
            this.panel34.Controls.Add(this.label118);
            this.panel34.Controls.Add(this.label119);
            this.panel34.Controls.Add(this.label121);
            this.panel34.Controls.Add(this.label122);
            this.panel34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel34.Location = new System.Drawing.Point(0, 0);
            this.panel34.Name = "panel34";
            this.panel34.Size = new System.Drawing.Size(1270, 24);
            this.panel34.TabIndex = 1;
            // 
            // label117
            // 
            this.label117.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label117.Dock = System.Windows.Forms.DockStyle.Right;
            this.label117.Location = new System.Drawing.Point(1269, 1);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(1, 22);
            this.label117.TabIndex = 98;
            // 
            // label118
            // 
            this.label118.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label118.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label118.Location = new System.Drawing.Point(1, 23);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(1269, 1);
            this.label118.TabIndex = 97;
            // 
            // label119
            // 
            this.label119.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label119.Dock = System.Windows.Forms.DockStyle.Top;
            this.label119.Location = new System.Drawing.Point(1, 0);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(1269, 1);
            this.label119.TabIndex = 99;
            // 
            // label121
            // 
            this.label121.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label121.Dock = System.Windows.Forms.DockStyle.Left;
            this.label121.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(0, 0);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(1, 24);
            this.label121.TabIndex = 100;
            this.label121.Text = "label4";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.BackColor = System.Drawing.Color.Transparent;
            this.label122.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(5, 5);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(456, 14);
            this.label122.TabIndex = 233;
            this.label122.Text = "1. Specify Appointment filters and options, then select Generate Batch : ";
            this.label122.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button50
            // 
            this.button50.BackColor = System.Drawing.Color.Transparent;
            this.button50.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button50.BackgroundImage")));
            this.button50.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button50.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button50.Image = ((System.Drawing.Image)(resources.GetObject("button50.Image")));
            this.button50.Location = new System.Drawing.Point(502, 157);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(23, 23);
            this.button50.TabIndex = 8;
            this.button50.UseVisualStyleBackColor = false;
            // 
            // button51
            // 
            this.button51.BackColor = System.Drawing.Color.Transparent;
            this.button51.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button51.BackgroundImage")));
            this.button51.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button51.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button51.Image = ((System.Drawing.Image)(resources.GetObject("button51.Image")));
            this.button51.Location = new System.Drawing.Point(473, 157);
            this.button51.Name = "button51";
            this.button51.Size = new System.Drawing.Size(23, 23);
            this.button51.TabIndex = 7;
            this.button51.UseVisualStyleBackColor = false;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(272, 158);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(37, 14);
            this.label124.TabIndex = 228;
            this.label124.Text = "CPT :";
            // 
            // comboBox24
            // 
            this.comboBox24.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox24.ForeColor = System.Drawing.Color.Black;
            this.comboBox24.FormattingEnabled = true;
            this.comboBox24.Location = new System.Drawing.Point(315, 157);
            this.comboBox24.Name = "comboBox24";
            this.comboBox24.Size = new System.Drawing.Size(149, 22);
            this.comboBox24.TabIndex = 6;
            // 
            // comboBox25
            // 
            this.comboBox25.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox25.FormattingEnabled = true;
            this.comboBox25.Location = new System.Drawing.Point(315, 188);
            this.comboBox25.Name = "comboBox25";
            this.comboBox25.Size = new System.Drawing.Size(149, 22);
            this.comboBox25.TabIndex = 10;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(213, 189);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(96, 14);
            this.label125.TabIndex = 220;
            this.label125.Text = "Diagnosis Code :";
            // 
            // button52
            // 
            this.button52.BackColor = System.Drawing.Color.Transparent;
            this.button52.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button52.BackgroundImage")));
            this.button52.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button52.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button52.Image = ((System.Drawing.Image)(resources.GetObject("button52.Image")));
            this.button52.Location = new System.Drawing.Point(473, 188);
            this.button52.Name = "button52";
            this.button52.Size = new System.Drawing.Size(23, 23);
            this.button52.TabIndex = 11;
            this.button52.UseVisualStyleBackColor = false;
            // 
            // button53
            // 
            this.button53.BackColor = System.Drawing.Color.Transparent;
            this.button53.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button53.BackgroundImage")));
            this.button53.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button53.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button53.Image = ((System.Drawing.Image)(resources.GetObject("button53.Image")));
            this.button53.Location = new System.Drawing.Point(502, 188);
            this.button53.Name = "button53";
            this.button53.Size = new System.Drawing.Size(23, 23);
            this.button53.TabIndex = 12;
            this.button53.UseVisualStyleBackColor = false;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel18.Controls.Add(this.treeView2);
            this.panel18.Controls.Add(this.label50);
            this.panel18.Controls.Add(this.label51);
            this.panel18.Controls.Add(this.label64);
            this.panel18.Controls.Add(this.label65);
            this.panel18.Controls.Add(this.label66);
            this.panel18.Controls.Add(this.label67);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel18.Location = new System.Drawing.Point(3, 406);
            this.panel18.Name = "panel18";
            this.panel18.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel18.Size = new System.Drawing.Size(1415, 236);
            this.panel18.TabIndex = 212;
            // 
            // treeView2
            // 
            this.treeView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView2.CheckBoxes = true;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.ForeColor = System.Drawing.Color.Black;
            this.treeView2.HideSelection = false;
            this.treeView2.LineColor = System.Drawing.Color.Empty;
            this.treeView2.Location = new System.Drawing.Point(4, 7);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(1410, 228);
            this.treeView2.TabIndex = 0;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.White;
            this.label50.Dock = System.Windows.Forms.DockStyle.Top;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label50.Location = new System.Drawing.Point(4, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1410, 3);
            this.label50.TabIndex = 221;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.White;
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(1, 4);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(3, 231);
            this.label51.TabIndex = 220;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label64.Location = new System.Drawing.Point(1, 235);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(1413, 1);
            this.label64.TabIndex = 218;
            this.label64.Text = "label2";
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Left;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(0, 4);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1, 232);
            this.label65.TabIndex = 217;
            this.label65.Text = "label4";
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Right;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label66.Location = new System.Drawing.Point(1414, 4);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(1, 232);
            this.label66.TabIndex = 216;
            this.label66.Text = "label3";
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Top;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(0, 3);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(1415, 1);
            this.label67.TabIndex = 215;
            this.label67.Text = "label1";
            // 
            // panel19
            // 
            this.panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel19.Controls.Add(this.panel20);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel19.Location = new System.Drawing.Point(3, 382);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(1415, 24);
            this.panel19.TabIndex = 211;
            // 
            // panel20
            // 
            this.panel20.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel20.Controls.Add(this.button14);
            this.panel20.Controls.Add(this.button15);
            this.panel20.Controls.Add(this.label68);
            this.panel20.Controls.Add(this.label69);
            this.panel20.Controls.Add(this.label70);
            this.panel20.Controls.Add(this.label71);
            this.panel20.Controls.Add(this.label72);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(1415, 24);
            this.panel20.TabIndex = 1;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Transparent;
            this.button14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button14.Dock = System.Windows.Forms.DockStyle.Right;
            this.button14.FlatAppearance.BorderSize = 0;
            this.button14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button14.Image = ((System.Drawing.Image)(resources.GetObject("button14.Image")));
            this.button14.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button14.Location = new System.Drawing.Point(1356, 1);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(29, 22);
            this.button14.TabIndex = 0;
            this.button14.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Transparent;
            this.button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button15.Dock = System.Windows.Forms.DockStyle.Right;
            this.button15.FlatAppearance.BorderSize = 0;
            this.button15.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.Image = ((System.Drawing.Image)(resources.GetObject("button15.Image")));
            this.button15.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button15.Location = new System.Drawing.Point(1385, 1);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(29, 22);
            this.button15.TabIndex = 102;
            this.button15.UseVisualStyleBackColor = false;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Location = new System.Drawing.Point(1, 1);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(1413, 22);
            this.label68.TabIndex = 0;
            this.label68.Text = "  3. Select word Template(s) to Print ";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label69.Location = new System.Drawing.Point(1, 23);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1413, 1);
            this.label69.TabIndex = 97;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Top;
            this.label70.Location = new System.Drawing.Point(1, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1413, 1);
            this.label70.TabIndex = 99;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Right;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(1414, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(1, 24);
            this.label71.TabIndex = 101;
            this.label71.Text = "label4";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Left;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(0, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(1, 24);
            this.label72.TabIndex = 100;
            this.label72.Text = "label4";
            // 
            // panel21
            // 
            this.panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel21.Controls.Add(this.panel23);
            this.panel21.Controls.Add(this.panel24);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel21.Location = new System.Drawing.Point(3, 150);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel21.Size = new System.Drawing.Size(1415, 232);
            this.panel21.TabIndex = 210;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.Transparent;
            this.panel23.Controls.Add(this.c1FlexGrid1);
            this.panel23.Controls.Add(this.label73);
            this.panel23.Controls.Add(this.label74);
            this.panel23.Controls.Add(this.label75);
            this.panel23.Controls.Add(this.label76);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(0, 27);
            this.panel23.Name = "panel23";
            this.panel23.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel23.Size = new System.Drawing.Size(1415, 202);
            this.panel23.TabIndex = 92;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGrid1.ColumnInfo = "3,1,0,0,0,95,Columns:";
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.ExtendLastCol = true;
            this.c1FlexGrid1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexGrid1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexGrid1.Location = new System.Drawing.Point(1, 4);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 5;
            this.c1FlexGrid1.Rows.DefaultSize = 19;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(1413, 197);
            this.c1FlexGrid1.StyleInfo = resources.GetString("c1FlexGrid1.StyleInfo");
            this.c1FlexGrid1.TabIndex = 104;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Left;
            this.label73.Location = new System.Drawing.Point(0, 4);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1, 197);
            this.label73.TabIndex = 100;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Dock = System.Windows.Forms.DockStyle.Top;
            this.label74.Location = new System.Drawing.Point(0, 3);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(1414, 1);
            this.label74.TabIndex = 102;
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Right;
            this.label75.Location = new System.Drawing.Point(1414, 3);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(1, 198);
            this.label75.TabIndex = 101;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label76.Location = new System.Drawing.Point(0, 201);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(1415, 1);
            this.label76.TabIndex = 103;
            // 
            // panel24
            // 
            this.panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel24.Controls.Add(this.panel25);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel24.Location = new System.Drawing.Point(0, 3);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(1415, 24);
            this.panel24.TabIndex = 0;
            // 
            // panel25
            // 
            this.panel25.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel25.Controls.Add(this.button16);
            this.panel25.Controls.Add(this.button17);
            this.panel25.Controls.Add(this.label77);
            this.panel25.Controls.Add(this.label78);
            this.panel25.Controls.Add(this.label79);
            this.panel25.Controls.Add(this.label80);
            this.panel25.Controls.Add(this.label81);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel25.Location = new System.Drawing.Point(0, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(1415, 24);
            this.panel25.TabIndex = 1;
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Transparent;
            this.button16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button16.Dock = System.Windows.Forms.DockStyle.Right;
            this.button16.FlatAppearance.BorderSize = 0;
            this.button16.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.Image = ((System.Drawing.Image)(resources.GetObject("button16.Image")));
            this.button16.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button16.Location = new System.Drawing.Point(1356, 1);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(29, 22);
            this.button16.TabIndex = 0;
            this.button16.UseVisualStyleBackColor = false;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button17.Dock = System.Windows.Forms.DockStyle.Right;
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button17.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.Image = ((System.Drawing.Image)(resources.GetObject("button17.Image")));
            this.button17.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button17.Location = new System.Drawing.Point(1385, 1);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(29, 22);
            this.button17.TabIndex = 102;
            this.button17.UseVisualStyleBackColor = false;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Location = new System.Drawing.Point(1, 1);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(1413, 22);
            this.label77.TabIndex = 0;
            this.label77.Text = "  2. Confirm Appointments for print";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label78.Location = new System.Drawing.Point(1, 23);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(1413, 1);
            this.label78.TabIndex = 97;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Top;
            this.label79.Location = new System.Drawing.Point(1, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1413, 1);
            this.label79.TabIndex = 99;
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Right;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(1414, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(1, 24);
            this.label80.TabIndex = 101;
            this.label80.Text = "label4";
            // 
            // label81
            // 
            this.label81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Dock = System.Windows.Forms.DockStyle.Left;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(0, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(1, 24);
            this.label81.TabIndex = 100;
            this.label81.Text = "label4";
            // 
            // panel26
            // 
            this.panel26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel26.Location = new System.Drawing.Point(431, 294);
            this.panel26.Name = "panel26";
            this.panel26.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel26.Size = new System.Drawing.Size(440, 10);
            this.panel26.TabIndex = 0;
            // 
            // panel27
            // 
            this.panel27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel27.Controls.Add(this.panel28);
            this.panel27.Controls.Add(this.panel29);
            this.panel27.Controls.Add(this.comboBox13);
            this.panel27.Controls.Add(this.dateTimePicker6);
            this.panel27.Controls.Add(this.label101);
            this.panel27.Controls.Add(this.button32);
            this.panel27.Controls.Add(this.button33);
            this.panel27.Controls.Add(this.label102);
            this.panel27.Controls.Add(this.comboBox14);
            this.panel27.Controls.Add(this.comboBox15);
            this.panel27.Controls.Add(this.label103);
            this.panel27.Controls.Add(this.button34);
            this.panel27.Controls.Add(this.button35);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel27.Location = new System.Drawing.Point(3, 3);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(1415, 147);
            this.panel27.TabIndex = 2;
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.label82);
            this.panel28.Controls.Add(this.label83);
            this.panel28.Controls.Add(this.checkBox2);
            this.panel28.Controls.Add(this.label84);
            this.panel28.Controls.Add(this.radioButton3);
            this.panel28.Controls.Add(this.label85);
            this.panel28.Controls.Add(this.radioButton4);
            this.panel28.Controls.Add(this.comboBox6);
            this.panel28.Controls.Add(this.dateTimePicker3);
            this.panel28.Controls.Add(this.label86);
            this.panel28.Controls.Add(this.dateTimePicker4);
            this.panel28.Controls.Add(this.button18);
            this.panel28.Controls.Add(this.comboBox7);
            this.panel28.Controls.Add(this.button19);
            this.panel28.Controls.Add(this.label87);
            this.panel28.Controls.Add(this.comboBox8);
            this.panel28.Controls.Add(this.button20);
            this.panel28.Controls.Add(this.label88);
            this.panel28.Controls.Add(this.button21);
            this.panel28.Controls.Add(this.button22);
            this.panel28.Controls.Add(this.comboBox9);
            this.panel28.Controls.Add(this.button23);
            this.panel28.Controls.Add(this.button24);
            this.panel28.Controls.Add(this.dateTimePicker5);
            this.panel28.Controls.Add(this.button25);
            this.panel28.Controls.Add(this.label89);
            this.panel28.Controls.Add(this.label90);
            this.panel28.Controls.Add(this.label91);
            this.panel28.Controls.Add(this.comboBox10);
            this.panel28.Controls.Add(this.label92);
            this.panel28.Controls.Add(this.button26);
            this.panel28.Controls.Add(this.button27);
            this.panel28.Controls.Add(this.button28);
            this.panel28.Controls.Add(this.button29);
            this.panel28.Controls.Add(this.comboBox11);
            this.panel28.Controls.Add(this.label93);
            this.panel28.Controls.Add(this.label94);
            this.panel28.Controls.Add(this.comboBox12);
            this.panel28.Controls.Add(this.button30);
            this.panel28.Controls.Add(this.button31);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel28.Location = new System.Drawing.Point(0, 24);
            this.panel28.Name = "panel28";
            this.panel28.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel28.Size = new System.Drawing.Size(1415, 123);
            this.panel28.TabIndex = 263;
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Top;
            this.label82.Location = new System.Drawing.Point(1, 3);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(1413, 1);
            this.label82.TabIndex = 262;
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label83.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label83.Location = new System.Drawing.Point(1, 122);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(1413, 1);
            this.label83.TabIndex = 220;
            this.label83.Text = "label2";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(771, 99);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(258, 18);
            this.checkBox2.TabIndex = 261;
            this.checkBox2.Text = "Include only one Appointment per Patient";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Right;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label84.Location = new System.Drawing.Point(1414, 3);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(1, 120);
            this.label84.TabIndex = 219;
            this.label84.Text = "label3";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(1244, 48);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 18);
            this.radioButton3.TabIndex = 260;
            this.radioButton3.Text = "Plan";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Dock = System.Windows.Forms.DockStyle.Left;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(0, 3);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(1, 120);
            this.label85.TabIndex = 218;
            this.label85.Text = "label4";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(1133, 48);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(75, 18);
            this.radioButton4.TabIndex = 259;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Company";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(406, 38);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(158, 21);
            this.comboBox6.TabIndex = 1;
            this.comboBox6.Visible = false;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker3.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker3.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker3.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker3.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker3.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker3.Location = new System.Drawing.Point(208, 38);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(76, 22);
            this.dateTimePicker3.TabIndex = 258;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.ForeColor = System.Drawing.Color.Red;
            this.label86.Location = new System.Drawing.Point(20, 15);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(14, 14);
            this.label86.TabIndex = 234;
            this.label86.Text = "*";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker4.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker4.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker4.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker4.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker4.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker4.Location = new System.Drawing.Point(126, 38);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(76, 22);
            this.dateTimePicker4.TabIndex = 257;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Transparent;
            this.button18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button18.BackgroundImage")));
            this.button18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button18.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Image = ((System.Drawing.Image)(resources.GetObject("button18.Image")));
            this.button18.Location = new System.Drawing.Point(567, 38);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(22, 22);
            this.button18.TabIndex = 221;
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Visible = false;
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(1133, 9);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(158, 21);
            this.comboBox7.TabIndex = 253;
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Transparent;
            this.button19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button19.BackgroundImage")));
            this.button19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button19.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.Image = ((System.Drawing.Image)(resources.GetObject("button19.Image")));
            this.button19.Location = new System.Drawing.Point(592, 38);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(22, 22);
            this.button19.TabIndex = 222;
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Visible = false;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(1076, 16);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(54, 14);
            this.label87.TabIndex = 256;
            this.label87.Text = "Patient :";
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(1133, 67);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(158, 21);
            this.comboBox8.TabIndex = 2;
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.Transparent;
            this.button20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button20.BackgroundImage")));
            this.button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button20.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.Image = ((System.Drawing.Image)(resources.GetObject("button20.Image")));
            this.button20.Location = new System.Drawing.Point(1294, 9);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(22, 22);
            this.button20.TabIndex = 254;
            this.button20.UseVisualStyleBackColor = false;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(1062, 71);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(68, 14);
            this.label88.TabIndex = 224;
            this.label88.Text = "Insurance :";
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.Transparent;
            this.button21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button21.BackgroundImage")));
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Image = ((System.Drawing.Image)(resources.GetObject("button21.Image")));
            this.button21.Location = new System.Drawing.Point(1319, 9);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(22, 22);
            this.button21.TabIndex = 255;
            this.button21.UseVisualStyleBackColor = false;
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.Transparent;
            this.button22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button22.BackgroundImage")));
            this.button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button22.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Image = ((System.Drawing.Image)(resources.GetObject("button22.Image")));
            this.button22.Location = new System.Drawing.Point(1294, 67);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(22, 22);
            this.button22.TabIndex = 3;
            this.button22.UseVisualStyleBackColor = false;
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(771, 67);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(158, 21);
            this.comboBox9.TabIndex = 250;
            this.comboBox9.Visible = false;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.Transparent;
            this.button23.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button23.BackgroundImage")));
            this.button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Image = ((System.Drawing.Image)(resources.GetObject("button23.Image")));
            this.button23.Location = new System.Drawing.Point(1319, 67);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(22, 22);
            this.button23.TabIndex = 4;
            this.button23.UseVisualStyleBackColor = false;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.Transparent;
            this.button24.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button24.BackgroundImage")));
            this.button24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button24.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Image = ((System.Drawing.Image)(resources.GetObject("button24.Image")));
            this.button24.Location = new System.Drawing.Point(957, 67);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(22, 22);
            this.button24.TabIndex = 252;
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Visible = false;
            // 
            // dateTimePicker5
            // 
            this.dateTimePicker5.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker5.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker5.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker5.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker5.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker5.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker5.Location = new System.Drawing.Point(126, 10);
            this.dateTimePicker5.Name = "dateTimePicker5";
            this.dateTimePicker5.Size = new System.Drawing.Size(158, 22);
            this.dateTimePicker5.TabIndex = 5;
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.Color.Transparent;
            this.button25.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button25.BackgroundImage")));
            this.button25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button25.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.Image = ((System.Drawing.Image)(resources.GetObject("button25.Image")));
            this.button25.Location = new System.Drawing.Point(932, 67);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(22, 22);
            this.button25.TabIndex = 251;
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Visible = false;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(345, 45);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(59, 14);
            this.label89.TabIndex = 0;
            this.label89.Text = "Provider :";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label89.Visible = false;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(695, 45);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(74, 14);
            this.label90.TabIndex = 246;
            this.label90.Text = "Appt Type :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label90.Visible = false;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(31, 17);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(93, 14);
            this.label91.TabIndex = 231;
            this.label91.Text = "Check-In Date :";
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(771, 38);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(158, 21);
            this.comboBox10.TabIndex = 247;
            this.comboBox10.Visible = false;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(15, 45);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(109, 14);
            this.label92.TabIndex = 232;
            this.label92.Text = "Appt Start Times :";
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.Transparent;
            this.button26.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button26.BackgroundImage")));
            this.button26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button26.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.Image = ((System.Drawing.Image)(resources.GetObject("button26.Image")));
            this.button26.Location = new System.Drawing.Point(957, 38);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(22, 22);
            this.button26.TabIndex = 249;
            this.button26.UseVisualStyleBackColor = false;
            this.button26.Visible = false;
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.Color.Transparent;
            this.button27.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button27.BackgroundImage")));
            this.button27.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button27.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.Image = ((System.Drawing.Image)(resources.GetObject("button27.Image")));
            this.button27.Location = new System.Drawing.Point(592, 10);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(22, 22);
            this.button27.TabIndex = 240;
            this.button27.UseVisualStyleBackColor = false;
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.Color.Transparent;
            this.button28.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button28.BackgroundImage")));
            this.button28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button28.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.Image = ((System.Drawing.Image)(resources.GetObject("button28.Image")));
            this.button28.Location = new System.Drawing.Point(932, 38);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(22, 22);
            this.button28.TabIndex = 248;
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Visible = false;
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.Color.Transparent;
            this.button29.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button29.BackgroundImage")));
            this.button29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button29.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button29.Image = ((System.Drawing.Image)(resources.GetObject("button29.Image")));
            this.button29.Location = new System.Drawing.Point(567, 10);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(22, 22);
            this.button29.TabIndex = 239;
            this.button29.UseVisualStyleBackColor = false;
            // 
            // comboBox11
            // 
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Location = new System.Drawing.Point(771, 9);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(158, 21);
            this.comboBox11.TabIndex = 242;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(338, 17);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(66, 14);
            this.label93.TabIndex = 241;
            this.label93.Text = "Locations :";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(699, 16);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(70, 14);
            this.label94.TabIndex = 245;
            this.label94.Text = "Resources :";
            // 
            // comboBox12
            // 
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Location = new System.Drawing.Point(406, 10);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(158, 21);
            this.comboBox12.TabIndex = 238;
            // 
            // button30
            // 
            this.button30.BackColor = System.Drawing.Color.Transparent;
            this.button30.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button30.BackgroundImage")));
            this.button30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button30.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button30.Image = ((System.Drawing.Image)(resources.GetObject("button30.Image")));
            this.button30.Location = new System.Drawing.Point(932, 9);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(22, 22);
            this.button30.TabIndex = 243;
            this.button30.UseVisualStyleBackColor = false;
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.Color.Transparent;
            this.button31.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button31.BackgroundImage")));
            this.button31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button31.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button31.Image = ((System.Drawing.Image)(resources.GetObject("button31.Image")));
            this.button31.Location = new System.Drawing.Point(957, 9);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(22, 22);
            this.button31.TabIndex = 244;
            this.button31.UseVisualStyleBackColor = false;
            // 
            // panel29
            // 
            this.panel29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel29.Controls.Add(this.panel30);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel29.Location = new System.Drawing.Point(0, 0);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(1415, 24);
            this.panel29.TabIndex = 262;
            // 
            // panel30
            // 
            this.panel30.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel30.Controls.Add(this.label95);
            this.panel30.Controls.Add(this.label96);
            this.panel30.Controls.Add(this.label97);
            this.panel30.Controls.Add(this.label98);
            this.panel30.Controls.Add(this.label99);
            this.panel30.Controls.Add(this.label100);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel30.Location = new System.Drawing.Point(0, 0);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(1415, 24);
            this.panel30.TabIndex = 1;
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label95.Dock = System.Windows.Forms.DockStyle.Right;
            this.label95.Location = new System.Drawing.Point(1413, 1);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(1, 22);
            this.label95.TabIndex = 98;
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label96.Location = new System.Drawing.Point(1, 23);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(1413, 1);
            this.label96.TabIndex = 97;
            // 
            // label97
            // 
            this.label97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label97.Dock = System.Windows.Forms.DockStyle.Top;
            this.label97.Location = new System.Drawing.Point(1, 0);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(1413, 1);
            this.label97.TabIndex = 99;
            // 
            // label98
            // 
            this.label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label98.Dock = System.Windows.Forms.DockStyle.Right;
            this.label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(1414, 0);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(1, 24);
            this.label98.TabIndex = 101;
            this.label98.Text = "label4";
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Dock = System.Windows.Forms.DockStyle.Left;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(0, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(1, 24);
            this.label99.TabIndex = 100;
            this.label99.Text = "label4";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.BackColor = System.Drawing.Color.Transparent;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(5, 5);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(403, 14);
            this.label100.TabIndex = 233;
            this.label100.Text = "1. Specify appointment filters and options, then select Refresh :";
            this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox13
            // 
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Location = new System.Drawing.Point(751, 193);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(108, 21);
            this.comboBox13.TabIndex = 1;
            // 
            // dateTimePicker6
            // 
            this.dateTimePicker6.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dateTimePicker6.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker6.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dateTimePicker6.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dateTimePicker6.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dateTimePicker6.CustomFormat = "MM/dd/yyyy";
            this.dateTimePicker6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker6.Location = new System.Drawing.Point(44, 171);
            this.dateTimePicker6.Name = "dateTimePicker6";
            this.dateTimePicker6.Size = new System.Drawing.Size(108, 22);
            this.dateTimePicker6.TabIndex = 9;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.Location = new System.Drawing.Point(668, 194);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(79, 14);
            this.label101.TabIndex = 212;
            this.label101.Text = "Date Range :";
            this.label101.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button32
            // 
            this.button32.BackColor = System.Drawing.Color.Transparent;
            this.button32.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button32.BackgroundImage")));
            this.button32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button32.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button32.Image = ((System.Drawing.Image)(resources.GetObject("button32.Image")));
            this.button32.Location = new System.Drawing.Point(502, 157);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(23, 23);
            this.button32.TabIndex = 8;
            this.button32.UseVisualStyleBackColor = false;
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.Color.Transparent;
            this.button33.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button33.BackgroundImage")));
            this.button33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button33.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button33.Image = ((System.Drawing.Image)(resources.GetObject("button33.Image")));
            this.button33.Location = new System.Drawing.Point(473, 157);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(23, 23);
            this.button33.TabIndex = 7;
            this.button33.UseVisualStyleBackColor = false;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(272, 158);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(37, 14);
            this.label102.TabIndex = 228;
            this.label102.Text = "CPT :";
            // 
            // comboBox14
            // 
            this.comboBox14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox14.ForeColor = System.Drawing.Color.Black;
            this.comboBox14.FormattingEnabled = true;
            this.comboBox14.Location = new System.Drawing.Point(315, 157);
            this.comboBox14.Name = "comboBox14";
            this.comboBox14.Size = new System.Drawing.Size(149, 21);
            this.comboBox14.TabIndex = 6;
            // 
            // comboBox15
            // 
            this.comboBox15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox15.FormattingEnabled = true;
            this.comboBox15.Location = new System.Drawing.Point(315, 188);
            this.comboBox15.Name = "comboBox15";
            this.comboBox15.Size = new System.Drawing.Size(149, 21);
            this.comboBox15.TabIndex = 10;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(213, 189);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(96, 14);
            this.label103.TabIndex = 220;
            this.label103.Text = "Diagnosis Code :";
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.Color.Transparent;
            this.button34.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button34.BackgroundImage")));
            this.button34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button34.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button34.Image = ((System.Drawing.Image)(resources.GetObject("button34.Image")));
            this.button34.Location = new System.Drawing.Point(473, 188);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(23, 23);
            this.button34.TabIndex = 11;
            this.button34.UseVisualStyleBackColor = false;
            // 
            // button35
            // 
            this.button35.BackColor = System.Drawing.Color.Transparent;
            this.button35.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button35.BackgroundImage")));
            this.button35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button35.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.button35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button35.Image = ((System.Drawing.Image)(resources.GetObject("button35.Image")));
            this.button35.Location = new System.Drawing.Point(502, 188);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(23, 23);
            this.button35.TabIndex = 12;
            this.button35.UseVisualStyleBackColor = false;
            // 
            // frmBatchPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1284, 725);
            this.Controls.Add(this.tbBatchPrint);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_Prgsbar);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBatchPrinting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Print Templates";
            this.Load += new System.EventHandler(this.frmBatchPrinting_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls_OverDue.ResumeLayout(false);
            this.tls_OverDue.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlChkInGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ChkInPatients)).EndInit();
            this.pnlProviderHeader.ResumeLayout(false);
            this.Panel22.ResumeLayout(false);
            this.pnl_Prgsbar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.tbBatchPrint.ResumeLayout(false);
            this.tbCheckInPre.ResumeLayout(false);
            this.panel39.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.tbAppLetters.ResumeLayout(false);
            this.panel40.ResumeLayout(false);
            this.panel41.ResumeLayout(false);
            this.panel42.ResumeLayout(false);
            this.panel43.ResumeLayout(false);
            this.panel35.ResumeLayout(false);
            this.pnlApptLetterGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ApptLetterPatients)).EndInit();
            this.panel37.ResumeLayout(false);
            this.panel38.ResumeLayout(false);
            this.panel31.ResumeLayout(false);
            this.panel31.PerformLayout();
            this.panel32.ResumeLayout(false);
            this.panel32.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel33.ResumeLayout(false);
            this.panel34.ResumeLayout(false);
            this.panel34.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.panel24.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel29.ResumeLayout(false);
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbChkInProvider;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnClearPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        private System.Windows.Forms.Panel pnl_tlspTOP;
        private  gloGlobal.gloToolStripIgnoreFocus tls_OverDue;
        //internal gloGlobal.gloToolStripIgnoreFocus tls_OverDue;
        private System.Windows.Forms.ToolStripButton ts_btnPrint;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.TreeView trvTemplates;
        private System.Windows.Forms.DateTimePicker dtpChkInDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_ToDate;
        private System.Windows.Forms.ToolStripButton ts_btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Prgsbar;
        private System.Windows.Forms.ProgressBar prgBar_Print;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnChkInClearInsurance;
        internal System.Windows.Forms.Button btnChkInBrowseInsurance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbChkInInsuranceCompany;
        internal System.Windows.Forms.Button btnClearDiagnosisCode;
        internal System.Windows.Forms.Button btnBrowseDiagnosisCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDiagnosisCode;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.Button btnSelectAllClearAll;
        internal System.Windows.Forms.Button btnChkInClearProvider;
        internal System.Windows.Forms.Button btnChkInBrowseProvider;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Panel pnlChkInGrid;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button btnChkInSelectAll;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Panel Panel22;
        internal System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        internal System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TabControl tbBatchPrint;
        private System.Windows.Forms.TabPage tbCheckInPre;
        private System.Windows.Forms.TabPage tbAppLetters;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox cmbChkInLocation;
        private System.Windows.Forms.Label label42;
        internal System.Windows.Forms.Button btnChkInBrowseLocation;
        internal System.Windows.Forms.Button btnChkInClearLocation;
        private System.Windows.Forms.ComboBox cmbChkInPatient;
        private System.Windows.Forms.Label label46;
        internal System.Windows.Forms.Button btnChkInBrowsePatient;
        internal System.Windows.Forms.Button btnChkInClearPatient;
        private System.Windows.Forms.ComboBox cmbChkInApptTypeType;
        internal System.Windows.Forms.Button btnChkInClearApptTypeType;
        internal System.Windows.Forms.Button btnChkInBrowseApptTypeType;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbChkInApptType;
        internal System.Windows.Forms.Button btnChkInClearApptType;
        internal System.Windows.Forms.Button btnChkInBrowseApptType;
        private System.Windows.Forms.ComboBox cmbChkInReosurce;
        private System.Windows.Forms.Label label41;
        internal System.Windows.Forms.Button btnChkInBrowseResource;
        internal System.Windows.Forms.Button btnChkInClearResource;
        private System.Windows.Forms.CheckBox chkChkInIncludeOneAppt;
        private System.Windows.Forms.RadioButton rbtnChkInPlan;
        private System.Windows.Forms.RadioButton rbtnChkInCompany;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel12;
        internal System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnChkInClearAll;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.TreeView trvChkInTemplate;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Panel panel15;
        internal System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btnChkInSelectAllTreeNode;
        private System.Windows.Forms.Button btnChkInClearAllTreeNode;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Panel panel19;
        internal System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Panel panel23;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Panel panel24;
        internal System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        internal System.Windows.Forms.Button button18;
        private System.Windows.Forms.ComboBox comboBox7;
        internal System.Windows.Forms.Button button19;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.ComboBox comboBox8;
        internal System.Windows.Forms.Button button20;
        private System.Windows.Forms.Label label88;
        internal System.Windows.Forms.Button button21;
        internal System.Windows.Forms.Button button22;
        private System.Windows.Forms.ComboBox comboBox9;
        internal System.Windows.Forms.Button button23;
        internal System.Windows.Forms.Button button24;
        private System.Windows.Forms.DateTimePicker dateTimePicker5;
        internal System.Windows.Forms.Button button25;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.Label label92;
        internal System.Windows.Forms.Button button26;
        internal System.Windows.Forms.Button button27;
        internal System.Windows.Forms.Button button28;
        internal System.Windows.Forms.Button button29;
        private System.Windows.Forms.ComboBox comboBox11;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.ComboBox comboBox12;
        internal System.Windows.Forms.Button button30;
        internal System.Windows.Forms.Button button31;
        private System.Windows.Forms.Panel panel29;
        internal System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.ComboBox comboBox13;
        private System.Windows.Forms.DateTimePicker dateTimePicker6;
        private System.Windows.Forms.Label label101;
        internal System.Windows.Forms.Button button32;
        internal System.Windows.Forms.Button button33;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.ComboBox comboBox14;
        private System.Windows.Forms.ComboBox comboBox15;
        private System.Windows.Forms.Label label103;
        internal System.Windows.Forms.Button button34;
        internal System.Windows.Forms.Button button35;
        private System.Windows.Forms.Panel panel31;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.RadioButton rbtnApptLetterNoShow;
        private System.Windows.Forms.RadioButton rbtnApptLetterCancelled;
        private System.Windows.Forms.RadioButton rbtnApptLetterOpen;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.DateTimePicker dtpApptLetterToDate;
        private System.Windows.Forms.DateTimePicker dtpApptLetterFromDate;
        private System.Windows.Forms.ComboBox cmbApptLetterDateRange;
        private System.Windows.Forms.CheckBox chkApptLetterIncludeOneAppt;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.RadioButton rbtnApptLetterPlan;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.RadioButton rbtnApptLetterCompany;
        private System.Windows.Forms.ComboBox cmbApptLetterProvider;
        internal System.Windows.Forms.Button btnApptLetterBrowseProvider;
        private System.Windows.Forms.ComboBox cmbApptLetterPatient;
        internal System.Windows.Forms.Button btnApptLetterClearProvider;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.ComboBox cmbApptLetterInsuranceCompany;
        internal System.Windows.Forms.Button btnApptLetterBrowsePatient;
        private System.Windows.Forms.Label label110;
        internal System.Windows.Forms.Button btnApptLetterClearPatient;
        internal System.Windows.Forms.Button btnApptLetterBrowseInsurance;
        private System.Windows.Forms.ComboBox cmbApptLetterApptTypeType;
        internal System.Windows.Forms.Button btnApptLetterClearInsurance;
        internal System.Windows.Forms.Button btnApptLetterClearApptTypeType;
        internal System.Windows.Forms.Button btnApptLetterBrowseApptTypeType;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.ComboBox cmbApptLetterApptType;
        private System.Windows.Forms.Label label114;
        internal System.Windows.Forms.Button btnApptLetterClearApptType;
        internal System.Windows.Forms.Button btnApptLetterClearLocation;
        internal System.Windows.Forms.Button btnApptLetterBrowseApptType;
        internal System.Windows.Forms.Button btnApptLetterBrowseLocation;
        private System.Windows.Forms.ComboBox cmbApptLetterResource;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.ComboBox cmbApptLetterLocation;
        internal System.Windows.Forms.Button btnApptLetterBrowseResource;
        internal System.Windows.Forms.Button btnApptLetterClearResource;
        private System.Windows.Forms.Panel panel33;
        internal System.Windows.Forms.Panel panel34;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.Label label122;
        internal System.Windows.Forms.Button button50;
        internal System.Windows.Forms.Button button51;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.ComboBox comboBox24;
        private System.Windows.Forms.ComboBox comboBox25;
        private System.Windows.Forms.Label label125;
        internal System.Windows.Forms.Button button52;
        internal System.Windows.Forms.Button button53;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ChkInPatients;
        private System.Windows.Forms.Panel panel39;
        private System.Windows.Forms.Panel panel40;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.TreeView trvApptLetterPrintTemplate;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label137;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Panel panel42;
        internal System.Windows.Forms.Panel panel43;
        private System.Windows.Forms.Button btnApptLetterSelectAllTreeNode;
        private System.Windows.Forms.Button btnApptLetterClearAllTreeNode;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Panel panel35;
        private System.Windows.Forms.Panel pnlApptLetterGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ApptLetterPatients;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Panel panel37;
        internal System.Windows.Forms.Panel panel38;
        private System.Windows.Forms.Button btnApptLetterSelectAll;
        private System.Windows.Forms.Button btnApptLetterClearAll;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.ComboBox cmbChkInInsurancePlan;
        private System.Windows.Forms.ComboBox cmbApptLetterInsurancePlan;
        internal System.Windows.Forms.DateTimePicker dtpChkInApptStartTimeTo;
        internal System.Windows.Forms.DateTimePicker dtpChkInApptStartTimeFrom;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateBatch;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.DateTimePicker dtpApptLetterApptStartTimeTo;
        internal System.Windows.Forms.DateTimePicker dtpApptLetterApptStartTimeFrom;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}
