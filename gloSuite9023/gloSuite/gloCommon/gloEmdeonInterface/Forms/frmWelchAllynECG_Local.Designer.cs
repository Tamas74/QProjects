namespace gloEmdeonInterface.Forms
{
    partial class frmWelchAllynECG_Local
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelchAllynECG_Local));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_InternetFailed = new System.Windows.Forms.Button();
            this.lblMainMsg = new System.Windows.Forms.Label();
            this.LblToolMsg = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btn_InternetFailed);
            this.panel1.Controls.Add(this.lblMainMsg);
            this.panel1.Controls.Add(this.LblToolMsg);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 146);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(91, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 36);
            this.panel2.TabIndex = 12;
            // 
            // btn_InternetFailed
            // 
            this.btn_InternetFailed.BackColor = System.Drawing.Color.Transparent;
            this.btn_InternetFailed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_InternetFailed.BackgroundImage")));
            this.btn_InternetFailed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_InternetFailed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_InternetFailed.Location = new System.Drawing.Point(394, 95);
            this.btn_InternetFailed.Name = "btn_InternetFailed";
            this.btn_InternetFailed.Size = new System.Drawing.Size(92, 25);
            this.btn_InternetFailed.TabIndex = 11;
            this.btn_InternetFailed.Text = "Disconnect";
            this.btn_InternetFailed.UseVisualStyleBackColor = false;
            this.btn_InternetFailed.Click += new System.EventHandler(this.btn_InternetFailed_Click);
            // 
            // lblMainMsg
            // 
            this.lblMainMsg.AutoSize = true;
            this.lblMainMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMainMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMainMsg.Location = new System.Drawing.Point(91, 20);
            this.lblMainMsg.Name = "lblMainMsg";
            this.lblMainMsg.Size = new System.Drawing.Size(292, 19);
            this.lblMainMsg.TabIndex = 10;
            this.lblMainMsg.Text = "Connecting WelchAllyn ECG Device\r\n";
            // 
            // LblToolMsg
            // 
            this.LblToolMsg.AutoSize = true;
            this.LblToolMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblToolMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblToolMsg.Location = new System.Drawing.Point(93, 50);
            this.LblToolMsg.Name = "LblToolMsg";
            this.LblToolMsg.Size = new System.Drawing.Size(166, 14);
            this.LblToolMsg.TabIndex = 9;
            this.LblToolMsg.Text = "Please Wait........................";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(17, 48);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // frmWelchAllynECG_Local
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(528, 146);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWelchAllynECG_Local";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWelchAllynECG_Local";
            this.Load += new System.EventHandler(this.frmWelchAllynECG_Local_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_InternetFailed;
        private System.Windows.Forms.Label lblMainMsg;
        private System.Windows.Forms.Label LblToolMsg;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}