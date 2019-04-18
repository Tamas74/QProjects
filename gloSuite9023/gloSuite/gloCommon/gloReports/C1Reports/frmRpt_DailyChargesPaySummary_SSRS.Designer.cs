namespace gloReports.C1Reports
{
    partial class frmRpt_DailyChargesPaySummary_SSRS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_DailyChargesPaySummary_SSRS));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_btnDailyClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabSummary = new System.Windows.Forms.TabControl();
            this.tabPgCharges = new System.Windows.Forms.TabPage();
            this.SSRSViewerCharge = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPgPayment = new System.Windows.Forms.TabPage();
            this.SSRSViewerPayment = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPgClosedTray = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SSRSViewerClose = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlCloseDaySearch = new System.Windows.Forms.Panel();
            this.pnlLSTMonths = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.lbl_pnlProviderBottomBrd = new System.Windows.Forms.Label();
            this.pnlProviderBody = new System.Windows.Forms.Panel();
            this.trvMonths = new System.Windows.Forms.TreeView();
            this.pnlProviderHeader = new System.Windows.Forms.Panel();
            this.btnDeSelectCreditCard = new System.Windows.Forms.Button();
            this.btnSelectCreditCard = new System.Windows.Forms.Button();
            this.lbl_pnlProviderHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTopBrd = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.lblUserNm = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.lblLstClosedDt = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabPgCharges.SuspendLayout();
            this.tabPgPayment.SuspendLayout();
            this.tabPgClosedTray.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCloseDaySearch.SuspendLayout();
            this.pnlLSTMonths.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlProviderBody.SuspendLayout();
            this.pnlProviderHeader.SuspendLayout();
            this.panel15.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1284, 54);
            this.pnlToolStrip.TabIndex = 30;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_btnDailyClose,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1284, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_btnDailyClose
            // 
            this.tsb_btnDailyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnDailyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnDailyClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnDailyClose.Image")));
            this.tsb_btnDailyClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnDailyClose.Name = "tsb_btnDailyClose";
            this.tsb_btnDailyClose.Size = new System.Drawing.Size(80, 50);
            this.tsb_btnDailyClose.Tag = "DailyClose ";
            this.tsb_btnDailyClose.Text = "&Daily Close ";
            this.tsb_btnDailyClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnDailyClose.ToolTipText = "Daily Close ";
            this.tsb_btnDailyClose.Visible = false;
            this.tsb_btnDailyClose.Click += new System.EventHandler(this.tsb_btnDailyClose_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.tabSummary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 539);
            this.panel1.TabIndex = 268;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.tabPgCharges);
            this.tabSummary.Controls.Add(this.tabPgPayment);
            this.tabSummary.Controls.Add(this.tabPgClosedTray);
            this.tabSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSummary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSummary.ImageList = this.imageList1;
            this.tabSummary.Location = new System.Drawing.Point(0, 0);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.SelectedIndex = 0;
            this.tabSummary.Size = new System.Drawing.Size(1284, 539);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.SelectedIndexChanged += new System.EventHandler(this.tabSummary_SelectedIndexChanged);
            // 
            // tabPgCharges
            // 
            this.tabPgCharges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgCharges.Controls.Add(this.SSRSViewerCharge);
            this.tabPgCharges.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tabPgCharges.ImageIndex = 0;
            this.tabPgCharges.Location = new System.Drawing.Point(4, 23);
            this.tabPgCharges.Name = "tabPgCharges";
            this.tabPgCharges.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabPgCharges.Size = new System.Drawing.Size(1276, 512);
            this.tabPgCharges.TabIndex = 0;
            this.tabPgCharges.Tag = "DailyCharges";
            this.tabPgCharges.Text = "Daily Charge Report";
            this.tabPgCharges.ToolTipText = "Daily Charge Report";
            // 
            // SSRSViewerCharge
            // 
            this.SSRSViewerCharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewerCharge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSRSViewerCharge.Location = new System.Drawing.Point(0, 3);
            this.SSRSViewerCharge.Name = "SSRSViewerCharge";
            this.SSRSViewerCharge.Size = new System.Drawing.Size(1276, 509);
            this.SSRSViewerCharge.TabIndex = 1;
            // 
            // tabPgPayment
            // 
            this.tabPgPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgPayment.Controls.Add(this.SSRSViewerPayment);
            this.tabPgPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tabPgPayment.ImageIndex = 1;
            this.tabPgPayment.Location = new System.Drawing.Point(4, 23);
            this.tabPgPayment.Name = "tabPgPayment";
            this.tabPgPayment.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabPgPayment.Size = new System.Drawing.Size(1276, 512);
            this.tabPgPayment.TabIndex = 1;
            this.tabPgPayment.Tag = "DailyPayment";
            this.tabPgPayment.Text = "Daily Payment Report";
            this.tabPgPayment.ToolTipText = "DailyPayment";
            // 
            // SSRSViewerPayment
            // 
            this.SSRSViewerPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewerPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSRSViewerPayment.Location = new System.Drawing.Point(0, 3);
            this.SSRSViewerPayment.Name = "SSRSViewerPayment";
            this.SSRSViewerPayment.Size = new System.Drawing.Size(1276, 509);
            this.SSRSViewerPayment.TabIndex = 1;
            // 
            // tabPgClosedTray
            // 
            this.tabPgClosedTray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPgClosedTray.Controls.Add(this.panel5);
            this.tabPgClosedTray.Controls.Add(this.pnlCloseDaySearch);
            this.tabPgClosedTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPgClosedTray.ImageIndex = 2;
            this.tabPgClosedTray.Location = new System.Drawing.Point(4, 23);
            this.tabPgClosedTray.Name = "tabPgClosedTray";
            this.tabPgClosedTray.Size = new System.Drawing.Size(1276, 512);
            this.tabPgClosedTray.TabIndex = 2;
            this.tabPgClosedTray.Tag = "DailyClose";
            this.tabPgClosedTray.Text = "Daily Close ";
            this.tabPgClosedTray.ToolTipText = "Daily Close ";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.SSRSViewerClose);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 78);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel5.Size = new System.Drawing.Size(1276, 434);
            this.panel5.TabIndex = 271;
            // 
            // SSRSViewerClose
            // 
            this.SSRSViewerClose.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewerClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewerClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSRSViewerClose.Location = new System.Drawing.Point(1, 4);
            this.SSRSViewerClose.Name = "SSRSViewerClose";
            this.SSRSViewerClose.Size = new System.Drawing.Size(1274, 429);
            this.SSRSViewerClose.TabIndex = 275;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 433);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1274, 1);
            this.label10.TabIndex = 274;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(1, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1274, 1);
            this.label9.TabIndex = 273;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(1275, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 431);
            this.label8.TabIndex = 272;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 431);
            this.label6.TabIndex = 271;
            // 
            // pnlCloseDaySearch
            // 
            this.pnlCloseDaySearch.Controls.Add(this.pnlLSTMonths);
            this.pnlCloseDaySearch.Controls.Add(this.panel15);
            this.pnlCloseDaySearch.Controls.Add(this.pnlPatients);
            this.pnlCloseDaySearch.Controls.Add(this.label43);
            this.pnlCloseDaySearch.Controls.Add(this.label44);
            this.pnlCloseDaySearch.Controls.Add(this.label45);
            this.pnlCloseDaySearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCloseDaySearch.Location = new System.Drawing.Point(0, 0);
            this.pnlCloseDaySearch.Name = "pnlCloseDaySearch";
            this.pnlCloseDaySearch.Size = new System.Drawing.Size(1276, 78);
            this.pnlCloseDaySearch.TabIndex = 269;
            // 
            // pnlLSTMonths
            // 
            this.pnlLSTMonths.Controls.Add(this.panel16);
            this.pnlLSTMonths.Location = new System.Drawing.Point(257, 3);
            this.pnlLSTMonths.Name = "pnlLSTMonths";
            this.pnlLSTMonths.Size = new System.Drawing.Size(169, 71);
            this.pnlLSTMonths.TabIndex = 229;
            // 
            // panel16
            // 
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.lbl_pnlProviderBottomBrd);
            this.panel16.Controls.Add(this.pnlProviderBody);
            this.panel16.Controls.Add(this.pnlProviderHeader);
            this.panel16.Controls.Add(this.lbl_pnlProviderLeftBrd);
            this.panel16.Controls.Add(this.lbl_pnlProviderRightBrd);
            this.panel16.Controls.Add(this.lbl_pnlProviderTopBrd);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(169, 71);
            this.panel16.TabIndex = 302;
            // 
            // lbl_pnlProviderBottomBrd
            // 
            this.lbl_pnlProviderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBottomBrd.Location = new System.Drawing.Point(1, 70);
            this.lbl_pnlProviderBottomBrd.Name = "lbl_pnlProviderBottomBrd";
            this.lbl_pnlProviderBottomBrd.Size = new System.Drawing.Size(167, 1);
            this.lbl_pnlProviderBottomBrd.TabIndex = 97;
            // 
            // pnlProviderBody
            // 
            this.pnlProviderBody.Controls.Add(this.trvMonths);
            this.pnlProviderBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderBody.Location = new System.Drawing.Point(1, 24);
            this.pnlProviderBody.Name = "pnlProviderBody";
            this.pnlProviderBody.Size = new System.Drawing.Size(167, 47);
            this.pnlProviderBody.TabIndex = 92;
            // 
            // trvMonths
            // 
            this.trvMonths.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMonths.CheckBoxes = true;
            this.trvMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMonths.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvMonths.ForeColor = System.Drawing.Color.Black;
            this.trvMonths.Location = new System.Drawing.Point(0, 0);
            this.trvMonths.Name = "trvMonths";
            this.trvMonths.ShowLines = false;
            this.trvMonths.ShowPlusMinus = false;
            this.trvMonths.ShowRootLines = false;
            this.trvMonths.Size = new System.Drawing.Size(167, 47);
            this.trvMonths.TabIndex = 19;
            // 
            // pnlProviderHeader
            // 
            this.pnlProviderHeader.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.pnlProviderHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderHeader.Controls.Add(this.btnDeSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.btnSelectCreditCard);
            this.pnlProviderHeader.Controls.Add(this.lbl_pnlProviderHeaderBottomBrd);
            this.pnlProviderHeader.Controls.Add(this.lblCreditCard);
            this.pnlProviderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderHeader.Location = new System.Drawing.Point(1, 1);
            this.pnlProviderHeader.Name = "pnlProviderHeader";
            this.pnlProviderHeader.Size = new System.Drawing.Size(167, 23);
            this.pnlProviderHeader.TabIndex = 91;
            // 
            // btnDeSelectCreditCard
            // 
            this.btnDeSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnDeSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDeSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnDeSelectCreditCard.Image")));
            this.btnDeSelectCreditCard.Location = new System.Drawing.Point(105, 0);
            this.btnDeSelectCreditCard.Name = "btnDeSelectCreditCard";
            this.btnDeSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnDeSelectCreditCard.TabIndex = 101;
            this.btnDeSelectCreditCard.Tag = "Select";
            this.btnDeSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnDeSelectCreditCard.Visible = false;
            // 
            // btnSelectCreditCard
            // 
            this.btnSelectCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectCreditCard.FlatAppearance.BorderSize = 0;
            this.btnSelectCreditCard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectCreditCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCreditCard.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCreditCard.Image")));
            this.btnSelectCreditCard.Location = new System.Drawing.Point(136, 0);
            this.btnSelectCreditCard.Name = "btnSelectCreditCard";
            this.btnSelectCreditCard.Size = new System.Drawing.Size(31, 22);
            this.btnSelectCreditCard.TabIndex = 100;
            this.btnSelectCreditCard.Tag = "Select";
            this.btnSelectCreditCard.UseVisualStyleBackColor = false;
            this.btnSelectCreditCard.Visible = false;
            // 
            // lbl_pnlProviderHeaderBottomBrd
            // 
            this.lbl_pnlProviderHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderHeaderBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlProviderHeaderBottomBrd.Name = "lbl_pnlProviderHeaderBottomBrd";
            this.lbl_pnlProviderHeaderBottomBrd.Size = new System.Drawing.Size(167, 1);
            this.lbl_pnlProviderHeaderBottomBrd.TabIndex = 97;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreditCard.Location = new System.Drawing.Point(0, 0);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(167, 23);
            this.lblCreditCard.TabIndex = 0;
            this.lblCreditCard.Text = "  Day to close :";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_pnlProviderLeftBrd
            // 
            this.lbl_pnlProviderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLeftBrd.Name = "lbl_pnlProviderLeftBrd";
            this.lbl_pnlProviderLeftBrd.Size = new System.Drawing.Size(1, 70);
            this.lbl_pnlProviderLeftBrd.TabIndex = 93;
            // 
            // lbl_pnlProviderRightBrd
            // 
            this.lbl_pnlProviderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRightBrd.Location = new System.Drawing.Point(168, 1);
            this.lbl_pnlProviderRightBrd.Name = "lbl_pnlProviderRightBrd";
            this.lbl_pnlProviderRightBrd.Size = new System.Drawing.Size(1, 70);
            this.lbl_pnlProviderRightBrd.TabIndex = 94;
            // 
            // lbl_pnlProviderTopBrd
            // 
            this.lbl_pnlProviderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTopBrd.Name = "lbl_pnlProviderTopBrd";
            this.lbl_pnlProviderTopBrd.Size = new System.Drawing.Size(169, 1);
            this.lbl_pnlProviderTopBrd.TabIndex = 96;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.lblUserNm);
            this.panel15.Controls.Add(this.label52);
            this.panel15.Controls.Add(this.label54);
            this.panel15.Location = new System.Drawing.Point(5, 30);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(246, 22);
            this.panel15.TabIndex = 209;
            // 
            // lblUserNm
            // 
            this.lblUserNm.AutoSize = true;
            this.lblUserNm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUserNm.Location = new System.Drawing.Point(123, 4);
            this.lblUserNm.Name = "lblUserNm";
            this.lblUserNm.Size = new System.Drawing.Size(35, 14);
            this.lblUserNm.TabIndex = 198;
            this.lblUserNm.Text = "User ";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(123, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(0, 14);
            this.label52.TabIndex = 197;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Location = new System.Drawing.Point(73, 4);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(41, 14);
            this.label54.TabIndex = 196;
            this.label54.Text = "User :";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.lblLstClosedDt);
            this.pnlPatients.Controls.Add(this.label51);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Location = new System.Drawing.Point(5, 5);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(246, 22);
            this.pnlPatients.TabIndex = 208;
            // 
            // lblLstClosedDt
            // 
            this.lblLstClosedDt.AutoSize = true;
            this.lblLstClosedDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLstClosedDt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLstClosedDt.Location = new System.Drawing.Point(123, 4);
            this.lblLstClosedDt.Name = "lblLstClosedDt";
            this.lblLstClosedDt.Size = new System.Drawing.Size(20, 14);
            this.lblLstClosedDt.TabIndex = 198;
            this.lblLstClosedDt.Text = "Dt";
            this.lblLstClosedDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(123, 8);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(0, 14);
            this.label51.TabIndex = 197;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(7, 4);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(118, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Last Closed Date :";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(1, 77);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1274, 1);
            this.label43.TabIndex = 14;
            this.label43.Text = "label1";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(1275, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 78);
            this.label44.TabIndex = 9;
            this.label44.Text = "label4";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1, 78);
            this.label45.TabIndex = 8;
            this.label45.Text = "label4";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Daily Charges rpt.ico");
            this.imageList1.Images.SetKeyName(1, "Daily Payment rtp.ico");
            this.imageList1.Images.SetKeyName(2, "Daily close.ico");
            // 
            // frmRpt_DailyChargesPaySummary_SSRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_DailyChargesPaySummary_SSRS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Daily Charge ";
            this.Load += new System.EventHandler(this.frmRpt_DailyChargesPaySummary_SSRS_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabPgCharges.ResumeLayout(false);
            this.tabPgPayment.ResumeLayout(false);
            this.tabPgClosedTray.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlCloseDaySearch.ResumeLayout(false);
            this.pnlLSTMonths.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.pnlProviderBody.ResumeLayout(false);
            this.pnlProviderHeader.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.pnlPatients.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabSummary;
        private System.Windows.Forms.TabPage tabPgCharges;
        private System.Windows.Forms.TabPage tabPgPayment;
        private System.Windows.Forms.TabPage tabPgClosedTray;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlCloseDaySearch;
        private System.Windows.Forms.Panel pnlLSTMonths;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label lbl_pnlProviderBottomBrd;
        private System.Windows.Forms.Panel pnlProviderBody;
        private System.Windows.Forms.TreeView trvMonths;
        private System.Windows.Forms.Panel pnlProviderHeader;
        private System.Windows.Forms.Button btnDeSelectCreditCard;
        private System.Windows.Forms.Button btnSelectCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderHeaderBottomBrd;
        private System.Windows.Forms.Label lblCreditCard;
        private System.Windows.Forms.Label lbl_pnlProviderLeftBrd;
        private System.Windows.Forms.Label lbl_pnlProviderRightBrd;
        private System.Windows.Forms.Label lbl_pnlProviderTopBrd;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label lblUserNm;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Label lblLstClosedDt;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        internal System.Windows.Forms.ToolStripButton tsb_btnDailyClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private Microsoft.Reporting.WinForms.ReportViewer SSRSViewerCharge;
        private Microsoft.Reporting.WinForms.ReportViewer SSRSViewerPayment;
        private Microsoft.Reporting.WinForms.ReportViewer SSRSViewerClose;
    }
}