namespace gloEmdeonInterface.Forms
{
    partial class FrmWelChallynECG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWelChallynECG));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_InternetFailed = new System.Windows.Forms.Button();
            this.lblMainMsg = new System.Windows.Forms.Label();
            this.LblToolMsg = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbBar = new System.Windows.Forms.ProgressBar();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.lblGetAllTest = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.panel1.TabIndex = 2;
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
            // pbBar
            // 
            this.pbBar.Location = new System.Drawing.Point(93, 104);
            this.pbBar.Name = "pbBar";
            this.pbBar.Size = new System.Drawing.Size(383, 22);
            this.pbBar.TabIndex = 15;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            // 
            // lblGetAllTest
            // 
            this.lblGetAllTest.AutoSize = true;
            this.lblGetAllTest.BackColor = System.Drawing.Color.Transparent;
            this.lblGetAllTest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGetAllTest.Location = new System.Drawing.Point(93, 82);
            this.lblGetAllTest.Name = "lblGetAllTest";
            this.lblGetAllTest.Size = new System.Drawing.Size(149, 14);
            this.lblGetAllTest.TabIndex = 19;
            this.lblGetAllTest.Text = "Importing Test 0 out of 0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(17, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(91, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "Connected to WelchAllyn ECG Device.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "Please Wait........................";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblGetAllTest);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.pbBar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(528, 146);
            this.panel3.TabIndex = 16;
            // 
            // FrmWelChallynECG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(528, 146);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmWelChallynECG";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmWelChallynECG";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWelChallynECG_FormClosing);
            this.Load += new System.EventHandler(this.FrmWelChallynECG_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblMainMsg;
        private System.Windows.Forms.Label LblToolMsg;
        private System.Windows.Forms.Button btn_InternetFailed;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar pbBar;
        private System.Windows.Forms.Label lblGetAllTest;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
    }
}