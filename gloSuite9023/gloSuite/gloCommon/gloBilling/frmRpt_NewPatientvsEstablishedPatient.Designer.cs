namespace gloBilling
{
    partial class frmRpt_NewPatientvsEstablishedPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_NewPatientvsEstablishedPatient));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls_NewvsEstablished = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.chkAppointmentType = new System.Windows.Forms.CheckBox();
            this.btnClearAppointmentType = new System.Windows.Forms.Button();
            this.btnBrowseAppointmentType = new System.Windows.Forms.Button();
            this.cmbApp_AppointmentType = new System.Windows.Forms.ComboBox();
            this.lblApp_AppointmentType = new System.Windows.Forms.Label();
            this.chkProvider = new System.Windows.Forms.CheckBox();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.grpSortCriteria = new System.Windows.Forms.GroupBox();
            this.rbSortNewPatient = new System.Windows.Forms.RadioButton();
            this.rbSortEstablishedPatient = new System.Windows.Forms.RadioButton();
            this.grpDates = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.pnlBalanceInfo = new System.Windows.Forms.Panel();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls_NewvsEstablished.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.grpSortCriteria.SuspendLayout();
            this.grpDates.SuspendLayout();
            this.pnlBalanceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls_NewvsEstablished);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(988, 55);
            this.pnl_tlspTOP.TabIndex = 5;
            // 
            // tls_NewvsEstablished
            // 
            this.tls_NewvsEstablished.BackColor = System.Drawing.Color.Transparent;
            this.tls_NewvsEstablished.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_NewvsEstablished.BackgroundImage")));
            this.tls_NewvsEstablished.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_NewvsEstablished.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls_NewvsEstablished.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_NewvsEstablished.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnRefresh,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.ts_btnCancel});
            this.tls_NewvsEstablished.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_NewvsEstablished.Location = new System.Drawing.Point(0, 0);
            this.tls_NewvsEstablished.Name = "tls_NewvsEstablished";
            this.tls_NewvsEstablished.Size = new System.Drawing.Size(988, 53);
            this.tls_NewvsEstablished.TabIndex = 0;
            this.tls_NewvsEstablished.Text = "toolStrip1";
            // 
            // ts_btnRefresh
            // 
            this.ts_btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRefresh.Image")));
            this.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRefresh.Name = "ts_btnRefresh";
            this.ts_btnRefresh.Size = new System.Drawing.Size(93, 50);
            this.ts_btnRefresh.Tag = "Show Report";
            this.ts_btnRefresh.Text = "&Show Report";
            this.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRefresh.Click += new System.EventHandler(this.ts_btnRefresh_Click);
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
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.cmb_datefilter);
            this.pnlCriteria.Controls.Add(this.lbl_datefilter);
            this.pnlCriteria.Controls.Add(this.chkAppointmentType);
            this.pnlCriteria.Controls.Add(this.btnClearAppointmentType);
            this.pnlCriteria.Controls.Add(this.btnBrowseAppointmentType);
            this.pnlCriteria.Controls.Add(this.cmbApp_AppointmentType);
            this.pnlCriteria.Controls.Add(this.lblApp_AppointmentType);
            this.pnlCriteria.Controls.Add(this.chkProvider);
            this.pnlCriteria.Controls.Add(this.btnClearProvider);
            this.pnlCriteria.Controls.Add(this.btnBrowseProvider);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.cmbProvider);
            this.pnlCriteria.Controls.Add(this.lbl_LeftBrd);
            this.pnlCriteria.Controls.Add(this.lbl_RightBrd);
            this.pnlCriteria.Controls.Add(this.lbl_TopBrd);
            this.pnlCriteria.Controls.Add(this.grpSortCriteria);
            this.pnlCriteria.Controls.Add(this.grpDates);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCriteria.Location = new System.Drawing.Point(0, 55);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria.Size = new System.Drawing.Size(988, 139);
            this.pnlCriteria.TabIndex = 90;
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(141, 17);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(103, 22);
            this.cmb_datefilter.TabIndex = 215;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(17, 21);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(117, 14);
            this.lbl_datefilter.TabIndex = 214;
            this.lbl_datefilter.Text = "Appointment Date :";
            // 
            // chkAppointmentType
            // 
            this.chkAppointmentType.AutoSize = true;
            this.chkAppointmentType.Location = new System.Drawing.Point(616, 59);
            this.chkAppointmentType.Name = "chkAppointmentType";
            this.chkAppointmentType.Size = new System.Drawing.Size(15, 14);
            this.chkAppointmentType.TabIndex = 213;
            this.chkAppointmentType.UseVisualStyleBackColor = true;
            this.chkAppointmentType.Visible = false;
            // 
            // btnClearAppointmentType
            // 
            this.btnClearAppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAppointmentType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearAppointmentType.BackgroundImage")));
            this.btnClearAppointmentType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearAppointmentType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAppointmentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAppointmentType.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAppointmentType.Image")));
            this.btnClearAppointmentType.Location = new System.Drawing.Point(583, 54);
            this.btnClearAppointmentType.Name = "btnClearAppointmentType";
            this.btnClearAppointmentType.Size = new System.Drawing.Size(23, 23);
            this.btnClearAppointmentType.TabIndex = 212;
            this.btnClearAppointmentType.UseVisualStyleBackColor = false;
            this.btnClearAppointmentType.Click += new System.EventHandler(this.btnClearAppointmentType_Click);
            // 
            // btnBrowseAppointmentType
            // 
            this.btnBrowseAppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseAppointmentType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppointmentType.BackgroundImage")));
            this.btnBrowseAppointmentType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseAppointmentType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseAppointmentType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAppointmentType.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppointmentType.Image")));
            this.btnBrowseAppointmentType.Location = new System.Drawing.Point(554, 54);
            this.btnBrowseAppointmentType.Name = "btnBrowseAppointmentType";
            this.btnBrowseAppointmentType.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseAppointmentType.TabIndex = 211;
            this.btnBrowseAppointmentType.UseVisualStyleBackColor = false;
            this.btnBrowseAppointmentType.Click += new System.EventHandler(this.btnBrowseAppointmentType_Click);
            // 
            // cmbApp_AppointmentType
            // 
            this.cmbApp_AppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_AppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_AppointmentType.FormattingEnabled = true;
            this.cmbApp_AppointmentType.Location = new System.Drawing.Point(398, 56);
            this.cmbApp_AppointmentType.Name = "cmbApp_AppointmentType";
            this.cmbApp_AppointmentType.Size = new System.Drawing.Size(149, 22);
            this.cmbApp_AppointmentType.TabIndex = 210;
            // 
            // lblApp_AppointmentType
            // 
            this.lblApp_AppointmentType.AutoSize = true;
            this.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_AppointmentType.Location = new System.Drawing.Point(276, 58);
            this.lblApp_AppointmentType.Name = "lblApp_AppointmentType";
            this.lblApp_AppointmentType.Size = new System.Drawing.Size(123, 14);
            this.lblApp_AppointmentType.TabIndex = 209;
            this.lblApp_AppointmentType.Text = " Appointment Type :";
            this.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // chkProvider
            // 
            this.chkProvider.AutoSize = true;
            this.chkProvider.Location = new System.Drawing.Point(616, 17);
            this.chkProvider.Name = "chkProvider";
            this.chkProvider.Size = new System.Drawing.Size(15, 14);
            this.chkProvider.TabIndex = 208;
            this.chkProvider.UseVisualStyleBackColor = true;
            this.chkProvider.Visible = false;
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(583, 12);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(23, 23);
            this.btnClearProvider.TabIndex = 207;
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
            this.btnBrowseProvider.Location = new System.Drawing.Point(554, 12);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseProvider.TabIndex = 206;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 205;
            this.label5.Text = "Provider :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(398, 13);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(149, 22);
            this.cmbProvider.TabIndex = 204;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 132);
            this.lbl_LeftBrd.TabIndex = 203;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(984, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 132);
            this.lbl_RightBrd.TabIndex = 202;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(982, 1);
            this.lbl_TopBrd.TabIndex = 201;
            this.lbl_TopBrd.Text = "label1";
            // 
            // grpSortCriteria
            // 
            this.grpSortCriteria.Controls.Add(this.rbSortNewPatient);
            this.grpSortCriteria.Controls.Add(this.rbSortEstablishedPatient);
            this.grpSortCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpSortCriteria.Location = new System.Drawing.Point(331, 87);
            this.grpSortCriteria.Name = "grpSortCriteria";
            this.grpSortCriteria.Size = new System.Drawing.Size(275, 40);
            this.grpSortCriteria.TabIndex = 200;
            this.grpSortCriteria.TabStop = false;
            // 
            // rbSortNewPatient
            // 
            this.rbSortNewPatient.AutoSize = true;
            this.rbSortNewPatient.Location = new System.Drawing.Point(167, 17);
            this.rbSortNewPatient.Name = "rbSortNewPatient";
            this.rbSortNewPatient.Size = new System.Drawing.Size(93, 18);
            this.rbSortNewPatient.TabIndex = 2;
            this.rbSortNewPatient.Text = "New Patient";
            this.rbSortNewPatient.UseVisualStyleBackColor = true;
            // 
            // rbSortEstablishedPatient
            // 
            this.rbSortEstablishedPatient.AutoSize = true;
            this.rbSortEstablishedPatient.Checked = true;
            this.rbSortEstablishedPatient.Location = new System.Drawing.Point(7, 17);
            this.rbSortEstablishedPatient.Name = "rbSortEstablishedPatient";
            this.rbSortEstablishedPatient.Size = new System.Drawing.Size(128, 18);
            this.rbSortEstablishedPatient.TabIndex = 0;
            this.rbSortEstablishedPatient.TabStop = true;
            this.rbSortEstablishedPatient.Text = "Established Patient";
            this.rbSortEstablishedPatient.UseVisualStyleBackColor = true;
            // 
            // grpDates
            // 
            this.grpDates.Controls.Add(this.dtpEndDate);
            this.grpDates.Controls.Add(this.lblEndDate);
            this.grpDates.Controls.Add(this.dtpStartDate);
            this.grpDates.Controls.Add(this.lblStartDate);
            this.grpDates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpDates.Location = new System.Drawing.Point(52, 45);
            this.grpDates.Name = "grpDates";
            this.grpDates.Size = new System.Drawing.Size(211, 76);
            this.grpDates.TabIndex = 95;
            this.grpDates.TabStop = false;
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
            this.dtpEndDate.Location = new System.Drawing.Point(84, 43);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(104, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(13, 47);
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
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(84, 15);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(104, 22);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(7, 19);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // pnlBalanceInfo
            // 
            this.pnlBalanceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBalanceInfo.Controls.Add(this.c1PatientList);
            this.pnlBalanceInfo.Controls.Add(this.panel8);
            this.pnlBalanceInfo.Controls.Add(this.label25);
            this.pnlBalanceInfo.Controls.Add(this.label26);
            this.pnlBalanceInfo.Controls.Add(this.label27);
            this.pnlBalanceInfo.Controls.Add(this.label28);
            this.pnlBalanceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBalanceInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBalanceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBalanceInfo.Location = new System.Drawing.Point(0, 194);
            this.pnlBalanceInfo.Name = "pnlBalanceInfo";
            this.pnlBalanceInfo.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.pnlBalanceInfo.Size = new System.Drawing.Size(988, 350);
            this.pnlBalanceInfo.TabIndex = 91;
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PatientList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PatientList.AutoResize = false;
            this.c1PatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PatientList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientList.Location = new System.Drawing.Point(4, 25);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Rows.Count = 1;
            this.c1PatientList.Rows.DefaultSize = 19;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.Size = new System.Drawing.Size(980, 323);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 14;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel8.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(4, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(980, 23);
            this.panel8.TabIndex = 13;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(0, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(980, 1);
            this.label19.TabIndex = 16;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(980, 23);
            this.label24.TabIndex = 0;
            this.label24.Text = " Patients ";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(4, 348);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(980, 1);
            this.label25.TabIndex = 4;
            this.label25.Text = "label2";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 2);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 347);
            this.label26.TabIndex = 3;
            this.label26.Text = "label4";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.Location = new System.Drawing.Point(984, 2);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 347);
            this.label27.TabIndex = 2;
            this.label27.Text = "label27";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(3, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(982, 1);
            this.label28.TabIndex = 0;
            this.label28.Text = "label1";
            // 
            // frmRpt_NewPatientvsEstablishedPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(988, 544);
            this.Controls.Add(this.pnlBalanceInfo);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_NewPatientvsEstablishedPatient";
            this.ShowInTaskbar = false;
            this.Text = "New  Patient vs.  Established Patient";
            this.Load += new System.EventHandler(this.frmRPT_New_PatientvsEstablishedPatient_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls_NewvsEstablished.ResumeLayout(false);
            this.tls_NewvsEstablished.PerformLayout();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.grpSortCriteria.ResumeLayout(false);
            this.grpSortCriteria.PerformLayout();
            this.grpDates.ResumeLayout(false);
            this.grpDates.PerformLayout();
            this.pnlBalanceInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls_NewvsEstablished;
        private System.Windows.Forms.ToolStripButton ts_btnRefresh;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.GroupBox grpSortCriteria;
        private System.Windows.Forms.RadioButton rbSortNewPatient;
        private System.Windows.Forms.RadioButton rbSortEstablishedPatient;
        private System.Windows.Forms.GroupBox grpDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Panel pnlBalanceInfo;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox chkProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        internal System.Windows.Forms.Label lblApp_AppointmentType;
        private System.Windows.Forms.CheckBox chkAppointmentType;
        internal System.Windows.Forms.Button btnClearAppointmentType;
        internal System.Windows.Forms.Button btnBrowseAppointmentType;
        internal System.Windows.Forms.ComboBox cmbApp_AppointmentType;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
    }
}