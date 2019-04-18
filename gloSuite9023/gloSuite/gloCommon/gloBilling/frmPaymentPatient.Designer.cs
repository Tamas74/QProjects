namespace gloBilling
{
    partial class frmPaymentPatient
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
                c1ModifyPaymentTempGrid.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentPatient));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnNew = new System.Windows.Forms.ToolStripButton();
            this.tls_btnNewCorrection = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCharge = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tls_btnDefaultReceipt = new System.Windows.Forms.ToolStripButton();
            this.tls_btnReceipt = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_ShowHideZeroBalance = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSaveNClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnModifyJournal = new System.Windows.Forms.Button();
            this.btnSetupJournal = new System.Windows.Forms.Button();
            this.label90 = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.rbPaySource_Insurance = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.rbPayType_Payment = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTraySelection = new System.Windows.Forms.Button();
            this.rbPayType_Refund = new System.Windows.Forms.RadioButton();
            this.rbPayType_ExitingPayment = new System.Windows.Forms.RadioButton();
            this.label39 = new System.Windows.Forms.Label();
            this.rbPaySource_Personal = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.lblPayer = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.mskCheckDate = new System.Windows.Forms.MaskedTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPaymetNo = new System.Windows.Forms.Label();
            this.chkPayMstIncludeNotes = new System.Windows.Forms.CheckBox();
            this.txtPayMstNotes = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblShowRemaining = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbRecType_Other = new System.Windows.Forms.RadioButton();
            this.rbRecType_Copay = new System.Windows.Forms.RadioButton();
            this.rbRecType_Advance = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDistubuteAmount = new System.Windows.Forms.Button();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.txtCheckRemaining = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCheckRemaining = new System.Windows.Forms.Label();
            this.txtCheckAmount = new System.Windows.Forms.TextBox();
            this.btnReserveRemaining = new System.Windows.Forms.Button();
            this.lblPayerDisplay = new System.Windows.Forms.Label();
            this.btnUseReserve = new System.Windows.Forms.Button();
            this.btnClearReserve = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlCredit = new System.Windows.Forms.Panel();
            this.mskCreditExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.txtCardAuthorizationNo = new System.Windows.Forms.TextBox();
            this.lblCardAuthorizationNo = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.lblPatientSearch = new System.Windows.Forms.Label();
            this.txtPatientSearch = new System.Windows.Forms.TextBox();
            this.cmbPayMode = new System.Windows.Forms.ComboBox();
            this.lblPayType = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pnlPatientStrip = new System.Windows.Forms.Panel();
            this.pnlSinglePayment = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.c1SinglePayment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.c1SinglePaymentTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label12 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlMultiplePayment = new System.Windows.Forms.Panel();
            this.c1MultiplePayment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.c1MultiplePaymentTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.pnlShortcut = new System.Windows.Forms.Panel();
            this.pnlTransactionOther2 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_SavenClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_PaymentGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_DistributePayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_NextServiceLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_PrvServiceLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tooltip_Billing = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlCredit.SuspendLayout();
            this.pnlPatientStrip.SuspendLayout();
            this.pnlSinglePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).BeginInit();
            this.pnlMultiplePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePaymentTotal)).BeginInit();
            this.pnlShortcut.SuspendLayout();
            this.pnlTransactionOther2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1185, 53);
            this.pnlToolStrip.TabIndex = 5;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnNew,
            this.tls_btnNewCorrection,
            this.tls_btnCharge,
            this.toolStripButton2,
            this.tls_btnDefaultReceipt,
            this.tls_btnReceipt,
            this.tsb_ShowHideZeroBalance,
            this.tls_btnSave,
            this.tls_btnSaveNClose,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1185, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            this.tls_Top.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Top_ItemClicked);
            // 
            // tls_btnNew
            // 
            this.tls_btnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnNew.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnNew.Image")));
            this.tls_btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnNew.Name = "tls_btnNew";
            this.tls_btnNew.Size = new System.Drawing.Size(99, 50);
            this.tls_btnNew.Tag = "New";
            this.tls_btnNew.Text = " &New Payment";
            this.tls_btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnNew.Click += new System.EventHandler(this.tls_btnNew_Click);
            // 
            // tls_btnNewCorrection
            // 
            this.tls_btnNewCorrection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnNewCorrection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnNewCorrection.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnNewCorrection.Image")));
            this.tls_btnNewCorrection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnNewCorrection.Name = "tls_btnNewCorrection";
            this.tls_btnNewCorrection.Size = new System.Drawing.Size(109, 50);
            this.tls_btnNewCorrection.Tag = "New";
            this.tls_btnNewCorrection.Text = " Ne&w Correction";
            this.tls_btnNewCorrection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnNewCorrection.Click += new System.EventHandler(this.tls_btnNewCorrection_Click);
            // 
            // tls_btnCharge
            // 
            this.tls_btnCharge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCharge.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCharge.Image")));
            this.tls_btnCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCharge.Name = "tls_btnCharge";
            this.tls_btnCharge.Size = new System.Drawing.Size(106, 50);
            this.tls_btnCharge.Tag = "OpenCharge";
            this.tls_btnCharge.Text = "&Modify Charges";
            this.tls_btnCharge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCharge.ToolTipText = "Modify Charges";
            this.tls_btnCharge.Click += new System.EventHandler(this.tls_btnCharge_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(53, 50);
            this.toolStripButton2.Tag = "New";
            this.toolStripButton2.Text = "Le&dger";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.ToolTipText = "Ledger";
            this.toolStripButton2.Visible = false;
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
            // tsb_ShowHideZeroBalance
            // 
            this.tsb_ShowHideZeroBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowHideZeroBalance.Image = global::gloBilling.Properties.Resources.Hide_Zero_Balance;
            this.tsb_ShowHideZeroBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHideZeroBalance.Name = "tsb_ShowHideZeroBalance";
            this.tsb_ShowHideZeroBalance.Size = new System.Drawing.Size(119, 50);
            this.tsb_ShowHideZeroBalance.Tag = "Hide";
            this.tsb_ShowHideZeroBalance.Text = "Hide Zero &Balance";
            this.tsb_ShowHideZeroBalance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowHideZeroBalance.Click += new System.EventHandler(this.tsb_ShowHideZeroBalance_Click);
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
            this.tls_btnSaveNClose.Text = "Sa&ve&&Cls";
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
            this.tls_btnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tls_btnClose_MouseDown);
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
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
            this.btnModifyJournal.Location = new System.Drawing.Point(1126, 6);
            this.btnModifyJournal.Name = "btnModifyJournal";
            this.btnModifyJournal.Size = new System.Drawing.Size(21, 20);
            this.btnModifyJournal.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnModifyJournal, "Modify Payment Tray");
            this.btnModifyJournal.UseVisualStyleBackColor = false;
            this.btnModifyJournal.Visible = false;
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
            this.btnSetupJournal.Location = new System.Drawing.Point(1101, 6);
            this.btnSetupJournal.Name = "btnSetupJournal";
            this.btnSetupJournal.Size = new System.Drawing.Size(21, 20);
            this.btnSetupJournal.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnSetupJournal, "Add Payment Tray");
            this.btnSetupJournal.UseVisualStyleBackColor = false;
            this.btnSetupJournal.Visible = false;
            this.btnSetupJournal.Click += new System.EventHandler(this.btnSetupJournal_Click);
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(798, 5);
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
            this.mskCloseDate.Location = new System.Drawing.Point(693, 1);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(97, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDate);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            this.mskCloseDate.Validated += new System.EventHandler(this.mskCloseDate_Validated);
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(617, 5);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 0;
            this.label42.Text = "Close Date :";
            // 
            // rbPaySource_Insurance
            // 
            this.rbPaySource_Insurance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rbPaySource_Insurance.AutoSize = true;
            this.rbPaySource_Insurance.Location = new System.Drawing.Point(315, 62);
            this.rbPaySource_Insurance.Name = "rbPaySource_Insurance";
            this.rbPaySource_Insurance.Size = new System.Drawing.Size(78, 18);
            this.rbPaySource_Insurance.TabIndex = 0;
            this.rbPaySource_Insurance.Text = "Insurance";
            this.rbPaySource_Insurance.UseVisualStyleBackColor = true;
            this.rbPaySource_Insurance.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1185, 30);
            this.panel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblPaymentTray);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.rbPayType_Payment);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label42);
            this.panel6.Controls.Add(this.btnTraySelection);
            this.panel6.Controls.Add(this.rbPayType_Refund);
            this.panel6.Controls.Add(this.rbPayType_ExitingPayment);
            this.panel6.Controls.Add(this.label90);
            this.panel6.Controls.Add(this.mskCloseDate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1179, 24);
            this.panel6.TabIndex = 212;
            this.panel6.TabStop = true;
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(896, 5);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(249, 14);
            this.lblPaymentTray.TabIndex = 208;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPaymentTray.MouseLeave += new System.EventHandler(this.lblPaymentTray_MouseLeave);
            this.lblPaymentTray.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblPaymentTray_MouseMove);
            this.lblPaymentTray.MouseEnter += new System.EventHandler(this.lblPaymentTray_MouseEnter);
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1177, 1);
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
            this.label22.Location = new System.Drawing.Point(1178, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 23);
            this.label22.TabIndex = 27;
            // 
            // rbPayType_Payment
            // 
            this.rbPayType_Payment.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPayType_Payment.AutoSize = true;
            this.rbPayType_Payment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPayType_Payment.BackgroundImage")));
            this.rbPayType_Payment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbPayType_Payment.Checked = true;
            this.rbPayType_Payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPayType_Payment.Location = new System.Drawing.Point(150, -3);
            this.rbPayType_Payment.Name = "rbPayType_Payment";
            this.rbPayType_Payment.Size = new System.Drawing.Size(67, 26);
            this.rbPayType_Payment.TabIndex = 0;
            this.rbPayType_Payment.TabStop = true;
            this.rbPayType_Payment.Text = "Payment";
            this.rbPayType_Payment.UseVisualStyleBackColor = true;
            this.rbPayType_Payment.Visible = false;
            this.rbPayType_Payment.CheckedChanged += new System.EventHandler(this.rbPayType_Payment_CheckedChanged);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1179, 1);
            this.label8.TabIndex = 29;
            // 
            // btnTraySelection
            // 
            this.btnTraySelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraySelection.AutoEllipsis = true;
            this.btnTraySelection.BackColor = System.Drawing.Color.Transparent;
            this.btnTraySelection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTraySelection.BackgroundImage")));
            this.btnTraySelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTraySelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraySelection.Image = ((System.Drawing.Image)(resources.GetObject("btnTraySelection.Image")));
            this.btnTraySelection.Location = new System.Drawing.Point(1151, 2);
            this.btnTraySelection.Name = "btnTraySelection";
            this.btnTraySelection.Size = new System.Drawing.Size(21, 20);
            this.btnTraySelection.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnTraySelection, "Select Payment Tray");
            this.btnTraySelection.UseVisualStyleBackColor = false;
            this.btnTraySelection.Click += new System.EventHandler(this.btnTraySelection_Click);
            // 
            // rbPayType_Refund
            // 
            this.rbPayType_Refund.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPayType_Refund.AutoSize = true;
            this.rbPayType_Refund.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPayType_Refund.BackgroundImage")));
            this.rbPayType_Refund.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbPayType_Refund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPayType_Refund.Location = new System.Drawing.Point(229, -3);
            this.rbPayType_Refund.Name = "rbPayType_Refund";
            this.rbPayType_Refund.Size = new System.Drawing.Size(58, 26);
            this.rbPayType_Refund.TabIndex = 1;
            this.rbPayType_Refund.Text = "Refund";
            this.rbPayType_Refund.UseVisualStyleBackColor = true;
            this.rbPayType_Refund.Visible = false;
            this.rbPayType_Refund.CheckedChanged += new System.EventHandler(this.rbPayType_Refund_CheckedChanged);
            // 
            // rbPayType_ExitingPayment
            // 
            this.rbPayType_ExitingPayment.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPayType_ExitingPayment.AutoSize = true;
            this.rbPayType_ExitingPayment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPayType_ExitingPayment.BackgroundImage")));
            this.rbPayType_ExitingPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbPayType_ExitingPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPayType_ExitingPayment.Location = new System.Drawing.Point(289, -3);
            this.rbPayType_ExitingPayment.Name = "rbPayType_ExitingPayment";
            this.rbPayType_ExitingPayment.Size = new System.Drawing.Size(117, 26);
            this.rbPayType_ExitingPayment.TabIndex = 2;
            this.rbPayType_ExitingPayment.Text = "Existing Payments";
            this.rbPayType_ExitingPayment.UseVisualStyleBackColor = true;
            this.rbPayType_ExitingPayment.Visible = false;
            this.rbPayType_ExitingPayment.CheckedChanged += new System.EventHandler(this.rbPayType_ExitingPayment_CheckedChanged);
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(850, 8);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(72, 14);
            this.label39.TabIndex = 206;
            this.label39.Text = "Payment# :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbPaySource_Personal
            // 
            this.rbPaySource_Personal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rbPaySource_Personal.AutoSize = true;
            this.rbPaySource_Personal.Checked = true;
            this.rbPaySource_Personal.Location = new System.Drawing.Point(315, 42);
            this.rbPaySource_Personal.Name = "rbPaySource_Personal";
            this.rbPaySource_Personal.Size = new System.Drawing.Size(70, 18);
            this.rbPaySource_Personal.TabIndex = 1;
            this.rbPaySource_Personal.TabStop = true;
            this.rbPaySource_Personal.Text = "Personal";
            this.rbPaySource_Personal.UseVisualStyleBackColor = true;
            this.rbPaySource_Personal.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(27, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Payer :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // lblCheckNo
            // 
            this.lblCheckNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckNo.AutoEllipsis = true;
            this.lblCheckNo.AutoSize = true;
            this.lblCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCheckNo.Location = new System.Drawing.Point(6, 62);
            this.lblCheckNo.MaximumSize = new System.Drawing.Size(90, 15);
            this.lblCheckNo.MinimumSize = new System.Drawing.Size(90, 15);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(90, 15);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Check# :";
            this.lblCheckNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckDate.AutoEllipsis = true;
            this.lblCheckDate.AutoSize = true;
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCheckDate.Location = new System.Drawing.Point(6, 87);
            this.lblCheckDate.MaximumSize = new System.Drawing.Size(90, 15);
            this.lblCheckDate.MinimumSize = new System.Drawing.Size(90, 15);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(90, 15);
            this.lblCheckDate.TabIndex = 6;
            this.lblCheckDate.Text = "Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayer
            // 
            this.lblPayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayer.BackColor = System.Drawing.Color.White;
            this.lblPayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPayer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayer.Location = new System.Drawing.Point(101, 66);
            this.lblPayer.Name = "lblPayer";
            this.lblPayer.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPayer.Size = new System.Drawing.Size(117, 20);
            this.lblPayer.TabIndex = 201;
            this.lblPayer.Visible = false;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheckNumber.Location = new System.Drawing.Point(97, 59);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(148, 22);
            this.txtCheckNumber.TabIndex = 2;
            this.txtCheckNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // mskCheckDate
            // 
            this.mskCheckDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCheckDate.Location = new System.Drawing.Point(97, 84);
            this.mskCheckDate.Mask = "00/00/0000";
            this.mskCheckDate.Name = "mskCheckDate";
            this.mskCheckDate.Size = new System.Drawing.Size(128, 22);
            this.mskCheckDate.TabIndex = 3;
            this.mskCheckDate.ValidatingType = typeof(System.DateTime);
            this.mskCheckDate.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDate);
            this.mskCheckDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            this.mskCheckDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label39);
            this.panel2.Controls.Add(this.lblPaymetNo);
            this.panel2.Controls.Add(this.chkPayMstIncludeNotes);
            this.panel2.Controls.Add(this.txtPayMstNotes);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.btnModifyJournal);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.btnSetupJournal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1185, 146);
            this.panel2.TabIndex = 0;
            this.panel2.TabStop = true;
            // 
            // lblPaymetNo
            // 
            this.lblPaymetNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymetNo.AutoEllipsis = true;
            this.lblPaymetNo.AutoSize = true;
            this.lblPaymetNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymetNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymetNo.Location = new System.Drawing.Point(925, 8);
            this.lblPaymetNo.Name = "lblPaymetNo";
            this.lblPaymetNo.Size = new System.Drawing.Size(41, 14);
            this.lblPaymetNo.TabIndex = 209;
            this.lblPaymetNo.Text = "PayNo";
            this.lblPaymetNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPayMstIncludeNotes
            // 
            this.chkPayMstIncludeNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPayMstIncludeNotes.AutoSize = true;
            this.chkPayMstIncludeNotes.Location = new System.Drawing.Point(925, 124);
            this.chkPayMstIncludeNotes.Name = "chkPayMstIncludeNotes";
            this.chkPayMstIncludeNotes.Size = new System.Drawing.Size(160, 18);
            this.chkPayMstIncludeNotes.TabIndex = 32;
            this.chkPayMstIncludeNotes.Text = "Include Note on Receipt";
            this.chkPayMstIncludeNotes.UseVisualStyleBackColor = true;
            // 
            // txtPayMstNotes
            // 
            this.txtPayMstNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayMstNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayMstNotes.ForeColor = System.Drawing.Color.Black;
            this.txtPayMstNotes.Location = new System.Drawing.Point(925, 32);
            this.txtPayMstNotes.MaxLength = 255;
            this.txtPayMstNotes.Multiline = true;
            this.txtPayMstNotes.Name = "txtPayMstNotes";
            this.txtPayMstNotes.Size = new System.Drawing.Size(248, 91);
            this.txtPayMstNotes.TabIndex = 31;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblShowRemaining);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.btnDistubuteAmount);
            this.panel4.Controls.Add(this.lblCheckAmount);
            this.panel4.Controls.Add(this.txtCheckRemaining);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lblCheckRemaining);
            this.panel4.Controls.Add(this.txtCheckAmount);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lblPayer);
            this.panel4.Controls.Add(this.btnReserveRemaining);
            this.panel4.Controls.Add(this.lblPayerDisplay);
            this.panel4.Controls.Add(this.btnUseReserve);
            this.panel4.Controls.Add(this.btnClearReserve);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(466, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(384, 141);
            this.panel4.TabIndex = 1;
            this.panel4.TabStop = true;
            // 
            // lblShowRemaining
            // 
            this.lblShowRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShowRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblShowRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblShowRemaining.Location = new System.Drawing.Point(75, 41);
            this.lblShowRemaining.Name = "lblShowRemaining";
            this.lblShowRemaining.Size = new System.Drawing.Size(141, 14);
            this.lblShowRemaining.TabIndex = 206;
            this.lblShowRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.rbRecType_Other);
            this.panel5.Controls.Add(this.rbRecType_Copay);
            this.panel5.Controls.Add(this.rbRecType_Advance);
            this.panel5.Location = new System.Drawing.Point(34, 100);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(206, 29);
            this.panel5.TabIndex = 10;
            this.panel5.Visible = false;
            // 
            // rbRecType_Other
            // 
            this.rbRecType_Other.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbRecType_Other.AutoSize = true;
            this.rbRecType_Other.Checked = true;
            this.rbRecType_Other.Location = new System.Drawing.Point(145, 4);
            this.rbRecType_Other.Name = "rbRecType_Other";
            this.rbRecType_Other.Size = new System.Drawing.Size(57, 18);
            this.rbRecType_Other.TabIndex = 4;
            this.rbRecType_Other.TabStop = true;
            this.rbRecType_Other.Text = "Other";
            this.rbRecType_Other.UseVisualStyleBackColor = true;
            // 
            // rbRecType_Copay
            // 
            this.rbRecType_Copay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbRecType_Copay.AutoSize = true;
            this.rbRecType_Copay.Location = new System.Drawing.Point(3, 4);
            this.rbRecType_Copay.Name = "rbRecType_Copay";
            this.rbRecType_Copay.Size = new System.Drawing.Size(58, 18);
            this.rbRecType_Copay.TabIndex = 2;
            this.rbRecType_Copay.Text = "Copay";
            this.rbRecType_Copay.UseVisualStyleBackColor = true;
            // 
            // rbRecType_Advance
            // 
            this.rbRecType_Advance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbRecType_Advance.AutoSize = true;
            this.rbRecType_Advance.Location = new System.Drawing.Point(67, 5);
            this.rbRecType_Advance.Name = "rbRecType_Advance";
            this.rbRecType_Advance.Size = new System.Drawing.Size(72, 18);
            this.rbRecType_Advance.TabIndex = 3;
            this.rbRecType_Advance.Text = "Advance";
            this.rbRecType_Advance.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(305, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "View All Claims";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btnDistubuteAmount
            // 
            this.btnDistubuteAmount.AutoEllipsis = true;
            this.btnDistubuteAmount.BackColor = System.Drawing.Color.Transparent;
            this.btnDistubuteAmount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDistubuteAmount.BackgroundImage")));
            this.btnDistubuteAmount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDistubuteAmount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDistubuteAmount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDistubuteAmount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDistubuteAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDistubuteAmount.Image = ((System.Drawing.Image)(resources.GetObject("btnDistubuteAmount.Image")));
            this.btnDistubuteAmount.Location = new System.Drawing.Point(225, 8);
            this.btnDistubuteAmount.Name = "btnDistubuteAmount";
            this.btnDistubuteAmount.Size = new System.Drawing.Size(22, 24);
            this.btnDistubuteAmount.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnDistubuteAmount, "Distribute Amount");
            this.btnDistubuteAmount.UseVisualStyleBackColor = false;
            this.btnDistubuteAmount.Click += new System.EventHandler(this.btnDistubuteAmount_Click);
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckAmount.AutoEllipsis = true;
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(13, 15);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 14);
            this.lblCheckAmount.TabIndex = 5;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCheckRemaining
            // 
            this.txtCheckRemaining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txtCheckRemaining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheckRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckRemaining.ForeColor = System.Drawing.Color.Green;
            this.txtCheckRemaining.Location = new System.Drawing.Point(74, 37);
            this.txtCheckRemaining.Name = "txtCheckRemaining";
            this.txtCheckRemaining.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.txtCheckRemaining.Size = new System.Drawing.Size(146, 22);
            this.txtCheckRemaining.TabIndex = 3;
            this.txtCheckRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtCheckRemaining.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(249, 106);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label7.Size = new System.Drawing.Size(56, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "Receipt :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Visible = false;
            // 
            // lblCheckRemaining
            // 
            this.lblCheckRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckRemaining.AutoEllipsis = true;
            this.lblCheckRemaining.AutoSize = true;
            this.lblCheckRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckRemaining.Location = new System.Drawing.Point(2, 41);
            this.lblCheckRemaining.Name = "lblCheckRemaining";
            this.lblCheckRemaining.Size = new System.Drawing.Size(70, 14);
            this.lblCheckRemaining.TabIndex = 7;
            this.lblCheckRemaining.Text = "Remaining :";
            this.lblCheckRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCheckAmount
            // 
            this.txtCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckAmount.Location = new System.Drawing.Point(74, 11);
            this.txtCheckAmount.MaxLength = 13;
            this.txtCheckAmount.Name = "txtCheckAmount";
            this.txtCheckAmount.ShortcutsEnabled = false;
            this.txtCheckAmount.Size = new System.Drawing.Size(148, 22);
            this.txtCheckAmount.TabIndex = 0;
            this.txtCheckAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheckAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckAmount_KeyDown);
            this.txtCheckAmount.Leave += new System.EventHandler(this.txtCheckAmount_Leave);
            this.txtCheckAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            this.txtCheckAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckAmount_KeyPress);
            // 
            // btnReserveRemaining
            // 
            this.btnReserveRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReserveRemaining.AutoEllipsis = true;
            this.btnReserveRemaining.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReserveRemaining.BackgroundImage")));
            this.btnReserveRemaining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReserveRemaining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReserveRemaining.Location = new System.Drawing.Point(225, 37);
            this.btnReserveRemaining.Name = "btnReserveRemaining";
            this.btnReserveRemaining.Size = new System.Drawing.Size(145, 24);
            this.btnReserveRemaining.TabIndex = 9;
            this.btnReserveRemaining.Text = "Reserve Remaining";
            this.btnReserveRemaining.UseVisualStyleBackColor = true;
            this.btnReserveRemaining.Click += new System.EventHandler(this.btnReserveRemaining_Click);
            // 
            // lblPayerDisplay
            // 
            this.lblPayerDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblPayerDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPayerDisplay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayerDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayerDisplay.Location = new System.Drawing.Point(101, 66);
            this.lblPayerDisplay.Name = "lblPayerDisplay";
            this.lblPayerDisplay.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPayerDisplay.Size = new System.Drawing.Size(69, 21);
            this.lblPayerDisplay.TabIndex = 205;
            this.lblPayerDisplay.Visible = false;
            // 
            // btnUseReserve
            // 
            this.btnUseReserve.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUseReserve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUseReserve.BackgroundImage")));
            this.btnUseReserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUseReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUseReserve.Location = new System.Drawing.Point(252, 8);
            this.btnUseReserve.Name = "btnUseReserve";
            this.btnUseReserve.Size = new System.Drawing.Size(118, 24);
            this.btnUseReserve.TabIndex = 11;
            this.btnUseReserve.Text = "Use Reserves";
            this.tooltip_Billing.SetToolTip(this.btnUseReserve, "Use Reserves");
            this.btnUseReserve.UseVisualStyleBackColor = true;
            this.btnUseReserve.Click += new System.EventHandler(this.btnUseReserve_Click);
            // 
            // btnClearReserve
            // 
            this.btnClearReserve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearReserve.BackgroundImage")));
            this.btnClearReserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReserve.Location = new System.Drawing.Point(252, 8);
            this.btnClearReserve.Name = "btnClearReserve";
            this.btnClearReserve.Size = new System.Drawing.Size(114, 24);
            this.btnClearReserve.TabIndex = 12;
            this.btnClearReserve.Text = "Clear Reserve";
            this.btnClearReserve.UseVisualStyleBackColor = true;
            this.btnClearReserve.Click += new System.EventHandler(this.btnClearReserve_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlCredit);
            this.panel3.Controls.Add(this.panel16);
            this.panel3.Controls.Add(this.lblPatientSearch);
            this.panel3.Controls.Add(this.txtPatientSearch);
            this.panel3.Controls.Add(this.cmbPayMode);
            this.panel3.Controls.Add(this.rbPaySource_Insurance);
            this.panel3.Controls.Add(this.lblCheckNo);
            this.panel3.Controls.Add(this.lblPayType);
            this.panel3.Controls.Add(this.lblCheckDate);
            this.panel3.Controls.Add(this.mskCheckDate);
            this.panel3.Controls.Add(this.txtCheckNumber);
            this.panel3.Controls.Add(this.rbPaySource_Personal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 141);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // pnlCredit
            // 
            this.pnlCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCredit.AutoSize = true;
            this.pnlCredit.Controls.Add(this.mskCreditExpiryDate);
            this.pnlCredit.Controls.Add(this.lblExpiryDate);
            this.pnlCredit.Controls.Add(this.txtCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.lblCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.cmbCardType);
            this.pnlCredit.Controls.Add(this.lblCardType);
            this.pnlCredit.Location = new System.Drawing.Point(0, 109);
            this.pnlCredit.Name = "pnlCredit";
            this.pnlCredit.Size = new System.Drawing.Size(462, 29);
            this.pnlCredit.TabIndex = 0;
            // 
            // mskCreditExpiryDate
            // 
            this.mskCreditExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCreditExpiryDate.Location = new System.Drawing.Point(410, 1);
            this.mskCreditExpiryDate.Mask = "00/00";
            this.mskCreditExpiryDate.Name = "mskCreditExpiryDate";
            this.mskCreditExpiryDate.Size = new System.Drawing.Size(50, 22);
            this.mskCreditExpiryDate.TabIndex = 2;
            this.mskCreditExpiryDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            this.mskCreditExpiryDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpiryDate.AutoEllipsis = true;
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblExpiryDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExpiryDate.Location = new System.Drawing.Point(373, 5);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(35, 14);
            this.lblExpiryDate.TabIndex = 34;
            this.lblExpiryDate.Text = "Exp :";
            this.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCardAuthorizationNo
            // 
            this.txtCardAuthorizationNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCardAuthorizationNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardAuthorizationNo.Location = new System.Drawing.Point(278, 1);
            this.txtCardAuthorizationNo.MaxLength = 16;
            this.txtCardAuthorizationNo.Name = "txtCardAuthorizationNo";
            this.txtCardAuthorizationNo.Size = new System.Drawing.Size(92, 22);
            this.txtCardAuthorizationNo.TabIndex = 1;
            this.txtCardAuthorizationNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // lblCardAuthorizationNo
            // 
            this.lblCardAuthorizationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCardAuthorizationNo.AutoEllipsis = true;
            this.lblCardAuthorizationNo.AutoSize = true;
            this.lblCardAuthorizationNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardAuthorizationNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardAuthorizationNo.Location = new System.Drawing.Point(227, 5);
            this.lblCardAuthorizationNo.Name = "lblCardAuthorizationNo";
            this.lblCardAuthorizationNo.Size = new System.Drawing.Size(51, 14);
            this.lblCardAuthorizationNo.TabIndex = 108;
            this.lblCardAuthorizationNo.Text = "Auth# :";
            this.lblCardAuthorizationNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCardType
            // 
            this.cmbCardType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.ForeColor = System.Drawing.Color.Black;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(97, 1);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(128, 22);
            this.cmbCardType.TabIndex = 0;
            this.cmbCardType.SelectedIndexChanged += new System.EventHandler(this.cmbCardType_SelectedIndexChanged);
            this.cmbCardType.MouseEnter += new System.EventHandler(this.cmbCardType_MouseEnter);
            this.cmbCardType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // lblCardType
            // 
            this.lblCardType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCardType.AutoEllipsis = true;
            this.lblCardType.AutoSize = true;
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardType.Location = new System.Drawing.Point(6, 5);
            this.lblCardType.MaximumSize = new System.Drawing.Size(90, 15);
            this.lblCardType.MinimumSize = new System.Drawing.Size(90, 15);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(90, 15);
            this.lblCardType.TabIndex = 36;
            this.lblCardType.Text = "Card Type :";
            this.lblCardType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel16
            // 
            this.panel16.AutoSize = true;
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(0, 138);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel16.Size = new System.Drawing.Size(462, 3);
            this.panel16.TabIndex = 4;
            this.panel16.Visible = false;
            // 
            // lblPatientSearch
            // 
            this.lblPatientSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientSearch.AutoEllipsis = true;
            this.lblPatientSearch.AutoSize = true;
            this.lblPatientSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientSearch.Location = new System.Drawing.Point(6, 12);
            this.lblPatientSearch.MaximumSize = new System.Drawing.Size(90, 15);
            this.lblPatientSearch.MinimumSize = new System.Drawing.Size(90, 15);
            this.lblPatientSearch.Name = "lblPatientSearch";
            this.lblPatientSearch.Size = new System.Drawing.Size(90, 15);
            this.lblPatientSearch.TabIndex = 206;
            this.lblPatientSearch.Text = "Patient :";
            this.lblPatientSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPatientSearch
            // 
            this.txtPatientSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatientSearch.Location = new System.Drawing.Point(97, 9);
            this.txtPatientSearch.Name = "txtPatientSearch";
            this.txtPatientSearch.Size = new System.Drawing.Size(148, 22);
            this.txtPatientSearch.TabIndex = 0;
            this.txtPatientSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            this.txtPatientSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatientSearch_KeyPress);
            // 
            // cmbPayMode
            // 
            this.cmbPayMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayMode.ForeColor = System.Drawing.Color.Black;
            this.cmbPayMode.FormattingEnabled = true;
            this.cmbPayMode.Items.AddRange(new object[] {
            ""});
            this.cmbPayMode.Location = new System.Drawing.Point(97, 34);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.Size = new System.Drawing.Size(148, 22);
            this.cmbPayMode.TabIndex = 1;
            this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
            this.cmbPayMode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursor);
            // 
            // lblPayType
            // 
            this.lblPayType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayType.AutoEllipsis = true;
            this.lblPayType.AutoSize = true;
            this.lblPayType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayType.Location = new System.Drawing.Point(6, 37);
            this.lblPayType.MaximumSize = new System.Drawing.Size(90, 15);
            this.lblPayType.MinimumSize = new System.Drawing.Size(90, 15);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(90, 15);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Pay Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 142);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1177, 1);
            this.label24.TabIndex = 29;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(1181, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 142);
            this.label26.TabIndex = 27;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(3, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 142);
            this.label27.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1179, 1);
            this.label1.TabIndex = 30;
            this.label1.Text = "Close Date :";
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(880, 32);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(42, 14);
            this.label31.TabIndex = 3;
            this.label31.Text = "Note :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPatientStrip
            // 
            this.pnlPatientStrip.Controls.Add(this.pnlSinglePayment);
            this.pnlPatientStrip.Controls.Add(this.label28);
            this.pnlPatientStrip.Controls.Add(this.pnlMultiplePayment);
            this.pnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientStrip.Location = new System.Drawing.Point(0, 229);
            this.pnlPatientStrip.Name = "pnlPatientStrip";
            this.pnlPatientStrip.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPatientStrip.Size = new System.Drawing.Size(1185, 432);
            this.pnlPatientStrip.TabIndex = 1;
            this.pnlPatientStrip.TabStop = true;
            // 
            // pnlSinglePayment
            // 
            this.pnlSinglePayment.Controls.Add(this.label11);
            this.pnlSinglePayment.Controls.Add(this.c1SinglePayment);
            this.pnlSinglePayment.Controls.Add(this.label13);
            this.pnlSinglePayment.Controls.Add(this.c1SinglePaymentTotal);
            this.pnlSinglePayment.Controls.Add(this.label12);
            this.pnlSinglePayment.Controls.Add(this.label25);
            this.pnlSinglePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSinglePayment.Location = new System.Drawing.Point(3, 0);
            this.pnlSinglePayment.Name = "pnlSinglePayment";
            this.pnlSinglePayment.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSinglePayment.Size = new System.Drawing.Size(1179, 263);
            this.pnlSinglePayment.TabIndex = 0;
            this.pnlSinglePayment.TabStop = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(1, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1177, 1);
            this.label11.TabIndex = 200;
            this.label11.Text = "Close Date :";
            // 
            // c1SinglePayment
            // 
            this.c1SinglePayment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SinglePayment.AutoGenerateColumns = false;
            this.c1SinglePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.c1SinglePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1SinglePayment.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SinglePayment.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SinglePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SinglePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SinglePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SinglePayment.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1SinglePayment.Location = new System.Drawing.Point(1, 3);
            this.c1SinglePayment.Name = "c1SinglePayment";
            this.c1SinglePayment.Rows.Count = 1;
            this.c1SinglePayment.Rows.DefaultSize = 19;
            this.c1SinglePayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePayment.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1SinglePayment.Size = new System.Drawing.Size(1177, 240);
            this.c1SinglePayment.StyleInfo = resources.GetString("c1SinglePayment.StyleInfo");
            this.c1SinglePayment.TabIndex = 0;
            this.c1SinglePayment.Click += new System.EventHandler(this.c1SinglePayment_Click);
            this.c1SinglePayment.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1SinglePayment_AfterRowColChange);
            this.c1SinglePayment.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_StartEdit);
            this.c1SinglePayment.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellButtonClick);
            this.c1SinglePayment.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterResizeColumn);
            this.c1SinglePayment.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_BeforeEdit);
            this.c1SinglePayment.DoubleClick += new System.EventHandler(this.c1SinglePayment_DoubleClick);
            this.c1SinglePayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);
            this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(1, 243);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1177, 1);
            this.label13.TabIndex = 1;
            this.label13.Text = "Close Date :";
            // 
            // c1SinglePaymentTotal
            // 
            this.c1SinglePaymentTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SinglePaymentTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SinglePaymentTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1SinglePaymentTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SinglePaymentTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SinglePaymentTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1SinglePaymentTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1SinglePaymentTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SinglePaymentTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SinglePaymentTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1SinglePaymentTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1SinglePaymentTotal.Location = new System.Drawing.Point(1, 244);
            this.c1SinglePaymentTotal.Name = "c1SinglePaymentTotal";
            this.c1SinglePaymentTotal.Rows.Count = 1;
            this.c1SinglePaymentTotal.Rows.DefaultSize = 19;
            this.c1SinglePaymentTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePaymentTotal.Size = new System.Drawing.Size(1177, 19);
            this.c1SinglePaymentTotal.StyleInfo = resources.GetString("c1SinglePaymentTotal.StyleInfo");
            this.c1SinglePaymentTotal.TabIndex = 2;
            this.c1SinglePaymentTotal.TabStop = false;
            this.c1SinglePaymentTotal.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1SinglePaymentTotal_AfterScroll);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 260);
            this.label12.TabIndex = 209;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(1178, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 260);
            this.label25.TabIndex = 210;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Location = new System.Drawing.Point(3, 263);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1179, 1);
            this.label28.TabIndex = 211;
            // 
            // pnlMultiplePayment
            // 
            this.pnlMultiplePayment.Controls.Add(this.c1MultiplePayment);
            this.pnlMultiplePayment.Controls.Add(this.label14);
            this.pnlMultiplePayment.Controls.Add(this.c1MultiplePaymentTotal);
            this.pnlMultiplePayment.Controls.Add(this.label10);
            this.pnlMultiplePayment.Controls.Add(this.label9);
            this.pnlMultiplePayment.Controls.Add(this.label29);
            this.pnlMultiplePayment.Controls.Add(this.label30);
            this.pnlMultiplePayment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMultiplePayment.Location = new System.Drawing.Point(3, 264);
            this.pnlMultiplePayment.Name = "pnlMultiplePayment";
            this.pnlMultiplePayment.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlMultiplePayment.Size = new System.Drawing.Size(1179, 168);
            this.pnlMultiplePayment.TabIndex = 205;
            // 
            // c1MultiplePayment
            // 
            this.c1MultiplePayment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1MultiplePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.c1MultiplePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1MultiplePayment.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1MultiplePayment.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1MultiplePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1MultiplePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1MultiplePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1MultiplePayment.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1MultiplePayment.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1MultiplePayment.Location = new System.Drawing.Point(1, 4);
            this.c1MultiplePayment.Name = "c1MultiplePayment";
            this.c1MultiplePayment.Rows.Count = 1;
            this.c1MultiplePayment.Rows.DefaultSize = 19;
            this.c1MultiplePayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MultiplePayment.Size = new System.Drawing.Size(1177, 143);
            this.c1MultiplePayment.StyleInfo = resources.GetString("c1MultiplePayment.StyleInfo");
            this.c1MultiplePayment.TabIndex = 0;
            this.c1MultiplePayment.TabStop = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(1, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1177, 1);
            this.label14.TabIndex = 1;
            this.label14.Text = "Close Date :";
            // 
            // c1MultiplePaymentTotal
            // 
            this.c1MultiplePaymentTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1MultiplePaymentTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1MultiplePaymentTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1MultiplePaymentTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1MultiplePaymentTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1MultiplePaymentTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1MultiplePaymentTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1MultiplePaymentTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1MultiplePaymentTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1MultiplePaymentTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1MultiplePaymentTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1MultiplePaymentTotal.Location = new System.Drawing.Point(1, 148);
            this.c1MultiplePaymentTotal.Name = "c1MultiplePaymentTotal";
            this.c1MultiplePaymentTotal.Rows.Count = 1;
            this.c1MultiplePaymentTotal.Rows.DefaultSize = 19;
            this.c1MultiplePaymentTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MultiplePaymentTotal.Size = new System.Drawing.Size(1177, 19);
            this.c1MultiplePaymentTotal.StyleInfo = resources.GetString("c1MultiplePaymentTotal.StyleInfo");
            this.c1MultiplePaymentTotal.TabIndex = 2;
            this.c1MultiplePaymentTotal.TabStop = false;
            this.c1MultiplePaymentTotal.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1MultiplePaymentTotal_AfterScroll);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(1, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1177, 1);
            this.label10.TabIndex = 3;
            this.label10.Text = "Close Date :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(0, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 164);
            this.label9.TabIndex = 209;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(1178, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 164);
            this.label29.TabIndex = 210;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(0, 167);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1179, 1);
            this.label30.TabIndex = 211;
            this.label30.Text = "Close Date :";
            // 
            // pnlShortcut
            // 
            this.pnlShortcut.Controls.Add(this.pnlTransactionOther2);
            this.pnlShortcut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcut.Location = new System.Drawing.Point(0, 661);
            this.pnlShortcut.Name = "pnlShortcut";
            this.pnlShortcut.Padding = new System.Windows.Forms.Padding(3);
            this.pnlShortcut.Size = new System.Drawing.Size(1185, 28);
            this.pnlShortcut.TabIndex = 201;
            // 
            // pnlTransactionOther2
            // 
            this.pnlTransactionOther2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlTransactionOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionOther2.Controls.Add(this.label19);
            this.pnlTransactionOther2.Controls.Add(this.label20);
            this.pnlTransactionOther2.Controls.Add(this.label130);
            this.pnlTransactionOther2.Controls.Add(this.label88);
            this.pnlTransactionOther2.Controls.Add(this.label89);
            this.pnlTransactionOther2.Controls.Add(this.label86);
            this.pnlTransactionOther2.Controls.Add(this.label87);
            this.pnlTransactionOther2.Controls.Add(this.label15);
            this.pnlTransactionOther2.Controls.Add(this.label34);
            this.pnlTransactionOther2.Controls.Add(this.label33);
            this.pnlTransactionOther2.Controls.Add(this.label32);
            this.pnlTransactionOther2.Controls.Add(this.label16);
            this.pnlTransactionOther2.Controls.Add(this.label17);
            this.pnlTransactionOther2.Controls.Add(this.label18);
            this.pnlTransactionOther2.Controls.Add(this.label49);
            this.pnlTransactionOther2.Controls.Add(this.label51);
            this.pnlTransactionOther2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionOther2.Location = new System.Drawing.Point(3, 3);
            this.pnlTransactionOther2.Name = "pnlTransactionOther2";
            this.pnlTransactionOther2.Size = new System.Drawing.Size(1179, 22);
            this.pnlTransactionOther2.TabIndex = 211;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(188, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 13);
            this.label19.TabIndex = 74;
            this.label19.Text = "- Save&&Close";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Maroon;
            this.label20.Location = new System.Drawing.Point(98, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(90, 13);
            this.label20.TabIndex = 73;
            this.label20.Text = "Ctrl + Shift + S";
            // 
            // label130
            // 
            this.label130.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label130.Location = new System.Drawing.Point(405, 4);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(125, 13);
            this.label130.TabIndex = 70;
            this.label130.Text = "- Distribute Payment";
            // 
            // label88
            // 
            this.label88.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(637, 4);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(67, 13);
            this.label88.TabIndex = 66;
            this.label88.Text = "- Prev Line";
            // 
            // label89
            // 
            this.label89.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.ForeColor = System.Drawing.Color.Maroon;
            this.label89.Location = new System.Drawing.Point(617, 4);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(20, 13);
            this.label89.TabIndex = 65;
            this.label89.Text = "F5";
            // 
            // label86
            // 
            this.label86.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(550, 4);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(67, 13);
            this.label86.TabIndex = 64;
            this.label86.Text = "- Next Line";
            // 
            // label87
            // 
            this.label87.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.ForeColor = System.Drawing.Color.Maroon;
            this.label87.Location = new System.Drawing.Point(530, 4);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(20, 13);
            this.label87.TabIndex = 63;
            this.label87.Text = "F4";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(293, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(92, 13);
            this.label15.TabIndex = 62;
            this.label15.Text = "- Payment Grid";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1177, 1);
            this.label34.TabIndex = 60;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1177, 1);
            this.label33.TabIndex = 59;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(1178, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 22);
            this.label32.TabIndex = 58;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 22);
            this.label16.TabIndex = 57;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(55, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 56;
            this.label17.Text = "- Save";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Maroon;
            this.label18.Location = new System.Drawing.Point(6, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 13);
            this.label18.TabIndex = 55;
            this.label18.Text = "Ctrl + S";
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.Maroon;
            this.label49.Location = new System.Drawing.Point(385, 4);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(20, 13);
            this.label49.TabIndex = 45;
            this.label49.Text = "F3";
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Maroon;
            this.label51.Location = new System.Drawing.Point(270, 4);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(23, 13);
            this.label51.TabIndex = 44;
            this.label51.Text = "F2 ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 232);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1185, 24);
            this.menuStrip1.TabIndex = 218;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPayment_Save,
            this.mnuPayment_SavenClose,
            this.mnuPayment_PaymentGrid,
            this.mnuPayment_DistributePayment,
            this.mnuPayment_NextServiceLine,
            this.mnuPayment_PrvServiceLine});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            this.mnuBilling.Visible = false;
            // 
            // mnuPayment_Save
            // 
            this.mnuPayment_Save.Name = "mnuPayment_Save";
            this.mnuPayment_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuPayment_Save.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_Save.Text = "Save";
            this.mnuPayment_Save.Click += new System.EventHandler(this.mnuPayment_Save_Click);
            // 
            // mnuPayment_SavenClose
            // 
            this.mnuPayment_SavenClose.Name = "mnuPayment_SavenClose";
            this.mnuPayment_SavenClose.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.mnuPayment_SavenClose.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_SavenClose.Text = "Save&Close";
            this.mnuPayment_SavenClose.Click += new System.EventHandler(this.mnuPayment_SavenClose_Click);
            // 
            // mnuPayment_PaymentGrid
            // 
            this.mnuPayment_PaymentGrid.Name = "mnuPayment_PaymentGrid";
            this.mnuPayment_PaymentGrid.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuPayment_PaymentGrid.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_PaymentGrid.Text = "Payment Grid";
            this.mnuPayment_PaymentGrid.Click += new System.EventHandler(this.mnuPayment_PaymentGrid_Click);
            // 
            // mnuPayment_DistributePayment
            // 
            this.mnuPayment_DistributePayment.Name = "mnuPayment_DistributePayment";
            this.mnuPayment_DistributePayment.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuPayment_DistributePayment.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_DistributePayment.Text = "Distribute Payment";
            this.mnuPayment_DistributePayment.Click += new System.EventHandler(this.mnuPayment_DistributePayment_Click);
            // 
            // mnuPayment_NextServiceLine
            // 
            this.mnuPayment_NextServiceLine.Name = "mnuPayment_NextServiceLine";
            this.mnuPayment_NextServiceLine.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuPayment_NextServiceLine.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_NextServiceLine.Text = "Next Line";
            this.mnuPayment_NextServiceLine.Visible = false;
            this.mnuPayment_NextServiceLine.Click += new System.EventHandler(this.mnuPayment_NextServiceLine_Click);
            // 
            // mnuPayment_PrvServiceLine
            // 
            this.mnuPayment_PrvServiceLine.Name = "mnuPayment_PrvServiceLine";
            this.mnuPayment_PrvServiceLine.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuPayment_PrvServiceLine.Size = new System.Drawing.Size(203, 22);
            this.mnuPayment_PrvServiceLine.Text = "Previous Line";
            this.mnuPayment_PrvServiceLine.Visible = false;
            this.mnuPayment_PrvServiceLine.Click += new System.EventHandler(this.mnuPayment_PrvServiceLine_Click);
            // 
            // tooltip_Billing
            // 
            this.tooltip_Billing.AutoPopDelay = 30000;
            this.tooltip_Billing.InitialDelay = 500;
            this.tooltip_Billing.ReshowDelay = 100;
            // 
            // frmPaymentPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1185, 689);
            this.Controls.Add(this.pnlPatientStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlShortcut);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPaymentPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Payment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPaymentInsurace_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlCredit.ResumeLayout(false);
            this.pnlCredit.PerformLayout();
            this.pnlPatientStrip.ResumeLayout(false);
            this.pnlSinglePayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).EndInit();
            this.pnlMultiplePayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePaymentTotal)).EndInit();
            this.pnlShortcut.ResumeLayout(false);
            this.pnlTransactionOther2.ResumeLayout(false);
            this.pnlTransactionOther2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnSave;
        private System.Windows.Forms.ToolStripButton tls_btnSaveNClose;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Button btnModifyJournal;
        private System.Windows.Forms.Button btnSetupJournal;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.RadioButton rbPaySource_Insurance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPaySource_Personal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.Label lblPayer;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.MaskedTextBox mskCheckDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbPayMode;
        private System.Windows.Forms.TextBox txtCheckAmount;
        private System.Windows.Forms.RadioButton rbPayType_Payment;
        private System.Windows.Forms.RadioButton rbPayType_ExitingPayment;
        private System.Windows.Forms.RadioButton rbPayType_Refund;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.Label lblCheckRemaining;
        private System.Windows.Forms.Label lblCheckAmount;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlPatientStrip;
        private System.Windows.Forms.Panel pnlMultiplePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MultiplePaymentTotal;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MultiplePayment;
        private System.Windows.Forms.Panel pnlSinglePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePaymentTotal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label txtCheckRemaining;
        private System.Windows.Forms.Button btnDistubuteAmount;
        private System.Windows.Forms.Panel pnlShortcut;
        private System.Windows.Forms.Panel pnlTransactionOther2;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_Save;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_SavenClose;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_PaymentGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_DistributePayment;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_NextServiceLine;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_PrvServiceLine;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnReserveRemaining;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbRecType_Other;
        private System.Windows.Forms.RadioButton rbRecType_Copay;
        private System.Windows.Forms.RadioButton rbRecType_Advance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tls_btnNew;
        private System.Windows.Forms.TextBox txtPayMstNotes;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox chkPayMstIncludeNotes;
        private System.Windows.Forms.Label lblPayerDisplay;
        private System.Windows.Forms.Button btnUseReserve;
        private System.Windows.Forms.Button btnClearReserve;
        private System.Windows.Forms.Label lblPatientSearch;
        private System.Windows.Forms.TextBox txtPatientSearch;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel pnlCredit;
        private System.Windows.Forms.MaskedTextBox mskCreditExpiryDate;
        private System.Windows.Forms.TextBox txtCardAuthorizationNo;
        private System.Windows.Forms.Label lblCardAuthorizationNo;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.ToolStripDropDownButton tls_btnReceipt;
        private System.Windows.Forms.Label lblShowRemaining;
        private System.Windows.Forms.ToolStripButton tls_btnDefaultReceipt;
        internal System.Windows.Forms.ToolStripButton tsb_ShowHideZeroBalance;
        private System.Windows.Forms.ToolStripButton tls_btnNewCorrection;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnTraySelection;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.Label lblPaymetNo;
        private System.Windows.Forms.ToolStripButton tls_btnCharge;
        private System.Windows.Forms.ToolTip tooltip_Billing;
    }
}