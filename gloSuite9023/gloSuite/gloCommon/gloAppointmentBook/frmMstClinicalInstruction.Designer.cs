namespace gloAppointmentBook
{
    partial class frmMstClinicalInstruction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMstClinicalInstruction));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.txtInstructionDesc = new System.Windows.Forms.RichTextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtInstruction = new System.Windows.Forms.TextBox();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_CategoryMaster = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel1.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_CategoryMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.txtInstructionDesc);
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.lbl_pnlBottom);
            this.Panel1.Controls.Add(this.lbl_pnlLeft);
            this.Panel1.Controls.Add(this.lbl_pnlRight);
            this.Panel1.Controls.Add(this.lbl_pnlTop);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.txtInstruction);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 53);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(694, 310);
            this.Panel1.TabIndex = 1;
            // 
            // txtInstructionDesc
            // 
            this.txtInstructionDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInstructionDesc.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtInstructionDesc.Location = new System.Drawing.Point(166, 48);
            this.txtInstructionDesc.MaxLength = 8000;
            this.txtInstructionDesc.Name = "txtInstructionDesc";
            this.txtInstructionDesc.Size = new System.Drawing.Size(510, 242);
            this.txtInstructionDesc.TabIndex = 1;
            this.txtInstructionDesc.Text = "";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Red;
            this.Label6.Location = new System.Drawing.Point(75, 21);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(14, 14);
            this.Label6.TabIndex = 16;
            this.Label6.Text = "*";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label1.Location = new System.Drawing.Point(23, 48);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(138, 14);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Instruction Description :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Red;
            this.Label3.Location = new System.Drawing.Point(10, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(14, 14);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "*";
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 306);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(686, 1);
            this.lbl_pnlBottom.TabIndex = 13;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 303);
            this.lbl_pnlLeft.TabIndex = 12;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(690, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 303);
            this.lbl_pnlRight.TabIndex = 11;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(688, 1);
            this.lbl_pnlTop.TabIndex = 10;
            this.lbl_pnlTop.Text = "label1";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label5.Location = new System.Drawing.Point(87, 21);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(74, 14);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "Instruction :";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInstruction
            // 
            this.txtInstruction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstruction.ForeColor = System.Drawing.Color.Black;
            this.txtInstruction.Location = new System.Drawing.Point(166, 17);
            this.txtInstruction.MaxLength = 255;
            this.txtInstruction.Name = "txtInstruction";
            this.txtInstruction.Size = new System.Drawing.Size(510, 22);
            this.txtInstruction.TabIndex = 0;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolStrip.Controls.Add(this.ts_CategoryMaster);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlToolStrip.ForeColor = System.Drawing.Color.Black;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(694, 53);
            this.pnlToolStrip.TabIndex = 2;
            // 
            // ts_CategoryMaster
            // 
            this.ts_CategoryMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_CategoryMaster.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.ts_CategoryMaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_CategoryMaster.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_CategoryMaster.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_CategoryMaster.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_CategoryMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.ts_btnSave,
            this.ts_btnClose});
            this.ts_CategoryMaster.Location = new System.Drawing.Point(0, 0);
            this.ts_CategoryMaster.Name = "ts_CategoryMaster";
            this.ts_CategoryMaster.Size = new System.Drawing.Size(694, 53);
            this.ts_CategoryMaster.TabIndex = 1;
            this.ts_CategoryMaster.Text = "ToolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "Sa&ve";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            this.ts_btnSave.Text = "&Save&&Cls";
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
            // frmMstClinicalInstruction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(694, 363);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMstClinicalInstruction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clinical Instruction";
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_CategoryMaster.ResumeLayout(false);
            this.ts_CategoryMaster.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtInstruction;
        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_CategoryMaster;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.RichTextBox txtInstructionDesc;
        public  System.Windows.Forms.ToolStripButton tsb_Save;
    }
}