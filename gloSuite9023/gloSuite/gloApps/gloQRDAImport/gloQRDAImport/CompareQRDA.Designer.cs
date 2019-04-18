namespace gloQRDAImport
{
    partial class CompareQRDA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareQRDA));
            this.btnAddSourcePath = new System.Windows.Forms.Button();
            this.btnAddDestPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDestinationPath = new System.Windows.Forms.TextBox();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnCompare = new System.Windows.Forms.ToolStripButton();
            this.pnlflexgrid = new System.Windows.Forms.Panel();
            this.lblGridBottom = new System.Windows.Forms.Label();
            this.lblGridLeft = new System.Windows.Forms.Label();
            this.lblGridRight = new System.Windows.Forms.Label();
            this.lblGridTop = new System.Windows.Forms.Label();
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlflexgrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddSourcePath
            // 
            this.btnAddSourcePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSourcePath.BackgroundImage")));
            this.btnAddSourcePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddSourcePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSourcePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSourcePath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAddSourcePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddSourcePath.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSourcePath.Image")));
            this.btnAddSourcePath.Location = new System.Drawing.Point(616, 18);
            this.btnAddSourcePath.Name = "btnAddSourcePath";
            this.btnAddSourcePath.Size = new System.Drawing.Size(24, 24);
            this.btnAddSourcePath.TabIndex = 6;
            this.btnAddSourcePath.UseVisualStyleBackColor = true;
            this.btnAddSourcePath.Click += new System.EventHandler(this.btnAddSourcePath_Click);
            this.btnAddSourcePath.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnAddSourcePath.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // btnAddDestPath
            // 
            this.btnAddDestPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDestPath.BackgroundImage")));
            this.btnAddDestPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDestPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDestPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDestPath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAddDestPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddDestPath.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDestPath.Image")));
            this.btnAddDestPath.Location = new System.Drawing.Point(616, 49);
            this.btnAddDestPath.Name = "btnAddDestPath";
            this.btnAddDestPath.Size = new System.Drawing.Size(24, 24);
            this.btnAddDestPath.TabIndex = 5;
            this.btnAddDestPath.UseVisualStyleBackColor = true;
            this.btnAddDestPath.Click += new System.EventHandler(this.btnAddDestPath_Click);
            this.btnAddDestPath.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.btnAddDestPath.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(10, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination File :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(33, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source File :";
            // 
            // txtDestinationPath
            // 
            this.txtDestinationPath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtDestinationPath.Location = new System.Drawing.Point(110, 50);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.Size = new System.Drawing.Size(501, 22);
            this.txtDestinationPath.TabIndex = 1;
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSourcePath.Location = new System.Drawing.Point(110, 19);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(501, 22);
            this.txtSourcePath.TabIndex = 0;
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(669, 56);
            this.pnlToolstrip.TabIndex = 7;
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCompare});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(669, 56);
            this.tls_Main.TabIndex = 3;
            this.tls_Main.TabStop = true;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // btnCompare
            // 
            this.btnCompare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompare.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCompare.Image = ((System.Drawing.Image)(resources.GetObject("btnCompare.Image")));
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(101, 53);
            this.btnCompare.Tag = "Start Compare";
            this.btnCompare.Text = "&Start Compare";
            this.btnCompare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCompare.ToolTipText = "Start Compare";
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // pnlflexgrid
            // 
            this.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlflexgrid.Controls.Add(this.lblGridBottom);
            this.pnlflexgrid.Controls.Add(this.lblGridLeft);
            this.pnlflexgrid.Controls.Add(this.btnAddSourcePath);
            this.pnlflexgrid.Controls.Add(this.lblGridRight);
            this.pnlflexgrid.Controls.Add(this.txtSourcePath);
            this.pnlflexgrid.Controls.Add(this.lblGridTop);
            this.pnlflexgrid.Controls.Add(this.btnAddDestPath);
            this.pnlflexgrid.Controls.Add(this.txtDestinationPath);
            this.pnlflexgrid.Controls.Add(this.label1);
            this.pnlflexgrid.Controls.Add(this.label2);
            this.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlflexgrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlflexgrid.Location = new System.Drawing.Point(0, 56);
            this.pnlflexgrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlflexgrid.Name = "pnlflexgrid";
            this.pnlflexgrid.Padding = new System.Windows.Forms.Padding(3);
            this.pnlflexgrid.Size = new System.Drawing.Size(669, 94);
            this.pnlflexgrid.TabIndex = 15;
            // 
            // lblGridBottom
            // 
            this.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGridBottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridBottom.Location = new System.Drawing.Point(4, 90);
            this.lblGridBottom.Name = "lblGridBottom";
            this.lblGridBottom.Size = new System.Drawing.Size(661, 1);
            this.lblGridBottom.TabIndex = 10;
            this.lblGridBottom.Text = "label2";
            // 
            // lblGridLeft
            // 
            this.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGridLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridLeft.Location = new System.Drawing.Point(3, 4);
            this.lblGridLeft.Name = "lblGridLeft";
            this.lblGridLeft.Size = new System.Drawing.Size(1, 87);
            this.lblGridLeft.TabIndex = 9;
            this.lblGridLeft.Text = "label4";
            // 
            // lblGridRight
            // 
            this.lblGridRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGridRight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridRight.Location = new System.Drawing.Point(665, 4);
            this.lblGridRight.Name = "lblGridRight";
            this.lblGridRight.Size = new System.Drawing.Size(1, 87);
            this.lblGridRight.TabIndex = 8;
            this.lblGridRight.Text = "label3";
            // 
            // lblGridTop
            // 
            this.lblGridTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridTop.Location = new System.Drawing.Point(3, 3);
            this.lblGridTop.Name = "lblGridTop";
            this.lblGridTop.Size = new System.Drawing.Size(663, 1);
            this.lblGridTop.TabIndex = 7;
            this.lblGridTop.Text = "label1";
            // 
            // CompareQRDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(669, 150);
            this.Controls.Add(this.pnlflexgrid);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CompareQRDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compare QRDA File";
            this.pnlToolstrip.ResumeLayout(false);
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlflexgrid.ResumeLayout(false);
            this.pnlflexgrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDestinationPath;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button btnAddDestPath;
        private System.Windows.Forms.Button btnAddSourcePath;
        internal System.Windows.Forms.Panel pnlToolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_Main;
        internal System.Windows.Forms.ToolStripButton btnCompare;
        internal System.Windows.Forms.Panel pnlflexgrid;
        private System.Windows.Forms.Label lblGridBottom;
        private System.Windows.Forms.Label lblGridLeft;
        private System.Windows.Forms.Label lblGridRight;
        private System.Windows.Forms.Label lblGridTop;
    }
}