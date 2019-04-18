namespace gloBilling
{
    partial class frmEOBPaymentReasonCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEOBPaymentReasonCode));
            this.pnltls_Notes = new System.Windows.Forms.Panel();
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.tls_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_SavenClose = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlReason = new System.Windows.Forms.Panel();
            this.ReasonGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.c1ReasonAmountTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTransactionOther2 = new System.Windows.Forms.Panel();
            this.lblFillAmount = new System.Windows.Forms.Label();
            this.lblCntrlF = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_RemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlInternalNotes = new System.Windows.Forms.Panel();
            this.txtInternalNotes = new System.Windows.Forms.TextBox();
            this.chkInternalNotes = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlStatementNotes = new System.Windows.Forms.Panel();
            this.txtStatementNotes = new System.Windows.Forms.TextBox();
            this.chkStatementNotes = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlTransactionOther2Hdr = new System.Windows.Forms.Panel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlReasonCodeHdr = new System.Windows.Forms.Panel();
            this.pnlReasonCodemain = new System.Windows.Forms.Panel();
            this.lblReasonCodemain = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlRemarkCodeHdr = new System.Windows.Forms.Panel();
            this.pnlRemarkCodemain = new System.Windows.Forms.Panel();
            this.lblRemarkCodemain = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlRemarkCode = new System.Windows.Forms.Panel();
            this.pnlInternalControlRmk = new System.Windows.Forms.Panel();
            this.RemarkGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.pnlAmountHeader = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlPendingAmount = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.lblPendingAmount = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblAmountHeader = new System.Windows.Forms.Label();
            this.lblReasoncodeType = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.pnltls_Notes.SuspendLayout();
            this.tls_Notes.SuspendLayout();
            this.pnlReason.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReasonGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ReasonAmountTotal)).BeginInit();
            this.pnlTransactionOther2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlInternalNotes.SuspendLayout();
            this.pnlStatementNotes.SuspendLayout();
            this.pnlTransactionOther2Hdr.SuspendLayout();
            this.pnlReasonCodeHdr.SuspendLayout();
            this.pnlReasonCodemain.SuspendLayout();
            this.pnlRemarkCodeHdr.SuspendLayout();
            this.pnlRemarkCodemain.SuspendLayout();
            this.pnlRemarkCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkGrid)).BeginInit();
            this.pnlAmountHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPendingAmount.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltls_Notes
            // 
            this.pnltls_Notes.Controls.Add(this.tls_Notes);
            this.pnltls_Notes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltls_Notes.Location = new System.Drawing.Point(0, 0);
            this.pnltls_Notes.Name = "pnltls_Notes";
            this.pnltls_Notes.Size = new System.Drawing.Size(613, 54);
            this.pnltls_Notes.TabIndex = 0;
            this.pnltls_Notes.TabStop = true;
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Notes.BackgroundImage")));
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnAddLine,
            this.tls_btnRemoveLine,
            this.toolStripSeparator1,
            this.tlb_SavenClose,
            this.tlb_Cancel});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(613, 53);
            this.tls_Notes.TabIndex = 0;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tls_btnAddLine
            // 
            this.tls_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnAddLine.Image")));
            this.tls_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnAddLine.Name = "tls_btnAddLine";
            this.tls_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.tls_btnAddLine.Tag = "AddLine";
            this.tls_btnAddLine.Text = "&Add Line";
            this.tls_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnAddLine.Click += new System.EventHandler(this.tls_btnAddLine_Click);
            // 
            // tls_btnRemoveLine
            // 
            this.tls_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnRemoveLine.Image")));
            this.tls_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnRemoveLine.Name = "tls_btnRemoveLine";
            this.tls_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.tls_btnRemoveLine.Tag = "RemoveLine";
            this.tls_btnRemoveLine.Text = "Re&move Line";
            this.tls_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnRemoveLine.Click += new System.EventHandler(this.tls_btnRemoveLine_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // tlb_SavenClose
            // 
            this.tlb_SavenClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SavenClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SavenClose.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SavenClose.Image")));
            this.tlb_SavenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SavenClose.Name = "tlb_SavenClose";
            this.tlb_SavenClose.Size = new System.Drawing.Size(66, 50);
            this.tlb_SavenClose.Tag = "OK";
            this.tlb_SavenClose.Text = "Sa&ve&&Cls";
            this.tlb_SavenClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SavenClose.ToolTipText = "Save and Close";
            this.tlb_SavenClose.Click += new System.EventHandler(this.tlb_SavenClose_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // pnlReason
            // 
            this.pnlReason.BackColor = System.Drawing.Color.Transparent;
            this.pnlReason.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReason.Controls.Add(this.ReasonGrid);
            this.pnlReason.Controls.Add(this.label23);
            this.pnlReason.Controls.Add(this.label18);
            this.pnlReason.Controls.Add(this.c1ReasonAmountTotal);
            this.pnlReason.Controls.Add(this.pnlInternalControl);
            this.pnlReason.Controls.Add(this.label4);
            this.pnlReason.Controls.Add(this.label6);
            this.pnlReason.Controls.Add(this.label7);
            this.pnlReason.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlReason.Location = new System.Drawing.Point(0, 106);
            this.pnlReason.Name = "pnlReason";
            this.pnlReason.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlReason.Size = new System.Drawing.Size(613, 211);
            this.pnlReason.TabIndex = 1;
            // 
            // ReasonGrid
            // 
            this.ReasonGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.ReasonGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ReasonGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ReasonGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.ReasonGrid.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.ReasonGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReasonGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ReasonGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.ReasonGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.ReasonGrid.Location = new System.Drawing.Point(4, 1);
            this.ReasonGrid.Name = "ReasonGrid";
            this.ReasonGrid.Rows.Count = 1;
            this.ReasonGrid.Rows.DefaultSize = 19;
            this.ReasonGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.ReasonGrid.Size = new System.Drawing.Size(605, 187);
            this.ReasonGrid.StyleInfo = resources.GetString("ReasonGrid.StyleInfo");
            this.ReasonGrid.TabIndex = 0;
            this.ReasonGrid.SelChange += new System.EventHandler(this.ReasonGrid_SelChange);
            this.ReasonGrid.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_StartEdit);
            this.ReasonGrid.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_AfterEdit);
            this.ReasonGrid.ChangeEdit += new System.EventHandler(this.ReasonGrid_ChangeEdit);
            this.ReasonGrid.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_CellChanged);
            this.ReasonGrid.Enter += new System.EventHandler(this.ReasonGrid_Enter);
            this.ReasonGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReasonGrid_KeyUp);
            this.ReasonGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReasonGrid_MouseMove);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(4, 188);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(605, 1);
            this.label23.TabIndex = 61;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(605, 1);
            this.label18.TabIndex = 60;
            // 
            // c1ReasonAmountTotal
            // 
            this.c1ReasonAmountTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ReasonAmountTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ReasonAmountTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ReasonAmountTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ReasonAmountTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1ReasonAmountTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1ReasonAmountTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1ReasonAmountTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ReasonAmountTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ReasonAmountTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ReasonAmountTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ReasonAmountTotal.Location = new System.Drawing.Point(4, 189);
            this.c1ReasonAmountTotal.Name = "c1ReasonAmountTotal";
            this.c1ReasonAmountTotal.Rows.Count = 1;
            this.c1ReasonAmountTotal.Rows.DefaultSize = 19;
            this.c1ReasonAmountTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ReasonAmountTotal.Size = new System.Drawing.Size(605, 21);
            this.c1ReasonAmountTotal.StyleInfo = resources.GetString("c1ReasonAmountTotal.StyleInfo");
            this.c1ReasonAmountTotal.TabIndex = 28;
            this.c1ReasonAmountTotal.TabStop = false;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(82, 49);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(485, 108);
            this.pnlInternalControl.TabIndex = 27;
            this.pnlInternalControl.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(4, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(605, 1);
            this.label4.TabIndex = 25;
            this.label4.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(609, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 211);
            this.label6.TabIndex = 23;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 211);
            this.label7.TabIndex = 22;
            this.label7.Text = "label7";
            // 
            // pnlTransactionOther2
            // 
            this.pnlTransactionOther2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlTransactionOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionOther2.Controls.Add(this.lblFillAmount);
            this.pnlTransactionOther2.Controls.Add(this.lblCntrlF);
            this.pnlTransactionOther2.Controls.Add(this.label17);
            this.pnlTransactionOther2.Controls.Add(this.label15);
            this.pnlTransactionOther2.Controls.Add(this.label20);
            this.pnlTransactionOther2.Controls.Add(this.label21);
            this.pnlTransactionOther2.Controls.Add(this.label19);
            this.pnlTransactionOther2.Controls.Add(this.label16);
            this.pnlTransactionOther2.Controls.Add(this.label34);
            this.pnlTransactionOther2.Controls.Add(this.label33);
            this.pnlTransactionOther2.Controls.Add(this.label32);
            this.pnlTransactionOther2.Controls.Add(this.label13);
            this.pnlTransactionOther2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionOther2.Location = new System.Drawing.Point(3, 3);
            this.pnlTransactionOther2.Name = "pnlTransactionOther2";
            this.pnlTransactionOther2.Size = new System.Drawing.Size(607, 19);
            this.pnlTransactionOther2.TabIndex = 211;
            // 
            // lblFillAmount
            // 
            this.lblFillAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblFillAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFillAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFillAmount.Location = new System.Drawing.Point(475, 1);
            this.lblFillAmount.Name = "lblFillAmount";
            this.lblFillAmount.Size = new System.Drawing.Size(93, 17);
            this.lblFillAmount.TabIndex = 64;
            this.lblFillAmount.Text = "- Fill Amount";
            this.lblFillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCntrlF
            // 
            this.lblCntrlF.BackColor = System.Drawing.Color.Transparent;
            this.lblCntrlF.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCntrlF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblCntrlF.ForeColor = System.Drawing.Color.Maroon;
            this.lblCntrlF.Location = new System.Drawing.Point(424, 1);
            this.lblCntrlF.Name = "lblCntrlF";
            this.lblCntrlF.Size = new System.Drawing.Size(51, 17);
            this.lblCntrlF.TabIndex = 63;
            this.lblCntrlF.Text = "Ctrl+F";
            this.lblCntrlF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(301, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 17);
            this.label17.TabIndex = 62;
            this.label17.Text = "- Save And Close    ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Maroon;
            this.label15.Location = new System.Drawing.Point(250, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 17);
            this.label15.TabIndex = 61;
            this.label15.Text = "Ctrl+S";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(138, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(112, 17);
            this.label20.TabIndex = 46;
            this.label20.Text = "- Remove Line";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Maroon;
            this.label21.Location = new System.Drawing.Point(113, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 17);
            this.label21.TabIndex = 45;
            this.label21.Text = "F3";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(32, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 17);
            this.label19.TabIndex = 44;
            this.label19.Text = "- Add Line";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Maroon;
            this.label16.Location = new System.Drawing.Point(1, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 17);
            this.label16.TabIndex = 44;
            this.label16.Text = "  F2 ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 18);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(605, 1);
            this.label34.TabIndex = 60;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(605, 1);
            this.label33.TabIndex = 59;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(606, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 19);
            this.label32.TabIndex = 58;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 19);
            this.label13.TabIndex = 57;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(613, 24);
            this.menuStrip1.TabIndex = 217;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling_AddLine,
            this.mnuBilling_RemoveLine,
            this.mnuBilling_Save});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            // 
            // mnuBilling_AddLine
            // 
            this.mnuBilling_AddLine.Name = "mnuBilling_AddLine";
            this.mnuBilling_AddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuBilling_AddLine.Size = new System.Drawing.Size(166, 22);
            this.mnuBilling_AddLine.Text = "Add Line";
            this.mnuBilling_AddLine.Click += new System.EventHandler(this.mnuBilling_AddLine_Click);
            // 
            // mnuBilling_RemoveLine
            // 
            this.mnuBilling_RemoveLine.Name = "mnuBilling_RemoveLine";
            this.mnuBilling_RemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuBilling_RemoveLine.Size = new System.Drawing.Size(166, 22);
            this.mnuBilling_RemoveLine.Text = "Remove Line";
            this.mnuBilling_RemoveLine.Click += new System.EventHandler(this.mnuBilling_RemoveLine_Click);
            // 
            // mnuBilling_Save
            // 
            this.mnuBilling_Save.Name = "mnuBilling_Save";
            this.mnuBilling_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuBilling_Save.Size = new System.Drawing.Size(166, 22);
            this.mnuBilling_Save.Text = "Save W/O";
            this.mnuBilling_Save.Click += new System.EventHandler(this.mnuBilling_Save_Click);
            // 
            // pnlInternalNotes
            // 
            this.pnlInternalNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInternalNotes.Controls.Add(this.txtInternalNotes);
            this.pnlInternalNotes.Controls.Add(this.chkInternalNotes);
            this.pnlInternalNotes.Controls.Add(this.label31);
            this.pnlInternalNotes.Controls.Add(this.label1);
            this.pnlInternalNotes.Controls.Add(this.label2);
            this.pnlInternalNotes.Controls.Add(this.label3);
            this.pnlInternalNotes.Controls.Add(this.label8);
            this.pnlInternalNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInternalNotes.Location = new System.Drawing.Point(0, 644);
            this.pnlInternalNotes.Name = "pnlInternalNotes";
            this.pnlInternalNotes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlInternalNotes.Size = new System.Drawing.Size(613, 48);
            this.pnlInternalNotes.TabIndex = 4;
            this.pnlInternalNotes.TabStop = true;
            // 
            // txtInternalNotes
            // 
            this.txtInternalNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInternalNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInternalNotes.ForeColor = System.Drawing.Color.Black;
            this.txtInternalNotes.Location = new System.Drawing.Point(126, 8);
            this.txtInternalNotes.MaxLength = 255;
            this.txtInternalNotes.Multiline = true;
            this.txtInternalNotes.Name = "txtInternalNotes";
            this.txtInternalNotes.Size = new System.Drawing.Size(441, 35);
            this.txtInternalNotes.TabIndex = 0;
            this.txtInternalNotes.Enter += new System.EventHandler(this.txtInternalNotes_Enter);
            // 
            // chkInternalNotes
            // 
            this.chkInternalNotes.AutoSize = true;
            this.chkInternalNotes.Location = new System.Drawing.Point(186, 57);
            this.chkInternalNotes.Name = "chkInternalNotes";
            this.chkInternalNotes.Size = new System.Drawing.Size(160, 18);
            this.chkInternalNotes.TabIndex = 63;
            this.chkInternalNotes.TabStop = false;
            this.chkInternalNotes.Text = "Include Note on Receipt";
            this.chkInternalNotes.UseVisualStyleBackColor = true;
            this.chkInternalNotes.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(31, 11);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(93, 14);
            this.label31.TabIndex = 61;
            this.label31.Text = "Internal Notes :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(4, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(605, 1);
            this.label1.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(605, 1);
            this.label2.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(609, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 45);
            this.label3.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 45);
            this.label8.TabIndex = 57;
            // 
            // pnlStatementNotes
            // 
            this.pnlStatementNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlStatementNotes.Controls.Add(this.txtStatementNotes);
            this.pnlStatementNotes.Controls.Add(this.chkStatementNotes);
            this.pnlStatementNotes.Controls.Add(this.label9);
            this.pnlStatementNotes.Controls.Add(this.label10);
            this.pnlStatementNotes.Controls.Add(this.label11);
            this.pnlStatementNotes.Controls.Add(this.label12);
            this.pnlStatementNotes.Controls.Add(this.label14);
            this.pnlStatementNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatementNotes.Location = new System.Drawing.Point(0, 576);
            this.pnlStatementNotes.Name = "pnlStatementNotes";
            this.pnlStatementNotes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlStatementNotes.Size = new System.Drawing.Size(613, 68);
            this.pnlStatementNotes.TabIndex = 3;
            this.pnlStatementNotes.TabStop = true;
            // 
            // txtStatementNotes
            // 
            this.txtStatementNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatementNotes.ForeColor = System.Drawing.Color.Black;
            this.txtStatementNotes.Location = new System.Drawing.Point(126, 8);
            this.txtStatementNotes.MaxLength = 255;
            this.txtStatementNotes.Multiline = true;
            this.txtStatementNotes.Name = "txtStatementNotes";
            this.txtStatementNotes.Size = new System.Drawing.Size(441, 53);
            this.txtStatementNotes.TabIndex = 0;
            this.txtStatementNotes.Enter += new System.EventHandler(this.txtStatementNotes_Enter);
            // 
            // chkStatementNotes
            // 
            this.chkStatementNotes.AutoSize = true;
            this.chkStatementNotes.Location = new System.Drawing.Point(186, 26);
            this.chkStatementNotes.Name = "chkStatementNotes";
            this.chkStatementNotes.Size = new System.Drawing.Size(160, 18);
            this.chkStatementNotes.TabIndex = 63;
            this.chkStatementNotes.TabStop = false;
            this.chkStatementNotes.Text = "Include Note on Receipt";
            this.chkStatementNotes.UseVisualStyleBackColor = true;
            this.chkStatementNotes.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(14, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 14);
            this.label9.TabIndex = 61;
            this.label9.Text = "Statement Notes :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(4, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(605, 1);
            this.label10.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(4, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(605, 1);
            this.label11.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(609, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 65);
            this.label12.TabIndex = 58;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 65);
            this.label14.TabIndex = 57;
            // 
            // pnlTransactionOther2Hdr
            // 
            this.pnlTransactionOther2Hdr.Controls.Add(this.pnlTransactionOther2);
            this.pnlTransactionOther2Hdr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTransactionOther2Hdr.Location = new System.Drawing.Point(0, 692);
            this.pnlTransactionOther2Hdr.Name = "pnlTransactionOther2Hdr";
            this.pnlTransactionOther2Hdr.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTransactionOther2Hdr.Size = new System.Drawing.Size(613, 25);
            this.pnlTransactionOther2Hdr.TabIndex = 218;
            this.pnlTransactionOther2Hdr.TabStop = true;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C1SuperTooltip1.ShowAlways = true;
            // 
            // pnlReasonCodeHdr
            // 
            this.pnlReasonCodeHdr.Controls.Add(this.pnlReasonCodemain);
            this.pnlReasonCodeHdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReasonCodeHdr.Location = new System.Drawing.Point(0, 81);
            this.pnlReasonCodeHdr.Name = "pnlReasonCodeHdr";
            this.pnlReasonCodeHdr.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlReasonCodeHdr.Size = new System.Drawing.Size(613, 25);
            this.pnlReasonCodeHdr.TabIndex = 0;
            // 
            // pnlReasonCodemain
            // 
            this.pnlReasonCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlReasonCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReasonCodemain.Controls.Add(this.lblReasonCodemain);
            this.pnlReasonCodemain.Controls.Add(this.label24);
            this.pnlReasonCodemain.Controls.Add(this.label25);
            this.pnlReasonCodemain.Controls.Add(this.label26);
            this.pnlReasonCodemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReasonCodemain.Location = new System.Drawing.Point(3, 3);
            this.pnlReasonCodemain.Name = "pnlReasonCodemain";
            this.pnlReasonCodemain.Size = new System.Drawing.Size(607, 22);
            this.pnlReasonCodemain.TabIndex = 1;
            // 
            // lblReasonCodemain
            // 
            this.lblReasonCodemain.BackColor = System.Drawing.Color.Transparent;
            this.lblReasonCodemain.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblReasonCodemain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReasonCodemain.Location = new System.Drawing.Point(1, 1);
            this.lblReasonCodemain.Name = "lblReasonCodemain";
            this.lblReasonCodemain.Size = new System.Drawing.Size(112, 21);
            this.lblReasonCodemain.TabIndex = 46;
            this.lblReasonCodemain.Text = " Reason Code";
            this.lblReasonCodemain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Location = new System.Drawing.Point(1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(605, 1);
            this.label24.TabIndex = 59;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(606, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 22);
            this.label25.TabIndex = 58;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 22);
            this.label26.TabIndex = 57;
            // 
            // pnlRemarkCodeHdr
            // 
            this.pnlRemarkCodeHdr.Controls.Add(this.pnlRemarkCodemain);
            this.pnlRemarkCodeHdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRemarkCodeHdr.Location = new System.Drawing.Point(0, 317);
            this.pnlRemarkCodeHdr.Name = "pnlRemarkCodeHdr";
            this.pnlRemarkCodeHdr.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlRemarkCodeHdr.Size = new System.Drawing.Size(613, 25);
            this.pnlRemarkCodeHdr.TabIndex = 220;
            this.pnlRemarkCodeHdr.TabStop = true;
            // 
            // pnlRemarkCodemain
            // 
            this.pnlRemarkCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlRemarkCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRemarkCodemain.Controls.Add(this.lblRemarkCodemain);
            this.pnlRemarkCodemain.Controls.Add(this.label22);
            this.pnlRemarkCodemain.Controls.Add(this.label27);
            this.pnlRemarkCodemain.Controls.Add(this.label28);
            this.pnlRemarkCodemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRemarkCodemain.Location = new System.Drawing.Point(3, 3);
            this.pnlRemarkCodemain.Name = "pnlRemarkCodemain";
            this.pnlRemarkCodemain.Size = new System.Drawing.Size(607, 22);
            this.pnlRemarkCodemain.TabIndex = 211;
            // 
            // lblRemarkCodemain
            // 
            this.lblRemarkCodemain.BackColor = System.Drawing.Color.Transparent;
            this.lblRemarkCodemain.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRemarkCodemain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarkCodemain.Location = new System.Drawing.Point(1, 1);
            this.lblRemarkCodemain.Name = "lblRemarkCodemain";
            this.lblRemarkCodemain.Size = new System.Drawing.Size(112, 21);
            this.lblRemarkCodemain.TabIndex = 46;
            this.lblRemarkCodemain.Text = " Remark Code";
            this.lblRemarkCodemain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Location = new System.Drawing.Point(1, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(605, 1);
            this.label22.TabIndex = 59;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(606, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 22);
            this.label27.TabIndex = 58;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 22);
            this.label28.TabIndex = 57;
            // 
            // pnlRemarkCode
            // 
            this.pnlRemarkCode.BackColor = System.Drawing.Color.Transparent;
            this.pnlRemarkCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlRemarkCode.Controls.Add(this.pnlInternalControlRmk);
            this.pnlRemarkCode.Controls.Add(this.RemarkGrid);
            this.pnlRemarkCode.Controls.Add(this.label29);
            this.pnlRemarkCode.Controls.Add(this.label30);
            this.pnlRemarkCode.Controls.Add(this.label35);
            this.pnlRemarkCode.Controls.Add(this.label36);
            this.pnlRemarkCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRemarkCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlRemarkCode.Location = new System.Drawing.Point(0, 342);
            this.pnlRemarkCode.Name = "pnlRemarkCode";
            this.pnlRemarkCode.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlRemarkCode.Size = new System.Drawing.Size(613, 234);
            this.pnlRemarkCode.TabIndex = 2;
            this.pnlRemarkCode.TabStop = true;
            // 
            // pnlInternalControlRmk
            // 
            this.pnlInternalControlRmk.AutoScroll = true;
            this.pnlInternalControlRmk.AutoSize = true;
            this.pnlInternalControlRmk.Location = new System.Drawing.Point(7, 55);
            this.pnlInternalControlRmk.Name = "pnlInternalControlRmk";
            this.pnlInternalControlRmk.Size = new System.Drawing.Size(582, 108);
            this.pnlInternalControlRmk.TabIndex = 27;
            this.pnlInternalControlRmk.Visible = false;
            // 
            // RemarkGrid
            // 
            this.RemarkGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.RemarkGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.RemarkGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RemarkGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.RemarkGrid.ColumnInfo = "1,0,0,0,0,105,Columns:0{Width:64;Style:\"Font:Tahoma, 9pt;\";}\t";
            this.RemarkGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemarkGrid.ExtendLastCol = true;
            this.RemarkGrid.Font = new System.Drawing.Font("Tahoma", 9F);
            this.RemarkGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.RemarkGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.RemarkGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.RemarkGrid.Location = new System.Drawing.Point(4, 1);
            this.RemarkGrid.Name = "RemarkGrid";
            this.RemarkGrid.Rows.Count = 13;
            this.RemarkGrid.Rows.DefaultSize = 21;
            this.RemarkGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.RemarkGrid.Size = new System.Drawing.Size(605, 232);
            this.RemarkGrid.StyleInfo = resources.GetString("RemarkGrid.StyleInfo");
            this.RemarkGrid.TabIndex = 0;
            this.RemarkGrid.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.RemarkGrid_StartEdit);
            this.RemarkGrid.ChangeEdit += new System.EventHandler(this.RemarkGrid_ChangeEdit);
            this.RemarkGrid.Enter += new System.EventHandler(this.RemarkGrid_Enter);
            this.RemarkGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemarkGrid_KeyUp);
            this.RemarkGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RemarkGrid_MouseMove);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(4, 233);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(605, 1);
            this.label29.TabIndex = 25;
            this.label29.Text = "label29";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Top;
            this.label30.Location = new System.Drawing.Point(4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(605, 1);
            this.label30.TabIndex = 24;
            this.label30.Text = "label30";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Location = new System.Drawing.Point(609, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 234);
            this.label35.TabIndex = 23;
            this.label35.Text = "label35";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(3, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 234);
            this.label36.TabIndex = 22;
            this.label36.Text = "label36";
            // 
            // pnlAmountHeader
            // 
            this.pnlAmountHeader.Controls.Add(this.panel3);
            this.pnlAmountHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAmountHeader.Location = new System.Drawing.Point(0, 54);
            this.pnlAmountHeader.Name = "pnlAmountHeader";
            this.pnlAmountHeader.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlAmountHeader.Size = new System.Drawing.Size(613, 27);
            this.pnlAmountHeader.TabIndex = 223;
            this.pnlAmountHeader.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.pnlPendingAmount);
            this.panel3.Controls.Add(this.lblAmount);
            this.panel3.Controls.Add(this.lblAmountHeader);
            this.panel3.Controls.Add(this.lblReasoncodeType);
            this.panel3.Controls.Add(this.label41);
            this.panel3.Controls.Add(this.label42);
            this.panel3.Controls.Add(this.label43);
            this.panel3.Controls.Add(this.label44);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(607, 24);
            this.panel3.TabIndex = 211;
            // 
            // pnlPendingAmount
            // 
            this.pnlPendingAmount.BackColor = System.Drawing.Color.Transparent;
            this.pnlPendingAmount.Controls.Add(this.label37);
            this.pnlPendingAmount.Controls.Add(this.lblPendingAmount);
            this.pnlPendingAmount.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPendingAmount.Location = new System.Drawing.Point(302, 1);
            this.pnlPendingAmount.Name = "pnlPendingAmount";
            this.pnlPendingAmount.Size = new System.Drawing.Size(304, 22);
            this.pnlPendingAmount.TabIndex = 64;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Dock = System.Windows.Forms.DockStyle.Right;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(134, 0);
            this.label37.Name = "label37";
            this.label37.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label37.Size = new System.Drawing.Size(119, 17);
            this.label37.TabIndex = 62;
            this.label37.Text = "Pending Amount :";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPendingAmount
            // 
            this.lblPendingAmount.AutoSize = true;
            this.lblPendingAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblPendingAmount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPendingAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingAmount.ForeColor = System.Drawing.Color.Maroon;
            this.lblPendingAmount.Location = new System.Drawing.Point(253, 0);
            this.lblPendingAmount.Name = "lblPendingAmount";
            this.lblPendingAmount.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblPendingAmount.Size = new System.Drawing.Size(51, 17);
            this.lblPendingAmount.TabIndex = 63;
            this.lblPendingAmount.Text = "  $0.00";
            this.lblPendingAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.Maroon;
            this.lblAmount.Location = new System.Drawing.Point(86, 1);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblAmount.Size = new System.Drawing.Size(51, 17);
            this.lblAmount.TabIndex = 44;
            this.lblAmount.Text = "  $0.00";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountHeader
            // 
            this.lblAmountHeader.AutoSize = true;
            this.lblAmountHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAmountHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountHeader.Location = new System.Drawing.Point(21, 1);
            this.lblAmountHeader.Name = "lblAmountHeader";
            this.lblAmountHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblAmountHeader.Size = new System.Drawing.Size(65, 17);
            this.lblAmountHeader.TabIndex = 44;
            this.lblAmountHeader.Text = "Amount :";
            this.lblAmountHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReasoncodeType
            // 
            this.lblReasoncodeType.AutoSize = true;
            this.lblReasoncodeType.BackColor = System.Drawing.Color.Transparent;
            this.lblReasoncodeType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblReasoncodeType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReasoncodeType.Location = new System.Drawing.Point(1, 1);
            this.lblReasoncodeType.Name = "lblReasoncodeType";
            this.lblReasoncodeType.Padding = new System.Windows.Forms.Padding(20, 3, 0, 0);
            this.lblReasoncodeType.Size = new System.Drawing.Size(20, 17);
            this.lblReasoncodeType.TabIndex = 61;
            this.lblReasoncodeType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(1, 23);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(605, 1);
            this.label41.TabIndex = 60;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(1, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(605, 1);
            this.label42.TabIndex = 59;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(606, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 24);
            this.label43.TabIndex = 58;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 24);
            this.label44.TabIndex = 57;
            // 
            // frmEOBPaymentReasonCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(613, 717);
            this.Controls.Add(this.pnlInternalNotes);
            this.Controls.Add(this.pnlTransactionOther2Hdr);
            this.Controls.Add(this.pnlStatementNotes);
            this.Controls.Add(this.pnlRemarkCode);
            this.Controls.Add(this.pnlRemarkCodeHdr);
            this.Controls.Add(this.pnlReason);
            this.Controls.Add(this.pnlReasonCodeHdr);
            this.Controls.Add(this.pnlAmountHeader);
            this.Controls.Add(this.pnltls_Notes);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEOBPaymentReasonCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reason & Remark Code";
            this.Load += new System.EventHandler(this.frmEOBPaymentReasonCode_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEOBPaymentReasonCode_KeyDown);
            this.pnltls_Notes.ResumeLayout(false);
            this.pnltls_Notes.PerformLayout();
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.pnlReason.ResumeLayout(false);
            this.pnlReason.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReasonGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ReasonAmountTotal)).EndInit();
            this.pnlTransactionOther2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlInternalNotes.ResumeLayout(false);
            this.pnlInternalNotes.PerformLayout();
            this.pnlStatementNotes.ResumeLayout(false);
            this.pnlStatementNotes.PerformLayout();
            this.pnlTransactionOther2Hdr.ResumeLayout(false);
            this.pnlReasonCodeHdr.ResumeLayout(false);
            this.pnlReasonCodemain.ResumeLayout(false);
            this.pnlRemarkCodeHdr.ResumeLayout(false);
            this.pnlRemarkCodemain.ResumeLayout(false);
            this.pnlRemarkCode.ResumeLayout(false);
            this.pnlRemarkCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkGrid)).EndInit();
            this.pnlAmountHeader.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPendingAmount.ResumeLayout(false);
            this.pnlPendingAmount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_SavenClose;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel pnltls_Notes;
        private System.Windows.Forms.Panel pnlReason;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton tls_btnAddLine;
        private System.Windows.Forms.ToolStripButton tls_btnRemoveLine;
        protected internal C1.Win.C1FlexGrid.C1FlexGrid ReasonGrid;
        private System.Windows.Forms.Panel pnlTransactionOther2;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_RemoveLine;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_Save;
        private System.Windows.Forms.Panel pnlInternalNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInternalNotes;
        private System.Windows.Forms.CheckBox chkInternalNotes;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Panel pnlStatementNotes;
        private System.Windows.Forms.TextBox txtStatementNotes;
        private System.Windows.Forms.CheckBox chkStatementNotes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlTransactionOther2Hdr;
        private System.Windows.Forms.Panel pnlReasonCodeHdr;
        private System.Windows.Forms.Panel pnlReasonCodemain;
        private System.Windows.Forms.Label lblReasonCodemain;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlRemarkCodeHdr;
        private System.Windows.Forms.Panel pnlRemarkCodemain;
        private System.Windows.Forms.Label lblRemarkCodemain;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel pnlRemarkCode;
        private System.Windows.Forms.Panel pnlInternalControlRmk;
        protected internal C1.Win.C1FlexGrid.C1FlexGrid RemarkGrid;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel pnlAmountHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblAmountHeader;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblReasoncodeType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ReasonAmountTotal;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlPendingAmount;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblPendingAmount;
        private System.Windows.Forms.Label lblFillAmount;
        private System.Windows.Forms.Label lblCntrlF;
    }
}