namespace gloCardScanning
{
    partial class frmSetupCriteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupCriteria));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUpdate = new System.Windows.Forms.RadioButton();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSSN = new System.Windows.Forms.CheckBox();
            this.chkMiddleName = new System.Windows.Forms.CheckBox();
            this.chkLastName = new System.Windows.Forms.CheckBox();
            this.chkFirstName = new System.Windows.Forms.CheckBox();
            this.chkPhoto = new System.Windows.Forms.CheckBox();
            this.chkDOB = new System.Windows.Forms.CheckBox();
            this.chkadress = new System.Windows.Forms.CheckBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(417, 54);
            this.pnl_tlsp_Top.TabIndex = 1;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOK,
            this.btnClose});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(417, 53);
            this.tstrip.TabIndex = 0;
            this.tstrip.Text = "ToolStrip1";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 50);
            this.btnOK.Text = "&Save&&Cls";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Save and Close";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 50);
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.groupBox1);
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.groupBox2);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 54);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(417, 187);
            this.Panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUpdate);
            this.groupBox1.Controls.Add(this.rbNew);
            this.groupBox1.Location = new System.Drawing.Point(26, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 38);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // rbUpdate
            // 
            this.rbUpdate.AutoSize = true;
            this.rbUpdate.Checked = true;
            this.rbUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUpdate.Location = new System.Drawing.Point(3, 12);
            this.rbUpdate.Name = "rbUpdate";
            this.rbUpdate.Size = new System.Drawing.Size(69, 18);
            this.rbUpdate.TabIndex = 15;
            this.rbUpdate.TabStop = true;
            this.rbUpdate.Text = "Update";
            this.rbUpdate.UseVisualStyleBackColor = true;
            this.rbUpdate.CheckedChanged += new System.EventHandler(this.rbUpdate_CheckedChanged);
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Location = new System.Drawing.Point(221, 12);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(50, 18);
            this.rbNew.TabIndex = 15;
            this.rbNew.Text = "New";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Do you want to update or register a new Patient?";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSSN);
            this.groupBox2.Controls.Add(this.chkMiddleName);
            this.groupBox2.Controls.Add(this.chkLastName);
            this.groupBox2.Controls.Add(this.chkFirstName);
            this.groupBox2.Controls.Add(this.chkPhoto);
            this.groupBox2.Controls.Add(this.chkDOB);
            this.groupBox2.Controls.Add(this.chkadress);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(26, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 98);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update Options :";
            // 
            // chkSSN
            // 
            this.chkSSN.AutoSize = true;
            this.chkSSN.Checked = true;
            this.chkSSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSSN.Location = new System.Drawing.Point(154, 21);
            this.chkSSN.Name = "chkSSN";
            this.chkSSN.Size = new System.Drawing.Size(48, 18);
            this.chkSSN.TabIndex = 15;
            this.chkSSN.Text = "SSN";
            this.chkSSN.UseVisualStyleBackColor = true;
            // 
            // chkMiddleName
            // 
            this.chkMiddleName.AutoSize = true;
            this.chkMiddleName.Checked = true;
            this.chkMiddleName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMiddleName.Location = new System.Drawing.Point(15, 45);
            this.chkMiddleName.Name = "chkMiddleName";
            this.chkMiddleName.Size = new System.Drawing.Size(95, 18);
            this.chkMiddleName.TabIndex = 13;
            this.chkMiddleName.Text = "Middle Name";
            this.chkMiddleName.UseVisualStyleBackColor = true;
            // 
            // chkLastName
            // 
            this.chkLastName.AutoSize = true;
            this.chkLastName.Checked = true;
            this.chkLastName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLastName.Location = new System.Drawing.Point(15, 69);
            this.chkLastName.Name = "chkLastName";
            this.chkLastName.Size = new System.Drawing.Size(83, 18);
            this.chkLastName.TabIndex = 14;
            this.chkLastName.Text = "Last Name";
            this.chkLastName.UseVisualStyleBackColor = true;
            // 
            // chkFirstName
            // 
            this.chkFirstName.AutoSize = true;
            this.chkFirstName.Checked = true;
            this.chkFirstName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFirstName.Location = new System.Drawing.Point(15, 21);
            this.chkFirstName.Name = "chkFirstName";
            this.chkFirstName.Size = new System.Drawing.Size(83, 18);
            this.chkFirstName.TabIndex = 12;
            this.chkFirstName.Text = "First Name";
            this.chkFirstName.UseVisualStyleBackColor = true;
            // 
            // chkPhoto
            // 
            this.chkPhoto.AutoSize = true;
            this.chkPhoto.Checked = true;
            this.chkPhoto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPhoto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPhoto.Location = new System.Drawing.Point(259, 21);
            this.chkPhoto.Name = "chkPhoto";
            this.chkPhoto.Size = new System.Drawing.Size(59, 18);
            this.chkPhoto.TabIndex = 18;
            this.chkPhoto.Text = "Photo";
            this.chkPhoto.UseVisualStyleBackColor = true;
            // 
            // chkDOB
            // 
            this.chkDOB.AutoSize = true;
            this.chkDOB.Checked = true;
            this.chkDOB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDOB.Location = new System.Drawing.Point(154, 45);
            this.chkDOB.Name = "chkDOB";
            this.chkDOB.Size = new System.Drawing.Size(50, 18);
            this.chkDOB.TabIndex = 16;
            this.chkDOB.Text = "DOB";
            this.chkDOB.UseVisualStyleBackColor = true;
            // 
            // chkadress
            // 
            this.chkadress.AutoSize = true;
            this.chkadress.Checked = true;
            this.chkadress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkadress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkadress.Location = new System.Drawing.Point(154, 69);
            this.chkadress.Name = "chkadress";
            this.chkadress.Size = new System.Drawing.Size(69, 18);
            this.chkadress.TabIndex = 17;
            this.chkadress.Text = "Address";
            this.chkadress.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 183);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(409, 1);
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
            this.Label6.Size = new System.Drawing.Size(1, 180);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(413, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 180);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(411, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // frmSetupCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(417, 241);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupCriteria";
            this.Text = "Setup Criteria";
            this.Load += new System.EventHandler(this.frmSetupCriteria_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton btnClose;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbUpdate;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkDOB;
        private System.Windows.Forms.CheckBox chkPhoto;
        private System.Windows.Forms.CheckBox chkadress;
        private System.Windows.Forms.CheckBox chkMiddleName;
        private System.Windows.Forms.CheckBox chkLastName;
        private System.Windows.Forms.CheckBox chkFirstName;
        private System.Windows.Forms.CheckBox chkSSN;
    }
}