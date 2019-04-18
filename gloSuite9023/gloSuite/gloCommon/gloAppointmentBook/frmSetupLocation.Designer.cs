namespace gloAppointmentBook
{
    partial class frmSetupLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupLocation));
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_Location = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.lblAppBlockType = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.lbl_L = new System.Windows.Forms.Label();
            this.lbl_T = new System.Windows.Forms.Label();
            this.lbl_B = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAddresControl = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.chkTurnOffReminders = new System.Windows.Forms.CheckBox();
            this.tlsp_Location.SuspendLayout();
            this.pnl_ToolStrip.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "Sa&ve&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            // 
            // tlsp_Location
            // 
            this.tlsp_Location.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_Location.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Location.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Location.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Location.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_Location.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Location.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Location.Name = "tlsp_Location";
            this.tlsp_Location.Size = new System.Drawing.Size(429, 53);
            this.tlsp_Location.TabIndex = 0;
            this.tlsp_Location.TabStop = true;
            this.tlsp_Location.Text = "ToolStrip1";
            this.tlsp_Location.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "&Save";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnCancel.ToolTipText = "Close";
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.tlsp_Location);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(429, 53);
            this.pnl_ToolStrip.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(22, 24);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 109;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAppBlockType
            // 
            this.lblAppBlockType.AutoSize = true;
            this.lblAppBlockType.BackColor = System.Drawing.Color.Transparent;
            this.lblAppBlockType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppBlockType.Location = new System.Drawing.Point(35, 24);
            this.lblAppBlockType.Name = "lblAppBlockType";
            this.lblAppBlockType.Size = new System.Drawing.Size(61, 14);
            this.lblAppBlockType.TabIndex = 6;
            this.lblAppBlockType.Text = "Location :";
            this.lblAppBlockType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(99, 20);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(237, 22);
            this.txtLocation.TabIndex = 0;
            this.txtLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocation_KeyPress);
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDefault.Location = new System.Drawing.Point(343, 23);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(65, 18);
            this.chkDefault.TabIndex = 1;
            this.chkDefault.Text = "Default";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // lbl_L
            // 
            this.lbl_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_L.Location = new System.Drawing.Point(3, 3);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(1, 211);
            this.lbl_L.TabIndex = 34;
            // 
            // lbl_T
            // 
            this.lbl_T.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_T.Location = new System.Drawing.Point(4, 3);
            this.lbl_T.Name = "lbl_T";
            this.lbl_T.Size = new System.Drawing.Size(422, 1);
            this.lbl_T.TabIndex = 35;
            // 
            // lbl_B
            // 
            this.lbl_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_B.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_B.Location = new System.Drawing.Point(4, 213);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(422, 1);
            this.lbl_B.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(425, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 209);
            this.label1.TabIndex = 110;
            // 
            // pnlAddresControl
            // 
            this.pnlAddresControl.Location = new System.Drawing.Point(17, 44);
            this.pnlAddresControl.Name = "pnlAddresControl";
            this.pnlAddresControl.Size = new System.Drawing.Size(396, 136);
            this.pnlAddresControl.TabIndex = 111;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.chkTurnOffReminders);
            this.pnlSearch.Controls.Add(this.pnlAddresControl);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.lbl_B);
            this.pnlSearch.Controls.Add(this.lbl_T);
            this.pnlSearch.Controls.Add(this.lbl_L);
            this.pnlSearch.Controls.Add(this.chkDefault);
            this.pnlSearch.Controls.Add(this.txtLocation);
            this.pnlSearch.Controls.Add(this.lblAppBlockType);
            this.pnlSearch.Controls.Add(this.label19);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(429, 217);
            this.pnlSearch.TabIndex = 0;
            // 
            // chkTurnOffReminders
            // 
            this.chkTurnOffReminders.AutoSize = true;
            this.chkTurnOffReminders.Location = new System.Drawing.Point(99, 182);
            this.chkTurnOffReminders.Name = "chkTurnOffReminders";
            this.chkTurnOffReminders.Size = new System.Drawing.Size(131, 18);
            this.chkTurnOffReminders.TabIndex = 112;
            this.chkTurnOffReminders.Text = "Turn off Reminders";
            this.chkTurnOffReminders.UseVisualStyleBackColor = true;
            // 
            // frmSetupLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(429, 270);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupLocation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Location";
            this.Load += new System.EventHandler(this.frmSetupLocation_Load);
            this.tlsp_Location.ResumeLayout(false);
            this.tlsp_Location.PerformLayout();
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStripButton tlsp_btnOK;
        internal gloGlobal.gloToolStripIgnoreFocus tlsp_Location;
        internal System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Panel pnl_ToolStrip;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblAppBlockType;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.Label lbl_T;
        private System.Windows.Forms.Label lbl_B;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlAddresControl;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.CheckBox chkTurnOffReminders;
    }
}