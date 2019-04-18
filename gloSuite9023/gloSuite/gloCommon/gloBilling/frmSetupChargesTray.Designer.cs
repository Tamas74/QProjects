namespace gloBilling
{
    partial class frmSetupChargesTray
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
                    if (dtpStartdate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartdate);

                        }
                        catch
                        {
                        }


                        dtpStartdate.Dispose();
                        dtpStartdate = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupChargesTray));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.chkisdefault = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtdescription = new System.Windows.Forms.TextBox();
            this.lbldesc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.dtpStartdate = new System.Windows.Forms.DateTimePicker();
            this.lblStDate = new System.Windows.Forms.Label();
            this.numnoofdays = new System.Windows.Forms.NumericUpDown();
            this.lblnoofdays = new System.Windows.Forms.Label();
            this.chkIsActivated = new System.Windows.Forms.CheckBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.Panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numnoofdays)).BeginInit();
            this.pnl_Toolstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.panel5);
            this.Panel2.Controls.Add(this.panel1);
            this.Panel2.Controls.Add(this.panel4);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.txtcode);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Controls.Add(this.dtpStartdate);
            this.Panel2.Controls.Add(this.lblStDate);
            this.Panel2.Controls.Add(this.numnoofdays);
            this.Panel2.Controls.Add(this.lblnoofdays);
            this.Panel2.Controls.Add(this.chkIsActivated);
            this.Panel2.Controls.Add(this.dtpEndDate);
            this.Panel2.Controls.Add(this.lblEndDate);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 54);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(382, 118);
            this.Panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbInactive);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.rbActive);
            this.panel5.Controls.Add(this.chkisdefault);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 61);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(374, 53);
            this.panel5.TabIndex = 124;
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Location = new System.Drawing.Point(189, 4);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(68, 18);
            this.rbInactive.TabIndex = 1;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 122;
            this.label2.Text = "Status :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActive.Location = new System.Drawing.Point(116, 4);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(63, 18);
            this.rbActive.TabIndex = 0;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.CheckedChanged += new System.EventHandler(this.rbActive_CheckedChanged);
            // 
            // chkisdefault
            // 
            this.chkisdefault.AutoSize = true;
            this.chkisdefault.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkisdefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkisdefault.Location = new System.Drawing.Point(116, 29);
            this.chkisdefault.Name = "chkisdefault";
            this.chkisdefault.Size = new System.Drawing.Size(65, 18);
            this.chkisdefault.TabIndex = 5;
            this.chkisdefault.Text = "Default";
            this.chkisdefault.UseVisualStyleBackColor = true;
            this.chkisdefault.CheckedChanged += new System.EventHandler(this.chkisdefault_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbUser);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 27);
            this.panel1.TabIndex = 123;
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.ForeColor = System.Drawing.Color.Black;
            this.cmbUser.Location = new System.Drawing.Point(114, 2);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(208, 22);
            this.cmbUser.TabIndex = 118;
            this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUser.Location = new System.Drawing.Point(72, 4);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 119;
            this.lblUser.Text = "User :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtdescription);
            this.panel4.Controls.Add(this.lbldesc);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(374, 30);
            this.panel4.TabIndex = 123;
            // 
            // txtdescription
            // 
            this.txtdescription.ForeColor = System.Drawing.Color.Black;
            this.txtdescription.Location = new System.Drawing.Point(114, 5);
            this.txtdescription.Name = "txtdescription";
            this.txtdescription.Size = new System.Drawing.Size(208, 22);
            this.txtdescription.TabIndex = 1;
            // 
            // lbldesc
            // 
            this.lbldesc.AutoSize = true;
            this.lbldesc.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbldesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbldesc.Location = new System.Drawing.Point(36, 9);
            this.lbldesc.Name = "lbldesc";
            this.lbldesc.Size = new System.Drawing.Size(75, 14);
            this.lbldesc.TabIndex = 12;
            this.lbldesc.Text = "Description :";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(21, 9);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 112;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 114);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(374, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 111);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // txtcode
            // 
            this.txtcode.BackColor = System.Drawing.SystemColors.Window;
            this.txtcode.ForeColor = System.Drawing.Color.Black;
            this.txtcode.Location = new System.Drawing.Point(196, 222);
            this.txtcode.Name = "txtcode";
            this.txtcode.ReadOnly = true;
            this.txtcode.Size = new System.Drawing.Size(76, 22);
            this.txtcode.TabIndex = 0;
            this.txtcode.TabStop = false;
            this.txtcode.Visible = false;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(378, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 111);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(115, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 113;
            this.label1.Text = "Code :";
            this.label1.Visible = false;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(376, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // dtpStartdate
            // 
            this.dtpStartdate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartdate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartdate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartdate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartdate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartdate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartdate.Location = new System.Drawing.Point(130, 257);
            this.dtpStartdate.Name = "dtpStartdate";
            this.dtpStartdate.Size = new System.Drawing.Size(131, 22);
            this.dtpStartdate.TabIndex = 2;
            this.dtpStartdate.Visible = false;
            this.dtpStartdate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
            // 
            // lblStDate
            // 
            this.lblStDate.AutoSize = true;
            this.lblStDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStDate.Location = new System.Drawing.Point(288, 242);
            this.lblStDate.Name = "lblStDate";
            this.lblStDate.Size = new System.Drawing.Size(72, 14);
            this.lblStDate.TabIndex = 115;
            this.lblStDate.Text = "Start Date :";
            this.lblStDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStDate.Visible = false;
            // 
            // numnoofdays
            // 
            this.numnoofdays.Location = new System.Drawing.Point(357, 196);
            this.numnoofdays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numnoofdays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numnoofdays.Name = "numnoofdays";
            this.numnoofdays.Size = new System.Drawing.Size(55, 22);
            this.numnoofdays.TabIndex = 4;
            this.numnoofdays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numnoofdays.Visible = false;
            this.numnoofdays.ValueChanged += new System.EventHandler(this.numnoofdays_ValueChanged);
            // 
            // lblnoofdays
            // 
            this.lblnoofdays.AutoSize = true;
            this.lblnoofdays.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblnoofdays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblnoofdays.Location = new System.Drawing.Point(182, 177);
            this.lblnoofdays.Name = "lblnoofdays";
            this.lblnoofdays.Size = new System.Drawing.Size(78, 14);
            this.lblnoofdays.TabIndex = 13;
            this.lblnoofdays.Text = "No. of Days :";
            this.lblnoofdays.Visible = false;
            // 
            // chkIsActivated
            // 
            this.chkIsActivated.AutoSize = true;
            this.chkIsActivated.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkIsActivated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkIsActivated.Location = new System.Drawing.Point(102, 175);
            this.chkIsActivated.Name = "chkIsActivated";
            this.chkIsActivated.Size = new System.Drawing.Size(60, 18);
            this.chkIsActivated.TabIndex = 120;
            this.chkIsActivated.Text = "Active";
            this.chkIsActivated.UseVisualStyleBackColor = true;
            this.chkIsActivated.Visible = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(220, 194);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(131, 22);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.Visible = false;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDate.Location = new System.Drawing.Point(150, 198);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 114;
            this.lblEndDate.Text = "End Date :";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEndDate.Visible = false;
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(382, 54);
            this.pnl_Toolstrip.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_btnSave,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(382, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_btnSave
            // 
            this.tsb_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnSave.Image")));
            this.tsb_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnSave.Name = "tsb_btnSave";
            this.tsb_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tsb_btnSave.Tag = "OK";
            this.tsb_btnSave.Text = "&Save";
            this.tsb_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnSave.ToolTipText = "Save";
            this.tsb_btnSave.Click += new System.EventHandler(this.tsb_Save_Click);
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
            // frmSetupChargesTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(382, 172);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupChargesTray";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Charge Tray";
            this.Load += new System.EventHandler(this.frmSetupChargesTray_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupChargesTray_FormClosing);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numnoofdays)).EndInit();
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartdate;
        internal System.Windows.Forms.Label lblStDate;
        internal System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.CheckBox chkisdefault;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numnoofdays;
        private System.Windows.Forms.Label lbldesc;
        private System.Windows.Forms.Label lblnoofdays;
        private System.Windows.Forms.TextBox txtdescription;
        private System.Windows.Forms.Panel pnl_Toolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
     //   internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_btnSave;
        private System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.CheckBox chkIsActivated;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
    }
}
