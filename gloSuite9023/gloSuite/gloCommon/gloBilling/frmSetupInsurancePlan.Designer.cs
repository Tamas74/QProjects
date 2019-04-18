namespace gloBilling
{
    partial class frmSetupInsurancePlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupInsurancePlan));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_InsurancePlan = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_save = new System.Windows.Forms.ToolStripButton();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.txtPlanDescription = new System.Windows.Forms.TextBox();
            this.txtPlanCode = new System.Windows.Forms.TextBox();
            this.lblPlanDescription = new System.Windows.Forms.Label();
            this.lblPlanCode = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_InsurancePlan.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_InsurancePlan);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(377, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tls_InsurancePlan
            // 
            this.tls_InsurancePlan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_InsurancePlan.BackgroundImage")));
            this.tls_InsurancePlan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_InsurancePlan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_InsurancePlan.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_InsurancePlan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_save,
            this.tls_btnOK,
            this.tls_btnCancel});
            this.tls_InsurancePlan.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_InsurancePlan.Location = new System.Drawing.Point(0, 0);
            this.tls_InsurancePlan.Name = "tls_InsurancePlan";
            this.tls_InsurancePlan.Size = new System.Drawing.Size(377, 53);
            this.tls_InsurancePlan.TabIndex = 0;
            this.tls_InsurancePlan.TabStop = true;
            this.tls_InsurancePlan.Text = "toolStrip1";
            this.tls_InsurancePlan.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_InsurancePlan_ItemClicked);
            // 
            // tsb_save
            // 
            this.tsb_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save.Image")));
            this.tsb_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save.Name = "tsb_save";
            this.tsb_save.Size = new System.Drawing.Size(40, 50);
            this.tsb_save.Tag = "Save";
            this.tsb_save.Text = "&Save";
            this.tsb_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_save.ToolTipText = "Save";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "Sa&ve&&Cls";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.ToolTipText = "Save and Close";
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
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Controls.Add(this.txtPlanDescription);
            this.pnlMain.Controls.Add(this.txtPlanCode);
            this.pnlMain.Controls.Add(this.lblPlanDescription);
            this.pnlMain.Controls.Add(this.lblPlanCode);
            this.pnlMain.Controls.Add(this.label19);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(377, 105);
            this.pnlMain.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 101);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(369, 1);
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
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 98);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(373, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 98);
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
            this.lbl_TopBrd.Size = new System.Drawing.Size(371, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // txtPlanDescription
            // 
            this.txtPlanDescription.ForeColor = System.Drawing.Color.Black;
            this.txtPlanDescription.Location = new System.Drawing.Point(129, 56);
            this.txtPlanDescription.Name = "txtPlanDescription";
            this.txtPlanDescription.Size = new System.Drawing.Size(227, 22);
            this.txtPlanDescription.TabIndex = 1;
            this.txtPlanDescription.Tag = "2";
            // 
            // txtPlanCode
            // 
            this.txtPlanCode.ForeColor = System.Drawing.Color.Black;
            this.txtPlanCode.Location = new System.Drawing.Point(129, 26);
            this.txtPlanCode.Name = "txtPlanCode";
            this.txtPlanCode.Size = new System.Drawing.Size(227, 22);
            this.txtPlanCode.TabIndex = 0;
            this.txtPlanCode.Tag = "1";
            // 
            // lblPlanDescription
            // 
            this.lblPlanDescription.AutoSize = true;
            this.lblPlanDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPlanDescription.Location = new System.Drawing.Point(27, 60);
            this.lblPlanDescription.Name = "lblPlanDescription";
            this.lblPlanDescription.Size = new System.Drawing.Size(101, 14);
            this.lblPlanDescription.TabIndex = 1;
            this.lblPlanDescription.Text = "Plan Description :";
            // 
            // lblPlanCode
            // 
            this.lblPlanCode.AutoSize = true;
            this.lblPlanCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPlanCode.Location = new System.Drawing.Point(59, 30);
            this.lblPlanCode.Name = "lblPlanCode";
            this.lblPlanCode.Size = new System.Drawing.Size(69, 14);
            this.lblPlanCode.TabIndex = 0;
            this.lblPlanCode.Text = "Plan Code :";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(15, 59);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(47, 30);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 111;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmSetupInsurancePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(377, 159);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupInsurancePlan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Plan Codes";
            this.Load += new System.EventHandler(this.frmSetupInsurancePlan_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_InsurancePlan.ResumeLayout(false);
            this.tls_InsurancePlan.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_InsurancePlan;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtPlanDescription;
        private System.Windows.Forms.TextBox txtPlanCode;
        private System.Windows.Forms.Label lblPlanDescription;
        private System.Windows.Forms.Label lblPlanCode;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ToolStripButton tsb_save;
    }
}