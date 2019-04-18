namespace gloAppointmentScheduling
{
    partial class frmRpt_NoShowAppointments
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpFromDate, dtpToDate };
            System.Windows.Forms.Control[] cntControls = { dtpFromDate, dtpToDate };

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


            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_NoShowAppointments));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnShowReport = new System.Windows.Forms.ToolStripButton();
            this.tlsPrintPatientsOn = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNoShowAppointments = new System.Windows.Forms.RadioButton();
            this.rbCancelAppointments = new System.Windows.Forms.RadioButton();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnl_BaseLeftBrd = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.c1Appointments = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbDeletedAppointments = new System.Windows.Forms.RadioButton();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(969, 55);
            this.pnlToolStrip.TabIndex = 2;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnShowReport,
            this.tlsPrintPatientsOn,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnOK,
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(969, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // ts_btnShowReport
            // 
            this.ts_btnShowReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnShowReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnShowReport.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnShowReport.Image")));
            this.ts_btnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnShowReport.Name = "ts_btnShowReport";
            this.ts_btnShowReport.Size = new System.Drawing.Size(93, 50);
            this.ts_btnShowReport.Tag = "Show Report";
            this.ts_btnShowReport.Text = "&Show Report";
            this.ts_btnShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnShowReport.Click += new System.EventHandler(this.ts_btnShowReport_Click);
            // 
            // tlsPrintPatientsOn
            // 
            this.tlsPrintPatientsOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsPrintPatientsOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsPrintPatientsOn.Image = ((System.Drawing.Image)(resources.GetObject("tlsPrintPatientsOn.Image")));
            this.tlsPrintPatientsOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsPrintPatientsOn.Name = "tlsPrintPatientsOn";
            this.tlsPrintPatientsOn.Size = new System.Drawing.Size(41, 50);
            this.tlsPrintPatientsOn.Tag = "LineDetails";
            this.tlsPrintPatientsOn.Text = "&Print";
            this.tlsPrintPatientsOn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsPrintPatientsOn.Visible = false;
            this.tlsPrintPatientsOn.Click += new System.EventHandler(this.tlsPrintPatientsOn_Click);
            // 
            // tls_btnExportToExcelOpen
            // 
            this.tls_btnExportToExcelOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcelOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcelOpen.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcelOpen.Image")));
            this.tls_btnExportToExcelOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcelOpen.Name = "tls_btnExportToExcelOpen";
            this.tls_btnExportToExcelOpen.Size = new System.Drawing.Size(154, 50);
            this.tls_btnExportToExcelOpen.Tag = "ExportnOpen";
            this.tls_btnExportToExcelOpen.Text = "Export To Excel && Open";
            this.tls_btnExportToExcelOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcelOpen.ToolTipText = "Export To Excel and Open";
            this.tls_btnExportToExcelOpen.Click += new System.EventHandler(this.tls_btnExportToExcelOpen_Click);
            // 
            // tls_btnExportToExcel
            // 
            this.tls_btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcel.Image")));
            this.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcel.Name = "tls_btnExportToExcel";
            this.tls_btnExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tls_btnExportToExcel.Tag = "Export";
            this.tls_btnExportToExcel.Text = "Export To Excel";
            this.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcel.Click += new System.EventHandler(this.tls_btnExportToExcel_Click);
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Save&&Cls";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.ToolTipText = "Save and Close";
            this.tls_btnOK.Visible = false;
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnClose.Image")));
            this.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tls_btnClose.Tag = "Cancel";
            this.tls_btnClose.Text = "&Close";
            this.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.groupBox1);
            this.pnlFilter.Controls.Add(this.cmb_datefilter);
            this.pnlFilter.Controls.Add(this.lbl_datefilter);
            this.pnlFilter.Controls.Add(this.label9);
            this.pnlFilter.Controls.Add(this.label8);
            this.pnlFilter.Controls.Add(this.cmbProviders);
            this.pnlFilter.Controls.Add(this.cmbLocation);
            this.pnlFilter.Controls.Add(this.label3);
            this.pnlFilter.Controls.Add(this.label2);
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Controls.Add(this.lbl_pnl_BaseLeftBrd);
            this.pnlFilter.Controls.Add(this.lblToDate);
            this.pnlFilter.Controls.Add(this.lblFromDate);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 55);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFilter.Size = new System.Drawing.Size(969, 122);
            this.pnlFilter.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDeletedAppointments);
            this.groupBox1.Controls.Add(this.rbNoShowAppointments);
            this.groupBox1.Controls.Add(this.rbCancelAppointments);
            this.groupBox1.Location = new System.Drawing.Point(320, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 30);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbNoShowAppointments
            // 
            this.rbNoShowAppointments.AutoSize = true;
            this.rbNoShowAppointments.Location = new System.Drawing.Point(92, 9);
            this.rbNoShowAppointments.Name = "rbNoShowAppointments";
            this.rbNoShowAppointments.Size = new System.Drawing.Size(75, 18);
            this.rbNoShowAppointments.TabIndex = 1;
            this.rbNoShowAppointments.Text = "No Show";
            this.rbNoShowAppointments.UseVisualStyleBackColor = true;
            this.rbNoShowAppointments.CheckedChanged += new System.EventHandler(this.rbCancelAppointments_CheckedChanged);
            // 
            // rbCancelAppointments
            // 
            this.rbCancelAppointments.AutoSize = true;
            this.rbCancelAppointments.Checked = true;
            this.rbCancelAppointments.Location = new System.Drawing.Point(15, 9);
            this.rbCancelAppointments.Name = "rbCancelAppointments";
            this.rbCancelAppointments.Size = new System.Drawing.Size(60, 18);
            this.rbCancelAppointments.TabIndex = 0;
            this.rbCancelAppointments.TabStop = true;
            this.rbCancelAppointments.Text = "Cancel";
            this.rbCancelAppointments.UseVisualStyleBackColor = true;
            this.rbCancelAppointments.CheckedChanged += new System.EventHandler(this.rbCancelAppointments_CheckedChanged);
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.ForeColor = System.Drawing.Color.Black;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(140, 15);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(129, 22);
            this.cmb_datefilter.TabIndex = 0;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(19, 19);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(117, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Appointment Date :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(319, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "Provider : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(317, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "Location : ";
            // 
            // cmbProviders
            // 
            this.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviders.ForeColor = System.Drawing.Color.Black;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(386, 83);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(160, 22);
            this.cmbProviders.TabIndex = 5;
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.ForeColor = System.Drawing.Color.Black;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(386, 49);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(160, 22);
            this.cmbLocation.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(961, 1);
            this.label3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(961, 1);
            this.label2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(965, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 116);
            this.label1.TabIndex = 7;
            // 
            // lbl_pnl_BaseLeftBrd
            // 
            this.lbl_pnl_BaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_BaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_BaseLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnl_BaseLeftBrd.Name = "lbl_pnl_BaseLeftBrd";
            this.lbl_pnl_BaseLeftBrd.Size = new System.Drawing.Size(1, 116);
            this.lbl_pnl_BaseLeftBrd.TabIndex = 6;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(76, 87);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 14);
            this.lblToDate.TabIndex = 5;
            this.lblToDate.Text = "To Date :";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(64, 53);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(72, 14);
            this.lblFromDate.TabIndex = 3;
            this.lblFromDate.Text = "From Date :";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpToDate.CustomFormat = "MM/dd/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(140, 83);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(129, 22);
            this.dtpToDate.TabIndex = 4;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpFromDate.CustomFormat = "MM/dd/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(140, 49);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(129, 22);
            this.dtpFromDate.TabIndex = 2;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.c1Appointments);
            this.pnlContainer.Controls.Add(this.label7);
            this.pnlContainer.Controls.Add(this.label6);
            this.pnlContainer.Controls.Add(this.label5);
            this.pnlContainer.Controls.Add(this.label4);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 177);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlContainer.Size = new System.Drawing.Size(969, 495);
            this.pnlContainer.TabIndex = 1;
            // 
            // c1Appointments
            // 
            this.c1Appointments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Appointments.AllowEditing = false;
            this.c1Appointments.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Appointments.AutoGenerateColumns = false;
            this.c1Appointments.AutoResize = false;
            this.c1Appointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Appointments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Appointments.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1Appointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Appointments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Appointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Appointments.Location = new System.Drawing.Point(4, 1);
            this.c1Appointments.Name = "c1Appointments";
            this.c1Appointments.Rows.DefaultSize = 19;
            this.c1Appointments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Appointments.ShowCellLabels = true;
            this.c1Appointments.Size = new System.Drawing.Size(961, 490);
            this.c1Appointments.StyleInfo = resources.GetString("c1Appointments.StyleInfo");
            this.c1Appointments.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(965, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 490);
            this.label7.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 490);
            this.label6.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 491);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(963, 1);
            this.label5.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(963, 1);
            this.label4.TabIndex = 9;
            // 
            // rbDeletedAppointments
            // 
            this.rbDeletedAppointments.AutoSize = true;
            this.rbDeletedAppointments.Location = new System.Drawing.Point(173, 10);
            this.rbDeletedAppointments.Name = "rbDeletedAppointments";
            this.rbDeletedAppointments.Size = new System.Drawing.Size(68, 18);
            this.rbDeletedAppointments.TabIndex = 1;
            this.rbDeletedAppointments.Text = "Deleted";
            this.rbDeletedAppointments.UseVisualStyleBackColor = true;
            this.rbDeletedAppointments.CheckedChanged += new System.EventHandler(this.rbCancelAppointments_CheckedChanged);
            // 
            // frmRpt_NoShowAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(969, 672);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_NoShowAppointments";
            this.Text = "Cancel Appointments";
            this.Load += new System.EventHandler(this.frmRpt_NoShowAppointments_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton ts_btnShowReport;
        private System.Windows.Forms.ToolStripButton tlsPrintPatientsOn;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbProviders;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pnl_BaseLeftBrd;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Panel pnlContainer;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Appointments;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNoShowAppointments;
        private System.Windows.Forms.RadioButton rbCancelAppointments;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        private System.Windows.Forms.RadioButton rbDeletedAppointments;
    }
}