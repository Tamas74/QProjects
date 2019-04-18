namespace gloPatient
{
    partial class gloPatientInsuranceControl
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
                try
                {
                    if (oTimer != null)
                    {
                        oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                        oTimer.Dispose();
                        oTimer = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (_oInsurancesDetails != null)
                    {
                        _oInsurancesDetails.Dispose();
                        _oInsurancesDetails = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (_oPatienInsuranceOther != null)
                    {
                        _oPatienInsuranceOther.Dispose();
                        _oPatienInsuranceOther = null;
                    }
                }
                catch
                {
                }
                if (_deletedInsurances != null)
                {
                    _deletedInsurances.Clear();
                    _deletedInsurances = null;
                }
                if (dtScannedImages != null)
                {
                    dtScannedImages.Clear();
                    dtScannedImages.Dispose();
                    dtScannedImages = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientInsuranceControl));
            this.pnlInsuranceInfo = new System.Windows.Forms.Panel();
            this.pnlPrimaryInsuDetails = new System.Windows.Forms.Panel();
            this.pnl_PrimaryInfo = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtEligibilityInsurance = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDeductableAmount = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.mtxtStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.mtxtEndDate = new System.Windows.Forms.MaskedTextBox();
            this.txtCoveragePercent = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtCopay = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.txtEmployer = new System.Windows.Forms.TextBox();
            this.lblEmployer = new System.Windows.Forms.Label();
            this.chkautoclaim = new System.Windows.Forms.CheckBox();
            this.chkAssignmentofBenifit = new System.Windows.Forms.CheckBox();
            this.chkworkerscomp = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblGroupMandatory = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.chkCompany = new System.Windows.Forms.CheckBox();
            this.chkAddrSameAsPatient = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.txtSubFName = new System.Windows.Forms.TextBox();
            this.lblInsuPhone = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.cmbRelationShip = new System.Windows.Forms.ComboBox();
            this.mtxtDOB = new System.Windows.Forms.MaskedTextBox();
            this.chkSameAsPatient = new System.Windows.Forms.CheckBox();
            this.txtSubMName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSubscriberName = new System.Windows.Forms.Label();
            this.mskInsurancePhone = new gloMaskControl.gloMaskBox();
            this.txtSubscriberID = new System.Windows.Forms.TextBox();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.txtSubLName = new System.Windows.Forms.TextBox();
            this.lblSubscriberID = new System.Windows.Forms.Label();
            this.pnlAddresControl = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gbPAGender = new System.Windows.Forms.GroupBox();
            this.rbGender3 = new System.Windows.Forms.RadioButton();
            this.rbGender2 = new System.Windows.Forms.RadioButton();
            this.rbGender1 = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.cmbMedicareTypeCode = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.cmbDefaultTypeCode = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txtInsurance = new System.Windows.Forms.TextBox();
            this.btnModifyInsurance = new System.Windows.Forms.Button();
            this.btn_AddInsurance = new System.Windows.Forms.Button();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.btnInsDelete = new System.Windows.Forms.Button();
            this.btnInsBrowse = new System.Windows.Forms.Button();
            this.radSetAsTertiary = new System.Windows.Forms.RadioButton();
            this.radSetAsPrimary = new System.Windows.Forms.RadioButton();
            this.lblInsuName = new System.Windows.Forms.Label();
            this.radSetAsSecondary = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPrimaryInsuDetails = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtSubscriberPolicy = new System.Windows.Forms.TextBox();
            this.lblSubscribePolicy = new System.Windows.Forms.Label();
            this.btnVwInsuDocs = new System.Windows.Forms.Button();
            this.btnScanInsuDocs = new System.Windows.Forms.Button();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtCounty = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.pnlTreeView = new System.Windows.Forms.Panel();
            this.tvInsurances = new System.Windows.Forms.TreeView();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbEligibilityCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsb_ScanPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInsuInfoHeader = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblInsuInfoHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTipInsurance = new System.Windows.Forms.ToolTip(this.components);
            this.pnlInsuranceInfo.SuspendLayout();
            this.pnlPrimaryInsuDetails.SuspendLayout();
            this.pnl_PrimaryInfo.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbPAGender.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlInsuInfoHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInsuranceInfo
            // 
            this.pnlInsuranceInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlInsuranceInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInsuranceInfo.Controls.Add(this.pnlPrimaryInsuDetails);
            this.pnlInsuranceInfo.Controls.Add(this.splitter3);
            this.pnlInsuranceInfo.Controls.Add(this.pnlTreeView);
            this.pnlInsuranceInfo.Controls.Add(this.pnlTOP);
            this.pnlInsuranceInfo.Controls.Add(this.panel1);
            this.pnlInsuranceInfo.Controls.Add(this.panel2);
            this.pnlInsuranceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsuranceInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInsuranceInfo.Name = "pnlInsuranceInfo";
            this.pnlInsuranceInfo.Size = new System.Drawing.Size(774, 786);
            this.pnlInsuranceInfo.TabIndex = 0;
            // 
            // pnlPrimaryInsuDetails
            // 
            this.pnlPrimaryInsuDetails.AutoScroll = true;
            this.pnlPrimaryInsuDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlPrimaryInsuDetails.Controls.Add(this.pnl_PrimaryInfo);
            this.pnlPrimaryInsuDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrimaryInsuDetails.Location = new System.Drawing.Point(207, 85);
            this.pnlPrimaryInsuDetails.Name = "pnlPrimaryInsuDetails";
            this.pnlPrimaryInsuDetails.Size = new System.Drawing.Size(567, 676);
            this.pnlPrimaryInsuDetails.TabIndex = 1;
            // 
            // pnl_PrimaryInfo
            // 
            this.pnl_PrimaryInfo.AccessibleDescription = "  ";
            this.pnl_PrimaryInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnl_PrimaryInfo.Controls.Add(this.panel5);
            this.pnl_PrimaryInfo.Controls.Add(this.panel9);
            this.pnl_PrimaryInfo.Controls.Add(this.panel4);
            this.pnl_PrimaryInfo.Controls.Add(this.panel8);
            this.pnl_PrimaryInfo.Controls.Add(this.panel3);
            this.pnl_PrimaryInfo.Controls.Add(this.panel6);
            this.pnl_PrimaryInfo.Controls.Add(this.groupBox2);
            this.pnl_PrimaryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PrimaryInfo.Location = new System.Drawing.Point(0, 0);
            this.pnl_PrimaryInfo.Name = "pnl_PrimaryInfo";
            this.pnl_PrimaryInfo.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnl_PrimaryInfo.Size = new System.Drawing.Size(567, 676);
            this.pnl_PrimaryInfo.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.txtEligibilityInsurance);
            this.panel5.Controls.Add(this.label63);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.txtDeductableAmount);
            this.panel5.Controls.Add(this.label32);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.mtxtStartDate);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.mtxtEndDate);
            this.panel5.Controls.Add(this.txtCoveragePercent);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.txtCopay);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 530);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(564, 143);
            this.panel5.TabIndex = 5;
            // 
            // txtEligibilityInsurance
            // 
            this.txtEligibilityInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEligibilityInsurance.ForeColor = System.Drawing.Color.Black;
            this.txtEligibilityInsurance.Location = new System.Drawing.Point(145, 60);
            this.txtEligibilityInsurance.MaxLength = 255;
            this.txtEligibilityInsurance.Multiline = true;
            this.txtEligibilityInsurance.Name = "txtEligibilityInsurance";
            this.txtEligibilityInsurance.Size = new System.Drawing.Size(351, 78);
            this.txtEligibilityInsurance.TabIndex = 9;
            // 
            // label63
            // 
            this.label63.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label63.AutoEllipsis = true;
            this.label63.AutoSize = true;
            this.label63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(99, 60);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(42, 14);
            this.label63.TabIndex = 58;
            this.label63.Text = "Note :";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(563, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 138);
            this.label19.TabIndex = 57;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 138);
            this.label18.TabIndex = 56;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 142);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(564, 1);
            this.label17.TabIndex = 55;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(564, 1);
            this.label16.TabIndex = 54;
            // 
            // txtDeductableAmount
            // 
            this.txtDeductableAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductableAmount.ForeColor = System.Drawing.Color.Black;
            this.txtDeductableAmount.Location = new System.Drawing.Point(144, 8);
            this.txtDeductableAmount.MaxLength = 8;
            this.txtDeductableAmount.Name = "txtDeductableAmount";
            this.txtDeductableAmount.ShortcutsEnabled = false;
            this.txtDeductableAmount.Size = new System.Drawing.Size(65, 22);
            this.txtDeductableAmount.TabIndex = 4;
            this.txtDeductableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.label32.Location = new System.Drawing.Point(330, 38);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(66, 14);
            this.label32.TabIndex = 12;
            this.label32.Text = "End Date :";
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
            this.label28.Location = new System.Drawing.Point(68, 12);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(73, 14);
            this.label28.TabIndex = 12;
            this.label28.Text = "Deductible :";
            // 
            // mtxtStartDate
            // 
            this.mtxtStartDate.Location = new System.Drawing.Point(144, 33);
            this.mtxtStartDate.Mask = "00/00/0000";
            this.mtxtStartDate.Name = "mtxtStartDate";
            this.mtxtStartDate.Size = new System.Drawing.Size(100, 22);
            this.mtxtStartDate.TabIndex = 7;
            this.mtxtStartDate.ValidatingType = typeof(System.DateTime);
            this.mtxtStartDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mtxtStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtStartDate_Validating);
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
            this.label31.Location = new System.Drawing.Point(69, 37);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 14);
            this.label31.TabIndex = 12;
            this.label31.Text = "Start Date :";
            // 
            // mtxtEndDate
            // 
            this.mtxtEndDate.Location = new System.Drawing.Point(398, 33);
            this.mtxtEndDate.Mask = "00/00/0000";
            this.mtxtEndDate.Name = "mtxtEndDate";
            this.mtxtEndDate.Size = new System.Drawing.Size(98, 22);
            this.mtxtEndDate.TabIndex = 8;
            this.mtxtEndDate.ValidatingType = typeof(System.DateTime);
            this.mtxtEndDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mtxtEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtEndDate_Validating);
            // 
            // txtCoveragePercent
            // 
            this.txtCoveragePercent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoveragePercent.ForeColor = System.Drawing.Color.Black;
            this.txtCoveragePercent.Location = new System.Drawing.Point(322, 8);
            this.txtCoveragePercent.MaxLength = 6;
            this.txtCoveragePercent.Name = "txtCoveragePercent";
            this.txtCoveragePercent.ShortcutsEnabled = false;
            this.txtCoveragePercent.Size = new System.Drawing.Size(54, 22);
            this.txtCoveragePercent.TabIndex = 5;
            this.txtCoveragePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCoveragePercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCoveragePercent_KeyPress);
            this.txtCoveragePercent.Validating += new System.ComponentModel.CancelEventHandler(this.txtCoveragePercent_Validating);
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
            this.label30.Location = new System.Drawing.Point(217, 12);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(102, 14);
            this.label30.TabIndex = 18;
            this.label30.Text = "Co-Insurance % :";
            // 
            // txtCopay
            // 
            this.txtCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopay.ForeColor = System.Drawing.Color.Black;
            this.txtCopay.Location = new System.Drawing.Point(442, 8);
            this.txtCopay.MaxLength = 6;
            this.txtCopay.Name = "txtCopay";
            this.txtCopay.ShortcutsEnabled = false;
            this.txtCopay.Size = new System.Drawing.Size(54, 22);
            this.txtCopay.TabIndex = 6;
            this.txtCopay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.label29.Location = new System.Drawing.Point(392, 12);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(48, 14);
            this.label29.TabIndex = 18;
            this.label29.Text = "Copay :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label59);
            this.panel9.Controls.Add(this.label60);
            this.panel9.Controls.Add(this.label61);
            this.panel9.Controls.Add(this.label62);
            this.panel9.Controls.Add(this.txtEmployer);
            this.panel9.Controls.Add(this.lblEmployer);
            this.panel9.Controls.Add(this.chkautoclaim);
            this.panel9.Controls.Add(this.chkAssignmentofBenifit);
            this.panel9.Controls.Add(this.chkworkerscomp);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 478);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(564, 52);
            this.panel9.TabIndex = 4;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Right;
            this.label59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(563, 1);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 50);
            this.label59.TabIndex = 57;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(0, 1);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 50);
            this.label60.TabIndex = 56;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(0, 51);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(564, 1);
            this.label61.TabIndex = 55;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(564, 1);
            this.label62.TabIndex = 54;
            // 
            // txtEmployer
            // 
            this.txtEmployer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployer.ForeColor = System.Drawing.Color.Black;
            this.txtEmployer.Location = new System.Drawing.Point(144, 5);
            this.txtEmployer.MaxLength = 255;
            this.txtEmployer.Name = "txtEmployer";
            this.txtEmployer.Size = new System.Drawing.Size(356, 22);
            this.txtEmployer.TabIndex = 0;
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
            this.lblEmployer.Location = new System.Drawing.Point(76, 8);
            this.lblEmployer.Name = "lblEmployer";
            this.lblEmployer.Size = new System.Drawing.Size(65, 14);
            this.lblEmployer.TabIndex = 18;
            this.lblEmployer.Text = "Employer :";
            // 
            // chkautoclaim
            // 
            this.chkautoclaim.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkautoclaim.AutoSize = true;
            this.chkautoclaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkautoclaim.Location = new System.Drawing.Point(268, 31);
            this.chkautoclaim.Name = "chkautoclaim";
            this.chkautoclaim.Size = new System.Drawing.Size(83, 18);
            this.chkautoclaim.TabIndex = 3;
            this.chkautoclaim.Text = "Auto claim";
            this.chkautoclaim.UseVisualStyleBackColor = true;
            // 
            // chkAssignmentofBenifit
            // 
            this.chkAssignmentofBenifit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAssignmentofBenifit.AutoSize = true;
            this.chkAssignmentofBenifit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAssignmentofBenifit.Location = new System.Drawing.Point(369, 31);
            this.chkAssignmentofBenifit.Name = "chkAssignmentofBenifit";
            this.chkAssignmentofBenifit.Size = new System.Drawing.Size(147, 18);
            this.chkAssignmentofBenifit.TabIndex = 4;
            this.chkAssignmentofBenifit.Text = "Assignment of benefit";
            this.chkAssignmentofBenifit.UseVisualStyleBackColor = true;
            // 
            // chkworkerscomp
            // 
            this.chkworkerscomp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkworkerscomp.AutoSize = true;
            this.chkworkerscomp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkworkerscomp.Location = new System.Drawing.Point(145, 31);
            this.chkworkerscomp.Name = "chkworkerscomp";
            this.chkworkerscomp.Size = new System.Drawing.Size(105, 18);
            this.chkworkerscomp.TabIndex = 2;
            this.chkworkerscomp.Text = "Workers comp";
            this.chkworkerscomp.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblGroupMandatory);
            this.panel4.Controls.Add(this.txtCompanyName);
            this.panel4.Controls.Add(this.chkCompany);
            this.panel4.Controls.Add(this.chkAddrSameAsPatient);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label37);
            this.panel4.Controls.Add(this.txtSubFName);
            this.panel4.Controls.Add(this.lblInsuPhone);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.lblDOB);
            this.panel4.Controls.Add(this.cmbRelationShip);
            this.panel4.Controls.Add(this.mtxtDOB);
            this.panel4.Controls.Add(this.chkSameAsPatient);
            this.panel4.Controls.Add(this.txtSubMName);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.lblGroup);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.lblSubscriberName);
            this.panel4.Controls.Add(this.mskInsurancePhone);
            this.panel4.Controls.Add(this.txtSubscriberID);
            this.panel4.Controls.Add(this.txtGroup);
            this.panel4.Controls.Add(this.txtSubLName);
            this.panel4.Controls.Add(this.lblSubscriberID);
            this.panel4.Controls.Add(this.pnlAddresControl);
            this.panel4.Controls.Add(this.label47);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.gbPAGender);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 145);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel4.Size = new System.Drawing.Size(564, 333);
            this.panel4.TabIndex = 3;
            // 
            // lblGroupMandatory
            // 
            this.lblGroupMandatory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGroupMandatory.AutoEllipsis = true;
            this.lblGroupMandatory.AutoSize = true;
            this.lblGroupMandatory.BackColor = System.Drawing.Color.Transparent;
            this.lblGroupMandatory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupMandatory.ForeColor = System.Drawing.Color.Red;
            this.lblGroupMandatory.Location = new System.Drawing.Point(82, 68);
            this.lblGroupMandatory.Name = "lblGroupMandatory";
            this.lblGroupMandatory.Size = new System.Drawing.Size(14, 14);
            this.lblGroupMandatory.TabIndex = 62;
            this.lblGroupMandatory.Text = "*";
            this.lblGroupMandatory.Visible = false;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCompanyName.Location = new System.Drawing.Point(144, 92);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(236, 22);
            this.txtCompanyName.TabIndex = 3;
            this.txtCompanyName.Visible = false;
            // 
            // chkCompany
            // 
            this.chkCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCompany.AutoSize = true;
            this.chkCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompany.Location = new System.Drawing.Point(387, 94);
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Size = new System.Drawing.Size(76, 18);
            this.chkCompany.TabIndex = 6;
            this.chkCompany.Text = "Company";
            this.chkCompany.UseVisualStyleBackColor = true;
            this.chkCompany.CheckedChanged += new System.EventHandler(this.chkCompany_CheckedChanged);
            // 
            // chkAddrSameAsPatient
            // 
            this.chkAddrSameAsPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAddrSameAsPatient.AutoSize = true;
            this.chkAddrSameAsPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddrSameAsPatient.Location = new System.Drawing.Point(145, 117);
            this.chkAddrSameAsPatient.Name = "chkAddrSameAsPatient";
            this.chkAddrSameAsPatient.Size = new System.Drawing.Size(159, 18);
            this.chkAddrSameAsPatient.TabIndex = 7;
            this.chkAddrSameAsPatient.Text = "Address same as patient";
            this.chkAddrSameAsPatient.UseVisualStyleBackColor = true;
            this.chkAddrSameAsPatient.CheckedChanged += new System.EventHandler(this.chkAddrSameAsPatient_CheckedChanged);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(563, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 328);
            this.label22.TabIndex = 61;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 328);
            this.label23.TabIndex = 60;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(0, 329);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(564, 1);
            this.label24.TabIndex = 59;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(564, 1);
            this.label37.TabIndex = 58;
            // 
            // txtSubFName
            // 
            this.txtSubFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubFName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSubFName.Location = new System.Drawing.Point(144, 92);
            this.txtSubFName.Name = "txtSubFName";
            this.txtSubFName.Size = new System.Drawing.Size(88, 22);
            this.txtSubFName.TabIndex = 3;
            this.txtSubFName.TextChanged += new System.EventHandler(this.txtSubLName_Validated);
            // 
            // lblInsuPhone
            // 
            this.lblInsuPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuPhone.AutoEllipsis = true;
            this.lblInsuPhone.AutoSize = true;
            this.lblInsuPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuPhone.Location = new System.Drawing.Point(271, 306);
            this.lblInsuPhone.Name = "lblInsuPhone";
            this.lblInsuPhone.Size = new System.Drawing.Size(110, 14);
            this.lblInsuPhone.TabIndex = 10;
            this.lblInsuPhone.Text = "Subscriber phone :";
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
            this.label34.Location = new System.Drawing.Point(86, 276);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(55, 14);
            this.label34.TabIndex = 14;
            this.label34.Text = "Gender :";
            this.label34.Click += new System.EventHandler(this.label34_Click);
            // 
            // lblDOB
            // 
            this.lblDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDOB.AutoEllipsis = true;
            this.lblDOB.AutoSize = true;
            this.lblDOB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOB.Location = new System.Drawing.Point(42, 306);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(99, 14);
            this.lblDOB.TabIndex = 12;
            this.lblDOB.Text = "Subscriber DOB :";
            // 
            // cmbRelationShip
            // 
            this.cmbRelationShip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationShip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRelationShip.FormattingEnabled = true;
            this.cmbRelationShip.Location = new System.Drawing.Point(144, 16);
            this.cmbRelationShip.Name = "cmbRelationShip";
            this.cmbRelationShip.Size = new System.Drawing.Size(338, 22);
            this.cmbRelationShip.TabIndex = 0;
            this.cmbRelationShip.SelectedIndexChanged += new System.EventHandler(this.cmbRelationShip_SelectedIndexChanged);
            // 
            // mtxtDOB
            // 
            this.mtxtDOB.Location = new System.Drawing.Point(144, 302);
            this.mtxtDOB.Mask = "00/00/0000";
            this.mtxtDOB.Name = "mtxtDOB";
            this.mtxtDOB.Size = new System.Drawing.Size(100, 22);
            this.mtxtDOB.TabIndex = 12;
            this.mtxtDOB.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtxtDOB.ValidatingType = typeof(System.DateTime);
            this.mtxtDOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mtxtDOB.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtDOB_Validating);
            // 
            // chkSameAsPatient
            // 
            this.chkSameAsPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSameAsPatient.AutoSize = true;
            this.chkSameAsPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSameAsPatient.Location = new System.Drawing.Point(387, 94);
            this.chkSameAsPatient.Name = "chkSameAsPatient";
            this.chkSameAsPatient.Size = new System.Drawing.Size(114, 18);
            this.chkSameAsPatient.TabIndex = 12;
            this.chkSameAsPatient.Text = "Same as patient";
            this.chkSameAsPatient.UseVisualStyleBackColor = true;
            this.chkSameAsPatient.Visible = false;
            this.chkSameAsPatient.CheckedChanged += new System.EventHandler(this.chkSameAsPatient_CheckedChanged);
            // 
            // txtSubMName
            // 
            this.txtSubMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubMName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSubMName.Location = new System.Drawing.Point(242, 92);
            this.txtSubMName.MaxLength = 2;
            this.txtSubMName.Name = "txtSubMName";
            this.txtSubMName.Size = new System.Drawing.Size(23, 22);
            this.txtSubMName.TabIndex = 4;
            this.txtSubMName.TextChanged += new System.EventHandler(this.txtSubLName_Validated);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(77, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 14);
            this.label11.TabIndex = 47;
            this.label11.Text = "*";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGroup.AutoEllipsis = true;
            this.lblGroup.AutoSize = true;
            this.lblGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.Location = new System.Drawing.Point(93, 71);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(48, 14);
            this.lblGroup.TabIndex = 16;
            this.lblGroup.Text = "Group :";
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
            this.label33.Location = new System.Drawing.Point(22, 5);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(119, 28);
            this.label33.TabIndex = 14;
            this.label33.Text = "Patient Relationship\r\n        to Subscriber :";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(31, 302);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 14);
            this.label14.TabIndex = 47;
            this.label14.Text = "*";
            // 
            // lblSubscriberName
            // 
            this.lblSubscriberName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberName.AutoEllipsis = true;
            this.lblSubscriberName.AutoSize = true;
            this.lblSubscriberName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscriberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberName.Location = new System.Drawing.Point(35, 96);
            this.lblSubscriberName.Name = "lblSubscriberName";
            this.lblSubscriberName.Size = new System.Drawing.Size(106, 14);
            this.lblSubscriberName.TabIndex = 5;
            this.lblSubscriberName.Text = "Subscriber Name :";
            // 
            // mskInsurancePhone
            // 
            this.mskInsurancePhone.AllowValidate = true;
            this.mskInsurancePhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskInsurancePhone.IncludeLiteralsAndPrompts = false;
            this.mskInsurancePhone.Location = new System.Drawing.Point(384, 302);
            this.mskInsurancePhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskInsurancePhone.Name = "mskInsurancePhone";
            this.mskInsurancePhone.ReadOnly = false;
            this.mskInsurancePhone.Size = new System.Drawing.Size(98, 22);
            this.mskInsurancePhone.TabIndex = 13;
            // 
            // txtSubscriberID
            // 
            this.txtSubscriberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberID.ForeColor = System.Drawing.Color.Black;
            this.txtSubscriberID.Location = new System.Drawing.Point(144, 41);
            this.txtSubscriberID.MaxLength = 25;
            this.txtSubscriberID.Name = "txtSubscriberID";
            this.txtSubscriberID.Size = new System.Drawing.Size(338, 22);
            this.txtSubscriberID.TabIndex = 1;
            this.txtSubscriberID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubscriberID_KeyPress);
            this.txtSubscriberID.Leave += new System.EventHandler(this.txtSubscriberID_Leave);
            this.txtSubscriberID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSubscriberID_MouseDown);
            // 
            // txtGroup
            // 
            this.txtGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroup.ForeColor = System.Drawing.Color.Black;
            this.txtGroup.Location = new System.Drawing.Point(144, 67);
            this.txtGroup.MaxLength = 50;
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(88, 22);
            this.txtGroup.TabIndex = 2;
            // 
            // txtSubLName
            // 
            this.txtSubLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubLName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSubLName.Location = new System.Drawing.Point(274, 92);
            this.txtSubLName.Name = "txtSubLName";
            this.txtSubLName.Size = new System.Drawing.Size(107, 22);
            this.txtSubLName.TabIndex = 5;
            this.txtSubLName.TextChanged += new System.EventHandler(this.txtSubLName_Validated);
            // 
            // lblSubscriberID
            // 
            this.lblSubscriberID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberID.AutoEllipsis = true;
            this.lblSubscriberID.AutoSize = true;
            this.lblSubscriberID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscriberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberID.Location = new System.Drawing.Point(57, 45);
            this.lblSubscriberID.Name = "lblSubscriberID";
            this.lblSubscriberID.Size = new System.Drawing.Size(84, 14);
            this.lblSubscriberID.TabIndex = 14;
            this.lblSubscriberID.Text = "Insurance ID :";
            // 
            // pnlAddresControl
            // 
            this.pnlAddresControl.Location = new System.Drawing.Point(64, 137);
            this.pnlAddresControl.Name = "pnlAddresControl";
            this.pnlAddresControl.Size = new System.Drawing.Size(331, 133);
            this.pnlAddresControl.TabIndex = 8;
            // 
            // label47
            // 
            this.label47.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label47.AutoEllipsis = true;
            this.label47.AutoSize = true;
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Red;
            this.label47.Location = new System.Drawing.Point(47, 42);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(14, 14);
            this.label47.TabIndex = 47;
            this.label47.Text = "*";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(25, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 14);
            this.label10.TabIndex = 47;
            this.label10.Text = "*";
            // 
            // gbPAGender
            // 
            this.gbPAGender.Controls.Add(this.rbGender3);
            this.gbPAGender.Controls.Add(this.rbGender2);
            this.gbPAGender.Controls.Add(this.rbGender1);
            this.gbPAGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbPAGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPAGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbPAGender.Location = new System.Drawing.Point(146, 263);
            this.gbPAGender.Name = "gbPAGender";
            this.gbPAGender.Size = new System.Drawing.Size(352, 33);
            this.gbPAGender.TabIndex = 9;
            this.gbPAGender.TabStop = false;
            this.gbPAGender.Enter += new System.EventHandler(this.gbPAGender_Enter);
            // 
            // rbGender3
            // 
            this.rbGender3.AutoSize = true;
            this.rbGender3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender3.Location = new System.Drawing.Point(279, 11);
            this.rbGender3.Name = "rbGender3";
            this.rbGender3.Size = new System.Drawing.Size(57, 18);
            this.rbGender3.TabIndex = 11;
            this.rbGender3.TabStop = true;
            this.rbGender3.Text = "Other";
            this.rbGender3.UseVisualStyleBackColor = true;
            this.rbGender3.CheckedChanged += new System.EventHandler(this.rbGender3_CheckedChanged);
            this.rbGender3.Click += new System.EventHandler(this.rbGenderAll_Click);
            // 
            // rbGender2
            // 
            this.rbGender2.AutoSize = true;
            this.rbGender2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender2.Location = new System.Drawing.Point(141, 11);
            this.rbGender2.Name = "rbGender2";
            this.rbGender2.Size = new System.Drawing.Size(63, 18);
            this.rbGender2.TabIndex = 10;
            this.rbGender2.TabStop = true;
            this.rbGender2.Text = "Female";
            this.rbGender2.UseVisualStyleBackColor = true;
            this.rbGender2.CheckedChanged += new System.EventHandler(this.rbGender2_CheckedChanged);
            this.rbGender2.Click += new System.EventHandler(this.rbGenderAll_Click);
            // 
            // rbGender1
            // 
            this.rbGender1.AutoSize = true;
            this.rbGender1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender1.Location = new System.Drawing.Point(17, 11);
            this.rbGender1.Name = "rbGender1";
            this.rbGender1.Size = new System.Drawing.Size(49, 18);
            this.rbGender1.TabIndex = 9;
            this.rbGender1.TabStop = true;
            this.rbGender1.Text = "Male";
            this.rbGender1.UseVisualStyleBackColor = true;
            this.rbGender1.CheckedChanged += new System.EventHandler(this.rbGender1_CheckedChanged);
            this.rbGender1.Click += new System.EventHandler(this.rbGenderAll_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label55);
            this.panel8.Controls.Add(this.label54);
            this.panel8.Controls.Add(this.cmbMedicareTypeCode);
            this.panel8.Controls.Add(this.label50);
            this.panel8.Controls.Add(this.cmbDefaultTypeCode);
            this.panel8.Controls.Add(this.label51);
            this.panel8.Controls.Add(this.label52);
            this.panel8.Controls.Add(this.label53);
            this.panel8.Controls.Add(this.label57);
            this.panel8.Controls.Add(this.label56);
            this.panel8.Controls.Add(this.label58);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 84);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel8.Size = new System.Drawing.Size(564, 61);
            this.panel8.TabIndex = 2;
            // 
            // label55
            // 
            this.label55.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label55.AutoEllipsis = true;
            this.label55.AutoSize = true;
            this.label55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(4, 34);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(156, 14);
            this.label55.TabIndex = 64;
            this.label55.Text = "Medicare Secondary Type :";
            // 
            // label54
            // 
            this.label54.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label54.AutoEllipsis = true;
            this.label54.AutoSize = true;
            this.label54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(60, 8);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(100, 14);
            this.label54.TabIndex = 64;
            this.label54.Text = "Insurance Type :";
            // 
            // cmbMedicareTypeCode
            // 
            this.cmbMedicareTypeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedicareTypeCode.FormattingEnabled = true;
            this.cmbMedicareTypeCode.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cmbMedicareTypeCode.Location = new System.Drawing.Point(163, 31);
            this.cmbMedicareTypeCode.Name = "cmbMedicareTypeCode";
            this.cmbMedicareTypeCode.Size = new System.Drawing.Size(319, 22);
            this.cmbMedicareTypeCode.TabIndex = 63;
            this.cmbMedicareTypeCode.SelectedIndexChanged += new System.EventHandler(this.cmbMedicareTypeCode_SelectedIndexChanged);
            this.cmbMedicareTypeCode.MouseEnter += new System.EventHandler(this.cmbMedicareTypeCode_MouseEnter);
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(563, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 56);
            this.label50.TabIndex = 57;
            // 
            // cmbDefaultTypeCode
            // 
            this.cmbDefaultTypeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultTypeCode.FormattingEnabled = true;
            this.cmbDefaultTypeCode.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cmbDefaultTypeCode.Location = new System.Drawing.Point(163, 5);
            this.cmbDefaultTypeCode.Name = "cmbDefaultTypeCode";
            this.cmbDefaultTypeCode.Size = new System.Drawing.Size(319, 22);
            this.cmbDefaultTypeCode.TabIndex = 0;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(0, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 56);
            this.label51.TabIndex = 56;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(0, 57);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(564, 1);
            this.label52.TabIndex = 55;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(0, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(564, 1);
            this.label53.TabIndex = 54;
            // 
            // label57
            // 
            this.label57.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label57.AutoEllipsis = true;
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.Red;
            this.label57.Location = new System.Drawing.Point(51, 4);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(14, 14);
            this.label57.TabIndex = 65;
            this.label57.Text = "*";
            // 
            // label56
            // 
            this.label56.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label56.AutoEllipsis = true;
            this.label56.AutoSize = true;
            this.label56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label56.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label56.Location = new System.Drawing.Point(434, 37);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(101, 11);
            this.label56.TabIndex = 64;
            this.label56.Text = "(if medicare secondary)";
            this.label56.Visible = false;
            // 
            // label58
            // 
            this.label58.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label58.AutoEllipsis = true;
            this.label58.AutoSize = true;
            this.label58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label58.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label58.Location = new System.Drawing.Point(461, 11);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(39, 11);
            this.label58.TabIndex = 64;
            this.label58.Text = "(default)";
            this.label58.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label38);
            this.panel3.Controls.Add(this.label39);
            this.panel3.Controls.Add(this.label40);
            this.panel3.Controls.Add(this.label41);
            this.panel3.Controls.Add(this.txtInsurance);
            this.panel3.Controls.Add(this.btnModifyInsurance);
            this.panel3.Controls.Add(this.btn_AddInsurance);
            this.panel3.Controls.Add(this.rbInactive);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.btnInsDelete);
            this.panel3.Controls.Add(this.btnInsBrowse);
            this.panel3.Controls.Add(this.radSetAsTertiary);
            this.panel3.Controls.Add(this.radSetAsPrimary);
            this.panel3.Controls.Add(this.lblInsuName);
            this.panel3.Controls.Add(this.radSetAsSecondary);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(564, 53);
            this.panel3.TabIndex = 1;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Right;
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(563, 1);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 48);
            this.label38.TabIndex = 61;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Left;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(0, 1);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 48);
            this.label39.TabIndex = 60;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label40.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(0, 49);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(564, 1);
            this.label40.TabIndex = 59;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(564, 1);
            this.label41.TabIndex = 58;
            // 
            // txtInsurance
            // 
            this.txtInsurance.BackColor = System.Drawing.Color.White;
            this.txtInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsurance.ForeColor = System.Drawing.Color.Black;
            this.txtInsurance.Location = new System.Drawing.Point(144, 5);
            this.txtInsurance.Name = "txtInsurance";
            this.txtInsurance.ReadOnly = true;
            this.txtInsurance.Size = new System.Drawing.Size(289, 22);
            this.txtInsurance.TabIndex = 1;
            this.txtInsurance.TabStop = false;
            // 
            // btnModifyInsurance
            // 
            this.btnModifyInsurance.AutoEllipsis = true;
            this.btnModifyInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnModifyInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifyInsurance.BackgroundImage")));
            this.btnModifyInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifyInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnModifyInsurance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnModifyInsurance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnModifyInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyInsurance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyInsurance.Image")));
            this.btnModifyInsurance.Location = new System.Drawing.Point(508, 5);
            this.btnModifyInsurance.Name = "btnModifyInsurance";
            this.btnModifyInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnModifyInsurance.TabIndex = 4;
            this.toolTipInsurance.SetToolTip(this.btnModifyInsurance, "Modify Insurance Plan");
            this.btnModifyInsurance.UseVisualStyleBackColor = false;
            this.btnModifyInsurance.Click += new System.EventHandler(this.btnModifyInsurance_Click);
            // 
            // btn_AddInsurance
            // 
            this.btn_AddInsurance.AutoEllipsis = true;
            this.btn_AddInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AddInsurance.BackgroundImage")));
            this.btn_AddInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_AddInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btn_AddInsurance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btn_AddInsurance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btn_AddInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddInsurance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddInsurance.Image")));
            this.btn_AddInsurance.Location = new System.Drawing.Point(484, 5);
            this.btn_AddInsurance.Name = "btn_AddInsurance";
            this.btn_AddInsurance.Size = new System.Drawing.Size(22, 22);
            this.btn_AddInsurance.TabIndex = 3;
            this.toolTipInsurance.SetToolTip(this.btn_AddInsurance, "Add Insurance Plan");
            this.btn_AddInsurance.UseVisualStyleBackColor = false;
            this.btn_AddInsurance.Click += new System.EventHandler(this.btn_AddInsurance_Click);
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Checked = true;
            this.rbInactive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInactive.Location = new System.Drawing.Point(386, 29);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(74, 18);
            this.rbInactive.TabIndex = 7;
            this.rbInactive.TabStop = true;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(91, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 14);
            this.label13.TabIndex = 32;
            this.label13.Text = "Status :";
            // 
            // btnInsDelete
            // 
            this.btnInsDelete.AutoEllipsis = true;
            this.btnInsDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnInsDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsDelete.BackgroundImage")));
            this.btnInsDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnInsDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnInsDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnInsDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnInsDelete.Image")));
            this.btnInsDelete.Location = new System.Drawing.Point(460, 5);
            this.btnInsDelete.Name = "btnInsDelete";
            this.btnInsDelete.Size = new System.Drawing.Size(22, 22);
            this.btnInsDelete.TabIndex = 2;
            this.toolTipInsurance.SetToolTip(this.btnInsDelete, "Clear All");
            this.btnInsDelete.UseVisualStyleBackColor = false;
            this.btnInsDelete.Click += new System.EventHandler(this.btnInsDelete_Click);
            this.btnInsDelete.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnInsDelete.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnInsBrowse
            // 
            this.btnInsBrowse.AutoEllipsis = true;
            this.btnInsBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnInsBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsBrowse.BackgroundImage")));
            this.btnInsBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnInsBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnInsBrowse.Image")));
            this.btnInsBrowse.Location = new System.Drawing.Point(436, 5);
            this.btnInsBrowse.Name = "btnInsBrowse";
            this.btnInsBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnInsBrowse.TabIndex = 0;
            this.toolTipInsurance.SetToolTip(this.btnInsBrowse, "Browse Insurance Plan");
            this.btnInsBrowse.UseVisualStyleBackColor = false;
            this.btnInsBrowse.Click += new System.EventHandler(this.btnInsBrowse_Click);
            this.btnInsBrowse.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnInsBrowse.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // radSetAsTertiary
            // 
            this.radSetAsTertiary.AutoSize = true;
            this.radSetAsTertiary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSetAsTertiary.ForeColor = System.Drawing.Color.ForestGreen;
            this.radSetAsTertiary.Location = new System.Drawing.Point(309, 29);
            this.radSetAsTertiary.Name = "radSetAsTertiary";
            this.radSetAsTertiary.Size = new System.Drawing.Size(67, 18);
            this.radSetAsTertiary.TabIndex = 6;
            this.radSetAsTertiary.TabStop = true;
            this.radSetAsTertiary.Text = "Tertiary";
            this.radSetAsTertiary.UseVisualStyleBackColor = true;
            this.radSetAsTertiary.CheckedChanged += new System.EventHandler(this.radSetAsTertiary_CheckedChanged);
            // 
            // radSetAsPrimary
            // 
            this.radSetAsPrimary.AutoSize = true;
            this.radSetAsPrimary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSetAsPrimary.ForeColor = System.Drawing.Color.DarkRed;
            this.radSetAsPrimary.Location = new System.Drawing.Point(144, 29);
            this.radSetAsPrimary.Name = "radSetAsPrimary";
            this.radSetAsPrimary.Size = new System.Drawing.Size(64, 18);
            this.radSetAsPrimary.TabIndex = 4;
            this.radSetAsPrimary.TabStop = true;
            this.radSetAsPrimary.Text = "Primary";
            this.radSetAsPrimary.UseVisualStyleBackColor = true;
            this.radSetAsPrimary.CheckedChanged += new System.EventHandler(this.radSetAsPrimary_CheckedChanged);
            // 
            // lblInsuName
            // 
            this.lblInsuName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuName.AutoEllipsis = true;
            this.lblInsuName.AutoSize = true;
            this.lblInsuName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuName.Location = new System.Drawing.Point(47, 9);
            this.lblInsuName.Name = "lblInsuName";
            this.lblInsuName.Size = new System.Drawing.Size(94, 14);
            this.lblInsuName.TabIndex = 10;
            this.lblInsuName.Text = "Insurance Plan :";
            this.lblInsuName.Click += new System.EventHandler(this.lblInsuName_Click);
            // 
            // radSetAsSecondary
            // 
            this.radSetAsSecondary.AutoSize = true;
            this.radSetAsSecondary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSetAsSecondary.ForeColor = System.Drawing.Color.OrangeRed;
            this.radSetAsSecondary.Location = new System.Drawing.Point(217, 29);
            this.radSetAsSecondary.Name = "radSetAsSecondary";
            this.radSetAsSecondary.Size = new System.Drawing.Size(82, 18);
            this.radSetAsSecondary.TabIndex = 5;
            this.radSetAsSecondary.TabStop = true;
            this.radSetAsSecondary.Text = "Secondary";
            this.radSetAsSecondary.UseVisualStyleBackColor = true;
            this.radSetAsSecondary.CheckedChanged += new System.EventHandler(this.radSetAsSecondary_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(37, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 47;
            this.label1.Text = "*";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel6.Size = new System.Drawing.Size(564, 28);
            this.panel6.TabIndex = 50;
            this.panel6.TabStop = true;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.lblPrimaryInsuDetails);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(564, 25);
            this.panel7.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(563, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 23);
            this.label2.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 23);
            this.label3.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(564, 1);
            this.label4.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(564, 1);
            this.label5.TabIndex = 58;
            // 
            // lblPrimaryInsuDetails
            // 
            this.lblPrimaryInsuDetails.AutoEllipsis = true;
            this.lblPrimaryInsuDetails.AutoSize = true;
            this.lblPrimaryInsuDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrimaryInsuDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryInsuDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPrimaryInsuDetails.Location = new System.Drawing.Point(0, 0);
            this.lblPrimaryInsuDetails.Name = "lblPrimaryInsuDetails";
            this.lblPrimaryInsuDetails.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.lblPrimaryInsuDetails.Size = new System.Drawing.Size(267, 24);
            this.lblPrimaryInsuDetails.TabIndex = 0;
            this.lblPrimaryInsuDetails.Text = "Patient Primary Insurance Information :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.txtSubscriberPolicy);
            this.groupBox2.Controls.Add(this.lblSubscribePolicy);
            this.groupBox2.Controls.Add(this.btnVwInsuDocs);
            this.groupBox2.Controls.Add(this.btnScanInsuDocs);
            this.groupBox2.Controls.Add(this.txtAddress1);
            this.groupBox2.Controls.Add(this.txtAddress2);
            this.groupBox2.Controls.Add(this.txtCity);
            this.groupBox2.Controls.Add(this.txtCounty);
            this.groupBox2.Controls.Add(this.txtZip);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cmbCountry);
            this.groupBox2.Controls.Add(this.cmbState);
            this.groupBox2.Controls.Add(this.label49);
            this.groupBox2.Location = new System.Drawing.Point(682, 278);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(72, 75);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
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
            this.label35.Location = new System.Drawing.Point(65, 50);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(35, 14);
            this.label35.TabIndex = 45;
            this.label35.Text = "City :";
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
            this.label25.Location = new System.Drawing.Point(5, 23);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(95, 14);
            this.label25.TabIndex = 42;
            this.label25.Text = "Address Line 1 :";
            this.label25.Visible = false;
            // 
            // txtSubscriberPolicy
            // 
            this.txtSubscriberPolicy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberPolicy.ForeColor = System.Drawing.Color.Black;
            this.txtSubscriberPolicy.Location = new System.Drawing.Point(161, 109);
            this.txtSubscriberPolicy.Name = "txtSubscriberPolicy";
            this.txtSubscriberPolicy.Size = new System.Drawing.Size(10, 22);
            this.txtSubscriberPolicy.TabIndex = 32;
            this.txtSubscriberPolicy.Visible = false;
            // 
            // lblSubscribePolicy
            // 
            this.lblSubscribePolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscribePolicy.AutoEllipsis = true;
            this.lblSubscribePolicy.AutoSize = true;
            this.lblSubscribePolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscribePolicy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscribePolicy.Location = new System.Drawing.Point(90, 113);
            this.lblSubscribePolicy.Name = "lblSubscribePolicy";
            this.lblSubscribePolicy.Size = new System.Drawing.Size(70, 14);
            this.lblSubscribePolicy.TabIndex = 8;
            this.lblSubscribePolicy.Text = "Sub Policy :";
            this.lblSubscribePolicy.Visible = false;
            // 
            // btnVwInsuDocs
            // 
            this.btnVwInsuDocs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVwInsuDocs.BackgroundImage")));
            this.btnVwInsuDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVwInsuDocs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnVwInsuDocs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnVwInsuDocs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnVwInsuDocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVwInsuDocs.Location = new System.Drawing.Point(182, 115);
            this.btnVwInsuDocs.Name = "btnVwInsuDocs";
            this.btnVwInsuDocs.Size = new System.Drawing.Size(10, 10);
            this.btnVwInsuDocs.TabIndex = 20;
            this.btnVwInsuDocs.Text = "View Insurance Docs";
            this.btnVwInsuDocs.UseVisualStyleBackColor = true;
            this.btnVwInsuDocs.Visible = false;
            this.btnVwInsuDocs.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnVwInsuDocs.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnScanInsuDocs
            // 
            this.btnScanInsuDocs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScanInsuDocs.BackgroundImage")));
            this.btnScanInsuDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScanInsuDocs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnScanInsuDocs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnScanInsuDocs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnScanInsuDocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanInsuDocs.Location = new System.Drawing.Point(217, 115);
            this.btnScanInsuDocs.Name = "btnScanInsuDocs";
            this.btnScanInsuDocs.Size = new System.Drawing.Size(10, 10);
            this.btnScanInsuDocs.TabIndex = 21;
            this.btnScanInsuDocs.Text = "Scan Insurance Docs";
            this.btnScanInsuDocs.UseVisualStyleBackColor = true;
            this.btnScanInsuDocs.Visible = false;
            this.btnScanInsuDocs.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnScanInsuDocs.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtAddress1.Location = new System.Drawing.Point(102, 19);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(12, 22);
            this.txtAddress1.TabIndex = 14;
            this.txtAddress1.TabStop = false;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtAddress2.Location = new System.Drawing.Point(225, 23);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(12, 22);
            this.txtAddress2.TabIndex = 15;
            this.txtAddress2.TabStop = false;
            this.txtAddress2.Visible = false;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(102, 46);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(10, 22);
            this.txtCity.TabIndex = 18;
            this.txtCity.TabStop = false;
            // 
            // txtCounty
            // 
            this.txtCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounty.ForeColor = System.Drawing.Color.Black;
            this.txtCounty.Location = new System.Drawing.Point(102, 74);
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(10, 22);
            this.txtCounty.TabIndex = 19;
            this.txtCounty.TabStop = false;
            // 
            // txtZip
            // 
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZip.ForeColor = System.Drawing.Color.Black;
            this.txtZip.Location = new System.Drawing.Point(227, 50);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(10, 22);
            this.txtZip.TabIndex = 16;
            this.txtZip.TabStop = false;
            this.txtZip.Visible = false;
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            this.txtZip.Leave += new System.EventHandler(this.txtZip_Leave);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoEllipsis = true;
            this.label26.AutoSize = true;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(127, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 14);
            this.label26.TabIndex = 43;
            this.label26.Text = "Address Line 2 :";
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
            this.label27.Location = new System.Drawing.Point(188, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(31, 14);
            this.label27.TabIndex = 44;
            this.label27.Text = "Zip :";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(45, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 14);
            this.label15.TabIndex = 45;
            this.label15.Text = "County :";
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
            this.label36.Location = new System.Drawing.Point(90, 54);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(45, 14);
            this.label36.TabIndex = 46;
            this.label36.Text = "State :";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(7, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 14);
            this.label12.TabIndex = 47;
            this.label12.Text = "*";
            this.label12.Visible = false;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbCountry.Location = new System.Drawing.Point(225, 78);
            this.cmbCountry.MaxDropDownItems = 3;
            this.cmbCountry.MaxLength = 20;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(10, 22);
            this.cmbCountry.TabIndex = 20;
            this.cmbCountry.Visible = false;
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(134, 50);
            this.cmbState.MaxLength = 20;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(10, 22);
            this.cmbState.TabIndex = 17;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(167, 81);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 48;
            this.label49.Text = "Country :";
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter3.Enabled = false;
            this.splitter3.Location = new System.Drawing.Point(204, 85);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 676);
            this.splitter3.TabIndex = 28;
            this.splitter3.TabStop = false;
            // 
            // pnlTreeView
            // 
            this.pnlTreeView.Controls.Add(this.tvInsurances);
            this.pnlTreeView.Controls.Add(this.label21);
            this.pnlTreeView.Controls.Add(this.label20);
            this.pnlTreeView.Controls.Add(this.label9);
            this.pnlTreeView.Controls.Add(this.label8);
            this.pnlTreeView.Controls.Add(this.label7);
            this.pnlTreeView.Controls.Add(this.label6);
            this.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTreeView.Location = new System.Drawing.Point(0, 85);
            this.pnlTreeView.Name = "pnlTreeView";
            this.pnlTreeView.Padding = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.pnlTreeView.Size = new System.Drawing.Size(204, 676);
            this.pnlTreeView.TabIndex = 1;
            this.pnlTreeView.TabStop = true;
            // 
            // tvInsurances
            // 
            this.tvInsurances.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvInsurances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvInsurances.ForeColor = System.Drawing.Color.Black;
            this.tvInsurances.Indent = 20;
            this.tvInsurances.ItemHeight = 20;
            this.tvInsurances.Location = new System.Drawing.Point(8, 8);
            this.tvInsurances.Name = "tvInsurances";
            this.tvInsurances.Size = new System.Drawing.Size(194, 664);
            this.tvInsurances.TabIndex = 0;
            this.tvInsurances.TabStop = false;
            this.tvInsurances.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvInsurances_BeforeSelect);
            this.tvInsurances.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvInsurances_AfterSelect);
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(194, 4);
            this.label21.TabIndex = 33;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(4, 668);
            this.label20.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 672);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 1);
            this.label9.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 1);
            this.label8.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(202, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 670);
            this.label7.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 670);
            this.label6.TabIndex = 26;
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 31);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(774, 54);
            this.pnlTOP.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEligibilityCheck,
            this.toolStripButton1,
            this.toolStripButton3,
            this.tsb_ScanPatient,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(768, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
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
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 50);
            this.toolStripButton1.Tag = "Add";
            this.toolStripButton1.Text = "&Add";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Add";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(60, 50);
            this.toolStripButton3.Tag = "Remove";
            this.toolStripButton3.Text = "&Remove";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_ScanPatient
            // 
            this.tsb_ScanPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ScanPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ScanPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ScanPatient.Image")));
            this.tsb_ScanPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ScanPatient.Name = "tsb_ScanPatient";
            this.tsb_ScanPatient.Size = new System.Drawing.Size(99, 50);
            this.tsb_ScanPatient.Tag = "ScanInsurance";
            this.tsb_ScanPatient.Text = "Scan &Ins. Card";
            this.tsb_ScanPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ScanPatient.ToolTipText = "Scan Insurance Card";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
            this.tsb_OK.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            this.tsb_OK.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            this.tsb_Cancel.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlInsuInfoHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(774, 31);
            this.panel1.TabIndex = 27;
            this.panel1.TabStop = true;
            // 
            // pnlInsuInfoHeader
            // 
            this.pnlInsuInfoHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlInsuInfoHeader.BackgroundImage")));
            this.pnlInsuInfoHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_BottomBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_LeftBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_RightBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_TopBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lblInsuInfoHeader);
            this.pnlInsuInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsuInfoHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlInsuInfoHeader.Name = "pnlInsuInfoHeader";
            this.pnlInsuInfoHeader.Size = new System.Drawing.Size(768, 25);
            this.pnlInsuInfoHeader.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(766, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(767, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(768, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblInsuInfoHeader
            // 
            this.lblInsuInfoHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblInsuInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInsuInfoHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuInfoHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuInfoHeader.ForeColor = System.Drawing.Color.White;
            this.lblInsuInfoHeader.Location = new System.Drawing.Point(0, 0);
            this.lblInsuInfoHeader.Name = "lblInsuInfoHeader";
            this.lblInsuInfoHeader.Size = new System.Drawing.Size(768, 25);
            this.lblInsuInfoHeader.TabIndex = 0;
            this.lblInsuInfoHeader.Text = "  Patient Insurance Information";
            this.lblInsuInfoHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label48);
            this.panel2.Controls.Add(this.label46);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 761);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(774, 25);
            this.panel2.TabIndex = 70;
            this.panel2.TabStop = true;
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Red;
            this.label48.Location = new System.Drawing.Point(15, 4);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(14, 14);
            this.label48.TabIndex = 33;
            this.label48.Text = "*";
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.AutoEllipsis = true;
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(28, 4);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(90, 14);
            this.label46.TabIndex = 31;
            this.label46.Text = "Required fields ";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label42.Location = new System.Drawing.Point(4, 21);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(766, 1);
            this.label42.TabIndex = 12;
            this.label42.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(3, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 21);
            this.label43.TabIndex = 11;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(770, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 21);
            this.label44.TabIndex = 10;
            this.label44.Text = "label3";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(3, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(768, 1);
            this.label45.TabIndex = 9;
            this.label45.Text = "label1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Insurance.ico");
            this.imageList1.Images.SetKeyName(1, "Bullet06.ico");
            this.imageList1.Images.SetKeyName(2, "red2.png");
            this.imageList1.Images.SetKeyName(3, "orange2.png");
            this.imageList1.Images.SetKeyName(4, "green2.png");
            this.imageList1.Images.SetKeyName(5, "Inactive1.png");
            // 
            // gloPatientInsuranceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlInsuranceInfo);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientInsuranceControl";
            this.Size = new System.Drawing.Size(774, 786);
            this.Load += new System.EventHandler(this.gloPatientInsuranceControl_Load);
            this.pnlInsuranceInfo.ResumeLayout(false);
            this.pnlPrimaryInsuDetails.ResumeLayout(false);
            this.pnl_PrimaryInfo.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbPAGender.ResumeLayout(false);
            this.gbPAGender.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlTreeView.ResumeLayout(false);
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlInsuInfoHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInsuranceInfo;
        private System.Windows.Forms.Panel pnlPrimaryInsuDetails;
        private System.Windows.Forms.Button btnScanInsuDocs;
        private System.Windows.Forms.Button btnVwInsuDocs;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Button btnInsDelete;
        private System.Windows.Forms.Button btnInsBrowse;
        private System.Windows.Forms.CheckBox chkSameAsPatient;
        private System.Windows.Forms.TextBox txtSubscriberPolicy;
        private System.Windows.Forms.TextBox txtEmployer;
        private System.Windows.Forms.TextBox txtSubscriberID;
        private System.Windows.Forms.TextBox txtSubFName;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.TextBox txtInsurance;
        private System.Windows.Forms.Label lblInsuPhone;
        private System.Windows.Forms.Label lblEmployer;
        private System.Windows.Forms.Label lblSubscribePolicy;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblSubscriberID;
        private System.Windows.Forms.Label lblSubscriberName;
        private System.Windows.Forms.Label lblInsuName;
        private System.Windows.Forms.Label lblPrimaryInsuDetails;
        private System.Windows.Forms.Panel pnlInsuInfoHeader;
        private System.Windows.Forms.Label lblInsuInfoHeader;
        private System.Windows.Forms.Panel pnlTreeView;
        private System.Windows.Forms.TreeView tvInsurances;
        private System.Windows.Forms.Panel pnl_PrimaryInfo;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripButton tsbEligibilityCheck;
        private System.Windows.Forms.TextBox txtSubLName;
        private System.Windows.Forms.TextBox txtSubMName;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtCopay;
        private System.Windows.Forms.CheckBox chkAssignmentofBenifit;
        private System.Windows.Forms.ComboBox cmbRelationShip;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtCoveragePercent;
        private System.Windows.Forms.TextBox txtDeductableAmount;
        internal System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radSetAsPrimary;
        private System.Windows.Forms.RadioButton radSetAsSecondary;
        private System.Windows.Forms.RadioButton radSetAsTertiary;
        private System.Windows.Forms.GroupBox gbPAGender;
        private System.Windows.Forms.RadioButton rbGender3;
        private System.Windows.Forms.RadioButton rbGender2;
        private System.Windows.Forms.RadioButton rbGender1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.CheckBox chkAddrSameAsPatient;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.Button btnModifyInsurance;
        private System.Windows.Forms.Button btn_AddInsurance;
        private System.Windows.Forms.MaskedTextBox mtxtStartDate;
        private System.Windows.Forms.MaskedTextBox mtxtDOB;
        private System.Windows.Forms.MaskedTextBox mtxtEndDate;
        private System.Windows.Forms.ToolTip toolTipInsurance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private gloMaskControl.gloMaskBox mskInsurancePhone;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCounty;
        private System.Windows.Forms.CheckBox chkautoclaim;
        private System.Windows.Forms.CheckBox chkworkerscomp;
        internal System.Windows.Forms.Panel pnlAddresControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.ComboBox cmbMedicareTypeCode;
        private System.Windows.Forms.ComboBox cmbDefaultTypeCode;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        internal System.Windows.Forms.ToolStripButton tsb_ScanPatient;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtEligibilityInsurance;
        private System.Windows.Forms.CheckBox chkCompany;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblGroupMandatory;


    }
}
