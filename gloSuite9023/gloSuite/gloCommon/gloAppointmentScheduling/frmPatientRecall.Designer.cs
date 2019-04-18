namespace gloAppointmentScheduling
{
    partial class frmPatientRecall
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpApp_DateTime_StartDate, dtpApp_DateTime_StartTime, dtpApp_DateTime_EndDate, dtpApp_DateTime_EndTime, dtpReSchEndDate, dtpReSchStartDate, dtpRescheduleDate };
            System.Windows.Forms.Control[] cntControls = { dtpApp_DateTime_StartDate, dtpApp_DateTime_StartTime, dtpApp_DateTime_EndDate, dtpApp_DateTime_EndTime, dtpReSchEndDate, dtpReSchStartDate, dtpRescheduleDate };

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
                try
                {
                    if (colorDialog1 != null)
                    {

                        colorDialog1.Dispose();
                        colorDialog1 = null;
                    }
                }
                catch
                {
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientRecall));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnSearch = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlRecallOptions = new System.Windows.Forms.Panel();
            this.grpSortCriteria = new System.Windows.Forms.GroupBox();
            this.rbRescheduleRecall = new System.Windows.Forms.RadioButton();
            this.rbIncopleteOrder = new System.Windows.Forms.RadioButton();
            this.rbTestResult = new System.Windows.Forms.RadioButton();
            this.rbExpiredAuthorization = new System.Windows.Forms.RadioButton();
            this.rbExhaustedPrescription = new System.Windows.Forms.RadioButton();
            this.rbRoutinePatient = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlc1Grid = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.C1Patients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlRecallAppointment = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFinishDate = new System.Windows.Forms.Label();
            this.lblFinishTime = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpApp_DateTime_EndTime = new System.Windows.Forms.DateTimePicker();
            this.numApp_DateTime_Duration = new System.Windows.Forms.NumericUpDown();
            this.dtpApp_DateTime_StartDate = new System.Windows.Forms.DateTimePicker();
            this.lblApp_DateTime_Time = new System.Windows.Forms.Label();
            this.lblApp_DateTime_Duration = new System.Windows.Forms.Label();
            this.lblDurationUnit = new System.Windows.Forms.Label();
            this.dtpApp_DateTime_StartTime = new System.Windows.Forms.DateTimePicker();
            this.lblApp_DateTime_Date = new System.Windows.Forms.Label();
            this.lblApp_DateTime_ColorContainer = new System.Windows.Forms.Label();
            this.btnApp_ClearDateTime_Color = new System.Windows.Forms.Button();
            this.lblApp_DateTime_Color = new System.Windows.Forms.Label();
            this.btnApp_DateTime_Color = new System.Windows.Forms.Button();
            this.lblApp_Notes = new System.Windows.Forms.Label();
            this.txtApp_Notes = new System.Windows.Forms.TextBox();
            this.cmbApp_AppointmentType = new System.Windows.Forms.ComboBox();
            this.lblApp_AppointmentType = new System.Windows.Forms.Label();
            this.cmbApp_Department = new System.Windows.Forms.ComboBox();
            this.lblApp_Department = new System.Windows.Forms.Label();
            this.cmbApp_Location = new System.Windows.Forms.ComboBox();
            this.lblApp_Location = new System.Windows.Forms.Label();
            this.btnApp_ClearProvider = new System.Windows.Forms.Button();
            this.btnApp_Provider = new System.Windows.Forms.Button();
            this.lblApp_Provider = new System.Windows.Forms.Label();
            this.cmbApp_Provider = new System.Windows.Forms.ComboBox();
            this.pnlRescheduleCriteria = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpRescheduleDate = new System.Windows.Forms.DateTimePicker();
            this.dtpReSchEndDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbReSchDepartment = new System.Windows.Forms.ComboBox();
            this.cmbReSchProviders = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbReSchLocation = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnBrowseReSchProvider = new System.Windows.Forms.Button();
            this.btnClearReSchProvider = new System.Windows.Forms.Button();
            this.dtpReSchStartDate = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbReSchAppointmentType = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlRecallOptions.SuspendLayout();
            this.grpSortCriteria.SuspendLayout();
            this.pnlc1Grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).BeginInit();
            this.pnlRecallAppointment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).BeginInit();
            this.pnlRescheduleCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(723, 54);
            this.pnlToolStrip.TabIndex = 4;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnSearch,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnOK,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(723, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnSearch
            // 
            this.tls_btnSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSearch.Image")));
            this.tls_btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSearch.Name = "tls_btnSearch";
            this.tls_btnSearch.Size = new System.Drawing.Size(101, 50);
            this.tls_btnSearch.Tag = "Search";
            this.tls_btnSearch.Text = "Show &Patients";
            this.tls_btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSearch.Click += new System.EventHandler(this.tls_btnSearch_Click);
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
            this.tls_btnExportToExcel.Text = "&Export To Excel";
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
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlRecallOptions
            // 
            this.pnlRecallOptions.Controls.Add(this.grpSortCriteria);
            this.pnlRecallOptions.Controls.Add(this.label5);
            this.pnlRecallOptions.Controls.Add(this.label6);
            this.pnlRecallOptions.Controls.Add(this.label7);
            this.pnlRecallOptions.Controls.Add(this.label8);
            this.pnlRecallOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRecallOptions.Location = new System.Drawing.Point(0, 54);
            this.pnlRecallOptions.Name = "pnlRecallOptions";
            this.pnlRecallOptions.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRecallOptions.Size = new System.Drawing.Size(723, 78);
            this.pnlRecallOptions.TabIndex = 0;
            // 
            // grpSortCriteria
            // 
            this.grpSortCriteria.BackColor = System.Drawing.Color.Transparent;
            this.grpSortCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.grpSortCriteria.Controls.Add(this.rbRescheduleRecall);
            this.grpSortCriteria.Controls.Add(this.rbIncopleteOrder);
            this.grpSortCriteria.Controls.Add(this.rbTestResult);
            this.grpSortCriteria.Controls.Add(this.rbExpiredAuthorization);
            this.grpSortCriteria.Controls.Add(this.rbExhaustedPrescription);
            this.grpSortCriteria.Controls.Add(this.rbRoutinePatient);
            this.grpSortCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpSortCriteria.Location = new System.Drawing.Point(10, 6);
            this.grpSortCriteria.Name = "grpSortCriteria";
            this.grpSortCriteria.Padding = new System.Windows.Forms.Padding(0);
            this.grpSortCriteria.Size = new System.Drawing.Size(701, 59);
            this.grpSortCriteria.TabIndex = 200;
            this.grpSortCriteria.TabStop = false;
            // 
            // rbRescheduleRecall
            // 
            this.rbRescheduleRecall.AutoSize = true;
            this.rbRescheduleRecall.Location = new System.Drawing.Point(255, 34);
            this.rbRescheduleRecall.Name = "rbRescheduleRecall";
            this.rbRescheduleRecall.Size = new System.Drawing.Size(121, 18);
            this.rbRescheduleRecall.TabIndex = 4;
            this.rbRescheduleRecall.Text = "Reschedule Recall";
            this.rbRescheduleRecall.UseVisualStyleBackColor = true;
            this.rbRescheduleRecall.CheckedChanged += new System.EventHandler(this.rbRescheduleRecall_CheckedChanged);
            // 
            // rbIncopleteOrder
            // 
            this.rbIncopleteOrder.AutoSize = true;
            this.rbIncopleteOrder.Location = new System.Drawing.Point(15, 12);
            this.rbIncopleteOrder.Name = "rbIncopleteOrder";
            this.rbIncopleteOrder.Size = new System.Drawing.Size(122, 18);
            this.rbIncopleteOrder.TabIndex = 0;
            this.rbIncopleteOrder.Text = "Incomplete Order";
            this.rbIncopleteOrder.UseVisualStyleBackColor = true;
            this.rbIncopleteOrder.CheckedChanged += new System.EventHandler(this.rbIncopleteOrder_CheckedChanged);
            // 
            // rbTestResult
            // 
            this.rbTestResult.AutoSize = true;
            this.rbTestResult.Location = new System.Drawing.Point(255, 12);
            this.rbTestResult.Name = "rbTestResult";
            this.rbTestResult.Size = new System.Drawing.Size(87, 18);
            this.rbTestResult.TabIndex = 1;
            this.rbTestResult.Text = "Test Result";
            this.rbTestResult.UseVisualStyleBackColor = true;
            this.rbTestResult.CheckedChanged += new System.EventHandler(this.rbTestResult_CheckedChanged);
            // 
            // rbExpiredAuthorization
            // 
            this.rbExpiredAuthorization.AutoSize = true;
            this.rbExpiredAuthorization.Checked = true;
            this.rbExpiredAuthorization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExpiredAuthorization.Location = new System.Drawing.Point(481, 12);
            this.rbExpiredAuthorization.Name = "rbExpiredAuthorization";
            this.rbExpiredAuthorization.Size = new System.Drawing.Size(159, 18);
            this.rbExpiredAuthorization.TabIndex = 2;
            this.rbExpiredAuthorization.TabStop = true;
            this.rbExpiredAuthorization.Text = "Expired Authorization";
            this.rbExpiredAuthorization.UseVisualStyleBackColor = true;
            this.rbExpiredAuthorization.CheckedChanged += new System.EventHandler(this.rbExpiredAuthorization_CheckedChanged);
            // 
            // rbExhaustedPrescription
            // 
            this.rbExhaustedPrescription.AutoSize = true;
            this.rbExhaustedPrescription.Location = new System.Drawing.Point(15, 34);
            this.rbExhaustedPrescription.Name = "rbExhaustedPrescription";
            this.rbExhaustedPrescription.Size = new System.Drawing.Size(149, 18);
            this.rbExhaustedPrescription.TabIndex = 3;
            this.rbExhaustedPrescription.Text = "Exhausted Prescription";
            this.rbExhaustedPrescription.UseVisualStyleBackColor = true;
            this.rbExhaustedPrescription.CheckedChanged += new System.EventHandler(this.rbExhaustedPrescription_CheckedChanged);
            // 
            // rbRoutinePatient
            // 
            this.rbRoutinePatient.AutoSize = true;
            this.rbRoutinePatient.Location = new System.Drawing.Point(481, 34);
            this.rbRoutinePatient.Name = "rbRoutinePatient";
            this.rbRoutinePatient.Size = new System.Drawing.Size(110, 18);
            this.rbRoutinePatient.TabIndex = 5;
            this.rbRoutinePatient.Text = "Routine Patient";
            this.rbRoutinePatient.UseVisualStyleBackColor = true;
            this.rbRoutinePatient.CheckedChanged += new System.EventHandler(this.rbRoutinePatient_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(4, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(715, 1);
            this.label5.TabIndex = 204;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 71);
            this.label6.TabIndex = 203;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(719, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 71);
            this.label7.TabIndex = 202;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(717, 1);
            this.label8.TabIndex = 201;
            this.label8.Text = "label1";
            // 
            // pnlc1Grid
            // 
            this.pnlc1Grid.Controls.Add(this.lbl_BottomBrd);
            this.pnlc1Grid.Controls.Add(this.lbl_LeftBrd);
            this.pnlc1Grid.Controls.Add(this.lbl_RightBrd);
            this.pnlc1Grid.Controls.Add(this.lbl_TopBrd);
            this.pnlc1Grid.Controls.Add(this.C1Patients);
            this.pnlc1Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1Grid.Location = new System.Drawing.Point(0, 132);
            this.pnlc1Grid.Name = "pnlc1Grid";
            this.pnlc1Grid.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.pnlc1Grid.Size = new System.Drawing.Size(723, 217);
            this.pnlc1Grid.TabIndex = 1;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 215);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(715, 1);
            this.lbl_BottomBrd.TabIndex = 91;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 214);
            this.lbl_LeftBrd.TabIndex = 90;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(719, 2);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 214);
            this.lbl_RightBrd.TabIndex = 89;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(717, 1);
            this.lbl_TopBrd.TabIndex = 88;
            this.lbl_TopBrd.Text = "label1";
            // 
            // C1Patients
            // 
            this.C1Patients.AllowEditing = false;
            this.C1Patients.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1Patients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Patients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1Patients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Patients.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.C1Patients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Patients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Patients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Patients.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1Patients.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1Patients.Location = new System.Drawing.Point(3, 1);
            this.C1Patients.Name = "C1Patients";
            this.C1Patients.Rows.Count = 1;
            this.C1Patients.Rows.DefaultSize = 19;
            this.C1Patients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Patients.Size = new System.Drawing.Size(717, 215);
            this.C1Patients.StyleInfo = resources.GetString("C1Patients.StyleInfo");
            this.C1Patients.TabIndex = 0;
            this.C1Patients.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1Patients_MouseMove);
            // 
            // pnlRecallAppointment
            // 
            this.pnlRecallAppointment.Controls.Add(this.label1);
            this.pnlRecallAppointment.Controls.Add(this.label2);
            this.pnlRecallAppointment.Controls.Add(this.label3);
            this.pnlRecallAppointment.Controls.Add(this.label4);
            this.pnlRecallAppointment.Controls.Add(this.lblFinishDate);
            this.pnlRecallAppointment.Controls.Add(this.lblFinishTime);
            this.pnlRecallAppointment.Controls.Add(this.dtpApp_DateTime_EndDate);
            this.pnlRecallAppointment.Controls.Add(this.dtpApp_DateTime_EndTime);
            this.pnlRecallAppointment.Controls.Add(this.numApp_DateTime_Duration);
            this.pnlRecallAppointment.Controls.Add(this.dtpApp_DateTime_StartDate);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_DateTime_Time);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_DateTime_Duration);
            this.pnlRecallAppointment.Controls.Add(this.lblDurationUnit);
            this.pnlRecallAppointment.Controls.Add(this.dtpApp_DateTime_StartTime);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_DateTime_Date);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_DateTime_ColorContainer);
            this.pnlRecallAppointment.Controls.Add(this.btnApp_ClearDateTime_Color);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_DateTime_Color);
            this.pnlRecallAppointment.Controls.Add(this.btnApp_DateTime_Color);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_Notes);
            this.pnlRecallAppointment.Controls.Add(this.txtApp_Notes);
            this.pnlRecallAppointment.Controls.Add(this.cmbApp_AppointmentType);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_AppointmentType);
            this.pnlRecallAppointment.Controls.Add(this.cmbApp_Department);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_Department);
            this.pnlRecallAppointment.Controls.Add(this.cmbApp_Location);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_Location);
            this.pnlRecallAppointment.Controls.Add(this.btnApp_ClearProvider);
            this.pnlRecallAppointment.Controls.Add(this.btnApp_Provider);
            this.pnlRecallAppointment.Controls.Add(this.lblApp_Provider);
            this.pnlRecallAppointment.Controls.Add(this.cmbApp_Provider);
            this.pnlRecallAppointment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRecallAppointment.Location = new System.Drawing.Point(0, 349);
            this.pnlRecallAppointment.Name = "pnlRecallAppointment";
            this.pnlRecallAppointment.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRecallAppointment.Size = new System.Drawing.Size(723, 161);
            this.pnlRecallAppointment.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(715, 1);
            this.label1.TabIndex = 203;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 154);
            this.label2.TabIndex = 202;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(719, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 154);
            this.label3.TabIndex = 201;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(717, 1);
            this.label4.TabIndex = 200;
            this.label4.Text = "label1";
            // 
            // lblFinishDate
            // 
            this.lblFinishDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinishDate.AutoEllipsis = true;
            this.lblFinishDate.AutoSize = true;
            this.lblFinishDate.BackColor = System.Drawing.Color.Transparent;
            this.lblFinishDate.Location = new System.Drawing.Point(519, 128);
            this.lblFinishDate.Name = "lblFinishDate";
            this.lblFinishDate.Size = new System.Drawing.Size(56, 14);
            this.lblFinishDate.TabIndex = 198;
            this.lblFinishDate.Text = "E. Date :";
            this.lblFinishDate.Visible = false;
            // 
            // lblFinishTime
            // 
            this.lblFinishTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinishTime.AutoEllipsis = true;
            this.lblFinishTime.AutoSize = true;
            this.lblFinishTime.BackColor = System.Drawing.Color.Transparent;
            this.lblFinishTime.Location = new System.Drawing.Point(619, 128);
            this.lblFinishTime.Name = "lblFinishTime";
            this.lblFinishTime.Size = new System.Drawing.Size(57, 14);
            this.lblFinishTime.TabIndex = 199;
            this.lblFinishTime.Text = "E. Time :";
            this.lblFinishTime.Visible = false;
            // 
            // dtpApp_DateTime_EndDate
            // 
            this.dtpApp_DateTime_EndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_EndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_EndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_EndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_EndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_EndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApp_DateTime_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_EndDate.Location = new System.Drawing.Point(547, 124);
            this.dtpApp_DateTime_EndDate.Name = "dtpApp_DateTime_EndDate";
            this.dtpApp_DateTime_EndDate.Size = new System.Drawing.Size(66, 22);
            this.dtpApp_DateTime_EndDate.TabIndex = 196;
            this.dtpApp_DateTime_EndDate.Visible = false;
            // 
            // dtpApp_DateTime_EndTime
            // 
            this.dtpApp_DateTime_EndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_EndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_EndTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_EndTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_EndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpApp_DateTime_EndTime.Location = new System.Drawing.Point(650, 124);
            this.dtpApp_DateTime_EndTime.Name = "dtpApp_DateTime_EndTime";
            this.dtpApp_DateTime_EndTime.Size = new System.Drawing.Size(60, 22);
            this.dtpApp_DateTime_EndTime.TabIndex = 197;
            this.dtpApp_DateTime_EndTime.Visible = false;
            // 
            // numApp_DateTime_Duration
            // 
            this.numApp_DateTime_Duration.ForeColor = System.Drawing.Color.Black;
            this.numApp_DateTime_Duration.Location = new System.Drawing.Point(372, 121);
            this.numApp_DateTime_Duration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.Name = "numApp_DateTime_Duration";
            this.numApp_DateTime_Duration.Size = new System.Drawing.Size(53, 22);
            this.numApp_DateTime_Duration.TabIndex = 11;
            this.numApp_DateTime_Duration.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numApp_DateTime_Duration.ValueChanged += new System.EventHandler(this.numApp_DateTime_Duration_ValueChanged);
            // 
            // dtpApp_DateTime_StartDate
            // 
            this.dtpApp_DateTime_StartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_StartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_StartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_StartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_StartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_StartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpApp_DateTime_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_StartDate.Location = new System.Drawing.Point(75, 121);
            this.dtpApp_DateTime_StartDate.Name = "dtpApp_DateTime_StartDate";
            this.dtpApp_DateTime_StartDate.Size = new System.Drawing.Size(99, 22);
            this.dtpApp_DateTime_StartDate.TabIndex = 9;
            this.dtpApp_DateTime_StartDate.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_StartDate_ValueChanged);
            // 
            // lblApp_DateTime_Time
            // 
            this.lblApp_DateTime_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_DateTime_Time.AutoEllipsis = true;
            this.lblApp_DateTime_Time.AutoSize = true;
            this.lblApp_DateTime_Time.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Time.Location = new System.Drawing.Point(176, 125);
            this.lblApp_DateTime_Time.Name = "lblApp_DateTime_Time";
            this.lblApp_DateTime_Time.Size = new System.Drawing.Size(42, 14);
            this.lblApp_DateTime_Time.TabIndex = 193;
            this.lblApp_DateTime_Time.Text = "Time :";
            // 
            // lblApp_DateTime_Duration
            // 
            this.lblApp_DateTime_Duration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_DateTime_Duration.AutoEllipsis = true;
            this.lblApp_DateTime_Duration.AutoSize = true;
            this.lblApp_DateTime_Duration.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Duration.Location = new System.Drawing.Point(308, 125);
            this.lblApp_DateTime_Duration.Name = "lblApp_DateTime_Duration";
            this.lblApp_DateTime_Duration.Size = new System.Drawing.Size(61, 14);
            this.lblApp_DateTime_Duration.TabIndex = 190;
            this.lblApp_DateTime_Duration.Text = "Duration :";
            // 
            // lblDurationUnit
            // 
            this.lblDurationUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDurationUnit.AutoEllipsis = true;
            this.lblDurationUnit.AutoSize = true;
            this.lblDurationUnit.Location = new System.Drawing.Point(427, 125);
            this.lblDurationUnit.Name = "lblDurationUnit";
            this.lblDurationUnit.Size = new System.Drawing.Size(41, 14);
            this.lblDurationUnit.TabIndex = 195;
            this.lblDurationUnit.Text = "(mins)";
            // 
            // dtpApp_DateTime_StartTime
            // 
            this.dtpApp_DateTime_StartTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpApp_DateTime_StartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpApp_DateTime_StartTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpApp_DateTime_StartTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpApp_DateTime_StartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpApp_DateTime_StartTime.CustomFormat = "hh:mm tt";
            this.dtpApp_DateTime_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApp_DateTime_StartTime.Location = new System.Drawing.Point(221, 121);
            this.dtpApp_DateTime_StartTime.Name = "dtpApp_DateTime_StartTime";
            this.dtpApp_DateTime_StartTime.ShowUpDown = true;
            this.dtpApp_DateTime_StartTime.Size = new System.Drawing.Size(81, 22);
            this.dtpApp_DateTime_StartTime.TabIndex = 10;
            this.dtpApp_DateTime_StartTime.ValueChanged += new System.EventHandler(this.dtpApp_DateTime_StartTime_ValueChanged);
            // 
            // lblApp_DateTime_Date
            // 
            this.lblApp_DateTime_Date.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_DateTime_Date.AutoEllipsis = true;
            this.lblApp_DateTime_Date.AutoSize = true;
            this.lblApp_DateTime_Date.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Date.Location = new System.Drawing.Point(30, 125);
            this.lblApp_DateTime_Date.Name = "lblApp_DateTime_Date";
            this.lblApp_DateTime_Date.Size = new System.Drawing.Size(41, 14);
            this.lblApp_DateTime_Date.TabIndex = 189;
            this.lblApp_DateTime_Date.Text = "Date :";
            this.lblApp_DateTime_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApp_DateTime_ColorContainer
            // 
            this.lblApp_DateTime_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lblApp_DateTime_ColorContainer.Location = new System.Drawing.Point(434, 87);
            this.lblApp_DateTime_ColorContainer.Name = "lblApp_DateTime_ColorContainer";
            this.lblApp_DateTime_ColorContainer.Size = new System.Drawing.Size(53, 19);
            this.lblApp_DateTime_ColorContainer.TabIndex = 187;
            // 
            // btnApp_ClearDateTime_Color
            // 
            this.btnApp_ClearDateTime_Color.AutoEllipsis = true;
            this.btnApp_ClearDateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearDateTime_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearDateTime_Color.BackgroundImage")));
            this.btnApp_ClearDateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearDateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearDateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearDateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearDateTime_Color.Image")));
            this.btnApp_ClearDateTime_Color.Location = new System.Drawing.Point(529, 85);
            this.btnApp_ClearDateTime_Color.Name = "btnApp_ClearDateTime_Color";
            this.btnApp_ClearDateTime_Color.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearDateTime_Color.TabIndex = 8;
            this.btnApp_ClearDateTime_Color.UseVisualStyleBackColor = false;
            this.btnApp_ClearDateTime_Color.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnApp_ClearDateTime_Color.Click += new System.EventHandler(this.btnApp_ClearDateTime_Color_Click);
            this.btnApp_ClearDateTime_Color.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblApp_DateTime_Color
            // 
            this.lblApp_DateTime_Color.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_DateTime_Color.AutoEllipsis = true;
            this.lblApp_DateTime_Color.AutoSize = true;
            this.lblApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_DateTime_Color.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApp_DateTime_Color.Location = new System.Drawing.Point(389, 89);
            this.lblApp_DateTime_Color.Name = "lblApp_DateTime_Color";
            this.lblApp_DateTime_Color.Size = new System.Drawing.Size(42, 14);
            this.lblApp_DateTime_Color.TabIndex = 185;
            this.lblApp_DateTime_Color.Text = "Color :";
            this.lblApp_DateTime_Color.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApp_DateTime_Color
            // 
            this.btnApp_DateTime_Color.AutoEllipsis = true;
            this.btnApp_DateTime_Color.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_DateTime_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.BackgroundImage")));
            this.btnApp_DateTime_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_DateTime_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_DateTime_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_DateTime_Color.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_DateTime_Color.Image")));
            this.btnApp_DateTime_Color.Location = new System.Drawing.Point(500, 85);
            this.btnApp_DateTime_Color.Name = "btnApp_DateTime_Color";
            this.btnApp_DateTime_Color.Size = new System.Drawing.Size(22, 22);
            this.btnApp_DateTime_Color.TabIndex = 7;
            this.btnApp_DateTime_Color.UseVisualStyleBackColor = false;
            this.btnApp_DateTime_Color.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnApp_DateTime_Color.Click += new System.EventHandler(this.btnApp_DateTime_Color_Click);
            this.btnApp_DateTime_Color.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblApp_Notes
            // 
            this.lblApp_Notes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Notes.AutoEllipsis = true;
            this.lblApp_Notes.AutoSize = true;
            this.lblApp_Notes.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Notes.Location = new System.Drawing.Point(24, 89);
            this.lblApp_Notes.Name = "lblApp_Notes";
            this.lblApp_Notes.Size = new System.Drawing.Size(47, 14);
            this.lblApp_Notes.TabIndex = 184;
            this.lblApp_Notes.Text = "Notes :";
            this.lblApp_Notes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtApp_Notes
            // 
            this.txtApp_Notes.ForeColor = System.Drawing.Color.Black;
            this.txtApp_Notes.Location = new System.Drawing.Point(77, 85);
            this.txtApp_Notes.Name = "txtApp_Notes";
            this.txtApp_Notes.Size = new System.Drawing.Size(270, 22);
            this.txtApp_Notes.TabIndex = 6;
            // 
            // cmbApp_AppointmentType
            // 
            this.cmbApp_AppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_AppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_AppointmentType.FormattingEnabled = true;
            this.cmbApp_AppointmentType.Location = new System.Drawing.Point(77, 49);
            this.cmbApp_AppointmentType.Name = "cmbApp_AppointmentType";
            this.cmbApp_AppointmentType.Size = new System.Drawing.Size(270, 22);
            this.cmbApp_AppointmentType.TabIndex = 4;
            this.cmbApp_AppointmentType.SelectionChangeCommitted += new System.EventHandler(this.cmbApp_AppointmentType_SelectionChangeCommitted);
            // 
            // lblApp_AppointmentType
            // 
            this.lblApp_AppointmentType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_AppointmentType.AutoEllipsis = true;
            this.lblApp_AppointmentType.AutoSize = true;
            this.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_AppointmentType.Location = new System.Drawing.Point(28, 53);
            this.lblApp_AppointmentType.Name = "lblApp_AppointmentType";
            this.lblApp_AppointmentType.Size = new System.Drawing.Size(43, 14);
            this.lblApp_AppointmentType.TabIndex = 181;
            this.lblApp_AppointmentType.Text = "Type :";
            this.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Department
            // 
            this.cmbApp_Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Department.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Department.FormattingEnabled = true;
            this.cmbApp_Department.Location = new System.Drawing.Point(434, 49);
            this.cmbApp_Department.Name = "cmbApp_Department";
            this.cmbApp_Department.Size = new System.Drawing.Size(190, 22);
            this.cmbApp_Department.TabIndex = 5;
            // 
            // lblApp_Department
            // 
            this.lblApp_Department.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Department.AutoEllipsis = true;
            this.lblApp_Department.AutoSize = true;
            this.lblApp_Department.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Department.Location = new System.Drawing.Point(350, 53);
            this.lblApp_Department.Name = "lblApp_Department";
            this.lblApp_Department.Size = new System.Drawing.Size(81, 14);
            this.lblApp_Department.TabIndex = 179;
            this.lblApp_Department.Text = "Department :";
            this.lblApp_Department.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Location
            // 
            this.cmbApp_Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Location.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Location.FormattingEnabled = true;
            this.cmbApp_Location.Location = new System.Drawing.Point(434, 13);
            this.cmbApp_Location.Name = "cmbApp_Location";
            this.cmbApp_Location.Size = new System.Drawing.Size(190, 22);
            this.cmbApp_Location.TabIndex = 3;
            // 
            // lblApp_Location
            // 
            this.lblApp_Location.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Location.AutoEllipsis = true;
            this.lblApp_Location.AutoSize = true;
            this.lblApp_Location.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Location.Location = new System.Drawing.Point(370, 17);
            this.lblApp_Location.Name = "lblApp_Location";
            this.lblApp_Location.Size = new System.Drawing.Size(61, 14);
            this.lblApp_Location.TabIndex = 177;
            this.lblApp_Location.Text = "Location :";
            this.lblApp_Location.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnApp_ClearProvider
            // 
            this.btnApp_ClearProvider.AutoEllipsis = true;
            this.btnApp_ClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_ClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProvider.BackgroundImage")));
            this.btnApp_ClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_ClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_ClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_ClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_ClearProvider.Image")));
            this.btnApp_ClearProvider.Location = new System.Drawing.Point(320, 13);
            this.btnApp_ClearProvider.Name = "btnApp_ClearProvider";
            this.btnApp_ClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnApp_ClearProvider.TabIndex = 2;
            this.btnApp_ClearProvider.UseVisualStyleBackColor = false;
            this.btnApp_ClearProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnApp_ClearProvider.Click += new System.EventHandler(this.btn_ClearProvider_Click);
            this.btnApp_ClearProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnApp_Provider
            // 
            this.btnApp_Provider.AutoEllipsis = true;
            this.btnApp_Provider.BackColor = System.Drawing.Color.Transparent;
            this.btnApp_Provider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApp_Provider.BackgroundImage")));
            this.btnApp_Provider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApp_Provider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnApp_Provider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApp_Provider.Image = ((System.Drawing.Image)(resources.GetObject("btnApp_Provider.Image")));
            this.btnApp_Provider.Location = new System.Drawing.Point(294, 13);
            this.btnApp_Provider.Name = "btnApp_Provider";
            this.btnApp_Provider.Size = new System.Drawing.Size(22, 22);
            this.btnApp_Provider.TabIndex = 1;
            this.btnApp_Provider.UseVisualStyleBackColor = false;
            this.btnApp_Provider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnApp_Provider.Click += new System.EventHandler(this.btn_BrowseProvider_Click);
            this.btnApp_Provider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblApp_Provider
            // 
            this.lblApp_Provider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApp_Provider.AutoEllipsis = true;
            this.lblApp_Provider.AutoSize = true;
            this.lblApp_Provider.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Provider.Location = new System.Drawing.Point(12, 17);
            this.lblApp_Provider.Name = "lblApp_Provider";
            this.lblApp_Provider.Size = new System.Drawing.Size(59, 14);
            this.lblApp_Provider.TabIndex = 173;
            this.lblApp_Provider.Text = "Provider :";
            this.lblApp_Provider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbApp_Provider
            // 
            this.cmbApp_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApp_Provider.ForeColor = System.Drawing.Color.Black;
            this.cmbApp_Provider.FormattingEnabled = true;
            this.cmbApp_Provider.Location = new System.Drawing.Point(77, 13);
            this.cmbApp_Provider.Name = "cmbApp_Provider";
            this.cmbApp_Provider.Size = new System.Drawing.Size(213, 22);
            this.cmbApp_Provider.TabIndex = 0;
            // 
            // pnlRescheduleCriteria
            // 
            this.pnlRescheduleCriteria.Controls.Add(this.label19);
            this.pnlRescheduleCriteria.Controls.Add(this.label18);
            this.pnlRescheduleCriteria.Controls.Add(this.label17);
            this.pnlRescheduleCriteria.Controls.Add(this.label16);
            this.pnlRescheduleCriteria.Controls.Add(this.label15);
            this.pnlRescheduleCriteria.Controls.Add(this.dtpRescheduleDate);
            this.pnlRescheduleCriteria.Controls.Add(this.dtpReSchEndDate);
            this.pnlRescheduleCriteria.Controls.Add(this.label9);
            this.pnlRescheduleCriteria.Controls.Add(this.cmbReSchDepartment);
            this.pnlRescheduleCriteria.Controls.Add(this.cmbReSchProviders);
            this.pnlRescheduleCriteria.Controls.Add(this.label10);
            this.pnlRescheduleCriteria.Controls.Add(this.label11);
            this.pnlRescheduleCriteria.Controls.Add(this.cmbReSchLocation);
            this.pnlRescheduleCriteria.Controls.Add(this.label12);
            this.pnlRescheduleCriteria.Controls.Add(this.btnBrowseReSchProvider);
            this.pnlRescheduleCriteria.Controls.Add(this.btnClearReSchProvider);
            this.pnlRescheduleCriteria.Controls.Add(this.dtpReSchStartDate);
            this.pnlRescheduleCriteria.Controls.Add(this.label13);
            this.pnlRescheduleCriteria.Controls.Add(this.cmbReSchAppointmentType);
            this.pnlRescheduleCriteria.Controls.Add(this.label14);
            this.pnlRescheduleCriteria.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRescheduleCriteria.Location = new System.Drawing.Point(0, 510);
            this.pnlRescheduleCriteria.Name = "pnlRescheduleCriteria";
            this.pnlRescheduleCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRescheduleCriteria.Size = new System.Drawing.Size(723, 116);
            this.pnlRescheduleCriteria.TabIndex = 3;
            this.pnlRescheduleCriteria.Visible = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(719, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 108);
            this.label19.TabIndex = 208;
            this.label19.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 108);
            this.label18.TabIndex = 207;
            this.label18.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(717, 1);
            this.label17.TabIndex = 206;
            this.label17.Text = "label2";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(3, 112);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(717, 1);
            this.label16.TabIndex = 205;
            this.label16.Text = "label2";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(404, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 14);
            this.label15.TabIndex = 195;
            this.label15.Text = "Reschedule Date:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRescheduleDate
            // 
            this.dtpRescheduleDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRescheduleDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRescheduleDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRescheduleDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRescheduleDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRescheduleDate.CustomFormat = "MM/dd/yyyy";
            this.dtpRescheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRescheduleDate.Location = new System.Drawing.Point(510, 77);
            this.dtpRescheduleDate.Name = "dtpRescheduleDate";
            this.dtpRescheduleDate.Size = new System.Drawing.Size(112, 22);
            this.dtpRescheduleDate.TabIndex = 8;
            // 
            // dtpReSchEndDate
            // 
            this.dtpReSchEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReSchEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReSchEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReSchEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReSchEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReSchEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpReSchEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReSchEndDate.Location = new System.Drawing.Point(274, 77);
            this.dtpReSchEndDate.Name = "dtpReSchEndDate";
            this.dtpReSchEndDate.Size = new System.Drawing.Size(112, 22);
            this.dtpReSchEndDate.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(215, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 14);
            this.label9.TabIndex = 192;
            this.label9.Text = "To Date :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReSchDepartment
            // 
            this.cmbReSchDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReSchDepartment.ForeColor = System.Drawing.Color.Black;
            this.cmbReSchDepartment.FormattingEnabled = true;
            this.cmbReSchDepartment.Location = new System.Drawing.Point(448, 47);
            this.cmbReSchDepartment.Name = "cmbReSchDepartment";
            this.cmbReSchDepartment.Size = new System.Drawing.Size(185, 22);
            this.cmbReSchDepartment.TabIndex = 5;
            // 
            // cmbReSchProviders
            // 
            this.cmbReSchProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReSchProviders.ForeColor = System.Drawing.Color.Black;
            this.cmbReSchProviders.FormattingEnabled = true;
            this.cmbReSchProviders.Location = new System.Drawing.Point(96, 16);
            this.cmbReSchProviders.Name = "cmbReSchProviders";
            this.cmbReSchProviders.Size = new System.Drawing.Size(206, 22);
            this.cmbReSchProviders.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(364, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 14);
            this.label10.TabIndex = 179;
            this.label10.Text = "Department :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(34, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 14);
            this.label11.TabIndex = 173;
            this.label11.Text = "Provider :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReSchLocation
            // 
            this.cmbReSchLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReSchLocation.ForeColor = System.Drawing.Color.Black;
            this.cmbReSchLocation.FormattingEnabled = true;
            this.cmbReSchLocation.Location = new System.Drawing.Point(448, 16);
            this.cmbReSchLocation.Name = "cmbReSchLocation";
            this.cmbReSchLocation.Size = new System.Drawing.Size(185, 22);
            this.cmbReSchLocation.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(384, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 14);
            this.label12.TabIndex = 177;
            this.label12.Text = "Location :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnBrowseReSchProvider
            // 
            this.btnBrowseReSchProvider.AutoEllipsis = true;
            this.btnBrowseReSchProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseReSchProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseReSchProvider.BackgroundImage")));
            this.btnBrowseReSchProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseReSchProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnBrowseReSchProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseReSchProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseReSchProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseReSchProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseReSchProvider.Image")));
            this.btnBrowseReSchProvider.Location = new System.Drawing.Point(308, 16);
            this.btnBrowseReSchProvider.Name = "btnBrowseReSchProvider";
            this.btnBrowseReSchProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseReSchProvider.TabIndex = 1;
            this.btnBrowseReSchProvider.UseVisualStyleBackColor = false;
            this.btnBrowseReSchProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseReSchProvider.Click += new System.EventHandler(this.btnBrowseReSchProvider_Click);
            this.btnBrowseReSchProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearReSchProvider
            // 
            this.btnClearReSchProvider.AutoEllipsis = true;
            this.btnClearReSchProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearReSchProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearReSchProvider.BackgroundImage")));
            this.btnClearReSchProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearReSchProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnClearReSchProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnClearReSchProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnClearReSchProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReSchProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearReSchProvider.Image")));
            this.btnClearReSchProvider.Location = new System.Drawing.Point(335, 16);
            this.btnClearReSchProvider.Name = "btnClearReSchProvider";
            this.btnClearReSchProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearReSchProvider.TabIndex = 2;
            this.btnClearReSchProvider.UseVisualStyleBackColor = false;
            this.btnClearReSchProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearReSchProvider.Click += new System.EventHandler(this.btnClearReSchProvider_Click);
            this.btnClearReSchProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // dtpReSchStartDate
            // 
            this.dtpReSchStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReSchStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReSchStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReSchStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReSchStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReSchStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpReSchStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReSchStartDate.Location = new System.Drawing.Point(96, 77);
            this.dtpReSchStartDate.Name = "dtpReSchStartDate";
            this.dtpReSchStartDate.Size = new System.Drawing.Size(112, 22);
            this.dtpReSchStartDate.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(21, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 14);
            this.label13.TabIndex = 189;
            this.label13.Text = "From Date :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbReSchAppointmentType
            // 
            this.cmbReSchAppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReSchAppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbReSchAppointmentType.FormattingEnabled = true;
            this.cmbReSchAppointmentType.Location = new System.Drawing.Point(96, 47);
            this.cmbReSchAppointmentType.Name = "cmbReSchAppointmentType";
            this.cmbReSchAppointmentType.Size = new System.Drawing.Size(206, 22);
            this.cmbReSchAppointmentType.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(50, 51);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 14);
            this.label14.TabIndex = 181;
            this.label14.Text = "Type :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPatientRecall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(723, 626);
            this.Controls.Add(this.pnlc1Grid);
            this.Controls.Add(this.pnlRecallAppointment);
            this.Controls.Add(this.pnlRescheduleCriteria);
            this.Controls.Add(this.pnlRecallOptions);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientRecall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Recall";
            this.Load += new System.EventHandler(this.frmPatientRecall_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlRecallOptions.ResumeLayout(false);
            this.grpSortCriteria.ResumeLayout(false);
            this.grpSortCriteria.PerformLayout();
            this.pnlc1Grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).EndInit();
            this.pnlRecallAppointment.ResumeLayout(false);
            this.pnlRecallAppointment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApp_DateTime_Duration)).EndInit();
            this.pnlRescheduleCriteria.ResumeLayout(false);
            this.pnlRescheduleCriteria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlRecallOptions;
        private System.Windows.Forms.GroupBox grpSortCriteria;
        private System.Windows.Forms.RadioButton rbIncopleteOrder;
        private System.Windows.Forms.RadioButton rbTestResult;
        private System.Windows.Forms.RadioButton rbExpiredAuthorization;
        private System.Windows.Forms.RadioButton rbExhaustedPrescription;
        private System.Windows.Forms.RadioButton rbRoutinePatient;
        private System.Windows.Forms.Panel pnlc1Grid;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Patients;
        private System.Windows.Forms.RadioButton rbRescheduleRecall;
        private System.Windows.Forms.ToolStripButton tls_btnSearch;
        private System.Windows.Forms.Panel pnlRecallAppointment;
        internal System.Windows.Forms.Button btnApp_ClearProvider;
        internal System.Windows.Forms.Button btnApp_Provider;
        internal System.Windows.Forms.Label lblApp_Provider;
        internal System.Windows.Forms.ComboBox cmbApp_Provider;
        internal System.Windows.Forms.ComboBox cmbApp_AppointmentType;
        internal System.Windows.Forms.Label lblApp_AppointmentType;
        private System.Windows.Forms.ComboBox cmbApp_Department;
        internal System.Windows.Forms.Label lblApp_Department;
        private System.Windows.Forms.ComboBox cmbApp_Location;
        internal System.Windows.Forms.Label lblApp_Location;
        internal System.Windows.Forms.Label lblApp_DateTime_ColorContainer;
        internal System.Windows.Forms.Button btnApp_ClearDateTime_Color;
        private System.Windows.Forms.Label lblApp_DateTime_Color;
        internal System.Windows.Forms.Button btnApp_DateTime_Color;
        internal System.Windows.Forms.Label lblApp_Notes;
        internal System.Windows.Forms.TextBox txtApp_Notes;
        internal System.Windows.Forms.NumericUpDown numApp_DateTime_Duration;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartDate;
        internal System.Windows.Forms.Label lblApp_DateTime_Time;
        internal System.Windows.Forms.Label lblApp_DateTime_Duration;
        private System.Windows.Forms.Label lblDurationUnit;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_StartTime;
        internal System.Windows.Forms.Label lblApp_DateTime_Date;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_EndDate;
        internal System.Windows.Forms.DateTimePicker dtpApp_DateTime_EndTime;
        internal System.Windows.Forms.Label lblFinishDate;
        internal System.Windows.Forms.Label lblFinishTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlRescheduleCriteria;
        internal System.Windows.Forms.DateTimePicker dtpReSchEndDate;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbReSchDepartment;
        internal System.Windows.Forms.ComboBox cmbReSchProviders;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbReSchLocation;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Button btnBrowseReSchProvider;
        internal System.Windows.Forms.Button btnClearReSchProvider;
        internal System.Windows.Forms.DateTimePicker dtpReSchStartDate;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.ComboBox cmbReSchAppointmentType;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.DateTimePicker dtpRescheduleDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}