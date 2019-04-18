namespace gloAccountsV2
{
    partial class frmInsurancePaymentRefundV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsurancePaymentRefundV2));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Generate = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtRefundPatient = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.CmbRefundClaim = new System.Windows.Forms.ComboBox();
            this.lblPayType = new System.Windows.Forms.Label();
            this.btnClearRefundPatient = new System.Windows.Forms.Button();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.btnSearchRefundPatient = new System.Windows.Forms.Button();
            this.cmbPayMode = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRefundAmount = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mskCheckDate = new System.Windows.Forms.MaskedTextBox();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlCredit = new System.Windows.Forms.Panel();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.mskCreditExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.txtCardAuthorizationNo = new System.Windows.Forms.TextBox();
            this.lblCardAuthorizationNo = new System.Windows.Forms.Label();
            this.pnlCPTGrid = new System.Windows.Forms.Panel();
            this.c1Reserve = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.mskrefunddate = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.TxtPatient = new System.Windows.Forms.TextBox();
            this.cmbClaimNo = new System.Windows.Forms.ComboBox();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnSearchCompany = new System.Windows.Forms.Button();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.btnSelectPaymentTray = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlCredit.SuspendLayout();
            this.pnlCPTGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1171, 55);
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
            this.tsb_Generate,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1171, 55);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Generate
            // 
            this.tsb_Generate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Generate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Generate.Image")));
            this.tsb_Generate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Generate.Name = "tsb_Generate";
            this.tsb_Generate.Size = new System.Drawing.Size(66, 50);
            this.tsb_Generate.Tag = "Cancel";
            this.tsb_Generate.Text = "&Generate";
            this.tsb_Generate.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Generate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Generate.ToolTipText = "Generate";
            this.tsb_Generate.Click += new System.EventHandler(this.tsb_Generate_Click);
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
            this.tsb_OK.Text = "Sa&ve&&Cls";
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
            this.pnlMain.Controls.Add(this.panel8);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(1177, 606);
            this.pnlMain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.pnlCredit);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 450);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel3.Size = new System.Drawing.Size(1171, 153);
            this.panel3.TabIndex = 4;
            this.panel3.TabStop = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TxtRefundPatient);
            this.panel4.Controls.Add(this.txtTo);
            this.panel4.Controls.Add(this.CmbRefundClaim);
            this.panel4.Controls.Add(this.lblPayType);
            this.panel4.Controls.Add(this.btnClearRefundPatient);
            this.panel4.Controls.Add(this.lblCheckAmount);
            this.panel4.Controls.Add(this.btnSearchRefundPatient);
            this.panel4.Controls.Add(this.cmbPayMode);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.txtRefundAmount);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.lblCheckNo);
            this.panel4.Controls.Add(this.txtNotes);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Controls.Add(this.txtCheckNumber);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.mskCheckDate);
            this.panel4.Controls.Add(this.lblCheckDate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1169, 148);
            this.panel4.TabIndex = 0;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // TxtRefundPatient
            // 
            this.TxtRefundPatient.BackColor = System.Drawing.Color.White;
            this.TxtRefundPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRefundPatient.ForeColor = System.Drawing.Color.Black;
            this.TxtRefundPatient.Location = new System.Drawing.Point(148, 115);
            this.TxtRefundPatient.Name = "TxtRefundPatient";
            this.TxtRefundPatient.ReadOnly = true;
            this.TxtRefundPatient.ShortcutsEnabled = false;
            this.TxtRefundPatient.Size = new System.Drawing.Size(385, 22);
            this.TxtRefundPatient.TabIndex = 244;
            this.TxtRefundPatient.TabStop = false;
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.ForeColor = System.Drawing.Color.Black;
            this.txtTo.Location = new System.Drawing.Point(148, 9);
            this.txtTo.MaxLength = 250;
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(385, 22);
            this.txtTo.TabIndex = 0;
            // 
            // CmbRefundClaim
            // 
            this.CmbRefundClaim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.CmbRefundClaim.FormattingEnabled = true;
            this.CmbRefundClaim.Location = new System.Drawing.Point(692, 115);
            this.CmbRefundClaim.MaxLength = 15;
            this.CmbRefundClaim.Name = "CmbRefundClaim";
            this.CmbRefundClaim.Size = new System.Drawing.Size(140, 22);
            this.CmbRefundClaim.TabIndex = 7;
            this.CmbRefundClaim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbRefundClaim_KeyPress);
            this.CmbRefundClaim.Leave += new System.EventHandler(this.CmbRefundClaim_Leave);
            // 
            // lblPayType
            // 
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(855, 39);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(86, 14);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Refund Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPayType.Visible = false;
            // 
            // btnClearRefundPatient
            // 
            this.btnClearRefundPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearRefundPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearRefundPatient.BackgroundImage")));
            this.btnClearRefundPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearRefundPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearRefundPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearRefundPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearRefundPatient.Image")));
            this.btnClearRefundPatient.Location = new System.Drawing.Point(566, 115);
            this.btnClearRefundPatient.Name = "btnClearRefundPatient";
            this.btnClearRefundPatient.Size = new System.Drawing.Size(21, 21);
            this.btnClearRefundPatient.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnClearRefundPatient, "Clear Patient");
            this.btnClearRefundPatient.UseVisualStyleBackColor = false;
            this.btnClearRefundPatient.Click += new System.EventHandler(this.btnClearRefundPatient_Click);
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(630, 11);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 18);
            this.lblCheckAmount.TabIndex = 222;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearchRefundPatient
            // 
            this.btnSearchRefundPatient.AutoEllipsis = true;
            this.btnSearchRefundPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchRefundPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchRefundPatient.BackgroundImage")));
            this.btnSearchRefundPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchRefundPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchRefundPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchRefundPatient.Image")));
            this.btnSearchRefundPatient.Location = new System.Drawing.Point(540, 115);
            this.btnSearchRefundPatient.Name = "btnSearchRefundPatient";
            this.btnSearchRefundPatient.Size = new System.Drawing.Size(21, 21);
            this.btnSearchRefundPatient.TabIndex = 5;
            this.btnSearchRefundPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchRefundPatient, "Select Patient");
            this.btnSearchRefundPatient.UseVisualStyleBackColor = false;
            this.btnSearchRefundPatient.Click += new System.EventHandler(this.btnSearchRefundPatient_Click);
            // 
            // cmbPayMode
            // 
            this.cmbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMode.ForeColor = System.Drawing.Color.Black;
            this.cmbPayMode.FormattingEnabled = true;
            this.cmbPayMode.Items.AddRange(new object[] {
            ""});
            this.cmbPayMode.Location = new System.Drawing.Point(944, 35);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.Size = new System.Drawing.Size(197, 22);
            this.cmbPayMode.TabIndex = 3;
            this.cmbPayMode.Visible = false;
            this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoEllipsis = true;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(634, 119);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 14);
            this.label17.TabIndex = 236;
            this.label17.Text = "Claim # :";
            // 
            // txtRefundAmount
            // 
            this.txtRefundAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefundAmount.Location = new System.Drawing.Point(692, 9);
            this.txtRefundAmount.Name = "txtRefundAmount";
            this.txtRefundAmount.ReadOnly = true;
            this.txtRefundAmount.ShortcutsEnabled = false;
            this.txtRefundAmount.Size = new System.Drawing.Size(140, 22);
            this.txtRefundAmount.TabIndex = 221;
            this.txtRefundAmount.TabStop = false;
            this.txtRefundAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(91, 119);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 14);
            this.label18.TabIndex = 235;
            this.label18.Text = "Patient :";
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.Location = new System.Drawing.Point(38, 39);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(107, 14);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Refund Check # : ";
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(148, 61);
            this.txtNotes.MaxLength = 250;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(993, 50);
            this.txtNotes.TabIndex = 4;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(60, 61);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 14);
            this.label31.TabIndex = 32;
            this.label31.Text = "Refund Note :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Location = new System.Drawing.Point(148, 35);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(257, 22);
            this.txtCheckNumber.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(72, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 209;
            this.label5.Text = "Refund To :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mskCheckDate
            // 
            this.mskCheckDate.Location = new System.Drawing.Point(692, 35);
            this.mskCheckDate.Mask = "00/00/0000";
            this.mskCheckDate.Name = "mskCheckDate";
            this.mskCheckDate.Size = new System.Drawing.Size(140, 22);
            this.mskCheckDate.TabIndex = 2;
            this.mskCheckDate.ValidatingType = typeof(System.DateTime);
            this.mskCheckDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            this.mskCheckDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCheckDate_Validating);
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.Location = new System.Drawing.Point(566, 39);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(123, 14);
            this.lblCheckDate.TabIndex = 215;
            this.lblCheckDate.Text = "Refund Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(1, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1169, 1);
            this.label13.TabIndex = 219;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(1, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1169, 1);
            this.label11.TabIndex = 218;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(1170, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 150);
            this.label10.TabIndex = 217;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 150);
            this.label7.TabIndex = 216;
            // 
            // pnlCredit
            // 
            this.pnlCredit.Controls.Add(this.lblExpiryDate);
            this.pnlCredit.Controls.Add(this.mskCreditExpiryDate);
            this.pnlCredit.Controls.Add(this.lblCardType);
            this.pnlCredit.Controls.Add(this.cmbCardType);
            this.pnlCredit.Controls.Add(this.txtCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.lblCardAuthorizationNo);
            this.pnlCredit.Location = new System.Drawing.Point(264, 89);
            this.pnlCredit.Name = "pnlCredit";
            this.pnlCredit.Size = new System.Drawing.Size(929, 27);
            this.pnlCredit.TabIndex = 1;
            this.pnlCredit.Visible = false;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblExpiryDate.Location = new System.Drawing.Point(730, 6);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(35, 14);
            this.lblExpiryDate.TabIndex = 225;
            this.lblExpiryDate.Text = "Exp :";
            // 
            // mskCreditExpiryDate
            // 
            this.mskCreditExpiryDate.Location = new System.Drawing.Point(269, 3);
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
            this.txtCardAuthorizationNo.Location = new System.Drawing.Point(504, 3);
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
            this.lblCardAuthorizationNo.Location = new System.Drawing.Point(447, 7);
            this.lblCardAuthorizationNo.Name = "lblCardAuthorizationNo";
            this.lblCardAuthorizationNo.Size = new System.Drawing.Size(51, 14);
            this.lblCardAuthorizationNo.TabIndex = 227;
            this.lblCardAuthorizationNo.Text = "Auth# :";
            this.lblCardAuthorizationNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCPTGrid
            // 
            this.pnlCPTGrid.Controls.Add(this.c1Reserve);
            this.pnlCPTGrid.Controls.Add(this.panel1);
            this.pnlCPTGrid.Controls.Add(this.label3);
            this.pnlCPTGrid.Controls.Add(this.label2);
            this.pnlCPTGrid.Controls.Add(this.label12);
            this.pnlCPTGrid.Controls.Add(this.label1);
            this.pnlCPTGrid.Controls.Add(this.panel5);
            this.pnlCPTGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCPTGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCPTGrid.Location = new System.Drawing.Point(3, 153);
            this.pnlCPTGrid.Name = "pnlCPTGrid";
            this.pnlCPTGrid.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlCPTGrid.Size = new System.Drawing.Size(1171, 297);
            this.pnlCPTGrid.TabIndex = 3;
            // 
            // c1Reserve
            // 
            this.c1Reserve.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Reserve.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Reserve.AutoGenerateColumns = false;
            this.c1Reserve.BackColor = System.Drawing.Color.White;
            this.c1Reserve.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Reserve.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1Reserve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Reserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Reserve.Location = new System.Drawing.Point(1, 28);
            this.c1Reserve.Name = "c1Reserve";
            this.c1Reserve.Rows.Count = 1;
            this.c1Reserve.Rows.DefaultSize = 21;
            this.c1Reserve.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Reserve.ShowCellLabels = true;
            this.c1Reserve.Size = new System.Drawing.Size(1169, 268);
            this.c1Reserve.StyleInfo = resources.GetString("c1Reserve.StyleInfo");
            this.c1Reserve.TabIndex = 0;
            this.c1Reserve.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1Reserve_KeyPressEdit);
            this.c1Reserve.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Reserve_CellChanged);
            this.c1Reserve.Click += new System.EventHandler(this.c1Reserve_Click);
            this.c1Reserve.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Reserve_KeyUp);
            this.c1Reserve.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Reserve_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1169, 24);
            this.panel1.TabIndex = 214;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(4);
            this.label14.Size = new System.Drawing.Size(194, 22);
            this.label14.TabIndex = 227;
            this.label14.Text = "Available Reserves to Refund";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1169, 1);
            this.label8.TabIndex = 214;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1169, 1);
            this.label3.TabIndex = 213;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(1, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1169, 1);
            this.label2.TabIndex = 212;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 294);
            this.label12.TabIndex = 210;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1170, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 294);
            this.label1.TabIndex = 211;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.mskrefunddate);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(197, 158);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1169, 84);
            this.panel5.TabIndex = 2;
            this.panel5.TabStop = true;
            // 
            // mskrefunddate
            // 
            this.mskrefunddate.Location = new System.Drawing.Point(494, -77);
            this.mskrefunddate.Mask = "00/00/0000";
            this.mskrefunddate.Name = "mskrefunddate";
            this.mskrefunddate.Size = new System.Drawing.Size(123, 22);
            this.mskrefunddate.TabIndex = 1;
            this.mskrefunddate.ValidatingType = typeof(System.DateTime);
            this.mskrefunddate.Visible = false;
            this.mskrefunddate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            this.mskrefunddate.Validating += new System.ComponentModel.CancelEventHandler(this.mskrefunddate_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(408, -72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "Refund Date :";
            this.label6.Visible = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.panel7);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(3, 88);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(1171, 65);
            this.pnlHeader.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.TxtPatient);
            this.panel7.Controls.Add(this.cmbClaimNo);
            this.panel7.Controls.Add(this.btnClearPatient);
            this.panel7.Controls.Add(this.btnSearchPatient);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.btnClearInsurance);
            this.panel7.Controls.Add(this.btnSearchCompany);
            this.panel7.Controls.Add(this.lblInsCompany);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.label70);
            this.panel7.Controls.Add(this.label71);
            this.panel7.Controls.Add(this.label72);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(0, 3);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1171, 62);
            this.panel7.TabIndex = 0;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // TxtPatient
            // 
            this.TxtPatient.BackColor = System.Drawing.Color.White;
            this.TxtPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPatient.ForeColor = System.Drawing.Color.Black;
            this.TxtPatient.Location = new System.Drawing.Point(148, 32);
            this.TxtPatient.Name = "TxtPatient";
            this.TxtPatient.ReadOnly = true;
            this.TxtPatient.ShortcutsEnabled = false;
            this.TxtPatient.Size = new System.Drawing.Size(386, 22);
            this.TxtPatient.TabIndex = 235;
            this.TxtPatient.TabStop = false;
            // 
            // cmbClaimNo
            // 
            this.cmbClaimNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbClaimNo.FormattingEnabled = true;
            this.cmbClaimNo.Location = new System.Drawing.Point(692, 32);
            this.cmbClaimNo.MaxLength = 15;
            this.cmbClaimNo.Name = "cmbClaimNo";
            this.cmbClaimNo.Size = new System.Drawing.Size(140, 22);
            this.cmbClaimNo.TabIndex = 6;
            this.cmbClaimNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbClaimNo_KeyPress);
            this.cmbClaimNo.Leave += new System.EventHandler(this.cmbClaimNo_Leave);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(566, 32);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(21, 21);
            this.btnClearPatient.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnClearPatient, "Clear Patient");
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.AutoEllipsis = true;
            this.btnSearchPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.BackgroundImage")));
            this.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.Image")));
            this.btnSearchPatient.Location = new System.Drawing.Point(540, 32);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(21, 21);
            this.btnSearchPatient.TabIndex = 4;
            this.btnSearchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchPatient, "Select Patient");
            this.btnSearchPatient.UseVisualStyleBackColor = false;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // label16
            // 
            this.label16.AutoEllipsis = true;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(630, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 14);
            this.label16.TabIndex = 230;
            this.label16.Text = "Claim # : ";
            // 
            // label15
            // 
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(88, 36);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 14);
            this.label15.TabIndex = 229;
            this.label15.Text = "Patient : ";
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(566, 7);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(21, 21);
            this.btnClearInsurance.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnClearInsurance, "Clear Insurance Company");
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            // 
            // btnSearchCompany
            // 
            this.btnSearchCompany.AutoEllipsis = true;
            this.btnSearchCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchCompany.BackgroundImage")));
            this.btnSearchCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCompany.Image")));
            this.btnSearchCompany.Location = new System.Drawing.Point(540, 7);
            this.btnSearchCompany.Name = "btnSearchCompany";
            this.btnSearchCompany.Size = new System.Drawing.Size(21, 21);
            this.btnSearchCompany.TabIndex = 0;
            this.btnSearchCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchCompany, "Select Insurance Company");
            this.btnSearchCompany.UseVisualStyleBackColor = false;
            this.btnSearchCompany.Click += new System.EventHandler(this.btnSearchCompany_Click);
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(148, 7);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(386, 19);
            this.lblInsCompany.TabIndex = 227;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInsCompany.MouseEnter += new System.EventHandler(this.lblInsCompany_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(19, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 14);
            this.label4.TabIndex = 226;
            this.label4.Text = "Insurance Company : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 60);
            this.label29.TabIndex = 7;
            this.label29.Text = "label4";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Right;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label70.Location = new System.Drawing.Point(1170, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(1, 60);
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
            this.label71.Size = new System.Drawing.Size(1171, 1);
            this.label71.TabIndex = 5;
            this.label71.Text = "label1";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(0, 61);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(1171, 1);
            this.label72.TabIndex = 8;
            this.label72.Text = "label2";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 55);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel8.Size = new System.Drawing.Size(1171, 33);
            this.panel8.TabIndex = 216;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label48);
            this.panel6.Controls.Add(this.mskCloseDate);
            this.panel6.Controls.Add(this.lblPaymentTray);
            this.panel6.Controls.Add(this.btnSelectPaymentTray);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1171, 30);
            this.panel6.TabIndex = 215;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1170, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 28);
            this.label22.TabIndex = 9;
            this.label22.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(0, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 28);
            this.label21.TabIndex = 8;
            this.label21.Text = "label4";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1171, 1);
            this.label20.TabIndex = 7;
            this.label20.Text = "label1";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1171, 1);
            this.label19.TabIndex = 6;
            this.label19.Text = "label1";
            // 
            // label48
            // 
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(613, 8);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(77, 14);
            this.label48.TabIndex = 59;
            this.label48.Text = "Close Date : ";
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskCloseDate.Location = new System.Drawing.Point(690, 4);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(143, 22);
            this.mskCloseDate.TabIndex = 2;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.masktext_click);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCloseDate_Validating_1);
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(907, 4);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(224, 23);
            this.lblPaymentTray.TabIndex = 208;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPaymentTray.MouseEnter += new System.EventHandler(this.lblPaymentTray_MouseEnter);
            // 
            // btnSelectPaymentTray
            // 
            this.btnSelectPaymentTray.AutoEllipsis = true;
            this.btnSelectPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectPaymentTray.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectPaymentTray.BackgroundImage")));
            this.btnSelectPaymentTray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectPaymentTray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPaymentTray.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPaymentTray.Image")));
            this.btnSelectPaymentTray.Location = new System.Drawing.Point(1142, 4);
            this.btnSelectPaymentTray.Name = "btnSelectPaymentTray";
            this.btnSelectPaymentTray.Size = new System.Drawing.Size(22, 22);
            this.btnSelectPaymentTray.TabIndex = 3;
            this.btnSelectPaymentTray.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.C1SuperTooltip1.SetToolTip(this.btnSelectPaymentTray, "Select Payment Tray");
            this.btnSelectPaymentTray.UseVisualStyleBackColor = false;
            this.btnSelectPaymentTray.Click += new System.EventHandler(this.btnSelectPaymentTray_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(862, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = " Tray :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmInsurancePaymentRefundV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1177, 606);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsurancePaymentRefundV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Refund";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPaymentUseReserve_FormClosed);
            this.Load += new System.EventHandler(this.frmPaymentUseReserve_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlCredit.ResumeLayout(false);
            this.pnlCredit.PerformLayout();
            this.pnlCPTGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Reserve)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlCPTGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Reserve;
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
        private System.Windows.Forms.Button btnClearInsurance;
        private System.Windows.Forms.Button btnSearchCompany;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MaskedTextBox mskrefunddate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnClearRefundPatient;
        private System.Windows.Forms.Button btnSearchRefundPatient;
        private System.Windows.Forms.ComboBox cmbClaimNo;
        internal System.Windows.Forms.ToolStripButton tsb_Generate;
        private System.Windows.Forms.ComboBox CmbRefundClaim;
        private System.Windows.Forms.TextBox TxtPatient;
        private System.Windows.Forms.TextBox TxtRefundPatient;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel8;
    }
}