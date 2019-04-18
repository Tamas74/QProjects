namespace gloBilling
{
    partial class frmPaymentTransferInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentTransferInfo));
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.cmbAdjustmentCode = new System.Windows.Forms.ComboBox();
            this.lblAdjustmentCode = new System.Windows.Forms.Label();
            this.chkBadDebtStatus = new System.Windows.Forms.CheckBox();
            this.chkBadDebtFollowup = new System.Windows.Forms.CheckBox();
            this.lblOnSuccess = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_SaveAndCloseMod,
            this.tls_CloseMod});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(412, 53);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tls_SaveAndCloseMod
            // 
            this.tls_SaveAndCloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveAndCloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveAndCloseMod.Image")));
            this.tls_SaveAndCloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveAndCloseMod.Name = "tls_SaveAndCloseMod";
            this.tls_SaveAndCloseMod.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveAndCloseMod.Text = "Sa&ve&&Cls";
            this.tls_SaveAndCloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveAndCloseMod.ToolTipText = "Save and Close";
            this.tls_SaveAndCloseMod.Click += new System.EventHandler(this.tls_SaveAndCloseMod_Click);
            // 
            // tls_CloseMod
            // 
            this.tls_CloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_CloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_CloseMod.Image")));
            this.tls_CloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_CloseMod.Name = "tls_CloseMod";
            this.tls_CloseMod.Size = new System.Drawing.Size(43, 50);
            this.tls_CloseMod.Text = "&Close";
            this.tls_CloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_CloseMod.Click += new System.EventHandler(this.tls_CloseMod_Click);
            // 
            // cmbAdjustmentCode
            // 
            this.cmbAdjustmentCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdjustmentCode.FormattingEnabled = true;
            this.cmbAdjustmentCode.Location = new System.Drawing.Point(155, 72);
            this.cmbAdjustmentCode.Name = "cmbAdjustmentCode";
            this.cmbAdjustmentCode.Size = new System.Drawing.Size(224, 22);
            this.cmbAdjustmentCode.TabIndex = 3;
            // 
            // lblAdjustmentCode
            // 
            this.lblAdjustmentCode.AutoSize = true;
            this.lblAdjustmentCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAdjustmentCode.Location = new System.Drawing.Point(40, 76);
            this.lblAdjustmentCode.Name = "lblAdjustmentCode";
            this.lblAdjustmentCode.Size = new System.Drawing.Size(111, 14);
            this.lblAdjustmentCode.TabIndex = 61;
            this.lblAdjustmentCode.Text = "Adjustment Code :";
            // 
            // chkBadDebtStatus
            // 
            this.chkBadDebtStatus.AutoSize = true;
            this.chkBadDebtStatus.Location = new System.Drawing.Point(155, 129);
            this.chkBadDebtStatus.Name = "chkBadDebtStatus";
            this.chkBadDebtStatus.Size = new System.Drawing.Size(164, 18);
            this.chkBadDebtStatus.TabIndex = 63;
            this.chkBadDebtStatus.Text = "Remove Bad Debt Status";
            this.chkBadDebtStatus.UseVisualStyleBackColor = true;
            // 
            // chkBadDebtFollowup
            // 
            this.chkBadDebtFollowup.AutoSize = true;
            this.chkBadDebtFollowup.Location = new System.Drawing.Point(155, 105);
            this.chkBadDebtFollowup.Name = "chkBadDebtFollowup";
            this.chkBadDebtFollowup.Size = new System.Drawing.Size(181, 18);
            this.chkBadDebtFollowup.TabIndex = 4;
            this.chkBadDebtFollowup.Text = "Remove Bad Debt Follow-up";
            this.chkBadDebtFollowup.UseVisualStyleBackColor = true;
            // 
            // lblOnSuccess
            // 
            this.lblOnSuccess.AutoEllipsis = true;
            this.lblOnSuccess.AutoSize = true;
            this.lblOnSuccess.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnSuccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOnSuccess.Location = new System.Drawing.Point(73, 105);
            this.lblOnSuccess.Name = "lblOnSuccess";
            this.lblOnSuccess.Size = new System.Drawing.Size(78, 14);
            this.lblOnSuccess.TabIndex = 62;
            this.lblOnSuccess.Text = "On Success :";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.mskCloseDate);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.cmbAdjustmentCode);
            this.panel3.Controls.Add(this.lblAdjustmentCode);
            this.panel3.Controls.Add(this.label48);
            this.panel3.Controls.Add(this.chkBadDebtStatus);
            this.panel3.Controls.Add(this.cmbPaymentTray);
            this.panel3.Controls.Add(this.chkBadDebtFollowup);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblOnSuccess);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(412, 159);
            this.panel3.TabIndex = 64;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Location = new System.Drawing.Point(155, 10);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(90, 22);
            this.mskCloseDate.TabIndex = 1;
            this.mskCloseDate.Tag = "Close Date";
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            this.mskCloseDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCloseDate_MouseClick);
            this.mskCloseDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskCloseDate_Validating);
            // 
            // label48
            // 
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(26, 14);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(125, 14);
            this.label48.TabIndex = 61;
            this.label48.Text = "Payment Close Date :";
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Location = new System.Drawing.Point(155, 41);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(224, 22);
            this.cmbPaymentTray.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(60, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 61;
            this.label1.Text = "Payment Tray :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(408, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 152);
            this.label13.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 152);
            this.label14.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(406, 1);
            this.label16.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 54);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(4, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(404, 1);
            this.label2.TabIndex = 64;
            // 
            // frmPaymentTransferInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(412, 213);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentTransferInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Write-off";
            this.Load += new System.EventHandler(this.frmPaymentTransferInfo_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
        private System.Windows.Forms.ToolStripButton tls_SaveAndCloseMod;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkBadDebtFollowup;
        private System.Windows.Forms.ComboBox cmbAdjustmentCode;
        private System.Windows.Forms.ComboBox cmbPaymentTray;
        private System.Windows.Forms.Label lblAdjustmentCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBadDebtStatus;
        private System.Windows.Forms.Label lblOnSuccess;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
    }
}