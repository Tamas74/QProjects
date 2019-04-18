namespace gloBilling
{
    partial class frmEOBLedger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEOBLedger));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_PendingCoPay = new System.Windows.Forms.ToolStripButton();
            this.tsb_CategoryTemplates = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowHideZeroBalance = new System.Windows.Forms.ToolStripButton();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCharges = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c1ClaimGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label7 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlChargesHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabClaims = new System.Windows.Forms.TabControl();
            this.tbPgClaims = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlPatDetails = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.C1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbPgDetails = new System.Windows.Forms.TabPage();
            this.pnlDetailsCharges = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.c1DetailCharges = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls.SuspendLayout();
            this.pnlCharges.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimGrid)).BeginInit();
            this.pnlChargesHeader.SuspendLayout();
            this.tabClaims.SuspendLayout();
            this.tbPgClaims.SuspendLayout();
            this.pnlPatDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1PatientDetails)).BeginInit();
            this.panel5.SuspendLayout();
            this.tbPgDetails.SuspendLayout();
            this.pnlDetailsCharges.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DetailCharges)).BeginInit();
            this.panel7.SuspendLayout();
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
            this.pnl_tlspTOP.Size = new System.Drawing.Size(981, 56);
            this.pnl_tlspTOP.TabIndex = 3;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Modify,
            this.tsb_PendingCoPay,
            this.tsb_CategoryTemplates,
            this.tsb_Refresh,
            this.tsb_ShowHideZeroBalance,
            this.ts_btnOk,
            this.ts_btnCancel});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(981, 53);
            this.tls.TabIndex = 0;
            this.tls.Text = "toolStrip1";
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(106, 50);
            this.tsb_Modify.Text = "Modify Charges";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_PendingCoPay
            // 
            this.tsb_PendingCoPay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PendingCoPay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PendingCoPay.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PendingCoPay.Image")));
            this.tsb_PendingCoPay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PendingCoPay.Name = "tsb_PendingCoPay";
            this.tsb_PendingCoPay.Size = new System.Drawing.Size(103, 50);
            this.tsb_PendingCoPay.Tag = "PendingCoPay";
            this.tsb_PendingCoPay.Text = "&Pending CoPay";
            this.tsb_PendingCoPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PendingCoPay.Visible = false;
            this.tsb_PendingCoPay.Click += new System.EventHandler(this.tsb_PendingCoPay_Click);
            // 
            // tsb_CategoryTemplates
            // 
            this.tsb_CategoryTemplates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_CategoryTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_CategoryTemplates.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CategoryTemplates.Image")));
            this.tsb_CategoryTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CategoryTemplates.Name = "tsb_CategoryTemplates";
            this.tsb_CategoryTemplates.Size = new System.Drawing.Size(82, 50);
            this.tsb_CategoryTemplates.Tag = "Templates";
            this.tsb_CategoryTemplates.Text = "&Templates";
            this.tsb_CategoryTemplates.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tsb_CategoryTemplates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CategoryTemplates.Visible = false;
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.Image = global::gloBilling.Properties.Resources.Ico_Refresh;
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_ShowHideZeroBalance
            // 
            this.tsb_ShowHideZeroBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowHideZeroBalance.Image = global::gloBilling.Properties.Resources.Hide_Zero_Balance;
            this.tsb_ShowHideZeroBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHideZeroBalance.Name = "tsb_ShowHideZeroBalance";
            this.tsb_ShowHideZeroBalance.Size = new System.Drawing.Size(119, 50);
            this.tsb_ShowHideZeroBalance.Tag = "Hide";
            this.tsb_ShowHideZeroBalance.Text = "&Hide Zero Balance";
            this.tsb_ShowHideZeroBalance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowHideZeroBalance.Visible = false;
            this.tsb_ShowHideZeroBalance.Click += new System.EventHandler(this.tsb_ShowHideZeroBalance_Click);
            // 
            // ts_btnOk
            // 
            this.ts_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOk.Image")));
            this.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOk.Name = "ts_btnOk";
            this.ts_btnOk.Size = new System.Drawing.Size(66, 50);
            this.ts_btnOk.Tag = "Save";
            this.ts_btnOk.Text = "&Save&&Cls";
            this.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnOk.ToolTipText = "Save and Close";
            this.ts_btnOk.Visible = false;
            this.ts_btnOk.Click += new System.EventHandler(this.ts_btnOk_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Close";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.ts_btnCancel_Click);
            // 
            // pnlCharges
            // 
            this.pnlCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCharges.Controls.Add(this.panel9);
            this.pnlCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCharges.Location = new System.Drawing.Point(171, 0);
            this.pnlCharges.Name = "pnlCharges";
            this.pnlCharges.Size = new System.Drawing.Size(802, 805);
            this.pnlCharges.TabIndex = 5;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel2);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.label45);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.pnlChargesHeader);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(802, 805);
            this.panel9.TabIndex = 112;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c1ClaimGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 780);
            this.panel2.TabIndex = 108;
            // 
            // c1ClaimGrid
            // 
            this.c1ClaimGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ClaimGrid.AutoResize = false;
            this.c1ClaimGrid.BackColor = System.Drawing.Color.White;
            this.c1ClaimGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ClaimGrid.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ClaimGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ClaimGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ClaimGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ClaimGrid.Location = new System.Drawing.Point(0, 0);
            this.c1ClaimGrid.Name = "c1ClaimGrid";
            this.c1ClaimGrid.Padding = new System.Windows.Forms.Padding(3);
            this.c1ClaimGrid.Rows.Count = 1;
            this.c1ClaimGrid.Rows.DefaultSize = 19;
            this.c1ClaimGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ClaimGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ClaimGrid.ShowCellLabels = true;
            this.c1ClaimGrid.Size = new System.Drawing.Size(800, 780);
            this.c1ClaimGrid.StyleInfo = resources.GetString("c1ClaimGrid.StyleInfo");
            this.c1ClaimGrid.TabIndex = 28;
            this.c1ClaimGrid.Tag = "ClosePeriod";
            this.c1ClaimGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1ClaimGrid_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 804);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(800, 1);
            this.label7.TabIndex = 111;
            this.label7.Text = "label4";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Right;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label45.Location = new System.Drawing.Point(801, 24);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 781);
            this.label45.TabIndex = 110;
            this.label45.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 781);
            this.label1.TabIndex = 109;
            this.label1.Text = "label2";
            // 
            // pnlChargesHeader
            // 
            this.pnlChargesHeader.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlChargesHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlChargesHeader.Controls.Add(this.label2);
            this.pnlChargesHeader.Controls.Add(this.label6);
            this.pnlChargesHeader.Controls.Add(this.label5);
            this.pnlChargesHeader.Controls.Add(this.label4);
            this.pnlChargesHeader.Controls.Add(this.label3);
            this.pnlChargesHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChargesHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlChargesHeader.Name = "pnlChargesHeader";
            this.pnlChargesHeader.Size = new System.Drawing.Size(802, 24);
            this.pnlChargesHeader.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 22);
            this.label2.TabIndex = 57;
            this.label2.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(801, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "  Charges";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(0, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(801, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(801, 1);
            this.label4.TabIndex = 56;
            this.label4.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(801, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 24);
            this.label3.TabIndex = 58;
            this.label3.Text = "label3";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            // 
            // tabClaims
            // 
            this.tabClaims.Controls.Add(this.tbPgClaims);
            this.tabClaims.Controls.Add(this.tbPgDetails);
            this.tabClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabClaims.Location = new System.Drawing.Point(0, 56);
            this.tabClaims.Name = "tabClaims";
            this.tabClaims.Padding = new System.Drawing.Point(6, 4);
            this.tabClaims.SelectedIndex = 0;
            this.tabClaims.Size = new System.Drawing.Size(981, 834);
            this.tabClaims.TabIndex = 6;
            this.tabClaims.SelectedIndexChanged += new System.EventHandler(this.tabClaims_SelectedIndexChanged);
            // 
            // tbPgClaims
            // 
            this.tbPgClaims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbPgClaims.Controls.Add(this.pnlCharges);
            this.tbPgClaims.Controls.Add(this.splitter1);
            this.tbPgClaims.Controls.Add(this.pnlPatDetails);
            this.tbPgClaims.Location = new System.Drawing.Point(4, 25);
            this.tbPgClaims.Name = "tbPgClaims";
            this.tbPgClaims.Size = new System.Drawing.Size(973, 805);
            this.tbPgClaims.TabIndex = 1;
            this.tbPgClaims.Tag = "Claims";
            this.tbPgClaims.Text = "Ledger";
            this.tbPgClaims.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(169, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 805);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // pnlPatDetails
            // 
            this.pnlPatDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatDetails.Controls.Add(this.panel3);
            this.pnlPatDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPatDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPatDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlPatDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlPatDetails.Name = "pnlPatDetails";
            this.pnlPatDetails.Size = new System.Drawing.Size(169, 805);
            this.pnlPatDetails.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.C1PatientDetails);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(169, 805);
            this.panel3.TabIndex = 112;
            // 
            // C1PatientDetails
            // 
            this.C1PatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1PatientDetails.AutoResize = false;
            this.C1PatientDetails.BackColor = System.Drawing.Color.White;
            this.C1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1PatientDetails.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.C1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1PatientDetails.Location = new System.Drawing.Point(1, 24);
            this.C1PatientDetails.Name = "C1PatientDetails";
            this.C1PatientDetails.Padding = new System.Windows.Forms.Padding(3);
            this.C1PatientDetails.Rows.Count = 1;
            this.C1PatientDetails.Rows.DefaultSize = 19;
            this.C1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1PatientDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1PatientDetails.ShowCellLabels = true;
            this.C1PatientDetails.Size = new System.Drawing.Size(167, 780);
            this.C1PatientDetails.StyleInfo = resources.GetString("C1PatientDetails.StyleInfo");
            this.C1PatientDetails.TabIndex = 112;
            this.C1PatientDetails.Tag = "ClosePeriod";
            this.C1PatientDetails.Resize += new System.EventHandler(this.C1PatientDetails_Resize);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 804);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 1);
            this.label8.TabIndex = 111;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(168, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 781);
            this.label9.TabIndex = 110;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(0, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 781);
            this.label10.TabIndex = 109;
            this.label10.Text = "label2";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(169, 24);
            this.panel5.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 22);
            this.label11.TabIndex = 57;
            this.label11.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(0, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 22);
            this.label12.TabIndex = 6;
            this.label12.Text = " Patient Details";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(0, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(168, 1);
            this.label13.TabIndex = 5;
            this.label13.Text = "label2";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(168, 1);
            this.label14.TabIndex = 56;
            this.label14.Text = "label1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(168, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 24);
            this.label15.TabIndex = 58;
            this.label15.Text = "label3";
            // 
            // tbPgDetails
            // 
            this.tbPgDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbPgDetails.Controls.Add(this.pnlDetailsCharges);
            this.tbPgDetails.Location = new System.Drawing.Point(4, 25);
            this.tbPgDetails.Name = "tbPgDetails";
            this.tbPgDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgDetails.Size = new System.Drawing.Size(973, 805);
            this.tbPgDetails.TabIndex = 2;
            this.tbPgDetails.Tag = "Details";
            this.tbPgDetails.Text = "Ledger Details";
            // 
            // pnlDetailsCharges
            // 
            this.pnlDetailsCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlDetailsCharges.Controls.Add(this.panel4);
            this.pnlDetailsCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailsCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetailsCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlDetailsCharges.Location = new System.Drawing.Point(3, 3);
            this.pnlDetailsCharges.Name = "pnlDetailsCharges";
            this.pnlDetailsCharges.Size = new System.Drawing.Size(967, 799);
            this.pnlDetailsCharges.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(967, 799);
            this.panel4.TabIndex = 112;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.c1DetailCharges);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 24);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(965, 774);
            this.panel6.TabIndex = 108;
            // 
            // c1DetailCharges
            // 
            this.c1DetailCharges.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1DetailCharges.AutoResize = false;
            this.c1DetailCharges.BackColor = System.Drawing.Color.White;
            this.c1DetailCharges.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1DetailCharges.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1DetailCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DetailCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1DetailCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1DetailCharges.Location = new System.Drawing.Point(0, 0);
            this.c1DetailCharges.Name = "c1DetailCharges";
            this.c1DetailCharges.Padding = new System.Windows.Forms.Padding(3);
            this.c1DetailCharges.Rows.Count = 1;
            this.c1DetailCharges.Rows.DefaultSize = 19;
            this.c1DetailCharges.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1DetailCharges.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1DetailCharges.ShowCellLabels = true;
            this.c1DetailCharges.Size = new System.Drawing.Size(965, 774);
            this.c1DetailCharges.StyleInfo = resources.GetString("c1DetailCharges.StyleInfo");
            this.c1DetailCharges.TabIndex = 28;
            this.c1DetailCharges.Tag = "ClosePeriod";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 798);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(965, 1);
            this.label16.TabIndex = 111;
            this.label16.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(966, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 775);
            this.label17.TabIndex = 110;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(0, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 775);
            this.label18.TabIndex = 109;
            this.label18.Text = "label2";
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.label20);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.label22);
            this.panel7.Controls.Add(this.label23);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(967, 24);
            this.panel7.TabIndex = 5;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 22);
            this.label19.TabIndex = 57;
            this.label19.Text = "label4";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Location = new System.Drawing.Point(0, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(966, 22);
            this.label20.TabIndex = 6;
            this.label20.Text = "  Charges";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(0, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(966, 1);
            this.label21.TabIndex = 5;
            this.label21.Text = "label2";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(966, 1);
            this.label22.TabIndex = 56;
            this.label22.Text = "label1";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(966, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 24);
            this.label23.TabIndex = 58;
            this.label23.Text = "label23";
            // 
            // frmEOBLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(981, 890);
            this.Controls.Add(this.tabClaims);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEOBLedger";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Ledger";
            this.Load += new System.EventHandler(this.frmBillingPatientLedger_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.pnlCharges.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ClaimGrid)).EndInit();
            this.pnlChargesHeader.ResumeLayout(false);
            this.tabClaims.ResumeLayout(false);
            this.tbPgClaims.ResumeLayout(false);
            this.pnlPatDetails.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1PatientDetails)).EndInit();
            this.panel5.ResumeLayout(false);
            this.tbPgDetails.ResumeLayout(false);
            this.pnlDetailsCharges.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DetailCharges)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.Panel pnlCharges;
        private System.Windows.Forms.Panel pnlChargesHeader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton tsb_PendingCoPay;
        private System.Windows.Forms.ToolStripDropDownButton tsb_CategoryTemplates;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripButton tsb_ShowHideZeroBalance;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ClaimGrid;
        private System.Windows.Forms.TabControl tabClaims;
        private System.Windows.Forms.TabPage tbPgClaims;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlPatDetails;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tbPgDetails;
        private C1.Win.C1FlexGrid.C1FlexGrid C1PatientDetails;
        private System.Windows.Forms.Panel pnlDetailsCharges;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private C1.Win.C1FlexGrid.C1FlexGrid c1DetailCharges;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ToolStripButton tsb_Modify;
    }
}