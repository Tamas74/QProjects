namespace gloBilling.Payment
{
    partial class frmInsurancePayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsurancePayment));
            this.pnlSinglePayment = new System.Windows.Forms.Panel();
            this.pnlSinglePaymentAllocation = new System.Windows.Forms.Panel();
            this.c1SinglePayment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSinglePaymentAllocationHdr = new System.Windows.Forms.Panel();
            this.label53 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.pnlSinglePaymentCorrTB = new System.Windows.Forms.Panel();
            this.label52 = new System.Windows.Forms.Label();
            this.c1SinglePaymentCorrTB = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSinglePaymentCorrTBHdr = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.c1SinglePaymentTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.c1MultiplePayment = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlMultiplePayment = new System.Windows.Forms.Panel();
            this.c1MultiplePaymentTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.pnlPatientStrip = new System.Windows.Forms.Panel();
            this.pnlAlerts = new System.Windows.Forms.Panel();
            this.chkShowCrosswalk = new System.Windows.Forms.CheckBox();
            this.txtClaimRemittanceRef = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.lblClaimNo = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblAlertMessage = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEOBRefNumber = new System.Windows.Forms.TextBox();
            this.txtCardAuthorizationNo = new System.Windows.Forms.TextBox();
            this.btnLoadCheck = new System.Windows.Forms.Button();
            this.cmbPayMode = new System.Windows.Forms.ComboBox();
            this.lblPayType = new System.Windows.Forms.Label();
            this.mskCreditExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.mnuPayment_DistributePayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_PaymentGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayment_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.mnuPayment_SavenClose = new System.Windows.Forms.ToolStripMenuItem();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.mnuPayment_NextServiceLine = new System.Windows.Forms.ToolStripMenuItem();
            this.label51 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mnuPayment_PrvServiceLine = new System.Windows.Forms.ToolStripMenuItem();
            this.label15 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTraySelection = new System.Windows.Forms.Button();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.rbPaySource_Insurance = new System.Windows.Forms.RadioButton();
            this.rbPaySource_Personal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPayType_Payment = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.rbPayType_Refund = new System.Windows.Forms.RadioButton();
            this.rbExistingPayment = new System.Windows.Forms.RadioButton();
            this.mnuPayment_ReasonCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.label17 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pnlTransactionOther2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlShortcut = new System.Windows.Forms.Panel();
            this.btnRemoveCheck = new System.Windows.Forms.Button();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.lblCheckNo = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.lblPayer = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.mskCheckDate = new System.Windows.Forms.MaskedTextBox();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.btnSearchInsuranceCompany = new System.Windows.Forms.Button();
            this.tls_btnViewEOB = new System.Windows.Forms.ToolStripButton();
            this.panel16 = new System.Windows.Forms.Panel();
            this.pnlCredit = new System.Windows.Forms.Panel();
            this.lblCardAuthorizationNo = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnNew = new System.Windows.Forms.ToolStripButton();
            this.tls_InsuranceLog = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refund = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCharge = new System.Windows.Forms.ToolStripButton();
            this.tls_btnPatAcct = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaymentPatient = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSaveNClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnSetupJournal = new System.Windows.Forms.Button();
            this.btnModifyJournal = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSameChkReserveAmount = new System.Windows.Forms.Label();
            this.lblReserveRemaining = new System.Windows.Forms.Label();
            this.lblCorrectionAmt = new System.Windows.Forms.Label();
            this.txtCheckNumber = new System.Windows.Forms.TextBox();
            this.lblPaymetNo = new System.Windows.Forms.Label();
            this.txtPayMstNotes = new System.Windows.Forms.TextBox();
            this.lblReserveAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkPayMstIncludeNotes = new System.Windows.Forms.CheckBox();
            this.btnUseReserve = new System.Windows.Forms.Button();
            this.btnReserveRemaining = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDistubuteAmount = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTakeBackAmt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalFunds = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtCheckRemaining = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCheckAmount = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.c1ClaimDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnlSinglePayment.SuspendLayout();
            this.pnlSinglePaymentAllocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePayment)).BeginInit();
            this.pnlSinglePaymentAllocationHdr.SuspendLayout();
            this.pnlSinglePaymentCorrTB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentCorrTB)).BeginInit();
            this.pnlSinglePaymentCorrTBHdr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePayment)).BeginInit();
            this.pnlMultiplePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePaymentTotal)).BeginInit();
            this.pnlPatientStrip.SuspendLayout();
            this.pnlAlerts.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlTransactionOther2.SuspendLayout();
            this.pnlShortcut.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlCredit.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSinglePayment
            // 
            this.pnlSinglePayment.Controls.Add(this.pnlSinglePaymentAllocation);
            this.pnlSinglePayment.Controls.Add(this.pnlSinglePaymentCorrTB);
            this.pnlSinglePayment.Controls.Add(this.label13);
            this.pnlSinglePayment.Controls.Add(this.c1SinglePaymentTotal);
            this.pnlSinglePayment.Controls.Add(this.label30);
            this.pnlSinglePayment.Controls.Add(this.label28);
            this.pnlSinglePayment.Controls.Add(this.label29);
            this.pnlSinglePayment.Controls.Add(this.label31);
            this.pnlSinglePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSinglePayment.Location = new System.Drawing.Point(3, 27);
            this.pnlSinglePayment.Name = "pnlSinglePayment";
            this.pnlSinglePayment.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSinglePayment.Size = new System.Drawing.Size(1236, 259);
            this.pnlSinglePayment.TabIndex = 207;
            // 
            // pnlSinglePaymentAllocation
            // 
            this.pnlSinglePaymentAllocation.Controls.Add(this.c1SinglePayment);
            this.pnlSinglePaymentAllocation.Controls.Add(this.pnlSinglePaymentAllocationHdr);
            this.pnlSinglePaymentAllocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSinglePaymentAllocation.Location = new System.Drawing.Point(1, 121);
            this.pnlSinglePaymentAllocation.Name = "pnlSinglePaymentAllocation";
            this.pnlSinglePaymentAllocation.Size = new System.Drawing.Size(1234, 120);
            this.pnlSinglePaymentAllocation.TabIndex = 205;
            // 
            // c1SinglePayment
            // 
            this.c1SinglePayment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SinglePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.c1SinglePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1SinglePayment.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SinglePayment.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SinglePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SinglePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SinglePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SinglePayment.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SinglePayment.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1SinglePayment.Location = new System.Drawing.Point(0, 23);
            this.c1SinglePayment.Name = "c1SinglePayment";
            this.c1SinglePayment.Rows.Count = 1;
            this.c1SinglePayment.Rows.DefaultSize = 19;
            this.c1SinglePayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePayment.Size = new System.Drawing.Size(1234, 97);
            this.c1SinglePayment.StyleInfo = resources.GetString("c1SinglePayment.StyleInfo");
            this.c1SinglePayment.TabIndex = 8;
            this.c1SinglePayment.TabStop = false;
            this.c1SinglePayment.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_StartEdit);
            this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
            this.c1SinglePayment.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);
            this.c1SinglePayment.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1SinglePayment_AfterScroll);
            this.c1SinglePayment.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellButtonClick);
            this.c1SinglePayment.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterResizeColumn);
            this.c1SinglePayment.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1SinglePayment_KeyUp);
            // 
            // pnlSinglePaymentAllocationHdr
            // 
            this.pnlSinglePaymentAllocationHdr.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.pnlSinglePaymentAllocationHdr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSinglePaymentAllocationHdr.Controls.Add(this.label53);
            this.pnlSinglePaymentAllocationHdr.Controls.Add(this.label48);
            this.pnlSinglePaymentAllocationHdr.Controls.Add(this.label47);
            this.pnlSinglePaymentAllocationHdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSinglePaymentAllocationHdr.Location = new System.Drawing.Point(0, 0);
            this.pnlSinglePaymentAllocationHdr.Name = "pnlSinglePaymentAllocationHdr";
            this.pnlSinglePaymentAllocationHdr.Size = new System.Drawing.Size(1234, 23);
            this.pnlSinglePaymentAllocationHdr.TabIndex = 9;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(0, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1234, 1);
            this.label53.TabIndex = 217;
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label48.Location = new System.Drawing.Point(5, 5);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(80, 14);
            this.label48.TabIndex = 216;
            this.label48.Text = " Allocation :";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(0, 22);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1234, 1);
            this.label47.TabIndex = 61;
            // 
            // pnlSinglePaymentCorrTB
            // 
            this.pnlSinglePaymentCorrTB.Controls.Add(this.label52);
            this.pnlSinglePaymentCorrTB.Controls.Add(this.c1SinglePaymentCorrTB);
            this.pnlSinglePaymentCorrTB.Controls.Add(this.pnlSinglePaymentCorrTBHdr);
            this.pnlSinglePaymentCorrTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSinglePaymentCorrTB.Location = new System.Drawing.Point(1, 4);
            this.pnlSinglePaymentCorrTB.Name = "pnlSinglePaymentCorrTB";
            this.pnlSinglePaymentCorrTB.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlSinglePaymentCorrTB.Size = new System.Drawing.Size(1234, 117);
            this.pnlSinglePaymentCorrTB.TabIndex = 205;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(0, 113);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1234, 1);
            this.label52.TabIndex = 62;
            // 
            // c1SinglePaymentCorrTB
            // 
            this.c1SinglePaymentCorrTB.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SinglePaymentCorrTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.c1SinglePaymentCorrTB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1SinglePaymentCorrTB.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SinglePaymentCorrTB.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1SinglePaymentCorrTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SinglePaymentCorrTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SinglePaymentCorrTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SinglePaymentCorrTB.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SinglePaymentCorrTB.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1SinglePaymentCorrTB.Location = new System.Drawing.Point(0, 23);
            this.c1SinglePaymentCorrTB.Name = "c1SinglePaymentCorrTB";
            this.c1SinglePaymentCorrTB.Rows.Count = 1;
            this.c1SinglePaymentCorrTB.Rows.DefaultSize = 19;
            this.c1SinglePaymentCorrTB.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePaymentCorrTB.Size = new System.Drawing.Size(1234, 91);
            this.c1SinglePaymentCorrTB.StyleInfo = resources.GetString("c1SinglePaymentCorrTB.StyleInfo");
            this.c1SinglePaymentCorrTB.TabIndex = 9;
            this.c1SinglePaymentCorrTB.TabStop = false;
            this.c1SinglePaymentCorrTB.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePaymentCorrTB_StartEdit);
            this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);
            this.c1SinglePaymentCorrTB.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePaymentCorrTB_AfterEdit);
            this.c1SinglePaymentCorrTB.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1SinglePaymentCorrTB_AfterScroll);
            this.c1SinglePaymentCorrTB.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePaymentCorrTB_CellButtonClick);
            this.c1SinglePaymentCorrTB.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePaymentCorrTB_AfterResizeColumn);
            this.c1SinglePaymentCorrTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1SinglePaymentCorrTB_KeyUp);
            // 
            // pnlSinglePaymentCorrTBHdr
            // 
            this.pnlSinglePaymentCorrTBHdr.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.pnlSinglePaymentCorrTBHdr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSinglePaymentCorrTBHdr.Controls.Add(this.label49);
            this.pnlSinglePaymentCorrTBHdr.Controls.Add(this.label50);
            this.pnlSinglePaymentCorrTBHdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSinglePaymentCorrTBHdr.Location = new System.Drawing.Point(0, 0);
            this.pnlSinglePaymentCorrTBHdr.Name = "pnlSinglePaymentCorrTBHdr";
            this.pnlSinglePaymentCorrTBHdr.Size = new System.Drawing.Size(1234, 23);
            this.pnlSinglePaymentCorrTBHdr.TabIndex = 10;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label49.Location = new System.Drawing.Point(5, 5);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(133, 14);
            this.label49.TabIndex = 216;
            this.label49.Text = " Correction (+ or -) :";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(0, 22);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1234, 1);
            this.label50.TabIndex = 61;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 241);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1234, 1);
            this.label13.TabIndex = 204;
            this.label13.Text = "label1";
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
            this.c1SinglePaymentTotal.Location = new System.Drawing.Point(1, 242);
            this.c1SinglePaymentTotal.Name = "c1SinglePaymentTotal";
            this.c1SinglePaymentTotal.Rows.Count = 1;
            this.c1SinglePaymentTotal.Rows.DefaultSize = 19;
            this.c1SinglePaymentTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SinglePaymentTotal.Size = new System.Drawing.Size(1234, 16);
            this.c1SinglePaymentTotal.StyleInfo = resources.GetString("c1SinglePaymentTotal.StyleInfo");
            this.c1SinglePaymentTotal.TabIndex = 2;
            this.c1SinglePaymentTotal.TabStop = false;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(1, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1234, 1);
            this.label30.TabIndex = 202;
            this.label30.Text = "label1";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(0, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 255);
            this.label28.TabIndex = 200;
            this.label28.Text = "Close Date :";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(1235, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 255);
            this.label29.TabIndex = 201;
            this.label29.Text = "Close Date :";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 258);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1236, 1);
            this.label31.TabIndex = 203;
            this.label31.Text = "label1";
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
            this.c1MultiplePayment.Location = new System.Drawing.Point(4, 1);
            this.c1MultiplePayment.Name = "c1MultiplePayment";
            this.c1MultiplePayment.Rows.Count = 1;
            this.c1MultiplePayment.Rows.DefaultSize = 19;
            this.c1MultiplePayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MultiplePayment.Size = new System.Drawing.Size(1234, 106);
            this.c1MultiplePayment.StyleInfo = resources.GetString("c1MultiplePayment.StyleInfo");
            this.c1MultiplePayment.TabIndex = 0;
            this.c1MultiplePayment.TabStop = false;
            this.c1MultiplePayment.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1MultiplePayment_AfterResizeColumn);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(4, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1234, 1);
            this.label14.TabIndex = 1;
            this.label14.Text = "Close Date :";
            // 
            // pnlMultiplePayment
            // 
            this.pnlMultiplePayment.Controls.Add(this.c1MultiplePayment);
            this.pnlMultiplePayment.Controls.Add(this.label14);
            this.pnlMultiplePayment.Controls.Add(this.c1MultiplePaymentTotal);
            this.pnlMultiplePayment.Controls.Add(this.label10);
            this.pnlMultiplePayment.Controls.Add(this.label11);
            this.pnlMultiplePayment.Controls.Add(this.label26);
            this.pnlMultiplePayment.Controls.Add(this.label27);
            this.pnlMultiplePayment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMultiplePayment.Location = new System.Drawing.Point(0, 536);
            this.pnlMultiplePayment.Name = "pnlMultiplePayment";
            this.pnlMultiplePayment.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlMultiplePayment.Size = new System.Drawing.Size(1242, 125);
            this.pnlMultiplePayment.TabIndex = 226;
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
            this.c1MultiplePaymentTotal.Location = new System.Drawing.Point(4, 108);
            this.c1MultiplePaymentTotal.Name = "c1MultiplePaymentTotal";
            this.c1MultiplePaymentTotal.Rows.Count = 1;
            this.c1MultiplePaymentTotal.Rows.DefaultSize = 19;
            this.c1MultiplePaymentTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1MultiplePaymentTotal.Size = new System.Drawing.Size(1234, 16);
            this.c1MultiplePaymentTotal.StyleInfo = resources.GetString("c1MultiplePaymentTotal.StyleInfo");
            this.c1MultiplePaymentTotal.TabIndex = 2;
            this.c1MultiplePaymentTotal.TabStop = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1234, 1);
            this.label10.TabIndex = 6;
            this.label10.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1234, 1);
            this.label11.TabIndex = 7;
            this.label11.Text = "label1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(3, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 125);
            this.label26.TabIndex = 200;
            this.label26.Text = "Close Date :";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(1238, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 125);
            this.label27.TabIndex = 201;
            this.label27.Text = "Close Date :";
            // 
            // pnlPatientStrip
            // 
            this.pnlPatientStrip.AutoSize = true;
            this.pnlPatientStrip.Controls.Add(this.pnlSinglePayment);
            this.pnlPatientStrip.Controls.Add(this.pnlAlerts);
            this.pnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientStrip.Location = new System.Drawing.Point(0, 227);
            this.pnlPatientStrip.Name = "pnlPatientStrip";
            this.pnlPatientStrip.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPatientStrip.Size = new System.Drawing.Size(1242, 286);
            this.pnlPatientStrip.TabIndex = 223;
            // 
            // pnlAlerts
            // 
            this.pnlAlerts.BackColor = System.Drawing.Color.Transparent;
            this.pnlAlerts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAlerts.Controls.Add(this.chkShowCrosswalk);
            this.pnlAlerts.Controls.Add(this.txtClaimRemittanceRef);
            this.pnlAlerts.Controls.Add(this.label41);
            this.pnlAlerts.Controls.Add(this.txtClaimNo);
            this.pnlAlerts.Controls.Add(this.lblClaimNo);
            this.pnlAlerts.Controls.Add(this.panel8);
            this.pnlAlerts.Controls.Add(this.label44);
            this.pnlAlerts.Controls.Add(this.label45);
            this.pnlAlerts.Controls.Add(this.label46);
            this.pnlAlerts.Controls.Add(this.label43);
            this.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAlerts.ForeColor = System.Drawing.Color.White;
            this.pnlAlerts.Location = new System.Drawing.Point(3, 0);
            this.pnlAlerts.Name = "pnlAlerts";
            this.pnlAlerts.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlAlerts.Size = new System.Drawing.Size(1236, 27);
            this.pnlAlerts.TabIndex = 208;
            // 
            // chkShowCrosswalk
            // 
            this.chkShowCrosswalk.AutoSize = true;
            this.chkShowCrosswalk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowCrosswalk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkShowCrosswalk.Location = new System.Drawing.Point(550, 6);
            this.chkShowCrosswalk.Name = "chkShowCrosswalk";
            this.chkShowCrosswalk.Size = new System.Drawing.Size(176, 18);
            this.chkShowCrosswalk.TabIndex = 217;
            this.chkShowCrosswalk.Text = "Display CPT billing crosswalk";
            this.chkShowCrosswalk.UseVisualStyleBackColor = true;
            this.chkShowCrosswalk.Visible = false;
            this.chkShowCrosswalk.CheckedChanged += new System.EventHandler(this.chkShowCrosswalk_CheckedChanged);
            // 
            // txtClaimRemittanceRef
            // 
            this.txtClaimRemittanceRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtClaimRemittanceRef.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimRemittanceRef.Location = new System.Drawing.Point(352, 4);
            this.txtClaimRemittanceRef.MaxLength = 50;
            this.txtClaimRemittanceRef.Name = "txtClaimRemittanceRef";
            this.txtClaimRemittanceRef.Size = new System.Drawing.Size(148, 22);
            this.txtClaimRemittanceRef.TabIndex = 7;
            this.txtClaimRemittanceRef.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
            this.txtClaimRemittanceRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimRemittanceRef_KeyPress);
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label41.AutoEllipsis = true;
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label41.Location = new System.Drawing.Point(189, 8);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(159, 14);
            this.label41.TabIndex = 7;
            this.label41.Text = "    Claim Remittance Ref # :";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtClaimNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txtClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimNo.ForeColor = System.Drawing.Color.Black;
            this.txtClaimNo.Location = new System.Drawing.Point(86, 4);
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.ReadOnly = true;
            this.txtClaimNo.Size = new System.Drawing.Size(98, 22);
            this.txtClaimNo.TabIndex = 214;
            this.txtClaimNo.TabStop = false;
            // 
            // lblClaimNo
            // 
            this.lblClaimNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblClaimNo.AutoEllipsis = true;
            this.lblClaimNo.AutoSize = true;
            this.lblClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblClaimNo.Location = new System.Drawing.Point(12, 8);
            this.lblClaimNo.Name = "lblClaimNo";
            this.lblClaimNo.Size = new System.Drawing.Size(71, 14);
            this.lblClaimNo.TabIndex = 215;
            this.lblClaimNo.Text = "    Claim # :";
            this.lblClaimNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lblAlertMessage);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(881, 4);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(3);
            this.panel8.Size = new System.Drawing.Size(354, 22);
            this.panel8.TabIndex = 213;
            // 
            // lblAlertMessage
            // 
            this.lblAlertMessage.AutoSize = true;
            this.lblAlertMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertMessage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAlertMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMessage.Location = new System.Drawing.Point(345, 3);
            this.lblAlertMessage.Name = "lblAlertMessage";
            this.lblAlertMessage.Padding = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.lblAlertMessage.Size = new System.Drawing.Size(6, 18);
            this.lblAlertMessage.TabIndex = 212;
            this.lblAlertMessage.Visible = false;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(1235, 4);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 22);
            this.label44.TabIndex = 62;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(0, 4);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 22);
            this.label45.TabIndex = 61;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(0, 3);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1236, 1);
            this.label46.TabIndex = 60;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(0, 26);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1236, 1);
            this.label43.TabIndex = 216;
            this.label43.Text = "Close Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(237, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "EOB/Reference # :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // txtEOBRefNumber
            // 
            this.txtEOBRefNumber.Location = new System.Drawing.Point(349, 30);
            this.txtEOBRefNumber.Name = "txtEOBRefNumber";
            this.txtEOBRefNumber.Size = new System.Drawing.Size(74, 22);
            this.txtEOBRefNumber.TabIndex = 4;
            this.txtEOBRefNumber.TabStop = false;
            this.txtEOBRefNumber.Visible = false;
            // 
            // txtCardAuthorizationNo
            // 
            this.txtCardAuthorizationNo.ForeColor = System.Drawing.Color.Black;
            this.txtCardAuthorizationNo.Location = new System.Drawing.Point(281, 3);
            this.txtCardAuthorizationNo.MaxLength = 16;
            this.txtCardAuthorizationNo.Name = "txtCardAuthorizationNo";
            this.txtCardAuthorizationNo.Size = new System.Drawing.Size(71, 22);
            this.txtCardAuthorizationNo.TabIndex = 1;
            this.txtCardAuthorizationNo.TabStop = false;
            this.txtCardAuthorizationNo.Visible = false;
            // 
            // btnLoadCheck
            // 
            this.btnLoadCheck.AutoEllipsis = true;
            this.btnLoadCheck.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoadCheck.BackgroundImage")));
            this.btnLoadCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadCheck.Image")));
            this.btnLoadCheck.Location = new System.Drawing.Point(237, 57);
            this.btnLoadCheck.Name = "btnLoadCheck";
            this.btnLoadCheck.Size = new System.Drawing.Size(22, 20);
            this.btnLoadCheck.TabIndex = 8;
            this.btnLoadCheck.TabStop = false;
            this.toolTip1.SetToolTip(this.btnLoadCheck, "Load Pending Check");
            this.btnLoadCheck.UseVisualStyleBackColor = false;
            this.btnLoadCheck.Click += new System.EventHandler(this.btnLoadCheck_Click);
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
            this.cmbPayMode.Location = new System.Drawing.Point(108, 30);
            this.cmbPayMode.Name = "cmbPayMode";
            this.cmbPayMode.Size = new System.Drawing.Size(123, 22);
            this.cmbPayMode.TabIndex = 2;
            this.cmbPayMode.SelectedIndexChanged += new System.EventHandler(this.cmbPayMode_SelectedIndexChanged);
            this.cmbPayMode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
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
            this.lblPayType.Location = new System.Drawing.Point(5, 33);
            this.lblPayType.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblPayType.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblPayType.Name = "lblPayType";
            this.lblPayType.Size = new System.Drawing.Size(100, 15);
            this.lblPayType.TabIndex = 3;
            this.lblPayType.Text = "Pay Type :";
            this.lblPayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskCreditExpiryDate
            // 
            this.mskCreditExpiryDate.Location = new System.Drawing.Point(393, 3);
            this.mskCreditExpiryDate.Mask = "00/00";
            this.mskCreditExpiryDate.Name = "mskCreditExpiryDate";
            this.mskCreditExpiryDate.Size = new System.Drawing.Size(44, 22);
            this.mskCreditExpiryDate.TabIndex = 2;
            this.mskCreditExpiryDate.TabStop = false;
            this.mskCreditExpiryDate.Visible = false;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblExpiryDate.Location = new System.Drawing.Point(355, 7);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(35, 14);
            this.lblExpiryDate.TabIndex = 34;
            this.lblExpiryDate.Text = "Exp :";
            this.lblExpiryDate.Visible = false;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(1238, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 137);
            this.label35.TabIndex = 204;
            this.label35.Text = "Close Date :";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Location = new System.Drawing.Point(3, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 137);
            this.label36.TabIndex = 203;
            this.label36.Text = "Close Date :";
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(3, 138);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1236, 1);
            this.label37.TabIndex = 202;
            this.label37.Text = "Close Date :";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(3, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1236, 1);
            this.label38.TabIndex = 201;
            this.label38.Text = "Close Date :";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.BackColor = System.Drawing.Color.Transparent;
            this.label136.Dock = System.Windows.Forms.DockStyle.Left;
            this.label136.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.ForeColor = System.Drawing.Color.Maroon;
            this.label136.Location = new System.Drawing.Point(388, 1);
            this.label136.Name = "label136";
            this.label136.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label136.Size = new System.Drawing.Size(20, 16);
            this.label136.TabIndex = 71;
            this.label136.Text = "F6";
            this.label136.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.BackColor = System.Drawing.Color.Transparent;
            this.label131.Dock = System.Windows.Forms.DockStyle.Left;
            this.label131.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(408, 1);
            this.label131.Name = "label131";
            this.label131.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label131.Size = new System.Drawing.Size(94, 16);
            this.label131.TabIndex = 72;
            this.label131.Text = "- Reason Codes";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuPayment_DistributePayment
            // 
            this.mnuPayment_DistributePayment.Name = "mnuPayment_DistributePayment";
            this.mnuPayment_DistributePayment.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuPayment_DistributePayment.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_DistributePayment.Text = "Distribute Payment";
            // 
            // mnuPayment_PaymentGrid
            // 
            this.mnuPayment_PaymentGrid.Name = "mnuPayment_PaymentGrid";
            this.mnuPayment_PaymentGrid.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuPayment_PaymentGrid.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_PaymentGrid.Text = "Payment Grid";
            this.mnuPayment_PaymentGrid.Click += new System.EventHandler(this.mnuPayment_PaymentGrid_Click);
            // 
            // mnuPayment_Save
            // 
            this.mnuPayment_Save.Name = "mnuPayment_Save";
            this.mnuPayment_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuPayment_Save.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_Save.Text = "Save";
            this.mnuPayment_Save.Click += new System.EventHandler(this.mnuPayment_Save_Click);
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.BackColor = System.Drawing.Color.Transparent;
            this.label88.Dock = System.Windows.Forms.DockStyle.Left;
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(321, 1);
            this.label88.Name = "label88";
            this.label88.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label88.Size = new System.Drawing.Size(67, 16);
            this.label88.TabIndex = 66;
            this.label88.Text = "- Prev Line";
            this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.BackColor = System.Drawing.Color.Transparent;
            this.label89.Dock = System.Windows.Forms.DockStyle.Left;
            this.label89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.ForeColor = System.Drawing.Color.Maroon;
            this.label89.Location = new System.Drawing.Point(301, 1);
            this.label89.Name = "label89";
            this.label89.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label89.Size = new System.Drawing.Size(20, 16);
            this.label89.TabIndex = 65;
            this.label89.Text = "F5";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuPayment_SavenClose
            // 
            this.mnuPayment_SavenClose.Name = "mnuPayment_SavenClose";
            this.mnuPayment_SavenClose.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.mnuPayment_SavenClose.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_SavenClose.Text = "Save&Close";
            this.mnuPayment_SavenClose.Click += new System.EventHandler(this.tls_btnSaveNClose_Click);
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.BackColor = System.Drawing.Color.Transparent;
            this.label86.Dock = System.Windows.Forms.DockStyle.Left;
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(234, 1);
            this.label86.Name = "label86";
            this.label86.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label86.Size = new System.Drawing.Size(67, 16);
            this.label86.TabIndex = 64;
            this.label86.Text = "- Next Line";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Dock = System.Windows.Forms.DockStyle.Left;
            this.label87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.ForeColor = System.Drawing.Color.Maroon;
            this.label87.Location = new System.Drawing.Point(214, 1);
            this.label87.Name = "label87";
            this.label87.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label87.Size = new System.Drawing.Size(20, 16);
            this.label87.TabIndex = 63;
            this.label87.Text = "F4";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mnuPayment_NextServiceLine
            // 
            this.mnuPayment_NextServiceLine.Name = "mnuPayment_NextServiceLine";
            this.mnuPayment_NextServiceLine.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuPayment_NextServiceLine.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_NextServiceLine.Text = "Next Line";
            this.mnuPayment_NextServiceLine.Visible = false;
            this.mnuPayment_NextServiceLine.Click += new System.EventHandler(this.mnuPayment_NextServiceLine_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Maroon;
            this.label51.Location = new System.Drawing.Point(99, 1);
            this.label51.Name = "label51";
            this.label51.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label51.Size = new System.Drawing.Size(23, 16);
            this.label51.TabIndex = 44;
            this.label51.Text = "F2 ";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 533);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1242, 3);
            this.splitter1.TabIndex = 225;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
            // 
            // mnuPayment_PrvServiceLine
            // 
            this.mnuPayment_PrvServiceLine.Name = "mnuPayment_PrvServiceLine";
            this.mnuPayment_PrvServiceLine.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuPayment_PrvServiceLine.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_PrvServiceLine.Text = "Previous Line";
            this.mnuPayment_PrvServiceLine.Visible = false;
            this.mnuPayment_PrvServiceLine.Click += new System.EventHandler(this.mnuPayment_PrvServiceLine_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(122, 1);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label15.Size = new System.Drawing.Size(92, 16);
            this.label15.TabIndex = 62;
            this.label15.Text = "- Payment Grid";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 53);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(1242, 32);
            this.panel5.TabIndex = 228;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnTraySelection);
            this.panel1.Controls.Add(this.lblPaymentTray);
            this.panel1.Controls.Add(this.mskCloseDate);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbPayType_Payment);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label90);
            this.panel1.Controls.Add(this.rbPayType_Refund);
            this.panel1.Controls.Add(this.rbExistingPayment);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1236, 26);
            this.panel1.TabIndex = 0;
            // 
            // btnTraySelection
            // 
            this.btnTraySelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraySelection.AutoEllipsis = true;
            this.btnTraySelection.BackColor = System.Drawing.Color.Transparent;
            this.btnTraySelection.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnTraySelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTraySelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraySelection.Image = ((System.Drawing.Image)(resources.GetObject("btnTraySelection.Image")));
            this.btnTraySelection.Location = new System.Drawing.Point(1210, 3);
            this.btnTraySelection.Name = "btnTraySelection";
            this.btnTraySelection.Size = new System.Drawing.Size(21, 21);
            this.btnTraySelection.TabIndex = 4;
            this.btnTraySelection.TabStop = false;
            this.toolTip1.SetToolTip(this.btnTraySelection, "Select Payment Tray");
            this.btnTraySelection.UseVisualStyleBackColor = false;
            this.btnTraySelection.Click += new System.EventHandler(this.btnTraySelection_Click);
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(949, 6);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(255, 14);
            this.lblPaymentTray.TabIndex = 207;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCloseDate.Location = new System.Drawing.Point(754, 2);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(97, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDate);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(678, 6);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 0;
            this.label42.Text = "Close Date :";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.rbPaySource_Insurance);
            this.panel7.Controls.Add(this.rbPaySource_Personal);
            this.panel7.Location = new System.Drawing.Point(347, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(191, 16);
            this.panel7.TabIndex = 206;
            this.panel7.Visible = false;
            // 
            // rbPaySource_Insurance
            // 
            this.rbPaySource_Insurance.AutoSize = true;
            this.rbPaySource_Insurance.Checked = true;
            this.rbPaySource_Insurance.Location = new System.Drawing.Point(3, -1);
            this.rbPaySource_Insurance.Name = "rbPaySource_Insurance";
            this.rbPaySource_Insurance.Size = new System.Drawing.Size(78, 18);
            this.rbPaySource_Insurance.TabIndex = 0;
            this.rbPaySource_Insurance.TabStop = true;
            this.rbPaySource_Insurance.Text = "Insurance";
            this.rbPaySource_Insurance.UseVisualStyleBackColor = true;
            // 
            // rbPaySource_Personal
            // 
            this.rbPaySource_Personal.AutoSize = true;
            this.rbPaySource_Personal.Location = new System.Drawing.Point(87, -1);
            this.rbPaySource_Personal.Name = "rbPaySource_Personal";
            this.rbPaySource_Personal.Size = new System.Drawing.Size(70, 18);
            this.rbPaySource_Personal.TabIndex = 1;
            this.rbPaySource_Personal.Text = "Personal";
            this.rbPaySource_Personal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(1235, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 24);
            this.label1.TabIndex = 205;
            this.label1.Text = "Close Date :";
            // 
            // rbPayType_Payment
            // 
            this.rbPayType_Payment.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPayType_Payment.BackColor = System.Drawing.Color.Transparent;
            this.rbPayType_Payment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPayType_Payment.BackgroundImage")));
            this.rbPayType_Payment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbPayType_Payment.Checked = true;
            this.rbPayType_Payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPayType_Payment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPayType_Payment.Location = new System.Drawing.Point(214, 2);
            this.rbPayType_Payment.Name = "rbPayType_Payment";
            this.rbPayType_Payment.Size = new System.Drawing.Size(77, 22);
            this.rbPayType_Payment.TabIndex = 0;
            this.rbPayType_Payment.TabStop = true;
            this.rbPayType_Payment.Text = "Payment";
            this.rbPayType_Payment.UseVisualStyleBackColor = false;
            this.rbPayType_Payment.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 24);
            this.label9.TabIndex = 204;
            this.label9.Text = "Close Date :";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(0, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1236, 1);
            this.label21.TabIndex = 203;
            this.label21.Text = "Close Date :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1236, 1);
            this.label22.TabIndex = 202;
            this.label22.Text = "Close Date :";
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(857, 6);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 14);
            this.label90.TabIndex = 2;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbPayType_Refund
            // 
            this.rbPayType_Refund.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPayType_Refund.BackColor = System.Drawing.Color.Transparent;
            this.rbPayType_Refund.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbPayType_Refund.BackgroundImage")));
            this.rbPayType_Refund.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbPayType_Refund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPayType_Refund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPayType_Refund.Location = new System.Drawing.Point(297, 2);
            this.rbPayType_Refund.Name = "rbPayType_Refund";
            this.rbPayType_Refund.Size = new System.Drawing.Size(75, 22);
            this.rbPayType_Refund.TabIndex = 1;
            this.rbPayType_Refund.Text = "Refund";
            this.rbPayType_Refund.UseVisualStyleBackColor = false;
            this.rbPayType_Refund.Visible = false;
            // 
            // rbExistingPayment
            // 
            this.rbExistingPayment.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbExistingPayment.BackColor = System.Drawing.Color.Transparent;
            this.rbExistingPayment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbExistingPayment.BackgroundImage")));
            this.rbExistingPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbExistingPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbExistingPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExistingPayment.Location = new System.Drawing.Point(378, 2);
            this.rbExistingPayment.Name = "rbExistingPayment";
            this.rbExistingPayment.Size = new System.Drawing.Size(74, 22);
            this.rbExistingPayment.TabIndex = 2;
            this.rbExistingPayment.Text = "Existing Payments";
            this.rbExistingPayment.UseVisualStyleBackColor = false;
            this.rbExistingPayment.Visible = false;
            // 
            // mnuPayment_ReasonCode
            // 
            this.mnuPayment_ReasonCode.Name = "mnuPayment_ReasonCode";
            this.mnuPayment_ReasonCode.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnuPayment_ReasonCode.Size = new System.Drawing.Size(199, 22);
            this.mnuPayment_ReasonCode.Text = "Reason Codes";
            this.mnuPayment_ReasonCode.Visible = false;
            this.mnuPayment_ReasonCode.Click += new System.EventHandler(this.mnuPayment_ReasonCode_Click);
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPayment_Save,
            this.mnuPayment_SavenClose,
            this.mnuPayment_PaymentGrid,
            this.mnuPayment_DistributePayment,
            this.mnuPayment_NextServiceLine,
            this.mnuPayment_ReasonCode,
            this.mnuPayment_PrvServiceLine});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(56, 1);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label17.Size = new System.Drawing.Size(43, 16);
            this.label17.TabIndex = 56;
            this.label17.Text = "- Save";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 227);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1242, 24);
            this.menuStrip1.TabIndex = 227;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // pnlTransactionOther2
            // 
            this.pnlTransactionOther2.AutoSize = true;
            this.pnlTransactionOther2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlTransactionOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionOther2.Controls.Add(this.label131);
            this.pnlTransactionOther2.Controls.Add(this.label136);
            this.pnlTransactionOther2.Controls.Add(this.label88);
            this.pnlTransactionOther2.Controls.Add(this.label89);
            this.pnlTransactionOther2.Controls.Add(this.label86);
            this.pnlTransactionOther2.Controls.Add(this.label87);
            this.pnlTransactionOther2.Controls.Add(this.label15);
            this.pnlTransactionOther2.Controls.Add(this.label51);
            this.pnlTransactionOther2.Controls.Add(this.label17);
            this.pnlTransactionOther2.Controls.Add(this.label18);
            this.pnlTransactionOther2.Controls.Add(this.label34);
            this.pnlTransactionOther2.Controls.Add(this.label33);
            this.pnlTransactionOther2.Controls.Add(this.label32);
            this.pnlTransactionOther2.Controls.Add(this.label16);
            this.pnlTransactionOther2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionOther2.Location = new System.Drawing.Point(3, 3);
            this.pnlTransactionOther2.Name = "pnlTransactionOther2";
            this.pnlTransactionOther2.Size = new System.Drawing.Size(1236, 22);
            this.pnlTransactionOther2.TabIndex = 211;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Maroon;
            this.label18.Location = new System.Drawing.Point(1, 1);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label18.Size = new System.Drawing.Size(55, 16);
            this.label18.TabIndex = 55;
            this.label18.Text = "  Ctrl + S";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1234, 1);
            this.label34.TabIndex = 60;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1234, 1);
            this.label33.TabIndex = 59;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(1235, 0);
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
            // pnlShortcut
            // 
            this.pnlShortcut.Controls.Add(this.pnlTransactionOther2);
            this.pnlShortcut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcut.Location = new System.Drawing.Point(0, 661);
            this.pnlShortcut.Name = "pnlShortcut";
            this.pnlShortcut.Padding = new System.Windows.Forms.Padding(3);
            this.pnlShortcut.Size = new System.Drawing.Size(1242, 28);
            this.pnlShortcut.TabIndex = 224;
            // 
            // btnRemoveCheck
            // 
            this.btnRemoveCheck.AutoEllipsis = true;
            this.btnRemoveCheck.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveCheck.BackgroundImage")));
            this.btnRemoveCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCheck.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveCheck.Image")));
            this.btnRemoveCheck.Location = new System.Drawing.Point(262, 57);
            this.btnRemoveCheck.Name = "btnRemoveCheck";
            this.btnRemoveCheck.Size = new System.Drawing.Size(22, 20);
            this.btnRemoveCheck.TabIndex = 9;
            this.btnRemoveCheck.TabStop = false;
            this.toolTip1.SetToolTip(this.btnRemoveCheck, "Remove Check ");
            this.btnRemoveCheck.UseVisualStyleBackColor = false;
            this.btnRemoveCheck.Click += new System.EventHandler(this.btnRemoveCheck_Click);
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.ForeColor = System.Drawing.Color.Black;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Items.AddRange(new object[] {
            "Card 1",
            "Card 2"});
            this.cmbCardType.Location = new System.Drawing.Point(96, 2);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(133, 22);
            this.cmbCardType.TabIndex = 0;
            this.cmbCardType.TabStop = false;
            this.cmbCardType.Visible = false;
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
            this.lblCheckNo.Location = new System.Drawing.Point(5, 59);
            this.lblCheckNo.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblCheckNo.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblCheckNo.Name = "lblCheckNo";
            this.lblCheckNo.Size = new System.Drawing.Size(100, 15);
            this.lblCheckNo.TabIndex = 4;
            this.lblCheckNo.Text = "Check # :";
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
            this.lblCheckDate.Location = new System.Drawing.Point(5, 85);
            this.lblCheckDate.MaximumSize = new System.Drawing.Size(100, 15);
            this.lblCheckDate.MinimumSize = new System.Drawing.Size(100, 15);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(100, 15);
            this.lblCheckDate.TabIndex = 6;
            this.lblCheckDate.Text = "Check Date :";
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayer
            // 
            this.lblPayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.lblPayer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayer.Location = new System.Drawing.Point(108, 7);
            this.lblPayer.MaximumSize = new System.Drawing.Size(313, 15);
            this.lblPayer.MinimumSize = new System.Drawing.Size(313, 15);
            this.lblPayer.Name = "lblPayer";
            this.lblPayer.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblPayer.Size = new System.Drawing.Size(313, 15);
            this.lblPayer.TabIndex = 3;
            // 
            // label39
            // 
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(317, 8);
            this.label39.MaximumSize = new System.Drawing.Size(120, 15);
            this.label39.MinimumSize = new System.Drawing.Size(120, 15);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(120, 15);
            this.label39.TabIndex = 3;
            this.label39.Text = "Payment # :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskCheckDate
            // 
            this.mskCheckDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCheckDate.Location = new System.Drawing.Point(108, 82);
            this.mskCheckDate.Mask = "00/00/0000";
            this.mskCheckDate.Name = "mskCheckDate";
            this.mskCheckDate.Size = new System.Drawing.Size(123, 22);
            this.mskCheckDate.TabIndex = 4;
            this.mskCheckDate.ValidatingType = typeof(System.DateTime);
            this.mskCheckDate.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateDate);
            this.mskCheckDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateMouseClick);
            this.mskCheckDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsCompany.AutoEllipsis = true;
            this.lblInsCompany.AutoSize = true;
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(108, 9);
            this.lblInsCompany.MaximumSize = new System.Drawing.Size(313, 15);
            this.lblInsCompany.MinimumSize = new System.Drawing.Size(313, 15);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(313, 15);
            this.lblInsCompany.TabIndex = 210;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearchInsuranceCompany
            // 
            this.btnSearchInsuranceCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchInsuranceCompany.AutoEllipsis = true;
            this.btnSearchInsuranceCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchInsuranceCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.BackgroundImage")));
            this.btnSearchInsuranceCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchInsuranceCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchInsuranceCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchInsuranceCompany.Image")));
            this.btnSearchInsuranceCompany.Location = new System.Drawing.Point(426, 5);
            this.btnSearchInsuranceCompany.Name = "btnSearchInsuranceCompany";
            this.btnSearchInsuranceCompany.Size = new System.Drawing.Size(21, 21);
            this.btnSearchInsuranceCompany.TabIndex = 0;
            this.btnSearchInsuranceCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSearchInsuranceCompany, "Add Insurance Company");
            this.btnSearchInsuranceCompany.UseVisualStyleBackColor = false;
            this.btnSearchInsuranceCompany.Click += new System.EventHandler(this.btnSearchInsuranceCompany_Click);
            this.btnSearchInsuranceCompany.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
            // 
            // tls_btnViewEOB
            // 
            this.tls_btnViewEOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnViewEOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnViewEOB.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnViewEOB.Image")));
            this.tls_btnViewEOB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnViewEOB.Name = "tls_btnViewEOB";
            this.tls_btnViewEOB.Size = new System.Drawing.Size(66, 50);
            this.tls_btnViewEOB.Tag = "HideEOB";
            this.tls_btnViewEOB.Text = "&Hide EOB";
            this.tls_btnViewEOB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnViewEOB.ToolTipText = "Hide Explanation of Benefit";
            this.tls_btnViewEOB.Click += new System.EventHandler(this.tls_btnViewEOB_Click);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.pnlCredit);
            this.panel16.Location = new System.Drawing.Point(454, 12);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel16.Size = new System.Drawing.Size(476, 30);
            this.panel16.TabIndex = 205;
            this.panel16.Visible = false;
            // 
            // pnlCredit
            // 
            this.pnlCredit.Controls.Add(this.mskCreditExpiryDate);
            this.pnlCredit.Controls.Add(this.cmbCardType);
            this.pnlCredit.Controls.Add(this.lblExpiryDate);
            this.pnlCredit.Controls.Add(this.lblCardAuthorizationNo);
            this.pnlCredit.Controls.Add(this.lblCardType);
            this.pnlCredit.Controls.Add(this.txtCardAuthorizationNo);
            this.pnlCredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCredit.Location = new System.Drawing.Point(3, 0);
            this.pnlCredit.Name = "pnlCredit";
            this.pnlCredit.Size = new System.Drawing.Size(470, 27);
            this.pnlCredit.TabIndex = 0;
            // 
            // lblCardAuthorizationNo
            // 
            this.lblCardAuthorizationNo.AutoSize = true;
            this.lblCardAuthorizationNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardAuthorizationNo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardAuthorizationNo.Location = new System.Drawing.Point(231, 7);
            this.lblCardAuthorizationNo.Name = "lblCardAuthorizationNo";
            this.lblCardAuthorizationNo.Size = new System.Drawing.Size(47, 14);
            this.lblCardAuthorizationNo.TabIndex = 108;
            this.lblCardAuthorizationNo.Text = "Auth#:";
            this.lblCardAuthorizationNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCardAuthorizationNo.Visible = false;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCardType.Location = new System.Drawing.Point(23, 6);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(71, 14);
            this.lblCardType.TabIndex = 36;
            this.lblCardType.Text = "Card Type :";
            this.lblCardType.Visible = false;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Controls.Add(this.panel16);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1242, 53);
            this.pnlToolStrip.TabIndex = 221;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnNew,
            this.tls_btnViewEOB,
            this.tls_InsuranceLog,
            this.tsb_Refund,
            this.tls_btnCharge,
            this.tls_btnPatAcct,
            this.tsb_PaymentPatient,
            this.tls_btnSave,
            this.tls_btnSaveNClose,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1242, 53);
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
            // tls_InsuranceLog
            // 
            this.tls_InsuranceLog.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_InsuranceLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_InsuranceLog.Image = ((System.Drawing.Image)(resources.GetObject("tls_InsuranceLog.Image")));
            this.tls_InsuranceLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_InsuranceLog.Name = "tls_InsuranceLog";
            this.tls_InsuranceLog.Size = new System.Drawing.Size(156, 50);
            this.tls_InsuranceLog.Tag = "Log";
            this.tls_InsuranceLog.Text = "&Insurance Payment Log";
            this.tls_InsuranceLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_InsuranceLog.ToolTipText = "Insurance Payment Log";
            this.tls_InsuranceLog.Click += new System.EventHandler(this.tls_InsuranceLog_Click);
            // 
            // tsb_Refund
            // 
            this.tsb_Refund.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Refund.Image = global::gloBilling.Properties.Resources.Refund;
            this.tsb_Refund.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refund.Name = "tsb_Refund";
            this.tsb_Refund.Size = new System.Drawing.Size(62, 50);
            this.tsb_Refund.Tag = "Refunds";
            this.tsb_Refund.Text = "Re&funds";
            this.tsb_Refund.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refund.ToolTipText = "Refunds";
            this.tsb_Refund.Click += new System.EventHandler(this.tsb_Refund_Click);
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
            // tls_btnPatAcct
            // 
            this.tls_btnPatAcct.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPatAcct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPatAcct.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPatAcct.Image")));
            this.tls_btnPatAcct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPatAcct.Name = "tls_btnPatAcct";
            this.tls_btnPatAcct.Size = new System.Drawing.Size(71, 50);
            this.tls_btnPatAcct.Tag = "Patient Account";
            this.tls_btnPatAcct.Text = "Pat. Acct.";
            this.tls_btnPatAcct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPatAcct.ToolTipText = "Patient Account";
            this.tls_btnPatAcct.Click += new System.EventHandler(this.tls_btnPatAcct_Click);
            // 
            // tsb_PaymentPatient
            // 
            this.tsb_PaymentPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PaymentPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PaymentPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentPatient.Image")));
            this.tsb_PaymentPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PaymentPatient.Name = "tsb_PaymentPatient";
            this.tsb_PaymentPatient.Size = new System.Drawing.Size(114, 50);
            this.tsb_PaymentPatient.Tag = "Patient Payment";
            this.tsb_PaymentPatient.Text = "Patient Pa&yment";
            this.tsb_PaymentPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PaymentPatient.ToolTipText = "Patient Payment";
            this.tsb_PaymentPatient.Click += new System.EventHandler(this.tsb_PaymentPatient_Click);
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
            this.tls_btnSaveNClose.Visible = false;
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
            // btnSetupJournal
            // 
            this.btnSetupJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetupJournal.AutoEllipsis = true;
            this.btnSetupJournal.BackColor = System.Drawing.Color.Transparent;
            this.btnSetupJournal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.BackgroundImage")));
            this.btnSetupJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetupJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupJournal.Image = ((System.Drawing.Image)(resources.GetObject("btnSetupJournal.Image")));
            this.btnSetupJournal.Location = new System.Drawing.Point(362, 54);
            this.btnSetupJournal.Name = "btnSetupJournal";
            this.btnSetupJournal.Size = new System.Drawing.Size(21, 21);
            this.btnSetupJournal.TabIndex = 4;
            this.btnSetupJournal.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSetupJournal, "Add Payment Tray");
            this.btnSetupJournal.UseVisualStyleBackColor = false;
            this.btnSetupJournal.Visible = false;
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
            this.btnModifyJournal.Location = new System.Drawing.Point(345, 56);
            this.btnModifyJournal.Name = "btnModifyJournal";
            this.btnModifyJournal.Size = new System.Drawing.Size(21, 21);
            this.btnModifyJournal.TabIndex = 5;
            this.btnModifyJournal.TabStop = false;
            this.toolTip1.SetToolTip(this.btnModifyJournal, "Modify Payment Tray");
            this.btnModifyJournal.UseVisualStyleBackColor = false;
            this.btnModifyJournal.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(5, 8);
            this.label2.MaximumSize = new System.Drawing.Size(100, 15);
            this.label2.MinimumSize = new System.Drawing.Size(100, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ins. Company :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSameChkReserveAmount
            // 
            this.lblSameChkReserveAmount.BackColor = System.Drawing.Color.MintCream;
            this.lblSameChkReserveAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSameChkReserveAmount.ForeColor = System.Drawing.Color.Green;
            this.lblSameChkReserveAmount.Location = new System.Drawing.Point(10, 59);
            this.lblSameChkReserveAmount.Name = "lblSameChkReserveAmount";
            this.lblSameChkReserveAmount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblSameChkReserveAmount.Size = new System.Drawing.Size(34, 21);
            this.lblSameChkReserveAmount.TabIndex = 211;
            this.lblSameChkReserveAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSameChkReserveAmount.Visible = false;
            // 
            // lblReserveRemaining
            // 
            this.lblReserveRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReserveRemaining.AutoEllipsis = true;
            this.lblReserveRemaining.AutoSize = true;
            this.lblReserveRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblReserveRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveRemaining.ForeColor = System.Drawing.Color.Green;
            this.lblReserveRemaining.Location = new System.Drawing.Point(270, 82);
            this.lblReserveRemaining.MaximumSize = new System.Drawing.Size(122, 21);
            this.lblReserveRemaining.MinimumSize = new System.Drawing.Size(122, 21);
            this.lblReserveRemaining.Name = "lblReserveRemaining";
            this.lblReserveRemaining.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblReserveRemaining.Size = new System.Drawing.Size(122, 21);
            this.lblReserveRemaining.TabIndex = 209;
            this.lblReserveRemaining.Text = "0.00";
            this.lblReserveRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReserveRemaining.Visible = false;
            // 
            // lblCorrectionAmt
            // 
            this.lblCorrectionAmt.BackColor = System.Drawing.Color.MintCream;
            this.lblCorrectionAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrectionAmt.ForeColor = System.Drawing.Color.Green;
            this.lblCorrectionAmt.Location = new System.Drawing.Point(10, 31);
            this.lblCorrectionAmt.Name = "lblCorrectionAmt";
            this.lblCorrectionAmt.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCorrectionAmt.Size = new System.Drawing.Size(34, 21);
            this.lblCorrectionAmt.TabIndex = 210;
            this.lblCorrectionAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCorrectionAmt.Visible = false;
            // 
            // txtCheckNumber
            // 
            this.txtCheckNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheckNumber.Location = new System.Drawing.Point(108, 56);
            this.txtCheckNumber.Name = "txtCheckNumber";
            this.txtCheckNumber.Size = new System.Drawing.Size(123, 22);
            this.txtCheckNumber.TabIndex = 3;
            this.txtCheckNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
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
            this.lblPaymetNo.Location = new System.Drawing.Point(440, 8);
            this.lblPaymetNo.Name = "lblPaymetNo";
            this.lblPaymetNo.Size = new System.Drawing.Size(41, 14);
            this.lblPaymetNo.TabIndex = 208;
            this.lblPaymetNo.Text = "PayNo";
            this.lblPaymetNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPayMstNotes
            // 
            this.txtPayMstNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayMstNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayMstNotes.ForeColor = System.Drawing.Color.Black;
            this.txtPayMstNotes.Location = new System.Drawing.Point(440, 30);
            this.txtPayMstNotes.MaxLength = 255;
            this.txtPayMstNotes.Multiline = true;
            this.txtPayMstNotes.Name = "txtPayMstNotes";
            this.txtPayMstNotes.Size = new System.Drawing.Size(332, 96);
            this.txtPayMstNotes.TabIndex = 5;
            this.txtPayMstNotes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
            // 
            // lblReserveAmount
            // 
            this.lblReserveAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReserveAmount.AutoEllipsis = true;
            this.lblReserveAmount.AutoSize = true;
            this.lblReserveAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblReserveAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReserveAmount.ForeColor = System.Drawing.Color.Green;
            this.lblReserveAmount.Location = new System.Drawing.Point(116, 57);
            this.lblReserveAmount.MaximumSize = new System.Drawing.Size(143, 18);
            this.lblReserveAmount.MinimumSize = new System.Drawing.Size(143, 18);
            this.lblReserveAmount.Name = "lblReserveAmount";
            this.lblReserveAmount.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblReserveAmount.Size = new System.Drawing.Size(143, 18);
            this.lblReserveAmount.TabIndex = 15;
            this.lblReserveAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(317, 30);
            this.label3.MaximumSize = new System.Drawing.Size(120, 15);
            this.label3.MinimumSize = new System.Drawing.Size(120, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 15);
            this.label3.TabIndex = 205;
            this.label3.Text = "Note :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPayMstIncludeNotes
            // 
            this.chkPayMstIncludeNotes.AutoSize = true;
            this.chkPayMstIncludeNotes.Location = new System.Drawing.Point(616, 5);
            this.chkPayMstIncludeNotes.Name = "chkPayMstIncludeNotes";
            this.chkPayMstIncludeNotes.Size = new System.Drawing.Size(160, 18);
            this.chkPayMstIncludeNotes.TabIndex = 207;
            this.chkPayMstIncludeNotes.TabStop = false;
            this.chkPayMstIncludeNotes.Text = "Include Note on Receipt";
            this.chkPayMstIncludeNotes.UseVisualStyleBackColor = true;
            this.chkPayMstIncludeNotes.Visible = false;
            // 
            // btnUseReserve
            // 
            this.btnUseReserve.AutoEllipsis = true;
            this.btnUseReserve.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUseReserve.BackgroundImage")));
            this.btnUseReserve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUseReserve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUseReserve.Location = new System.Drawing.Point(270, 54);
            this.btnUseReserve.Name = "btnUseReserve";
            this.btnUseReserve.Size = new System.Drawing.Size(119, 24);
            this.btnUseReserve.TabIndex = 6;
            this.btnUseReserve.TabStop = false;
            this.btnUseReserve.Text = "Use Reserves";
            this.btnUseReserve.UseVisualStyleBackColor = true;
            this.btnUseReserve.Click += new System.EventHandler(this.btnUseReserve_Click);
            // 
            // btnReserveRemaining
            // 
            this.btnReserveRemaining.AutoEllipsis = true;
            this.btnReserveRemaining.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReserveRemaining.BackgroundImage")));
            this.btnReserveRemaining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReserveRemaining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReserveRemaining.Location = new System.Drawing.Point(270, 106);
            this.btnReserveRemaining.Name = "btnReserveRemaining";
            this.btnReserveRemaining.Size = new System.Drawing.Size(119, 24);
            this.btnReserveRemaining.TabIndex = 7;
            this.btnReserveRemaining.TabStop = false;
            this.btnReserveRemaining.Text = "Reserve Remaining";
            this.btnReserveRemaining.UseVisualStyleBackColor = true;
            this.btnReserveRemaining.Click += new System.EventHandler(this.btnReserveRemaining_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblInsCompany);
            this.panel3.Controls.Add(this.btnSearchInsuranceCompany);
            this.panel3.Controls.Add(this.btnRemoveCheck);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtEOBRefNumber);
            this.panel3.Controls.Add(this.btnLoadCheck);
            this.panel3.Controls.Add(this.lblCheckNo);
            this.panel3.Controls.Add(this.lblCheckDate);
            this.panel3.Controls.Add(this.cmbPayMode);
            this.panel3.Controls.Add(this.lblPayer);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.mskCheckDate);
            this.panel3.Controls.Add(this.txtCheckNumber);
            this.panel3.Controls.Add(this.lblPayType);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(454, 137);
            this.panel3.TabIndex = 0;
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
            this.btnDistubuteAmount.Location = new System.Drawing.Point(270, 4);
            this.btnDistubuteAmount.Name = "btnDistubuteAmount";
            this.btnDistubuteAmount.Size = new System.Drawing.Size(21, 21);
            this.btnDistubuteAmount.TabIndex = 8;
            this.btnDistubuteAmount.TabStop = false;
            this.toolTip1.SetToolTip(this.btnDistubuteAmount, "Distribute Amount");
            this.btnDistubuteAmount.UseVisualStyleBackColor = false;
            this.btnDistubuteAmount.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label35);
            this.panel2.Controls.Add(this.label36);
            this.panel2.Controls.Add(this.label37);
            this.panel2.Controls.Add(this.label38);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 85);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1242, 142);
            this.panel2.TabIndex = 222;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblReserveRemaining);
            this.panel4.Controls.Add(this.lblPaymetNo);
            this.panel4.Controls.Add(this.txtPayMstNotes);
            this.panel4.Controls.Add(this.lblReserveAmount);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.chkPayMstIncludeNotes);
            this.panel4.Controls.Add(this.label39);
            this.panel4.Controls.Add(this.btnUseReserve);
            this.panel4.Controls.Add(this.btnReserveRemaining);
            this.panel4.Controls.Add(this.btnDistubuteAmount);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lblTakeBackAmt);
            this.panel4.Controls.Add(this.btnSetupJournal);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lblTotalFunds);
            this.panel4.Controls.Add(this.btnModifyJournal);
            this.panel4.Controls.Add(this.label40);
            this.panel4.Controls.Add(this.txtCheckRemaining);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.txtCheckAmount);
            this.panel4.Controls.Add(this.lblSameChkReserveAmount);
            this.panel4.Controls.Add(this.lblCorrectionAmt);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(458, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 137);
            this.panel4.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(10, 7);
            this.label7.MaximumSize = new System.Drawing.Size(100, 15);
            this.label7.MinimumSize = new System.Drawing.Size(100, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Amount :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTakeBackAmt
            // 
            this.lblTakeBackAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTakeBackAmt.AutoEllipsis = true;
            this.lblTakeBackAmt.AutoSize = true;
            this.lblTakeBackAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblTakeBackAmt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTakeBackAmt.ForeColor = System.Drawing.Color.Green;
            this.lblTakeBackAmt.Location = new System.Drawing.Point(116, 31);
            this.lblTakeBackAmt.MaximumSize = new System.Drawing.Size(143, 18);
            this.lblTakeBackAmt.MinimumSize = new System.Drawing.Size(143, 18);
            this.lblTakeBackAmt.Name = "lblTakeBackAmt";
            this.lblTakeBackAmt.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTakeBackAmt.Size = new System.Drawing.Size(143, 18);
            this.lblTakeBackAmt.TabIndex = 3;
            this.lblTakeBackAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(10, 59);
            this.label6.MaximumSize = new System.Drawing.Size(100, 15);
            this.label6.MinimumSize = new System.Drawing.Size(100, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Reserves Used :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(10, 33);
            this.label4.MaximumSize = new System.Drawing.Size(100, 15);
            this.label4.MinimumSize = new System.Drawing.Size(100, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Take Backs :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalFunds
            // 
            this.lblTotalFunds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalFunds.AutoEllipsis = true;
            this.lblTotalFunds.AutoSize = true;
            this.lblTotalFunds.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalFunds.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFunds.ForeColor = System.Drawing.Color.Green;
            this.lblTotalFunds.Location = new System.Drawing.Point(116, 83);
            this.lblTotalFunds.MaximumSize = new System.Drawing.Size(143, 18);
            this.lblTotalFunds.MinimumSize = new System.Drawing.Size(143, 18);
            this.lblTotalFunds.Name = "lblTotalFunds";
            this.lblTotalFunds.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblTotalFunds.Size = new System.Drawing.Size(143, 18);
            this.lblTotalFunds.TabIndex = 3;
            this.lblTotalFunds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(10, 85);
            this.label40.MaximumSize = new System.Drawing.Size(100, 15);
            this.label40.MinimumSize = new System.Drawing.Size(100, 15);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(100, 15);
            this.label40.TabIndex = 7;
            this.label40.Text = "Total Funds :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckRemaining
            // 
            this.txtCheckRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheckRemaining.AutoEllipsis = true;
            this.txtCheckRemaining.AutoSize = true;
            this.txtCheckRemaining.BackColor = System.Drawing.Color.Transparent;
            this.txtCheckRemaining.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckRemaining.ForeColor = System.Drawing.Color.Green;
            this.txtCheckRemaining.Location = new System.Drawing.Point(116, 109);
            this.txtCheckRemaining.MaximumSize = new System.Drawing.Size(143, 18);
            this.txtCheckRemaining.MinimumSize = new System.Drawing.Size(143, 18);
            this.txtCheckRemaining.Name = "txtCheckRemaining";
            this.txtCheckRemaining.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.txtCheckRemaining.Size = new System.Drawing.Size(143, 18);
            this.txtCheckRemaining.TabIndex = 3;
            this.txtCheckRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(10, 111);
            this.label8.MaximumSize = new System.Drawing.Size(100, 15);
            this.label8.MinimumSize = new System.Drawing.Size(100, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Remaining :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckAmount
            // 
            this.txtCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckAmount.Location = new System.Drawing.Point(115, 3);
            this.txtCheckAmount.MaxLength = 13;
            this.txtCheckAmount.Name = "txtCheckAmount";
            this.txtCheckAmount.ShortcutsEnabled = false;
            this.txtCheckAmount.Size = new System.Drawing.Size(148, 22);
            this.txtCheckAmount.TabIndex = 1;
            this.txtCheckAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCheckAmount.TextChanged += new System.EventHandler(this.txtCheckAmount_TextChanged);
            this.txtCheckAmount.Leave += new System.EventHandler(this.txtCheckAmount_Leave);
            this.txtCheckAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MoveCursorOnEnter);
            this.txtCheckAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckAmount_KeyPress);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.c1ClaimDetails);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 513);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel6.Size = new System.Drawing.Size(1242, 20);
            this.panel6.TabIndex = 229;
            this.panel6.Visible = false;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(1238, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 15);
            this.label25.TabIndex = 200;
            this.label25.Text = "Close Date :";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(3, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 15);
            this.label12.TabIndex = 199;
            this.label12.Text = "Close Date :";
            // 
            // c1ClaimDetails
            // 
            this.c1ClaimDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ClaimDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ClaimDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ClaimDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ClaimDetails.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1ClaimDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ClaimDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ClaimDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ClaimDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimDetails.Location = new System.Drawing.Point(3, 4);
            this.c1ClaimDetails.Name = "c1ClaimDetails";
            this.c1ClaimDetails.Rows.Count = 1;
            this.c1ClaimDetails.Rows.DefaultSize = 19;
            this.c1ClaimDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ClaimDetails.Size = new System.Drawing.Size(1236, 15);
            this.c1ClaimDetails.StyleInfo = resources.GetString("c1ClaimDetails.StyleInfo");
            this.c1ClaimDetails.TabIndex = 88;
            this.c1ClaimDetails.TabStop = false;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(3, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1236, 1);
            this.label24.TabIndex = 198;
            this.label24.Text = "Close Date :";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Location = new System.Drawing.Point(3, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1236, 1);
            this.label23.TabIndex = 197;
            this.label23.Text = "Close Date :";
            // 
            // frmInsurancePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1242, 689);
            this.Controls.Add(this.pnlPatientStrip);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlMultiplePayment);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlShortcut);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsurancePayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Payment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInsurancePayment_Load);
            this.pnlSinglePayment.ResumeLayout(false);
            this.pnlSinglePaymentAllocation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePayment)).EndInit();
            this.pnlSinglePaymentAllocationHdr.ResumeLayout(false);
            this.pnlSinglePaymentAllocationHdr.PerformLayout();
            this.pnlSinglePaymentCorrTB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentCorrTB)).EndInit();
            this.pnlSinglePaymentCorrTBHdr.ResumeLayout(false);
            this.pnlSinglePaymentCorrTBHdr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SinglePaymentTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePayment)).EndInit();
            this.pnlMultiplePayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1MultiplePaymentTotal)).EndInit();
            this.pnlPatientStrip.ResumeLayout(false);
            this.pnlAlerts.ResumeLayout(false);
            this.pnlAlerts.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlTransactionOther2.ResumeLayout(false);
            this.pnlTransactionOther2.PerformLayout();
            this.pnlShortcut.ResumeLayout(false);
            this.pnlShortcut.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.pnlCredit.ResumeLayout(false);
            this.pnlCredit.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSinglePayment;
        private System.Windows.Forms.Label label13;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePaymentTotal;
        private System.Windows.Forms.Label label30;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePayment;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MultiplePayment;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlMultiplePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1MultiplePaymentTotal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel pnlPatientStrip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEOBRefNumber;
        private System.Windows.Forms.TextBox txtCardAuthorizationNo;
        private System.Windows.Forms.Button btnLoadCheck;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbPayMode;
        private System.Windows.Forms.Label lblPayType;
        private System.Windows.Forms.MaskedTextBox mskCreditExpiryDate;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_DistributePayment;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_PaymentGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_Save;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_SavenClose;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_NextServiceLine;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_PrvServiceLine;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTraySelection;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton rbPaySource_Insurance;
        private System.Windows.Forms.RadioButton rbPaySource_Personal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbPayType_Payment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.RadioButton rbPayType_Refund;
        private System.Windows.Forms.RadioButton rbExistingPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuPayment_ReasonCode;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel pnlTransactionOther2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlShortcut;
        private System.Windows.Forms.Button btnRemoveCheck;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label lblCheckNo;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.Label lblPayer;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.MaskedTextBox mskCheckDate;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.Button btnSearchInsuranceCompany;
        private System.Windows.Forms.ToolStripButton tls_btnViewEOB;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel pnlCredit;
        private System.Windows.Forms.Label lblCardAuthorizationNo;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnNew;
        private System.Windows.Forms.ToolStripButton tls_btnCharge;
        private System.Windows.Forms.ToolStripButton tls_btnSave;
        private System.Windows.Forms.ToolStripButton tls_btnSaveNClose;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Button btnSetupJournal;
        private System.Windows.Forms.Button btnModifyJournal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSameChkReserveAmount;
        private System.Windows.Forms.Label lblReserveRemaining;
        private System.Windows.Forms.Label lblCorrectionAmt;
        private System.Windows.Forms.TextBox txtCheckNumber;
        private System.Windows.Forms.Label lblPaymetNo;
        private System.Windows.Forms.TextBox txtPayMstNotes;
        private System.Windows.Forms.Label lblReserveAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkPayMstIncludeNotes;
        private System.Windows.Forms.Button btnUseReserve;
        private System.Windows.Forms.Button btnReserveRemaining;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDistubuteAmount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTakeBackAmt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalFunds;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label txtCheckRemaining;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCheckAmount;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label12;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ClaimDetails;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtClaimRemittanceRef;
        private System.Windows.Forms.Panel pnlAlerts;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label lblAlertMessage;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtClaimNo;
        private System.Windows.Forms.Label lblClaimNo;
        private System.Windows.Forms.ToolStripButton tls_InsuranceLog;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.CheckBox chkShowCrosswalk;
        private System.Windows.Forms.ToolStripButton tsb_PaymentPatient;
        private System.Windows.Forms.ToolStripButton tsb_Refund;
        private System.Windows.Forms.ToolStripButton tls_btnPatAcct;
        private System.Windows.Forms.Panel pnlSinglePaymentCorrTB;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SinglePaymentCorrTB;
        private System.Windows.Forms.Panel pnlSinglePaymentAllocation;
        private System.Windows.Forms.Panel pnlSinglePaymentAllocationHdr;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Panel pnlSinglePaymentCorrTBHdr;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
    }
}