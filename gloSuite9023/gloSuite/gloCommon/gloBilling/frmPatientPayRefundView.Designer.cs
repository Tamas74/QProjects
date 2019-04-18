namespace gloBilling
{
    partial class frmPatientPayRefundView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientPayRefundView));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_void_refund = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbldatetime = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblusername = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblvoidtray = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.pnlCredit = new System.Windows.Forms.Panel();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.mskCreditExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.txtCardAuthorizationNo = new System.Windows.Forms.TextBox();
            this.lblCardAuthorizationNo = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.txtRefundAmount = new System.Windows.Forms.TextBox();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.lblPayType = new System.Windows.Forms.Label();
            this.cmbPayMode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mskrefunddate = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.mskCheckDate = new System.Windows.Forms.MaskedTextBox();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlCPTGrid = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1Refund = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblvoid = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelectPaymentTray = new System.Windows.Forms.Button();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCredit.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlCPTGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Refund)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(937, 55);
            this.panel2.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_void_refund,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(937, 55);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_void_refund
            // 
            this.tsb_void_refund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_void_refund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_void_refund.Image = ((System.Drawing.Image)(resources.GetObject("tsb_void_refund.Image")));
            this.tsb_void_refund.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_void_refund.Name = "tsb_void_refund";
            this.tsb_void_refund.Size = new System.Drawing.Size(87, 50);
            this.tsb_void_refund.Tag = "OK";
            this.tsb_void_refund.Text = "&Void Refund";
            this.tsb_void_refund.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_void_refund.ToolTipText = "Void Refund";
            this.tsb_void_refund.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
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
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.pnlCPTGrid);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(937, 378);
            this.pnlMain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.pnlCredit);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 182);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel3.Size = new System.Drawing.Size(931, 193);
            this.panel3.TabIndex = 1;
            this.panel3.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbldatetime);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.lblusername);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblvoidtray);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(929, 27);
            this.panel1.TabIndex = 220;
            this.panel1.TabStop = true;
            // 
            // lbldatetime
            // 
            this.lbldatetime.AutoSize = true;
            this.lbldatetime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldatetime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldatetime.Location = new System.Drawing.Point(495, 6);
            this.lbldatetime.Name = "lbldatetime";
            this.lbldatetime.Size = new System.Drawing.Size(0, 14);
            this.lbldatetime.TabIndex = 229;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(420, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 14);
            this.label18.TabIndex = 229;
            this.label18.Text = "Date/Time :";
            // 
            // lblusername
            // 
            this.lblusername.AutoSize = true;
            this.lblusername.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblusername.Location = new System.Drawing.Point(106, 7);
            this.lblusername.Name = "lblusername";
            this.lblusername.Size = new System.Drawing.Size(0, 14);
            this.lblusername.TabIndex = 226;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(30, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 14);
            this.label8.TabIndex = 226;
            this.label8.Text = "User Name :";
            // 
            // lblvoidtray
            // 
            this.lblvoidtray.AutoSize = true;
            this.lblvoidtray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvoidtray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblvoidtray.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblvoidtray.Location = new System.Drawing.Point(494, 7);
            this.lblvoidtray.Name = "lblvoidtray";
            this.lblvoidtray.Size = new System.Drawing.Size(0, 14);
            this.lblvoidtray.TabIndex = 227;
            this.lblvoidtray.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtNotes);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 89);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(929, 76);
            this.panel5.TabIndex = 2;
            this.panel5.TabStop = true;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(106, 3);
            this.txtNotes.MaxLength = 250;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(784, 69);
            this.txtNotes.TabIndex = 0;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(19, 3);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 14);
            this.label31.TabIndex = 32;
            this.label31.Text = "Refund Note :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCredit
            // 
            this.pnlCredit.Controls.Add(this.lblExpiryDate);
            this.pnlCredit.Controls.Add(this.mskCreditExpiryDate);
            this.pnlCredit.Controls.Add(this.lblCardType);
            this.pnlCredit.Controls.Add(this.cmbCardType);
            this.pnlCredit.Controls.Add(this.txtCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.lblCardAuthorizationNo);
            this.pnlCredit.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCredit.Location = new System.Drawing.Point(1, 62);
            this.pnlCredit.Name = "pnlCredit";
            this.pnlCredit.Size = new System.Drawing.Size(929, 27);
            this.pnlCredit.TabIndex = 1;
            this.pnlCredit.TabStop = true;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblExpiryDate.Location = new System.Drawing.Point(729, 7);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(35, 14);
            this.lblExpiryDate.TabIndex = 225;
            this.lblExpiryDate.Text = "Exp :";
            // 
            // mskCreditExpiryDate
            // 
            this.mskCreditExpiryDate.Location = new System.Drawing.Point(766, 3);
            this.mskCreditExpiryDate.Mask = "00/00";
            this.mskCreditExpiryDate.Name = "mskCreditExpiryDate";
            this.mskCreditExpiryDate.Size = new System.Drawing.Size(123, 22);
            this.mskCreditExpiryDate.TabIndex = 2;
            this.mskCreditExpiryDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCreditExpiryDate_MouseClick);
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardType.Location = new System.Drawing.Point(33, 7);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(71, 14);
            this.lblCardType.TabIndex = 226;
            this.lblCardType.Text = "Card Type :";
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.ForeColor = System.Drawing.Color.Black;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(106, 3);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(157, 22);
            this.cmbCardType.TabIndex = 0;
            // 
            // txtCardAuthorizationNo
            // 
            this.txtCardAuthorizationNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardAuthorizationNo.Location = new System.Drawing.Point(494, 3);
            this.txtCardAuthorizationNo.MaxLength = 16;
            this.txtCardAuthorizationNo.Name = "txtCardAuthorizationNo";
            this.txtCardAuthorizationNo.Size = new System.Drawing.Size(123, 22);
            this.txtCardAuthorizationNo.TabIndex = 1;
            // 
            // lblCardAuthorizationNo
            // 
            this.lblCardAuthorizationNo.AutoSize = true;
            this.lblCardAuthorizationNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardAuthorizationNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardAuthorizationNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardAuthorizationNo.Location = new System.Drawing.Point(441, 7);
            this.lblCardAuthorizationNo.Name = "lblCardAuthorizationNo";
            this.lblCardAuthorizationNo.Size = new System.Drawing.Size(51, 14);
            this.lblCardAuthorizationNo.TabIndex = 227;
            this.lblCardAuthorizationNo.Text = "Auth# :";
            this.lblCardAuthorizationNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblCheckAmount);
            this.panel4.Controls.Add(this.txtRefundAmount);
            this.panel4.Controls.Add(this.lblCheckNo);
            this.panel4.Controls.Add(this.txtCheckNumber);
            this.panel4.Controls.Add(this.lblPayType);
            this.panel4.Controls.Add(this.cmbPayMode);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.mskrefunddate);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txtTo);
            this.panel4.Controls.Add(this.mskCheckDate);
            this.panel4.Controls.Add(this.lblCheckDate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(929, 58);
            this.panel4.TabIndex = 0;
            this.panel4.TabStop = true;
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(704, 7);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 18);
            this.lblCheckAmount.TabIndex = 222;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRefundAmount
            // 
            this.txtRefundAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefundAmount.Location = new System.Drawing.Point(766, 7);
            this.txtRefundAmount.Name = "txtRefundAmount";
            this.txtRefundAmount.ReadOnly = true;
            this.txtRefundAmount.ShortcutsEnabled = false;
            this.txtRefundAmount.Size = new System.Drawing.Size(123, 22);
            this.txtRefundAmount.TabIndex = 221;
            this.txtRefundAmount.TabStop = false;
            this.txtRefundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.Location = new System.Drawing.Point(392, 36);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(100, 14);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Check# :";
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Location = new System.Drawing.Point(494, 32);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(123, 22);
            this.txtCheckNumber.TabIndex = 3;
            // 
            // lblPayType
            // 
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(18, 36);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(86, 14);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Refund Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbPayMode
            // 
            this.cmbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMode.ForeColor = System.Drawing.Color.Black;
            this.cmbPayMode.FormattingEnabled = true;
            this.cmbPayMode.Items.AddRange(new object[] {
            ""});
            this.cmbPayMode.Location = new System.Drawing.Point(106, 32);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.Size = new System.Drawing.Size(157, 22);
            this.cmbPayMode.TabIndex = 2;
            this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(408, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Refund Date :";
            // 
            // mskrefunddate
            // 
            this.mskrefunddate.Location = new System.Drawing.Point(494, 6);
            this.mskrefunddate.Mask = "00/00/0000";
            this.mskrefunddate.Name = "mskrefunddate";
            this.mskrefunddate.Size = new System.Drawing.Size(123, 22);
            this.mskrefunddate.TabIndex = 1;
            this.mskrefunddate.ValidatingType = typeof(System.DateTime);
            this.mskrefunddate.Validating += new System.ComponentModel.CancelEventHandler(this.mskrefunddate_Validating);
            this.mskrefunddate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(31, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 209;
            this.label5.Text = "Refund To :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.ForeColor = System.Drawing.Color.Black;
            this.txtTo.Location = new System.Drawing.Point(106, 6);
            this.txtTo.MaxLength = 250;
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(272, 22);
            this.txtTo.TabIndex = 0;
            // 
            // mskCheckDate
            // 
            this.mskCheckDate.Location = new System.Drawing.Point(766, 32);
            this.mskCheckDate.Mask = "00/00/0000";
            this.mskCheckDate.Name = "mskCheckDate";
            this.mskCheckDate.Size = new System.Drawing.Size(123, 22);
            this.mskCheckDate.TabIndex = 4;
            this.mskCheckDate.ValidatingType = typeof(System.DateTime);
            this.mskCheckDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCheckDate_Validating);
            this.mskCheckDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.Location = new System.Drawing.Point(647, 36);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(117, 14);
            this.lblCheckDate.TabIndex = 215;
            this.lblCheckDate.Text = "Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(1, 192);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(929, 1);
            this.label13.TabIndex = 219;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(1, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(929, 1);
            this.label11.TabIndex = 218;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(930, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 190);
            this.label10.TabIndex = 217;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 190);
            this.label7.TabIndex = 216;
            // 
            // pnlCPTGrid
            // 
            this.pnlCPTGrid.Controls.Add(this.label3);
            this.pnlCPTGrid.Controls.Add(this.label2);
            this.pnlCPTGrid.Controls.Add(this.label12);
            this.pnlCPTGrid.Controls.Add(this.label1);
            this.pnlCPTGrid.Controls.Add(this.c1Refund);
            this.pnlCPTGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCPTGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCPTGrid.Location = new System.Drawing.Point(3, 27);
            this.pnlCPTGrid.Name = "pnlCPTGrid";
            this.pnlCPTGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlCPTGrid.Size = new System.Drawing.Size(931, 155);
            this.pnlCPTGrid.TabIndex = 0;
            this.pnlCPTGrid.TabStop = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(929, 1);
            this.label3.TabIndex = 213;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(1, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(929, 1);
            this.label2.TabIndex = 212;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 152);
            this.label12.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(930, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 152);
            this.label1.TabIndex = 211;
            // 
            // c1Refund
            // 
            this.c1Refund.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Refund.AllowEditing = false;
            this.c1Refund.AutoResize = false;
            this.c1Refund.BackColor = System.Drawing.Color.White;
            this.c1Refund.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Refund.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1Refund.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Refund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Refund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Refund.Location = new System.Drawing.Point(0, 3);
            this.c1Refund.Name = "c1Refund";
            this.c1Refund.Padding = new System.Windows.Forms.Padding(3);
            this.c1Refund.Rows.Count = 1;
            this.c1Refund.Rows.DefaultSize = 19;
            this.c1Refund.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Refund.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Refund.ShowCellLabels = true;
            this.c1Refund.Size = new System.Drawing.Size(931, 152);
            this.c1Refund.StyleInfo = resources.GetString("c1Refund.StyleInfo");
            this.c1Refund.TabIndex = 216;
            this.c1Refund.Tag = "ClosePeriod";
            this.c1Refund.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Refund_MouseMove);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.panel7);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(3, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(931, 27);
            this.pnlHeader.TabIndex = 8;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.lblvoid);
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.btnSelectPaymentTray);
            this.panel7.Controls.Add(this.lblPaymentTray);
            this.panel7.Controls.Add(this.label70);
            this.panel7.Controls.Add(this.label71);
            this.panel7.Controls.Add(this.label72);
            this.panel7.Controls.Add(this.mskCloseDate);
            this.panel7.Controls.Add(this.label48);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(0, 3);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(931, 24);
            this.panel7.TabIndex = 213;
            // 
            // lblvoid
            // 
            this.lblvoid.AutoSize = true;
            this.lblvoid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvoid.ForeColor = System.Drawing.Color.Red;
            this.lblvoid.Location = new System.Drawing.Point(17, 5);
            this.lblvoid.Name = "lblvoid";
            this.lblvoid.Size = new System.Drawing.Size(0, 14);
            this.lblvoid.TabIndex = 227;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 22);
            this.label29.TabIndex = 7;
            this.label29.Text = "label4";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(622, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = " Tray :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectPaymentTray
            // 
            this.btnSelectPaymentTray.AutoEllipsis = true;
            this.btnSelectPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectPaymentTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectPaymentTray.BackgroundImage")));
            this.btnSelectPaymentTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectPaymentTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPaymentTray.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPaymentTray.Image")));
            this.btnSelectPaymentTray.Location = new System.Drawing.Point(901, 1);
            this.btnSelectPaymentTray.Name = "btnSelectPaymentTray";
            this.btnSelectPaymentTray.Size = new System.Drawing.Size(22, 22);
            this.btnSelectPaymentTray.TabIndex = 2;
            this.btnSelectPaymentTray.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.C1SuperTooltip1.SetToolTip(this.btnSelectPaymentTray, "Select Payment Tray");
            this.btnSelectPaymentTray.UseVisualStyleBackColor = false;
            this.btnSelectPaymentTray.Click += new System.EventHandler(this.btnSelectPaymentTray_Click);
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(667, 5);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(224, 14);
            this.lblPaymentTray.TabIndex = 208;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(930, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 22);
            this.label70.TabIndex = 6;
            this.label70.Text = "label3";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(0, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(931, 1);
            this.label71.TabIndex = 5;
            this.label71.Text = "label1";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(0, 23);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(931, 1);
            this.label72.TabIndex = 8;
            this.label72.Text = "label2";
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCloseDate.Location = new System.Drawing.Point(540, 1);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(78, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCloseDate_Validating_1);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(461, 5);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(73, 14);
            this.label48.TabIndex = 59;
            this.label48.Text = "Close Date :";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPatientPayRefundView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(937, 433);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientPayRefundView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Payment Refund View";
            this.Load += new System.EventHandler(this.frmPaymentUseReserve_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPaymentUseReserve_FormClosed);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlCredit.ResumeLayout(false);
            this.pnlCredit.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlCPTGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Refund)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCPTGrid;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MaskedTextBox mskrefunddate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.MaskedTextBox mskCheckDate;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbPayMode;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.Panel pnlCredit;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.MaskedTextBox mskCreditExpiryDate;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.TextBox txtCardAuthorizationNo;
        private System.Windows.Forms.Label lblCardAuthorizationNo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblCheckAmount;
        private System.Windows.Forms.TextBox txtRefundAmount;
        internal System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnSelectPaymentTray;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label48;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Refund;
        internal System.Windows.Forms.ToolStripButton tsb_void_refund;
        private System.Windows.Forms.Label lblvoid;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbldatetime;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblusername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblvoidtray;
    }
}