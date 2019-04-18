namespace gloReminder
{
    partial class frmSetupReminder
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
            System.Windows.Forms.DateTimePicker[] cntdtControls =   { dtpStartDate, dtpStartTime, dtpEndDate, dtpEndTime };
            System.Windows.Forms.Control[] cntControls = { dtpStartDate, dtpStartTime, dtpEndDate, dtpEndTime };
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupReminder));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlText = new System.Windows.Forms.Panel();
            this.cmbReminderIntervel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnltlsStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_Top);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(462, 54);
            this.pnltlsStrip.TabIndex = 6;
            // 
            // tls_Top
            // 
            this.tls_Top.BackColor = System.Drawing.Color.Transparent;
            this.tls_Top.BackgroundImage = global::gloReminder.Properties.Resources.Img_Toolstrip;
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnCancel});
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(462, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            this.tls_Top.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Top_ItemClicked);
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(40, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Save";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.cmbReminderIntervel);
            this.pnlText.Controls.Add(this.label7);
            this.pnlText.Controls.Add(this.label6);
            this.pnlText.Controls.Add(this.label5);
            this.pnlText.Controls.Add(this.dtpStartTime);
            this.pnlText.Controls.Add(this.dtpEndTime);
            this.pnlText.Controls.Add(this.dtpStartDate);
            this.pnlText.Controls.Add(this.dtpEndDate);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.txtLocation);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label8);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label4);
            this.pnlText.Controls.Add(this.txtDescription);
            this.pnlText.Controls.Add(this.lblDescription);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 54);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(462, 189);
            this.pnlText.TabIndex = 7;
            // 
            // cmbReminderIntervel
            // 
            this.cmbReminderIntervel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReminderIntervel.ForeColor = System.Drawing.Color.Black;
            this.cmbReminderIntervel.FormattingEnabled = true;
            this.cmbReminderIntervel.Items.AddRange(new object[] {
            "0 Minutes",
            "5 Minutes",
            "10 Minutes",
            "15 Minutes",
            "30 Minutes"});
            this.cmbReminderIntervel.Location = new System.Drawing.Point(312, 73);
            this.cmbReminderIntervel.Name = "cmbReminderIntervel";
            this.cmbReminderIntervel.Size = new System.Drawing.Size(117, 22);
            this.cmbReminderIntervel.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Reminder :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "End Time :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "Start Time :";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartTime.CalendarTitleBackColor = System.Drawing.Color.Brown;
            this.dtpStartTime.CalendarTitleForeColor = System.Drawing.Color.Orange;
            this.dtpStartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartTime.CustomFormat = "hh:mm tt";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(219, 109);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(100, 22);
            this.dtpStartTime.TabIndex = 15;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndTime.CalendarTitleBackColor = System.Drawing.Color.Brown;
            this.dtpEndTime.CalendarTitleForeColor = System.Drawing.Color.Orange;
            this.dtpEndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndTime.CustomFormat = "hh:mm tt";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(219, 145);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(100, 22);
            this.dtpEndTime.TabIndex = 14;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(96, 109);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(100, 22);
            this.dtpStartDate.TabIndex = 13;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(96, 145);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 22);
            this.dtpEndDate.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Location :";
            // 
            // txtLocation
            // 
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(96, 73);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(136, 22);
            this.txtLocation.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(458, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 181);
            this.label2.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 181);
            this.label8.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 1);
            this.label1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(456, 1);
            this.label4.TabIndex = 6;
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(96, 18);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(333, 41);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(18, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 14);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description :";
            // 
            // frmSetupReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(462, 243);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupReminder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Reminder";
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbReminderIntervel;
        private System.Windows.Forms.Label label7;
    }
}