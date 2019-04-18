namespace SampleLibrary
{
    partial class DataPathInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataPathInfo));
            this.lblPrerequisistesPath = new System.Windows.Forms.Label();
            this.txtPrerequisitesPath = new System.Windows.Forms.TextBox();
            this.rbtVoice = new System.Windows.Forms.RadioButton();
            this.rbtNonVoice = new System.Windows.Forms.RadioButton();
            this.lblFeature = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrerequisistesPath
            // 
            this.lblPrerequisistesPath.AutoSize = true;
            this.lblPrerequisistesPath.Location = new System.Drawing.Point(82, 168);
            this.lblPrerequisistesPath.Name = "lblPrerequisistesPath";
            this.lblPrerequisistesPath.Size = new System.Drawing.Size(95, 13);
            this.lblPrerequisistesPath.TabIndex = 0;
            this.lblPrerequisistesPath.Text = "Prerequisites Path:";
            // 
            // txtPrerequisitesPath
            // 
            this.txtPrerequisitesPath.Location = new System.Drawing.Point(182, 165);
            this.txtPrerequisitesPath.Name = "txtPrerequisitesPath";
            this.txtPrerequisitesPath.Size = new System.Drawing.Size(231, 20);
            this.txtPrerequisitesPath.TabIndex = 1;
            // 
            // rbtVoice
            // 
            this.rbtVoice.AutoSize = true;
            this.rbtVoice.Location = new System.Drawing.Point(16, 4);
            this.rbtVoice.Name = "rbtVoice";
            this.rbtVoice.Size = new System.Drawing.Size(52, 17);
            this.rbtVoice.TabIndex = 2;
            this.rbtVoice.TabStop = true;
            this.rbtVoice.Text = "Voice";
            this.rbtVoice.UseVisualStyleBackColor = true;
            // 
            // rbtNonVoice
            // 
            this.rbtNonVoice.AutoSize = true;
            this.rbtNonVoice.Location = new System.Drawing.Point(97, 4);
            this.rbtNonVoice.Name = "rbtNonVoice";
            this.rbtNonVoice.Size = new System.Drawing.Size(75, 17);
            this.rbtNonVoice.TabIndex = 3;
            this.rbtNonVoice.TabStop = true;
            this.rbtNonVoice.Text = "Non Voice";
            this.rbtNonVoice.UseVisualStyleBackColor = true;
            // 
            // lblFeature
            // 
            this.lblFeature.AutoSize = true;
            this.lblFeature.Location = new System.Drawing.Point(98, 197);
            this.lblFeature.Name = "lblFeature";
            this.lblFeature.Size = new System.Drawing.Size(79, 13);
            this.lblFeature.TabIndex = 4;
            this.lblFeature.Text = "Select Feature:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtVoice);
            this.panel1.Controls.Add(this.rbtNonVoice);
            this.panel1.Location = new System.Drawing.Point(182, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 24);
            this.panel1.TabIndex = 5;
            // 
            // btnSubmit
            // 
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(325, 344);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "&Install";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(407, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(494, 70);
            this.panel2.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(10, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "Installing gloEMR Client";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(494, 1);
            this.label6.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-11, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(516, 1);
            this.label5.TabIndex = 33;
            // 
            // DataPathInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 379);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblFeature);
            this.Controls.Add(this.txtPrerequisitesPath);
            this.Controls.Add(this.lblPrerequisistesPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataPathInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "gloEMR Client Install Information";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrerequisistesPath;
        private System.Windows.Forms.TextBox txtPrerequisitesPath;
        private System.Windows.Forms.RadioButton rbtVoice;
        private System.Windows.Forms.RadioButton rbtNonVoice;
        private System.Windows.Forms.Label lblFeature;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}