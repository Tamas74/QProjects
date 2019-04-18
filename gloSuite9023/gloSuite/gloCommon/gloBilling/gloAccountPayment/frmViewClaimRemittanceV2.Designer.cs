namespace gloAccountsV2
{
    partial class frmViewClaimRemittanceV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewClaimRemittanceV2));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbViewHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbViewInsPmnt = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.ts_btnPrint = new System.Windows.Forms.ToolStripButton();
            this.panel20 = new System.Windows.Forms.Panel();
            this.c1FlexRemittance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlTotalCaptionPanel = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPlan = new System.Windows.Forms.Label();
            this.lblPlanCaption = new System.Windows.Forms.Label();
            this.lblClaim = new System.Windows.Forms.Label();
            this.lblClaimCaption = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblPatientCaption = new System.Windows.Forms.Label();
            this.lblRefNo = new System.Windows.Forms.Label();
            this.lblCheckDate = new System.Windows.Forms.Label();
            this.lblChkNo = new System.Windows.Forms.Label();
            this.lblInsCompany = new System.Windows.Forms.Label();
            this.lblChkNoCap = new System.Windows.Forms.Label();
            this.lblRefNoCap = new System.Windows.Forms.Label();
            this.lblCheckDateCap = new System.Windows.Forms.Label();
            this.lblInsCmpnyCap = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.C1SuperTooltipDx = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.lblAlertMessage = new System.Windows.Forms.Label();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlCorrection = new System.Windows.Forms.Panel();
            this.c1FlexDeltaRemittance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.tls.SuspendLayout();
            this.panel20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexRemittance)).BeginInit();
            this.pnlTotalCaptionPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlCorrection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexDeltaRemittance)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.tls);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1072, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbViewHistory,
            this.tsbViewInsPmnt,
            this.ts_btnPrint,
            this.ts_btnCancel});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(1072, 53);
            this.tls.TabIndex = 1;
            this.tls.Text = "toolStrip1";
            // 
            // tsbViewHistory
            // 
            this.tsbViewHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbViewHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbViewHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewHistory.Image")));
            this.tsbViewHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewHistory.Name = "tsbViewHistory";
            this.tsbViewHistory.Size = new System.Drawing.Size(124, 50);
            this.tsbViewHistory.Tag = "Hide";
            this.tsbViewHistory.Text = "View Claim &History";
            this.tsbViewHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbViewHistory.Click += new System.EventHandler(this.tsbViewHistory_Click);
            // 
            // tsbViewInsPmnt
            // 
            this.tsbViewInsPmnt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbViewInsPmnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbViewInsPmnt.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewInsPmnt.Image")));
            this.tsbViewInsPmnt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewInsPmnt.Name = "tsbViewInsPmnt";
            this.tsbViewInsPmnt.Size = new System.Drawing.Size(98, 50);
            this.tsbViewInsPmnt.Tag = "Hide";
            this.tsbViewInsPmnt.Text = "View &Payment";
            this.tsbViewInsPmnt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbViewInsPmnt.Click += new System.EventHandler(this.tsbViewInsPmnt_Click);
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
            // ts_btnPrint
            // 
            this.ts_btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ts_btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnPrint.Image")));
            this.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnPrint.Name = "ts_btnPrint";
            this.ts_btnPrint.Size = new System.Drawing.Size(41, 50);
            this.ts_btnPrint.Tag = "Print";
            this.ts_btnPrint.Text = "Prin&t";
            this.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnPrint.Click += new System.EventHandler(this.ts_btnPrint_Click);
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Transparent;
            this.panel20.Controls.Add(this.c1FlexRemittance);
            this.panel20.Controls.Add(this.pnlTotalCaptionPanel);
            this.panel20.Controls.Add(this.label6);
            this.panel20.Controls.Add(this.label19);
            this.panel20.Controls.Add(this.label21);
            this.panel20.Controls.Add(this.label22);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(0, 390);
            this.panel20.Name = "panel20";
            this.panel20.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel20.Size = new System.Drawing.Size(1072, 198);
            this.panel20.TabIndex = 43;
            // 
            // c1FlexRemittance
            // 
            this.c1FlexRemittance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexRemittance.AllowEditing = false;
            this.c1FlexRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
            this.c1FlexRemittance.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexRemittance.AutoGenerateColumns = false;
            this.c1FlexRemittance.BackColor = System.Drawing.Color.White;
            this.c1FlexRemittance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexRemittance.ColumnInfo = resources.GetString("c1FlexRemittance.ColumnInfo");
            this.c1FlexRemittance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexRemittance.ExtendLastCol = true;
            this.c1FlexRemittance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexRemittance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexRemittance.Location = new System.Drawing.Point(4, 25);
            this.c1FlexRemittance.Name = "c1FlexRemittance";
            this.c1FlexRemittance.Padding = new System.Windows.Forms.Padding(3);
            this.c1FlexRemittance.Rows.Count = 1;
            this.c1FlexRemittance.Rows.DefaultSize = 19;
            this.c1FlexRemittance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexRemittance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlexRemittance.ShowCellLabels = true;
            this.c1FlexRemittance.Size = new System.Drawing.Size(1064, 169);
            this.c1FlexRemittance.StyleInfo = resources.GetString("c1FlexRemittance.StyleInfo");
            this.c1FlexRemittance.TabIndex = 117;
            this.c1FlexRemittance.Tag = "ClosePeriod";
            this.c1FlexRemittance.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1FlexRemittance_AfterScroll);
            this.c1FlexRemittance.EnterCell += new System.EventHandler(this.c1FlexRemittance_EnterCell);
            this.c1FlexRemittance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexRemittance_MouseMove);
            // 
            // pnlTotalCaptionPanel
            // 
            this.pnlTotalCaptionPanel.BackColor = System.Drawing.Color.Transparent;
            this.pnlTotalCaptionPanel.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlTotalCaptionPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTotalCaptionPanel.Controls.Add(this.label17);
            this.pnlTotalCaptionPanel.Controls.Add(this.label10);
            this.pnlTotalCaptionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotalCaptionPanel.Location = new System.Drawing.Point(4, 1);
            this.pnlTotalCaptionPanel.Name = "pnlTotalCaptionPanel";
            this.pnlTotalCaptionPanel.Size = new System.Drawing.Size(1064, 24);
            this.pnlTotalCaptionPanel.TabIndex = 118;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1064, 23);
            this.label17.TabIndex = 230;
            this.label17.Text = "  Total :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1064, 1);
            this.label10.TabIndex = 116;
            this.label10.Text = "label1";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1064, 1);
            this.label6.TabIndex = 116;
            this.label6.Text = "label1";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1064, 1);
            this.label19.TabIndex = 115;
            this.label19.Text = "label1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(1068, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 195);
            this.label21.TabIndex = 113;
            this.label21.Text = "label2";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 195);
            this.label22.TabIndex = 112;
            this.label22.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblPlan);
            this.panel1.Controls.Add(this.lblPlanCaption);
            this.panel1.Controls.Add(this.lblClaim);
            this.panel1.Controls.Add(this.lblClaimCaption);
            this.panel1.Controls.Add(this.lblPatient);
            this.panel1.Controls.Add(this.lblPatientCaption);
            this.panel1.Controls.Add(this.lblRefNo);
            this.panel1.Controls.Add(this.lblCheckDate);
            this.panel1.Controls.Add(this.lblChkNo);
            this.panel1.Controls.Add(this.lblInsCompany);
            this.panel1.Controls.Add(this.lblChkNoCap);
            this.panel1.Controls.Add(this.lblRefNoCap);
            this.panel1.Controls.Add(this.lblCheckDateCap);
            this.panel1.Controls.Add(this.lblInsCmpnyCap);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Size = new System.Drawing.Size(1072, 107);
            this.panel1.TabIndex = 44;
            // 
            // lblPlan
            // 
            this.lblPlan.BackColor = System.Drawing.Color.Transparent;
            this.lblPlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPlan.Location = new System.Drawing.Point(698, 22);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(281, 14);
            this.lblPlan.TabIndex = 233;
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlanCaption
            // 
            this.lblPlanCaption.AutoSize = true;
            this.lblPlanCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPlanCaption.Location = new System.Drawing.Point(658, 22);
            this.lblPlanCaption.Name = "lblPlanCaption";
            this.lblPlanCaption.Size = new System.Drawing.Size(37, 14);
            this.lblPlanCaption.TabIndex = 232;
            this.lblPlanCaption.Text = "Plan :";
            this.lblPlanCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClaim
            // 
            this.lblClaim.BackColor = System.Drawing.Color.Transparent;
            this.lblClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaim.Location = new System.Drawing.Point(414, 22);
            this.lblClaim.Name = "lblClaim";
            this.lblClaim.Size = new System.Drawing.Size(190, 14);
            this.lblClaim.TabIndex = 231;
            this.lblClaim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClaim.MouseEnter += new System.EventHandler(this.lblClaim_MouseEnter);
            // 
            // lblClaimCaption
            // 
            this.lblClaimCaption.AutoSize = true;
            this.lblClaimCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblClaimCaption.Location = new System.Drawing.Point(353, 22);
            this.lblClaimCaption.Name = "lblClaimCaption";
            this.lblClaimCaption.Size = new System.Drawing.Size(55, 14);
            this.lblClaimCaption.TabIndex = 230;
            this.lblClaimCaption.Text = "Claim # :";
            this.lblClaimCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPatient
            // 
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(119, 22);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(241, 14);
            this.lblPatient.TabIndex = 229;
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPatient.MouseEnter += new System.EventHandler(this.lblPatient_MouseEnter);
            // 
            // lblPatientCaption
            // 
            this.lblPatientCaption.AutoSize = true;
            this.lblPatientCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientCaption.Location = new System.Drawing.Point(60, 22);
            this.lblPatientCaption.Name = "lblPatientCaption";
            this.lblPatientCaption.Size = new System.Drawing.Size(54, 14);
            this.lblPatientCaption.TabIndex = 228;
            this.lblPatientCaption.Text = "Patient :";
            this.lblPatientCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefNo
            // 
            this.lblRefNo.BackColor = System.Drawing.Color.Transparent;
            this.lblRefNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRefNo.Location = new System.Drawing.Point(698, 78);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(281, 14);
            this.lblRefNo.TabIndex = 227;
            this.lblRefNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCheckDate
            // 
            this.lblCheckDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDate.Location = new System.Drawing.Point(119, 78);
            this.lblCheckDate.Name = "lblCheckDate";
            this.lblCheckDate.Size = new System.Drawing.Size(93, 14);
            this.lblCheckDate.TabIndex = 226;
            this.lblCheckDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChkNo
            // 
            this.lblChkNo.BackColor = System.Drawing.Color.Transparent;
            this.lblChkNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChkNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblChkNo.Location = new System.Drawing.Point(414, 78);
            this.lblChkNo.Name = "lblChkNo";
            this.lblChkNo.Size = new System.Drawing.Size(190, 14);
            this.lblChkNo.TabIndex = 225;
            this.lblChkNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInsCompany
            // 
            this.lblInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.lblInsCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCompany.Location = new System.Drawing.Point(119, 50);
            this.lblInsCompany.Name = "lblInsCompany";
            this.lblInsCompany.Size = new System.Drawing.Size(860, 14);
            this.lblInsCompany.TabIndex = 224;
            this.lblInsCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChkNoCap
            // 
            this.lblChkNoCap.AutoSize = true;
            this.lblChkNoCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChkNoCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblChkNoCap.Location = new System.Drawing.Point(307, 78);
            this.lblChkNoCap.Name = "lblChkNoCap";
            this.lblChkNoCap.Size = new System.Drawing.Size(101, 14);
            this.lblChkNoCap.TabIndex = 217;
            this.lblChkNoCap.Text = "Check# / Ref.# :";
            this.lblChkNoCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefNoCap
            // 
            this.lblRefNoCap.AutoSize = true;
            this.lblRefNoCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefNoCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRefNoCap.Location = new System.Drawing.Point(611, 78);
            this.lblRefNoCap.Name = "lblRefNoCap";
            this.lblRefNoCap.Size = new System.Drawing.Size(84, 14);
            this.lblRefNoCap.TabIndex = 220;
            this.lblRefNoCap.Text = "Reference # :";
            this.lblRefNoCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCheckDateCap
            // 
            this.lblCheckDateCap.AutoSize = true;
            this.lblCheckDateCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDateCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckDateCap.Location = new System.Drawing.Point(21, 78);
            this.lblCheckDateCap.Name = "lblCheckDateCap";
            this.lblCheckDateCap.Size = new System.Drawing.Size(93, 14);
            this.lblCheckDateCap.TabIndex = 221;
            this.lblCheckDateCap.Text = "Payment Date :";
            this.lblCheckDateCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsCmpnyCap
            // 
            this.lblInsCmpnyCap.AutoSize = true;
            this.lblInsCmpnyCap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsCmpnyCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsCmpnyCap.Location = new System.Drawing.Point(25, 50);
            this.lblInsCmpnyCap.Name = "lblInsCmpnyCap";
            this.lblInsCmpnyCap.Size = new System.Drawing.Size(89, 14);
            this.lblInsCmpnyCap.TabIndex = 212;
            this.lblInsCmpnyCap.Text = "Ins. Company :";
            this.lblInsCmpnyCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1064, 1);
            this.label1.TabIndex = 116;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1064, 1);
            this.label2.TabIndex = 115;
            this.label2.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(1068, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 107);
            this.label3.TabIndex = 113;
            this.label3.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 107);
            this.label4.TabIndex = 112;
            this.label4.Text = "label2";
            // 
            // C1SuperTooltipDx
            // 
            this.C1SuperTooltipDx.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltipDx.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 54);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(1072, 32);
            this.panel5.TabIndex = 230;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblCloseDate);
            this.panel2.Controls.Add(this.lblAlertMessage);
            this.panel2.Controls.Add(this.lblPaymentTray);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label90);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1066, 26);
            this.panel2.TabIndex = 0;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDate.Location = new System.Drawing.Point(589, 6);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(0, 14);
            this.lblCloseDate.TabIndex = 214;
            this.lblCloseDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAlertMessage
            // 
            this.lblAlertMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlertMessage.AutoSize = true;
            this.lblAlertMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMessage.Location = new System.Drawing.Point(17, 6);
            this.lblAlertMessage.Name = "lblAlertMessage";
            this.lblAlertMessage.Size = new System.Drawing.Size(0, 14);
            this.lblAlertMessage.TabIndex = 213;
            this.lblAlertMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(779, 6);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(281, 14);
            this.lblPaymentTray.TabIndex = 212;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label42.AutoSize = true;
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(510, 6);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(73, 14);
            this.label42.TabIndex = 208;
            this.label42.Text = "Close Date :";
            // 
            // label90
            // 
            this.label90.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(685, 6);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 14);
            this.label90.TabIndex = 210;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(1065, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 24);
            this.label5.TabIndex = 205;
            this.label5.Text = "Close Date :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(0, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 24);
            this.label7.TabIndex = 204;
            this.label7.Text = "Close Date :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(0, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1066, 1);
            this.label8.TabIndex = 203;
            this.label8.Text = "Close Date :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1066, 1);
            this.label9.TabIndex = 202;
            this.label9.Text = "Close Date :";
            // 
            // pnlCorrection
            // 
            this.pnlCorrection.BackColor = System.Drawing.Color.Transparent;
            this.pnlCorrection.Controls.Add(this.c1FlexDeltaRemittance);
            this.pnlCorrection.Controls.Add(this.panel6);
            this.pnlCorrection.Controls.Add(this.label12);
            this.pnlCorrection.Controls.Add(this.label13);
            this.pnlCorrection.Controls.Add(this.label14);
            this.pnlCorrection.Controls.Add(this.label15);
            this.pnlCorrection.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCorrection.Location = new System.Drawing.Point(0, 193);
            this.pnlCorrection.Name = "pnlCorrection";
            this.pnlCorrection.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlCorrection.Size = new System.Drawing.Size(1072, 194);
            this.pnlCorrection.TabIndex = 231;
            // 
            // c1FlexDeltaRemittance
            // 
            this.c1FlexDeltaRemittance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexDeltaRemittance.AllowEditing = false;
            this.c1FlexDeltaRemittance.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Custom;
            this.c1FlexDeltaRemittance.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexDeltaRemittance.AutoGenerateColumns = false;
            this.c1FlexDeltaRemittance.BackColor = System.Drawing.Color.White;
            this.c1FlexDeltaRemittance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexDeltaRemittance.ColumnInfo = resources.GetString("c1FlexDeltaRemittance.ColumnInfo");
            this.c1FlexDeltaRemittance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexDeltaRemittance.ExtendLastCol = true;
            this.c1FlexDeltaRemittance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexDeltaRemittance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexDeltaRemittance.Location = new System.Drawing.Point(4, 28);
            this.c1FlexDeltaRemittance.Name = "c1FlexDeltaRemittance";
            this.c1FlexDeltaRemittance.Padding = new System.Windows.Forms.Padding(3);
            this.c1FlexDeltaRemittance.Rows.Count = 1;
            this.c1FlexDeltaRemittance.Rows.DefaultSize = 19;
            this.c1FlexDeltaRemittance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexDeltaRemittance.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlexDeltaRemittance.Size = new System.Drawing.Size(1064, 165);
            this.c1FlexDeltaRemittance.StyleInfo = resources.GetString("c1FlexDeltaRemittance.StyleInfo");
            this.c1FlexDeltaRemittance.TabIndex = 117;
            this.c1FlexDeltaRemittance.Tag = "ClosePeriod";
            this.c1FlexDeltaRemittance.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1FlexDeltaRemittance_AfterScroll);
            this.c1FlexDeltaRemittance.EnterCell += new System.EventHandler(this.c1FlexDeltaRemittance_EnterCell);
            this.c1FlexDeltaRemittance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexDeltaRemittance_MouseMove);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1064, 24);
            this.panel6.TabIndex = 118;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1064, 23);
            this.label16.TabIndex = 229;
            this.label16.Text = "  Correction (+ or -) :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1064, 1);
            this.label11.TabIndex = 116;
            this.label11.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 193);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1064, 1);
            this.label12.TabIndex = 116;
            this.label12.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1064, 1);
            this.label13.TabIndex = 115;
            this.label13.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(1068, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 191);
            this.label14.TabIndex = 113;
            this.label14.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 191);
            this.label15.TabIndex = 112;
            this.label15.Text = "label2";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 387);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1072, 3);
            this.splitter1.TabIndex = 232;
            this.splitter1.TabStop = false;
            // 
            // frmViewClaimRemittanceV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1072, 588);
            this.Controls.Add(this.panel20);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlCorrection);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewClaimRemittanceV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Claim Remittance";
            this.Load += new System.EventHandler(this.frmViewClaimRemittanceV2_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.panel20.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexRemittance)).EndInit();
            this.pnlTotalCaptionPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlCorrection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexDeltaRemittance)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        internal System.Windows.Forms.ToolStripButton tsbViewInsPmnt;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexRemittance;
        private System.Windows.Forms.Label lblInsCompany;
        private System.Windows.Forms.Label lblChkNoCap;
        private System.Windows.Forms.Label lblRefNoCap;
        private System.Windows.Forms.Label lblCheckDateCap;
        private System.Windows.Forms.Label lblInsCmpnyCap;
        private System.Windows.Forms.Label lblChkNo;
        private System.Windows.Forms.Label lblRefNo;
        private System.Windows.Forms.Label lblCheckDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.Label lblAlertMessage;
        private System.Windows.Forms.Label lblPaymentTray;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPatientCaption;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblClaim;
        private System.Windows.Forms.Label lblClaimCaption;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.Label lblPlanCaption;
        private System.Windows.Forms.Panel pnlTotalCaptionPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlCorrection;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexDeltaRemittance;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.ToolStripButton tsbViewHistory;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton ts_btnPrint;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltipDx;
    }
}