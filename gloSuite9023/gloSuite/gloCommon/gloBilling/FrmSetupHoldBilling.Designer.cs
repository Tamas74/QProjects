namespace gloBilling
{
    partial class FrmSetupHoldBilling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetupHoldBilling));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Main = new System.Windows.Forms.ToolStrip();
            this.tlsbtn_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tlsbtn_Save = new System.Windows.Forms.ToolStripButton();
            this.tlsbtn_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoldBillingReasonDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHoldBillingReasonCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(495, 56);
            this.pnlToolstrip.TabIndex = 1;
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtn_SaveCls,
            this.tlsbtn_Save,
            this.tlsbtn_Cancel});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(495, 56);
            this.tls_Main.TabIndex = 2;
            this.tls_Main.TabStop = true;
            this.tls_Main.Text = "ToolStrip1";
            this.tls_Main.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Main_ItemClicked);
            // 
            // tlsbtn_SaveCls
            // 
            this.tlsbtn_SaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtn_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtn_SaveCls.Image")));
            this.tlsbtn_SaveCls.Name = "tlsbtn_SaveCls";
            this.tlsbtn_SaveCls.Size = new System.Drawing.Size(66, 53);
            this.tlsbtn_SaveCls.Tag = "SaveCls";
            this.tlsbtn_SaveCls.Text = "&Save&&Cls";
            this.tlsbtn_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtn_SaveCls.ToolTipText = "Save and Close";
            // 
            // tlsbtn_Save
            // 
            this.tlsbtn_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtn_Save.Image")));
            this.tlsbtn_Save.Name = "tlsbtn_Save";
            this.tlsbtn_Save.Size = new System.Drawing.Size(40, 53);
            this.tlsbtn_Save.Tag = "Save";
            this.tlsbtn_Save.Text = "Sa&ve";
            this.tlsbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtn_Save.ToolTipText = "Save";
            // 
            // tlsbtn_Cancel
            // 
            this.tlsbtn_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtn_Cancel.Image")));
            this.tlsbtn_Cancel.Name = "tlsbtn_Cancel";
            this.tlsbtn_Cancel.Size = new System.Drawing.Size(43, 53);
            this.tlsbtn_Cancel.Tag = "Close";
            this.tlsbtn_Cancel.Text = "&Close";
            this.tlsbtn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtn_Cancel.ToolTipText = "Close";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.txtHoldBillingReasonDesc);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.txtHoldBillingReasonCode);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 56);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(495, 125);
            this.panel4.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(27, 24);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 112;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtHoldBillingReasonDesc
            // 
            this.txtHoldBillingReasonDesc.Location = new System.Drawing.Point(195, 54);
            this.txtHoldBillingReasonDesc.Multiline = true;
            this.txtHoldBillingReasonDesc.Name = "txtHoldBillingReasonDesc";
            this.txtHoldBillingReasonDesc.Size = new System.Drawing.Size(255, 58);
            this.txtHoldBillingReasonDesc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hold Billing Description :";
            // 
            // txtHoldBillingReasonCode
            // 
            this.txtHoldBillingReasonCode.Location = new System.Drawing.Point(195, 24);
            this.txtHoldBillingReasonCode.MaxLength = 255;
            this.txtHoldBillingReasonCode.Name = "txtHoldBillingReasonCode";
            this.txtHoldBillingReasonCode.Size = new System.Drawing.Size(255, 22);
            this.txtHoldBillingReasonCode.TabIndex = 0;
            this.txtHoldBillingReasonCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHoldBillingReasonCode_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Hold Billing Reason Code :";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(487, 1);
            this.label19.TabIndex = 12;
            this.label19.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(4, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(487, 1);
            this.label20.TabIndex = 11;
            this.label20.Text = "label1";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 119);
            this.label21.TabIndex = 10;
            this.label21.Text = "label1";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(491, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 119);
            this.label22.TabIndex = 9;
            this.label22.Text = "label1";
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(34, 58);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 113;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FrmSetupHoldBilling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(495, 181);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetupHoldBilling";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Hold Billing";
            this.Load += new System.EventHandler(this.FrmSetupHoldBilling_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolstrip;
        internal System.Windows.Forms.ToolStrip tls_Main;
        internal System.Windows.Forms.ToolStripButton tlsbtn_SaveCls;
        internal System.Windows.Forms.ToolStripButton tlsbtn_Save;
        internal System.Windows.Forms.ToolStripButton tlsbtn_Cancel;
        internal System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtHoldBillingReasonDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHoldBillingReasonCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

    }
}