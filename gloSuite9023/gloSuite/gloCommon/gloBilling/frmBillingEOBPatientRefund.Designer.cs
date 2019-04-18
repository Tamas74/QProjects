namespace gloBilling
{
    partial class frmBillingEOBPatientRefund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillingEOBPatientRefund));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkPayMstIncludeNotes = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.txtRefundAmount = new System.Windows.Forms.TextBox();
            this.txtRefundReason = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.pnlCredit = new System.Windows.Forms.Panel();
            this.mskCreditExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.txtCardAuthorizationNo = new System.Windows.Forms.TextBox();
            this.lblCardAuthorizationNo = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.mskCheckDate = new System.Windows.Forms.MaskedTextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPatientNm = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPatientSearch = new System.Windows.Forms.Label();
            this.cmbPayMode = new System.Windows.Forms.ComboBox();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.lblPayType = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSaveNClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.btnModifyJournal = new System.Windows.Forms.Button();
            this.btnSetupJournal = new System.Windows.Forms.Button();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.label90 = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlCredit.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.pnlCredit);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.label24);
            this.pnlMain.Controls.Add(this.label26);
            this.pnlMain.Controls.Add(this.label27);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 82);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(550, 329);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPayMstIncludeNotes);
            this.panel1.Controls.Add(this.txtNotes);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCheckAmount);
            this.panel1.Controls.Add(this.txtRefundAmount);
            this.panel1.Controls.Add(this.txtRefundReason);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 155);
            this.panel1.TabIndex = 31;
            // 
            // chkPayMstIncludeNotes
            // 
            this.chkPayMstIncludeNotes.AutoSize = true;
            this.chkPayMstIncludeNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPayMstIncludeNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkPayMstIncludeNotes.Location = new System.Drawing.Point(93, 130);
            this.chkPayMstIncludeNotes.Name = "chkPayMstIncludeNotes";
            this.chkPayMstIncludeNotes.Size = new System.Drawing.Size(160, 18);
            this.chkPayMstIncludeNotes.TabIndex = 209;
            this.chkPayMstIncludeNotes.TabStop = false;
            this.chkPayMstIncludeNotes.Text = "Include Note on Receipt";
            this.chkPayMstIncludeNotes.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(92, 28);
            this.txtNotes.MaxLength = 255;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(430, 96);
            this.txtNotes.TabIndex = 31;
            this.txtNotes.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(278, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 207;
            this.label2.Text = "Refund Reason :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(30, 5);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 18);
            this.lblCheckAmount.TabIndex = 5;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRefundAmount
            // 
            this.txtRefundAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefundAmount.Location = new System.Drawing.Point(92, 3);
            this.txtRefundAmount.Name = "txtRefundAmount";
            this.txtRefundAmount.ShortcutsEnabled = false;
            this.txtRefundAmount.Size = new System.Drawing.Size(148, 22);
            this.txtRefundAmount.TabIndex = 0;
            this.txtRefundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRefundReason
            // 
            this.txtRefundReason.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefundReason.Location = new System.Drawing.Point(378, 3);
            this.txtRefundReason.Name = "txtRefundReason";
            this.txtRefundReason.Size = new System.Drawing.Size(144, 22);
            this.txtRefundReason.TabIndex = 208;
            this.txtRefundReason.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(47, 28);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 3;
            this.label31.Text = "Note :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCredit
            // 
            this.pnlCredit.Controls.Add(this.mskCreditExpiryDate);
            this.pnlCredit.Controls.Add(this.lblExpiryDate);
            this.pnlCredit.Controls.Add(this.txtCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.lblCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.cmbCardType);
            this.pnlCredit.Controls.Add(this.lblCardType);
            this.pnlCredit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCredit.Location = new System.Drawing.Point(4, 141);
            this.pnlCredit.Name = "pnlCredit";
            this.pnlCredit.Size = new System.Drawing.Size(542, 29);
            this.pnlCredit.TabIndex = 0;
            // 
            // mskCreditExpiryDate
            // 
            this.mskCreditExpiryDate.Location = new System.Drawing.Point(478, 4);
            this.mskCreditExpiryDate.Mask = "00/00";
            this.mskCreditExpiryDate.Name = "mskCreditExpiryDate";
            this.mskCreditExpiryDate.Size = new System.Drawing.Size(44, 20);
            this.mskCreditExpiryDate.TabIndex = 2;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblExpiryDate.Location = new System.Drawing.Point(441, 7);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(35, 14);
            this.lblExpiryDate.TabIndex = 34;
            this.lblExpiryDate.Text = "Exp :";
            // 
            // txtCardAuthorizationNo
            // 
            this.txtCardAuthorizationNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardAuthorizationNo.Location = new System.Drawing.Point(309, 4);
            this.txtCardAuthorizationNo.MaxLength = 16;
            this.txtCardAuthorizationNo.Name = "txtCardAuthorizationNo";
            this.txtCardAuthorizationNo.Size = new System.Drawing.Size(128, 20);
            this.txtCardAuthorizationNo.TabIndex = 1;
            // 
            // lblCardAuthorizationNo
            // 
            this.lblCardAuthorizationNo.AutoSize = true;
            this.lblCardAuthorizationNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardAuthorizationNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardAuthorizationNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardAuthorizationNo.Location = new System.Drawing.Point(252, 8);
            this.lblCardAuthorizationNo.Name = "lblCardAuthorizationNo";
            this.lblCardAuthorizationNo.Size = new System.Drawing.Size(55, 14);
            this.lblCardAuthorizationNo.TabIndex = 108;
            this.lblCardAuthorizationNo.Text = "Auth # :";
            this.lblCardAuthorizationNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.ForeColor = System.Drawing.Color.Black;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Items.AddRange(new object[] {
            "Card 1",
            "Card 2"});
            this.cmbCardType.Location = new System.Drawing.Point(92, 4);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(148, 21);
            this.cmbCardType.TabIndex = 0;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardType.Location = new System.Drawing.Point(18, 8);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(71, 14);
            this.lblCardType.TabIndex = 36;
            this.lblCardType.Text = "Card Type :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblCheckDate);
            this.panel3.Controls.Add(this.mskCheckDate);
            this.panel3.Controls.Add(this.lblUser);
            this.panel3.Controls.Add(this.lblDate);
            this.panel3.Controls.Add(this.txtAddress);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtTo);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.lblPatientNm);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblPatientSearch);
            this.panel3.Controls.Add(this.cmbPayMode);
            this.panel3.Controls.Add(this.lblCheckNo);
            this.panel3.Controls.Add(this.lblPayType);
            this.panel3.Controls.Add(this.txtCheckNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(542, 140);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.Location = new System.Drawing.Point(361, 116);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(78, 18);
            this.lblCheckDate.TabIndex = 215;
            this.lblCheckDate.Text = "Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mskCheckDate
            // 
            this.mskCheckDate.Location = new System.Drawing.Point(439, 115);
            this.mskCheckDate.Mask = "00/00/0000";
            this.mskCheckDate.Name = "mskCheckDate";
            this.mskCheckDate.Size = new System.Drawing.Size(83, 20);
            this.mskCheckDate.TabIndex = 214;
            this.mskCheckDate.ValidatingType = typeof(System.DateTime);
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.BackColor = System.Drawing.Color.White;
            this.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUser.ForeColor = System.Drawing.Color.Black;
            this.lblUser.Location = new System.Drawing.Point(362, 32);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(117, 22);
            this.lblUser.TabIndex = 213;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.BackColor = System.Drawing.Color.White;
            this.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblDate.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Location = new System.Drawing.Point(362, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(117, 22);
            this.lblDate.TabIndex = 213;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.Location = new System.Drawing.Point(92, 58);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(432, 54);
            this.txtAddress.TabIndex = 212;
            this.txtAddress.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(27, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 14);
            this.label4.TabIndex = 211;
            this.label4.Text = "Address  :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.ForeColor = System.Drawing.Color.Black;
            this.txtTo.Location = new System.Drawing.Point(92, 32);
            this.txtTo.MaxLength = 255;
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(191, 22);
            this.txtTo.TabIndex = 210;
            this.txtTo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(59, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 209;
            this.label3.Text = "To :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(320, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "User :";
            // 
            // lblPatientNm
            // 
            this.lblPatientNm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientNm.BackColor = System.Drawing.Color.White;
            this.lblPatientNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatientNm.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPatientNm.ForeColor = System.Drawing.Color.Black;
            this.lblPatientNm.Location = new System.Drawing.Point(92, 6);
            this.lblPatientNm.Name = "lblPatientNm";
            this.lblPatientNm.Size = new System.Drawing.Size(191, 22);
            this.lblPatientNm.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(318, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "Date :";
            // 
            // lblPatientSearch
            // 
            this.lblPatientSearch.AutoSize = true;
            this.lblPatientSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientSearch.Location = new System.Drawing.Point(35, 8);
            this.lblPatientSearch.Name = "lblPatientSearch";
            this.lblPatientSearch.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPatientSearch.Size = new System.Drawing.Size(54, 18);
            this.lblPatientSearch.TabIndex = 206;
            this.lblPatientSearch.Text = "Patient :";
            this.lblPatientSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPayMode
            // 
            this.cmbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMode.ForeColor = System.Drawing.Color.Black;
            this.cmbPayMode.FormattingEnabled = true;
            this.cmbPayMode.Items.AddRange(new object[] {
            ""});
            this.cmbPayMode.Location = new System.Drawing.Point(92, 115);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.Size = new System.Drawing.Size(121, 21);
            this.cmbPayMode.TabIndex = 1;
            this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.AutoSize = true;
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.Location = new System.Drawing.Point(218, 118);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(61, 14);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Check # :";
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPayType
            // 
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(23, 118);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(66, 14);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Pay Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Location = new System.Drawing.Point(282, 115);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(71, 20);
            this.txtCheckNumber.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 325);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(542, 1);
            this.label24.TabIndex = 29;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(546, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 325);
            this.label26.TabIndex = 27;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(3, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 325);
            this.label27.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 1);
            this.label1.TabIndex = 30;
            this.label1.Text = "Close Date :";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(550, 56);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnSave,
            this.tls_btnSaveNClose,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(550, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnSave
            // 
            this.tls_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSave.Image")));
            this.tls_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSave.Name = "tls_btnSave";
            this.tls_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tls_btnSave.Tag = "Save";
            this.tls_btnSave.Text = "&Save";
            this.tls_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSave.ToolTipText = "Save";
            this.tls_btnSave.Visible = false;
            this.tls_btnSave.Click += new System.EventHandler(this.tls_btnSave_Click);
            // 
            // tls_btnSaveNClose
            // 
            this.tls_btnSaveNClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSaveNClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSaveNClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSaveNClose.Image")));
            this.tls_btnSaveNClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSaveNClose.Name = "tls_btnSaveNClose";
            this.tls_btnSaveNClose.Size = new System.Drawing.Size(66, 50);
            this.tls_btnSaveNClose.Tag = "SaveNClose";
            this.tls_btnSaveNClose.Text = "&Save&&Cls";
            this.tls_btnSaveNClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSaveNClose.ToolTipText = "Save and Close";
            this.tls_btnSaveNClose.Click += new System.EventHandler(this.tls_btnSaveNClose_Click);
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnClose.Image")));
            this.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tls_btnClose.Tag = "Close";
            this.tls_btnClose.Text = "&Close";
            this.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.panel6);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 56);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlHeader.Size = new System.Drawing.Size(550, 26);
            this.pnlHeader.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label42);
            this.panel6.Controls.Add(this.btnModifyJournal);
            this.panel6.Controls.Add(this.btnSetupJournal);
            this.panel6.Controls.Add(this.cmbPaymentTray);
            this.panel6.Controls.Add(this.label90);
            this.panel6.Controls.Add(this.mskCloseDate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(544, 23);
            this.panel6.TabIndex = 212;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(542, 1);
            this.label21.TabIndex = 28;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 22);
            this.label23.TabIndex = 26;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(543, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 22);
            this.label22.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(544, 1);
            this.label8.TabIndex = 29;
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(41, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 0;
            this.label42.Text = "Close Date :";
            // 
            // btnModifyJournal
            // 
            this.btnModifyJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyJournal.AutoEllipsis = true;
            this.btnModifyJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnModifyJournal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModifyJournal.BackgroundImage")));
            this.btnModifyJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModifyJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyJournal.Image = ((System.Drawing.Image)(resources.GetObject("btnModifyJournal.Image")));
            this.btnModifyJournal.Location = new System.Drawing.Point(509, 2);
            this.btnModifyJournal.Name = "btnModifyJournal";
            this.btnModifyJournal.Size = new System.Drawing.Size(20, 19);
            this.btnModifyJournal.TabIndex = 5;
            this.btnModifyJournal.TabStop = false;
            this.toolTip1.SetToolTip(this.btnModifyJournal, " Modify Payment Tray");
            this.btnModifyJournal.UseVisualStyleBackColor = false;
            this.btnModifyJournal.Click += new System.EventHandler(this.btnModifyJournal_Click);
            // 
            // btnSetupJournal
            // 
            this.btnSetupJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetupJournal.AutoEllipsis = true;
            this.btnSetupJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnSetupJournal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.BackgroundImage")));
            this.btnSetupJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetupJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupJournal.Image = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.Image")));
            this.btnSetupJournal.Location = new System.Drawing.Point(484, 2);
            this.btnSetupJournal.Name = "btnSetupJournal";
            this.btnSetupJournal.Size = new System.Drawing.Size(20, 19);
            this.btnSetupJournal.TabIndex = 4;
            this.btnSetupJournal.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSetupJournal, "Add Payment Tray");
            this.btnSetupJournal.UseVisualStyleBackColor = false;
            this.btnSetupJournal.Click += new System.EventHandler(this.btnSetupJournal_Click);
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Items.AddRange(new object[] {
            ""});
            this.cmbPaymentTray.Location = new System.Drawing.Point(317, 1);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(160, 21);
            this.cmbPaymentTray.TabIndex = 3;
            this.cmbPaymentTray.TabStop = false;
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(222, 5);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 14);
            this.label90.TabIndex = 2;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCloseDate.Location = new System.Drawing.Point(117, 1);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(69, 20);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            // 
            // frmBillingEOBPatientRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(550, 411);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlToolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBillingEOBPatientRefund";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Refund";
            this.Load += new System.EventHandler(this.frmBillingEOBPatientRefund_Load);
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlCredit.ResumeLayout(false);
            this.pnlCredit.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblCheckAmount;
        private System.Windows.Forms.TextBox txtRefundAmount;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPatientSearch;
        private System.Windows.Forms.ComboBox cmbPayMode;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnSave;
        private System.Windows.Forms.ToolStripButton tls_btnSaveNClose;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Button btnModifyJournal;
        private System.Windows.Forms.Button btnSetupJournal;
        private System.Windows.Forms.ComboBox cmbPaymentTray;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRefundReason;
        private System.Windows.Forms.Panel pnlCredit;
        private System.Windows.Forms.MaskedTextBox mskCreditExpiryDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.TextBox txtCardAuthorizationNo;
        private System.Windows.Forms.Label lblCardAuthorizationNo;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPatientNm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.MaskedTextBox mskCheckDate;
        private System.Windows.Forms.CheckBox chkPayMstIncludeNotes;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}