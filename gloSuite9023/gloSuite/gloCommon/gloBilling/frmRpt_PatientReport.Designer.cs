namespace gloBilling
{
    partial class frmRpt_PatientReport
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
                    if (dtpDOSFrom != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDOSFrom);

                        }
                        catch
                        {
                        }


                        dtpDOSFrom.Dispose();
                        dtpDOSFrom = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtpDOSTo != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpDOSTo);

                        }
                        catch
                        {
                        }


                        dtpDOSTo.Dispose();
                        dtpDOSTo = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_PatientReport));
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.cmb_datefilter1 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClearDiagnosisCode = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseDiagnosisCode = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbReferral = new System.Windows.Forms.ComboBox();
            this.grpDateOfService = new System.Windows.Forms.GroupBox();
            this.dtpDOSTo = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDOSFrom = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.chkDateOfService = new System.Windows.Forms.CheckBox();
            this.grpDates = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.chkFromToDates = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.cmbDiagnosisCode = new System.Windows.Forms.ComboBox();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.btnClearRefProvider = new System.Windows.Forms.Button();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseRefProvider = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPatientDetails = new System.Windows.Forms.Panel();
            this.C1Patients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria.SuspendLayout();
            this.grpDateOfService.SuspendLayout();
            this.grpDates.SuspendLayout();
            this.pnlPatientDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.cmb_datefilter1);
            this.pnlCriteria.Controls.Add(this.label25);
            this.pnlCriteria.Controls.Add(this.label7);
            this.pnlCriteria.Controls.Add(this.label11);
            this.pnlCriteria.Controls.Add(this.cmb_datefilter);
            this.pnlCriteria.Controls.Add(this.txtZipCode);
            this.pnlCriteria.Controls.Add(this.cmbGender);
            this.pnlCriteria.Controls.Add(this.txtCity);
            this.pnlCriteria.Controls.Add(this.label6);
            this.pnlCriteria.Controls.Add(this.txtState);
            this.pnlCriteria.Controls.Add(this.lbl_datefilter);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.cmbProvider);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.label3);
            this.pnlCriteria.Controls.Add(this.btnClearDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.label15);
            this.pnlCriteria.Controls.Add(this.btnClearInsurance);
            this.pnlCriteria.Controls.Add(this.btnBrowseDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.label16);
            this.pnlCriteria.Controls.Add(this.cmbReferral);
            this.pnlCriteria.Controls.Add(this.grpDateOfService);
            this.pnlCriteria.Controls.Add(this.grpDates);
            this.pnlCriteria.Controls.Add(this.label8);
            this.pnlCriteria.Controls.Add(this.cmbInsurance);
            this.pnlCriteria.Controls.Add(this.cmbCPT);
            this.pnlCriteria.Controls.Add(this.label12);
            this.pnlCriteria.Controls.Add(this.label9);
            this.pnlCriteria.Controls.Add(this.label10);
            this.pnlCriteria.Controls.Add(this.cmbFacility);
            this.pnlCriteria.Controls.Add(this.label4);
            this.pnlCriteria.Controls.Add(this.btnClearCPT);
            this.pnlCriteria.Controls.Add(this.btnBrowseProvider);
            this.pnlCriteria.Controls.Add(this.cmbDiagnosisCode);
            this.pnlCriteria.Controls.Add(this.btnBrowseInsurance);
            this.pnlCriteria.Controls.Add(this.btnBrowseCPT);
            this.pnlCriteria.Controls.Add(this.btnClearRefProvider);
            this.pnlCriteria.Controls.Add(this.btnClearProvider);
            this.pnlCriteria.Controls.Add(this.btnBrowseRefProvider);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 55);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria.Size = new System.Drawing.Size(1145, 180);
            this.pnlCriteria.TabIndex = 90;
            // 
            // cmb_datefilter1
            // 
            this.cmb_datefilter1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter1.FormattingEnabled = true;
            this.cmb_datefilter1.Location = new System.Drawing.Point(378, 16);
            this.cmb_datefilter1.Name = "cmb_datefilter1";
            this.cmb_datefilter1.Size = new System.Drawing.Size(122, 22);
            this.cmb_datefilter1.TabIndex = 239;
            this.cmb_datefilter1.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter1_SelectedIndexChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(291, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(84, 14);
            this.label25.TabIndex = 238;
            this.label25.Text = "Service Date :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(4, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1137, 1);
            this.label7.TabIndex = 246;
            this.label7.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 173);
            this.label11.TabIndex = 245;
            this.label11.Text = "label4";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(125, 16);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(125, 22);
            this.cmb_datefilter.TabIndex = 237;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // txtZipCode
            // 
            this.txtZipCode.Location = new System.Drawing.Point(991, 141);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(56, 22);
            this.txtZipCode.TabIndex = 200;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "",
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(577, 141);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(83, 22);
            this.cmbGender.TabIndex = 209;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(709, 141);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(93, 22);
            this.txtCity.TabIndex = 201;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(521, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 211;
            this.label6.Text = "Gender :";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(860, 141);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(80, 22);
            this.txtState.TabIndex = 202;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(15, 20);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 236;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(671, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 206;
            this.label1.Text = "City :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(577, 51);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(149, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(812, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 207;
            this.label2.Text = "State :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(952, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 208;
            this.label3.Text = "Zip  :";
            // 
            // btnClearDiagnosisCode
            // 
            this.btnClearDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnClearDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.BackgroundImage")));
            this.btnClearDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDiagnosisCode.Image")));
            this.btnClearDiagnosisCode.Location = new System.Drawing.Point(1091, 49);
            this.btnClearDiagnosisCode.Name = "btnClearDiagnosisCode";
            this.btnClearDiagnosisCode.Size = new System.Drawing.Size(23, 23);
            this.btnClearDiagnosisCode.TabIndex = 220;
            this.btnClearDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnClearDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearDiagnosisCode.Click += new System.EventHandler(this.btnClearDiagnosisCode_Click);
            this.btnClearDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(1141, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 173);
            this.label15.TabIndex = 244;
            this.label15.Text = "label3";
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(762, 80);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(23, 23);
            this.btnClearInsurance.TabIndex = 228;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            this.btnClearInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseDiagnosisCode
            // 
            this.btnBrowseDiagnosisCode.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseDiagnosisCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.BackgroundImage")));
            this.btnBrowseDiagnosisCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseDiagnosisCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseDiagnosisCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDiagnosisCode.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseDiagnosisCode.Image")));
            this.btnBrowseDiagnosisCode.Location = new System.Drawing.Point(1062, 49);
            this.btnBrowseDiagnosisCode.Name = "btnBrowseDiagnosisCode";
            this.btnBrowseDiagnosisCode.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseDiagnosisCode.TabIndex = 219;
            this.btnBrowseDiagnosisCode.UseVisualStyleBackColor = false;
            this.btnBrowseDiagnosisCode.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseDiagnosisCode.Click += new System.EventHandler(this.btnBrowseDiagnosisCode_Click);
            this.btnBrowseDiagnosisCode.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1139, 1);
            this.label16.TabIndex = 243;
            this.label16.Text = "label1";
            // 
            // cmbReferral
            // 
            this.cmbReferral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferral.FormattingEnabled = true;
            this.cmbReferral.Location = new System.Drawing.Point(906, 82);
            this.cmbReferral.Name = "cmbReferral";
            this.cmbReferral.Size = new System.Drawing.Size(149, 22);
            this.cmbReferral.TabIndex = 242;
            // 
            // grpDateOfService
            // 
            this.grpDateOfService.Controls.Add(this.dtpDOSTo);
            this.grpDateOfService.Controls.Add(this.label13);
            this.grpDateOfService.Controls.Add(this.dtpDOSFrom);
            this.grpDateOfService.Controls.Add(this.label14);
            this.grpDateOfService.Controls.Add(this.chkDateOfService);
            this.grpDateOfService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpDateOfService.Location = new System.Drawing.Point(261, 43);
            this.grpDateOfService.Name = "grpDateOfService";
            this.grpDateOfService.Size = new System.Drawing.Size(239, 120);
            this.grpDateOfService.TabIndex = 237;
            this.grpDateOfService.TabStop = false;
            this.grpDateOfService.Text = "Date Of Service";
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
            this.dtpDOSTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOSTo.Location = new System.Drawing.Point(84, 73);
            this.dtpDOSTo.Name = "dtpDOSTo";
            this.dtpDOSTo.Size = new System.Drawing.Size(104, 22);
            this.dtpDOSTo.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 77);
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
            this.dtpDOSFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOSFrom.Location = new System.Drawing.Point(84, 34);
            this.dtpDOSFrom.Name = "dtpDOSFrom";
            this.dtpDOSFrom.Size = new System.Drawing.Size(104, 22);
            this.dtpDOSFrom.TabIndex = 5;
            this.dtpDOSFrom.ValueChanged += new System.EventHandler(this.dtpDOSFrom_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 38);
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
            this.chkDateOfService.Location = new System.Drawing.Point(194, 37);
            this.chkDateOfService.Name = "chkDateOfService";
            this.chkDateOfService.Size = new System.Drawing.Size(15, 14);
            this.chkDateOfService.TabIndex = 238;
            this.chkDateOfService.UseVisualStyleBackColor = true;
            this.chkDateOfService.Visible = false;
            // 
            // grpDates
            // 
            this.grpDates.Controls.Add(this.dtpEndDate);
            this.grpDates.Controls.Add(this.lblEndDate);
            this.grpDates.Controls.Add(this.dtpStartDate);
            this.grpDates.Controls.Add(this.lblStartDate);
            this.grpDates.Controls.Add(this.chkFromToDates);
            this.grpDates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpDates.Location = new System.Drawing.Point(15, 43);
            this.grpDates.Name = "grpDates";
            this.grpDates.Size = new System.Drawing.Size(235, 120);
            this.grpDates.TabIndex = 236;
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
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(84, 73);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(104, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(13, 77);
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
            this.dtpStartDate.Location = new System.Drawing.Point(84, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(104, 22);
            this.dtpStartDate.TabIndex = 5;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(7, 38);
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
            this.chkFromToDates.Location = new System.Drawing.Point(194, 38);
            this.chkFromToDates.Name = "chkFromToDates";
            this.chkFromToDates.Size = new System.Drawing.Size(15, 14);
            this.chkFromToDates.TabIndex = 235;
            this.chkFromToDates.UseVisualStyleBackColor = true;
            this.chkFromToDates.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(855, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 14);
            this.label8.TabIndex = 234;
            this.label8.Text = "Facility :";
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(577, 81);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(149, 22);
            this.cmbInsurance.TabIndex = 225;
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(577, 111);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(149, 22);
            this.cmbCPT.TabIndex = 229;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(809, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 14);
            this.label12.TabIndex = 218;
            this.label12.Text = "Diagnosis Code :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(539, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 14);
            this.label9.TabIndex = 230;
            this.label9.Text = "CPT :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(508, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 14);
            this.label10.TabIndex = 226;
            this.label10.Text = "Insurance :";
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(906, 112);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(149, 22);
            this.cmbFacility.TabIndex = 233;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(793, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 14);
            this.label4.TabIndex = 241;
            this.label4.Text = "Referring Provider :";
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(762, 109);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(23, 23);
            this.btnClearCPT.TabIndex = 232;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            this.btnClearCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnBrowseProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(732, 50);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(24, 24);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // cmbDiagnosisCode
            // 
            this.cmbDiagnosisCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosisCode.FormattingEnabled = true;
            this.cmbDiagnosisCode.Location = new System.Drawing.Point(906, 52);
            this.cmbDiagnosisCode.Name = "cmbDiagnosisCode";
            this.cmbDiagnosisCode.Size = new System.Drawing.Size(149, 22);
            this.cmbDiagnosisCode.TabIndex = 217;
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(733, 80);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseInsurance.TabIndex = 227;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(733, 109);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseCPT.TabIndex = 231;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            this.btnBrowseCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearRefProvider
            // 
            this.btnClearRefProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearRefProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearRefProvider.BackgroundImage")));
            this.btnClearRefProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearRefProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearRefProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearRefProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearRefProvider.Image")));
            this.btnClearRefProvider.Location = new System.Drawing.Point(1091, 78);
            this.btnClearRefProvider.Name = "btnClearRefProvider";
            this.btnClearRefProvider.Size = new System.Drawing.Size(23, 23);
            this.btnClearRefProvider.TabIndex = 240;
            this.btnClearRefProvider.UseVisualStyleBackColor = false;
            this.btnClearRefProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearRefProvider.Click += new System.EventHandler(this.btnClearRefProvider_Click);
            this.btnClearRefProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnClearProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnClearProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(761, 50);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(24, 24);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            this.btnClearProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseRefProvider
            // 
            this.btnBrowseRefProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseRefProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseRefProvider.BackgroundImage")));
            this.btnBrowseRefProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseRefProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseRefProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseRefProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseRefProvider.Image")));
            this.btnBrowseRefProvider.Location = new System.Drawing.Point(1062, 78);
            this.btnBrowseRefProvider.Name = "btnBrowseRefProvider";
            this.btnBrowseRefProvider.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseRefProvider.TabIndex = 239;
            this.btnBrowseRefProvider.UseVisualStyleBackColor = false;
            this.btnBrowseRefProvider.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseRefProvider.Click += new System.EventHandler(this.btnBrowseRefProvider_Click);
            this.btnBrowseRefProvider.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // pnlPatientDetails
            // 
            this.pnlPatientDetails.Controls.Add(this.C1Patients);
            this.pnlPatientDetails.Controls.Add(this.label21);
            this.pnlPatientDetails.Controls.Add(this.label22);
            this.pnlPatientDetails.Controls.Add(this.label23);
            this.pnlPatientDetails.Controls.Add(this.label24);
            this.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientDetails.Location = new System.Drawing.Point(0, 263);
            this.pnlPatientDetails.Name = "pnlPatientDetails";
            this.pnlPatientDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPatientDetails.Size = new System.Drawing.Size(1145, 400);
            this.pnlPatientDetails.TabIndex = 216;
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
            this.C1Patients.Location = new System.Drawing.Point(4, 1);
            this.C1Patients.Name = "C1Patients";
            this.C1Patients.Rows.Count = 1;
            this.C1Patients.Rows.DefaultSize = 19;
            this.C1Patients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Patients.ShowCellLabels = true;
            this.C1Patients.Size = new System.Drawing.Size(1137, 395);
            this.C1Patients.StyleInfo = resources.GetString("C1Patients.StyleInfo");
            this.C1Patients.TabIndex = 87;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(4, 396);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1137, 1);
            this.label21.TabIndex = 91;
            this.label21.Text = "label2";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 396);
            this.label22.TabIndex = 90;
            this.label22.Text = "label4";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(1141, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 396);
            this.label23.TabIndex = 89;
            this.label23.Text = "label3";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1139, 1);
            this.label24.TabIndex = 88;
            this.label24.Text = "label1";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1145, 55);
            this.pnlToolStrip.TabIndex = 88;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1145, 53);
            this.tls_Top.TabIndex = 10;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 235);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1145, 28);
            this.panel1.TabIndex = 217;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1139, 25);
            this.panel2.TabIndex = 86;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(1, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1137, 1);
            this.label17.TabIndex = 12;
            this.label17.Text = "label2";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(1, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1137, 24);
            this.label26.TabIndex = 6;
            this.label26.Text = "   Patient";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 24);
            this.label18.TabIndex = 11;
            this.label18.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(1138, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 24);
            this.label19.TabIndex = 10;
            this.label19.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1139, 1);
            this.label20.TabIndex = 9;
            this.label20.Text = "label1";
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
            // frmRpt_PatientReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1145, 663);
            this.Controls.Add(this.pnlPatientDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_PatientReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Report";
            this.Load += new System.EventHandler(this.frmRpt_PatientReport_Load);
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.grpDateOfService.ResumeLayout(false);
            this.grpDateOfService.PerformLayout();
            this.grpDates.ResumeLayout(false);
            this.grpDates.PerformLayout();
            this.pnlPatientDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.Panel pnlPatientDetails;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Patients;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtZipCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Button btnClearDiagnosisCode;
        internal System.Windows.Forms.Button btnBrowseDiagnosisCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbDiagnosisCode;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.CheckBox chkDateOfService;
        private System.Windows.Forms.GroupBox grpDateOfService;
        private System.Windows.Forms.DateTimePicker dtpDOSTo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDOSFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkFromToDates;
        private System.Windows.Forms.GroupBox grpDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ComboBox cmbReferral;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button btnClearRefProvider;
        internal System.Windows.Forms.Button btnBrowseRefProvider;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
    }
}