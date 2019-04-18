namespace ChargeRules
{
    partial class frmClaimRuleVerification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClaimRuleVerification));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.tls_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlHD = new System.Windows.Forms.Panel();
            this.pnlChargeGrid = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1Charge = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label55 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTransactionMasterInfo = new System.Windows.Forms.Panel();
            this.pnlHospitalisationDates = new System.Windows.Forms.Panel();
            this.txtPriorAuthorization = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mskHospitaliztionFrom = new System.Windows.Forms.MaskedTextBox();
            this.lblHopitalizationDateFrom = new System.Windows.Forms.Label();
            this.lblHospitaliztionDateTo = new System.Windows.Forms.Label();
            this.mskHospitaliztionTo = new System.Windows.Forms.MaskedTextBox();
            this.pnlClaimDates = new System.Windows.Forms.Panel();
            this.mskClaimDate = new System.Windows.Forms.MaskedTextBox();
            this.cmbBox14DateQualifier = new System.Windows.Forms.ComboBox();
            this.lblClaimDateHyphen = new System.Windows.Forms.Label();
            this.cmbBox15DateQualifier = new System.Windows.Forms.ComboBox();
            this.mskOtherClaimDate = new System.Windows.Forms.MaskedTextBox();
            this.lblBox15Date = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lblOnsiteDate = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.pnlFacility = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.cmbBillingProvider = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.cmbReferralProvider = new System.Windows.Forms.ComboBox();
            this.cmbProviderType = new System.Windows.Forms.ComboBox();
            this.label84 = new System.Windows.Forms.Label();
            this.pnlPatient = new System.Windows.Forms.Panel();
            this.cmbRelationToSubscriber = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPatientAgeDays = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbPatientAgeMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPatientAgeYears = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlInsurannce = new System.Windows.Forms.Panel();
            this.cmbInsurancePlanName = new System.Windows.Forms.ComboBox();
            this.cmbInsuranceCompanyName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblSelectedText = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlHD.SuspendLayout();
            this.pnlChargeGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Charge)).BeginInit();
            this.pnlTransactionMasterInfo.SuspendLayout();
            this.pnlHospitalisationDates.SuspendLayout();
            this.pnlClaimDates.SuspendLayout();
            this.pnlFacility.SuspendLayout();
            this.pnlPatient.SuspendLayout();
            this.pnlInsurannce.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(861, 53);
            this.pnlToolStrip.TabIndex = 1;
            this.pnlToolStrip.Tag = "pnlToolStrip";
            // 
            // tls_Top
            // 
            this.tls_Top.BackColor = System.Drawing.Color.Transparent;
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnAddLine,
            this.tls_btnRemoveLine,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(861, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.TabStop = true;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.BackColor = System.Drawing.Color.Transparent;
            this.tls_btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.BackgroundImage")));
            this.tls_btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(59, 50);
            this.tls_btnOK.Tag = "Execute ";
            this.tls_btnOK.Text = "&Execute";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.ToolTipText = "Execute Against Rules";
            this.tls_btnOK.Click += new System.EventHandler(this.tls_Execute_Click);
            // 
            // tls_btnAddLine
            // 
            this.tls_btnAddLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnAddLine.BackgroundImage")));
            this.tls_btnAddLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnAddLine.Image")));
            this.tls_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnAddLine.Name = "tls_btnAddLine";
            this.tls_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.tls_btnAddLine.Tag = "AddLine";
            this.tls_btnAddLine.Text = "&Add Line";
            this.tls_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnAddLine.Click += new System.EventHandler(this.tls_btnAddLine_Click);
            // 
            // tls_btnRemoveLine
            // 
            this.tls_btnRemoveLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnRemoveLine.BackgroundImage")));
            this.tls_btnRemoveLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnRemoveLine.Image")));
            this.tls_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnRemoveLine.Name = "tls_btnRemoveLine";
            this.tls_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.tls_btnRemoveLine.Tag = "RemoveLine";
            this.tls_btnRemoveLine.Text = "Re&move Line";
            this.tls_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnRemoveLine.Click += new System.EventHandler(this.tls_btnRemoveLine_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.tls_btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.BackgroundImage")));
            this.tls_btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlHD
            // 
            this.pnlHD.Controls.Add(this.pnlChargeGrid);
            this.pnlHD.Controls.Add(this.splitter1);
            this.pnlHD.Controls.Add(this.pnlTransactionMasterInfo);
            this.pnlHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHD.Location = new System.Drawing.Point(0, 53);
            this.pnlHD.Name = "pnlHD";
            this.pnlHD.Size = new System.Drawing.Size(861, 617);
            this.pnlHD.TabIndex = 0;
            // 
            // pnlChargeGrid
            // 
            this.pnlChargeGrid.Controls.Add(this.pnlInternalControl);
            this.pnlChargeGrid.Controls.Add(this.c1Charge);
            this.pnlChargeGrid.Controls.Add(this.label55);
            this.pnlChargeGrid.Controls.Add(this.label44);
            this.pnlChargeGrid.Controls.Add(this.label43);
            this.pnlChargeGrid.Controls.Add(this.label42);
            this.pnlChargeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChargeGrid.Location = new System.Drawing.Point(0, 340);
            this.pnlChargeGrid.Name = "pnlChargeGrid";
            this.pnlChargeGrid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlChargeGrid.Size = new System.Drawing.Size(861, 277);
            this.pnlChargeGrid.TabIndex = 1;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(224, 40);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(300, 150);
            this.pnlInternalControl.TabIndex = 25;
            this.pnlInternalControl.Visible = false;
            // 
            // c1Charge
            // 
            this.c1Charge.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1Charge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Charge.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Charge.ColumnInfo = "15,0,0,0,0,105,Columns:";
            this.c1Charge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Charge.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1Charge.ExtendLastCol = true;
            this.c1Charge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Charge.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Charge.Location = new System.Drawing.Point(4, 1);
            this.c1Charge.Name = "c1Charge";
            this.c1Charge.Padding = new System.Windows.Forms.Padding(3);
            this.c1Charge.Rows.Count = 2;
            this.c1Charge.Rows.DefaultSize = 21;
            this.c1Charge.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Charge.Size = new System.Drawing.Size(853, 272);
            this.c1Charge.StyleInfo = resources.GetString("c1Charge.StyleInfo");
            this.c1Charge.TabIndex = 0;
            this.c1Charge.TabStop = false;
            this.c1Charge.BeforeScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Charge_BeforeScroll);
            this.c1Charge.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Charge_AfterScroll);
            this.c1Charge.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Charge_StartEdit);
            this.c1Charge.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Charge_LeaveEdit);
            this.c1Charge.ChangeEdit += new System.EventHandler(this.c1Charge_ChangeEdit);
            this.c1Charge.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1Charge_KeyPressEdit);
            this.c1Charge.Click += new System.EventHandler(this.c1Charge_Click);
            this.c1Charge.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Charge_KeyUp);
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(3, 1);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 272);
            this.label55.TabIndex = 0;
            this.label55.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(857, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 272);
            this.label44.TabIndex = 0;
            this.label44.Text = "label4";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Top;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(3, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(855, 1);
            this.label43.TabIndex = 0;
            this.label43.Text = "label1";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(3, 273);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(855, 1);
            this.label42.TabIndex = 0;
            this.label42.Text = "label1";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 337);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(861, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // pnlTransactionMasterInfo
            // 
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlHospitalisationDates);
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlClaimDates);
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlFacility);
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlPatient);
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlInsurannce);
            this.pnlTransactionMasterInfo.Controls.Add(this.pnlHeader);
            this.pnlTransactionMasterInfo.Controls.Add(this.label14);
            this.pnlTransactionMasterInfo.Controls.Add(this.label15);
            this.pnlTransactionMasterInfo.Controls.Add(this.label17);
            this.pnlTransactionMasterInfo.Controls.Add(this.label18);
            this.pnlTransactionMasterInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTransactionMasterInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlTransactionMasterInfo.Name = "pnlTransactionMasterInfo";
            this.pnlTransactionMasterInfo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlTransactionMasterInfo.Size = new System.Drawing.Size(861, 337);
            this.pnlTransactionMasterInfo.TabIndex = 0;
            // 
            // pnlHospitalisationDates
            // 
            this.pnlHospitalisationDates.Controls.Add(this.txtPriorAuthorization);
            this.pnlHospitalisationDates.Controls.Add(this.label8);
            this.pnlHospitalisationDates.Controls.Add(this.mskHospitaliztionFrom);
            this.pnlHospitalisationDates.Controls.Add(this.lblHopitalizationDateFrom);
            this.pnlHospitalisationDates.Controls.Add(this.lblHospitaliztionDateTo);
            this.pnlHospitalisationDates.Controls.Add(this.mskHospitaliztionTo);
            this.pnlHospitalisationDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHospitalisationDates.Location = new System.Drawing.Point(4, 278);
            this.pnlHospitalisationDates.Name = "pnlHospitalisationDates";
            this.pnlHospitalisationDates.Size = new System.Drawing.Size(853, 58);
            this.pnlHospitalisationDates.TabIndex = 4;
            // 
            // txtPriorAuthorization
            // 
            this.txtPriorAuthorization.Location = new System.Drawing.Point(593, 5);
            this.txtPriorAuthorization.Name = "txtPriorAuthorization";
            this.txtPriorAuthorization.Size = new System.Drawing.Size(213, 22);
            this.txtPriorAuthorization.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(435, 9);
            this.label8.MaximumSize = new System.Drawing.Size(152, 14);
            this.label8.MinimumSize = new System.Drawing.Size(152, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(152, 14);
            this.label8.TabIndex = 13;
            this.label8.Text = "Prior Authorization :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskHospitaliztionFrom
            // 
            this.mskHospitaliztionFrom.Location = new System.Drawing.Point(205, 5);
            this.mskHospitaliztionFrom.Mask = "00/00/0000";
            this.mskHospitaliztionFrom.Name = "mskHospitaliztionFrom";
            this.mskHospitaliztionFrom.Size = new System.Drawing.Size(127, 22);
            this.mskHospitaliztionFrom.TabIndex = 0;
            this.mskHospitaliztionFrom.ValidatingType = typeof(System.DateTime);
            this.mskHospitaliztionFrom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskDate_MouseClick);
            this.mskHospitaliztionFrom.Validating += new System.ComponentModel.CancelEventHandler(this.mskDate_Validating);
            // 
            // lblHopitalizationDateFrom
            // 
            this.lblHopitalizationDateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHopitalizationDateFrom.AutoEllipsis = true;
            this.lblHopitalizationDateFrom.AutoSize = true;
            this.lblHopitalizationDateFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHopitalizationDateFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblHopitalizationDateFrom.Location = new System.Drawing.Point(50, 9);
            this.lblHopitalizationDateFrom.MaximumSize = new System.Drawing.Size(152, 14);
            this.lblHopitalizationDateFrom.MinimumSize = new System.Drawing.Size(152, 14);
            this.lblHopitalizationDateFrom.Name = "lblHopitalizationDateFrom";
            this.lblHopitalizationDateFrom.Size = new System.Drawing.Size(152, 14);
            this.lblHopitalizationDateFrom.TabIndex = 6;
            this.lblHopitalizationDateFrom.Text = "Hospitalization Date From :";
            this.lblHopitalizationDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHospitaliztionDateTo
            // 
            this.lblHospitaliztionDateTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHospitaliztionDateTo.AutoEllipsis = true;
            this.lblHospitaliztionDateTo.AutoSize = true;
            this.lblHospitaliztionDateTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHospitaliztionDateTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblHospitaliztionDateTo.Location = new System.Drawing.Point(50, 35);
            this.lblHospitaliztionDateTo.MaximumSize = new System.Drawing.Size(152, 14);
            this.lblHospitaliztionDateTo.MinimumSize = new System.Drawing.Size(152, 14);
            this.lblHospitaliztionDateTo.Name = "lblHospitaliztionDateTo";
            this.lblHospitaliztionDateTo.Size = new System.Drawing.Size(152, 14);
            this.lblHospitaliztionDateTo.TabIndex = 12;
            this.lblHospitaliztionDateTo.Text = "Hospitalization Date To :";
            this.lblHospitaliztionDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskHospitaliztionTo
            // 
            this.mskHospitaliztionTo.Location = new System.Drawing.Point(205, 30);
            this.mskHospitaliztionTo.Mask = "00/00/0000";
            this.mskHospitaliztionTo.Name = "mskHospitaliztionTo";
            this.mskHospitaliztionTo.Size = new System.Drawing.Size(127, 22);
            this.mskHospitaliztionTo.TabIndex = 1;
            this.mskHospitaliztionTo.ValidatingType = typeof(System.DateTime);
            this.mskHospitaliztionTo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskDate_MouseClick);
            this.mskHospitaliztionTo.Validating += new System.ComponentModel.CancelEventHandler(this.mskDate_Validating);
            // 
            // pnlClaimDates
            // 
            this.pnlClaimDates.Controls.Add(this.mskClaimDate);
            this.pnlClaimDates.Controls.Add(this.cmbBox14DateQualifier);
            this.pnlClaimDates.Controls.Add(this.lblClaimDateHyphen);
            this.pnlClaimDates.Controls.Add(this.cmbBox15DateQualifier);
            this.pnlClaimDates.Controls.Add(this.mskOtherClaimDate);
            this.pnlClaimDates.Controls.Add(this.lblBox15Date);
            this.pnlClaimDates.Controls.Add(this.label73);
            this.pnlClaimDates.Controls.Add(this.lblOnsiteDate);
            this.pnlClaimDates.Controls.Add(this.label85);
            this.pnlClaimDates.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlClaimDates.Location = new System.Drawing.Point(4, 223);
            this.pnlClaimDates.Name = "pnlClaimDates";
            this.pnlClaimDates.Size = new System.Drawing.Size(853, 55);
            this.pnlClaimDates.TabIndex = 3;
            // 
            // mskClaimDate
            // 
            this.mskClaimDate.Location = new System.Drawing.Point(205, 4);
            this.mskClaimDate.Mask = "00/00/0000";
            this.mskClaimDate.Name = "mskClaimDate";
            this.mskClaimDate.Size = new System.Drawing.Size(127, 22);
            this.mskClaimDate.TabIndex = 0;
            this.mskClaimDate.ValidatingType = typeof(System.DateTime);
            this.mskClaimDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskDate_MouseClick);
            this.mskClaimDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskDate_Validating);
            // 
            // cmbBox14DateQualifier
            // 
            this.cmbBox14DateQualifier.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBox14DateQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox14DateQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox14DateQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbBox14DateQualifier.FormattingEnabled = true;
            this.cmbBox14DateQualifier.Location = new System.Drawing.Point(347, 4);
            this.cmbBox14DateQualifier.Name = "cmbBox14DateQualifier";
            this.cmbBox14DateQualifier.Size = new System.Drawing.Size(240, 22);
            this.cmbBox14DateQualifier.TabIndex = 1;
            // 
            // lblClaimDateHyphen
            // 
            this.lblClaimDateHyphen.AutoSize = true;
            this.lblClaimDateHyphen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimDateHyphen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimDateHyphen.Location = new System.Drawing.Point(290, 8);
            this.lblClaimDateHyphen.MaximumSize = new System.Drawing.Size(100, 14);
            this.lblClaimDateHyphen.MinimumSize = new System.Drawing.Size(100, 14);
            this.lblClaimDateHyphen.Name = "lblClaimDateHyphen";
            this.lblClaimDateHyphen.Size = new System.Drawing.Size(100, 14);
            this.lblClaimDateHyphen.TabIndex = 30;
            this.lblClaimDateHyphen.Text = "-";
            this.lblClaimDateHyphen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbBox15DateQualifier
            // 
            this.cmbBox15DateQualifier.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBox15DateQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox15DateQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox15DateQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbBox15DateQualifier.FormattingEnabled = true;
            this.cmbBox15DateQualifier.Location = new System.Drawing.Point(347, 29);
            this.cmbBox15DateQualifier.Name = "cmbBox15DateQualifier";
            this.cmbBox15DateQualifier.Size = new System.Drawing.Size(240, 22);
            this.cmbBox15DateQualifier.TabIndex = 3;
            // 
            // mskOtherClaimDate
            // 
            this.mskOtherClaimDate.Location = new System.Drawing.Point(205, 29);
            this.mskOtherClaimDate.Mask = "00/00/0000";
            this.mskOtherClaimDate.Name = "mskOtherClaimDate";
            this.mskOtherClaimDate.Size = new System.Drawing.Size(127, 22);
            this.mskOtherClaimDate.TabIndex = 2;
            this.mskOtherClaimDate.ValidatingType = typeof(System.DateTime);
            this.mskOtherClaimDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskDate_MouseClick);
            this.mskOtherClaimDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskDate_Validating);
            // 
            // lblBox15Date
            // 
            this.lblBox15Date.AutoSize = true;
            this.lblBox15Date.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBox15Date.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBox15Date.Location = new System.Drawing.Point(92, 33);
            this.lblBox15Date.MaximumSize = new System.Drawing.Size(110, 14);
            this.lblBox15Date.MinimumSize = new System.Drawing.Size(110, 14);
            this.lblBox15Date.Name = "lblBox15Date";
            this.lblBox15Date.Size = new System.Drawing.Size(110, 14);
            this.lblBox15Date.TabIndex = 22;
            this.lblBox15Date.Text = "Other Claim Date :";
            this.lblBox15Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label73.Location = new System.Drawing.Point(0, 54);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(853, 1);
            this.label73.TabIndex = 17;
            this.label73.Text = "label2";
            // 
            // lblOnsiteDate
            // 
            this.lblOnsiteDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOnsiteDate.AutoEllipsis = true;
            this.lblOnsiteDate.AutoSize = true;
            this.lblOnsiteDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnsiteDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOnsiteDate.Location = new System.Drawing.Point(130, 8);
            this.lblOnsiteDate.Name = "lblOnsiteDate";
            this.lblOnsiteDate.Size = new System.Drawing.Size(72, 14);
            this.lblOnsiteDate.TabIndex = 0;
            this.lblOnsiteDate.Text = "Claim Date :";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Location = new System.Drawing.Point(290, 33);
            this.label85.MaximumSize = new System.Drawing.Size(100, 14);
            this.label85.MinimumSize = new System.Drawing.Size(100, 14);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(100, 14);
            this.label85.TabIndex = 28;
            this.label85.Text = "-";
            this.label85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFacility
            // 
            this.pnlFacility.Controls.Add(this.label6);
            this.pnlFacility.Controls.Add(this.cmbFacility);
            this.pnlFacility.Controls.Add(this.cmbBillingProvider);
            this.pnlFacility.Controls.Add(this.label58);
            this.pnlFacility.Controls.Add(this.label59);
            this.pnlFacility.Controls.Add(this.cmbReferralProvider);
            this.pnlFacility.Controls.Add(this.cmbProviderType);
            this.pnlFacility.Controls.Add(this.label84);
            this.pnlFacility.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFacility.Location = new System.Drawing.Point(4, 142);
            this.pnlFacility.Name = "pnlFacility";
            this.pnlFacility.Size = new System.Drawing.Size(853, 81);
            this.pnlFacility.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(0, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(853, 1);
            this.label6.TabIndex = 17;
            this.label6.Text = "label2";
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFacility.ForeColor = System.Drawing.Color.Black;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(205, 4);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(382, 22);
            this.cmbFacility.TabIndex = 0;
            // 
            // cmbBillingProvider
            // 
            this.cmbBillingProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillingProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillingProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbBillingProvider.FormattingEnabled = true;
            this.cmbBillingProvider.Location = new System.Drawing.Point(205, 29);
            this.cmbBillingProvider.Name = "cmbBillingProvider";
            this.cmbBillingProvider.Size = new System.Drawing.Size(382, 22);
            this.cmbBillingProvider.TabIndex = 1;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(110, 33);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(92, 14);
            this.label58.TabIndex = 8;
            this.label58.Text = "Billing Provider :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(152, 8);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(50, 14);
            this.label59.TabIndex = 5;
            this.label59.Text = "Facility :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReferralProvider
            // 
            this.cmbReferralProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferralProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReferralProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbReferralProvider.FormattingEnabled = true;
            this.cmbReferralProvider.Location = new System.Drawing.Point(205, 54);
            this.cmbReferralProvider.Name = "cmbReferralProvider";
            this.cmbReferralProvider.Size = new System.Drawing.Size(382, 22);
            this.cmbReferralProvider.TabIndex = 2;
            // 
            // cmbProviderType
            // 
            this.cmbProviderType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.cmbProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviderType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProviderType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProviderType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(128)))));
            this.cmbProviderType.FormattingEnabled = true;
            this.cmbProviderType.Location = new System.Drawing.Point(54, 54);
            this.cmbProviderType.Name = "cmbProviderType";
            this.cmbProviderType.Size = new System.Drawing.Size(135, 22);
            this.cmbProviderType.TabIndex = 26;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Location = new System.Drawing.Point(191, 58);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(11, 14);
            this.label84.TabIndex = 27;
            this.label84.Text = ":";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPatient
            // 
            this.pnlPatient.Controls.Add(this.cmbRelationToSubscriber);
            this.pnlPatient.Controls.Add(this.label9);
            this.pnlPatient.Controls.Add(this.cmbGender);
            this.pnlPatient.Controls.Add(this.label4);
            this.pnlPatient.Controls.Add(this.cmbPatientAgeDays);
            this.pnlPatient.Controls.Add(this.label3);
            this.pnlPatient.Controls.Add(this.cmbPatientAgeMonth);
            this.pnlPatient.Controls.Add(this.label2);
            this.pnlPatient.Controls.Add(this.cmbPatientAgeYears);
            this.pnlPatient.Controls.Add(this.label1);
            this.pnlPatient.Controls.Add(this.label28);
            this.pnlPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatient.Location = new System.Drawing.Point(4, 85);
            this.pnlPatient.Name = "pnlPatient";
            this.pnlPatient.Size = new System.Drawing.Size(853, 57);
            this.pnlPatient.TabIndex = 1;
            // 
            // cmbRelationToSubscriber
            // 
            this.cmbRelationToSubscriber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationToSubscriber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRelationToSubscriber.ForeColor = System.Drawing.Color.Black;
            this.cmbRelationToSubscriber.FormattingEnabled = true;
            this.cmbRelationToSubscriber.Location = new System.Drawing.Point(205, 30);
            this.cmbRelationToSubscriber.Name = "cmbRelationToSubscriber";
            this.cmbRelationToSubscriber.Size = new System.Drawing.Size(382, 22);
            this.cmbRelationToSubscriber.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(44, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(158, 14);
            this.label9.TabIndex = 36;
            this.label9.Text = "Relationship To Subscriber :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.ForeColor = System.Drawing.Color.Black;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Other",
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(731, 30);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(108, 22);
            this.cmbGender.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(673, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 34;
            this.label4.Text = "Gender :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPatientAgeDays
            // 
            this.cmbPatientAgeDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientAgeDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientAgeDays.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientAgeDays.FormattingEnabled = true;
            this.cmbPatientAgeDays.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29"});
            this.cmbPatientAgeDays.Location = new System.Drawing.Point(731, 5);
            this.cmbPatientAgeDays.Name = "cmbPatientAgeDays";
            this.cmbPatientAgeDays.Size = new System.Drawing.Size(108, 22);
            this.cmbPatientAgeDays.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(636, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 14);
            this.label3.TabIndex = 32;
            this.label3.Text = "Pat. Age Days :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPatientAgeMonth
            // 
            this.cmbPatientAgeMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientAgeMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientAgeMonth.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientAgeMonth.FormattingEnabled = true;
            this.cmbPatientAgeMonth.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11"});
            this.cmbPatientAgeMonth.Location = new System.Drawing.Point(479, 5);
            this.cmbPatientAgeMonth.Name = "cmbPatientAgeMonth";
            this.cmbPatientAgeMonth.Size = new System.Drawing.Size(108, 22);
            this.cmbPatientAgeMonth.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(374, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "Pat. Age Month :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPatientAgeYears
            // 
            this.cmbPatientAgeYears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientAgeYears.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientAgeYears.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientAgeYears.FormattingEnabled = true;
            this.cmbPatientAgeYears.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100"});
            this.cmbPatientAgeYears.Location = new System.Drawing.Point(205, 5);
            this.cmbPatientAgeYears.Name = "cmbPatientAgeYears";
            this.cmbPatientAgeYears.Size = new System.Drawing.Size(108, 22);
            this.cmbPatientAgeYears.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 28;
            this.label1.Text = "Pat. Age Years :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label28.Location = new System.Drawing.Point(0, 56);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(853, 1);
            this.label28.TabIndex = 19;
            this.label28.Text = "label2";
            // 
            // pnlInsurannce
            // 
            this.pnlInsurannce.Controls.Add(this.cmbInsurancePlanName);
            this.pnlInsurannce.Controls.Add(this.cmbInsuranceCompanyName);
            this.pnlInsurannce.Controls.Add(this.label7);
            this.pnlInsurannce.Controls.Add(this.label19);
            this.pnlInsurannce.Controls.Add(this.label5);
            this.pnlInsurannce.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInsurannce.Location = new System.Drawing.Point(4, 30);
            this.pnlInsurannce.Name = "pnlInsurannce";
            this.pnlInsurannce.Size = new System.Drawing.Size(853, 55);
            this.pnlInsurannce.TabIndex = 0;
            // 
            // cmbInsurancePlanName
            // 
            this.cmbInsurancePlanName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurancePlanName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurancePlanName.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurancePlanName.FormattingEnabled = true;
            this.cmbInsurancePlanName.Location = new System.Drawing.Point(205, 4);
            this.cmbInsurancePlanName.Name = "cmbInsurancePlanName";
            this.cmbInsurancePlanName.Size = new System.Drawing.Size(382, 22);
            this.cmbInsurancePlanName.TabIndex = 0;
            // 
            // cmbInsuranceCompanyName
            // 
            this.cmbInsuranceCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsuranceCompanyName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsuranceCompanyName.ForeColor = System.Drawing.Color.Black;
            this.cmbInsuranceCompanyName.FormattingEnabled = true;
            this.cmbInsuranceCompanyName.Location = new System.Drawing.Point(205, 29);
            this.cmbInsuranceCompanyName.Name = "cmbInsuranceCompanyName";
            this.cmbInsuranceCompanyName.Size = new System.Drawing.Size(382, 22);
            this.cmbInsuranceCompanyName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(45, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 14);
            this.label7.TabIndex = 22;
            this.label7.Text = "Insurance Company Name :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Location = new System.Drawing.Point(73, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(129, 14);
            this.label19.TabIndex = 21;
            this.label19.Text = "Insurance Plan Name :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(0, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(853, 1);
            this.label5.TabIndex = 17;
            this.label5.Text = "label2";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.panel5);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(4, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(853, 26);
            this.pnlHeader.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.lblSelectedText);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(853, 26);
            this.panel5.TabIndex = 0;
            // 
            // lblSelectedText
            // 
            this.lblSelectedText.AutoEllipsis = true;
            this.lblSelectedText.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedText.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectedText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedText.ForeColor = System.Drawing.Color.White;
            this.lblSelectedText.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedText.Name = "lblSelectedText";
            this.lblSelectedText.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.lblSelectedText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSelectedText.Size = new System.Drawing.Size(826, 25);
            this.lblSelectedText.TabIndex = 9;
            this.lblSelectedText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(0, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(853, 1);
            this.label12.TabIndex = 8;
            this.label12.Text = "label2";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 336);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(853, 1);
            this.label14.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(4, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(853, 1);
            this.label15.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(857, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 334);
            this.label17.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(3, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 334);
            this.label18.TabIndex = 0;
            // 
            // frmClaimRuleVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(861, 670);
            this.Controls.Add(this.pnlHD);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimRuleVerification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test Rules";
            this.Load += new System.EventHandler(this.frmClaimRuleVerification_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlHD.ResumeLayout(false);
            this.pnlChargeGrid.ResumeLayout(false);
            this.pnlChargeGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Charge)).EndInit();
            this.pnlTransactionMasterInfo.ResumeLayout(false);
            this.pnlHospitalisationDates.ResumeLayout(false);
            this.pnlHospitalisationDates.PerformLayout();
            this.pnlClaimDates.ResumeLayout(false);
            this.pnlClaimDates.PerformLayout();
            this.pnlFacility.ResumeLayout(false);
            this.pnlFacility.PerformLayout();
            this.pnlPatient.ResumeLayout(false);
            this.pnlPatient.PerformLayout();
            this.pnlInsurannce.ResumeLayout(false);
            this.pnlInsurannce.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlTransactionMasterInfo;
        private System.Windows.Forms.Panel pnlClaimDates;
        private System.Windows.Forms.MaskedTextBox mskClaimDate;
        private System.Windows.Forms.ComboBox cmbBox14DateQualifier;
        private System.Windows.Forms.ComboBox cmbBox15DateQualifier;
        private System.Windows.Forms.MaskedTextBox mskOtherClaimDate;
        private System.Windows.Forms.Label lblBox15Date;
        private System.Windows.Forms.Label lblOnsiteDate;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Panel pnlPatient;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.ComboBox cmbProviderType;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbReferralProvider;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox cmbBillingProvider;
        private System.Windows.Forms.ComboBox cmbFacility;
        private System.Windows.Forms.MaskedTextBox mskHospitaliztionTo;
        private System.Windows.Forms.MaskedTextBox mskHospitaliztionFrom;
        private System.Windows.Forms.Label lblHospitaliztionDateTo;
        private System.Windows.Forms.Label lblHopitalizationDateFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlChargeGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Charge;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox cmbRelationToSubscriber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPatientAgeDays;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbPatientAgeMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPatientAgeYears;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlHospitalisationDates;
        private System.Windows.Forms.Label lblClaimDateHyphen;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Panel pnlFacility;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlInsurannce;
        private System.Windows.Forms.ComboBox cmbInsurancePlanName;
        private System.Windows.Forms.ComboBox cmbInsuranceCompanyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tls_btnAddLine;
        private System.Windows.Forms.ToolStripButton tls_btnRemoveLine;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlHD;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSelectedText;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPriorAuthorization;
        private System.Windows.Forms.Label label8;
    }
}