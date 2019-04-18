namespace gloEmdeonInterface.Forms
{
    partial class frmConfirmInternetConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfirmInternetConnection));
            this.pnlConnectionFailed = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ConnectionFailed = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlInternetFailed = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_InternetFailed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlConnectionFailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlInternetFailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlConnectionFailed
            // 
            this.pnlConnectionFailed.BackColor = System.Drawing.Color.White;
            this.pnlConnectionFailed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlConnectionFailed.BackgroundImage")));
            this.pnlConnectionFailed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlConnectionFailed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConnectionFailed.Controls.Add(this.label14);
            this.pnlConnectionFailed.Controls.Add(this.pictureBox1);
            this.pnlConnectionFailed.Controls.Add(this.btn_ConnectionFailed);
            this.pnlConnectionFailed.Controls.Add(this.label16);
            this.pnlConnectionFailed.Location = new System.Drawing.Point(0, 0);
            this.pnlConnectionFailed.Name = "pnlConnectionFailed";
            this.pnlConnectionFailed.Size = new System.Drawing.Size(366, 126);
            this.pnlConnectionFailed.TabIndex = 25;
            this.pnlConnectionFailed.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(82, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(216, 19);
            this.label14.TabIndex = 8;
            this.label14.Text = "Secure Connection failed.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(16, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btn_ConnectionFailed
            // 
            this.btn_ConnectionFailed.BackColor = System.Drawing.Color.Transparent;
            this.btn_ConnectionFailed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ConnectionFailed.BackgroundImage")));
            this.btn_ConnectionFailed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ConnectionFailed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConnectionFailed.Location = new System.Drawing.Point(304, 88);
            this.btn_ConnectionFailed.Name = "btn_ConnectionFailed";
            this.btn_ConnectionFailed.Size = new System.Drawing.Size(47, 26);
            this.btn_ConnectionFailed.TabIndex = 1;
            this.btn_ConnectionFailed.Text = "OK";
            this.btn_ConnectionFailed.UseVisualStyleBackColor = false;
            this.btn_ConnectionFailed.Click += new System.EventHandler(this.btn_ConnectionFailed_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(83, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(232, 28);
            this.label16.TabIndex = 0;
            this.label16.Text = "Server is not responding at this moment.\r\nPlease try again later.";
            // 
            // pnlInternetFailed
            // 
            this.pnlInternetFailed.BackColor = System.Drawing.Color.White;
            this.pnlInternetFailed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlInternetFailed.BackgroundImage")));
            this.pnlInternetFailed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInternetFailed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInternetFailed.Controls.Add(this.label1);
            this.pnlInternetFailed.Controls.Add(this.pictureBox2);
            this.pnlInternetFailed.Controls.Add(this.btn_InternetFailed);
            this.pnlInternetFailed.Controls.Add(this.label2);
            this.pnlInternetFailed.Location = new System.Drawing.Point(0, 0);
            this.pnlInternetFailed.Name = "pnlInternetFailed";
            this.pnlInternetFailed.Size = new System.Drawing.Size(366, 126);
            this.pnlInternetFailed.TabIndex = 26;
            this.pnlInternetFailed.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(75, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Internet connection not available.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(16, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // btn_InternetFailed
            // 
            this.btn_InternetFailed.BackColor = System.Drawing.Color.Transparent;
            this.btn_InternetFailed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_InternetFailed.BackgroundImage")));
            this.btn_InternetFailed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_InternetFailed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_InternetFailed.Location = new System.Drawing.Point(304, 88);
            this.btn_InternetFailed.Name = "btn_InternetFailed";
            this.btn_InternetFailed.Size = new System.Drawing.Size(47, 26);
            this.btn_InternetFailed.TabIndex = 1;
            this.btn_InternetFailed.Text = "OK";
            this.btn_InternetFailed.UseVisualStyleBackColor = false;
            this.btn_InternetFailed.Click += new System.EventHandler(this.btn_InternetFailed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "You must be connected to the Internet \r\nto access Lab orders.\r\n";
            // 
            // frmConfirmInternetConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 126);
            this.Controls.Add(this.pnlInternetFailed);
            this.Controls.Add(this.pnlConnectionFailed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfirmInternetConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmConfirmInternetConnection";
            this.Load += new System.EventHandler(this.frmConfirmInternetConnection_Load);
            this.pnlConnectionFailed.ResumeLayout(false);
            this.pnlConnectionFailed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlInternetFailed.ResumeLayout(false);
            this.pnlInternetFailed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlConnectionFailed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_ConnectionFailed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pnlInternetFailed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn_InternetFailed;
        private System.Windows.Forms.Label label2;
    }
}