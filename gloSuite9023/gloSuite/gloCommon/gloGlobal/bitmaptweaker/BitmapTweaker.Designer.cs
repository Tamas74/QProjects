namespace bitmaptweaker
{
    partial class BitmapTweaker
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
                if (myWatcher != null)
                {
                    myWatcher.Stop();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BitmapTweaker));
            this.dlgBitmapTweaker = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileSelection = new System.Windows.Forms.TextBox();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rdBorder = new System.Windows.Forms.RadioButton();
            this.rdTweaker = new System.Windows.Forms.RadioButton();
            this.pnl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBottom = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Main = new System.Windows.Forms.ToolStrip();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.pnl.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select an Image to Tweak :";
            // 
            // txtFileSelection
            // 
            this.txtFileSelection.Location = new System.Drawing.Point(186, 19);
            this.txtFileSelection.Name = "txtFileSelection";
            this.txtFileSelection.Size = new System.Drawing.Size(415, 22);
            this.txtFileSelection.TabIndex = 2;
            this.txtFileSelection.Click += new System.EventHandler(this.txtFileSelection_Click);
            // 
            // picOriginal
            // 
            this.picOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picOriginal.Location = new System.Drawing.Point(0, 0);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(288, 168);
            this.picOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOriginal.TabIndex = 2;
            this.picOriginal.TabStop = false;
            // 
            // picResult
            // 
            this.picResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picResult.Location = new System.Drawing.Point(0, 0);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(288, 168);
            this.picResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResult.TabIndex = 3;
            this.picResult.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(123, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Original Picture";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(431, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tweaked Picture";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.Location = new System.Drawing.Point(605, 18);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 24);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.txtFileSelection_Click);
            // 
            // rdBorder
            // 
            this.rdBorder.AutoSize = true;
            this.rdBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this.rdBorder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBorder.Location = new System.Drawing.Point(111, 0);
            this.rdBorder.Margin = new System.Windows.Forms.Padding(2);
            this.rdBorder.Name = "rdBorder";
            this.rdBorder.Size = new System.Drawing.Size(93, 26);
            this.rdBorder.TabIndex = 1;
            this.rdBorder.Text = "Draw Border";
            this.rdBorder.UseVisualStyleBackColor = true;
            this.rdBorder.CheckedChanged += new System.EventHandler(this.rdBorder_CheckedChanged);
            // 
            // rdTweaker
            // 
            this.rdTweaker.AutoSize = true;
            this.rdTweaker.Checked = true;
            this.rdTweaker.Dock = System.Windows.Forms.DockStyle.Left;
            this.rdTweaker.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTweaker.Location = new System.Drawing.Point(0, 0);
            this.rdTweaker.Margin = new System.Windows.Forms.Padding(2);
            this.rdTweaker.Name = "rdTweaker";
            this.rdTweaker.Size = new System.Drawing.Size(76, 26);
            this.rdTweaker.TabIndex = 0;
            this.rdTweaker.TabStop = true;
            this.rdTweaker.Text = "Tweaker";
            this.rdTweaker.UseVisualStyleBackColor = true;
            this.rdTweaker.CheckedChanged += new System.EventHandler(this.rdTweaker_CheckedChanged);
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.Transparent;
            this.pnl.Controls.Add(this.panel3);
            this.pnl.Controls.Add(this.panel2);
            this.pnl.Controls.Add(this.panel1);
            this.pnl.Controls.Add(this.lblBottom);
            this.pnl.Controls.Add(this.label3);
            this.pnl.Controls.Add(this.btnBrowse);
            this.pnl.Controls.Add(this.label2);
            this.pnl.Controls.Add(this.lblTop);
            this.pnl.Controls.Add(this.lblLeft);
            this.pnl.Controls.Add(this.lblRight);
            this.pnl.Controls.Add(this.txtFileSelection);
            this.pnl.Controls.Add(this.label12);
            this.pnl.Controls.Add(this.label1);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 56);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(3);
            this.pnl.Size = new System.Drawing.Size(653, 307);
            this.pnl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.picResult);
            this.panel2.Location = new System.Drawing.Point(342, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 168);
            this.panel2.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(287, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 166);
            this.label8.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 166);
            this.label9.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(288, 1);
            this.label10.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(288, 1);
            this.label11.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.picOriginal);
            this.panel1.Location = new System.Drawing.Point(24, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 168);
            this.panel1.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(287, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 166);
            this.label7.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 166);
            this.label6.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 1);
            this.label5.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(288, 1);
            this.label4.TabIndex = 7;
            // 
            // lblBottom
            // 
            this.lblBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottom.Location = new System.Drawing.Point(4, 303);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(645, 1);
            this.lblBottom.TabIndex = 12;
            this.lblBottom.Text = "label1";
            // 
            // lblTop
            // 
            this.lblTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTop.Location = new System.Drawing.Point(4, 3);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(645, 1);
            this.lblTop.TabIndex = 11;
            this.lblTop.Text = "label1";
            // 
            // lblLeft
            // 
            this.lblLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Location = new System.Drawing.Point(3, 3);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(1, 301);
            this.lblLeft.TabIndex = 10;
            this.lblLeft.Text = "label1";
            // 
            // lblRight
            // 
            this.lblRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRight.Location = new System.Drawing.Point(649, 3);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(1, 301);
            this.lblRight.TabIndex = 9;
            this.lblRight.Text = "label1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(162, 14);
            this.label12.TabIndex = 0;
            this.label12.Text = "Select a Method to Tweak :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdBorder);
            this.panel3.Controls.Add(this.rdTweaker);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(188, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(204, 26);
            this.panel3.TabIndex = 14;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(653, 56);
            this.pnlToolstrip.TabIndex = 27;
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOK,
            this.btnCancel});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(653, 56);
            this.tls_Main.TabIndex = 3;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 53);
            this.btnOK.Tag = "SaveCls";
            this.btnOK.Text = "&Tweak&&Save";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Tweak and Save";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(43, 53);
            this.btnCancel.Tag = "Close";
            this.btnCancel.Text = "&Close";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.ToolTipText = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BitmapTweaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(653, 363);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BitmapTweaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitmap Tweaker";
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlToolstrip.ResumeLayout(false);
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgBitmapTweaker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileSelection;
        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton rdBorder;
        private System.Windows.Forms.RadioButton rdTweaker;
        internal System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblBottom;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Panel pnlToolstrip;
        internal System.Windows.Forms.ToolStrip tls_Main;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton btnCancel;
    }
}

