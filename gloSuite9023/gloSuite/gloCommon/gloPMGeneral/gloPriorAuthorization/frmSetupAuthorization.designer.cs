namespace gloPMGeneral
{
    partial class frmSetupAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAuthorization));
            this.lblPatientName1 = new System.Windows.Forms.Label();
            this.lblPtN = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnrefremove = new System.Windows.Forms.Button();
            this.btnremove = new System.Windows.Forms.Button();
            this.btnToProvider = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txttoprovider = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chklimityes = new System.Windows.Forms.CheckBox();
            this.chklimitno = new System.Windows.Forms.CheckBox();
            this.mskauthexp = new System.Windows.Forms.MaskedTextBox();
            this.mskAuthorizationstart = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtvisitsallow = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInsnote = new System.Windows.Forms.TextBox();
            this.cmbGenInfoInsurance = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbboth = new System.Windows.Forms.RadioButton();
            this.rdbreferralout = new System.Windows.Forms.RadioButton();
            this.rdbreferralin = new System.Windows.Forms.RadioButton();
            this.cmbReferralProvider = new System.Windows.Forms.ComboBox();
            this.btnAdd_Referral = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtauth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAuthorizationNote = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPatientName1
            // 
            this.lblPatientName1.AutoSize = true;
            this.lblPatientName1.Location = new System.Drawing.Point(123, 16);
            this.lblPatientName1.Name = "lblPatientName1";
            this.lblPatientName1.Size = new System.Drawing.Size(0, 14);
            this.lblPatientName1.TabIndex = 30;
            // 
            // lblPtN
            // 
            this.lblPtN.AutoSize = true;
            this.lblPtN.Location = new System.Drawing.Point(64, 16);
            this.lblPtN.Name = "lblPtN";
            this.lblPtN.Size = new System.Drawing.Size(54, 14);
            this.lblPtN.TabIndex = 0;
            this.lblPtN.Text = "Patient :";
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(924, 54);
            this.pnltlsStrip.TabIndex = 2;
            this.pnltlsStrip.TabStop = true;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(924, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton1.Tag = "OK";
            this.toolStripButton1.Text = "Sa&ve&&Cls";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Save and Close";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnrefremove);
            this.panel1.Controls.Add(this.btnremove);
            this.panel1.Controls.Add(this.btnToProvider);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txttoprovider);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cmbReferralProvider);
            this.panel1.Controls.Add(this.btnAdd_Referral);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtauth);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtAuthorizationNote);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblPtN);
            this.panel1.Controls.Add(this.lblPatientName1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(924, 252);
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
            this.btnrefremove.Location = new System.Drawing.Point(502, 38);
            this.btnrefremove.Name = "btnrefremove";
            this.btnrefremove.Size = new System.Drawing.Size(21, 21);
            this.btnrefremove.TabIndex = 3;
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
            this.btnremove.Location = new System.Drawing.Point(502, 66);
            this.btnremove.Name = "btnremove";
            this.btnremove.Size = new System.Drawing.Size(21, 21);
            this.btnremove.TabIndex = 5;
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
            this.btnToProvider.Location = new System.Drawing.Point(477, 66);
            this.btnToProvider.Name = "btnToProvider";
            this.btnToProvider.Size = new System.Drawing.Size(21, 21);
            this.btnToProvider.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnToProvider, "Browse Provider");
            this.btnToProvider.UseVisualStyleBackColor = false;
            this.btnToProvider.Click += new System.EventHandler(this.button1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(225, 69);
            this.label16.MaximumSize = new System.Drawing.Size(112, 14);
            this.label16.MinimumSize = new System.Drawing.Size(112, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 14);
            this.label16.TabIndex = 82;
            this.label16.Text = "To Provider :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txttoprovider
            // 
            this.txttoprovider.BackColor = System.Drawing.Color.White;
            this.txttoprovider.ForeColor = System.Drawing.Color.Black;
            this.txttoprovider.Location = new System.Drawing.Point(339, 66);
            this.txttoprovider.MaxLength = 20;
            this.txttoprovider.Name = "txttoprovider";
            this.txttoprovider.ReadOnly = true;
            this.txttoprovider.Size = new System.Drawing.Size(134, 22);
            this.txttoprovider.TabIndex = 45;
            this.txttoprovider.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtInsnote);
            this.panel3.Controls.Add(this.cmbGenInfoInsurance);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(12, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(482, 144);
            this.panel3.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chklimityes);
            this.groupBox1.Controls.Add(this.chklimitno);
            this.groupBox1.Controls.Add(this.mskauthexp);
            this.groupBox1.Controls.Add(this.mskAuthorizationstart);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtvisitsallow);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(3, -7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // chklimityes
            // 
            this.chklimityes.AutoSize = true;
            this.chklimityes.Location = new System.Drawing.Point(170, 14);
            this.chklimityes.Name = "chklimityes";
            this.chklimityes.Size = new System.Drawing.Size(44, 17);
            this.chklimityes.TabIndex = 1;
            this.chklimityes.Text = "Yes";
            this.chklimityes.UseVisualStyleBackColor = true;
            this.chklimityes.CheckedChanged += new System.EventHandler(this.chklimityes_CheckedChanged);
            // 
            // chklimitno
            // 
            this.chklimitno.AutoSize = true;
            this.chklimitno.Location = new System.Drawing.Point(127, 14);
            this.chklimitno.Name = "chklimitno";
            this.chklimitno.Size = new System.Drawing.Size(40, 17);
            this.chklimitno.TabIndex = 0;
            this.chklimitno.Text = "No";
            this.chklimitno.UseVisualStyleBackColor = true;
            this.chklimitno.CheckedChanged += new System.EventHandler(this.chklimitno_CheckedChanged);
            // 
            // mskauthexp
            // 
            this.mskauthexp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskauthexp.Location = new System.Drawing.Point(324, 36);
            this.mskauthexp.Mask = "00/00/0000";
            this.mskauthexp.Name = "mskauthexp";
            this.mskauthexp.Size = new System.Drawing.Size(93, 22);
            this.mskauthexp.TabIndex = 3;
            this.mskauthexp.Tag = "Expriration Date";
            this.mskauthexp.ValidatingType = typeof(System.DateTime);
            this.mskauthexp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskauthexp_MouseClick);
            this.mskauthexp.Validating += new System.ComponentModel.CancelEventHandler(this.mskauthexp_Validating);
            // 
            // mskAuthorizationstart
            // 
            this.mskAuthorizationstart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskAuthorizationstart.Location = new System.Drawing.Point(125, 37);
            this.mskAuthorizationstart.Mask = "00/00/0000";
            this.mskAuthorizationstart.Name = "mskAuthorizationstart";
            this.mskAuthorizationstart.Size = new System.Drawing.Size(91, 22);
            this.mskAuthorizationstart.TabIndex = 2;
            this.mskAuthorizationstart.Tag = "Start Date";
            this.mskAuthorizationstart.ValidatingType = typeof(System.DateTime);
            this.mskAuthorizationstart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskAuthorizationstart_MouseClick);
            this.mskAuthorizationstart.Validating += new System.ComponentModel.CancelEventHandler(this.mskAuthorizationstart_Validating);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(3, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 14);
            this.label15.TabIndex = 59;
            this.label15.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 14);
            this.label13.TabIndex = 48;
            this.label13.Text = "Track Auth Limits :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(220, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(102, 14);
            this.label14.TabIndex = 50;
            this.label14.Text = "# Visits Allowed :";
            // 
            // txtvisitsallow
            // 
            this.txtvisitsallow.ForeColor = System.Drawing.Color.Black;
            this.txtvisitsallow.Location = new System.Drawing.Point(324, 62);
            this.txtvisitsallow.MaxLength = 5;
            this.txtvisitsallow.Name = "txtvisitsallow";
            this.txtvisitsallow.ShortcutsEnabled = false;
            this.txtvisitsallow.Size = new System.Drawing.Size(93, 22);
            this.txtvisitsallow.TabIndex = 4;
            this.txtvisitsallow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvisitsallow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtvisitsallow_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(52, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 14);
            this.label11.TabIndex = 43;
            this.label11.Text = "Start Date :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(224, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 14);
            this.label12.TabIndex = 45;
            this.label12.Text = "Expiration Date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 14);
            this.label1.TabIndex = 54;
            this.label1.Text = "Select Insurance :";
            // 
            // txtInsnote
            // 
            this.txtInsnote.ForeColor = System.Drawing.Color.Black;
            this.txtInsnote.Location = new System.Drawing.Point(114, 115);
            this.txtInsnote.MaxLength = 255;
            this.txtInsnote.Name = "txtInsnote";
            this.txtInsnote.Size = new System.Drawing.Size(306, 22);
            this.txtInsnote.TabIndex = 12;
            // 
            // cmbGenInfoInsurance
            // 
            this.cmbGenInfoInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGenInfoInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenInfoInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenInfoInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbGenInfoInsurance.FormattingEnabled = true;
            this.cmbGenInfoInsurance.Location = new System.Drawing.Point(114, 88);
            this.cmbGenInfoInsurance.Name = "cmbGenInfoInsurance";
            this.cmbGenInfoInsurance.Size = new System.Drawing.Size(306, 22);
            this.cmbGenInfoInsurance.TabIndex = 11;
            this.cmbGenInfoInsurance.MouseLeave += new System.EventHandler(this.cmbGenInfoInsurance_MouseHover);
            this.cmbGenInfoInsurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbGenInfoInsurance_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 14);
            this.label2.TabIndex = 56;
            this.label2.Text = "Insurance Note :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbboth);
            this.panel2.Controls.Add(this.rdbreferralout);
            this.panel2.Controls.Add(this.rdbreferralin);
            this.panel2.Location = new System.Drawing.Point(590, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 31);
            this.panel2.TabIndex = 0;
            // 
            // rdbboth
            // 
            this.rdbboth.AutoSize = true;
            this.rdbboth.Location = new System.Drawing.Point(260, 9);
            this.rdbboth.Name = "rdbboth";
            this.rdbboth.Size = new System.Drawing.Size(51, 18);
            this.rdbboth.TabIndex = 30000;
            this.rdbboth.Text = "Both";
            this.rdbboth.UseVisualStyleBackColor = true;
            this.rdbboth.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // rdbreferralout
            // 
            this.rdbreferralout.AutoSize = true;
            this.rdbreferralout.Location = new System.Drawing.Point(161, 9);
            this.rdbreferralout.Name = "rdbreferralout";
            this.rdbreferralout.Size = new System.Drawing.Size(91, 18);
            this.rdbreferralout.TabIndex = 20000;
            this.rdbreferralout.Text = "Referral Out";
            this.rdbreferralout.UseVisualStyleBackColor = true;
            this.rdbreferralout.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // rdbreferralin
            // 
            this.rdbreferralin.AutoSize = true;
            this.rdbreferralin.Checked = true;
            this.rdbreferralin.Location = new System.Drawing.Point(4, 9);
            this.rdbreferralin.Name = "rdbreferralin";
            this.rdbreferralin.Size = new System.Drawing.Size(149, 18);
            this.rdbreferralin.TabIndex = 0;
            this.rdbreferralin.TabStop = true;
            this.rdbreferralin.Text = "Prior Auth / Referral In";
            this.rdbreferralin.UseVisualStyleBackColor = true;
            this.rdbreferralin.CheckedChanged += new System.EventHandler(this.radreferralin_CheckedChanged);
            // 
            // cmbReferralProvider
            // 
            this.cmbReferralProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferralProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbReferralProvider.FormattingEnabled = true;
            this.cmbReferralProvider.Location = new System.Drawing.Point(339, 38);
            this.cmbReferralProvider.Name = "cmbReferralProvider";
            this.cmbReferralProvider.Size = new System.Drawing.Size(134, 22);
            this.cmbReferralProvider.TabIndex = 1;
            this.cmbReferralProvider.MouseEnter += new System.EventHandler(this.cmbReferralProvider_MouseEnter);
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
            this.btnAdd_Referral.Location = new System.Drawing.Point(477, 38);
            this.btnAdd_Referral.Name = "btnAdd_Referral";
            this.btnAdd_Referral.Size = new System.Drawing.Size(21, 21);
            this.btnAdd_Referral.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnAdd_Referral, "Browse Provider");
            this.btnAdd_Referral.UseVisualStyleBackColor = false;
            this.btnAdd_Referral.Click += new System.EventHandler(this.btnAdd_Referral_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(50, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 58;
            this.label3.Text = "*";
            // 
            // txtauth
            // 
            this.txtauth.ForeColor = System.Drawing.Color.Black;
            this.txtauth.Location = new System.Drawing.Point(118, 38);
            this.txtauth.MaxLength = 22;
            this.txtauth.Name = "txtauth";
            this.txtauth.Size = new System.Drawing.Size(102, 22);
            this.txtauth.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(225, 42);
            this.label10.MaximumSize = new System.Drawing.Size(112, 14);
            this.label10.MinimumSize = new System.Drawing.Size(112, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 14);
            this.label10.TabIndex = 41;
            this.label10.Text = "Referring Provider :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAuthorizationNote
            // 
            this.txtAuthorizationNote.ForeColor = System.Drawing.Color.Black;
            this.txtAuthorizationNote.Location = new System.Drawing.Point(654, 44);
            this.txtAuthorizationNote.MaxLength = 255;
            this.txtAuthorizationNote.Multiline = true;
            this.txtAuthorizationNote.Name = "txtAuthorizationNote";
            this.txtAuthorizationNote.Size = new System.Drawing.Size(252, 98);
            this.txtAuthorizationNote.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(529, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 14);
            this.label9.TabIndex = 39;
            this.label9.Text = "Authorization Note :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "Auth # :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(916, 1);
            this.label7.TabIndex = 34;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(916, 1);
            this.label6.TabIndex = 33;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(920, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 246);
            this.label5.TabIndex = 32;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 246);
            this.label4.TabIndex = 31;
            this.label4.Text = "label4";
            // 
            // frmSetupAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(924, 306);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupAuthorization";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prior Authorization";
            this.Load += new System.EventHandler(this.frmSetupAuthorization_Load_1);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label lblPtN;
        private System.Windows.Forms.Label lblPatientName1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAuthorizationNote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtvisitsallow;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtauth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInsnote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbreferralout;
        private System.Windows.Forms.RadioButton rdbreferralin;
        private System.Windows.Forms.ComboBox cmbReferralProvider;
        internal System.Windows.Forms.Button btnAdd_Referral;
        private System.Windows.Forms.ComboBox cmbGenInfoInsurance;
        private System.Windows.Forms.RadioButton rdbboth;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txttoprovider;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Button btnToProvider;
        internal System.Windows.Forms.Button btnremove;
        internal System.Windows.Forms.Button btnrefremove;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox mskauthexp;
        private System.Windows.Forms.MaskedTextBox mskAuthorizationstart;
        private System.Windows.Forms.CheckBox chklimityes;
        private System.Windows.Forms.CheckBox chklimitno;
    }
}