namespace gloBilling
{
    partial class frmClaimChargeHistoryV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClaimChargeHistoryV2));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tls_btnViewRemit = new System.Windows.Forms.ToolStripButton();
            this.tls_btnViewPmnt = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewClaimHistoryReport = new System.Windows.Forms.ToolStripButton();
            this.tlb_AddNotes = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlMultiplePayment = new System.Windows.Forms.Panel();
            this.c1ClaimCharges = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblClaimHistory = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1ClaimChargeHistory = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblClmCharge = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.C1SuperTooltipDx = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlShortcut = new System.Windows.Forms.Panel();
            this.pnlTransactionOther2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlMultiplePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimCharges)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimChargeHistory)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlShortcut.SuspendLayout();
            this.pnlTransactionOther2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1257, 56);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Modify,
            this.tls_btnViewRemit,
            this.tls_btnViewPmnt,
            this.tsb_ViewClaimHistoryReport,
            this.tlb_AddNotes,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1257, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(106, 50);
            this.tsb_Modify.Text = "&Modify Charges";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tls_btnViewRemit
            // 
            this.tls_btnViewRemit.Enabled = false;
            this.tls_btnViewRemit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnViewRemit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnViewRemit.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnViewRemit.Image")));
            this.tls_btnViewRemit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnViewRemit.Name = "tls_btnViewRemit";
            this.tls_btnViewRemit.Size = new System.Drawing.Size(80, 50);
            this.tls_btnViewRemit.Tag = "Save";
            this.tls_btnViewRemit.Text = "&View Remit";
            this.tls_btnViewRemit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnViewRemit.ToolTipText = "View Remit";
            this.tls_btnViewRemit.Click += new System.EventHandler(this.tls_btnViewRemit_Click);
            // 
            // tls_btnViewPmnt
            // 
            this.tls_btnViewPmnt.Enabled = false;
            this.tls_btnViewPmnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnViewPmnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnViewPmnt.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnViewPmnt.Image")));
            this.tls_btnViewPmnt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnViewPmnt.Name = "tls_btnViewPmnt";
            this.tls_btnViewPmnt.Size = new System.Drawing.Size(98, 50);
            this.tls_btnViewPmnt.Tag = "Save";
            this.tls_btnViewPmnt.Text = "View &Payment";
            this.tls_btnViewPmnt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnViewPmnt.ToolTipText = "View Payment";
            this.tls_btnViewPmnt.Click += new System.EventHandler(this.tls_btnViewPmnt_Click);
            // 
            // tsb_ViewClaimHistoryReport
            // 
            this.tsb_ViewClaimHistoryReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewClaimHistoryReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ViewClaimHistoryReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewClaimHistoryReport.Image")));
            this.tsb_ViewClaimHistoryReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewClaimHistoryReport.Name = "tsb_ViewClaimHistoryReport";
            this.tsb_ViewClaimHistoryReport.Size = new System.Drawing.Size(54, 50);
            this.tsb_ViewClaimHistoryReport.Tag = "Report";
            this.tsb_ViewClaimHistoryReport.Text = "&Report";
            this.tsb_ViewClaimHistoryReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewClaimHistoryReport.Click += new System.EventHandler(this.tsb_ViewClaimHistoryReport_Click);
            // 
            // tlb_AddNotes
            // 
            this.tlb_AddNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_AddNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_AddNotes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_AddNotes.Image")));
            this.tlb_AddNotes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_AddNotes.Name = "tlb_AddNotes";
            this.tlb_AddNotes.Size = new System.Drawing.Size(50, 50);
            this.tlb_AddNotes.Tag = "AddNotes";
            this.tlb_AddNotes.Text = " &Notes";
            this.tlb_AddNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_AddNotes.Click += new System.EventHandler(this.tlb_AddNotes_Click);
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
            // pnlMultiplePayment
            // 
            this.pnlMultiplePayment.Controls.Add(this.c1ClaimCharges);
            this.pnlMultiplePayment.Controls.Add(this.panel3);
            this.pnlMultiplePayment.Controls.Add(this.label14);
            this.pnlMultiplePayment.Controls.Add(this.label10);
            this.pnlMultiplePayment.Controls.Add(this.label9);
            this.pnlMultiplePayment.Controls.Add(this.label29);
            this.pnlMultiplePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMultiplePayment.Location = new System.Drawing.Point(0, 56);
            this.pnlMultiplePayment.Name = "pnlMultiplePayment";
            this.pnlMultiplePayment.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlMultiplePayment.Size = new System.Drawing.Size(1257, 278);
            this.pnlMultiplePayment.TabIndex = 206;
            // 
            // c1ClaimCharges
            // 
            this.c1ClaimCharges.AllowEditing = false;
            this.c1ClaimCharges.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1ClaimCharges.AutoGenerateColumns = false;
            this.c1ClaimCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.c1ClaimCharges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ClaimCharges.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ClaimCharges.ColumnInfo = resources.GetString("c1ClaimCharges.ColumnInfo");
            this.c1ClaimCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ClaimCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ClaimCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ClaimCharges.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimCharges.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimCharges.Location = new System.Drawing.Point(4, 23);
            this.c1ClaimCharges.Name = "c1ClaimCharges";
            this.c1ClaimCharges.Rows.Count = 1;
            this.c1ClaimCharges.Rows.DefaultSize = 19;
            this.c1ClaimCharges.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ClaimCharges.Size = new System.Drawing.Size(1249, 254);
            this.c1ClaimCharges.StyleInfo = resources.GetString("c1ClaimCharges.StyleInfo");
            this.c1ClaimCharges.TabIndex = 0;
            this.c1ClaimCharges.TabStop = false;
            this.c1ClaimCharges.RowColChange += new System.EventHandler(this.c1ClaimCharges_RowColChange);
            this.c1ClaimCharges.Click += new System.EventHandler(this.c1ClaimCharges_Click);
            this.c1ClaimCharges.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ClaimCharges_MouseMove);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lblClaimHistory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1249, 22);
            this.panel3.TabIndex = 212;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(0, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1249, 1);
            this.label7.TabIndex = 4;
            this.label7.Text = "Close Date :";
            // 
            // lblClaimHistory
            // 
            this.lblClaimHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClaimHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimHistory.Location = new System.Drawing.Point(0, 0);
            this.lblClaimHistory.Name = "lblClaimHistory";
            this.lblClaimHistory.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lblClaimHistory.Size = new System.Drawing.Size(1249, 22);
            this.lblClaimHistory.TabIndex = 0;
            this.lblClaimHistory.Text = "Current Status";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(4, 277);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1249, 1);
            this.label14.TabIndex = 1;
            this.label14.Text = "Close Date :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1249, 1);
            this.label10.TabIndex = 3;
            this.label10.Text = "Close Date :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 278);
            this.label9.TabIndex = 209;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(1253, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 278);
            this.label29.TabIndex = 210;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1ClaimChargeHistory);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1257, 503);
            this.panel1.TabIndex = 207;
            // 
            // c1ClaimChargeHistory
            // 
            this.c1ClaimChargeHistory.AllowEditing = false;
            this.c1ClaimChargeHistory.AutoGenerateColumns = false;
            this.c1ClaimChargeHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.c1ClaimChargeHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ClaimChargeHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ClaimChargeHistory.ColumnInfo = resources.GetString("c1ClaimChargeHistory.ColumnInfo");
            this.c1ClaimChargeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ClaimChargeHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ClaimChargeHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ClaimChargeHistory.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimChargeHistory.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ClaimChargeHistory.Location = new System.Drawing.Point(4, 23);
            this.c1ClaimChargeHistory.Name = "c1ClaimChargeHistory";
            this.c1ClaimChargeHistory.Padding = new System.Windows.Forms.Padding(3);
            this.c1ClaimChargeHistory.Rows.Count = 1;
            this.c1ClaimChargeHistory.Rows.DefaultSize = 19;
            this.c1ClaimChargeHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ClaimChargeHistory.Size = new System.Drawing.Size(1249, 476);
            this.c1ClaimChargeHistory.StyleInfo = resources.GetString("c1ClaimChargeHistory.StyleInfo");
            this.c1ClaimChargeHistory.TabIndex = 0;
            this.c1ClaimChargeHistory.TabStop = false;
            this.c1ClaimChargeHistory.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1ClaimChargeHistory_AfterSort);
            this.c1ClaimChargeHistory.RowColChange += new System.EventHandler(this.c1ClaimChargeHistory_RowColChange);
            this.c1ClaimChargeHistory.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ClaimChargeHistory_AfterEdit);
            this.c1ClaimChargeHistory.Click += new System.EventHandler(this.c1ClaimChargeHistory_Click);
            this.c1ClaimChargeHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1ClaimChargeHistory_MouseDoubleClick);
            this.c1ClaimChargeHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ClaimChargeHistory_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lblClmCharge);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1249, 22);
            this.panel2.TabIndex = 211;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(0, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1249, 1);
            this.label6.TabIndex = 4;
            this.label6.Text = "Close Date :";
            // 
            // lblClmCharge
            // 
            this.lblClmCharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClmCharge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClmCharge.Location = new System.Drawing.Point(0, 0);
            this.lblClmCharge.Name = "lblClmCharge";
            this.lblClmCharge.Padding = new System.Windows.Forms.Padding(5, 2, 0, 0);
            this.lblClmCharge.Size = new System.Drawing.Size(1249, 22);
            this.lblClmCharge.TabIndex = 0;
            this.lblClmCharge.Text = "Charge History";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(4, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1249, 1);
            this.label1.TabIndex = 1;
            this.label1.Text = "Close Date :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1249, 1);
            this.label2.TabIndex = 3;
            this.label2.Text = "Close Date :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 500);
            this.label3.TabIndex = 209;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1253, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 500);
            this.label4.TabIndex = 210;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 334);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1257, 3);
            this.splitter1.TabIndex = 208;
            this.splitter1.TabStop = false;
            // 
            // C1SuperTooltipDx
            // 
            this.C1SuperTooltipDx.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltipDx.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // pnlShortcut
            // 
            this.pnlShortcut.Controls.Add(this.pnlTransactionOther2);
            this.pnlShortcut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcut.Location = new System.Drawing.Point(0, 840);
            this.pnlShortcut.Name = "pnlShortcut";
            this.pnlShortcut.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlShortcut.Size = new System.Drawing.Size(1257, 28);
            this.pnlShortcut.TabIndex = 225;
            // 
            // pnlTransactionOther2
            // 
            this.pnlTransactionOther2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlTransactionOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionOther2.Controls.Add(this.label17);
            this.pnlTransactionOther2.Controls.Add(this.label18);
            this.pnlTransactionOther2.Controls.Add(this.label34);
            this.pnlTransactionOther2.Controls.Add(this.label33);
            this.pnlTransactionOther2.Controls.Add(this.label32);
            this.pnlTransactionOther2.Controls.Add(this.label16);
            this.pnlTransactionOther2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionOther2.Location = new System.Drawing.Point(3, 0);
            this.pnlTransactionOther2.Name = "pnlTransactionOther2";
            this.pnlTransactionOther2.Size = new System.Drawing.Size(1251, 25);
            this.pnlTransactionOther2.TabIndex = 211;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(43, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(251, 23);
            this.label17.TabIndex = 56;
            this.label17.Text = "To view history, select the claim or charge";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Maroon;
            this.label18.Location = new System.Drawing.Point(1, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 23);
            this.label18.TabIndex = 55;
            this.label18.Text = " Note :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 24);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1249, 1);
            this.label34.TabIndex = 60;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1249, 1);
            this.label33.TabIndex = 59;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(1250, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 25);
            this.label32.TabIndex = 58;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 25);
            this.label16.TabIndex = 57;
            // 
            // frmClaimChargeHistoryV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1257, 868);
            this.Controls.Add(this.pnlMultiplePayment);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlShortcut);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimChargeHistoryV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Claim History";
            this.Load += new System.EventHandler(this.frmClaimChargeHistoryV2_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlMultiplePayment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimCharges)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimChargeHistory)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlShortcut.ResumeLayout(false);
            this.pnlTransactionOther2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnViewRemit;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel pnlMultiplePayment;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ClaimCharges;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblClaimHistory;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ClaimChargeHistory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblClmCharge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlShortcut;
        private System.Windows.Forms.Panel pnlTransactionOther2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolStripButton tls_btnViewPmnt;
        private System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.ToolStripButton tlb_AddNotes;
        private System.Windows.Forms.ToolStripButton tsb_ViewClaimHistoryReport;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltipDx;
    }
}