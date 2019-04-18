namespace gloAppointmentBook
{
    partial class frmSetupClinicDays
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupClinicDays));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_ClinicDays = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnOk = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lbl_B = new System.Windows.Forms.Label();
            this.lbl_T = new System.Windows.Forms.Label();
            this.lbl_R = new System.Windows.Forms.Label();
            this.lbl_L = new System.Windows.Forms.Label();
            this.gbOffDays = new System.Windows.Forms.GroupBox();
            this.chkOffSunday = new System.Windows.Forms.CheckBox();
            this.chkOffSaturday = new System.Windows.Forms.CheckBox();
            this.chkOffFriday = new System.Windows.Forms.CheckBox();
            this.chkOffThursday = new System.Windows.Forms.CheckBox();
            this.chkOffWednesday = new System.Windows.Forms.CheckBox();
            this.chkOffTuesday = new System.Windows.Forms.CheckBox();
            this.chkOFF = new System.Windows.Forms.CheckBox();
            this.gbWorkingDays = new System.Windows.Forms.GroupBox();
            this.chkWorkSunday = new System.Windows.Forms.CheckBox();
            this.chkWorkSaturday = new System.Windows.Forms.CheckBox();
            this.chkWorkFriday = new System.Windows.Forms.CheckBox();
            this.chkWorkThursday = new System.Windows.Forms.CheckBox();
            this.chkWorkWednesday = new System.Windows.Forms.CheckBox();
            this.chkWorkTuesday = new System.Windows.Forms.CheckBox();
            this.chkWork = new System.Windows.Forms.CheckBox();
            this.pnlToolStrip.SuspendLayout();
            this.tlsp_ClinicDays.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.gbOffDays.SuspendLayout();
            this.gbWorkingDays.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolStrip.Controls.Add(this.tlsp_ClinicDays);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(364, 53);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tlsp_ClinicDays
            // 
            this.tlsp_ClinicDays.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_ClinicDays.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_ClinicDays.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_ClinicDays.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsp_ClinicDays.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_ClinicDays.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.tlsp_btnOk,
            this.tlsp_btnCancel});
            this.tlsp_ClinicDays.Location = new System.Drawing.Point(0, 0);
            this.tlsp_ClinicDays.Name = "tlsp_ClinicDays";
            this.tlsp_ClinicDays.Size = new System.Drawing.Size(364, 53);
            this.tlsp_ClinicDays.TabIndex = 0;
            this.tlsp_ClinicDays.Text = "toolStrip1";
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "Sa&ve";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // tlsp_btnOk
            // 
            this.tlsp_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOk.Image")));
            this.tlsp_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOk.Name = "tlsp_btnOk";
            this.tlsp_btnOk.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOk.Text = "&Save&&Cls";
            this.tlsp_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOk.ToolTipText = "Save and Close";
            this.tlsp_btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnCancel.ToolTipText = "Close";
            this.tlsp_btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.lbl_B);
            this.pnlMain.Controls.Add(this.lbl_T);
            this.pnlMain.Controls.Add(this.lbl_R);
            this.pnlMain.Controls.Add(this.lbl_L);
            this.pnlMain.Controls.Add(this.gbOffDays);
            this.pnlMain.Controls.Add(this.gbWorkingDays);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(364, 334);
            this.pnlMain.TabIndex = 0;
            // 
            // lbl_B
            // 
            this.lbl_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_B.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_B.Location = new System.Drawing.Point(4, 330);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(356, 1);
            this.lbl_B.TabIndex = 34;
            // 
            // lbl_T
            // 
            this.lbl_T.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_T.Location = new System.Drawing.Point(4, 3);
            this.lbl_T.Name = "lbl_T";
            this.lbl_T.Size = new System.Drawing.Size(356, 1);
            this.lbl_T.TabIndex = 33;
            // 
            // lbl_R
            // 
            this.lbl_R.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_R.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_R.Location = new System.Drawing.Point(360, 3);
            this.lbl_R.Name = "lbl_R";
            this.lbl_R.Size = new System.Drawing.Size(1, 328);
            this.lbl_R.TabIndex = 32;
            // 
            // lbl_L
            // 
            this.lbl_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_L.Location = new System.Drawing.Point(3, 3);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(1, 328);
            this.lbl_L.TabIndex = 31;
            // 
            // gbOffDays
            // 
            this.gbOffDays.BackColor = System.Drawing.Color.Transparent;
            this.gbOffDays.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbOffDays.Controls.Add(this.chkOffSunday);
            this.gbOffDays.Controls.Add(this.chkOffSaturday);
            this.gbOffDays.Controls.Add(this.chkOffFriday);
            this.gbOffDays.Controls.Add(this.chkOffThursday);
            this.gbOffDays.Controls.Add(this.chkOffWednesday);
            this.gbOffDays.Controls.Add(this.chkOffTuesday);
            this.gbOffDays.Controls.Add(this.chkOFF);
            this.gbOffDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOffDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbOffDays.Location = new System.Drawing.Point(190, 15);
            this.gbOffDays.Name = "gbOffDays";
            this.gbOffDays.Size = new System.Drawing.Size(146, 302);
            this.gbOffDays.TabIndex = 1;
            this.gbOffDays.TabStop = false;
            this.gbOffDays.Text = "OFF Days";
            // 
            // chkOffSunday
            // 
            this.chkOffSunday.AutoSize = true;
            this.chkOffSunday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffSunday.Location = new System.Drawing.Point(17, 267);
            this.chkOffSunday.Name = "chkOffSunday";
            this.chkOffSunday.Size = new System.Drawing.Size(66, 18);
            this.chkOffSunday.TabIndex = 13;
            this.chkOffSunday.Tag = "7";
            this.chkOffSunday.Text = "Sunday";
            this.chkOffSunday.UseVisualStyleBackColor = true;
            this.chkOffSunday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOffSaturday
            // 
            this.chkOffSaturday.AutoSize = true;
            this.chkOffSaturday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffSaturday.Location = new System.Drawing.Point(17, 229);
            this.chkOffSaturday.Name = "chkOffSaturday";
            this.chkOffSaturday.Size = new System.Drawing.Size(74, 18);
            this.chkOffSaturday.TabIndex = 12;
            this.chkOffSaturday.Tag = "6";
            this.chkOffSaturday.Text = "Saturday";
            this.chkOffSaturday.UseVisualStyleBackColor = true;
            this.chkOffSaturday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOffFriday
            // 
            this.chkOffFriday.AutoSize = true;
            this.chkOffFriday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffFriday.Location = new System.Drawing.Point(17, 192);
            this.chkOffFriday.Name = "chkOffFriday";
            this.chkOffFriday.Size = new System.Drawing.Size(57, 18);
            this.chkOffFriday.TabIndex = 11;
            this.chkOffFriday.Tag = "5";
            this.chkOffFriday.Text = "Friday";
            this.chkOffFriday.UseVisualStyleBackColor = true;
            this.chkOffFriday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOffThursday
            // 
            this.chkOffThursday.AutoSize = true;
            this.chkOffThursday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffThursday.Location = new System.Drawing.Point(17, 154);
            this.chkOffThursday.Name = "chkOffThursday";
            this.chkOffThursday.Size = new System.Drawing.Size(76, 18);
            this.chkOffThursday.TabIndex = 10;
            this.chkOffThursday.Tag = "4";
            this.chkOffThursday.Text = "Thursday";
            this.chkOffThursday.UseVisualStyleBackColor = true;
            this.chkOffThursday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOffWednesday
            // 
            this.chkOffWednesday.AutoSize = true;
            this.chkOffWednesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffWednesday.Location = new System.Drawing.Point(17, 116);
            this.chkOffWednesday.Name = "chkOffWednesday";
            this.chkOffWednesday.Size = new System.Drawing.Size(90, 18);
            this.chkOffWednesday.TabIndex = 9;
            this.chkOffWednesday.Tag = "3";
            this.chkOffWednesday.Text = "Wednesday";
            this.chkOffWednesday.UseVisualStyleBackColor = true;
            this.chkOffWednesday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOffTuesday
            // 
            this.chkOffTuesday.AutoSize = true;
            this.chkOffTuesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOffTuesday.Location = new System.Drawing.Point(17, 79);
            this.chkOffTuesday.Name = "chkOffTuesday";
            this.chkOffTuesday.Size = new System.Drawing.Size(72, 18);
            this.chkOffTuesday.TabIndex = 8;
            this.chkOffTuesday.Tag = "2";
            this.chkOffTuesday.Text = "Tuesday";
            this.chkOffTuesday.UseVisualStyleBackColor = true;
            this.chkOffTuesday.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // chkOFF
            // 
            this.chkOFF.AutoSize = true;
            this.chkOFF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOFF.Location = new System.Drawing.Point(17, 41);
            this.chkOFF.Name = "chkOFF";
            this.chkOFF.Size = new System.Drawing.Size(68, 18);
            this.chkOFF.TabIndex = 7;
            this.chkOFF.Tag = "1";
            this.chkOFF.Text = "Monday";
            this.chkOFF.UseVisualStyleBackColor = true;
            this.chkOFF.CheckedChanged += new System.EventHandler(this.chkOFF_CheckedChanged);
            // 
            // gbWorkingDays
            // 
            this.gbWorkingDays.BackColor = System.Drawing.Color.Transparent;
            this.gbWorkingDays.Controls.Add(this.chkWorkSunday);
            this.gbWorkingDays.Controls.Add(this.chkWorkSaturday);
            this.gbWorkingDays.Controls.Add(this.chkWorkFriday);
            this.gbWorkingDays.Controls.Add(this.chkWorkThursday);
            this.gbWorkingDays.Controls.Add(this.chkWorkWednesday);
            this.gbWorkingDays.Controls.Add(this.chkWorkTuesday);
            this.gbWorkingDays.Controls.Add(this.chkWork);
            this.gbWorkingDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbWorkingDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbWorkingDays.Location = new System.Drawing.Point(20, 15);
            this.gbWorkingDays.Name = "gbWorkingDays";
            this.gbWorkingDays.Size = new System.Drawing.Size(152, 300);
            this.gbWorkingDays.TabIndex = 0;
            this.gbWorkingDays.TabStop = false;
            this.gbWorkingDays.Text = "Working Days";
            // 
            // chkWorkSunday
            // 
            this.chkWorkSunday.AutoSize = true;
            this.chkWorkSunday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkSunday.Location = new System.Drawing.Point(19, 267);
            this.chkWorkSunday.Name = "chkWorkSunday";
            this.chkWorkSunday.Size = new System.Drawing.Size(66, 18);
            this.chkWorkSunday.TabIndex = 6;
            this.chkWorkSunday.Tag = "7";
            this.chkWorkSunday.Text = "Sunday";
            this.chkWorkSunday.UseVisualStyleBackColor = true;
            this.chkWorkSunday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWorkSaturday
            // 
            this.chkWorkSaturday.AutoSize = true;
            this.chkWorkSaturday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkSaturday.Location = new System.Drawing.Point(19, 229);
            this.chkWorkSaturday.Name = "chkWorkSaturday";
            this.chkWorkSaturday.Size = new System.Drawing.Size(74, 18);
            this.chkWorkSaturday.TabIndex = 5;
            this.chkWorkSaturday.Tag = "6";
            this.chkWorkSaturday.Text = "Saturday";
            this.chkWorkSaturday.UseVisualStyleBackColor = true;
            this.chkWorkSaturday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWorkFriday
            // 
            this.chkWorkFriday.AutoSize = true;
            this.chkWorkFriday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkFriday.Location = new System.Drawing.Point(19, 192);
            this.chkWorkFriday.Name = "chkWorkFriday";
            this.chkWorkFriday.Size = new System.Drawing.Size(57, 18);
            this.chkWorkFriday.TabIndex = 4;
            this.chkWorkFriday.Tag = "5";
            this.chkWorkFriday.Text = "Friday";
            this.chkWorkFriday.UseVisualStyleBackColor = true;
            this.chkWorkFriday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWorkThursday
            // 
            this.chkWorkThursday.AutoSize = true;
            this.chkWorkThursday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkThursday.Location = new System.Drawing.Point(19, 154);
            this.chkWorkThursday.Name = "chkWorkThursday";
            this.chkWorkThursday.Size = new System.Drawing.Size(76, 18);
            this.chkWorkThursday.TabIndex = 3;
            this.chkWorkThursday.Tag = "4";
            this.chkWorkThursday.Text = "Thursday";
            this.chkWorkThursday.UseVisualStyleBackColor = true;
            this.chkWorkThursday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWorkWednesday
            // 
            this.chkWorkWednesday.AutoSize = true;
            this.chkWorkWednesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkWednesday.Location = new System.Drawing.Point(19, 116);
            this.chkWorkWednesday.Name = "chkWorkWednesday";
            this.chkWorkWednesday.Size = new System.Drawing.Size(90, 18);
            this.chkWorkWednesday.TabIndex = 2;
            this.chkWorkWednesday.Tag = "3";
            this.chkWorkWednesday.Text = "Wednesday";
            this.chkWorkWednesday.UseVisualStyleBackColor = true;
            this.chkWorkWednesday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWorkTuesday
            // 
            this.chkWorkTuesday.AutoSize = true;
            this.chkWorkTuesday.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWorkTuesday.Location = new System.Drawing.Point(19, 79);
            this.chkWorkTuesday.Name = "chkWorkTuesday";
            this.chkWorkTuesday.Size = new System.Drawing.Size(72, 18);
            this.chkWorkTuesday.TabIndex = 1;
            this.chkWorkTuesday.Tag = "2";
            this.chkWorkTuesday.Text = "Tuesday";
            this.chkWorkTuesday.UseVisualStyleBackColor = true;
            this.chkWorkTuesday.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // chkWork
            // 
            this.chkWork.AutoSize = true;
            this.chkWork.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWork.Location = new System.Drawing.Point(19, 41);
            this.chkWork.Name = "chkWork";
            this.chkWork.Size = new System.Drawing.Size(68, 18);
            this.chkWork.TabIndex = 0;
            this.chkWork.Tag = "1";
            this.chkWork.Text = "Monday";
            this.chkWork.UseVisualStyleBackColor = true;
            this.chkWork.CheckedChanged += new System.EventHandler(this.chkWork_CheckedChanged);
            // 
            // frmSetupClinicDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(364, 387);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupClinicDays";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clinic Days";
            this.Load += new System.EventHandler(this.frmSetupClinicDays_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tlsp_ClinicDays.ResumeLayout(false);
            this.tlsp_ClinicDays.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.gbOffDays.ResumeLayout(false);
            this.gbOffDays.PerformLayout();
            this.gbWorkingDays.ResumeLayout(false);
            this.gbWorkingDays.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox gbOffDays;
        private System.Windows.Forms.CheckBox chkOffSunday;
        private System.Windows.Forms.CheckBox chkOffSaturday;
        private System.Windows.Forms.CheckBox chkOffFriday;
        private System.Windows.Forms.CheckBox chkOffThursday;
        private System.Windows.Forms.CheckBox chkOffWednesday;
        private System.Windows.Forms.CheckBox chkOffTuesday;
        private System.Windows.Forms.CheckBox chkOFF;
        private System.Windows.Forms.GroupBox gbWorkingDays;
        private System.Windows.Forms.CheckBox chkWorkSunday;
        private System.Windows.Forms.CheckBox chkWorkSaturday;
        private System.Windows.Forms.CheckBox chkWorkFriday;
        private System.Windows.Forms.CheckBox chkWorkThursday;
        private System.Windows.Forms.CheckBox chkWorkWednesday;
        private System.Windows.Forms.CheckBox chkWorkTuesday;
        private System.Windows.Forms.CheckBox chkWork;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_ClinicDays;
        private System.Windows.Forms.ToolStripButton tlsp_btnOk;
        private System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.Label lbl_R;
        private System.Windows.Forms.Label lbl_T;
        private System.Windows.Forms.Label lbl_B;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
    }
}