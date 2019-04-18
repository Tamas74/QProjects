namespace gloBilling
{
    partial class frmRpt_MonthEnd
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpEndDate, dtpStartDate, dtpDOSFrom, dtpDOSTo };
            System.Windows.Forms.Control[] cntControls = { dtpEndDate, dtpStartDate, dtpDOSFrom, dtpDOSTo };

            if (disposing && (components != null))
            {
                components.Dispose();
                base.Dispose(disposing);
                try
                {
                     if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    //if (dtpEndDate != null)
                    //{
                    //    try
                    //    {
                    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);

                    //    }
                    //    catch
                    //    {
                    //    }


                    //    dtpEndDate.Dispose();
                    //    dtpEndDate = null;
                    //}
                }
                catch
                {
                }

                //try
                //{
                //    if (dtpStartDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtpStartDate.Dispose();
                //        dtpStartDate = null;
                //    }
                //}
                //catch
                //{
                //}


                //try
                //{
                //    if (dtpDOSFrom != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDOSFrom);

                //        }
                //        catch
                //        {
                //        }


                //        dtpDOSFrom.Dispose();
                //        dtpDOSFrom = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpDOSTo != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDOSTo);

                //        }
                //        catch
                //        {
                //        }


                //        dtpDOSTo.Dispose();
                //        dtpDOSTo = null;
                //    }
                //}
                //catch
                //{
                //}


                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_MonthEnd));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlC1Grid = new System.Windows.Forms.Panel();
            this.C1Report = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grpDates = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.chkFromToDates = new System.Windows.Forms.CheckBox();
            this.btnBrowsePatient = new System.Windows.Forms.Button();
            this.btnClearPatient = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDiagnosisCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseDiagnosisCode = new System.Windows.Forms.Button();
            this.btnClearDiagnosisCode = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpCriteria = new System.Windows.Forms.GroupBox();
            this.rbWriteOff = new System.Windows.Forms.RadioButton();
            this.rbAmountBilled = new System.Windows.Forms.RadioButton();
            this.rbMoneyReceived = new System.Windows.Forms.RadioButton();
            this.rbPatient_VS_Insurance = new System.Windows.Forms.RadioButton();
            this.grpDateOfService = new System.Windows.Forms.GroupBox();
            this.dtpDOSTo = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDOSFrom = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.chkDateOfService = new System.Windows.Forms.CheckBox();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.lblChargesTray = new System.Windows.Forms.Label();
            this.cmbChargesTray = new System.Windows.Forms.ComboBox();
            this.cmb_datefilter1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlC1Grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Report)).BeginInit();
            this.grpDates.SuspendLayout();
            this.grpCriteria.SuspendLayout();
            this.grpDateOfService.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1218, 54);
            this.pnlToolStrip.TabIndex = 2;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1216, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(93, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Show Report";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
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
            // pnlC1Grid
            // 
            this.pnlC1Grid.Controls.Add(this.C1Report);
            this.pnlC1Grid.Controls.Add(this.label9);
            this.pnlC1Grid.Controls.Add(this.label10);
            this.pnlC1Grid.Controls.Add(this.label11);
            this.pnlC1Grid.Controls.Add(this.label12);
            this.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1Grid.Location = new System.Drawing.Point(0, 269);
            this.pnlC1Grid.Name = "pnlC1Grid";
            this.pnlC1Grid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlC1Grid.Size = new System.Drawing.Size(1218, 438);
            this.pnlC1Grid.TabIndex = 1;
            // 
            // C1Report
            // 
            this.C1Report.AllowEditing = false;
            this.C1Report.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1Report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Report.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1Report.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Report.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.C1Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Report.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Report.ForeColor = System.Drawing.Color.DarkBlue;
            this.C1Report.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1Report.Location = new System.Drawing.Point(4, 1);
            this.C1Report.Name = "C1Report";
            this.C1Report.Rows.Count = 1;
            this.C1Report.Rows.DefaultSize = 19;
            this.C1Report.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Report.ShowCellLabels = true;
            this.C1Report.Size = new System.Drawing.Size(1210, 433);
            this.C1Report.StyleInfo = resources.GetString("C1Report.StyleInfo");
            this.C1Report.TabIndex = 0;
            this.C1Report.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1Report_MouseMove);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(4, 434);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1210, 1);
            this.label9.TabIndex = 91;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 434);
            this.label10.TabIndex = 90;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(1214, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 434);
            this.label11.TabIndex = 89;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1212, 1);
            this.label12.TabIndex = 88;
            this.label12.Text = "label1";
            // 
            // grpDates
            // 
            this.grpDates.Controls.Add(this.dtpEndDate);
            this.grpDates.Controls.Add(this.lblEndDate);
            this.grpDates.Controls.Add(this.dtpStartDate);
            this.grpDates.Controls.Add(this.lblStartDate);
            this.grpDates.Controls.Add(this.chkFromToDates);
            this.grpDates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpDates.Location = new System.Drawing.Point(15, 52);
            this.grpDates.Name = "grpDates";
            this.grpDates.Size = new System.Drawing.Size(226, 101);
            this.grpDates.TabIndex = 3;
            this.grpDates.TabStop = false;
            this.grpDates.Text = "Transaction Date";
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
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(84, 62);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(104, 22);
            this.dtpEndDate.TabIndex = 2;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(13, 66);
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
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(84, 27);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(104, 22);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(7, 31);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // chkFromToDates
            // 
            this.chkFromToDates.AutoSize = true;
            this.chkFromToDates.Checked = true;
            this.chkFromToDates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFromToDates.Location = new System.Drawing.Point(193, 31);
            this.chkFromToDates.Name = "chkFromToDates";
            this.chkFromToDates.Size = new System.Drawing.Size(15, 14);
            this.chkFromToDates.TabIndex = 1;
            this.chkFromToDates.UseVisualStyleBackColor = true;
            this.chkFromToDates.Visible = false;
            // 
            // btnBrowsePatient
            // 
            this.btnBrowsePatient.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.BackgroundImage")));
            this.btnBrowsePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowsePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePatient.Image")));
            this.btnBrowsePatient.Location = new System.Drawing.Point(781, 14);
            this.btnBrowsePatient.Name = "btnBrowsePatient";
            this.btnBrowsePatient.Size = new System.Drawing.Size(22, 22);
            this.btnBrowsePatient.TabIndex = 5;
            this.btnBrowsePatient.UseVisualStyleBackColor = false;
            this.btnBrowsePatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowsePatient.Click += new System.EventHandler(this.btnBrowsePatient_Click);
            this.btnBrowsePatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearPatient
            // 
            this.btnClearPatient.BackColor = System.Drawing.Color.Transparent;
            this.btnClearPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.BackgroundImage")));
            this.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPatient.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPatient.Image")));
            this.btnClearPatient.Location = new System.Drawing.Point(808, 14);
            this.btnClearPatient.Name = "btnClearPatient";
            this.btnClearPatient.Size = new System.Drawing.Size(22, 22);
            this.btnClearPatient.TabIndex = 6;
            this.btnClearPatient.UseVisualStyleBackColor = false;
            this.btnClearPatient.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearPatient.Click += new System.EventHandler(this.btnClearPatient_Click);
            this.btnClearPatient.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(523, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 177;
            this.label1.Text = "Patient :";
            // 
            // cmbDiagnosisCode
            // 
            this.cmbDiagnosisCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosisCode.FormattingEnabled = true;
            this.cmbDiagnosisCode.Location = new System.Drawing.Point(580, 104);
            this.cmbDiagnosisCode.Name = "cmbDiagnosisCode";
            this.cmbDiagnosisCode.Size = new System.Drawing.Size(196, 22);
            this.cmbDiagnosisCode.TabIndex = 0;
            this.cmbDiagnosisCode.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(481, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 14);
            this.label4.TabIndex = 181;
            this.label4.Text = "Diagnosis Code :";
            // 
            // btnBrowseDiagnosisCode
            // 
            this.btnBrowseDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.BackgroundImage")));
            this.btnBrowseDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.Image")));
            this.btnBrowseDiagnosisCode.Location = new System.Drawing.Point(781, 104);
            this.btnBrowseDiagnosisCode.Name = "btnBrowseDiagnosisCode";
            this.btnBrowseDiagnosisCode.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseDiagnosisCode.TabIndex = 15;
            this.btnBrowseDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnBrowseDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseDiagnosisCode.Click += new System.EventHandler(this.btnBrowseDiagnosisCode_Click);
            this.btnBrowseDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearDiagnosisCode
            // 
            this.btnClearDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.BackgroundImage")));
            this.btnClearDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.Image")));
            this.btnClearDiagnosisCode.Location = new System.Drawing.Point(808, 104);
            this.btnClearDiagnosisCode.Name = "btnClearDiagnosisCode";
            this.btnClearDiagnosisCode.Size = new System.Drawing.Size(22, 22);
            this.btnClearDiagnosisCode.TabIndex = 16;
            this.btnClearDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnClearDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearDiagnosisCode.Click += new System.EventHandler(this.btnClearDiagnosisCode_Click);
            this.btnClearDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(580, 44);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(196, 22);
            this.cmbProvider.TabIndex = 184;
            this.cmbProvider.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(518, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(781, 44);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseProvider.TabIndex = 9;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            this.btnBrowseProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(808, 44);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearProvider.TabIndex = 10;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            this.btnClearProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(580, 74);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(196, 22);
            this.cmbInsurance.TabIndex = 189;
            this.cmbInsurance.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 14);
            this.label6.TabIndex = 190;
            this.label6.Text = "Insurance :";
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(781, 74);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 12;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(808, 74);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsurance.TabIndex = 13;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            this.btnClearInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(580, 14);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(196, 22);
            this.cmbPatients.TabIndex = 193;
            this.cmbPatients.TabStop = false;
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(1212, 1);
            this.lbl_TopBrd.TabIndex = 201;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(1214, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 208);
            this.lbl_RightBrd.TabIndex = 202;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 208);
            this.lbl_LeftBrd.TabIndex = 203;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 211);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(1210, 1);
            this.lbl_BottomBrd.TabIndex = 204;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(935, 14);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(196, 22);
            this.cmbCPT.TabIndex = 210;
            this.cmbCPT.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(895, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 14);
            this.label7.TabIndex = 211;
            this.label7.Text = "CPT :";
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(1135, 14);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCPT.TabIndex = 7;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            this.btnBrowseCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(1162, 14);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearCPT.TabIndex = 8;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            this.btnClearCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(935, 74);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(196, 22);
            this.cmbFacility.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(882, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 14);
            this.label3.TabIndex = 215;
            this.label3.Text = "Facility :";
            // 
            // grpCriteria
            // 
            this.grpCriteria.Controls.Add(this.rbWriteOff);
            this.grpCriteria.Controls.Add(this.rbAmountBilled);
            this.grpCriteria.Controls.Add(this.rbMoneyReceived);
            this.grpCriteria.Controls.Add(this.rbPatient_VS_Insurance);
            this.grpCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpCriteria.Location = new System.Drawing.Point(15, 160);
            this.grpCriteria.Name = "grpCriteria";
            this.grpCriteria.Size = new System.Drawing.Size(608, 40);
            this.grpCriteria.TabIndex = 21;
            this.grpCriteria.TabStop = false;
            // 
            // rbWriteOff
            // 
            this.rbWriteOff.AutoSize = true;
            this.rbWriteOff.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWriteOff.Location = new System.Drawing.Point(511, 15);
            this.rbWriteOff.Name = "rbWriteOff";
            this.rbWriteOff.Size = new System.Drawing.Size(76, 18);
            this.rbWriteOff.TabIndex = 3;
            this.rbWriteOff.Text = "Write-Off";
            this.rbWriteOff.UseVisualStyleBackColor = true;
            this.rbWriteOff.CheckedChanged += new System.EventHandler(this.rbWriteOff_CheckedChanged);
            // 
            // rbAmountBilled
            // 
            this.rbAmountBilled.AutoSize = true;
            this.rbAmountBilled.Checked = true;
            this.rbAmountBilled.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAmountBilled.Location = new System.Drawing.Point(18, 15);
            this.rbAmountBilled.Name = "rbAmountBilled";
            this.rbAmountBilled.Size = new System.Drawing.Size(111, 18);
            this.rbAmountBilled.TabIndex = 0;
            this.rbAmountBilled.TabStop = true;
            this.rbAmountBilled.Text = "Amount Billed";
            this.rbAmountBilled.UseVisualStyleBackColor = true;
            this.rbAmountBilled.CheckedChanged += new System.EventHandler(this.rbAmountBilled_CheckedChanged);
            // 
            // rbMoneyReceived
            // 
            this.rbMoneyReceived.AutoSize = true;
            this.rbMoneyReceived.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMoneyReceived.Location = new System.Drawing.Point(154, 15);
            this.rbMoneyReceived.Name = "rbMoneyReceived";
            this.rbMoneyReceived.Size = new System.Drawing.Size(114, 18);
            this.rbMoneyReceived.TabIndex = 1;
            this.rbMoneyReceived.Text = "Money Received";
            this.rbMoneyReceived.UseVisualStyleBackColor = true;
            this.rbMoneyReceived.CheckedChanged += new System.EventHandler(this.rbMoneyReceived_CheckedChanged);
            // 
            // rbPatient_VS_Insurance
            // 
            this.rbPatient_VS_Insurance.AutoSize = true;
            this.rbPatient_VS_Insurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPatient_VS_Insurance.Location = new System.Drawing.Point(293, 15);
            this.rbPatient_VS_Insurance.Name = "rbPatient_VS_Insurance";
            this.rbPatient_VS_Insurance.Size = new System.Drawing.Size(193, 18);
            this.rbPatient_VS_Insurance.TabIndex = 2;
            this.rbPatient_VS_Insurance.Text = "Patient v/s Insurance Payment";
            this.rbPatient_VS_Insurance.UseVisualStyleBackColor = true;
            this.rbPatient_VS_Insurance.CheckedChanged += new System.EventHandler(this.rbPatient_VS_Insurance_CheckedChanged);
            // 
            // grpDateOfService
            // 
            this.grpDateOfService.Controls.Add(this.dtpDOSTo);
            this.grpDateOfService.Controls.Add(this.label13);
            this.grpDateOfService.Controls.Add(this.dtpDOSFrom);
            this.grpDateOfService.Controls.Add(this.label14);
            this.grpDateOfService.Controls.Add(this.chkDateOfService);
            this.grpDateOfService.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDateOfService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpDateOfService.Location = new System.Drawing.Point(258, 52);
            this.grpDateOfService.Name = "grpDateOfService";
            this.grpDateOfService.Size = new System.Drawing.Size(217, 101);
            this.grpDateOfService.TabIndex = 4;
            this.grpDateOfService.TabStop = false;
            this.grpDateOfService.Text = "Date of service";
            // 
            // dtpDOSTo
            // 
            this.dtpDOSTo.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOSTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOSTo.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOSTo.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOSTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOSTo.Checked = false;
            this.dtpDOSTo.CustomFormat = "MM/dd/yyyy";
            this.dtpDOSTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOSTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOSTo.Location = new System.Drawing.Point(84, 62);
            this.dtpDOSTo.Name = "dtpDOSTo";
            this.dtpDOSTo.Size = new System.Drawing.Size(104, 22);
            this.dtpDOSTo.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(13, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 14);
            this.label13.TabIndex = 6;
            this.label13.Text = "End Date :";
            // 
            // dtpDOSFrom
            // 
            this.dtpDOSFrom.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDOSFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDOSFrom.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDOSFrom.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDOSFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDOSFrom.Checked = false;
            this.dtpDOSFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpDOSFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOSFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOSFrom.Location = new System.Drawing.Point(84, 27);
            this.dtpDOSFrom.Name = "dtpDOSFrom";
            this.dtpDOSFrom.Size = new System.Drawing.Size(104, 22);
            this.dtpDOSFrom.TabIndex = 0;
            this.dtpDOSFrom.ValueChanged += new System.EventHandler(this.dtpDOSFrom_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 14);
            this.label14.TabIndex = 4;
            this.label14.Text = "Start Date :";
            // 
            // chkDateOfService
            // 
            this.chkDateOfService.AutoSize = true;
            this.chkDateOfService.Checked = true;
            this.chkDateOfService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDateOfService.Location = new System.Drawing.Point(195, 33);
            this.chkDateOfService.Name = "chkDateOfService";
            this.chkDateOfService.Size = new System.Drawing.Size(15, 14);
            this.chkDateOfService.TabIndex = 1;
            this.chkDateOfService.UseVisualStyleBackColor = true;
            this.chkDateOfService.Visible = false;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.lblChargesTray);
            this.pnlCriteria.Controls.Add(this.cmbChargesTray);
            this.pnlCriteria.Controls.Add(this.cmb_datefilter1);
            this.pnlCriteria.Controls.Add(this.label8);
            this.pnlCriteria.Controls.Add(this.cmb_datefilter);
            this.pnlCriteria.Controls.Add(this.lbl_datefilter);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.cmbGender);
            this.pnlCriteria.Controls.Add(this.label15);
            this.pnlCriteria.Controls.Add(this.label16);
            this.pnlCriteria.Controls.Add(this.label17);
            this.pnlCriteria.Controls.Add(this.txtState);
            this.pnlCriteria.Controls.Add(this.txtCity);
            this.pnlCriteria.Controls.Add(this.txtZipCode);
            this.pnlCriteria.Controls.Add(this.grpDateOfService);
            this.pnlCriteria.Controls.Add(this.grpCriteria);
            this.pnlCriteria.Controls.Add(this.label3);
            this.pnlCriteria.Controls.Add(this.cmbFacility);
            this.pnlCriteria.Controls.Add(this.btnClearCPT);
            this.pnlCriteria.Controls.Add(this.btnBrowseCPT);
            this.pnlCriteria.Controls.Add(this.label7);
            this.pnlCriteria.Controls.Add(this.cmbCPT);
            this.pnlCriteria.Controls.Add(this.lbl_BottomBrd);
            this.pnlCriteria.Controls.Add(this.lbl_LeftBrd);
            this.pnlCriteria.Controls.Add(this.lbl_RightBrd);
            this.pnlCriteria.Controls.Add(this.lbl_TopBrd);
            this.pnlCriteria.Controls.Add(this.cmbPatients);
            this.pnlCriteria.Controls.Add(this.btnClearInsurance);
            this.pnlCriteria.Controls.Add(this.btnBrowseInsurance);
            this.pnlCriteria.Controls.Add(this.label6);
            this.pnlCriteria.Controls.Add(this.cmbInsurance);
            this.pnlCriteria.Controls.Add(this.btnClearProvider);
            this.pnlCriteria.Controls.Add(this.btnBrowseProvider);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.cmbProvider);
            this.pnlCriteria.Controls.Add(this.btnClearDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.btnBrowseDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.cmbDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.btnClearPatient);
            this.pnlCriteria.Controls.Add(this.btnBrowsePatient);
            this.pnlCriteria.Controls.Add(this.grpDates);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCriteria.Location = new System.Drawing.Point(0, 54);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria.Size = new System.Drawing.Size(1218, 215);
            this.pnlCriteria.TabIndex = 0;
            // 
            // lblChargesTray
            // 
            this.lblChargesTray.AutoSize = true;
            this.lblChargesTray.Location = new System.Drawing.Point(846, 48);
            this.lblChargesTray.Name = "lblChargesTray";
            this.lblChargesTray.Size = new System.Drawing.Size(86, 14);
            this.lblChargesTray.TabIndex = 237;
            this.lblChargesTray.Text = "Charges Tray :";
            // 
            // cmbChargesTray
            // 
            this.cmbChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesTray.FormattingEnabled = true;
            this.cmbChargesTray.Location = new System.Drawing.Point(935, 44);
            this.cmbChargesTray.Name = "cmbChargesTray";
            this.cmbChargesTray.Size = new System.Drawing.Size(196, 22);
            this.cmbChargesTray.TabIndex = 11;
            // 
            // cmb_datefilter1
            // 
            this.cmb_datefilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter1.FormattingEnabled = true;
            this.cmb_datefilter1.Location = new System.Drawing.Point(345, 18);
            this.cmb_datefilter1.Name = "cmb_datefilter1";
            this.cmb_datefilter1.Size = new System.Drawing.Size(117, 22);
            this.cmb_datefilter1.TabIndex = 2;
            this.cmb_datefilter1.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 14);
            this.label8.TabIndex = 234;
            this.label8.Text = "Service Date :";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(124, 18);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(117, 22);
            this.cmb_datefilter.TabIndex = 0;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(15, 22);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 232;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(877, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 231;
            this.label2.Text = "Gender :";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "",
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(935, 104);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(196, 22);
            this.cmbGender.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(769, 138);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 14);
            this.label15.TabIndex = 228;
            this.label15.Text = "Zip :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(651, 138);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 14);
            this.label16.TabIndex = 227;
            this.label16.Text = "State :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(542, 138);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 14);
            this.label17.TabIndex = 226;
            this.label17.Text = "City :";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(699, 134);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(65, 22);
            this.txtState.TabIndex = 19;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(580, 134);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(68, 22);
            this.txtCity.TabIndex = 18;
            // 
            // txtZipCode
            // 
            this.txtZipCode.Location = new System.Drawing.Point(801, 134);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(57, 22);
            this.txtZipCode.TabIndex = 20;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmRpt_MonthEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1218, 707);
            this.Controls.Add(this.pnlC1Grid);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_MonthEnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Charge Summary Report";
            this.Load += new System.EventHandler(this.frmRpt_MonthEnd_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlC1Grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Report)).EndInit();
            this.grpDates.ResumeLayout(false);
            this.grpDates.PerformLayout();
            this.grpCriteria.ResumeLayout(false);
            this.grpCriteria.PerformLayout();
            this.grpDateOfService.ResumeLayout(false);
            this.grpDateOfService.PerformLayout();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlC1Grid;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Report;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grpDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        internal System.Windows.Forms.Button btnBrowsePatient;
        internal System.Windows.Forms.Button btnClearPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDiagnosisCode;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button btnBrowseDiagnosisCode;
        internal System.Windows.Forms.Button btnClearDiagnosisCode;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button btnBrowseProvider;
        internal System.Windows.Forms.Button btnClearProvider;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        internal System.Windows.Forms.Button btnClearInsurance;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.CheckBox chkFromToDates;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.ComboBox cmbCPT;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button btnBrowseCPT;
        internal System.Windows.Forms.Button btnClearCPT;
        private System.Windows.Forms.ComboBox cmbFacility;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpCriteria;
        private System.Windows.Forms.RadioButton rbAmountBilled;
        private System.Windows.Forms.RadioButton rbMoneyReceived;
        private System.Windows.Forms.RadioButton rbPatient_VS_Insurance;
        private System.Windows.Forms.GroupBox grpDateOfService;
        private System.Windows.Forms.DateTimePicker dtpDOSTo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDOSFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.CheckBox chkDateOfService;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.RadioButton rbWriteOff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        private System.Windows.Forms.Label lblChargesTray;
        private System.Windows.Forms.ComboBox cmbChargesTray;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}