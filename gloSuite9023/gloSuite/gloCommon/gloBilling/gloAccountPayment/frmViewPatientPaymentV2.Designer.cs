namespace gloAccountsV2
{
    partial class frmViewPatientPaymentV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewPatientPaymentV2));
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCheckNum = new System.Windows.Forms.Label();
            this.lblShowCheckDate = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblPmntType = new System.Windows.Forms.Label();
            this.lblAvailableReserve = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblPatientSearch = new System.Windows.Forms.Label();
            this.txtPayMstNotes = new System.Windows.Forms.TextBox();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCheckRemaining = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.lblPayType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnDefaultReceipt = new System.Windows.Forms.ToolStripButton();
            this.tls_btnReceipt = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_VoidPayment = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.ts_VoidCleargagePayment = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lblAlertMessage = new System.Windows.Forms.Label();
            this.pnlSinglePayment = new System.Windows.Forms.Panel();
            this.c1PaymentDetail = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel2.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlSinglePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PaymentDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCheckNum);
            this.panel2.Controls.Add(this.lblShowCheckDate);
            this.panel2.Controls.Add(this.lblPatient);
            this.panel2.Controls.Add(this.lblPmntType);
            this.panel2.Controls.Add(this.lblAvailableReserve);
            this.panel2.Controls.Add(this.lblAmount);
            this.panel2.Controls.Add(this.lblRemaining);
            this.panel2.Controls.Add(this.lblPatientSearch);
            this.panel2.Controls.Add(this.txtPayMstNotes);
            this.panel2.Controls.Add(this.lblCheckAmount);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblCheckRemaining);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.lblCheckNo);
            this.panel2.Controls.Add(this.lblPayType);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblCheckDate);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1034, 119);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = true;
            // 
            // lblCheckNum
            // 
            this.lblCheckNum.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckNum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNum.Location = new System.Drawing.Point(112, 64);
            this.lblCheckNum.Name = "lblCheckNum";
            this.lblCheckNum.Size = new System.Drawing.Size(258, 15);
            this.lblCheckNum.TabIndex = 209;
            this.lblCheckNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblShowCheckDate
            // 
            this.lblShowCheckDate.BackColor = System.Drawing.Color.Transparent;
            this.lblShowCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowCheckDate.Location = new System.Drawing.Point(112, 88);
            this.lblShowCheckDate.Name = "lblShowCheckDate";
            this.lblShowCheckDate.Size = new System.Drawing.Size(258, 15);
            this.lblShowCheckDate.TabIndex = 210;
            this.lblShowCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatient
            // 
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.Location = new System.Drawing.Point(112, 16);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(258, 15);
            this.lblPatient.TabIndex = 207;
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPmntType
            // 
            this.lblPmntType.BackColor = System.Drawing.Color.Transparent;
            this.lblPmntType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPmntType.Location = new System.Drawing.Point(112, 40);
            this.lblPmntType.Name = "lblPmntType";
            this.lblPmntType.Size = new System.Drawing.Size(258, 15);
            this.lblPmntType.TabIndex = 208;
            this.lblPmntType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAvailableReserve
            // 
            this.lblAvailableReserve.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailableReserve.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvailableReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAvailableReserve.Location = new System.Drawing.Point(504, 64);
            this.lblAvailableReserve.Name = "lblAvailableReserve";
            this.lblAvailableReserve.Size = new System.Drawing.Size(144, 15);
            this.lblAvailableReserve.TabIndex = 206;
            this.lblAvailableReserve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmount
            // 
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAmount.Location = new System.Drawing.Point(504, 16);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(144, 15);
            this.lblAmount.TabIndex = 206;
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemaining
            // 
            this.lblRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRemaining.Location = new System.Drawing.Point(504, 40);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(144, 15);
            this.lblRemaining.TabIndex = 206;
            this.lblRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPatientSearch
            // 
            this.lblPatientSearch.AutoEllipsis = true;
            this.lblPatientSearch.AutoSize = true;
            this.lblPatientSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientSearch.Location = new System.Drawing.Point(11, 16);
            this.lblPatientSearch.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblPatientSearch.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblPatientSearch.Name = "lblPatientSearch";
            this.lblPatientSearch.Size = new System.Drawing.Size(100, 15);
            this.lblPatientSearch.TabIndex = 206;
            this.lblPatientSearch.Text = "Patient :";
            this.lblPatientSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPayMstNotes
            // 
            this.txtPayMstNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayMstNotes.ForeColor = System.Drawing.Color.Black;
            this.txtPayMstNotes.Location = new System.Drawing.Point(777, 16);
            this.txtPayMstNotes.MaxLength = 255;
            this.txtPayMstNotes.Multiline = true;
            this.txtPayMstNotes.Name = "txtPayMstNotes";
            this.txtPayMstNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPayMstNotes.Size = new System.Drawing.Size(246, 93);
            this.txtPayMstNotes.TabIndex = 3;
            this.txtPayMstNotes.TabStop = false;
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.AutoEllipsis = true;
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(449, 16);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 14);
            this.lblCheckAmount.TabIndex = 5;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 115);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1026, 1);
            this.label24.TabIndex = 29;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(1030, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 115);
            this.label26.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(395, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Available Reserves :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckRemaining
            // 
            this.lblCheckRemaining.AutoEllipsis = true;
            this.lblCheckRemaining.AutoSize = true;
            this.lblCheckRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckRemaining.Location = new System.Drawing.Point(438, 41);
            this.lblCheckRemaining.Name = "lblCheckRemaining";
            this.lblCheckRemaining.Size = new System.Drawing.Size(70, 14);
            this.lblCheckRemaining.TabIndex = 7;
            this.lblCheckRemaining.Text = "Remaining :";
            this.lblCheckRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(3, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 115);
            this.label27.TabIndex = 26;
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.AutoEllipsis = true;
            this.lblCheckNo.AutoSize = true;
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCheckNo.Location = new System.Drawing.Point(11, 64);
            this.lblCheckNo.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblCheckNo.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(100, 15);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Check# :";
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayType
            // 
            this.lblPayType.AutoEllipsis = true;
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(11, 40);
            this.lblPayType.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblPayType.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(100, 15);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Pay Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1028, 1);
            this.label1.TabIndex = 30;
            this.label1.Text = "Close Date :";
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.AutoEllipsis = true;
            this.lblCheckDate.AutoSize = true;
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCheckDate.Location = new System.Drawing.Point(11, 88);
            this.lblCheckDate.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblCheckDate.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(100, 15);
            this.lblCheckDate.TabIndex = 6;
            this.lblCheckDate.Text = "Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(734, 16);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 3;
            this.label31.Text = "Note :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1034, 53);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnDefaultReceipt,
            this.tls_btnReceipt,
            this.ts_VoidPayment,
            this.ts_VoidCleargagePayment,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1034, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnDefaultReceipt
            // 
            this.tls_btnDefaultReceipt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnDefaultReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnDefaultReceipt.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnDefaultReceipt.Image")));
            this.tls_btnDefaultReceipt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnDefaultReceipt.Name = "tls_btnDefaultReceipt";
            this.tls_btnDefaultReceipt.Size = new System.Drawing.Size(57, 50);
            this.tls_btnDefaultReceipt.Tag = "Receipt";
            this.tls_btnDefaultReceipt.Text = "&Receipt";
            this.tls_btnDefaultReceipt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnDefaultReceipt.Click += new System.EventHandler(this.tls_btnDefaultReceipt_Click);
            // 
            // tls_btnReceipt
            // 
            this.tls_btnReceipt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnReceipt.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnReceipt.Image")));
            this.tls_btnReceipt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnReceipt.Name = "tls_btnReceipt";
            this.tls_btnReceipt.Size = new System.Drawing.Size(72, 50);
            this.tls_btnReceipt.Tag = "Receipt";
            this.tls_btnReceipt.Text = "R&eceipts";
            this.tls_btnReceipt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ts_VoidPayment
            // 
            this.ts_VoidPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_VoidPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_VoidPayment.Image = ((System.Drawing.Image)(resources.GetObject("ts_VoidPayment.Image")));
            this.ts_VoidPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_VoidPayment.Name = "ts_VoidPayment";
            this.ts_VoidPayment.Size = new System.Drawing.Size(96, 50);
            this.ts_VoidPayment.Tag = "Hide";
            this.ts_VoidPayment.Text = "&Void Payment";
            this.ts_VoidPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_VoidPayment.Click += new System.EventHandler(this.ts_VoidPayment_Click);
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
            // ts_VoidCleargagePayment
            // 
            this.ts_VoidCleargagePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_VoidCleargagePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_VoidCleargagePayment.Image = ((System.Drawing.Image)(resources.GetObject("ts_VoidCleargagePayment.Image")));
            this.ts_VoidCleargagePayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_VoidCleargagePayment.Name = "ts_VoidCleargagePayment";
            this.ts_VoidCleargagePayment.Size = new System.Drawing.Size(160, 50);
            this.ts_VoidCleargagePayment.Tag = "Hide";
            this.ts_VoidCleargagePayment.Text = "Void Cleargage &Payment";
            this.ts_VoidCleargagePayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_VoidCleargagePayment.Visible = false;
            this.ts_VoidCleargagePayment.Click += new System.EventHandler(this.ts_VoidCleargagePayment_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1034, 30);
            this.panel1.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblCloseDate);
            this.panel6.Controls.Add(this.lblPaymentTray);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label42);
            this.panel6.Controls.Add(this.label90);
            this.panel6.Controls.Add(this.lblAlertMessage);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1028, 24);
            this.panel6.TabIndex = 212;
            this.panel6.TabStop = true;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDate.Location = new System.Drawing.Point(500, 3);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(119, 17);
            this.lblCloseDate.TabIndex = 209;
            this.lblCloseDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(777, 4);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(246, 14);
            this.lblPaymentTray.TabIndex = 208;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1026, 1);
            this.label21.TabIndex = 28;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 23);
            this.label23.TabIndex = 26;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(1027, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 23);
            this.label22.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1028, 1);
            this.label8.TabIndex = 29;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(432, 4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 0;
            this.label42.Text = "Close Date :";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(682, 4);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 14);
            this.label90.TabIndex = 2;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertMessage
            // 
            this.lblAlertMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMessage.Location = new System.Drawing.Point(3, 3);
            this.lblAlertMessage.Name = "lblAlertMessage";
            this.lblAlertMessage.Size = new System.Drawing.Size(431, 17);
            this.lblAlertMessage.TabIndex = 210;
            this.lblAlertMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSinglePayment
            // 
            this.pnlSinglePayment.Controls.Add(this.c1PaymentDetail);
            this.pnlSinglePayment.Controls.Add(this.label13);
            this.pnlSinglePayment.Controls.Add(this.label14);
            this.pnlSinglePayment.Controls.Add(this.label15);
            this.pnlSinglePayment.Controls.Add(this.label25);
            this.pnlSinglePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSinglePayment.Location = new System.Drawing.Point(0, 202);
            this.pnlSinglePayment.Name = "pnlSinglePayment";
            this.pnlSinglePayment.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSinglePayment.Size = new System.Drawing.Size(1034, 513);
            this.pnlSinglePayment.TabIndex = 0;
            this.pnlSinglePayment.TabStop = true;
            // 
            // c1PaymentDetail
            // 
            this.c1PaymentDetail.AllowEditing = false;
            this.c1PaymentDetail.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PaymentDetail.AutoGenerateColumns = false;
            this.c1PaymentDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.c1PaymentDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1PaymentDetail.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PaymentDetail.ColumnInfo = resources.GetString("c1PaymentDetail.ColumnInfo");
            this.c1PaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PaymentDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PaymentDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PaymentDetail.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PaymentDetail.Location = new System.Drawing.Point(4, 1);
            this.c1PaymentDetail.Name = "c1PaymentDetail";
            this.c1PaymentDetail.Rows.Count = 1;
            this.c1PaymentDetail.Rows.DefaultSize = 19;
            this.c1PaymentDetail.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PaymentDetail.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PaymentDetail.Size = new System.Drawing.Size(1026, 508);
            this.c1PaymentDetail.StyleInfo = resources.GetString("c1PaymentDetail.StyleInfo");
            this.c1PaymentDetail.TabIndex = 2;
            this.c1PaymentDetail.TabStop = false;
            this.c1PaymentDetail.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PaymentDetail_MouseMove);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1026, 1);
            this.label13.TabIndex = 200;
            this.label13.Text = "Close Date :";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(4, 509);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1026, 1);
            this.label14.TabIndex = 1;
            this.label14.Text = "Close Date :";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 510);
            this.label15.TabIndex = 209;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(1030, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 510);
            this.label25.TabIndex = 210;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmViewPatientPaymentV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1034, 715);
            this.Controls.Add(this.pnlSinglePayment);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewPatientPaymentV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Patient Payment";
            this.Load += new System.EventHandler(this.frmViewPatientPayment_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pnlSinglePayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PaymentDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblCheckAmount;
        private System.Windows.Forms.Label lblCheckRemaining;
        private System.Windows.Forms.Label lblPatientSearch;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnDefaultReceipt;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label lblAvailableReserve;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCheckNum;
        private System.Windows.Forms.Label lblShowCheckDate;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblPmntType;
        private System.Windows.Forms.Panel pnlSinglePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PaymentDetail;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.Label lblAlertMessage;
        internal System.Windows.Forms.ToolStripDropDownButton tls_btnReceipt;
        internal System.Windows.Forms.ToolStripButton ts_VoidPayment;
        private System.Windows.Forms.TextBox txtPayMstNotes;
        internal System.Windows.Forms.ToolStripButton ts_VoidCleargagePayment;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}