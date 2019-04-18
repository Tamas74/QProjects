namespace gloReports
{
    partial class frmRpt_PatientStatementForGateWayEDI
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
            System.Windows.Forms.DateTimePicker[] cntdtControls  = { dtpEndDate, dtpStartDate, dtCriteriaEndDate, dtCriteriaStartDate };
            System.Windows.Forms.Control[] cntControls = { dtpEndDate, dtpStartDate, dtCriteriaEndDate, dtCriteriaStartDate };
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_PatientStatementForGateWayEDI));
            this.pnlcrvReportViewer = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblCPT = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.grpPayType = new System.Windows.Forms.GroupBox();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbAllowed = new System.Windows.Forms.RadioButton();
            this.rbCharges = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCriteria = new System.Windows.Forms.RadioButton();
            this.rbNoCriteria = new System.Windows.Forms.RadioButton();
            this.cmbSettings = new System.Windows.Forms.ComboBox();
            this.lblSelectSettings = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDueAmt = new System.Windows.Forms.TextBox();
            this.rbGreater = new System.Windows.Forms.RadioButton();
            this.rbLess = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbChargesTray = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportToTxt = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnGenerateBatchTxt = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnGenerateBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnHideCriteria = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnCriteria = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnShowList = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPatientNameCriteria = new System.Windows.Forms.Panel();
            this.grpPatientNameCriteria = new System.Windows.Forms.GroupBox();
            this.cmbNameTo = new System.Windows.Forms.ComboBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.rbFirstName = new System.Windows.Forms.RadioButton();
            this.rbLastName = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.cmbNameFrom = new System.Windows.Forms.ComboBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.pnlCDuedate = new System.Windows.Forms.Panel();
            this.grpCDueDate = new System.Windows.Forms.GroupBox();
            this.rdbCriteriaDays = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.numCriteriaDuration = new System.Windows.Forms.NumericUpDown();
            this.rdbCriteriaWeek = new System.Windows.Forms.RadioButton();
            this.rdbCriteriaYear = new System.Windows.Forms.RadioButton();
            this.rdbCriteriaMonth = new System.Windows.Forms.RadioButton();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.dtCriteriaEndDate = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbCriteriaTransactionDate = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.dtCriteriaStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlSelectSet = new System.Windows.Forms.Panel();
            this.btnModifySettings = new System.Windows.Forms.Button();
            this.btnSetupSettings = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPatientList = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlDuedate = new System.Windows.Forms.Panel();
            this.gbSelectFollowupType = new System.Windows.Forms.GroupBox();
            this.rdbDays = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.rdbWeek = new System.Windows.Forms.RadioButton();
            this.rdbYear = new System.Windows.Forms.RadioButton();
            this.rdbMonth = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlNoFiliterPatient = new System.Windows.Forms.Panel();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlFilteredPatList = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlc1PatientListHeader = new System.Windows.Forms.Panel();
            this.pnlc1PatientList = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblFileCounter = new System.Windows.Forms.Label();
            this.prgFileGeneration = new System.Windows.Forms.ProgressBar();
            this.label38 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.pnlPleasewait = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.pnlcrvReportViewer.SuspendLayout();
            this.grpPayType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlPatientNameCriteria.SuspendLayout();
            this.grpPatientNameCriteria.SuspendLayout();
            this.pnlCDuedate.SuspendLayout();
            this.grpCDueDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCriteriaDuration)).BeginInit();
            this.pnlSelectSet.SuspendLayout();
            this.pnlPatientList.SuspendLayout();
            this.pnlDuedate.SuspendLayout();
            this.gbSelectFollowupType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.pnlNoFiliterPatient.SuspendLayout();
            this.pnlFilteredPatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.pnlc1PatientListHeader.SuspendLayout();
            this.pnlc1PatientList.SuspendLayout();
            this.pnlProgressBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPleasewait.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlcrvReportViewer
            // 
            this.pnlcrvReportViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlcrvReportViewer.Controls.Add(this.label19);
            this.pnlcrvReportViewer.Controls.Add(this.label20);
            this.pnlcrvReportViewer.Controls.Add(this.label21);
            this.pnlcrvReportViewer.Controls.Add(this.label22);
            this.pnlcrvReportViewer.Controls.Add(this.crvReportViewer);
            this.pnlcrvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcrvReportViewer.Location = new System.Drawing.Point(0, 300);
            this.pnlcrvReportViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlcrvReportViewer.Name = "pnlcrvReportViewer";
            this.pnlcrvReportViewer.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.pnlcrvReportViewer.Size = new System.Drawing.Size(1489, 629);
            this.pnlcrvReportViewer.TabIndex = 91;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(5, 624);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1479, 1);
            this.label19.TabIndex = 33;
            this.label19.Text = "label19";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(5, 0);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1479, 1);
            this.label20.TabIndex = 32;
            this.label20.Text = "label20";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Location = new System.Drawing.Point(1484, 0);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 625);
            this.label21.TabIndex = 31;
            this.label21.Text = "label21";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(4, 0);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 625);
            this.label22.TabIndex = 30;
            this.label22.Text = "label22";
            // 
            // crvReportViewer
            // 
            this.crvReportViewer.ActiveViewIndex = -1;
            this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReportViewer.DisplayBackgroundEdge = false;
            this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReportViewer.EnableDrillDown = false;
            this.crvReportViewer.Font = new System.Drawing.Font("Tahoma", 9F);
            this.crvReportViewer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.crvReportViewer.Location = new System.Drawing.Point(4, 0);
            this.crvReportViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.crvReportViewer.Name = "crvReportViewer";
            this.crvReportViewer.SelectionFormula = "";
            this.crvReportViewer.ShowCloseButton = false;
            this.crvReportViewer.ShowGroupTreeButton = false;
            this.crvReportViewer.ShowPrintButton = false;
            this.crvReportViewer.ShowRefreshButton = false;
            this.crvReportViewer.Size = new System.Drawing.Size(1481, 625);
            this.crvReportViewer.TabIndex = 29;
            this.crvReportViewer.ToolPanelWidth = 267;
            this.crvReportViewer.ViewTimeSelectionFormula = "";
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_datefilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_datefilter.Location = new System.Drawing.Point(20, 44);
            this.lbl_datefilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(144, 17);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            this.lbl_datefilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(168, 39);
            this.cmb_datefilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(156, 26);
            this.cmb_datefilter.TabIndex = 217;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
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
            this.dtpEndDate.Location = new System.Drawing.Point(168, 106);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(156, 26);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStartDate.Location = new System.Drawing.Point(68, 78);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(96, 17);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDate.Location = new System.Drawing.Point(76, 111);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(88, 17);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dtpStartDate.Location = new System.Drawing.Point(168, 73);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(156, 26);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(168, 5);
            this.cmbPatients.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(247, 26);
            this.cmbPatients.TabIndex = 197;
            // 
            // lblPatient
            // 
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(91, 10);
            this.lblPatient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(72, 17);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Patient :";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCPT
            // 
            this.lblCPT.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPT.Location = new System.Drawing.Point(811, 39);
            this.lblCPT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCPT.Name = "lblCPT";
            this.lblCPT.Size = new System.Drawing.Size(49, 17);
            this.lblCPT.TabIndex = 214;
            this.lblCPT.Text = "CPT :";
            this.lblCPT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(864, 34);
            this.cmbCPT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(235, 26);
            this.cmbCPT.TabIndex = 210;
            // 
            // grpPayType
            // 
            this.grpPayType.Controls.Add(this.rbBoth);
            this.grpPayType.Controls.Add(this.rbAllowed);
            this.grpPayType.Controls.Add(this.rbCharges);
            this.grpPayType.Location = new System.Drawing.Point(499, -6);
            this.grpPayType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPayType.Name = "grpPayType";
            this.grpPayType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPayType.Size = new System.Drawing.Size(292, 41);
            this.grpPayType.TabIndex = 2;
            this.grpPayType.TabStop = false;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Checked = true;
            this.rbBoth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBoth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbBoth.Location = new System.Drawing.Point(204, 12);
            this.rbBoth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(64, 22);
            this.rbBoth.TabIndex = 1;
            this.rbBoth.TabStop = true;
            this.rbBoth.Text = "Both";
            this.rbBoth.UseVisualStyleBackColor = true;
            this.rbBoth.Visible = false;
            // 
            // rbAllowed
            // 
            this.rbAllowed.AutoSize = true;
            this.rbAllowed.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbAllowed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbAllowed.Location = new System.Drawing.Point(108, 12);
            this.rbAllowed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbAllowed.Name = "rbAllowed";
            this.rbAllowed.Size = new System.Drawing.Size(78, 22);
            this.rbAllowed.TabIndex = 1;
            this.rbAllowed.Text = "Allowed";
            this.rbAllowed.UseVisualStyleBackColor = true;
            // 
            // rbCharges
            // 
            this.rbCharges.AutoSize = true;
            this.rbCharges.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbCharges.Location = new System.Drawing.Point(12, 12);
            this.rbCharges.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbCharges.Name = "rbCharges";
            this.rbCharges.Size = new System.Drawing.Size(82, 22);
            this.rbCharges.TabIndex = 0;
            this.rbCharges.Text = "Charges";
            this.rbCharges.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCriteria);
            this.groupBox2.Controls.Add(this.rbNoCriteria);
            this.groupBox2.Location = new System.Drawing.Point(1189, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(91, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // rbCriteria
            // 
            this.rbCriteria.AutoSize = true;
            this.rbCriteria.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbCriteria.Location = new System.Drawing.Point(41, 14);
            this.rbCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbCriteria.Name = "rbCriteria";
            this.rbCriteria.Size = new System.Drawing.Size(179, 22);
            this.rbCriteria.TabIndex = 1;
            this.rbCriteria.Text = "Generate From Criteria";
            this.rbCriteria.UseVisualStyleBackColor = true;
            this.rbCriteria.CheckedChanged += new System.EventHandler(this.rbCriteria_CheckedChanged);
            // 
            // rbNoCriteria
            // 
            this.rbNoCriteria.AutoSize = true;
            this.rbNoCriteria.Checked = true;
            this.rbNoCriteria.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbNoCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbNoCriteria.Location = new System.Drawing.Point(8, 14);
            this.rbNoCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbNoCriteria.Name = "rbNoCriteria";
            this.rbNoCriteria.Size = new System.Drawing.Size(155, 22);
            this.rbNoCriteria.TabIndex = 0;
            this.rbNoCriteria.TabStop = true;
            this.rbNoCriteria.Text = "Generate From List";
            this.rbNoCriteria.UseVisualStyleBackColor = true;
            this.rbNoCriteria.CheckedChanged += new System.EventHandler(this.rbNoCriteria_CheckedChanged);
            // 
            // cmbSettings
            // 
            this.cmbSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSettings.ForeColor = System.Drawing.Color.Black;
            this.cmbSettings.FormattingEnabled = true;
            this.cmbSettings.Location = new System.Drawing.Point(177, 4);
            this.cmbSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSettings.Name = "cmbSettings";
            this.cmbSettings.Size = new System.Drawing.Size(284, 26);
            this.cmbSettings.TabIndex = 219;
            this.cmbSettings.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            // 
            // lblSelectSettings
            // 
            this.lblSelectSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectSettings.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSelectSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSelectSettings.Location = new System.Drawing.Point(40, 9);
            this.lblSelectSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectSettings.Name = "lblSelectSettings";
            this.lblSelectSettings.Size = new System.Drawing.Size(131, 17);
            this.lblSelectSettings.TabIndex = 218;
            this.lblSelectSettings.Text = "Select Settings :";
            this.lblSelectSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDueAmt);
            this.groupBox1.Controls.Add(this.rbGreater);
            this.groupBox1.Controls.Add(this.rbLess);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(5, 96);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(404, 62);
            this.groupBox1.TabIndex = 229;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Due Amount";
            // 
            // txtDueAmt
            // 
            this.txtDueAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDueAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtDueAmt.Location = new System.Drawing.Point(273, 25);
            this.txtDueAmt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDueAmt.MaxLength = 12;
            this.txtDueAmt.Name = "txtDueAmt";
            this.txtDueAmt.Size = new System.Drawing.Size(108, 26);
            this.txtDueAmt.TabIndex = 227;
            this.txtDueAmt.Text = "0.00";
            this.txtDueAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDueAmt.TextChanged += new System.EventHandler(this.txtDueAmt_TextChanged);
            this.txtDueAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDueAmt_KeyPress);
            // 
            // rbGreater
            // 
            this.rbGreater.AutoSize = true;
            this.rbGreater.Checked = true;
            this.rbGreater.Location = new System.Drawing.Point(13, 27);
            this.rbGreater.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbGreater.Name = "rbGreater";
            this.rbGreater.Size = new System.Drawing.Size(129, 22);
            this.rbGreater.TabIndex = 1;
            this.rbGreater.TabStop = true;
            this.rbGreater.Text = "Greater Than";
            this.rbGreater.UseVisualStyleBackColor = true;
            this.rbGreater.CheckedChanged += new System.EventHandler(this.rbGreater_CheckedChanged);
            // 
            // rbLess
            // 
            this.rbLess.AutoSize = true;
            this.rbLess.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLess.Location = new System.Drawing.Point(159, 27);
            this.rbLess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbLess.Name = "rbLess";
            this.rbLess.Size = new System.Drawing.Size(96, 22);
            this.rbLess.TabIndex = 0;
            this.rbLess.Text = "Less Than";
            this.rbLess.UseVisualStyleBackColor = true;
            this.rbLess.CheckedChanged += new System.EventHandler(this.rbLess_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(776, 70);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 17);
            this.label11.TabIndex = 228;
            this.label11.Text = "Zip Code :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip
            // 
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtZip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtZip.Location = new System.Drawing.Point(864, 65);
            this.txtZip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(100, 26);
            this.txtZip.TabIndex = 226;
            this.txtZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFacility.ForeColor = System.Drawing.Color.Black;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(465, 65);
            this.cmbFacility.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(293, 26);
            this.cmbFacility.TabIndex = 224;
            this.cmbFacility.SelectedIndexChanged += new System.EventHandler(this.cmbFacility_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(353, 71);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 223;
            this.label9.Text = "Facility :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentTray.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Location = new System.Drawing.Point(465, 34);
            this.cmbPaymentTray.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(293, 26);
            this.cmbPaymentTray.TabIndex = 222;
            this.cmbPaymentTray.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentTray_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(321, 41);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 17);
            this.label7.TabIndex = 221;
            this.label7.Text = "Payment Tray :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbChargesTray
            // 
            this.cmbChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChargesTray.ForeColor = System.Drawing.Color.Black;
            this.cmbChargesTray.FormattingEnabled = true;
            this.cmbChargesTray.Location = new System.Drawing.Point(465, 4);
            this.cmbChargesTray.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbChargesTray.Name = "cmbChargesTray";
            this.cmbChargesTray.Size = new System.Drawing.Size(293, 26);
            this.cmbChargesTray.TabIndex = 218;
            this.cmbChargesTray.SelectedIndexChanged += new System.EventHandler(this.cmbChargesTray_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(353, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 217;
            this.label6.Text = "Charge Tray :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(864, 4);
            this.cmbInsurance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(235, 26);
            this.cmbInsurance.TabIndex = 214;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(769, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 213;
            this.label3.Text = "Insurance :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Controls.Add(this.groupBox4);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1489, 66);
            this.pnlToolStrip.TabIndex = 257;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateReport,
            this.tsb_btnExportReport,
            this.tsb_btnExportToTxt,
            this.tsb_btnGenerateBatchTxt,
            this.tsb_btnGenerateBatch,
            this.tsb_Print,
            this.tsb_btnHideCriteria,
            this.tsb_btnCriteria,
            this.tsb_btnShowList,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1489, 57);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_GenerateReport
            // 
            this.tsb_GenerateReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateReport.Image")));
            this.tsb_GenerateReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateReport.Name = "tsb_GenerateReport";
            this.tsb_GenerateReport.Size = new System.Drawing.Size(137, 54);
            this.tsb_GenerateReport.Tag = "Generate Report";
            this.tsb_GenerateReport.Text = "&Generate Report";
            this.tsb_GenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateReport.ToolTipText = "Generate Report";
            this.tsb_GenerateReport.Click += new System.EventHandler(this.tsb_GenerateReport_Click);
            // 
            // tsb_btnExportReport
            // 
            this.tsb_btnExportReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnExportReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnExportReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnExportReport.Image")));
            this.tsb_btnExportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnExportReport.Name = "tsb_btnExportReport";
            this.tsb_btnExportReport.Size = new System.Drawing.Size(61, 54);
            this.tsb_btnExportReport.Tag = "Export";
            this.tsb_btnExportReport.Text = "Export";
            this.tsb_btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnExportReport.Click += new System.EventHandler(this.tsb_btnExportReport_Click);
            // 
            // tsb_btnExportToTxt
            // 
            this.tsb_btnExportToTxt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnExportToTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnExportToTxt.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnExportToTxt.Image")));
            this.tsb_btnExportToTxt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnExportToTxt.Name = "tsb_btnExportToTxt";
            this.tsb_btnExportToTxt.Size = new System.Drawing.Size(112, 54);
            this.tsb_btnExportToTxt.Tag = "ExportToText";
            this.tsb_btnExportToTxt.Text = "ExportToText";
            this.tsb_btnExportToTxt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnExportToTxt.Click += new System.EventHandler(this.tsb_btnExportToTxt_Click);
            // 
            // tsb_btnGenerateBatchTxt
            // 
            this.tsb_btnGenerateBatchTxt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnGenerateBatchTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnGenerateBatchTxt.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnGenerateBatchTxt.Image")));
            this.tsb_btnGenerateBatchTxt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnGenerateBatchTxt.Name = "tsb_btnGenerateBatchTxt";
            this.tsb_btnGenerateBatchTxt.Size = new System.Drawing.Size(92, 54);
            this.tsb_btnGenerateBatchTxt.Tag = "Batch Text ";
            this.tsb_btnGenerateBatchTxt.Text = "Batch Text";
            this.tsb_btnGenerateBatchTxt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnGenerateBatchTxt.Click += new System.EventHandler(this.tsb_btnGenerateBatchTxt_Click);
            // 
            // tsb_btnGenerateBatch
            // 
            this.tsb_btnGenerateBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnGenerateBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnGenerateBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnGenerateBatch.Image")));
            this.tsb_btnGenerateBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnGenerateBatch.Name = "tsb_btnGenerateBatch";
            this.tsb_btnGenerateBatch.Size = new System.Drawing.Size(129, 54);
            this.tsb_btnGenerateBatch.Tag = "Generate Batch";
            this.tsb_btnGenerateBatch.Text = "Generate Batch";
            this.tsb_btnGenerateBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnGenerateBatch.Click += new System.EventHandler(this.tsb_btnGenerateBatch_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(50, 54);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_btnHideCriteria
            // 
            this.tsb_btnHideCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnHideCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnHideCriteria.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnHideCriteria.Image")));
            this.tsb_btnHideCriteria.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnHideCriteria.Name = "tsb_btnHideCriteria";
            this.tsb_btnHideCriteria.Size = new System.Drawing.Size(108, 54);
            this.tsb_btnHideCriteria.Tag = "ShowList";
            this.tsb_btnHideCriteria.Text = "&Hide Criteria";
            this.tsb_btnHideCriteria.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnHideCriteria.ToolTipText = "Hide Criteria";
            this.tsb_btnHideCriteria.Visible = false;
            this.tsb_btnHideCriteria.Click += new System.EventHandler(this.tsb_btnHideCriteria_Click);
            // 
            // tsb_btnCriteria
            // 
            this.tsb_btnCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnCriteria.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnCriteria.Image")));
            this.tsb_btnCriteria.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnCriteria.Name = "tsb_btnCriteria";
            this.tsb_btnCriteria.Size = new System.Drawing.Size(116, 54);
            this.tsb_btnCriteria.Tag = "ShowCriteria";
            this.tsb_btnCriteria.Text = "Show &Criteria";
            this.tsb_btnCriteria.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnCriteria.ToolTipText = "Show Criteria";
            this.tsb_btnCriteria.Click += new System.EventHandler(this.tsb_btnCriteria_Click);
            // 
            // tsb_btnShowList
            // 
            this.tsb_btnShowList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnShowList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnShowList.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnShowList.Image")));
            this.tsb_btnShowList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnShowList.Name = "tsb_btnShowList";
            this.tsb_btnShowList.Size = new System.Drawing.Size(86, 54);
            this.tsb_btnShowList.Tag = "ShowList";
            this.tsb_btnShowList.Text = "Show List";
            this.tsb_btnShowList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnShowList.ToolTipText = "Show List";
            this.tsb_btnShowList.Visible = false;
            this.tsb_btnShowList.Click += new System.EventHandler(this.tsb_btnShowList_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(53, 54);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(1283, 16);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(25, 30);
            this.groupBox4.TabIndex = 231;
            this.groupBox4.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlCriteria);
            this.panel2.Controls.Add(this.pnlPatientList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 66);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.panel2.Size = new System.Drawing.Size(1489, 204);
            this.panel2.TabIndex = 30;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.panel1);
            this.pnlCriteria.Controls.Add(this.pnlSelectSet);
            this.pnlCriteria.Controls.Add(this.label8);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCriteria.Location = new System.Drawing.Point(4, 0);
            this.pnlCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1481, 200);
            this.pnlCriteria.TabIndex = 233;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlPatientNameCriteria);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.cmbCPT);
            this.panel1.Controls.Add(this.btnClearCPT);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cmbInsurance);
            this.panel1.Controls.Add(this.txtZip);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbFacility);
            this.panel1.Controls.Add(this.btnBrowseCPT);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnBrowseInsurance);
            this.panel1.Controls.Add(this.pnlCDuedate);
            this.panel1.Controls.Add(this.btnClearInsurance);
            this.panel1.Controls.Add(this.cmbPaymentTray);
            this.panel1.Controls.Add(this.lblCPT);
            this.panel1.Controls.Add(this.dtCriteriaEndDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmbChargesTray);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbCriteriaTransactionDate);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.dtCriteriaStartDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1479, 162);
            this.panel1.TabIndex = 237;
            // 
            // pnlPatientNameCriteria
            // 
            this.pnlPatientNameCriteria.Controls.Add(this.grpPatientNameCriteria);
            this.pnlPatientNameCriteria.Location = new System.Drawing.Point(983, 65);
            this.pnlPatientNameCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPatientNameCriteria.Name = "pnlPatientNameCriteria";
            this.pnlPatientNameCriteria.Size = new System.Drawing.Size(361, 82);
            this.pnlPatientNameCriteria.TabIndex = 237;
            // 
            // grpPatientNameCriteria
            // 
            this.grpPatientNameCriteria.Controls.Add(this.cmbNameTo);
            this.grpPatientNameCriteria.Controls.Add(this.label40);
            this.grpPatientNameCriteria.Controls.Add(this.label39);
            this.grpPatientNameCriteria.Controls.Add(this.rbFirstName);
            this.grpPatientNameCriteria.Controls.Add(this.rbLastName);
            this.grpPatientNameCriteria.Controls.Add(this.radioButton3);
            this.grpPatientNameCriteria.Controls.Add(this.cmbNameFrom);
            this.grpPatientNameCriteria.Controls.Add(this.radioButton4);
            this.grpPatientNameCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPatientNameCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPatientNameCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpPatientNameCriteria.Location = new System.Drawing.Point(0, 0);
            this.grpPatientNameCriteria.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPatientNameCriteria.Name = "grpPatientNameCriteria";
            this.grpPatientNameCriteria.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPatientNameCriteria.Size = new System.Drawing.Size(361, 82);
            this.grpPatientNameCriteria.TabIndex = 231;
            this.grpPatientNameCriteria.TabStop = false;
            this.grpPatientNameCriteria.Text = "Patient Name ";
            // 
            // cmbNameTo
            // 
            this.cmbNameTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameTo.FormattingEnabled = true;
            this.cmbNameTo.Location = new System.Drawing.Point(193, 21);
            this.cmbNameTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbNameTo.Name = "cmbNameTo";
            this.cmbNameTo.Size = new System.Drawing.Size(59, 26);
            this.cmbNameTo.TabIndex = 0;
            this.cmbNameTo.SelectedIndexChanged += new System.EventHandler(this.cmbNameFrom_SelectedIndexChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(152, 26);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(35, 18);
            this.label40.TabIndex = 228;
            this.label40.Text = "To :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(16, 26);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(52, 18);
            this.label39.TabIndex = 228;
            this.label39.Text = "From :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // rbFirstName
            // 
            this.rbFirstName.AutoSize = true;
            this.rbFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFirstName.Location = new System.Drawing.Point(199, 53);
            this.rbFirstName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbFirstName.Name = "rbFirstName";
            this.rbFirstName.Size = new System.Drawing.Size(101, 22);
            this.rbFirstName.TabIndex = 1;
            this.rbFirstName.Text = "First Name";
            this.rbFirstName.UseVisualStyleBackColor = true;
            this.rbFirstName.CheckedChanged += new System.EventHandler(this.rbFirstName_CheckedChanged);
            // 
            // rbLastName
            // 
            this.rbLastName.AutoSize = true;
            this.rbLastName.Checked = true;
            this.rbLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLastName.Location = new System.Drawing.Point(76, 53);
            this.rbLastName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbLastName.Name = "rbLastName";
            this.rbLastName.Size = new System.Drawing.Size(109, 22);
            this.rbLastName.TabIndex = 1;
            this.rbLastName.TabStop = true;
            this.rbLastName.Text = "Last Name";
            this.rbLastName.UseVisualStyleBackColor = true;
            this.rbLastName.CheckedChanged += new System.EventHandler(this.rbLastName_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(463, 23);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 22);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "Year";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // cmbNameFrom
            // 
            this.cmbNameFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameFrom.FormattingEnabled = true;
            this.cmbNameFrom.Location = new System.Drawing.Point(72, 21);
            this.cmbNameFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbNameFrom.Name = "cmbNameFrom";
            this.cmbNameFrom.Size = new System.Drawing.Size(64, 26);
            this.cmbNameFrom.TabIndex = 0;
            this.cmbNameFrom.SelectedIndexChanged += new System.EventHandler(this.cmbNameFrom_SelectedIndexChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(369, 23);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(70, 22);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.Text = "Month";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(1139, 36);
            this.btnClearCPT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(28, 25);
            this.btnClearCPT.TabIndex = 213;
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
            this.btnBrowseCPT.Location = new System.Drawing.Point(1105, 36);
            this.btnBrowseCPT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(28, 25);
            this.btnBrowseCPT.TabIndex = 212;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(1105, 5);
            this.btnBrowseInsurance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(28, 25);
            this.btnBrowseInsurance.TabIndex = 215;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            // 
            // pnlCDuedate
            // 
            this.pnlCDuedate.Controls.Add(this.grpCDueDate);
            this.pnlCDuedate.Location = new System.Drawing.Point(416, 96);
            this.pnlCDuedate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlCDuedate.Name = "pnlCDuedate";
            this.pnlCDuedate.Size = new System.Drawing.Size(557, 63);
            this.pnlCDuedate.TabIndex = 236;
            // 
            // grpCDueDate
            // 
            this.grpCDueDate.Controls.Add(this.rdbCriteriaDays);
            this.grpCDueDate.Controls.Add(this.label24);
            this.grpCDueDate.Controls.Add(this.numCriteriaDuration);
            this.grpCDueDate.Controls.Add(this.rdbCriteriaWeek);
            this.grpCDueDate.Controls.Add(this.rdbCriteriaYear);
            this.grpCDueDate.Controls.Add(this.rdbCriteriaMonth);
            this.grpCDueDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpCDueDate.Location = new System.Drawing.Point(4, 1);
            this.grpCDueDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCDueDate.Name = "grpCDueDate";
            this.grpCDueDate.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCDueDate.Size = new System.Drawing.Size(541, 59);
            this.grpCDueDate.TabIndex = 230;
            this.grpCDueDate.TabStop = false;
            this.grpCDueDate.Text = "Due Date ";
            // 
            // rdbCriteriaDays
            // 
            this.rdbCriteriaDays.AutoSize = true;
            this.rdbCriteriaDays.Checked = true;
            this.rdbCriteriaDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCriteriaDays.Location = new System.Drawing.Point(195, 23);
            this.rdbCriteriaDays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCriteriaDays.Name = "rdbCriteriaDays";
            this.rdbCriteriaDays.Size = new System.Drawing.Size(66, 22);
            this.rdbCriteriaDays.TabIndex = 0;
            this.rdbCriteriaDays.TabStop = true;
            this.rdbCriteriaDays.Text = "Days";
            this.rdbCriteriaDays.UseVisualStyleBackColor = true;
            this.rdbCriteriaDays.CheckedChanged += new System.EventHandler(this.rdbCriteriaDays_CheckedChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(16, 26);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 18);
            this.label24.TabIndex = 228;
            this.label24.Text = "Due Date :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numCriteriaDuration
            // 
            this.numCriteriaDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCriteriaDuration.Location = new System.Drawing.Point(112, 21);
            this.numCriteriaDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numCriteriaDuration.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numCriteriaDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCriteriaDuration.Name = "numCriteriaDuration";
            this.numCriteriaDuration.Size = new System.Drawing.Size(69, 26);
            this.numCriteriaDuration.TabIndex = 229;
            this.numCriteriaDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rdbCriteriaWeek
            // 
            this.rdbCriteriaWeek.AutoSize = true;
            this.rdbCriteriaWeek.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCriteriaWeek.Location = new System.Drawing.Point(280, 23);
            this.rdbCriteriaWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCriteriaWeek.Name = "rdbCriteriaWeek";
            this.rdbCriteriaWeek.Size = new System.Drawing.Size(66, 22);
            this.rdbCriteriaWeek.TabIndex = 1;
            this.rdbCriteriaWeek.Text = "Week";
            this.rdbCriteriaWeek.UseVisualStyleBackColor = true;
            this.rdbCriteriaWeek.CheckedChanged += new System.EventHandler(this.rdbCriteriaWeek_CheckedChanged);
            // 
            // rdbCriteriaYear
            // 
            this.rdbCriteriaYear.AutoSize = true;
            this.rdbCriteriaYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCriteriaYear.Location = new System.Drawing.Point(463, 23);
            this.rdbCriteriaYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCriteriaYear.Name = "rdbCriteriaYear";
            this.rdbCriteriaYear.Size = new System.Drawing.Size(59, 22);
            this.rdbCriteriaYear.TabIndex = 3;
            this.rdbCriteriaYear.Text = "Year";
            this.rdbCriteriaYear.UseVisualStyleBackColor = true;
            this.rdbCriteriaYear.Visible = false;
            this.rdbCriteriaYear.CheckedChanged += new System.EventHandler(this.rdbCriteriaYear_CheckedChanged);
            // 
            // rdbCriteriaMonth
            // 
            this.rdbCriteriaMonth.AutoSize = true;
            this.rdbCriteriaMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCriteriaMonth.Location = new System.Drawing.Point(369, 23);
            this.rdbCriteriaMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCriteriaMonth.Name = "rdbCriteriaMonth";
            this.rdbCriteriaMonth.Size = new System.Drawing.Size(70, 22);
            this.rdbCriteriaMonth.TabIndex = 2;
            this.rdbCriteriaMonth.Text = "Month";
            this.rdbCriteriaMonth.UseVisualStyleBackColor = true;
            this.rdbCriteriaMonth.CheckedChanged += new System.EventHandler(this.rdbCriteriaMonth_CheckedChanged);
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(1139, 5);
            this.btnClearInsurance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(28, 25);
            this.btnClearInsurance.TabIndex = 216;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            // 
            // dtCriteriaEndDate
            // 
            this.dtCriteriaEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtCriteriaEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtCriteriaEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtCriteriaEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtCriteriaEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtCriteriaEndDate.Checked = false;
            this.dtCriteriaEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtCriteriaEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCriteriaEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCriteriaEndDate.Location = new System.Drawing.Point(177, 65);
            this.dtCriteriaEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtCriteriaEndDate.Name = "dtCriteriaEndDate";
            this.dtCriteriaEndDate.ShowCheckBox = true;
            this.dtCriteriaEndDate.Size = new System.Drawing.Size(143, 26);
            this.dtCriteriaEndDate.TabIndex = 233;
            this.dtCriteriaEndDate.ValueChanged += new System.EventHandler(this.dtCriteriaEndDate_ValueChanged);
            this.dtCriteriaEndDate.EnabledChanged += new System.EventHandler(this.dtCriteriaStartDate_EnabledChanged);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(28, 9);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(144, 17);
            this.label25.TabIndex = 234;
            this.label25.Text = "Transaction Date :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCriteriaTransactionDate
            // 
            this.cmbCriteriaTransactionDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteriaTransactionDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCriteriaTransactionDate.FormattingEnabled = true;
            this.cmbCriteriaTransactionDate.Location = new System.Drawing.Point(177, 4);
            this.cmbCriteriaTransactionDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCriteriaTransactionDate.Name = "cmbCriteriaTransactionDate";
            this.cmbCriteriaTransactionDate.Size = new System.Drawing.Size(143, 26);
            this.cmbCriteriaTransactionDate.TabIndex = 235;
            this.cmbCriteriaTransactionDate.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(76, 39);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(96, 17);
            this.label26.TabIndex = 230;
            this.label26.Text = "Start Date :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(84, 70);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(88, 17);
            this.label27.TabIndex = 232;
            this.label27.Text = "End Date :";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCriteriaStartDate
            // 
            this.dtCriteriaStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtCriteriaStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtCriteriaStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtCriteriaStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtCriteriaStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtCriteriaStartDate.Checked = false;
            this.dtCriteriaStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtCriteriaStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCriteriaStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCriteriaStartDate.Location = new System.Drawing.Point(177, 34);
            this.dtCriteriaStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtCriteriaStartDate.Name = "dtCriteriaStartDate";
            this.dtCriteriaStartDate.ShowCheckBox = true;
            this.dtCriteriaStartDate.Size = new System.Drawing.Size(143, 26);
            this.dtCriteriaStartDate.TabIndex = 231;
            this.dtCriteriaStartDate.ValueChanged += new System.EventHandler(this.dtCriteriaStartDate_ValueChanged);
            this.dtCriteriaStartDate.EnabledChanged += new System.EventHandler(this.dtCriteriaStartDate_EnabledChanged);
            // 
            // pnlSelectSet
            // 
            this.pnlSelectSet.Controls.Add(this.btnModifySettings);
            this.pnlSelectSet.Controls.Add(this.cmbSettings);
            this.pnlSelectSet.Controls.Add(this.btnSetupSettings);
            this.pnlSelectSet.Controls.Add(this.lblSelectSettings);
            this.pnlSelectSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectSet.Location = new System.Drawing.Point(1, 1);
            this.pnlSelectSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSelectSet.Name = "pnlSelectSet";
            this.pnlSelectSet.Size = new System.Drawing.Size(1479, 34);
            this.pnlSelectSet.TabIndex = 232;
            // 
            // btnModifySettings
            // 
            this.btnModifySettings.BackColor = System.Drawing.Color.Transparent;
            this.btnModifySettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifySettings.BackgroundImage")));
            this.btnModifySettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifySettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifySettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifySettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnModifySettings.Image = ((System.Drawing.Image)(resources.GetObject("btnModifySettings.Image")));
            this.btnModifySettings.Location = new System.Drawing.Point(499, 4);
            this.btnModifySettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModifySettings.Name = "btnModifySettings";
            this.btnModifySettings.Size = new System.Drawing.Size(28, 25);
            this.btnModifySettings.TabIndex = 259;
            this.btnModifySettings.UseVisualStyleBackColor = false;
            this.btnModifySettings.Click += new System.EventHandler(this.btnModifySettings_Click);
            // 
            // btnSetupSettings
            // 
            this.btnSetupSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSetupSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetupSettings.BackgroundImage")));
            this.btnSetupSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetupSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetupSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSetupSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSetupSettings.Image")));
            this.btnSetupSettings.Location = new System.Drawing.Point(467, 4);
            this.btnSetupSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSetupSettings.Name = "btnSetupSettings";
            this.btnSetupSettings.Size = new System.Drawing.Size(28, 25);
            this.btnSetupSettings.TabIndex = 258;
            this.btnSetupSettings.UseVisualStyleBackColor = false;
            this.btnSetupSettings.Click += new System.EventHandler(this.btnSetupSettings_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(1, 199);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1479, 1);
            this.label8.TabIndex = 236;
            this.label8.Text = "label8";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1479, 1);
            this.label2.TabIndex = 235;
            this.label2.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1480, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 200);
            this.label4.TabIndex = 234;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 200);
            this.label5.TabIndex = 233;
            this.label5.Text = "label5";
            // 
            // pnlPatientList
            // 
            this.pnlPatientList.Controls.Add(this.label10);
            this.pnlPatientList.Controls.Add(this.pnlDuedate);
            this.pnlPatientList.Controls.Add(this.label12);
            this.pnlPatientList.Controls.Add(this.pnlNoFiliterPatient);
            this.pnlPatientList.Controls.Add(this.dtpEndDate);
            this.pnlPatientList.Controls.Add(this.label13);
            this.pnlPatientList.Controls.Add(this.lbl_datefilter);
            this.pnlPatientList.Controls.Add(this.label14);
            this.pnlPatientList.Controls.Add(this.lblStartDate);
            this.pnlPatientList.Controls.Add(this.cmb_datefilter);
            this.pnlPatientList.Controls.Add(this.dtpStartDate);
            this.pnlPatientList.Controls.Add(this.lblEndDate);
            this.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientList.Location = new System.Drawing.Point(4, 0);
            this.pnlPatientList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPatientList.Name = "pnlPatientList";
            this.pnlPatientList.Size = new System.Drawing.Size(1481, 200);
            this.pnlPatientList.TabIndex = 235;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 199);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1479, 1);
            this.label10.TabIndex = 235;
            this.label10.Text = "label10";
            // 
            // pnlDuedate
            // 
            this.pnlDuedate.Controls.Add(this.gbSelectFollowupType);
            this.pnlDuedate.Location = new System.Drawing.Point(331, 41);
            this.pnlDuedate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDuedate.Name = "pnlDuedate";
            this.pnlDuedate.Size = new System.Drawing.Size(591, 92);
            this.pnlDuedate.TabIndex = 229;
            // 
            // gbSelectFollowupType
            // 
            this.gbSelectFollowupType.Controls.Add(this.rdbDays);
            this.gbSelectFollowupType.Controls.Add(this.label1);
            this.gbSelectFollowupType.Controls.Add(this.numDuration);
            this.gbSelectFollowupType.Controls.Add(this.rdbWeek);
            this.gbSelectFollowupType.Controls.Add(this.rdbYear);
            this.gbSelectFollowupType.Controls.Add(this.rdbMonth);
            this.gbSelectFollowupType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectFollowupType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbSelectFollowupType.Location = new System.Drawing.Point(4, 10);
            this.gbSelectFollowupType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSelectFollowupType.Name = "gbSelectFollowupType";
            this.gbSelectFollowupType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSelectFollowupType.Size = new System.Drawing.Size(539, 70);
            this.gbSelectFollowupType.TabIndex = 230;
            this.gbSelectFollowupType.TabStop = false;
            this.gbSelectFollowupType.Text = "Due Date Type";
            // 
            // rdbDays
            // 
            this.rdbDays.AutoSize = true;
            this.rdbDays.Checked = true;
            this.rdbDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDays.Location = new System.Drawing.Point(197, 30);
            this.rdbDays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbDays.Name = "rdbDays";
            this.rdbDays.Size = new System.Drawing.Size(66, 22);
            this.rdbDays.TabIndex = 0;
            this.rdbDays.TabStop = true;
            this.rdbDays.Text = "Days";
            this.rdbDays.UseVisualStyleBackColor = true;
            this.rdbDays.CheckedChanged += new System.EventHandler(this.rdbDays_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 228;
            this.label1.Text = "Due Date :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numDuration
            // 
            this.numDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDuration.Location = new System.Drawing.Point(112, 27);
            this.numDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numDuration.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(73, 26);
            this.numDuration.TabIndex = 229;
            this.numDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rdbWeek
            // 
            this.rdbWeek.AutoSize = true;
            this.rdbWeek.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbWeek.Location = new System.Drawing.Point(281, 30);
            this.rdbWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbWeek.Name = "rdbWeek";
            this.rdbWeek.Size = new System.Drawing.Size(66, 22);
            this.rdbWeek.TabIndex = 1;
            this.rdbWeek.Text = "Week";
            this.rdbWeek.UseVisualStyleBackColor = true;
            this.rdbWeek.CheckedChanged += new System.EventHandler(this.rdbWeek_CheckedChanged);
            // 
            // rdbYear
            // 
            this.rdbYear.AutoSize = true;
            this.rdbYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbYear.Location = new System.Drawing.Point(461, 30);
            this.rdbYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbYear.Name = "rdbYear";
            this.rdbYear.Size = new System.Drawing.Size(59, 22);
            this.rdbYear.TabIndex = 3;
            this.rdbYear.Text = "Year";
            this.rdbYear.UseVisualStyleBackColor = true;
            this.rdbYear.Visible = false;
            this.rdbYear.CheckedChanged += new System.EventHandler(this.rdbYear_CheckedChanged);
            // 
            // rdbMonth
            // 
            this.rdbMonth.AutoSize = true;
            this.rdbMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMonth.Location = new System.Drawing.Point(369, 30);
            this.rdbMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbMonth.Name = "rdbMonth";
            this.rdbMonth.Size = new System.Drawing.Size(70, 22);
            this.rdbMonth.TabIndex = 2;
            this.rdbMonth.Text = "Month";
            this.rdbMonth.UseVisualStyleBackColor = true;
            this.rdbMonth.CheckedChanged += new System.EventHandler(this.rdbMonth_CheckedChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1479, 1);
            this.label12.TabIndex = 234;
            this.label12.Text = "label12";
            // 
            // pnlNoFiliterPatient
            // 
            this.pnlNoFiliterPatient.Controls.Add(this.lblPatient);
            this.pnlNoFiliterPatient.Controls.Add(this.btnBrowsePatient);
            this.pnlNoFiliterPatient.Controls.Add(this.cmbPatients);
            this.pnlNoFiliterPatient.Controls.Add(this.btnClearPatient);
            this.pnlNoFiliterPatient.Controls.Add(this.grpPayType);
            this.pnlNoFiliterPatient.Location = new System.Drawing.Point(1, 2);
            this.pnlNoFiliterPatient.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlNoFiliterPatient.Name = "pnlNoFiliterPatient";
            this.pnlNoFiliterPatient.Size = new System.Drawing.Size(804, 37);
            this.pnlNoFiliterPatient.TabIndex = 218;
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(420, 5);
            this.btnBrowsePatient.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(29, 27);
            this.btnBrowsePatient.TabIndex = 194;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(455, 5);
            this.btnClearPatient.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(29, 27);
            this.btnClearPatient.TabIndex = 195;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(1480, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 200);
            this.label13.TabIndex = 233;
            this.label13.Text = "label13";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 200);
            this.label14.TabIndex = 232;
            this.label14.Text = "label14";
            // 
            // pnlFilteredPatList
            // 
            this.pnlFilteredPatList.Controls.Add(this.label15);
            this.pnlFilteredPatList.Controls.Add(this.c1PatientList);
            this.pnlFilteredPatList.Controls.Add(this.label16);
            this.pnlFilteredPatList.Controls.Add(this.label17);
            this.pnlFilteredPatList.Controls.Add(this.label18);
            this.pnlFilteredPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilteredPatList.Location = new System.Drawing.Point(0, 300);
            this.pnlFilteredPatList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFilteredPatList.Name = "pnlFilteredPatList";
            this.pnlFilteredPatList.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.pnlFilteredPatList.Size = new System.Drawing.Size(1489, 629);
            this.pnlFilteredPatList.TabIndex = 218;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(5, 624);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1479, 1);
            this.label15.TabIndex = 32;
            this.label15.Text = "label15";
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PatientList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1PatientList.Location = new System.Drawing.Point(5, 1);
            this.c1PatientList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c1PatientList.Rows.Count = 1;
            this.c1PatientList.Rows.DefaultSize = 19;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PatientList.ShowCellLabels = true;
            this.c1PatientList.Size = new System.Drawing.Size(1479, 624);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 28;
            this.c1PatientList.Tag = "ClosePeriod";
            this.c1PatientList.DoubleClick += new System.EventHandler(this.c1PatientList_DoubleClick);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(5, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1479, 1);
            this.label16.TabIndex = 31;
            this.label16.Text = "label16";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(1484, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 625);
            this.label17.TabIndex = 30;
            this.label17.Text = "label17";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(4, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 625);
            this.label18.TabIndex = 29;
            this.label18.Text = "label18";
            // 
            // pnlc1PatientListHeader
            // 
            this.pnlc1PatientListHeader.Controls.Add(this.pnlc1PatientList);
            this.pnlc1PatientListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlc1PatientListHeader.Location = new System.Drawing.Point(0, 270);
            this.pnlc1PatientListHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlc1PatientListHeader.Name = "pnlc1PatientListHeader";
            this.pnlc1PatientListHeader.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.pnlc1PatientListHeader.Size = new System.Drawing.Size(1489, 30);
            this.pnlc1PatientListHeader.TabIndex = 83;
            // 
            // pnlc1PatientList
            // 
            this.pnlc1PatientList.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.pnlc1PatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlc1PatientList.Controls.Add(this.label31);
            this.pnlc1PatientList.Controls.Add(this.btnDown);
            this.pnlc1PatientList.Controls.Add(this.btnUp);
            this.pnlc1PatientList.Controls.Add(this.label30);
            this.pnlc1PatientList.Controls.Add(this.label29);
            this.pnlc1PatientList.Controls.Add(this.label28);
            this.pnlc1PatientList.Controls.Add(this.label23);
            this.pnlc1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1PatientList.Location = new System.Drawing.Point(4, 0);
            this.pnlc1PatientList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlc1PatientList.Name = "pnlc1PatientList";
            this.pnlc1PatientList.Size = new System.Drawing.Size(1481, 26);
            this.pnlc1PatientList.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(1, 1);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label31.Size = new System.Drawing.Size(151, 20);
            this.label31.TabIndex = 236;
            this.label31.Text = "  Filtered Patients :";
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(1422, 1);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(29, 24);
            this.btnDown.TabIndex = 217;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(1451, 1);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(29, 24);
            this.btnUp.TabIndex = 218;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseLeave += new System.EventHandler(this.btnUp_MouseLeave);
            this.btnUp.MouseHover += new System.EventHandler(this.btnUp_MouseHover);
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(1480, 1);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 24);
            this.label30.TabIndex = 235;
            this.label30.Text = "label30";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 24);
            this.label29.TabIndex = 234;
            this.label29.Text = "label29";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1481, 1);
            this.label28.TabIndex = 219;
            this.label28.Text = "label2";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(0, 25);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1481, 1);
            this.label23.TabIndex = 12;
            this.label23.Text = "label2";
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.pnlProgressBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProgressBar.Controls.Add(this.panel3);
            this.pnlProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProgressBar.Location = new System.Drawing.Point(0, 929);
            this.pnlProgressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.pnlProgressBar.Size = new System.Drawing.Size(1489, 31);
            this.pnlProgressBar.TabIndex = 258;
            this.pnlProgressBar.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lblFile);
            this.panel3.Controls.Add(this.lblFileCounter);
            this.panel3.Controls.Add(this.prgFileGeneration);
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.label32);
            this.panel3.Controls.Add(this.label37);
            this.panel3.Controls.Add(this.label33);
            this.panel3.Controls.Add(this.label34);
            this.panel3.Controls.Add(this.label36);
            this.panel3.Controls.Add(this.label35);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1481, 27);
            this.panel3.TabIndex = 32;
            // 
            // lblFile
            // 
            this.lblFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFile.Location = new System.Drawing.Point(1, 3);
            this.lblFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(248, 21);
            this.lblFile.TabIndex = 1;
            this.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileCounter
            // 
            this.lblFileCounter.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblFileCounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileCounter.Location = new System.Drawing.Point(134, 3);
            this.lblFileCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileCounter.Name = "lblFileCounter";
            this.lblFileCounter.Size = new System.Drawing.Size(156, 21);
            this.lblFileCounter.TabIndex = 24;
            this.lblFileCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgFileGeneration
            // 
            this.prgFileGeneration.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgFileGeneration.Location = new System.Drawing.Point(290, 3);
            this.prgFileGeneration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prgFileGeneration.Name = "prgFileGeneration";
            this.prgFileGeneration.Size = new System.Drawing.Size(1187, 21);
            this.prgFileGeneration.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgFileGeneration.TabIndex = 0;
            // 
            // label38
            // 
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label38.Location = new System.Drawing.Point(1477, 3);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(3, 21);
            this.label38.TabIndex = 31;
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(1, 1);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label32.Size = new System.Drawing.Size(1479, 2);
            this.label32.TabIndex = 23;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(1, 0);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1479, 1);
            this.label37.TabIndex = 30;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Location = new System.Drawing.Point(1, 24);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label33.Size = new System.Drawing.Size(1479, 2);
            this.label33.TabIndex = 29;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 26);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1479, 1);
            this.label34.TabIndex = 26;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 27);
            this.label36.TabIndex = 27;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Location = new System.Drawing.Point(1480, 0);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 27);
            this.label35.TabIndex = 28;
            // 
            // pnlPleasewait
            // 
            this.pnlPleasewait.Controls.Add(this.label45);
            this.pnlPleasewait.Controls.Add(this.label44);
            this.pnlPleasewait.Controls.Add(this.label43);
            this.pnlPleasewait.Controls.Add(this.label42);
            this.pnlPleasewait.Controls.Add(this.label41);
            this.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPleasewait.Location = new System.Drawing.Point(0, 300);
            this.pnlPleasewait.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlPleasewait.Name = "pnlPleasewait";
            this.pnlPleasewait.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.pnlPleasewait.Size = new System.Drawing.Size(1489, 629);
            this.pnlPleasewait.TabIndex = 33;
            this.pnlPleasewait.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.White;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Baskerville Old Face", 48F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(5, 1);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1479, 623);
            this.label45.TabIndex = 238;
            this.label45.Text = "Please Wait...";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(4, 1);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 623);
            this.label44.TabIndex = 36;
            this.label44.Text = "label44";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(1484, 1);
            this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 623);
            this.label43.TabIndex = 35;
            this.label43.Text = "label43";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(4, 0);
            this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1481, 1);
            this.label42.TabIndex = 34;
            this.label42.Text = "label42";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(4, 624);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1481, 1);
            this.label41.TabIndex = 33;
            this.label41.Text = "label41";
            // 
            // frmRpt_PatientStatementForGateWayEDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1489, 960);
            this.Controls.Add(this.pnlPleasewait);
            this.Controls.Add(this.pnlFilteredPatList);
            this.Controls.Add(this.pnlcrvReportViewer);
            this.Controls.Add(this.pnlc1PatientListHeader);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.pnlProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmRpt_PatientStatementForGateWayEDI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_PatientStatementForGateWayEDI_FormClosed);
            this.Load += new System.EventHandler(this.frmRpt_PatientStatementForGateWayEDI_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRpt_PatientStatementForGateWayEDI_Paint);
            this.pnlcrvReportViewer.ResumeLayout(false);
            this.grpPayType.ResumeLayout(false);
            this.grpPayType.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlCriteria.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlPatientNameCriteria.ResumeLayout(false);
            this.grpPatientNameCriteria.ResumeLayout(false);
            this.grpPatientNameCriteria.PerformLayout();
            this.pnlCDuedate.ResumeLayout(false);
            this.grpCDueDate.ResumeLayout(false);
            this.grpCDueDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCriteriaDuration)).EndInit();
            this.pnlSelectSet.ResumeLayout(false);
            this.pnlPatientList.ResumeLayout(false);
            this.pnlDuedate.ResumeLayout(false);
            this.gbSelectFollowupType.ResumeLayout(false);
            this.gbSelectFollowupType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.pnlNoFiliterPatient.ResumeLayout(false);
            this.pnlFilteredPatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.pnlc1PatientListHeader.ResumeLayout(false);
            this.pnlc1PatientList.ResumeLayout(false);
            this.pnlc1PatientList.PerformLayout();
            this.pnlProgressBar.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlPleasewait.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlcrvReportViewer;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReportViewer;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        internal System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Label lblCPT;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        private System.Windows.Forms.GroupBox grpPayType;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbAllowed;
        private System.Windows.Forms.RadioButton rbCharges;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        private System.Windows.Forms.ToolStripButton tsb_btnGenerateBatch;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDueAmt;
        private System.Windows.Forms.RadioButton rbGreater;
        private System.Windows.Forms.RadioButton rbLess;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtZip;
        internal System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ComboBox cmbPaymentTray;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox cmbChargesTray;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        internal System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCriteria;
        private System.Windows.Forms.RadioButton rbNoCriteria;
        internal System.Windows.Forms.ComboBox cmbSettings;
        internal System.Windows.Forms.Label lblSelectSettings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlSelectSet;
        private System.Windows.Forms.Button btnSetupSettings;
        private System.Windows.Forms.Button btnModifySettings;
        private System.Windows.Forms.Panel pnlFilteredPatList;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        private System.Windows.Forms.Panel pnlNoFiliterPatient;
        private System.Windows.Forms.ToolStripButton tsb_btnExportToTxt;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsb_btnGenerateBatchTxt;
        private System.Windows.Forms.Panel pnlPatientList;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Panel pnlc1PatientListHeader;
        private System.Windows.Forms.Panel pnlc1PatientList;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlDuedate;
        private System.Windows.Forms.DateTimePicker dtCriteriaEndDate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dtCriteriaStartDate;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbCriteriaTransactionDate;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.ToolStripButton tsb_btnHideCriteria;
        internal System.Windows.Forms.ToolStripButton tsb_btnCriteria;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.GroupBox gbSelectFollowupType;
        private System.Windows.Forms.RadioButton rdbDays;
        private System.Windows.Forms.RadioButton rdbWeek;
        private System.Windows.Forms.RadioButton rdbYear;
        private System.Windows.Forms.RadioButton rdbMonth;
        private System.Windows.Forms.Panel pnlCDuedate;
        private System.Windows.Forms.GroupBox grpCDueDate;
        private System.Windows.Forms.RadioButton rdbCriteriaDays;
        private System.Windows.Forms.RadioButton rdbCriteriaWeek;
        private System.Windows.Forms.RadioButton rdbCriteriaYear;
        private System.Windows.Forms.RadioButton rdbCriteriaMonth;
        private System.Windows.Forms.NumericUpDown numCriteriaDuration;
        internal System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripButton tsb_btnShowList;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lblFileCounter;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ProgressBar prgFileGeneration;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlPatientNameCriteria;
        private System.Windows.Forms.ComboBox cmbNameTo;
        private System.Windows.Forms.ComboBox cmbNameFrom;
        private System.Windows.Forms.GroupBox grpPatientNameCriteria;
        internal System.Windows.Forms.Label label40;
        internal System.Windows.Forms.Label label39;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton rbFirstName;
        private System.Windows.Forms.RadioButton rbLastName;
        private System.Windows.Forms.Panel pnlPleasewait;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
    }
}