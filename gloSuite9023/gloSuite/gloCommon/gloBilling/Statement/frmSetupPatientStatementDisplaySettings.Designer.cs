namespace gloBilling
{
    partial class frmSetupPatientStatementDisplaySettings
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
                    if (dtpOfficeEndTime != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOfficeEndTime);
                        }
                        catch
                        {
                        }
                        dtpOfficeEndTime.Dispose();
                        dtpOfficeEndTime = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtpOfficeStartTime != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOfficeStartTime);
                        }
                        catch
                        {
                        }
                        dtpOfficeStartTime.Dispose();
                        dtpOfficeStartTime = null;
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPatientStatementDisplaySettings));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel20 = new System.Windows.Forms.Panel();
            this.Panel21 = new System.Windows.Forms.Panel();
            this.chkDefault1 = new System.Windows.Forms.CheckBox();
            this.txtStatementDisplaySettingsName = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label47 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlOther = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.c1StatementMessage = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label67 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkIncludeonEachStatement = new System.Windows.Forms.CheckBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtClinicMessage1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblDetachInstructions = new System.Windows.Forms.Label();
            this.txtDetachInstructions = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkIncludeClaim = new System.Windows.Forms.CheckBox();
            this.chkPaymentRemit = new System.Windows.Forms.CheckBox();
            this.label66 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkGuarantorIndicator = new System.Windows.Forms.CheckBox();
            this.chkPendingInsurance = new System.Windows.Forms.CheckBox();
            this.txtClinicMessage2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlOtherAddress = new System.Windows.Forms.Panel();
            this.pnlInternalZipControl = new System.Windows.Forms.Panel();
            this.txtOtherName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOtherState = new System.Windows.Forms.ComboBox();
            this.txtOtherZip = new System.Windows.Forms.TextBox();
            this.txtOtherCity = new System.Windows.Forms.TextBox();
            this.txtOtherAddress2 = new System.Windows.Forms.TextBox();
            this.txtOtherAddress1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.pnlRbuttons = new System.Windows.Forms.Panel();
            this.rbOtherAddress = new System.Windows.Forms.RadioButton();
            this.rbBillingProvider = new System.Windows.Forms.RadioButton();
            this.rbRemitAddress = new System.Windows.Forms.RadioButton();
            this.label58 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.pnlRemitAddress = new System.Windows.Forms.Panel();
            this.txtRemitName = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.cmbRemitState = new System.Windows.Forms.ComboBox();
            this.txtRemitZip = new System.Windows.Forms.TextBox();
            this.txtRemitCity = new System.Windows.Forms.TextBox();
            this.txtRemitAddress2 = new System.Windows.Forms.TextBox();
            this.txtRemitAddress1 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbRemitOtherAddress = new System.Windows.Forms.RadioButton();
            this.rbRemitBillingProvider = new System.Windows.Forms.RadioButton();
            this.label62 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.pnlTaxInfo = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPracticeTaxID = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlOfficeHrs = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpOfficeEndTime = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpOfficeStartTime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.pnlBillingContactInfo = new System.Windows.Forms.Panel();
            this.txtBillingEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.txtBillingURL = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBillingContactPhone = new gloMaskControl.gloMaskBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtBillingContactName = new System.Windows.Forms.TextBox();
            this.pnlCardInfo = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.lbl_pnlProviderBottomBrd = new System.Windows.Forms.Label();
            this.pnlProviderBody = new System.Windows.Forms.Panel();
            this.trvCreditCard = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectCreditCard = new System.Windows.Forms.Button();
            this.btnSelectCreditCard = new System.Windows.Forms.Button();
            this.lbl_pnlProviderHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTopBrd = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlPracAdd = new System.Windows.Forms.Panel();
            this.lblAddressDetails = new System.Windows.Forms.Label();
            this.cmbPracState = new System.Windows.Forms.ComboBox();
            this.txtPracZip = new System.Windows.Forms.TextBox();
            this.txtPracCity = new System.Windows.Forms.TextBox();
            this.txtPracAddress2 = new System.Windows.Forms.TextBox();
            this.txtPracAddress1 = new System.Windows.Forms.TextBox();
            this.lblOIZip = new System.Windows.Forms.Label();
            this.lblOIState = new System.Windows.Forms.Label();
            this.lblOICity = new System.Windows.Forms.Label();
            this.lblOIAddressLine2 = new System.Windows.Forms.Label();
            this.lblOIAddressLine1 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.imgtabControl1 = new System.Windows.Forms.ImageList(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.Panel20.SuspendLayout();
            this.Panel21.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlOther.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatementMessage)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlOtherAddress.SuspendLayout();
            this.pnlRbuttons.SuspendLayout();
            this.pnlRemitAddress.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlTaxInfo.SuspendLayout();
            this.pnlOfficeHrs.SuspendLayout();
            this.pnlBillingContactInfo.SuspendLayout();
            this.pnlCardInfo.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.pnlPracAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(680, 54);
            this.pnl_tlsp_Top.TabIndex = 18;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.btnOK,
            this.btnClose});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(680, 53);
            this.tstrip.TabIndex = 0;
            this.tstrip.Text = "ToolStrip1";
            this.tstrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "&Save";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 50);
            this.btnOK.Tag = "Save&Close";
            this.btnOK.Text = "Sa&ve&&Cls";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Save and Close";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 50);
            this.btnClose.Tag = "Close";
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close";
            // 
            // Panel20
            // 
            this.Panel20.Controls.Add(this.Panel21);
            this.Panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel20.Location = new System.Drawing.Point(0, 54);
            this.Panel20.Name = "Panel20";
            this.Panel20.Padding = new System.Windows.Forms.Padding(3);
            this.Panel20.Size = new System.Drawing.Size(680, 30);
            this.Panel20.TabIndex = 81;
            // 
            // Panel21
            // 
            this.Panel21.BackColor = System.Drawing.Color.Transparent;
            this.Panel21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel21.BackgroundImage")));
            this.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel21.Controls.Add(this.chkDefault1);
            this.Panel21.Controls.Add(this.txtStatementDisplaySettingsName);
            this.Panel21.Controls.Add(this.Label23);
            this.Panel21.Controls.Add(this.Label44);
            this.Panel21.Controls.Add(this.Label45);
            this.Panel21.Controls.Add(this.Label46);
            this.Panel21.Controls.Add(this.Label47);
            this.Panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel21.Location = new System.Drawing.Point(3, 3);
            this.Panel21.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel21.Name = "Panel21";
            this.Panel21.Size = new System.Drawing.Size(674, 24);
            this.Panel21.TabIndex = 19;
            // 
            // chkDefault1
            // 
            this.chkDefault1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDefault1.AutoSize = true;
            this.chkDefault1.Location = new System.Drawing.Point(346, 3);
            this.chkDefault1.Name = "chkDefault1";
            this.chkDefault1.Size = new System.Drawing.Size(65, 18);
            this.chkDefault1.TabIndex = 1;
            this.chkDefault1.Text = "Default";
            this.chkDefault1.UseVisualStyleBackColor = true;
            // 
            // txtStatementDisplaySettingsName
            // 
            this.txtStatementDisplaySettingsName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStatementDisplaySettingsName.Location = new System.Drawing.Point(63, 1);
            this.txtStatementDisplaySettingsName.MaxLength = 50;
            this.txtStatementDisplaySettingsName.Name = "txtStatementDisplaySettingsName";
            this.txtStatementDisplaySettingsName.Size = new System.Drawing.Size(272, 22);
            this.txtStatementDisplaySettingsName.TabIndex = 0;
            // 
            // Label23
            // 
            this.Label23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label23.AutoSize = true;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1, 1);
            this.Label23.Name = "Label23";
            this.Label23.Padding = new System.Windows.Forms.Padding(3);
            this.Label23.Size = new System.Drawing.Size(62, 20);
            this.Label23.TabIndex = 0;
            this.Label23.Text = "  Name :";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label44.Location = new System.Drawing.Point(1, 23);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(672, 1);
            this.Label44.TabIndex = 8;
            this.Label44.Text = "label2";
            // 
            // Label45
            // 
            this.Label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(0, 1);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(1, 23);
            this.Label45.TabIndex = 7;
            this.Label45.Text = "label4";
            // 
            // Label46
            // 
            this.Label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label46.Location = new System.Drawing.Point(673, 1);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(1, 23);
            this.Label46.TabIndex = 6;
            this.Label46.Text = "label3";
            // 
            // Label47
            // 
            this.Label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label47.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(0, 0);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(674, 1);
            this.Label47.TabIndex = 5;
            this.Label47.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlOther);
            this.panel1.Controls.Add(this.pnlOtherAddress);
            this.panel1.Controls.Add(this.pnlRbuttons);
            this.panel1.Controls.Add(this.pnlRemitAddress);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.pnlTaxInfo);
            this.panel1.Controls.Add(this.pnlOfficeHrs);
            this.panel1.Controls.Add(this.pnlBillingContactInfo);
            this.panel1.Controls.Add(this.pnlCardInfo);
            this.panel1.Controls.Add(this.pnlPracAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(680, 706);
            this.panel1.TabIndex = 82;
            // 
            // pnlOther
            // 
            this.pnlOther.Controls.Add(this.panel4);
            this.pnlOther.Controls.Add(this.panel3);
            this.pnlOther.Controls.Add(this.panel6);
            this.pnlOther.Controls.Add(this.panel2);
            this.pnlOther.Controls.Add(this.checkBox1);
            this.pnlOther.Controls.Add(this.chkGuarantorIndicator);
            this.pnlOther.Controls.Add(this.chkPendingInsurance);
            this.pnlOther.Controls.Add(this.txtClinicMessage2);
            this.pnlOther.Controls.Add(this.label30);
            this.pnlOther.Controls.Add(this.label15);
            this.pnlOther.Controls.Add(this.label50);
            this.pnlOther.Controls.Add(this.label3);
            this.pnlOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOther.Location = new System.Drawing.Point(3, 443);
            this.pnlOther.Name = "pnlOther";
            this.pnlOther.Size = new System.Drawing.Size(674, 260);
            this.pnlOther.TabIndex = 330;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.c1StatementMessage);
            this.panel4.Controls.Add(this.label67);
            this.panel4.Controls.Add(this.label51);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 143);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(673, 117);
            this.panel4.TabIndex = 9;
            // 
            // c1StatementMessage
            // 
            this.c1StatementMessage.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1StatementMessage.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1StatementMessage.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1StatementMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1StatementMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1StatementMessage.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1StatementMessage.ColumnInfo = resources.GetString("c1StatementMessage.ColumnInfo");
            this.c1StatementMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1StatementMessage.ExtendLastCol = true;
            this.c1StatementMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1StatementMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1StatementMessage.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1StatementMessage.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1StatementMessage.Location = new System.Drawing.Point(1, 0);
            this.c1StatementMessage.Name = "c1StatementMessage";
            this.c1StatementMessage.Rows.Count = 6;
            this.c1StatementMessage.Rows.DefaultSize = 17;
            this.c1StatementMessage.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1StatementMessage.Size = new System.Drawing.Size(672, 116);
            this.c1StatementMessage.StyleInfo = resources.GetString("c1StatementMessage.StyleInfo");
            this.c1StatementMessage.TabIndex = 319;
            this.c1StatementMessage.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1StatementMessage_StartEdit);
            this.c1StatementMessage.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1StatementMessage_SetupEditor);
            this.c1StatementMessage.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1StatementMessage_KeyPressEdit);
            this.c1StatementMessage.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1StatementMessage_CellChanged);
            this.c1StatementMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1StatementMessage_MouseMove);
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label67.Location = new System.Drawing.Point(1, 116);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(672, 1);
            this.label67.TabIndex = 318;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Location = new System.Drawing.Point(0, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 117);
            this.label51.TabIndex = 317;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkIncludeonEachStatement);
            this.panel3.Controls.Add(this.label39);
            this.panel3.Controls.Add(this.txtClinicMessage1);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(673, 86);
            this.panel3.TabIndex = 8;
            // 
            // chkIncludeonEachStatement
            // 
            this.chkIncludeonEachStatement.AutoSize = true;
            this.chkIncludeonEachStatement.Location = new System.Drawing.Point(112, 57);
            this.chkIncludeonEachStatement.Name = "chkIncludeonEachStatement";
            this.chkIncludeonEachStatement.Size = new System.Drawing.Size(183, 18);
            this.chkIncludeonEachStatement.TabIndex = 319;
            this.chkIncludeonEachStatement.Text = "Include On Every Statement";
            this.chkIncludeonEachStatement.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Left;
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 86);
            this.label39.TabIndex = 317;
            // 
            // txtClinicMessage1
            // 
            this.txtClinicMessage1.Location = new System.Drawing.Point(110, 3);
            this.txtClinicMessage1.MaxLength = 200;
            this.txtClinicMessage1.Multiline = true;
            this.txtClinicMessage1.Name = "txtClinicMessage1";
            this.txtClinicMessage1.Size = new System.Drawing.Size(550, 50);
            this.txtClinicMessage1.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(10, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 14);
            this.label16.TabIndex = 312;
            this.label16.Text = "Clinic Message :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lblDetachInstructions);
            this.panel6.Controls.Add(this.txtDetachInstructions);
            this.panel6.Controls.Add(this.label70);
            this.panel6.Controls.Add(this.label71);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 29);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(673, 28);
            this.panel6.TabIndex = 320;
            // 
            // lblDetachInstructions
            // 
            this.lblDetachInstructions.AutoSize = true;
            this.lblDetachInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDetachInstructions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetachInstructions.Location = new System.Drawing.Point(19, 7);
            this.lblDetachInstructions.Name = "lblDetachInstructions";
            this.lblDetachInstructions.Size = new System.Drawing.Size(223, 14);
            this.lblDetachInstructions.TabIndex = 320;
            this.lblDetachInstructions.Text = "   Detach and Return Instructions :";
            this.lblDetachInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetachInstructions
            // 
            this.txtDetachInstructions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetachInstructions.ForeColor = System.Drawing.Color.Black;
            this.txtDetachInstructions.Location = new System.Drawing.Point(247, 2);
            this.txtDetachInstructions.MaxLength = 76;
            this.txtDetachInstructions.Name = "txtDetachInstructions";
            this.txtDetachInstructions.Size = new System.Drawing.Size(413, 22);
            this.txtDetachInstructions.TabIndex = 318;
            this.txtDetachInstructions.MouseHover += new System.EventHandler(this.txtDetachInstructions_MouseHover);
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Left;
            this.label70.Location = new System.Drawing.Point(0, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 27);
            this.label70.TabIndex = 308;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label71.Location = new System.Drawing.Point(0, 27);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(673, 1);
            this.label71.TabIndex = 307;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkIncludeClaim);
            this.panel2.Controls.Add(this.chkPaymentRemit);
            this.panel2.Controls.Add(this.label66);
            this.panel2.Controls.Add(this.label69);
            this.panel2.Controls.Add(this.label59);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(673, 28);
            this.panel2.TabIndex = 7;
            // 
            // chkIncludeClaim
            // 
            this.chkIncludeClaim.AutoSize = true;
            this.chkIncludeClaim.Location = new System.Drawing.Point(359, 5);
            this.chkIncludeClaim.Name = "chkIncludeClaim";
            this.chkIncludeClaim.Size = new System.Drawing.Size(110, 18);
            this.chkIncludeClaim.TabIndex = 319;
            this.chkIncludeClaim.Text = "Include Claim #";
            this.chkIncludeClaim.UseVisualStyleBackColor = true;
            // 
            // chkPaymentRemit
            // 
            this.chkPaymentRemit.AutoSize = true;
            this.chkPaymentRemit.Location = new System.Drawing.Point(134, 5);
            this.chkPaymentRemit.Name = "chkPaymentRemit";
            this.chkPaymentRemit.Size = new System.Drawing.Size(210, 18);
            this.chkPaymentRemit.TabIndex = 318;
            this.chkPaymentRemit.Text = "Include Insurance Payment Remit";
            this.chkPaymentRemit.UseVisualStyleBackColor = true;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(20, 6);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(109, 14);
            this.label66.TabIndex = 0;
            this.label66.Text = "   Service Detail :";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Left;
            this.label69.Location = new System.Drawing.Point(0, 0);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1, 27);
            this.label69.TabIndex = 308;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label59.Location = new System.Drawing.Point(0, 27);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(673, 1);
            this.label59.TabIndex = 307;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(279, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(210, 18);
            this.checkBox1.TabIndex = 318;
            this.checkBox1.Text = "Include Insurance Payment Remit";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkGuarantorIndicator
            // 
            this.chkGuarantorIndicator.AutoSize = true;
            this.chkGuarantorIndicator.Location = new System.Drawing.Point(430, 205);
            this.chkGuarantorIndicator.Name = "chkGuarantorIndicator";
            this.chkGuarantorIndicator.Size = new System.Drawing.Size(132, 18);
            this.chkGuarantorIndicator.TabIndex = 1;
            this.chkGuarantorIndicator.Text = "Guarantor Indicator";
            this.chkGuarantorIndicator.UseVisualStyleBackColor = true;
            this.chkGuarantorIndicator.Visible = false;
            // 
            // chkPendingInsurance
            // 
            this.chkPendingInsurance.AutoSize = true;
            this.chkPendingInsurance.Location = new System.Drawing.Point(297, 205);
            this.chkPendingInsurance.Name = "chkPendingInsurance";
            this.chkPendingInsurance.Size = new System.Drawing.Size(127, 18);
            this.chkPendingInsurance.TabIndex = 0;
            this.chkPendingInsurance.Text = "Pending Insurance";
            this.chkPendingInsurance.UseVisualStyleBackColor = true;
            this.chkPendingInsurance.Visible = false;
            // 
            // txtClinicMessage2
            // 
            this.txtClinicMessage2.Location = new System.Drawing.Point(204, 124);
            this.txtClinicMessage2.MaxLength = 255;
            this.txtClinicMessage2.Name = "txtClinicMessage2";
            this.txtClinicMessage2.Size = new System.Drawing.Size(297, 22);
            this.txtClinicMessage2.TabIndex = 32;
            this.txtClinicMessage2.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(99, 129);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 14);
            this.label30.TabIndex = 314;
            this.label30.Text = "Clinic Message 2 :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label30.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(163, 205);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 14);
            this.label15.TabIndex = 8;
            this.label15.Text = "Other Information :";
            this.label15.Visible = false;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Location = new System.Drawing.Point(673, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 259);
            this.label50.TabIndex = 316;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(674, 1);
            this.label3.TabIndex = 319;
            // 
            // pnlOtherAddress
            // 
            this.pnlOtherAddress.Controls.Add(this.pnlInternalZipControl);
            this.pnlOtherAddress.Controls.Add(this.txtOtherName);
            this.pnlOtherAddress.Controls.Add(this.label1);
            this.pnlOtherAddress.Controls.Add(this.cmbOtherState);
            this.pnlOtherAddress.Controls.Add(this.txtOtherZip);
            this.pnlOtherAddress.Controls.Add(this.txtOtherCity);
            this.pnlOtherAddress.Controls.Add(this.txtOtherAddress2);
            this.pnlOtherAddress.Controls.Add(this.txtOtherAddress1);
            this.pnlOtherAddress.Controls.Add(this.label6);
            this.pnlOtherAddress.Controls.Add(this.label7);
            this.pnlOtherAddress.Controls.Add(this.label9);
            this.pnlOtherAddress.Controls.Add(this.label11);
            this.pnlOtherAddress.Controls.Add(this.label53);
            this.pnlOtherAddress.Controls.Add(this.label55);
            this.pnlOtherAddress.Controls.Add(this.label56);
            this.pnlOtherAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOtherAddress.Location = new System.Drawing.Point(3, 305);
            this.pnlOtherAddress.Name = "pnlOtherAddress";
            this.pnlOtherAddress.Size = new System.Drawing.Size(674, 138);
            this.pnlOtherAddress.TabIndex = 6;
            this.pnlOtherAddress.Visible = false;
            // 
            // pnlInternalZipControl
            // 
            this.pnlInternalZipControl.Location = new System.Drawing.Point(412, 33);
            this.pnlInternalZipControl.Name = "pnlInternalZipControl";
            this.pnlInternalZipControl.Size = new System.Drawing.Size(217, 99);
            this.pnlInternalZipControl.TabIndex = 5;
            this.pnlInternalZipControl.Visible = false;
            // 
            // txtOtherName
            // 
            this.txtOtherName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherName.ForeColor = System.Drawing.Color.Black;
            this.txtOtherName.Location = new System.Drawing.Point(334, 6);
            this.txtOtherName.MaxLength = 50;
            this.txtOtherName.Name = "txtOtherName";
            this.txtOtherName.Size = new System.Drawing.Size(324, 22);
            this.txtOtherName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(244, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Other Name :";
            // 
            // cmbOtherState
            // 
            this.cmbOtherState.FormattingEnabled = true;
            this.cmbOtherState.Location = new System.Drawing.Point(534, 86);
            this.cmbOtherState.MaxLength = 20;
            this.cmbOtherState.Name = "cmbOtherState";
            this.cmbOtherState.Size = new System.Drawing.Size(94, 22);
            this.cmbOtherState.TabIndex = 5;
            this.cmbOtherState.SelectedIndexChanged += new System.EventHandler(this.cmbOtherState_SelectedIndexChanged);
            // 
            // txtOtherZip
            // 
            this.txtOtherZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherZip.ForeColor = System.Drawing.Color.Black;
            this.txtOtherZip.Location = new System.Drawing.Point(334, 111);
            this.txtOtherZip.MaxLength = 5;
            this.txtOtherZip.Name = "txtOtherZip";
            this.txtOtherZip.Size = new System.Drawing.Size(74, 22);
            this.txtOtherZip.TabIndex = 3;
            this.txtOtherZip.TextChanged += new System.EventHandler(this.txtOtherZip_TextChanged);
            this.txtOtherZip.Enter += new System.EventHandler(this.txtOtherZip_GotFocus);
            this.txtOtherZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherZip_KeyDown);
            this.txtOtherZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOtherZip_KeyPress);
            this.txtOtherZip.Leave += new System.EventHandler(this.txtOtherZip_LostFocus);
            // 
            // txtOtherCity
            // 
            this.txtOtherCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherCity.ForeColor = System.Drawing.Color.Black;
            this.txtOtherCity.Location = new System.Drawing.Point(334, 85);
            this.txtOtherCity.MaxLength = 50;
            this.txtOtherCity.Name = "txtOtherCity";
            this.txtOtherCity.Size = new System.Drawing.Size(151, 22);
            this.txtOtherCity.TabIndex = 4;
            this.txtOtherCity.TextChanged += new System.EventHandler(this.txtOtherCity_TextChanged);
            // 
            // txtOtherAddress2
            // 
            this.txtOtherAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtOtherAddress2.Location = new System.Drawing.Point(334, 59);
            this.txtOtherAddress2.MaxLength = 50;
            this.txtOtherAddress2.Name = "txtOtherAddress2";
            this.txtOtherAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtOtherAddress2.TabIndex = 2;
            // 
            // txtOtherAddress1
            // 
            this.txtOtherAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtOtherAddress1.Location = new System.Drawing.Point(334, 33);
            this.txtOtherAddress1.MaxLength = 50;
            this.txtOtherAddress1.Name = "txtOtherAddress1";
            this.txtOtherAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtOtherAddress1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(295, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "Zip :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(486, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "State :";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(291, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "City :";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(231, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 14);
            this.label11.TabIndex = 6;
            this.label11.Text = "Address Line 2 :";
            // 
            // label53
            // 
            this.label53.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label53.AutoEllipsis = true;
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(231, 37);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(95, 14);
            this.label53.TabIndex = 3;
            this.label53.Text = "Address Line 1 :";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(0, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 138);
            this.label55.TabIndex = 316;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Location = new System.Drawing.Point(673, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 138);
            this.label56.TabIndex = 317;
            // 
            // pnlRbuttons
            // 
            this.pnlRbuttons.Controls.Add(this.rbOtherAddress);
            this.pnlRbuttons.Controls.Add(this.rbBillingProvider);
            this.pnlRbuttons.Controls.Add(this.rbRemitAddress);
            this.pnlRbuttons.Controls.Add(this.label58);
            this.pnlRbuttons.Controls.Add(this.label60);
            this.pnlRbuttons.Controls.Add(this.label61);
            this.pnlRbuttons.Controls.Add(this.label32);
            this.pnlRbuttons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRbuttons.Location = new System.Drawing.Point(3, 276);
            this.pnlRbuttons.Name = "pnlRbuttons";
            this.pnlRbuttons.Size = new System.Drawing.Size(674, 29);
            this.pnlRbuttons.TabIndex = 5;
            // 
            // rbOtherAddress
            // 
            this.rbOtherAddress.AutoSize = true;
            this.rbOtherAddress.Location = new System.Drawing.Point(441, 6);
            this.rbOtherAddress.Name = "rbOtherAddress";
            this.rbOtherAddress.Size = new System.Drawing.Size(151, 18);
            this.rbOtherAddress.TabIndex = 2;
            this.rbOtherAddress.Text = "Other Name && Address";
            this.rbOtherAddress.UseVisualStyleBackColor = true;
            this.rbOtherAddress.CheckedChanged += new System.EventHandler(this.rbOtherAddress_CheckedChanged);
            // 
            // rbBillingProvider
            // 
            this.rbBillingProvider.AutoSize = true;
            this.rbBillingProvider.Location = new System.Drawing.Point(298, 6);
            this.rbBillingProvider.Name = "rbBillingProvider";
            this.rbBillingProvider.Size = new System.Drawing.Size(120, 18);
            this.rbBillingProvider.TabIndex = 1;
            this.rbBillingProvider.Text = "Patient\'s Provider";
            this.rbBillingProvider.UseVisualStyleBackColor = true;
            this.rbBillingProvider.CheckedChanged += new System.EventHandler(this.rbBillingProvider_CheckedChanged);
            // 
            // rbRemitAddress
            // 
            this.rbRemitAddress.AutoSize = true;
            this.rbRemitAddress.Checked = true;
            this.rbRemitAddress.Location = new System.Drawing.Point(134, 6);
            this.rbRemitAddress.Name = "rbRemitAddress";
            this.rbRemitAddress.Size = new System.Drawing.Size(150, 18);
            this.rbRemitAddress.TabIndex = 0;
            this.rbRemitAddress.TabStop = true;
            this.rbRemitAddress.Text = "Remit Name && Address";
            this.rbRemitAddress.UseVisualStyleBackColor = true;
            this.rbRemitAddress.CheckedChanged += new System.EventHandler(this.rbRemitAddress_CheckedChanged);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(36, 7);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(93, 14);
            this.label58.TabIndex = 309;
            this.label58.Text = "   Payable To :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Location = new System.Drawing.Point(0, 1);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 28);
            this.label60.TabIndex = 1;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Location = new System.Drawing.Point(673, 1);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 28);
            this.label61.TabIndex = 308;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(674, 1);
            this.label32.TabIndex = 316;
            // 
            // pnlRemitAddress
            // 
            this.pnlRemitAddress.Controls.Add(this.txtRemitName);
            this.pnlRemitAddress.Controls.Add(this.label63);
            this.pnlRemitAddress.Controls.Add(this.pnlInternalControl);
            this.pnlRemitAddress.Controls.Add(this.cmbRemitState);
            this.pnlRemitAddress.Controls.Add(this.txtRemitZip);
            this.pnlRemitAddress.Controls.Add(this.txtRemitCity);
            this.pnlRemitAddress.Controls.Add(this.txtRemitAddress2);
            this.pnlRemitAddress.Controls.Add(this.txtRemitAddress1);
            this.pnlRemitAddress.Controls.Add(this.label25);
            this.pnlRemitAddress.Controls.Add(this.label26);
            this.pnlRemitAddress.Controls.Add(this.label27);
            this.pnlRemitAddress.Controls.Add(this.label28);
            this.pnlRemitAddress.Controls.Add(this.label29);
            this.pnlRemitAddress.Controls.Add(this.label49);
            this.pnlRemitAddress.Controls.Add(this.label38);
            this.pnlRemitAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRemitAddress.Location = new System.Drawing.Point(3, 135);
            this.pnlRemitAddress.Name = "pnlRemitAddress";
            this.pnlRemitAddress.Size = new System.Drawing.Size(674, 141);
            this.pnlRemitAddress.TabIndex = 4;
            this.pnlRemitAddress.Visible = false;
            // 
            // txtRemitName
            // 
            this.txtRemitName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitName.ForeColor = System.Drawing.Color.Black;
            this.txtRemitName.Location = new System.Drawing.Point(336, 6);
            this.txtRemitName.MaxLength = 50;
            this.txtRemitName.Name = "txtRemitName";
            this.txtRemitName.Size = new System.Drawing.Size(322, 22);
            this.txtRemitName.TabIndex = 0;
            // 
            // label63
            // 
            this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label63.AutoEllipsis = true;
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(248, 10);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(81, 14);
            this.label63.TabIndex = 1;
            this.label63.Text = "Remit Name :";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.Location = new System.Drawing.Point(411, 40);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(217, 93);
            this.pnlInternalControl.TabIndex = 5;
            this.pnlInternalControl.Visible = false;
            // 
            // cmbRemitState
            // 
            this.cmbRemitState.FormattingEnabled = true;
            this.cmbRemitState.Location = new System.Drawing.Point(536, 85);
            this.cmbRemitState.MaxLength = 20;
            this.cmbRemitState.Name = "cmbRemitState";
            this.cmbRemitState.Size = new System.Drawing.Size(94, 22);
            this.cmbRemitState.TabIndex = 5;
            this.cmbRemitState.SelectedIndexChanged += new System.EventHandler(this.cmbRemitState_SelectedIndexChanged);
            // 
            // txtRemitZip
            // 
            this.txtRemitZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitZip.ForeColor = System.Drawing.Color.Black;
            this.txtRemitZip.Location = new System.Drawing.Point(336, 111);
            this.txtRemitZip.MaxLength = 5;
            this.txtRemitZip.Name = "txtRemitZip";
            this.txtRemitZip.Size = new System.Drawing.Size(74, 22);
            this.txtRemitZip.TabIndex = 3;
            this.txtRemitZip.TextChanged += new System.EventHandler(this.txtRemitZip_TextChanged);
            this.txtRemitZip.Enter += new System.EventHandler(this.txtZip_GotFocus);
            this.txtRemitZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemitZip_KeyDown);
            this.txtRemitZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemitZip_KeyPress);
            this.txtRemitZip.Leave += new System.EventHandler(this.txtZip_LostFocus);
            // 
            // txtRemitCity
            // 
            this.txtRemitCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitCity.ForeColor = System.Drawing.Color.Black;
            this.txtRemitCity.Location = new System.Drawing.Point(336, 85);
            this.txtRemitCity.MaxLength = 50;
            this.txtRemitCity.Name = "txtRemitCity";
            this.txtRemitCity.Size = new System.Drawing.Size(151, 22);
            this.txtRemitCity.TabIndex = 4;
            this.txtRemitCity.TextChanged += new System.EventHandler(this.txtRemitCity_TextChanged);
            // 
            // txtRemitAddress2
            // 
            this.txtRemitAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtRemitAddress2.Location = new System.Drawing.Point(336, 59);
            this.txtRemitAddress2.MaxLength = 50;
            this.txtRemitAddress2.Name = "txtRemitAddress2";
            this.txtRemitAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtRemitAddress2.TabIndex = 2;
            // 
            // txtRemitAddress1
            // 
            this.txtRemitAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemitAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtRemitAddress1.Location = new System.Drawing.Point(336, 33);
            this.txtRemitAddress1.MaxLength = 50;
            this.txtRemitAddress1.Name = "txtRemitAddress1";
            this.txtRemitAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtRemitAddress1.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(298, 115);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(31, 14);
            this.label25.TabIndex = 12;
            this.label25.Text = "Zip :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(487, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 14);
            this.label26.TabIndex = 10;
            this.label26.Text = "State :";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(294, 89);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 14);
            this.label27.TabIndex = 8;
            this.label27.Text = "City :";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoEllipsis = true;
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(234, 63);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(95, 14);
            this.label28.TabIndex = 6;
            this.label28.Text = "Address Line 2 :";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(234, 37);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(95, 14);
            this.label29.TabIndex = 3;
            this.label29.Text = "Address Line 1 :";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Right;
            this.label49.Location = new System.Drawing.Point(673, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 141);
            this.label49.TabIndex = 317;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 141);
            this.label38.TabIndex = 316;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbRemitOtherAddress);
            this.panel5.Controls.Add(this.rbRemitBillingProvider);
            this.panel5.Controls.Add(this.label62);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.label68);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 106);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(674, 29);
            this.panel5.TabIndex = 3;
            // 
            // rbRemitOtherAddress
            // 
            this.rbRemitOtherAddress.AutoSize = true;
            this.rbRemitOtherAddress.Location = new System.Drawing.Point(272, 6);
            this.rbRemitOtherAddress.Name = "rbRemitOtherAddress";
            this.rbRemitOtherAddress.Size = new System.Drawing.Size(151, 18);
            this.rbRemitOtherAddress.TabIndex = 2;
            this.rbRemitOtherAddress.Text = "Other Name && Address";
            this.rbRemitOtherAddress.UseVisualStyleBackColor = true;
            this.rbRemitOtherAddress.CheckedChanged += new System.EventHandler(this.rbRemitOtherAddress_CheckedChanged);
            // 
            // rbRemitBillingProvider
            // 
            this.rbRemitBillingProvider.AutoSize = true;
            this.rbRemitBillingProvider.Checked = true;
            this.rbRemitBillingProvider.Location = new System.Drawing.Point(134, 6);
            this.rbRemitBillingProvider.Name = "rbRemitBillingProvider";
            this.rbRemitBillingProvider.Size = new System.Drawing.Size(120, 18);
            this.rbRemitBillingProvider.TabIndex = 1;
            this.rbRemitBillingProvider.TabStop = true;
            this.rbRemitBillingProvider.Text = "Patient\'s Provider";
            this.rbRemitBillingProvider.UseVisualStyleBackColor = true;
            this.rbRemitBillingProvider.CheckedChanged += new System.EventHandler(this.rbRemitBillingProvider_CheckedChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(13, 7);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(116, 14);
            this.label62.TabIndex = 309;
            this.label62.Text = "   Remit Address :";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Left;
            this.label65.Location = new System.Drawing.Point(0, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1, 29);
            this.label65.TabIndex = 1;
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Right;
            this.label68.Location = new System.Drawing.Point(673, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(1, 29);
            this.label68.TabIndex = 308;
            // 
            // pnlTaxInfo
            // 
            this.pnlTaxInfo.Controls.Add(this.label31);
            this.pnlTaxInfo.Controls.Add(this.label13);
            this.pnlTaxInfo.Controls.Add(this.txtPracticeTaxID);
            this.pnlTaxInfo.Controls.Add(this.label21);
            this.pnlTaxInfo.Controls.Add(this.label37);
            this.pnlTaxInfo.Controls.Add(this.label48);
            this.pnlTaxInfo.Location = new System.Drawing.Point(3, 249);
            this.pnlTaxInfo.Name = "pnlTaxInfo";
            this.pnlTaxInfo.Size = new System.Drawing.Size(590, 42);
            this.pnlTaxInfo.TabIndex = 4;
            this.pnlTaxInfo.Visible = false;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Location = new System.Drawing.Point(1, 41);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(588, 1);
            this.label31.TabIndex = 306;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(105, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(98, 14);
            this.label13.TabIndex = 1;
            this.label13.Text = "Practice Tax ID :";
            // 
            // txtPracticeTaxID
            // 
            this.txtPracticeTaxID.Location = new System.Drawing.Point(206, 12);
            this.txtPracticeTaxID.MaxLength = 50;
            this.txtPracticeTaxID.Name = "txtPracticeTaxID";
            this.txtPracticeTaxID.Size = new System.Drawing.Size(193, 22);
            this.txtPracticeTaxID.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(588, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Tax Information :";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 42);
            this.label37.TabIndex = 307;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Location = new System.Drawing.Point(589, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 42);
            this.label48.TabIndex = 308;
            // 
            // pnlOfficeHrs
            // 
            this.pnlOfficeHrs.Controls.Add(this.label24);
            this.pnlOfficeHrs.Controls.Add(this.dtpOfficeEndTime);
            this.pnlOfficeHrs.Controls.Add(this.label12);
            this.pnlOfficeHrs.Controls.Add(this.dtpOfficeStartTime);
            this.pnlOfficeHrs.Controls.Add(this.label8);
            this.pnlOfficeHrs.Controls.Add(this.label20);
            this.pnlOfficeHrs.Controls.Add(this.label36);
            this.pnlOfficeHrs.Controls.Add(this.label43);
            this.pnlOfficeHrs.Location = new System.Drawing.Point(3, 182);
            this.pnlOfficeHrs.Name = "pnlOfficeHrs";
            this.pnlOfficeHrs.Size = new System.Drawing.Size(562, 67);
            this.pnlOfficeHrs.TabIndex = 3;
            this.pnlOfficeHrs.Visible = false;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(1, 66);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(560, 1);
            this.label24.TabIndex = 328;
            // 
            // dtpOfficeEndTime
            // 
            this.dtpOfficeEndTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpOfficeEndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOfficeEndTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpOfficeEndTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpOfficeEndTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpOfficeEndTime.CustomFormat = "hh:mm tt";
            this.dtpOfficeEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOfficeEndTime.Location = new System.Drawing.Point(206, 39);
            this.dtpOfficeEndTime.Name = "dtpOfficeEndTime";
            this.dtpOfficeEndTime.ShowUpDown = true;
            this.dtpOfficeEndTime.Size = new System.Drawing.Size(95, 22);
            this.dtpOfficeEndTime.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(98, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 14);
            this.label12.TabIndex = 3;
            this.label12.Text = "Office End Time :";
            // 
            // dtpOfficeStartTime
            // 
            this.dtpOfficeStartTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(66)))), ((int)(((byte)(125)))));
            this.dtpOfficeStartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOfficeStartTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(146)))), ((int)(((byte)(207)))));
            this.dtpOfficeStartTime.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtpOfficeStartTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(222)))));
            this.dtpOfficeStartTime.CustomFormat = "hh:mm tt";
            this.dtpOfficeStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOfficeStartTime.Location = new System.Drawing.Point(206, 13);
            this.dtpOfficeStartTime.Name = "dtpOfficeStartTime";
            this.dtpOfficeStartTime.ShowUpDown = true;
            this.dtpOfficeStartTime.Size = new System.Drawing.Size(95, 22);
            this.dtpOfficeStartTime.TabIndex = 0;
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
            this.label8.Location = new System.Drawing.Point(92, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 14);
            this.label8.TabIndex = 1;
            this.label8.Text = "Office Start Time :";
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(560, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Office Hours :";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 67);
            this.label36.TabIndex = 329;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(561, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 67);
            this.label43.TabIndex = 330;
            // 
            // pnlBillingContactInfo
            // 
            this.pnlBillingContactInfo.Controls.Add(this.txtBillingEmail);
            this.pnlBillingContactInfo.Controls.Add(this.label4);
            this.pnlBillingContactInfo.Controls.Add(this.label19);
            this.pnlBillingContactInfo.Controls.Add(this.label54);
            this.pnlBillingContactInfo.Controls.Add(this.txtBillingURL);
            this.pnlBillingContactInfo.Controls.Add(this.label18);
            this.pnlBillingContactInfo.Controls.Add(this.txtBillingContactPhone);
            this.pnlBillingContactInfo.Controls.Add(this.label5);
            this.pnlBillingContactInfo.Controls.Add(this.label57);
            this.pnlBillingContactInfo.Controls.Add(this.label42);
            this.pnlBillingContactInfo.Controls.Add(this.label35);
            this.pnlBillingContactInfo.Controls.Add(this.txtBillingContactName);
            this.pnlBillingContactInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBillingContactInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlBillingContactInfo.Name = "pnlBillingContactInfo";
            this.pnlBillingContactInfo.Size = new System.Drawing.Size(674, 103);
            this.pnlBillingContactInfo.TabIndex = 2;
            // 
            // txtBillingEmail
            // 
            this.txtBillingEmail.Location = new System.Drawing.Point(334, 72);
            this.txtBillingEmail.MaxLength = 50;
            this.txtBillingEmail.Name = "txtBillingEmail";
            this.txtBillingEmail.Size = new System.Drawing.Size(322, 22);
            this.txtBillingEmail.TabIndex = 323;
            this.txtBillingEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtBillingEmail_Validating);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(285, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Email :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1, 1);
            this.label19.Name = "label19";
            this.label19.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label19.Size = new System.Drawing.Size(194, 17);
            this.label19.TabIndex = 0;
            this.label19.Text = "   Billing Contact Information :";
            // 
            // label54
            // 
            this.label54.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label54.AutoEllipsis = true;
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(291, 51);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(36, 14);
            this.label54.TabIndex = 322;
            this.label54.Text = "URL :";
            // 
            // txtBillingURL
            // 
            this.txtBillingURL.Location = new System.Drawing.Point(334, 47);
            this.txtBillingURL.MaxLength = 50;
            this.txtBillingURL.Name = "txtBillingURL";
            this.txtBillingURL.Size = new System.Drawing.Size(322, 22);
            this.txtBillingURL.TabIndex = 321;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(1, 102);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(672, 1);
            this.label18.TabIndex = 307;
            // 
            // txtBillingContactPhone
            // 
            this.txtBillingContactPhone.AllowValidate = true;
            this.txtBillingContactPhone.IncludeLiteralsAndPrompts = false;
            this.txtBillingContactPhone.Location = new System.Drawing.Point(334, 22);
            this.txtBillingContactPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtBillingContactPhone.Name = "txtBillingContactPhone";
            this.txtBillingContactPhone.ReadOnly = false;
            this.txtBillingContactPhone.Size = new System.Drawing.Size(96, 22);
            this.txtBillingContactPhone.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(197, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Billing Contact Phone :";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Top;
            this.label57.Location = new System.Drawing.Point(1, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(672, 1);
            this.label57.TabIndex = 320;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Right;
            this.label42.Location = new System.Drawing.Point(673, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 103);
            this.label42.TabIndex = 309;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Location = new System.Drawing.Point(0, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 103);
            this.label35.TabIndex = 308;
            // 
            // txtBillingContactName
            // 
            this.txtBillingContactName.Location = new System.Drawing.Point(334, 72);
            this.txtBillingContactName.MaxLength = 50;
            this.txtBillingContactName.Name = "txtBillingContactName";
            this.txtBillingContactName.Size = new System.Drawing.Size(322, 22);
            this.txtBillingContactName.TabIndex = 0;
            this.txtBillingContactName.WordWrap = false;
            // 
            // pnlCardInfo
            // 
            this.pnlCardInfo.Controls.Add(this.label14);
            this.pnlCardInfo.Controls.Add(this.pnlProvider);
            this.pnlCardInfo.Controls.Add(this.label17);
            this.pnlCardInfo.Controls.Add(this.label2);
            this.pnlCardInfo.Controls.Add(this.label34);
            this.pnlCardInfo.Controls.Add(this.label41);
            this.pnlCardInfo.Controls.Add(this.label10);
            this.pnlCardInfo.Location = new System.Drawing.Point(3, 0);
            this.pnlCardInfo.Name = "pnlCardInfo";
            this.pnlCardInfo.Size = new System.Drawing.Size(615, 110);
            this.pnlCardInfo.TabIndex = 1;
            this.pnlCardInfo.Visible = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(1, 109);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(613, 1);
            this.label14.TabIndex = 302;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.pnlProvider.Controls.Add(this.pnlProviderBody);
            this.pnlProvider.Controls.Add(this.pnlProviderHeader);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.pnlProvider.Location = new System.Drawing.Point(206, 15);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(294, 88);
            this.pnlProvider.TabIndex = 301;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 87);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(292, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 0;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvCreditCard);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(292, 64);
            this.pnlProviderBody.TabIndex = 92;
            // 
            // trvCreditCard
            // 
            this.trvCreditCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCreditCard.CheckBoxes = true;
            this.trvCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCreditCard.ForeColor = System.Drawing.Color.Black;
            this.trvCreditCard.Location = new System.Drawing.Point(0, 0);
            this.trvCreditCard.Name = "trvCreditCard";
            this.trvCreditCard.ShowLines = false;
            this.trvCreditCard.ShowPlusMinus = false;
            this.trvCreditCard.ShowRootLines = false;
            this.trvCreditCard.Size = new System.Drawing.Size(292, 64);
            this.trvCreditCard.TabIndex = 0;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.btnSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.lbl_pnlProviderHeaderBottomBrd);
            this.pnlProviderHeader.Controls.Add(this.lblCreditCard);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(292, 23);
            this.pnlProviderHeader.TabIndex = 91;
            // 
            // btnDeSelectCreditCard
            // 
            this.btnDeSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnDeSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectCreditCard.Image")));
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(230, 0);
            this.btnDeSelectCreditCard.Name = "btnDeSelectCreditCard";
            this.btnDeSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectCreditCard.TabIndex = 1;
            this.btnDeSelectCreditCard.Tag = "Select";
            this.btnDeSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnDeSelectCreditCard.Visible = false;
            this.btnDeSelectCreditCard.Click += new System.EventHandler(this.btnDeSelectCreditCard_Click);
            // 
            // btnSelectCreditCard
            // 
            this.btnSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCreditCard.Image")));
            this.btnSelectCreditCard.Location = new System.Drawing.Point(261, 0);
            this.btnSelectCreditCard.Name = "btnSelectCreditCard";
            this.btnSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnSelectCreditCard.TabIndex = 2;
            this.btnSelectCreditCard.Tag = "Select";
            this.btnSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnSelectCreditCard.Click += new System.EventHandler(this.btnSelectCreditCard_Click);
            // 
            // lbl_pnlProviderHeaderBottomBrd
            // 
            this.lbl_pnlProviderHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlProviderHeaderBottomBrd.Name = "lbl_pnlProviderHeaderBottomBrd";
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(292, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 0;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreditCard.Location = new System.Drawing.Point(0, 0);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(292, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "Credit Card Type";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(293, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 87);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(294, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(613, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "Credit Card Information :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(93, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Credit Card Type :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Location = new System.Drawing.Point(0, 1);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 109);
            this.label34.TabIndex = 303;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Right;
            this.label41.Location = new System.Drawing.Point(614, 1);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 109);
            this.label41.TabIndex = 304;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(615, 1);
            this.label10.TabIndex = 308;
            // 
            // pnlPracAdd
            // 
            this.pnlPracAdd.Controls.Add(this.lblAddressDetails);
            this.pnlPracAdd.Controls.Add(this.cmbPracState);
            this.pnlPracAdd.Controls.Add(this.txtPracZip);
            this.pnlPracAdd.Controls.Add(this.txtPracCity);
            this.pnlPracAdd.Controls.Add(this.txtPracAddress2);
            this.pnlPracAdd.Controls.Add(this.txtPracAddress1);
            this.pnlPracAdd.Controls.Add(this.lblOIZip);
            this.pnlPracAdd.Controls.Add(this.lblOIState);
            this.pnlPracAdd.Controls.Add(this.lblOICity);
            this.pnlPracAdd.Controls.Add(this.lblOIAddressLine2);
            this.pnlPracAdd.Controls.Add(this.lblOIAddressLine1);
            this.pnlPracAdd.Controls.Add(this.label33);
            this.pnlPracAdd.Controls.Add(this.label40);
            this.pnlPracAdd.Controls.Add(this.label52);
            this.pnlPracAdd.Controls.Add(this.label64);
            this.pnlPracAdd.Location = new System.Drawing.Point(3, 0);
            this.pnlPracAdd.Name = "pnlPracAdd";
            this.pnlPracAdd.Size = new System.Drawing.Size(591, 125);
            this.pnlPracAdd.TabIndex = 324;
            this.pnlPracAdd.Visible = false;
            // 
            // lblAddressDetails
            // 
            this.lblAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressDetails.Location = new System.Drawing.Point(1, 1);
            this.lblAddressDetails.Name = "lblAddressDetails";
            this.lblAddressDetails.Size = new System.Drawing.Size(589, 17);
            this.lblAddressDetails.TabIndex = 1;
            this.lblAddressDetails.Text = "Practice Address :";
            // 
            // cmbPracState
            // 
            this.cmbPracState.FormattingEnabled = true;
            this.cmbPracState.Location = new System.Drawing.Point(408, 72);
            this.cmbPracState.MaxLength = 20;
            this.cmbPracState.Name = "cmbPracState";
            this.cmbPracState.Size = new System.Drawing.Size(94, 22);
            this.cmbPracState.TabIndex = 3;
            // 
            // txtPracZip
            // 
            this.txtPracZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracZip.ForeColor = System.Drawing.Color.Black;
            this.txtPracZip.Location = new System.Drawing.Point(206, 96);
            this.txtPracZip.MaxLength = 10;
            this.txtPracZip.Name = "txtPracZip";
            this.txtPracZip.Size = new System.Drawing.Size(74, 22);
            this.txtPracZip.TabIndex = 4;
            this.txtPracZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPracZip_KeyPress);
            this.txtPracZip.Leave += new System.EventHandler(this.txtPracZip_Leave);
            // 
            // txtPracCity
            // 
            this.txtPracCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracCity.ForeColor = System.Drawing.Color.Black;
            this.txtPracCity.Location = new System.Drawing.Point(206, 70);
            this.txtPracCity.MaxLength = 50;
            this.txtPracCity.Name = "txtPracCity";
            this.txtPracCity.Size = new System.Drawing.Size(151, 22);
            this.txtPracCity.TabIndex = 2;
            // 
            // txtPracAddress2
            // 
            this.txtPracAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtPracAddress2.Location = new System.Drawing.Point(206, 44);
            this.txtPracAddress2.MaxLength = 50;
            this.txtPracAddress2.Name = "txtPracAddress2";
            this.txtPracAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtPracAddress2.TabIndex = 1;
            // 
            // txtPracAddress1
            // 
            this.txtPracAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPracAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtPracAddress1.Location = new System.Drawing.Point(206, 18);
            this.txtPracAddress1.MaxLength = 50;
            this.txtPracAddress1.Name = "txtPracAddress1";
            this.txtPracAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtPracAddress1.TabIndex = 0;
            // 
            // lblOIZip
            // 
            this.lblOIZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIZip.AutoEllipsis = true;
            this.lblOIZip.AutoSize = true;
            this.lblOIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIZip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIZip.Location = new System.Drawing.Point(172, 100);
            this.lblOIZip.Name = "lblOIZip";
            this.lblOIZip.Size = new System.Drawing.Size(31, 14);
            this.lblOIZip.TabIndex = 11;
            this.lblOIZip.Text = "Zip :";
            // 
            // lblOIState
            // 
            this.lblOIState.AutoSize = true;
            this.lblOIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIState.Location = new System.Drawing.Point(360, 76);
            this.lblOIState.Name = "lblOIState";
            this.lblOIState.Size = new System.Drawing.Size(45, 14);
            this.lblOIState.TabIndex = 9;
            this.lblOIState.Text = "State :";
            // 
            // lblOICity
            // 
            this.lblOICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOICity.AutoEllipsis = true;
            this.lblOICity.AutoSize = true;
            this.lblOICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOICity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOICity.Location = new System.Drawing.Point(168, 74);
            this.lblOICity.Name = "lblOICity";
            this.lblOICity.Size = new System.Drawing.Size(35, 14);
            this.lblOICity.TabIndex = 7;
            this.lblOICity.Text = "City :";
            // 
            // lblOIAddressLine2
            // 
            this.lblOIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine2.AutoEllipsis = true;
            this.lblOIAddressLine2.AutoSize = true;
            this.lblOIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine2.Location = new System.Drawing.Point(108, 48);
            this.lblOIAddressLine2.Name = "lblOIAddressLine2";
            this.lblOIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine2.TabIndex = 5;
            this.lblOIAddressLine2.Text = "Address Line 2 :";
            // 
            // lblOIAddressLine1
            // 
            this.lblOIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine1.AutoEllipsis = true;
            this.lblOIAddressLine1.AutoSize = true;
            this.lblOIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine1.Location = new System.Drawing.Point(108, 22);
            this.lblOIAddressLine1.Name = "lblOIAddressLine1";
            this.lblOIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine1.TabIndex = 3;
            this.lblOIAddressLine1.Text = "Address Line 1 :";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Location = new System.Drawing.Point(0, 1);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 123);
            this.label33.TabIndex = 2;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Right;
            this.label40.Location = new System.Drawing.Point(590, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 123);
            this.label40.TabIndex = 288;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(591, 1);
            this.label52.TabIndex = 307;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label64.Location = new System.Drawing.Point(0, 124);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(591, 1);
            this.label64.TabIndex = 308;
            // 
            // imgtabControl1
            // 
            this.imgtabControl1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgtabControl1.ImageStream")));
            this.imgtabControl1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgtabControl1.Images.SetKeyName(0, "Filter Criteria.ico");
            this.imgtabControl1.Images.SetKeyName(1, "Display.ico");
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupPatientStatementDisplaySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(680, 790);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel20);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSetupPatientStatementDisplaySettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement Display Setting";
            this.Load += new System.EventHandler(this.frmSetupPatientStatementDisplaySettings_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.Panel20.ResumeLayout(false);
            this.Panel21.ResumeLayout(false);
            this.Panel21.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlOther.ResumeLayout(false);
            this.pnlOther.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatementMessage)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlOtherAddress.ResumeLayout(false);
            this.pnlOtherAddress.PerformLayout();
            this.pnlRbuttons.ResumeLayout(false);
            this.pnlRbuttons.PerformLayout();
            this.pnlRemitAddress.ResumeLayout(false);
            this.pnlRemitAddress.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlTaxInfo.ResumeLayout(false);
            this.pnlTaxInfo.PerformLayout();
            this.pnlOfficeHrs.ResumeLayout(false);
            this.pnlOfficeHrs.PerformLayout();
            this.pnlBillingContactInfo.ResumeLayout(false);
            this.pnlBillingContactInfo.PerformLayout();
            this.pnlCardInfo.ResumeLayout(false);
            this.pnlCardInfo.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.pnlPracAdd.ResumeLayout(false);
            this.pnlPracAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton btnClose;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
        internal System.Windows.Forms.Panel Panel20;
        internal System.Windows.Forms.Panel Panel21;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label44;
        private System.Windows.Forms.Label Label45;
        private System.Windows.Forms.Label Label46;
        private System.Windows.Forms.Label Label47;
        private System.Windows.Forms.TextBox txtStatementDisplaySettingsName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imgtabControl1;
        private System.Windows.Forms.Panel pnlPracAdd;
        private System.Windows.Forms.ComboBox cmbPracState;
        private System.Windows.Forms.TextBox txtPracZip;
        private System.Windows.Forms.TextBox txtPracCity;
        private System.Windows.Forms.TextBox txtPracAddress2;
        private System.Windows.Forms.TextBox txtPracAddress1;
        private System.Windows.Forms.Label lblOIZip;
        private System.Windows.Forms.Label lblOIState;
        private System.Windows.Forms.Label lblOICity;
        private System.Windows.Forms.Label lblOIAddressLine2;
        private System.Windows.Forms.Label lblOIAddressLine1;
        private System.Windows.Forms.Label lblAddressDetails;
        private System.Windows.Forms.Panel pnlOfficeHrs;
        internal System.Windows.Forms.DateTimePicker dtpOfficeEndTime;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.DateTimePicker dtpOfficeStartTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlBillingContactInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBillingContactName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel pnlCardInfo;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlOther;
        private System.Windows.Forms.TextBox txtClinicMessage2;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtClinicMessage1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlRemitAddress;
        private System.Windows.Forms.ComboBox cmbRemitState;
        private System.Windows.Forms.TextBox txtRemitZip;
        private System.Windows.Forms.TextBox txtRemitCity;
        private System.Windows.Forms.TextBox txtRemitAddress2;
        private System.Windows.Forms.TextBox txtRemitAddress1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel pnlTaxInfo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPracticeTaxID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkGuarantorIndicator;
        private System.Windows.Forms.CheckBox chkPendingInsurance;
        private gloMaskControl.gloMaskBox txtBillingContactPhone;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label lbl_pnlProviderBottomBrd;
        private System.Windows.Forms.Panel pnlProviderBody;
        private System.Windows.Forms.TreeView trvCreditCard;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Button btnDeSelectCreditCard;
        private System.Windows.Forms.Button btnSelectCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderHeaderBottomBrd;
        private System.Windows.Forms.Label lblCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlProviderRightBrd;
        private System.Windows.Forms.Label lbl_pnlProviderTopBrd;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.CheckBox chkDefault1;
        private System.Windows.Forms.TextBox txtRemitName;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        internal System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlOtherAddress;
        internal System.Windows.Forms.Panel pnlInternalZipControl;
        private System.Windows.Forms.TextBox txtOtherName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOtherState;
        private System.Windows.Forms.TextBox txtOtherZip;
        private System.Windows.Forms.TextBox txtOtherCity;
        private System.Windows.Forms.TextBox txtOtherAddress2;
        private System.Windows.Forms.TextBox txtOtherAddress1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlRbuttons;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.RadioButton rbOtherAddress;
        private System.Windows.Forms.RadioButton rbBillingProvider;
        private System.Windows.Forms.RadioButton rbRemitAddress;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.CheckBox chkPaymentRemit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtBillingURL;
        private System.Windows.Forms.TextBox txtBillingEmail;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label67;
        private C1.Win.C1FlexGrid.C1FlexGrid c1StatementMessage;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbRemitOtherAddress;
        private System.Windows.Forms.RadioButton rbRemitBillingProvider;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox chkIncludeClaim;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblDetachInstructions;
        private System.Windows.Forms.TextBox txtDetachInstructions;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.CheckBox chkIncludeonEachStatement;
    }
}