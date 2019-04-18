namespace gloEMRReports
{
    partial class frmSSRSViewer
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpicTo, dtpicFrom, dtToAppt, dtFromAppt, dtToDueDate, dtFromDueDate, };
            System.Windows.Forms.Control[] cntControls = { dtpicTo, dtpicFrom, dtToAppt, dtFromAppt, dtToDueDate, dtFromDueDate, };
 
            if (disposing && (components != null))
            {
                components.Dispose();
                try
                {
                    if (saveFileDialog1 != null)
                    {
                        try
                        {
                            saveFileDialog1.FileOk -= new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
                        }
                        catch
                        {
                        }
                        saveFileDialog1.Dispose();
                        saveFileDialog1 = null;
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



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSSRSViewer));
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tblStrip_32 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tblButtonShow = new System.Windows.Forms.ToolStripButton();
            this.tsb_ReminderLetters = new System.Windows.Forms.ToolStripButton();
            this.tblbtnGenReport = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Print_32 = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Export = new System.Windows.Forms.ToolStripButton();
            this.Tblbtn_More = new System.Windows.Forms.ToolStripButton();
            this.Tblbtn_Hide = new System.Windows.Forms.ToolStripButton();
            this.Tblbtn_DisplayOptn = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Close_32 = new System.Windows.Forms.ToolStripButton();
            this.pnlcustomTask = new System.Windows.Forms.Panel();
            this.pnlSSRSRpt = new System.Windows.Forms.Panel();
            this.C1Patients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlWarning = new System.Windows.Forms.Panel();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.chkShowAllProviders = new System.Windows.Forms.CheckBox();
            this.pnlMedicationDate = new System.Windows.Forms.Panel();
            this.label75 = new System.Windows.Forms.Label();
            this.dtMedicationEndDate = new System.Windows.Forms.DateTimePicker();
            this.label76 = new System.Windows.Forms.Label();
            this.dtMedicationStartDate = new System.Windows.Forms.DateTimePicker();
            this.label77 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpicTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpicFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblAgeTo = new System.Windows.Forms.Label();
            this.lblAgeFrom = new System.Windows.Forms.Label();
            this.cmbAgeTo = new System.Windows.Forms.ComboBox();
            this.cmbAgeFrom = new System.Windows.Forms.ComboBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmbAge = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Lblage = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.chkExcControlledSubstance = new System.Windows.Forms.CheckBox();
            this.pnlDemo = new System.Windows.Forms.Panel();
            this.chkDOB = new System.Windows.Forms.CheckBox();
            this.chkEthnicity = new System.Windows.Forms.CheckBox();
            this.chkRace = new System.Windows.Forms.CheckBox();
            this.chkGender = new System.Windows.Forms.CheckBox();
            this.chkInsurance = new System.Windows.Forms.CheckBox();
            this.chkLanguage = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lblDemoElement = new System.Windows.Forms.Label();
            this.chkOnlyDrug = new System.Windows.Forms.CheckBox();
            this.pnlCheckBoxes = new System.Windows.Forms.Panel();
            this.chkShowUsageDeatal = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlMed = new System.Windows.Forms.Panel();
            this.rbtnAllMedications = new System.Windows.Forms.RadioButton();
            this.rbtnPresByClinic = new System.Windows.Forms.RadioButton();
            this.BtnClearAllDrg = new System.Windows.Forms.Button();
            this.btnClearDrug = new System.Windows.Forms.Button();
            this.btnBrowseDrug = new System.Windows.Forms.Button();
            this.LstMedication = new System.Windows.Forms.ListBox();
            this.lblMedication = new System.Windows.Forms.Label();
            this.pnlDiag = new System.Windows.Forms.Panel();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.btnClearAllDiag = new System.Windows.Forms.Button();
            this.btnClearDiag = new System.Windows.Forms.Button();
            this.btnBrowseDiag = new System.Windows.Forms.Button();
            this.LstDiagnosis = new System.Windows.Forms.ListBox();
            this.pnlDrugDiagnosis = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pnlDmPatientList = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cmbapptst = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.ChkAppt = new System.Windows.Forms.CheckBox();
            this.dtToAppt = new System.Windows.Forms.DateTimePicker();
            this.dtFromAppt = new System.Windows.Forms.DateTimePicker();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.btnClearAllDMPatientprb = new System.Windows.Forms.Button();
            this.btnClearDMPatientprb = new System.Windows.Forms.Button();
            this.btnBrowseDMPatientPrb = new System.Windows.Forms.Button();
            this.lstDMProblemList = new System.Windows.Forms.ListBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chkDueDate = new System.Windows.Forms.CheckBox();
            this.dtToDueDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDueDate = new System.Windows.Forms.DateTimePicker();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.BtnClearAllDMSetup = new System.Windows.Forms.Button();
            this.btnClearDMSetup = new System.Windows.Forms.Button();
            this.btnBrowseDMSetup = new System.Windows.Forms.Button();
            this.lstdmsetup = new System.Windows.Forms.ListBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.btnClearAllDMMedication = new System.Windows.Forms.Button();
            this.btnClearDMMedication = new System.Windows.Forms.Button();
            this.btnBrowseDMMedication = new System.Windows.Forms.Button();
            this.lstmeddmsetup = new System.Windows.Forms.ListBox();
            this.pnlPatientList = new System.Windows.Forms.Panel();
            this.btnBrowseSnomedCT = new System.Windows.Forms.Button();
            this.cmbMediAll = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.BtnClearAllAllergy = new System.Windows.Forms.Button();
            this.BtnClearAllergy = new System.Windows.Forms.Button();
            this.BtnBrowserAllergy = new System.Windows.Forms.Button();
            this.lstAllergy = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbImGiven = new System.Windows.Forms.RadioButton();
            this.rbImNotGiven = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.BtnClearAllPatientDrug = new System.Windows.Forms.Button();
            this.btnClearPatientDrug = new System.Windows.Forms.Button();
            this.btnBrowsePatientDrug = new System.Windows.Forms.Button();
            this.lstPatMedication = new System.Windows.Forms.ListBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.BtnClearAllImmunization = new System.Windows.Forms.Button();
            this.btnClearImmunization = new System.Windows.Forms.Button();
            this.btnSearchImmunization = new System.Windows.Forms.Button();
            this.lstImmunization = new System.Windows.Forms.ListBox();
            this.cmbSndPat = new System.Windows.Forms.ComboBox();
            this.cmb3rdPat = new System.Windows.Forms.ComboBox();
            this.cmbFstPat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.btnClearAllPatientprb = new System.Windows.Forms.Button();
            this.btnClearPatientprb = new System.Windows.Forms.Button();
            this.btnBrowsePatientPrb = new System.Windows.Forms.Button();
            this.lstProblemList = new System.Windows.Forms.ListBox();
            this.pnlTreat = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.btnBrowseLabTestResult = new System.Windows.Forms.Button();
            this.lstLabResult = new System.Windows.Forms.ListBox();
            this.btnClearAllPatientLab = new System.Windows.Forms.Button();
            this.btnClearPatientLab = new System.Windows.Forms.Button();
            this.pnlLabTestResult = new System.Windows.Forms.Panel();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.lblPatTo = new System.Windows.Forms.Label();
            this.lblPatFrom = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlTO = new System.Windows.Forms.Panel();
            this.btlCloseLabResult = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.btnAddResultCond = new System.Windows.Forms.Button();
            this.lblNext = new System.Windows.Forms.Label();
            this.cmbThiPat = new System.Windows.Forms.ComboBox();
            this.lblToBlank = new System.Windows.Forms.Label();
            this.txtPatTo = new System.Windows.Forms.TextBox();
            this.lblForm = new System.Windows.Forms.Label();
            this.txtPatFrom = new System.Windows.Forms.TextBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.cmbPatCondition = new System.Windows.Forms.ComboBox();
            this.lblLab = new System.Windows.Forms.Label();
            this.btnBrowseLabResults = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.cmbLabResult = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlDemoFilter = new System.Windows.Forms.Panel();
            this.btnClearInsPlan = new System.Windows.Forms.Button();
            this.btnBrowseInsPlan = new System.Windows.Forms.Button();
            this.label72 = new System.Windows.Forms.Label();
            this.cmbPatInsPlan = new System.Windows.Forms.ComboBox();
            this.btnClearMedCategory = new System.Windows.Forms.Button();
            this.btnBrowseMedCategory = new System.Windows.Forms.Button();
            this.label64 = new System.Windows.Forms.Label();
            this.cmbMedicalCategory = new System.Windows.Forms.ComboBox();
            this.btnClearCommPref = new System.Windows.Forms.Button();
            this.btnBrowseCommPref = new System.Windows.Forms.Button();
            this.btnClearLanguage = new System.Windows.Forms.Button();
            this.btnBrowseLanguage = new System.Windows.Forms.Button();
            this.btnclearethnicity = new System.Windows.Forms.Button();
            this.btnBrowseEthnicity = new System.Windows.Forms.Button();
            this.btnCleaseRace = new System.Windows.Forms.Button();
            this.btnBrowseRace = new System.Windows.Forms.Button();
            this.btncleargender = new System.Windows.Forms.Button();
            this.btnBrowseGender = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.cmbComPre = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.cmblanguage = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.cmbethnicity = new System.Windows.Forms.ComboBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.cmbRace = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.pnlIncludeDemographics = new System.Windows.Forms.Panel();
            this.pnlPstnOthr = new System.Windows.Forms.Panel();
            this.chkFacility = new System.Windows.Forms.CheckBox();
            this.chkClaimDX = new System.Windows.Forms.CheckBox();
            this.chkClaimCPT = new System.Windows.Forms.CheckBox();
            this.chkDateTime = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrAllergy = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrLabResult = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrImmunization = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrMedication = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrProblemList = new System.Windows.Forms.CheckBox();
            this.chkPrblmLstOthrElement = new System.Windows.Forms.CheckBox();
            this.label61 = new System.Windows.Forms.Label();
            this.pnlPtntLstDemo = new System.Windows.Forms.Panel();
            this.chksZip = new System.Windows.Forms.CheckBox();
            this.chksAddressLine1 = new System.Windows.Forms.CheckBox();
            this.chksAddressLine2 = new System.Windows.Forms.CheckBox();
            this.chksState = new System.Windows.Forms.CheckBox();
            this.chksCity = new System.Windows.Forms.CheckBox();
            this.chkPtLastName = new System.Windows.Forms.CheckBox();
            this.chkPCP = new System.Windows.Forms.CheckBox();
            this.chkPtMiddleName = new System.Windows.Forms.CheckBox();
            this.chkPatNotes = new System.Windows.Forms.CheckBox();
            this.chkPtFirstName = new System.Windows.Forms.CheckBox();
            this.chkPriInsPlan = new System.Windows.Forms.CheckBox();
            this.ChkSecInsPlan = new System.Windows.Forms.CheckBox();
            this.chkMobile = new System.Windows.Forms.CheckBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.chkPhone = new System.Windows.Forms.CheckBox();
            this.chkAddress = new System.Windows.Forms.CheckBox();
            this.chkPrblLMedicalCategory = new System.Windows.Forms.CheckBox();
            this.chksortlistwthnpatnt = new System.Windows.Forms.CheckBox();
            this.chkPrblLAge = new System.Windows.Forms.CheckBox();
            this.chkPrblLCommPref = new System.Windows.Forms.CheckBox();
            this.chkPrblLLangauge = new System.Windows.Forms.CheckBox();
            this.chkPrblLDOB = new System.Windows.Forms.CheckBox();
            this.chkPrblLEthnicity = new System.Windows.Forms.CheckBox();
            this.chkPrblLstRace = new System.Windows.Forms.CheckBox();
            this.chkPrblLstGender = new System.Windows.Forms.CheckBox();
            this.chkPrblLstDemographicAll = new System.Windows.Forms.CheckBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlClaimDetails = new System.Windows.Forms.Panel();
            this.pnlFacility = new System.Windows.Forms.Panel();
            this.label71 = new System.Windows.Forms.Label();
            this.btnClearAllFacility = new System.Windows.Forms.Button();
            this.btnClearFacility = new System.Windows.Forms.Button();
            this.btnBrowseFacility = new System.Windows.Forms.Button();
            this.lstFacility = new System.Windows.Forms.ListBox();
            this.pnlClaimDx = new System.Windows.Forms.Panel();
            this.label65 = new System.Windows.Forms.Label();
            this.btnClearAllClaimDx = new System.Windows.Forms.Button();
            this.btnClearClaimDx = new System.Windows.Forms.Button();
            this.btnBrowseClaimDx = new System.Windows.Forms.Button();
            this.lstClaimDx = new System.Windows.Forms.ListBox();
            this.label66 = new System.Windows.Forms.Label();
            this.pnlClaimCpt = new System.Windows.Forms.Panel();
            this.btnClearAllClaimCpt = new System.Windows.Forms.Button();
            this.btnClearClaimCpt = new System.Windows.Forms.Button();
            this.btnBrowseClaimCpt = new System.Windows.Forms.Button();
            this.lstClaimCpt = new System.Windows.Forms.ListBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tblStrip_32.SuspendLayout();
            this.pnlSSRSRpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).BeginInit();
            this.pnlWarning.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlMedicationDate.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.pnlDemo.SuspendLayout();
            this.pnlCheckBoxes.SuspendLayout();
            this.pnlMed.SuspendLayout();
            this.pnlDiag.SuspendLayout();
            this.pnlDrugDiagnosis.SuspendLayout();
            this.pnlDmPatientList.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlPatientList.SuspendLayout();
            this.panel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTreat.SuspendLayout();
            this.pnlLabTestResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlTO.SuspendLayout();
            this.pnlDemoFilter.SuspendLayout();
            this.pnlIncludeDemographics.SuspendLayout();
            this.pnlPstnOthr.SuspendLayout();
            this.pnlPtntLstDemo.SuspendLayout();
            this.pnlClaimDetails.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.pnlClaimDx.SuspendLayout();
            this.pnlClaimCpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(4, 0);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.SSRSViewer.Size = new System.Drawing.Size(1268, 102);
            this.SSRSViewer.TabIndex = 0;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlToolStrip.Controls.Add(this.tblStrip_32);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1276, 54);
            this.pnlToolStrip.TabIndex = 9;
            // 
            // tblStrip_32
            // 
            this.tblStrip_32.BackColor = System.Drawing.Color.Transparent;
            this.tblStrip_32.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblStrip_32.BackgroundImage")));
            this.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblStrip_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblStrip_32.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tblStrip_32.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblButtonShow,
            this.tsb_ReminderLetters,
            this.tblbtnGenReport,
            this.tblbtn_Print_32,
            this.tblbtn_Export,
            this.Tblbtn_More,
            this.Tblbtn_Hide,
            this.Tblbtn_DisplayOptn,
            this.tblbtn_Close_32});
            this.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tblStrip_32.Location = new System.Drawing.Point(0, 0);
            this.tblStrip_32.Name = "tblStrip_32";
            this.tblStrip_32.Size = new System.Drawing.Size(1276, 53);
            this.tblStrip_32.TabIndex = 6;
            this.tblStrip_32.TabStop = true;
            this.tblStrip_32.Text = "ToolStrip1";
            this.tblStrip_32.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tblStrip_32_ItemClicked);
            // 
            // tblButtonShow
            // 
            this.tblButtonShow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblButtonShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblButtonShow.Image = ((System.Drawing.Image)(resources.GetObject("tblButtonShow.Image")));
            this.tblButtonShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblButtonShow.Name = "tblButtonShow";
            this.tblButtonShow.Size = new System.Drawing.Size(101, 50);
            this.tblButtonShow.Tag = "ShowPatients";
            this.tblButtonShow.Text = "&Show Patients";
            this.tblButtonShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblButtonShow.Visible = false;
            // 
            // tsb_ReminderLetters
            // 
            this.tsb_ReminderLetters.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ReminderLetters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ReminderLetters.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ReminderLetters.Image")));
            this.tsb_ReminderLetters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ReminderLetters.Name = "tsb_ReminderLetters";
            this.tsb_ReminderLetters.Size = new System.Drawing.Size(110, 50);
            this.tsb_ReminderLetters.Tag = "SendReminders";
            this.tsb_ReminderLetters.Text = "Send &Reminders";
            this.tsb_ReminderLetters.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ReminderLetters.Visible = false;
            // 
            // tblbtnGenReport
            // 
            this.tblbtnGenReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtnGenReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtnGenReport.Image = ((System.Drawing.Image)(resources.GetObject("tblbtnGenReport.Image")));
            this.tblbtnGenReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtnGenReport.Name = "tblbtnGenReport";
            this.tblbtnGenReport.Size = new System.Drawing.Size(113, 50);
            this.tblbtnGenReport.Tag = "Generate Report";
            this.tblbtnGenReport.Text = "&Generate Report";
            this.tblbtnGenReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tblbtn_Print_32
            // 
            this.tblbtn_Print_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Print_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Print_32.Image")));
            this.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Print_32.Name = "tblbtn_Print_32";
            this.tblbtn_Print_32.Size = new System.Drawing.Size(41, 50);
            this.tblbtn_Print_32.Tag = "Print";
            this.tblbtn_Print_32.Text = "&Print";
            this.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tblbtn_Export
            // 
            this.tblbtn_Export.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Export.Image")));
            this.tblbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Export.Name = "tblbtn_Export";
            this.tblbtn_Export.Size = new System.Drawing.Size(52, 50);
            this.tblbtn_Export.Tag = "Export";
            this.tblbtn_Export.Text = "E&xport";
            this.tblbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Tblbtn_More
            // 
            this.Tblbtn_More.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tblbtn_More.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tblbtn_More.Image = ((System.Drawing.Image)(resources.GetObject("Tblbtn_More.Image")));
            this.Tblbtn_More.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tblbtn_More.Name = "Tblbtn_More";
            this.Tblbtn_More.Size = new System.Drawing.Size(46, 50);
            this.Tblbtn_More.Tag = "More ";
            this.Tblbtn_More.Text = "&More ";
            this.Tblbtn_More.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tblbtn_More.ToolTipText = "More Options";
            this.Tblbtn_More.Click += new System.EventHandler(this.Tblbtn_More_Click);
            // 
            // Tblbtn_Hide
            // 
            this.Tblbtn_Hide.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tblbtn_Hide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tblbtn_Hide.Image = ((System.Drawing.Image)(resources.GetObject("Tblbtn_Hide.Image")));
            this.Tblbtn_Hide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tblbtn_Hide.Name = "Tblbtn_Hide";
            this.Tblbtn_Hide.Size = new System.Drawing.Size(38, 50);
            this.Tblbtn_Hide.Tag = "Hide";
            this.Tblbtn_Hide.Text = "&Hide";
            this.Tblbtn_Hide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tblbtn_Hide.ToolTipText = "Hide Options";
            this.Tblbtn_Hide.Visible = false;
            this.Tblbtn_Hide.Click += new System.EventHandler(this.Tblbtn_Hide_Click);
            // 
            // Tblbtn_DisplayOptn
            // 
            this.Tblbtn_DisplayOptn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tblbtn_DisplayOptn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Tblbtn_DisplayOptn.Image = ((System.Drawing.Image)(resources.GetObject("Tblbtn_DisplayOptn.Image")));
            this.Tblbtn_DisplayOptn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tblbtn_DisplayOptn.Name = "Tblbtn_DisplayOptn";
            this.Tblbtn_DisplayOptn.Size = new System.Drawing.Size(100, 50);
            this.Tblbtn_DisplayOptn.Tag = "Display Option";
            this.Tblbtn_DisplayOptn.Text = "&Display Option";
            this.Tblbtn_DisplayOptn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Tblbtn_DisplayOptn.ToolTipText = "Display Option";
            this.Tblbtn_DisplayOptn.Visible = false;
            this.Tblbtn_DisplayOptn.Click += new System.EventHandler(this.Tblbtn_DisplayOptn_Click);
            // 
            // tblbtn_Close_32
            // 
            this.tblbtn_Close_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Close_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Close_32.Image")));
            this.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Close_32.Name = "tblbtn_Close_32";
            this.tblbtn_Close_32.Size = new System.Drawing.Size(43, 50);
            this.tblbtn_Close_32.Tag = "Close";
            this.tblbtn_Close_32.Text = "&Close";
            this.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlcustomTask
            // 
            this.pnlcustomTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlcustomTask.Location = new System.Drawing.Point(240, 232);
            this.pnlcustomTask.Name = "pnlcustomTask";
            this.pnlcustomTask.Size = new System.Drawing.Size(373, 225);
            this.pnlcustomTask.TabIndex = 225;
            // 
            // pnlSSRSRpt
            // 
            this.pnlSSRSRpt.Controls.Add(this.C1Patients);
            this.pnlSSRSRpt.Controls.Add(this.pnlWarning);
            this.pnlSSRSRpt.Controls.Add(this.pnlMessage);
            this.pnlSSRSRpt.Controls.Add(this.Label20);
            this.pnlSSRSRpt.Controls.Add(this.Label17);
            this.pnlSSRSRpt.Controls.Add(this.SSRSViewer);
            this.pnlSSRSRpt.Controls.Add(this.Label16);
            this.pnlSSRSRpt.Controls.Add(this.Label14);
            this.pnlSSRSRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSSRSRpt.Location = new System.Drawing.Point(0, 791);
            this.pnlSSRSRpt.Name = "pnlSSRSRpt";
            this.pnlSSRSRpt.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSSRSRpt.Size = new System.Drawing.Size(1276, 105);
            this.pnlSSRSRpt.TabIndex = 8;
            // 
            // C1Patients
            // 
            this.C1Patients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Patients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1Patients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Patients.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.C1Patients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Patients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Patients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Patients.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1Patients.Location = new System.Drawing.Point(4, 71);
            this.C1Patients.Name = "C1Patients";
            this.C1Patients.Rows.DefaultSize = 19;
            this.C1Patients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Patients.Size = new System.Drawing.Size(1268, 30);
            this.C1Patients.StyleInfo = resources.GetString("C1Patients.StyleInfo");
            this.C1Patients.TabIndex = 18;
            this.C1Patients.Visible = false;
            // 
            // pnlWarning
            // 
            this.pnlWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pnlWarning.Controls.Add(this.label62);
            this.pnlWarning.Controls.Add(this.label63);
            this.pnlWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWarning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlWarning.ForeColor = System.Drawing.Color.Maroon;
            this.pnlWarning.Location = new System.Drawing.Point(4, 24);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(1268, 47);
            this.pnlWarning.TabIndex = 217;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(17, 7);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(816, 36);
            this.label62.TabIndex = 214;
            this.label62.Text = resources.GetString("label62.Text");
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.Maroon;
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(0, 46);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(1268, 1);
            this.label63.TabIndex = 17;
            this.label63.Text = "label1";
            // 
            // pnlMessage
            // 
            this.pnlMessage.BackColor = System.Drawing.Color.Transparent;
            this.pnlMessage.Controls.Add(this.lblMessage);
            this.pnlMessage.Controls.Add(this.label45);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMessage.Location = new System.Drawing.Point(4, 1);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Size = new System.Drawing.Size(1268, 23);
            this.pnlMessage.TabIndex = 216;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(17, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(286, 16);
            this.lblMessage.TabIndex = 214;
            this.lblMessage.Text = "Select parameters to generate the report.";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 22);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1268, 1);
            this.label45.TabIndex = 17;
            this.label45.Text = "label1";
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(4, 101);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(1268, 1);
            this.Label20.TabIndex = 16;
            this.Label20.Text = "label1";
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(4, 0);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(1268, 1);
            this.Label17.TabIndex = 15;
            this.Label17.Text = "label1";
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(1272, 0);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(1, 102);
            this.Label16.TabIndex = 14;
            this.Label16.Text = "label4";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(3, 0);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(1, 102);
            this.Label14.TabIndex = 13;
            this.Label14.Text = "label4";
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.chkShowAllProviders);
            this.pnlProvider.Controls.Add(this.pnlMedicationDate);
            this.pnlProvider.Controls.Add(this.pnlDate);
            this.pnlProvider.Controls.Add(this.lblAgeTo);
            this.pnlProvider.Controls.Add(this.lblAgeFrom);
            this.pnlProvider.Controls.Add(this.cmbAgeTo);
            this.pnlProvider.Controls.Add(this.cmbAgeFrom);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.cmbAge);
            this.pnlProvider.Controls.Add(this.Label19);
            this.pnlProvider.Controls.Add(this.Label18);
            this.pnlProvider.Controls.Add(this.Label15);
            this.pnlProvider.Controls.Add(this.Lblage);
            this.pnlProvider.Controls.Add(this.Label5);
            this.pnlProvider.Controls.Add(this.Label13);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProvider.Location = new System.Drawing.Point(0, 54);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Padding = new System.Windows.Forms.Padding(3);
            this.pnlProvider.Size = new System.Drawing.Size(1276, 44);
            this.pnlProvider.TabIndex = 0;
            // 
            // chkShowAllProviders
            // 
            this.chkShowAllProviders.AutoSize = true;
            this.chkShowAllProviders.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowAllProviders.Location = new System.Drawing.Point(278, 13);
            this.chkShowAllProviders.Name = "chkShowAllProviders";
            this.chkShowAllProviders.Size = new System.Drawing.Size(124, 18);
            this.chkShowAllProviders.TabIndex = 17;
            this.chkShowAllProviders.Text = "Show all Providers";
            this.chkShowAllProviders.UseVisualStyleBackColor = true;
            this.chkShowAllProviders.Visible = false;
            this.chkShowAllProviders.CheckedChanged += new System.EventHandler(this.chkShowAllProviders_CheckedChanged);
            // 
            // pnlMedicationDate
            // 
            this.pnlMedicationDate.Controls.Add(this.label75);
            this.pnlMedicationDate.Controls.Add(this.dtMedicationEndDate);
            this.pnlMedicationDate.Controls.Add(this.label76);
            this.pnlMedicationDate.Controls.Add(this.dtMedicationStartDate);
            this.pnlMedicationDate.Controls.Add(this.label77);
            this.pnlMedicationDate.Location = new System.Drawing.Point(423, 7);
            this.pnlMedicationDate.Name = "pnlMedicationDate";
            this.pnlMedicationDate.Size = new System.Drawing.Size(410, 30);
            this.pnlMedicationDate.TabIndex = 5;
            this.pnlMedicationDate.Visible = false;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(2, 8);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(112, 14);
            this.label75.TabIndex = 5;
            this.label75.Text = "Prescription Date : ";
            // 
            // dtMedicationEndDate
            // 
            this.dtMedicationEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtMedicationEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtMedicationEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtMedicationEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtMedicationEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtMedicationEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtMedicationEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtMedicationEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMedicationEndDate.Location = new System.Drawing.Point(298, 4);
            this.dtMedicationEndDate.Name = "dtMedicationEndDate";
            this.dtMedicationEndDate.Size = new System.Drawing.Size(105, 22);
            this.dtMedicationEndDate.TabIndex = 1;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(110, 8);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(34, 14);
            this.label76.TabIndex = 0;
            this.label76.Text = "Start";
            // 
            // dtMedicationStartDate
            // 
            this.dtMedicationStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtMedicationStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtMedicationStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtMedicationStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtMedicationStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtMedicationStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtMedicationStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtMedicationStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMedicationStartDate.Location = new System.Drawing.Point(147, 4);
            this.dtMedicationStartDate.Name = "dtMedicationStartDate";
            this.dtMedicationStartDate.Size = new System.Drawing.Size(117, 22);
            this.dtMedicationStartDate.TabIndex = 0;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(268, 8);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(28, 14);
            this.label77.TabIndex = 4;
            this.label77.Text = "End";
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.lblDate);
            this.pnlDate.Controls.Add(this.dtpicTo);
            this.pnlDate.Controls.Add(this.lblFrom);
            this.pnlDate.Controls.Add(this.dtpicFrom);
            this.pnlDate.Controls.Add(this.lblTo);
            this.pnlDate.Location = new System.Drawing.Point(420, 7);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(395, 30);
            this.pnlDate.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(13, 8);
            this.lblDate.MaximumSize = new System.Drawing.Size(81, 14);
            this.lblDate.MinimumSize = new System.Drawing.Size(81, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(81, 14);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Visit Date :";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpicTo
            // 
            this.dtpicTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpicTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpicTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpicTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpicTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpicTo.CustomFormat = "MM/dd/yyyy";
            this.dtpicTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpicTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpicTo.Location = new System.Drawing.Point(285, 4);
            this.dtpicTo.Name = "dtpicTo";
            this.dtpicTo.Size = new System.Drawing.Size(105, 22);
            this.dtpicTo.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(96, 8);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(34, 14);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From";
            // 
            // dtpicFrom
            // 
            this.dtpicFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpicFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpicFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpicFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpicFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpicFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpicFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpicFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpicFrom.Location = new System.Drawing.Point(132, 4);
            this.dtpicFrom.Name = "dtpicFrom";
            this.dtpicFrom.ShowCheckBox = true;
            this.dtpicFrom.Size = new System.Drawing.Size(117, 22);
            this.dtpicFrom.TabIndex = 0;
            this.dtpicFrom.ValueChanged += new System.EventHandler(this.dtpicFrom_ValueChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(260, 8);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 14);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // lblAgeTo
            // 
            this.lblAgeTo.AutoSize = true;
            this.lblAgeTo.BackColor = System.Drawing.Color.Transparent;
            this.lblAgeTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeTo.Location = new System.Drawing.Point(1118, 14);
            this.lblAgeTo.Name = "lblAgeTo";
            this.lblAgeTo.Size = new System.Drawing.Size(26, 14);
            this.lblAgeTo.TabIndex = 10;
            this.lblAgeTo.Text = "To ";
            // 
            // lblAgeFrom
            // 
            this.lblAgeFrom.AutoSize = true;
            this.lblAgeFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblAgeFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgeFrom.Location = new System.Drawing.Point(1017, 14);
            this.lblAgeFrom.Name = "lblAgeFrom";
            this.lblAgeFrom.Size = new System.Drawing.Size(34, 14);
            this.lblAgeFrom.TabIndex = 9;
            this.lblAgeFrom.Text = "From";
            this.lblAgeFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAgeTo
            // 
            this.cmbAgeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgeTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgeTo.FormattingEnabled = true;
            this.cmbAgeTo.Location = new System.Drawing.Point(1145, 10);
            this.cmbAgeTo.Name = "cmbAgeTo";
            this.cmbAgeTo.Size = new System.Drawing.Size(63, 22);
            this.cmbAgeTo.TabIndex = 5;
            // 
            // cmbAgeFrom
            // 
            this.cmbAgeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgeFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgeFrom.FormattingEnabled = true;
            this.cmbAgeFrom.Location = new System.Drawing.Point(1054, 10);
            this.cmbAgeFrom.Name = "cmbAgeFrom";
            this.cmbAgeFrom.Size = new System.Drawing.Size(63, 22);
            this.cmbAgeFrom.TabIndex = 4;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(86, 11);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(183, 22);
            this.cmbProvider.TabIndex = 0;
            // 
            // cmbAge
            // 
            this.cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAge.FormattingEnabled = true;
            this.cmbAge.Location = new System.Drawing.Point(910, 10);
            this.cmbAge.Name = "cmbAge";
            this.cmbAge.Size = new System.Drawing.Size(105, 22);
            this.cmbAge.TabIndex = 3;
            this.cmbAge.SelectedIndexChanged += new System.EventHandler(this.cmbAge_SelectedIndexChanged);
            this.cmbAge.TextChanged += new System.EventHandler(this.cmbAge_TextChanged);
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.Location = new System.Drawing.Point(4, 40);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(1268, 1);
            this.Label19.TabIndex = 16;
            this.Label19.Text = "label1";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(4, 3);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(1268, 1);
            this.Label18.TabIndex = 15;
            this.Label18.Text = "label1";
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(1272, 3);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(1, 38);
            this.Label15.TabIndex = 14;
            this.Label15.Text = "label4";
            // 
            // Lblage
            // 
            this.Lblage.AutoSize = true;
            this.Lblage.BackColor = System.Drawing.Color.Transparent;
            this.Lblage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lblage.Location = new System.Drawing.Point(872, 14);
            this.Lblage.Name = "Lblage";
            this.Lblage.Size = new System.Drawing.Size(37, 14);
            this.Lblage.TabIndex = 0;
            this.Lblage.Text = "Age :";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(25, 15);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 14);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Provider :";
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(3, 3);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(1, 38);
            this.Label13.TabIndex = 13;
            this.Label13.Text = "label4";
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Transparent;
            this.Panel3.Controls.Add(this.chkExcControlledSubstance);
            this.Panel3.Controls.Add(this.pnlDemo);
            this.Panel3.Controls.Add(this.chkOnlyDrug);
            this.Panel3.Controls.Add(this.pnlCheckBoxes);
            this.Panel3.Controls.Add(this.label1);
            this.Panel3.Controls.Add(this.label2);
            this.Panel3.Controls.Add(this.label3);
            this.Panel3.Controls.Add(this.label4);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Location = new System.Drawing.Point(0, 724);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Panel3.Size = new System.Drawing.Size(1276, 67);
            this.Panel3.TabIndex = 7;
            // 
            // chkExcControlledSubstance
            // 
            this.chkExcControlledSubstance.AutoSize = true;
            this.chkExcControlledSubstance.Checked = true;
            this.chkExcControlledSubstance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcControlledSubstance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExcControlledSubstance.Location = new System.Drawing.Point(316, 8);
            this.chkExcControlledSubstance.Name = "chkExcControlledSubstance";
            this.chkExcControlledSubstance.Size = new System.Drawing.Size(193, 18);
            this.chkExcControlledSubstance.TabIndex = 2;
            this.chkExcControlledSubstance.Text = "Exclude Controlled Substances";
            this.chkExcControlledSubstance.UseVisualStyleBackColor = true;
            this.chkExcControlledSubstance.Visible = false;
            // 
            // pnlDemo
            // 
            this.pnlDemo.Controls.Add(this.chkDOB);
            this.pnlDemo.Controls.Add(this.chkEthnicity);
            this.pnlDemo.Controls.Add(this.chkRace);
            this.pnlDemo.Controls.Add(this.chkGender);
            this.pnlDemo.Controls.Add(this.chkInsurance);
            this.pnlDemo.Controls.Add(this.chkLanguage);
            this.pnlDemo.Controls.Add(this.chkAll);
            this.pnlDemo.Controls.Add(this.lblDemoElement);
            this.pnlDemo.Location = new System.Drawing.Point(6, 36);
            this.pnlDemo.Name = "pnlDemo";
            this.pnlDemo.Size = new System.Drawing.Size(1052, 25);
            this.pnlDemo.TabIndex = 222;
            // 
            // chkDOB
            // 
            this.chkDOB.AutoSize = true;
            this.chkDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDOB.Location = new System.Drawing.Point(637, 3);
            this.chkDOB.Name = "chkDOB";
            this.chkDOB.Size = new System.Drawing.Size(50, 18);
            this.chkDOB.TabIndex = 6;
            this.chkDOB.Text = "DOB";
            this.chkDOB.UseVisualStyleBackColor = true;
            // 
            // chkEthnicity
            // 
            this.chkEthnicity.AutoSize = true;
            this.chkEthnicity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEthnicity.Location = new System.Drawing.Point(552, 3);
            this.chkEthnicity.Name = "chkEthnicity";
            this.chkEthnicity.Size = new System.Drawing.Size(73, 18);
            this.chkEthnicity.TabIndex = 5;
            this.chkEthnicity.Text = "Ethnicity";
            this.chkEthnicity.UseVisualStyleBackColor = true;
            // 
            // chkRace
            // 
            this.chkRace.AutoSize = true;
            this.chkRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRace.Location = new System.Drawing.Point(488, 3);
            this.chkRace.Name = "chkRace";
            this.chkRace.Size = new System.Drawing.Size(52, 18);
            this.chkRace.TabIndex = 4;
            this.chkRace.Text = "Race";
            this.chkRace.UseVisualStyleBackColor = true;
            // 
            // chkGender
            // 
            this.chkGender.AutoSize = true;
            this.chkGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGender.Location = new System.Drawing.Point(410, 3);
            this.chkGender.Name = "chkGender";
            this.chkGender.Size = new System.Drawing.Size(66, 18);
            this.chkGender.TabIndex = 3;
            this.chkGender.Text = "Gender";
            this.chkGender.UseVisualStyleBackColor = true;
            // 
            // chkInsurance
            // 
            this.chkInsurance.AutoSize = true;
            this.chkInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsurance.Location = new System.Drawing.Point(287, 3);
            this.chkInsurance.Name = "chkInsurance";
            this.chkInsurance.Size = new System.Drawing.Size(111, 18);
            this.chkInsurance.TabIndex = 2;
            this.chkInsurance.Text = "Insurance Type";
            this.chkInsurance.UseVisualStyleBackColor = true;
            // 
            // chkLanguage
            // 
            this.chkLanguage.AutoSize = true;
            this.chkLanguage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLanguage.Location = new System.Drawing.Point(196, 3);
            this.chkLanguage.Name = "chkLanguage";
            this.chkLanguage.Size = new System.Drawing.Size(79, 18);
            this.chkLanguage.TabIndex = 1;
            this.chkLanguage.Text = "Language";
            this.chkLanguage.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.Location = new System.Drawing.Point(144, 3);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(38, 18);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // lblDemoElement
            // 
            this.lblDemoElement.AutoSize = true;
            this.lblDemoElement.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoElement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoElement.Location = new System.Drawing.Point(4, 4);
            this.lblDemoElement.Name = "lblDemoElement";
            this.lblDemoElement.Size = new System.Drawing.Size(135, 14);
            this.lblDemoElement.TabIndex = 221;
            this.lblDemoElement.Text = "Demographic Element :";
            // 
            // chkOnlyDrug
            // 
            this.chkOnlyDrug.AutoSize = true;
            this.chkOnlyDrug.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOnlyDrug.Location = new System.Drawing.Point(173, 8);
            this.chkOnlyDrug.Name = "chkOnlyDrug";
            this.chkOnlyDrug.Size = new System.Drawing.Size(120, 18);
            this.chkOnlyDrug.TabIndex = 1;
            this.chkOnlyDrug.Text = "Only Drug Allergy";
            this.chkOnlyDrug.UseVisualStyleBackColor = true;
            // 
            // pnlCheckBoxes
            // 
            this.pnlCheckBoxes.Controls.Add(this.chkShowUsageDeatal);
            this.pnlCheckBoxes.Location = new System.Drawing.Point(10, 4);
            this.pnlCheckBoxes.Name = "pnlCheckBoxes";
            this.pnlCheckBoxes.Size = new System.Drawing.Size(149, 25);
            this.pnlCheckBoxes.TabIndex = 0;
            // 
            // chkShowUsageDeatal
            // 
            this.chkShowUsageDeatal.AutoSize = true;
            this.chkShowUsageDeatal.Checked = true;
            this.chkShowUsageDeatal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUsageDeatal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowUsageDeatal.Location = new System.Drawing.Point(4, 3);
            this.chkShowUsageDeatal.Name = "chkShowUsageDeatal";
            this.chkShowUsageDeatal.Size = new System.Drawing.Size(142, 18);
            this.chkShowUsageDeatal.TabIndex = 1;
            this.chkShowUsageDeatal.Text = "Include Usage Details";
            this.chkShowUsageDeatal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1268, 1);
            this.label1.TabIndex = 16;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1268, 1);
            this.label2.TabIndex = 15;
            this.label2.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1272, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 64);
            this.label3.TabIndex = 14;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 64);
            this.label4.TabIndex = 13;
            this.label4.Text = "label4";
            // 
            // pnlMed
            // 
            this.pnlMed.Controls.Add(this.rbtnAllMedications);
            this.pnlMed.Controls.Add(this.rbtnPresByClinic);
            this.pnlMed.Controls.Add(this.BtnClearAllDrg);
            this.pnlMed.Controls.Add(this.btnClearDrug);
            this.pnlMed.Controls.Add(this.btnBrowseDrug);
            this.pnlMed.Controls.Add(this.LstMedication);
            this.pnlMed.Controls.Add(this.lblMedication);
            this.pnlMed.Location = new System.Drawing.Point(15, 1);
            this.pnlMed.Name = "pnlMed";
            this.pnlMed.Size = new System.Drawing.Size(388, 97);
            this.pnlMed.TabIndex = 0;
            // 
            // rbtnAllMedications
            // 
            this.rbtnAllMedications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbtnAllMedications.AutoSize = true;
            this.rbtnAllMedications.Checked = true;
            this.rbtnAllMedications.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAllMedications.Location = new System.Drawing.Point(157, 78);
            this.rbtnAllMedications.Name = "rbtnAllMedications";
            this.rbtnAllMedications.Size = new System.Drawing.Size(40, 18);
            this.rbtnAllMedications.TabIndex = 4;
            this.rbtnAllMedications.TabStop = true;
            this.rbtnAllMedications.Text = "All";
            this.rbtnAllMedications.UseVisualStyleBackColor = true;
            this.rbtnAllMedications.Visible = false;
            // 
            // rbtnPresByClinic
            // 
            this.rbtnPresByClinic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbtnPresByClinic.AutoSize = true;
            this.rbtnPresByClinic.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPresByClinic.Location = new System.Drawing.Point(218, 78);
            this.rbtnPresByClinic.Name = "rbtnPresByClinic";
            this.rbtnPresByClinic.Size = new System.Drawing.Size(128, 18);
            this.rbtnPresByClinic.TabIndex = 5;
            this.rbtnPresByClinic.Text = "Prescribed by Clinic";
            this.rbtnPresByClinic.UseVisualStyleBackColor = true;
            this.rbtnPresByClinic.Visible = false;
            // 
            // BtnClearAllDrg
            // 
            this.BtnClearAllDrg.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllDrg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDrg.BackgroundImage")));
            this.BtnClearAllDrg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllDrg.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllDrg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllDrg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllDrg.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDrg.Image")));
            this.BtnClearAllDrg.Location = new System.Drawing.Point(358, 55);
            this.BtnClearAllDrg.Name = "BtnClearAllDrg";
            this.BtnClearAllDrg.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllDrg.TabIndex = 3;
            this.BtnClearAllDrg.UseVisualStyleBackColor = false;
            this.BtnClearAllDrg.Click += new System.EventHandler(this.BtnClearAllDrg_Click);
            // 
            // btnClearDrug
            // 
            this.btnClearDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDrug.BackgroundImage")));
            this.btnClearDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDrug.Image")));
            this.btnClearDrug.Location = new System.Drawing.Point(358, 29);
            this.btnClearDrug.Name = "btnClearDrug";
            this.btnClearDrug.Size = new System.Drawing.Size(22, 22);
            this.btnClearDrug.TabIndex = 2;
            this.btnClearDrug.UseVisualStyleBackColor = false;
            this.btnClearDrug.Click += new System.EventHandler(this.btnClearDrug_Click);
            // 
            // btnBrowseDrug
            // 
            this.btnBrowseDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDrug.BackgroundImage")));
            this.btnBrowseDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDrug.Image")));
            this.btnBrowseDrug.Location = new System.Drawing.Point(358, 3);
            this.btnBrowseDrug.Name = "btnBrowseDrug";
            this.btnBrowseDrug.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDrug.TabIndex = 0;
            this.btnBrowseDrug.UseVisualStyleBackColor = false;
            this.btnBrowseDrug.Click += new System.EventHandler(this.btnBrowseDrug_Click);
            // 
            // LstMedication
            // 
            this.LstMedication.FormattingEnabled = true;
            this.LstMedication.ItemHeight = 14;
            this.LstMedication.Location = new System.Drawing.Point(150, 2);
            this.LstMedication.Name = "LstMedication";
            this.LstMedication.Size = new System.Drawing.Size(204, 74);
            this.LstMedication.TabIndex = 1;
            // 
            // lblMedication
            // 
            this.lblMedication.AutoSize = true;
            this.lblMedication.BackColor = System.Drawing.Color.Transparent;
            this.lblMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedication.Location = new System.Drawing.Point(6, 3);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(138, 14);
            this.lblMedication.TabIndex = 213;
            this.lblMedication.Text = "Medication To Exclude :";
            // 
            // pnlDiag
            // 
            this.pnlDiag.Controls.Add(this.lblDiagnosis);
            this.pnlDiag.Controls.Add(this.btnClearAllDiag);
            this.pnlDiag.Controls.Add(this.btnClearDiag);
            this.pnlDiag.Controls.Add(this.btnBrowseDiag);
            this.pnlDiag.Controls.Add(this.LstDiagnosis);
            this.pnlDiag.Location = new System.Drawing.Point(420, 2);
            this.pnlDiag.Name = "pnlDiag";
            this.pnlDiag.Size = new System.Drawing.Size(326, 92);
            this.pnlDiag.TabIndex = 1;
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.BackColor = System.Drawing.Color.Transparent;
            this.lblDiagnosis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosis.Location = new System.Drawing.Point(8, 4);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(64, 14);
            this.lblDiagnosis.TabIndex = 213;
            this.lblDiagnosis.Text = "Diagnosis :";
            // 
            // btnClearAllDiag
            // 
            this.btnClearAllDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllDiag.BackgroundImage")));
            this.btnClearAllDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllDiag.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllDiag.Image")));
            this.btnClearAllDiag.Location = new System.Drawing.Point(286, 54);
            this.btnClearAllDiag.Name = "btnClearAllDiag";
            this.btnClearAllDiag.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllDiag.TabIndex = 3;
            this.btnClearAllDiag.UseVisualStyleBackColor = false;
            this.btnClearAllDiag.Click += new System.EventHandler(this.btnClearAllDiag_Click_1);
            // 
            // btnClearDiag
            // 
            this.btnClearDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiag.BackgroundImage")));
            this.btnClearDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiag.Image")));
            this.btnClearDiag.Location = new System.Drawing.Point(286, 28);
            this.btnClearDiag.Name = "btnClearDiag";
            this.btnClearDiag.Size = new System.Drawing.Size(22, 22);
            this.btnClearDiag.TabIndex = 2;
            this.btnClearDiag.UseVisualStyleBackColor = false;
            this.btnClearDiag.Click += new System.EventHandler(this.btnClearDiag_Click_1);
            // 
            // btnBrowseDiag
            // 
            this.btnBrowseDiag.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiag.BackgroundImage")));
            this.btnBrowseDiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiag.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiag.Image")));
            this.btnBrowseDiag.Location = new System.Drawing.Point(286, 2);
            this.btnBrowseDiag.Name = "btnBrowseDiag";
            this.btnBrowseDiag.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDiag.TabIndex = 0;
            this.btnBrowseDiag.UseVisualStyleBackColor = false;
            this.btnBrowseDiag.Click += new System.EventHandler(this.btnBrowseDiag_Click_1);
            // 
            // LstDiagnosis
            // 
            this.LstDiagnosis.FormattingEnabled = true;
            this.LstDiagnosis.ItemHeight = 14;
            this.LstDiagnosis.Location = new System.Drawing.Point(75, 2);
            this.LstDiagnosis.Name = "LstDiagnosis";
            this.LstDiagnosis.Size = new System.Drawing.Size(204, 74);
            this.LstDiagnosis.TabIndex = 1;
            // 
            // pnlDrugDiagnosis
            // 
            this.pnlDrugDiagnosis.Controls.Add(this.pnlDiag);
            this.pnlDrugDiagnosis.Controls.Add(this.label9);
            this.pnlDrugDiagnosis.Controls.Add(this.pnlMed);
            this.pnlDrugDiagnosis.Controls.Add(this.label11);
            this.pnlDrugDiagnosis.Controls.Add(this.label12);
            this.pnlDrugDiagnosis.Controls.Add(this.label24);
            this.pnlDrugDiagnosis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDrugDiagnosis.Location = new System.Drawing.Point(0, 161);
            this.pnlDrugDiagnosis.Name = "pnlDrugDiagnosis";
            this.pnlDrugDiagnosis.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDrugDiagnosis.Size = new System.Drawing.Size(1276, 105);
            this.pnlDrugDiagnosis.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1268, 1);
            this.label9.TabIndex = 16;
            this.label9.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1268, 1);
            this.label11.TabIndex = 15;
            this.label11.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1272, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 102);
            this.label12.TabIndex = 14;
            this.label12.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 102);
            this.label24.TabIndex = 13;
            this.label24.Text = "label4";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // pnlDmPatientList
            // 
            this.pnlDmPatientList.Controls.Add(this.label36);
            this.pnlDmPatientList.Controls.Add(this.label35);
            this.pnlDmPatientList.Controls.Add(this.label34);
            this.pnlDmPatientList.Controls.Add(this.label33);
            this.pnlDmPatientList.Controls.Add(this.panel10);
            this.pnlDmPatientList.Controls.Add(this.panel9);
            this.pnlDmPatientList.Controls.Add(this.panel8);
            this.pnlDmPatientList.Controls.Add(this.panel7);
            this.pnlDmPatientList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDmPatientList.Location = new System.Drawing.Point(0, 501);
            this.pnlDmPatientList.Name = "pnlDmPatientList";
            this.pnlDmPatientList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDmPatientList.Size = new System.Drawing.Size(1276, 119);
            this.pnlDmPatientList.TabIndex = 5;
            this.pnlDmPatientList.Visible = false;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(1272, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 114);
            this.label36.TabIndex = 221;
            this.label36.Text = "label4";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(3, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 114);
            this.label35.TabIndex = 220;
            this.label35.Text = "label4";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(3, 115);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1270, 1);
            this.label34.TabIndex = 219;
            this.label34.Text = "label1";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(3, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1270, 1);
            this.label33.TabIndex = 218;
            this.label33.Text = "label1";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.cmbapptst);
            this.panel10.Controls.Add(this.label44);
            this.panel10.Controls.Add(this.ChkAppt);
            this.panel10.Controls.Add(this.dtToAppt);
            this.panel10.Controls.Add(this.dtFromAppt);
            this.panel10.Controls.Add(this.label32);
            this.panel10.Controls.Add(this.label31);
            this.panel10.Location = new System.Drawing.Point(885, 6);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(342, 105);
            this.panel10.TabIndex = 3;
            // 
            // cmbapptst
            // 
            this.cmbapptst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbapptst.Enabled = false;
            this.cmbapptst.FormattingEnabled = true;
            this.cmbapptst.Location = new System.Drawing.Point(194, 34);
            this.cmbapptst.Name = "cmbapptst";
            this.cmbapptst.Size = new System.Drawing.Size(121, 22);
            this.cmbapptst.TabIndex = 2;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(194, 5);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(134, 14);
            this.label44.TabIndex = 217;
            this.label44.Text = "Appointment Status";
            // 
            // ChkAppt
            // 
            this.ChkAppt.AutoSize = true;
            this.ChkAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAppt.Location = new System.Drawing.Point(17, 5);
            this.ChkAppt.Name = "ChkAppt";
            this.ChkAppt.Size = new System.Drawing.Size(141, 18);
            this.ChkAppt.TabIndex = 0;
            this.ChkAppt.Text = "Appointment Date";
            this.ChkAppt.UseVisualStyleBackColor = true;
            this.ChkAppt.CheckedChanged += new System.EventHandler(this.ChkAppt_CheckedChanged);
            // 
            // dtToAppt
            // 
            this.dtToAppt.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtToAppt.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtToAppt.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtToAppt.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtToAppt.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtToAppt.CustomFormat = "MM/dd/yyyy";
            this.dtToAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToAppt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToAppt.Location = new System.Drawing.Point(88, 62);
            this.dtToAppt.Name = "dtToAppt";
            this.dtToAppt.Size = new System.Drawing.Size(95, 22);
            this.dtToAppt.TabIndex = 3;
            // 
            // dtFromAppt
            // 
            this.dtFromAppt.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtFromAppt.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtFromAppt.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtFromAppt.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtFromAppt.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtFromAppt.CustomFormat = "MM/dd/yyyy";
            this.dtFromAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromAppt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromAppt.Location = new System.Drawing.Point(88, 34);
            this.dtFromAppt.Name = "dtFromAppt";
            this.dtFromAppt.Size = new System.Drawing.Size(95, 22);
            this.dtFromAppt.TabIndex = 1;
            this.dtFromAppt.ValueChanged += new System.EventHandler(this.dtFromAppt_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(26, 66);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 14);
            this.label32.TabIndex = 213;
            this.label32.Text = "To Date :";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(14, 38);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 14);
            this.label31.TabIndex = 213;
            this.label31.Text = "From Date :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label29);
            this.panel9.Controls.Add(this.btnClearAllDMPatientprb);
            this.panel9.Controls.Add(this.btnClearDMPatientprb);
            this.panel9.Controls.Add(this.btnBrowseDMPatientPrb);
            this.panel9.Controls.Add(this.lstDMProblemList);
            this.panel9.Location = new System.Drawing.Point(649, 7);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(241, 105);
            this.panel9.TabIndex = 2;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(241, 20);
            this.label29.TabIndex = 213;
            this.label29.Text = "Problem List";
            this.label29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnClearAllDMPatientprb
            // 
            this.btnClearAllDMPatientprb.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllDMPatientprb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllDMPatientprb.BackgroundImage")));
            this.btnClearAllDMPatientprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllDMPatientprb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllDMPatientprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllDMPatientprb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllDMPatientprb.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllDMPatientprb.Image")));
            this.btnClearAllDMPatientprb.Location = new System.Drawing.Point(208, 78);
            this.btnClearAllDMPatientprb.Name = "btnClearAllDMPatientprb";
            this.btnClearAllDMPatientprb.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllDMPatientprb.TabIndex = 3;
            this.btnClearAllDMPatientprb.UseVisualStyleBackColor = false;
            this.btnClearAllDMPatientprb.Click += new System.EventHandler(this.btnClearAllDMPatientprb_Click);
            // 
            // btnClearDMPatientprb
            // 
            this.btnClearDMPatientprb.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDMPatientprb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDMPatientprb.BackgroundImage")));
            this.btnClearDMPatientprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDMPatientprb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDMPatientprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDMPatientprb.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDMPatientprb.Image")));
            this.btnClearDMPatientprb.Location = new System.Drawing.Point(208, 52);
            this.btnClearDMPatientprb.Name = "btnClearDMPatientprb";
            this.btnClearDMPatientprb.Size = new System.Drawing.Size(22, 22);
            this.btnClearDMPatientprb.TabIndex = 2;
            this.btnClearDMPatientprb.UseVisualStyleBackColor = false;
            this.btnClearDMPatientprb.Click += new System.EventHandler(this.btnClearDMPatientprb_Click);
            // 
            // btnBrowseDMPatientPrb
            // 
            this.btnBrowseDMPatientPrb.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDMPatientPrb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMPatientPrb.BackgroundImage")));
            this.btnBrowseDMPatientPrb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDMPatientPrb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDMPatientPrb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDMPatientPrb.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMPatientPrb.Image")));
            this.btnBrowseDMPatientPrb.Location = new System.Drawing.Point(208, 26);
            this.btnBrowseDMPatientPrb.Name = "btnBrowseDMPatientPrb";
            this.btnBrowseDMPatientPrb.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDMPatientPrb.TabIndex = 0;
            this.btnBrowseDMPatientPrb.UseVisualStyleBackColor = false;
            this.btnBrowseDMPatientPrb.Click += new System.EventHandler(this.btnBrowseDMPatientPrb_Click);
            // 
            // lstDMProblemList
            // 
            this.lstDMProblemList.FormattingEnabled = true;
            this.lstDMProblemList.ItemHeight = 14;
            this.lstDMProblemList.Location = new System.Drawing.Point(3, 26);
            this.lstDMProblemList.Name = "lstDMProblemList";
            this.lstDMProblemList.Size = new System.Drawing.Size(200, 74);
            this.lstDMProblemList.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.chkDueDate);
            this.panel8.Controls.Add(this.dtToDueDate);
            this.panel8.Controls.Add(this.dtFromDueDate);
            this.panel8.Controls.Add(this.label73);
            this.panel8.Controls.Add(this.label74);
            this.panel8.Controls.Add(this.label28);
            this.panel8.Controls.Add(this.BtnClearAllDMSetup);
            this.panel8.Controls.Add(this.btnClearDMSetup);
            this.panel8.Controls.Add(this.btnBrowseDMSetup);
            this.panel8.Controls.Add(this.lstdmsetup);
            this.panel8.Location = new System.Drawing.Point(8, 6);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(387, 105);
            this.panel8.TabIndex = 0;
            // 
            // chkDueDate
            // 
            this.chkDueDate.AutoSize = true;
            this.chkDueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDueDate.Location = new System.Drawing.Point(270, 5);
            this.chkDueDate.Name = "chkDueDate";
            this.chkDueDate.Size = new System.Drawing.Size(107, 18);
            this.chkDueDate.TabIndex = 214;
            this.chkDueDate.Text = "DM Due Date";
            this.chkDueDate.UseVisualStyleBackColor = true;
            this.chkDueDate.CheckedChanged += new System.EventHandler(this.chkDueDate_CheckedChanged);
            // 
            // dtToDueDate
            // 
            this.dtToDueDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtToDueDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtToDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtToDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtToDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtToDueDate.CustomFormat = "MM/dd/yyyy";
            this.dtToDueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDueDate.Location = new System.Drawing.Point(286, 63);
            this.dtToDueDate.Name = "dtToDueDate";
            this.dtToDueDate.Size = new System.Drawing.Size(95, 22);
            this.dtToDueDate.TabIndex = 216;
            // 
            // dtFromDueDate
            // 
            this.dtFromDueDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtFromDueDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtFromDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtFromDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtFromDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtFromDueDate.CustomFormat = "MM/dd/yyyy";
            this.dtFromDueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDueDate.Location = new System.Drawing.Point(286, 35);
            this.dtFromDueDate.Name = "dtFromDueDate";
            this.dtFromDueDate.Size = new System.Drawing.Size(95, 22);
            this.dtFromDueDate.TabIndex = 215;
            this.dtFromDueDate.ValueChanged += new System.EventHandler(this.dtFromDueDate_ValueChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(250, 67);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(30, 14);
            this.label73.TabIndex = 218;
            this.label73.Text = "To :";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(238, 39);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(42, 14);
            this.label74.TabIndex = 217;
            this.label74.Text = "From :";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(4, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(232, 20);
            this.label28.TabIndex = 0;
            this.label28.Text = "Disease Management";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BtnClearAllDMSetup
            // 
            this.BtnClearAllDMSetup.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllDMSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDMSetup.BackgroundImage")));
            this.BtnClearAllDMSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllDMSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllDMSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllDMSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllDMSetup.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllDMSetup.Image")));
            this.BtnClearAllDMSetup.Location = new System.Drawing.Point(208, 78);
            this.BtnClearAllDMSetup.Name = "BtnClearAllDMSetup";
            this.BtnClearAllDMSetup.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllDMSetup.TabIndex = 3;
            this.BtnClearAllDMSetup.UseVisualStyleBackColor = false;
            this.BtnClearAllDMSetup.Click += new System.EventHandler(this.BtnClearAllDMSetup_Click);
            // 
            // btnClearDMSetup
            // 
            this.btnClearDMSetup.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDMSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDMSetup.BackgroundImage")));
            this.btnClearDMSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDMSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDMSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDMSetup.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDMSetup.Image")));
            this.btnClearDMSetup.Location = new System.Drawing.Point(208, 52);
            this.btnClearDMSetup.Name = "btnClearDMSetup";
            this.btnClearDMSetup.Size = new System.Drawing.Size(22, 22);
            this.btnClearDMSetup.TabIndex = 2;
            this.btnClearDMSetup.UseVisualStyleBackColor = false;
            this.btnClearDMSetup.Click += new System.EventHandler(this.btnClearDMSetup_Click);
            // 
            // btnBrowseDMSetup
            // 
            this.btnBrowseDMSetup.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDMSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMSetup.BackgroundImage")));
            this.btnBrowseDMSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDMSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDMSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDMSetup.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMSetup.Image")));
            this.btnBrowseDMSetup.Location = new System.Drawing.Point(208, 26);
            this.btnBrowseDMSetup.Name = "btnBrowseDMSetup";
            this.btnBrowseDMSetup.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDMSetup.TabIndex = 0;
            this.btnBrowseDMSetup.UseVisualStyleBackColor = false;
            this.btnBrowseDMSetup.Click += new System.EventHandler(this.btnBrowseDMSetup_Click);
            // 
            // lstdmsetup
            // 
            this.lstdmsetup.FormattingEnabled = true;
            this.lstdmsetup.ItemHeight = 14;
            this.lstdmsetup.Location = new System.Drawing.Point(3, 26);
            this.lstdmsetup.Name = "lstdmsetup";
            this.lstdmsetup.Size = new System.Drawing.Size(200, 74);
            this.lstdmsetup.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.btnClearAllDMMedication);
            this.panel7.Controls.Add(this.btnClearDMMedication);
            this.panel7.Controls.Add(this.btnBrowseDMMedication);
            this.panel7.Controls.Add(this.lstmeddmsetup);
            this.panel7.Location = new System.Drawing.Point(404, 6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(237, 105);
            this.panel7.TabIndex = 1;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(237, 20);
            this.label27.TabIndex = 213;
            this.label27.Text = "Medication";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnClearAllDMMedication
            // 
            this.btnClearAllDMMedication.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllDMMedication.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllDMMedication.BackgroundImage")));
            this.btnClearAllDMMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllDMMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllDMMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllDMMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllDMMedication.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllDMMedication.Image")));
            this.btnClearAllDMMedication.Location = new System.Drawing.Point(207, 78);
            this.btnClearAllDMMedication.Name = "btnClearAllDMMedication";
            this.btnClearAllDMMedication.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllDMMedication.TabIndex = 3;
            this.btnClearAllDMMedication.UseVisualStyleBackColor = false;
            this.btnClearAllDMMedication.Click += new System.EventHandler(this.btnClearAllDMMedication_Click);
            // 
            // btnClearDMMedication
            // 
            this.btnClearDMMedication.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDMMedication.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDMMedication.BackgroundImage")));
            this.btnClearDMMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDMMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDMMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDMMedication.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDMMedication.Image")));
            this.btnClearDMMedication.Location = new System.Drawing.Point(207, 52);
            this.btnClearDMMedication.Name = "btnClearDMMedication";
            this.btnClearDMMedication.Size = new System.Drawing.Size(22, 22);
            this.btnClearDMMedication.TabIndex = 2;
            this.btnClearDMMedication.UseVisualStyleBackColor = false;
            this.btnClearDMMedication.Click += new System.EventHandler(this.btnClearDMMedication_Click);
            // 
            // btnBrowseDMMedication
            // 
            this.btnBrowseDMMedication.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDMMedication.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMMedication.BackgroundImage")));
            this.btnBrowseDMMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDMMedication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDMMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDMMedication.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDMMedication.Image")));
            this.btnBrowseDMMedication.Location = new System.Drawing.Point(207, 26);
            this.btnBrowseDMMedication.Name = "btnBrowseDMMedication";
            this.btnBrowseDMMedication.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDMMedication.TabIndex = 0;
            this.btnBrowseDMMedication.UseVisualStyleBackColor = false;
            this.btnBrowseDMMedication.Click += new System.EventHandler(this.btnBrowseDMMedication_Click);
            // 
            // lstmeddmsetup
            // 
            this.lstmeddmsetup.FormattingEnabled = true;
            this.lstmeddmsetup.ItemHeight = 14;
            this.lstmeddmsetup.Location = new System.Drawing.Point(3, 26);
            this.lstmeddmsetup.Name = "lstmeddmsetup";
            this.lstmeddmsetup.Size = new System.Drawing.Size(200, 74);
            this.lstmeddmsetup.TabIndex = 1;
            // 
            // pnlPatientList
            // 
            this.pnlPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientList.Controls.Add(this.btnBrowseSnomedCT);
            this.pnlPatientList.Controls.Add(this.cmbMediAll);
            this.pnlPatientList.Controls.Add(this.panel11);
            this.pnlPatientList.Controls.Add(this.groupBox1);
            this.pnlPatientList.Controls.Add(this.cmb3rdPat);
            this.pnlPatientList.Controls.Add(this.cmbFstPat);
            this.pnlPatientList.Controls.Add(this.label6);
            this.pnlPatientList.Controls.Add(this.label7);
            this.pnlPatientList.Controls.Add(this.label8);
            this.pnlPatientList.Controls.Add(this.label10);
            this.pnlPatientList.Controls.Add(this.panel4);
            this.pnlPatientList.Controls.Add(this.pnlTreat);
            this.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientList.Location = new System.Drawing.Point(0, 266);
            this.pnlPatientList.Name = "pnlPatientList";
            this.pnlPatientList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPatientList.Size = new System.Drawing.Size(1276, 129);
            this.pnlPatientList.TabIndex = 3;
            this.pnlPatientList.Visible = false;
            // 
            // btnBrowseSnomedCT
            // 
            this.btnBrowseSnomedCT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseSnomedCT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseSnomedCT.BackgroundImage")));
            this.btnBrowseSnomedCT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseSnomedCT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseSnomedCT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSnomedCT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseSnomedCT.Image")));
            this.btnBrowseSnomedCT.Location = new System.Drawing.Point(215, 38);
            this.btnBrowseSnomedCT.Name = "btnBrowseSnomedCT";
            this.btnBrowseSnomedCT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseSnomedCT.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.btnBrowseSnomedCT, "Select Snomed Code");
            this.btnBrowseSnomedCT.UseVisualStyleBackColor = false;
            this.btnBrowseSnomedCT.Click += new System.EventHandler(this.btnBrowseSnomedCT_Click);
            // 
            // cmbMediAll
            // 
            this.cmbMediAll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMediAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMediAll.FormattingEnabled = true;
            this.cmbMediAll.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbMediAll.Location = new System.Drawing.Point(988, 64);
            this.cmbMediAll.Name = "cmbMediAll";
            this.cmbMediAll.Size = new System.Drawing.Size(47, 22);
            this.cmbMediAll.TabIndex = 10;
            this.cmbMediAll.Visible = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.label49);
            this.panel11.Controls.Add(this.BtnClearAllAllergy);
            this.panel11.Controls.Add(this.BtnClearAllergy);
            this.panel11.Controls.Add(this.BtnBrowserAllergy);
            this.panel11.Controls.Add(this.lstAllergy);
            this.panel11.Location = new System.Drawing.Point(1037, 10);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(204, 107);
            this.panel11.TabIndex = 11;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(28, 7);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(121, 20);
            this.label49.TabIndex = 213;
            this.label49.Text = "Medication Allergy";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClearAllAllergy
            // 
            this.BtnClearAllAllergy.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllAllergy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllAllergy.BackgroundImage")));
            this.BtnClearAllAllergy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllAllergy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllAllergy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllAllergy.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllAllergy.Image")));
            this.BtnClearAllAllergy.Location = new System.Drawing.Point(176, 80);
            this.BtnClearAllAllergy.Name = "BtnClearAllAllergy";
            this.BtnClearAllAllergy.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllAllergy.TabIndex = 3;
            this.BtnClearAllAllergy.UseVisualStyleBackColor = false;
            this.BtnClearAllAllergy.Click += new System.EventHandler(this.BtnClearAllAllergy_Click);
            // 
            // BtnClearAllergy
            // 
            this.BtnClearAllergy.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllergy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllergy.BackgroundImage")));
            this.BtnClearAllergy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllergy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllergy.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllergy.Image")));
            this.BtnClearAllergy.Location = new System.Drawing.Point(176, 54);
            this.BtnClearAllergy.Name = "BtnClearAllergy";
            this.BtnClearAllergy.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllergy.TabIndex = 2;
            this.BtnClearAllergy.UseVisualStyleBackColor = false;
            this.BtnClearAllergy.Click += new System.EventHandler(this.BtnClearAllergy_Click);
            // 
            // BtnBrowserAllergy
            // 
            this.BtnBrowserAllergy.BackColor = System.Drawing.Color.Transparent;
            this.BtnBrowserAllergy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnBrowserAllergy.BackgroundImage")));
            this.BtnBrowserAllergy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnBrowserAllergy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnBrowserAllergy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowserAllergy.Image = ((System.Drawing.Image)(resources.GetObject("BtnBrowserAllergy.Image")));
            this.BtnBrowserAllergy.Location = new System.Drawing.Point(176, 28);
            this.BtnBrowserAllergy.Name = "BtnBrowserAllergy";
            this.BtnBrowserAllergy.Size = new System.Drawing.Size(22, 22);
            this.BtnBrowserAllergy.TabIndex = 0;
            this.BtnBrowserAllergy.UseVisualStyleBackColor = false;
            this.BtnBrowserAllergy.Click += new System.EventHandler(this.BtnBrowserAllergy_Click);
            // 
            // lstAllergy
            // 
            this.lstAllergy.FormattingEnabled = true;
            this.lstAllergy.ItemHeight = 14;
            this.lstAllergy.Location = new System.Drawing.Point(3, 27);
            this.lstAllergy.Name = "lstAllergy";
            this.lstAllergy.Size = new System.Drawing.Size(169, 74);
            this.lstAllergy.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbImGiven);
            this.groupBox1.Controls.Add(this.rbImNotGiven);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.cmbSndPat);
            this.groupBox1.Location = new System.Drawing.Point(261, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // rbImGiven
            // 
            this.rbImGiven.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbImGiven.AutoSize = true;
            this.rbImGiven.Checked = true;
            this.rbImGiven.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImGiven.Location = new System.Drawing.Point(160, 0);
            this.rbImGiven.Name = "rbImGiven";
            this.rbImGiven.Size = new System.Drawing.Size(58, 18);
            this.rbImGiven.TabIndex = 3;
            this.rbImGiven.TabStop = true;
            this.rbImGiven.Text = "Given";
            this.rbImGiven.UseVisualStyleBackColor = true;
            this.rbImGiven.CheckedChanged += new System.EventHandler(this.rbImGiven_CheckedChanged);
            // 
            // rbImNotGiven
            // 
            this.rbImNotGiven.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbImNotGiven.AutoSize = true;
            this.rbImNotGiven.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbImNotGiven.Location = new System.Drawing.Point(237, 0);
            this.rbImNotGiven.Name = "rbImNotGiven";
            this.rbImNotGiven.Size = new System.Drawing.Size(79, 18);
            this.rbImNotGiven.TabIndex = 4;
            this.rbImNotGiven.Text = "Not Given";
            this.rbImNotGiven.UseVisualStyleBackColor = true;
            this.rbImNotGiven.CheckedChanged += new System.EventHandler(this.rbImNotGiven_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.BtnClearAllPatientDrug);
            this.panel2.Controls.Add(this.btnClearPatientDrug);
            this.panel2.Controls.Add(this.btnBrowsePatientDrug);
            this.panel2.Controls.Add(this.lstPatMedication);
            this.panel2.Location = new System.Drawing.Point(3, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 100);
            this.panel2.TabIndex = 5;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(56, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 20);
            this.label21.TabIndex = 213;
            this.label21.Text = "Medication";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClearAllPatientDrug
            // 
            this.BtnClearAllPatientDrug.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllPatientDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllPatientDrug.BackgroundImage")));
            this.BtnClearAllPatientDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllPatientDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllPatientDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllPatientDrug.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllPatientDrug.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllPatientDrug.Image")));
            this.BtnClearAllPatientDrug.Location = new System.Drawing.Point(177, 74);
            this.BtnClearAllPatientDrug.Name = "BtnClearAllPatientDrug";
            this.BtnClearAllPatientDrug.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllPatientDrug.TabIndex = 3;
            this.BtnClearAllPatientDrug.UseVisualStyleBackColor = false;
            this.BtnClearAllPatientDrug.Click += new System.EventHandler(this.BtnClearAllPatientDrug_Click);
            // 
            // btnClearPatientDrug
            // 
            this.btnClearPatientDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatientDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatientDrug.BackgroundImage")));
            this.btnClearPatientDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatientDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatientDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatientDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatientDrug.Image")));
            this.btnClearPatientDrug.Location = new System.Drawing.Point(177, 48);
            this.btnClearPatientDrug.Name = "btnClearPatientDrug";
            this.btnClearPatientDrug.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatientDrug.TabIndex = 2;
            this.btnClearPatientDrug.UseVisualStyleBackColor = false;
            this.btnClearPatientDrug.Click += new System.EventHandler(this.btnClearPatientDrug_Click);
            // 
            // btnBrowsePatientDrug
            // 
            this.btnBrowsePatientDrug.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatientDrug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientDrug.BackgroundImage")));
            this.btnBrowsePatientDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatientDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatientDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatientDrug.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientDrug.Image")));
            this.btnBrowsePatientDrug.Location = new System.Drawing.Point(177, 22);
            this.btnBrowsePatientDrug.Name = "btnBrowsePatientDrug";
            this.btnBrowsePatientDrug.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatientDrug.TabIndex = 0;
            this.btnBrowsePatientDrug.UseVisualStyleBackColor = false;
            this.btnBrowsePatientDrug.Click += new System.EventHandler(this.btnBrowsePatientDrug_Click);
            // 
            // lstPatMedication
            // 
            this.lstPatMedication.FormattingEnabled = true;
            this.lstPatMedication.ItemHeight = 14;
            this.lstPatMedication.Location = new System.Drawing.Point(3, 21);
            this.lstPatMedication.Name = "lstPatMedication";
            this.lstPatMedication.Size = new System.Drawing.Size(169, 74);
            this.lstPatMedication.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label37);
            this.panel6.Controls.Add(this.BtnClearAllImmunization);
            this.panel6.Controls.Add(this.btnClearImmunization);
            this.panel6.Controls.Add(this.btnSearchImmunization);
            this.panel6.Controls.Add(this.lstImmunization);
            this.panel6.Location = new System.Drawing.Point(258, 13);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(204, 100);
            this.panel6.TabIndex = 7;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(41, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(102, 20);
            this.label37.TabIndex = 213;
            this.label37.Text = "Immunization";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnClearAllImmunization
            // 
            this.BtnClearAllImmunization.BackColor = System.Drawing.Color.Transparent;
            this.BtnClearAllImmunization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnClearAllImmunization.BackgroundImage")));
            this.BtnClearAllImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClearAllImmunization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClearAllImmunization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClearAllImmunization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClearAllImmunization.Image = ((System.Drawing.Image)(resources.GetObject("BtnClearAllImmunization.Image")));
            this.BtnClearAllImmunization.Location = new System.Drawing.Point(177, 74);
            this.BtnClearAllImmunization.Name = "BtnClearAllImmunization";
            this.BtnClearAllImmunization.Size = new System.Drawing.Size(22, 22);
            this.BtnClearAllImmunization.TabIndex = 3;
            this.BtnClearAllImmunization.UseVisualStyleBackColor = false;
            this.BtnClearAllImmunization.Click += new System.EventHandler(this.BtnClearAllImmunization_Click);
            // 
            // btnClearImmunization
            // 
            this.btnClearImmunization.BackColor = System.Drawing.Color.Transparent;
            this.btnClearImmunization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearImmunization.BackgroundImage")));
            this.btnClearImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearImmunization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearImmunization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearImmunization.Image = ((System.Drawing.Image)(resources.GetObject("btnClearImmunization.Image")));
            this.btnClearImmunization.Location = new System.Drawing.Point(177, 48);
            this.btnClearImmunization.Name = "btnClearImmunization";
            this.btnClearImmunization.Size = new System.Drawing.Size(22, 22);
            this.btnClearImmunization.TabIndex = 2;
            this.btnClearImmunization.UseVisualStyleBackColor = false;
            this.btnClearImmunization.Click += new System.EventHandler(this.btnClearImmunization_Click);
            // 
            // btnSearchImmunization
            // 
            this.btnSearchImmunization.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchImmunization.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchImmunization.BackgroundImage")));
            this.btnSearchImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchImmunization.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSearchImmunization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchImmunization.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchImmunization.Image")));
            this.btnSearchImmunization.Location = new System.Drawing.Point(177, 22);
            this.btnSearchImmunization.Name = "btnSearchImmunization";
            this.btnSearchImmunization.Size = new System.Drawing.Size(22, 22);
            this.btnSearchImmunization.TabIndex = 0;
            this.btnSearchImmunization.UseVisualStyleBackColor = false;
            this.btnSearchImmunization.Click += new System.EventHandler(this.btnSearchImmunization_Click);
            // 
            // lstImmunization
            // 
            this.lstImmunization.FormattingEnabled = true;
            this.lstImmunization.ItemHeight = 14;
            this.lstImmunization.Location = new System.Drawing.Point(3, 21);
            this.lstImmunization.Name = "lstImmunization";
            this.lstImmunization.Size = new System.Drawing.Size(169, 74);
            this.lstImmunization.TabIndex = 1;
            // 
            // cmbSndPat
            // 
            this.cmbSndPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSndPat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSndPat.FormattingEnabled = true;
            this.cmbSndPat.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbSndPat.Location = new System.Drawing.Point(209, 61);
            this.cmbSndPat.Name = "cmbSndPat";
            this.cmbSndPat.Size = new System.Drawing.Size(47, 22);
            this.cmbSndPat.TabIndex = 6;
            // 
            // cmb3rdPat
            // 
            this.cmb3rdPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb3rdPat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb3rdPat.FormattingEnabled = true;
            this.cmb3rdPat.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmb3rdPat.Location = new System.Drawing.Point(733, 64);
            this.cmb3rdPat.Name = "cmb3rdPat";
            this.cmb3rdPat.Size = new System.Drawing.Size(47, 22);
            this.cmb3rdPat.TabIndex = 8;
            this.cmb3rdPat.Visible = false;
            // 
            // cmbFstPat
            // 
            this.cmbFstPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFstPat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFstPat.FormattingEnabled = true;
            this.cmbFstPat.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbFstPat.Location = new System.Drawing.Point(210, 64);
            this.cmbFstPat.Name = "cmbFstPat";
            this.cmbFstPat.Size = new System.Drawing.Size(47, 22);
            this.cmbFstPat.TabIndex = 2;
            this.cmbFstPat.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(1272, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 124);
            this.label6.TabIndex = 11;
            this.label6.Text = "label3";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 124);
            this.label7.TabIndex = 12;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(3, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1270, 1);
            this.label8.TabIndex = 13;
            this.label8.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1270, 1);
            this.label10.TabIndex = 10;
            this.label10.Text = "label1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.btnClearAllPatientprb);
            this.panel4.Controls.Add(this.btnClearPatientprb);
            this.panel4.Controls.Add(this.btnBrowsePatientPrb);
            this.panel4.Controls.Add(this.lstProblemList);
            this.panel4.Location = new System.Drawing.Point(5, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 107);
            this.panel4.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(43, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(90, 20);
            this.label22.TabIndex = 213;
            this.label22.Text = "Problem List";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearAllPatientprb
            // 
            this.btnClearAllPatientprb.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllPatientprb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllPatientprb.BackgroundImage")));
            this.btnClearAllPatientprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllPatientprb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllPatientprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllPatientprb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllPatientprb.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllPatientprb.Image")));
            this.btnClearAllPatientprb.Location = new System.Drawing.Point(178, 82);
            this.btnClearAllPatientprb.Name = "btnClearAllPatientprb";
            this.btnClearAllPatientprb.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllPatientprb.TabIndex = 3;
            this.btnClearAllPatientprb.UseVisualStyleBackColor = false;
            this.btnClearAllPatientprb.Click += new System.EventHandler(this.btnClearAllPatientprb_Click);
            // 
            // btnClearPatientprb
            // 
            this.btnClearPatientprb.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatientprb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatientprb.BackgroundImage")));
            this.btnClearPatientprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatientprb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatientprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatientprb.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatientprb.Image")));
            this.btnClearPatientprb.Location = new System.Drawing.Point(178, 56);
            this.btnClearPatientprb.Name = "btnClearPatientprb";
            this.btnClearPatientprb.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatientprb.TabIndex = 2;
            this.btnClearPatientprb.UseVisualStyleBackColor = false;
            this.btnClearPatientprb.Click += new System.EventHandler(this.btnClearPatientprb_Click);
            // 
            // btnBrowsePatientPrb
            // 
            this.btnBrowsePatientPrb.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatientPrb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientPrb.BackgroundImage")));
            this.btnBrowsePatientPrb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatientPrb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatientPrb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatientPrb.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatientPrb.Image")));
            this.btnBrowsePatientPrb.Location = new System.Drawing.Point(178, 30);
            this.btnBrowsePatientPrb.Name = "btnBrowsePatientPrb";
            this.btnBrowsePatientPrb.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatientPrb.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.btnBrowsePatientPrb, "Select ICD9 Code");
            this.btnBrowsePatientPrb.UseVisualStyleBackColor = false;
            this.btnBrowsePatientPrb.Click += new System.EventHandler(this.btnBrowsePatientPrb_Click);
            // 
            // lstProblemList
            // 
            this.lstProblemList.FormattingEnabled = true;
            this.lstProblemList.ItemHeight = 14;
            this.lstProblemList.Location = new System.Drawing.Point(4, 29);
            this.lstProblemList.Name = "lstProblemList";
            this.lstProblemList.Size = new System.Drawing.Size(169, 74);
            this.lstProblemList.TabIndex = 1;
            // 
            // pnlTreat
            // 
            this.pnlTreat.Controls.Add(this.label30);
            this.pnlTreat.Controls.Add(this.btnBrowseLabTestResult);
            this.pnlTreat.Controls.Add(this.lstLabResult);
            this.pnlTreat.Controls.Add(this.btnClearAllPatientLab);
            this.pnlTreat.Controls.Add(this.btnClearPatientLab);
            this.pnlTreat.Location = new System.Drawing.Point(781, 8);
            this.pnlTreat.Name = "pnlTreat";
            this.pnlTreat.Size = new System.Drawing.Size(204, 107);
            this.pnlTreat.TabIndex = 9;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(35, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(113, 20);
            this.label30.TabIndex = 217;
            this.label30.Text = "Lab Test Results";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBrowseLabTestResult
            // 
            this.btnBrowseLabTestResult.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseLabTestResult.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseLabTestResult.BackgroundImage")));
            this.btnBrowseLabTestResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseLabTestResult.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseLabTestResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLabTestResult.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseLabTestResult.Image")));
            this.btnBrowseLabTestResult.Location = new System.Drawing.Point(177, 30);
            this.btnBrowseLabTestResult.Name = "btnBrowseLabTestResult";
            this.btnBrowseLabTestResult.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseLabTestResult.TabIndex = 0;
            this.btnBrowseLabTestResult.UseVisualStyleBackColor = false;
            this.btnBrowseLabTestResult.Click += new System.EventHandler(this.btnBrowseLabTestResult_Click);
            // 
            // lstLabResult
            // 
            this.lstLabResult.FormattingEnabled = true;
            this.lstLabResult.ItemHeight = 14;
            this.lstLabResult.Location = new System.Drawing.Point(4, 29);
            this.lstLabResult.Name = "lstLabResult";
            this.lstLabResult.Size = new System.Drawing.Size(169, 74);
            this.lstLabResult.TabIndex = 1;
            // 
            // btnClearAllPatientLab
            // 
            this.btnClearAllPatientLab.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllPatientLab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllPatientLab.BackgroundImage")));
            this.btnClearAllPatientLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllPatientLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllPatientLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllPatientLab.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllPatientLab.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllPatientLab.Image")));
            this.btnClearAllPatientLab.Location = new System.Drawing.Point(177, 82);
            this.btnClearAllPatientLab.Name = "btnClearAllPatientLab";
            this.btnClearAllPatientLab.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllPatientLab.TabIndex = 3;
            this.btnClearAllPatientLab.UseVisualStyleBackColor = false;
            this.btnClearAllPatientLab.Click += new System.EventHandler(this.btnClearAllPatientLab_Click);
            // 
            // btnClearPatientLab
            // 
            this.btnClearPatientLab.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatientLab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatientLab.BackgroundImage")));
            this.btnClearPatientLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatientLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatientLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatientLab.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatientLab.Image")));
            this.btnClearPatientLab.Location = new System.Drawing.Point(177, 56);
            this.btnClearPatientLab.Name = "btnClearPatientLab";
            this.btnClearPatientLab.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatientLab.TabIndex = 2;
            this.btnClearPatientLab.UseVisualStyleBackColor = false;
            this.btnClearPatientLab.Click += new System.EventHandler(this.btnClearPatientLab_Click);
            // 
            // pnlLabTestResult
            // 
            this.pnlLabTestResult.Controls.Add(this.label42);
            this.pnlLabTestResult.Controls.Add(this.label41);
            this.pnlLabTestResult.Controls.Add(this.label40);
            this.pnlLabTestResult.Controls.Add(this.label39);
            this.pnlLabTestResult.Controls.Add(this.panel1);
            this.pnlLabTestResult.Location = new System.Drawing.Point(364, 881);
            this.pnlLabTestResult.Name = "pnlLabTestResult";
            this.pnlLabTestResult.Size = new System.Drawing.Size(538, 75);
            this.pnlLabTestResult.TabIndex = 236;
            this.pnlLabTestResult.Visible = false;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(1, 74);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(536, 1);
            this.label42.TabIndex = 237;
            this.label42.Text = "label1";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(1, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(536, 1);
            this.label41.TabIndex = 234;
            this.label41.Text = "label1";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Right;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(537, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 75);
            this.label40.TabIndex = 236;
            this.label40.Text = "label4";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Left;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 75);
            this.label39.TabIndex = 235;
            this.label39.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.lblPatTo);
            this.panel1.Controls.Add(this.lblPatFrom);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(538, 75);
            this.panel1.TabIndex = 233;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(427, 10);
            this.label26.MaximumSize = new System.Drawing.Size(99, 14);
            this.label26.MinimumSize = new System.Drawing.Size(99, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(99, 14);
            this.label26.TabIndex = 229;
            this.label26.Text = "Next Condition";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatTo
            // 
            this.lblPatTo.AutoSize = true;
            this.lblPatTo.BackColor = System.Drawing.Color.Transparent;
            this.lblPatTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatTo.Location = new System.Drawing.Point(375, 10);
            this.lblPatTo.MaximumSize = new System.Drawing.Size(52, 14);
            this.lblPatTo.MinimumSize = new System.Drawing.Size(52, 14);
            this.lblPatTo.Name = "lblPatTo";
            this.lblPatTo.Size = new System.Drawing.Size(52, 14);
            this.lblPatTo.TabIndex = 229;
            this.lblPatTo.Text = "To         ";
            this.lblPatTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatFrom
            // 
            this.lblPatFrom.AutoSize = true;
            this.lblPatFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblPatFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatFrom.Location = new System.Drawing.Point(327, 10);
            this.lblPatFrom.MaximumSize = new System.Drawing.Size(48, 14);
            this.lblPatFrom.MinimumSize = new System.Drawing.Size(48, 14);
            this.lblPatFrom.Name = "lblPatFrom";
            this.lblPatFrom.Size = new System.Drawing.Size(48, 14);
            this.lblPatFrom.TabIndex = 230;
            this.lblPatFrom.Text = "From  ";
            this.lblPatFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(224, 10);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(103, 14);
            this.label25.TabIndex = 231;
            this.label25.Text = "Condition         ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pnlTO);
            this.panel5.Controls.Add(this.lblForm);
            this.panel5.Controls.Add(this.txtPatFrom);
            this.panel5.Controls.Add(this.lblCondition);
            this.panel5.Controls.Add(this.cmbPatCondition);
            this.panel5.Controls.Add(this.lblLab);
            this.panel5.Controls.Add(this.btnBrowseLabResults);
            this.panel5.Controls.Add(this.label38);
            this.panel5.Controls.Add(this.cmbLabResult);
            this.panel5.Location = new System.Drawing.Point(10, 36);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(528, 22);
            this.panel5.TabIndex = 233;
            // 
            // pnlTO
            // 
            this.pnlTO.Controls.Add(this.btlCloseLabResult);
            this.pnlTO.Controls.Add(this.label43);
            this.pnlTO.Controls.Add(this.btnAddResultCond);
            this.pnlTO.Controls.Add(this.lblNext);
            this.pnlTO.Controls.Add(this.cmbThiPat);
            this.pnlTO.Controls.Add(this.lblToBlank);
            this.pnlTO.Controls.Add(this.txtPatTo);
            this.pnlTO.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTO.Location = new System.Drawing.Point(356, 0);
            this.pnlTO.Name = "pnlTO";
            this.pnlTO.Size = new System.Drawing.Size(180, 22);
            this.pnlTO.TabIndex = 218;
            // 
            // btlCloseLabResult
            // 
            this.btlCloseLabResult.BackColor = System.Drawing.Color.Transparent;
            this.btlCloseLabResult.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btlCloseLabResult.BackgroundImage")));
            this.btlCloseLabResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btlCloseLabResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.btlCloseLabResult.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btlCloseLabResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btlCloseLabResult.Image = ((System.Drawing.Image)(resources.GetObject("btlCloseLabResult.Image")));
            this.btlCloseLabResult.Location = new System.Drawing.Point(141, 0);
            this.btlCloseLabResult.Name = "btlCloseLabResult";
            this.btlCloseLabResult.Size = new System.Drawing.Size(22, 22);
            this.btlCloseLabResult.TabIndex = 238;
            this.btlCloseLabResult.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btlCloseLabResult.UseVisualStyleBackColor = false;
            this.btlCloseLabResult.Click += new System.EventHandler(this.btlCloseLabResult_Click);
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(135, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(6, 22);
            this.label43.TabIndex = 237;
            // 
            // btnAddResultCond
            // 
            this.btnAddResultCond.BackColor = System.Drawing.Color.Transparent;
            this.btnAddResultCond.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddResultCond.BackgroundImage")));
            this.btnAddResultCond.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddResultCond.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddResultCond.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddResultCond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddResultCond.Image = ((System.Drawing.Image)(resources.GetObject("btnAddResultCond.Image")));
            this.btnAddResultCond.Location = new System.Drawing.Point(113, 0);
            this.btnAddResultCond.Name = "btnAddResultCond";
            this.btnAddResultCond.Size = new System.Drawing.Size(22, 22);
            this.btnAddResultCond.TabIndex = 223;
            this.btnAddResultCond.UseVisualStyleBackColor = false;
            this.btnAddResultCond.Click += new System.EventHandler(this.btnAddResultCond_Click);
            // 
            // lblNext
            // 
            this.lblNext.BackColor = System.Drawing.Color.Transparent;
            this.lblNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNext.Location = new System.Drawing.Point(107, 0);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(6, 22);
            this.lblNext.TabIndex = 236;
            // 
            // cmbThiPat
            // 
            this.cmbThiPat.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbThiPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThiPat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbThiPat.FormattingEnabled = true;
            this.cmbThiPat.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbThiPat.Location = new System.Drawing.Point(51, 0);
            this.cmbThiPat.Name = "cmbThiPat";
            this.cmbThiPat.Size = new System.Drawing.Size(56, 22);
            this.cmbThiPat.TabIndex = 224;
            // 
            // lblToBlank
            // 
            this.lblToBlank.BackColor = System.Drawing.Color.Transparent;
            this.lblToBlank.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblToBlank.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToBlank.Location = new System.Drawing.Point(45, 0);
            this.lblToBlank.Name = "lblToBlank";
            this.lblToBlank.Size = new System.Drawing.Size(6, 22);
            this.lblToBlank.TabIndex = 235;
            // 
            // txtPatTo
            // 
            this.txtPatTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPatTo.Location = new System.Drawing.Point(0, 0);
            this.txtPatTo.Name = "txtPatTo";
            this.txtPatTo.Size = new System.Drawing.Size(45, 22);
            this.txtPatTo.TabIndex = 228;
            this.txtPatTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatTo_KeyPress_1);
            // 
            // lblForm
            // 
            this.lblForm.BackColor = System.Drawing.Color.Transparent;
            this.lblForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm.Location = new System.Drawing.Point(350, 0);
            this.lblForm.Name = "lblForm";
            this.lblForm.Size = new System.Drawing.Size(6, 22);
            this.lblForm.TabIndex = 234;
            // 
            // txtPatFrom
            // 
            this.txtPatFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPatFrom.Location = new System.Drawing.Point(305, 0);
            this.txtPatFrom.Name = "txtPatFrom";
            this.txtPatFrom.Size = new System.Drawing.Size(45, 22);
            this.txtPatFrom.TabIndex = 227;
            this.txtPatFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatFrom_KeyPress_1);
            // 
            // lblCondition
            // 
            this.lblCondition.BackColor = System.Drawing.Color.Transparent;
            this.lblCondition.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCondition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCondition.Location = new System.Drawing.Point(299, 0);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(6, 22);
            this.lblCondition.TabIndex = 233;
            // 
            // cmbPatCondition
            // 
            this.cmbPatCondition.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbPatCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatCondition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatCondition.FormattingEnabled = true;
            this.cmbPatCondition.Items.AddRange(new object[] {
            "Less Than",
            "Greater Than",
            "Between",
            "Equal To"});
            this.cmbPatCondition.Location = new System.Drawing.Point(203, 0);
            this.cmbPatCondition.Name = "cmbPatCondition";
            this.cmbPatCondition.Size = new System.Drawing.Size(96, 22);
            this.cmbPatCondition.TabIndex = 226;
            this.cmbPatCondition.SelectedIndexChanged += new System.EventHandler(this.cmbPatCondition_SelectedIndexChanged);
            // 
            // lblLab
            // 
            this.lblLab.BackColor = System.Drawing.Color.Transparent;
            this.lblLab.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLab.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLab.Location = new System.Drawing.Point(197, 0);
            this.lblLab.Name = "lblLab";
            this.lblLab.Size = new System.Drawing.Size(6, 22);
            this.lblLab.TabIndex = 232;
            // 
            // btnBrowseLabResults
            // 
            this.btnBrowseLabResults.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseLabResults.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseLabResults.BackgroundImage")));
            this.btnBrowseLabResults.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseLabResults.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBrowseLabResults.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseLabResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLabResults.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseLabResults.Image")));
            this.btnBrowseLabResults.Location = new System.Drawing.Point(175, 0);
            this.btnBrowseLabResults.Name = "btnBrowseLabResults";
            this.btnBrowseLabResults.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseLabResults.TabIndex = 234;
            this.btnBrowseLabResults.UseVisualStyleBackColor = false;
            this.btnBrowseLabResults.Click += new System.EventHandler(this.btnBrowseLabResults_Click);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(169, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(6, 22);
            this.label38.TabIndex = 234;
            // 
            // cmbLabResult
            // 
            this.cmbLabResult.BackColor = System.Drawing.Color.White;
            this.cmbLabResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbLabResult.Location = new System.Drawing.Point(0, 0);
            this.cmbLabResult.Name = "cmbLabResult";
            this.cmbLabResult.ReadOnly = true;
            this.cmbLabResult.Size = new System.Drawing.Size(169, 22);
            this.cmbLabResult.TabIndex = 235;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(10, 10);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(214, 14);
            this.label23.TabIndex = 232;
            this.label23.Text = "Lab Test Result                            ";
            // 
            // pnlDemoFilter
            // 
            this.pnlDemoFilter.Controls.Add(this.btnClearInsPlan);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseInsPlan);
            this.pnlDemoFilter.Controls.Add(this.label72);
            this.pnlDemoFilter.Controls.Add(this.cmbPatInsPlan);
            this.pnlDemoFilter.Controls.Add(this.btnClearMedCategory);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseMedCategory);
            this.pnlDemoFilter.Controls.Add(this.label64);
            this.pnlDemoFilter.Controls.Add(this.cmbMedicalCategory);
            this.pnlDemoFilter.Controls.Add(this.btnClearCommPref);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseCommPref);
            this.pnlDemoFilter.Controls.Add(this.btnClearLanguage);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseLanguage);
            this.pnlDemoFilter.Controls.Add(this.btnclearethnicity);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseEthnicity);
            this.pnlDemoFilter.Controls.Add(this.btnCleaseRace);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseRace);
            this.pnlDemoFilter.Controls.Add(this.btncleargender);
            this.pnlDemoFilter.Controls.Add(this.btnBrowseGender);
            this.pnlDemoFilter.Controls.Add(this.label51);
            this.pnlDemoFilter.Controls.Add(this.cmbComPre);
            this.pnlDemoFilter.Controls.Add(this.label50);
            this.pnlDemoFilter.Controls.Add(this.cmblanguage);
            this.pnlDemoFilter.Controls.Add(this.label54);
            this.pnlDemoFilter.Controls.Add(this.cmbethnicity);
            this.pnlDemoFilter.Controls.Add(this.cmbGender);
            this.pnlDemoFilter.Controls.Add(this.cmbRace);
            this.pnlDemoFilter.Controls.Add(this.label55);
            this.pnlDemoFilter.Controls.Add(this.label56);
            this.pnlDemoFilter.Controls.Add(this.label57);
            this.pnlDemoFilter.Controls.Add(this.label58);
            this.pnlDemoFilter.Controls.Add(this.label59);
            this.pnlDemoFilter.Controls.Add(this.label60);
            this.pnlDemoFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDemoFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDemoFilter.Location = new System.Drawing.Point(0, 98);
            this.pnlDemoFilter.Name = "pnlDemoFilter";
            this.pnlDemoFilter.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDemoFilter.Size = new System.Drawing.Size(1276, 63);
            this.pnlDemoFilter.TabIndex = 1;
            this.pnlDemoFilter.Visible = false;
            // 
            // btnClearInsPlan
            // 
            this.btnClearInsPlan.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsPlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsPlan.BackgroundImage")));
            this.btnClearInsPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsPlan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsPlan.Image")));
            this.btnClearInsPlan.Location = new System.Drawing.Point(307, 6);
            this.btnClearInsPlan.Name = "btnClearInsPlan";
            this.btnClearInsPlan.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsPlan.TabIndex = 2;
            this.btnClearInsPlan.UseVisualStyleBackColor = false;
            this.btnClearInsPlan.Click += new System.EventHandler(this.btnClearInsPlan_Click);
            // 
            // btnBrowseInsPlan
            // 
            this.btnBrowseInsPlan.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsPlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsPlan.BackgroundImage")));
            this.btnBrowseInsPlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsPlan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsPlan.Image")));
            this.btnBrowseInsPlan.Location = new System.Drawing.Point(281, 6);
            this.btnBrowseInsPlan.Name = "btnBrowseInsPlan";
            this.btnBrowseInsPlan.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsPlan.TabIndex = 1;
            this.btnBrowseInsPlan.UseVisualStyleBackColor = false;
            this.btnBrowseInsPlan.Click += new System.EventHandler(this.btnBrowseInsPlan_Click);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(5, 10);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(94, 14);
            this.label72.TabIndex = 222;
            this.label72.Text = "Insurance Plan :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPatInsPlan
            // 
            this.cmbPatInsPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatInsPlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatInsPlan.FormattingEnabled = true;
            this.cmbPatInsPlan.Location = new System.Drawing.Point(102, 6);
            this.cmbPatInsPlan.Name = "cmbPatInsPlan";
            this.cmbPatInsPlan.Size = new System.Drawing.Size(174, 22);
            this.cmbPatInsPlan.TabIndex = 0;
            // 
            // btnClearMedCategory
            // 
            this.btnClearMedCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnClearMedCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearMedCategory.BackgroundImage")));
            this.btnClearMedCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearMedCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearMedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearMedCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnClearMedCategory.Image")));
            this.btnClearMedCategory.Location = new System.Drawing.Point(999, 33);
            this.btnClearMedCategory.Name = "btnClearMedCategory";
            this.btnClearMedCategory.Size = new System.Drawing.Size(22, 22);
            this.btnClearMedCategory.TabIndex = 20;
            this.btnClearMedCategory.UseVisualStyleBackColor = false;
            this.btnClearMedCategory.Click += new System.EventHandler(this.btnClearMedCategory_Click);
            // 
            // btnBrowseMedCategory
            // 
            this.btnBrowseMedCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMedCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMedCategory.BackgroundImage")));
            this.btnBrowseMedCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMedCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMedCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMedCategory.Image")));
            this.btnBrowseMedCategory.Location = new System.Drawing.Point(972, 33);
            this.btnBrowseMedCategory.Name = "btnBrowseMedCategory";
            this.btnBrowseMedCategory.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMedCategory.TabIndex = 19;
            this.btnBrowseMedCategory.UseVisualStyleBackColor = false;
            this.btnBrowseMedCategory.Click += new System.EventHandler(this.btnBrowseMedCategory_Click_1);
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(684, 37);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(107, 14);
            this.label64.TabIndex = 218;
            this.label64.Text = "Medical Category :";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMedicalCategory
            // 
            this.cmbMedicalCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicalCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMedicalCategory.FormattingEnabled = true;
            this.cmbMedicalCategory.Location = new System.Drawing.Point(793, 33);
            this.cmbMedicalCategory.Name = "cmbMedicalCategory";
            this.cmbMedicalCategory.Size = new System.Drawing.Size(175, 22);
            this.cmbMedicalCategory.TabIndex = 18;
            // 
            // btnClearCommPref
            // 
            this.btnClearCommPref.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCommPref.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCommPref.BackgroundImage")));
            this.btnClearCommPref.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCommPref.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCommPref.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCommPref.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCommPref.Image")));
            this.btnClearCommPref.Location = new System.Drawing.Point(647, 33);
            this.btnClearCommPref.Name = "btnClearCommPref";
            this.btnClearCommPref.Size = new System.Drawing.Size(22, 22);
            this.btnClearCommPref.TabIndex = 17;
            this.btnClearCommPref.UseVisualStyleBackColor = false;
            this.btnClearCommPref.Click += new System.EventHandler(this.btnClearCommPref_Click);
            // 
            // btnBrowseCommPref
            // 
            this.btnBrowseCommPref.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCommPref.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCommPref.BackgroundImage")));
            this.btnBrowseCommPref.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCommPref.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCommPref.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCommPref.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCommPref.Image")));
            this.btnBrowseCommPref.Location = new System.Drawing.Point(621, 33);
            this.btnBrowseCommPref.Name = "btnBrowseCommPref";
            this.btnBrowseCommPref.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCommPref.TabIndex = 16;
            this.btnBrowseCommPref.UseVisualStyleBackColor = false;
            this.btnBrowseCommPref.Click += new System.EventHandler(this.btnBrowseCommPref_Click);
            // 
            // btnClearLanguage
            // 
            this.btnClearLanguage.BackColor = System.Drawing.Color.Transparent;
            this.btnClearLanguage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearLanguage.BackgroundImage")));
            this.btnClearLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearLanguage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLanguage.Image = ((System.Drawing.Image)(resources.GetObject("btnClearLanguage.Image")));
            this.btnClearLanguage.Location = new System.Drawing.Point(307, 33);
            this.btnClearLanguage.Name = "btnClearLanguage";
            this.btnClearLanguage.Size = new System.Drawing.Size(22, 22);
            this.btnClearLanguage.TabIndex = 14;
            this.btnClearLanguage.UseVisualStyleBackColor = false;
            this.btnClearLanguage.Click += new System.EventHandler(this.btnClearLanguage_Click);
            // 
            // btnBrowseLanguage
            // 
            this.btnBrowseLanguage.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseLanguage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseLanguage.BackgroundImage")));
            this.btnBrowseLanguage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseLanguage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLanguage.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseLanguage.Image")));
            this.btnBrowseLanguage.Location = new System.Drawing.Point(281, 33);
            this.btnBrowseLanguage.Name = "btnBrowseLanguage";
            this.btnBrowseLanguage.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseLanguage.TabIndex = 13;
            this.btnBrowseLanguage.UseVisualStyleBackColor = false;
            this.btnBrowseLanguage.Click += new System.EventHandler(this.btnBrowseLanguage_Click);
            // 
            // btnclearethnicity
            // 
            this.btnclearethnicity.BackColor = System.Drawing.Color.Transparent;
            this.btnclearethnicity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclearethnicity.BackgroundImage")));
            this.btnclearethnicity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclearethnicity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnclearethnicity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearethnicity.Image = ((System.Drawing.Image)(resources.GetObject("btnclearethnicity.Image")));
            this.btnclearethnicity.Location = new System.Drawing.Point(999, 6);
            this.btnclearethnicity.Name = "btnclearethnicity";
            this.btnclearethnicity.Size = new System.Drawing.Size(22, 22);
            this.btnclearethnicity.TabIndex = 8;
            this.btnclearethnicity.UseVisualStyleBackColor = false;
            this.btnclearethnicity.Click += new System.EventHandler(this.btnclearethnicity_Click);
            // 
            // btnBrowseEthnicity
            // 
            this.btnBrowseEthnicity.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseEthnicity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseEthnicity.BackgroundImage")));
            this.btnBrowseEthnicity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseEthnicity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseEthnicity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseEthnicity.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseEthnicity.Image")));
            this.btnBrowseEthnicity.Location = new System.Drawing.Point(972, 6);
            this.btnBrowseEthnicity.Name = "btnBrowseEthnicity";
            this.btnBrowseEthnicity.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseEthnicity.TabIndex = 7;
            this.btnBrowseEthnicity.UseVisualStyleBackColor = false;
            this.btnBrowseEthnicity.Click += new System.EventHandler(this.btnBrowseEthnicity_Click);
            // 
            // btnCleaseRace
            // 
            this.btnCleaseRace.BackColor = System.Drawing.Color.Transparent;
            this.btnCleaseRace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCleaseRace.BackgroundImage")));
            this.btnCleaseRace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCleaseRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCleaseRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleaseRace.Image = ((System.Drawing.Image)(resources.GetObject("btnCleaseRace.Image")));
            this.btnCleaseRace.Location = new System.Drawing.Point(647, 6);
            this.btnCleaseRace.Name = "btnCleaseRace";
            this.btnCleaseRace.Size = new System.Drawing.Size(22, 22);
            this.btnCleaseRace.TabIndex = 5;
            this.btnCleaseRace.UseVisualStyleBackColor = false;
            this.btnCleaseRace.Click += new System.EventHandler(this.btnCleaseRace_Click);
            // 
            // btnBrowseRace
            // 
            this.btnBrowseRace.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseRace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseRace.BackgroundImage")));
            this.btnBrowseRace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseRace.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseRace.Image")));
            this.btnBrowseRace.Location = new System.Drawing.Point(621, 6);
            this.btnBrowseRace.Name = "btnBrowseRace";
            this.btnBrowseRace.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseRace.TabIndex = 4;
            this.btnBrowseRace.UseVisualStyleBackColor = false;
            this.btnBrowseRace.Click += new System.EventHandler(this.btnBrowseRace_Click);
            // 
            // btncleargender
            // 
            this.btncleargender.BackColor = System.Drawing.Color.Transparent;
            this.btncleargender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncleargender.BackgroundImage")));
            this.btncleargender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncleargender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btncleargender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncleargender.Image = ((System.Drawing.Image)(resources.GetObject("btncleargender.Image")));
            this.btncleargender.Location = new System.Drawing.Point(1210, 6);
            this.btncleargender.Name = "btncleargender";
            this.btncleargender.Size = new System.Drawing.Size(22, 22);
            this.btncleargender.TabIndex = 11;
            this.btncleargender.UseVisualStyleBackColor = false;
            this.btncleargender.Click += new System.EventHandler(this.btncleargender_Click);
            // 
            // btnBrowseGender
            // 
            this.btnBrowseGender.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseGender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseGender.BackgroundImage")));
            this.btnBrowseGender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseGender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseGender.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseGender.Image")));
            this.btnBrowseGender.Location = new System.Drawing.Point(1184, 6);
            this.btnBrowseGender.Name = "btnBrowseGender";
            this.btnBrowseGender.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseGender.TabIndex = 10;
            this.btnBrowseGender.UseVisualStyleBackColor = false;
            this.btnBrowseGender.Click += new System.EventHandler(this.btnBrowseGender_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(337, 37);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(103, 14);
            this.label51.TabIndex = 20;
            this.label51.Text = "Com Preference :";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbComPre
            // 
            this.cmbComPre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPre.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbComPre.FormattingEnabled = true;
            this.cmbComPre.Location = new System.Drawing.Point(443, 33);
            this.cmbComPre.Name = "cmbComPre";
            this.cmbComPre.Size = new System.Drawing.Size(175, 22);
            this.cmbComPre.TabIndex = 15;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.BackColor = System.Drawing.Color.Transparent;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(31, 35);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(68, 14);
            this.label50.TabIndex = 18;
            this.label50.Text = "Language :";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmblanguage
            // 
            this.cmblanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmblanguage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmblanguage.FormattingEnabled = true;
            this.cmblanguage.Location = new System.Drawing.Point(102, 33);
            this.cmblanguage.Name = "cmblanguage";
            this.cmblanguage.Size = new System.Drawing.Size(174, 22);
            this.cmblanguage.TabIndex = 12;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(729, 10);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(62, 14);
            this.label54.TabIndex = 9;
            this.label54.Text = "Ethnicity :";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbethnicity
            // 
            this.cmbethnicity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbethnicity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbethnicity.FormattingEnabled = true;
            this.cmbethnicity.Location = new System.Drawing.Point(793, 6);
            this.cmbethnicity.Name = "cmbethnicity";
            this.cmbethnicity.Size = new System.Drawing.Size(175, 22);
            this.cmbethnicity.TabIndex = 6;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.ForeColor = System.Drawing.Color.Black;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(1098, 6);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(82, 22);
            this.cmbGender.TabIndex = 9;
            // 
            // cmbRace
            // 
            this.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRace.FormattingEnabled = true;
            this.cmbRace.Location = new System.Drawing.Point(443, 6);
            this.cmbRace.Name = "cmbRace";
            this.cmbRace.Size = new System.Drawing.Size(175, 22);
            this.cmbRace.TabIndex = 3;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(4, 59);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1268, 1);
            this.label55.TabIndex = 16;
            this.label55.Text = "label1";
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Top;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(4, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1268, 1);
            this.label56.TabIndex = 15;
            this.label56.Text = "label1";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Right;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(1272, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1, 60);
            this.label57.TabIndex = 14;
            this.label57.Text = "label4";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(399, 10);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(41, 14);
            this.label58.TabIndex = 0;
            this.label58.Text = "Race :";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(1040, 10);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(55, 14);
            this.label59.TabIndex = 0;
            this.label59.Text = "Gender :";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(3, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 60);
            this.label60.TabIndex = 13;
            this.label60.Text = "label4";
            // 
            // pnlIncludeDemographics
            // 
            this.pnlIncludeDemographics.BackColor = System.Drawing.Color.Transparent;
            this.pnlIncludeDemographics.Controls.Add(this.pnlPtntLstDemo);
            this.pnlIncludeDemographics.Controls.Add(this.pnlPstnOthr);
            this.pnlIncludeDemographics.Controls.Add(this.label47);
            this.pnlIncludeDemographics.Controls.Add(this.label48);
            this.pnlIncludeDemographics.Controls.Add(this.label53);
            this.pnlIncludeDemographics.Controls.Add(this.label52);
            this.pnlIncludeDemographics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIncludeDemographics.Location = new System.Drawing.Point(0, 620);
            this.pnlIncludeDemographics.Name = "pnlIncludeDemographics";
            this.pnlIncludeDemographics.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlIncludeDemographics.Size = new System.Drawing.Size(1276, 104);
            this.pnlIncludeDemographics.TabIndex = 6;
            this.pnlIncludeDemographics.Visible = false;
            // 
            // pnlPstnOthr
            // 
            this.pnlPstnOthr.BackColor = System.Drawing.Color.Transparent;
            this.pnlPstnOthr.Controls.Add(this.chkFacility);
            this.pnlPstnOthr.Controls.Add(this.chkClaimDX);
            this.pnlPstnOthr.Controls.Add(this.chkClaimCPT);
            this.pnlPstnOthr.Controls.Add(this.chkDateTime);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrAllergy);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrLabResult);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrImmunization);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrMedication);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrProblemList);
            this.pnlPstnOthr.Controls.Add(this.chkPrblmLstOthrElement);
            this.pnlPstnOthr.Controls.Add(this.label61);
            this.pnlPstnOthr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPstnOthr.Location = new System.Drawing.Point(4, 75);
            this.pnlPstnOthr.Name = "pnlPstnOthr";
            this.pnlPstnOthr.Size = new System.Drawing.Size(1268, 25);
            this.pnlPstnOthr.TabIndex = 223;
            // 
            // chkFacility
            // 
            this.chkFacility.AutoSize = true;
            this.chkFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFacility.Location = new System.Drawing.Point(1137, 4);
            this.chkFacility.Name = "chkFacility";
            this.chkFacility.Size = new System.Drawing.Size(61, 18);
            this.chkFacility.TabIndex = 9;
            this.chkFacility.Text = "Facility";
            this.chkFacility.UseVisualStyleBackColor = true;
            // 
            // chkClaimDX
            // 
            this.chkClaimDX.AutoSize = true;
            this.chkClaimDX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClaimDX.Location = new System.Drawing.Point(1050, 4);
            this.chkClaimDX.Name = "chkClaimDX";
            this.chkClaimDX.Size = new System.Drawing.Size(72, 18);
            this.chkClaimDX.TabIndex = 8;
            this.chkClaimDX.Text = "Claim DX";
            this.chkClaimDX.UseVisualStyleBackColor = true;
            // 
            // chkClaimCPT
            // 
            this.chkClaimCPT.AutoSize = true;
            this.chkClaimCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClaimCPT.Location = new System.Drawing.Point(956, 4);
            this.chkClaimCPT.Name = "chkClaimCPT";
            this.chkClaimCPT.Size = new System.Drawing.Size(79, 18);
            this.chkClaimCPT.TabIndex = 7;
            this.chkClaimCPT.Text = "Claim CPT";
            this.chkClaimCPT.UseVisualStyleBackColor = true;
            // 
            // chkDateTime
            // 
            this.chkDateTime.AutoSize = true;
            this.chkDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateTime.Location = new System.Drawing.Point(779, 4);
            this.chkDateTime.Name = "chkDateTime";
            this.chkDateTime.Size = new System.Drawing.Size(162, 18);
            this.chkDateTime.TabIndex = 6;
            this.chkDateTime.Text = "Include DateTime Details";
            this.chkDateTime.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrAllergy
            // 
            this.chkPrblmLstOthrAllergy.AutoSize = true;
            this.chkPrblmLstOthrAllergy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrAllergy.Location = new System.Drawing.Point(640, 4);
            this.chkPrblmLstOthrAllergy.Name = "chkPrblmLstOthrAllergy";
            this.chkPrblmLstOthrAllergy.Size = new System.Drawing.Size(124, 18);
            this.chkPrblmLstOthrAllergy.TabIndex = 5;
            this.chkPrblmLstOthrAllergy.Text = "Medication Allergy";
            this.chkPrblmLstOthrAllergy.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrLabResult
            // 
            this.chkPrblmLstOthrLabResult.AutoSize = true;
            this.chkPrblmLstOthrLabResult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrLabResult.Location = new System.Drawing.Point(514, 4);
            this.chkPrblmLstOthrLabResult.Name = "chkPrblmLstOthrLabResult";
            this.chkPrblmLstOthrLabResult.Size = new System.Drawing.Size(111, 18);
            this.chkPrblmLstOthrLabResult.TabIndex = 4;
            this.chkPrblmLstOthrLabResult.Text = "Lab Test Result";
            this.chkPrblmLstOthrLabResult.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrImmunization
            // 
            this.chkPrblmLstOthrImmunization.AutoSize = true;
            this.chkPrblmLstOthrImmunization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrImmunization.Location = new System.Drawing.Point(401, 4);
            this.chkPrblmLstOthrImmunization.Name = "chkPrblmLstOthrImmunization";
            this.chkPrblmLstOthrImmunization.Size = new System.Drawing.Size(98, 18);
            this.chkPrblmLstOthrImmunization.TabIndex = 3;
            this.chkPrblmLstOthrImmunization.Text = "Immunization";
            this.chkPrblmLstOthrImmunization.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrMedication
            // 
            this.chkPrblmLstOthrMedication.AutoSize = true;
            this.chkPrblmLstOthrMedication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrMedication.Location = new System.Drawing.Point(302, 4);
            this.chkPrblmLstOthrMedication.Name = "chkPrblmLstOthrMedication";
            this.chkPrblmLstOthrMedication.Size = new System.Drawing.Size(84, 18);
            this.chkPrblmLstOthrMedication.TabIndex = 2;
            this.chkPrblmLstOthrMedication.Text = "Medication";
            this.chkPrblmLstOthrMedication.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrProblemList
            // 
            this.chkPrblmLstOthrProblemList.AutoSize = true;
            this.chkPrblmLstOthrProblemList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrProblemList.Location = new System.Drawing.Point(195, 4);
            this.chkPrblmLstOthrProblemList.Name = "chkPrblmLstOthrProblemList";
            this.chkPrblmLstOthrProblemList.Size = new System.Drawing.Size(92, 18);
            this.chkPrblmLstOthrProblemList.TabIndex = 1;
            this.chkPrblmLstOthrProblemList.Text = "Problem List";
            this.chkPrblmLstOthrProblemList.UseVisualStyleBackColor = true;
            // 
            // chkPrblmLstOthrElement
            // 
            this.chkPrblmLstOthrElement.AutoSize = true;
            this.chkPrblmLstOthrElement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblmLstOthrElement.Location = new System.Drawing.Point(142, 4);
            this.chkPrblmLstOthrElement.Name = "chkPrblmLstOthrElement";
            this.chkPrblmLstOthrElement.Size = new System.Drawing.Size(38, 18);
            this.chkPrblmLstOthrElement.TabIndex = 0;
            this.chkPrblmLstOthrElement.Text = "All";
            this.chkPrblmLstOthrElement.UseVisualStyleBackColor = true;
            this.chkPrblmLstOthrElement.CheckedChanged += new System.EventHandler(this.chkPrblmLstOthrElement_CheckedChanged);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(42, 5);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(96, 14);
            this.label61.TabIndex = 221;
            this.label61.Text = "Other Element :";
            // 
            // pnlPtntLstDemo
            // 
            this.pnlPtntLstDemo.BackColor = System.Drawing.Color.Transparent;
            this.pnlPtntLstDemo.Controls.Add(this.chksZip);
            this.pnlPtntLstDemo.Controls.Add(this.chksAddressLine1);
            this.pnlPtntLstDemo.Controls.Add(this.chksAddressLine2);
            this.pnlPtntLstDemo.Controls.Add(this.chksState);
            this.pnlPtntLstDemo.Controls.Add(this.chksCity);
            this.pnlPtntLstDemo.Controls.Add(this.chkPtLastName);
            this.pnlPtntLstDemo.Controls.Add(this.chkPCP);
            this.pnlPtntLstDemo.Controls.Add(this.chkPtMiddleName);
            this.pnlPtntLstDemo.Controls.Add(this.chkPatNotes);
            this.pnlPtntLstDemo.Controls.Add(this.chkPtFirstName);
            this.pnlPtntLstDemo.Controls.Add(this.chkPriInsPlan);
            this.pnlPtntLstDemo.Controls.Add(this.ChkSecInsPlan);
            this.pnlPtntLstDemo.Controls.Add(this.chkMobile);
            this.pnlPtntLstDemo.Controls.Add(this.chkEmail);
            this.pnlPtntLstDemo.Controls.Add(this.chkPhone);
            this.pnlPtntLstDemo.Controls.Add(this.chkAddress);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLMedicalCategory);
            this.pnlPtntLstDemo.Controls.Add(this.chksortlistwthnpatnt);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLAge);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLCommPref);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLLangauge);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLDOB);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLEthnicity);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLstRace);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLstGender);
            this.pnlPtntLstDemo.Controls.Add(this.chkPrblLstDemographicAll);
            this.pnlPtntLstDemo.Controls.Add(this.label46);
            this.pnlPtntLstDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPtntLstDemo.Location = new System.Drawing.Point(4, 1);
            this.pnlPtntLstDemo.Name = "pnlPtntLstDemo";
            this.pnlPtntLstDemo.Size = new System.Drawing.Size(1268, 74);
            this.pnlPtntLstDemo.TabIndex = 222;
            this.pnlPtntLstDemo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPtntLstDemo_Paint);
            // 
            // chksZip
            // 
            this.chksZip.AutoSize = true;
            this.chksZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksZip.Location = new System.Drawing.Point(746, 27);
            this.chksZip.Name = "chksZip";
            this.chksZip.Size = new System.Drawing.Size(42, 18);
            this.chksZip.TabIndex = 231;
            this.chksZip.Text = "Zip";
            this.chksZip.UseVisualStyleBackColor = true;
            // 
            // chksAddressLine1
            // 
            this.chksAddressLine1.AutoSize = true;
            this.chksAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksAddressLine1.Location = new System.Drawing.Point(222, 27);
            this.chksAddressLine1.Name = "chksAddressLine1";
            this.chksAddressLine1.Size = new System.Drawing.Size(76, 18);
            this.chksAddressLine1.TabIndex = 227;
            this.chksAddressLine1.Text = "Address1";
            this.chksAddressLine1.UseVisualStyleBackColor = true;
            // 
            // chksAddressLine2
            // 
            this.chksAddressLine2.AutoSize = true;
            this.chksAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksAddressLine2.Location = new System.Drawing.Point(391, 27);
            this.chksAddressLine2.Name = "chksAddressLine2";
            this.chksAddressLine2.Size = new System.Drawing.Size(76, 18);
            this.chksAddressLine2.TabIndex = 228;
            this.chksAddressLine2.Text = "Address2";
            this.chksAddressLine2.UseVisualStyleBackColor = true;
            // 
            // chksState
            // 
            this.chksState.AutoSize = true;
            this.chksState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksState.Location = new System.Drawing.Point(607, 27);
            this.chksState.Name = "chksState";
            this.chksState.Size = new System.Drawing.Size(56, 18);
            this.chksState.TabIndex = 230;
            this.chksState.Text = "State";
            this.chksState.UseVisualStyleBackColor = true;
            // 
            // chksCity
            // 
            this.chksCity.AutoSize = true;
            this.chksCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksCity.Location = new System.Drawing.Point(510, 27);
            this.chksCity.Name = "chksCity";
            this.chksCity.Size = new System.Drawing.Size(46, 18);
            this.chksCity.TabIndex = 229;
            this.chksCity.Text = "City";
            this.chksCity.UseVisualStyleBackColor = true;
            // 
            // chkPtLastName
            // 
            this.chkPtLastName.AutoSize = true;
            this.chkPtLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPtLastName.Location = new System.Drawing.Point(1043, 51);
            this.chkPtLastName.Name = "chkPtLastName";
            this.chkPtLastName.Size = new System.Drawing.Size(126, 18);
            this.chkPtLastName.TabIndex = 226;
            this.chkPtLastName.Text = "Patient Last Name";
            this.chkPtLastName.UseVisualStyleBackColor = true;
            // 
            // chkPCP
            // 
            this.chkPCP.AutoSize = true;
            this.chkPCP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPCP.Location = new System.Drawing.Point(1178, 3);
            this.chkPCP.Name = "chkPCP";
            this.chkPCP.Size = new System.Drawing.Size(47, 18);
            this.chkPCP.TabIndex = 222;
            this.chkPCP.Text = "PCP";
            this.chkPCP.UseVisualStyleBackColor = true;
            // 
            // chkPtMiddleName
            // 
            this.chkPtMiddleName.AutoSize = true;
            this.chkPtMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPtMiddleName.Location = new System.Drawing.Point(887, 51);
            this.chkPtMiddleName.Name = "chkPtMiddleName";
            this.chkPtMiddleName.Size = new System.Drawing.Size(138, 18);
            this.chkPtMiddleName.TabIndex = 225;
            this.chkPtMiddleName.Text = "Patient Middle Name";
            this.chkPtMiddleName.UseVisualStyleBackColor = true;
            // 
            // chkPatNotes
            // 
            this.chkPatNotes.AutoSize = true;
            this.chkPatNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPatNotes.Location = new System.Drawing.Point(391, 51);
            this.chkPatNotes.Name = "chkPatNotes";
            this.chkPatNotes.Size = new System.Drawing.Size(101, 18);
            this.chkPatNotes.TabIndex = 15;
            this.chkPatNotes.Text = "Patient Notes";
            this.chkPatNotes.UseVisualStyleBackColor = true;
            // 
            // chkPtFirstName
            // 
            this.chkPtFirstName.AutoSize = true;
            this.chkPtFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPtFirstName.Location = new System.Drawing.Point(746, 51);
            this.chkPtFirstName.Name = "chkPtFirstName";
            this.chkPtFirstName.Size = new System.Drawing.Size(126, 18);
            this.chkPtFirstName.TabIndex = 224;
            this.chkPtFirstName.Text = "Patient First Name";
            this.chkPtFirstName.UseVisualStyleBackColor = true;
            // 
            // chkPriInsPlan
            // 
            this.chkPriInsPlan.AutoSize = true;
            this.chkPriInsPlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPriInsPlan.Location = new System.Drawing.Point(887, 3);
            this.chkPriInsPlan.Name = "chkPriInsPlan";
            this.chkPriInsPlan.Size = new System.Drawing.Size(85, 18);
            this.chkPriInsPlan.TabIndex = 6;
            this.chkPriInsPlan.Text = "Pri Ins Plan";
            this.chkPriInsPlan.UseVisualStyleBackColor = true;
            // 
            // ChkSecInsPlan
            // 
            this.ChkSecInsPlan.AutoSize = true;
            this.ChkSecInsPlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkSecInsPlan.Location = new System.Drawing.Point(1043, 3);
            this.ChkSecInsPlan.Name = "ChkSecInsPlan";
            this.ChkSecInsPlan.Size = new System.Drawing.Size(92, 18);
            this.ChkSecInsPlan.TabIndex = 7;
            this.ChkSecInsPlan.Text = "Sec Ins Plan";
            this.ChkSecInsPlan.UseVisualStyleBackColor = true;
            // 
            // chkMobile
            // 
            this.chkMobile.AutoSize = true;
            this.chkMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMobile.Location = new System.Drawing.Point(1043, 27);
            this.chkMobile.Name = "chkMobile";
            this.chkMobile.Size = new System.Drawing.Size(60, 18);
            this.chkMobile.TabIndex = 10;
            this.chkMobile.Text = "Mobile";
            this.chkMobile.UseVisualStyleBackColor = true;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmail.Location = new System.Drawing.Point(1178, 27);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(53, 18);
            this.chkEmail.TabIndex = 11;
            this.chkEmail.Text = "Email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // chkPhone
            // 
            this.chkPhone.AutoSize = true;
            this.chkPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPhone.Location = new System.Drawing.Point(887, 27);
            this.chkPhone.Name = "chkPhone";
            this.chkPhone.Size = new System.Drawing.Size(61, 18);
            this.chkPhone.TabIndex = 9;
            this.chkPhone.Text = "Phone";
            this.chkPhone.UseVisualStyleBackColor = true;
            // 
            // chkAddress
            // 
            this.chkAddress.AutoSize = true;
            this.chkAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddress.Location = new System.Drawing.Point(96, 27);
            this.chkAddress.Name = "chkAddress";
            this.chkAddress.Size = new System.Drawing.Size(69, 18);
            this.chkAddress.TabIndex = 8;
            this.chkAddress.Text = "Address";
            this.chkAddress.UseVisualStyleBackColor = true;
            // 
            // chkPrblLMedicalCategory
            // 
            this.chkPrblLMedicalCategory.AutoSize = true;
            this.chkPrblLMedicalCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLMedicalCategory.Location = new System.Drawing.Point(96, 51);
            this.chkPrblLMedicalCategory.Name = "chkPrblLMedicalCategory";
            this.chkPrblLMedicalCategory.Size = new System.Drawing.Size(118, 18);
            this.chkPrblLMedicalCategory.TabIndex = 12;
            this.chkPrblLMedicalCategory.Text = "Medical Category";
            this.chkPrblLMedicalCategory.UseVisualStyleBackColor = true;
            // 
            // chksortlistwthnpatnt
            // 
            this.chksortlistwthnpatnt.AutoSize = true;
            this.chksortlistwthnpatnt.Checked = true;
            this.chksortlistwthnpatnt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chksortlistwthnpatnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksortlistwthnpatnt.Location = new System.Drawing.Point(222, 51);
            this.chksortlistwthnpatnt.Name = "chksortlistwthnpatnt";
            this.chksortlistwthnpatnt.Size = new System.Drawing.Size(151, 18);
            this.chksortlistwthnpatnt.TabIndex = 16;
            this.chksortlistwthnpatnt.Text = "Single Row Per Patient";
            this.chksortlistwthnpatnt.UseVisualStyleBackColor = true;
            // 
            // chkPrblLAge
            // 
            this.chkPrblLAge.AutoSize = true;
            this.chkPrblLAge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLAge.Location = new System.Drawing.Point(222, 3);
            this.chkPrblLAge.Name = "chkPrblLAge";
            this.chkPrblLAge.Size = new System.Drawing.Size(48, 18);
            this.chkPrblLAge.TabIndex = 1;
            this.chkPrblLAge.Text = "Age";
            this.chkPrblLAge.UseVisualStyleBackColor = true;
            // 
            // chkPrblLCommPref
            // 
            this.chkPrblLCommPref.AutoSize = true;
            this.chkPrblLCommPref.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLCommPref.Location = new System.Drawing.Point(607, 51);
            this.chkPrblLCommPref.Name = "chkPrblLCommPref";
            this.chkPrblLCommPref.Size = new System.Drawing.Size(124, 18);
            this.chkPrblLCommPref.TabIndex = 13;
            this.chkPrblLCommPref.Text = "Comm Preference";
            this.chkPrblLCommPref.UseVisualStyleBackColor = true;
            // 
            // chkPrblLLangauge
            // 
            this.chkPrblLLangauge.AutoSize = true;
            this.chkPrblLLangauge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLLangauge.Location = new System.Drawing.Point(510, 51);
            this.chkPrblLLangauge.Name = "chkPrblLLangauge";
            this.chkPrblLLangauge.Size = new System.Drawing.Size(79, 18);
            this.chkPrblLLangauge.TabIndex = 14;
            this.chkPrblLLangauge.Text = "Language";
            this.chkPrblLLangauge.UseVisualStyleBackColor = true;
            // 
            // chkPrblLDOB
            // 
            this.chkPrblLDOB.AutoSize = true;
            this.chkPrblLDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLDOB.Location = new System.Drawing.Point(391, 3);
            this.chkPrblLDOB.Name = "chkPrblLDOB";
            this.chkPrblLDOB.Size = new System.Drawing.Size(50, 18);
            this.chkPrblLDOB.TabIndex = 2;
            this.chkPrblLDOB.Text = "DOB";
            this.chkPrblLDOB.UseVisualStyleBackColor = true;
            // 
            // chkPrblLEthnicity
            // 
            this.chkPrblLEthnicity.AutoSize = true;
            this.chkPrblLEthnicity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLEthnicity.Location = new System.Drawing.Point(746, 3);
            this.chkPrblLEthnicity.Name = "chkPrblLEthnicity";
            this.chkPrblLEthnicity.Size = new System.Drawing.Size(73, 18);
            this.chkPrblLEthnicity.TabIndex = 5;
            this.chkPrblLEthnicity.Text = "Ethnicity";
            this.chkPrblLEthnicity.UseVisualStyleBackColor = true;
            // 
            // chkPrblLstRace
            // 
            this.chkPrblLstRace.AutoSize = true;
            this.chkPrblLstRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLstRace.Location = new System.Drawing.Point(607, 3);
            this.chkPrblLstRace.Name = "chkPrblLstRace";
            this.chkPrblLstRace.Size = new System.Drawing.Size(52, 18);
            this.chkPrblLstRace.TabIndex = 4;
            this.chkPrblLstRace.Text = "Race";
            this.chkPrblLstRace.UseVisualStyleBackColor = true;
            // 
            // chkPrblLstGender
            // 
            this.chkPrblLstGender.AutoSize = true;
            this.chkPrblLstGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLstGender.Location = new System.Drawing.Point(510, 3);
            this.chkPrblLstGender.Name = "chkPrblLstGender";
            this.chkPrblLstGender.Size = new System.Drawing.Size(66, 18);
            this.chkPrblLstGender.TabIndex = 3;
            this.chkPrblLstGender.Text = "Gender";
            this.chkPrblLstGender.UseVisualStyleBackColor = true;
            // 
            // chkPrblLstDemographicAll
            // 
            this.chkPrblLstDemographicAll.AutoSize = true;
            this.chkPrblLstDemographicAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrblLstDemographicAll.Location = new System.Drawing.Point(96, 3);
            this.chkPrblLstDemographicAll.Name = "chkPrblLstDemographicAll";
            this.chkPrblLstDemographicAll.Size = new System.Drawing.Size(38, 18);
            this.chkPrblLstDemographicAll.TabIndex = 0;
            this.chkPrblLstDemographicAll.Text = "All";
            this.chkPrblLstDemographicAll.UseVisualStyleBackColor = true;
            this.chkPrblLstDemographicAll.CheckedChanged += new System.EventHandler(this.chkPrblLstDemographicAll_CheckedChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.BackColor = System.Drawing.Color.Transparent;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(3, 5);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(86, 28);
            this.label46.TabIndex = 221;
            this.label46.Text = "Demographic :\r\n       Element";
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(4, 100);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1268, 1);
            this.label47.TabIndex = 16;
            this.label47.Text = "label47";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(4, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1268, 1);
            this.label48.TabIndex = 15;
            this.label48.Text = "label1";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(3, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 101);
            this.label53.TabIndex = 13;
            this.label53.Text = "label53";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Right;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(1272, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 101);
            this.label52.TabIndex = 14;
            this.label52.Text = "label4";
            // 
            // pnlClaimDetails
            // 
            this.pnlClaimDetails.Controls.Add(this.pnlFacility);
            this.pnlClaimDetails.Controls.Add(this.pnlClaimDx);
            this.pnlClaimDetails.Controls.Add(this.label66);
            this.pnlClaimDetails.Controls.Add(this.pnlClaimCpt);
            this.pnlClaimDetails.Controls.Add(this.label68);
            this.pnlClaimDetails.Controls.Add(this.label69);
            this.pnlClaimDetails.Controls.Add(this.label70);
            this.pnlClaimDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClaimDetails.Location = new System.Drawing.Point(0, 395);
            this.pnlClaimDetails.Name = "pnlClaimDetails";
            this.pnlClaimDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlClaimDetails.Size = new System.Drawing.Size(1276, 106);
            this.pnlClaimDetails.TabIndex = 4;
            this.pnlClaimDetails.Visible = false;
            // 
            // pnlFacility
            // 
            this.pnlFacility.Controls.Add(this.label71);
            this.pnlFacility.Controls.Add(this.btnClearAllFacility);
            this.pnlFacility.Controls.Add(this.btnClearFacility);
            this.pnlFacility.Controls.Add(this.btnBrowseFacility);
            this.pnlFacility.Controls.Add(this.lstFacility);
            this.pnlFacility.Location = new System.Drawing.Point(494, 2);
            this.pnlFacility.Name = "pnlFacility";
            this.pnlFacility.Size = new System.Drawing.Size(263, 97);
            this.pnlFacility.TabIndex = 2;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(88, 4);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(48, 14);
            this.label71.TabIndex = 213;
            this.label71.Text = "Facility";
            // 
            // btnClearAllFacility
            // 
            this.btnClearAllFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllFacility.BackgroundImage")));
            this.btnClearAllFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllFacility.Image")));
            this.btnClearAllFacility.Location = new System.Drawing.Point(202, 72);
            this.btnClearAllFacility.Name = "btnClearAllFacility";
            this.btnClearAllFacility.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllFacility.TabIndex = 3;
            this.btnClearAllFacility.UseVisualStyleBackColor = false;
            this.btnClearAllFacility.Click += new System.EventHandler(this.btnClearAllFacility_Click);
            // 
            // btnClearFacility
            // 
            this.btnClearFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnClearFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearFacility.BackgroundImage")));
            this.btnClearFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFacility.Image")));
            this.btnClearFacility.Location = new System.Drawing.Point(202, 46);
            this.btnClearFacility.Name = "btnClearFacility";
            this.btnClearFacility.Size = new System.Drawing.Size(22, 22);
            this.btnClearFacility.TabIndex = 2;
            this.btnClearFacility.UseVisualStyleBackColor = false;
            this.btnClearFacility.Click += new System.EventHandler(this.btnClearFacility_Click);
            // 
            // btnBrowseFacility
            // 
            this.btnBrowseFacility.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseFacility.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.BackgroundImage")));
            this.btnBrowseFacility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseFacility.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseFacility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFacility.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseFacility.Image")));
            this.btnBrowseFacility.Location = new System.Drawing.Point(202, 20);
            this.btnBrowseFacility.Name = "btnBrowseFacility";
            this.btnBrowseFacility.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseFacility.TabIndex = 0;
            this.btnBrowseFacility.UseVisualStyleBackColor = false;
            this.btnBrowseFacility.Click += new System.EventHandler(this.btnBrowseFacility_Click);
            // 
            // lstFacility
            // 
            this.lstFacility.FormattingEnabled = true;
            this.lstFacility.ItemHeight = 14;
            this.lstFacility.Location = new System.Drawing.Point(4, 20);
            this.lstFacility.Name = "lstFacility";
            this.lstFacility.Size = new System.Drawing.Size(193, 74);
            this.lstFacility.TabIndex = 1;
            // 
            // pnlClaimDx
            // 
            this.pnlClaimDx.Controls.Add(this.label65);
            this.pnlClaimDx.Controls.Add(this.btnClearAllClaimDx);
            this.pnlClaimDx.Controls.Add(this.btnClearClaimDx);
            this.pnlClaimDx.Controls.Add(this.btnBrowseClaimDx);
            this.pnlClaimDx.Controls.Add(this.lstClaimDx);
            this.pnlClaimDx.Location = new System.Drawing.Point(251, 2);
            this.pnlClaimDx.Name = "pnlClaimDx";
            this.pnlClaimDx.Size = new System.Drawing.Size(237, 97);
            this.pnlClaimDx.TabIndex = 1;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(88, 4);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(59, 14);
            this.label65.TabIndex = 213;
            this.label65.Text = "Claim Dx";
            // 
            // btnClearAllClaimDx
            // 
            this.btnClearAllClaimDx.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllClaimDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllClaimDx.BackgroundImage")));
            this.btnClearAllClaimDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllClaimDx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllClaimDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllClaimDx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllClaimDx.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllClaimDx.Image")));
            this.btnClearAllClaimDx.Location = new System.Drawing.Point(202, 72);
            this.btnClearAllClaimDx.Name = "btnClearAllClaimDx";
            this.btnClearAllClaimDx.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllClaimDx.TabIndex = 3;
            this.btnClearAllClaimDx.UseVisualStyleBackColor = false;
            this.btnClearAllClaimDx.Click += new System.EventHandler(this.btnClearAllClaimDx_Click);
            // 
            // btnClearClaimDx
            // 
            this.btnClearClaimDx.BackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearClaimDx.BackgroundImage")));
            this.btnClearClaimDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearClaimDx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearClaimDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearClaimDx.Image = ((System.Drawing.Image)(resources.GetObject("btnClearClaimDx.Image")));
            this.btnClearClaimDx.Location = new System.Drawing.Point(202, 46);
            this.btnClearClaimDx.Name = "btnClearClaimDx";
            this.btnClearClaimDx.Size = new System.Drawing.Size(22, 22);
            this.btnClearClaimDx.TabIndex = 2;
            this.btnClearClaimDx.UseVisualStyleBackColor = false;
            this.btnClearClaimDx.Click += new System.EventHandler(this.btnClearClaimDx_Click);
            // 
            // btnBrowseClaimDx
            // 
            this.btnBrowseClaimDx.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseClaimDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseClaimDx.BackgroundImage")));
            this.btnBrowseClaimDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseClaimDx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseClaimDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseClaimDx.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseClaimDx.Image")));
            this.btnBrowseClaimDx.Location = new System.Drawing.Point(202, 20);
            this.btnBrowseClaimDx.Name = "btnBrowseClaimDx";
            this.btnBrowseClaimDx.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseClaimDx.TabIndex = 0;
            this.btnBrowseClaimDx.UseVisualStyleBackColor = false;
            this.btnBrowseClaimDx.Click += new System.EventHandler(this.btnBrowseClaimDx_Click);
            // 
            // lstClaimDx
            // 
            this.lstClaimDx.FormattingEnabled = true;
            this.lstClaimDx.ItemHeight = 14;
            this.lstClaimDx.Location = new System.Drawing.Point(4, 20);
            this.lstClaimDx.Name = "lstClaimDx";
            this.lstClaimDx.Size = new System.Drawing.Size(193, 74);
            this.lstClaimDx.TabIndex = 1;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(4, 102);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(1268, 1);
            this.label66.TabIndex = 16;
            this.label66.Text = "label1";
            // 
            // pnlClaimCpt
            // 
            this.pnlClaimCpt.Controls.Add(this.btnClearAllClaimCpt);
            this.pnlClaimCpt.Controls.Add(this.btnClearClaimCpt);
            this.pnlClaimCpt.Controls.Add(this.btnBrowseClaimCpt);
            this.pnlClaimCpt.Controls.Add(this.lstClaimCpt);
            this.pnlClaimCpt.Controls.Add(this.label67);
            this.pnlClaimCpt.Location = new System.Drawing.Point(5, 2);
            this.pnlClaimCpt.Name = "pnlClaimCpt";
            this.pnlClaimCpt.Size = new System.Drawing.Size(242, 97);
            this.pnlClaimCpt.TabIndex = 0;
            // 
            // btnClearAllClaimCpt
            // 
            this.btnClearAllClaimCpt.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAllClaimCpt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAllClaimCpt.BackgroundImage")));
            this.btnClearAllClaimCpt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAllClaimCpt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAllClaimCpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAllClaimCpt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAllClaimCpt.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAllClaimCpt.Image")));
            this.btnClearAllClaimCpt.Location = new System.Drawing.Point(211, 72);
            this.btnClearAllClaimCpt.Name = "btnClearAllClaimCpt";
            this.btnClearAllClaimCpt.Size = new System.Drawing.Size(22, 22);
            this.btnClearAllClaimCpt.TabIndex = 3;
            this.btnClearAllClaimCpt.UseVisualStyleBackColor = false;
            this.btnClearAllClaimCpt.Click += new System.EventHandler(this.btnClearAllClaimCpt_Click);
            // 
            // btnClearClaimCpt
            // 
            this.btnClearClaimCpt.BackColor = System.Drawing.Color.Transparent;
            this.btnClearClaimCpt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearClaimCpt.BackgroundImage")));
            this.btnClearClaimCpt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearClaimCpt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearClaimCpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearClaimCpt.Image = ((System.Drawing.Image)(resources.GetObject("btnClearClaimCpt.Image")));
            this.btnClearClaimCpt.Location = new System.Drawing.Point(211, 46);
            this.btnClearClaimCpt.Name = "btnClearClaimCpt";
            this.btnClearClaimCpt.Size = new System.Drawing.Size(22, 22);
            this.btnClearClaimCpt.TabIndex = 2;
            this.btnClearClaimCpt.UseVisualStyleBackColor = false;
            this.btnClearClaimCpt.Click += new System.EventHandler(this.btnClearClaimCpt_Click);
            // 
            // btnBrowseClaimCpt
            // 
            this.btnBrowseClaimCpt.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseClaimCpt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseClaimCpt.BackgroundImage")));
            this.btnBrowseClaimCpt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseClaimCpt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseClaimCpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseClaimCpt.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseClaimCpt.Image")));
            this.btnBrowseClaimCpt.Location = new System.Drawing.Point(211, 20);
            this.btnBrowseClaimCpt.Name = "btnBrowseClaimCpt";
            this.btnBrowseClaimCpt.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseClaimCpt.TabIndex = 0;
            this.btnBrowseClaimCpt.UseVisualStyleBackColor = false;
            this.btnBrowseClaimCpt.Click += new System.EventHandler(this.btnBrowseClaimCpt_Click);
            // 
            // lstClaimCpt
            // 
            this.lstClaimCpt.FormattingEnabled = true;
            this.lstClaimCpt.ItemHeight = 14;
            this.lstClaimCpt.Location = new System.Drawing.Point(6, 20);
            this.lstClaimCpt.Name = "lstClaimCpt";
            this.lstClaimCpt.Size = new System.Drawing.Size(200, 74);
            this.lstClaimCpt.TabIndex = 1;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(81, 4);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(66, 14);
            this.label67.TabIndex = 213;
            this.label67.Text = "Claim CPT";
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Top;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(4, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(1268, 1);
            this.label68.TabIndex = 15;
            this.label68.Text = "label1";
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Right;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(1272, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1, 103);
            this.label69.TabIndex = 14;
            this.label69.Text = "label4";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Left;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(3, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 103);
            this.label70.TabIndex = 13;
            this.label70.Text = "label4";
            // 
            // frmSSRSViewer
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1276, 896);
            this.Controls.Add(this.pnlSSRSRpt);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.pnlLabTestResult);
            this.Controls.Add(this.pnlIncludeDemographics);
            this.Controls.Add(this.pnlDmPatientList);
            this.Controls.Add(this.pnlClaimDetails);
            this.Controls.Add(this.pnlPatientList);
            this.Controls.Add(this.pnlDrugDiagnosis);
            this.Controls.Add(this.pnlDemoFilter);
            this.Controls.Add(this.pnlProvider);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.pnlcustomTask);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSSRSViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSRS Reports";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSSRSViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmSSRSViewer_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tblStrip_32.ResumeLayout(false);
            this.tblStrip_32.PerformLayout();
            this.pnlSSRSRpt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).EndInit();
            this.pnlWarning.ResumeLayout(false);
            this.pnlMessage.ResumeLayout(false);
            this.pnlMessage.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlMedicationDate.ResumeLayout(false);
            this.pnlMedicationDate.PerformLayout();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.pnlDemo.ResumeLayout(false);
            this.pnlDemo.PerformLayout();
            this.pnlCheckBoxes.ResumeLayout(false);
            this.pnlCheckBoxes.PerformLayout();
            this.pnlMed.ResumeLayout(false);
            this.pnlMed.PerformLayout();
            this.pnlDiag.ResumeLayout(false);
            this.pnlDiag.PerformLayout();
            this.pnlDrugDiagnosis.ResumeLayout(false);
            this.pnlDmPatientList.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.pnlPatientList.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlTreat.ResumeLayout(false);
            this.pnlLabTestResult.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlTO.ResumeLayout(false);
            this.pnlTO.PerformLayout();
            this.pnlDemoFilter.ResumeLayout(false);
            this.pnlDemoFilter.PerformLayout();
            this.pnlIncludeDemographics.ResumeLayout(false);
            this.pnlPstnOthr.ResumeLayout(false);
            this.pnlPstnOthr.PerformLayout();
            this.pnlPtntLstDemo.ResumeLayout(false);
            this.pnlPtntLstDemo.PerformLayout();
            this.pnlClaimDetails.ResumeLayout(false);
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.pnlClaimDx.ResumeLayout(false);
            this.pnlClaimDx.PerformLayout();
            this.pnlClaimCpt.ResumeLayout(false);
            this.pnlClaimCpt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus tblStrip_32;
        internal System.Windows.Forms.ToolStripButton tblbtnGenReport;
        internal System.Windows.Forms.ToolStripButton tblbtn_Print_32;
        private System.Windows.Forms.ToolStripButton tblbtn_Export;
        internal System.Windows.Forms.ToolStripButton Tblbtn_More;
        internal System.Windows.Forms.ToolStripButton Tblbtn_Hide;
        internal System.Windows.Forms.ToolStripButton tblbtn_Close_32;
        internal System.Windows.Forms.Panel pnlSSRSRpt;
        private System.Windows.Forms.Label Label20;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Panel pnlProvider;
        internal System.Windows.Forms.Label lblAgeTo;
        internal System.Windows.Forms.DateTimePicker dtpicTo;
        internal System.Windows.Forms.Label lblAgeFrom;
        internal System.Windows.Forms.ComboBox cmbAgeTo;
        internal System.Windows.Forms.DateTimePicker dtpicFrom;
        internal System.Windows.Forms.ComboBox cmbAgeFrom;
        internal System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.ComboBox cmbAge;
        internal System.Windows.Forms.ListBox LstTreatment;
        internal System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Lblage;
        internal System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlCheckBoxes;
        private System.Windows.Forms.CheckBox chkShowUsageDeatal;
        private System.Windows.Forms.CheckBox chkOnlyDrug;
        internal System.Windows.Forms.Label lblDemoElement;
        private System.Windows.Forms.Panel pnlDemo;
        private System.Windows.Forms.CheckBox chkInsurance;
        private System.Windows.Forms.CheckBox chkLanguage;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkEthnicity;
        private System.Windows.Forms.CheckBox chkRace;
        private System.Windows.Forms.CheckBox chkGender;
        private System.Windows.Forms.CheckBox chkDOB;
        private System.Windows.Forms.Panel pnlMed;
        internal System.Windows.Forms.RadioButton rbtnAllMedications;
        internal System.Windows.Forms.RadioButton rbtnPresByClinic;
        internal System.Windows.Forms.Label lblMedication;
        internal System.Windows.Forms.Button BtnClearAllDrg;
        internal System.Windows.Forms.Button btnClearDrug;
        internal System.Windows.Forms.Button btnBrowseDrug;
        internal System.Windows.Forms.ListBox LstMedication;
        private System.Windows.Forms.Panel pnlDiag;
        internal System.Windows.Forms.Label lblDiagnosis;
        internal System.Windows.Forms.Button btnClearAllDiag;
        internal System.Windows.Forms.Button btnClearDiag;
        internal System.Windows.Forms.Button btnBrowseDiag;
        internal System.Windows.Forms.ListBox LstDiagnosis;
        internal System.Windows.Forms.Panel pnlDrugDiagnosis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlcustomTask;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.CheckBox chkExcControlledSubstance;
        private System.Windows.Forms.Panel pnlDmPatientList;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ComboBox cmbapptst;
        internal System.Windows.Forms.Label label44;
        private System.Windows.Forms.CheckBox ChkAppt;
        internal System.Windows.Forms.DateTimePicker dtToAppt;
        internal System.Windows.Forms.DateTimePicker dtFromAppt;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel9;
        internal System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Button btnClearAllDMPatientprb;
        internal System.Windows.Forms.Button btnClearDMPatientprb;
        internal System.Windows.Forms.Button btnBrowseDMPatientPrb;
        internal System.Windows.Forms.ListBox lstDMProblemList;
        private System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.Button BtnClearAllDMSetup;
        internal System.Windows.Forms.Button btnClearDMSetup;
        internal System.Windows.Forms.Button btnBrowseDMSetup;
        internal System.Windows.Forms.ListBox lstdmsetup;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.Button btnClearAllDMMedication;
        internal System.Windows.Forms.Button btnClearDMMedication;
        internal System.Windows.Forms.Button btnBrowseDMMedication;
        internal System.Windows.Forms.ListBox lstmeddmsetup;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Patients;
        internal System.Windows.Forms.ToolStripButton tblButtonShow;
        internal System.Windows.Forms.ToolStripButton tsb_ReminderLetters;
        internal System.Windows.Forms.Panel pnlPatientList;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.RadioButton rbImGiven;
        internal System.Windows.Forms.RadioButton rbImNotGiven;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Button BtnClearAllPatientDrug;
        internal System.Windows.Forms.Button btnClearPatientDrug;
        internal System.Windows.Forms.Button btnBrowsePatientDrug;
        internal System.Windows.Forms.ListBox lstPatMedication;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Label label37;
        internal System.Windows.Forms.Button BtnClearAllImmunization;
        internal System.Windows.Forms.Button btnClearImmunization;
        internal System.Windows.Forms.Button btnSearchImmunization;
        internal System.Windows.Forms.ListBox lstImmunization;
        internal System.Windows.Forms.ComboBox cmbSndPat;
        internal System.Windows.Forms.ComboBox cmb3rdPat;
        internal System.Windows.Forms.ComboBox cmbFstPat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Button btnClearAllPatientprb;
        internal System.Windows.Forms.Button btnClearPatientprb;
        internal System.Windows.Forms.Button btnBrowsePatientPrb;
        internal System.Windows.Forms.ListBox lstProblemList;
        private System.Windows.Forms.Panel pnlTreat;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Button btnBrowseLabTestResult;
        internal System.Windows.Forms.ListBox lstLabResult;
        internal System.Windows.Forms.Button btnClearAllPatientLab;
        internal System.Windows.Forms.Button btnClearPatientLab;
        private System.Windows.Forms.Panel pnlLabTestResult;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label lblPatTo;
        internal System.Windows.Forms.Label lblPatFrom;
        internal System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlTO;
        internal System.Windows.Forms.Button btlCloseLabResult;
        internal System.Windows.Forms.Label label43;
        internal System.Windows.Forms.Button btnAddResultCond;
        internal System.Windows.Forms.Label lblNext;
        internal System.Windows.Forms.ComboBox cmbThiPat;
        internal System.Windows.Forms.Label lblToBlank;
        private System.Windows.Forms.TextBox txtPatTo;
        internal System.Windows.Forms.Label lblForm;
        private System.Windows.Forms.TextBox txtPatFrom;
        internal System.Windows.Forms.Label lblCondition;
        internal System.Windows.Forms.ComboBox cmbPatCondition;
        internal System.Windows.Forms.Label lblLab;
        internal System.Windows.Forms.Button btnBrowseLabResults;
        internal System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox cmbLabResult;
        internal System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlMessage;
        internal System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Panel panel11;
        internal System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Button BtnClearAllAllergy;
        internal System.Windows.Forms.Button BtnClearAllergy;
        internal System.Windows.Forms.Button BtnBrowserAllergy;
        internal System.Windows.Forms.ListBox lstAllergy;
        internal System.Windows.Forms.ComboBox cmbMediAll;
        internal System.Windows.Forms.Panel pnlDemoFilter;
        internal System.Windows.Forms.Label label54;
        internal System.Windows.Forms.ComboBox cmbethnicity;
        internal System.Windows.Forms.ComboBox cmbGender;
        internal System.Windows.Forms.ComboBox cmbRace;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        internal System.Windows.Forms.Label label58;
        internal System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        internal System.Windows.Forms.Label label50;
        internal System.Windows.Forms.ComboBox cmblanguage;
        internal System.Windows.Forms.Label label51;
        internal System.Windows.Forms.ComboBox cmbComPre;
        internal System.Windows.Forms.Button btnClearCommPref;
        internal System.Windows.Forms.Button btnBrowseCommPref;
        internal System.Windows.Forms.Button btnClearLanguage;
        internal System.Windows.Forms.Button btnBrowseLanguage;
        internal System.Windows.Forms.Button btnclearethnicity;
        internal System.Windows.Forms.Button btnBrowseEthnicity;
        internal System.Windows.Forms.Button btnCleaseRace;
        internal System.Windows.Forms.Button btnBrowseRace;
        internal System.Windows.Forms.Button btncleargender;
        internal System.Windows.Forms.Button btnBrowseGender;
        internal System.Windows.Forms.Button btnBrowseSnomedCT;
        internal System.Windows.Forms.Panel pnlIncludeDemographics;
        private System.Windows.Forms.Panel pnlPtntLstDemo;
        private System.Windows.Forms.CheckBox chkPrblLCommPref;
        private System.Windows.Forms.CheckBox chkPrblLLangauge;
        private System.Windows.Forms.CheckBox chkPrblLDOB;
        private System.Windows.Forms.CheckBox chkPrblLEthnicity;
        private System.Windows.Forms.CheckBox chkPrblLstRace;
        private System.Windows.Forms.CheckBox chkPrblLstGender;
        private System.Windows.Forms.CheckBox chkPrblLstDemographicAll;
        internal System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Panel pnlPstnOthr;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrLabResult;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrImmunization;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrMedication;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrProblemList;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrElement;
        internal System.Windows.Forms.Label label61;
        private System.Windows.Forms.CheckBox chkPrblmLstOthrAllergy;
        private System.Windows.Forms.CheckBox chkPrblLAge;
        private System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.ToolStripButton Tblbtn_DisplayOptn;
        private System.Windows.Forms.CheckBox chkDateTime;
        private System.Windows.Forms.Panel pnlWarning;
        internal System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.CheckBox chksortlistwthnpatnt;
        internal System.Windows.Forms.Button btnClearMedCategory;
        internal System.Windows.Forms.Button btnBrowseMedCategory;
        internal System.Windows.Forms.Label label64;
        internal System.Windows.Forms.ComboBox cmbMedicalCategory;
        private System.Windows.Forms.CheckBox chkPrblLMedicalCategory;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.CheckBox chkPhone;
        private System.Windows.Forms.CheckBox chkAddress;
        private System.Windows.Forms.CheckBox chkMobile;
        internal System.Windows.Forms.Panel pnlClaimDetails;
        private System.Windows.Forms.Panel pnlClaimDx;
        internal System.Windows.Forms.Label label65;
        internal System.Windows.Forms.Button btnClearAllClaimDx;
        internal System.Windows.Forms.Button btnClearClaimDx;
        internal System.Windows.Forms.Button btnBrowseClaimDx;
        internal System.Windows.Forms.ListBox lstClaimDx;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Panel pnlClaimCpt;
        internal System.Windows.Forms.Button btnClearAllClaimCpt;
        internal System.Windows.Forms.Button btnClearClaimCpt;
        internal System.Windows.Forms.Button btnBrowseClaimCpt;
        internal System.Windows.Forms.ListBox lstClaimCpt;
        internal System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.CheckBox chkClaimDX;
        private System.Windows.Forms.CheckBox chkClaimCPT;
        private System.Windows.Forms.CheckBox chkFacility;
        private System.Windows.Forms.Panel pnlFacility;
        internal System.Windows.Forms.Label label71;
        internal System.Windows.Forms.Button btnClearAllFacility;
        internal System.Windows.Forms.Button btnClearFacility;
        internal System.Windows.Forms.Button btnBrowseFacility;
        internal System.Windows.Forms.ListBox lstFacility;
        internal System.Windows.Forms.Button btnClearInsPlan;
        internal System.Windows.Forms.Button btnBrowseInsPlan;
        internal System.Windows.Forms.Label label72;
        internal System.Windows.Forms.ComboBox cmbPatInsPlan;
        private System.Windows.Forms.CheckBox chkPriInsPlan;
        private System.Windows.Forms.CheckBox ChkSecInsPlan;
        private System.Windows.Forms.CheckBox chkPatNotes;
        private System.Windows.Forms.CheckBox chkPCP;
        private System.Windows.Forms.CheckBox chkDueDate;
        internal System.Windows.Forms.DateTimePicker dtToDueDate;
        internal System.Windows.Forms.DateTimePicker dtFromDueDate;
        internal System.Windows.Forms.Label label73;
        internal System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel pnlMedicationDate;
        private System.Windows.Forms.Label label75;
        internal System.Windows.Forms.DateTimePicker dtMedicationEndDate;
        internal System.Windows.Forms.Label label76;
        internal System.Windows.Forms.DateTimePicker dtMedicationStartDate;
        internal System.Windows.Forms.Label label77;
        private System.Windows.Forms.CheckBox chkPtLastName;
        private System.Windows.Forms.CheckBox chkPtMiddleName;
        private System.Windows.Forms.CheckBox chkPtFirstName;
        private System.Windows.Forms.CheckBox chkShowAllProviders;
        private System.Windows.Forms.CheckBox chksZip;
        private System.Windows.Forms.CheckBox chksAddressLine1;
        private System.Windows.Forms.CheckBox chksAddressLine2;
        private System.Windows.Forms.CheckBox chksState;
        private System.Windows.Forms.CheckBox chksCity; 



    }
}
