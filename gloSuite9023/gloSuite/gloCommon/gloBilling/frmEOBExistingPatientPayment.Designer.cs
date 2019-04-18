namespace gloBilling
{
    partial class frmEOBExistingPatientPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEOBExistingPatientPayment));
            this.lblChkNo = new System.Windows.Forms.Label();
            this.lblChkDate = new System.Windows.Forms.Label();
            this.lblTray = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.mskChkDate = new System.Windows.Forms.MaskedTextBox();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.c1Payment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.chkShowOnlyPayments = new System.Windows.Forms.CheckBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.lblPayNo = new System.Windows.Forms.Label();
            this.txtPaymentNo = new System.Windows.Forms.TextBox();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c1PendingCheck = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1Payment)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PendingCheck)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChkNo
            // 
            this.lblChkNo.AutoSize = true;
            this.lblChkNo.Location = new System.Drawing.Point(22, 197);
            this.lblChkNo.Name = "lblChkNo";
            this.lblChkNo.Size = new System.Drawing.Size(61, 14);
            this.lblChkNo.TabIndex = 4;
            this.lblChkNo.Text = "Check # :";
            // 
            // lblChkDate
            // 
            this.lblChkDate.AutoSize = true;
            this.lblChkDate.Location = new System.Drawing.Point(381, 196);
            this.lblChkDate.Name = "lblChkDate";
            this.lblChkDate.Size = new System.Drawing.Size(65, 14);
            this.lblChkDate.TabIndex = 5;
            this.lblChkDate.Text = "Chk Date :";
            // 
            // lblTray
            // 
            this.lblTray.AutoSize = true;
            this.lblTray.Location = new System.Drawing.Point(196, 196);
            this.lblTray.Name = "lblTray";
            this.lblTray.Size = new System.Drawing.Size(39, 14);
            this.lblTray.TabIndex = 6;
            this.lblTray.Text = "Tray :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(546, 201);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 7;
            this.lblUser.Text = "User :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(4, 1);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(8, 5, 0, 0);
            this.label9.Size = new System.Drawing.Size(192, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = "Search Patient Payments By:";
            // 
            // mskChkDate
            // 
            this.mskChkDate.Location = new System.Drawing.Point(452, 193);
            this.mskChkDate.Mask = "00/00/0000";
            this.mskChkDate.Name = "mskChkDate";
            this.mskChkDate.Size = new System.Drawing.Size(79, 22);
            this.mskChkDate.TabIndex = 209;
            this.mskChkDate.ValidatingType = typeof(System.DateTime);
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Location = new System.Drawing.Point(85, 193);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(111, 22);
            this.txtCheckNumber.TabIndex = 210;
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Items.AddRange(new object[] {
            ""});
            this.cmbPaymentTray.Location = new System.Drawing.Point(241, 193);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(134, 22);
            this.cmbPaymentTray.TabIndex = 212;
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(594, 196);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(135, 22);
            this.txtuser.TabIndex = 213;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(547, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(22, 22);
            this.btnSearch.TabIndex = 215;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // c1Payment
            // 
            this.c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Payment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.c1Payment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Payment.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Payment.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Payment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Payment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Payment.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Payment.Location = new System.Drawing.Point(3, 1);
            this.c1Payment.Name = "c1Payment";
            this.c1Payment.Rows.Count = 1;
            this.c1Payment.Rows.DefaultSize = 19;
            this.c1Payment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Payment.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Payment.Size = new System.Drawing.Size(649, 316);
            this.c1Payment.StyleInfo = resources.GetString("c1Payment.StyleInfo");
            this.c1Payment.TabIndex = 216;
            this.c1Payment.TabStop = false;
            this.c1Payment.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.chkShowOnlyPayments);
            this.panel1.Controls.Add(this.cmbPaymentTray);
            this.panel1.Controls.Add(this.txtuser);
            this.panel1.Controls.Add(this.lblPatient);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCloseDate);
            this.panel1.Controls.Add(this.txtCheckNumber);
            this.panel1.Controls.Add(this.lblChkNo);
            this.panel1.Controls.Add(this.mskChkDate);
            this.panel1.Controls.Add(this.lblChkDate);
            this.panel1.Controls.Add(this.lblTray);
            this.panel1.Controls.Add(this.maskedTextBox1);
            this.panel1.Controls.Add(this.mskCloseDate);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(655, 95);
            this.panel1.TabIndex = 217;
            this.panel1.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(95, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(446, 20);
            this.label3.TabIndex = 216;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 90);
            this.label10.TabIndex = 33;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(651, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 90);
            this.label26.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(649, 1);
            this.label4.TabIndex = 31;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(3, 91);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(649, 1);
            this.label24.TabIndex = 30;
            // 
            // chkShowOnlyPayments
            // 
            this.chkShowOnlyPayments.AutoSize = true;
            this.chkShowOnlyPayments.Location = new System.Drawing.Point(549, 172);
            this.chkShowOnlyPayments.Name = "chkShowOnlyPayments";
            this.chkShowOnlyPayments.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowOnlyPayments.Size = new System.Drawing.Size(180, 18);
            this.chkShowOnlyPayments.TabIndex = 214;
            this.chkShowOnlyPayments.Text = "Show only open payments :";
            this.chkShowOnlyPayments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkShowOnlyPayments.UseVisualStyleBackColor = true;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(39, 32);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 1;
            this.lblPatient.Text = "Patient :";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Close Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start";
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.Location = new System.Drawing.Point(220, 64);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(28, 14);
            this.lblCloseDate.TabIndex = 2;
            this.lblCloseDate.Text = "End";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(132, 59);
            this.maskedTextBox1.Mask = "00/00/0000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(79, 22);
            this.maskedTextBox1.TabIndex = 10;
            this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Location = new System.Drawing.Point(254, 60);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(79, 22);
            this.mskCloseDate.TabIndex = 10;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            // 
            // lblPayNo
            // 
            this.lblPayNo.AutoSize = true;
            this.lblPayNo.Location = new System.Drawing.Point(222, 50);
            this.lblPayNo.Name = "lblPayNo";
            this.lblPayNo.Size = new System.Drawing.Size(76, 14);
            this.lblPayNo.TabIndex = 0;
            this.lblPayNo.Text = "Payment # :";
            // 
            // txtPaymentNo
            // 
            this.txtPaymentNo.Location = new System.Drawing.Point(301, 46);
            this.txtPaymentNo.Name = "txtPaymentNo";
            this.txtPaymentNo.Size = new System.Drawing.Size(116, 22);
            this.txtPaymentNo.TabIndex = 211;
            // 
            // txtPatient
            // 
            this.txtPatient.Location = new System.Drawing.Point(122, 67);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(335, 22);
            this.txtPatient.TabIndex = 208;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c1PendingCheck);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.c1Payment);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtPaymentNo);
            this.panel2.Controls.Add(this.lblPayNo);
            this.panel2.Controls.Add(this.txtPatient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 151);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(655, 321);
            this.panel2.TabIndex = 217;
            // 
            // c1PendingCheck
            // 
            this.c1PendingCheck.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PendingCheck.AllowEditing = false;
            this.c1PendingCheck.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PendingCheck.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PendingCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PendingCheck.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PendingCheck.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1PendingCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PendingCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PendingCheck.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PendingCheck.Location = new System.Drawing.Point(4, 1);
            this.c1PendingCheck.Name = "c1PendingCheck";
            this.c1PendingCheck.Rows.Count = 1;
            this.c1PendingCheck.Rows.DefaultSize = 19;
            this.c1PendingCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PendingCheck.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PendingCheck.Size = new System.Drawing.Size(647, 316);
            this.c1PendingCheck.StyleInfo = resources.GetString("c1PendingCheck.StyleInfo");
            this.c1PendingCheck.TabIndex = 217;
            this.c1PendingCheck.Click += new System.EventHandler(this.c1PendingCheck_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 316);
            this.label11.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(651, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 316);
            this.label12.TabIndex = 32;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(649, 1);
            this.label13.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(3, 317);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(649, 1);
            this.label14.TabIndex = 30;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(655, 56);
            this.pnlToolStrip.TabIndex = 218;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.tls_btnSave,
            this.toolStripButton3,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(655, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(121, 50);
            this.toolStripButton1.Tag = "Save";
            this.toolStripButton1.Text = "&Select for Posting";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Save";
            this.toolStripButton1.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(38, 50);
            this.toolStripButton2.Tag = "Void";
            this.toolStripButton2.Text = "&Void";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
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
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(91, 50);
            this.toolStripButton3.Tag = "PrintReceipt";
            this.toolStripButton3.Text = "&Print Receipt";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Visible = false;
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
            // frmEOBExistingPatientPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(655, 472);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEOBExistingPatientPayment";
            this.ShowInTaskbar = false;
            this.Text = "Existing Payment";
            this.Load += new System.EventHandler(this.frmEOBExistingPatientPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Payment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PendingCheck)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblChkNo;
        private System.Windows.Forms.Label lblChkDate;
        private System.Windows.Forms.Label lblTray;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox mskChkDate;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.ComboBox cmbPaymentTray;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.Button btnSearch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Payment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.ToolStrip tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnSave;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.CheckBox chkShowOnlyPayments;
        private System.Windows.Forms.Label lblPayNo;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.TextBox txtPaymentNo;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PendingCheck;
    }
}