namespace gloBilling
{
    partial class frmSetupEPSDTFamilyPlanning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupEPSDTFamilyPlanning));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDOSText = new System.Windows.Forms.Label();
            this.lblCPTDescText = new System.Windows.Forms.Label();
            this.lblMod2Text = new System.Windows.Forms.Label();
            this.lblMod1Text = new System.Windows.Forms.Label();
            this.lblCPTCodeText = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.chkServiceFamilyPlanningIndicator = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkThisServiceIsTheResultOfScreening = new System.Windows.Forms.CheckBox();
            this.chkThisServiceIsTheScreening = new System.Windows.Forms.CheckBox();
            this.lblReferralCode = new System.Windows.Forms.Label();
            this.cmbReferralCode = new System.Windows.Forms.ComboBox();
            this.cmbPatientGivenEPSDTReferral = new System.Windows.Forms.ComboBox();
            this.chkPatientGivenEPSDTReferral = new System.Windows.Forms.CheckBox();
            this.chkClaimIncludesanEPSDTScreening = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnltlsStrip.BackgroundImage")));
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(596, 54);
            this.pnltlsStrip.TabIndex = 117;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(596, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "OK";
            this.tsb_Save.Text = "Sa&ve&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Cancel";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.lblDOSText);
            this.pnlMain.Controls.Add(this.lblCPTDescText);
            this.pnlMain.Controls.Add(this.lblMod2Text);
            this.pnlMain.Controls.Add(this.lblMod1Text);
            this.pnlMain.Controls.Add(this.lblCPTCodeText);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(596, 64);
            this.pnlMain.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(240, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 14);
            this.label9.TabIndex = 36;
            this.label9.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(203, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "M2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(165, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 37;
            this.label7.Text = "M1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(96, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 14);
            this.label6.TabIndex = 40;
            this.label6.Text = "CPT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 14);
            this.label5.TabIndex = 39;
            this.label5.Text = "DOS";
            // 
            // lblDOSText
            // 
            this.lblDOSText.BackColor = System.Drawing.Color.Transparent;
            this.lblDOSText.Location = new System.Drawing.Point(14, 34);
            this.lblDOSText.Name = "lblDOSText";
            this.lblDOSText.Size = new System.Drawing.Size(73, 14);
            this.lblDOSText.TabIndex = 35;
            this.lblDOSText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTDescText
            // 
            this.lblCPTDescText.AutoEllipsis = true;
            this.lblCPTDescText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTDescText.Location = new System.Drawing.Point(240, 34);
            this.lblCPTDescText.Name = "lblCPTDescText";
            this.lblCPTDescText.Size = new System.Drawing.Size(333, 14);
            this.lblCPTDescText.TabIndex = 34;
            this.lblCPTDescText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod2Text
            // 
            this.lblMod2Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod2Text.Location = new System.Drawing.Point(203, 34);
            this.lblMod2Text.Name = "lblMod2Text";
            this.lblMod2Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod2Text.TabIndex = 33;
            this.lblMod2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMod1Text
            // 
            this.lblMod1Text.BackColor = System.Drawing.Color.Transparent;
            this.lblMod1Text.Location = new System.Drawing.Point(165, 34);
            this.lblMod1Text.Name = "lblMod1Text";
            this.lblMod1Text.Size = new System.Drawing.Size(31, 14);
            this.lblMod1Text.TabIndex = 32;
            this.lblMod1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTCodeText
            // 
            this.lblCPTCodeText.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTCodeText.Location = new System.Drawing.Point(96, 34);
            this.lblCPTCodeText.Name = "lblCPTCodeText";
            this.lblCPTCodeText.Size = new System.Drawing.Size(67, 14);
            this.lblCPTCodeText.TabIndex = 31;
            this.lblCPTCodeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 60);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(588, 1);
            this.lbl_BottomBrd.TabIndex = 29;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 57);
            this.lbl_LeftBrd.TabIndex = 28;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(592, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 57);
            this.lbl_RightBrd.TabIndex = 27;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(590, 1);
            this.lbl_TopBrd.TabIndex = 26;
            this.lbl_TopBrd.Text = "label1";
            // 
            // chkServiceFamilyPlanningIndicator
            // 
            this.chkServiceFamilyPlanningIndicator.AutoSize = true;
            this.chkServiceFamilyPlanningIndicator.Location = new System.Drawing.Point(30, 29);
            this.chkServiceFamilyPlanningIndicator.Name = "chkServiceFamilyPlanningIndicator";
            this.chkServiceFamilyPlanningIndicator.Size = new System.Drawing.Size(200, 18);
            this.chkServiceFamilyPlanningIndicator.TabIndex = 50;
            this.chkServiceFamilyPlanningIndicator.Text = "Service Family Planning indicator";
            this.chkServiceFamilyPlanningIndicator.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 14);
            this.label2.TabIndex = 49;
            this.label2.Text = "Family Planning";
            // 
            // chkThisServiceIsTheResultOfScreening
            // 
            this.chkThisServiceIsTheResultOfScreening.AutoSize = true;
            this.chkThisServiceIsTheResultOfScreening.Location = new System.Drawing.Point(30, 134);
            this.chkThisServiceIsTheResultOfScreening.Name = "chkThisServiceIsTheResultOfScreening";
            this.chkThisServiceIsTheResultOfScreening.Size = new System.Drawing.Size(238, 18);
            this.chkThisServiceIsTheResultOfScreening.TabIndex = 48;
            this.chkThisServiceIsTheResultOfScreening.Text = "This service is the result of a screening";
            this.chkThisServiceIsTheResultOfScreening.UseVisualStyleBackColor = true;
            // 
            // chkThisServiceIsTheScreening
            // 
            this.chkThisServiceIsTheScreening.AutoSize = true;
            this.chkThisServiceIsTheScreening.Enabled = false;
            this.chkThisServiceIsTheScreening.Location = new System.Drawing.Point(44, 110);
            this.chkThisServiceIsTheScreening.Name = "chkThisServiceIsTheScreening";
            this.chkThisServiceIsTheScreening.Size = new System.Drawing.Size(179, 18);
            this.chkThisServiceIsTheScreening.TabIndex = 47;
            this.chkThisServiceIsTheScreening.Text = "This service is the screening";
            this.chkThisServiceIsTheScreening.UseVisualStyleBackColor = true;
            // 
            // lblReferralCode
            // 
            this.lblReferralCode.AutoSize = true;
            this.lblReferralCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferralCode.Location = new System.Drawing.Point(331, 86);
            this.lblReferralCode.Name = "lblReferralCode";
            this.lblReferralCode.Size = new System.Drawing.Size(88, 14);
            this.lblReferralCode.TabIndex = 46;
            this.lblReferralCode.Text = "Referral Code :";
            // 
            // cmbReferralCode
            // 
            this.cmbReferralCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferralCode.Enabled = false;
            this.cmbReferralCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReferralCode.FormattingEnabled = true;
            this.cmbReferralCode.Location = new System.Drawing.Point(421, 82);
            this.cmbReferralCode.Name = "cmbReferralCode";
            this.cmbReferralCode.Size = new System.Drawing.Size(152, 22);
            this.cmbReferralCode.TabIndex = 45;
            this.cmbReferralCode.MouseEnter += new System.EventHandler(this.cmbReferralCode_MouseEnter);
            // 
            // cmbPatientGivenEPSDTReferral
            // 
            this.cmbPatientGivenEPSDTReferral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientGivenEPSDTReferral.Enabled = false;
            this.cmbPatientGivenEPSDTReferral.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientGivenEPSDTReferral.FormattingEnabled = true;
            this.cmbPatientGivenEPSDTReferral.Location = new System.Drawing.Point(64, 82);
            this.cmbPatientGivenEPSDTReferral.Name = "cmbPatientGivenEPSDTReferral";
            this.cmbPatientGivenEPSDTReferral.Size = new System.Drawing.Size(252, 22);
            this.cmbPatientGivenEPSDTReferral.TabIndex = 44;
            this.cmbPatientGivenEPSDTReferral.MouseEnter += new System.EventHandler(this.cmbPatientGivenEPSDTReferral_MouseEnter);
            // 
            // chkPatientGivenEPSDTReferral
            // 
            this.chkPatientGivenEPSDTReferral.AutoSize = true;
            this.chkPatientGivenEPSDTReferral.Enabled = false;
            this.chkPatientGivenEPSDTReferral.Location = new System.Drawing.Point(44, 58);
            this.chkPatientGivenEPSDTReferral.Name = "chkPatientGivenEPSDTReferral";
            this.chkPatientGivenEPSDTReferral.Size = new System.Drawing.Size(185, 18);
            this.chkPatientGivenEPSDTReferral.TabIndex = 43;
            this.chkPatientGivenEPSDTReferral.Text = "Patient Given EPSDT Referral";
            this.chkPatientGivenEPSDTReferral.UseVisualStyleBackColor = true;
            this.chkPatientGivenEPSDTReferral.CheckedChanged += new System.EventHandler(this.chkPatientGivenEPSDTReferral_CheckedChanged);
            // 
            // chkClaimIncludesanEPSDTScreening
            // 
            this.chkClaimIncludesanEPSDTScreening.AutoSize = true;
            this.chkClaimIncludesanEPSDTScreening.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkClaimIncludesanEPSDTScreening.Location = new System.Drawing.Point(30, 34);
            this.chkClaimIncludesanEPSDTScreening.Name = "chkClaimIncludesanEPSDTScreening";
            this.chkClaimIncludesanEPSDTScreening.Size = new System.Drawing.Size(218, 18);
            this.chkClaimIncludesanEPSDTScreening.TabIndex = 42;
            this.chkClaimIncludesanEPSDTScreening.Text = "Claim Includes an EPSDT Screening";
            this.chkClaimIncludesanEPSDTScreening.UseVisualStyleBackColor = true;
            this.chkClaimIncludesanEPSDTScreening.CheckedChanged += new System.EventHandler(this.chkClaimIncludesanEPSDTScreening_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 41;
            this.label1.Text = "EPSDT";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.chkThisServiceIsTheResultOfScreening);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.chkThisServiceIsTheScreening);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.lblReferralCode);
            this.panel1.Controls.Add(this.cmbPatientGivenEPSDTReferral);
            this.panel1.Controls.Add(this.cmbReferralCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkClaimIncludesanEPSDTScreening);
            this.panel1.Controls.Add(this.chkPatientGivenEPSDTReferral);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.panel1.Location = new System.Drawing.Point(0, 118);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(596, 170);
            this.panel1.TabIndex = 119;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(4, 166);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(588, 1);
            this.label21.TabIndex = 29;
            this.label21.Text = "label2";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 166);
            this.label22.TabIndex = 28;
            this.label22.Text = "label4";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(592, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 166);
            this.label23.TabIndex = 27;
            this.label23.Text = "label3";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(590, 1);
            this.label24.TabIndex = 26;
            this.label24.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkServiceFamilyPlanningIndicator);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.panel2.Location = new System.Drawing.Point(0, 288);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(596, 65);
            this.panel2.TabIndex = 120;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(4, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(588, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 61);
            this.label4.TabIndex = 28;
            this.label4.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(592, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 61);
            this.label10.TabIndex = 27;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(590, 1);
            this.label11.TabIndex = 26;
            this.label11.Text = "label1";
            // 
            // frmSetupEPSDTFamilyPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(596, 353);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnltlsStrip);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupEPSDTFamilyPlanning";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EPSDT/Family Planning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupEPSDTFamilyPlanning_FormClosing);
            this.Load += new System.EventHandler(this.frmSetupEPSDTFamilyPlanning_Load);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDOSText;
        private System.Windows.Forms.Label lblCPTDescText;
        private System.Windows.Forms.Label lblMod2Text;
        private System.Windows.Forms.Label lblMod1Text;
        private System.Windows.Forms.Label lblCPTCodeText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkClaimIncludesanEPSDTScreening;
        private System.Windows.Forms.CheckBox chkPatientGivenEPSDTReferral;
        private System.Windows.Forms.ComboBox cmbReferralCode;
        private System.Windows.Forms.ComboBox cmbPatientGivenEPSDTReferral;
        private System.Windows.Forms.Label lblReferralCode;
        private System.Windows.Forms.CheckBox chkThisServiceIsTheScreening;
        private System.Windows.Forms.CheckBox chkThisServiceIsTheResultOfScreening;
        private System.Windows.Forms.CheckBox chkServiceFamilyPlanningIndicator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}