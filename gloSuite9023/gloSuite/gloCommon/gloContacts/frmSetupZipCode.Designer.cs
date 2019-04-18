namespace gloContacts
{
    partial class frmSetupZipCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupZipCode));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblAreaCode = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtAreacode = new System.Windows.Forms.TextBox();
            this.txtCounty = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtZIPCode = new System.Windows.Forms.TextBox();
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_MSTCPT = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSaveOnly = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel1.SuspendLayout();
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_MSTCPT.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.lbl_pnlBottom);
            this.Panel1.Controls.Add(this.lbl_pnlLeft);
            this.Panel1.Controls.Add(this.lbl_pnlRight);
            this.Panel1.Controls.Add(this.lbl_pnlTop);
            this.Panel1.Controls.Add(this.cmbState);
            this.Panel1.Controls.Add(this.lblAreaCode);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.txtAreacode);
            this.Panel1.Controls.Add(this.txtCounty);
            this.Panel1.Controls.Add(this.txtCity);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.txtZIPCode);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 54);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(337, 167);
            this.Panel1.TabIndex = 0;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Red;
            this.Label6.Location = new System.Drawing.Point(53, 49);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.Label5.Location = new System.Drawing.Point(26, 21);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 163);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(329, 1);
            this.lbl_pnlBottom.TabIndex = 20;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 160);
            this.lbl_pnlLeft.TabIndex = 19;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(333, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 160);
            this.lbl_pnlRight.TabIndex = 18;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(331, 1);
            this.lbl_pnlTop.TabIndex = 17;
            this.lbl_pnlTop.Text = "label1";
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(104, 72);
            this.cmbState.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(207, 22);
            this.cmbState.TabIndex = 2;
            // 
            // lblAreaCode
            // 
            this.lblAreaCode.AutoSize = true;
            this.lblAreaCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaCode.Location = new System.Drawing.Point(33, 130);
            this.lblAreaCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAreaCode.Name = "lblAreaCode";
            this.lblAreaCode.Size = new System.Drawing.Size(68, 14);
            this.lblAreaCode.TabIndex = 12;
            this.lblAreaCode.Text = "Area Code:";
            this.lblAreaCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(47, 103);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(54, 14);
            this.Label3.TabIndex = 12;
            this.Label3.Text = "County :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(56, 76);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(45, 14);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "State :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAreacode
            // 
            this.txtAreacode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAreacode.ForeColor = System.Drawing.Color.Black;
            this.txtAreacode.Location = new System.Drawing.Point(104, 128);
            this.txtAreacode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            //this.txtAreacode.MaxLength = 16;
            this.txtAreacode.MaxLength = 3;//Added by mitesh as per EMR Zipcode.
            this.txtAreacode.Name = "txtAreacode";
            this.txtAreacode.Size = new System.Drawing.Size(207, 22);
            this.txtAreacode.TabIndex = 4;
            this.txtAreacode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAreacode_KeyPress);
            // 
            // txtCounty
            // 
            this.txtCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounty.ForeColor = System.Drawing.Color.Black;
            this.txtCounty.Location = new System.Drawing.Point(104, 100);
            this.txtCounty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCounty.MaxLength = 50;
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(207, 22);
            this.txtCounty.TabIndex = 3;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(104, 44);
            this.txtCity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCity.MaxLength = 50;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(207, 22);
            this.txtCity.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(66, 49);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 14);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "City :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(38, 22);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 14);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Zip Code :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZIPCode
            // 
            this.txtZIPCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZIPCode.ForeColor = System.Drawing.Color.Black;
            this.txtZIPCode.Location = new System.Drawing.Point(104, 16);
            this.txtZIPCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            //this.txtZIPCode.MaxLength = 10;
            this.txtZIPCode.MaxLength = 6; //Added by mitesh as per EMR Zip code.
            this.txtZIPCode.Name = "txtZIPCode";
            this.txtZIPCode.Size = new System.Drawing.Size(207, 22);
            this.txtZIPCode.TabIndex = 0;
            this.txtZIPCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZIPCode_KeyPress);
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_MSTCPT);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(337, 54);
            this.pnl_tlsp.TabIndex = 1;
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
            this.tlsp_MSTCPT.Size = new System.Drawing.Size(337, 53);
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
            this.ts_btnSaveOnly.Click += new System.EventHandler(this.ts_btnSaveOnly_Click);
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // frmSetupZipCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(337, 221);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupZipCode";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Zip Code";
            this.Load += new System.EventHandler(this.frmSetupZipCode_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_MSTCPT.ResumeLayout(false);
            this.tlsp_MSTCPT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        internal System.Windows.Forms.ComboBox cmbState;
        internal System.Windows.Forms.Label lblAreaCode;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtAreacode;
        internal System.Windows.Forms.TextBox txtCounty;
        internal System.Windows.Forms.TextBox txtCity;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtZIPCode;
        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_MSTCPT;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.ToolStripButton ts_btnSaveOnly;
    }
}