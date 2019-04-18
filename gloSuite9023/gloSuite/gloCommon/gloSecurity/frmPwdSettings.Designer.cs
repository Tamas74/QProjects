namespace gloSecurity
{
    partial class frmPwdSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPwdSettings));
            this.pnl_tlsStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtMinLength = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtSpecialChar = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtDigits = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtLetters = new System.Windows.Forms.TextBox();
            this.txtCapLetters = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtdays = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_tlsStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsStrip
            // 
            this.pnl_tlsStrip.Controls.Add(this.ts_Commands);
            this.pnl_tlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsStrip.Name = "pnl_tlsStrip";
            this.pnl_tlsStrip.Size = new System.Drawing.Size(409, 54);
            this.pnl_tlsStrip.TabIndex = 0;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(409, 53);
            this.ts_Commands.TabIndex = 17;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(40, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(14, 157);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(198, 14);
            this.Label5.TabIndex = 22;
            this.Label5.Text = "Minimum Length of the Password :";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMinLength
            // 
            this.txtMinLength.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinLength.ForeColor = System.Drawing.Color.Black;
            this.txtMinLength.Location = new System.Drawing.Point(214, 151);
            this.txtMinLength.Name = "txtMinLength";
            this.txtMinLength.Size = new System.Drawing.Size(168, 22);
            this.txtMinLength.TabIndex = 4;
            this.txtMinLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(26, 123);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(186, 14);
            this.Label4.TabIndex = 21;
            this.Label4.Text = "Minimum No. Of Special Letters :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSpecialChar
            // 
            this.txtSpecialChar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecialChar.ForeColor = System.Drawing.Color.Black;
            this.txtSpecialChar.Location = new System.Drawing.Point(214, 117);
            this.txtSpecialChar.Name = "txtSpecialChar";
            this.txtSpecialChar.Size = new System.Drawing.Size(168, 22);
            this.txtSpecialChar.TabIndex = 3;
            this.txtSpecialChar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(77, 89);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(135, 14);
            this.Label3.TabIndex = 20;
            this.Label3.Text = "Minimum No. Of Digits :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDigits
            // 
            this.txtDigits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDigits.ForeColor = System.Drawing.Color.Black;
            this.txtDigits.Location = new System.Drawing.Point(214, 83);
            this.txtDigits.Name = "txtDigits";
            this.txtDigits.Size = new System.Drawing.Size(168, 22);
            this.txtDigits.TabIndex = 2;
            this.txtDigits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(67, 55);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(145, 14);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "Minimum No. Of Letters :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLetters
            // 
            this.txtLetters.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLetters.ForeColor = System.Drawing.Color.Black;
            this.txtLetters.Location = new System.Drawing.Point(214, 49);
            this.txtLetters.Name = "txtLetters";
            this.txtLetters.Size = new System.Drawing.Size(168, 22);
            this.txtLetters.TabIndex = 1;
            this.txtLetters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // txtCapLetters
            // 
            this.txtCapLetters.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapLetters.ForeColor = System.Drawing.Color.Black;
            this.txtCapLetters.Location = new System.Drawing.Point(214, 15);
            this.txtCapLetters.Name = "txtCapLetters";
            this.txtCapLetters.Size = new System.Drawing.Size(168, 22);
            this.txtCapLetters.TabIndex = 0;
            this.txtCapLetters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(28, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(184, 14);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Minimum No. Of Capital Letters :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(95, 191);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(117, 14);
            this.Label6.TabIndex = 23;
            this.Label6.Text = "TimeFrame in Days :";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtdays
            // 
            this.txtdays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdays.ForeColor = System.Drawing.Color.Black;
            this.txtdays.Location = new System.Drawing.Point(214, 185);
            this.txtdays.Name = "txtdays";
            this.txtdays.Size = new System.Drawing.Size(168, 22);
            this.txtdays.TabIndex = 5;
            this.txtdays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapLetters_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtMinLength);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtSpecialChar);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.txtdays);
            this.panel1.Controls.Add(this.txtDigits);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.txtCapLetters);
            this.panel1.Controls.Add(this.txtLetters);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(409, 226);
            this.panel1.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(405, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 218);
            this.label12.TabIndex = 28;
            this.label12.Text = "label12";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 218);
            this.label10.TabIndex = 27;
            this.label10.Text = "label10";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(3, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(403, 1);
            this.label9.TabIndex = 26;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(403, 1);
            this.label8.TabIndex = 25;
            this.label8.Text = "label8";
            // 
            // frmPwdSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(409, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPwdSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Settings";
            this.Load += new System.EventHandler(this.frmPwdSettings_Load);
            this.pnl_tlsStrip.ResumeLayout(false);
            this.pnl_tlsStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtMinLength;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtSpecialChar;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtDigits;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtLetters;
        internal System.Windows.Forms.TextBox txtCapLetters;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtdays;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}