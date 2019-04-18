namespace gloEmdeonInterface.Forms
{
    partial class FrmMidmarkECGTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMidmarkECGTest));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlbbtnclose = new System.Windows.Forms.Button();
            this.lblMainMsg = new System.Windows.Forms.Label();
            this.LblToolMsg = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.MainPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainPanel.BackgroundImage")));
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.lblMainMsg);
            this.MainPanel.Controls.Add(this.LblToolMsg);
            this.MainPanel.Controls.Add(this.pictureBox2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(542, 105);
            this.MainPanel.TabIndex = 49;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tlbbtnclose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(513, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(27, 103);
            this.panel1.TabIndex = 12;
            // 
            // tlbbtnclose
            // 
            this.tlbbtnclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbbtnclose.BackgroundImage")));
            this.tlbbtnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbbtnclose.FlatAppearance.BorderSize = 0;
            this.tlbbtnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.tlbbtnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.tlbbtnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tlbbtnclose.Location = new System.Drawing.Point(6, 3);
            this.tlbbtnclose.Name = "tlbbtnclose";
            this.tlbbtnclose.Size = new System.Drawing.Size(16, 16);
            this.tlbbtnclose.TabIndex = 11;
            this.toolTip1.SetToolTip(this.tlbbtnclose, "Close");
            this.tlbbtnclose.UseVisualStyleBackColor = true;
            this.tlbbtnclose.Click += new System.EventHandler(this.tlbbtnclose_Click);
            // 
            // lblMainMsg
            // 
            this.lblMainMsg.AutoSize = true;
            this.lblMainMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMainMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblMainMsg.Location = new System.Drawing.Point(76, 16);
            this.lblMainMsg.Name = "lblMainMsg";
            this.lblMainMsg.Size = new System.Drawing.Size(323, 19);
            this.lblMainMsg.TabIndex = 10;
            this.lblMainMsg.Text = "Connecting To Midmark IQ ECG Device\r\n";
            // 
            // LblToolMsg
            // 
            this.LblToolMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblToolMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblToolMsg.Location = new System.Drawing.Point(76, 41);
            this.LblToolMsg.Name = "LblToolMsg";
            this.LblToolMsg.Size = new System.Drawing.Size(428, 51);
            this.LblToolMsg.TabIndex = 9;
            this.LblToolMsg.Text = "Please Wait....................................................";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(12, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // FrmMidmarkECGTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(542, 105);
            this.ControlBox = false;
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMidmarkECGTest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Midmark IQ ECG Test";
            this.Load += new System.EventHandler(this.FrmMidmarkECGTest_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button tlbbtnclose;
        private System.Windows.Forms.Label lblMainMsg;
        private System.Windows.Forms.Label LblToolMsg;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
    }
}