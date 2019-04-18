namespace gloPatient
{
    partial class gloPatientRepresentativeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientRepresentativeControl));
            this.pnlPatientRepresentativeInfo = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbPRSecurityQuestion = new System.Windows.Forms.ComboBox();
            this.txtPRSecurityAnswer = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mskPRDOB = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPRConfirmPassword = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPRConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPREmail = new System.Windows.Forms.TextBox();
            this.txtPRPassword = new System.Windows.Forms.TextBox();
            this.lblPRFirstName = new System.Windows.Forms.Label();
            this.lblPRPassword = new System.Windows.Forms.Label();
            this.txtPRFirstName = new System.Windows.Forms.TextBox();
            this.txtPRUserName = new System.Windows.Forms.TextBox();
            this.lblPRLastName = new System.Windows.Forms.Label();
            this.lblPRUserName = new System.Windows.Forms.Label();
            this.txtPRLastName = new System.Windows.Forms.TextBox();
            this.mtxtPRPhone = new gloMaskControl.gloMaskBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPRDOB = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPREmail = new System.Windows.Forms.Label();
            this.grpboxGIGender = new System.Windows.Forms.GroupBox();
            this.radbtnGIOthers = new System.Windows.Forms.RadioButton();
            this.radbtnGIFemale = new System.Windows.Forms.RadioButton();
            this.radbtnGIMale = new System.Windows.Forms.RadioButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTreeView = new System.Windows.Forms.Panel();
            this.trvPatientRepresentative = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGIHeader = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblGIHeader = new System.Windows.Forms.Label();
            this.pnlPatientRepresentativeInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grpboxGIGender.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlGIHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPatientRepresentativeInfo
            // 
            this.pnlPatientRepresentativeInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatientRepresentativeInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientRepresentativeInfo.Controls.Add(this.panel3);
            this.pnlPatientRepresentativeInfo.Controls.Add(this.splitter1);
            this.pnlPatientRepresentativeInfo.Controls.Add(this.pnlTreeView);
            this.pnlPatientRepresentativeInfo.Controls.Add(this.pnlTOP);
            this.pnlPatientRepresentativeInfo.Controls.Add(this.panel1);
            this.pnlPatientRepresentativeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientRepresentativeInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientRepresentativeInfo.Name = "pnlPatientRepresentativeInfo";
            this.pnlPatientRepresentativeInfo.Size = new System.Drawing.Size(759, 491);
            this.pnlPatientRepresentativeInfo.TabIndex = 12;
            this.pnlPatientRepresentativeInfo.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label27);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.cmbPRSecurityQuestion);
            this.panel3.Controls.Add(this.txtPRSecurityAnswer);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label59);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.mskPRDOB);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblPRConfirmPassword);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtPRConfirmPassword);
            this.panel3.Controls.Add(this.txtPREmail);
            this.panel3.Controls.Add(this.txtPRPassword);
            this.panel3.Controls.Add(this.lblPRFirstName);
            this.panel3.Controls.Add(this.lblPRPassword);
            this.panel3.Controls.Add(this.txtPRFirstName);
            this.panel3.Controls.Add(this.txtPRUserName);
            this.panel3.Controls.Add(this.lblPRLastName);
            this.panel3.Controls.Add(this.lblPRUserName);
            this.panel3.Controls.Add(this.txtPRLastName);
            this.panel3.Controls.Add(this.mtxtPRPhone);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblPRDOB);
            this.panel3.Controls.Add(this.lblPhone);
            this.panel3.Controls.Add(this.lblPREmail);
            this.panel3.Controls.Add(this.grpboxGIGender);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(261, 85);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel3.Size = new System.Drawing.Size(498, 406);
            this.panel3.TabIndex = 1;
            this.panel3.TabStop = true;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(40, 367);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 14);
            this.label27.TabIndex = 108;
            this.label27.Text = "*";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoEllipsis = true;
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(32, 333);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 14);
            this.label26.TabIndex = 107;
            this.label26.Text = "*";
            // 
            // cmbPRSecurityQuestion
            // 
            this.cmbPRSecurityQuestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPRSecurityQuestion.FormattingEnabled = true;
            this.cmbPRSecurityQuestion.Items.AddRange(new object[] {
            "",
            "What was the name of your first pet?",
            "What is the middle name of your youngest child?",
            "What school did you attend for sixth grade?",
            "What was your dream job as a child?",
            "What school did you go to?",
            "What is the name of the first beach you visited?",
            "In what city did you meet your spouse?",
            "What was the make and model of your first car?",
            "What was your maternal grandfather’s first name?",
            "What was the name of your primary school?",
            "What time of the day were you born? (hh:mm)",
            "What was your favorite place to visit as a child?",
            "What is the place of your ultimate dream vacation?",
            "What is the name of your favorite teacher?",
            "In what city does your nearest sibling live?"});
            this.cmbPRSecurityQuestion.Location = new System.Drawing.Point(158, 329);
            this.cmbPRSecurityQuestion.Name = "cmbPRSecurityQuestion";
            this.cmbPRSecurityQuestion.Size = new System.Drawing.Size(307, 22);
            this.cmbPRSecurityQuestion.TabIndex = 10;
            // 
            // txtPRSecurityAnswer
            // 
            this.txtPRSecurityAnswer.BackColor = System.Drawing.Color.White;
            this.txtPRSecurityAnswer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRSecurityAnswer.ForeColor = System.Drawing.Color.Black;
            this.txtPRSecurityAnswer.Location = new System.Drawing.Point(158, 363);
            this.txtPRSecurityAnswer.MaxLength = 50;
            this.txtPRSecurityAnswer.Name = "txtPRSecurityAnswer";
            this.txtPRSecurityAnswer.Size = new System.Drawing.Size(307, 22);
            this.txtPRSecurityAnswer.TabIndex = 11;
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
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(51, 367);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(104, 14);
            this.label25.TabIndex = 105;
            this.label25.Text = "Security Answer :";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoEllipsis = true;
            this.label24.AutoSize = true;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(43, 333);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(112, 14);
            this.label24.TabIndex = 103;
            this.label24.Text = "Security Question :";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(33, 299);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(14, 14);
            this.label23.TabIndex = 101;
            this.label23.Text = "*";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(77, 265);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(14, 14);
            this.label22.TabIndex = 100;
            this.label22.Text = "*";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(69, 231);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 99;
            this.label19.Text = "*";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(93, 197);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 14);
            this.label18.TabIndex = 98;
            this.label18.Text = "*";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(85, 155);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 14);
            this.label13.TabIndex = 97;
            this.label13.Text = "*";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(56, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 14);
            this.label8.TabIndex = 96;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(98, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 95;
            this.label7.Text = "*";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(70, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 94;
            this.label2.Text = "*";
            // 
            // label59
            // 
            this.label59.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label59.AutoEllipsis = true;
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.Red;
            this.label59.Location = new System.Drawing.Point(69, 19);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(14, 14);
            this.label59.TabIndex = 93;
            this.label59.Text = "*";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 402);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(493, 1);
            this.label3.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(493, 1);
            this.label4.TabIndex = 28;
            // 
            // mskPRDOB
            // 
            this.mskPRDOB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.mskPRDOB.ForeColor = System.Drawing.Color.Black;
            this.mskPRDOB.Location = new System.Drawing.Point(156, 117);
            this.mskPRDOB.Mask = "00/00/0000";
            this.mskPRDOB.Name = "mskPRDOB";
            this.mskPRDOB.Size = new System.Drawing.Size(95, 22);
            this.mskPRDOB.TabIndex = 4;
            this.mskPRDOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskPRDOB_MouseClick);
            this.mskPRDOB.Validating += new System.ComponentModel.CancelEventHandler(this.mskPRDOB_Validating_1);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(494, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 400);
            this.label5.TabIndex = 27;
            // 
            // lblPRConfirmPassword
            // 
            this.lblPRConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRConfirmPassword.AutoEllipsis = true;
            this.lblPRConfirmPassword.AutoSize = true;
            this.lblPRConfirmPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRConfirmPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRConfirmPassword.Location = new System.Drawing.Point(44, 299);
            this.lblPRConfirmPassword.Name = "lblPRConfirmPassword";
            this.lblPRConfirmPassword.Size = new System.Drawing.Size(111, 14);
            this.lblPRConfirmPassword.TabIndex = 87;
            this.lblPRConfirmPassword.Text = "Confirm Password :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 400);
            this.label6.TabIndex = 26;
            // 
            // txtPRConfirmPassword
            // 
            this.txtPRConfirmPassword.BackColor = System.Drawing.Color.White;
            this.txtPRConfirmPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPRConfirmPassword.Location = new System.Drawing.Point(158, 295);
            this.txtPRConfirmPassword.Name = "txtPRConfirmPassword";
            this.txtPRConfirmPassword.PasswordChar = '*';
            this.txtPRConfirmPassword.Size = new System.Drawing.Size(307, 22);
            this.txtPRConfirmPassword.TabIndex = 9;
            // 
            // txtPREmail
            // 
            this.txtPREmail.BackColor = System.Drawing.Color.White;
            this.txtPREmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPREmail.ForeColor = System.Drawing.Color.Black;
            this.txtPREmail.Location = new System.Drawing.Point(156, 83);
            this.txtPREmail.Name = "txtPREmail";
            this.txtPREmail.Size = new System.Drawing.Size(307, 22);
            this.txtPREmail.TabIndex = 3;
            // 
            // txtPRPassword
            // 
            this.txtPRPassword.BackColor = System.Drawing.Color.White;
            this.txtPRPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPRPassword.Location = new System.Drawing.Point(158, 261);
            this.txtPRPassword.Name = "txtPRPassword";
            this.txtPRPassword.PasswordChar = '*';
            this.txtPRPassword.Size = new System.Drawing.Size(307, 22);
            this.txtPRPassword.TabIndex = 8;
            // 
            // lblPRFirstName
            // 
            this.lblPRFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRFirstName.AutoEllipsis = true;
            this.lblPRFirstName.AutoSize = true;
            this.lblPRFirstName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRFirstName.Location = new System.Drawing.Point(81, 19);
            this.lblPRFirstName.Name = "lblPRFirstName";
            this.lblPRFirstName.Size = new System.Drawing.Size(72, 14);
            this.lblPRFirstName.TabIndex = 70;
            this.lblPRFirstName.Text = "First Name :";
            // 
            // lblPRPassword
            // 
            this.lblPRPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRPassword.AutoEllipsis = true;
            this.lblPRPassword.AutoSize = true;
            this.lblPRPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRPassword.Location = new System.Drawing.Point(89, 265);
            this.lblPRPassword.Name = "lblPRPassword";
            this.lblPRPassword.Size = new System.Drawing.Size(66, 14);
            this.lblPRPassword.TabIndex = 84;
            this.lblPRPassword.Text = "Password :";
            // 
            // txtPRFirstName
            // 
            this.txtPRFirstName.BackColor = System.Drawing.Color.White;
            this.txtPRFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtPRFirstName.Location = new System.Drawing.Point(156, 15);
            this.txtPRFirstName.Name = "txtPRFirstName";
            this.txtPRFirstName.Size = new System.Drawing.Size(307, 22);
            this.txtPRFirstName.TabIndex = 1;
            // 
            // txtPRUserName
            // 
            this.txtPRUserName.BackColor = System.Drawing.Color.White;
            this.txtPRUserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRUserName.ForeColor = System.Drawing.Color.Black;
            this.txtPRUserName.Location = new System.Drawing.Point(158, 227);
            this.txtPRUserName.Name = "txtPRUserName";
            this.txtPRUserName.Size = new System.Drawing.Size(307, 22);
            this.txtPRUserName.TabIndex = 7;
            // 
            // lblPRLastName
            // 
            this.lblPRLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRLastName.AutoEllipsis = true;
            this.lblPRLastName.AutoSize = true;
            this.lblPRLastName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRLastName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRLastName.Location = new System.Drawing.Point(81, 53);
            this.lblPRLastName.Name = "lblPRLastName";
            this.lblPRLastName.Size = new System.Drawing.Size(72, 14);
            this.lblPRLastName.TabIndex = 72;
            this.lblPRLastName.Text = "Last Name :";
            // 
            // lblPRUserName
            // 
            this.lblPRUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRUserName.AutoEllipsis = true;
            this.lblPRUserName.AutoSize = true;
            this.lblPRUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRUserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRUserName.Location = new System.Drawing.Point(81, 231);
            this.lblPRUserName.Name = "lblPRUserName";
            this.lblPRUserName.Size = new System.Drawing.Size(74, 14);
            this.lblPRUserName.TabIndex = 82;
            this.lblPRUserName.Text = "User Name :";
            // 
            // txtPRLastName
            // 
            this.txtPRLastName.BackColor = System.Drawing.Color.White;
            this.txtPRLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPRLastName.ForeColor = System.Drawing.Color.Black;
            this.txtPRLastName.Location = new System.Drawing.Point(156, 49);
            this.txtPRLastName.Name = "txtPRLastName";
            this.txtPRLastName.Size = new System.Drawing.Size(307, 22);
            this.txtPRLastName.TabIndex = 2;
            // 
            // mtxtPRPhone
            // 
            this.mtxtPRPhone.AllowValidate = true;
            this.mtxtPRPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPRPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtPRPhone.Location = new System.Drawing.Point(158, 193);
            this.mtxtPRPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtPRPhone.Name = "mtxtPRPhone";
            this.mtxtPRPhone.ReadOnly = false;
            this.mtxtPRPhone.Size = new System.Drawing.Size(93, 22);
            this.mtxtPRPhone.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(97, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 74;
            this.label1.Text = "Gender :";
            // 
            // lblPRDOB
            // 
            this.lblPRDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPRDOB.AutoEllipsis = true;
            this.lblPRDOB.AutoSize = true;
            this.lblPRDOB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPRDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRDOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPRDOB.Location = new System.Drawing.Point(68, 121);
            this.lblPRDOB.Name = "lblPRDOB";
            this.lblPRDOB.Size = new System.Drawing.Size(85, 14);
            this.lblPRDOB.TabIndex = 74;
            this.lblPRDOB.Text = "Date of Birth :";
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhone.AutoEllipsis = true;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(105, 197);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(50, 14);
            this.lblPhone.TabIndex = 80;
            this.lblPhone.Text = "Phone :";
            // 
            // lblPREmail
            // 
            this.lblPREmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPREmail.AutoEllipsis = true;
            this.lblPREmail.AutoSize = true;
            this.lblPREmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPREmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPREmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPREmail.Location = new System.Drawing.Point(111, 87);
            this.lblPREmail.Name = "lblPREmail";
            this.lblPREmail.Size = new System.Drawing.Size(42, 14);
            this.lblPREmail.TabIndex = 76;
            this.lblPREmail.Text = "Email :";
            // 
            // grpboxGIGender
            // 
            this.grpboxGIGender.Controls.Add(this.radbtnGIOthers);
            this.grpboxGIGender.Controls.Add(this.radbtnGIFemale);
            this.grpboxGIGender.Controls.Add(this.radbtnGIMale);
            this.grpboxGIGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpboxGIGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpboxGIGender.Location = new System.Drawing.Point(155, 143);
            this.grpboxGIGender.Name = "grpboxGIGender";
            this.grpboxGIGender.Size = new System.Drawing.Size(308, 38);
            this.grpboxGIGender.TabIndex = 5;
            this.grpboxGIGender.TabStop = false;
            // 
            // radbtnGIOthers
            // 
            this.radbtnGIOthers.AutoSize = true;
            this.radbtnGIOthers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIOthers.Location = new System.Drawing.Point(217, 14);
            this.radbtnGIOthers.Name = "radbtnGIOthers";
            this.radbtnGIOthers.Size = new System.Drawing.Size(57, 18);
            this.radbtnGIOthers.TabIndex = 3;
            this.radbtnGIOthers.TabStop = true;
            this.radbtnGIOthers.Text = "Other";
            this.radbtnGIOthers.UseVisualStyleBackColor = true;
            // 
            // radbtnGIFemale
            // 
            this.radbtnGIFemale.AutoSize = true;
            this.radbtnGIFemale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIFemale.Location = new System.Drawing.Point(105, 14);
            this.radbtnGIFemale.Name = "radbtnGIFemale";
            this.radbtnGIFemale.Size = new System.Drawing.Size(63, 18);
            this.radbtnGIFemale.TabIndex = 2;
            this.radbtnGIFemale.TabStop = true;
            this.radbtnGIFemale.Text = "Female";
            this.radbtnGIFemale.UseVisualStyleBackColor = true;
            // 
            // radbtnGIMale
            // 
            this.radbtnGIMale.AutoSize = true;
            this.radbtnGIMale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIMale.Location = new System.Drawing.Point(7, 14);
            this.radbtnGIMale.Name = "radbtnGIMale";
            this.radbtnGIMale.Size = new System.Drawing.Size(49, 18);
            this.radbtnGIMale.TabIndex = 1;
            this.radbtnGIMale.TabStop = true;
            this.radbtnGIMale.Text = "Male";
            this.radbtnGIMale.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(258, 85);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 406);
            this.splitter1.TabIndex = 28;
            this.splitter1.TabStop = false;
            // 
            // pnlTreeView
            // 
            this.pnlTreeView.Controls.Add(this.trvPatientRepresentative);
            this.pnlTreeView.Controls.Add(this.label21);
            this.pnlTreeView.Controls.Add(this.label20);
            this.pnlTreeView.Controls.Add(this.label14);
            this.pnlTreeView.Controls.Add(this.label15);
            this.pnlTreeView.Controls.Add(this.label16);
            this.pnlTreeView.Controls.Add(this.label17);
            this.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTreeView.Location = new System.Drawing.Point(0, 85);
            this.pnlTreeView.Name = "pnlTreeView";
            this.pnlTreeView.Padding = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.pnlTreeView.Size = new System.Drawing.Size(258, 406);
            this.pnlTreeView.TabIndex = 3;
            this.pnlTreeView.TabStop = true;
            // 
            // trvPatientRepresentative
            // 
            this.trvPatientRepresentative.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvPatientRepresentative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvPatientRepresentative.ForeColor = System.Drawing.Color.Black;
            this.trvPatientRepresentative.ImageIndex = 0;
            this.trvPatientRepresentative.ImageList = this.imageList1;
            this.trvPatientRepresentative.Indent = 20;
            this.trvPatientRepresentative.ItemHeight = 20;
            this.trvPatientRepresentative.Location = new System.Drawing.Point(8, 8);
            this.trvPatientRepresentative.Name = "trvPatientRepresentative";
            this.trvPatientRepresentative.SelectedImageIndex = 0;
            this.trvPatientRepresentative.ShowLines = false;
            this.trvPatientRepresentative.Size = new System.Drawing.Size(248, 394);
            this.trvPatientRepresentative.TabIndex = 20;
            this.trvPatientRepresentative.TabStop = false;
            this.trvPatientRepresentative.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvGuarantors_BeforeSelect);
            this.trvPatientRepresentative.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvGuarantors_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Gaurantor.ico");
            this.imageList1.Images.SetKeyName(1, "Bullet06.png");
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(248, 4);
            this.label21.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(4, 398);
            this.label20.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 402);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(252, 1);
            this.label14.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(4, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(252, 1);
            this.label15.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(256, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 400);
            this.label16.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 400);
            this.label17.TabIndex = 26;
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 30);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(759, 55);
            this.pnlTOP.TabIndex = 2;
            this.pnlTOP.TabStop = true;
            this.pnlTOP.Visible = false;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton3,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(753, 53);
            this.ts_Commands.TabIndex = 21;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.Visible = false;
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
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
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(40, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save";
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
            this.tsb_Cancel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlGIHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel1.Size = new System.Drawing.Size(759, 30);
            this.panel1.TabIndex = 26;
            this.panel1.TabStop = true;
            // 
            // pnlGIHeader
            // 
            this.pnlGIHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlGIHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGIHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGIHeader.Controls.Add(this.label9);
            this.pnlGIHeader.Controls.Add(this.label12);
            this.pnlGIHeader.Controls.Add(this.lblGIHeader);
            this.pnlGIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlGIHeader.Name = "pnlGIHeader";
            this.pnlGIHeader.Size = new System.Drawing.Size(759, 24);
            this.pnlGIHeader.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(0, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(759, 1);
            this.label9.TabIndex = 8;
            this.label9.Text = "label2";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(759, 1);
            this.label12.TabIndex = 5;
            this.label12.Text = "label1";
            // 
            // lblGIHeader
            // 
            this.lblGIHeader.AutoSize = true;
            this.lblGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIHeader.ForeColor = System.Drawing.Color.White;
            this.lblGIHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGIHeader.Location = new System.Drawing.Point(3, 4);
            this.lblGIHeader.Name = "lblGIHeader";
            this.lblGIHeader.Size = new System.Drawing.Size(149, 14);
            this.lblGIHeader.TabIndex = 0;
            this.lblGIHeader.Text = "Patient Representative\r\n";
            this.lblGIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gloPatientRepresentativeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlPatientRepresentativeInfo);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientRepresentativeControl";
            this.Size = new System.Drawing.Size(759, 491);
            this.Load += new System.EventHandler(this.gloPatientGuarantor_Load);
            this.pnlPatientRepresentativeInfo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grpboxGIGender.ResumeLayout(false);
            this.grpboxGIGender.PerformLayout();
            this.pnlTreeView.ResumeLayout(false);
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlGIHeader.ResumeLayout(false);
            this.pnlGIHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPatientRepresentativeInfo;
        private System.Windows.Forms.Panel pnlGIHeader;
        private System.Windows.Forms.Label lblGIHeader;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        internal System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlTreeView;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TreeView trvPatientRepresentative;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtPRConfirmPassword;
        private System.Windows.Forms.TextBox txtPRPassword;
        private System.Windows.Forms.Label lblPRPassword;
        private System.Windows.Forms.TextBox txtPRUserName;
        private System.Windows.Forms.Label lblPRUserName;
        private gloMaskControl.gloMaskBox mtxtPRPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPREmail;
        private System.Windows.Forms.Label lblPREmail;
        private System.Windows.Forms.Label lblPRDOB;
        private System.Windows.Forms.TextBox txtPRLastName;
        private System.Windows.Forms.Label lblPRLastName;
        private System.Windows.Forms.TextBox txtPRFirstName;
        private System.Windows.Forms.Label lblPRFirstName;
        private System.Windows.Forms.Label lblPRConfirmPassword;
        private System.Windows.Forms.MaskedTextBox mskPRDOB;
        private System.Windows.Forms.RadioButton radbtnGIOthers;
        private System.Windows.Forms.RadioButton radbtnGIFemale;
        private System.Windows.Forms.RadioButton radbtnGIMale;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpboxGIGender;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtPRSecurityAnswer;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbPRSecurityQuestion;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
    }
}