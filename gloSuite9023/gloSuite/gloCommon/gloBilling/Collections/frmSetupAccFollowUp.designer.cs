namespace gloBilling.Collections
{
    partial class frmSetupAccFollowUp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAccFollowUp));
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupAcctFollowUp = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClearTemplate = new System.Windows.Forms.Button();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.txtDefaultTemplate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDefNextActiondays = new System.Windows.Forms.TextBox();
            this.cmbPatAccDefFUAction = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupAcctFollowUp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupAcctFollowUp);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(467, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupAcctFollowUp
            // 
            this.tls_SetupAcctFollowUp.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupAcctFollowUp.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupAcctFollowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupAcctFollowUp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupAcctFollowUp.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupAcctFollowUp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.toolStripButton1,
            this.toolStripButton2});
            this.tls_SetupAcctFollowUp.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupAcctFollowUp.Name = "tls_SetupAcctFollowUp";
            this.tls_SetupAcctFollowUp.Padding = new System.Windows.Forms.Padding(0);
            this.tls_SetupAcctFollowUp.Size = new System.Drawing.Size(467, 53);
            this.tls_SetupAcctFollowUp.TabIndex = 0;
            this.tls_SetupAcctFollowUp.TabStop = true;
            this.tls_SetupAcctFollowUp.Text = "toolStrip1";
            this.tls_SetupAcctFollowUp.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SetupResource_ItemClicked);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton1.Tag = "OK";
            this.toolStripButton1.Text = "Sa&ve&&Cls";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Save and Close";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // txtDesc
            // 
            this.txtDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Location = new System.Drawing.Point(182, 37);
            this.txtDesc.MaxLength = 255;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(258, 22);
            this.txtDesc.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(104, 42);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(75, 14);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Description :";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(182, 11);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(106, 22);
            this.txtCode.TabIndex = 1;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(136, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(43, 14);
            this.lblCode.TabIndex = 11;
            this.lblCode.Text = "Code :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(461, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(460, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(463, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 141);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 142);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClearTemplate);
            this.panel1.Controls.Add(this.btnBrowseTemplate);
            this.panel1.Controls.Add(this.txtDefaultTemplate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtDefNextActiondays);
            this.panel1.Controls.Add(this.cmbPatAccDefFUAction);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblCode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.txtDesc);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(467, 149);
            this.panel1.TabIndex = 0;
            // 
            // btnClearTemplate
            // 
            this.btnClearTemplate.AutoEllipsis = true;
            this.btnClearTemplate.BackColor = System.Drawing.Color.Transparent;
            this.btnClearTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearTemplate.BackgroundImage")));
            this.btnClearTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnClearTemplate.Image")));
            this.btnClearTemplate.Location = new System.Drawing.Point(419, 64);
            this.btnClearTemplate.Name = "btnClearTemplate";
            this.btnClearTemplate.Size = new System.Drawing.Size(21, 21);
            this.btnClearTemplate.TabIndex = 228;
            this.toolTip1.SetToolTip(this.btnClearTemplate, "Clear Template");
            this.btnClearTemplate.UseVisualStyleBackColor = false;
            this.btnClearTemplate.Click += new System.EventHandler(this.btnClearTemplate_Click);
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.AutoEllipsis = true;
            this.btnBrowseTemplate.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseTemplate.BackgroundImage")));
            this.btnBrowseTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseTemplate.Image")));
            this.btnBrowseTemplate.Location = new System.Drawing.Point(395, 64);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseTemplate.TabIndex = 227;
            this.btnBrowseTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnBrowseTemplate, "Browse Template");
            this.btnBrowseTemplate.UseVisualStyleBackColor = false;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.btnBrowseTemplate_Click);
            // 
            // txtDefaultTemplate
            // 
            this.txtDefaultTemplate.BackColor = System.Drawing.Color.White;
            this.txtDefaultTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultTemplate.ForeColor = System.Drawing.Color.Black;
            this.txtDefaultTemplate.Location = new System.Drawing.Point(182, 63);
            this.txtDefaultTemplate.MaxLength = 3;
            this.txtDefaultTemplate.Name = "txtDefaultTemplate";
            this.txtDefaultTemplate.ReadOnly = true;
            this.txtDefaultTemplate.Size = new System.Drawing.Size(209, 22);
            this.txtDefaultTemplate.TabIndex = 136;
            this.txtDefaultTemplate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(112, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 135;
            this.label6.Text = "Template :";
            // 
            // txtDefNextActiondays
            // 
            this.txtDefNextActiondays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefNextActiondays.ForeColor = System.Drawing.Color.Black;
            this.txtDefNextActiondays.Location = new System.Drawing.Point(182, 115);
            this.txtDefNextActiondays.MaxLength = 3;
            this.txtDefNextActiondays.Name = "txtDefNextActiondays";
            this.txtDefNextActiondays.Size = new System.Drawing.Size(67, 22);
            this.txtDefNextActiondays.TabIndex = 134;
            this.txtDefNextActiondays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDefNextActiondays_KeyPress);
            // 
            // cmbPatAccDefFUAction
            // 
            this.cmbPatAccDefFUAction.DropDownHeight = 130;
            this.cmbPatAccDefFUAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatAccDefFUAction.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmbPatAccDefFUAction.FormattingEnabled = true;
            this.cmbPatAccDefFUAction.IntegralHeight = false;
            this.cmbPatAccDefFUAction.Location = new System.Drawing.Point(182, 89);
            this.cmbPatAccDefFUAction.Name = "cmbPatAccDefFUAction";
            this.cmbPatAccDefFUAction.Size = new System.Drawing.Size(258, 22);
            this.cmbPatAccDefFUAction.TabIndex = 3;
            this.cmbPatAccDefFUAction.SelectedIndexChanged += new System.EventHandler(this.cmbPatAccDefFUAction_SelectedIndexChanged);
            this.cmbPatAccDefFUAction.MouseEnter += new System.EventHandler(this.cmbPatAccDefFUAction_MouseEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 14);
            this.label5.TabIndex = 133;
            this.label5.Text = "Default next action :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 14);
            this.label7.TabIndex = 131;
            this.label7.Text = "Default next action days :";
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(126, 16);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 111;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(93, 42);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmSetupAccFollowUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(467, 203);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupAccFollowUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Account Follow-up Actions";
            this.Load += new System.EventHandler(this.frmSetupClaimAccFollowUp_Load);
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupAcctFollowUp.ResumeLayout(false);
            this.tls_SetupAcctFollowUp.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupAcctFollowUp;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cmbPatAccDefFUAction;
        private System.Windows.Forms.TextBox txtDefNextActiondays;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtDefaultTemplate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClearTemplate;
        private System.Windows.Forms.Button btnBrowseTemplate;
    }
}