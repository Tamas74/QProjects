namespace gloBilling
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
                    System.Windows.Forms.DateTimePicker [] cntdtControls = { dtauthexp, dtAuthorizationstart };
                    System.Windows.Forms.Control[] cntControls = { dtauthexp, dtAuthorizationstart };
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    //if (dtauthexp != null)
                    //{
                    //    try
                    //    {
                    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtauthexp);

                    //    }
                    //    catch
                    //    {
                    //    }


                    //    dtauthexp.Dispose();
                    //    dtauthexp = null;
                    //}
                }
                catch
                {
                }

                //try
                //{
                //    //if (dtAuthorizationstart != null)
                //    //{
                //    //    try
                //    //    {
                //    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtAuthorizationstart);
                //    //    }
                //    //    catch
                //    //    {
                //    //    }
                //    //    dtAuthorizationstart.Dispose();
                //    //    dtAuthorizationstart = null;
                //    //}
                //}
                //catch
                //{
                //}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAuthorization));
            this.lblPatientName1 = new System.Windows.Forms.Label();
            this.lblPtN = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.rdblimitno = new System.Windows.Forms.RadioButton();
            this.rdblimityes = new System.Windows.Forms.RadioButton();
            this.dtauthexp = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtAuthorizationstart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.txtvisitsallow = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radboth = new System.Windows.Forms.RadioButton();
            this.radreferralout = new System.Windows.Forms.RadioButton();
            this.radreferralin = new System.Windows.Forms.RadioButton();
            this.cmbGenInfoInsurance = new System.Windows.Forms.ComboBox();
            this.cmbReferralProvider = new System.Windows.Forms.ComboBox();
            this.btnAdd_Referral = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInsnote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtauth = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAuthorizationNote = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPatientName1
            // 
            this.lblPatientName1.AutoSize = true;
            this.lblPatientName1.Location = new System.Drawing.Point(127, 20);
            this.lblPatientName1.Name = "lblPatientName1";
            this.lblPatientName1.Size = new System.Drawing.Size(0, 14);
            this.lblPatientName1.TabIndex = 30;
            // 
            // lblPtN
            // 
            this.lblPtN.AutoSize = true;
            this.lblPtN.Location = new System.Drawing.Point(66, 20);
            this.lblPtN.Name = "lblPtN";
            this.lblPtN.Size = new System.Drawing.Size(54, 14);
            this.lblPtN.TabIndex = 0;
            this.lblPtN.Text = "Patient :";
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(921, 54);
            this.pnltlsStrip.TabIndex = 1;
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
            this.tls_SetupResource.Size = new System.Drawing.Size(921, 53);
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
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cmbGenInfoInsurance);
            this.panel1.Controls.Add(this.cmbReferralProvider);
            this.panel1.Controls.Add(this.btnAdd_Referral);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtInsnote);
            this.panel1.Controls.Add(this.label1);
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
            this.panel1.Size = new System.Drawing.Size(921, 233);
            this.panel1.TabIndex = 31;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.rdblimitno);
            this.panel3.Controls.Add(this.rdblimityes);
            this.panel3.Controls.Add(this.dtauthexp);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.dtAuthorizationstart);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txtvisitsallow);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Location = new System.Drawing.Point(7, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(481, 90);
            this.panel3.TabIndex = 81;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(12, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 14);
            this.label15.TabIndex = 59;
            this.label15.Text = "*";
            // 
            // rdblimitno
            // 
            this.rdblimitno.AutoSize = true;
            this.rdblimitno.Location = new System.Drawing.Point(175, 3);
            this.rdblimitno.Name = "rdblimitno";
            this.rdblimitno.Size = new System.Drawing.Size(40, 18);
            this.rdblimitno.TabIndex = 52;
            this.rdblimitno.Text = "No";
            this.rdblimitno.UseVisualStyleBackColor = true;
            // 
            // rdblimityes
            // 
            this.rdblimityes.AutoSize = true;
            this.rdblimityes.Checked = true;
            this.rdblimityes.Location = new System.Drawing.Point(117, 4);
            this.rdblimityes.Name = "rdblimityes";
            this.rdblimityes.Size = new System.Drawing.Size(45, 18);
            this.rdblimityes.TabIndex = 51;
            this.rdblimityes.TabStop = true;
            this.rdblimityes.Text = "Yes";
            this.rdblimityes.UseVisualStyleBackColor = true;
            this.rdblimityes.CheckedChanged += new System.EventHandler(this.rdblimityes_CheckedChanged);
            // 
            // dtauthexp
            // 
            this.dtauthexp.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtauthexp.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtauthexp.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtauthexp.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtauthexp.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtauthexp.CustomFormat = "MM/dd/yyyy";
            this.dtauthexp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtauthexp.Location = new System.Drawing.Point(336, 30);
            this.dtauthexp.Name = "dtauthexp";
            this.dtauthexp.Size = new System.Drawing.Size(134, 22);
            this.dtauthexp.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(264, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 14);
            this.label12.TabIndex = 45;
            this.label12.Text = "Expiration :";
            // 
            // dtAuthorizationstart
            // 
            this.dtAuthorizationstart.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtAuthorizationstart.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtAuthorizationstart.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtAuthorizationstart.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtAuthorizationstart.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtAuthorizationstart.CustomFormat = "MM/dd/yyyy";
            this.dtAuthorizationstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAuthorizationstart.Location = new System.Drawing.Point(117, 30);
            this.dtAuthorizationstart.Name = "dtAuthorizationstart";
            this.dtAuthorizationstart.Size = new System.Drawing.Size(98, 22);
            this.dtAuthorizationstart.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(72, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 14);
            this.label11.TabIndex = 43;
            this.label11.Text = "Start :";
            // 
            // txtvisitsallow
            // 
            this.txtvisitsallow.ForeColor = System.Drawing.Color.Black;
            this.txtvisitsallow.Location = new System.Drawing.Point(336, 56);
            this.txtvisitsallow.MaxLength = 20;
            this.txtvisitsallow.Name = "txtvisitsallow";
            this.txtvisitsallow.Size = new System.Drawing.Size(134, 22);
            this.txtvisitsallow.TabIndex = 49;
            this.txtvisitsallow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtvisitsallow_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(234, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 14);
            this.label14.TabIndex = 50;
            this.label14.Text = "#Visits Allowed :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 14);
            this.label13.TabIndex = 48;
            this.label13.Text = "Limit Tracking :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radboth);
            this.panel2.Controls.Add(this.radreferralout);
            this.panel2.Controls.Add(this.radreferralin);
            this.panel2.Location = new System.Drawing.Point(635, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 33);
            this.panel2.TabIndex = 80;
            // 
            // radboth
            // 
            this.radboth.AutoSize = true;
            this.radboth.Location = new System.Drawing.Point(201, 9);
            this.radboth.Name = "radboth";
            this.radboth.Size = new System.Drawing.Size(51, 18);
            this.radboth.TabIndex = 77;
            this.radboth.Text = "Both";
            this.radboth.UseVisualStyleBackColor = true;
            // 
            // radreferralout
            // 
            this.radreferralout.AutoSize = true;
            this.radreferralout.Location = new System.Drawing.Point(101, 9);
            this.radreferralout.Name = "radreferralout";
            this.radreferralout.Size = new System.Drawing.Size(91, 18);
            this.radreferralout.TabIndex = 73;
            this.radreferralout.Text = "Referral Out";
            this.radreferralout.UseVisualStyleBackColor = true;
            // 
            // radreferralin
            // 
            this.radreferralin.AutoSize = true;
            this.radreferralin.Checked = true;
            this.radreferralin.Location = new System.Drawing.Point(11, 9);
            this.radreferralin.Name = "radreferralin";
            this.radreferralin.Size = new System.Drawing.Size(81, 18);
            this.radreferralin.TabIndex = 72;
            this.radreferralin.TabStop = true;
            this.radreferralin.Text = "Referral In";
            this.radreferralin.UseVisualStyleBackColor = true;
            // 
            // cmbGenInfoInsurance
            // 
            this.cmbGenInfoInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGenInfoInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenInfoInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenInfoInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbGenInfoInsurance.FormattingEnabled = true;
            this.cmbGenInfoInsurance.Location = new System.Drawing.Point(130, 164);
            this.cmbGenInfoInsurance.Name = "cmbGenInfoInsurance";
            this.cmbGenInfoInsurance.Size = new System.Drawing.Size(347, 22);
            this.cmbGenInfoInsurance.TabIndex = 76;
            // 
            // cmbReferralProvider
            // 
            this.cmbReferralProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferralProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbReferralProvider.FormattingEnabled = true;
            this.cmbReferralProvider.Location = new System.Drawing.Point(343, 45);
            this.cmbReferralProvider.Name = "cmbReferralProvider";
            this.cmbReferralProvider.Size = new System.Drawing.Size(134, 22);
            this.cmbReferralProvider.TabIndex = 75;
            // 
            // btnAdd_Referral
            // 
            this.btnAdd_Referral.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd_Referral.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd_Referral.BackgroundImage")));
            this.btnAdd_Referral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd_Referral.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdd_Referral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd_Referral.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd_Referral.Image")));
            this.btnAdd_Referral.Location = new System.Drawing.Point(482, 46);
            this.btnAdd_Referral.Name = "btnAdd_Referral";
            this.btnAdd_Referral.Size = new System.Drawing.Size(21, 21);
            this.btnAdd_Referral.TabIndex = 74;
            this.btnAdd_Referral.UseVisualStyleBackColor = false;
            this.btnAdd_Referral.Click += new System.EventHandler(this.btnAdd_Referral_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(541, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 14);
            this.label25.TabIndex = 71;
            this.label25.Text = "Referral Type :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(57, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 58;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 14);
            this.label2.TabIndex = 56;
            this.label2.Text = "Insurance Note :";
            // 
            // txtInsnote
            // 
            this.txtInsnote.ForeColor = System.Drawing.Color.Black;
            this.txtInsnote.Location = new System.Drawing.Point(130, 194);
            this.txtInsnote.MaxLength = 250;
            this.txtInsnote.Name = "txtInsnote";
            this.txtInsnote.Size = new System.Drawing.Size(347, 22);
            this.txtInsnote.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 14);
            this.label1.TabIndex = 54;
            this.label1.Text = "Select Insurance :";
            // 
            // txtauth
            // 
            this.txtauth.ForeColor = System.Drawing.Color.Black;
            this.txtauth.Location = new System.Drawing.Point(124, 45);
            this.txtauth.MaxLength = 20;
            this.txtauth.Name = "txtauth";
            this.txtauth.Size = new System.Drawing.Size(98, 22);
            this.txtauth.TabIndex = 46;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 14);
            this.label10.TabIndex = 41;
            this.label10.Text = "Referring Provider :";
            // 
            // txtAuthorizationNote
            // 
            this.txtAuthorizationNote.ForeColor = System.Drawing.Color.Black;
            this.txtAuthorizationNote.Location = new System.Drawing.Point(635, 46);
            this.txtAuthorizationNote.MaxLength = 250;
            this.txtAuthorizationNote.Multiline = true;
            this.txtAuthorizationNote.Name = "txtAuthorizationNote";
            this.txtAuthorizationNote.Size = new System.Drawing.Size(263, 46);
            this.txtAuthorizationNote.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(514, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 14);
            this.label9.TabIndex = 39;
            this.label9.Text = "Authorization Note :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "Auth# :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(913, 1);
            this.label7.TabIndex = 34;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(913, 1);
            this.label6.TabIndex = 33;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(917, 3);
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
            // frmSetupAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(921, 287);
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
            this.Text = "Setup Prior Authorization";
            this.Load += new System.EventHandler(this.frmSetupAuthorization_Load_1);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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
        private System.Windows.Forms.DateTimePicker dtauthexp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtAuthorizationstart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtvisitsallow;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtauth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInsnote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdblimitno;
        private System.Windows.Forms.RadioButton rdblimityes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.RadioButton radreferralout;
        private System.Windows.Forms.RadioButton radreferralin;
        private System.Windows.Forms.ComboBox cmbReferralProvider;
        internal System.Windows.Forms.Button btnAdd_Referral;
        private System.Windows.Forms.ComboBox cmbGenInfoInsurance;
        private System.Windows.Forms.RadioButton radboth;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}