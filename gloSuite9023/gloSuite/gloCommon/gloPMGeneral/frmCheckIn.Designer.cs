namespace gloPMGeneral
{
    partial class frmCheckIn
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpCheckInDate };
            System.Windows.Forms.Control[] cntControls = { dtpCheckInDate };
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckIn));
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnPriorAuthorization = new System.Windows.Forms.ToolStripButton();
            this.ts_btnViewBenefit = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnPatientConsent = new System.Windows.Forms.ToolStripButton();
            this.ts_btnAccountOnFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pnlDateTime = new System.Windows.Forms.Panel();
            this.lblCopayAlert = new System.Windows.Forms.Label();
            this.lblGlobalPeriodAlert = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpCheckInDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlInsuranceList = new System.Windows.Forms.Panel();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkExcludeInactiveInsurance = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlTemplate = new System.Windows.Forms.Panel();
            this.c1Templates = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PnlPatientStrip = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblPatientAge = new System.Windows.Forms.Label();
            this.lblPatientDOB = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblProviderName = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label52 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btn_ModityPatient = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tmrCopayAlertBlink = new System.Windows.Forms.Timer(this.components);
            this.pnlAccountOnFile = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.lblConsentEsign = new System.Windows.Forms.Label();
            this.lnklblPatientConsent = new System.Windows.Forms.LinkLabel();
            this.label46 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblConsentSMS = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblConsentEmail = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lblAOFUnsuspendDate = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblAOFEventCountMax = new System.Windows.Forms.Label();
            this.lblAOFEffectiveDate = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblAOFAgreementDurationMax = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblAOFPerEventAmountMax = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblAOFAgreementStatus = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnRefreshStatus = new System.Windows.Forms.Button();
            this.lblAOFAlert = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.tmrAOFAlertBlink = new System.Windows.Forms.Timer(this.components);
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.pnlDateTime.SuspendLayout();
            this.pnlInsuranceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Templates)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PnlPatientStrip.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlAccountOnFile.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel7.SuspendLayout();
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
            this.pnlTopToolStrip.Size = new System.Drawing.Size(832, 56);
            this.pnlTopToolStrip.TabIndex = 18;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnPriorAuthorization,
            this.ts_btnViewBenefit,
            this.ts_btnSave,
            this.ts_btnPatientConsent,
            this.ts_btnAccountOnFile,
            this.toolStripButton1});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(832, 53);
            this.TopToolStrip.TabIndex = 0;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnPriorAuthorization
            // 
            this.ts_btnPriorAuthorization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnPriorAuthorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnPriorAuthorization.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPriorAuthorization.Image")));
            this.ts_btnPriorAuthorization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPriorAuthorization.Name = "ts_btnPriorAuthorization";
            this.ts_btnPriorAuthorization.Size = new System.Drawing.Size(129, 50);
            this.ts_btnPriorAuthorization.Tag = "PriorAuthorization";
            this.ts_btnPriorAuthorization.Text = "&Prior Authorization";
            this.ts_btnPriorAuthorization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPriorAuthorization.Visible = false;
            this.ts_btnPriorAuthorization.Click += new System.EventHandler(this.ts_btnPriorAuthorization_Click);
            // 
            // ts_btnViewBenefit
            // 
            this.ts_btnViewBenefit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnViewBenefit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnViewBenefit.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnViewBenefit.Image")));
            this.ts_btnViewBenefit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnViewBenefit.Name = "ts_btnViewBenefit";
            this.ts_btnViewBenefit.Size = new System.Drawing.Size(94, 50);
            this.ts_btnViewBenefit.Tag = "View Benefit";
            this.ts_btnViewBenefit.Text = "&View Benefits";
            this.ts_btnViewBenefit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnViewBenefit.Click += new System.EventHandler(this.ts_btnViewBenefit_Click);
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
            this.ts_btnSave.Text = "&Save&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
            // 
            // ts_btnPatientConsent
            // 
            this.ts_btnPatientConsent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnPatientConsent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnPatientConsent.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPatientConsent.Image")));
            this.ts_btnPatientConsent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPatientConsent.Name = "ts_btnPatientConsent";
            this.ts_btnPatientConsent.Size = new System.Drawing.Size(111, 50);
            this.ts_btnPatientConsent.Tag = "AccountOnFile";
            this.ts_btnPatientConsent.Text = "Patient C&oncent";
            this.ts_btnPatientConsent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPatientConsent.Click += new System.EventHandler(this.ts_btnPatientConsent_Click);
            // 
            // ts_btnAccountOnFile
            // 
            this.ts_btnAccountOnFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAccountOnFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnAccountOnFile.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAccountOnFile.Image")));
            this.ts_btnAccountOnFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAccountOnFile.Name = "ts_btnAccountOnFile";
            this.ts_btnAccountOnFile.Size = new System.Drawing.Size(106, 50);
            this.ts_btnAccountOnFile.Tag = "AccountOnFile";
            this.ts_btnAccountOnFile.Text = "&Account On File";
            this.ts_btnAccountOnFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnAccountOnFile.Click += new System.EventHandler(this.ts_btnAccountOnFile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton1.Tag = "Close";
            this.toolStripButton1.Text = "&Close";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // pnlDateTime
            // 
            this.pnlDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlDateTime.Controls.Add(this.lblCopayAlert);
            this.pnlDateTime.Controls.Add(this.lblGlobalPeriodAlert);
            this.pnlDateTime.Controls.Add(this.label17);
            this.pnlDateTime.Controls.Add(this.dtpCheckInDate);
            this.pnlDateTime.Controls.Add(this.label1);
            this.pnlDateTime.Controls.Add(this.label2);
            this.pnlDateTime.Controls.Add(this.label3);
            this.pnlDateTime.Controls.Add(this.label4);
            this.pnlDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDateTime.Location = new System.Drawing.Point(0, 294);
            this.pnlDateTime.Name = "pnlDateTime";
            this.pnlDateTime.Padding = new System.Windows.Forms.Padding(3);
            this.pnlDateTime.Size = new System.Drawing.Size(832, 36);
            this.pnlDateTime.TabIndex = 20;
            // 
            // lblCopayAlert
            // 
            this.lblCopayAlert.AutoSize = true;
            this.lblCopayAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopayAlert.ForeColor = System.Drawing.Color.Black;
            this.lblCopayAlert.Location = new System.Drawing.Point(378, 11);
            this.lblCopayAlert.Name = "lblCopayAlert";
            this.lblCopayAlert.Size = new System.Drawing.Size(45, 14);
            this.lblCopayAlert.TabIndex = 220;
            this.lblCopayAlert.Text = "label24";
            this.lblCopayAlert.Visible = false;
            // 
            // lblGlobalPeriodAlert
            // 
            this.lblGlobalPeriodAlert.AutoSize = true;
            this.lblGlobalPeriodAlert.BackColor = System.Drawing.Color.Transparent;
            this.lblGlobalPeriodAlert.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGlobalPeriodAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGlobalPeriodAlert.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGlobalPeriodAlert.Location = new System.Drawing.Point(690, 4);
            this.lblGlobalPeriodAlert.Name = "lblGlobalPeriodAlert";
            this.lblGlobalPeriodAlert.Padding = new System.Windows.Forms.Padding(5, 8, 0, 0);
            this.lblGlobalPeriodAlert.Size = new System.Drawing.Size(138, 22);
            this.lblGlobalPeriodAlert.TabIndex = 218;
            this.lblGlobalPeriodAlert.Text = "Global Period Alert...";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(39, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 14);
            this.label17.TabIndex = 7;
            this.label17.Text = "Date/Time :";
            // 
            // dtpCheckInDate
            // 
            this.dtpCheckInDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpCheckInDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpCheckInDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpCheckInDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpCheckInDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpCheckInDate.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dtpCheckInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckInDate.Location = new System.Drawing.Point(116, 7);
            this.dtpCheckInDate.Name = "dtpCheckInDate";
            this.dtpCheckInDate.Size = new System.Drawing.Size(179, 22);
            this.dtpCheckInDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(824, 1);
            this.label1.TabIndex = 4;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(828, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(826, 1);
            this.label4.TabIndex = 0;
            this.label4.Text = "label1";
            // 
            // pnlInsuranceList
            // 
            this.pnlInsuranceList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlInsuranceList.Controls.Add(this.c1PatientDetails);
            this.pnlInsuranceList.Controls.Add(this.panel3);
            this.pnlInsuranceList.Controls.Add(this.label7);
            this.pnlInsuranceList.Controls.Add(this.label8);
            this.pnlInsuranceList.Controls.Add(this.label9);
            this.pnlInsuranceList.Controls.Add(this.label10);
            this.pnlInsuranceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInsuranceList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlInsuranceList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlInsuranceList.Location = new System.Drawing.Point(0, 330);
            this.pnlInsuranceList.Name = "pnlInsuranceList";
            this.pnlInsuranceList.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlInsuranceList.Size = new System.Drawing.Size(832, 162);
            this.pnlInsuranceList.TabIndex = 21;
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PatientDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientDetails.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.ExtendLastCol = true;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PatientDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientDetails.Location = new System.Drawing.Point(4, 24);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 1;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.Size = new System.Drawing.Size(824, 137);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 14;
            this.c1PatientDetails.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_CellChanged);
            this.c1PatientDetails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseMove);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.chkExcludeInactiveInsurance);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 23);
            this.panel3.TabIndex = 13;
            // 
            // chkExcludeInactiveInsurance
            // 
            this.chkExcludeInactiveInsurance.AutoSize = true;
            this.chkExcludeInactiveInsurance.Checked = true;
            this.chkExcludeInactiveInsurance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeInactiveInsurance.Location = new System.Drawing.Point(649, 2);
            this.chkExcludeInactiveInsurance.Name = "chkExcludeInactiveInsurance";
            this.chkExcludeInactiveInsurance.Size = new System.Drawing.Size(172, 18);
            this.chkExcludeInactiveInsurance.TabIndex = 17;
            this.chkExcludeInactiveInsurance.Text = "Exclude Inactive Insurance";
            this.chkExcludeInactiveInsurance.UseVisualStyleBackColor = true;
            this.chkExcludeInactiveInsurance.CheckedChanged += new System.EventHandler(this.chkExcludeInactiveInsurance_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(824, 1);
            this.label5.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(824, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "  Eligibility Verification";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(4, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(824, 1);
            this.label7.TabIndex = 4;
            this.label7.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 161);
            this.label8.TabIndex = 3;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(828, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 161);
            this.label9.TabIndex = 2;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(826, 1);
            this.label10.TabIndex = 0;
            this.label10.Text = "label1";
            // 
            // pnlTemplate
            // 
            this.pnlTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTemplate.Controls.Add(this.c1Templates);
            this.pnlTemplate.Controls.Add(this.panel5);
            this.pnlTemplate.Controls.Add(this.label13);
            this.pnlTemplate.Controls.Add(this.label14);
            this.pnlTemplate.Controls.Add(this.label15);
            this.pnlTemplate.Controls.Add(this.label16);
            this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTemplate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTemplate.Location = new System.Drawing.Point(0, 495);
            this.pnlTemplate.Name = "pnlTemplate";
            this.pnlTemplate.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTemplate.Size = new System.Drawing.Size(832, 270);
            this.pnlTemplate.TabIndex = 22;
            // 
            // c1Templates
            // 
            this.c1Templates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Templates.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Templates.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Templates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Templates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Templates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Templates.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Templates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Templates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Templates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Templates.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Templates.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Templates.Location = new System.Drawing.Point(4, 24);
            this.c1Templates.Name = "c1Templates";
            this.c1Templates.Rows.Count = 1;
            this.c1Templates.Rows.DefaultSize = 19;
            this.c1Templates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Templates.Size = new System.Drawing.Size(824, 242);
            this.c1Templates.StyleInfo = resources.GetString("c1Templates.StyleInfo");
            this.c1Templates.TabIndex = 14;
            this.c1Templates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Templates_MouseMove);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel5.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(824, 23);
            this.panel5.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(824, 1);
            this.label11.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(824, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "  Template ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(4, 266);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(824, 1);
            this.label13.TabIndex = 4;
            this.label13.Text = "label2";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 266);
            this.label14.TabIndex = 3;
            this.label14.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(828, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 266);
            this.label15.TabIndex = 2;
            this.label15.Text = "label15";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(826, 1);
            this.label16.TabIndex = 0;
            this.label16.Text = "label1";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel6.Controls.Add(this.panel1);
            this.panel6.Controls.Add(this.lblNotes);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Controls.Add(this.label31);
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel6.Location = new System.Drawing.Point(0, 800);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.panel6.Size = new System.Drawing.Size(886, 73);
            this.panel6.TabIndex = 25;
            this.panel6.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNotes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(139, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(743, 67);
            this.panel1.TabIndex = 9;
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(5, 5);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(733, 57);
            this.txtNotes.TabIndex = 8;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(4, 2);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(58, 14);
            this.lblNotes.TabIndex = 7;
            this.lblNotes.Text = " Notes : ";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label30.Location = new System.Drawing.Point(4, 69);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(878, 1);
            this.label30.TabIndex = 4;
            this.label30.Text = "label2";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(3, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 68);
            this.label31.TabIndex = 3;
            this.label31.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label32.Location = new System.Drawing.Point(882, 2);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 68);
            this.label32.TabIndex = 2;
            this.label32.Text = "label32";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(3, 1);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(880, 1);
            this.label33.TabIndex = 0;
            this.label33.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 492);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(832, 3);
            this.splitter1.TabIndex = 104;
            this.splitter1.TabStop = false;
            // 
            // PnlPatientStrip
            // 
            this.PnlPatientStrip.BackColor = System.Drawing.Color.Transparent;
            this.PnlPatientStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PnlPatientStrip.Controls.Add(this.panel4);
            this.PnlPatientStrip.Controls.Add(this.pnlTop);
            this.PnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlPatientStrip.Location = new System.Drawing.Point(0, 56);
            this.PnlPatientStrip.Name = "PnlPatientStrip";
            this.PnlPatientStrip.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.PnlPatientStrip.Size = new System.Drawing.Size(832, 106);
            this.PnlPatientStrip.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label36);
            this.panel4.Controls.Add(this.label28);
            this.panel4.Controls.Add(this.label35);
            this.panel4.Controls.Add(this.lblToday);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.lblPatientAge);
            this.panel4.Controls.Add(this.lblPatientDOB);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.lblProviderName);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.lblPatientName);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.lblPatientCode);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(826, 76);
            this.panel4.TabIndex = 21;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label36.Location = new System.Drawing.Point(1, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(824, 1);
            this.label36.TabIndex = 7;
            this.label36.Text = "label2";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(51, 50);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(59, 14);
            this.label28.TabIndex = 20;
            this.label28.Text = "Provider :";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label35.Location = new System.Drawing.Point(1, 75);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(824, 1);
            this.label35.TabIndex = 6;
            this.label35.Text = "label2";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(505, 50);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(43, 14);
            this.lblToday.TabIndex = 19;
            this.lblToday.Text = "10 Jan";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Right;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(825, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 76);
            this.label34.TabIndex = 5;
            this.label34.Text = "label4";
            // 
            // lblPatientAge
            // 
            this.lblPatientAge.AutoSize = true;
            this.lblPatientAge.Location = new System.Drawing.Point(505, 29);
            this.lblPatientAge.Name = "lblPatientAge";
            this.lblPatientAge.Size = new System.Drawing.Size(55, 14);
            this.lblPatientAge.TabIndex = 18;
            this.lblPatientAge.Text = "10 Years";
            // 
            // lblPatientDOB
            // 
            this.lblPatientDOB.AutoSize = true;
            this.lblPatientDOB.Location = new System.Drawing.Point(505, 8);
            this.lblPatientDOB.Name = "lblPatientDOB";
            this.lblPatientDOB.Size = new System.Drawing.Size(73, 14);
            this.lblPatientDOB.TabIndex = 17;
            this.lblPatientDOB.Text = "10/25/1993";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 76);
            this.label29.TabIndex = 4;
            this.label29.Text = "label4";
            // 
            // lblProviderName
            // 
            this.lblProviderName.AutoSize = true;
            this.lblProviderName.Location = new System.Drawing.Point(112, 50);
            this.lblProviderName.Name = "lblProviderName";
            this.lblProviderName.Size = new System.Drawing.Size(30, 14);
            this.lblProviderName.TabIndex = 16;
            this.lblProviderName.Text = "Alex";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(67, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 14);
            this.label22.TabIndex = 8;
            this.label22.Text = "Code :";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(112, 29);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(75, 14);
            this.lblPatientName.TabIndex = 15;
            this.lblPatientName.Text = "Micky Mouse";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(64, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 14);
            this.label23.TabIndex = 9;
            this.label23.Text = "Name :";
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.AutoSize = true;
            this.lblPatientCode.Location = new System.Drawing.Point(112, 8);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(49, 14);
            this.lblPatientCode.TabIndex = 14;
            this.lblPatientCode.Text = "464564";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(464, 8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 14);
            this.label27.TabIndex = 11;
            this.label27.Text = "DOB :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(462, 50);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 14);
            this.label25.TabIndex = 13;
            this.label25.Text = "Date :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(466, 29);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 14);
            this.label26.TabIndex = 12;
            this.label26.Text = "Age :";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.panel2);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlTop.Size = new System.Drawing.Size(826, 27);
            this.pnlTop.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label52);
            this.panel2.Controls.Add(this.pnlButton);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 24);
            this.panel2.TabIndex = 61;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 1);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 22);
            this.label52.TabIndex = 22;
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlButton.Controls.Add(this.btn_ModityPatient);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.ForeColor = System.Drawing.Color.Black;
            this.pnlButton.Location = new System.Drawing.Point(799, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(26, 22);
            this.pnlButton.TabIndex = 2;
            // 
            // btn_ModityPatient
            // 
            this.btn_ModityPatient.BackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.BackgroundImage = global::gloPMGeneral.Properties.Resources.Patient;
            this.btn_ModityPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ModityPatient.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ModityPatient.FlatAppearance.BorderSize = 0;
            this.btn_ModityPatient.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ModityPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ModityPatient.Location = new System.Drawing.Point(3, 0);
            this.btn_ModityPatient.Name = "btn_ModityPatient";
            this.btn_ModityPatient.Size = new System.Drawing.Size(23, 22);
            this.btn_ModityPatient.TabIndex = 57;
            this.btn_ModityPatient.UseVisualStyleBackColor = false;
            this.btn_ModityPatient.Click += new System.EventHandler(this.btn_ModityPatient_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label18.Location = new System.Drawing.Point(9, 1);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label18.Size = new System.Drawing.Size(101, 18);
            this.label18.TabIndex = 20;
            this.label18.Text = " Patient Details";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Location = new System.Drawing.Point(825, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 22);
            this.label19.TabIndex = 23;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(826, 1);
            this.label20.TabIndex = 24;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(0, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(826, 1);
            this.label21.TabIndex = 58;
            // 
            // tmrCopayAlertBlink
            // 
            this.tmrCopayAlertBlink.Interval = 500;
            this.tmrCopayAlertBlink.Tick += new System.EventHandler(this.tmrCopayAlertBlink_Tick);
            // 
            // pnlAccountOnFile
            // 
            this.pnlAccountOnFile.BackColor = System.Drawing.Color.Transparent;
            this.pnlAccountOnFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAccountOnFile.Controls.Add(this.panel8);
            this.pnlAccountOnFile.Controls.Add(this.panel9);
            this.pnlAccountOnFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAccountOnFile.Location = new System.Drawing.Point(0, 162);
            this.pnlAccountOnFile.Name = "pnlAccountOnFile";
            this.pnlAccountOnFile.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlAccountOnFile.Size = new System.Drawing.Size(832, 132);
            this.pnlAccountOnFile.TabIndex = 22;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.lblAOFUnsuspendDate);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Controls.Add(this.label37);
            this.panel8.Controls.Add(this.label38);
            this.panel8.Controls.Add(this.label40);
            this.panel8.Controls.Add(this.lblAOFEventCountMax);
            this.panel8.Controls.Add(this.lblAOFEffectiveDate);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Controls.Add(this.lblAOFAgreementDurationMax);
            this.panel8.Controls.Add(this.label45);
            this.panel8.Controls.Add(this.lblAOFPerEventAmountMax);
            this.panel8.Controls.Add(this.label47);
            this.panel8.Controls.Add(this.lblAOFAgreementStatus);
            this.panel8.Controls.Add(this.label49);
            this.panel8.Controls.Add(this.label51);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 30);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(826, 102);
            this.panel8.TabIndex = 21;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.AliceBlue;
            this.panel11.Controls.Add(this.label48);
            this.panel11.Controls.Add(this.lblConsentEsign);
            this.panel11.Controls.Add(this.lnklblPatientConsent);
            this.panel11.Controls.Add(this.label46);
            this.panel11.Controls.Add(this.label44);
            this.panel11.Controls.Add(this.label41);
            this.panel11.Controls.Add(this.lblConsentSMS);
            this.panel11.Controls.Add(this.label42);
            this.panel11.Controls.Add(this.lblConsentEmail);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(1, 77);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(824, 24);
            this.panel11.TabIndex = 29;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label48.Location = new System.Drawing.Point(563, 5);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(47, 14);
            this.label48.TabIndex = 33;
            this.label48.Text = "Esign : ";
            // 
            // lblConsentEsign
            // 
            this.lblConsentEsign.AutoSize = true;
            this.lblConsentEsign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsentEsign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConsentEsign.Location = new System.Drawing.Point(616, 5);
            this.lblConsentEsign.Name = "lblConsentEsign";
            this.lblConsentEsign.Size = new System.Drawing.Size(0, 14);
            this.lblConsentEsign.TabIndex = 34;
            this.lblConsentEsign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnklblPatientConsent
            // 
            this.lnklblPatientConsent.AutoSize = true;
            this.lnklblPatientConsent.Location = new System.Drawing.Point(129, 5);
            this.lnklblPatientConsent.Name = "lnklblPatientConsent";
            this.lnklblPatientConsent.Size = new System.Drawing.Size(253, 14);
            this.lnklblPatientConsent.TabIndex = 32;
            this.lnklblPatientConsent.TabStop = true;
            this.lnklblPatientConsent.Text = "Click here to register/modify Patient Consent";
            this.lnklblPatientConsent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblPatientConsent_LinkClicked);
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(824, 1);
            this.label46.TabIndex = 30;
            this.label46.Text = "label2";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label44.Location = new System.Drawing.Point(714, 5);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(42, 14);
            this.label44.TabIndex = 27;
            this.label44.Text = "SMS : ";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label41.Location = new System.Drawing.Point(16, 5);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(107, 14);
            this.label41.TabIndex = 23;
            this.label41.Text = "Patient Consent : ";
            // 
            // lblConsentSMS
            // 
            this.lblConsentSMS.AutoSize = true;
            this.lblConsentSMS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsentSMS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConsentSMS.Location = new System.Drawing.Point(759, 5);
            this.lblConsentSMS.Name = "lblConsentSMS";
            this.lblConsentSMS.Size = new System.Drawing.Size(0, 14);
            this.lblConsentSMS.TabIndex = 28;
            this.lblConsentSMS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label42.Location = new System.Drawing.Point(409, 5);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(46, 14);
            this.label42.TabIndex = 25;
            this.label42.Text = "Email : ";
            // 
            // lblConsentEmail
            // 
            this.lblConsentEmail.AutoSize = true;
            this.lblConsentEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsentEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConsentEmail.Location = new System.Drawing.Point(459, 5);
            this.lblConsentEmail.Name = "lblConsentEmail";
            this.lblConsentEmail.Size = new System.Drawing.Size(0, 14);
            this.lblConsentEmail.TabIndex = 26;
            this.lblConsentEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(410, 54);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(105, 14);
            this.label39.TabIndex = 22;
            this.label39.Text = "Unsuspend Date :";
            // 
            // lblAOFUnsuspendDate
            // 
            this.lblAOFUnsuspendDate.AutoSize = true;
            this.lblAOFUnsuspendDate.Location = new System.Drawing.Point(521, 54);
            this.lblAOFUnsuspendDate.Name = "lblAOFUnsuspendDate";
            this.lblAOFUnsuspendDate.Size = new System.Drawing.Size(0, 14);
            this.lblAOFUnsuspendDate.TabIndex = 21;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(824, 1);
            this.label24.TabIndex = 7;
            this.label24.Text = "label2";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(17, 54);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(302, 14);
            this.label37.TabIndex = 20;
            this.label37.Text = "Maximum Preauthorized Amount per Payment Event :";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label38.Location = new System.Drawing.Point(1, 101);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(824, 1);
            this.label38.TabIndex = 6;
            this.label38.Text = "label2";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Right;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(825, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 102);
            this.label40.TabIndex = 5;
            this.label40.Text = "label4";
            // 
            // lblAOFEventCountMax
            // 
            this.lblAOFEventCountMax.AutoSize = true;
            this.lblAOFEventCountMax.Location = new System.Drawing.Point(715, 33);
            this.lblAOFEventCountMax.Name = "lblAOFEventCountMax";
            this.lblAOFEventCountMax.Size = new System.Drawing.Size(0, 14);
            this.lblAOFEventCountMax.TabIndex = 18;
            // 
            // lblAOFEffectiveDate
            // 
            this.lblAOFEffectiveDate.AutoSize = true;
            this.lblAOFEffectiveDate.Location = new System.Drawing.Point(505, 12);
            this.lblAOFEffectiveDate.Name = "lblAOFEffectiveDate";
            this.lblAOFEffectiveDate.Size = new System.Drawing.Size(0, 14);
            this.lblAOFEffectiveDate.TabIndex = 17;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(0, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 102);
            this.label43.TabIndex = 4;
            this.label43.Text = "label4";
            // 
            // lblAOFAgreementDurationMax
            // 
            this.lblAOFAgreementDurationMax.AutoSize = true;
            this.lblAOFAgreementDurationMax.Location = new System.Drawing.Point(296, 33);
            this.lblAOFAgreementDurationMax.Name = "lblAOFAgreementDurationMax";
            this.lblAOFAgreementDurationMax.Size = new System.Drawing.Size(0, 14);
            this.lblAOFAgreementDurationMax.TabIndex = 16;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(17, 12);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(176, 14);
            this.label45.TabIndex = 8;
            this.label45.Text = "Account Authorization Status :";
            // 
            // lblAOFPerEventAmountMax
            // 
            this.lblAOFPerEventAmountMax.AutoSize = true;
            this.lblAOFPerEventAmountMax.Location = new System.Drawing.Point(325, 54);
            this.lblAOFPerEventAmountMax.Name = "lblAOFPerEventAmountMax";
            this.lblAOFPerEventAmountMax.Size = new System.Drawing.Size(0, 14);
            this.lblAOFPerEventAmountMax.TabIndex = 15;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(17, 33);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(273, 14);
            this.label47.TabIndex = 9;
            this.label47.Text = "Authorized Duration of Agreement (in months) :";
            // 
            // lblAOFAgreementStatus
            // 
            this.lblAOFAgreementStatus.AutoSize = true;
            this.lblAOFAgreementStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAOFAgreementStatus.Location = new System.Drawing.Point(199, 12);
            this.lblAOFAgreementStatus.Name = "lblAOFAgreementStatus";
            this.lblAOFAgreementStatus.Size = new System.Drawing.Size(0, 14);
            this.lblAOFAgreementStatus.TabIndex = 14;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(410, 12);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(93, 14);
            this.label49.TabIndex = 11;
            this.label49.Text = "Effective Date :";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(410, 33);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(299, 14);
            this.label51.TabIndex = 12;
            this.label51.Text = "Maximum Number of Preauthorized Payment Events :";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel9.ForeColor = System.Drawing.Color.White;
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel9.Size = new System.Drawing.Size(826, 27);
            this.panel9.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.panel7);
            this.panel10.Controls.Add(this.lblAOFAlert);
            this.panel10.Controls.Add(this.label53);
            this.panel10.Controls.Add(this.label54);
            this.panel10.Controls.Add(this.label55);
            this.panel10.Controls.Add(this.label56);
            this.panel10.Controls.Add(this.label57);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(826, 24);
            this.panel10.TabIndex = 61;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.btnRefreshStatus);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.ForeColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(799, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(26, 22);
            this.panel7.TabIndex = 222;
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshStatus.BackgroundImage")));
            this.btnRefreshStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefreshStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshStatus.FlatAppearance.BorderSize = 0;
            this.btnRefreshStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefreshStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshStatus.Location = new System.Drawing.Point(3, 0);
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshStatus.TabIndex = 57;
            this.btnRefreshStatus.UseVisualStyleBackColor = false;
            this.btnRefreshStatus.Click += new System.EventHandler(this.btnRefreshStatus_Click);
            // 
            // lblAOFAlert
            // 
            this.lblAOFAlert.AutoSize = true;
            this.lblAOFAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAOFAlert.ForeColor = System.Drawing.Color.Black;
            this.lblAOFAlert.Location = new System.Drawing.Point(177, 5);
            this.lblAOFAlert.Name = "lblAOFAlert";
            this.lblAOFAlert.Size = new System.Drawing.Size(45, 14);
            this.lblAOFAlert.TabIndex = 221;
            this.lblAOFAlert.Text = "label24";
            this.lblAOFAlert.Visible = false;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(0, 1);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 22);
            this.label53.TabIndex = 22;
            // 
            // label54
            // 
            this.label54.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label54.AutoEllipsis = true;
            this.label54.AutoSize = true;
            this.label54.BackColor = System.Drawing.Color.Transparent;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label54.Location = new System.Drawing.Point(9, 1);
            this.label54.Name = "label54";
            this.label54.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label54.Size = new System.Drawing.Size(106, 18);
            this.label54.TabIndex = 20;
            this.label54.Text = " Account On File";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Right;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Location = new System.Drawing.Point(825, 1);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 22);
            this.label55.TabIndex = 23;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Top;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(0, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(826, 1);
            this.label56.TabIndex = 24;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(0, 23);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(826, 1);
            this.label57.TabIndex = 58;
            // 
            // tmrAOFAlertBlink
            // 
            this.tmrAOFAlertBlink.Interval = 500;
            this.tmrAOFAlertBlink.Tick += new System.EventHandler(this.tmrAOFAlertBlink_Tick);
            // 
            // frmCheckIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(832, 765);
            this.Controls.Add(this.pnlTemplate);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlInsuranceList);
            this.Controls.Add(this.pnlDateTime);
            this.Controls.Add(this.pnlAccountOnFile);
            this.Controls.Add(this.PnlPatientStrip);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Controls.Add(this.panel6);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckIn";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Checkin";
            this.Load += new System.EventHandler(this.frmCheckIn_Load);
            this.Shown += new System.EventHandler(this.frmCheckIn_Shown);
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.pnlDateTime.ResumeLayout(false);
            this.pnlDateTime.PerformLayout();
            this.pnlInsuranceList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Templates)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PnlPatientStrip.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlAccountOnFile.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnViewBenefit;
        private System.Windows.Forms.Panel pnlDateTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpCheckInDate;
        private System.Windows.Forms.Panel pnlInsuranceList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlTemplate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Templates;
        private System.Windows.Forms.ToolStripButton ts_btnPriorAuthorization;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel PnlPatientStrip;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btn_ModityPatient;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblPatientAge;
        private System.Windows.Forms.Label lblPatientDOB;
        private System.Windows.Forms.Label lblProviderName;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblGlobalPeriodAlert;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.CheckBox chkExcludeInactiveInsurance;
        private System.Windows.Forms.Label lblCopayAlert;
        private System.Windows.Forms.Timer tmrCopayAlertBlink;
        private System.Windows.Forms.ToolStripButton ts_btnAccountOnFile;
        private System.Windows.Forms.Panel pnlAccountOnFile;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label lblAOFEventCountMax;
        private System.Windows.Forms.Label lblAOFEffectiveDate;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lblAOFAgreementDurationMax;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label lblAOFPerEventAmountMax;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblAOFAgreementStatus;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label lblAOFAlert;
        private System.Windows.Forms.Timer tmrAOFAlertBlink;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblAOFUnsuspendDate;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnRefreshStatus;
        private System.Windows.Forms.ToolStripButton ts_btnPatientConsent;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblConsentSMS;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblConsentEmail;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.LinkLabel lnklblPatientConsent;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label lblConsentEsign;
    }
}