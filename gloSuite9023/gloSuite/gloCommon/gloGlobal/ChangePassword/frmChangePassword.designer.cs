namespace gloGlobal.ChangePassword
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.txtChangePassword = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbtnGenPass = new System.Windows.Forms.ToolStripButton();
            this.btn_tls_Ok = new System.Windows.Forms.ToolStripButton();
            this.btn_tls_Close = new System.Windows.Forms.ToolStripButton();
            this.pnl_Base.SuspendLayout();
            this.pnl_ToolStrip.SuspendLayout();
            this.tls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Base.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.txtOldPass);
            this.pnl_Base.Controls.Add(this.txtConfirmPass);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Controls.Add(this.txtChangePassword);
            this.pnl_Base.Controls.Add(this.Label1);
            this.pnl_Base.Controls.Add(this.Label2);
            this.pnl_Base.Controls.Add(this.Label3);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 54);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(360, 127);
            this.pnl_Base.TabIndex = 11;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 123);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(352, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 120);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(356, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 120);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // txtOldPass
            // 
            this.txtOldPass.BackColor = System.Drawing.Color.GhostWhite;
            this.txtOldPass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPass.ForeColor = System.Drawing.Color.Black;
            this.txtOldPass.Location = new System.Drawing.Point(135, 21);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.PasswordChar = '*';
            this.txtOldPass.Size = new System.Drawing.Size(189, 22);
            this.txtOldPass.TabIndex = 0;
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.BackColor = System.Drawing.Color.GhostWhite;
            this.txtConfirmPass.Enabled = false;
            this.txtConfirmPass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPass.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmPass.Location = new System.Drawing.Point(135, 81);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.PasswordChar = '*';
            this.txtConfirmPass.Size = new System.Drawing.Size(189, 22);
            this.txtConfirmPass.TabIndex = 2;
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(354, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // txtChangePassword
            // 
            this.txtChangePassword.BackColor = System.Drawing.Color.GhostWhite;
            this.txtChangePassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChangePassword.ForeColor = System.Drawing.Color.Black;
            this.txtChangePassword.Location = new System.Drawing.Point(135, 51);
            this.txtChangePassword.Name = "txtChangePassword";
            this.txtChangePassword.PasswordChar = '*';
            this.txtChangePassword.Size = new System.Drawing.Size(189, 22);
            this.txtChangePassword.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(45, 25);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(88, 14);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Old Password :";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(38, 55);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 14);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "New Password :";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(22, 85);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(111, 14);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Confirm Password :";
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_ToolStrip.Controls.Add(this.tls);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(360, 54);
            this.pnl_ToolStrip.TabIndex = 10;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGenPass,
            this.btn_tls_Ok,
            this.btn_tls_Close});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(360, 53);
            this.tls.TabIndex = 0;
            this.tls.Text = "toolStrip1";
            // 
            // tsbtnGenPass
            // 
            this.tsbtnGenPass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnGenPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbtnGenPass.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGenPass.Image")));
            this.tsbtnGenPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGenPass.Name = "tsbtnGenPass";
            this.tsbtnGenPass.Size = new System.Drawing.Size(90, 50);
            this.tsbtnGenPass.Tag = "Close";
            this.tsbtnGenPass.Text = "&Generate PW";
            this.tsbtnGenPass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnGenPass.ToolTipText = "Generate Password";
            this.tsbtnGenPass.Click += new System.EventHandler(this.tsbtnGenPass_Click);
            // 
            // btn_tls_Ok
            // 
            this.btn_tls_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Ok.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Ok.Image")));
            this.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Ok.Name = "btn_tls_Ok";
            this.btn_tls_Ok.Size = new System.Drawing.Size(66, 50);
            this.btn_tls_Ok.Tag = "OK";
            this.btn_tls_Ok.Text = "&Save&&Cls";
            this.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Ok.ToolTipText = "Save and Close";
            this.btn_tls_Ok.Click += new System.EventHandler(this.btn_tls_Ok_Click);
            // 
            // btn_tls_Close
            // 
            this.btn_tls_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Close.Image")));
            this.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Close.Name = "btn_tls_Close";
            this.btn_tls_Close.Size = new System.Drawing.Size(43, 50);
            this.btn_tls_Close.Tag = "Close";
            this.btn_tls_Close.Text = "&Close";
            this.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Close.Click += new System.EventHandler(this.btn_tls_Close_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(360, 181);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        internal System.Windows.Forms.TextBox txtOldPass;
        internal System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.TextBox txtChangePassword;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Panel pnl_ToolStrip;
        private gloToolStripIgnoreFocus tls;
        internal System.Windows.Forms.ToolStripButton tsbtnGenPass;
        private System.Windows.Forms.ToolStripButton btn_tls_Ok;
        internal System.Windows.Forms.ToolStripButton btn_tls_Close;

    }
}