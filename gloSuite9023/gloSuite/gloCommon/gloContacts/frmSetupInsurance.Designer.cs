namespace gloContacts
{
    partial class frmSetupInsurance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupInsurance));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnAdd_FeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRemoveAll = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnFeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnImportFeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.tls_Hold = new System.Windows.Forms.ToolStripButton();
            this.tls_Release = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave_FeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnNewAlternateID = new System.Windows.Forms.ToolStripButton();
            this.ts_btnEditAlternateID = new System.Windows.Forms.ToolStripButton();
            this.ts_btnDeleteAlternateID = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose_FeeSchedule = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.ttInsuranceType = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbp_BillingSettings = new System.Windows.Forms.TabPage();
            this.pnlBillingDetailsMain = new System.Windows.Forms.Panel();
            this.pnlFlexGrid = new System.Windows.Forms.Panel();
            this.c1ProviderSettings = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.pnlBillingSourceOther = new System.Windows.Forms.Panel();
            this.chkBillingProviderSwap = new System.Windows.Forms.CheckBox();
            this.cmbBillingProviderSourceOtherIDType = new System.Windows.Forms.ComboBox();
            this.lblProviderSourceChkSwap = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.chkBillingProviderOtherID = new System.Windows.Forms.CheckBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.cmbBillingProviderSource = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.chkOtherIDonEDI = new System.Windows.Forms.CheckBox();
            this.pnlServiceFacilityOther = new System.Windows.Forms.Panel();
            this.chkServiceFacilitySwap = new System.Windows.Forms.CheckBox();
            this.cmbServiceFacilityOtherIDType = new System.Windows.Forms.ComboBox();
            this.lblServiceFacChkSwap = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.chkServiceFacOtherID = new System.Windows.Forms.CheckBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.cmbServiceFacilitySource = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chkIDInBox31 = new System.Windows.Forms.CheckBox();
            this.lblElectronicRendering = new System.Windows.Forms.Label();
            this.cmbReferringProviderOtherIDType = new System.Windows.Forms.ComboBox();
            this.label108 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.lblPaperRendering = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbElectronicRendering = new System.Windows.Forms.ComboBox();
            this.cmbPaperRendering = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.panel28 = new System.Windows.Forms.Panel();
            this.label161 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.txtBox19DefaultNote = new System.Windows.Forms.TextBox();
            this.label159 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.chkRefferingID = new System.Windows.Forms.CheckBox();
            this.chkNotesInBox19 = new System.Windows.Forms.CheckBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.chkSecondaryPayerAddress = new System.Windows.Forms.CheckBox();
            this.chkReportClinicName = new System.Windows.Forms.CheckBox();
            this.label164 = new System.Windows.Forms.Label();
            this.cmbCMSDateFormat = new System.Windows.Forms.ComboBox();
            this.label163 = new System.Windows.Forms.Label();
            this.chkEdiAltPayerID = new System.Windows.Forms.CheckBox();
            this.label162 = new System.Windows.Forms.Label();
            this.cmbBox11bSettings = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbRefSecIdentification = new System.Windows.Forms.ComboBox();
            this.chkClaimFreq = new System.Windows.Forms.CheckBox();
            this.chkMedClaimRef = new System.Windows.Forms.CheckBox();
            this.chkWorkersComp = new System.Windows.Forms.CheckBox();
            this.chkShowClaim = new System.Windows.Forms.CheckBox();
            this.ChkEMGAsX = new System.Windows.Forms.CheckBox();
            this.chkSwap1a9a1aMCare = new System.Windows.Forms.CheckBox();
            this.chkPaperDisplayMailingAddress = new System.Windows.Forms.CheckBox();
            this.chkIncludePlanName = new System.Windows.Forms.CheckBox();
            this.txtBillClaimOfficeNo = new System.Windows.Forms.TextBox();
            this.cmbClIAPostn = new System.Windows.Forms.ComboBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbStatementToPatientYes = new System.Windows.Forms.RadioButton();
            this.rbStatementToPatientNo = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBox29 = new System.Windows.Forms.ComboBox();
            this.cmbBox30 = new System.Windows.Forms.ComboBox();
            this.Box29 = new System.Windows.Forms.Label();
            this.Box30 = new System.Windows.Forms.Label();
            this.TxtDiagnosisperClaim = new System.Windows.Forms.TextBox();
            this.TxtChargesperClaim = new System.Windows.Forms.TextBox();
            this.lblDiagnosisperClaim = new System.Windows.Forms.Label();
            this.lblChargeperClaim = new System.Windows.Forms.Label();
            this.cmbClearingHouse = new System.Windows.Forms.ComboBox();
            this.cmbTypeOFBilling = new System.Windows.Forms.ComboBox();
            this.label75 = new System.Windows.Forms.Label();
            this.chkCorrectRplmnt = new System.Windows.Forms.CheckBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lblCptCrosswalk = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFeeSchedules = new System.Windows.Forms.ComboBox();
            this.cmbCptCrosswalk = new System.Windows.Forms.ComboBox();
            this.chkIncludeOTAFAmount = new System.Windows.Forms.CheckBox();
            this.cmbdonotprintfacility = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.chkAcceptAssignment = new System.Windows.Forms.CheckBox();
            this.pnlBillingHeader = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBillingSettings = new System.Windows.Forms.Label();
            this.lblLeftAlign = new System.Windows.Forms.Label();
            this.lblRightAlign = new System.Windows.Forms.Label();
            this.lblTopAlign = new System.Windows.Forms.Label();
            this.lblBottomAlign = new System.Windows.Forms.Label();
            this.tbp_MidLevel = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.c1MidlevelSettings = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cmbMidLevelSpeProvider = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tbp_InsurancePlan = new System.Windows.Forms.TabPage();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.label76 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbBox33B = new System.Windows.Forms.ComboBox();
            this.cmbBox33A = new System.Windows.Forms.ComboBox();
            this.cmbBox33 = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbBox32B = new System.Windows.Forms.ComboBox();
            this.cmbBox32 = new System.Windows.Forms.ComboBox();
            this.cmbBox32A = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.chkPARequired = new System.Windows.Forms.CheckBox();
            this.lblInsEligibilityProviderID = new System.Windows.Forms.Label();
            this.txtInsEligibilityProvderID = new System.Windows.Forms.TextBox();
            this.GBox_GeneralInfo = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label74 = new System.Windows.Forms.Label();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.txtname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtPayerID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbInsuranceCompany = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.mtxtPhone = new gloMaskControl.gloMaskBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.cmbReportingCategory = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.cmbInsuranceType = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txtFax = new gloMaskControl.gloMaskBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_PayerPhExt = new System.Windows.Forms.TextBox();
            this.mtxt_PayerPhone = new gloMaskControl.gloMaskBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.txtAdditionalInfo = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.txtServicingState = new System.Windows.Forms.TextBox();
            this.txtOfficeID = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbAcceptAssignmentYes = new System.Windows.Forms.RadioButton();
            this.rbAcceptAssignmentNo = new System.Windows.Forms.RadioButton();
            this.chkShowPayment = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkNameOfFacilityinBox33 = new System.Windows.Forms.CheckBox();
            this.chkRemittanceAdvice = new System.Windows.Forms.CheckBox();
            this.chkDoNotPrintFacility = new System.Windows.Forms.CheckBox();
            this.lblOfficeID = new System.Windows.Forms.Label();
            this.chkBox31Blank = new System.Windows.Forms.CheckBox();
            this.chkEnrollmentRequired = new System.Windows.Forms.CheckBox();
            this.chkOnlyPrintFirstPointer = new System.Windows.Forms.CheckBox();
            this.chkElectronicCOB = new System.Windows.Forms.CheckBox();
            this.chkRealTimeClaimStatus = new System.Windows.Forms.CheckBox();
            this.chkMedigap = new System.Windows.Forms.CheckBox();
            this.chkClaims = new System.Windows.Forms.CheckBox();
            this.GBox_Companyadrs = new System.Windows.Forms.GroupBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.gBoxComContact = new System.Windows.Forms.GroupBox();
            this.chkRealTimeEligibility = new System.Windows.Forms.CheckBox();
            this.pnlHoldMessage = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblHoldMessage = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.tbInsuranceSetup = new System.Windows.Forms.TabControl();
            this.tbp_Eligibility = new System.Windows.Forms.TabPage();
            this.panel19 = new System.Windows.Forms.Panel();
            this.mskeligibiltyPhone = new gloMaskControl.gloMaskBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtEligibiltyNote = new System.Windows.Forms.TextBox();
            this.lblphone = new System.Windows.Forms.Label();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.TxtEligibiltyWebste = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.TxtEligiblitycntct = new System.Windows.Forms.TextBox();
            this.cmbInsEligibilityPrimProvType = new System.Windows.Forms.ComboBox();
            this.cmbInsEligibilitySecProvType = new System.Windows.Forms.ComboBox();
            this.label120 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.txtInsEligibilitySecProvID = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.txtInsEligibilityPrimProvID = new System.Windows.Forms.TextBox();
            this.tbp_BillingTaxon = new System.Windows.Forms.TabPage();
            this.panel17 = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1BillingTaxonomy = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.chkIncludeTaxBillElec = new System.Windows.Forms.CheckBox();
            this.chkIncludeTaxRenElec = new System.Windows.Forms.CheckBox();
            this.chkIncludeTaxBilPaper = new System.Windows.Forms.CheckBox();
            this.chkIncludeTaxRenPaper = new System.Windows.Forms.CheckBox();
            this.chkIncludeTax4Paper = new System.Windows.Forms.CheckBox();
            this.chkIncludeTax4Elec = new System.Windows.Forms.CheckBox();
            this.cmbQualifier = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label46 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.tbp_5010Transition = new System.Windows.Forms.TabPage();
            this.panel20 = new System.Windows.Forms.Panel();
            this.chkIncludeMod_in_SVD = new System.Windows.Forms.CheckBox();
            this.chkIncludePatientSSN = new System.Windows.Forms.CheckBox();
            this.label166 = new System.Windows.Forms.Label();
            this.panel29 = new System.Windows.Forms.Panel();
            this.rbDoNotSend = new System.Windows.Forms.RadioButton();
            this.rbSend = new System.Windows.Forms.RadioButton();
            this.label170 = new System.Windows.Forms.Label();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.label169 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.chkIncludeRefnOrdering = new System.Windows.Forms.CheckBox();
            this.chkIncludeRefnSupervising = new System.Windows.Forms.CheckBox();
            this.chkIncludeOrdering = new System.Windows.Forms.CheckBox();
            this.chkIncludeSubscriberAddress = new System.Windows.Forms.CheckBox();
            this.chkIncludeServiceFacility = new System.Windows.Forms.CheckBox();
            this.chkIncludeRendering = new System.Windows.Forms.CheckBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.tbp_AlternatePayerID = new System.Windows.Forms.TabPage();
            this.panel21 = new System.Windows.Forms.Panel();
            this.dgMasters = new System.Windows.Forms.DataGridView();
            this.c1AlternatePayerID = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label124 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.tbp_Institutional = new System.Windows.Forms.TabPage();
            this.panel24 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txt81DQual = new System.Windows.Forms.TextBox();
            this.cmb81DValue = new System.Windows.Forms.ComboBox();
            this.label176 = new System.Windows.Forms.Label();
            this.txt81CQual = new System.Windows.Forms.TextBox();
            this.txt81BQual = new System.Windows.Forms.TextBox();
            this.txt81AQual = new System.Windows.Forms.TextBox();
            this.cmb81CValue = new System.Windows.Forms.ComboBox();
            this.label171 = new System.Windows.Forms.Label();
            this.cmb81BValue = new System.Windows.Forms.ComboBox();
            this.label173 = new System.Windows.Forms.Label();
            this.cmb81AValue = new System.Windows.Forms.ComboBox();
            this.label174 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbBox77Rendering = new System.Windows.Forms.ComboBox();
            this.label165 = new System.Windows.Forms.Label();
            this.cmbOprtingPrvderBox77 = new System.Windows.Forms.ComboBox();
            this.lblFedTaxNoBox5 = new System.Windows.Forms.Label();
            this.cmbFedTaxNoBox5 = new System.Windows.Forms.ComboBox();
            this.label155 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.cmbUBBlngprvdraltID = new System.Windows.Forms.ComboBox();
            this.cmbExtendedZipCode = new System.Windows.Forms.ComboBox();
            this.label156 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ChkIncludeAttendingPrvTaxonomy = new System.Windows.Forms.CheckBox();
            this.chkIncludeEstimatedAmtDue = new System.Windows.Forms.CheckBox();
            this.chkIncludePrimaryDxInBox69 = new System.Windows.Forms.CheckBox();
            this.ckhSentUB04RevenuecodeTotal = new System.Windows.Forms.CheckBox();
            this.chkIncludeUB04AdmissionHour = new System.Windows.Forms.CheckBox();
            this.chkIncludeUB04DischargeHour = new System.Windows.Forms.CheckBox();
            this.chkIncludeRendering_Attending = new System.Windows.Forms.CheckBox();
            this.chkIsInstitutionalBilling = new System.Windows.Forms.CheckBox();
            this.chkDefaultDOS = new System.Windows.Forms.CheckBox();
            this.tbp_Collection = new System.Windows.Forms.TabPage();
            this.panel25 = new System.Windows.Forms.Panel();
            this.numInsClmRebillFUActionDays = new System.Windows.Forms.NumericUpDown();
            this.numInsClmStartFUActionDays = new System.Windows.Forms.NumericUpDown();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.cmbInsClmStartFUAction = new System.Windows.Forms.ComboBox();
            this.cmbInsClmRebillFUAction = new System.Windows.Forms.ComboBox();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.tbp_EPSDT = new System.Windows.Forms.TabPage();
            this.panel26 = new System.Windows.Forms.Panel();
            this.txtFamPlanCode = new System.Windows.Forms.TextBox();
            this.txtEPSDTCode = new System.Windows.Forms.TextBox();
            this.chkSuppressRenderPaperEdi = new System.Windows.Forms.CheckBox();
            this.cmbFamPlanCodeBox = new System.Windows.Forms.ComboBox();
            this.label149 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.label144 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.chkIncludeRefCode = new System.Windows.Forms.CheckBox();
            this.chkIncludeCRC = new System.Windows.Forms.CheckBox();
            this.chkIncludeSV = new System.Windows.Forms.CheckBox();
            this.chkBillEpsdtFamPlan = new System.Windows.Forms.CheckBox();
            this.cmbEPSDTCodeBox = new System.Windows.Forms.ComboBox();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.tpAnesthesia = new System.Windows.Forms.TabPage();
            this.panel27 = new System.Windows.Forms.Panel();
            this.label154 = new System.Windows.Forms.Label();
            this.cmbBillUnitsAs = new System.Windows.Forms.ComboBox();
            this.label142 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.txtBaseUnits = new System.Windows.Forms.TextBox();
            this.label153 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.chkGroupMandatory = new System.Windows.Forms.CheckBox();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.tbp_BillingSettings.SuspendLayout();
            this.pnlBillingDetailsMain.SuspendLayout();
            this.pnlFlexGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderSettings)).BeginInit();
            this.panel11.SuspendLayout();
            this.pnlBillingSourceOther.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel9.SuspendLayout();
            this.pnlServiceFacilityOther.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBillingHeader.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.tbp_MidLevel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1MidlevelSettings)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tbp_InsurancePlan.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GBox_GeneralInfo.SuspendLayout();
            this.panel12.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GBox_Companyadrs.SuspendLayout();
            this.gBoxComContact.SuspendLayout();
            this.pnlHoldMessage.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tbInsuranceSetup.SuspendLayout();
            this.tbp_Eligibility.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tbp_BillingTaxon.SuspendLayout();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BillingTaxonomy)).BeginInit();
            this.panel16.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.tbp_5010Transition.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel29.SuspendLayout();
            this.tbp_AlternatePayerID.SuspendLayout();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMasters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1AlternatePayerID)).BeginInit();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.tbp_Institutional.SuspendLayout();
            this.panel24.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tbp_Collection.SuspendLayout();
            this.panel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInsClmRebillFUActionDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInsClmStartFUActionDays)).BeginInit();
            this.tbp_EPSDT.SuspendLayout();
            this.panel26.SuspendLayout();
            this.tpAnesthesia.SuspendLayout();
            this.panel27.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.TopToolStrip);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(731, 53);
            this.pnlTopToolStrip.TabIndex = 0;
            this.pnlTopToolStrip.TabStop = true;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnAdd_FeeSchedule,
            this.ts_btnAddLine,
            this.ts_btnRemoveAll,
            this.ts_btnRemoveLine,
            this.ts_btnSave,
            this.ts_btnFeeSchedule,
            this.ts_btnImportFeeSchedule,
            this.tls_Hold,
            this.tls_Release,
            this.ts_btnSave_FeeSchedule,
            this.ts_btnNewAlternateID,
            this.ts_btnEditAlternateID,
            this.ts_btnDeleteAlternateID,
            this.ts_btnClose_FeeSchedule,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(731, 53);
            this.TopToolStrip.TabIndex = 0;
            this.TopToolStrip.TabStop = true;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnAdd_FeeSchedule
            // 
            this.ts_btnAdd_FeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAdd_FeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnAdd_FeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAdd_FeeSchedule.Image")));
            this.ts_btnAdd_FeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAdd_FeeSchedule.Name = "ts_btnAdd_FeeSchedule";
            this.ts_btnAdd_FeeSchedule.Size = new System.Drawing.Size(40, 50);
            this.ts_btnAdd_FeeSchedule.Tag = "Add";
            this.ts_btnAdd_FeeSchedule.Text = "&Add ";
            this.ts_btnAdd_FeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnAdd_FeeSchedule.Click += new System.EventHandler(this.ts_btnAdd_FeeSchedule_Click);
            // 
            // ts_btnAddLine
            // 
            this.ts_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAddLine.Image")));
            this.ts_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAddLine.Name = "ts_btnAddLine";
            this.ts_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.ts_btnAddLine.Tag = "AddLine";
            this.ts_btnAddLine.Text = "Add &Line";
            this.ts_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnAddLine.Click += new System.EventHandler(this.tls_btnAddLine_Click);
            // 
            // ts_btnRemoveAll
            // 
            this.ts_btnRemoveAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRemoveAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRemoveAll.Image")));
            this.ts_btnRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRemoveAll.Name = "ts_btnRemoveAll";
            this.ts_btnRemoveAll.Size = new System.Drawing.Size(79, 50);
            this.ts_btnRemoveAll.Tag = "RemoveAll";
            this.ts_btnRemoveAll.Text = "Re&move All";
            this.ts_btnRemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRemoveAll.Click += new System.EventHandler(this.ts_btnRemoveAll_Click);
            // 
            // ts_btnRemoveLine
            // 
            this.ts_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRemoveLine.Image")));
            this.ts_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRemoveLine.Name = "ts_btnRemoveLine";
            this.ts_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.ts_btnRemoveLine.Tag = "RemoveLine";
            this.ts_btnRemoveLine.Text = "Remo&ve Line";
            this.ts_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRemoveLine.Click += new System.EventHandler(this.tls_btnRemoveLine_Click);
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "Sa&ve&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
            // 
            // ts_btnFeeSchedule
            // 
            this.ts_btnFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnFeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnFeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnFeeSchedule.Image")));
            this.ts_btnFeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnFeeSchedule.Name = "ts_btnFeeSchedule";
            this.ts_btnFeeSchedule.Size = new System.Drawing.Size(90, 50);
            this.ts_btnFeeSchedule.Tag = "FeeSchedule";
            this.ts_btnFeeSchedule.Text = "&Fee Schedule";
            this.ts_btnFeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnFeeSchedule.Visible = false;
            this.ts_btnFeeSchedule.Click += new System.EventHandler(this.ts_btnFeeSchedule_Click);
            // 
            // ts_btnImportFeeSchedule
            // 
            this.ts_btnImportFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnImportFeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnImportFeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnImportFeeSchedule.Image")));
            this.ts_btnImportFeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnImportFeeSchedule.Name = "ts_btnImportFeeSchedule";
            this.ts_btnImportFeeSchedule.Size = new System.Drawing.Size(54, 50);
            this.ts_btnImportFeeSchedule.Tag = "ImportFeeSchedule";
            this.ts_btnImportFeeSchedule.Text = "&Import";
            this.ts_btnImportFeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnImportFeeSchedule.ToolTipText = "Import Standard  Fee Schedule";
            this.ts_btnImportFeeSchedule.Click += new System.EventHandler(this.ts_btnImportFeeSchedule_Click);
            // 
            // tls_Hold
            // 
            this.tls_Hold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Hold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_Hold.Image = ((System.Drawing.Image)(resources.GetObject("tls_Hold.Image")));
            this.tls_Hold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Hold.Name = "tls_Hold";
            this.tls_Hold.Size = new System.Drawing.Size(69, 50);
            this.tls_Hold.Tag = "Hold";
            this.tls_Hold.Text = "&Hold Plan";
            this.tls_Hold.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Hold.Visible = false;
            this.tls_Hold.Click += new System.EventHandler(this.tls_Hold_Click);
            // 
            // tls_Release
            // 
            this.tls_Release.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Release.Image = ((System.Drawing.Image)(resources.GetObject("tls_Release.Image")));
            this.tls_Release.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Release.Name = "tls_Release";
            this.tls_Release.Size = new System.Drawing.Size(89, 50);
            this.tls_Release.Text = "&Release Hold";
            this.tls_Release.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Release.Visible = false;
            this.tls_Release.Click += new System.EventHandler(this.tls_Release_Click);
            // 
            // ts_btnSave_FeeSchedule
            // 
            this.ts_btnSave_FeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave_FeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave_FeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave_FeeSchedule.Image")));
            this.ts_btnSave_FeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave_FeeSchedule.Name = "ts_btnSave_FeeSchedule";
            this.ts_btnSave_FeeSchedule.Size = new System.Drawing.Size(40, 50);
            this.ts_btnSave_FeeSchedule.Tag = "SaveFeeSchedule";
            this.ts_btnSave_FeeSchedule.Text = "&Save";
            this.ts_btnSave_FeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave_FeeSchedule.ToolTipText = "Save Fee Schedule";
            this.ts_btnSave_FeeSchedule.Click += new System.EventHandler(this.ts_btnSave_FeeSchedule_Click);
            // 
            // ts_btnNewAlternateID
            // 
            this.ts_btnNewAlternateID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnNewAlternateID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnNewAlternateID.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnNewAlternateID.Image")));
            this.ts_btnNewAlternateID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnNewAlternateID.Name = "ts_btnNewAlternateID";
            this.ts_btnNewAlternateID.Size = new System.Drawing.Size(37, 50);
            this.ts_btnNewAlternateID.Tag = "New";
            this.ts_btnNewAlternateID.Text = "&New";
            this.ts_btnNewAlternateID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnNewAlternateID.Visible = false;
            this.ts_btnNewAlternateID.Click += new System.EventHandler(this.ts_btnNewAlternateID_Click);
            // 
            // ts_btnEditAlternateID
            // 
            this.ts_btnEditAlternateID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnEditAlternateID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnEditAlternateID.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnEditAlternateID.Image")));
            this.ts_btnEditAlternateID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnEditAlternateID.Name = "ts_btnEditAlternateID";
            this.ts_btnEditAlternateID.Size = new System.Drawing.Size(36, 50);
            this.ts_btnEditAlternateID.Tag = "Edit";
            this.ts_btnEditAlternateID.Text = "&Edit";
            this.ts_btnEditAlternateID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnEditAlternateID.Visible = false;
            this.ts_btnEditAlternateID.Click += new System.EventHandler(this.ts_btnEditAlternateID_Click);
            // 
            // ts_btnDeleteAlternateID
            // 
            this.ts_btnDeleteAlternateID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnDeleteAlternateID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnDeleteAlternateID.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnDeleteAlternateID.Image")));
            this.ts_btnDeleteAlternateID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnDeleteAlternateID.Name = "ts_btnDeleteAlternateID";
            this.ts_btnDeleteAlternateID.Size = new System.Drawing.Size(50, 50);
            this.ts_btnDeleteAlternateID.Tag = "Delete";
            this.ts_btnDeleteAlternateID.Text = "&Delete";
            this.ts_btnDeleteAlternateID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnDeleteAlternateID.Visible = false;
            this.ts_btnDeleteAlternateID.Click += new System.EventHandler(this.ts_btnDeleteAlternateID_Click);
            // 
            // ts_btnClose_FeeSchedule
            // 
            this.ts_btnClose_FeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose_FeeSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose_FeeSchedule.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose_FeeSchedule.Image")));
            this.ts_btnClose_FeeSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose_FeeSchedule.Name = "ts_btnClose_FeeSchedule";
            this.ts_btnClose_FeeSchedule.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose_FeeSchedule.Tag = "Close";
            this.ts_btnClose_FeeSchedule.Text = "&Close";
            this.ts_btnClose_FeeSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose_FeeSchedule.ToolTipText = "Close Fee Schedule";
            this.ts_btnClose_FeeSchedule.Click += new System.EventHandler(this.ts_btnClose_FeeSchedule_Click);
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
            // tbp_BillingSettings
            // 
            this.tbp_BillingSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbp_BillingSettings.Controls.Add(this.pnlBillingDetailsMain);
            this.tbp_BillingSettings.Location = new System.Drawing.Point(4, 42);
            this.tbp_BillingSettings.Name = "tbp_BillingSettings";
            this.tbp_BillingSettings.Size = new System.Drawing.Size(723, 467);
            this.tbp_BillingSettings.TabIndex = 2;
            this.tbp_BillingSettings.Tag = "Billing";
            this.tbp_BillingSettings.Text = "Billing";
            this.tbp_BillingSettings.ToolTipText = "Billing";
            this.tbp_BillingSettings.UseVisualStyleBackColor = true;
            // 
            // pnlBillingDetailsMain
            // 
            this.pnlBillingDetailsMain.Controls.Add(this.pnlFlexGrid);
            this.pnlBillingDetailsMain.Controls.Add(this.panel11);
            this.pnlBillingDetailsMain.Controls.Add(this.panel13);
            this.pnlBillingDetailsMain.Controls.Add(this.panel9);
            this.pnlBillingDetailsMain.Controls.Add(this.panel8);
            this.pnlBillingDetailsMain.Controls.Add(this.panel28);
            this.pnlBillingDetailsMain.Controls.Add(this.panel10);
            this.pnlBillingDetailsMain.Controls.Add(this.pnlBillingHeader);
            this.pnlBillingDetailsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBillingDetailsMain.Location = new System.Drawing.Point(0, 0);
            this.pnlBillingDetailsMain.Name = "pnlBillingDetailsMain";
            this.pnlBillingDetailsMain.Size = new System.Drawing.Size(723, 467);
            this.pnlBillingDetailsMain.TabIndex = 0;
            // 
            // pnlFlexGrid
            // 
            this.pnlFlexGrid.Controls.Add(this.c1ProviderSettings);
            this.pnlFlexGrid.Controls.Add(this.label92);
            this.pnlFlexGrid.Controls.Add(this.label93);
            this.pnlFlexGrid.Controls.Add(this.label94);
            this.pnlFlexGrid.Controls.Add(this.label95);
            this.pnlFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFlexGrid.Location = new System.Drawing.Point(0, 626);
            this.pnlFlexGrid.Name = "pnlFlexGrid";
            this.pnlFlexGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlFlexGrid.Size = new System.Drawing.Size(723, 0);
            this.pnlFlexGrid.TabIndex = 224;
            this.pnlFlexGrid.TabStop = true;
            // 
            // c1ProviderSettings
            // 
            this.c1ProviderSettings.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ProviderSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ProviderSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ProviderSettings.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProviderSettings.ColumnInfo = resources.GetString("c1ProviderSettings.ColumnInfo");
            this.c1ProviderSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProviderSettings.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ProviderSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProviderSettings.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ProviderSettings.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ProviderSettings.Location = new System.Drawing.Point(1, 4);
            this.c1ProviderSettings.Name = "c1ProviderSettings";
            this.c1ProviderSettings.Rows.Count = 1;
            this.c1ProviderSettings.Rows.DefaultSize = 22;
            this.c1ProviderSettings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProviderSettings.ShowCellLabels = true;
            this.c1ProviderSettings.Size = new System.Drawing.Size(721, 0);
            this.c1ProviderSettings.StyleInfo = resources.GetString("c1ProviderSettings.StyleInfo");
            this.c1ProviderSettings.TabIndex = 193;
            this.c1ProviderSettings.TabStop = false;
            // 
            // label92
            // 
            this.label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label92.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(1, -1);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(721, 1);
            this.label92.TabIndex = 7;
            this.label92.Text = "label1";
            // 
            // label93
            // 
            this.label93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Dock = System.Windows.Forms.DockStyle.Top;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(1, 3);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(721, 1);
            this.label93.TabIndex = 6;
            this.label93.Text = "label1";
            // 
            // label94
            // 
            this.label94.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label94.Dock = System.Windows.Forms.DockStyle.Right;
            this.label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(722, 3);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(1, 0);
            this.label94.TabIndex = 5;
            this.label94.Text = "label4";
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Dock = System.Windows.Forms.DockStyle.Left;
            this.label95.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.Location = new System.Drawing.Point(0, 3);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(1, 0);
            this.label95.TabIndex = 4;
            this.label95.Text = "label4";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.pnlBillingSourceOther);
            this.panel11.Controls.Add(this.chkBillingProviderOtherID);
            this.panel11.Controls.Add(this.label62);
            this.panel11.Controls.Add(this.label63);
            this.panel11.Controls.Add(this.label64);
            this.panel11.Controls.Add(this.cmbBillingProviderSource);
            this.panel11.Controls.Add(this.label65);
            this.panel11.Controls.Add(this.label66);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 546);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel11.Size = new System.Drawing.Size(723, 80);
            this.panel11.TabIndex = 223;
            this.panel11.TabStop = true;
            // 
            // pnlBillingSourceOther
            // 
            this.pnlBillingSourceOther.Controls.Add(this.chkBillingProviderSwap);
            this.pnlBillingSourceOther.Controls.Add(this.cmbBillingProviderSourceOtherIDType);
            this.pnlBillingSourceOther.Controls.Add(this.lblProviderSourceChkSwap);
            this.pnlBillingSourceOther.Controls.Add(this.label91);
            this.pnlBillingSourceOther.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBillingSourceOther.Location = new System.Drawing.Point(1, 51);
            this.pnlBillingSourceOther.Name = "pnlBillingSourceOther";
            this.pnlBillingSourceOther.Size = new System.Drawing.Size(721, 28);
            this.pnlBillingSourceOther.TabIndex = 194;
            // 
            // chkBillingProviderSwap
            // 
            this.chkBillingProviderSwap.AutoSize = true;
            this.chkBillingProviderSwap.Location = new System.Drawing.Point(546, 6);
            this.chkBillingProviderSwap.Name = "chkBillingProviderSwap";
            this.chkBillingProviderSwap.Size = new System.Drawing.Size(15, 14);
            this.chkBillingProviderSwap.TabIndex = 193;
            this.chkBillingProviderSwap.UseVisualStyleBackColor = true;
            // 
            // cmbBillingProviderSourceOtherIDType
            // 
            this.cmbBillingProviderSourceOtherIDType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBillingProviderSourceOtherIDType.DropDownHeight = 105;
            this.cmbBillingProviderSourceOtherIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillingProviderSourceOtherIDType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillingProviderSourceOtherIDType.FormattingEnabled = true;
            this.cmbBillingProviderSourceOtherIDType.IntegralHeight = false;
            this.cmbBillingProviderSourceOtherIDType.Location = new System.Drawing.Point(235, 2);
            this.cmbBillingProviderSourceOtherIDType.Name = "cmbBillingProviderSourceOtherIDType";
            this.cmbBillingProviderSourceOtherIDType.Size = new System.Drawing.Size(304, 22);
            this.cmbBillingProviderSourceOtherIDType.TabIndex = 18;
            this.cmbBillingProviderSourceOtherIDType.SelectedIndexChanged += new System.EventHandler(this.cmbBillingProviderSourceOtherIDType_SelectedIndexChanged);
            this.cmbBillingProviderSourceOtherIDType.MouseEnter += new System.EventHandler(this.cmbBillingProviderSourceOtherIDType_MouseEnter);
            // 
            // lblProviderSourceChkSwap
            // 
            this.lblProviderSourceChkSwap.AutoSize = true;
            this.lblProviderSourceChkSwap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderSourceChkSwap.Location = new System.Drawing.Point(562, 5);
            this.lblProviderSourceChkSwap.Name = "lblProviderSourceChkSwap";
            this.lblProviderSourceChkSwap.Size = new System.Drawing.Size(122, 14);
            this.lblProviderSourceChkSwap.TabIndex = 19;
            this.lblProviderSourceChkSwap.Text = "Swap 33-a with 33-b";
            this.lblProviderSourceChkSwap.Click += new System.EventHandler(this.lblProviderSourceChkSwap_Click);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(136, 6);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(95, 14);
            this.label91.TabIndex = 19;
            this.label91.Text = "Other ID Type :";
            // 
            // chkBillingProviderOtherID
            // 
            this.chkBillingProviderOtherID.AutoSize = true;
            this.chkBillingProviderOtherID.Location = new System.Drawing.Point(235, 34);
            this.chkBillingProviderOtherID.Name = "chkBillingProviderOtherID";
            this.chkBillingProviderOtherID.Size = new System.Drawing.Size(154, 18);
            this.chkBillingProviderOtherID.TabIndex = 193;
            this.chkBillingProviderOtherID.Text = "Other Billing ID Needed";
            this.chkBillingProviderOtherID.UseVisualStyleBackColor = true;
            this.chkBillingProviderOtherID.CheckedChanged += new System.EventHandler(this.chkBillingProviderOtherID_CheckedChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(97, 13);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(134, 14);
            this.label62.TabIndex = 19;
            this.label62.Text = "Billing Provider Source :";
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(1, 79);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(721, 1);
            this.label63.TabIndex = 7;
            this.label63.Text = "label1";
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Top;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(1, 3);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(721, 1);
            this.label64.TabIndex = 6;
            this.label64.Text = "label1";
            // 
            // cmbBillingProviderSource
            // 
            this.cmbBillingProviderSource.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBillingProviderSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillingProviderSource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillingProviderSource.FormattingEnabled = true;
            this.cmbBillingProviderSource.Location = new System.Drawing.Point(235, 9);
            this.cmbBillingProviderSource.Name = "cmbBillingProviderSource";
            this.cmbBillingProviderSource.Size = new System.Drawing.Size(304, 22);
            this.cmbBillingProviderSource.TabIndex = 18;
            this.cmbBillingProviderSource.SelectedIndexChanged += new System.EventHandler(this.cmbBillingProviderSource_SelectedIndexChanged);
            this.cmbBillingProviderSource.MouseEnter += new System.EventHandler(this.cmbBillingProviderSource_MouseEnter);
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Right;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(722, 3);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1, 77);
            this.label65.TabIndex = 5;
            this.label65.Text = "label4";
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Left;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(0, 3);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(1, 77);
            this.label66.TabIndex = 4;
            this.label66.Text = "label4";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label81);
            this.panel13.Controls.Add(this.label82);
            this.panel13.Controls.Add(this.label83);
            this.panel13.Controls.Add(this.label84);
            this.panel13.Controls.Add(this.label85);
            this.panel13.Location = new System.Drawing.Point(231, 516);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel13.Size = new System.Drawing.Size(170, 36);
            this.panel13.TabIndex = 222;
            this.panel13.TabStop = true;
            this.panel13.Visible = false;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Dock = System.Windows.Forms.DockStyle.Top;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Location = new System.Drawing.Point(1, 4);
            this.label81.Name = "label81";
            this.label81.Padding = new System.Windows.Forms.Padding(3);
            this.label81.Size = new System.Drawing.Size(136, 20);
            this.label81.TabIndex = 194;
            this.label81.Text = "General Information";
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label82.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(1, 35);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(168, 1);
            this.label82.TabIndex = 7;
            this.label82.Text = "label1";
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Dock = System.Windows.Forms.DockStyle.Top;
            this.label83.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(1, 3);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(168, 1);
            this.label83.TabIndex = 6;
            this.label83.Text = "label1";
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Right;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(169, 3);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(1, 33);
            this.label84.TabIndex = 5;
            this.label84.Text = "label4";
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Dock = System.Windows.Forms.DockStyle.Left;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(0, 3);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(1, 33);
            this.label85.TabIndex = 4;
            this.label85.Text = "label4";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.chkOtherIDonEDI);
            this.panel9.Controls.Add(this.pnlServiceFacilityOther);
            this.panel9.Controls.Add(this.chkServiceFacOtherID);
            this.panel9.Controls.Add(this.label47);
            this.panel9.Controls.Add(this.label52);
            this.panel9.Controls.Add(this.label53);
            this.panel9.Controls.Add(this.cmbServiceFacilitySource);
            this.panel9.Controls.Add(this.label54);
            this.panel9.Controls.Add(this.label55);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 466);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(723, 80);
            this.panel9.TabIndex = 3;
            this.panel9.TabStop = true;
            // 
            // chkOtherIDonEDI
            // 
            this.chkOtherIDonEDI.AutoSize = true;
            this.chkOtherIDonEDI.Location = new System.Drawing.Point(386, 33);
            this.chkOtherIDonEDI.Name = "chkOtherIDonEDI";
            this.chkOtherIDonEDI.Size = new System.Drawing.Size(115, 18);
            this.chkOtherIDonEDI.TabIndex = 3;
            this.chkOtherIDonEDI.Text = "Other ID on EDI";
            this.chkOtherIDonEDI.UseVisualStyleBackColor = true;
            // 
            // pnlServiceFacilityOther
            // 
            this.pnlServiceFacilityOther.Controls.Add(this.chkServiceFacilitySwap);
            this.pnlServiceFacilityOther.Controls.Add(this.cmbServiceFacilityOtherIDType);
            this.pnlServiceFacilityOther.Controls.Add(this.lblServiceFacChkSwap);
            this.pnlServiceFacilityOther.Controls.Add(this.label72);
            this.pnlServiceFacilityOther.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlServiceFacilityOther.Location = new System.Drawing.Point(1, 51);
            this.pnlServiceFacilityOther.Name = "pnlServiceFacilityOther";
            this.pnlServiceFacilityOther.Size = new System.Drawing.Size(721, 28);
            this.pnlServiceFacilityOther.TabIndex = 3;
            // 
            // chkServiceFacilitySwap
            // 
            this.chkServiceFacilitySwap.AutoSize = true;
            this.chkServiceFacilitySwap.Location = new System.Drawing.Point(546, 6);
            this.chkServiceFacilitySwap.Name = "chkServiceFacilitySwap";
            this.chkServiceFacilitySwap.Size = new System.Drawing.Size(15, 14);
            this.chkServiceFacilitySwap.TabIndex = 2;
            this.chkServiceFacilitySwap.UseVisualStyleBackColor = true;
            // 
            // cmbServiceFacilityOtherIDType
            // 
            this.cmbServiceFacilityOtherIDType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbServiceFacilityOtherIDType.DropDownHeight = 105;
            this.cmbServiceFacilityOtherIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceFacilityOtherIDType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceFacilityOtherIDType.FormattingEnabled = true;
            this.cmbServiceFacilityOtherIDType.IntegralHeight = false;
            this.cmbServiceFacilityOtherIDType.Location = new System.Drawing.Point(235, 2);
            this.cmbServiceFacilityOtherIDType.Name = "cmbServiceFacilityOtherIDType";
            this.cmbServiceFacilityOtherIDType.Size = new System.Drawing.Size(304, 22);
            this.cmbServiceFacilityOtherIDType.TabIndex = 1;
            this.cmbServiceFacilityOtherIDType.SelectedIndexChanged += new System.EventHandler(this.cmbServiceFacilityOtherIDType_SelectedIndexChanged);
            this.cmbServiceFacilityOtherIDType.MouseEnter += new System.EventHandler(this.cmbServiceFacilityOtherIDType_MouseEnter);
            // 
            // lblServiceFacChkSwap
            // 
            this.lblServiceFacChkSwap.AutoSize = true;
            this.lblServiceFacChkSwap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceFacChkSwap.Location = new System.Drawing.Point(562, 5);
            this.lblServiceFacChkSwap.Name = "lblServiceFacChkSwap";
            this.lblServiceFacChkSwap.Size = new System.Drawing.Size(122, 14);
            this.lblServiceFacChkSwap.TabIndex = 3;
            this.lblServiceFacChkSwap.Text = "Swap 32-a with 32-b";
            this.lblServiceFacChkSwap.Click += new System.EventHandler(this.lblServiceFacChkSwap_Click);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(136, 6);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(95, 14);
            this.label72.TabIndex = 19;
            this.label72.Text = "Other ID Type :";
            // 
            // chkServiceFacOtherID
            // 
            this.chkServiceFacOtherID.AutoSize = true;
            this.chkServiceFacOtherID.Location = new System.Drawing.Point(235, 33);
            this.chkServiceFacOtherID.Name = "chkServiceFacOtherID";
            this.chkServiceFacOtherID.Size = new System.Drawing.Size(154, 18);
            this.chkServiceFacOtherID.TabIndex = 2;
            this.chkServiceFacOtherID.Text = "Other Billing ID Needed";
            this.chkServiceFacOtherID.UseVisualStyleBackColor = true;
            this.chkServiceFacOtherID.CheckedChanged += new System.EventHandler(this.chkServiceFacOtherID_CheckedChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(96, 14);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(135, 14);
            this.label47.TabIndex = 19;
            this.label47.Text = "Service Facility Source :";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(1, 79);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(721, 1);
            this.label52.TabIndex = 7;
            this.label52.Text = "label1";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(1, 3);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(721, 1);
            this.label53.TabIndex = 6;
            this.label53.Text = "label1";
            // 
            // cmbServiceFacilitySource
            // 
            this.cmbServiceFacilitySource.BackColor = System.Drawing.SystemColors.Window;
            this.cmbServiceFacilitySource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceFacilitySource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceFacilitySource.FormattingEnabled = true;
            this.cmbServiceFacilitySource.Location = new System.Drawing.Point(235, 9);
            this.cmbServiceFacilitySource.Name = "cmbServiceFacilitySource";
            this.cmbServiceFacilitySource.Size = new System.Drawing.Size(304, 22);
            this.cmbServiceFacilitySource.TabIndex = 1;
            this.cmbServiceFacilitySource.SelectedIndexChanged += new System.EventHandler(this.cmbServiceFacilitySource_SelectedIndexChanged);
            this.cmbServiceFacilitySource.MouseEnter += new System.EventHandler(this.cmbServiceFacilitySource_MouseEnter);
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(722, 3);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 77);
            this.label54.TabIndex = 5;
            this.label54.Text = "label4";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(0, 3);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 77);
            this.label55.TabIndex = 4;
            this.label55.Text = "label4";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.chkIDInBox31);
            this.panel8.Controls.Add(this.lblElectronicRendering);
            this.panel8.Controls.Add(this.cmbReferringProviderOtherIDType);
            this.panel8.Controls.Add(this.label108);
            this.panel8.Controls.Add(this.label48);
            this.panel8.Controls.Add(this.lblPaperRendering);
            this.panel8.Controls.Add(this.label49);
            this.panel8.Controls.Add(this.cmbElectronicRendering);
            this.panel8.Controls.Add(this.cmbPaperRendering);
            this.panel8.Controls.Add(this.label50);
            this.panel8.Controls.Add(this.label51);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 374);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel8.Size = new System.Drawing.Size(723, 92);
            this.panel8.TabIndex = 3;
            this.panel8.TabStop = true;
            // 
            // chkIDInBox31
            // 
            this.chkIDInBox31.AutoSize = true;
            this.chkIDInBox31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIDInBox31.Location = new System.Drawing.Point(547, 10);
            this.chkIDInBox31.Name = "chkIDInBox31";
            this.chkIDInBox31.Size = new System.Drawing.Size(93, 18);
            this.chkIDInBox31.TabIndex = 1;
            this.chkIDInBox31.Text = "ID in Box 31";
            this.chkIDInBox31.UseVisualStyleBackColor = true;
            // 
            // lblElectronicRendering
            // 
            this.lblElectronicRendering.AutoSize = true;
            this.lblElectronicRendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElectronicRendering.Location = new System.Drawing.Point(8, 39);
            this.lblElectronicRendering.Name = "lblElectronicRendering";
            this.lblElectronicRendering.Size = new System.Drawing.Size(223, 14);
            this.lblElectronicRendering.TabIndex = 19;
            this.lblElectronicRendering.Text = "Electronic Rendering Provider ID Type :";
            // 
            // cmbReferringProviderOtherIDType
            // 
            this.cmbReferringProviderOtherIDType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbReferringProviderOtherIDType.DropDownHeight = 105;
            this.cmbReferringProviderOtherIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferringProviderOtherIDType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReferringProviderOtherIDType.FormattingEnabled = true;
            this.cmbReferringProviderOtherIDType.IntegralHeight = false;
            this.cmbReferringProviderOtherIDType.Location = new System.Drawing.Point(235, 64);
            this.cmbReferringProviderOtherIDType.Name = "cmbReferringProviderOtherIDType";
            this.cmbReferringProviderOtherIDType.Size = new System.Drawing.Size(301, 22);
            this.cmbReferringProviderOtherIDType.TabIndex = 3;
            this.cmbReferringProviderOtherIDType.SelectedIndexChanged += new System.EventHandler(this.cmbReferringProviderOtherIDType_SelectedIndexChanged);
            this.cmbReferringProviderOtherIDType.MouseEnter += new System.EventHandler(this.cmbReferringProviderOtherIDType_MouseEnter);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(35, 68);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(196, 14);
            this.label108.TabIndex = 21;
            this.label108.Text = "Referring Provider Other ID Type :";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(1, 91);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(721, 1);
            this.label48.TabIndex = 7;
            this.label48.Text = "label1";
            // 
            // lblPaperRendering
            // 
            this.lblPaperRendering.AutoSize = true;
            this.lblPaperRendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaperRendering.Location = new System.Drawing.Point(30, 12);
            this.lblPaperRendering.Name = "lblPaperRendering";
            this.lblPaperRendering.Size = new System.Drawing.Size(201, 14);
            this.lblPaperRendering.TabIndex = 19;
            this.lblPaperRendering.Text = "Paper Rendering Provider ID Type :";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Top;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(1, 3);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(721, 1);
            this.label49.TabIndex = 6;
            this.label49.Text = "label1";
            // 
            // cmbElectronicRendering
            // 
            this.cmbElectronicRendering.BackColor = System.Drawing.SystemColors.Window;
            this.cmbElectronicRendering.DropDownHeight = 105;
            this.cmbElectronicRendering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbElectronicRendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbElectronicRendering.FormattingEnabled = true;
            this.cmbElectronicRendering.IntegralHeight = false;
            this.cmbElectronicRendering.Location = new System.Drawing.Point(235, 35);
            this.cmbElectronicRendering.Name = "cmbElectronicRendering";
            this.cmbElectronicRendering.Size = new System.Drawing.Size(301, 22);
            this.cmbElectronicRendering.TabIndex = 2;
            this.cmbElectronicRendering.SelectedIndexChanged += new System.EventHandler(this.cmbElectronicRendering_SelectedIndexChanged);
            this.cmbElectronicRendering.MouseEnter += new System.EventHandler(this.cmbElectronicRendering_MouseEnter);
            // 
            // cmbPaperRendering
            // 
            this.cmbPaperRendering.BackColor = System.Drawing.SystemColors.Window;
            this.cmbPaperRendering.DropDownHeight = 105;
            this.cmbPaperRendering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperRendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaperRendering.FormattingEnabled = true;
            this.cmbPaperRendering.IntegralHeight = false;
            this.cmbPaperRendering.Location = new System.Drawing.Point(235, 8);
            this.cmbPaperRendering.Name = "cmbPaperRendering";
            this.cmbPaperRendering.Size = new System.Drawing.Size(301, 22);
            this.cmbPaperRendering.TabIndex = 0;
            this.cmbPaperRendering.SelectedIndexChanged += new System.EventHandler(this.cmbPaperRendering_SelectedIndexChanged);
            this.cmbPaperRendering.MouseEnter += new System.EventHandler(this.cmbPaperRendering_MouseEnter);
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(722, 3);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 89);
            this.label50.TabIndex = 5;
            this.label50.Text = "label4";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(0, 3);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 89);
            this.label51.TabIndex = 4;
            this.label51.Text = "label4";
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.label161);
            this.panel28.Controls.Add(this.label157);
            this.panel28.Controls.Add(this.label160);
            this.panel28.Controls.Add(this.txtBox19DefaultNote);
            this.panel28.Controls.Add(this.label159);
            this.panel28.Controls.Add(this.label158);
            this.panel28.Controls.Add(this.chkRefferingID);
            this.panel28.Controls.Add(this.chkNotesInBox19);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(0, 324);
            this.panel28.Name = "panel28";
            this.panel28.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel28.Size = new System.Drawing.Size(723, 50);
            this.panel28.TabIndex = 2;
            // 
            // label161
            // 
            this.label161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label161.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label161.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label161.Location = new System.Drawing.Point(1, 49);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(721, 1);
            this.label161.TabIndex = 8;
            this.label161.Text = "label1";
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(35, 14);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(126, 14);
            this.label157.TabIndex = 192;
            this.label157.Text = "Default Box-19 note :";
            // 
            // label160
            // 
            this.label160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label160.Dock = System.Windows.Forms.DockStyle.Top;
            this.label160.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label160.Location = new System.Drawing.Point(1, 3);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(721, 1);
            this.label160.TabIndex = 7;
            this.label160.Text = "label1";
            // 
            // txtBox19DefaultNote
            // 
            this.txtBox19DefaultNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox19DefaultNote.Location = new System.Drawing.Point(166, 9);
            this.txtBox19DefaultNote.MaxLength = 160;
            this.txtBox19DefaultNote.Multiline = true;
            this.txtBox19DefaultNote.Name = "txtBox19DefaultNote";
            this.txtBox19DefaultNote.Size = new System.Drawing.Size(370, 35);
            this.txtBox19DefaultNote.TabIndex = 0;
            // 
            // label159
            // 
            this.label159.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label159.Dock = System.Windows.Forms.DockStyle.Right;
            this.label159.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label159.Location = new System.Drawing.Point(722, 3);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(1, 47);
            this.label159.TabIndex = 6;
            this.label159.Text = "label4";
            // 
            // label158
            // 
            this.label158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label158.Dock = System.Windows.Forms.DockStyle.Left;
            this.label158.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label158.Location = new System.Drawing.Point(0, 3);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(1, 47);
            this.label158.TabIndex = 5;
            this.label158.Text = "label4";
            // 
            // chkRefferingID
            // 
            this.chkRefferingID.AutoSize = true;
            this.chkRefferingID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRefferingID.Location = new System.Drawing.Point(547, 28);
            this.chkRefferingID.Name = "chkRefferingID";
            this.chkRefferingID.Size = new System.Drawing.Size(152, 18);
            this.chkRefferingID.TabIndex = 2;
            this.chkRefferingID.Text = "Referring ID In Box 19 ";
            this.chkRefferingID.UseVisualStyleBackColor = true;
            this.chkRefferingID.Visible = false;
            this.chkRefferingID.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkNotesInBox19
            // 
            this.chkNotesInBox19.AutoSize = true;
            this.chkNotesInBox19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNotesInBox19.Location = new System.Drawing.Point(547, 9);
            this.chkNotesInBox19.Name = "chkNotesInBox19";
            this.chkNotesInBox19.Size = new System.Drawing.Size(113, 18);
            this.chkNotesInBox19.TabIndex = 1;
            this.chkNotesInBox19.Text = "Notes in Box 19";
            this.chkNotesInBox19.UseVisualStyleBackColor = true;
            this.chkNotesInBox19.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.chkGroupMandatory);
            this.panel10.Controls.Add(this.chkSecondaryPayerAddress);
            this.panel10.Controls.Add(this.chkReportClinicName);
            this.panel10.Controls.Add(this.label164);
            this.panel10.Controls.Add(this.cmbCMSDateFormat);
            this.panel10.Controls.Add(this.label163);
            this.panel10.Controls.Add(this.chkEdiAltPayerID);
            this.panel10.Controls.Add(this.label162);
            this.panel10.Controls.Add(this.cmbBox11bSettings);
            this.panel10.Controls.Add(this.groupBox5);
            this.panel10.Controls.Add(this.cmbRefSecIdentification);
            this.panel10.Controls.Add(this.chkClaimFreq);
            this.panel10.Controls.Add(this.chkMedClaimRef);
            this.panel10.Controls.Add(this.chkWorkersComp);
            this.panel10.Controls.Add(this.chkShowClaim);
            this.panel10.Controls.Add(this.ChkEMGAsX);
            this.panel10.Controls.Add(this.chkSwap1a9a1aMCare);
            this.panel10.Controls.Add(this.chkPaperDisplayMailingAddress);
            this.panel10.Controls.Add(this.chkIncludePlanName);
            this.panel10.Controls.Add(this.txtBillClaimOfficeNo);
            this.panel10.Controls.Add(this.cmbClIAPostn);
            this.panel10.Controls.Add(this.label106);
            this.panel10.Controls.Add(this.label5);
            this.panel10.Controls.Add(this.panel2);
            this.panel10.Controls.Add(this.label2);
            this.panel10.Controls.Add(this.cmbBox29);
            this.panel10.Controls.Add(this.cmbBox30);
            this.panel10.Controls.Add(this.Box29);
            this.panel10.Controls.Add(this.Box30);
            this.panel10.Controls.Add(this.TxtDiagnosisperClaim);
            this.panel10.Controls.Add(this.TxtChargesperClaim);
            this.panel10.Controls.Add(this.lblDiagnosisperClaim);
            this.panel10.Controls.Add(this.lblChargeperClaim);
            this.panel10.Controls.Add(this.cmbClearingHouse);
            this.panel10.Controls.Add(this.cmbTypeOFBilling);
            this.panel10.Controls.Add(this.label75);
            this.panel10.Controls.Add(this.chkCorrectRplmnt);
            this.panel10.Controls.Add(this.label57);
            this.panel10.Controls.Add(this.label58);
            this.panel10.Controls.Add(this.lblCptCrosswalk);
            this.panel10.Controls.Add(this.label59);
            this.panel10.Controls.Add(this.label60);
            this.panel10.Controls.Add(this.label7);
            this.panel10.Controls.Add(this.cmbFeeSchedules);
            this.panel10.Controls.Add(this.cmbCptCrosswalk);
            this.panel10.Controls.Add(this.chkIncludeOTAFAmount);
            this.panel10.Controls.Add(this.cmbdonotprintfacility);
            this.panel10.Controls.Add(this.label28);
            this.panel10.Controls.Add(this.label21);
            this.panel10.Controls.Add(this.chkAcceptAssignment);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 26);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(723, 298);
            this.panel10.TabIndex = 1;
            this.panel10.TabStop = true;
            // 
            // chkSecondaryPayerAddress
            // 
            this.chkSecondaryPayerAddress.AutoSize = true;
            this.chkSecondaryPayerAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecondaryPayerAddress.Location = new System.Drawing.Point(366, 166);
            this.chkSecondaryPayerAddress.Name = "chkSecondaryPayerAddress";
            this.chkSecondaryPayerAddress.Size = new System.Drawing.Size(208, 18);
            this.chkSecondaryPayerAddress.TabIndex = 23;
            this.chkSecondaryPayerAddress.Text = "Include Secondary Payer Address";
            this.chkSecondaryPayerAddress.UseVisualStyleBackColor = true;
            // 
            // chkReportClinicName
            // 
            this.chkReportClinicName.AutoSize = true;
            this.chkReportClinicName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReportClinicName.Location = new System.Drawing.Point(562, 245);
            this.chkReportClinicName.Name = "chkReportClinicName";
            this.chkReportClinicName.Size = new System.Drawing.Size(142, 18);
            this.chkReportClinicName.TabIndex = 28;
            this.chkReportClinicName.Text = "Clinic Name in Box 31";
            this.chkReportClinicName.UseVisualStyleBackColor = true;
            // 
            // label164
            // 
            this.label164.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label164.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label164.Location = new System.Drawing.Point(356, 0);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(1, 309);
            this.label164.TabIndex = 201;
            this.label164.Text = "label4";
            // 
            // cmbCMSDateFormat
            // 
            this.cmbCMSDateFormat.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCMSDateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCMSDateFormat.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmbCMSDateFormat.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCMSDateFormat.FormattingEnabled = true;
            this.cmbCMSDateFormat.Items.AddRange(new object[] {
            "YYYY",
            "YY"});
            this.cmbCMSDateFormat.Location = new System.Drawing.Point(540, 267);
            this.cmbCMSDateFormat.Name = "cmbCMSDateFormat";
            this.cmbCMSDateFormat.Size = new System.Drawing.Size(59, 22);
            this.cmbCMSDateFormat.TabIndex = 29;
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label163.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label163.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label163.Location = new System.Drawing.Point(360, 271);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(179, 14);
            this.label163.TabIndex = 200;
            this.label163.Text = "CMS 1500 02/12 Date Format :";
            // 
            // chkEdiAltPayerID
            // 
            this.chkEdiAltPayerID.AutoSize = true;
            this.chkEdiAltPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEdiAltPayerID.Location = new System.Drawing.Point(366, 144);
            this.chkEdiAltPayerID.Name = "chkEdiAltPayerID";
            this.chkEdiAltPayerID.Size = new System.Drawing.Size(272, 18);
            this.chkEdiAltPayerID.TabIndex = 22;
            this.chkEdiAltPayerID.Text = "Include EDI Alt. Payer ID on Secondary Claim";
            this.chkEdiAltPayerID.UseVisualStyleBackColor = true;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.Location = new System.Drawing.Point(94, 267);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(64, 14);
            this.label162.TabIndex = 192;
            this.label162.Text = "Box 11 b :";
            // 
            // cmbBox11bSettings
            // 
            this.cmbBox11bSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox11bSettings.FormattingEnabled = true;
            this.cmbBox11bSettings.Location = new System.Drawing.Point(164, 263);
            this.cmbBox11bSettings.Name = "cmbBox11bSettings";
            this.cmbBox11bSettings.Size = new System.Drawing.Size(183, 22);
            this.cmbBox11bSettings.TabIndex = 10;
            // 
            // groupBox5
            // 
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox5.Location = new System.Drawing.Point(646, 267);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(31, 22);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Paper Billing Rules";
            this.groupBox5.Visible = false;
            // 
            // cmbRefSecIdentification
            // 
            this.cmbRefSecIdentification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefSecIdentification.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRefSecIdentification.FormattingEnabled = true;
            this.cmbRefSecIdentification.Location = new System.Drawing.Point(600, 215);
            this.cmbRefSecIdentification.Name = "cmbRefSecIdentification";
            this.cmbRefSecIdentification.Size = new System.Drawing.Size(118, 22);
            this.cmbRefSecIdentification.TabIndex = 26;
            // 
            // chkClaimFreq
            // 
            this.chkClaimFreq.AutoSize = true;
            this.chkClaimFreq.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClaimFreq.Location = new System.Drawing.Point(562, 100);
            this.chkClaimFreq.Name = "chkClaimFreq";
            this.chkClaimFreq.Size = new System.Drawing.Size(139, 18);
            this.chkClaimFreq.TabIndex = 20;
            this.chkClaimFreq.Text = "Claim Freq Always ‘1’";
            this.chkClaimFreq.UseVisualStyleBackColor = true;
            // 
            // chkMedClaimRef
            // 
            this.chkMedClaimRef.AutoSize = true;
            this.chkMedClaimRef.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMedClaimRef.Location = new System.Drawing.Point(366, 122);
            this.chkMedClaimRef.Name = "chkMedClaimRef";
            this.chkMedClaimRef.Size = new System.Drawing.Size(337, 18);
            this.chkMedClaimRef.TabIndex = 21;
            this.chkMedClaimRef.Text = "Include Medicare Claim Reference # on Secondary Claims";
            this.chkMedClaimRef.UseVisualStyleBackColor = true;
            this.chkMedClaimRef.CheckedChanged += new System.EventHandler(this.chkWorkersComp_CheckedChanged);
            // 
            // chkWorkersComp
            // 
            this.chkWorkersComp.AutoSize = true;
            this.chkWorkersComp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkersComp.Location = new System.Drawing.Point(366, 100);
            this.chkWorkersComp.Name = "chkWorkersComp";
            this.chkWorkersComp.Size = new System.Drawing.Size(109, 18);
            this.chkWorkersComp.TabIndex = 19;
            this.chkWorkersComp.Text = "Worker’s Comp";
            this.chkWorkersComp.UseVisualStyleBackColor = true;
            this.chkWorkersComp.CheckedChanged += new System.EventHandler(this.chkWorkersComp_CheckedChanged);
            // 
            // chkShowClaim
            // 
            this.chkShowClaim.AutoSize = true;
            this.chkShowClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowClaim.Location = new System.Drawing.Point(562, 78);
            this.chkShowClaim.Name = "chkShowClaim";
            this.chkShowClaim.Size = new System.Drawing.Size(115, 18);
            this.chkShowClaim.TabIndex = 18;
            this.chkShowClaim.Text = "Box 1a - Claim #";
            this.chkShowClaim.UseVisualStyleBackColor = true;
            // 
            // ChkEMGAsX
            // 
            this.ChkEMGAsX.AutoSize = true;
            this.ChkEMGAsX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEMGAsX.Location = new System.Drawing.Point(366, 78);
            this.ChkEMGAsX.Name = "ChkEMGAsX";
            this.ChkEMGAsX.Size = new System.Drawing.Size(139, 18);
            this.ChkEMGAsX.TabIndex = 17;
            this.ChkEMGAsX.Text = "Box 24C - EMG as ‘X\'";
            this.ChkEMGAsX.UseVisualStyleBackColor = true;
            // 
            // chkSwap1a9a1aMCare
            // 
            this.chkSwap1a9a1aMCare.AutoSize = true;
            this.chkSwap1a9a1aMCare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSwap1a9a1aMCare.Location = new System.Drawing.Point(562, 56);
            this.chkSwap1a9a1aMCare.Name = "chkSwap1a9a1aMCare";
            this.chkSwap1a9a1aMCare.Size = new System.Drawing.Size(136, 18);
            this.chkSwap1a9a1aMCare.TabIndex = 16;
            this.chkSwap1a9a1aMCare.Text = "Swap 1a9a;1aMCare";
            this.chkSwap1a9a1aMCare.UseVisualStyleBackColor = true;
            // 
            // chkPaperDisplayMailingAddress
            // 
            this.chkPaperDisplayMailingAddress.AutoSize = true;
            this.chkPaperDisplayMailingAddress.Checked = true;
            this.chkPaperDisplayMailingAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaperDisplayMailingAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPaperDisplayMailingAddress.Location = new System.Drawing.Point(366, 56);
            this.chkPaperDisplayMailingAddress.Name = "chkPaperDisplayMailingAddress";
            this.chkPaperDisplayMailingAddress.Size = new System.Drawing.Size(191, 18);
            this.chkPaperDisplayMailingAddress.TabIndex = 15;
            this.chkPaperDisplayMailingAddress.Text = "Paper - Display Mailing Address";
            this.chkPaperDisplayMailingAddress.UseVisualStyleBackColor = true;
            // 
            // chkIncludePlanName
            // 
            this.chkIncludePlanName.AutoSize = true;
            this.chkIncludePlanName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludePlanName.Location = new System.Drawing.Point(366, 34);
            this.chkIncludePlanName.Name = "chkIncludePlanName";
            this.chkIncludePlanName.Size = new System.Drawing.Size(166, 18);
            this.chkIncludePlanName.TabIndex = 13;
            this.chkIncludePlanName.Text = "Report Plan Name for EDI";
            this.chkIncludePlanName.UseVisualStyleBackColor = true;
            // 
            // txtBillClaimOfficeNo
            // 
            this.txtBillClaimOfficeNo.Location = new System.Drawing.Point(470, 215);
            this.txtBillClaimOfficeNo.MaxLength = 30;
            this.txtBillClaimOfficeNo.Name = "txtBillClaimOfficeNo";
            this.txtBillClaimOfficeNo.Size = new System.Drawing.Size(129, 22);
            this.txtBillClaimOfficeNo.TabIndex = 25;
            // 
            // cmbClIAPostn
            // 
            this.cmbClIAPostn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClIAPostn.FormattingEnabled = true;
            this.cmbClIAPostn.Location = new System.Drawing.Point(470, 241);
            this.cmbClIAPostn.Name = "cmbClIAPostn";
            this.cmbClIAPostn.Size = new System.Drawing.Size(70, 22);
            this.cmbClIAPostn.TabIndex = 27;
            // 
            // label106
            // 
            this.label106.Location = new System.Drawing.Point(362, 219);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(107, 14);
            this.label106.TabIndex = 183;
            this.label106.Text = "EDI Alt. Payer ID :";
            this.label106.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(366, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 14);
            this.label5.TabIndex = 184;
            this.label5.Text = "CLIA # Location :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbStatementToPatientYes);
            this.panel2.Controls.Add(this.rbStatementToPatientNo);
            this.panel2.Location = new System.Drawing.Point(502, 190);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(103, 24);
            this.panel2.TabIndex = 24;
            this.panel2.TabStop = true;
            // 
            // rbStatementToPatientYes
            // 
            this.rbStatementToPatientYes.AutoSize = true;
            this.rbStatementToPatientYes.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbStatementToPatientYes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStatementToPatientYes.Location = new System.Drawing.Point(0, 0);
            this.rbStatementToPatientYes.Name = "rbStatementToPatientYes";
            this.rbStatementToPatientYes.Size = new System.Drawing.Size(45, 24);
            this.rbStatementToPatientYes.TabIndex = 1;
            this.rbStatementToPatientYes.Text = "Yes";
            this.rbStatementToPatientYes.UseVisualStyleBackColor = true;
            // 
            // rbStatementToPatientNo
            // 
            this.rbStatementToPatientNo.AutoSize = true;
            this.rbStatementToPatientNo.Checked = true;
            this.rbStatementToPatientNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbStatementToPatientNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStatementToPatientNo.Location = new System.Drawing.Point(63, 0);
            this.rbStatementToPatientNo.Name = "rbStatementToPatientNo";
            this.rbStatementToPatientNo.Size = new System.Drawing.Size(40, 24);
            this.rbStatementToPatientNo.TabIndex = 0;
            this.rbStatementToPatientNo.TabStop = true;
            this.rbStatementToPatientNo.Text = "No";
            this.rbStatementToPatientNo.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(366, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 14);
            this.label2.TabIndex = 182;
            this.label2.Text = "Statement to Patient :";
            // 
            // cmbBox29
            // 
            this.cmbBox29.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox29.FormattingEnabled = true;
            this.cmbBox29.Location = new System.Drawing.Point(164, 207);
            this.cmbBox29.Name = "cmbBox29";
            this.cmbBox29.Size = new System.Drawing.Size(183, 22);
            this.cmbBox29.TabIndex = 8;
            this.cmbBox29.MouseEnter += new System.EventHandler(this.cmbBox29_MouseEnter);
            // 
            // cmbBox30
            // 
            this.cmbBox30.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox30.FormattingEnabled = true;
            this.cmbBox30.Location = new System.Drawing.Point(164, 235);
            this.cmbBox30.Name = "cmbBox30";
            this.cmbBox30.Size = new System.Drawing.Size(183, 22);
            this.cmbBox30.TabIndex = 9;
            // 
            // Box29
            // 
            this.Box29.AutoSize = true;
            this.Box29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Box29.Location = new System.Drawing.Point(108, 211);
            this.Box29.Name = "Box29";
            this.Box29.Size = new System.Drawing.Size(53, 14);
            this.Box29.TabIndex = 0;
            this.Box29.Text = "Box 29 :";
            // 
            // Box30
            // 
            this.Box30.AutoSize = true;
            this.Box30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Box30.Location = new System.Drawing.Point(108, 239);
            this.Box30.Name = "Box30";
            this.Box30.Size = new System.Drawing.Size(53, 14);
            this.Box30.TabIndex = 1;
            this.Box30.Text = "Box 30 :";
            // 
            // TxtDiagnosisperClaim
            // 
            this.TxtDiagnosisperClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiagnosisperClaim.Location = new System.Drawing.Point(164, 179);
            this.TxtDiagnosisperClaim.MaxLength = 5;
            this.TxtDiagnosisperClaim.Name = "TxtDiagnosisperClaim";
            this.TxtDiagnosisperClaim.ShortcutsEnabled = false;
            this.TxtDiagnosisperClaim.Size = new System.Drawing.Size(80, 22);
            this.TxtDiagnosisperClaim.TabIndex = 7;
            this.TxtDiagnosisperClaim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDiagnosisperClaim_KeyDown);
            this.TxtDiagnosisperClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiagnosisperClaim_KeyPress);
            this.TxtDiagnosisperClaim.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDiagnosisperClaim_Validating);
            // 
            // TxtChargesperClaim
            // 
            this.TxtChargesperClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChargesperClaim.Location = new System.Drawing.Point(164, 151);
            this.TxtChargesperClaim.MaxLength = 5;
            this.TxtChargesperClaim.Name = "TxtChargesperClaim";
            this.TxtChargesperClaim.ShortcutsEnabled = false;
            this.TxtChargesperClaim.Size = new System.Drawing.Size(80, 22);
            this.TxtChargesperClaim.TabIndex = 6;
            this.TxtChargesperClaim.Tag = "";
            this.TxtChargesperClaim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChargesperClaim_KeyDown);
            this.TxtChargesperClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtChargesperClaim_KeyPress);
            this.TxtChargesperClaim.Validating += new System.ComponentModel.CancelEventHandler(this.TxtChargesperClaim_Validating);
            // 
            // lblDiagnosisperClaim
            // 
            this.lblDiagnosisperClaim.AutoSize = true;
            this.lblDiagnosisperClaim.Location = new System.Drawing.Point(14, 183);
            this.lblDiagnosisperClaim.Name = "lblDiagnosisperClaim";
            this.lblDiagnosisperClaim.Size = new System.Drawing.Size(147, 14);
            this.lblDiagnosisperClaim.TabIndex = 172;
            this.lblDiagnosisperClaim.Text = "Max Diagnoses Per Claim :";
            this.lblDiagnosisperClaim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblChargeperClaim
            // 
            this.lblChargeperClaim.AutoSize = true;
            this.lblChargeperClaim.Location = new System.Drawing.Point(25, 155);
            this.lblChargeperClaim.Name = "lblChargeperClaim";
            this.lblChargeperClaim.Size = new System.Drawing.Size(136, 14);
            this.lblChargeperClaim.TabIndex = 171;
            this.lblChargeperClaim.Text = "Max Charges Per Claim :";
            this.lblChargeperClaim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbClearingHouse
            // 
            this.cmbClearingHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClearingHouse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClearingHouse.FormattingEnabled = true;
            this.cmbClearingHouse.Location = new System.Drawing.Point(164, 123);
            this.cmbClearingHouse.Name = "cmbClearingHouse";
            this.cmbClearingHouse.Size = new System.Drawing.Size(183, 22);
            this.cmbClearingHouse.TabIndex = 5;
            this.cmbClearingHouse.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            this.cmbClearingHouse.MouseEnter += new System.EventHandler(this.cmbClearingHouse_MouseEnter);
            // 
            // cmbTypeOFBilling
            // 
            this.cmbTypeOFBilling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeOFBilling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeOFBilling.FormattingEnabled = true;
            this.cmbTypeOFBilling.Items.AddRange(new object[] {
            ""});
            this.cmbTypeOFBilling.Location = new System.Drawing.Point(164, 39);
            this.cmbTypeOFBilling.Name = "cmbTypeOFBilling";
            this.cmbTypeOFBilling.Size = new System.Drawing.Size(183, 22);
            this.cmbTypeOFBilling.TabIndex = 2;
            this.cmbTypeOFBilling.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(28, 43);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(133, 14);
            this.label75.TabIndex = 136;
            this.label75.Text = "Default Billing Method :";
            // 
            // chkCorrectRplmnt
            // 
            this.chkCorrectRplmnt.AutoSize = true;
            this.chkCorrectRplmnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCorrectRplmnt.Location = new System.Drawing.Point(562, 34);
            this.chkCorrectRplmnt.Name = "chkCorrectRplmnt";
            this.chkCorrectRplmnt.Size = new System.Drawing.Size(128, 18);
            this.chkCorrectRplmnt.TabIndex = 14;
            this.chkCorrectRplmnt.Text = "Replacement Claim";
            this.chkCorrectRplmnt.UseVisualStyleBackColor = true;
            this.chkCorrectRplmnt.Leave += new System.EventHandler(this.chkCorrectRplmnt_Leave);
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(1, 297);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(721, 1);
            this.label57.TabIndex = 135;
            this.label57.Text = "label1";
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Top;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(1, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(721, 1);
            this.label58.TabIndex = 0;
            this.label58.Text = "label1";
            // 
            // lblCptCrosswalk
            // 
            this.lblCptCrosswalk.AutoSize = true;
            this.lblCptCrosswalk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCptCrosswalk.Location = new System.Drawing.Point(68, 71);
            this.lblCptCrosswalk.Name = "lblCptCrosswalk";
            this.lblCptCrosswalk.Size = new System.Drawing.Size(93, 14);
            this.lblCptCrosswalk.TabIndex = 116;
            this.lblCptCrosswalk.Text = "CPT Crosswalk :";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Right;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(722, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 298);
            this.label59.TabIndex = 5;
            this.label59.Text = "label4";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(0, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 298);
            this.label60.TabIndex = 4;
            this.label60.Text = "label4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(66, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 14);
            this.label7.TabIndex = 142;
            this.label7.Text = "Clearing House :";
            // 
            // cmbFeeSchedules
            // 
            this.cmbFeeSchedules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFeeSchedules.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFeeSchedules.FormattingEnabled = true;
            this.cmbFeeSchedules.ItemHeight = 14;
            this.cmbFeeSchedules.Location = new System.Drawing.Point(164, 11);
            this.cmbFeeSchedules.Name = "cmbFeeSchedules";
            this.cmbFeeSchedules.Size = new System.Drawing.Size(183, 22);
            this.cmbFeeSchedules.TabIndex = 1;
            this.cmbFeeSchedules.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            this.cmbFeeSchedules.MouseEnter += new System.EventHandler(this.cmbFeeSchedules_MouseEnter);
            // 
            // cmbCptCrosswalk
            // 
            this.cmbCptCrosswalk.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCptCrosswalk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCptCrosswalk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCptCrosswalk.FormattingEnabled = true;
            this.cmbCptCrosswalk.ItemHeight = 14;
            this.cmbCptCrosswalk.Location = new System.Drawing.Point(164, 67);
            this.cmbCptCrosswalk.Name = "cmbCptCrosswalk";
            this.cmbCptCrosswalk.Size = new System.Drawing.Size(183, 22);
            this.cmbCptCrosswalk.TabIndex = 3;
            this.cmbCptCrosswalk.SelectionChangeCommitted += new System.EventHandler(this.cmbCptCrosswalk_SelectionChangeCommitted);
            this.cmbCptCrosswalk.MouseEnter += new System.EventHandler(this.cmbCptCrosswalk_MouseEnter);
            // 
            // chkIncludeOTAFAmount
            // 
            this.chkIncludeOTAFAmount.AutoSize = true;
            this.chkIncludeOTAFAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeOTAFAmount.Location = new System.Drawing.Point(562, 12);
            this.chkIncludeOTAFAmount.Name = "chkIncludeOTAFAmount";
            this.chkIncludeOTAFAmount.Size = new System.Drawing.Size(149, 18);
            this.chkIncludeOTAFAmount.TabIndex = 12;
            this.chkIncludeOTAFAmount.Text = "Include OTAF Amount";
            this.chkIncludeOTAFAmount.UseVisualStyleBackColor = true;
            this.chkIncludeOTAFAmount.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // cmbdonotprintfacility
            // 
            this.cmbdonotprintfacility.BackColor = System.Drawing.SystemColors.Window;
            this.cmbdonotprintfacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdonotprintfacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbdonotprintfacility.FormattingEnabled = true;
            this.cmbdonotprintfacility.ItemHeight = 14;
            this.cmbdonotprintfacility.Location = new System.Drawing.Point(164, 95);
            this.cmbdonotprintfacility.Name = "cmbdonotprintfacility";
            this.cmbdonotprintfacility.Size = new System.Drawing.Size(183, 22);
            this.cmbdonotprintfacility.TabIndex = 4;
            this.cmbdonotprintfacility.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(10, 99);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(151, 14);
            this.label28.TabIndex = 55;
            this.label28.Text = "Include POS 11 Facilities  :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(154, 14);
            this.label21.TabIndex = 140;
            this.label21.Text = "Reimbursement Schedule :";
            // 
            // chkAcceptAssignment
            // 
            this.chkAcceptAssignment.AutoSize = true;
            this.chkAcceptAssignment.Checked = true;
            this.chkAcceptAssignment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAcceptAssignment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcceptAssignment.Location = new System.Drawing.Point(366, 12);
            this.chkAcceptAssignment.Name = "chkAcceptAssignment";
            this.chkAcceptAssignment.Size = new System.Drawing.Size(136, 18);
            this.chkAcceptAssignment.TabIndex = 11;
            this.chkAcceptAssignment.Text = "Accept Assignment ";
            this.chkAcceptAssignment.UseVisualStyleBackColor = true;
            // 
            // pnlBillingHeader
            // 
            this.pnlBillingHeader.Controls.Add(this.pnlHeader);
            this.pnlBillingHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBillingHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBillingHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBillingHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlBillingHeader.Name = "pnlBillingHeader";
            this.pnlBillingHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlBillingHeader.Size = new System.Drawing.Size(723, 26);
            this.pnlBillingHeader.TabIndex = 1;
            this.pnlBillingHeader.Visible = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.lblBillingSettings);
            this.pnlHeader.Controls.Add(this.lblLeftAlign);
            this.pnlHeader.Controls.Add(this.lblRightAlign);
            this.pnlHeader.Controls.Add(this.lblTopAlign);
            this.pnlHeader.Controls.Add(this.lblBottomAlign);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(723, 23);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblBillingSettings
            // 
            this.lblBillingSettings.AutoSize = true;
            this.lblBillingSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillingSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBillingSettings.Location = new System.Drawing.Point(5, 5);
            this.lblBillingSettings.Name = "lblBillingSettings";
            this.lblBillingSettings.Size = new System.Drawing.Size(99, 14);
            this.lblBillingSettings.TabIndex = 190;
            this.lblBillingSettings.Text = "Billing Settings";
            this.lblBillingSettings.Visible = false;
            // 
            // lblLeftAlign
            // 
            this.lblLeftAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLeftAlign.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeftAlign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeftAlign.Location = new System.Drawing.Point(0, 1);
            this.lblLeftAlign.Name = "lblLeftAlign";
            this.lblLeftAlign.Size = new System.Drawing.Size(1, 21);
            this.lblLeftAlign.TabIndex = 7;
            this.lblLeftAlign.Text = "label4";
            // 
            // lblRightAlign
            // 
            this.lblRightAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRightAlign.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRightAlign.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRightAlign.Location = new System.Drawing.Point(722, 1);
            this.lblRightAlign.Name = "lblRightAlign";
            this.lblRightAlign.Size = new System.Drawing.Size(1, 21);
            this.lblRightAlign.TabIndex = 6;
            this.lblRightAlign.Text = "label3";
            // 
            // lblTopAlign
            // 
            this.lblTopAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTopAlign.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopAlign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTopAlign.Location = new System.Drawing.Point(0, 0);
            this.lblTopAlign.Name = "lblTopAlign";
            this.lblTopAlign.Size = new System.Drawing.Size(723, 1);
            this.lblTopAlign.TabIndex = 5;
            this.lblTopAlign.Text = "label1";
            // 
            // lblBottomAlign
            // 
            this.lblBottomAlign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBottomAlign.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottomAlign.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBottomAlign.Location = new System.Drawing.Point(0, 22);
            this.lblBottomAlign.Name = "lblBottomAlign";
            this.lblBottomAlign.Size = new System.Drawing.Size(723, 1);
            this.lblBottomAlign.TabIndex = 8;
            this.lblBottomAlign.Text = "label2";
            // 
            // tbp_MidLevel
            // 
            this.tbp_MidLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbp_MidLevel.Controls.Add(this.panel3);
            this.tbp_MidLevel.Controls.Add(this.panel6);
            this.tbp_MidLevel.Controls.Add(this.panel4);
            this.tbp_MidLevel.Location = new System.Drawing.Point(4, 42);
            this.tbp_MidLevel.Name = "tbp_MidLevel";
            this.tbp_MidLevel.Size = new System.Drawing.Size(723, 467);
            this.tbp_MidLevel.TabIndex = 1;
            this.tbp_MidLevel.Text = "Mid-Level";
            this.tbp_MidLevel.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.c1MidlevelSettings);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel3.Size = new System.Drawing.Size(723, 409);
            this.panel3.TabIndex = 2;
            this.panel3.TabStop = true;
            // 
            // c1MidlevelSettings
            // 
            this.c1MidlevelSettings.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1MidlevelSettings.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1MidlevelSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1MidlevelSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1MidlevelSettings.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1MidlevelSettings.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1MidlevelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1MidlevelSettings.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1MidlevelSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1MidlevelSettings.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1MidlevelSettings.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1MidlevelSettings.Location = new System.Drawing.Point(1, 4);
            this.c1MidlevelSettings.Name = "c1MidlevelSettings";
            this.c1MidlevelSettings.Rows.Count = 1;
            this.c1MidlevelSettings.Rows.DefaultSize = 22;
            this.c1MidlevelSettings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MidlevelSettings.ShowCellLabels = true;
            this.c1MidlevelSettings.Size = new System.Drawing.Size(721, 404);
            this.c1MidlevelSettings.StyleInfo = resources.GetString("c1MidlevelSettings.StyleInfo");
            this.c1MidlevelSettings.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 408);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(721, 1);
            this.label14.TabIndex = 7;
            this.label14.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(721, 1);
            this.label13.TabIndex = 6;
            this.label13.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(722, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 406);
            this.label12.TabIndex = 5;
            this.label12.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 406);
            this.label10.TabIndex = 4;
            this.label10.Text = "label4";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.cmbMidLevelSpeProvider);
            this.panel6.Controls.Add(this.label41);
            this.panel6.Controls.Add(this.label42);
            this.panel6.Controls.Add(this.label43);
            this.panel6.Controls.Add(this.label44);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(0, 24);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel6.Size = new System.Drawing.Size(723, 34);
            this.panel6.TabIndex = 1;
            // 
            // cmbMidLevelSpeProvider
            // 
            this.cmbMidLevelSpeProvider.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMidLevelSpeProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMidLevelSpeProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMidLevelSpeProvider.FormattingEnabled = true;
            this.cmbMidLevelSpeProvider.Location = new System.Drawing.Point(275, 7);
            this.cmbMidLevelSpeProvider.Name = "cmbMidLevelSpeProvider";
            this.cmbMidLevelSpeProvider.Size = new System.Drawing.Size(297, 22);
            this.cmbMidLevelSpeProvider.TabIndex = 191;
            this.cmbMidLevelSpeProvider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbMidLevelSpeProvider_MouseMove);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Location = new System.Drawing.Point(8, 11);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(265, 14);
            this.label41.TabIndex = 190;
            this.label41.Text = "Report Rendering Provider on Insurance Claim :";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Left;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(0, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 29);
            this.label42.TabIndex = 7;
            this.label42.Text = "label4";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label43.Location = new System.Drawing.Point(722, 4);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 29);
            this.label43.TabIndex = 6;
            this.label43.Text = "label3";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(0, 3);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(723, 1);
            this.label44.TabIndex = 5;
            this.label44.Text = "label1";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label45.Location = new System.Drawing.Point(0, 33);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(723, 1);
            this.label45.TabIndex = 8;
            this.label45.Text = "label2";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(723, 24);
            this.panel4.TabIndex = 219;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(723, 24);
            this.panel5.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(6, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 14);
            this.label16.TabIndex = 190;
            this.label16.Text = "Mid-Level Settings";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 22);
            this.label17.TabIndex = 7;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(722, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 22);
            this.label18.TabIndex = 6;
            this.label18.Text = "label3";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(723, 1);
            this.label19.TabIndex = 5;
            this.label19.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label20.Location = new System.Drawing.Point(0, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(723, 1);
            this.label20.TabIndex = 8;
            this.label20.Text = "label2";
            // 
            // tbp_InsurancePlan
            // 
            this.tbp_InsurancePlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbp_InsurancePlan.Controls.Add(this.pnl_Base);
            this.tbp_InsurancePlan.Controls.Add(this.pnlHoldMessage);
            this.tbp_InsurancePlan.Location = new System.Drawing.Point(4, 42);
            this.tbp_InsurancePlan.Name = "tbp_InsurancePlan";
            this.tbp_InsurancePlan.Size = new System.Drawing.Size(723, 467);
            this.tbp_InsurancePlan.TabIndex = 0;
            this.tbp_InsurancePlan.Text = "Insurance Plan";
            this.tbp_InsurancePlan.UseVisualStyleBackColor = true;
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.pnlOptions);
            this.pnl_Base.Controls.Add(this.GBox_GeneralInfo);
            this.pnl_Base.Controls.Add(this.groupBox2);
            this.pnl_Base.Controls.Add(this.GBox_Companyadrs);
            this.pnl_Base.Controls.Add(this.gBoxComContact);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 26);
            this.pnl_Base.Margin = new System.Windows.Forms.Padding(10);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Size = new System.Drawing.Size(723, 441);
            this.pnl_Base.TabIndex = 0;
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.label76);
            this.pnlOptions.Controls.Add(this.groupBox1);
            this.pnlOptions.Controls.Add(this.label77);
            this.pnlOptions.Controls.Add(this.label78);
            this.pnlOptions.Controls.Add(this.label79);
            this.pnlOptions.Controls.Add(this.label80);
            this.pnlOptions.Controls.Add(this.chkPARequired);
            this.pnlOptions.Controls.Add(this.lblInsEligibilityProviderID);
            this.pnlOptions.Controls.Add(this.txtInsEligibilityProvderID);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptions.Location = new System.Drawing.Point(0, 222);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlOptions.Size = new System.Drawing.Size(723, 219);
            this.pnlOptions.TabIndex = 2;
            this.pnlOptions.TabStop = true;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Dock = System.Windows.Forms.DockStyle.Top;
            this.label76.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Location = new System.Drawing.Point(1, 4);
            this.label76.Name = "label76";
            this.label76.Padding = new System.Windows.Forms.Padding(3);
            this.label76.Size = new System.Drawing.Size(61, 20);
            this.label76.TabIndex = 194;
            this.label76.Text = "Options";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(653, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(61, 16);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.cmbBox33B);
            this.groupBox4.Controls.Add(this.cmbBox33A);
            this.groupBox4.Controls.Add(this.cmbBox33);
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox4.Location = new System.Drawing.Point(33, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(320, 98);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Box 33";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(77, 72);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(40, 14);
            this.label25.TabIndex = 14;
            this.label25.Text = "33-B :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(76, 45);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(41, 14);
            this.label26.TabIndex = 14;
            this.label26.Text = "33-A :";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(88, 18);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(29, 14);
            this.label27.TabIndex = 14;
            this.label27.Text = "33 :";
            // 
            // cmbBox33B
            // 
            this.cmbBox33B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox33B.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox33B.FormattingEnabled = true;
            this.cmbBox33B.Items.AddRange(new object[] {
            "",
            "Billing Provider NPI",
            "Facility NPI",
            "Clinic NPI"});
            this.cmbBox33B.Location = new System.Drawing.Point(119, 69);
            this.cmbBox33B.Name = "cmbBox33B";
            this.cmbBox33B.Size = new System.Drawing.Size(157, 22);
            this.cmbBox33B.TabIndex = 2;
            this.cmbBox33B.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // cmbBox33A
            // 
            this.cmbBox33A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox33A.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox33A.FormattingEnabled = true;
            this.cmbBox33A.Items.AddRange(new object[] {
            "",
            "Billing Provider NPI",
            "Facility NPI",
            "Clinic NPI"});
            this.cmbBox33A.Location = new System.Drawing.Point(119, 42);
            this.cmbBox33A.Name = "cmbBox33A";
            this.cmbBox33A.Size = new System.Drawing.Size(157, 22);
            this.cmbBox33A.TabIndex = 1;
            this.cmbBox33A.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // cmbBox33
            // 
            this.cmbBox33.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox33.FormattingEnabled = true;
            this.cmbBox33.Items.AddRange(new object[] {
            "",
            "Provider Address",
            "Facility Address",
            "Clinic Address"});
            this.cmbBox33.Location = new System.Drawing.Point(119, 15);
            this.cmbBox33.Name = "cmbBox33";
            this.cmbBox33.Size = new System.Drawing.Size(157, 22);
            this.cmbBox33.TabIndex = 0;
            this.cmbBox33.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cmbBox32B);
            this.groupBox3.Controls.Add(this.cmbBox32);
            this.groupBox3.Controls.Add(this.cmbBox32A);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox3.Location = new System.Drawing.Point(378, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 95);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Box 32";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(77, 69);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(40, 14);
            this.label24.TabIndex = 14;
            this.label24.Text = "32-B :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(76, 42);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(41, 14);
            this.label23.TabIndex = 14;
            this.label23.Text = "32-A :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(88, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 14);
            this.label15.TabIndex = 14;
            this.label15.Text = "32 :";
            // 
            // cmbBox32B
            // 
            this.cmbBox32B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox32B.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox32B.FormattingEnabled = true;
            this.cmbBox32B.Items.AddRange(new object[] {
            "",
            "Billing Provider NPI",
            "Facility NPI",
            "Clinic NPI"});
            this.cmbBox32B.Location = new System.Drawing.Point(119, 66);
            this.cmbBox32B.Name = "cmbBox32B";
            this.cmbBox32B.Size = new System.Drawing.Size(157, 22);
            this.cmbBox32B.TabIndex = 2;
            this.cmbBox32B.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // cmbBox32
            // 
            this.cmbBox32.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox32.FormattingEnabled = true;
            this.cmbBox32.Items.AddRange(new object[] {
            "",
            "Provider Address",
            "Facility Address",
            "Clinic Address"});
            this.cmbBox32.Location = new System.Drawing.Point(119, 12);
            this.cmbBox32.Name = "cmbBox32";
            this.cmbBox32.Size = new System.Drawing.Size(157, 22);
            this.cmbBox32.TabIndex = 0;
            this.cmbBox32.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // cmbBox32A
            // 
            this.cmbBox32A.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox32A.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox32A.FormattingEnabled = true;
            this.cmbBox32A.Items.AddRange(new object[] {
            "",
            "Billing Provider NPI",
            "Facility NPI",
            "Clinic NPI"});
            this.cmbBox32A.Location = new System.Drawing.Point(119, 39);
            this.cmbBox32A.Name = "cmbBox32A";
            this.cmbBox32A.Size = new System.Drawing.Size(157, 22);
            this.cmbBox32A.TabIndex = 1;
            this.cmbBox32A.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(1, 218);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(721, 1);
            this.label77.TabIndex = 7;
            this.label77.Text = "label1";
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Top;
            this.label78.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(1, 3);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(721, 1);
            this.label78.TabIndex = 6;
            this.label78.Text = "label1";
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Right;
            this.label79.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(722, 3);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1, 216);
            this.label79.TabIndex = 5;
            this.label79.Text = "label4";
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Left;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(0, 3);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(1, 216);
            this.label80.TabIndex = 4;
            this.label80.Text = "label4";
            // 
            // chkPARequired
            // 
            this.chkPARequired.AutoSize = true;
            this.chkPARequired.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPARequired.Location = new System.Drawing.Point(76, 31);
            this.chkPARequired.Name = "chkPARequired";
            this.chkPARequired.Size = new System.Drawing.Size(221, 18);
            this.chkPARequired.TabIndex = 1;
            this.chkPARequired.Text = "Default Prior Authorization Required";
            this.chkPARequired.UseVisualStyleBackColor = true;
            this.chkPARequired.Leave += new System.EventHandler(this.chkPARequired_Leave);
            // 
            // lblInsEligibilityProviderID
            // 
            this.lblInsEligibilityProviderID.AutoSize = true;
            this.lblInsEligibilityProviderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsEligibilityProviderID.Location = new System.Drawing.Point(15, 89);
            this.lblInsEligibilityProviderID.Name = "lblInsEligibilityProviderID";
            this.lblInsEligibilityProviderID.Size = new System.Drawing.Size(180, 14);
            this.lblInsEligibilityProviderID.TabIndex = 196;
            this.lblInsEligibilityProviderID.Text = "Insurance Eligibility Provider ID :";
            this.lblInsEligibilityProviderID.Visible = false;
            // 
            // txtInsEligibilityProvderID
            // 
            this.txtInsEligibilityProvderID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtInsEligibilityProvderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsEligibilityProvderID.ForeColor = System.Drawing.Color.Black;
            this.txtInsEligibilityProvderID.Location = new System.Drawing.Point(196, 85);
            this.txtInsEligibilityProvderID.MaxLength = 35;
            this.txtInsEligibilityProvderID.Name = "txtInsEligibilityProvderID";
            this.txtInsEligibilityProvderID.Size = new System.Drawing.Size(238, 22);
            this.txtInsEligibilityProvderID.TabIndex = 0;
            this.txtInsEligibilityProvderID.Visible = false;
            this.txtInsEligibilityProvderID.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // GBox_GeneralInfo
            // 
            this.GBox_GeneralInfo.Controls.Add(this.panel12);
            this.GBox_GeneralInfo.Controls.Add(this.mtxtPhone);
            this.GBox_GeneralInfo.Controls.Add(this.label67);
            this.GBox_GeneralInfo.Controls.Add(this.label39);
            this.GBox_GeneralInfo.Controls.Add(this.label68);
            this.GBox_GeneralInfo.Controls.Add(this.cmbReportingCategory);
            this.GBox_GeneralInfo.Controls.Add(this.label69);
            this.GBox_GeneralInfo.Controls.Add(this.txtURL);
            this.GBox_GeneralInfo.Controls.Add(this.label73);
            this.GBox_GeneralInfo.Controls.Add(this.txtcontact);
            this.GBox_GeneralInfo.Controls.Add(this.label34);
            this.GBox_GeneralInfo.Controls.Add(this.label40);
            this.GBox_GeneralInfo.Controls.Add(this.txtEmail);
            this.GBox_GeneralInfo.Controls.Add(this.Label3);
            this.GBox_GeneralInfo.Controls.Add(this.cmbInsuranceType);
            this.GBox_GeneralInfo.Controls.Add(this.label35);
            this.GBox_GeneralInfo.Controls.Add(this.label37);
            this.GBox_GeneralInfo.Controls.Add(this.txtFax);
            this.GBox_GeneralInfo.Controls.Add(this.label4);
            this.GBox_GeneralInfo.Controls.Add(this.label36);
            this.GBox_GeneralInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.GBox_GeneralInfo.Location = new System.Drawing.Point(0, 0);
            this.GBox_GeneralInfo.Name = "GBox_GeneralInfo";
            this.GBox_GeneralInfo.Size = new System.Drawing.Size(723, 222);
            this.GBox_GeneralInfo.TabIndex = 1;
            this.GBox_GeneralInfo.TabStop = true;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label74);
            this.panel12.Controls.Add(this.pnlAddresssControl);
            this.panel12.Controls.Add(this.txtname);
            this.panel12.Controls.Add(this.label6);
            this.panel12.Controls.Add(this.label29);
            this.panel12.Controls.Add(this.txtPayerID);
            this.panel12.Controls.Add(this.label22);
            this.panel12.Controls.Add(this.cmbInsuranceCompany);
            this.panel12.Controls.Add(this.Label1);
            this.panel12.Location = new System.Drawing.Point(5, 6);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(381, 210);
            this.panel12.TabIndex = 2;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Dock = System.Windows.Forms.DockStyle.Top;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Location = new System.Drawing.Point(0, 0);
            this.label74.Name = "label74";
            this.label74.Padding = new System.Windows.Forms.Padding(3);
            this.label74.Size = new System.Drawing.Size(136, 20);
            this.label74.TabIndex = 202;
            this.label74.Text = "General Information";
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(45, 102);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 105);
            this.pnlAddresssControl.TabIndex = 6;
            this.pnlAddresssControl.TabStop = true;
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.ForeColor = System.Drawing.Color.Black;
            this.txtname.Location = new System.Drawing.Point(127, 50);
            this.txtname.MaxLength = 50;
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(238, 22);
            this.txtname.TabIndex = 4;
            this.txtname.TextChanged += new System.EventHandler(this.txtname_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(63, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 14);
            this.label6.TabIndex = 194;
            this.label6.Text = "Payer ID :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(2, 28);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(122, 14);
            this.label29.TabIndex = 200;
            this.label29.Text = "Insurance Company :";
            // 
            // txtPayerID
            // 
            this.txtPayerID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerID.ForeColor = System.Drawing.Color.Black;
            this.txtPayerID.Location = new System.Drawing.Point(127, 76);
            this.txtPayerID.MaxLength = 35;
            this.txtPayerID.Name = "txtPayerID";
            this.txtPayerID.Size = new System.Drawing.Size(238, 22);
            this.txtPayerID.TabIndex = 5;
            // 
            // label22
            // 
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(39, 54);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label22.Size = new System.Drawing.Size(14, 14);
            this.label22.TabIndex = 201;
            this.label22.Text = "*";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbInsuranceCompany
            // 
            this.cmbInsuranceCompany.BackColor = System.Drawing.SystemColors.Window;
            this.cmbInsuranceCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsuranceCompany.FormattingEnabled = true;
            this.cmbInsuranceCompany.Location = new System.Drawing.Point(127, 24);
            this.cmbInsuranceCompany.Name = "cmbInsuranceCompany";
            this.cmbInsuranceCompany.Size = new System.Drawing.Size(238, 22);
            this.cmbInsuranceCompany.TabIndex = 3;
            this.cmbInsuranceCompany.SelectedIndexChanged += new System.EventHandler(this.cmbInsuranceCompany_SelectedIndexChanged);
            this.cmbInsuranceCompany.MouseEnter += new System.EventHandler(this.cmbInsuranceCompany_MouseEnter);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(52, 54);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(72, 14);
            this.Label1.TabIndex = 196;
            this.Label1.Text = "Plan Name :";
            // 
            // mtxtPhone
            // 
            this.mtxtPhone.AllowValidate = true;
            this.mtxtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtPhone.Location = new System.Drawing.Point(508, 109);
            this.mtxtPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtPhone.Name = "mtxtPhone";
            this.mtxtPhone.ReadOnly = false;
            this.mtxtPhone.Size = new System.Drawing.Size(113, 24);
            this.mtxtPhone.TabIndex = 10;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(1, 221);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(721, 1);
            this.label67.TabIndex = 7;
            this.label67.Text = "label1";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(384, 32);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(121, 14);
            this.label39.TabIndex = 19;
            this.label39.Text = "Reporting Category :";
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Top;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(1, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(721, 1);
            this.label68.TabIndex = 6;
            this.label68.Text = "label1";
            // 
            // cmbReportingCategory
            // 
            this.cmbReportingCategory.BackColor = System.Drawing.SystemColors.Window;
            this.cmbReportingCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportingCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReportingCategory.FormattingEnabled = true;
            this.cmbReportingCategory.Location = new System.Drawing.Point(508, 31);
            this.cmbReportingCategory.Name = "cmbReportingCategory";
            this.cmbReportingCategory.Size = new System.Drawing.Size(191, 22);
            this.cmbReportingCategory.TabIndex = 7;
            this.cmbReportingCategory.SelectedIndexChanged += new System.EventHandler(this.cmbReportingCategory_SelectedIndexChanged);
            this.cmbReportingCategory.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            this.cmbReportingCategory.MouseEnter += new System.EventHandler(this.cmbReportingCategory_MouseEnter);
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Right;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(722, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1, 222);
            this.label69.TabIndex = 5;
            this.label69.Text = "label4";
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.Location = new System.Drawing.Point(508, 189);
            this.txtURL.MaxLength = 50;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(191, 22);
            this.txtURL.TabIndex = 13;
            this.txtURL.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            this.txtURL.Validating += new System.ComponentModel.CancelEventHandler(this.txtURL_Validating);
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Left;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(0, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1, 222);
            this.label73.TabIndex = 4;
            this.label73.Text = "label4";
            // 
            // txtcontact
            // 
            this.txtcontact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontact.ForeColor = System.Drawing.Color.Black;
            this.txtcontact.Location = new System.Drawing.Point(508, 83);
            this.txtcontact.MaxLength = 50;
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(191, 22);
            this.txtcontact.TabIndex = 9;
            this.txtcontact.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(469, 190);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(36, 14);
            this.label34.TabIndex = 14;
            this.label34.Text = "URL :";
            // 
            // label40
            // 
            this.label40.AutoEllipsis = true;
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(423, 58);
            this.label40.Name = "label40";
            this.label40.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label40.Size = new System.Drawing.Size(14, 14);
            this.label40.TabIndex = 114;
            this.label40.Text = "*";
            this.label40.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(508, 163);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(191, 22);
            this.txtEmail.TabIndex = 12;
            this.txtEmail.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(447, 84);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(58, 14);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Contact :";
            // 
            // cmbInsuranceType
            // 
            this.cmbInsuranceType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbInsuranceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsuranceType.FormattingEnabled = true;
            this.cmbInsuranceType.Location = new System.Drawing.Point(508, 57);
            this.cmbInsuranceType.Name = "cmbInsuranceType";
            this.cmbInsuranceType.Size = new System.Drawing.Size(191, 22);
            this.cmbInsuranceType.TabIndex = 8;
            this.cmbInsuranceType.SelectedIndexChanged += new System.EventHandler(this.cmbInsuranceType_SelectedIndexChanged);
            this.cmbInsuranceType.SelectionChangeCommitted += new System.EventHandler(this.AllControlValueChanged_Event);
            this.cmbInsuranceType.MouseEnter += new System.EventHandler(this.cmbInsuranceType_MouseEnter);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(463, 164);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(42, 14);
            this.label35.TabIndex = 300;
            this.label35.Text = "Email :";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(455, 111);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(50, 14);
            this.label37.TabIndex = 0;
            this.label37.Text = "Phone :";
            // 
            // txtFax
            // 
            this.txtFax.AllowValidate = true;
            this.txtFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.IncludeLiteralsAndPrompts = false;
            this.txtFax.Location = new System.Drawing.Point(508, 137);
            this.txtFax.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtFax.Name = "txtFax";
            this.txtFax.ReadOnly = false;
            this.txtFax.Size = new System.Drawing.Size(113, 22);
            this.txtFax.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(436, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 14);
            this.label4.TabIndex = 15;
            this.label4.Text = "Plan Type :";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(472, 138);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(33, 14);
            this.label36.TabIndex = 6;
            this.label36.Text = "Fax :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_PayerPhExt);
            this.groupBox2.Controls.Add(this.mtxt_PayerPhone);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label89);
            this.groupBox2.Controls.Add(this.txtAdditionalInfo);
            this.groupBox2.Controls.Add(this.label88);
            this.groupBox2.Controls.Add(this.txtServicingState);
            this.groupBox2.Controls.Add(this.txtOfficeID);
            this.groupBox2.Controls.Add(this.label87);
            this.groupBox2.Controls.Add(this.txtWebsite);
            this.groupBox2.Controls.Add(this.label86);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.chkShowPayment);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.chkNameOfFacilityinBox33);
            this.groupBox2.Controls.Add(this.chkRemittanceAdvice);
            this.groupBox2.Controls.Add(this.chkDoNotPrintFacility);
            this.groupBox2.Controls.Add(this.lblOfficeID);
            this.groupBox2.Controls.Add(this.chkBox31Blank);
            this.groupBox2.Controls.Add(this.chkEnrollmentRequired);
            this.groupBox2.Controls.Add(this.chkOnlyPrintFirstPointer);
            this.groupBox2.Controls.Add(this.chkElectronicCOB);
            this.groupBox2.Controls.Add(this.chkRealTimeClaimStatus);
            this.groupBox2.Controls.Add(this.chkMedigap);
            this.groupBox2.Controls.Add(this.chkClaims);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(53, 620);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(646, 312);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // txt_PayerPhExt
            // 
            this.txt_PayerPhExt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PayerPhExt.Location = new System.Drawing.Point(106, 196);
            this.txt_PayerPhExt.MaxLength = 5;
            this.txt_PayerPhExt.Name = "txt_PayerPhExt";
            this.txt_PayerPhExt.Size = new System.Drawing.Size(195, 22);
            this.txt_PayerPhExt.TabIndex = 16;
            this.txt_PayerPhExt.Click += new System.EventHandler(this.txt_PayerPhExt_Click);
            this.txt_PayerPhExt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_PayerPhExt_MouseClick);
            this.txt_PayerPhExt.ContextMenuStripChanged += new System.EventHandler(this.txt_PayerPhExt_ContextMenuStripChanged);
            this.txt_PayerPhExt.CursorChanged += new System.EventHandler(this.txt_PayerPhExt_CursorChanged);
            this.txt_PayerPhExt.TextChanged += new System.EventHandler(this.txt_PayerPhExt_TextChanged);
            this.txt_PayerPhExt.Enter += new System.EventHandler(this.txt_PayerPhExt_Enter);
            this.txt_PayerPhExt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PayerPhExt_KeyPress);
            // 
            // mtxt_PayerPhone
            // 
            this.mtxt_PayerPhone.AllowValidate = true;
            this.mtxt_PayerPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxt_PayerPhone.IncludeLiteralsAndPrompts = false;
            this.mtxt_PayerPhone.Location = new System.Drawing.Point(106, 166);
            this.mtxt_PayerPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxt_PayerPhone.Name = "mtxt_PayerPhone";
            this.mtxt_PayerPhone.ReadOnly = false;
            this.mtxt_PayerPhone.Size = new System.Drawing.Size(195, 26);
            this.mtxt_PayerPhone.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-2, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 14);
            this.label9.TabIndex = 31;
            this.label9.Text = "Payer Phone Ext :";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(10, 144);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(94, 14);
            this.label89.TabIndex = 29;
            this.label89.Text = "Additional Info :";
            // 
            // txtAdditionalInfo
            // 
            this.txtAdditionalInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdditionalInfo.Location = new System.Drawing.Point(106, 140);
            this.txtAdditionalInfo.MaxLength = 50;
            this.txtAdditionalInfo.Name = "txtAdditionalInfo";
            this.txtAdditionalInfo.Size = new System.Drawing.Size(195, 22);
            this.txtAdditionalInfo.TabIndex = 33;
            this.txtAdditionalInfo.TabStop = false;
            this.txtAdditionalInfo.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(7, 116);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(97, 14);
            this.label88.TabIndex = 28;
            this.label88.Text = "Servicing State :";
            // 
            // txtServicingState
            // 
            this.txtServicingState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServicingState.Location = new System.Drawing.Point(106, 112);
            this.txtServicingState.MaxLength = 50;
            this.txtServicingState.Name = "txtServicingState";
            this.txtServicingState.Size = new System.Drawing.Size(195, 22);
            this.txtServicingState.TabIndex = 10;
            this.txtServicingState.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // txtOfficeID
            // 
            this.txtOfficeID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtOfficeID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfficeID.ForeColor = System.Drawing.Color.Black;
            this.txtOfficeID.Location = new System.Drawing.Point(118, 36);
            this.txtOfficeID.MaxLength = 35;
            this.txtOfficeID.Name = "txtOfficeID";
            this.txtOfficeID.Size = new System.Drawing.Size(94, 22);
            this.txtOfficeID.TabIndex = 9;
            this.txtOfficeID.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(40, 228);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(64, 14);
            this.label87.TabIndex = 27;
            this.label87.Text = "Web site :";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsite.Location = new System.Drawing.Point(106, 224);
            this.txtWebsite.MaxLength = 50;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(195, 22);
            this.txtWebsite.TabIndex = 18;
            this.txtWebsite.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(20, 172);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(84, 14);
            this.label86.TabIndex = 25;
            this.label86.Text = "Payer Phone :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbAcceptAssignmentYes);
            this.panel1.Controls.Add(this.rbAcceptAssignmentNo);
            this.panel1.Location = new System.Drawing.Point(454, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 26);
            this.panel1.TabIndex = 30;
            // 
            // rbAcceptAssignmentYes
            // 
            this.rbAcceptAssignmentYes.AutoSize = true;
            this.rbAcceptAssignmentYes.Checked = true;
            this.rbAcceptAssignmentYes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAcceptAssignmentYes.Location = new System.Drawing.Point(3, 3);
            this.rbAcceptAssignmentYes.Name = "rbAcceptAssignmentYes";
            this.rbAcceptAssignmentYes.Size = new System.Drawing.Size(45, 18);
            this.rbAcceptAssignmentYes.TabIndex = 31;
            this.rbAcceptAssignmentYes.TabStop = true;
            this.rbAcceptAssignmentYes.Text = "Yes";
            this.rbAcceptAssignmentYes.UseVisualStyleBackColor = true;
            this.rbAcceptAssignmentYes.CheckedChanged += new System.EventHandler(this.rbAcceptAssignmentYes_CheckedChanged);
            // 
            // rbAcceptAssignmentNo
            // 
            this.rbAcceptAssignmentNo.AutoSize = true;
            this.rbAcceptAssignmentNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAcceptAssignmentNo.Location = new System.Drawing.Point(62, 3);
            this.rbAcceptAssignmentNo.Name = "rbAcceptAssignmentNo";
            this.rbAcceptAssignmentNo.Size = new System.Drawing.Size(40, 18);
            this.rbAcceptAssignmentNo.TabIndex = 32;
            this.rbAcceptAssignmentNo.Text = "No";
            this.rbAcceptAssignmentNo.UseVisualStyleBackColor = true;
            this.rbAcceptAssignmentNo.CheckedChanged += new System.EventHandler(this.rbAcceptAssignmentNo_CheckedChanged);
            // 
            // chkShowPayment
            // 
            this.chkShowPayment.AutoSize = true;
            this.chkShowPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPayment.Location = new System.Drawing.Point(460, 153);
            this.chkShowPayment.Name = "chkShowPayment";
            this.chkShowPayment.Size = new System.Drawing.Size(109, 18);
            this.chkShowPayment.TabIndex = 10;
            this.chkShowPayment.Text = "Show Payment";
            this.chkShowPayment.UseVisualStyleBackColor = true;
            this.chkShowPayment.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(331, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "Accept Assignment :";
            // 
            // chkNameOfFacilityinBox33
            // 
            this.chkNameOfFacilityinBox33.AutoSize = true;
            this.chkNameOfFacilityinBox33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNameOfFacilityinBox33.Location = new System.Drawing.Point(460, 130);
            this.chkNameOfFacilityinBox33.Name = "chkNameOfFacilityinBox33";
            this.chkNameOfFacilityinBox33.Size = new System.Drawing.Size(164, 18);
            this.chkNameOfFacilityinBox33.TabIndex = 9;
            this.chkNameOfFacilityinBox33.Text = "Name of facility in box 33";
            this.chkNameOfFacilityinBox33.UseVisualStyleBackColor = true;
            this.chkNameOfFacilityinBox33.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkRemittanceAdvice
            // 
            this.chkRemittanceAdvice.AutoSize = true;
            this.chkRemittanceAdvice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRemittanceAdvice.Location = new System.Drawing.Point(413, 19);
            this.chkRemittanceAdvice.Name = "chkRemittanceAdvice";
            this.chkRemittanceAdvice.Size = new System.Drawing.Size(128, 18);
            this.chkRemittanceAdvice.TabIndex = 44;
            this.chkRemittanceAdvice.Text = "Remittance Advice";
            this.chkRemittanceAdvice.UseVisualStyleBackColor = true;
            this.chkRemittanceAdvice.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkDoNotPrintFacility
            // 
            this.chkDoNotPrintFacility.AutoSize = true;
            this.chkDoNotPrintFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDoNotPrintFacility.Location = new System.Drawing.Point(444, 244);
            this.chkDoNotPrintFacility.Name = "chkDoNotPrintFacility";
            this.chkDoNotPrintFacility.Size = new System.Drawing.Size(196, 18);
            this.chkDoNotPrintFacility.TabIndex = 51;
            this.chkDoNotPrintFacility.Text = "Do not print facility if POS is 11";
            this.chkDoNotPrintFacility.UseVisualStyleBackColor = true;
            this.chkDoNotPrintFacility.Visible = false;
            this.chkDoNotPrintFacility.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // lblOfficeID
            // 
            this.lblOfficeID.AutoSize = true;
            this.lblOfficeID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfficeID.Location = new System.Drawing.Point(45, 40);
            this.lblOfficeID.Name = "lblOfficeID";
            this.lblOfficeID.Size = new System.Drawing.Size(63, 14);
            this.lblOfficeID.TabIndex = 0;
            this.lblOfficeID.Text = "Office ID :";
            // 
            // chkBox31Blank
            // 
            this.chkBox31Blank.AutoSize = true;
            this.chkBox31Blank.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox31Blank.Location = new System.Drawing.Point(297, 244);
            this.chkBox31Blank.Name = "chkBox31Blank";
            this.chkBox31Blank.Size = new System.Drawing.Size(96, 18);
            this.chkBox31Blank.TabIndex = 48;
            this.chkBox31Blank.Text = "Box 31 Blank";
            this.chkBox31Blank.UseVisualStyleBackColor = true;
            this.chkBox31Blank.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkEnrollmentRequired
            // 
            this.chkEnrollmentRequired.AutoSize = true;
            this.chkEnrollmentRequired.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnrollmentRequired.Location = new System.Drawing.Point(297, 152);
            this.chkEnrollmentRequired.Name = "chkEnrollmentRequired";
            this.chkEnrollmentRequired.Size = new System.Drawing.Size(136, 18);
            this.chkEnrollmentRequired.TabIndex = 40;
            this.chkEnrollmentRequired.Text = "Enrollment Required";
            this.chkEnrollmentRequired.UseVisualStyleBackColor = true;
            this.chkEnrollmentRequired.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkOnlyPrintFirstPointer
            // 
            this.chkOnlyPrintFirstPointer.AutoSize = true;
            this.chkOnlyPrintFirstPointer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOnlyPrintFirstPointer.Location = new System.Drawing.Point(460, 175);
            this.chkOnlyPrintFirstPointer.Name = "chkOnlyPrintFirstPointer";
            this.chkOnlyPrintFirstPointer.Size = new System.Drawing.Size(143, 18);
            this.chkOnlyPrintFirstPointer.TabIndex = 43;
            this.chkOnlyPrintFirstPointer.Text = "Only print 1st Pointer";
            this.chkOnlyPrintFirstPointer.UseVisualStyleBackColor = true;
            this.chkOnlyPrintFirstPointer.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkElectronicCOB
            // 
            this.chkElectronicCOB.AutoSize = true;
            this.chkElectronicCOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkElectronicCOB.Location = new System.Drawing.Point(297, 106);
            this.chkElectronicCOB.Name = "chkElectronicCOB";
            this.chkElectronicCOB.Size = new System.Drawing.Size(106, 18);
            this.chkElectronicCOB.TabIndex = 36;
            this.chkElectronicCOB.Text = "Electronic COB";
            this.chkElectronicCOB.UseVisualStyleBackColor = true;
            this.chkElectronicCOB.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkRealTimeClaimStatus
            // 
            this.chkRealTimeClaimStatus.AutoSize = true;
            this.chkRealTimeClaimStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRealTimeClaimStatus.Location = new System.Drawing.Point(297, 129);
            this.chkRealTimeClaimStatus.Name = "chkRealTimeClaimStatus";
            this.chkRealTimeClaimStatus.Size = new System.Drawing.Size(149, 18);
            this.chkRealTimeClaimStatus.TabIndex = 38;
            this.chkRealTimeClaimStatus.Text = "Real Time Claim Status";
            this.chkRealTimeClaimStatus.UseVisualStyleBackColor = true;
            this.chkRealTimeClaimStatus.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkMedigap
            // 
            this.chkMedigap.AutoSize = true;
            this.chkMedigap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMedigap.Location = new System.Drawing.Point(460, 106);
            this.chkMedigap.Name = "chkMedigap";
            this.chkMedigap.Size = new System.Drawing.Size(75, 18);
            this.chkMedigap.TabIndex = 37;
            this.chkMedigap.Text = "Medigap ";
            this.chkMedigap.UseVisualStyleBackColor = true;
            this.chkMedigap.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // chkClaims
            // 
            this.chkClaims.AutoSize = true;
            this.chkClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClaims.Location = new System.Drawing.Point(297, 175);
            this.chkClaims.Name = "chkClaims";
            this.chkClaims.Size = new System.Drawing.Size(58, 18);
            this.chkClaims.TabIndex = 42;
            this.chkClaims.Text = "Claims";
            this.chkClaims.UseVisualStyleBackColor = true;
            this.chkClaims.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // GBox_Companyadrs
            // 
            this.GBox_Companyadrs.Controls.Add(this.txtState);
            this.GBox_Companyadrs.Controls.Add(this.txtZip);
            this.GBox_Companyadrs.Controls.Add(this.label38);
            this.GBox_Companyadrs.Controls.Add(this.txtAddressLine2);
            this.GBox_Companyadrs.Controls.Add(this.label30);
            this.GBox_Companyadrs.Controls.Add(this.txtCity);
            this.GBox_Companyadrs.Controls.Add(this.txtAddressLine1);
            this.GBox_Companyadrs.Controls.Add(this.label33);
            this.GBox_Companyadrs.Controls.Add(this.label32);
            this.GBox_Companyadrs.Controls.Add(this.label31);
            this.GBox_Companyadrs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBox_Companyadrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GBox_Companyadrs.Location = new System.Drawing.Point(48, 814);
            this.GBox_Companyadrs.Name = "GBox_Companyadrs";
            this.GBox_Companyadrs.Size = new System.Drawing.Size(420, 34);
            this.GBox_Companyadrs.TabIndex = 2;
            this.GBox_Companyadrs.TabStop = false;
            this.GBox_Companyadrs.Text = "Address Information";
            // 
            // txtState
            // 
            this.txtState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.ForeColor = System.Drawing.Color.Black;
            this.txtState.Location = new System.Drawing.Point(274, 82);
            this.txtState.MaxLength = 50;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(101, 22);
            this.txtState.TabIndex = 4;
            this.txtState.TabStop = false;
            this.txtState.Visible = false;
            // 
            // txtZip
            // 
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZip.ForeColor = System.Drawing.Color.Black;
            this.txtZip.Location = new System.Drawing.Point(122, 109);
            this.txtZip.MaxLength = 8;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(94, 22);
            this.txtZip.TabIndex = 2;
            this.txtZip.TabStop = false;
            this.txtZip.Visible = false;
            this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(88, 113);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 14);
            this.label38.TabIndex = 14;
            this.label38.Text = "Zip :";
            this.label38.Visible = false;
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressLine2.ForeColor = System.Drawing.Color.Black;
            this.txtAddressLine2.Location = new System.Drawing.Point(122, 53);
            this.txtAddressLine2.MaxLength = 50;
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(253, 22);
            this.txtAddressLine2.TabIndex = 1;
            this.txtAddressLine2.TabStop = false;
            this.txtAddressLine2.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(33, 57);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(87, 14);
            this.label30.TabIndex = 352;
            this.label30.Text = "AddressLine2 :";
            this.label30.Visible = false;
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(122, 81);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(95, 22);
            this.txtCity.TabIndex = 3;
            this.txtCity.TabStop = false;
            this.txtCity.Visible = false;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressLine1.ForeColor = System.Drawing.Color.Black;
            this.txtAddressLine1.Location = new System.Drawing.Point(122, 25);
            this.txtAddressLine1.MaxLength = 50;
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(253, 22);
            this.txtAddressLine1.TabIndex = 0;
            this.txtAddressLine1.TabStop = false;
            this.txtAddressLine1.Visible = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(33, 29);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(87, 14);
            this.label33.TabIndex = 0;
            this.label33.Text = "AddressLine1 :";
            this.label33.Visible = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(85, 85);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 14);
            this.label32.TabIndex = 1;
            this.label32.Text = "City :";
            this.label32.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(227, 85);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(45, 14);
            this.label31.TabIndex = 2;
            this.label31.Text = "State :";
            this.label31.Visible = false;
            // 
            // gBoxComContact
            // 
            this.gBoxComContact.Controls.Add(this.chkRealTimeEligibility);
            this.gBoxComContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxComContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gBoxComContact.Location = new System.Drawing.Point(35, 378);
            this.gBoxComContact.Name = "gBoxComContact";
            this.gBoxComContact.Size = new System.Drawing.Size(10, 30);
            this.gBoxComContact.TabIndex = 1;
            this.gBoxComContact.TabStop = false;
            this.gBoxComContact.Text = "Options";
            this.gBoxComContact.Visible = false;
            this.gBoxComContact.Enter += new System.EventHandler(this.gBoxComContact_Enter);
            // 
            // chkRealTimeEligibility
            // 
            this.chkRealTimeEligibility.AutoSize = true;
            this.chkRealTimeEligibility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRealTimeEligibility.Location = new System.Drawing.Point(77, 21);
            this.chkRealTimeEligibility.Name = "chkRealTimeEligibility";
            this.chkRealTimeEligibility.Size = new System.Drawing.Size(127, 18);
            this.chkRealTimeEligibility.TabIndex = 0;
            this.chkRealTimeEligibility.Text = "Real Time Eligibility";
            this.chkRealTimeEligibility.UseVisualStyleBackColor = true;
            this.chkRealTimeEligibility.CheckedChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // pnlHoldMessage
            // 
            this.pnlHoldMessage.Controls.Add(this.panel7);
            this.pnlHoldMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHoldMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlHoldMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlHoldMessage.Location = new System.Drawing.Point(0, 0);
            this.pnlHoldMessage.Name = "pnlHoldMessage";
            this.pnlHoldMessage.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlHoldMessage.Size = new System.Drawing.Size(723, 26);
            this.pnlHoldMessage.TabIndex = 218;
            this.pnlHoldMessage.Visible = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.lbl_TopBrd);
            this.panel7.Controls.Add(this.lblHoldMessage);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label70);
            this.panel7.Controls.Add(this.label71);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(723, 23);
            this.panel7.TabIndex = 19;
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(1, 22);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(721, 1);
            this.lbl_TopBrd.TabIndex = 191;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblHoldMessage
            // 
            this.lblHoldMessage.AutoSize = true;
            this.lblHoldMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoldMessage.ForeColor = System.Drawing.Color.Red;
            this.lblHoldMessage.Location = new System.Drawing.Point(6, 4);
            this.lblHoldMessage.Name = "lblHoldMessage";
            this.lblHoldMessage.Size = new System.Drawing.Size(101, 14);
            this.lblHoldMessage.TabIndex = 190;
            this.lblHoldMessage.Text = "lblHoldMessage";
            this.lblHoldMessage.Visible = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 22);
            this.label11.TabIndex = 7;
            this.label11.Text = "label4";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(722, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 22);
            this.label70.TabIndex = 6;
            this.label70.Text = "label3";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(0, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(723, 1);
            this.label71.TabIndex = 5;
            this.label71.Text = "label1";
            // 
            // tbInsuranceSetup
            // 
            this.tbInsuranceSetup.Controls.Add(this.tbp_InsurancePlan);
            this.tbInsuranceSetup.Controls.Add(this.tbp_Eligibility);
            this.tbInsuranceSetup.Controls.Add(this.tbp_MidLevel);
            this.tbInsuranceSetup.Controls.Add(this.tbp_BillingSettings);
            this.tbInsuranceSetup.Controls.Add(this.tbp_BillingTaxon);
            this.tbInsuranceSetup.Controls.Add(this.tbp_5010Transition);
            this.tbInsuranceSetup.Controls.Add(this.tbp_AlternatePayerID);
            this.tbInsuranceSetup.Controls.Add(this.tbp_Institutional);
            this.tbInsuranceSetup.Controls.Add(this.tbp_Collection);
            this.tbInsuranceSetup.Controls.Add(this.tbp_EPSDT);
            this.tbInsuranceSetup.Controls.Add(this.tpAnesthesia);
            this.tbInsuranceSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInsuranceSetup.Location = new System.Drawing.Point(0, 53);
            this.tbInsuranceSetup.Multiline = true;
            this.tbInsuranceSetup.Name = "tbInsuranceSetup";
            this.tbInsuranceSetup.SelectedIndex = 0;
            this.tbInsuranceSetup.Size = new System.Drawing.Size(731, 513);
            this.tbInsuranceSetup.TabIndex = 115;
            this.tbInsuranceSetup.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbInsuranceSetup_Selected);
            // 
            // tbp_Eligibility
            // 
            this.tbp_Eligibility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbp_Eligibility.Controls.Add(this.panel19);
            this.tbp_Eligibility.Location = new System.Drawing.Point(4, 42);
            this.tbp_Eligibility.Name = "tbp_Eligibility";
            this.tbp_Eligibility.Size = new System.Drawing.Size(723, 467);
            this.tbp_Eligibility.TabIndex = 4;
            this.tbp_Eligibility.Text = "Eligibility";
            this.tbp_Eligibility.UseVisualStyleBackColor = true;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel19.Controls.Add(this.mskeligibiltyPhone);
            this.panel19.Controls.Add(this.lblNote);
            this.panel19.Controls.Add(this.txtEligibiltyNote);
            this.panel19.Controls.Add(this.lblphone);
            this.panel19.Controls.Add(this.lblWebsite);
            this.panel19.Controls.Add(this.TxtEligibiltyWebste);
            this.panel19.Controls.Add(this.lblContact);
            this.panel19.Controls.Add(this.TxtEligiblitycntct);
            this.panel19.Controls.Add(this.cmbInsEligibilityPrimProvType);
            this.panel19.Controls.Add(this.cmbInsEligibilitySecProvType);
            this.panel19.Controls.Add(this.label120);
            this.panel19.Controls.Add(this.label121);
            this.panel19.Controls.Add(this.label107);
            this.panel19.Controls.Add(this.txtInsEligibilitySecProvID);
            this.panel19.Controls.Add(this.label115);
            this.panel19.Controls.Add(this.label116);
            this.panel19.Controls.Add(this.label117);
            this.panel19.Controls.Add(this.label118);
            this.panel19.Controls.Add(this.label119);
            this.panel19.Controls.Add(this.txtInsEligibilityPrimProvID);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(723, 467);
            this.panel19.TabIndex = 3;
            this.panel19.TabStop = true;
            // 
            // mskeligibiltyPhone
            // 
            this.mskeligibiltyPhone.AllowValidate = true;
            this.mskeligibiltyPhone.IncludeLiteralsAndPrompts = false;
            this.mskeligibiltyPhone.Location = new System.Drawing.Point(581, 107);
            this.mskeligibiltyPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskeligibiltyPhone.Name = "mskeligibiltyPhone";
            this.mskeligibiltyPhone.ReadOnly = false;
            this.mskeligibiltyPhone.Size = new System.Drawing.Size(106, 22);
            this.mskeligibiltyPhone.TabIndex = 212;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(41, 181);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(42, 14);
            this.lblNote.TabIndex = 211;
            this.lblNote.Text = "Note :";
            // 
            // txtEligibiltyNote
            // 
            this.txtEligibiltyNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtEligibiltyNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEligibiltyNote.ForeColor = System.Drawing.Color.Black;
            this.txtEligibiltyNote.Location = new System.Drawing.Point(91, 181);
            this.txtEligibiltyNote.MaxLength = 255;
            this.txtEligibiltyNote.Multiline = true;
            this.txtEligibiltyNote.Name = "txtEligibiltyNote";
            this.txtEligibiltyNote.Size = new System.Drawing.Size(598, 64);
            this.txtEligibiltyNote.TabIndex = 210;
            // 
            // lblphone
            // 
            this.lblphone.AutoSize = true;
            this.lblphone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblphone.Location = new System.Drawing.Point(525, 110);
            this.lblphone.Name = "lblphone";
            this.lblphone.Size = new System.Drawing.Size(50, 14);
            this.lblphone.TabIndex = 208;
            this.lblphone.Text = "Phone :";
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsite.Location = new System.Drawing.Point(48, 144);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(36, 14);
            this.lblWebsite.TabIndex = 207;
            this.lblWebsite.Text = "URL :";
            // 
            // TxtEligibiltyWebste
            // 
            this.TxtEligibiltyWebste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.TxtEligibiltyWebste.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEligibiltyWebste.ForeColor = System.Drawing.Color.Black;
            this.TxtEligibiltyWebste.Location = new System.Drawing.Point(91, 144);
            this.TxtEligibiltyWebste.MaxLength = 255;
            this.TxtEligibiltyWebste.Name = "TxtEligibiltyWebste";
            this.TxtEligibiltyWebste.Size = new System.Drawing.Size(598, 22);
            this.TxtEligibiltyWebste.TabIndex = 206;
            this.TxtEligibiltyWebste.Validating += new System.ComponentModel.CancelEventHandler(this.TxtEligibiltyWebste_Validating);
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.Location = new System.Drawing.Point(27, 110);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(58, 14);
            this.lblContact.TabIndex = 205;
            this.lblContact.Text = "Contact :";
            // 
            // TxtEligiblitycntct
            // 
            this.TxtEligiblitycntct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.TxtEligiblitycntct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEligiblitycntct.ForeColor = System.Drawing.Color.Black;
            this.TxtEligiblitycntct.Location = new System.Drawing.Point(91, 107);
            this.TxtEligiblitycntct.MaxLength = 255;
            this.TxtEligiblitycntct.Name = "TxtEligiblitycntct";
            this.TxtEligiblitycntct.Size = new System.Drawing.Size(418, 22);
            this.TxtEligiblitycntct.TabIndex = 204;
            // 
            // cmbInsEligibilityPrimProvType
            // 
            this.cmbInsEligibilityPrimProvType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbInsEligibilityPrimProvType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInsEligibilityPrimProvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsEligibilityPrimProvType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsEligibilityPrimProvType.FormattingEnabled = true;
            this.cmbInsEligibilityPrimProvType.Location = new System.Drawing.Point(406, 27);
            this.cmbInsEligibilityPrimProvType.Name = "cmbInsEligibilityPrimProvType";
            this.cmbInsEligibilityPrimProvType.Size = new System.Drawing.Size(297, 23);
            this.cmbInsEligibilityPrimProvType.TabIndex = 203;
            this.cmbInsEligibilityPrimProvType.MouseEnter += new System.EventHandler(this.cmbInsEligibilityPrimProvType_MouseEnter);
            // 
            // cmbInsEligibilitySecProvType
            // 
            this.cmbInsEligibilitySecProvType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInsEligibilitySecProvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsEligibilitySecProvType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsEligibilitySecProvType.FormattingEnabled = true;
            this.cmbInsEligibilitySecProvType.Items.AddRange(new object[] {
            "Q4 - Prior Identifier Number",
            "TJ - Federal Taxpayer’s Identification Number",
            "HPI - Centers for Medicare and Medicaid Services National Provider Identifier"});
            this.cmbInsEligibilitySecProvType.Location = new System.Drawing.Point(406, 67);
            this.cmbInsEligibilitySecProvType.Name = "cmbInsEligibilitySecProvType";
            this.cmbInsEligibilitySecProvType.Size = new System.Drawing.Size(297, 23);
            this.cmbInsEligibilitySecProvType.TabIndex = 202;
            this.cmbInsEligibilitySecProvType.MouseEnter += new System.EventHandler(this.cmbInsEligibilitySecProvType_MouseEnter);
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(356, 71);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(47, 14);
            this.label120.TabIndex = 200;
            this.label120.Text = "Type : ";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(356, 31);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(47, 14);
            this.label121.TabIndex = 199;
            this.label121.Text = "Type : ";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(9, 71);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(241, 14);
            this.label107.TabIndex = 198;
            this.label107.Text = "Insurance Eligibility Secondary Provider ID :";
            // 
            // txtInsEligibilitySecProvID
            // 
            this.txtInsEligibilitySecProvID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtInsEligibilitySecProvID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsEligibilitySecProvID.ForeColor = System.Drawing.Color.Black;
            this.txtInsEligibilitySecProvID.Location = new System.Drawing.Point(254, 67);
            this.txtInsEligibilitySecProvID.MaxLength = 35;
            this.txtInsEligibilitySecProvID.Name = "txtInsEligibilitySecProvID";
            this.txtInsEligibilitySecProvID.Size = new System.Drawing.Size(87, 22);
            this.txtInsEligibilitySecProvID.TabIndex = 197;
            // 
            // label115
            // 
            this.label115.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label115.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label115.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(1, 466);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(721, 1);
            this.label115.TabIndex = 7;
            this.label115.Text = "label1";
            // 
            // label116
            // 
            this.label116.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label116.Dock = System.Windows.Forms.DockStyle.Top;
            this.label116.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.Location = new System.Drawing.Point(1, 0);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(721, 1);
            this.label116.TabIndex = 6;
            this.label116.Text = "label1";
            // 
            // label117
            // 
            this.label117.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label117.Dock = System.Windows.Forms.DockStyle.Right;
            this.label117.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(722, 0);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(1, 467);
            this.label117.TabIndex = 5;
            this.label117.Text = "label4";
            // 
            // label118
            // 
            this.label118.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label118.Dock = System.Windows.Forms.DockStyle.Left;
            this.label118.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(0, 0);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(1, 467);
            this.label118.TabIndex = 4;
            this.label118.Text = "label4";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(27, 31);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(223, 14);
            this.label119.TabIndex = 196;
            this.label119.Text = "Insurance Eligibility Primary Provider ID :";
            // 
            // txtInsEligibilityPrimProvID
            // 
            this.txtInsEligibilityPrimProvID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtInsEligibilityPrimProvID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsEligibilityPrimProvID.ForeColor = System.Drawing.Color.Black;
            this.txtInsEligibilityPrimProvID.Location = new System.Drawing.Point(254, 27);
            this.txtInsEligibilityPrimProvID.MaxLength = 35;
            this.txtInsEligibilityPrimProvID.Name = "txtInsEligibilityPrimProvID";
            this.txtInsEligibilityPrimProvID.Size = new System.Drawing.Size(87, 22);
            this.txtInsEligibilityPrimProvID.TabIndex = 0;
            // 
            // tbp_BillingTaxon
            // 
            this.tbp_BillingTaxon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbp_BillingTaxon.Controls.Add(this.panel17);
            this.tbp_BillingTaxon.Controls.Add(this.panel16);
            this.tbp_BillingTaxon.Controls.Add(this.panel14);
            this.tbp_BillingTaxon.Location = new System.Drawing.Point(4, 42);
            this.tbp_BillingTaxon.Name = "tbp_BillingTaxon";
            this.tbp_BillingTaxon.Size = new System.Drawing.Size(723, 467);
            this.tbp_BillingTaxon.TabIndex = 3;
            this.tbp_BillingTaxon.Text = "Billing Taxonomy";
            this.tbp_BillingTaxon.UseVisualStyleBackColor = true;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.pnlInternalControl);
            this.panel17.Controls.Add(this.c1BillingTaxonomy);
            this.panel17.Controls.Add(this.label102);
            this.panel17.Controls.Add(this.label103);
            this.panel17.Controls.Add(this.label104);
            this.panel17.Controls.Add(this.label105);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(0, 189);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel17.Size = new System.Drawing.Size(723, 278);
            this.panel17.TabIndex = 222;
            this.panel17.TabStop = true;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(193, 50);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(337, 211);
            this.pnlInternalControl.TabIndex = 9;
            this.pnlInternalControl.Visible = false;
            // 
            // c1BillingTaxonomy
            // 
            this.c1BillingTaxonomy.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1BillingTaxonomy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1BillingTaxonomy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1BillingTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1BillingTaxonomy.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BillingTaxonomy.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1BillingTaxonomy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BillingTaxonomy.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1BillingTaxonomy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BillingTaxonomy.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1BillingTaxonomy.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1BillingTaxonomy.Location = new System.Drawing.Point(1, 4);
            this.c1BillingTaxonomy.Name = "c1BillingTaxonomy";
            this.c1BillingTaxonomy.Rows.Count = 1;
            this.c1BillingTaxonomy.Rows.DefaultSize = 22;
            this.c1BillingTaxonomy.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BillingTaxonomy.ShowCellLabels = true;
            this.c1BillingTaxonomy.Size = new System.Drawing.Size(721, 273);
            this.c1BillingTaxonomy.StyleInfo = resources.GetString("c1BillingTaxonomy.StyleInfo");
            this.c1BillingTaxonomy.TabIndex = 8;
            this.c1BillingTaxonomy.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1BillingTaxonomy_AfterScroll);
            this.c1BillingTaxonomy.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BillingTaxonomy_StartEdit);
            this.c1BillingTaxonomy.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BillingTaxonomy_AfterEdit);
            this.c1BillingTaxonomy.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BillingTaxonomy_LeaveEdit);
            this.c1BillingTaxonomy.ChangeEdit += new System.EventHandler(this.c1BillingTaxonomy_ChangeEdit);
            this.c1BillingTaxonomy.KeyDownEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.c1BillingTaxonomy_KeyDownEdit);
            this.c1BillingTaxonomy.Click += new System.EventHandler(this.c1BillingTaxonomy_Click);
            this.c1BillingTaxonomy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1BillingTaxonomy_KeyUp);
            this.c1BillingTaxonomy.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1BillingTaxonomy_MouseMove);
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(1, 277);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(721, 1);
            this.label102.TabIndex = 7;
            this.label102.Text = "label1";
            // 
            // label103
            // 
            this.label103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Dock = System.Windows.Forms.DockStyle.Top;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(1, 3);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(721, 1);
            this.label103.TabIndex = 6;
            this.label103.Text = "label1";
            // 
            // label104
            // 
            this.label104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label104.Dock = System.Windows.Forms.DockStyle.Right;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(722, 3);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(1, 275);
            this.label104.TabIndex = 5;
            this.label104.Text = "label4";
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Dock = System.Windows.Forms.DockStyle.Left;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(0, 3);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(1, 275);
            this.label105.TabIndex = 4;
            this.label105.Text = "label4";
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.panel18);
            this.panel16.Controls.Add(this.cmbQualifier);
            this.panel16.Controls.Add(this.label97);
            this.panel16.Controls.Add(this.label98);
            this.panel16.Controls.Add(this.label99);
            this.panel16.Controls.Add(this.label100);
            this.panel16.Controls.Add(this.label101);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel16.Location = new System.Drawing.Point(0, 24);
            this.panel16.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel16.Size = new System.Drawing.Size(723, 165);
            this.panel16.TabIndex = 221;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.chkIncludeTaxBillElec);
            this.panel18.Controls.Add(this.chkIncludeTaxRenElec);
            this.panel18.Controls.Add(this.chkIncludeTaxBilPaper);
            this.panel18.Controls.Add(this.chkIncludeTaxRenPaper);
            this.panel18.Controls.Add(this.chkIncludeTax4Paper);
            this.panel18.Controls.Add(this.chkIncludeTax4Elec);
            this.panel18.Location = new System.Drawing.Point(9, 13);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(253, 144);
            this.panel18.TabIndex = 195;
            // 
            // chkIncludeTaxBillElec
            // 
            this.chkIncludeTaxBillElec.AutoSize = true;
            this.chkIncludeTaxBillElec.Location = new System.Drawing.Point(46, 122);
            this.chkIncludeTaxBillElec.Name = "chkIncludeTaxBillElec";
            this.chkIncludeTaxBillElec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTaxBillElec.Size = new System.Drawing.Size(103, 18);
            this.chkIncludeTaxBillElec.TabIndex = 196;
            this.chkIncludeTaxBillElec.Text = "Billing Provider";
            this.chkIncludeTaxBillElec.UseVisualStyleBackColor = true;
            this.chkIncludeTaxBillElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
            // 
            // chkIncludeTaxRenElec
            // 
            this.chkIncludeTaxRenElec.AutoSize = true;
            this.chkIncludeTaxRenElec.Location = new System.Drawing.Point(46, 98);
            this.chkIncludeTaxRenElec.Name = "chkIncludeTaxRenElec";
            this.chkIncludeTaxRenElec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTaxRenElec.Size = new System.Drawing.Size(129, 18);
            this.chkIncludeTaxRenElec.TabIndex = 195;
            this.chkIncludeTaxRenElec.Text = "Rendering Provider";
            this.chkIncludeTaxRenElec.UseVisualStyleBackColor = true;
            this.chkIncludeTaxRenElec.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenElec_CheckedChanged);
            // 
            // chkIncludeTaxBilPaper
            // 
            this.chkIncludeTaxBilPaper.AutoSize = true;
            this.chkIncludeTaxBilPaper.Location = new System.Drawing.Point(46, 51);
            this.chkIncludeTaxBilPaper.Name = "chkIncludeTaxBilPaper";
            this.chkIncludeTaxBilPaper.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTaxBilPaper.Size = new System.Drawing.Size(103, 18);
            this.chkIncludeTaxBilPaper.TabIndex = 194;
            this.chkIncludeTaxBilPaper.Text = "Billing Provider";
            this.chkIncludeTaxBilPaper.UseVisualStyleBackColor = true;
            this.chkIncludeTaxBilPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
            // 
            // chkIncludeTaxRenPaper
            // 
            this.chkIncludeTaxRenPaper.AutoSize = true;
            this.chkIncludeTaxRenPaper.Location = new System.Drawing.Point(46, 27);
            this.chkIncludeTaxRenPaper.Name = "chkIncludeTaxRenPaper";
            this.chkIncludeTaxRenPaper.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTaxRenPaper.Size = new System.Drawing.Size(129, 18);
            this.chkIncludeTaxRenPaper.TabIndex = 193;
            this.chkIncludeTaxRenPaper.Text = "Rendering Provider";
            this.chkIncludeTaxRenPaper.UseVisualStyleBackColor = true;
            this.chkIncludeTaxRenPaper.CheckedChanged += new System.EventHandler(this.chkIncludeTaxRenPaper_CheckedChanged);
            // 
            // chkIncludeTax4Paper
            // 
            this.chkIncludeTax4Paper.AutoSize = true;
            this.chkIncludeTax4Paper.Location = new System.Drawing.Point(24, 3);
            this.chkIncludeTax4Paper.Name = "chkIncludeTax4Paper";
            this.chkIncludeTax4Paper.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTax4Paper.Size = new System.Drawing.Size(181, 18);
            this.chkIncludeTax4Paper.TabIndex = 191;
            this.chkIncludeTax4Paper.Text = "Include Taxonomy for Paper";
            this.chkIncludeTax4Paper.UseVisualStyleBackColor = true;
            this.chkIncludeTax4Paper.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Paper_CheckedChanged);
            // 
            // chkIncludeTax4Elec
            // 
            this.chkIncludeTax4Elec.AutoSize = true;
            this.chkIncludeTax4Elec.Location = new System.Drawing.Point(24, 75);
            this.chkIncludeTax4Elec.Name = "chkIncludeTax4Elec";
            this.chkIncludeTax4Elec.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkIncludeTax4Elec.Size = new System.Drawing.Size(203, 18);
            this.chkIncludeTax4Elec.TabIndex = 192;
            this.chkIncludeTax4Elec.Text = "Include Taxonomy for Electronic";
            this.chkIncludeTax4Elec.UseVisualStyleBackColor = true;
            this.chkIncludeTax4Elec.CheckedChanged += new System.EventHandler(this.chkIncludeTax4Elec_CheckedChanged);
            // 
            // cmbQualifier
            // 
            this.cmbQualifier.BackColor = System.Drawing.SystemColors.Window;
            this.cmbQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQualifier.FormattingEnabled = true;
            this.cmbQualifier.Location = new System.Drawing.Point(374, 13);
            this.cmbQualifier.Name = "cmbQualifier";
            this.cmbQualifier.Size = new System.Drawing.Size(121, 22);
            this.cmbQualifier.TabIndex = 194;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label97.Location = new System.Drawing.Point(315, 16);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(58, 14);
            this.label97.TabIndex = 190;
            this.label97.Text = "Qualifier :";
            // 
            // label98
            // 
            this.label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label98.Dock = System.Windows.Forms.DockStyle.Left;
            this.label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(0, 4);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(1, 160);
            this.label98.TabIndex = 7;
            this.label98.Text = "label4";
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Dock = System.Windows.Forms.DockStyle.Right;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label99.Location = new System.Drawing.Point(722, 4);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(1, 160);
            this.label99.TabIndex = 6;
            this.label99.Text = "label3";
            // 
            // label100
            // 
            this.label100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label100.Dock = System.Windows.Forms.DockStyle.Top;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(0, 3);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(723, 1);
            this.label100.TabIndex = 5;
            this.label100.Text = "label1";
            // 
            // label101
            // 
            this.label101.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label101.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label101.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label101.Location = new System.Drawing.Point(0, 164);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(723, 1);
            this.label101.TabIndex = 8;
            this.label101.Text = "label2";
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(723, 24);
            this.panel14.TabIndex = 220;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.Transparent;
            this.panel15.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel15.Controls.Add(this.label46);
            this.panel15.Controls.Add(this.label56);
            this.panel15.Controls.Add(this.label61);
            this.panel15.Controls.Add(this.label90);
            this.panel15.Controls.Add(this.label96);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(723, 24);
            this.panel15.TabIndex = 19;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(6, 5);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(70, 14);
            this.label46.TabIndex = 190;
            this.label46.Text = "Taxonomy";
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Left;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(0, 1);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 22);
            this.label56.TabIndex = 7;
            this.label56.Text = "label4";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label61.Location = new System.Drawing.Point(722, 1);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 22);
            this.label61.TabIndex = 6;
            this.label61.Text = "label3";
            // 
            // label90
            // 
            this.label90.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Dock = System.Windows.Forms.DockStyle.Top;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(0, 0);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(723, 1);
            this.label90.TabIndex = 5;
            this.label90.Text = "label1";
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label96.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label96.Location = new System.Drawing.Point(0, 23);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(723, 1);
            this.label96.TabIndex = 8;
            this.label96.Text = "label2";
            // 
            // tbp_5010Transition
            // 
            this.tbp_5010Transition.Controls.Add(this.panel20);
            this.tbp_5010Transition.Location = new System.Drawing.Point(4, 42);
            this.tbp_5010Transition.Name = "tbp_5010Transition";
            this.tbp_5010Transition.Size = new System.Drawing.Size(723, 467);
            this.tbp_5010Transition.TabIndex = 5;
            this.tbp_5010Transition.Text = "5010 Transition";
            this.tbp_5010Transition.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel20.Controls.Add(this.chkIncludeMod_in_SVD);
            this.panel20.Controls.Add(this.chkIncludePatientSSN);
            this.panel20.Controls.Add(this.label166);
            this.panel20.Controls.Add(this.panel29);
            this.panel20.Controls.Add(this.chkIncludeRefnOrdering);
            this.panel20.Controls.Add(this.chkIncludeRefnSupervising);
            this.panel20.Controls.Add(this.chkIncludeOrdering);
            this.panel20.Controls.Add(this.chkIncludeSubscriberAddress);
            this.panel20.Controls.Add(this.chkIncludeServiceFacility);
            this.panel20.Controls.Add(this.chkIncludeRendering);
            this.panel20.Controls.Add(this.label112);
            this.panel20.Controls.Add(this.label113);
            this.panel20.Controls.Add(this.label114);
            this.panel20.Controls.Add(this.label122);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(0, 0);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(723, 467);
            this.panel20.TabIndex = 4;
            this.panel20.TabStop = true;
            // 
            // chkIncludeMod_in_SVD
            // 
            this.chkIncludeMod_in_SVD.AutoSize = true;
            this.chkIncludeMod_in_SVD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkIncludeMod_in_SVD.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIncludeMod_in_SVD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkIncludeMod_in_SVD.Location = new System.Drawing.Point(29, 174);
            this.chkIncludeMod_in_SVD.Name = "chkIncludeMod_in_SVD";
            this.chkIncludeMod_in_SVD.Size = new System.Drawing.Size(269, 18);
            this.chkIncludeMod_in_SVD.TabIndex = 8;
            this.chkIncludeMod_in_SVD.Text = "Include Modifier in 2430 Loop SVD Segment";
            this.chkIncludeMod_in_SVD.UseVisualStyleBackColor = false;
            // 
            // chkIncludePatientSSN
            // 
            this.chkIncludePatientSSN.AutoSize = true;
            this.chkIncludePatientSSN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkIncludePatientSSN.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIncludePatientSSN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkIncludePatientSSN.Location = new System.Drawing.Point(29, 150);
            this.chkIncludePatientSSN.Name = "chkIncludePatientSSN";
            this.chkIncludePatientSSN.Size = new System.Drawing.Size(230, 18);
            this.chkIncludePatientSSN.TabIndex = 6;
            this.chkIncludePatientSSN.Text = "Include Patient SSN in 2010 CA Loop";
            this.chkIncludePatientSSN.UseVisualStyleBackColor = false;
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label166.Location = new System.Drawing.Point(48, 198);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(276, 14);
            this.label166.TabIndex = 0;
            this.label166.Text = "Prior Patient Payment into \"AMT\" segment ";
            // 
            // panel29
            // 
            this.panel29.Controls.Add(this.rbDoNotSend);
            this.panel29.Controls.Add(this.rbSend);
            this.panel29.Controls.Add(this.label170);
            this.panel29.Controls.Add(this.rbNone);
            this.panel29.Controls.Add(this.label169);
            this.panel29.Controls.Add(this.label168);
            this.panel29.Controls.Add(this.label167);
            this.panel29.Location = new System.Drawing.Point(30, 204);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(309, 45);
            this.panel29.TabIndex = 7;
            // 
            // rbDoNotSend
            // 
            this.rbDoNotSend.AutoSize = true;
            this.rbDoNotSend.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDoNotSend.Location = new System.Drawing.Point(188, 17);
            this.rbDoNotSend.Name = "rbDoNotSend";
            this.rbDoNotSend.Size = new System.Drawing.Size(95, 18);
            this.rbDoNotSend.TabIndex = 2;
            this.rbDoNotSend.TabStop = true;
            this.rbDoNotSend.Text = "Do not Send";
            this.rbDoNotSend.UseVisualStyleBackColor = true;
            this.rbDoNotSend.CheckedChanged += new System.EventHandler(this.rbDoNotSend_CheckedChanged);
            // 
            // rbSend
            // 
            this.rbSend.AutoSize = true;
            this.rbSend.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSend.Location = new System.Drawing.Point(109, 17);
            this.rbSend.Name = "rbSend";
            this.rbSend.Size = new System.Drawing.Size(53, 18);
            this.rbSend.TabIndex = 1;
            this.rbSend.TabStop = true;
            this.rbSend.Text = "Send";
            this.rbSend.UseVisualStyleBackColor = true;
            this.rbSend.CheckedChanged += new System.EventHandler(this.rbSend_CheckedChanged);
            // 
            // label170
            // 
            this.label170.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label170.Dock = System.Windows.Forms.DockStyle.Left;
            this.label170.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label170.Location = new System.Drawing.Point(0, 1);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(1, 43);
            this.label170.TabIndex = 4;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNone.Location = new System.Drawing.Point(29, 17);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(54, 18);
            this.rbNone.TabIndex = 0;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // label169
            // 
            this.label169.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label169.Dock = System.Windows.Forms.DockStyle.Right;
            this.label169.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label169.Location = new System.Drawing.Point(308, 1);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(1, 43);
            this.label169.TabIndex = 3;
            // 
            // label168
            // 
            this.label168.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label168.Dock = System.Windows.Forms.DockStyle.Top;
            this.label168.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label168.Location = new System.Drawing.Point(0, 0);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(309, 1);
            this.label168.TabIndex = 2;
            // 
            // label167
            // 
            this.label167.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label167.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label167.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label167.Location = new System.Drawing.Point(0, 44);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(309, 1);
            this.label167.TabIndex = 1;
            // 
            // chkIncludeRefnOrdering
            // 
            this.chkIncludeRefnOrdering.AutoSize = true;
            this.chkIncludeRefnOrdering.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkIncludeRefnOrdering.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIncludeRefnOrdering.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkIncludeRefnOrdering.Location = new System.Drawing.Point(29, 126);
            this.chkIncludeRefnOrdering.Name = "chkIncludeRefnOrdering";
            this.chkIncludeRefnOrdering.Size = new System.Drawing.Size(298, 18);
            this.chkIncludeRefnOrdering.TabIndex = 5;
            this.chkIncludeRefnOrdering.Text = "Include Referring Provider with Ordering Provider ";
            this.chkIncludeRefnOrdering.UseVisualStyleBackColor = false;
            // 
            // chkIncludeRefnSupervising
            // 
            this.chkIncludeRefnSupervising.AutoSize = true;
            this.chkIncludeRefnSupervising.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.chkIncludeRefnSupervising.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIncludeRefnSupervising.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkIncludeRefnSupervising.Location = new System.Drawing.Point(29, 102);
            this.chkIncludeRefnSupervising.Name = "chkIncludeRefnSupervising";
            this.chkIncludeRefnSupervising.Size = new System.Drawing.Size(312, 18);
            this.chkIncludeRefnSupervising.TabIndex = 4;
            this.chkIncludeRefnSupervising.Text = "Include Referring Provider with Supervising Provider ";
            this.chkIncludeRefnSupervising.UseVisualStyleBackColor = false;
            // 
            // chkIncludeOrdering
            // 
            this.chkIncludeOrdering.AutoSize = true;
            this.chkIncludeOrdering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeOrdering.Location = new System.Drawing.Point(29, 78);
            this.chkIncludeOrdering.Name = "chkIncludeOrdering";
            this.chkIncludeOrdering.Size = new System.Drawing.Size(354, 18);
            this.chkIncludeOrdering.TabIndex = 3;
            this.chkIncludeOrdering.Text = "Include Ordering Provider when same as Rendering Provider";
            this.chkIncludeOrdering.UseVisualStyleBackColor = true;
            // 
            // chkIncludeSubscriberAddress
            // 
            this.chkIncludeSubscriberAddress.AutoSize = true;
            this.chkIncludeSubscriberAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeSubscriberAddress.Location = new System.Drawing.Point(29, 54);
            this.chkIncludeSubscriberAddress.Name = "chkIncludeSubscriberAddress";
            this.chkIncludeSubscriberAddress.Size = new System.Drawing.Size(313, 18);
            this.chkIncludeSubscriberAddress.TabIndex = 2;
            this.chkIncludeSubscriberAddress.Text = "Include Subscriber Address and DOB when NOT Self";
            this.chkIncludeSubscriberAddress.UseVisualStyleBackColor = true;
            // 
            // chkIncludeServiceFacility
            // 
            this.chkIncludeServiceFacility.AutoSize = true;
            this.chkIncludeServiceFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeServiceFacility.Location = new System.Drawing.Point(29, 30);
            this.chkIncludeServiceFacility.Name = "chkIncludeServiceFacility";
            this.chkIncludeServiceFacility.Size = new System.Drawing.Size(311, 18);
            this.chkIncludeServiceFacility.TabIndex = 1;
            this.chkIncludeServiceFacility.Text = "Include Service Facility when same as Billing Provider";
            this.chkIncludeServiceFacility.UseVisualStyleBackColor = true;
            // 
            // chkIncludeRendering
            // 
            this.chkIncludeRendering.AutoSize = true;
            this.chkIncludeRendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeRendering.Location = new System.Drawing.Point(29, 6);
            this.chkIncludeRendering.Name = "chkIncludeRendering";
            this.chkIncludeRendering.Size = new System.Drawing.Size(336, 18);
            this.chkIncludeRendering.TabIndex = 0;
            this.chkIncludeRendering.Text = "Include Rendering Provider when same as Billing Provider";
            this.chkIncludeRendering.UseVisualStyleBackColor = true;
            // 
            // label112
            // 
            this.label112.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label112.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(1, 466);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(721, 1);
            this.label112.TabIndex = 7;
            this.label112.Text = "label1";
            // 
            // label113
            // 
            this.label113.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label113.Dock = System.Windows.Forms.DockStyle.Top;
            this.label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(1, 0);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(721, 1);
            this.label113.TabIndex = 6;
            this.label113.Text = "label1";
            // 
            // label114
            // 
            this.label114.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label114.Dock = System.Windows.Forms.DockStyle.Right;
            this.label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(722, 0);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(1, 467);
            this.label114.TabIndex = 5;
            this.label114.Text = "label4";
            // 
            // label122
            // 
            this.label122.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label122.Dock = System.Windows.Forms.DockStyle.Left;
            this.label122.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(0, 0);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(1, 467);
            this.label122.TabIndex = 4;
            this.label122.Text = "label4";
            // 
            // tbp_AlternatePayerID
            // 
            this.tbp_AlternatePayerID.Controls.Add(this.panel21);
            this.tbp_AlternatePayerID.Controls.Add(this.panel22);
            this.tbp_AlternatePayerID.Location = new System.Drawing.Point(4, 42);
            this.tbp_AlternatePayerID.Name = "tbp_AlternatePayerID";
            this.tbp_AlternatePayerID.Size = new System.Drawing.Size(723, 467);
            this.tbp_AlternatePayerID.TabIndex = 6;
            this.tbp_AlternatePayerID.Text = "ERA Alt. Payer ID";
            this.tbp_AlternatePayerID.UseVisualStyleBackColor = true;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.dgMasters);
            this.panel21.Controls.Add(this.c1AlternatePayerID);
            this.panel21.Controls.Add(this.label109);
            this.panel21.Controls.Add(this.label110);
            this.panel21.Controls.Add(this.label111);
            this.panel21.Controls.Add(this.label123);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(0, 24);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel21.Size = new System.Drawing.Size(723, 443);
            this.panel21.TabIndex = 223;
            this.panel21.TabStop = true;
            // 
            // dgMasters
            // 
            this.dgMasters.AllowUserToAddRows = false;
            this.dgMasters.AllowUserToDeleteRows = false;
            this.dgMasters.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMasters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMasters.BackgroundColor = System.Drawing.Color.White;
            this.dgMasters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMasters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMasters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMasters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMasters.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgMasters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMasters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMasters.EnableHeadersVisualStyles = false;
            this.dgMasters.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgMasters.Location = new System.Drawing.Point(1, 4);
            this.dgMasters.MultiSelect = false;
            this.dgMasters.Name = "dgMasters";
            this.dgMasters.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMasters.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMasters.RowHeadersVisible = false;
            this.dgMasters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMasters.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgMasters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMasters.Size = new System.Drawing.Size(721, 438);
            this.dgMasters.TabIndex = 19;
            this.dgMasters.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMasters_CellDoubleClick_1);
            // 
            // c1AlternatePayerID
            // 
            this.c1AlternatePayerID.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1AlternatePayerID.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1AlternatePayerID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1AlternatePayerID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1AlternatePayerID.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AlternatePayerID.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1AlternatePayerID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AlternatePayerID.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1AlternatePayerID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1AlternatePayerID.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1AlternatePayerID.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1AlternatePayerID.Location = new System.Drawing.Point(1, 4);
            this.c1AlternatePayerID.Name = "c1AlternatePayerID";
            this.c1AlternatePayerID.Rows.Count = 1;
            this.c1AlternatePayerID.Rows.DefaultSize = 22;
            this.c1AlternatePayerID.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1AlternatePayerID.ShowCellLabels = true;
            this.c1AlternatePayerID.Size = new System.Drawing.Size(721, 438);
            this.c1AlternatePayerID.StyleInfo = resources.GetString("c1AlternatePayerID.StyleInfo");
            this.c1AlternatePayerID.TabIndex = 8;
            // 
            // label109
            // 
            this.label109.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label109.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label109.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(1, 442);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(721, 1);
            this.label109.TabIndex = 7;
            this.label109.Text = "label1";
            // 
            // label110
            // 
            this.label110.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label110.Dock = System.Windows.Forms.DockStyle.Top;
            this.label110.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.Location = new System.Drawing.Point(1, 3);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(721, 1);
            this.label110.TabIndex = 6;
            this.label110.Text = "label1";
            // 
            // label111
            // 
            this.label111.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label111.Dock = System.Windows.Forms.DockStyle.Right;
            this.label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(722, 3);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(1, 440);
            this.label111.TabIndex = 5;
            this.label111.Text = "label4";
            // 
            // label123
            // 
            this.label123.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label123.Dock = System.Windows.Forms.DockStyle.Left;
            this.label123.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(0, 3);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(1, 440);
            this.label123.TabIndex = 4;
            this.label123.Text = "label4";
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.panel23);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel22.Location = new System.Drawing.Point(0, 0);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(723, 24);
            this.panel22.TabIndex = 221;
            // 
            // panel23
            // 
            this.panel23.BackColor = System.Drawing.Color.Transparent;
            this.panel23.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.panel23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel23.Controls.Add(this.label124);
            this.panel23.Controls.Add(this.label125);
            this.panel23.Controls.Add(this.label126);
            this.panel23.Controls.Add(this.label127);
            this.panel23.Controls.Add(this.label128);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(723, 24);
            this.panel23.TabIndex = 19;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label124.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label124.Location = new System.Drawing.Point(6, 5);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(121, 14);
            this.label124.TabIndex = 190;
            this.label124.Text = "Alternate Payer ID";
            // 
            // label125
            // 
            this.label125.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label125.Dock = System.Windows.Forms.DockStyle.Left;
            this.label125.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.Location = new System.Drawing.Point(0, 1);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(1, 22);
            this.label125.TabIndex = 7;
            this.label125.Text = "label4";
            // 
            // label126
            // 
            this.label126.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label126.Dock = System.Windows.Forms.DockStyle.Right;
            this.label126.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label126.Location = new System.Drawing.Point(722, 1);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(1, 22);
            this.label126.TabIndex = 6;
            this.label126.Text = "label3";
            // 
            // label127
            // 
            this.label127.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label127.Dock = System.Windows.Forms.DockStyle.Top;
            this.label127.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label127.Location = new System.Drawing.Point(0, 0);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(723, 1);
            this.label127.TabIndex = 5;
            this.label127.Text = "label1";
            // 
            // label128
            // 
            this.label128.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label128.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label128.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label128.Location = new System.Drawing.Point(0, 23);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(723, 1);
            this.label128.TabIndex = 8;
            this.label128.Text = "label2";
            // 
            // tbp_Institutional
            // 
            this.tbp_Institutional.Controls.Add(this.panel24);
            this.tbp_Institutional.Location = new System.Drawing.Point(4, 42);
            this.tbp_Institutional.Name = "tbp_Institutional";
            this.tbp_Institutional.Size = new System.Drawing.Size(723, 467);
            this.tbp_Institutional.TabIndex = 7;
            this.tbp_Institutional.Text = "UB/Institutional ";
            this.tbp_Institutional.UseVisualStyleBackColor = true;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel24.Controls.Add(this.groupBox8);
            this.panel24.Controls.Add(this.groupBox7);
            this.panel24.Controls.Add(this.label129);
            this.panel24.Controls.Add(this.label130);
            this.panel24.Controls.Add(this.label131);
            this.panel24.Controls.Add(this.label132);
            this.panel24.Controls.Add(this.groupBox6);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(0, 0);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(723, 467);
            this.panel24.TabIndex = 5;
            this.panel24.TabStop = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txt81DQual);
            this.groupBox8.Controls.Add(this.cmb81DValue);
            this.groupBox8.Controls.Add(this.label176);
            this.groupBox8.Controls.Add(this.txt81CQual);
            this.groupBox8.Controls.Add(this.txt81BQual);
            this.groupBox8.Controls.Add(this.txt81AQual);
            this.groupBox8.Controls.Add(this.cmb81CValue);
            this.groupBox8.Controls.Add(this.label171);
            this.groupBox8.Controls.Add(this.cmb81BValue);
            this.groupBox8.Controls.Add(this.label173);
            this.groupBox8.Controls.Add(this.cmb81AValue);
            this.groupBox8.Controls.Add(this.label174);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox8.Location = new System.Drawing.Point(16, 321);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(691, 138);
            this.groupBox8.TabIndex = 204;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Box 81CC ";
            // 
            // txt81DQual
            // 
            this.txt81DQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt81DQual.Location = new System.Drawing.Point(46, 106);
            this.txt81DQual.MaxLength = 3;
            this.txt81DQual.Name = "txt81DQual";
            this.txt81DQual.ShortcutsEnabled = false;
            this.txt81DQual.Size = new System.Drawing.Size(47, 22);
            this.txt81DQual.TabIndex = 210;
            this.txt81DQual.Tag = "";
            this.txt81DQual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt81DQual_KeyPress);
            // 
            // cmb81DValue
            // 
            this.cmb81DValue.BackColor = System.Drawing.SystemColors.Window;
            this.cmb81DValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb81DValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb81DValue.FormattingEnabled = true;
            this.cmb81DValue.Items.AddRange(new object[] {
            "",
            "Billing Provider",
            "Attending Provider , BOX 76",
            "Operating Provider , BOX 77",
            "BOX 78",
            "BOX 79"});
            this.cmb81DValue.Location = new System.Drawing.Point(99, 106);
            this.cmb81DValue.Name = "cmb81DValue";
            this.cmb81DValue.Size = new System.Drawing.Size(293, 22);
            this.cmb81DValue.TabIndex = 209;
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label176.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label176.Location = new System.Drawing.Point(24, 110);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(22, 14);
            this.label176.TabIndex = 208;
            this.label176.Text = "d. ";
            // 
            // txt81CQual
            // 
            this.txt81CQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt81CQual.Location = new System.Drawing.Point(46, 78);
            this.txt81CQual.MaxLength = 3;
            this.txt81CQual.Name = "txt81CQual";
            this.txt81CQual.ShortcutsEnabled = false;
            this.txt81CQual.Size = new System.Drawing.Size(47, 22);
            this.txt81CQual.TabIndex = 207;
            this.txt81CQual.Tag = "";
            this.txt81CQual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt81CQual_KeyPress);
            // 
            // txt81BQual
            // 
            this.txt81BQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt81BQual.Location = new System.Drawing.Point(46, 50);
            this.txt81BQual.MaxLength = 3;
            this.txt81BQual.Name = "txt81BQual";
            this.txt81BQual.ShortcutsEnabled = false;
            this.txt81BQual.Size = new System.Drawing.Size(47, 22);
            this.txt81BQual.TabIndex = 206;
            this.txt81BQual.Tag = "";
            this.txt81BQual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt81BQual_KeyPress);
            // 
            // txt81AQual
            // 
            this.txt81AQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt81AQual.Location = new System.Drawing.Point(46, 22);
            this.txt81AQual.MaxLength = 3;
            this.txt81AQual.Name = "txt81AQual";
            this.txt81AQual.ShortcutsEnabled = false;
            this.txt81AQual.Size = new System.Drawing.Size(47, 22);
            this.txt81AQual.TabIndex = 205;
            this.txt81AQual.Tag = "";
            this.txt81AQual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt81AQual_KeyPress);
            // 
            // cmb81CValue
            // 
            this.cmb81CValue.BackColor = System.Drawing.SystemColors.Window;
            this.cmb81CValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb81CValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb81CValue.FormattingEnabled = true;
            this.cmb81CValue.Items.AddRange(new object[] {
            "",
            "Billing Provider",
            "Attending Provider , BOX 76",
            "Operating Provider , BOX 77",
            "BOX 78",
            "BOX 79"});
            this.cmb81CValue.Location = new System.Drawing.Point(99, 78);
            this.cmb81CValue.Name = "cmb81CValue";
            this.cmb81CValue.Size = new System.Drawing.Size(293, 22);
            this.cmb81CValue.TabIndex = 204;
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label171.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label171.Location = new System.Drawing.Point(24, 82);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(21, 14);
            this.label171.TabIndex = 203;
            this.label171.Text = "c. ";
            // 
            // cmb81BValue
            // 
            this.cmb81BValue.BackColor = System.Drawing.SystemColors.Window;
            this.cmb81BValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb81BValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb81BValue.FormattingEnabled = true;
            this.cmb81BValue.Items.AddRange(new object[] {
            "",
            "Billing Provider",
            "Attending Provider , BOX 76",
            "Operating Provider , BOX 77",
            "BOX 78",
            "BOX 79"});
            this.cmb81BValue.Location = new System.Drawing.Point(99, 50);
            this.cmb81BValue.Name = "cmb81BValue";
            this.cmb81BValue.Size = new System.Drawing.Size(293, 22);
            this.cmb81BValue.TabIndex = 198;
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label173.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label173.Location = new System.Drawing.Point(23, 54);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(22, 14);
            this.label173.TabIndex = 197;
            this.label173.Text = "b. ";
            // 
            // cmb81AValue
            // 
            this.cmb81AValue.BackColor = System.Drawing.SystemColors.Window;
            this.cmb81AValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb81AValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb81AValue.FormattingEnabled = true;
            this.cmb81AValue.Items.AddRange(new object[] {
            "",
            "Billing Provider",
            "Attending Provider , BOX 76",
            "Operating Provider , BOX 77",
            "BOX 78",
            "BOX 79"});
            this.cmb81AValue.Location = new System.Drawing.Point(99, 22);
            this.cmb81AValue.Name = "cmb81AValue";
            this.cmb81AValue.Size = new System.Drawing.Size(293, 22);
            this.cmb81AValue.TabIndex = 200;
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label174.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label174.Location = new System.Drawing.Point(24, 26);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(21, 14);
            this.label174.TabIndex = 199;
            this.label174.Text = "a. ";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbBox77Rendering);
            this.groupBox7.Controls.Add(this.label165);
            this.groupBox7.Controls.Add(this.cmbOprtingPrvderBox77);
            this.groupBox7.Controls.Add(this.lblFedTaxNoBox5);
            this.groupBox7.Controls.Add(this.cmbFedTaxNoBox5);
            this.groupBox7.Controls.Add(this.label155);
            this.groupBox7.Controls.Add(this.label172);
            this.groupBox7.Controls.Add(this.cmbUBBlngprvdraltID);
            this.groupBox7.Controls.Add(this.cmbExtendedZipCode);
            this.groupBox7.Controls.Add(this.label156);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox7.Location = new System.Drawing.Point(16, 154);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(691, 160);
            this.groupBox7.TabIndex = 203;
            this.groupBox7.TabStop = false;
            // 
            // cmbBox77Rendering
            // 
            this.cmbBox77Rendering.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBox77Rendering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox77Rendering.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox77Rendering.FormattingEnabled = true;
            this.cmbBox77Rendering.Items.AddRange(new object[] {
            "",
            "Operating",
            "Attending",
            "Both Operating and Attending"});
            this.cmbBox77Rendering.Location = new System.Drawing.Point(205, 100);
            this.cmbBox77Rendering.Name = "cmbBox77Rendering";
            this.cmbBox77Rendering.Size = new System.Drawing.Size(360, 22);
            this.cmbBox77Rendering.TabIndex = 204;
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label165.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label165.Location = new System.Drawing.Point(24, 104);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(178, 14);
            this.label165.TabIndex = 203;
            this.label165.Text = "Box 76,77 Rendering Provider :";
            // 
            // cmbOprtingPrvderBox77
            // 
            this.cmbOprtingPrvderBox77.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOprtingPrvderBox77.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOprtingPrvderBox77.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOprtingPrvderBox77.FormattingEnabled = true;
            this.cmbOprtingPrvderBox77.Items.AddRange(new object[] {
            "",
            "Operating",
            "Attending",
            "Both Operating and Attending"});
            this.cmbOprtingPrvderBox77.Location = new System.Drawing.Point(205, 71);
            this.cmbOprtingPrvderBox77.Name = "cmbOprtingPrvderBox77";
            this.cmbOprtingPrvderBox77.Size = new System.Drawing.Size(360, 22);
            this.cmbOprtingPrvderBox77.TabIndex = 198;
            // 
            // lblFedTaxNoBox5
            // 
            this.lblFedTaxNoBox5.AutoSize = true;
            this.lblFedTaxNoBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFedTaxNoBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblFedTaxNoBox5.Location = new System.Drawing.Point(75, 17);
            this.lblFedTaxNoBox5.Name = "lblFedTaxNoBox5";
            this.lblFedTaxNoBox5.Size = new System.Drawing.Size(127, 14);
            this.lblFedTaxNoBox5.TabIndex = 195;
            this.lblFedTaxNoBox5.Text = "Box 5 - FED TAX NO :";
            // 
            // cmbFedTaxNoBox5
            // 
            this.cmbFedTaxNoBox5.BackColor = System.Drawing.SystemColors.Window;
            this.cmbFedTaxNoBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFedTaxNoBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFedTaxNoBox5.FormattingEnabled = true;
            this.cmbFedTaxNoBox5.Items.AddRange(new object[] {
            "Operating",
            "Attending",
            "Both Operating and Attending"});
            this.cmbFedTaxNoBox5.Location = new System.Drawing.Point(205, 13);
            this.cmbFedTaxNoBox5.Name = "cmbFedTaxNoBox5";
            this.cmbFedTaxNoBox5.Size = new System.Drawing.Size(360, 22);
            this.cmbFedTaxNoBox5.TabIndex = 201;
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label155.Location = new System.Drawing.Point(50, 75);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(152, 14);
            this.label155.TabIndex = 197;
            this.label155.Text = "Box 76,77 Billing Provider :";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label172.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label172.Location = new System.Drawing.Point(23, 133);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(179, 14);
            this.label172.TabIndex = 195;
            this.label172.Text = "Include extended zip code on :";
            // 
            // cmbUBBlngprvdraltID
            // 
            this.cmbUBBlngprvdraltID.BackColor = System.Drawing.SystemColors.Window;
            this.cmbUBBlngprvdraltID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUBBlngprvdraltID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUBBlngprvdraltID.FormattingEnabled = true;
            this.cmbUBBlngprvdraltID.Items.AddRange(new object[] {
            "Operating",
            "Attending",
            "Both Operating and Attending"});
            this.cmbUBBlngprvdraltID.Location = new System.Drawing.Point(205, 42);
            this.cmbUBBlngprvdraltID.Name = "cmbUBBlngprvdraltID";
            this.cmbUBBlngprvdraltID.Size = new System.Drawing.Size(360, 22);
            this.cmbUBBlngprvdraltID.TabIndex = 200;
            // 
            // cmbExtendedZipCode
            // 
            this.cmbExtendedZipCode.BackColor = System.Drawing.SystemColors.Window;
            this.cmbExtendedZipCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtendedZipCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbExtendedZipCode.FormattingEnabled = true;
            this.cmbExtendedZipCode.Items.AddRange(new object[] {
            "",
            "Paper",
            "Electronic",
            "Both,Paper & Electronic"});
            this.cmbExtendedZipCode.Location = new System.Drawing.Point(205, 129);
            this.cmbExtendedZipCode.Name = "cmbExtendedZipCode";
            this.cmbExtendedZipCode.Size = new System.Drawing.Size(360, 22);
            this.cmbExtendedZipCode.TabIndex = 201;
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label156.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label156.Location = new System.Drawing.Point(60, 46);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(142, 14);
            this.label156.TabIndex = 199;
            this.label156.Text = "Box 51 - Health Plan ID :";
            // 
            // label129
            // 
            this.label129.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label129.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label129.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label129.Location = new System.Drawing.Point(1, 466);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(721, 1);
            this.label129.TabIndex = 7;
            this.label129.Text = "label1";
            // 
            // label130
            // 
            this.label130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label130.Dock = System.Windows.Forms.DockStyle.Top;
            this.label130.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label130.Location = new System.Drawing.Point(1, 0);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(721, 1);
            this.label130.TabIndex = 6;
            this.label130.Text = "label1";
            // 
            // label131
            // 
            this.label131.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label131.Dock = System.Windows.Forms.DockStyle.Right;
            this.label131.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(722, 0);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(1, 467);
            this.label131.TabIndex = 5;
            this.label131.Text = "label4";
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label132.Dock = System.Windows.Forms.DockStyle.Left;
            this.label132.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label132.Location = new System.Drawing.Point(0, 0);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(1, 467);
            this.label132.TabIndex = 4;
            this.label132.Text = "label4";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ChkIncludeAttendingPrvTaxonomy);
            this.groupBox6.Controls.Add(this.chkIncludeEstimatedAmtDue);
            this.groupBox6.Controls.Add(this.chkIncludePrimaryDxInBox69);
            this.groupBox6.Controls.Add(this.ckhSentUB04RevenuecodeTotal);
            this.groupBox6.Controls.Add(this.chkIncludeUB04AdmissionHour);
            this.groupBox6.Controls.Add(this.chkIncludeUB04DischargeHour);
            this.groupBox6.Controls.Add(this.chkIncludeRendering_Attending);
            this.groupBox6.Controls.Add(this.chkIsInstitutionalBilling);
            this.groupBox6.Controls.Add(this.chkDefaultDOS);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox6.Location = new System.Drawing.Point(16, 11);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(691, 136);
            this.groupBox6.TabIndex = 202;
            this.groupBox6.TabStop = false;
            // 
            // ChkIncludeAttendingPrvTaxonomy
            // 
            this.ChkIncludeAttendingPrvTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkIncludeAttendingPrvTaxonomy.Location = new System.Drawing.Point(379, 84);
            this.ChkIncludeAttendingPrvTaxonomy.Name = "ChkIncludeAttendingPrvTaxonomy";
            this.ChkIncludeAttendingPrvTaxonomy.Size = new System.Drawing.Size(413, 18);
            this.ChkIncludeAttendingPrvTaxonomy.TabIndex = 14;
            this.ChkIncludeAttendingPrvTaxonomy.Text = "Include Attending Provider Taxonomy in Electronic";
            this.ChkIncludeAttendingPrvTaxonomy.UseVisualStyleBackColor = true;
            // 
            // chkIncludeEstimatedAmtDue
            // 
            this.chkIncludeEstimatedAmtDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeEstimatedAmtDue.Location = new System.Drawing.Point(25, 84);
            this.chkIncludeEstimatedAmtDue.Name = "chkIncludeEstimatedAmtDue";
            this.chkIncludeEstimatedAmtDue.Size = new System.Drawing.Size(413, 18);
            this.chkIncludeEstimatedAmtDue.TabIndex = 13;
            this.chkIncludeEstimatedAmtDue.Text = "Include Estimated amount due on UB04 paper";
            this.chkIncludeEstimatedAmtDue.UseVisualStyleBackColor = true;
            // 
            // chkIncludePrimaryDxInBox69
            // 
            this.chkIncludePrimaryDxInBox69.AutoSize = true;
            this.chkIncludePrimaryDxInBox69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludePrimaryDxInBox69.Location = new System.Drawing.Point(379, 61);
            this.chkIncludePrimaryDxInBox69.Name = "chkIncludePrimaryDxInBox69";
            this.chkIncludePrimaryDxInBox69.Size = new System.Drawing.Size(186, 18);
            this.chkIncludePrimaryDxInBox69.TabIndex = 12;
            this.chkIncludePrimaryDxInBox69.Text = "Include Primary Dx in box 69 ";
            this.chkIncludePrimaryDxInBox69.UseVisualStyleBackColor = true;
            this.chkIncludePrimaryDxInBox69.CheckedChanged += new System.EventHandler(this.chkIncludePrimaryDxInBox69_CheckedChanged);
            // 
            // ckhSentUB04RevenuecodeTotal
            // 
            this.ckhSentUB04RevenuecodeTotal.AutoSize = true;
            this.ckhSentUB04RevenuecodeTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckhSentUB04RevenuecodeTotal.Location = new System.Drawing.Point(379, 38);
            this.ckhSentUB04RevenuecodeTotal.Name = "ckhSentUB04RevenuecodeTotal";
            this.ckhSentUB04RevenuecodeTotal.Size = new System.Drawing.Size(245, 18);
            this.ckhSentUB04RevenuecodeTotal.TabIndex = 11;
            this.ckhSentUB04RevenuecodeTotal.Text = "Sent Revenue code \"001\" in box 42L23";
            this.ckhSentUB04RevenuecodeTotal.UseVisualStyleBackColor = true;
            // 
            // chkIncludeUB04AdmissionHour
            // 
            this.chkIncludeUB04AdmissionHour.AutoSize = true;
            this.chkIncludeUB04AdmissionHour.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeUB04AdmissionHour.Location = new System.Drawing.Point(379, 15);
            this.chkIncludeUB04AdmissionHour.Name = "chkIncludeUB04AdmissionHour";
            this.chkIncludeUB04AdmissionHour.Size = new System.Drawing.Size(186, 18);
            this.chkIncludeUB04AdmissionHour.TabIndex = 10;
            this.chkIncludeUB04AdmissionHour.Text = "Include UB04 Admission Hour";
            this.chkIncludeUB04AdmissionHour.UseVisualStyleBackColor = true;
            // 
            // chkIncludeUB04DischargeHour
            // 
            this.chkIncludeUB04DischargeHour.AutoSize = true;
            this.chkIncludeUB04DischargeHour.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeUB04DischargeHour.Location = new System.Drawing.Point(25, 61);
            this.chkIncludeUB04DischargeHour.Name = "chkIncludeUB04DischargeHour";
            this.chkIncludeUB04DischargeHour.Size = new System.Drawing.Size(185, 18);
            this.chkIncludeUB04DischargeHour.TabIndex = 9;
            this.chkIncludeUB04DischargeHour.Text = "Include UB04 Discharge Hour";
            this.chkIncludeUB04DischargeHour.UseVisualStyleBackColor = true;
            // 
            // chkIncludeRendering_Attending
            // 
            this.chkIncludeRendering_Attending.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeRendering_Attending.Location = new System.Drawing.Point(25, 107);
            this.chkIncludeRendering_Attending.Name = "chkIncludeRendering_Attending";
            this.chkIncludeRendering_Attending.Size = new System.Drawing.Size(413, 18);
            this.chkIncludeRendering_Attending.TabIndex = 2;
            this.chkIncludeRendering_Attending.Text = "Include Rendering Provider on UB04 when same as Attending Provider";
            this.chkIncludeRendering_Attending.UseVisualStyleBackColor = true;
            // 
            // chkIsInstitutionalBilling
            // 
            this.chkIsInstitutionalBilling.AutoSize = true;
            this.chkIsInstitutionalBilling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsInstitutionalBilling.Location = new System.Drawing.Point(25, 15);
            this.chkIsInstitutionalBilling.Name = "chkIsInstitutionalBilling";
            this.chkIsInstitutionalBilling.Size = new System.Drawing.Size(123, 18);
            this.chkIsInstitutionalBilling.TabIndex = 1;
            this.chkIsInstitutionalBilling.Text = "Institutional Billing";
            this.chkIsInstitutionalBilling.UseVisualStyleBackColor = true;
            // 
            // chkDefaultDOS
            // 
            this.chkDefaultDOS.AutoSize = true;
            this.chkDefaultDOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDefaultDOS.Location = new System.Drawing.Point(25, 38);
            this.chkDefaultDOS.Name = "chkDefaultDOS";
            this.chkDefaultDOS.Size = new System.Drawing.Size(205, 18);
            this.chkDefaultDOS.TabIndex = 8;
            this.chkDefaultDOS.Text = "Default Occurrence Date as DOS";
            this.chkDefaultDOS.UseVisualStyleBackColor = true;
            // 
            // tbp_Collection
            // 
            this.tbp_Collection.Controls.Add(this.panel25);
            this.tbp_Collection.Location = new System.Drawing.Point(4, 42);
            this.tbp_Collection.Name = "tbp_Collection";
            this.tbp_Collection.Size = new System.Drawing.Size(723, 467);
            this.tbp_Collection.TabIndex = 8;
            this.tbp_Collection.Text = "Collection";
            this.tbp_Collection.UseVisualStyleBackColor = true;
            // 
            // panel25
            // 
            this.panel25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel25.Controls.Add(this.numInsClmRebillFUActionDays);
            this.panel25.Controls.Add(this.numInsClmStartFUActionDays);
            this.panel25.Controls.Add(this.label137);
            this.panel25.Controls.Add(this.label138);
            this.panel25.Controls.Add(this.cmbInsClmStartFUAction);
            this.panel25.Controls.Add(this.cmbInsClmRebillFUAction);
            this.panel25.Controls.Add(this.label139);
            this.panel25.Controls.Add(this.label140);
            this.panel25.Controls.Add(this.label133);
            this.panel25.Controls.Add(this.label134);
            this.panel25.Controls.Add(this.label135);
            this.panel25.Controls.Add(this.label136);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel25.Location = new System.Drawing.Point(0, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(723, 467);
            this.panel25.TabIndex = 6;
            this.panel25.TabStop = true;
            // 
            // numInsClmRebillFUActionDays
            // 
            this.numInsClmRebillFUActionDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numInsClmRebillFUActionDays.Location = new System.Drawing.Point(260, 98);
            this.numInsClmRebillFUActionDays.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numInsClmRebillFUActionDays.Name = "numInsClmRebillFUActionDays";
            this.numInsClmRebillFUActionDays.Size = new System.Drawing.Size(109, 22);
            this.numInsClmRebillFUActionDays.TabIndex = 184;
            // 
            // numInsClmStartFUActionDays
            // 
            this.numInsClmStartFUActionDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numInsClmStartFUActionDays.Location = new System.Drawing.Point(260, 44);
            this.numInsClmStartFUActionDays.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numInsClmStartFUActionDays.Name = "numInsClmStartFUActionDays";
            this.numInsClmStartFUActionDays.Size = new System.Drawing.Size(109, 22);
            this.numInsClmStartFUActionDays.TabIndex = 183;
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(92, 46);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(162, 14);
            this.label137.TabIndex = 147;
            this.label137.Text = "Number of Days after Filing :";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(29, 73);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(225, 14);
            this.label138.TabIndex = 146;
            this.label138.Text = "Insurance Claim Rebill Follow-up Action :";
            // 
            // cmbInsClmStartFUAction
            // 
            this.cmbInsClmStartFUAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsClmStartFUAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsClmStartFUAction.FormattingEnabled = true;
            this.cmbInsClmStartFUAction.ItemHeight = 14;
            this.cmbInsClmStartFUAction.Location = new System.Drawing.Point(259, 18);
            this.cmbInsClmStartFUAction.Name = "cmbInsClmStartFUAction";
            this.cmbInsClmStartFUAction.Size = new System.Drawing.Size(416, 22);
            this.cmbInsClmStartFUAction.TabIndex = 141;
            this.cmbInsClmStartFUAction.SelectedIndexChanged += new System.EventHandler(this.cmbInsClmStartFUAction_SelectedIndexChanged);
            this.cmbInsClmStartFUAction.MouseEnter += new System.EventHandler(this.cmbInsClmStartFUAction_MouseEnter);
            // 
            // cmbInsClmRebillFUAction
            // 
            this.cmbInsClmRebillFUAction.BackColor = System.Drawing.SystemColors.Window;
            this.cmbInsClmRebillFUAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsClmRebillFUAction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsClmRebillFUAction.FormattingEnabled = true;
            this.cmbInsClmRebillFUAction.ItemHeight = 14;
            this.cmbInsClmRebillFUAction.Location = new System.Drawing.Point(259, 72);
            this.cmbInsClmRebillFUAction.Name = "cmbInsClmRebillFUAction";
            this.cmbInsClmRebillFUAction.Size = new System.Drawing.Size(416, 22);
            this.cmbInsClmRebillFUAction.TabIndex = 143;
            this.cmbInsClmRebillFUAction.SelectedIndexChanged += new System.EventHandler(this.cmbInsClmRebillFUAction_SelectedIndexChanged);
            this.cmbInsClmRebillFUAction.MouseEnter += new System.EventHandler(this.cmbInsClmRebillFUAction_MouseEnter);
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(92, 100);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(162, 14);
            this.label139.TabIndex = 145;
            this.label139.Text = "Number of Days after Filing :";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(12, 19);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(242, 14);
            this.label140.TabIndex = 148;
            this.label140.Text = "Insurance Claim Starting Follow-Up Action :";
            // 
            // label133
            // 
            this.label133.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label133.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label133.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(1, 466);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(721, 1);
            this.label133.TabIndex = 7;
            this.label133.Text = "label1";
            // 
            // label134
            // 
            this.label134.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label134.Dock = System.Windows.Forms.DockStyle.Top;
            this.label134.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(1, 0);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(721, 1);
            this.label134.TabIndex = 6;
            this.label134.Text = "label1";
            // 
            // label135
            // 
            this.label135.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label135.Dock = System.Windows.Forms.DockStyle.Right;
            this.label135.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(722, 0);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(1, 467);
            this.label135.TabIndex = 5;
            this.label135.Text = "label4";
            // 
            // label136
            // 
            this.label136.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label136.Dock = System.Windows.Forms.DockStyle.Left;
            this.label136.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.Location = new System.Drawing.Point(0, 0);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(1, 467);
            this.label136.TabIndex = 4;
            this.label136.Text = "label4";
            // 
            // tbp_EPSDT
            // 
            this.tbp_EPSDT.Controls.Add(this.panel26);
            this.tbp_EPSDT.Location = new System.Drawing.Point(4, 42);
            this.tbp_EPSDT.Name = "tbp_EPSDT";
            this.tbp_EPSDT.Size = new System.Drawing.Size(723, 467);
            this.tbp_EPSDT.TabIndex = 9;
            this.tbp_EPSDT.Text = "EPSDT/Family Planning";
            this.tbp_EPSDT.UseVisualStyleBackColor = true;
            // 
            // panel26
            // 
            this.panel26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel26.Controls.Add(this.txtFamPlanCode);
            this.panel26.Controls.Add(this.txtEPSDTCode);
            this.panel26.Controls.Add(this.chkSuppressRenderPaperEdi);
            this.panel26.Controls.Add(this.cmbFamPlanCodeBox);
            this.panel26.Controls.Add(this.label149);
            this.panel26.Controls.Add(this.label150);
            this.panel26.Controls.Add(this.label144);
            this.panel26.Controls.Add(this.label141);
            this.panel26.Controls.Add(this.chkIncludeRefCode);
            this.panel26.Controls.Add(this.chkIncludeCRC);
            this.panel26.Controls.Add(this.chkIncludeSV);
            this.panel26.Controls.Add(this.chkBillEpsdtFamPlan);
            this.panel26.Controls.Add(this.cmbEPSDTCodeBox);
            this.panel26.Controls.Add(this.label145);
            this.panel26.Controls.Add(this.label146);
            this.panel26.Controls.Add(this.label147);
            this.panel26.Controls.Add(this.label148);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel26.Location = new System.Drawing.Point(0, 0);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(723, 467);
            this.panel26.TabIndex = 7;
            this.panel26.TabStop = true;
            // 
            // txtFamPlanCode
            // 
            this.txtFamPlanCode.Location = new System.Drawing.Point(295, 200);
            this.txtFamPlanCode.MaxLength = 2;
            this.txtFamPlanCode.Name = "txtFamPlanCode";
            this.txtFamPlanCode.Size = new System.Drawing.Size(107, 22);
            this.txtFamPlanCode.TabIndex = 196;
            // 
            // txtEPSDTCode
            // 
            this.txtEPSDTCode.Location = new System.Drawing.Point(295, 136);
            this.txtEPSDTCode.MaxLength = 2;
            this.txtEPSDTCode.Name = "txtEPSDTCode";
            this.txtEPSDTCode.Size = new System.Drawing.Size(107, 22);
            this.txtEPSDTCode.TabIndex = 195;
            // 
            // chkSuppressRenderPaperEdi
            // 
            this.chkSuppressRenderPaperEdi.AutoSize = true;
            this.chkSuppressRenderPaperEdi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSuppressRenderPaperEdi.Location = new System.Drawing.Point(74, 267);
            this.chkSuppressRenderPaperEdi.Name = "chkSuppressRenderPaperEdi";
            this.chkSuppressRenderPaperEdi.Size = new System.Drawing.Size(388, 18);
            this.chkSuppressRenderPaperEdi.TabIndex = 194;
            this.chkSuppressRenderPaperEdi.Text = "Suppress Rendering on Paper and EDI for EPSDT Screening Claims";
            this.chkSuppressRenderPaperEdi.UseVisualStyleBackColor = true;
            // 
            // cmbFamPlanCodeBox
            // 
            this.cmbFamPlanCodeBox.BackColor = System.Drawing.SystemColors.Window;
            this.cmbFamPlanCodeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFamPlanCodeBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFamPlanCodeBox.FormattingEnabled = true;
            this.cmbFamPlanCodeBox.ItemHeight = 14;
            this.cmbFamPlanCodeBox.Location = new System.Drawing.Point(295, 232);
            this.cmbFamPlanCodeBox.Name = "cmbFamPlanCodeBox";
            this.cmbFamPlanCodeBox.Size = new System.Drawing.Size(107, 22);
            this.cmbFamPlanCodeBox.TabIndex = 193;
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(74, 236);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(217, 14);
            this.label149.TabIndex = 192;
            this.label149.Text = "Paper Claim Family Planning code Box :";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(98, 204);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(193, 14);
            this.label150.TabIndex = 191;
            this.label150.Text = "Paper Claim Family Planning code :";
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label144.Location = new System.Drawing.Point(118, 172);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(173, 14);
            this.label144.TabIndex = 190;
            this.label144.Text = "Paper Claim EPSDT code Box :";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(142, 140);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(149, 14);
            this.label141.TabIndex = 189;
            this.label141.Text = "Paper Claim EPSDT code :";
            // 
            // chkIncludeRefCode
            // 
            this.chkIncludeRefCode.AutoSize = true;
            this.chkIncludeRefCode.Location = new System.Drawing.Point(74, 108);
            this.chkIncludeRefCode.Name = "chkIncludeRefCode";
            this.chkIncludeRefCode.Size = new System.Drawing.Size(256, 18);
            this.chkIncludeRefCode.TabIndex = 188;
            this.chkIncludeRefCode.Text = "Paper – Include Referral Code, Box 10(d) ";
            this.chkIncludeRefCode.UseVisualStyleBackColor = true;
            // 
            // chkIncludeCRC
            // 
            this.chkIncludeCRC.AutoSize = true;
            this.chkIncludeCRC.Location = new System.Drawing.Point(74, 80);
            this.chkIncludeCRC.Name = "chkIncludeCRC";
            this.chkIncludeCRC.Size = new System.Drawing.Size(219, 18);
            this.chkIncludeCRC.TabIndex = 187;
            this.chkIncludeCRC.Text = "EDI – Include CRC - EPSDT Referral";
            this.chkIncludeCRC.UseVisualStyleBackColor = true;
            // 
            // chkIncludeSV
            // 
            this.chkIncludeSV.AutoSize = true;
            this.chkIncludeSV.Location = new System.Drawing.Point(74, 52);
            this.chkIncludeSV.Name = "chkIncludeSV";
            this.chkIncludeSV.Size = new System.Drawing.Size(208, 18);
            this.chkIncludeSV.TabIndex = 186;
            this.chkIncludeSV.Text = "EDI – Include SV111 and SV112 ";
            this.chkIncludeSV.UseVisualStyleBackColor = true;
            // 
            // chkBillEpsdtFamPlan
            // 
            this.chkBillEpsdtFamPlan.AutoSize = true;
            this.chkBillEpsdtFamPlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBillEpsdtFamPlan.Location = new System.Drawing.Point(17, 16);
            this.chkBillEpsdtFamPlan.Name = "chkBillEpsdtFamPlan";
            this.chkBillEpsdtFamPlan.Size = new System.Drawing.Size(199, 18);
            this.chkBillEpsdtFamPlan.TabIndex = 185;
            this.chkBillEpsdtFamPlan.Text = "Bill EPSDT / Family Planning ";
            this.chkBillEpsdtFamPlan.UseVisualStyleBackColor = true;
            this.chkBillEpsdtFamPlan.CheckedChanged += new System.EventHandler(this.chkBillEpsdtFamPlan_CheckedChanged);
            // 
            // cmbEPSDTCodeBox
            // 
            this.cmbEPSDTCodeBox.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEPSDTCodeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEPSDTCodeBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEPSDTCodeBox.FormattingEnabled = true;
            this.cmbEPSDTCodeBox.ItemHeight = 14;
            this.cmbEPSDTCodeBox.Location = new System.Drawing.Point(295, 168);
            this.cmbEPSDTCodeBox.Name = "cmbEPSDTCodeBox";
            this.cmbEPSDTCodeBox.Size = new System.Drawing.Size(107, 22);
            this.cmbEPSDTCodeBox.TabIndex = 143;
            // 
            // label145
            // 
            this.label145.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label145.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label145.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(1, 466);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(721, 1);
            this.label145.TabIndex = 7;
            this.label145.Text = "label1";
            // 
            // label146
            // 
            this.label146.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label146.Dock = System.Windows.Forms.DockStyle.Top;
            this.label146.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label146.Location = new System.Drawing.Point(1, 0);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(721, 1);
            this.label146.TabIndex = 6;
            this.label146.Text = "label1";
            // 
            // label147
            // 
            this.label147.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label147.Dock = System.Windows.Forms.DockStyle.Right;
            this.label147.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label147.Location = new System.Drawing.Point(722, 0);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(1, 467);
            this.label147.TabIndex = 5;
            this.label147.Text = "label4";
            // 
            // label148
            // 
            this.label148.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label148.Dock = System.Windows.Forms.DockStyle.Left;
            this.label148.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label148.Location = new System.Drawing.Point(0, 0);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(1, 467);
            this.label148.TabIndex = 4;
            this.label148.Text = "label4";
            // 
            // tpAnesthesia
            // 
            this.tpAnesthesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tpAnesthesia.Controls.Add(this.panel27);
            this.tpAnesthesia.Location = new System.Drawing.Point(4, 42);
            this.tpAnesthesia.Name = "tpAnesthesia";
            this.tpAnesthesia.Padding = new System.Windows.Forms.Padding(3);
            this.tpAnesthesia.Size = new System.Drawing.Size(723, 467);
            this.tpAnesthesia.TabIndex = 10;
            this.tpAnesthesia.Text = "Anesthesia";
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.label154);
            this.panel27.Controls.Add(this.cmbBillUnitsAs);
            this.panel27.Controls.Add(this.label142);
            this.panel27.Controls.Add(this.label143);
            this.panel27.Controls.Add(this.label151);
            this.panel27.Controls.Add(this.label152);
            this.panel27.Controls.Add(this.txtBaseUnits);
            this.panel27.Controls.Add(this.label153);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel27.Location = new System.Drawing.Point(3, 3);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(717, 461);
            this.panel27.TabIndex = 131;
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(32, 49);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(74, 14);
            this.label154.TabIndex = 192;
            this.label154.Text = "Bill Units as :";
            // 
            // cmbBillUnitsAs
            // 
            this.cmbBillUnitsAs.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBillUnitsAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillUnitsAs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillUnitsAs.FormattingEnabled = true;
            this.cmbBillUnitsAs.ItemHeight = 14;
            this.cmbBillUnitsAs.Location = new System.Drawing.Point(110, 45);
            this.cmbBillUnitsAs.Name = "cmbBillUnitsAs";
            this.cmbBillUnitsAs.Size = new System.Drawing.Size(107, 22);
            this.cmbBillUnitsAs.TabIndex = 191;
            // 
            // label142
            // 
            this.label142.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label142.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label142.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label142.Location = new System.Drawing.Point(1, 460);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(715, 1);
            this.label142.TabIndex = 132;
            this.label142.Text = "label2";
            // 
            // label143
            // 
            this.label143.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label143.Dock = System.Windows.Forms.DockStyle.Right;
            this.label143.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(716, 1);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(1, 460);
            this.label143.TabIndex = 131;
            this.label143.Text = "label4";
            // 
            // label151
            // 
            this.label151.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label151.Dock = System.Windows.Forms.DockStyle.Top;
            this.label151.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label151.Location = new System.Drawing.Point(1, 0);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(716, 1);
            this.label151.TabIndex = 130;
            this.label151.Text = "label2";
            // 
            // label152
            // 
            this.label152.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label152.Dock = System.Windows.Forms.DockStyle.Left;
            this.label152.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(0, 0);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(1, 461);
            this.label152.TabIndex = 127;
            this.label152.Text = "label4";
            // 
            // txtBaseUnits
            // 
            this.txtBaseUnits.Location = new System.Drawing.Point(110, 16);
            this.txtBaseUnits.MaxLength = 3;
            this.txtBaseUnits.Name = "txtBaseUnits";
            this.txtBaseUnits.Size = new System.Drawing.Size(107, 22);
            this.txtBaseUnits.TabIndex = 125;
            this.txtBaseUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBaseUnits_KeyPress);
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(25, 20);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(81, 14);
            this.label153.TabIndex = 124;
            this.label153.Text = "Min Per Unit :";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.BorderColor = System.Drawing.Color.Black;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.C1SuperTooltip1.ForeColor = System.Drawing.Color.Black;
            this.C1SuperTooltip1.IsBalloon = true;
            // 
            // chkGroupMandatory
            // 
            this.chkGroupMandatory.AutoSize = true;
            this.chkGroupMandatory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupMandatory.Location = new System.Drawing.Point(581, 166);
            this.chkGroupMandatory.Name = "chkGroupMandatory";
            this.chkGroupMandatory.Size = new System.Drawing.Size(120, 18);
            this.chkGroupMandatory.TabIndex = 203;
            this.chkGroupMandatory.Text = "Group Mandatory";
            this.chkGroupMandatory.UseVisualStyleBackColor = true;
            // 
            // frmSetupInsurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(731, 566);
            this.Controls.Add(this.tbInsuranceSetup);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupInsurance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Insurance Plan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupInsurance_FormClosing);
            this.Load += new System.EventHandler(this.frmSetupInsurance_Load);
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.tbp_BillingSettings.ResumeLayout(false);
            this.pnlBillingDetailsMain.ResumeLayout(false);
            this.pnlFlexGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProviderSettings)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.pnlBillingSourceOther.ResumeLayout(false);
            this.pnlBillingSourceOther.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.pnlServiceFacilityOther.ResumeLayout(false);
            this.pnlServiceFacilityOther.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBillingHeader.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tbp_MidLevel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1MidlevelSettings)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tbp_InsurancePlan.ResumeLayout(false);
            this.pnl_Base.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.GBox_GeneralInfo.ResumeLayout(false);
            this.GBox_GeneralInfo.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GBox_Companyadrs.ResumeLayout(false);
            this.GBox_Companyadrs.PerformLayout();
            this.gBoxComContact.ResumeLayout(false);
            this.gBoxComContact.PerformLayout();
            this.pnlHoldMessage.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tbInsuranceSetup.ResumeLayout(false);
            this.tbp_Eligibility.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.tbp_BillingTaxon.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BillingTaxonomy)).EndInit();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.tbp_5010Transition.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            this.tbp_AlternatePayerID.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMasters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1AlternatePayerID)).EndInit();
            this.panel22.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.tbp_Institutional.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tbp_Collection.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInsClmRebillFUActionDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInsClmStartFUActionDays)).EndInit();
            this.tbp_EPSDT.ResumeLayout(false);
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.tpAnesthesia.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolTip ttInsuranceType;
        private System.Windows.Forms.ToolStripButton ts_btnFeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnSave_FeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnAddLine;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveLine;
        private System.Windows.Forms.ToolStripButton ts_btnClose_FeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnImportFeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnAdd_FeeSchedule;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tls_Hold;
        private System.Windows.Forms.ToolStripButton tls_Release;
        private System.Windows.Forms.TabPage tbp_BillingSettings;
        private System.Windows.Forms.Panel pnlBillingDetailsMain;
        internal System.Windows.Forms.Panel pnlFlexGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProviderSettings;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label95;
        internal System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel pnlBillingSourceOther;
        private System.Windows.Forms.CheckBox chkBillingProviderSwap;
        private System.Windows.Forms.ComboBox cmbBillingProviderSourceOtherIDType;
        internal System.Windows.Forms.Label lblProviderSourceChkSwap;
        internal System.Windows.Forms.Label label91;
        private System.Windows.Forms.CheckBox chkBillingProviderOtherID;
        internal System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.ComboBox cmbBillingProviderSource;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        internal System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        internal System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox chkOtherIDonEDI;
        private System.Windows.Forms.Panel pnlServiceFacilityOther;
        private System.Windows.Forms.CheckBox chkServiceFacilitySwap;
        private System.Windows.Forms.ComboBox cmbServiceFacilityOtherIDType;
        internal System.Windows.Forms.Label lblServiceFacChkSwap;
        internal System.Windows.Forms.Label label72;
        private System.Windows.Forms.CheckBox chkServiceFacOtherID;
        internal System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox cmbServiceFacilitySource;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        internal System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Label lblElectronicRendering;
        private System.Windows.Forms.Label label48;
        internal System.Windows.Forms.Label lblPaperRendering;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox cmbElectronicRendering;
        private System.Windows.Forms.ComboBox cmbPaperRendering;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        internal System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.TextBox TxtDiagnosisperClaim;
        internal System.Windows.Forms.TextBox TxtChargesperClaim;
        private System.Windows.Forms.Label lblDiagnosisperClaim;
        private System.Windows.Forms.Label lblChargeperClaim;
        private System.Windows.Forms.ComboBox cmbClearingHouse;
        private System.Windows.Forms.ComboBox cmbTypeOFBilling;
        internal System.Windows.Forms.Label label75;
        private System.Windows.Forms.CheckBox chkCorrectRplmnt;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        internal System.Windows.Forms.Label lblCptCrosswalk;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFeeSchedules;
        private System.Windows.Forms.CheckBox chkNotesInBox19;
        private System.Windows.Forms.ComboBox cmbCptCrosswalk;
        private System.Windows.Forms.CheckBox chkIncludeOTAFAmount;
        private System.Windows.Forms.ComboBox cmbdonotprintfacility;
        private System.Windows.Forms.CheckBox chkRefferingID;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkAcceptAssignment;
        internal System.Windows.Forms.Panel pnlBillingHeader;
        internal System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBillingSettings;
        private System.Windows.Forms.Label lblLeftAlign;
        private System.Windows.Forms.Label lblRightAlign;
        private System.Windows.Forms.Label lblTopAlign;
        private System.Windows.Forms.Label lblBottomAlign;
        private System.Windows.Forms.TabPage tbp_MidLevel;
        internal System.Windows.Forms.Panel panel3;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MidlevelSettings;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cmbMidLevelSpeProvider;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage tbp_InsurancePlan;
        private System.Windows.Forms.Panel pnl_Base;
        internal System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.Label label76;
        internal System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbBox33B;
        private System.Windows.Forms.ComboBox cmbBox33A;
        private System.Windows.Forms.ComboBox cmbBox33;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbBox32B;
        private System.Windows.Forms.ComboBox cmbBox32;
        private System.Windows.Forms.ComboBox cmbBox32A;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.CheckBox chkPARequired;
        internal System.Windows.Forms.Label lblInsEligibilityProviderID;
        private System.Windows.Forms.TextBox txtInsEligibilityProvderID;
        internal System.Windows.Forms.Panel GBox_GeneralInfo;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label74;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        internal System.Windows.Forms.TextBox txtname;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtPayerID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbInsuranceCompany;
        internal System.Windows.Forms.Label Label1;
        private gloMaskControl.gloMaskBox mtxtPhone;
        private System.Windows.Forms.Label label67;
        internal System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cmbReportingCategory;
        private System.Windows.Forms.Label label69;
        internal System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label73;
        internal System.Windows.Forms.TextBox txtcontact;
        internal System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label40;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.ComboBox cmbInsuranceType;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.Label label37;
        internal gloMaskControl.gloMaskBox txtFax;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label36;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_PayerPhExt;
        private gloMaskControl.gloMaskBox mtxt_PayerPhone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.TextBox txtAdditionalInfo;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox txtServicingState;
        private System.Windows.Forms.TextBox txtOfficeID;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbAcceptAssignmentYes;
        private System.Windows.Forms.RadioButton rbAcceptAssignmentNo;
        private System.Windows.Forms.CheckBox chkShowPayment;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkNameOfFacilityinBox33;
        private System.Windows.Forms.CheckBox chkRemittanceAdvice;
        private System.Windows.Forms.CheckBox chkDoNotPrintFacility;
        internal System.Windows.Forms.Label lblOfficeID;
        private System.Windows.Forms.CheckBox chkBox31Blank;
        private System.Windows.Forms.CheckBox chkEnrollmentRequired;
        private System.Windows.Forms.CheckBox chkOnlyPrintFirstPointer;
        private System.Windows.Forms.CheckBox chkElectronicCOB;
        private System.Windows.Forms.CheckBox chkRealTimeClaimStatus;
        private System.Windows.Forms.CheckBox chkMedigap;
        private System.Windows.Forms.CheckBox chkClaims;
        internal System.Windows.Forms.GroupBox GBox_Companyadrs;
        private System.Windows.Forms.TextBox txtState;
        internal System.Windows.Forms.TextBox txtZip;
        internal System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox txtAddressLine2;
        internal System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtCity;
        internal System.Windows.Forms.TextBox txtAddressLine1;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.GroupBox gBoxComContact;
        private System.Windows.Forms.CheckBox chkRealTimeEligibility;
        internal System.Windows.Forms.Panel pnlHoldMessage;
        internal System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblHoldMessage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TabControl tbInsuranceSetup;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbBox30;
        private System.Windows.Forms.Label Box30;
        private System.Windows.Forms.Label Box29;
        private System.Windows.Forms.ComboBox cmbBox29;
        private System.Windows.Forms.TabPage tbp_BillingTaxon;
        internal System.Windows.Forms.Panel panel17;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BillingTaxonomy;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        internal System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.CheckBox chkIncludeTax4Paper;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        internal System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.CheckBox chkIncludeTax4Elec;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.ComboBox cmbQualifier;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.ComboBox cmbReferringProviderOtherIDType;
        internal System.Windows.Forms.Label label108;
        private System.Windows.Forms.TabPage tbp_Eligibility;
        internal System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.ComboBox cmbInsEligibilitySecProvType;
        internal System.Windows.Forms.Label label120;
        internal System.Windows.Forms.Label label121;
        internal System.Windows.Forms.Label label107;
        private System.Windows.Forms.TextBox txtInsEligibilitySecProvID;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        internal System.Windows.Forms.Label label119;
        private System.Windows.Forms.TextBox txtInsEligibilityPrimProvID;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.ComboBox cmbInsEligibilityPrimProvType;
        private System.Windows.Forms.TabPage tbp_5010Transition;
        internal System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.CheckBox chkIncludeRendering;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.CheckBox chkIncludeSubscriberAddress;
        private System.Windows.Forms.CheckBox chkIncludeServiceFacility;
        private System.Windows.Forms.TabPage tbp_AlternatePayerID;
        internal System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.DataGridView dgMasters;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AlternatePayerID;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label label123;
        internal System.Windows.Forms.Panel panel22;
        internal System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.ToolStripButton ts_btnNewAlternateID;
        private System.Windows.Forms.ToolStripButton ts_btnEditAlternateID;
        private System.Windows.Forms.ToolStripButton ts_btnDeleteAlternateID;
        private System.Windows.Forms.CheckBox chkSwap1a9a1aMCare;
        private System.Windows.Forms.CheckBox chkPaperDisplayMailingAddress;
        private System.Windows.Forms.CheckBox chkIncludePlanName;
        private System.Windows.Forms.TextBox txtBillClaimOfficeNo;
        private System.Windows.Forms.ComboBox cmbClIAPostn;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbStatementToPatientYes;
        private System.Windows.Forms.RadioButton rbStatementToPatientNo;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIDInBox31;
        private System.Windows.Forms.TabPage tbp_Institutional;
        internal System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.CheckBox chkIsInstitutionalBilling;
        private System.Windows.Forms.CheckBox chkIncludeRendering_Attending;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.CheckBox chkIncludeTaxBilPaper;
        private System.Windows.Forms.CheckBox chkIncludeTaxRenPaper;
        private System.Windows.Forms.CheckBox chkIncludeTaxBillElec;
        private System.Windows.Forms.CheckBox chkIncludeTaxRenElec;
        internal System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtEligibiltyNote;
        internal System.Windows.Forms.Label lblphone;
        internal System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.TextBox TxtEligibiltyWebste;
        internal System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox TxtEligiblitycntct;
        private gloMaskControl.gloMaskBox mskeligibiltyPhone;
        private System.Windows.Forms.TabPage tbp_Collection;
        internal System.Windows.Forms.Panel panel25;
        internal System.Windows.Forms.Label label137;
        internal System.Windows.Forms.Label label138;
        private System.Windows.Forms.ComboBox cmbInsClmStartFUAction;
        private System.Windows.Forms.ComboBox cmbInsClmRebillFUAction;
        internal System.Windows.Forms.Label label139;
        internal System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.NumericUpDown numInsClmRebillFUActionDays;
        private System.Windows.Forms.NumericUpDown numInsClmStartFUActionDays;
        private System.Windows.Forms.TabPage tbp_EPSDT;
        internal System.Windows.Forms.Panel panel26;
        private System.Windows.Forms.TextBox txtFamPlanCode;
        private System.Windows.Forms.TextBox txtEPSDTCode;
        private System.Windows.Forms.CheckBox chkSuppressRenderPaperEdi;
        private System.Windows.Forms.ComboBox cmbFamPlanCodeBox;
        internal System.Windows.Forms.Label label149;
        internal System.Windows.Forms.Label label150;
        internal System.Windows.Forms.Label label144;
        internal System.Windows.Forms.Label label141;
        private System.Windows.Forms.CheckBox chkIncludeRefCode;
        private System.Windows.Forms.CheckBox chkIncludeCRC;
        private System.Windows.Forms.CheckBox chkIncludeSV;
        private System.Windows.Forms.CheckBox chkBillEpsdtFamPlan;
        private System.Windows.Forms.ComboBox cmbEPSDTCodeBox;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label146;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.CheckBox ChkEMGAsX;
        private System.Windows.Forms.CheckBox chkShowClaim;
        private System.Windows.Forms.TabPage tpAnesthesia;
        private System.Windows.Forms.Panel panel27;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.TextBox txtBaseUnits;
        private System.Windows.Forms.Label label153;
        internal System.Windows.Forms.Label label154;
        private System.Windows.Forms.ComboBox cmbBillUnitsAs;
        private System.Windows.Forms.CheckBox chkWorkersComp;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.CheckBox chkClaimFreq;
        private System.Windows.Forms.CheckBox chkMedClaimRef;
        private System.Windows.Forms.ComboBox cmbRefSecIdentification;
        private System.Windows.Forms.CheckBox chkDefaultDOS;
        private System.Windows.Forms.ComboBox cmbOprtingPrvderBox77;
        private System.Windows.Forms.Label label155;
        private System.Windows.Forms.Label lblFedTaxNoBox5;
        private System.Windows.Forms.Label label156;
        private System.Windows.Forms.Label label157;
        internal System.Windows.Forms.TextBox txtBox19DefaultNote;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbFedTaxNoBox5;
        private System.Windows.Forms.ComboBox cmbUBBlngprvdraltID;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.ComboBox cmbBox11bSettings;
        private System.Windows.Forms.CheckBox chkIncludeOrdering;
        private System.Windows.Forms.CheckBox chkEdiAltPayerID;
        private System.Windows.Forms.ComboBox cmbCMSDateFormat;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.CheckBox chkIncludeRefnOrdering;
        private System.Windows.Forms.CheckBox chkIncludeRefnSupervising;
        private System.Windows.Forms.Label label164;
        private System.Windows.Forms.CheckBox chkReportClinicName;
        private System.Windows.Forms.ComboBox cmbBox77Rendering;
        private System.Windows.Forms.Label label165;
        private System.Windows.Forms.CheckBox chkIncludeUB04AdmissionHour;
        private System.Windows.Forms.CheckBox chkIncludeUB04DischargeHour;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.RadioButton rbDoNotSend;
        private System.Windows.Forms.RadioButton rbSend;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label167;
        private System.Windows.Forms.CheckBox ckhSentUB04RevenuecodeTotal;
        private System.Windows.Forms.CheckBox chkSecondaryPayerAddress;
        private System.Windows.Forms.CheckBox chkIncludePatientSSN;
        private System.Windows.Forms.CheckBox chkIncludeMod_in_SVD;
        private System.Windows.Forms.CheckBox chkIncludePrimaryDxInBox69;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cmb81CValue;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.ComboBox cmb81BValue;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.ComboBox cmbExtendedZipCode;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.ComboBox cmb81AValue;
        private System.Windows.Forms.Label label174;
        internal System.Windows.Forms.TextBox txt81CQual;
        internal System.Windows.Forms.TextBox txt81BQual;
        internal System.Windows.Forms.TextBox txt81AQual;
        internal System.Windows.Forms.TextBox txt81DQual;
        private System.Windows.Forms.ComboBox cmb81DValue;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.CheckBox ChkIncludeAttendingPrvTaxonomy;
        private System.Windows.Forms.CheckBox chkIncludeEstimatedAmtDue;
        private System.Windows.Forms.CheckBox chkGroupMandatory;
    }
}