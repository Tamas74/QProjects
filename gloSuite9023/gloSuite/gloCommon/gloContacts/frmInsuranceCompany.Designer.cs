namespace gloContacts
{
    partial class frmInsuranceCompany
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
                    components.Dispose();
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (c1Insurance.ContextMenu != null)
                    {

                        System.Windows.Forms.ContextMenu[] cntdtControls = { c1Insurance.ContextMenu };
                     
                        if (cntdtControls != null)
                        {
                            if (cntdtControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                            }
                        }
                        gloGlobal.cEventHelper.DisposeContextMenu(ref cntdtControls);
                    }
                }
                catch
                {
                }
              
                if (tooltip_Rpt != null)
                {
                    tooltip_Rpt.Dispose();
                    tooltip_Rpt = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsuranceCompany));
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.txtDFL = new System.Windows.Forms.TextBox();
            this.txtTFL = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbBox29 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBox30 = new System.Windows.Forms.ComboBox();
            this.Box29 = new System.Windows.Forms.Label();
            this.TxtDiagnosisperClaim = new System.Windows.Forms.TextBox();
            this.Box30 = new System.Windows.Forms.Label();
            this.TxtChargesperClaim = new System.Windows.Forms.TextBox();
            this.lblDiagnosisperClaim = new System.Windows.Forms.Label();
            this.lblChargeperClaim = new System.Windows.Forms.Label();
            this.chkIsInstitutionalBilling = new System.Windows.Forms.CheckBox();
            this.cmbTypeOFBilling = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCptCrosswalk = new System.Windows.Forms.Label();
            this.cmbCptCrosswalk = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDefaultFeeSheduleDelete = new System.Windows.Forms.Button();
            this.btnDefaultFeeSheduleBrowse = new System.Windows.Forms.Button();
            this.txtDefaultInsBillingType = new System.Windows.Forms.TextBox();
            this.btnModifyBillingType = new System.Windows.Forms.Button();
            this.btn_AddBillingType = new System.Windows.Forms.Button();
            this.btnBillingTypeDelete = new System.Windows.Forms.Button();
            this.btnBillingTypeBrowse = new System.Windows.Forms.Button();
            this.btnModifyReportingCategory = new System.Windows.Forms.Button();
            this.btn_AddReportingCategory = new System.Windows.Forms.Button();
            this.btnRptCatDelete = new System.Windows.Forms.Button();
            this.btnRptCatBrowse = new System.Windows.Forms.Button();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.txtDefaultPayerID = new System.Windows.Forms.TextBox();
            this.txtDefaultFeeSchedule = new System.Windows.Forms.TextBox();
            this.txtDefaultReportingCategory = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1Insurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.tlsStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).BeginInit();
            this.pnlTopToolStrip.SuspendLayout();
            this.tlsStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.txtDFL);
            this.pnl_Base.Controls.Add(this.txtTFL);
            this.pnl_Base.Controls.Add(this.label18);
            this.pnl_Base.Controls.Add(this.label19);
            this.pnl_Base.Controls.Add(this.cmbBox29);
            this.pnl_Base.Controls.Add(this.groupBox1);
            this.pnl_Base.Controls.Add(this.cmbBox30);
            this.pnl_Base.Controls.Add(this.Box29);
            this.pnl_Base.Controls.Add(this.TxtDiagnosisperClaim);
            this.pnl_Base.Controls.Add(this.Box30);
            this.pnl_Base.Controls.Add(this.TxtChargesperClaim);
            this.pnl_Base.Controls.Add(this.lblDiagnosisperClaim);
            this.pnl_Base.Controls.Add(this.lblChargeperClaim);
            this.pnl_Base.Controls.Add(this.chkIsInstitutionalBilling);
            this.pnl_Base.Controls.Add(this.cmbTypeOFBilling);
            this.pnl_Base.Controls.Add(this.label17);
            this.pnl_Base.Controls.Add(this.lblCptCrosswalk);
            this.pnl_Base.Controls.Add(this.cmbCptCrosswalk);
            this.pnl_Base.Controls.Add(this.label4);
            this.pnl_Base.Controls.Add(this.btnDefaultFeeSheduleDelete);
            this.pnl_Base.Controls.Add(this.btnDefaultFeeSheduleBrowse);
            this.pnl_Base.Controls.Add(this.txtDefaultInsBillingType);
            this.pnl_Base.Controls.Add(this.btnModifyBillingType);
            this.pnl_Base.Controls.Add(this.btn_AddBillingType);
            this.pnl_Base.Controls.Add(this.btnBillingTypeDelete);
            this.pnl_Base.Controls.Add(this.btnBillingTypeBrowse);
            this.pnl_Base.Controls.Add(this.btnModifyReportingCategory);
            this.pnl_Base.Controls.Add(this.btn_AddReportingCategory);
            this.pnl_Base.Controls.Add(this.btnRptCatDelete);
            this.pnl_Base.Controls.Add(this.btnRptCatBrowse);
            this.pnl_Base.Controls.Add(this.pnlAddresssControl);
            this.pnl_Base.Controls.Add(this.txtDefaultPayerID);
            this.pnl_Base.Controls.Add(this.txtDefaultFeeSchedule);
            this.pnl_Base.Controls.Add(this.txtDefaultReportingCategory);
            this.pnl_Base.Controls.Add(this.label20);
            this.pnl_Base.Controls.Add(this.label16);
            this.pnl_Base.Controls.Add(this.label15);
            this.pnl_Base.Controls.Add(this.label14);
            this.pnl_Base.Controls.Add(this.label13);
            this.pnl_Base.Controls.Add(this.label12);
            this.pnl_Base.Controls.Add(this.txtDescription);
            this.pnl_Base.Controls.Add(this.txtCode);
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.label11);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 54);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(794, 374);
            this.pnl_Base.TabIndex = 0;
            this.pnl_Base.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Base_Paint);
            // 
            // txtDFL
            // 
            this.txtDFL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDFL.Location = new System.Drawing.Point(432, 343);
            this.txtDFL.MaxLength = 3;
            this.txtDFL.Name = "txtDFL";
            this.txtDFL.ShortcutsEnabled = false;
            this.txtDFL.Size = new System.Drawing.Size(60, 22);
            this.txtDFL.TabIndex = 178;
            this.txtDFL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDFL_KeyPress);
            // 
            // txtTFL
            // 
            this.txtTFL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTFL.Location = new System.Drawing.Point(432, 315);
            this.txtTFL.MaxLength = 3;
            this.txtTFL.Name = "txtTFL";
            this.txtTFL.ShortcutsEnabled = false;
            this.txtTFL.Size = new System.Drawing.Size(60, 22);
            this.txtTFL.TabIndex = 177;
            this.txtTFL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTFL_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(325, 345);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 14);
            this.label18.TabIndex = 176;
            this.label18.Text = "Denial Filing Limit :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(330, 318);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 14);
            this.label19.TabIndex = 175;
            this.label19.Text = "Claim Filing Limit :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbBox29
            // 
            this.cmbBox29.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox29.FormattingEnabled = true;
            this.cmbBox29.Location = new System.Drawing.Point(432, 231);
            this.cmbBox29.Name = "cmbBox29";
            this.cmbBox29.Size = new System.Drawing.Size(261, 22);
            this.cmbBox29.TabIndex = 172;
            this.cmbBox29.MouseEnter += new System.EventHandler(this.cmbBox29_MouseEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(713, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(47, 15);
            this.groupBox1.TabIndex = 173;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paper Billing Rules";
            this.groupBox1.Visible = false;
            // 
            // cmbBox30
            // 
            this.cmbBox30.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBox30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBox30.FormattingEnabled = true;
            this.cmbBox30.Location = new System.Drawing.Point(432, 260);
            this.cmbBox30.Name = "cmbBox30";
            this.cmbBox30.Size = new System.Drawing.Size(261, 22);
            this.cmbBox30.TabIndex = 174;
            // 
            // Box29
            // 
            this.Box29.AutoSize = true;
            this.Box29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Box29.Location = new System.Drawing.Point(380, 232);
            this.Box29.Name = "Box29";
            this.Box29.Size = new System.Drawing.Size(53, 14);
            this.Box29.TabIndex = 171;
            this.Box29.Text = "Box 29 :";
            // 
            // TxtDiagnosisperClaim
            // 
            this.TxtDiagnosisperClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiagnosisperClaim.Location = new System.Drawing.Point(198, 342);
            this.TxtDiagnosisperClaim.MaxLength = 5;
            this.TxtDiagnosisperClaim.Name = "TxtDiagnosisperClaim";
            this.TxtDiagnosisperClaim.ShortcutsEnabled = false;
            this.TxtDiagnosisperClaim.Size = new System.Drawing.Size(60, 22);
            this.TxtDiagnosisperClaim.TabIndex = 170;
            this.TxtDiagnosisperClaim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDiagnosisperClaim_KeyDown);
            this.TxtDiagnosisperClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDiagnosisperClaim_KeyPress);
            this.TxtDiagnosisperClaim.Validating += new System.ComponentModel.CancelEventHandler(this.TxtDiagnosisperClaim_Validating);
            // 
            // Box30
            // 
            this.Box30.AutoSize = true;
            this.Box30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Box30.Location = new System.Drawing.Point(382, 262);
            this.Box30.Name = "Box30";
            this.Box30.Size = new System.Drawing.Size(53, 14);
            this.Box30.TabIndex = 8;
            this.Box30.Text = "Box 30 :";
            // 
            // TxtChargesperClaim
            // 
            this.TxtChargesperClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChargesperClaim.Location = new System.Drawing.Point(198, 314);
            this.TxtChargesperClaim.MaxLength = 5;
            this.TxtChargesperClaim.Name = "TxtChargesperClaim";
            this.TxtChargesperClaim.ShortcutsEnabled = false;
            this.TxtChargesperClaim.Size = new System.Drawing.Size(60, 22);
            this.TxtChargesperClaim.TabIndex = 169;
            this.TxtChargesperClaim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtChargesperClaim_KeyDown);
            this.TxtChargesperClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtChargesperClaim_KeyPress);
            this.TxtChargesperClaim.Validating += new System.ComponentModel.CancelEventHandler(this.TxtChargesperClaim_Validating);
            // 
            // lblDiagnosisperClaim
            // 
            this.lblDiagnosisperClaim.AutoSize = true;
            this.lblDiagnosisperClaim.Location = new System.Drawing.Point(48, 345);
            this.lblDiagnosisperClaim.Name = "lblDiagnosisperClaim";
            this.lblDiagnosisperClaim.Size = new System.Drawing.Size(147, 14);
            this.lblDiagnosisperClaim.TabIndex = 167;
            this.lblDiagnosisperClaim.Text = "Max Diagnoses Per Claim :";
            this.lblDiagnosisperClaim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblChargeperClaim
            // 
            this.lblChargeperClaim.AutoSize = true;
            this.lblChargeperClaim.Location = new System.Drawing.Point(59, 318);
            this.lblChargeperClaim.Name = "lblChargeperClaim";
            this.lblChargeperClaim.Size = new System.Drawing.Size(136, 14);
            this.lblChargeperClaim.TabIndex = 165;
            this.lblChargeperClaim.Text = "Max Charges Per Claim :";
            this.lblChargeperClaim.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkIsInstitutionalBilling
            // 
            this.chkIsInstitutionalBilling.AutoSize = true;
            this.chkIsInstitutionalBilling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsInstitutionalBilling.Location = new System.Drawing.Point(382, 288);
            this.chkIsInstitutionalBilling.Name = "chkIsInstitutionalBilling";
            this.chkIsInstitutionalBilling.Size = new System.Drawing.Size(123, 18);
            this.chkIsInstitutionalBilling.TabIndex = 122;
            this.chkIsInstitutionalBilling.Text = "Institutional Billing";
            this.chkIsInstitutionalBilling.UseVisualStyleBackColor = true;
            // 
            // cmbTypeOFBilling
            // 
            this.cmbTypeOFBilling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeOFBilling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeOFBilling.FormattingEnabled = true;
            this.cmbTypeOFBilling.Items.AddRange(new object[] {
            "",
            "Electronic",
            "Paper"});
            this.cmbTypeOFBilling.Location = new System.Drawing.Point(198, 286);
            this.cmbTypeOFBilling.Name = "cmbTypeOFBilling";
            this.cmbTypeOFBilling.Size = new System.Drawing.Size(173, 22);
            this.cmbTypeOFBilling.TabIndex = 121;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(62, 289);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 14);
            this.label17.TabIndex = 120;
            this.label17.Text = "Default Billing Method :";
            // 
            // lblCptCrosswalk
            // 
            this.lblCptCrosswalk.AutoSize = true;
            this.lblCptCrosswalk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCptCrosswalk.Location = new System.Drawing.Point(102, 263);
            this.lblCptCrosswalk.Name = "lblCptCrosswalk";
            this.lblCptCrosswalk.Size = new System.Drawing.Size(93, 14);
            this.lblCptCrosswalk.TabIndex = 119;
            this.lblCptCrosswalk.Text = "CPT Crosswalk :";
            // 
            // cmbCptCrosswalk
            // 
            this.cmbCptCrosswalk.BackColor = System.Drawing.SystemColors.Window;
            this.cmbCptCrosswalk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCptCrosswalk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCptCrosswalk.FormattingEnabled = true;
            this.cmbCptCrosswalk.Location = new System.Drawing.Point(198, 258);
            this.cmbCptCrosswalk.Name = "cmbCptCrosswalk";
            this.cmbCptCrosswalk.Size = new System.Drawing.Size(173, 22);
            this.cmbCptCrosswalk.TabIndex = 33;
            this.cmbCptCrosswalk.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbCptCrosswalk_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "Insurance Company Name :";
            // 
            // btnDefaultFeeSheduleDelete
            // 
            this.btnDefaultFeeSheduleDelete.AutoEllipsis = true;
            this.btnDefaultFeeSheduleDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDefaultFeeSheduleDelete.BackgroundImage")));
            this.btnDefaultFeeSheduleDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefaultFeeSheduleDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefaultFeeSheduleDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnDefaultFeeSheduleDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultFeeSheduleDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultFeeSheduleDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultFeeSheduleDelete.Image")));
            this.btnDefaultFeeSheduleDelete.Location = new System.Drawing.Point(571, 99);
            this.btnDefaultFeeSheduleDelete.Name = "btnDefaultFeeSheduleDelete";
            this.btnDefaultFeeSheduleDelete.Size = new System.Drawing.Size(21, 21);
            this.btnDefaultFeeSheduleDelete.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnDefaultFeeSheduleDelete, "Clear Fee Schedule");
            this.btnDefaultFeeSheduleDelete.UseVisualStyleBackColor = false;
            this.btnDefaultFeeSheduleDelete.Click += new System.EventHandler(this.btnDefaultFeeSheduleDelete_Click);
            // 
            // btnDefaultFeeSheduleBrowse
            // 
            this.btnDefaultFeeSheduleBrowse.AutoEllipsis = true;
            this.btnDefaultFeeSheduleBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDefaultFeeSheduleBrowse.BackgroundImage")));
            this.btnDefaultFeeSheduleBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefaultFeeSheduleBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefaultFeeSheduleBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDefaultFeeSheduleBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDefaultFeeSheduleBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultFeeSheduleBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefaultFeeSheduleBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnDefaultFeeSheduleBrowse.Image")));
            this.btnDefaultFeeSheduleBrowse.Location = new System.Drawing.Point(545, 99);
            this.btnDefaultFeeSheduleBrowse.Name = "btnDefaultFeeSheduleBrowse";
            this.btnDefaultFeeSheduleBrowse.Size = new System.Drawing.Size(21, 21);
            this.btnDefaultFeeSheduleBrowse.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnDefaultFeeSheduleBrowse, "Browse Fee Schedule");
            this.btnDefaultFeeSheduleBrowse.UseVisualStyleBackColor = false;
            this.btnDefaultFeeSheduleBrowse.Click += new System.EventHandler(this.btnDefaultFeeSheduleBrowse_Click);
            // 
            // txtDefaultInsBillingType
            // 
            this.txtDefaultInsBillingType.BackColor = System.Drawing.Color.White;
            this.txtDefaultInsBillingType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultInsBillingType.Location = new System.Drawing.Point(198, 70);
            this.txtDefaultInsBillingType.Name = "txtDefaultInsBillingType";
            this.txtDefaultInsBillingType.ReadOnly = true;
            this.txtDefaultInsBillingType.Size = new System.Drawing.Size(341, 22);
            this.txtDefaultInsBillingType.TabIndex = 100;
            this.txtDefaultInsBillingType.TabStop = false;
            this.txtDefaultInsBillingType.Tag = "0";
            this.txtDefaultInsBillingType.TextChanged += new System.EventHandler(this.AllTextChanged_Event);
            // 
            // btnModifyBillingType
            // 
            this.btnModifyBillingType.AutoEllipsis = true;
            this.btnModifyBillingType.BackColor = System.Drawing.Color.Transparent;
            this.btnModifyBillingType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifyBillingType.BackgroundImage")));
            this.btnModifyBillingType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifyBillingType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifyBillingType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnModifyBillingType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnModifyBillingType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnModifyBillingType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyBillingType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyBillingType.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyBillingType.Image")));
            this.btnModifyBillingType.Location = new System.Drawing.Point(623, 71);
            this.btnModifyBillingType.Name = "btnModifyBillingType";
            this.btnModifyBillingType.Size = new System.Drawing.Size(21, 21);
            this.btnModifyBillingType.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnModifyBillingType, "Modify Plan Type");
            this.btnModifyBillingType.UseVisualStyleBackColor = false;
            this.btnModifyBillingType.Click += new System.EventHandler(this.btnModifyBillingType_Click);
            // 
            // btn_AddBillingType
            // 
            this.btn_AddBillingType.AutoEllipsis = true;
            this.btn_AddBillingType.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddBillingType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddBillingType.BackgroundImage")));
            this.btn_AddBillingType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddBillingType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddBillingType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btn_AddBillingType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AddBillingType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AddBillingType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddBillingType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddBillingType.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddBillingType.Image")));
            this.btn_AddBillingType.Location = new System.Drawing.Point(597, 71);
            this.btn_AddBillingType.Name = "btn_AddBillingType";
            this.btn_AddBillingType.Size = new System.Drawing.Size(21, 21);
            this.btn_AddBillingType.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btn_AddBillingType, "Add Plan Type");
            this.btn_AddBillingType.UseVisualStyleBackColor = false;
            this.btn_AddBillingType.Click += new System.EventHandler(this.btn_AddBillingType_Click);
            // 
            // btnBillingTypeDelete
            // 
            this.btnBillingTypeDelete.AutoEllipsis = true;
            this.btnBillingTypeDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBillingTypeDelete.BackgroundImage")));
            this.btnBillingTypeDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBillingTypeDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBillingTypeDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnBillingTypeDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBillingTypeDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillingTypeDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnBillingTypeDelete.Image")));
            this.btnBillingTypeDelete.Location = new System.Drawing.Point(571, 71);
            this.btnBillingTypeDelete.Name = "btnBillingTypeDelete";
            this.btnBillingTypeDelete.Size = new System.Drawing.Size(21, 21);
            this.btnBillingTypeDelete.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnBillingTypeDelete, "Clear Plan Type");
            this.btnBillingTypeDelete.UseVisualStyleBackColor = false;
            this.btnBillingTypeDelete.Click += new System.EventHandler(this.btnBillingTypeDelete_Click);
            // 
            // btnBillingTypeBrowse
            // 
            this.btnBillingTypeBrowse.AutoEllipsis = true;
            this.btnBillingTypeBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBillingTypeBrowse.BackgroundImage")));
            this.btnBillingTypeBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBillingTypeBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBillingTypeBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBillingTypeBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBillingTypeBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBillingTypeBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBillingTypeBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBillingTypeBrowse.Image")));
            this.btnBillingTypeBrowse.Location = new System.Drawing.Point(545, 71);
            this.btnBillingTypeBrowse.Name = "btnBillingTypeBrowse";
            this.btnBillingTypeBrowse.Size = new System.Drawing.Size(21, 21);
            this.btnBillingTypeBrowse.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnBillingTypeBrowse, "Browse  Plan Type");
            this.btnBillingTypeBrowse.UseVisualStyleBackColor = false;
            this.btnBillingTypeBrowse.Click += new System.EventHandler(this.btnBillingTypeBrowse_Click);
            // 
            // btnModifyReportingCategory
            // 
            this.btnModifyReportingCategory.AutoEllipsis = true;
            this.btnModifyReportingCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnModifyReportingCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifyReportingCategory.BackgroundImage")));
            this.btnModifyReportingCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifyReportingCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifyReportingCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnModifyReportingCategory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnModifyReportingCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnModifyReportingCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyReportingCategory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyReportingCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyReportingCategory.Image")));
            this.btnModifyReportingCategory.Location = new System.Drawing.Point(623, 43);
            this.btnModifyReportingCategory.Name = "btnModifyReportingCategory";
            this.btnModifyReportingCategory.Size = new System.Drawing.Size(21, 21);
            this.btnModifyReportingCategory.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnModifyReportingCategory, "Modify Reporting Category");
            this.btnModifyReportingCategory.UseVisualStyleBackColor = false;
            this.btnModifyReportingCategory.Click += new System.EventHandler(this.btnModifyReportingCategory_Click);
            // 
            // btn_AddReportingCategory
            // 
            this.btn_AddReportingCategory.AutoEllipsis = true;
            this.btn_AddReportingCategory.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddReportingCategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddReportingCategory.BackgroundImage")));
            this.btn_AddReportingCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddReportingCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddReportingCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btn_AddReportingCategory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_AddReportingCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_AddReportingCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddReportingCategory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddReportingCategory.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddReportingCategory.Image")));
            this.btn_AddReportingCategory.Location = new System.Drawing.Point(597, 43);
            this.btn_AddReportingCategory.Name = "btn_AddReportingCategory";
            this.btn_AddReportingCategory.Size = new System.Drawing.Size(21, 21);
            this.btn_AddReportingCategory.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btn_AddReportingCategory, "Add Reporting Category");
            this.btn_AddReportingCategory.UseVisualStyleBackColor = false;
            this.btn_AddReportingCategory.Click += new System.EventHandler(this.btn_AddReportingCategory_Click);
            // 
            // btnRptCatDelete
            // 
            this.btnRptCatDelete.AutoEllipsis = true;
            this.btnRptCatDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnRptCatDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRptCatDelete.BackgroundImage")));
            this.btnRptCatDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRptCatDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRptCatDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnRptCatDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRptCatDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRptCatDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRptCatDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRptCatDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnRptCatDelete.Image")));
            this.btnRptCatDelete.Location = new System.Drawing.Point(571, 43);
            this.btnRptCatDelete.Name = "btnRptCatDelete";
            this.btnRptCatDelete.Size = new System.Drawing.Size(21, 21);
            this.btnRptCatDelete.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnRptCatDelete, "Clear Reporting Category");
            this.btnRptCatDelete.UseVisualStyleBackColor = false;
            this.btnRptCatDelete.Click += new System.EventHandler(this.btnInsDelete_Click);
            // 
            // btnRptCatBrowse
            // 
            this.btnRptCatBrowse.AutoEllipsis = true;
            this.btnRptCatBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnRptCatBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRptCatBrowse.BackgroundImage")));
            this.btnRptCatBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRptCatBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRptCatBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRptCatBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRptCatBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRptCatBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRptCatBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRptCatBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnRptCatBrowse.Image")));
            this.btnRptCatBrowse.Location = new System.Drawing.Point(545, 43);
            this.btnRptCatBrowse.Name = "btnRptCatBrowse";
            this.btnRptCatBrowse.Size = new System.Drawing.Size(21, 21);
            this.btnRptCatBrowse.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnRptCatBrowse, "Browse Reporting Category");
            this.btnRptCatBrowse.UseVisualStyleBackColor = false;
            this.btnRptCatBrowse.Click += new System.EventHandler(this.btnRptCatBrowse_Click);
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(116, 119);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(430, 108);
            this.pnlAddresssControl.TabIndex = 12;
            // 
            // txtDefaultPayerID
            // 
            this.txtDefaultPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultPayerID.Location = new System.Drawing.Point(198, 230);
            this.txtDefaultPayerID.MaxLength = 35;
            this.txtDefaultPayerID.Name = "txtDefaultPayerID";
            this.txtDefaultPayerID.Size = new System.Drawing.Size(173, 22);
            this.txtDefaultPayerID.TabIndex = 13;
            this.txtDefaultPayerID.TextChanged += new System.EventHandler(this.AllTextChanged_Event);
            // 
            // txtDefaultFeeSchedule
            // 
            this.txtDefaultFeeSchedule.BackColor = System.Drawing.Color.White;
            this.txtDefaultFeeSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultFeeSchedule.Location = new System.Drawing.Point(198, 98);
            this.txtDefaultFeeSchedule.Name = "txtDefaultFeeSchedule";
            this.txtDefaultFeeSchedule.ReadOnly = true;
            this.txtDefaultFeeSchedule.Size = new System.Drawing.Size(341, 22);
            this.txtDefaultFeeSchedule.TabIndex = 100;
            this.txtDefaultFeeSchedule.TabStop = false;
            this.txtDefaultFeeSchedule.Tag = "0";
            this.txtDefaultFeeSchedule.TextChanged += new System.EventHandler(this.AllTextChanged_Event);
            // 
            // txtDefaultReportingCategory
            // 
            this.txtDefaultReportingCategory.BackColor = System.Drawing.Color.White;
            this.txtDefaultReportingCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultReportingCategory.Location = new System.Drawing.Point(198, 42);
            this.txtDefaultReportingCategory.MaxLength = 255;
            this.txtDefaultReportingCategory.Name = "txtDefaultReportingCategory";
            this.txtDefaultReportingCategory.ReadOnly = true;
            this.txtDefaultReportingCategory.Size = new System.Drawing.Size(341, 22);
            this.txtDefaultReportingCategory.TabIndex = 100;
            this.txtDefaultReportingCategory.TabStop = false;
            this.txtDefaultReportingCategory.Tag = "0";
            this.txtDefaultReportingCategory.TextChanged += new System.EventHandler(this.AllTextChanged_Event);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(87, 234);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(108, 14);
            this.label20.TabIndex = 30;
            this.label20.Text = "Default Payer ID  :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(63, 102);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(132, 14);
            this.label16.TabIndex = 26;
            this.label16.Text = "Default Fee Schedule :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(83, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 14);
            this.label15.TabIndex = 25;
            this.label15.Text = "Default Plan Type :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(31, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(164, 14);
            this.label14.TabIndex = 24;
            this.label14.Text = "Default Reporting Category :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(26, 18);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 14);
            this.label13.TabIndex = 23;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(699, 18);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 14);
            this.label12.TabIndex = 22;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Visible = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(198, 14);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(341, 22);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.TextChanged += new System.EventHandler(this.AllTextChanged_Event);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(756, 18);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(21, 22);
            this.txtCode.TabIndex = 0;
            this.txtCode.Visible = false;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 370);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(710, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 14);
            this.label11.TabIndex = 12;
            this.label11.Text = "Code :";
            this.label11.Visible = false;
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 367);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(790, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 367);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(788, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1Insurance
            // 
            this.c1Insurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Insurance.AllowEditing = false;
            this.c1Insurance.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Insurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Insurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Insurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Insurance.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Insurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Insurance.ExtendLastCol = true;
            this.c1Insurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Insurance.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Insurance.Location = new System.Drawing.Point(3, 0);
            this.c1Insurance.Name = "c1Insurance";
            this.c1Insurance.Rows.Count = 1;
            this.c1Insurance.Rows.DefaultSize = 19;
            this.c1Insurance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Insurance.Size = new System.Drawing.Size(788, 211);
            this.c1Insurance.StyleInfo = resources.GetString("c1Insurance.StyleInfo");
            this.c1Insurance.TabIndex = 19;
            this.c1Insurance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Insurance_MouseDown);
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearInsurance.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatAppearance.BorderSize = 0;
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(766, 1);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(21, 21);
            this.btnClearInsurance.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnClearInsurance, "Clear");
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseInsurance.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatAppearance.BorderSize = 0;
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(745, 1);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseInsurance.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnBrowseInsurance, "Browse Insurance Plans");
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label2.Size = new System.Drawing.Size(123, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "     Insurance Plans";
            // 
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.tlsStrip);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(794, 54);
            this.pnlTopToolStrip.TabIndex = 50;
            this.pnlTopToolStrip.TabStop = true;
            // 
            // tlsStrip
            // 
            this.tlsStrip.BackColor = System.Drawing.Color.Transparent;
            this.tlsStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsStrip.BackgroundImage")));
            this.tlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tlsStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSave,
            this.ts_btnClose});
            this.tlsStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsStrip.Location = new System.Drawing.Point(0, 0);
            this.tlsStrip.Name = "tlsStrip";
            this.tlsStrip.Size = new System.Drawing.Size(794, 53);
            this.tlsStrip.TabIndex = 7;
            this.tlsStrip.TabStop = true;
            this.tlsStrip.Text = "toolStrip1";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.c1Insurance);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 454);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(794, 214);
            this.panel1.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(790, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 209);
            this.label6.TabIndex = 24;
            this.label6.Text = "label4";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 209);
            this.label1.TabIndex = 23;
            this.label1.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(3, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(788, 1);
            this.label5.TabIndex = 22;
            this.label5.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(788, 1);
            this.label3.TabIndex = 21;
            this.label3.Text = "label2";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.btnBrowseInsurance);
            this.pnlHeader.Controls.Add(this.btnClearInsurance);
            this.pnlHeader.Controls.Add(this.label7);
            this.pnlHeader.Controls.Add(this.label8);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.label9);
            this.pnlHeader.Controls.Add(this.label10);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(3, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(788, 23);
            this.pnlHeader.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(787, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 21);
            this.label7.TabIndex = 24;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 21);
            this.label8.TabIndex = 23;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(0, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(788, 1);
            this.label9.TabIndex = 22;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(788, 1);
            this.label10.TabIndex = 21;
            this.label10.Text = "label2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlHeader);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 428);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(794, 26);
            this.panel3.TabIndex = 1;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmInsuranceCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(794, 668);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsuranceCompany";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Insurance Company";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInsuranceCompany_FormClosing);
            this.Load += new System.EventHandler(this.frmInsuranceCompany_Load);
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Insurance)).EndInit();
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.tlsStrip.ResumeLayout(false);
            this.tlsStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Base;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tlsStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        internal System.Windows.Forms.Button btnClearInsurance;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Insurance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox txtDefaultReportingCategory;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox txtDefaultPayerID;
        internal System.Windows.Forms.TextBox txtDefaultFeeSchedule;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private System.Windows.Forms.Button btnModifyReportingCategory;
        private System.Windows.Forms.Button btn_AddReportingCategory;
        private System.Windows.Forms.Button btnRptCatDelete;
        private System.Windows.Forms.Button btnRptCatBrowse;
        internal System.Windows.Forms.TextBox txtDefaultInsBillingType;
        private System.Windows.Forms.Button btnModifyBillingType;
        private System.Windows.Forms.Button btn_AddBillingType;
        private System.Windows.Forms.Button btnBillingTypeDelete;
        private System.Windows.Forms.Button btnBillingTypeBrowse;
        private System.Windows.Forms.Button btnDefaultFeeSheduleDelete;
        private System.Windows.Forms.Button btnDefaultFeeSheduleBrowse;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ComboBox cmbCptCrosswalk;
        internal System.Windows.Forms.Label lblCptCrosswalk;
        private System.Windows.Forms.CheckBox chkIsInstitutionalBilling;
        private System.Windows.Forms.ComboBox cmbTypeOFBilling;
        internal System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblDiagnosisperClaim;
        private System.Windows.Forms.Label lblChargeperClaim;
        internal System.Windows.Forms.TextBox TxtDiagnosisperClaim;
        internal System.Windows.Forms.TextBox TxtChargesperClaim;
        private System.Windows.Forms.ComboBox cmbBox30;
        private System.Windows.Forms.Label Box30;
        private System.Windows.Forms.Label Box29;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbBox29;
        internal System.Windows.Forms.TextBox txtDFL;
        internal System.Windows.Forms.TextBox txtTFL;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;

    }
}