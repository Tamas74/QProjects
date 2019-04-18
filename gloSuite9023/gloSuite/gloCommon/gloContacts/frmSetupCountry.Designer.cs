namespace gloContacts
{
    partial class frmSetupCountry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupCountry));
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_MSTCPT = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSaveOnly = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatelable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSubCode = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.txtCountryName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_MSTCPT.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_MSTCPT);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(344, 54);
            this.pnl_tlsp.TabIndex = 2;
            // 
            // tlsp_MSTCPT
            // 
            this.tlsp_MSTCPT.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_MSTCPT.BackgroundImage = global::gloContacts.Properties.Resources.Img_Toolstrip;
            this.tlsp_MSTCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_MSTCPT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.tlsp_MSTCPT.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_MSTCPT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSaveOnly,
            this.ts_btnSave,
            this.ts_btnClose});
            this.tlsp_MSTCPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_MSTCPT.Location = new System.Drawing.Point(0, 0);
            this.tlsp_MSTCPT.Name = "tlsp_MSTCPT";
            this.tlsp_MSTCPT.Size = new System.Drawing.Size(344, 53);
            this.tlsp_MSTCPT.TabIndex = 0;
            this.tlsp_MSTCPT.Text = "toolStrip1";
            // 
            // ts_btnSaveOnly
            // 
            this.ts_btnSaveOnly.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSaveOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSaveOnly.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveOnly.Image")));
            this.ts_btnSaveOnly.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveOnly.Name = "ts_btnSaveOnly";
            this.ts_btnSaveOnly.Size = new System.Drawing.Size(40, 50);
            this.ts_btnSaveOnly.Tag = "Save";
            this.ts_btnSaveOnly.Text = "&Save";
            this.ts_btnSaveOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSaveOnly.ToolTipText = "Save";
            this.ts_btnSaveOnly.Visible = false;
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "Sa&ve&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.txtStatelable);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.txtSubCode);
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.lbl_pnlBottom);
            this.Panel1.Controls.Add(this.lbl_pnlLeft);
            this.Panel1.Controls.Add(this.lbl_pnlRight);
            this.Panel1.Controls.Add(this.lbl_pnlTop);
            this.Panel1.Controls.Add(this.txtCountryName);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.txtCode);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 54);
            this.Panel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(344, 89);
            this.Panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(25, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 14);
            this.label3.TabIndex = 26;
            this.label3.Text = "State Label : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Visible = false;
            // 
            // txtStatelable
            // 
            this.txtStatelable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatelable.ForeColor = System.Drawing.Color.Black;
            this.txtStatelable.Location = new System.Drawing.Point(107, 125);
            this.txtStatelable.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtStatelable.MaxLength = 8;
            this.txtStatelable.Name = "txtStatelable";
            this.txtStatelable.Size = new System.Drawing.Size(218, 22);
            this.txtStatelable.TabIndex = 25;
            this.txtStatelable.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(31, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 14);
            this.label4.TabIndex = 24;
            this.label4.Text = "Sub Code : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // txtSubCode
            // 
            this.txtSubCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubCode.ForeColor = System.Drawing.Color.Black;
            this.txtSubCode.Location = new System.Drawing.Point(107, 94);
            this.txtSubCode.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtSubCode.MaxLength = 5;
            this.txtSubCode.Name = "txtSubCode";
            this.txtSubCode.Size = new System.Drawing.Size(87, 22);
            this.txtSubCode.TabIndex = 2;
            this.txtSubCode.Visible = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Red;
            this.Label6.Location = new System.Drawing.Point(28, 23);
            this.Label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(14, 14);
            this.Label6.TabIndex = 22;
            this.Label6.Text = "*";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.Red;
            this.Label5.Location = new System.Drawing.Point(41, 54);
            this.Label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(14, 14);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "*";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 85);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(336, 1);
            this.lbl_pnlBottom.TabIndex = 20;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlLeft.TabIndex = 19;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(340, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 82);
            this.lbl_pnlRight.TabIndex = 18;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(338, 1);
            this.lbl_pnlTop.TabIndex = 17;
            this.lbl_pnlTop.Text = "label1";
            // 
            // txtCountryName
            // 
            this.txtCountryName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountryName.ForeColor = System.Drawing.Color.Black;
            this.txtCountryName.Location = new System.Drawing.Point(107, 19);
            this.txtCountryName.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtCountryName.MaxLength = 50;
            this.txtCountryName.Name = "txtCountryName";
            this.txtCountryName.Size = new System.Drawing.Size(218, 22);
            this.txtCountryName.TabIndex = 0;
            this.txtCountryName.MouseHover += new System.EventHandler(this.txtCountryName_MouseHover);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Location = new System.Drawing.Point(41, 23);
            this.Label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(62, 14);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Country : ";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Location = new System.Drawing.Point(56, 54);
            this.Label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 14);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Code : ";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(107, 50);
            this.txtCode.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtCode.MaxLength = 3;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(90, 22);
            this.txtCode.TabIndex = 1;
            // 
            // frmSetupCountry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(344, 143);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupCountry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Country";
            this.Load += new System.EventHandler(this.frmSetupCountry_Load);
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_MSTCPT.ResumeLayout(false);
            this.tlsp_MSTCPT.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MSTCPT;
        private System.Windows.Forms.ToolStripButton ts_btnSaveOnly;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        internal System.Windows.Forms.TextBox txtCountryName;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtCode;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtSubCode;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtStatelable;
    }
}