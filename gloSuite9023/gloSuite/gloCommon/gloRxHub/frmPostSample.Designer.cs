namespace gloRxHub
{
    partial class frmSamplePost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSamplePost));
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.rbType4 = new System.Windows.Forms.RadioButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Button3 = new System.Windows.Forms.Button();
            this.rbType3 = new System.Windows.Forms.RadioButton();
            this.rbType2 = new System.Windows.Forms.RadioButton();
            this.rbType1 = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.Button2);
            this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox3.Location = new System.Drawing.Point(4, 179);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(566, 68);
            this.GroupBox3.TabIndex = 10;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Post RxHistoryRequest";
            // 
            // Button2
            // 
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button2.Location = new System.Drawing.Point(127, 33);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(204, 29);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Post RxHistoryRequest";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // rbType4
            // 
            this.rbType4.AutoSize = true;
            this.rbType4.Checked = true;
            this.rbType4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbType4.Location = new System.Drawing.Point(33, 27);
            this.rbType4.Name = "rbType4";
            this.rbType4.Size = new System.Drawing.Size(81, 18);
            this.rbType4.TabIndex = 6;
            this.rbType4.TabStop = true;
            this.rbType4.Text = "WebClient";
            this.rbType4.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.rbType4);
            this.GroupBox2.Controls.Add(this.Button3);
            this.GroupBox2.Controls.Add(this.rbType3);
            this.GroupBox2.Controls.Add(this.rbType2);
            this.GroupBox2.Controls.Add(this.rbType1);
            this.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.Location = new System.Drawing.Point(4, 72);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(566, 107);
            this.GroupBox2.TabIndex = 10;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Post EDI with URLEncode";
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button3.BackgroundImage")));
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button3.Location = new System.Drawing.Point(127, 60);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(204, 27);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "Post EDI with URLEncode";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // rbType3
            // 
            this.rbType3.AutoSize = true;
            this.rbType3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbType3.Location = new System.Drawing.Point(314, 27);
            this.rbType3.Name = "rbType3";
            this.rbType3.Size = new System.Drawing.Size(109, 18);
            this.rbType3.TabIndex = 5;
            this.rbType3.Text = "ASCII Encoding";
            this.rbType3.UseVisualStyleBackColor = true;
            this.rbType3.Visible = false;
            // 
            // rbType2
            // 
            this.rbType2.AutoSize = true;
            this.rbType2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbType2.Location = new System.Drawing.Point(147, 27);
            this.rbType2.Name = "rbType2";
            this.rbType2.Size = new System.Drawing.Size(113, 18);
            this.rbType2.TabIndex = 4;
            this.rbType2.Text = "Encoding(1252)";
            this.rbType2.UseVisualStyleBackColor = true;
            this.rbType2.Visible = false;
            // 
            // rbType1
            // 
            this.rbType1.AutoSize = true;
            this.rbType1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbType1.Location = new System.Drawing.Point(461, 68);
            this.rbType1.Name = "rbType1";
            this.rbType1.Size = new System.Drawing.Size(54, 18);
            this.rbType1.TabIndex = 3;
            this.rbType1.Text = "UTF8";
            this.rbType1.UseVisualStyleBackColor = true;
            this.rbType1.Visible = false;
            this.rbType1.CheckedChanged += new System.EventHandler(this.rbType1_CheckedChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.Location = new System.Drawing.Point(4, 4);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.GroupBox1.Size = new System.Drawing.Size(566, 68);
            this.GroupBox1.TabIndex = 9;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Post EDI ";
            this.GroupBox1.Visible = false;
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button1.BackgroundImage")));
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(127, 24);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(204, 27);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "Post EDI";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 247);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(566, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 244);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(570, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 244);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(568, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.GroupBox3);
            this.Panel2.Controls.Add(this.GroupBox2);
            this.Panel2.Controls.Add(this.GroupBox1);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(574, 251);
            this.Panel2.TabIndex = 21;
            // 
            // frmSamplePost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(574, 251);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSamplePost";
            this.ShowInTaskbar = false;
            this.Text = "Post Sample";
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.RadioButton rbType4;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.RadioButton rbType3;
        internal System.Windows.Forms.RadioButton rbType2;
        internal System.Windows.Forms.RadioButton rbType1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Panel Panel2;
    }
}