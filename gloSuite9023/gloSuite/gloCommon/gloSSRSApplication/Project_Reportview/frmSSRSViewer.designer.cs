namespace SSRSApplication
{
    partial class frmSSRSViewer
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



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSSRSViewer));
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Accept = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_print = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExit = new System.Windows.Forms.ToolStripButton();
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.lbl_pnlPaymentDetailsBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsTopBrd = new System.Windows.Forms.Label();
            this.pnlPaymentTrayDate = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.btnTraySelection = new System.Windows.Forms.Button();
            this.lblPaymentTray = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblPayTray = new System.Windows.Forms.Label();
            this.pnlLastUpdatedOn = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblLastUpdatedOn = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlPaymentDetails.SuspendLayout();
            this.pnlPaymentTrayDate.SuspendLayout();
            this.pnlLastUpdatedOn.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(4, 4);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.Size = new System.Drawing.Size(955, 552);
            this.SSRSViewer.TabIndex = 0;
            this.SSRSViewer.PrintingBegin += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.SSRSViewer_PrintingBegin);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(963, 56);
            this.pnlToolStrip.TabIndex = 17;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Accept,
            this.tsbtn_print,
            this.tsbUpdate,
            this.tls_btnExit});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(963, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tsb_Accept
            // 
            this.tsb_Accept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Accept.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Accept.Image")));
            this.tsb_Accept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Accept.Name = "tsb_Accept";
            this.tsb_Accept.Size = new System.Drawing.Size(53, 50);
            this.tsb_Accept.Tag = "Accept";
            this.tsb_Accept.Text = "&Accept";
            this.tsb_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Accept.Click += new System.EventHandler(this.tsb_Accept_Click);
            // 
            // tsbtn_print
            // 
            this.tsbtn_print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtn_print.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_print.Image")));
            this.tsbtn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_print.Name = "tsbtn_print";
            this.tsbtn_print.Size = new System.Drawing.Size(41, 50);
            this.tsbtn_print.Tag = "Print";
            this.tsbtn_print.Text = "&Print";
            this.tsbtn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_print.Click += new System.EventHandler(this.tsbtn_print_Click);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(91, 50);
            this.tsbUpdate.Tag = "Delta Update";
            this.tsbUpdate.Text = "&Delta Update";
            this.tsbUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // tls_btnExit
            // 
            this.tls_btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExit.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExit.Image")));
            this.tls_btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExit.Name = "tls_btnExit";
            this.tls_btnExit.Size = new System.Drawing.Size(43, 50);
            this.tls_btnExit.Tag = "Close";
            this.tls_btnExit.Text = "&Close";
            this.tls_btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExit.Click += new System.EventHandler(this.tls_btnExit_Click);
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.Controls.Add(this.SSRSViewer);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsBottomBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsLeftBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsRightBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsTopBrd);
            this.pnlPaymentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(0, 112);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPaymentDetails.Size = new System.Drawing.Size(963, 560);
            this.pnlPaymentDetails.TabIndex = 18;
            // 
            // lbl_pnlPaymentDetailsBottomBrd
            // 
            this.lbl_pnlPaymentDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlPaymentDetailsBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsBottomBrd.Location = new System.Drawing.Point(4, 556);
            this.lbl_pnlPaymentDetailsBottomBrd.Name = "lbl_pnlPaymentDetailsBottomBrd";
            this.lbl_pnlPaymentDetailsBottomBrd.Size = new System.Drawing.Size(955, 1);
            this.lbl_pnlPaymentDetailsBottomBrd.TabIndex = 119;
            this.lbl_pnlPaymentDetailsBottomBrd.Text = "label2";
            // 
            // lbl_pnlPaymentDetailsLeftBrd
            // 
            this.lbl_pnlPaymentDetailsLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlPaymentDetailsLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlPaymentDetailsLeftBrd.Name = "lbl_pnlPaymentDetailsLeftBrd";
            this.lbl_pnlPaymentDetailsLeftBrd.Size = new System.Drawing.Size(1, 553);
            this.lbl_pnlPaymentDetailsLeftBrd.TabIndex = 118;
            this.lbl_pnlPaymentDetailsLeftBrd.Text = "label4";
            // 
            // lbl_pnlPaymentDetailsRightBrd
            // 
            this.lbl_pnlPaymentDetailsRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlPaymentDetailsRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsRightBrd.Location = new System.Drawing.Point(959, 4);
            this.lbl_pnlPaymentDetailsRightBrd.Name = "lbl_pnlPaymentDetailsRightBrd";
            this.lbl_pnlPaymentDetailsRightBrd.Size = new System.Drawing.Size(1, 553);
            this.lbl_pnlPaymentDetailsRightBrd.TabIndex = 117;
            this.lbl_pnlPaymentDetailsRightBrd.Text = "label3";
            // 
            // lbl_pnlPaymentDetailsTopBrd
            // 
            this.lbl_pnlPaymentDetailsTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlPaymentDetailsTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlPaymentDetailsTopBrd.Name = "lbl_pnlPaymentDetailsTopBrd";
            this.lbl_pnlPaymentDetailsTopBrd.Size = new System.Drawing.Size(957, 1);
            this.lbl_pnlPaymentDetailsTopBrd.TabIndex = 116;
            this.lbl_pnlPaymentDetailsTopBrd.Text = "label1";
            // 
            // pnlPaymentTrayDate
            // 
            this.pnlPaymentTrayDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPaymentTrayDate.Controls.Add(this.lblProgress);
            this.pnlPaymentTrayDate.Controls.Add(this.prgProgress);
            this.pnlPaymentTrayDate.Controls.Add(this.btnTraySelection);
            this.pnlPaymentTrayDate.Controls.Add(this.lblPaymentTray);
            this.pnlPaymentTrayDate.Controls.Add(this.mskCloseDate);
            this.pnlPaymentTrayDate.Controls.Add(this.lblCloseDate);
            this.pnlPaymentTrayDate.Controls.Add(this.label5);
            this.pnlPaymentTrayDate.Controls.Add(this.label9);
            this.pnlPaymentTrayDate.Controls.Add(this.label21);
            this.pnlPaymentTrayDate.Controls.Add(this.label22);
            this.pnlPaymentTrayDate.Controls.Add(this.lblPayTray);
            this.pnlPaymentTrayDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPaymentTrayDate.Location = new System.Drawing.Point(0, 56);
            this.pnlPaymentTrayDate.Name = "pnlPaymentTrayDate";
            this.pnlPaymentTrayDate.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPaymentTrayDate.Size = new System.Drawing.Size(963, 30);
            this.pnlPaymentTrayDate.TabIndex = 24;
            this.pnlPaymentTrayDate.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProgress.Location = new System.Drawing.Point(354, 1);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(155, 25);
            this.lblProgress.TabIndex = 209;
            this.lblProgress.Text = "label23";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProgress.Visible = false;
            // 
            // prgProgress
            // 
            this.prgProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgProgress.Location = new System.Drawing.Point(509, 1);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(450, 25);
            this.prgProgress.TabIndex = 208;
            this.prgProgress.Visible = false;
            // 
            // btnTraySelection
            // 
            this.btnTraySelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraySelection.AutoEllipsis = true;
            this.btnTraySelection.BackColor = System.Drawing.Color.Transparent;
            this.btnTraySelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTraySelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraySelection.Image = ((System.Drawing.Image)(resources.GetObject("btnTraySelection.Image")));
            this.btnTraySelection.Location = new System.Drawing.Point(934, 3);
            this.btnTraySelection.Name = "btnTraySelection";
            this.btnTraySelection.Size = new System.Drawing.Size(21, 22);
            this.btnTraySelection.TabIndex = 4;
            this.btnTraySelection.TabStop = false;
            this.btnTraySelection.UseVisualStyleBackColor = false;
            this.btnTraySelection.Click += new System.EventHandler(this.btnTraySelection_Click);
            // 
            // lblPaymentTray
            // 
            this.lblPaymentTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentTray.Location = new System.Drawing.Point(649, 6);
            this.lblPaymentTray.Name = "lblPaymentTray";
            this.lblPaymentTray.Size = new System.Drawing.Size(279, 15);
            this.lblPaymentTray.TabIndex = 207;
            this.lblPaymentTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mskCloseDate.Location = new System.Drawing.Point(449, 2);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(97, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.TabStop = false;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCloseDate_MouseClick);
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDate.Location = new System.Drawing.Point(373, 6);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(73, 14);
            this.lblCloseDate.TabIndex = 0;
            this.lblCloseDate.Text = "Close Date :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(959, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 25);
            this.label5.TabIndex = 205;
            this.label5.Text = "Close Date :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 25);
            this.label9.TabIndex = 204;
            this.label9.Text = "Close Date :";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(3, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(957, 1);
            this.label21.TabIndex = 203;
            this.label21.Text = "Close Date :";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(957, 1);
            this.label22.TabIndex = 202;
            this.label22.Text = "Close Date :";
            // 
            // lblPayTray
            // 
            this.lblPayTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayTray.AutoSize = true;
            this.lblPayTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayTray.Location = new System.Drawing.Point(552, 6);
            this.lblPayTray.Name = "lblPayTray";
            this.lblPayTray.Size = new System.Drawing.Size(91, 14);
            this.lblPayTray.TabIndex = 2;
            this.lblPayTray.Text = "Payment Tray :";
            this.lblPayTray.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLastUpdatedOn
            // 
            this.pnlLastUpdatedOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLastUpdatedOn.Controls.Add(this.lblPleaseWait);
            this.pnlLastUpdatedOn.Controls.Add(this.lblLastUpdatedOn);
            this.pnlLastUpdatedOn.Controls.Add(this.label4);
            this.pnlLastUpdatedOn.Controls.Add(this.label6);
            this.pnlLastUpdatedOn.Controls.Add(this.label7);
            this.pnlLastUpdatedOn.Controls.Add(this.label8);
            this.pnlLastUpdatedOn.Controls.Add(this.label10);
            this.pnlLastUpdatedOn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLastUpdatedOn.Location = new System.Drawing.Point(0, 86);
            this.pnlLastUpdatedOn.Name = "pnlLastUpdatedOn";
            this.pnlLastUpdatedOn.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlLastUpdatedOn.Size = new System.Drawing.Size(963, 26);
            this.pnlLastUpdatedOn.TabIndex = 25;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.ForeColor = System.Drawing.Color.Red;
            this.lblPleaseWait.Location = new System.Drawing.Point(428, 1);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(316, 24);
            this.lblPleaseWait.TabIndex = 208;
            this.lblPleaseWait.Text = "Please wait while report is being updated...";
            this.lblPleaseWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPleaseWait.Visible = false;
            // 
            // lblLastUpdatedOn
            // 
            this.lblLastUpdatedOn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastUpdatedOn.BackColor = System.Drawing.Color.Transparent;
            this.lblLastUpdatedOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdatedOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLastUpdatedOn.Location = new System.Drawing.Point(125, 6);
            this.lblLastUpdatedOn.Name = "lblLastUpdatedOn";
            this.lblLastUpdatedOn.Size = new System.Drawing.Size(279, 14);
            this.lblLastUpdatedOn.TabIndex = 207;
            this.lblLastUpdatedOn.Text = "Date";
            this.lblLastUpdatedOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(959, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 24);
            this.label4.TabIndex = 205;
            this.label4.Text = "Close Date :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 24);
            this.label6.TabIndex = 204;
            this.label6.Text = "Close Date :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(3, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(957, 1);
            this.label7.TabIndex = 203;
            this.label7.Text = "Close Date :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(957, 1);
            this.label8.TabIndex = 202;
            this.label8.Text = "Close Date :";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(12, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 14);
            this.label10.TabIndex = 2;
            this.label10.Text = "Last Updated On :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSSRSViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(963, 672);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.pnlLastUpdatedOn);
            this.Controls.Add(this.pnlPaymentTrayDate);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmSSRSViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSRS Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSSRSViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmSSRSViewer_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlPaymentDetails.ResumeLayout(false);
            this.pnlPaymentTrayDate.ResumeLayout(false);
            this.pnlPaymentTrayDate.PerformLayout();
            this.pnlLastUpdatedOn.ResumeLayout(false);
            this.pnlLastUpdatedOn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnExit;
        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsBottomBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsLeftBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsRightBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsTopBrd;
        private System.Windows.Forms.ToolStripButton tsb_Accept;
        private System.Windows.Forms.Panel pnlPaymentTrayDate;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblPayTray;
        public System.Windows.Forms.MaskedTextBox mskCloseDate;
        public System.Windows.Forms.Button btnTraySelection;
        public System.Windows.Forms.Label lblPaymentTray;
        public Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ToolStripButton tsbtn_print;
        private System.Windows.Forms.Panel pnlLastUpdatedOn;
        public System.Windows.Forms.Label lblLastUpdatedOn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.ToolStripButton tsbUpdate;


    }
}
