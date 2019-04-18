namespace gloBilling
{
    partial class frmSetupClearingHouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupClearingHouse));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.txt_ftpURL = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.gbClaimManagement = new System.Windows.Forms.GroupBox();
            this.txt_WorkedTransactions = new System.Windows.Forms.TextBox();
            this.txt_Statements = new System.Windows.Forms.TextBox();
            this.txt_997INAcknowledgement = new System.Windows.Forms.TextBox();
            this.txt_Reports = new System.Windows.Forms.TextBox();
            this.txt_997OUTAcknowledgement = new System.Windows.Forms.TextBox();
            this.txt_835RemittanceAdvice = new System.Windows.Forms.TextBox();
            this.txt_Letters = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_837PclaimSubmission = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_277ClaimStatusResponse = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_276Eligibilityenquiry = new System.Windows.Forms.TextBox();
            this.txt_CSRReports = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_271EligibilityResponse = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkISA = new System.Windows.Forms.CheckBox();
            this.chkLoop1000BNM109 = new System.Windows.Forms.CheckBox();
            this.chkVenderCode = new System.Windows.Forms.CheckBox();
            this.chkSenderCode = new System.Windows.Forms.CheckBox();
            this.chk1JQulifier = new System.Windows.Forms.CheckBox();
            this.cmbTypeofData = new System.Windows.Forms.ComboBox();
            this.txtLoop1000BNM109 = new System.Windows.Forms.TextBox();
            this.txtVenderCode = new System.Windows.Forms.TextBox();
            this.txtSenderCode = new System.Windows.Forms.TextBox();
            this.txt1JQulifier = new System.Windows.Forms.TextBox();
            this.txtSubmitterID = new System.Windows.Forms.TextBox();
            this.txtReceiverID = new System.Windows.Forms.TextBox();
            this.txtNameofReceiver = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblTypeofData = new System.Windows.Forms.Label();
            this.lblSubmitterID = new System.Windows.Forms.Label();
            this.lblReceiverID = new System.Windows.Forms.Label();
            this.lblNameofReceiver = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbClaimManagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(848, 53);
            this.pnl_tlspTOP.TabIndex = 0;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton1,
            this.ts_btnSave,
            this.ts_btnClose});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(848, 53);
            this.tls.TabIndex = 0;
            this.tls.TabStop = true;
            this.tls.Text = "toolStrip1";
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(40, 50);
            this.ToolStripButton1.Text = "&Save";
            this.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolStripButton1.ToolTipText = "Save";
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
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.label19);
            this.pnl_Base.Controls.Add(this.groupBox1);
            this.pnl_Base.Controls.Add(this.gbClaimManagement);
            this.pnl_Base.Controls.Add(this.chkISA);
            this.pnl_Base.Controls.Add(this.chkLoop1000BNM109);
            this.pnl_Base.Controls.Add(this.chkVenderCode);
            this.pnl_Base.Controls.Add(this.chkSenderCode);
            this.pnl_Base.Controls.Add(this.chk1JQulifier);
            this.pnl_Base.Controls.Add(this.cmbTypeofData);
            this.pnl_Base.Controls.Add(this.txtLoop1000BNM109);
            this.pnl_Base.Controls.Add(this.txtVenderCode);
            this.pnl_Base.Controls.Add(this.txtSenderCode);
            this.pnl_Base.Controls.Add(this.txt1JQulifier);
            this.pnl_Base.Controls.Add(this.txtSubmitterID);
            this.pnl_Base.Controls.Add(this.txtReceiverID);
            this.pnl_Base.Controls.Add(this.txtNameofReceiver);
            this.pnl_Base.Controls.Add(this.txtName);
            this.pnl_Base.Controls.Add(this.lblTypeofData);
            this.pnl_Base.Controls.Add(this.lblSubmitterID);
            this.pnl_Base.Controls.Add(this.lblReceiverID);
            this.pnl_Base.Controls.Add(this.lblNameofReceiver);
            this.pnl_Base.Controls.Add(this.lblCode);
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 53);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(848, 468);
            this.pnl_Base.TabIndex = 1;
            this.pnl_Base.TabStop = true;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(36, 19);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 111;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Password);
            this.groupBox1.Controls.Add(this.txt_Username);
            this.groupBox1.Controls.Add(this.txt_ftpURL);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Location = new System.Drawing.Point(49, 302);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 121);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Parameters";
            // 
            // txt_Password
            // 
            this.txt_Password.ForeColor = System.Drawing.Color.Black;
            this.txt_Password.Location = new System.Drawing.Point(129, 86);
            this.txt_Password.MaxLength = 50;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(191, 22);
            this.txt_Password.TabIndex = 2;
            // 
            // txt_Username
            // 
            this.txt_Username.ForeColor = System.Drawing.Color.Black;
            this.txt_Username.Location = new System.Drawing.Point(129, 58);
            this.txt_Username.MaxLength = 50;
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(191, 22);
            this.txt_Username.TabIndex = 1;
            // 
            // txt_ftpURL
            // 
            this.txt_ftpURL.ForeColor = System.Drawing.Color.Black;
            this.txt_ftpURL.Location = new System.Drawing.Point(129, 30);
            this.txt_ftpURL.MaxLength = 50;
            this.txt_ftpURL.Name = "txt_ftpURL";
            this.txt_ftpURL.Size = new System.Drawing.Size(191, 22);
            this.txt_ftpURL.TabIndex = 0;
            this.txt_ftpURL.Tag = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(50, 89);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 14);
            this.label21.TabIndex = 6;
            this.label21.Text = "Password";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(42, 61);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(66, 14);
            this.label25.TabIndex = 6;
            this.label25.Text = "User Name";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(60, 33);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(48, 14);
            this.label29.TabIndex = 6;
            this.label29.Text = "ftp URL";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbClaimManagement
            // 
            this.gbClaimManagement.Controls.Add(this.txt_WorkedTransactions);
            this.gbClaimManagement.Controls.Add(this.txt_Statements);
            this.gbClaimManagement.Controls.Add(this.txt_997INAcknowledgement);
            this.gbClaimManagement.Controls.Add(this.txt_Reports);
            this.gbClaimManagement.Controls.Add(this.txt_997OUTAcknowledgement);
            this.gbClaimManagement.Controls.Add(this.txt_835RemittanceAdvice);
            this.gbClaimManagement.Controls.Add(this.txt_Letters);
            this.gbClaimManagement.Controls.Add(this.label15);
            this.gbClaimManagement.Controls.Add(this.txt_837PclaimSubmission);
            this.gbClaimManagement.Controls.Add(this.label14);
            this.gbClaimManagement.Controls.Add(this.txt_277ClaimStatusResponse);
            this.gbClaimManagement.Controls.Add(this.label7);
            this.gbClaimManagement.Controls.Add(this.txt_276Eligibilityenquiry);
            this.gbClaimManagement.Controls.Add(this.txt_CSRReports);
            this.gbClaimManagement.Controls.Add(this.label10);
            this.gbClaimManagement.Controls.Add(this.label13);
            this.gbClaimManagement.Controls.Add(this.txt_271EligibilityResponse);
            this.gbClaimManagement.Controls.Add(this.label6);
            this.gbClaimManagement.Controls.Add(this.label9);
            this.gbClaimManagement.Controls.Add(this.label12);
            this.gbClaimManagement.Controls.Add(this.label3);
            this.gbClaimManagement.Controls.Add(this.label5);
            this.gbClaimManagement.Controls.Add(this.label8);
            this.gbClaimManagement.Controls.Add(this.label11);
            this.gbClaimManagement.Controls.Add(this.label2);
            this.gbClaimManagement.Controls.Add(this.label4);
            this.gbClaimManagement.Controls.Add(this.label1);
            this.gbClaimManagement.Location = new System.Drawing.Point(394, 16);
            this.gbClaimManagement.Name = "gbClaimManagement";
            this.gbClaimManagement.Size = new System.Drawing.Size(438, 436);
            this.gbClaimManagement.TabIndex = 16;
            this.gbClaimManagement.TabStop = false;
            this.gbClaimManagement.Text = "Claim Management";
            // 
            // txt_WorkedTransactions
            // 
            this.txt_WorkedTransactions.ForeColor = System.Drawing.Color.Black;
            this.txt_WorkedTransactions.Location = new System.Drawing.Point(209, 400);
            this.txt_WorkedTransactions.MaxLength = 50;
            this.txt_WorkedTransactions.Name = "txt_WorkedTransactions";
            this.txt_WorkedTransactions.Size = new System.Drawing.Size(191, 22);
            this.txt_WorkedTransactions.TabIndex = 11;
            // 
            // txt_Statements
            // 
            this.txt_Statements.ForeColor = System.Drawing.Color.Black;
            this.txt_Statements.Location = new System.Drawing.Point(209, 372);
            this.txt_Statements.MaxLength = 50;
            this.txt_Statements.Name = "txt_Statements";
            this.txt_Statements.Size = new System.Drawing.Size(191, 22);
            this.txt_Statements.TabIndex = 10;
            // 
            // txt_997INAcknowledgement
            // 
            this.txt_997INAcknowledgement.ForeColor = System.Drawing.Color.Black;
            this.txt_997INAcknowledgement.Location = new System.Drawing.Point(209, 132);
            this.txt_997INAcknowledgement.MaxLength = 50;
            this.txt_997INAcknowledgement.Name = "txt_997INAcknowledgement";
            this.txt_997INAcknowledgement.Size = new System.Drawing.Size(191, 22);
            this.txt_997INAcknowledgement.TabIndex = 3;
            // 
            // txt_Reports
            // 
            this.txt_Reports.ForeColor = System.Drawing.Color.Black;
            this.txt_Reports.Location = new System.Drawing.Point(209, 344);
            this.txt_Reports.MaxLength = 50;
            this.txt_Reports.Name = "txt_Reports";
            this.txt_Reports.Size = new System.Drawing.Size(191, 22);
            this.txt_Reports.TabIndex = 9;
            // 
            // txt_997OUTAcknowledgement
            // 
            this.txt_997OUTAcknowledgement.ForeColor = System.Drawing.Color.Black;
            this.txt_997OUTAcknowledgement.Location = new System.Drawing.Point(209, 241);
            this.txt_997OUTAcknowledgement.MaxLength = 50;
            this.txt_997OUTAcknowledgement.Name = "txt_997OUTAcknowledgement";
            this.txt_997OUTAcknowledgement.Size = new System.Drawing.Size(191, 22);
            this.txt_997OUTAcknowledgement.TabIndex = 6;
            this.txt_997OUTAcknowledgement.TextChanged += new System.EventHandler(this.txt_997OUTAcknowledgement_TextChanged);
            // 
            // txt_835RemittanceAdvice
            // 
            this.txt_835RemittanceAdvice.ForeColor = System.Drawing.Color.Black;
            this.txt_835RemittanceAdvice.Location = new System.Drawing.Point(209, 104);
            this.txt_835RemittanceAdvice.MaxLength = 50;
            this.txt_835RemittanceAdvice.Name = "txt_835RemittanceAdvice";
            this.txt_835RemittanceAdvice.Size = new System.Drawing.Size(191, 22);
            this.txt_835RemittanceAdvice.TabIndex = 2;
            // 
            // txt_Letters
            // 
            this.txt_Letters.ForeColor = System.Drawing.Color.Black;
            this.txt_Letters.Location = new System.Drawing.Point(209, 316);
            this.txt_Letters.MaxLength = 50;
            this.txt_Letters.Name = "txt_Letters";
            this.txt_Letters.Size = new System.Drawing.Size(191, 22);
            this.txt_Letters.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(50, 403);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 14);
            this.label15.TabIndex = 6;
            this.label15.Text = "Worked Transactions";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_837PclaimSubmission
            // 
            this.txt_837PclaimSubmission.ForeColor = System.Drawing.Color.Black;
            this.txt_837PclaimSubmission.Location = new System.Drawing.Point(209, 213);
            this.txt_837PclaimSubmission.MaxLength = 50;
            this.txt_837PclaimSubmission.Name = "txt_837PclaimSubmission";
            this.txt_837PclaimSubmission.Size = new System.Drawing.Size(191, 22);
            this.txt_837PclaimSubmission.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(101, 375);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 14);
            this.label14.TabIndex = 6;
            this.label14.Text = "Statements";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_277ClaimStatusResponse
            // 
            this.txt_277ClaimStatusResponse.ForeColor = System.Drawing.Color.Black;
            this.txt_277ClaimStatusResponse.Location = new System.Drawing.Point(209, 76);
            this.txt_277ClaimStatusResponse.MaxLength = 50;
            this.txt_277ClaimStatusResponse.Name = "txt_277ClaimStatusResponse";
            this.txt_277ClaimStatusResponse.Size = new System.Drawing.Size(191, 22);
            this.txt_277ClaimStatusResponse.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "997 Acknowledgement";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_276Eligibilityenquiry
            // 
            this.txt_276Eligibilityenquiry.ForeColor = System.Drawing.Color.Black;
            this.txt_276Eligibilityenquiry.Location = new System.Drawing.Point(209, 185);
            this.txt_276Eligibilityenquiry.MaxLength = 50;
            this.txt_276Eligibilityenquiry.Name = "txt_276Eligibilityenquiry";
            this.txt_276Eligibilityenquiry.Size = new System.Drawing.Size(191, 22);
            this.txt_276Eligibilityenquiry.TabIndex = 4;
            // 
            // txt_CSRReports
            // 
            this.txt_CSRReports.ForeColor = System.Drawing.Color.Black;
            this.txt_CSRReports.Location = new System.Drawing.Point(209, 288);
            this.txt_CSRReports.MaxLength = 50;
            this.txt_CSRReports.Name = "txt_CSRReports";
            this.txt_CSRReports.Size = new System.Drawing.Size(191, 22);
            this.txt_CSRReports.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 244);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 14);
            this.label10.TabIndex = 6;
            this.label10.Text = "997 Acknowledgement";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(123, 347);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 14);
            this.label13.TabIndex = 6;
            this.label13.Text = "Reports";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_271EligibilityResponse
            // 
            this.txt_271EligibilityResponse.ForeColor = System.Drawing.Color.Black;
            this.txt_271EligibilityResponse.Location = new System.Drawing.Point(209, 48);
            this.txt_271EligibilityResponse.MaxLength = 50;
            this.txt_271EligibilityResponse.Name = "txt_271EligibilityResponse";
            this.txt_271EligibilityResponse.Size = new System.Drawing.Size(191, 22);
            this.txt_271EligibilityResponse.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "835 Remittance Advice";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(58, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 14);
            this.label9.TabIndex = 6;
            this.label9.Text = "837P Claim submission";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 319);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 14);
            this.label12.TabIndex = 6;
            this.label12.Text = "Letters";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "General";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "277 Claim Status Response";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 14);
            this.label8.TabIndex = 6;
            this.label8.Text = "276 Eligibility Enquiry";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 14);
            this.label11.TabIndex = 6;
            this.label11.Text = "CSR Reports";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Outbox";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "271 Eligibility Response";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Inbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkISA
            // 
            this.chkISA.AutoSize = true;
            this.chkISA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkISA.Location = new System.Drawing.Point(107, 270);
            this.chkISA.Name = "chkISA";
            this.chkISA.Size = new System.Drawing.Size(84, 18);
            this.chkISA.TabIndex = 14;
            this.chkISA.Text = "ISA = 01 :";
            this.chkISA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkISA.UseVisualStyleBackColor = true;
            this.chkISA.CheckedChanged += new System.EventHandler(this.chkISA_CheckedChanged);
            // 
            // chkLoop1000BNM109
            // 
            this.chkLoop1000BNM109.AutoSize = true;
            this.chkLoop1000BNM109.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLoop1000BNM109.Location = new System.Drawing.Point(49, 214);
            this.chkLoop1000BNM109.Name = "chkLoop1000BNM109";
            this.chkLoop1000BNM109.Size = new System.Drawing.Size(142, 18);
            this.chkLoop1000BNM109.TabIndex = 11;
            this.chkLoop1000BNM109.Text = "Loop 1000B NM109 :";
            this.chkLoop1000BNM109.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLoop1000BNM109.UseVisualStyleBackColor = true;
            this.chkLoop1000BNM109.CheckedChanged += new System.EventHandler(this.chkLoop1000BNM109_CheckedChanged);
            // 
            // chkVenderCode
            // 
            this.chkVenderCode.AutoSize = true;
            this.chkVenderCode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVenderCode.Location = new System.Drawing.Point(79, 184);
            this.chkVenderCode.Name = "chkVenderCode";
            this.chkVenderCode.Size = new System.Drawing.Size(112, 18);
            this.chkVenderCode.TabIndex = 9;
            this.chkVenderCode.Text = "Receiver Code :";
            this.chkVenderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVenderCode.UseVisualStyleBackColor = true;
            this.chkVenderCode.CheckedChanged += new System.EventHandler(this.chkVenderCode_CheckedChanged);
            // 
            // chkSenderCode
            // 
            this.chkSenderCode.AutoSize = true;
            this.chkSenderCode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSenderCode.Location = new System.Drawing.Point(86, 154);
            this.chkSenderCode.Name = "chkSenderCode";
            this.chkSenderCode.Size = new System.Drawing.Size(105, 18);
            this.chkSenderCode.TabIndex = 7;
            this.chkSenderCode.Text = "Sender Code :";
            this.chkSenderCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSenderCode.UseVisualStyleBackColor = true;
            this.chkSenderCode.CheckedChanged += new System.EventHandler(this.chkSenderCode_CheckedChanged);
            // 
            // chk1JQulifier
            // 
            this.chk1JQulifier.AutoSize = true;
            this.chk1JQulifier.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk1JQulifier.Location = new System.Drawing.Point(104, 124);
            this.chk1JQulifier.Name = "chk1JQulifier";
            this.chk1JQulifier.Size = new System.Drawing.Size(87, 18);
            this.chk1JQulifier.TabIndex = 5;
            this.chk1JQulifier.Text = "1J Qulifier :";
            this.chk1JQulifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk1JQulifier.UseVisualStyleBackColor = true;
            this.chk1JQulifier.CheckedChanged += new System.EventHandler(this.chk1JQulifier_CheckedChanged);
            // 
            // cmbTypeofData
            // 
            this.cmbTypeofData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeofData.ForeColor = System.Drawing.Color.Black;
            this.cmbTypeofData.FormattingEnabled = true;
            this.cmbTypeofData.Items.AddRange(new object[] {
            "",
            "Test Data",
            "Production Data"});
            this.cmbTypeofData.Location = new System.Drawing.Point(178, 240);
            this.cmbTypeofData.Name = "cmbTypeofData";
            this.cmbTypeofData.Size = new System.Drawing.Size(191, 22);
            this.cmbTypeofData.TabIndex = 13;
            // 
            // txtLoop1000BNM109
            // 
            this.txtLoop1000BNM109.Enabled = false;
            this.txtLoop1000BNM109.ForeColor = System.Drawing.Color.Black;
            this.txtLoop1000BNM109.Location = new System.Drawing.Point(200, 212);
            this.txtLoop1000BNM109.MaxLength = 50;
            this.txtLoop1000BNM109.Name = "txtLoop1000BNM109";
            this.txtLoop1000BNM109.Size = new System.Drawing.Size(169, 22);
            this.txtLoop1000BNM109.TabIndex = 12;
            // 
            // txtVenderCode
            // 
            this.txtVenderCode.Enabled = false;
            this.txtVenderCode.ForeColor = System.Drawing.Color.Black;
            this.txtVenderCode.Location = new System.Drawing.Point(200, 182);
            this.txtVenderCode.MaxLength = 50;
            this.txtVenderCode.Name = "txtVenderCode";
            this.txtVenderCode.Size = new System.Drawing.Size(169, 22);
            this.txtVenderCode.TabIndex = 10;
            // 
            // txtSenderCode
            // 
            this.txtSenderCode.Enabled = false;
            this.txtSenderCode.ForeColor = System.Drawing.Color.Black;
            this.txtSenderCode.Location = new System.Drawing.Point(200, 152);
            this.txtSenderCode.MaxLength = 50;
            this.txtSenderCode.Name = "txtSenderCode";
            this.txtSenderCode.Size = new System.Drawing.Size(169, 22);
            this.txtSenderCode.TabIndex = 8;
            // 
            // txt1JQulifier
            // 
            this.txt1JQulifier.Enabled = false;
            this.txt1JQulifier.ForeColor = System.Drawing.Color.Black;
            this.txt1JQulifier.Location = new System.Drawing.Point(200, 122);
            this.txt1JQulifier.MaxLength = 50;
            this.txt1JQulifier.Name = "txt1JQulifier";
            this.txt1JQulifier.Size = new System.Drawing.Size(169, 22);
            this.txt1JQulifier.TabIndex = 6;
            // 
            // txtSubmitterID
            // 
            this.txtSubmitterID.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterID.Location = new System.Drawing.Point(178, 94);
            this.txtSubmitterID.MaxLength = 50;
            this.txtSubmitterID.Name = "txtSubmitterID";
            this.txtSubmitterID.Size = new System.Drawing.Size(191, 22);
            this.txtSubmitterID.TabIndex = 4;
            // 
            // txtReceiverID
            // 
            this.txtReceiverID.ForeColor = System.Drawing.Color.Black;
            this.txtReceiverID.Location = new System.Drawing.Point(178, 68);
            this.txtReceiverID.MaxLength = 50;
            this.txtReceiverID.Name = "txtReceiverID";
            this.txtReceiverID.Size = new System.Drawing.Size(191, 22);
            this.txtReceiverID.TabIndex = 3;
            // 
            // txtNameofReceiver
            // 
            this.txtNameofReceiver.ForeColor = System.Drawing.Color.Black;
            this.txtNameofReceiver.Location = new System.Drawing.Point(178, 42);
            this.txtNameofReceiver.MaxLength = 50;
            this.txtNameofReceiver.Name = "txtNameofReceiver";
            this.txtNameofReceiver.Size = new System.Drawing.Size(191, 22);
            this.txtNameofReceiver.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(178, 16);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 22);
            this.txtName.TabIndex = 1;
            // 
            // lblTypeofData
            // 
            this.lblTypeofData.AutoSize = true;
            this.lblTypeofData.Location = new System.Drawing.Point(86, 244);
            this.lblTypeofData.Name = "lblTypeofData";
            this.lblTypeofData.Size = new System.Drawing.Size(87, 14);
            this.lblTypeofData.TabIndex = 5;
            this.lblTypeofData.Text = "Type of Data :";
            this.lblTypeofData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubmitterID
            // 
            this.lblSubmitterID.AutoSize = true;
            this.lblSubmitterID.Location = new System.Drawing.Point(103, 97);
            this.lblSubmitterID.Name = "lblSubmitterID";
            this.lblSubmitterID.Size = new System.Drawing.Size(70, 14);
            this.lblSubmitterID.TabIndex = 5;
            this.lblSubmitterID.Text = "Sender ID :";
            this.lblSubmitterID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReceiverID
            // 
            this.lblReceiverID.AutoSize = true;
            this.lblReceiverID.Location = new System.Drawing.Point(96, 72);
            this.lblReceiverID.Name = "lblReceiverID";
            this.lblReceiverID.Size = new System.Drawing.Size(77, 14);
            this.lblReceiverID.TabIndex = 5;
            this.lblReceiverID.Text = "Receiver ID :";
            this.lblReceiverID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNameofReceiver
            // 
            this.lblNameofReceiver.AutoSize = true;
            this.lblNameofReceiver.Location = new System.Drawing.Point(62, 46);
            this.lblNameofReceiver.Name = "lblNameofReceiver";
            this.lblNameofReceiver.Size = new System.Drawing.Size(111, 14);
            this.lblNameofReceiver.TabIndex = 5;
            this.lblNameofReceiver.Text = "Name of Receiver :";
            this.lblNameofReceiver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(48, 19);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(125, 14);
            this.lblCode.TabIndex = 5;
            this.lblCode.Text = "Clearinghouse Name :";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 464);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(840, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 461);
            this.lbl_LeftBrd.TabIndex = 0;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(844, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 461);
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
            this.lbl_TopBrd.Size = new System.Drawing.Size(842, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // frmSetupClearingHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(848, 521);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupClearingHouse";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Clearinghouse";
            this.Load += new System.EventHandler(this.frmSetupClearingHouse_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbClaimManagement.ResumeLayout(false);
            this.gbClaimManagement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.ComboBox cmbTypeofData;
        private System.Windows.Forms.TextBox txtLoop1000BNM109;
        private System.Windows.Forms.TextBox txtVenderCode;
        private System.Windows.Forms.TextBox txtSenderCode;
        private System.Windows.Forms.TextBox txt1JQulifier;
        private System.Windows.Forms.TextBox txtSubmitterID;
        private System.Windows.Forms.TextBox txtReceiverID;
        private System.Windows.Forms.TextBox txtNameofReceiver;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblTypeofData;
        private System.Windows.Forms.Label lblSubmitterID;
        private System.Windows.Forms.Label lblReceiverID;
        private System.Windows.Forms.Label lblNameofReceiver;
        private System.Windows.Forms.CheckBox chkISA;
        private System.Windows.Forms.CheckBox chkLoop1000BNM109;
        private System.Windows.Forms.CheckBox chkVenderCode;
        private System.Windows.Forms.CheckBox chkSenderCode;
        private System.Windows.Forms.CheckBox chk1JQulifier;
        private System.Windows.Forms.GroupBox gbClaimManagement;
        private System.Windows.Forms.TextBox txt_271EligibilityResponse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_WorkedTransactions;
        private System.Windows.Forms.TextBox txt_Statements;
        private System.Windows.Forms.TextBox txt_997INAcknowledgement;
        private System.Windows.Forms.TextBox txt_Reports;
        private System.Windows.Forms.TextBox txt_997OUTAcknowledgement;
        private System.Windows.Forms.TextBox txt_835RemittanceAdvice;
        private System.Windows.Forms.TextBox txt_Letters;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_837PclaimSubmission;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_277ClaimStatusResponse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_276Eligibilityenquiry;
        private System.Windows.Forms.TextBox txt_CSRReports;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.TextBox txt_ftpURL;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.ToolStripButton ToolStripButton1;
    }
}