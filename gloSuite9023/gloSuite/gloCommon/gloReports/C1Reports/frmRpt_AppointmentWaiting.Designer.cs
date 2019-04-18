namespace gloReports.C1Reports
{
    partial class frmRpt_AppointmentWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_AppointmentWaiting));
            this.PnlC1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.c1AppointmentWaiting = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_GenerateReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnExportReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.fpnlCriteria = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PnlC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1AppointmentWaiting)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.fpnlCriteria.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlC1
            // 
            this.PnlC1.BackColor = System.Drawing.Color.Transparent;
            this.PnlC1.Controls.Add(this.label10);
            this.PnlC1.Controls.Add(this.label4);
            this.PnlC1.Controls.Add(this.label3);
            this.PnlC1.Controls.Add(this.label2);
            this.PnlC1.Controls.Add(this.c1AppointmentWaiting);
            this.PnlC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlC1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.PnlC1.Location = new System.Drawing.Point(0, 171);
            this.PnlC1.Name = "PnlC1";
            this.PnlC1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.PnlC1.Size = new System.Drawing.Size(1262, 669);
            this.PnlC1.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1254, 1);
            this.label10.TabIndex = 94;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1258, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 665);
            this.label4.TabIndex = 93;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 665);
            this.label3.TabIndex = 92;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 665);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1256, 1);
            this.label2.TabIndex = 91;
            // 
            // c1AppointmentWaiting
            // 
            this.c1AppointmentWaiting.AllowEditing = false;
            this.c1AppointmentWaiting.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1AppointmentWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1AppointmentWaiting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1AppointmentWaiting.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1AppointmentWaiting.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1AppointmentWaiting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1AppointmentWaiting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1AppointmentWaiting.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1AppointmentWaiting.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1AppointmentWaiting.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1AppointmentWaiting.Location = new System.Drawing.Point(3, 0);
            this.c1AppointmentWaiting.Name = "c1AppointmentWaiting";
            this.c1AppointmentWaiting.Rows.Count = 1;
            this.c1AppointmentWaiting.Rows.DefaultSize = 19;
            this.c1AppointmentWaiting.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1AppointmentWaiting.Size = new System.Drawing.Size(1256, 666);
            this.c1AppointmentWaiting.StyleInfo = resources.GetString("c1AppointmentWaiting.StyleInfo");
            this.c1AppointmentWaiting.TabIndex = 89;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1262, 55);
            this.pnlToolStrip.TabIndex = 29;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_GenerateReport,
            this.tsb_btnExportReport,
            this.tsb_Print,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1262, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_GenerateReport
            // 
            this.tsb_GenerateReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GenerateReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_GenerateReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GenerateReport.Image")));
            this.tsb_GenerateReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GenerateReport.Name = "tsb_GenerateReport";
            this.tsb_GenerateReport.Size = new System.Drawing.Size(113, 50);
            this.tsb_GenerateReport.Tag = "Generate Report";
            this.tsb_GenerateReport.Text = "&Generate Report";
            this.tsb_GenerateReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GenerateReport.ToolTipText = "Generate Report";
            this.tsb_GenerateReport.Click += new System.EventHandler(this.tsb_GenerateReport_Click);
            // 
            // tsb_btnExportReport
            // 
            this.tsb_btnExportReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnExportReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnExportReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnExportReport.Image")));
            this.tsb_btnExportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnExportReport.Name = "tsb_btnExportReport";
            this.tsb_btnExportReport.Size = new System.Drawing.Size(52, 50);
            this.tsb_btnExportReport.Tag = "Export";
            this.tsb_btnExportReport.Text = "Export";
            this.tsb_btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnExportReport.Visible = false;
            this.tsb_btnExportReport.Click += new System.EventHandler(this.tsb_btnExportReport_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
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
            // fpnlCriteria
            // 
            this.fpnlCriteria.Controls.Add(this.pnlTransDate);
            this.fpnlCriteria.Controls.Add(this.pnlDates);
            this.fpnlCriteria.Controls.Add(this.pnlPatients);
            this.fpnlCriteria.Controls.Add(this.pnlProvider);
            this.fpnlCriteria.Controls.Add(this.pnlLocation);
            this.fpnlCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnlCriteria.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpnlCriteria.Location = new System.Drawing.Point(4, 3);
            this.fpnlCriteria.Name = "fpnlCriteria";
            this.fpnlCriteria.Size = new System.Drawing.Size(1254, 110);
            this.fpnlCriteria.TabIndex = 256;
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Controls.Add(this.cmb_datefilter);
            this.pnlTransDate.Location = new System.Drawing.Point(3, 3);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(275, 29);
            this.pnlTransDate.TabIndex = 205;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(5, 6);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(117, 2);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(151, 22);
            this.cmb_datefilter.TabIndex = 217;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.dtpEndDate);
            this.pnlDates.Controls.Add(this.lblStartDate);
            this.pnlDates.Controls.Add(this.lblEndDate);
            this.pnlDates.Controls.Add(this.dtpStartDate);
            this.pnlDates.Location = new System.Drawing.Point(3, 38);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(275, 67);
            this.pnlDates.TabIndex = 206;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(118, 33);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(105, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(41, 9);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(47, 37);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(118, 7);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(105, 22);
            this.dtpStartDate.TabIndex = 5;
            // 
            // pnlPatients
            // 
            this.pnlPatients.Controls.Add(this.cmbPatients);
            this.pnlPatients.Controls.Add(this.lblPatient);
            this.pnlPatients.Controls.Add(this.btnBrowsePatient);
            this.pnlPatients.Controls.Add(this.btnClearPatient);
            this.pnlPatients.Location = new System.Drawing.Point(284, 3);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Size = new System.Drawing.Size(328, 29);
            this.pnlPatients.TabIndex = 207;
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(79, 4);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(189, 22);
            this.cmbPatients.TabIndex = 197;
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(21, 8);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 196;
            this.lblPatient.Text = "Patient :";
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(272, 3);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(21, 21);
            this.btnBrowsePatient.TabIndex = 194;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(297, 3);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(21, 21);
            this.btnClearPatient.TabIndex = 195;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            // 
            // pnlProvider
            // 
            this.pnlProvider.Controls.Add(this.label5);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.btnClearProvider);
            this.pnlProvider.Controls.Add(this.btnBrowseProvider);
            this.pnlProvider.Location = new System.Drawing.Point(284, 38);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(328, 29);
            this.pnlProvider.TabIndex = 208;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(79, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(189, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(297, 3);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(21, 21);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(272, 3);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.label8);
            this.pnlLocation.Controls.Add(this.cmbLocation);
            this.pnlLocation.Location = new System.Drawing.Point(284, 73);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(328, 29);
            this.pnlLocation.TabIndex = 221;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 14);
            this.label8.TabIndex = 14;
            this.label8.Text = "Location : ";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(81, 3);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(189, 22);
            this.cmbLocation.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.fpnlCriteria);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(1262, 116);
            this.panel1.TabIndex = 257;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(4, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1254, 1);
            this.label9.TabIndex = 97;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1254, 1);
            this.label7.TabIndex = 96;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 110);
            this.label6.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1258, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 110);
            this.label1.TabIndex = 94;
            // 
            // frmRpt_AppointmentWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1262, 840);
            this.Controls.Add(this.PnlC1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_AppointmentWaiting";
            this.ShowInTaskbar = false;
            this.Text = "Waiting Appointments";
            this.LocationChanged += new System.EventHandler(this.frmRpt_AppointmentWaiting_LocationChanged);
            this.Load += new System.EventHandler(this.frmRpt_AppointmentWaiting_Load);
            this.PnlC1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1AppointmentWaiting)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.fpnlCriteria.ResumeLayout(false);
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.pnlPatients.PerformLayout();
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlC1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1AppointmentWaiting;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_GenerateReport;
        private System.Windows.Forms.ToolStripButton tsb_btnExportReport;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.FlowLayoutPanel fpnlCriteria;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Button btnBrowsePatient;
        internal System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Panel pnlProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
    }
}