namespace gloBilling
{
    partial class frmEligibilityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEligibilityForm));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnCheckEligibility = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.txtServiceType = new System.Windows.Forms.TextBox();
            this.lblServiceType = new System.Windows.Forms.Label();
            this.grp_InsuranceType = new System.Windows.Forms.GroupBox();
            this.rdbMutualOfOmaha = new System.Windows.Forms.RadioButton();
            this.rdbTricare = new System.Windows.Forms.RadioButton();
            this.rdb_Aetna = new System.Windows.Forms.RadioButton();
            this.rdbMedicare = new System.Windows.Forms.RadioButton();
            this.rdbMedicaidCalifornia = new System.Windows.Forms.RadioButton();
            this.rdbMailHandlers = new System.Windows.Forms.RadioButton();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.grp_InsuranceType.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(710, 54);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnCheckEligibility,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(710, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnCheckEligibility
            // 
            this.tls_btnCheckEligibility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCheckEligibility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCheckEligibility.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCheckEligibility.Image")));
            this.tls_btnCheckEligibility.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCheckEligibility.Name = "tls_btnCheckEligibility";
            this.tls_btnCheckEligibility.Size = new System.Drawing.Size(105, 50);
            this.tls_btnCheckEligibility.Tag = "Check Eligibility";
            this.tls_btnCheckEligibility.Text = "&Check Eligibility";
            this.tls_btnCheckEligibility.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCheckEligibility.Click += new System.EventHandler(this.tls_btnCheckEligibility_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.ToolTipText = "Close";
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Controls.Add(this.txtServiceType);
            this.pnlMain.Controls.Add(this.lblServiceType);
            this.pnlMain.Controls.Add(this.grp_InsuranceType);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(710, 113);
            this.pnlMain.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 109);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(702, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 106);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(706, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 106);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(704, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // txtServiceType
            // 
            this.txtServiceType.Location = new System.Drawing.Point(99, 15);
            this.txtServiceType.Name = "txtServiceType";
            this.txtServiceType.Size = new System.Drawing.Size(298, 22);
            this.txtServiceType.TabIndex = 0;
            // 
            // lblServiceType
            // 
            this.lblServiceType.AutoSize = true;
            this.lblServiceType.Location = new System.Drawing.Point(10, 19);
            this.lblServiceType.Name = "lblServiceType";
            this.lblServiceType.Size = new System.Drawing.Size(86, 14);
            this.lblServiceType.TabIndex = 3;
            this.lblServiceType.Text = "Service Type :";
            // 
            // grp_InsuranceType
            // 
            this.grp_InsuranceType.Controls.Add(this.rdbMutualOfOmaha);
            this.grp_InsuranceType.Controls.Add(this.rdbTricare);
            this.grp_InsuranceType.Controls.Add(this.rdb_Aetna);
            this.grp_InsuranceType.Controls.Add(this.rdbMedicare);
            this.grp_InsuranceType.Controls.Add(this.rdbMedicaidCalifornia);
            this.grp_InsuranceType.Controls.Add(this.rdbMailHandlers);
            this.grp_InsuranceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_InsuranceType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_InsuranceType.Location = new System.Drawing.Point(13, 43);
            this.grp_InsuranceType.Name = "grp_InsuranceType";
            this.grp_InsuranceType.Size = new System.Drawing.Size(677, 56);
            this.grp_InsuranceType.TabIndex = 1;
            this.grp_InsuranceType.TabStop = false;
            this.grp_InsuranceType.Text = "Insurance Types";
            // 
            // rdbMutualOfOmaha
            // 
            this.rdbMutualOfOmaha.AutoSize = true;
            this.rdbMutualOfOmaha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMutualOfOmaha.Location = new System.Drawing.Point(457, 23);
            this.rdbMutualOfOmaha.Name = "rdbMutualOfOmaha";
            this.rdbMutualOfOmaha.Size = new System.Drawing.Size(118, 18);
            this.rdbMutualOfOmaha.TabIndex = 4;
            this.rdbMutualOfOmaha.Text = "Mutual of Omaha";
            this.rdbMutualOfOmaha.UseVisualStyleBackColor = true;
            this.rdbMutualOfOmaha.CheckedChanged += new System.EventHandler(this.rdbMutualOfOmaha_CheckedChanged);
            // 
            // rdbTricare
            // 
            this.rdbTricare.AutoSize = true;
            this.rdbTricare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTricare.Location = new System.Drawing.Point(597, 23);
            this.rdbTricare.Name = "rdbTricare";
            this.rdbTricare.Size = new System.Drawing.Size(62, 18);
            this.rdbTricare.TabIndex = 5;
            this.rdbTricare.Text = "Tricare";
            this.rdbTricare.UseVisualStyleBackColor = true;
            this.rdbTricare.CheckedChanged += new System.EventHandler(this.rdbTricare_CheckedChanged);
            // 
            // rdb_Aetna
            // 
            this.rdb_Aetna.AutoSize = true;
            this.rdb_Aetna.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_Aetna.Location = new System.Drawing.Point(22, 23);
            this.rdb_Aetna.Name = "rdb_Aetna";
            this.rdb_Aetna.Size = new System.Drawing.Size(58, 18);
            this.rdb_Aetna.TabIndex = 0;
            this.rdb_Aetna.Text = "Aetna";
            this.rdb_Aetna.UseVisualStyleBackColor = true;
            this.rdb_Aetna.CheckedChanged += new System.EventHandler(this.rdb_Aetna_CheckedChanged);
            // 
            // rdbMedicare
            // 
            this.rdbMedicare.AutoSize = true;
            this.rdbMedicare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMedicare.Location = new System.Drawing.Point(246, 23);
            this.rdbMedicare.Name = "rdbMedicare";
            this.rdbMedicare.Size = new System.Drawing.Size(73, 18);
            this.rdbMedicare.TabIndex = 2;
            this.rdbMedicare.Text = "Medicare";
            this.rdbMedicare.UseVisualStyleBackColor = true;
            this.rdbMedicare.CheckedChanged += new System.EventHandler(this.rdbMedicare_CheckedChanged);
            // 
            // rdbMedicaidCalifornia
            // 
            this.rdbMedicaidCalifornia.AutoSize = true;
            this.rdbMedicaidCalifornia.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMedicaidCalifornia.Location = new System.Drawing.Point(102, 23);
            this.rdbMedicaidCalifornia.Name = "rdbMedicaidCalifornia";
            this.rdbMedicaidCalifornia.Size = new System.Drawing.Size(122, 18);
            this.rdbMedicaidCalifornia.TabIndex = 1;
            this.rdbMedicaidCalifornia.Text = "Medicaid California";
            this.rdbMedicaidCalifornia.UseVisualStyleBackColor = true;
            this.rdbMedicaidCalifornia.CheckedChanged += new System.EventHandler(this.rdbMedicaidCalifornia_CheckedChanged);
            // 
            // rdbMailHandlers
            // 
            this.rdbMailHandlers.AutoSize = true;
            this.rdbMailHandlers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMailHandlers.Location = new System.Drawing.Point(341, 23);
            this.rdbMailHandlers.Name = "rdbMailHandlers";
            this.rdbMailHandlers.Size = new System.Drawing.Size(94, 18);
            this.rdbMailHandlers.TabIndex = 3;
            this.rdbMailHandlers.Text = "Mail Handlers";
            this.rdbMailHandlers.UseVisualStyleBackColor = true;
            this.rdbMailHandlers.CheckedChanged += new System.EventHandler(this.rdbMailHandlers_CheckedChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.Label5);
            this.pnlBottom.Controls.Add(this.Label6);
            this.pnlBottom.Controls.Add(this.Label7);
            this.pnlBottom.Controls.Add(this.Label8);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 167);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBottom.Size = new System.Drawing.Size(710, 306);
            this.pnlBottom.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 302);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(702, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 302);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(706, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 302);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(704, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // frmEligibilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(710, 473);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEligibilityForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eligibility Check";
            this.Load += new System.EventHandler(this.frmEligibilityForm_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.grp_InsuranceType.ResumeLayout(false);
            this.grp_InsuranceType.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnCheckEligibility;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.RadioButton rdbMedicare;
        private System.Windows.Forms.RadioButton rdbMailHandlers;
        private System.Windows.Forms.RadioButton rdbMedicaidCalifornia;
        private System.Windows.Forms.RadioButton rdb_Aetna;
        private System.Windows.Forms.GroupBox grp_InsuranceType;
        private System.Windows.Forms.RadioButton rdbMutualOfOmaha;
        private System.Windows.Forms.RadioButton rdbTricare;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TextBox txtServiceType;
        private System.Windows.Forms.Label lblServiceType;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
    }
}