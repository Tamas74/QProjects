namespace gloPMGeneral
{
    partial class frmViewBenefit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewBenefit));
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblViewBenefitBottom = new System.Windows.Forms.Label();
            this.lblViewBenefitRight = new System.Windows.Forms.Label();
            this.lblViewBenefitLeft = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlAlertsNotesProvider = new System.Windows.Forms.Panel();
            this.lblAlerts = new System.Windows.Forms.Label();
            this.pnlPatientNotes = new System.Windows.Forms.Panel();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblAlertsCap = new System.Windows.Forms.Label();
            this.btn_Alerts = new System.Windows.Forms.Button();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this.lblDemoCopay = new System.Windows.Forms.Label();
            this.lblDemoProvider = new System.Windows.Forms.Label();
            this.lblDemoCopayCaption = new System.Windows.Forms.Label();
            this.lblDemoProviderCaption = new System.Windows.Forms.Label();
            this.pnlMedCategory = new System.Windows.Forms.Panel();
            this.lblDemoMedCat = new System.Windows.Forms.Label();
            this.lblDemoMedCatCaption = new System.Windows.Forms.Label();
            this.pnlPatient = new System.Windows.Forms.Panel();
            this.lblDemoPatientCaption = new System.Windows.Forms.Label();
            this.lblDemoPatient = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblInactiveInsurance = new System.Windows.Forms.Label();
            this.lblSubscriberDOBCaption = new System.Windows.Forms.Label();
            this.lblSubscriberIDCaption = new System.Windows.Forms.Label();
            this.lblSubscriberDOB = new System.Windows.Forms.Label();
            this.lblSubscriberID = new System.Windows.Forms.Label();
            this.lblSubsroberName = new System.Windows.Forms.Label();
            this.lblSubsroberNameCaption = new System.Windows.Forms.Label();
            this.lblDemoLastPatPayment = new System.Windows.Forms.Label();
            this.pnlAlertNextAppt = new System.Windows.Forms.Panel();
            this.pnlEMRAlertsCaption = new System.Windows.Forms.Panel();
            this.lblDemoEMRAlerts = new System.Windows.Forms.Label();
            this.lblDemogloEMRAlertsCaption = new System.Windows.Forms.Label();
            this.lblDemoNextApptCaption = new System.Windows.Forms.Label();
            this.lblDemoNextAppt = new System.Windows.Forms.Label();
            this.pnlAccountDetails = new System.Windows.Forms.Panel();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.lblcmbInsuranceCaption = new System.Windows.Forms.Label();
            this.pnlPatientPhoto = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.picPAPhoto = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblScanDate = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.TrackbarPlus = new System.Windows.Forms.Button();
            this.TrackbarMinus = new System.Windows.Forms.Button();
            this.picPatient_Cards = new gloPictureBox.gloPictureBox();
            this.TrackBar = new System.Windows.Forms.TrackBar();
            this.panel8 = new System.Windows.Forms.Panel();
            this.InsCardMovefirst = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.InsCardMovePrevious = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.InsCardMoveNext = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.InsCardMoveLast = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPlanEligibilityNote = new System.Windows.Forms.Label();
            this.labelContact = new System.Windows.Forms.Label();
            this.lblEligibilityInsuranceName = new System.Windows.Forms.Label();
            this.lblClickable = new System.Windows.Forms.LinkLabel();
            this.label40 = new System.Windows.Forms.Label();
            this.lbleligibilityContact = new System.Windows.Forms.Label();
            this.lblInsuranceName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblLastchngedDatetime = new System.Windows.Forms.Label();
            this.lblPateintBenefitUptoDate = new System.Windows.Forms.Label();
            this.lblLastChanged = new System.Windows.Forms.Label();
            this.lblUserAndDateTime = new System.Windows.Forms.Label();
            this.txtCopay = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDeductableAmount = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.chkMarkReviewed = new System.Windows.Forms.CheckBox();
            this.mskEndDate = new System.Windows.Forms.MaskedTextBox();
            this.txtCoveragePercent = new System.Windows.Forms.TextBox();
            this.txtPatientRecordedBenefitsNote = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lblEmployer = new System.Windows.Forms.Label();
            this.lblPateintBenefitUptoDateNew = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.picPC_Cards = new System.Windows.Forms.PictureBox();
            this.label61 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPC_MoveFirst = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPC_MovePrevious = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnPC_MoveNext = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.btnPC_MoveLast = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.btnPC_PrintCards = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.btnPC_DeleteCard = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.btnPC_ScanCard = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.lblScannedDate = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbEligibilityCheck = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.c1Response = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.label58 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.pnlPatStripBottom = new System.Windows.Forms.Panel();
            this.lblPayerID = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.labelPayer = new System.Windows.Forms.Label();
            this.lblInsuranceType = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSubscbrDOB = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblDateTimeofLstRsp = new System.Windows.Forms.Label();
            this.lblPrimsryCareProvider = new System.Windows.Forms.Label();
            this.lblastRewdDateTime = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBenefitAsOfDate = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblErrorNote = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lblPrimaryCareProvider = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnInsuranceDown = new System.Windows.Forms.Button();
            this.btnInsuranceUp = new System.Windows.Forms.Button();
            this.label81 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderInner = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btn_ModifyPatient = new System.Windows.Forms.Button();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.lblPatientCodeCaption = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblGenderCaption = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.lblDOBCaption = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblViewBenefitTopHeader = new System.Windows.Forms.Label();
            this.lblViewBenefitLeftHeader = new System.Windows.Forms.Label();
            this.lblViewBenefitRightHeader = new System.Windows.Forms.Label();
            this.lblViewBenefitBottomHeader = new System.Windows.Forms.Label();
            this.pnlFiller = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlAlertsNotesProvider.SuspendLayout();
            this.pnlPatientNotes.SuspendLayout();
            this.pnlMedCategory.SuspendLayout();
            this.pnlPatient.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel14.SuspendLayout();
            this.pnlAlertNextAppt.SuspendLayout();
            this.pnlEMRAlertsCaption.SuspendLayout();
            this.pnlAccountDetails.SuspendLayout();
            this.pnlPatientPhoto.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPAPhoto)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPC_Cards)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel22.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).BeginInit();
            this.panel7.SuspendLayout();
            this.pnlPatStripBottom.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel18.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlHeaderInner.SuspendLayout();
            this.panel19.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.lblViewBenefitBottom);
            this.panel3.Controls.Add(this.lblViewBenefitRight);
            this.panel3.Controls.Add(this.lblViewBenefitLeft);
            this.panel3.Controls.Add(this.pnlLeft);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(924, 149);
            this.panel3.TabIndex = 7;
            // 
            // lblViewBenefitBottom
            // 
            this.lblViewBenefitBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblViewBenefitBottom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitBottom.Location = new System.Drawing.Point(4, 148);
            this.lblViewBenefitBottom.Name = "lblViewBenefitBottom";
            this.lblViewBenefitBottom.Size = new System.Drawing.Size(919, 1);
            this.lblViewBenefitBottom.TabIndex = 77;
            // 
            // lblViewBenefitRight
            // 
            this.lblViewBenefitRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblViewBenefitRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitRight.Location = new System.Drawing.Point(923, 0);
            this.lblViewBenefitRight.Name = "lblViewBenefitRight";
            this.lblViewBenefitRight.Size = new System.Drawing.Size(1, 149);
            this.lblViewBenefitRight.TabIndex = 76;
            // 
            // lblViewBenefitLeft
            // 
            this.lblViewBenefitLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblViewBenefitLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitLeft.Location = new System.Drawing.Point(3, 0);
            this.lblViewBenefitLeft.Name = "lblViewBenefitLeft";
            this.lblViewBenefitLeft.Size = new System.Drawing.Size(1, 149);
            this.lblViewBenefitLeft.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.BackgroundImage = global::gloPMGeneral.Properties.Resources.MedicalCategoryImages_5_BottomOrange;
            this.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeft.Controls.Add(this.pnlAlertsNotesProvider);
            this.pnlLeft.Controls.Add(this.pnlMedCategory);
            this.pnlLeft.Controls.Add(this.pnlPatient);
            this.pnlLeft.Controls.Add(this.panel12);
            this.pnlLeft.Controls.Add(this.pnlPatientPhoto);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(921, 149);
            this.pnlLeft.TabIndex = 68;
            // 
            // pnlAlertsNotesProvider
            // 
            this.pnlAlertsNotesProvider.Controls.Add(this.lblAlerts);
            this.pnlAlertsNotesProvider.Controls.Add(this.pnlPatientNotes);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblAlertsCap);
            this.pnlAlertsNotesProvider.Controls.Add(this.btn_Alerts);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblNotesCaption);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblDemoCopay);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblDemoProvider);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblDemoCopayCaption);
            this.pnlAlertsNotesProvider.Controls.Add(this.lblDemoProviderCaption);
            this.pnlAlertsNotesProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlertsNotesProvider.Location = new System.Drawing.Point(142, 44);
            this.pnlAlertsNotesProvider.Name = "pnlAlertsNotesProvider";
            this.pnlAlertsNotesProvider.Size = new System.Drawing.Size(385, 105);
            this.pnlAlertsNotesProvider.TabIndex = 83;
            // 
            // lblAlerts
            // 
            this.lblAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlerts.AutoEllipsis = true;
            this.lblAlerts.BackColor = System.Drawing.Color.Transparent;
            this.lblAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlerts.ForeColor = System.Drawing.Color.Black;
            this.lblAlerts.Location = new System.Drawing.Point(90, 42);
            this.lblAlerts.MaximumSize = new System.Drawing.Size(245, 14);
            this.lblAlerts.MinimumSize = new System.Drawing.Size(245, 14);
            this.lblAlerts.Name = "lblAlerts";
            this.lblAlerts.Size = new System.Drawing.Size(245, 14);
            this.lblAlerts.TabIndex = 2;
            this.lblAlerts.Text = "Alerts";
            // 
            // pnlPatientNotes
            // 
            this.pnlPatientNotes.AutoScroll = true;
            this.pnlPatientNotes.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatientNotes.Controls.Add(this.lblNotes);
            this.pnlPatientNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPatientNotes.Location = new System.Drawing.Point(90, 61);
            this.pnlPatientNotes.Name = "pnlPatientNotes";
            this.pnlPatientNotes.Size = new System.Drawing.Size(279, 41);
            this.pnlPatientNotes.TabIndex = 1;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.ForeColor = System.Drawing.Color.Black;
            this.lblNotes.Location = new System.Drawing.Point(1, -1);
            this.lblNotes.Margin = new System.Windows.Forms.Padding(0);
            this.lblNotes.MaximumSize = new System.Drawing.Size(280, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(46, 14);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "Sample";
            // 
            // lblAlertsCap
            // 
            this.lblAlertsCap.AutoSize = true;
            this.lblAlertsCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertsCap.ForeColor = System.Drawing.Color.Black;
            this.lblAlertsCap.Location = new System.Drawing.Point(3, 42);
            this.lblAlertsCap.MaximumSize = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.MinimumSize = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.Name = "lblAlertsCap";
            this.lblAlertsCap.Size = new System.Drawing.Size(90, 14);
            this.lblAlertsCap.TabIndex = 93;
            this.lblAlertsCap.Text = "PM Alerts (#) :";
            this.lblAlertsCap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Alerts
            // 
            this.btn_Alerts.BackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Alerts.BackgroundImage")));
            this.btn_Alerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Alerts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.btn_Alerts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Alerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Alerts.Image = ((System.Drawing.Image)(resources.GetObject("btn_Alerts.Image")));
            this.btn_Alerts.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Alerts.Location = new System.Drawing.Point(344, 38);
            this.btn_Alerts.Name = "btn_Alerts";
            this.btn_Alerts.Size = new System.Drawing.Size(22, 22);
            this.btn_Alerts.TabIndex = 0;
            this.btn_Alerts.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Alerts.UseVisualStyleBackColor = false;
            this.btn_Alerts.Click += new System.EventHandler(this.btn_Alerts_Click);
            // 
            // lblNotesCaption
            // 
            this.lblNotesCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotesCaption.AutoSize = true;
            this.lblNotesCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNotesCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotesCaption.ForeColor = System.Drawing.Color.Black;
            this.lblNotesCaption.Location = new System.Drawing.Point(3, 61);
            this.lblNotesCaption.Name = "lblNotesCaption";
            this.lblNotesCaption.Size = new System.Drawing.Size(90, 14);
            this.lblNotesCaption.TabIndex = 91;
            this.lblNotesCaption.Text = "Patient Notes :";
            // 
            // lblDemoCopay
            // 
            this.lblDemoCopay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoCopay.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoCopay.ForeColor = System.Drawing.Color.Black;
            this.lblDemoCopay.Location = new System.Drawing.Point(92, 23);
            this.lblDemoCopay.MinimumSize = new System.Drawing.Size(243, 14);
            this.lblDemoCopay.Name = "lblDemoCopay";
            this.lblDemoCopay.Size = new System.Drawing.Size(252, 18);
            this.lblDemoCopay.TabIndex = 1;
            this.lblDemoCopay.Text = "Copay";
            // 
            // lblDemoProvider
            // 
            this.lblDemoProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoProvider.AutoSize = true;
            this.lblDemoProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoProvider.ForeColor = System.Drawing.Color.Black;
            this.lblDemoProvider.Location = new System.Drawing.Point(92, 4);
            this.lblDemoProvider.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.Name = "lblDemoProvider";
            this.lblDemoProvider.Size = new System.Drawing.Size(200, 14);
            this.lblDemoProvider.TabIndex = 0;
            this.lblDemoProvider.Text = "Provider   ";
            // 
            // lblDemoCopayCaption
            // 
            this.lblDemoCopayCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoCopayCaption.AutoSize = true;
            this.lblDemoCopayCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoCopayCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoCopayCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoCopayCaption.Location = new System.Drawing.Point(45, 23);
            this.lblDemoCopayCaption.Name = "lblDemoCopayCaption";
            this.lblDemoCopayCaption.Size = new System.Drawing.Size(48, 14);
            this.lblDemoCopayCaption.TabIndex = 86;
            this.lblDemoCopayCaption.Text = "Copay :";
            // 
            // lblDemoProviderCaption
            // 
            this.lblDemoProviderCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoProviderCaption.AutoSize = true;
            this.lblDemoProviderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoProviderCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoProviderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoProviderCaption.Location = new System.Drawing.Point(34, 4);
            this.lblDemoProviderCaption.Name = "lblDemoProviderCaption";
            this.lblDemoProviderCaption.Size = new System.Drawing.Size(59, 14);
            this.lblDemoProviderCaption.TabIndex = 84;
            this.lblDemoProviderCaption.Text = "Provider :";
            // 
            // pnlMedCategory
            // 
            this.pnlMedCategory.Controls.Add(this.lblDemoMedCat);
            this.pnlMedCategory.Controls.Add(this.lblDemoMedCatCaption);
            this.pnlMedCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMedCategory.Location = new System.Drawing.Point(142, 22);
            this.pnlMedCategory.Name = "pnlMedCategory";
            this.pnlMedCategory.Size = new System.Drawing.Size(385, 22);
            this.pnlMedCategory.TabIndex = 74;
            // 
            // lblDemoMedCat
            // 
            this.lblDemoMedCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoMedCat.AutoSize = true;
            this.lblDemoMedCat.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoMedCat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoMedCat.ForeColor = System.Drawing.Color.Black;
            this.lblDemoMedCat.Location = new System.Drawing.Point(94, 4);
            this.lblDemoMedCat.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.Name = "lblDemoMedCat";
            this.lblDemoMedCat.Size = new System.Drawing.Size(200, 14);
            this.lblDemoMedCat.TabIndex = 98;
            this.lblDemoMedCat.Text = "Medical Category";
            // 
            // lblDemoMedCatCaption
            // 
            this.lblDemoMedCatCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoMedCatCaption.AutoSize = true;
            this.lblDemoMedCatCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoMedCatCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoMedCatCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoMedCatCaption.Location = new System.Drawing.Point(30, 4);
            this.lblDemoMedCatCaption.Name = "lblDemoMedCatCaption";
            this.lblDemoMedCatCaption.Size = new System.Drawing.Size(64, 14);
            this.lblDemoMedCatCaption.TabIndex = 97;
            this.lblDemoMedCatCaption.Text = "Med. Cat :";
            // 
            // pnlPatient
            // 
            this.pnlPatient.Controls.Add(this.lblDemoPatientCaption);
            this.pnlPatient.Controls.Add(this.lblDemoPatient);
            this.pnlPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatient.Location = new System.Drawing.Point(142, 0);
            this.pnlPatient.Name = "pnlPatient";
            this.pnlPatient.Size = new System.Drawing.Size(385, 22);
            this.pnlPatient.TabIndex = 0;
            // 
            // lblDemoPatientCaption
            // 
            this.lblDemoPatientCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoPatientCaption.AutoSize = true;
            this.lblDemoPatientCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoPatientCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoPatientCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoPatientCaption.Location = new System.Drawing.Point(40, 4);
            this.lblDemoPatientCaption.Name = "lblDemoPatientCaption";
            this.lblDemoPatientCaption.Size = new System.Drawing.Size(54, 14);
            this.lblDemoPatientCaption.TabIndex = 0;
            this.lblDemoPatientCaption.Text = "Patient :";
            // 
            // lblDemoPatient
            // 
            this.lblDemoPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoPatient.AutoSize = true;
            this.lblDemoPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoPatient.ForeColor = System.Drawing.Color.Black;
            this.lblDemoPatient.Location = new System.Drawing.Point(93, 4);
            this.lblDemoPatient.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.Name = "lblDemoPatient";
            this.lblDemoPatient.Size = new System.Drawing.Size(200, 14);
            this.lblDemoPatient.TabIndex = 1;
            this.lblDemoPatient.Text = "Patient";
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel14);
            this.panel12.Controls.Add(this.pnlAlertNextAppt);
            this.panel12.Controls.Add(this.pnlAccountDetails);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel12.Location = new System.Drawing.Point(527, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(394, 149);
            this.panel12.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.lblInactiveInsurance);
            this.panel14.Controls.Add(this.lblSubscriberDOBCaption);
            this.panel14.Controls.Add(this.lblSubscriberIDCaption);
            this.panel14.Controls.Add(this.lblSubscriberDOB);
            this.panel14.Controls.Add(this.lblSubscriberID);
            this.panel14.Controls.Add(this.lblSubsroberName);
            this.panel14.Controls.Add(this.lblSubsroberNameCaption);
            this.panel14.Controls.Add(this.lblDemoLastPatPayment);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 64);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(394, 85);
            this.panel14.TabIndex = 4;
            // 
            // lblInactiveInsurance
            // 
            this.lblInactiveInsurance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInactiveInsurance.AutoEllipsis = true;
            this.lblInactiveInsurance.AutoSize = true;
            this.lblInactiveInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInactiveInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInactiveInsurance.ForeColor = System.Drawing.Color.Red;
            this.lblInactiveInsurance.Location = new System.Drawing.Point(102, 65);
            this.lblInactiveInsurance.Name = "lblInactiveInsurance";
            this.lblInactiveInsurance.Size = new System.Drawing.Size(172, 14);
            this.lblInactiveInsurance.TabIndex = 111;
            this.lblInactiveInsurance.Text = "Selected Insurance Is Inactive";
            this.lblInactiveInsurance.Visible = false;
            // 
            // lblSubscriberDOBCaption
            // 
            this.lblSubscriberDOBCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberDOBCaption.AutoSize = true;
            this.lblSubscriberDOBCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblSubscriberDOBCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberDOBCaption.ForeColor = System.Drawing.Color.Black;
            this.lblSubscriberDOBCaption.Location = new System.Drawing.Point(4, 45);
            this.lblSubscriberDOBCaption.Name = "lblSubscriberDOBCaption";
            this.lblSubscriberDOBCaption.Size = new System.Drawing.Size(99, 14);
            this.lblSubscriberDOBCaption.TabIndex = 91;
            this.lblSubscriberDOBCaption.Text = "Subscriber DOB :";
            // 
            // lblSubscriberIDCaption
            // 
            this.lblSubscriberIDCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberIDCaption.AutoSize = true;
            this.lblSubscriberIDCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblSubscriberIDCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberIDCaption.ForeColor = System.Drawing.Color.Black;
            this.lblSubscriberIDCaption.Location = new System.Drawing.Point(15, 24);
            this.lblSubscriberIDCaption.Name = "lblSubscriberIDCaption";
            this.lblSubscriberIDCaption.Size = new System.Drawing.Size(87, 14);
            this.lblSubscriberIDCaption.TabIndex = 90;
            this.lblSubscriberIDCaption.Text = "Subscriber ID :";
            // 
            // lblSubscriberDOB
            // 
            this.lblSubscriberDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberDOB.AutoSize = true;
            this.lblSubscriberDOB.BackColor = System.Drawing.Color.Transparent;
            this.lblSubscriberDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberDOB.ForeColor = System.Drawing.Color.Black;
            this.lblSubscriberDOB.Location = new System.Drawing.Point(102, 45);
            this.lblSubscriberDOB.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblSubscriberDOB.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblSubscriberDOB.Name = "lblSubscriberDOB";
            this.lblSubscriberDOB.Size = new System.Drawing.Size(200, 14);
            this.lblSubscriberDOB.TabIndex = 2;
            this.lblSubscriberDOB.Text = "DOB     ";
            // 
            // lblSubscriberID
            // 
            this.lblSubscriberID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberID.AutoSize = true;
            this.lblSubscriberID.BackColor = System.Drawing.Color.Transparent;
            this.lblSubscriberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberID.ForeColor = System.Drawing.Color.Black;
            this.lblSubscriberID.Location = new System.Drawing.Point(102, 25);
            this.lblSubscriberID.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblSubscriberID.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblSubscriberID.Name = "lblSubscriberID";
            this.lblSubscriberID.Size = new System.Drawing.Size(200, 14);
            this.lblSubscriberID.TabIndex = 1;
            this.lblSubscriberID.Text = "ID           ";
            // 
            // lblSubsroberName
            // 
            this.lblSubsroberName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubsroberName.AutoSize = true;
            this.lblSubsroberName.BackColor = System.Drawing.Color.Transparent;
            this.lblSubsroberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubsroberName.ForeColor = System.Drawing.Color.Black;
            this.lblSubsroberName.Location = new System.Drawing.Point(102, 4);
            this.lblSubsroberName.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblSubsroberName.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblSubsroberName.Name = "lblSubsroberName";
            this.lblSubsroberName.Size = new System.Drawing.Size(200, 14);
            this.lblSubsroberName.TabIndex = 0;
            this.lblSubsroberName.Text = "Name                 ";
            // 
            // lblSubsroberNameCaption
            // 
            this.lblSubsroberNameCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubsroberNameCaption.AutoSize = true;
            this.lblSubsroberNameCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblSubsroberNameCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubsroberNameCaption.ForeColor = System.Drawing.Color.Black;
            this.lblSubsroberNameCaption.Location = new System.Drawing.Point(31, 4);
            this.lblSubsroberNameCaption.Name = "lblSubsroberNameCaption";
            this.lblSubsroberNameCaption.Size = new System.Drawing.Size(71, 14);
            this.lblSubsroberNameCaption.TabIndex = 4;
            this.lblSubsroberNameCaption.Text = "Subscriber :";
            // 
            // lblDemoLastPatPayment
            // 
            this.lblDemoLastPatPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoLastPatPayment.AutoSize = true;
            this.lblDemoLastPatPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoLastPatPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoLastPatPayment.ForeColor = System.Drawing.Color.Black;
            this.lblDemoLastPatPayment.Location = new System.Drawing.Point(102, 23);
            this.lblDemoLastPatPayment.MaximumSize = new System.Drawing.Size(235, 14);
            this.lblDemoLastPatPayment.MinimumSize = new System.Drawing.Size(235, 14);
            this.lblDemoLastPatPayment.Name = "lblDemoLastPatPayment";
            this.lblDemoLastPatPayment.Size = new System.Drawing.Size(235, 14);
            this.lblDemoLastPatPayment.TabIndex = 89;
            this.lblDemoLastPatPayment.Text = "                                ";
            // 
            // pnlAlertNextAppt
            // 
            this.pnlAlertNextAppt.Controls.Add(this.pnlEMRAlertsCaption);
            this.pnlAlertNextAppt.Controls.Add(this.lblDemoNextApptCaption);
            this.pnlAlertNextAppt.Controls.Add(this.lblDemoNextAppt);
            this.pnlAlertNextAppt.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAlertNextAppt.Location = new System.Drawing.Point(0, 26);
            this.pnlAlertNextAppt.Name = "pnlAlertNextAppt";
            this.pnlAlertNextAppt.Size = new System.Drawing.Size(394, 38);
            this.pnlAlertNextAppt.TabIndex = 5;
            // 
            // pnlEMRAlertsCaption
            // 
            this.pnlEMRAlertsCaption.Controls.Add(this.lblDemoEMRAlerts);
            this.pnlEMRAlertsCaption.Controls.Add(this.lblDemogloEMRAlertsCaption);
            this.pnlEMRAlertsCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEMRAlertsCaption.Location = new System.Drawing.Point(0, 18);
            this.pnlEMRAlertsCaption.Name = "pnlEMRAlertsCaption";
            this.pnlEMRAlertsCaption.Size = new System.Drawing.Size(394, 20);
            this.pnlEMRAlertsCaption.TabIndex = 100;
            // 
            // lblDemoEMRAlerts
            // 
            this.lblDemoEMRAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoEMRAlerts.AutoEllipsis = true;
            this.lblDemoEMRAlerts.AutoSize = true;
            this.lblDemoEMRAlerts.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoEMRAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoEMRAlerts.ForeColor = System.Drawing.Color.Black;
            this.lblDemoEMRAlerts.Location = new System.Drawing.Point(102, 2);
            this.lblDemoEMRAlerts.MaximumSize = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.MinimumSize = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.Name = "lblDemoEMRAlerts";
            this.lblDemoEMRAlerts.Size = new System.Drawing.Size(235, 14);
            this.lblDemoEMRAlerts.TabIndex = 93;
            // 
            // lblDemogloEMRAlertsCaption
            // 
            this.lblDemogloEMRAlertsCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemogloEMRAlertsCaption.AutoSize = true;
            this.lblDemogloEMRAlertsCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemogloEMRAlertsCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemogloEMRAlertsCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemogloEMRAlertsCaption.Location = new System.Drawing.Point(29, 3);
            this.lblDemogloEMRAlertsCaption.Name = "lblDemogloEMRAlertsCaption";
            this.lblDemogloEMRAlertsCaption.Size = new System.Drawing.Size(73, 14);
            this.lblDemogloEMRAlertsCaption.TabIndex = 91;
            this.lblDemogloEMRAlertsCaption.Text = "EMR Alerts :";
            // 
            // lblDemoNextApptCaption
            // 
            this.lblDemoNextApptCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoNextApptCaption.AutoSize = true;
            this.lblDemoNextApptCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoNextApptCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoNextApptCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDemoNextApptCaption.Location = new System.Drawing.Point(26, 1);
            this.lblDemoNextApptCaption.Name = "lblDemoNextApptCaption";
            this.lblDemoNextApptCaption.Size = new System.Drawing.Size(76, 14);
            this.lblDemoNextApptCaption.TabIndex = 98;
            this.lblDemoNextApptCaption.Text = "Next Appt. :";
            // 
            // lblDemoNextAppt
            // 
            this.lblDemoNextAppt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDemoNextAppt.AutoSize = true;
            this.lblDemoNextAppt.BackColor = System.Drawing.Color.Transparent;
            this.lblDemoNextAppt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDemoNextAppt.ForeColor = System.Drawing.Color.Black;
            this.lblDemoNextAppt.Location = new System.Drawing.Point(102, 3);
            this.lblDemoNextAppt.MaximumSize = new System.Drawing.Size(200, 14);
            this.lblDemoNextAppt.MinimumSize = new System.Drawing.Size(200, 14);
            this.lblDemoNextAppt.Name = "lblDemoNextAppt";
            this.lblDemoNextAppt.Size = new System.Drawing.Size(200, 14);
            this.lblDemoNextAppt.TabIndex = 99;
            // 
            // pnlAccountDetails
            // 
            this.pnlAccountDetails.Controls.Add(this.cmbInsurance);
            this.pnlAccountDetails.Controls.Add(this.lblcmbInsuranceCaption);
            this.pnlAccountDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAccountDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlAccountDetails.Name = "pnlAccountDetails";
            this.pnlAccountDetails.Size = new System.Drawing.Size(394, 26);
            this.pnlAccountDetails.TabIndex = 0;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(102, 2);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(240, 22);
            this.cmbInsurance.TabIndex = 0;
            this.cmbInsurance.SelectedIndexChanged += new System.EventHandler(this.cmbInsurance_SelectedIndexChanged);
            this.cmbInsurance.MouseEnter += new System.EventHandler(this.cmbInsurance_MouseEnter);
            // 
            // lblcmbInsuranceCaption
            // 
            this.lblcmbInsuranceCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblcmbInsuranceCaption.AutoSize = true;
            this.lblcmbInsuranceCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblcmbInsuranceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcmbInsuranceCaption.ForeColor = System.Drawing.Color.Black;
            this.lblcmbInsuranceCaption.Location = new System.Drawing.Point(33, 6);
            this.lblcmbInsuranceCaption.Name = "lblcmbInsuranceCaption";
            this.lblcmbInsuranceCaption.Size = new System.Drawing.Size(68, 14);
            this.lblcmbInsuranceCaption.TabIndex = 0;
            this.lblcmbInsuranceCaption.Text = "Insurance :";
            // 
            // pnlPatientPhoto
            // 
            this.pnlPatientPhoto.Controls.Add(this.panel15);
            this.pnlPatientPhoto.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPatientPhoto.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientPhoto.Name = "pnlPatientPhoto";
            this.pnlPatientPhoto.Size = new System.Drawing.Size(142, 149);
            this.pnlPatientPhoto.TabIndex = 73;
            // 
            // panel15
            // 
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.picPAPhoto);
            this.panel15.Location = new System.Drawing.Point(7, 7);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(127, 127);
            this.panel15.TabIndex = 1;
            // 
            // picPAPhoto
            // 
            this.picPAPhoto.BackColor = System.Drawing.Color.Transparent;
            this.picPAPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPAPhoto.Location = new System.Drawing.Point(0, 0);
            this.picPAPhoto.Name = "picPAPhoto";
            this.picPAPhoto.Size = new System.Drawing.Size(125, 125);
            this.picPAPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPAPhoto.TabIndex = 0;
            this.picPAPhoto.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblPhone);
            this.panel2.Controls.Add(this.label72);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label55);
            this.panel2.Controls.Add(this.label46);
            this.panel2.Controls.Add(this.label38);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblPlanEligibilityNote);
            this.panel2.Controls.Add(this.labelContact);
            this.panel2.Controls.Add(this.lblEligibilityInsuranceName);
            this.panel2.Controls.Add(this.lblClickable);
            this.panel2.Controls.Add(this.label40);
            this.panel2.Controls.Add(this.lbleligibilityContact);
            this.panel2.Controls.Add(this.lblInsuranceName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 158);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel2.Size = new System.Drawing.Size(927, 242);
            this.panel2.TabIndex = 2;
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhone.AutoEllipsis = true;
            this.lblPhone.AutoSize = true;
            this.lblPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(133, 61);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(42, 14);
            this.lblPhone.TabIndex = 117;
            this.lblPhone.Text = "Phone";
            // 
            // label72
            // 
            this.label72.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label72.AutoEllipsis = true;
            this.label72.AutoSize = true;
            this.label72.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(80, 60);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(50, 14);
            this.label72.TabIndex = 116;
            this.label72.Text = "Phone :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblScanDate);
            this.panel1.Controls.Add(this.label77);
            this.panel1.Controls.Add(this.TrackbarPlus);
            this.panel1.Controls.Add(this.TrackbarMinus);
            this.panel1.Controls.Add(this.picPatient_Cards);
            this.panel1.Controls.Add(this.TrackBar);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(435, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(488, 237);
            this.panel1.TabIndex = 109;
            // 
            // lblScanDate
            // 
            this.lblScanDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScanDate.AutoEllipsis = true;
            this.lblScanDate.AutoSize = true;
            this.lblScanDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblScanDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanDate.Location = new System.Drawing.Point(370, 219);
            this.lblScanDate.Name = "lblScanDate";
            this.lblScanDate.Size = new System.Drawing.Size(80, 14);
            this.lblScanDate.TabIndex = 125;
            this.lblScanDate.Text = "ScannedDate";
            // 
            // label77
            // 
            this.label77.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label77.AutoEllipsis = true;
            this.label77.AutoSize = true;
            this.label77.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label77.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(305, 219);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(62, 14);
            this.label77.TabIndex = 124;
            this.label77.Text = "Scanned :";
            // 
            // TrackbarPlus
            // 
            this.TrackbarPlus.FlatAppearance.BorderSize = 0;
            this.TrackbarPlus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TrackbarPlus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TrackbarPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrackbarPlus.Image = ((System.Drawing.Image)(resources.GetObject("TrackbarPlus.Image")));
            this.TrackbarPlus.Location = new System.Drawing.Point(17, 4);
            this.TrackbarPlus.Name = "TrackbarPlus";
            this.TrackbarPlus.Size = new System.Drawing.Size(18, 16);
            this.TrackbarPlus.TabIndex = 123;
            this.toolTip1.SetToolTip(this.TrackbarPlus, "Zoom In");
            this.TrackbarPlus.UseVisualStyleBackColor = true;
            this.TrackbarPlus.Click += new System.EventHandler(this.TrackbarPlus_Click);
            // 
            // TrackbarMinus
            // 
            this.TrackbarMinus.FlatAppearance.BorderSize = 0;
            this.TrackbarMinus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TrackbarMinus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TrackbarMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrackbarMinus.Image = ((System.Drawing.Image)(resources.GetObject("TrackbarMinus.Image")));
            this.TrackbarMinus.Location = new System.Drawing.Point(17, 203);
            this.TrackbarMinus.Name = "TrackbarMinus";
            this.TrackbarMinus.Size = new System.Drawing.Size(18, 15);
            this.TrackbarMinus.TabIndex = 110;
            this.TrackbarMinus.TabStop = false;
            this.toolTip1.SetToolTip(this.TrackbarMinus, "Zoom Out");
            this.TrackbarMinus.UseVisualStyleBackColor = true;
            this.TrackbarMinus.Click += new System.EventHandler(this.TrackbarMinus_Click);
            // 
            // picPatient_Cards
            // 
            this.picPatient_Cards.AutoScroll = true;
            this.picPatient_Cards.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.picPatient_Cards.AutoSize = true;
            this.picPatient_Cards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPatient_Cards.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPatient_Cards.byteImage = null;
            this.picPatient_Cards.Image = null;
            this.picPatient_Cards.IsMovable = true;
            this.picPatient_Cards.IsPAPhotomodified = false;
            this.picPatient_Cards.Location = new System.Drawing.Point(39, 8);
            this.picPatient_Cards.Name = "picPatient_Cards";
            this.picPatient_Cards.PictBoxHeight = ((short)(137));
            this.picPatient_Cards.PictBoxWidth = ((short)(123));
            this.picPatient_Cards.PLocation = new System.Drawing.Point(-258, -171);
            this.picPatient_Cards.Rotation = 0;
            this.picPatient_Cards.Size = new System.Drawing.Size(405, 210);
            this.picPatient_Cards.sZoomVersion = "5X";
            this.picPatient_Cards.TabIndex = 0;
            this.picPatient_Cards.TabStop = false;
            this.picPatient_Cards.Zoom = 1;
            this.picPatient_Cards.ZoomValueForTrackBar = 44;
            // 
            // TrackBar
            // 
            this.TrackBar.Location = new System.Drawing.Point(17, 8);
            this.TrackBar.Maximum = 44;
            this.TrackBar.Minimum = -44;
            this.TrackBar.Name = "TrackBar";
            this.TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackBar.Size = new System.Drawing.Size(45, 210);
            this.TrackBar.TabIndex = 109;
            this.TrackBar.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged_1);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel8.Controls.Add(this.InsCardMovefirst);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.InsCardMovePrevious);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.InsCardMoveNext);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.InsCardMoveLast);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(450, 5);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(33, 227);
            this.panel8.TabIndex = 108;
            // 
            // InsCardMovefirst
            // 
            this.InsCardMovefirst.BackColor = System.Drawing.Color.Transparent;
            this.InsCardMovefirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InsCardMovefirst.BackgroundImage")));
            this.InsCardMovefirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsCardMovefirst.Dock = System.Windows.Forms.DockStyle.Top;
            this.InsCardMovefirst.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.InsCardMovefirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsCardMovefirst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsCardMovefirst.Image = ((System.Drawing.Image)(resources.GetObject("InsCardMovefirst.Image")));
            this.InsCardMovefirst.Location = new System.Drawing.Point(0, 95);
            this.InsCardMovefirst.Name = "InsCardMovefirst";
            this.InsCardMovefirst.Size = new System.Drawing.Size(33, 27);
            this.InsCardMovefirst.TabIndex = 3;
            this.toolTip1.SetToolTip(this.InsCardMovefirst, "Move First");
            this.InsCardMovefirst.UseVisualStyleBackColor = false;
            this.InsCardMovefirst.Click += new System.EventHandler(this.InsCardMovefirst_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(0, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 5);
            this.label6.TabIndex = 108;
            // 
            // InsCardMovePrevious
            // 
            this.InsCardMovePrevious.BackColor = System.Drawing.Color.Transparent;
            this.InsCardMovePrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InsCardMovePrevious.BackgroundImage")));
            this.InsCardMovePrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsCardMovePrevious.Dock = System.Windows.Forms.DockStyle.Top;
            this.InsCardMovePrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.InsCardMovePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsCardMovePrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsCardMovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("InsCardMovePrevious.Image")));
            this.InsCardMovePrevious.Location = new System.Drawing.Point(0, 65);
            this.InsCardMovePrevious.Name = "InsCardMovePrevious";
            this.InsCardMovePrevious.Size = new System.Drawing.Size(33, 25);
            this.InsCardMovePrevious.TabIndex = 2;
            this.toolTip1.SetToolTip(this.InsCardMovePrevious, "Move Previous");
            this.InsCardMovePrevious.UseVisualStyleBackColor = false;
            this.InsCardMovePrevious.Click += new System.EventHandler(this.InsCardMovePrevious_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(0, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 5);
            this.label9.TabIndex = 110;
            // 
            // InsCardMoveNext
            // 
            this.InsCardMoveNext.BackColor = System.Drawing.Color.Transparent;
            this.InsCardMoveNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InsCardMoveNext.BackgroundImage")));
            this.InsCardMoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsCardMoveNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.InsCardMoveNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.InsCardMoveNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsCardMoveNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsCardMoveNext.Image = ((System.Drawing.Image)(resources.GetObject("InsCardMoveNext.Image")));
            this.InsCardMoveNext.Location = new System.Drawing.Point(0, 35);
            this.InsCardMoveNext.Name = "InsCardMoveNext";
            this.InsCardMoveNext.Size = new System.Drawing.Size(33, 25);
            this.InsCardMoveNext.TabIndex = 1;
            this.toolTip1.SetToolTip(this.InsCardMoveNext, "Move Next");
            this.InsCardMoveNext.UseVisualStyleBackColor = false;
            this.InsCardMoveNext.Click += new System.EventHandler(this.InsCardMoveNext_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(0, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 5);
            this.label10.TabIndex = 112;
            // 
            // InsCardMoveLast
            // 
            this.InsCardMoveLast.BackColor = System.Drawing.Color.Transparent;
            this.InsCardMoveLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InsCardMoveLast.BackgroundImage")));
            this.InsCardMoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InsCardMoveLast.Dock = System.Windows.Forms.DockStyle.Top;
            this.InsCardMoveLast.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.InsCardMoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InsCardMoveLast.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsCardMoveLast.Image = ((System.Drawing.Image)(resources.GetObject("InsCardMoveLast.Image")));
            this.InsCardMoveLast.Location = new System.Drawing.Point(0, 5);
            this.InsCardMoveLast.Name = "InsCardMoveLast";
            this.InsCardMoveLast.Size = new System.Drawing.Size(33, 25);
            this.InsCardMoveLast.TabIndex = 0;
            this.toolTip1.SetToolTip(this.InsCardMoveLast, "Move Last");
            this.InsCardMoveLast.UseVisualStyleBackColor = false;
            this.InsCardMoveLast.Click += new System.EventHandler(this.InsCardMoveLast_Click);
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(33, 5);
            this.label41.TabIndex = 115;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Location = new System.Drawing.Point(0, 225);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(33, 2);
            this.label42.TabIndex = 100;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label55.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(4, 241);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(919, 1);
            this.label55.TabIndex = 115;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(4, 3);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(919, 1);
            this.label46.TabIndex = 114;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(923, 3);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 239);
            this.label38.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 239);
            this.label1.TabIndex = 112;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 14);
            this.label4.TabIndex = 111;
            this.label4.Text = "Eligibility URL :";
            // 
            // lblPlanEligibilityNote
            // 
            this.lblPlanEligibilityNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlanEligibilityNote.AutoEllipsis = true;
            this.lblPlanEligibilityNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPlanEligibilityNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanEligibilityNote.Location = new System.Drawing.Point(132, 108);
            this.lblPlanEligibilityNote.Name = "lblPlanEligibilityNote";
            this.lblPlanEligibilityNote.Size = new System.Drawing.Size(295, 107);
            this.lblPlanEligibilityNote.TabIndex = 110;
            this.lblPlanEligibilityNote.Text = "PlanEligibilityNote";
            // 
            // labelContact
            // 
            this.labelContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelContact.AutoEllipsis = true;
            this.labelContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContact.Location = new System.Drawing.Point(132, 38);
            this.labelContact.Name = "labelContact";
            this.labelContact.Size = new System.Drawing.Size(295, 27);
            this.labelContact.TabIndex = 16;
            this.labelContact.Text = "Contact";
            // 
            // lblEligibilityInsuranceName
            // 
            this.lblEligibilityInsuranceName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEligibilityInsuranceName.AutoEllipsis = true;
            this.lblEligibilityInsuranceName.AutoSize = true;
            this.lblEligibilityInsuranceName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEligibilityInsuranceName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEligibilityInsuranceName.Location = new System.Drawing.Point(132, 15);
            this.lblEligibilityInsuranceName.Name = "lblEligibilityInsuranceName";
            this.lblEligibilityInsuranceName.Size = new System.Drawing.Size(95, 14);
            this.lblEligibilityInsuranceName.TabIndex = 107;
            this.lblEligibilityInsuranceName.Text = "Insurance Name";
            // 
            // lblClickable
            // 
            this.lblClickable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClickable.AutoEllipsis = true;
            this.lblClickable.BackColor = System.Drawing.Color.Transparent;
            this.lblClickable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblClickable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickable.Location = new System.Drawing.Point(132, 82);
            this.lblClickable.Name = "lblClickable";
            this.lblClickable.Size = new System.Drawing.Size(295, 33);
            this.lblClickable.TabIndex = 0;
            this.lblClickable.TabStop = true;
            this.lblClickable.Text = "Clickable URL";
            this.lblClickable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClickable_LinkClicked);
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.AutoEllipsis = true;
            this.label40.AutoSize = true;
            this.label40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(15, 108);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(116, 14);
            this.label40.TabIndex = 105;
            this.label40.Text = "Plan Eligibility Note :";
            // 
            // lbleligibilityContact
            // 
            this.lbleligibilityContact.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbleligibilityContact.AutoEllipsis = true;
            this.lbleligibilityContact.AutoSize = true;
            this.lbleligibilityContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbleligibilityContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbleligibilityContact.Location = new System.Drawing.Point(25, 38);
            this.lbleligibilityContact.Name = "lbleligibilityContact";
            this.lbleligibilityContact.Size = new System.Drawing.Size(106, 14);
            this.lbleligibilityContact.TabIndex = 14;
            this.lbleligibilityContact.Text = "Eligibility Contact :";
            // 
            // lblInsuranceName
            // 
            this.lblInsuranceName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuranceName.AutoEllipsis = true;
            this.lblInsuranceName.AutoSize = true;
            this.lblInsuranceName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuranceName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuranceName.Location = new System.Drawing.Point(28, 15);
            this.lblInsuranceName.Name = "lblInsuranceName";
            this.lblInsuranceName.Size = new System.Drawing.Size(103, 14);
            this.lblInsuranceName.TabIndex = 13;
            this.lblInsuranceName.Text = "Insurance Name :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblLastchngedDatetime);
            this.panel5.Controls.Add(this.lblPateintBenefitUptoDate);
            this.panel5.Controls.Add(this.lblLastChanged);
            this.panel5.Controls.Add(this.lblUserAndDateTime);
            this.panel5.Controls.Add(this.txtCopay);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Controls.Add(this.label39);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.txtDeductableAmount);
            this.panel5.Controls.Add(this.label32);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.mskStartDate);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.chkMarkReviewed);
            this.panel5.Controls.Add(this.mskEndDate);
            this.panel5.Controls.Add(this.txtCoveragePercent);
            this.panel5.Controls.Add(this.txtPatientRecordedBenefitsNote);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.lblEmployer);
            this.panel5.Controls.Add(this.lblPateintBenefitUptoDateNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(927, 132);
            this.panel5.TabIndex = 1;
            // 
            // lblLastchngedDatetime
            // 
            this.lblLastchngedDatetime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastchngedDatetime.AutoEllipsis = true;
            this.lblLastchngedDatetime.AutoSize = true;
            this.lblLastchngedDatetime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLastchngedDatetime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastchngedDatetime.ForeColor = System.Drawing.Color.Red;
            this.lblLastchngedDatetime.Location = new System.Drawing.Point(652, 85);
            this.lblLastchngedDatetime.Name = "lblLastchngedDatetime";
            this.lblLastchngedDatetime.Size = new System.Drawing.Size(79, 14);
            this.lblLastchngedDatetime.TabIndex = 110;
            this.lblLastchngedDatetime.Text = "DateAndtime";
            // 
            // lblPateintBenefitUptoDate
            // 
            this.lblPateintBenefitUptoDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPateintBenefitUptoDate.AutoEllipsis = true;
            this.lblPateintBenefitUptoDate.AutoSize = true;
            this.lblPateintBenefitUptoDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPateintBenefitUptoDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPateintBenefitUptoDate.ForeColor = System.Drawing.Color.Red;
            this.lblPateintBenefitUptoDate.Location = new System.Drawing.Point(211, 10);
            this.lblPateintBenefitUptoDate.Name = "lblPateintBenefitUptoDate";
            this.lblPateintBenefitUptoDate.Size = new System.Drawing.Size(257, 14);
            this.lblPateintBenefitUptoDate.TabIndex = 108;
            this.lblPateintBenefitUptoDate.Text = "today’s date is not within the start/end date ";
            this.lblPateintBenefitUptoDate.Visible = false;
            // 
            // lblLastChanged
            // 
            this.lblLastChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastChanged.AutoEllipsis = true;
            this.lblLastChanged.AutoSize = true;
            this.lblLastChanged.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLastChanged.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastChanged.Location = new System.Drawing.Point(617, 85);
            this.lblLastChanged.Name = "lblLastChanged";
            this.lblLastChanged.Size = new System.Drawing.Size(35, 14);
            this.lblLastChanged.TabIndex = 65;
            this.lblLastChanged.Text = "User:";
            // 
            // lblUserAndDateTime
            // 
            this.lblUserAndDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserAndDateTime.AutoEllipsis = true;
            this.lblUserAndDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserAndDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserAndDateTime.Location = new System.Drawing.Point(614, 64);
            this.lblUserAndDateTime.Name = "lblUserAndDateTime";
            this.lblUserAndDateTime.Size = new System.Drawing.Size(258, 19);
            this.lblUserAndDateTime.TabIndex = 64;
            this.lblUserAndDateTime.Text = "<UserName> <TodaysDate>";
            // 
            // txtCopay
            // 
            this.txtCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopay.ForeColor = System.Drawing.Color.Black;
            this.txtCopay.Location = new System.Drawing.Point(77, 32);
            this.txtCopay.MaxLength = 6;
            this.txtCopay.Name = "txtCopay";
            this.txtCopay.ShortcutsEnabled = false;
            this.txtCopay.Size = new System.Drawing.Size(54, 22);
            this.txtCopay.TabIndex = 0;
            this.txtCopay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCopay.TextChanged += new System.EventHandler(this.txtCopay_TextChanged);
            this.txtCopay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCopay_KeyPress);
            this.txtCopay.Validating += new System.ComponentModel.CancelEventHandler(this.txtCopay_Validating);
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(24, 36);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(48, 14);
            this.label29.TabIndex = 63;
            this.label29.Text = "Copay :";
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(11, 10);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(185, 14);
            this.label39.TabIndex = 61;
            this.label39.Text = "Patient\'s Recorded Benefits :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(520, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 14);
            this.label2.TabIndex = 59;
            this.label2.Text = "Last Reviewed :";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(923, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 124);
            this.label19.TabIndex = 57;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 124);
            this.label18.TabIndex = 56;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(921, 1);
            this.label17.TabIndex = 55;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(921, 1);
            this.label16.TabIndex = 54;
            // 
            // txtDeductableAmount
            // 
            this.txtDeductableAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductableAmount.ForeColor = System.Drawing.Color.Black;
            this.txtDeductableAmount.Location = new System.Drawing.Point(235, 32);
            this.txtDeductableAmount.MaxLength = 8;
            this.txtDeductableAmount.Name = "txtDeductableAmount";
            this.txtDeductableAmount.ShortcutsEnabled = false;
            this.txtDeductableAmount.Size = new System.Drawing.Size(65, 22);
            this.txtDeductableAmount.TabIndex = 1;
            this.txtDeductableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeductableAmount.TextChanged += new System.EventHandler(this.txtDeductableAmount_TextChanged);
            this.txtDeductableAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeductableAmount_KeyPress);
            this.txtDeductableAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txtDeductableAmount_Validating);
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoEllipsis = true;
            this.label32.AutoSize = true;
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(702, 36);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(36, 14);
            this.label32.TabIndex = 12;
            this.label32.Text = "End :";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoEllipsis = true;
            this.label28.AutoSize = true;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(160, 36);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(73, 14);
            this.label28.TabIndex = 1;
            this.label28.Text = "Deductible :";
            // 
            // mskStartDate
            // 
            this.mskStartDate.Location = new System.Drawing.Point(551, 32);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(100, 22);
            this.mskStartDate.TabIndex = 3;
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            this.mskStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskStartDate_MouseClick);
            this.mskStartDate.TextChanged += new System.EventHandler(this.mskStartDate_TextChanged);
            this.mskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartDate_Validating);
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(505, 36);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 12;
            this.label31.Text = "Start :";
            // 
            // chkMarkReviewed
            // 
            this.chkMarkReviewed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarkReviewed.AutoSize = true;
            this.chkMarkReviewed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMarkReviewed.Location = new System.Drawing.Point(439, 64);
            this.chkMarkReviewed.Name = "chkMarkReviewed";
            this.chkMarkReviewed.Size = new System.Drawing.Size(178, 18);
            this.chkMarkReviewed.TabIndex = 5;
            this.chkMarkReviewed.Text = "Mark benefits reviewed by :";
            this.chkMarkReviewed.UseVisualStyleBackColor = true;
            this.chkMarkReviewed.CheckedChanged += new System.EventHandler(this.chkMarkReviewed_CheckedChanged);
            // 
            // mskEndDate
            // 
            this.mskEndDate.Location = new System.Drawing.Point(741, 32);
            this.mskEndDate.Mask = "00/00/0000";
            this.mskEndDate.Name = "mskEndDate";
            this.mskEndDate.Size = new System.Drawing.Size(98, 22);
            this.mskEndDate.TabIndex = 4;
            this.mskEndDate.ValidatingType = typeof(System.DateTime);
            this.mskEndDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mskEndDate.TextChanged += new System.EventHandler(this.mskEndDate_TextChanged);
            this.mskEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndDate_Validating);
            // 
            // txtCoveragePercent
            // 
            this.txtCoveragePercent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoveragePercent.ForeColor = System.Drawing.Color.Black;
            this.txtCoveragePercent.Location = new System.Drawing.Point(426, 32);
            this.txtCoveragePercent.MaxLength = 6;
            this.txtCoveragePercent.Name = "txtCoveragePercent";
            this.txtCoveragePercent.ShortcutsEnabled = false;
            this.txtCoveragePercent.Size = new System.Drawing.Size(54, 22);
            this.txtCoveragePercent.TabIndex = 2;
            this.txtCoveragePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCoveragePercent.TextChanged += new System.EventHandler(this.txtCoveragePercent_TextChanged);
            this.txtCoveragePercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCoveragePercent_KeyPress);
            this.txtCoveragePercent.Validating += new System.ComponentModel.CancelEventHandler(this.txtCoveragePercent_Validating);
            // 
            // txtPatientRecordedBenefitsNote
            // 
            this.txtPatientRecordedBenefitsNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientRecordedBenefitsNote.ForeColor = System.Drawing.Color.Black;
            this.txtPatientRecordedBenefitsNote.Location = new System.Drawing.Point(77, 60);
            this.txtPatientRecordedBenefitsNote.MaxLength = 255;
            this.txtPatientRecordedBenefitsNote.Multiline = true;
            this.txtPatientRecordedBenefitsNote.Name = "txtPatientRecordedBenefitsNote";
            this.txtPatientRecordedBenefitsNote.Size = new System.Drawing.Size(356, 62);
            this.txtPatientRecordedBenefitsNote.TabIndex = 6;
            this.txtPatientRecordedBenefitsNote.TextChanged += new System.EventHandler(this.txtPatientRecordedBenefitsNote_TextChanged);
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoEllipsis = true;
            this.label30.AutoSize = true;
            this.label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(321, 36);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 14);
            this.label30.TabIndex = 18;
            this.label30.Text = "Co-Insurance % :";
            // 
            // lblEmployer
            // 
            this.lblEmployer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmployer.AutoEllipsis = true;
            this.lblEmployer.AutoSize = true;
            this.lblEmployer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmployer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployer.Location = new System.Drawing.Point(30, 63);
            this.lblEmployer.Name = "lblEmployer";
            this.lblEmployer.Size = new System.Drawing.Size(42, 14);
            this.lblEmployer.TabIndex = 18;
            this.lblEmployer.Text = "Note :";
            // 
            // lblPateintBenefitUptoDateNew
            // 
            this.lblPateintBenefitUptoDateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPateintBenefitUptoDateNew.AutoEllipsis = true;
            this.lblPateintBenefitUptoDateNew.AutoSize = true;
            this.lblPateintBenefitUptoDateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPateintBenefitUptoDateNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPateintBenefitUptoDateNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPateintBenefitUptoDateNew.Location = new System.Drawing.Point(518, 108);
            this.lblPateintBenefitUptoDateNew.Name = "lblPateintBenefitUptoDateNew";
            this.lblPateintBenefitUptoDateNew.Size = new System.Drawing.Size(280, 14);
            this.lblPateintBenefitUptoDateNew.TabIndex = 109;
            this.lblPateintBenefitUptoDateNew.Text = "Patient Recorded Benefits may not be up to date";
            this.lblPateintBenefitUptoDateNew.Visible = false;
            // 
            // panel13
            // 
            this.panel13.AutoScroll = true;
            this.panel13.Controls.Add(this.picPC_Cards);
            this.panel13.Controls.Add(this.label61);
            this.panel13.Controls.Add(this.panel4);
            this.panel13.Controls.Add(this.label15);
            this.panel13.Controls.Add(this.label12);
            this.panel13.Controls.Add(this.label13);
            this.panel13.Controls.Add(this.label14);
            this.panel13.Controls.Add(this.label23);
            this.panel13.Location = new System.Drawing.Point(3, 18);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(369, 236);
            this.panel13.TabIndex = 110;
            // 
            // picPC_Cards
            // 
            this.picPC_Cards.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPC_Cards.BackgroundImage")));
            this.picPC_Cards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPC_Cards.Dock = System.Windows.Forms.DockStyle.Top;
            this.picPC_Cards.Location = new System.Drawing.Point(17, 3);
            this.picPC_Cards.Name = "picPC_Cards";
            this.picPC_Cards.Size = new System.Drawing.Size(296, 210);
            this.picPC_Cards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPC_Cards.TabIndex = 106;
            this.picPC_Cards.TabStop = false;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.Black;
            this.label61.Location = new System.Drawing.Point(313, 3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(9, 229);
            this.label61.TabIndex = 109;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPC_MoveFirst);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.btnPC_MovePrevious);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.btnPC_MoveNext);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.btnPC_MoveLast);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.btnPC_PrintCards);
            this.panel4.Controls.Add(this.label60);
            this.panel4.Controls.Add(this.btnPC_DeleteCard);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.btnPC_ScanCard);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(322, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(38, 229);
            this.panel4.TabIndex = 108;
            // 
            // btnPC_MoveFirst
            // 
            this.btnPC_MoveFirst.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveFirst.BackgroundImage")));
            this.btnPC_MoveFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveFirst.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveFirst.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveFirst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveFirst.Image")));
            this.btnPC_MoveFirst.Location = new System.Drawing.Point(0, 183);
            this.btnPC_MoveFirst.Name = "btnPC_MoveFirst";
            this.btnPC_MoveFirst.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveFirst.TabIndex = 5;
            this.btnPC_MoveFirst.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 5);
            this.label7.TabIndex = 108;
            // 
            // btnPC_MovePrevious
            // 
            this.btnPC_MovePrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MovePrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MovePrevious.BackgroundImage")));
            this.btnPC_MovePrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MovePrevious.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MovePrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MovePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MovePrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MovePrevious.Image")));
            this.btnPC_MovePrevious.Location = new System.Drawing.Point(0, 153);
            this.btnPC_MovePrevious.Name = "btnPC_MovePrevious";
            this.btnPC_MovePrevious.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MovePrevious.TabIndex = 4;
            this.btnPC_MovePrevious.UseVisualStyleBackColor = false;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(0, 148);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 5);
            this.label20.TabIndex = 110;
            // 
            // btnPC_MoveNext
            // 
            this.btnPC_MoveNext.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveNext.BackgroundImage")));
            this.btnPC_MoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveNext.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveNext.Image")));
            this.btnPC_MoveNext.Location = new System.Drawing.Point(0, 123);
            this.btnPC_MoveNext.Name = "btnPC_MoveNext";
            this.btnPC_MoveNext.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveNext.TabIndex = 3;
            this.btnPC_MoveNext.UseVisualStyleBackColor = false;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(0, 118);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 5);
            this.label21.TabIndex = 112;
            // 
            // btnPC_MoveLast
            // 
            this.btnPC_MoveLast.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveLast.BackgroundImage")));
            this.btnPC_MoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveLast.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveLast.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveLast.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveLast.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveLast.Image")));
            this.btnPC_MoveLast.Location = new System.Drawing.Point(0, 93);
            this.btnPC_MoveLast.Name = "btnPC_MoveLast";
            this.btnPC_MoveLast.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveLast.TabIndex = 2;
            this.btnPC_MoveLast.UseVisualStyleBackColor = false;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(0, 88);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 5);
            this.label24.TabIndex = 118;
            // 
            // btnPC_PrintCards
            // 
            this.btnPC_PrintCards.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_PrintCards.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_PrintCards.BackgroundImage")));
            this.btnPC_PrintCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_PrintCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_PrintCards.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_PrintCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_PrintCards.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_PrintCards.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_PrintCards.Image")));
            this.btnPC_PrintCards.Location = new System.Drawing.Point(0, 63);
            this.btnPC_PrintCards.Name = "btnPC_PrintCards";
            this.btnPC_PrintCards.Size = new System.Drawing.Size(38, 25);
            this.btnPC_PrintCards.TabIndex = 0;
            this.btnPC_PrintCards.UseVisualStyleBackColor = false;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Dock = System.Windows.Forms.DockStyle.Top;
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.Black;
            this.label60.Location = new System.Drawing.Point(0, 57);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(38, 6);
            this.label60.TabIndex = 120;
            // 
            // btnPC_DeleteCard
            // 
            this.btnPC_DeleteCard.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_DeleteCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_DeleteCard.BackgroundImage")));
            this.btnPC_DeleteCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_DeleteCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_DeleteCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_DeleteCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_DeleteCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_DeleteCard.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_DeleteCard.Image")));
            this.btnPC_DeleteCard.Location = new System.Drawing.Point(0, 32);
            this.btnPC_DeleteCard.Name = "btnPC_DeleteCard";
            this.btnPC_DeleteCard.Size = new System.Drawing.Size(38, 25);
            this.btnPC_DeleteCard.TabIndex = 1;
            this.btnPC_DeleteCard.UseVisualStyleBackColor = false;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(0, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 5);
            this.label22.TabIndex = 115;
            // 
            // btnPC_ScanCard
            // 
            this.btnPC_ScanCard.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_ScanCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_ScanCard.BackgroundImage")));
            this.btnPC_ScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_ScanCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_ScanCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_ScanCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_ScanCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_ScanCard.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_ScanCard.Image")));
            this.btnPC_ScanCard.Location = new System.Drawing.Point(0, 2);
            this.btnPC_ScanCard.Name = "btnPC_ScanCard";
            this.btnPC_ScanCard.Size = new System.Drawing.Size(38, 25);
            this.btnPC_ScanCard.TabIndex = 0;
            this.btnPC_ScanCard.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 2);
            this.label5.TabIndex = 100;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 2);
            this.label8.TabIndex = 99;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(360, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(9, 229);
            this.label15.TabIndex = 99;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(9, 229);
            this.label12.TabIndex = 107;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(8, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(361, 3);
            this.label13.TabIndex = 101;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(8, 232);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(361, 4);
            this.label14.TabIndex = 100;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(8, 236);
            this.label23.TabIndex = 98;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.lblScannedDate);
            this.panel22.Controls.Add(this.label67);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel22.Location = new System.Drawing.Point(3, 239);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(369, 22);
            this.panel22.TabIndex = 111;
            // 
            // lblScannedDate
            // 
            this.lblScannedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblScannedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScannedDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScannedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblScannedDate.Location = new System.Drawing.Point(0, 0);
            this.lblScannedDate.Name = "lblScannedDate";
            this.lblScannedDate.Size = new System.Drawing.Size(316, 22);
            this.lblScannedDate.TabIndex = 110;
            this.lblScannedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Dock = System.Windows.Forms.DockStyle.Right;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Location = new System.Drawing.Point(316, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(53, 22);
            this.label67.TabIndex = 111;
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(927, 53);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEligibilityCheck,
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(927, 53);
            this.ts_Commands.TabIndex = 21;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsbEligibilityCheck
            // 
            this.tsbEligibilityCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbEligibilityCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbEligibilityCheck.Image")));
            this.tsbEligibilityCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEligibilityCheck.Name = "tsbEligibilityCheck";
            this.tsbEligibilityCheck.Size = new System.Drawing.Size(65, 50);
            this.tsbEligibilityCheck.Tag = "Eligibility";
            this.tsbEligibilityCheck.Text = "&Eligibility";
            this.tsbEligibilityCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEligibilityCheck.Click += new System.EventHandler(this.tsbEligibilityCheck_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
            // c1Response
            // 
            this.c1Response.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Response.AllowEditing = false;
            this.c1Response.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this.c1Response.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Response.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Response.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Response.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Response.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Response.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Response.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Response.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Response.Location = new System.Drawing.Point(3, 0);
            this.c1Response.Name = "c1Response";
            this.c1Response.Padding = new System.Windows.Forms.Padding(2);
            this.c1Response.Rows.Count = 1;
            this.c1Response.Rows.DefaultSize = 19;
            this.c1Response.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Response.Size = new System.Drawing.Size(921, 138);
            this.c1Response.StyleInfo = resources.GetString("c1Response.StyleInfo");
            this.c1Response.TabIndex = 0;
            this.c1Response.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Response_AfterResizeColumn);
            this.c1Response.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Response_MouseMove);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label58);
            this.panel7.Controls.Add(this.label54);
            this.panel7.Controls.Add(this.label44);
            this.panel7.Controls.Add(this.label37);
            this.panel7.Controls.Add(this.c1Response);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 515);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel7.Size = new System.Drawing.Size(927, 141);
            this.panel7.TabIndex = 122;
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(4, 137);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(919, 1);
            this.label58.TabIndex = 60;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Top;
            this.label54.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(4, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(919, 1);
            this.label54.TabIndex = 59;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(923, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 138);
            this.label44.TabIndex = 58;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(3, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 138);
            this.label37.TabIndex = 57;
            // 
            // pnlPatStripBottom
            // 
            this.pnlPatStripBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatStripBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatStripBottom.Controls.Add(this.panel3);
            this.pnlPatStripBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatStripBottom.Location = new System.Drawing.Point(0, 83);
            this.pnlPatStripBottom.Name = "pnlPatStripBottom";
            this.pnlPatStripBottom.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlPatStripBottom.Size = new System.Drawing.Size(927, 149);
            this.pnlPatStripBottom.TabIndex = 124;
            // 
            // lblPayerID
            // 
            this.lblPayerID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayerID.AutoEllipsis = true;
            this.lblPayerID.AutoSize = true;
            this.lblPayerID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayerID.Location = new System.Drawing.Point(225, 39);
            this.lblPayerID.Name = "lblPayerID";
            this.lblPayerID.Size = new System.Drawing.Size(53, 14);
            this.lblPayerID.TabIndex = 27;
            this.lblPayerID.Text = "Payer ID";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.AutoSize = true;
            this.label25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(13, 13);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(129, 14);
            this.label25.TabIndex = 14;
            this.label25.Text = "Last eEligibility Check :";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.AutoSize = true;
            this.label27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(97, 39);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(45, 14);
            this.label27.TabIndex = 16;
            this.label27.Text = "Payer :";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoEllipsis = true;
            this.label33.AutoSize = true;
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(479, 13);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(117, 14);
            this.label33.TabIndex = 17;
            this.label33.Text = "Insured/Subscriber :";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoEllipsis = true;
            this.label34.AutoSize = true;
            this.label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(42, 65);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(100, 14);
            this.label34.TabIndex = 18;
            this.label34.Text = "Insurance Type :";
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label35.AutoEllipsis = true;
            this.label35.AutoSize = true;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(467, 62);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(130, 14);
            this.label35.TabIndex = 19;
            this.label35.Text = "Primary Care Provider :";
            // 
            // labelPayer
            // 
            this.labelPayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPayer.AutoEllipsis = true;
            this.labelPayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelPayer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPayer.Location = new System.Drawing.Point(146, 39);
            this.labelPayer.Name = "labelPayer";
            this.labelPayer.Size = new System.Drawing.Size(260, 14);
            this.labelPayer.TabIndex = 20;
            this.labelPayer.Text = "Payer Name";
            // 
            // lblInsuranceType
            // 
            this.lblInsuranceType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuranceType.AutoEllipsis = true;
            this.lblInsuranceType.AutoSize = true;
            this.lblInsuranceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuranceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuranceType.Location = new System.Drawing.Point(146, 65);
            this.lblInsuranceType.Name = "lblInsuranceType";
            this.lblInsuranceType.Size = new System.Drawing.Size(92, 14);
            this.lblInsuranceType.TabIndex = 21;
            this.lblInsuranceType.Text = "Insurance Type";
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoEllipsis = true;
            this.lblName.AutoSize = true;
            this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(596, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 14);
            this.lblName.TabIndex = 23;
            this.lblName.Text = "Name";
            // 
            // lblSubscbrDOB
            // 
            this.lblSubscbrDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscbrDOB.AutoEllipsis = true;
            this.lblSubscbrDOB.AutoSize = true;
            this.lblSubscbrDOB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscbrDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscbrDOB.Location = new System.Drawing.Point(596, 63);
            this.lblSubscbrDOB.Name = "lblSubscbrDOB";
            this.lblSubscbrDOB.Size = new System.Drawing.Size(31, 14);
            this.lblSubscbrDOB.TabIndex = 24;
            this.lblSubscbrDOB.Text = "DOB";
            this.lblSubscbrDOB.Visible = false;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.AutoEllipsis = true;
            this.lblID.AutoSize = true;
            this.lblID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(596, 39);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 14);
            this.lblID.TabIndex = 25;
            this.lblID.Text = "Id";
            // 
            // lblDateTimeofLstRsp
            // 
            this.lblDateTimeofLstRsp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTimeofLstRsp.AutoEllipsis = true;
            this.lblDateTimeofLstRsp.AutoSize = true;
            this.lblDateTimeofLstRsp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDateTimeofLstRsp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTimeofLstRsp.Location = new System.Drawing.Point(146, 13);
            this.lblDateTimeofLstRsp.Name = "lblDateTimeofLstRsp";
            this.lblDateTimeofLstRsp.Size = new System.Drawing.Size(157, 14);
            this.lblDateTimeofLstRsp.TabIndex = 26;
            this.lblDateTimeofLstRsp.Text = "DateTime of Last Response";
            // 
            // lblPrimsryCareProvider
            // 
            this.lblPrimsryCareProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrimsryCareProvider.AutoEllipsis = true;
            this.lblPrimsryCareProvider.AutoSize = true;
            this.lblPrimsryCareProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPrimsryCareProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimsryCareProvider.Location = new System.Drawing.Point(146, 91);
            this.lblPrimsryCareProvider.Name = "lblPrimsryCareProvider";
            this.lblPrimsryCareProvider.Size = new System.Drawing.Size(0, 14);
            this.lblPrimsryCareProvider.TabIndex = 28;
            // 
            // lblastRewdDateTime
            // 
            this.lblastRewdDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblastRewdDateTime.AutoEllipsis = true;
            this.lblastRewdDateTime.AutoSize = true;
            this.lblastRewdDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblastRewdDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblastRewdDateTime.Location = new System.Drawing.Point(729, 90);
            this.lblastRewdDateTime.Name = "lblastRewdDateTime";
            this.lblastRewdDateTime.Size = new System.Drawing.Size(179, 14);
            this.lblastRewdDateTime.TabIndex = 29;
            this.lblastRewdDateTime.Text = "<Last Received DateAndTime>";
            this.lblastRewdDateTime.Visible = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 109);
            this.label11.TabIndex = 62;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(923, 3);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 109);
            this.label43.TabIndex = 63;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(4, 3);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(919, 1);
            this.label48.TabIndex = 64;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(4, 111);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(919, 1);
            this.label57.TabIndex = 65;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(641, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 14);
            this.label3.TabIndex = 66;
            this.label3.Text = "Last Received :";
            this.label3.Visible = false;
            // 
            // lblBenefitAsOfDate
            // 
            this.lblBenefitAsOfDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBenefitAsOfDate.AutoEllipsis = true;
            this.lblBenefitAsOfDate.AutoSize = true;
            this.lblBenefitAsOfDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBenefitAsOfDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBenefitAsOfDate.Location = new System.Drawing.Point(548, 90);
            this.lblBenefitAsOfDate.Name = "lblBenefitAsOfDate";
            this.lblBenefitAsOfDate.Size = new System.Drawing.Size(67, 14);
            this.lblBenefitAsOfDate.TabIndex = 67;
            this.lblBenefitAsOfDate.Text = "TodayDate";
            this.lblBenefitAsOfDate.Visible = false;
            // 
            // label74
            // 
            this.label74.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label74.AutoSize = true;
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Location = new System.Drawing.Point(509, 38);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(87, 14);
            this.label74.TabIndex = 92;
            this.label74.Text = "Subscriber ID :";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblErrorNote);
            this.panel6.Controls.Add(this.label75);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.lblPrimaryCareProvider);
            this.panel6.Controls.Add(this.label73);
            this.panel6.Controls.Add(this.label74);
            this.panel6.Controls.Add(this.lblBenefitAsOfDate);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label57);
            this.panel6.Controls.Add(this.label48);
            this.panel6.Controls.Add(this.label43);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.lblastRewdDateTime);
            this.panel6.Controls.Add(this.lblPrimsryCareProvider);
            this.panel6.Controls.Add(this.lblDateTimeofLstRsp);
            this.panel6.Controls.Add(this.lblID);
            this.panel6.Controls.Add(this.lblSubscbrDOB);
            this.panel6.Controls.Add(this.lblName);
            this.panel6.Controls.Add(this.lblInsuranceType);
            this.panel6.Controls.Add(this.labelPayer);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.lblPayerID);
            this.panel6.Controls.Add(this.label36);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(0, 400);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(927, 115);
            this.panel6.TabIndex = 3;
            // 
            // lblErrorNote
            // 
            this.lblErrorNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblErrorNote.AutoEllipsis = true;
            this.lblErrorNote.AutoSize = true;
            this.lblErrorNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblErrorNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblErrorNote.Location = new System.Drawing.Point(50, 91);
            this.lblErrorNote.Name = "lblErrorNote";
            this.lblErrorNote.Size = new System.Drawing.Size(66, 14);
            this.lblErrorNote.TabIndex = 96;
            this.lblErrorNote.Text = "ErrorNote";
            // 
            // label75
            // 
            this.label75.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label75.AutoEllipsis = true;
            this.label75.AutoSize = true;
            this.label75.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(7, 91);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(42, 14);
            this.label75.TabIndex = 95;
            this.label75.Text = "Note :";
            // 
            // lblPrimaryCareProvider
            // 
            this.lblPrimaryCareProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrimaryCareProvider.AutoEllipsis = true;
            this.lblPrimaryCareProvider.AutoSize = true;
            this.lblPrimaryCareProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPrimaryCareProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryCareProvider.Location = new System.Drawing.Point(599, 63);
            this.lblPrimaryCareProvider.Name = "lblPrimaryCareProvider";
            this.lblPrimaryCareProvider.Size = new System.Drawing.Size(130, 14);
            this.lblPrimaryCareProvider.TabIndex = 94;
            this.lblPrimaryCareProvider.Text = "Primary Care Provider :";
            // 
            // label73
            // 
            this.label73.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label73.AutoSize = true;
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Location = new System.Drawing.Point(499, 63);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(99, 14);
            this.label73.TabIndex = 93;
            this.label73.Text = "Subscriber DOB :";
            this.label73.Visible = false;
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoEllipsis = true;
            this.label36.AutoSize = true;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(456, 90);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(89, 14);
            this.label36.TabIndex = 22;
            this.label36.Text = "Benefits as of :";
            this.label36.Visible = false;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.panel16);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 132);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel17.Size = new System.Drawing.Size(927, 26);
            this.panel17.TabIndex = 126;
            // 
            // panel16
            // 
            this.panel16.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.btnInsuranceDown);
            this.panel16.Controls.Add(this.btnInsuranceUp);
            this.panel16.Controls.Add(this.label81);
            this.panel16.Controls.Add(this.label76);
            this.panel16.Controls.Add(this.label78);
            this.panel16.Controls.Add(this.label79);
            this.panel16.Controls.Add(this.label80);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(3, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(921, 26);
            this.panel16.TabIndex = 125;
            // 
            // btnInsuranceDown
            // 
            this.btnInsuranceDown.BackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInsuranceDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInsuranceDown.FlatAppearance.BorderSize = 0;
            this.btnInsuranceDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsuranceDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnInsuranceDown.ForeColor = System.Drawing.Color.Black;
            this.btnInsuranceDown.Image = ((System.Drawing.Image)(resources.GetObject("btnInsuranceDown.Image")));
            this.btnInsuranceDown.Location = new System.Drawing.Point(874, 1);
            this.btnInsuranceDown.Name = "btnInsuranceDown";
            this.btnInsuranceDown.Size = new System.Drawing.Size(23, 24);
            this.btnInsuranceDown.TabIndex = 111;
            this.btnInsuranceDown.UseVisualStyleBackColor = false;
            this.btnInsuranceDown.Visible = false;
            this.btnInsuranceDown.Click += new System.EventHandler(this.btnInsuranceDown_Click);
            // 
            // btnInsuranceUp
            // 
            this.btnInsuranceUp.BackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInsuranceUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnInsuranceUp.FlatAppearance.BorderSize = 0;
            this.btnInsuranceUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInsuranceUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsuranceUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnInsuranceUp.ForeColor = System.Drawing.Color.Black;
            this.btnInsuranceUp.Image = ((System.Drawing.Image)(resources.GetObject("btnInsuranceUp.Image")));
            this.btnInsuranceUp.Location = new System.Drawing.Point(897, 1);
            this.btnInsuranceUp.Name = "btnInsuranceUp";
            this.btnInsuranceUp.Size = new System.Drawing.Size(23, 24);
            this.btnInsuranceUp.TabIndex = 112;
            this.btnInsuranceUp.UseVisualStyleBackColor = false;
            this.btnInsuranceUp.Click += new System.EventHandler(this.btnInsuranceUp_Click);
            // 
            // label81
            // 
            this.label81.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label81.AutoEllipsis = true;
            this.label81.AutoSize = true;
            this.label81.BackColor = System.Drawing.Color.Transparent;
            this.label81.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(8, 5);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(120, 14);
            this.label81.TabIndex = 62;
            this.label81.Text = "Insurance Details :";
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label76.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(1, 25);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(919, 1);
            this.label76.TabIndex = 12;
            this.label76.Text = "label1";
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Top;
            this.label78.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(1, 0);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(919, 1);
            this.label78.TabIndex = 11;
            this.label78.Text = "label1";
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Left;
            this.label79.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(0, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1, 26);
            this.label79.TabIndex = 10;
            this.label79.Text = "label1";
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Dock = System.Windows.Forms.DockStyle.Right;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(920, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(1, 26);
            this.label80.TabIndex = 9;
            this.label80.Text = "label1";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.panel7);
            this.panel18.Controls.Add(this.panel6);
            this.panel18.Controls.Add(this.panel2);
            this.panel18.Controls.Add(this.panel17);
            this.panel18.Controls.Add(this.panel5);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(927, 656);
            this.panel18.TabIndex = 118;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlHeaderInner);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 53);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlHeader.Size = new System.Drawing.Size(927, 30);
            this.pnlHeader.TabIndex = 11;
            // 
            // pnlHeaderInner
            // 
            this.pnlHeaderInner.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderInner.BackgroundImage = global::gloPMGeneral.Properties.Resources.MedicalCategoryImages_5_TopOrange;
            this.pnlHeaderInner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeaderInner.Controls.Add(this.btnDown);
            this.pnlHeaderInner.Controls.Add(this.btnUp);
            this.pnlHeaderInner.Controls.Add(this.btn_ModifyPatient);
            this.pnlHeaderInner.Controls.Add(this.lblPatientCode);
            this.pnlHeaderInner.Controls.Add(this.lblPatientCodeCaption);
            this.pnlHeaderInner.Controls.Add(this.lblGender);
            this.pnlHeaderInner.Controls.Add(this.lblGenderCaption);
            this.pnlHeaderInner.Controls.Add(this.lblDOB);
            this.pnlHeaderInner.Controls.Add(this.lblDOBCaption);
            this.pnlHeaderInner.Controls.Add(this.lblPatientName);
            this.pnlHeaderInner.Controls.Add(this.lblViewBenefitTopHeader);
            this.pnlHeaderInner.Controls.Add(this.lblViewBenefitLeftHeader);
            this.pnlHeaderInner.Controls.Add(this.lblViewBenefitRightHeader);
            this.pnlHeaderInner.Controls.Add(this.lblViewBenefitBottomHeader);
            this.pnlHeaderInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderInner.Location = new System.Drawing.Point(3, 3);
            this.pnlHeaderInner.Name = "pnlHeaderInner";
            this.pnlHeaderInner.Size = new System.Drawing.Size(921, 27);
            this.pnlHeaderInner.TabIndex = 123;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDown.ForeColor = System.Drawing.Color.Black;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(874, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 25);
            this.btnDown.TabIndex = 1;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Visible = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnUp.ForeColor = System.Drawing.Color.Black;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(897, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 25);
            this.btnUp.TabIndex = 10;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Visible = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btn_ModifyPatient
            // 
            this.btn_ModifyPatient.BackColor = System.Drawing.Color.Transparent;
            this.btn_ModifyPatient.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_ModifyPatient.FlatAppearance.BorderSize = 0;
            this.btn_ModifyPatient.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_ModifyPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_ModifyPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ModifyPatient.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ModifyPatient.Location = new System.Drawing.Point(527, 1);
            this.btn_ModifyPatient.Name = "btn_ModifyPatient";
            this.btn_ModifyPatient.Size = new System.Drawing.Size(29, 25);
            this.btn_ModifyPatient.TabIndex = 0;
            this.btn_ModifyPatient.UseVisualStyleBackColor = false;
            this.btn_ModifyPatient.Click += new System.EventHandler(this.btn_ModifyPatient_Click);
            this.btn_ModifyPatient.MouseLeave += new System.EventHandler(this.btn_ModityPatient_MouseLeave);
            this.btn_ModifyPatient.MouseHover += new System.EventHandler(this.btn_ModityPatient_MouseHover);
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.AutoSize = true;
            this.lblPatientCode.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientCode.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCode.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCode.Location = new System.Drawing.Point(427, 1);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblPatientCode.Size = new System.Drawing.Size(100, 21);
            this.lblPatientCode.TabIndex = 6;
            this.lblPatientCode.Text = "                       ";
            // 
            // lblPatientCodeCaption
            // 
            this.lblPatientCodeCaption.AutoSize = true;
            this.lblPatientCodeCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientCodeCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientCodeCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCodeCaption.ForeColor = System.Drawing.Color.Black;
            this.lblPatientCodeCaption.Location = new System.Drawing.Point(342, 1);
            this.lblPatientCodeCaption.Name = "lblPatientCodeCaption";
            this.lblPatientCodeCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblPatientCodeCaption.Size = new System.Drawing.Size(85, 21);
            this.lblPatientCodeCaption.TabIndex = 5;
            this.lblPatientCodeCaption.Text = "Patient Code:";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.BackColor = System.Drawing.Color.Transparent;
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGender.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.Color.Black;
            this.lblGender.Location = new System.Drawing.Point(278, 1);
            this.lblGender.Name = "lblGender";
            this.lblGender.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGender.Size = new System.Drawing.Size(64, 21);
            this.lblGender.TabIndex = 4;
            this.lblGender.Text = "              ";
            // 
            // lblGenderCaption
            // 
            this.lblGenderCaption.AutoSize = true;
            this.lblGenderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblGenderCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGenderCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblGenderCaption.Location = new System.Drawing.Point(224, 1);
            this.lblGenderCaption.Name = "lblGenderCaption";
            this.lblGenderCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGenderCaption.Size = new System.Drawing.Size(54, 21);
            this.lblGenderCaption.TabIndex = 3;
            this.lblGenderCaption.Text = "Gender:";
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.BackColor = System.Drawing.Color.Transparent;
            this.lblDOB.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOB.ForeColor = System.Drawing.Color.Black;
            this.lblDOB.Location = new System.Drawing.Point(97, 1);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblDOB.Size = new System.Drawing.Size(127, 19);
            this.lblDOB.TabIndex = 2;
            this.lblDOB.Text = "                              ";
            this.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDOBCaption
            // 
            this.lblDOBCaption.AutoSize = true;
            this.lblDOBCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDOBCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDOBCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOBCaption.ForeColor = System.Drawing.Color.Black;
            this.lblDOBCaption.Location = new System.Drawing.Point(60, 1);
            this.lblDOBCaption.Name = "lblDOBCaption";
            this.lblDOBCaption.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblDOBCaption.Size = new System.Drawing.Size(37, 21);
            this.lblDOBCaption.TabIndex = 1;
            this.lblDOBCaption.Text = "DOB:";
            this.lblDOBCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Black;
            this.lblPatientName.Location = new System.Drawing.Point(1, 1);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Padding = new System.Windows.Forms.Padding(3, 5, 0, 0);
            this.lblPatientName.Size = new System.Drawing.Size(59, 21);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "            ";
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblViewBenefitTopHeader
            // 
            this.lblViewBenefitTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblViewBenefitTopHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitTopHeader.Location = new System.Drawing.Point(1, 0);
            this.lblViewBenefitTopHeader.Name = "lblViewBenefitTopHeader";
            this.lblViewBenefitTopHeader.Size = new System.Drawing.Size(919, 1);
            this.lblViewBenefitTopHeader.TabIndex = 4;
            // 
            // lblViewBenefitLeftHeader
            // 
            this.lblViewBenefitLeftHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitLeftHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblViewBenefitLeftHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.lblViewBenefitLeftHeader.Name = "lblViewBenefitLeftHeader";
            this.lblViewBenefitLeftHeader.Size = new System.Drawing.Size(1, 26);
            this.lblViewBenefitLeftHeader.TabIndex = 6;
            // 
            // lblViewBenefitRightHeader
            // 
            this.lblViewBenefitRightHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitRightHeader.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblViewBenefitRightHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitRightHeader.Location = new System.Drawing.Point(920, 0);
            this.lblViewBenefitRightHeader.Name = "lblViewBenefitRightHeader";
            this.lblViewBenefitRightHeader.Size = new System.Drawing.Size(1, 26);
            this.lblViewBenefitRightHeader.TabIndex = 7;
            // 
            // lblViewBenefitBottomHeader
            // 
            this.lblViewBenefitBottomHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitBottomHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblViewBenefitBottomHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(142)))), ((int)(((byte)(123)))));
            this.lblViewBenefitBottomHeader.Location = new System.Drawing.Point(0, 26);
            this.lblViewBenefitBottomHeader.Name = "lblViewBenefitBottomHeader";
            this.lblViewBenefitBottomHeader.Size = new System.Drawing.Size(921, 1);
            this.lblViewBenefitBottomHeader.TabIndex = 5;
            // 
            // pnlFiller
            // 
            this.pnlFiller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlFiller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFiller.Location = new System.Drawing.Point(0, 0);
            this.pnlFiller.MaximumSize = new System.Drawing.Size(927, 1);
            this.pnlFiller.MinimumSize = new System.Drawing.Size(927, 1);
            this.pnlFiller.Name = "pnlFiller";
            this.pnlFiller.Size = new System.Drawing.Size(927, 1);
            this.pnlFiller.TabIndex = 61;
            // 
            // panel19
            // 
            this.panel19.AutoScroll = true;
            this.panel19.Controls.Add(this.panel18);
            this.panel19.Controls.Add(this.pnlFiller);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(0, 232);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(927, 662);
            this.panel19.TabIndex = 118;
            // 
            // frmViewBenefit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(927, 894);
            this.Controls.Add(this.panel19);
            this.Controls.Add(this.pnlPatStripBottom);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewBenefit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Insurance Benefits";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewBenefit_FormClosing);
            this.Load += new System.EventHandler(this.frmViewBenefit_Load);
            this.panel3.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlAlertsNotesProvider.ResumeLayout(false);
            this.pnlAlertsNotesProvider.PerformLayout();
            this.pnlPatientNotes.ResumeLayout(false);
            this.pnlPatientNotes.PerformLayout();
            this.pnlMedCategory.ResumeLayout(false);
            this.pnlMedCategory.PerformLayout();
            this.pnlPatient.ResumeLayout(false);
            this.pnlPatient.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.pnlAlertNextAppt.ResumeLayout(false);
            this.pnlAlertNextAppt.PerformLayout();
            this.pnlEMRAlertsCaption.ResumeLayout(false);
            this.pnlEMRAlertsCaption.PerformLayout();
            this.pnlAccountDetails.ResumeLayout(false);
            this.pnlAccountDetails.PerformLayout();
            this.pnlPatientPhoto.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPAPhoto)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPC_Cards)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).EndInit();
            this.panel7.ResumeLayout(false);
            this.pnlPatStripBottom.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeaderInner.ResumeLayout(false);
            this.pnlHeaderInner.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDeductableAmount;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox chkMarkReviewed;
        private System.Windows.Forms.MaskedTextBox mskEndDate;
        private System.Windows.Forms.TextBox txtCoveragePercent;
        private System.Windows.Forms.TextBox txtPatientRecordedBenefitsNote;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblEmployer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelContact;
        private System.Windows.Forms.Label lblInsuranceName;
        private System.Windows.Forms.Label lbleligibilityContact;
        private System.Windows.Forms.Label label39;
      //  private Janus.Windows.EditControls.UIGroupBox gb_Cards;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.PictureBox picPC_Cards;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPC_MoveFirst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPC_MovePrevious;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnPC_MoveNext;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnPC_MoveLast;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnPC_PrintCards;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Button btnPC_DeleteCard;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnPC_ScanCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label lblScannedDate;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button InsCardMovePrevious;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button InsCardMoveNext;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button InsCardMoveLast;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Response;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlMedCategory;
        private System.Windows.Forms.Panel pnlAlertsNotesProvider;
        private System.Windows.Forms.Label lblAlerts;
        private System.Windows.Forms.Label lblAlertsCap;
        private System.Windows.Forms.Button btn_Alerts;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.Label lblDemoCopay;
        private System.Windows.Forms.Label lblDemoProvider;
        private System.Windows.Forms.Label lblDemoCopayCaption;
        private System.Windows.Forms.Label lblDemoProviderCaption;
        private System.Windows.Forms.Panel pnlPatient;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.Label lblDemoPatientCaption;
        private System.Windows.Forms.Label lblDemoPatient;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lblSubsroberName;
        private System.Windows.Forms.Label lblSubsroberNameCaption;
        private System.Windows.Forms.Label lblDemoLastPatPayment;
        private System.Windows.Forms.Panel pnlAccountDetails;
        private System.Windows.Forms.Label lblcmbInsuranceCaption;
        private System.Windows.Forms.Panel pnlPatientPhoto;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.PictureBox picPAPhoto;
        private System.Windows.Forms.Button InsCardMovefirst;
        private System.Windows.Forms.Label lblSubscriberDOB;
        private System.Windows.Forms.Label lblSubscriberID;
        private System.Windows.Forms.TextBox txtCopay;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ToolTip toolTip1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Label lblEligibilityInsuranceName;
        private gloPictureBox.gloPictureBox picPatient_Cards;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TrackBar TrackBar;
        private System.Windows.Forms.Button TrackbarPlus;
        private System.Windows.Forms.Button TrackbarMinus;
        private System.Windows.Forms.Label lblPlanEligibilityNote;
        private System.Windows.Forms.LinkLabel lblClickable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlPatientNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblUserAndDateTime;
        private System.Windows.Forms.Label lblLastChanged;
        private System.Windows.Forms.Label lblViewBenefitBottom;
        private System.Windows.Forms.Label lblViewBenefitRight;
        private System.Windows.Forms.Label lblViewBenefitLeft;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel pnlPatStripBottom;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label label72;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.Label lblViewBenefitBottomHeader;
        private System.Windows.Forms.Label lblViewBenefitRightHeader;
        private System.Windows.Forms.Label lblViewBenefitLeftHeader;
        private System.Windows.Forms.Label lblViewBenefitTopHeader;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblDOBCaption;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblGenderCaption;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblPatientCodeCaption;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Button btn_ModifyPatient;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Panel pnlHeaderInner;
        private System.Windows.Forms.Label lblSubscriberDOBCaption;
        private System.Windows.Forms.Label lblSubscriberIDCaption;
        private System.Windows.Forms.Label lblPayerID;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label labelPayer;
        private System.Windows.Forms.Label lblInsuranceType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSubscbrDOB;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDateTimeofLstRsp;
        private System.Windows.Forms.Label lblPrimsryCareProvider;
        private System.Windows.Forms.Label lblastRewdDateTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBenefitAsOfDate;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblPateintBenefitUptoDate;
        private System.Windows.Forms.Label lblPateintBenefitUptoDateNew;
        private System.Windows.Forms.Label lblLastchngedDatetime;
        private System.Windows.Forms.Label lblPrimaryCareProvider;
        private System.Windows.Forms.Label lblInactiveInsurance;
        private System.Windows.Forms.Label lblScanDate;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label lblErrorNote;
        internal System.Windows.Forms.ToolStripButton tsbEligibilityCheck;
        private System.Windows.Forms.Button btnInsuranceUp;
        private System.Windows.Forms.Button btnInsuranceDown;
        internal System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFiller;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label lblDemoMedCat;
        private System.Windows.Forms.Label lblDemoMedCatCaption;
        private System.Windows.Forms.Panel pnlAlertNextAppt;
        private System.Windows.Forms.Panel pnlEMRAlertsCaption;
        private System.Windows.Forms.Label lblDemoEMRAlerts;
        private System.Windows.Forms.Label lblDemogloEMRAlertsCaption;
        private System.Windows.Forms.Label lblDemoNextApptCaption;
        private System.Windows.Forms.Label lblDemoNextAppt;
    
    }
}
