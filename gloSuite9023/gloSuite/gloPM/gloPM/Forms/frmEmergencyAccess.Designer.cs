namespace gloPM.Forms
{
    partial class frmEmergencyAccess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmergencyAccess));
            this.pnl_tls_ = new System.Windows.Forms.Panel();
            this.tlsDropdown = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.BtnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnl_tls_.SuspendLayout();
            this.tlsDropdown.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tls_
            // 
            this.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tls_.Controls.Add(this.tlsDropdown);
            this.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tls_.Location = new System.Drawing.Point(0, 0);
            this.pnl_tls_.Name = "pnl_tls_";
            this.pnl_tls_.Size = new System.Drawing.Size(330, 56);
            this.pnl_tls_.TabIndex = 14;
            // 
            // tlsDropdown
            // 
            this.tlsDropdown.BackColor = System.Drawing.Color.Transparent;
            this.tlsDropdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsDropdown.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsDropdown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOK,
            this.BtnClose});
            this.tlsDropdown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsDropdown.Location = new System.Drawing.Point(0, 0);
            this.tlsDropdown.Name = "tlsDropdown";
            this.tlsDropdown.Size = new System.Drawing.Size(330, 53);
            this.tlsDropdown.TabIndex = 0;
            this.tlsDropdown.Text = "toolStrip1";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(36, 50);
            this.btnOK.Tag = "Ok";
            this.btnOK.Text = "&Ok";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnClose.Image = ((System.Drawing.Image)(resources.GetObject("BtnClose.Image")));
            this.BtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(43, 50);
            this.BtnClose.Tag = "Cancel";
            this.BtnClose.Text = "&Close";
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnClose.ToolTipText = "Close";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.Label1);
            this.pnlLogin.Controls.Add(this.Label2);
            this.pnlLogin.Controls.Add(this.Label13);
            this.pnlLogin.Controls.Add(this.Label4);
            this.pnlLogin.Controls.Add(this.Label3);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(0, 56);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLogin.Size = new System.Drawing.Size(330, 81);
            this.pnlLogin.TabIndex = 15;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Location = new System.Drawing.Point(4, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(322, 1);
            this.Label1.TabIndex = 26;
            this.Label1.Text = "label1";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label2.Location = new System.Drawing.Point(326, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 74);
            this.Label2.TabIndex = 25;
            this.Label2.Text = "label4";
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label13.Location = new System.Drawing.Point(4, 77);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(323, 1);
            this.Label13.TabIndex = 24;
            this.Label13.Text = "label1";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label4.Location = new System.Drawing.Point(3, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 75);
            this.Label4.TabIndex = 23;
            this.Label4.Text = "label4";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(24, 30);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(78, 14);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "Password : ";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(103, 26);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(177, 22);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // frmEmergencyAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(330, 137);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnl_tls_);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmergencyAccess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Allow Emergency Access of Patient Chart";
            this.Load += new System.EventHandler(this.frmEmergencyAccess_Load);
            this.pnl_tls_.ResumeLayout(false);
            this.pnl_tls_.PerformLayout();
            this.tlsDropdown.ResumeLayout(false);
            this.tlsDropdown.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tls_;
        private gloGlobal.gloToolStripIgnoreFocus tlsDropdown;
        private System.Windows.Forms.ToolStripButton btnOK;
        private System.Windows.Forms.ToolStripButton BtnClose;
        internal System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label13;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtPassword;
    }
}