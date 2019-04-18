namespace gloAppointmentBook
{
    partial class frmSetupResource
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
                try
                {
                    if (dlgOpenFile != null)
                    {
                         
                        dlgOpenFile.Dispose();
                        dlgOpenFile = null;
                    }
                }
                catch
                {
                }
               
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupResource));
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.cmbResourceType = new System.Windows.Forms.ComboBox();
            this.lbl_ResourceType = new System.Windows.Forms.Label();
            this.pnlResource = new System.Windows.Forms.Panel();
            this.chkTurnOffReminders = new System.Windows.Forms.CheckBox();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lbl_pnlResourceBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlResourceTopBrd = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlToolstrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.pnlResource.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolstrip.Controls.Add(this.tls_SetupResource);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(414, 54);
            this.pnlToolstrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(414, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            this.tls_SetupResource.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SetupResource_ItemClicked);
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
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "Sa&ve&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            this.tlsp_btnOK.Click += new System.EventHandler(this.tlsp_btnOK_Click);
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // cmbResourceType
            // 
            this.cmbResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResourceType.ForeColor = System.Drawing.Color.Black;
            this.cmbResourceType.FormattingEnabled = true;
            this.cmbResourceType.Items.AddRange(new object[] {
            "General"});
            this.cmbResourceType.Location = new System.Drawing.Point(336, 15);
            this.cmbResourceType.Name = "cmbResourceType";
            this.cmbResourceType.Size = new System.Drawing.Size(61, 22);
            this.cmbResourceType.TabIndex = 2;
            this.cmbResourceType.Visible = false;
            this.cmbResourceType.SelectedIndexChanged += new System.EventHandler(this.cmbResourceType_SelectedIndexChanged);
            // 
            // lbl_ResourceType
            // 
            this.lbl_ResourceType.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ResourceType.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_ResourceType.Location = new System.Drawing.Point(222, 15);
            this.lbl_ResourceType.Name = "lbl_ResourceType";
            this.lbl_ResourceType.Size = new System.Drawing.Size(110, 23);
            this.lbl_ResourceType.TabIndex = 2;
            this.lbl_ResourceType.Text = "Resource Type :";
            this.lbl_ResourceType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_ResourceType.Visible = false;
            // 
            // pnlResource
            // 
            this.pnlResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlResource.Controls.Add(this.chkTurnOffReminders);
            this.pnlResource.Controls.Add(this.cmbUsers);
            this.pnlResource.Controls.Add(this.lblUser);
            this.pnlResource.Controls.Add(this.txtDescription);
            this.pnlResource.Controls.Add(this.lblDescription);
            this.pnlResource.Controls.Add(this.lblCode);
            this.pnlResource.Controls.Add(this.txtCode);
            this.pnlResource.Controls.Add(this.lbl_pnlResourceBottomBrd);
            this.pnlResource.Controls.Add(this.lbl_pnlResourceLeftBrd);
            this.pnlResource.Controls.Add(this.lbl_pnlResourceRightBrd);
            this.pnlResource.Controls.Add(this.lbl_pnlResourceTopBrd);
            this.pnlResource.Controls.Add(this.cmbResourceType);
            this.pnlResource.Controls.Add(this.lbl_ResourceType);
            this.pnlResource.Controls.Add(this.label19);
            this.pnlResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResource.Location = new System.Drawing.Point(0, 54);
            this.pnlResource.Name = "pnlResource";
            this.pnlResource.Padding = new System.Windows.Forms.Padding(3);
            this.pnlResource.Size = new System.Drawing.Size(414, 134);
            this.pnlResource.TabIndex = 0;
            // 
            // chkTurnOffReminders
            // 
            this.chkTurnOffReminders.AutoSize = true;
            this.chkTurnOffReminders.Location = new System.Drawing.Point(99, 102);
            this.chkTurnOffReminders.Name = "chkTurnOffReminders";
            this.chkTurnOffReminders.Size = new System.Drawing.Size(131, 18);
            this.chkTurnOffReminders.TabIndex = 5;
            this.chkTurnOffReminders.Text = "Turn off Reminders";
            this.chkTurnOffReminders.UseVisualStyleBackColor = true;
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsers.ForeColor = System.Drawing.Color.Black;
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(99, 73);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(300, 22);
            this.cmbUsers.TabIndex = 4;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(55, 77);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 100;
            this.lblUser.Text = "User :";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(99, 44);
            this.txtDescription.MaxLength = 99;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(300, 22);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(19, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 14);
            this.lblDescription.TabIndex = 98;
            this.lblDescription.Text = "Description :";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(51, 19);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(43, 14);
            this.lblCode.TabIndex = 95;
            this.lblCode.Text = "Code :";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(99, 15);
            this.txtCode.MaxLength = 100;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(121, 22);
            this.txtCode.TabIndex = 1;
            // 
            // lbl_pnlResourceBottomBrd
            // 
            this.lbl_pnlResourceBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlResourceBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlResourceBottomBrd.Location = new System.Drawing.Point(4, 130);
            this.lbl_pnlResourceBottomBrd.Name = "lbl_pnlResourceBottomBrd";
            this.lbl_pnlResourceBottomBrd.Size = new System.Drawing.Size(406, 1);
            this.lbl_pnlResourceBottomBrd.TabIndex = 105;
            this.lbl_pnlResourceBottomBrd.Text = "label2";
            // 
            // lbl_pnlResourceLeftBrd
            // 
            this.lbl_pnlResourceLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlResourceLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlResourceLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlResourceLeftBrd.Name = "lbl_pnlResourceLeftBrd";
            this.lbl_pnlResourceLeftBrd.Size = new System.Drawing.Size(1, 127);
            this.lbl_pnlResourceLeftBrd.TabIndex = 104;
            this.lbl_pnlResourceLeftBrd.Text = "label4";
            // 
            // lbl_pnlResourceRightBrd
            // 
            this.lbl_pnlResourceRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlResourceRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlResourceRightBrd.Location = new System.Drawing.Point(410, 4);
            this.lbl_pnlResourceRightBrd.Name = "lbl_pnlResourceRightBrd";
            this.lbl_pnlResourceRightBrd.Size = new System.Drawing.Size(1, 127);
            this.lbl_pnlResourceRightBrd.TabIndex = 103;
            this.lbl_pnlResourceRightBrd.Text = "label3";
            // 
            // lbl_pnlResourceTopBrd
            // 
            this.lbl_pnlResourceTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlResourceTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlResourceTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlResourceTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlResourceTopBrd.Name = "lbl_pnlResourceTopBrd";
            this.lbl_pnlResourceTopBrd.Size = new System.Drawing.Size(408, 1);
            this.lbl_pnlResourceTopBrd.TabIndex = 102;
            this.lbl_pnlResourceTopBrd.Text = "label1";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(7, 48);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 106;
            this.label19.Text = "*";
            // 
            // frmSetupResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(414, 188);
            this.Controls.Add(this.pnlResource);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupResource";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Resource";
            this.Load += new System.EventHandler(this.frmSetupResource_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlResource.ResumeLayout(false);
            this.pnlResource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbResourceType;
        private System.Windows.Forms.Label lbl_ResourceType;
        internal System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Panel pnlToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tlsp_btnOK;
        private System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Panel pnlResource;
        private System.Windows.Forms.ComboBox cmbUsers;
        internal System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lbl_pnlResourceBottomBrd;
        private System.Windows.Forms.Label lbl_pnlResourceLeftBrd;
        private System.Windows.Forms.Label lbl_pnlResourceRightBrd;
        private System.Windows.Forms.Label lbl_pnlResourceTopBrd;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
        private System.Windows.Forms.CheckBox chkTurnOffReminders;
    }
}
