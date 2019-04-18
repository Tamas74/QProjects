namespace gloPMGeneral
{
    partial class frmModifyAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyAuthorization));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Activate = new System.Windows.Forms.ToolStripButton();
            this.tsb_Deactivate = new System.Windows.Forms.ToolStripButton();
            this.tsb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.lblActivate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnrefremove = new System.Windows.Forms.Button();
            this.btnremove = new System.Windows.Forms.Button();
            this.btnToProvider = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txttoprovider = new System.Windows.Forms.TextBox();
            this.cmbReferralProvider = new System.Windows.Forms.ComboBox();
            this.btnAdd_Referral = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mskauthexp = new System.Windows.Forms.MaskedTextBox();
            this.mskAuthorizationstart = new System.Windows.Forms.MaskedTextBox();
            this.chklimityes = new System.Windows.Forms.CheckBox();
            this.chklimitno = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtvisitsallow = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInsurancenote = new System.Windows.Forms.TextBox();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rdbboth = new System.Windows.Forms.RadioButton();
            this.rdbreferralout = new System.Windows.Forms.RadioButton();
            this.rdbreferralin = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtauth = new System.Windows.Forms.TextBox();
            this.txtAuthorizationNote = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPtN = new System.Windows.Forms.Label();
            this.lblPatientName1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbllastdos = new System.Windows.Forms.TextBox();
            this.lblVisitsused = new System.Windows.Forms.TextBox();
            this.lblvisitsremain = new System.Windows.Forms.TextBox();
            this.lblnextdos = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlText = new System.Windows.Forms.Panel();
            this.c1ProirAuthorization = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblCharges = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProirAuthorization)).BeginInit();
            this.pnlTotal.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(940, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Activate,
            this.tsb_Deactivate,
            this.tsb_Ok,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(940, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Activate
            // 
            this.tsb_Activate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Activate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Activate.Image")));
            this.tsb_Activate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Activate.Name = "tsb_Activate";
            this.tsb_Activate.Size = new System.Drawing.Size(62, 50);
            this.tsb_Activate.Tag = "OK";
            this.tsb_Activate.Text = "&Activate";
            this.tsb_Activate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Activate.ToolTipText = "Activate Prior Authorization";
            this.tsb_Activate.Click += new System.EventHandler(this.tsb_Activate_Click);
            // 
            // tsb_Deactivate
            // 
            this.tsb_Deactivate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Deactivate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Deactivate.Image")));
            this.tsb_Deactivate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Deactivate.Name = "tsb_Deactivate";
            this.tsb_Deactivate.Size = new System.Drawing.Size(76, 50);
            this.tsb_Deactivate.Tag = "OK";
            this.tsb_Deactivate.Text = "&Deactivate";
            this.tsb_Deactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Deactivate.ToolTipText = "Deactivate Prior Authorization";
            this.tsb_Deactivate.Click += new System.EventHandler(this.tsb_Deactivate_Click);
            // 
            // tsb_Ok
            // 
            this.tsb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Ok.Image")));
            this.tsb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Ok.Name = "tsb_Ok";
            this.tsb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tsb_Ok.Tag = "OK";
            this.tsb_Ok.Text = "Sa&ve&&Cls";
            this.tsb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Ok.ToolTipText = "Save and Close";
            this.tsb_Ok.Click += new System.EventHandler(this.tsb_Ok_Click);
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
            // lblActivate
            // 
            this.lblActivate.AutoSize = true;
            this.lblActivate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivate.ForeColor = System.Drawing.Color.Red;
            this.lblActivate.Location = new System.Drawing.Point(14, 4);
            this.lblActivate.Name = "lblActivate";
            this.lblActivate.Size = new System.Drawing.Size(72, 14);
            this.lblActivate.TabIndex = 2;
            this.lblActivate.Text = "lblActivate";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnrefremove);
            this.panel1.Controls.Add(this.btnremove);
            this.panel1.Controls.Add(this.btnToProvider);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txttoprovider);
            this.panel1.Controls.Add(this.cmbReferralProvider);
            this.panel1.Controls.Add(this.btnAdd_Referral);
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtauth);
            this.panel1.Controls.Add(this.txtAuthorizationNote);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblPtN);
            this.panel1.Controls.Add(this.lblPatientName1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbllastdos);
            this.panel1.Controls.Add(this.lblVisitsused);
            this.panel1.Controls.Add(this.lblvisitsremain);
            this.panel1.Controls.Add(this.lblnextdos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(940, 233);
            this.panel1.TabIndex = 1;
            // 
            // btnrefremove
            // 
            this.btnrefremove.BackColor = System.Drawing.Color.Transparent;
            this.btnrefremove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnrefremove.BackgroundImage")));
            this.btnrefremove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefremove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnrefremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefremove.Image = ((System.Drawing.Image)(resources.GetObject("btnrefremove.Image")));
            this.btnrefremove.Location = new System.Drawing.Point(497, 30);
            this.btnrefremove.Name = "btnrefremove";
            this.btnrefremove.Size = new System.Drawing.Size(21, 21);
            this.btnrefremove.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnrefremove, "Remove Provider");
            this.btnrefremove.UseVisualStyleBackColor = false;
            this.btnrefremove.Click += new System.EventHandler(this.btnrefremove_Click);
            // 
            // btnremove
            // 
            this.btnremove.BackColor = System.Drawing.Color.Transparent;
            this.btnremove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnremove.BackgroundImage")));
            this.btnremove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnremove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnremove.Image = ((System.Drawing.Image)(resources.GetObject("btnremove.Image")));
            this.btnremove.Location = new System.Drawing.Point(497, 55);
            this.btnremove.Name = "btnremove";
            this.btnremove.Size = new System.Drawing.Size(21, 21);
            this.btnremove.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnremove, "Remove Provider");
            this.btnremove.UseVisualStyleBackColor = false;
            this.btnremove.Click += new System.EventHandler(this.btnremove_Click);
            // 
            // btnToProvider
            // 
            this.btnToProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnToProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToProvider.BackgroundImage")));
            this.btnToProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnToProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnToProvider.Image")));
            this.btnToProvider.Location = new System.Drawing.Point(473, 55);
            this.btnToProvider.Name = "btnToProvider";
            this.btnToProvider.Size = new System.Drawing.Size(21, 21);
            this.btnToProvider.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnToProvider, "Browse Provider");
            this.btnToProvider.UseVisualStyleBackColor = false;
            this.btnToProvider.Click += new System.EventHandler(this.btnToProvider_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(256, 58);
            this.label10.MaximumSize = new System.Drawing.Size(78, 14);
            this.label10.MinimumSize = new System.Drawing.Size(78, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 14);
            this.label10.TabIndex = 104;
            this.label10.Text = "To Provider :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttoprovider
            // 
            this.txttoprovider.BackColor = System.Drawing.Color.White;
            this.txttoprovider.ForeColor = System.Drawing.Color.Black;
            this.txttoprovider.Location = new System.Drawing.Point(335, 54);
            this.txttoprovider.MaxLength = 20;
            this.txttoprovider.Name = "txttoprovider";
            this.txttoprovider.ReadOnly = true;
            this.txttoprovider.Size = new System.Drawing.Size(134, 22);
            this.txttoprovider.TabIndex = 103;
            this.txttoprovider.TabStop = false;
            // 
            // cmbReferralProvider
            // 
            this.cmbReferralProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferralProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbReferralProvider.FormattingEnabled = true;
            this.cmbReferralProvider.Location = new System.Drawing.Point(335, 30);
            this.cmbReferralProvider.Name = "cmbReferralProvider";
            this.cmbReferralProvider.Size = new System.Drawing.Size(134, 22);
            this.cmbReferralProvider.TabIndex = 2;
            this.cmbReferralProvider.MouseLeave += new System.EventHandler(this.cmbReferralProvider_MouseLeave);
            this.cmbReferralProvider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbReferralProvider_MouseMove);
            // 
            // btnAdd_Referral
            // 
            this.btnAdd_Referral.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd_Referral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd_Referral.BackgroundImage")));
            this.btnAdd_Referral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd_Referral.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdd_Referral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd_Referral.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd_Referral.Image")));
            this.btnAdd_Referral.Location = new System.Drawing.Point(473, 30);
            this.btnAdd_Referral.Name = "btnAdd_Referral";
            this.btnAdd_Referral.Size = new System.Drawing.Size(21, 21);
            this.btnAdd_Referral.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAdd_Referral, "Browse Provider");
            this.btnAdd_Referral.UseVisualStyleBackColor = false;
            this.btnAdd_Referral.Click += new System.EventHandler(this.btnAdd_Referral_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(222, 34);
            this.label33.MaximumSize = new System.Drawing.Size(112, 14);
            this.label33.MinimumSize = new System.Drawing.Size(112, 14);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 14);
            this.label33.TabIndex = 102;
            this.label33.Text = "Referring Provider :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(535, 151);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 14);
            this.label21.TabIndex = 98;
            this.label21.Text = "Visits Used :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtInsurancenote);
            this.panel4.Controls.Add(this.cmbInsurance);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(6, 79);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(497, 149);
            this.panel4.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mskauthexp);
            this.groupBox1.Controls.Add(this.mskAuthorizationstart);
            this.groupBox1.Controls.Add(this.chklimityes);
            this.groupBox1.Controls.Add(this.chklimitno);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtvisitsallow);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(6, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // mskauthexp
            // 
            this.mskauthexp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskauthexp.Location = new System.Drawing.Point(327, 36);
            this.mskauthexp.Mask = "00/00/0000";
            this.mskauthexp.Name = "mskauthexp";
            this.mskauthexp.Size = new System.Drawing.Size(93, 22);
            this.mskauthexp.TabIndex = 4;
            this.mskauthexp.Tag = "Expriration Date";
            this.mskauthexp.ValidatingType = typeof(System.DateTime);
            this.mskauthexp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskauthexp_MouseClick);
            this.mskauthexp.Validating += new System.ComponentModel.CancelEventHandler(this.mskauthexp_Validating);
            // 
            // mskAuthorizationstart
            // 
            this.mskAuthorizationstart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskAuthorizationstart.Location = new System.Drawing.Point(128, 36);
            this.mskAuthorizationstart.Mask = "00/00/0000";
            this.mskAuthorizationstart.Name = "mskAuthorizationstart";
            this.mskAuthorizationstart.Size = new System.Drawing.Size(91, 22);
            this.mskAuthorizationstart.TabIndex = 3;
            this.mskAuthorizationstart.Tag = "Start Date";
            this.mskAuthorizationstart.ValidatingType = typeof(System.DateTime);
            this.mskAuthorizationstart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskAuthorizationstart_MouseClick);
            this.mskAuthorizationstart.Validating += new System.ComponentModel.CancelEventHandler(this.mskAuthorizationstart_Validating);
            // 
            // chklimityes
            // 
            this.chklimityes.AutoSize = true;
            this.chklimityes.Location = new System.Drawing.Point(171, 14);
            this.chklimityes.Name = "chklimityes";
            this.chklimityes.Size = new System.Drawing.Size(44, 17);
            this.chklimityes.TabIndex = 2;
            this.chklimityes.Text = "Yes";
            this.chklimityes.UseVisualStyleBackColor = true;
            this.chklimityes.CheckedChanged += new System.EventHandler(this.chklimityes_CheckedChanged);
            // 
            // chklimitno
            // 
            this.chklimitno.AutoSize = true;
            this.chklimitno.Location = new System.Drawing.Point(128, 14);
            this.chklimitno.Name = "chklimitno";
            this.chklimitno.Size = new System.Drawing.Size(40, 17);
            this.chklimitno.TabIndex = 1;
            this.chklimitno.Text = "No";
            this.chklimitno.UseVisualStyleBackColor = true;
            this.chklimitno.CheckedChanged += new System.EventHandler(this.chklimitno_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(1, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 14);
            this.label15.TabIndex = 59;
            this.label15.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 14);
            this.label13.TabIndex = 48;
            this.label13.Text = "Track Auth Limits :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(219, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 14);
            this.label14.TabIndex = 50;
            this.label14.Text = "# Visits Allowed :";
            // 
            // txtvisitsallow
            // 
            this.txtvisitsallow.ForeColor = System.Drawing.Color.Black;
            this.txtvisitsallow.Location = new System.Drawing.Point(326, 61);
            this.txtvisitsallow.MaxLength = 5;
            this.txtvisitsallow.Name = "txtvisitsallow";
            this.txtvisitsallow.ShortcutsEnabled = false;
            this.txtvisitsallow.Size = new System.Drawing.Size(93, 22);
            this.txtvisitsallow.TabIndex = 5;
            this.txtvisitsallow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvisitsallow.TextChanged += new System.EventHandler(this.txtvisitsallow_TextChanged);
            this.txtvisitsallow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtvisitsallow_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(50, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 14);
            this.label11.TabIndex = 43;
            this.label11.Text = "Start Date :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(223, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 14);
            this.label12.TabIndex = 45;
            this.label12.Text = "Expiration Date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 14);
            this.label1.TabIndex = 54;
            this.label1.Text = "Select Insurance :";
            // 
            // txtInsurancenote
            // 
            this.txtInsurancenote.ForeColor = System.Drawing.Color.Black;
            this.txtInsurancenote.Location = new System.Drawing.Point(118, 124);
            this.txtInsurancenote.MaxLength = 255;
            this.txtInsurancenote.Name = "txtInsurancenote";
            this.txtInsurancenote.Size = new System.Drawing.Size(307, 22);
            this.txtInsurancenote.TabIndex = 12;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(118, 96);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(307, 22);
            this.cmbInsurance.TabIndex = 11;
            this.cmbInsurance.MouseLeave += new System.EventHandler(this.cmbInsurance_MouseLeave);
            this.cmbInsurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbInsurance_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 14);
            this.label2.TabIndex = 56;
            this.label2.Text = "Insurance Note :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rdbboth);
            this.panel5.Controls.Add(this.rdbreferralout);
            this.panel5.Controls.Add(this.rdbreferralin);
            this.panel5.Location = new System.Drawing.Point(610, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(313, 23);
            this.panel5.TabIndex = 0;
            // 
            // rdbboth
            // 
            this.rdbboth.AutoSize = true;
            this.rdbboth.Location = new System.Drawing.Point(258, 2);
            this.rdbboth.Name = "rdbboth";
            this.rdbboth.Size = new System.Drawing.Size(51, 18);
            this.rdbboth.TabIndex = 0;
            this.rdbboth.Text = "Both";
            this.rdbboth.UseVisualStyleBackColor = true;
            this.rdbboth.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // rdbreferralout
            // 
            this.rdbreferralout.AutoSize = true;
            this.rdbreferralout.Location = new System.Drawing.Point(159, 2);
            this.rdbreferralout.Name = "rdbreferralout";
            this.rdbreferralout.Size = new System.Drawing.Size(91, 18);
            this.rdbreferralout.TabIndex = 0;
            this.rdbreferralout.Text = "Referral Out";
            this.rdbreferralout.UseVisualStyleBackColor = true;
            this.rdbreferralout.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // rdbreferralin
            // 
            this.rdbreferralin.AutoSize = true;
            this.rdbreferralin.Location = new System.Drawing.Point(2, 2);
            this.rdbreferralin.Name = "rdbreferralin";
            this.rdbreferralin.Size = new System.Drawing.Size(149, 18);
            this.rdbreferralin.TabIndex = 0;
            this.rdbreferralin.Text = "Prior Auth / Referral In";
            this.rdbreferralin.UseVisualStyleBackColor = true;
            this.rdbreferralin.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(48, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 91;
            this.label3.Text = "*";
            // 
            // txtauth
            // 
            this.txtauth.ForeColor = System.Drawing.Color.Black;
            this.txtauth.Location = new System.Drawing.Point(116, 31);
            this.txtauth.MaxLength = 22;
            this.txtauth.Name = "txtauth";
            this.txtauth.Size = new System.Drawing.Size(90, 22);
            this.txtauth.TabIndex = 1;
            // 
            // txtAuthorizationNote
            // 
            this.txtAuthorizationNote.ForeColor = System.Drawing.Color.Black;
            this.txtAuthorizationNote.Location = new System.Drawing.Point(639, 31);
            this.txtAuthorizationNote.MaxLength = 255;
            this.txtAuthorizationNote.Multiline = true;
            this.txtAuthorizationNote.Name = "txtAuthorizationNote";
            this.txtAuthorizationNote.Size = new System.Drawing.Size(283, 79);
            this.txtAuthorizationNote.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(520, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 14);
            this.label9.TabIndex = 87;
            this.label9.Text = "Authorization Note :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 14);
            this.label8.TabIndex = 86;
            this.label8.Text = "Auth # :";
            // 
            // lblPtN
            // 
            this.lblPtN.AutoSize = true;
            this.lblPtN.Location = new System.Drawing.Point(61, 14);
            this.lblPtN.Name = "lblPtN";
            this.lblPtN.Size = new System.Drawing.Size(54, 14);
            this.lblPtN.TabIndex = 83;
            this.lblPtN.Text = "Patient :";
            // 
            // lblPatientName1
            // 
            this.lblPatientName1.AutoSize = true;
            this.lblPatientName1.Location = new System.Drawing.Point(116, 14);
            this.lblPatientName1.Name = "lblPatientName1";
            this.lblPatientName1.Size = new System.Drawing.Size(0, 14);
            this.lblPatientName1.TabIndex = 84;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(715, 151);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 14);
            this.label18.TabIndex = 9;
            this.label18.Text = "# Visits Remaining :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(759, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 14);
            this.label16.TabIndex = 63;
            this.label16.Text = "Next DOS :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(543, 125);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 14);
            this.label17.TabIndex = 61;
            this.label17.Text = "Last DOS :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(932, 1);
            this.label7.TabIndex = 34;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(932, 1);
            this.label6.TabIndex = 33;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(936, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 227);
            this.label5.TabIndex = 32;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 227);
            this.label4.TabIndex = 31;
            this.label4.Text = "label4";
            // 
            // lbllastdos
            // 
            this.lbllastdos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lbllastdos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllastdos.ForeColor = System.Drawing.Color.Black;
            this.lbllastdos.Location = new System.Drawing.Point(614, 121);
            this.lbllastdos.Name = "lbllastdos";
            this.lbllastdos.ReadOnly = true;
            this.lbllastdos.Size = new System.Drawing.Size(90, 22);
            this.lbllastdos.TabIndex = 105;
            this.lbllastdos.TabStop = false;
            // 
            // lblVisitsused
            // 
            this.lblVisitsused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblVisitsused.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitsused.ForeColor = System.Drawing.Color.Black;
            this.lblVisitsused.Location = new System.Drawing.Point(614, 147);
            this.lblVisitsused.Name = "lblVisitsused";
            this.lblVisitsused.ReadOnly = true;
            this.lblVisitsused.Size = new System.Drawing.Size(90, 22);
            this.lblVisitsused.TabIndex = 106;
            this.lblVisitsused.TabStop = false;
            this.lblVisitsused.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblvisitsremain
            // 
            this.lblvisitsremain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblvisitsremain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvisitsremain.ForeColor = System.Drawing.Color.Black;
            this.lblvisitsremain.Location = new System.Drawing.Point(831, 147);
            this.lblvisitsremain.Name = "lblvisitsremain";
            this.lblvisitsremain.ReadOnly = true;
            this.lblvisitsremain.Size = new System.Drawing.Size(90, 22);
            this.lblvisitsremain.TabIndex = 107;
            this.lblvisitsremain.TabStop = false;
            this.lblvisitsremain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblnextdos
            // 
            this.lblnextdos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblnextdos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnextdos.ForeColor = System.Drawing.Color.Black;
            this.lblnextdos.Location = new System.Drawing.Point(831, 121);
            this.lblnextdos.Name = "lblnextdos";
            this.lblnextdos.ReadOnly = true;
            this.lblnextdos.Size = new System.Drawing.Size(90, 22);
            this.lblnextdos.TabIndex = 108;
            this.lblnextdos.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(16, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 14);
            this.label20.TabIndex = 64;
            this.label20.Text = "Visits :";
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.c1ProirAuthorization);
            this.pnlText.Controls.Add(this.label22);
            this.pnlText.Controls.Add(this.label26);
            this.pnlText.Controls.Add(this.label27);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 338);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlText.Size = new System.Drawing.Size(940, 204);
            this.pnlText.TabIndex = 32;
            // 
            // c1ProirAuthorization
            // 
            this.c1ProirAuthorization.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ProirAuthorization.AllowEditing = false;
            this.c1ProirAuthorization.BackColor = System.Drawing.Color.White;
            this.c1ProirAuthorization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProirAuthorization.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ProirAuthorization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProirAuthorization.ExtendLastCol = true;
            this.c1ProirAuthorization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ProirAuthorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProirAuthorization.Location = new System.Drawing.Point(4, 1);
            this.c1ProirAuthorization.Name = "c1ProirAuthorization";
            this.c1ProirAuthorization.Padding = new System.Windows.Forms.Padding(3);
            this.c1ProirAuthorization.Rows.Count = 1;
            this.c1ProirAuthorization.Rows.DefaultSize = 19;
            this.c1ProirAuthorization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProirAuthorization.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ProirAuthorization.ShowCellLabels = true;
            this.c1ProirAuthorization.Size = new System.Drawing.Size(932, 199);
            this.c1ProirAuthorization.StyleInfo = resources.GetString("c1ProirAuthorization.StyleInfo");
            this.c1ProirAuthorization.TabIndex = 31;
            this.c1ProirAuthorization.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1ProirAuthorization_AfterSort);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Location = new System.Drawing.Point(4, 200);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(932, 1);
            this.label22.TabIndex = 29;
            this.label22.Text = "label22";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Location = new System.Drawing.Point(4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(932, 1);
            this.label26.TabIndex = 28;
            this.label26.Text = "label26";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(936, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 201);
            this.label27.TabIndex = 27;
            this.label27.Text = "label27";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 201);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // pnlTotal
            // 
            this.pnlTotal.Controls.Add(this.panel6);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotal.Location = new System.Drawing.Point(0, 542);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(940, 28);
            this.pnlTotal.TabIndex = 32;
            this.pnlTotal.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.lblCharges);
            this.panel6.Controls.Add(this.label37);
            this.panel6.Controls.Add(this.lblUnits);
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(940, 28);
            this.panel6.TabIndex = 33;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(932, 1);
            this.label19.TabIndex = 29;
            this.label19.Text = "label22";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Location = new System.Drawing.Point(4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(932, 1);
            this.label34.TabIndex = 28;
            this.label34.Text = "label26";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Location = new System.Drawing.Point(936, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 25);
            this.label35.TabIndex = 27;
            this.label35.Text = "label27";
            // 
            // lblCharges
            // 
            this.lblCharges.AutoSize = true;
            this.lblCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharges.Location = new System.Drawing.Point(573, 6);
            this.lblCharges.Name = "lblCharges";
            this.lblCharges.Size = new System.Drawing.Size(0, 14);
            this.lblCharges.TabIndex = 5;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Left;
            this.label37.Location = new System.Drawing.Point(3, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1, 25);
            this.label37.TabIndex = 26;
            this.label37.Text = "label59";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits.Location = new System.Drawing.Point(427, 6);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(0, 14);
            this.lblUnits.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(510, 6);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 14);
            this.label32.TabIndex = 2;
            this.label32.Text = "Charges :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(383, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(42, 14);
            this.label24.TabIndex = 1;
            this.label24.Text = "Units :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(332, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 14);
            this.label23.TabIndex = 0;
            this.label23.Text = "Total :";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(940, 25);
            this.panel2.TabIndex = 32;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Location = new System.Drawing.Point(4, 21);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(932, 1);
            this.label28.TabIndex = 29;
            this.label28.Text = "label22";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Location = new System.Drawing.Point(4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(932, 1);
            this.label29.TabIndex = 28;
            this.label29.Text = "label26";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(936, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 22);
            this.label30.TabIndex = 27;
            this.label30.Text = "label27";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(3, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 22);
            this.label31.TabIndex = 26;
            this.label31.Text = "label59";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 313);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(940, 25);
            this.panel3.TabIndex = 30;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 54);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel7.Size = new System.Drawing.Size(940, 26);
            this.panel7.TabIndex = 33;
            this.panel7.Visible = false;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.lblActivate);
            this.panel8.Controls.Add(this.label36);
            this.panel8.Controls.Add(this.label38);
            this.panel8.Controls.Add(this.label39);
            this.panel8.Controls.Add(this.label40);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(934, 23);
            this.panel8.TabIndex = 33;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label36.Location = new System.Drawing.Point(1, 22);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(932, 1);
            this.label36.TabIndex = 29;
            this.label36.Text = "label22";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Location = new System.Drawing.Point(1, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(932, 1);
            this.label38.TabIndex = 28;
            this.label38.Text = "label26";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Right;
            this.label39.Location = new System.Drawing.Point(933, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 23);
            this.label39.TabIndex = 27;
            this.label39.Text = "label27";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(570, 6);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(0, 14);
            this.label40.TabIndex = 5;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Left;
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 23);
            this.label41.TabIndex = 26;
            this.label41.Text = "label59";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(424, 6);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(0, 14);
            this.label42.TabIndex = 4;
            // 
            // frmModifyAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(940, 570);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnlTotal);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifyAuthorization";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify Prior Authorization";
            this.Load += new System.EventHandler(this.frmModifyAuthorization_Load);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProirAuthorization)).EndInit();
            this.pnlTotal.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Ok;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnlText;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProirAuthorization;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtvisitsallow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInsurancenote;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rdbboth;
        private System.Windows.Forms.RadioButton rdbreferralout;
        private System.Windows.Forms.RadioButton rdbreferralin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtauth;
        private System.Windows.Forms.TextBox txtAuthorizationNote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPtN;
        private System.Windows.Forms.Label lblPatientName1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblCharges;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ToolStripButton tsb_Activate;
        internal System.Windows.Forms.Button btnToProvider;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txttoprovider;
        private System.Windows.Forms.ComboBox cmbReferralProvider;
        internal System.Windows.Forms.Button btnAdd_Referral;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button btnrefremove;
        internal System.Windows.Forms.Button btnremove;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblActivate;
        private System.Windows.Forms.ToolStripButton tsb_Deactivate;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox lbllastdos;
        private System.Windows.Forms.TextBox lblVisitsused;
        private System.Windows.Forms.TextBox lblvisitsremain;
        private System.Windows.Forms.TextBox lblnextdos;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox mskauthexp;
        private System.Windows.Forms.MaskedTextBox mskAuthorizationstart;
        private System.Windows.Forms.CheckBox chklimityes;
        private System.Windows.Forms.CheckBox chklimitno;
    }
}