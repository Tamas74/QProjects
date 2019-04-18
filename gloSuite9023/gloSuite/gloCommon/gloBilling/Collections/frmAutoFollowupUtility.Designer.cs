namespace gloBilling.Collections
{
    partial class frmAutoFollowupUtility
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.CPTStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClaim = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.lblClaimStatus = new System.Windows.Forms.Label();
            this.lblAccountStatus = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblClaim = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6.SuspendLayout();
            this.CPTStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.CPTStatus);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 82);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(681, 52);
            this.panel6.TabIndex = 24;
            // 
            // CPTStatus
            // 
            this.CPTStatus.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.CPTStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CPTStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.ProgressBar});
            this.CPTStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.CPTStatus.Location = new System.Drawing.Point(4, 6);
            this.CPTStatus.Name = "CPTStatus";
            this.CPTStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.CPTStatus.Size = new System.Drawing.Size(673, 42);
            this.CPTStatus.TabIndex = 21;
            this.CPTStatus.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(119, 14);
            this.lblStatus.Text = "Precessing Accounts";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(668, 17);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.btnClaim);
            this.panel1.Controls.Add(this.btnAccount);
            this.panel1.Controls.Add(this.lblClaimStatus);
            this.panel1.Controls.Add(this.lblAccountStatus);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblClaim);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblAccount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Size = new System.Drawing.Size(681, 82);
            this.panel1.TabIndex = 31;
            // 
            // btnClaim
            // 
            this.btnClaim.FlatAppearance.BorderSize = 0;
            this.btnClaim.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClaim.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClaim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClaim.Location = new System.Drawing.Point(25, 53);
            this.btnClaim.Name = "btnClaim";
            this.btnClaim.Size = new System.Drawing.Size(15, 18);
            this.btnClaim.TabIndex = 34;
            this.btnClaim.UseVisualStyleBackColor = true;
            // 
            // btnAccount
            // 
            this.btnAccount.FlatAppearance.BorderSize = 0;
            this.btnAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccount.Location = new System.Drawing.Point(25, 31);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(15, 18);
            this.btnAccount.TabIndex = 33;
            this.btnAccount.UseVisualStyleBackColor = true;
            // 
            // lblClaimStatus
            // 
            this.lblClaimStatus.AutoSize = true;
            this.lblClaimStatus.Location = new System.Drawing.Point(209, 55);
            this.lblClaimStatus.Name = "lblClaimStatus";
            this.lblClaimStatus.Size = new System.Drawing.Size(0, 14);
            this.lblClaimStatus.TabIndex = 32;
            // 
            // lblAccountStatus
            // 
            this.lblAccountStatus.AutoSize = true;
            this.lblAccountStatus.Location = new System.Drawing.Point(209, 33);
            this.lblAccountStatus.Name = "lblAccountStatus";
            this.lblAccountStatus.Size = new System.Drawing.Size(64, 14);
            this.lblAccountStatus.TabIndex = 31;
            this.lblAccountStatus.Text = "In Progess";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(209, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 14);
            this.label10.TabIndex = 30;
            this.label10.Text = "Status";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(677, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 77);
            this.label6.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 77);
            this.label5.TabIndex = 28;
            // 
            // lblClaim
            // 
            this.lblClaim.AutoSize = true;
            this.lblClaim.Location = new System.Drawing.Point(44, 55);
            this.lblClaim.Name = "lblClaim";
            this.lblClaim.Size = new System.Drawing.Size(90, 14);
            this.lblClaim.TabIndex = 28;
            this.lblClaim.Text = "Claim Follow-up";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(675, 1);
            this.label4.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(675, 1);
            this.label3.TabIndex = 26;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(44, 33);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(109, 14);
            this.lblAccount.TabIndex = 26;
            this.lblAccount.Text = "Account Follow-up";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 25;
            this.label2.Text = "Action Item";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 45);
            this.label1.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(677, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 45);
            this.label7.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(4, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(673, 1);
            this.label8.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(3, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(675, 1);
            this.label9.TabIndex = 32;
            // 
            // frmAutoFollowupUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(681, 134);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAutoFollowupUtility";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Follow-up Utility";
            this.Load += new System.EventHandler(this.frmAutoFollowupUtility_Load);
            this.Shown += new System.EventHandler(this.frmAutoFollowupUtility_Shown);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.CPTStatus.ResumeLayout(false);
            this.CPTStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.StatusStrip CPTStatus;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblClaimStatus;
        private System.Windows.Forms.Label lblAccountStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblClaim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button btnClaim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}