namespace gloPM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.btn_tls_Ok = new System.Windows.Forms.ToolStripButton();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.txtChangePassword = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnl_ToolStrip.SuspendLayout();
            this.tls.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_ToolStrip.Controls.Add(this.tls);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(333, 54);
            this.pnl_ToolStrip.TabIndex = 7;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = global::gloPM.Properties.Resources.Img_Toolstrip;
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_tls_Ok});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(333, 53);
            this.tls.TabIndex = 0;
            this.tls.Text = "toolStrip1";
            // 
            // btn_tls_Ok
            // 
            this.btn_tls_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Ok.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Ok.Image")));
            this.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Ok.Name = "btn_tls_Ok";
            this.btn_tls_Ok.Size = new System.Drawing.Size(66, 50);
            this.btn_tls_Ok.Tag = "OK";
            this.btn_tls_Ok.Text = "Sa&ve&&Cls";
            this.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Ok.ToolTipText = "Save and Close";
            this.btn_tls_Ok.Click += new System.EventHandler(this.btn_tls_Ok_Click);
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.txtChangePassword);
            this.pnl_Base.Controls.Add(this.Label1);
            this.pnl_Base.Controls.Add(this.txtConfirmPassword);
            this.pnl_Base.Controls.Add(this.Label2);
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 54);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(333, 84);
            this.pnl_Base.TabIndex = 8;
            // 
            // txtChangePassword
            // 
            this.txtChangePassword.BackColor = System.Drawing.Color.GhostWhite;
            this.txtChangePassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChangePassword.ForeColor = System.Drawing.Color.Black;
            this.txtChangePassword.Location = new System.Drawing.Point(155, 17);
            this.txtChangePassword.Name = "txtChangePassword";
            this.txtChangePassword.PasswordChar = '*';
            this.txtChangePassword.Size = new System.Drawing.Size(155, 22);
            this.txtChangePassword.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(23, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(128, 14);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Enter new password :";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BackColor = System.Drawing.Color.GhostWhite;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmPassword.Location = new System.Drawing.Point(155, 45);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(155, 22);
            this.txtConfirmPassword.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(40, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(111, 14);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "Confirm password :";
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 80);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(325, 1);
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
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 77);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(329, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 77);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(327, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(333, 138);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmChangePassword";
            this.Text = "Change Password";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChangePassword_FormClosing);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.pnl_Base.ResumeLayout(false);
            this.pnl_Base.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton btn_tls_Ok;
        private System.Windows.Forms.Panel pnl_Base;
        internal System.Windows.Forms.TextBox txtChangePassword;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtConfirmPassword;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
    }
}