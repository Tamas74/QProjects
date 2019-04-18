namespace gloEDocumentV3.Forms
{
    partial class frmScanProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanProgress));
            this.lblCurrentProgress = new System.Windows.Forms.Label();
            this.prbarScanning = new System.Windows.Forms.ProgressBar();
            this.pnlflexgrid = new System.Windows.Forms.Panel();
            this.lblGridBottom = new System.Windows.Forms.Label();
            this.lblGridLeft = new System.Windows.Forms.Label();
            this.lblGridRight = new System.Windows.Forms.Label();
            this.lblGridTop = new System.Windows.Forms.Label();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.Label107 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlflexgrid.SuspendLayout();
            this.pnlMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCurrentProgress
            // 
            this.lblCurrentProgress.AutoSize = true;
            this.lblCurrentProgress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentProgress.Location = new System.Drawing.Point(19, 47);
            this.lblCurrentProgress.Name = "lblCurrentProgress";
            this.lblCurrentProgress.Size = new System.Drawing.Size(99, 14);
            this.lblCurrentProgress.TabIndex = 0;
            this.lblCurrentProgress.Text = "Reading Image";
            this.lblCurrentProgress.UseWaitCursor = true;
            // 
            // prbarScanning
            // 
            this.prbarScanning.Location = new System.Drawing.Point(17, 73);
            this.prbarScanning.Name = "prbarScanning";
            this.prbarScanning.Size = new System.Drawing.Size(397, 23);
            this.prbarScanning.TabIndex = 1;
            this.prbarScanning.UseWaitCursor = true;
            // 
            // pnlflexgrid
            // 
            this.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlflexgrid.Controls.Add(this.pnlMid);
            this.pnlflexgrid.Controls.Add(this.lblGridBottom);
            this.pnlflexgrid.Controls.Add(this.prbarScanning);
            this.pnlflexgrid.Controls.Add(this.lblGridLeft);
            this.pnlflexgrid.Controls.Add(this.lblCurrentProgress);
            this.pnlflexgrid.Controls.Add(this.lblGridRight);
            this.pnlflexgrid.Controls.Add(this.lblGridTop);
            this.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlflexgrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlflexgrid.Location = new System.Drawing.Point(0, 0);
            this.pnlflexgrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlflexgrid.Name = "pnlflexgrid";
            this.pnlflexgrid.Size = new System.Drawing.Size(431, 111);
            this.pnlflexgrid.TabIndex = 15;
            // 
            // lblGridBottom
            // 
            this.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGridBottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridBottom.Location = new System.Drawing.Point(1, 110);
            this.lblGridBottom.Name = "lblGridBottom";
            this.lblGridBottom.Size = new System.Drawing.Size(429, 1);
            this.lblGridBottom.TabIndex = 10;
            this.lblGridBottom.Text = "label2";
            // 
            // lblGridLeft
            // 
            this.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGridLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridLeft.Location = new System.Drawing.Point(0, 1);
            this.lblGridLeft.Name = "lblGridLeft";
            this.lblGridLeft.Size = new System.Drawing.Size(1, 110);
            this.lblGridLeft.TabIndex = 9;
            this.lblGridLeft.Text = "label4";
            // 
            // lblGridRight
            // 
            this.lblGridRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGridRight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridRight.Location = new System.Drawing.Point(430, 1);
            this.lblGridRight.Name = "lblGridRight";
            this.lblGridRight.Size = new System.Drawing.Size(1, 110);
            this.lblGridRight.TabIndex = 8;
            this.lblGridRight.Text = "label3";
            // 
            // lblGridTop
            // 
            this.lblGridTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridTop.Location = new System.Drawing.Point(0, 0);
            this.lblGridTop.Name = "lblGridTop";
            this.lblGridTop.Size = new System.Drawing.Size(431, 1);
            this.lblGridTop.TabIndex = 7;
            this.lblGridTop.Text = "label1";
            // 
            // pnlMid
            // 
            this.pnlMid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlMid.BackgroundImage")));
            this.pnlMid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMid.Controls.Add(this.Label107);
            this.pnlMid.Controls.Add(this.lblHeader);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMid.Location = new System.Drawing.Point(1, 1);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Size = new System.Drawing.Size(429, 25);
            this.pnlMid.TabIndex = 45;
            // 
            // Label107
            // 
            this.Label107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label107.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label107.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label107.Location = new System.Drawing.Point(0, 24);
            this.Label107.Name = "Label107";
            this.Label107.Size = new System.Drawing.Size(429, 1);
            this.Label107.TabIndex = 13;
            this.Label107.Text = "label2";
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(429, 25);
            this.lblHeader.TabIndex = 9;
            this.lblHeader.Text = "  Scanning in Progress";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmScanProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(431, 111);
            this.Controls.Add(this.pnlflexgrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScanProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanning in Progress";
            this.UseWaitCursor = true;
            this.pnlflexgrid.ResumeLayout(false);
            this.pnlflexgrid.PerformLayout();
            this.pnlMid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblCurrentProgress;
        public System.Windows.Forms.ProgressBar prbarScanning;
        internal System.Windows.Forms.Panel pnlflexgrid;
        internal System.Windows.Forms.Panel pnlMid;
        private System.Windows.Forms.Label Label107;
        internal System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblGridBottom;
        private System.Windows.Forms.Label lblGridLeft;
        private System.Windows.Forms.Label lblGridRight;
        private System.Windows.Forms.Label lblGridTop;
    }
}