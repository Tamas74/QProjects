namespace gloEmdeonInterface.Forms
{
    partial class frmSelectGuarantor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectGuarantor));
            this.lblName = new System.Windows.Forms.Label();
            this.lblPatName = new System.Windows.Forms.Label();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.cmbGuarantor = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_Lab_ViewAcknoledgement = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnOk = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_Lab_ViewAcknoledgement.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(26, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(54, 14);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Patient :";
            // 
            // lblPatName
            // 
            this.lblPatName.AutoSize = true;
            this.lblPatName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatName.Location = new System.Drawing.Point(82, 19);
            this.lblPatName.Name = "lblPatName";
            this.lblPatName.Size = new System.Drawing.Size(48, 14);
            this.lblPatName.TabIndex = 1;
            this.lblPatName.Text = "Mickey";
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.AutoSize = true;
            this.lblGuarantor.Location = new System.Drawing.Point(19, 50);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(61, 14);
            this.lblGuarantor.TabIndex = 2;
            this.lblGuarantor.Text = "Account :";
            // 
            // cmbGuarantor
            // 
            this.cmbGuarantor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGuarantor.FormattingEnabled = true;
            this.cmbGuarantor.Location = new System.Drawing.Point(82, 46);
            this.cmbGuarantor.Name = "cmbGuarantor";
            this.cmbGuarantor.Size = new System.Drawing.Size(257, 22);
            this.cmbGuarantor.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbGuarantor);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblPatName);
            this.panel1.Controls.Add(this.lblGuarantor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(363, 91);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(4, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(355, 1);
            this.label4.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(355, 1);
            this.label3.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(359, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 85);
            this.label2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 85);
            this.label1.TabIndex = 5;
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_Lab_ViewAcknoledgement);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(363, 54);
            this.pnl_tlsp.TabIndex = 1;
            // 
            // tlsp_Lab_ViewAcknoledgement
            // 
            this.tlsp_Lab_ViewAcknoledgement.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_Lab_ViewAcknoledgement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_Lab_ViewAcknoledgement.BackgroundImage")));
            this.tlsp_Lab_ViewAcknoledgement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Lab_ViewAcknoledgement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Lab_ViewAcknoledgement.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Lab_ViewAcknoledgement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOk});
            this.tlsp_Lab_ViewAcknoledgement.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Lab_ViewAcknoledgement.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Lab_ViewAcknoledgement.Name = "tlsp_Lab_ViewAcknoledgement";
            this.tlsp_Lab_ViewAcknoledgement.Size = new System.Drawing.Size(363, 53);
            this.tlsp_Lab_ViewAcknoledgement.TabIndex = 1;
            this.tlsp_Lab_ViewAcknoledgement.TabStop = true;
            this.tlsp_Lab_ViewAcknoledgement.Text = "toolStrip1";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(42, 50);
            this.btnOk.Tag = "Send";
            this.btnOk.Text = "&Send";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.ToolTipText = "Send";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSelectGuarantor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(363, 145);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectGuarantor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Guarantor";
            this.Load += new System.EventHandler(this.frmSelectGuarantor_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_Lab_ViewAcknoledgement.ResumeLayout(false);
            this.tlsp_Lab_ViewAcknoledgement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPatName;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.ComboBox cmbGuarantor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_Lab_ViewAcknoledgement;
        private System.Windows.Forms.ToolStripButton btnOk;
    }
}