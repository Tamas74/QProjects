namespace gloBilling
{
    partial class frmSetupGlobalPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupGlobalPeriod));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDaysCaption = new System.Windows.Forms.Label();
            this.lblEndDateCaption = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbPeriodDays = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBillingReminder = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblCPTDesc = new System.Windows.Forms.Label();
            this.txtCpt = new System.Windows.Forms.TextBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblPatientCaption = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.TabIndex = 118;
            this.label1.Text = "Insurance :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(53, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 119;
            this.label2.Text = "CPT :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(445, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 120;
            this.label3.Text = "Start :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDaysCaption
            // 
            this.lblDaysCaption.AutoSize = true;
            this.lblDaysCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDaysCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDaysCaption.Location = new System.Drawing.Point(615, 66);
            this.lblDaysCaption.Name = "lblDaysCaption";
            this.lblDaysCaption.Size = new System.Drawing.Size(40, 14);
            this.lblDaysCaption.TabIndex = 121;
            this.lblDaysCaption.Text = "Days :";
            this.lblDaysCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDateCaption
            // 
            this.lblEndDateCaption.AutoSize = true;
            this.lblEndDateCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDateCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDateCaption.Location = new System.Drawing.Point(721, 66);
            this.lblEndDateCaption.Name = "lblEndDateCaption";
            this.lblEndDateCaption.Size = new System.Drawing.Size(36, 14);
            this.lblEndDateCaption.TabIndex = 122;
            this.lblEndDateCaption.Text = "End :";
            this.lblEndDateCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(428, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 14);
            this.label6.TabIndex = 124;
            this.label6.Text = "Provider :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(24, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 125;
            this.label7.Text = "Reminder :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(441, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 126;
            this.label8.Text = "Note :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(488, 62);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(111, 22);
            this.dtpStartDate.TabIndex = 127;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(488, 91);
            this.txtNote.MaxLength = 255;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(327, 155);
            this.txtNote.TabIndex = 129;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(849, 53);
            this.ts_Commands.TabIndex = 134;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
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
            this.panel1.Controls.Add(this.cmbPeriodDays);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtBillingReminder);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblEndDate);
            this.panel1.Controls.Add(this.lblDaysCaption);
            this.panel1.Controls.Add(this.lblEndDateCaption);
            this.panel1.Controls.Add(this.lblCPTDesc);
            this.panel1.Controls.Add(this.txtCpt);
            this.panel1.Controls.Add(this.cmbProvider);
            this.panel1.Controls.Add(this.cmbInsurance);
            this.panel1.Controls.Add(this.lblPatient);
            this.panel1.Controls.Add(this.lblPatientCaption);
            this.panel1.Controls.Add(this.lbl_LeftBrd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtNote);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.pnlInternalControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(849, 258);
            this.panel1.TabIndex = 135;
            // 
            // cmbPeriodDays
            // 
            this.cmbPeriodDays.FormattingEnabled = true;
            this.cmbPeriodDays.Location = new System.Drawing.Point(656, 62);
            this.cmbPeriodDays.MaxLength = 4;
            this.cmbPeriodDays.Name = "cmbPeriodDays";
            this.cmbPeriodDays.Size = new System.Drawing.Size(63, 22);
            this.cmbPeriodDays.TabIndex = 158;
            this.cmbPeriodDays.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodDays_SelectedIndexChanged);
            this.cmbPeriodDays.TextChanged += new System.EventHandler(this.cmbPeriodDays_TextChanged);
            this.cmbPeriodDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPeriodDays_KeyPress);
            this.cmbPeriodDays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbPeriodDays_MouseDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(604, 66);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 14);
            this.label10.TabIndex = 157;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(41, 65);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 14);
            this.label13.TabIndex = 156;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBillingReminder
            // 
            this.txtBillingReminder.BackColor = System.Drawing.Color.Transparent;
            this.txtBillingReminder.Location = new System.Drawing.Point(97, 91);
            this.txtBillingReminder.Name = "txtBillingReminder";
            this.txtBillingReminder.Size = new System.Drawing.Size(327, 155);
            this.txtBillingReminder.TabIndex = 0;
            this.txtBillingReminder.Text = "label10";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(841, 1);
            this.label9.TabIndex = 155;
            this.label9.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(841, 1);
            this.label5.TabIndex = 154;
            this.label5.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(845, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 252);
            this.label4.TabIndex = 153;
            this.label4.Text = "label4";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDate.Location = new System.Drawing.Point(757, 66);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(73, 14);
            this.lblEndDate.TabIndex = 134;
            this.lblEndDate.Text = "09/14/2011";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCPTDesc
            // 
            this.lblCPTDesc.AutoEllipsis = true;
            this.lblCPTDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPTDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTDesc.Location = new System.Drawing.Point(175, 65);
            this.lblCPTDesc.Name = "lblCPTDesc";
            this.lblCPTDesc.Size = new System.Drawing.Size(209, 15);
            this.lblCPTDesc.TabIndex = 143;
            this.lblCPTDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCpt
            // 
            this.txtCpt.Location = new System.Drawing.Point(94, 62);
            this.txtCpt.MaxLength = 10;
            this.txtCpt.Name = "txtCpt";
            this.txtCpt.Size = new System.Drawing.Size(79, 22);
            this.txtCpt.TabIndex = 142;
            this.txtCpt.TextChanged += new System.EventHandler(this.txtCpt_TextChanged);
            this.txtCpt.Enter += new System.EventHandler(this.txtCpt_Enter);
            this.txtCpt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCpt_KeyUp);
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(488, 33);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(329, 22);
            this.cmbProvider.TabIndex = 138;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.Location = new System.Drawing.Point(94, 33);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(329, 22);
            this.cmbInsurance.TabIndex = 137;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(94, 13);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(0, 14);
            this.lblPatient.TabIndex = 136;
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientCaption
            // 
            this.lblPatientCaption.AutoSize = true;
            this.lblPatientCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientCaption.Location = new System.Drawing.Point(36, 13);
            this.lblPatientCaption.Name = "lblPatientCaption";
            this.lblPatientCaption.Size = new System.Drawing.Size(54, 14);
            this.lblPatientCaption.TabIndex = 135;
            this.lblPatientCaption.Text = "Patient :";
            this.lblPatientCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 252);
            this.lbl_LeftBrd.TabIndex = 132;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(94, 85);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(333, 158);
            this.pnlInternalControl.TabIndex = 140;
            this.pnlInternalControl.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 55);
            this.panel2.TabIndex = 156;
            // 
            // frmSetupGlobalPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(849, 313);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupGlobalPeriod";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global Period";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupGlobalPeriod_FormClosing);
            this.Load += new System.EventHandler(this.frmSetupGlobalPeriod_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSetupGlobalPeriod_KeyUp);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label lblDaysCaption;
        internal System.Windows.Forms.Label lblEndDateCaption;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtNote;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_LeftBrd;
        internal System.Windows.Forms.Label lblEndDate;
        internal System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Label lblPatientCaption;
        internal System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.TextBox txtCpt;
        internal System.Windows.Forms.Label lblCPTDesc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtBillingReminder;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbPeriodDays;



    }
}