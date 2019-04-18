namespace gloBilling.Collections
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
                    if (dtpStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);

                        }
                        catch
                        {
                        }


                        dtpStartDate.Dispose();
                        dtpStartDate = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtpEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);

                        }
                        catch
                        {
                        }


                        dtpEndDate.Dispose();
                        dtpEndDate = null;
                    }
                }
                catch
                {
                }


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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSSRSViewer));
            this.SSRSViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnViewReport = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExit = new System.Windows.Forms.ToolStripButton();
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseMultiActionCode = new System.Windows.Forms.Button();
            this.btnClearActionCode = new System.Windows.Forms.Button();
            this.cmbActionCode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowseMultiProvider = new System.Windows.Forms.Button();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGroupBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mskEnddate = new System.Windows.Forms.MaskedTextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lbl_pnlPaymentDetailsBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsTopBrd = new System.Windows.Forms.Label();
            this.tooltip_Billing = new System.Windows.Forms.ToolTip(this.components);
            this.chkExcludeReserves = new System.Windows.Forms.CheckBox();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlPaymentDetails.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSRSViewer
            // 
            this.SSRSViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSRSViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SSRSViewer.Location = new System.Drawing.Point(4, 133);
            this.SSRSViewer.Name = "SSRSViewer";
            this.SSRSViewer.Size = new System.Drawing.Size(937, 459);
            this.SSRSViewer.TabIndex = 15968;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(945, 54);
            this.pnlToolStrip.TabIndex = 7896;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnViewReport,
            this.tls_btnExit});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(945, 53);
            this.tls_Top.TabIndex = 9;
            this.tls_Top.TabStop = true;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnViewReport
            // 
            this.tls_btnViewReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnViewReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnViewReport.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnViewReport.Image")));
            this.tls_btnViewReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnViewReport.Name = "tls_btnViewReport";
            this.tls_btnViewReport.Size = new System.Drawing.Size(87, 50);
            this.tls_btnViewReport.Tag = "Close";
            this.tls_btnViewReport.Text = "&View Report";
            this.tls_btnViewReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
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
            this.pnlPaymentDetails.Controls.Add(this.panel1);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsBottomBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsLeftBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsRightBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsTopBrd);
            this.pnlPaymentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(0, 54);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPaymentDetails.Size = new System.Drawing.Size(945, 596);
            this.pnlPaymentDetails.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.chkExcludeReserves);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnBrowseMultiActionCode);
            this.panel1.Controls.Add(this.btnClearActionCode);
            this.panel1.Controls.Add(this.cmbActionCode);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnBrowseMultiProvider);
            this.panel1.Controls.Add(this.btnClearProvider);
            this.panel1.Controls.Add(this.cmbProvider);
            this.panel1.Controls.Add(this.label57);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbGroupBy);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 129);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(937, 1);
            this.label1.TabIndex = 2028;
            this.label1.Text = "label1";
            // 
            // btnBrowseMultiActionCode
            // 
            this.btnBrowseMultiActionCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiActionCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiActionCode.BackgroundImage")));
            this.btnBrowseMultiActionCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiActionCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiActionCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiActionCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiActionCode.Image")));
            this.btnBrowseMultiActionCode.Location = new System.Drawing.Point(410, 36);
            this.btnBrowseMultiActionCode.Name = "btnBrowseMultiActionCode";
            this.btnBrowseMultiActionCode.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiActionCode.TabIndex = 3;
            this.btnBrowseMultiActionCode.UseVisualStyleBackColor = false;
            this.btnBrowseMultiActionCode.Click += new System.EventHandler(this.btnBrowseMultiActionCode_Click);
            // 
            // btnClearActionCode
            // 
            this.btnClearActionCode.BackColor = System.Drawing.Color.Transparent;
            this.btnClearActionCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearActionCode.BackgroundImage")));
            this.btnClearActionCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearActionCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearActionCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearActionCode.Image = ((System.Drawing.Image)(resources.GetObject("btnClearActionCode.Image")));
            this.btnClearActionCode.Location = new System.Drawing.Point(435, 36);
            this.btnClearActionCode.Name = "btnClearActionCode";
            this.btnClearActionCode.Size = new System.Drawing.Size(22, 22);
            this.btnClearActionCode.TabIndex = 4;
            this.btnClearActionCode.UseVisualStyleBackColor = false;
            this.btnClearActionCode.Click += new System.EventHandler(this.btnClearActionCode_Click);
            // 
            // cmbActionCode
            // 
            this.cmbActionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionCode.FormattingEnabled = true;
            this.cmbActionCode.Location = new System.Drawing.Point(155, 38);
            this.cmbActionCode.Name = "cmbActionCode";
            this.cmbActionCode.Size = new System.Drawing.Size(249, 22);
            this.cmbActionCode.TabIndex = 2;
            this.cmbActionCode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbActionCode_MouseMove);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(11, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 14);
            this.label6.TabIndex = 2024;
            this.label6.Text = "Scheduled Action Code :";
            // 
            // btnBrowseMultiProvider
            // 
            this.btnBrowseMultiProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseMultiProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiProvider.BackgroundImage")));
            this.btnBrowseMultiProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseMultiProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseMultiProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseMultiProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseMultiProvider.Image")));
            this.btnBrowseMultiProvider.Location = new System.Drawing.Point(410, 68);
            this.btnBrowseMultiProvider.Name = "btnBrowseMultiProvider";
            this.btnBrowseMultiProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseMultiProvider.TabIndex = 6;
            this.btnBrowseMultiProvider.UseVisualStyleBackColor = false;
            this.btnBrowseMultiProvider.Click += new System.EventHandler(this.btnBrowseMultiProvider_Click);
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(435, 68);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearProvider.TabIndex = 7;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(155, 68);
            this.cmbProvider.MaxDropDownItems = 5;
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(249, 22);
            this.cmbProvider.TabIndex = 5;
            this.cmbProvider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbProvider_MouseMove);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(93, 72);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(59, 14);
            this.label57.TabIndex = 2020;
            this.label57.Text = "Provider :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 14);
            this.label5.TabIndex = 2019;
            this.label5.Text = "Group By :";
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupBy.FormattingEnabled = true;
            this.cmbGroupBy.Items.AddRange(new object[] {
            "None",
            "Provider"});
            this.cmbGroupBy.Location = new System.Drawing.Point(155, 98);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Size = new System.Drawing.Size(121, 22);
            this.cmbGroupBy.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 2017;
            this.label4.Text = "End Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 14);
            this.label3.TabIndex = 2016;
            this.label3.Text = "Start Date :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mskEnddate);
            this.panel4.Controls.Add(this.dtpEndDate);
            this.panel4.Location = new System.Drawing.Point(348, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(104, 23);
            this.panel4.TabIndex = 17632;
            this.panel4.TabStop = true;
            // 
            // mskEnddate
            // 
            this.mskEnddate.Dock = System.Windows.Forms.DockStyle.Left;
            this.mskEnddate.Location = new System.Drawing.Point(0, 0);
            this.mskEnddate.Mask = "00/00/0000";
            this.mskEnddate.Name = "mskEnddate";
            this.mskEnddate.Size = new System.Drawing.Size(74, 22);
            this.mskEnddate.TabIndex = 1;
            this.mskEnddate.ValidatingType = typeof(System.DateTime);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpEndDate.Location = new System.Drawing.Point(0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpEndDate.Size = new System.Drawing.Size(104, 22);
            this.dtpEndDate.TabIndex = 2011;
            this.dtpEndDate.CloseUp += new System.EventHandler(this.dtpEndDate_CloseUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mskStartDate);
            this.panel3.Controls.Add(this.dtpStartDate);
            this.panel3.Location = new System.Drawing.Point(155, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(104, 23);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // mskStartDate
            // 
            this.mskStartDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.mskStartDate.Location = new System.Drawing.Point(0, 0);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(74, 22);
            this.mskStartDate.TabIndex = 0;
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpStartDate.Location = new System.Drawing.Point(0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpStartDate.Size = new System.Drawing.Size(104, 22);
            this.dtpStartDate.TabIndex = 1011;
            this.dtpStartDate.CloseUp += new System.EventHandler(this.dtpStartDate_CloseUp);
            // 
            // lbl_pnlPaymentDetailsBottomBrd
            // 
            this.lbl_pnlPaymentDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlPaymentDetailsBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsBottomBrd.Location = new System.Drawing.Point(4, 592);
            this.lbl_pnlPaymentDetailsBottomBrd.Name = "lbl_pnlPaymentDetailsBottomBrd";
            this.lbl_pnlPaymentDetailsBottomBrd.Size = new System.Drawing.Size(937, 1);
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
            this.lbl_pnlPaymentDetailsLeftBrd.Size = new System.Drawing.Size(1, 589);
            this.lbl_pnlPaymentDetailsLeftBrd.TabIndex = 118;
            this.lbl_pnlPaymentDetailsLeftBrd.Text = "label4";
            // 
            // lbl_pnlPaymentDetailsRightBrd
            // 
            this.lbl_pnlPaymentDetailsRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlPaymentDetailsRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsRightBrd.Location = new System.Drawing.Point(941, 4);
            this.lbl_pnlPaymentDetailsRightBrd.Name = "lbl_pnlPaymentDetailsRightBrd";
            this.lbl_pnlPaymentDetailsRightBrd.Size = new System.Drawing.Size(1, 589);
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
            this.lbl_pnlPaymentDetailsTopBrd.Size = new System.Drawing.Size(939, 1);
            this.lbl_pnlPaymentDetailsTopBrd.TabIndex = 116;
            this.lbl_pnlPaymentDetailsTopBrd.Text = "label1";
            // 
            // chkExcludeReserves
            // 
            this.chkExcludeReserves.AutoSize = true;
            this.chkExcludeReserves.Location = new System.Drawing.Point(287, 100);
            this.chkExcludeReserves.Name = "chkExcludeReserves";
            this.chkExcludeReserves.Size = new System.Drawing.Size(170, 18);
            this.chkExcludeReserves.TabIndex = 9;
            this.chkExcludeReserves.Text = "Exclude Available Reserves";
            this.chkExcludeReserves.UseVisualStyleBackColor = true;
            // 
            // frmSSRSViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(945, 650);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmSSRSViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Follow-up Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSSRSViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmSSRSViewer_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlPaymentDetails.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsBottomBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsLeftBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsRightBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsTopBrd;
        public Microsoft.Reporting.WinForms.ReportViewer SSRSViewer;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MaskedTextBox mskEnddate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGroupBy;
        internal System.Windows.Forms.Button btnBrowseMultiProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label57;
        internal System.Windows.Forms.Button btnBrowseMultiActionCode;
        internal System.Windows.Forms.Button btnClearActionCode;
        private System.Windows.Forms.ComboBox cmbActionCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton tls_btnViewReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip tooltip_Billing;
        private System.Windows.Forms.CheckBox chkExcludeReserves;


    }
}
