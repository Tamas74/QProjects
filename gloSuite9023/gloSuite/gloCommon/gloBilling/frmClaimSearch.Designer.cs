namespace gloBilling
{
    partial class frmClaimSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClaimSearch));
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSelectedClaim = new System.Windows.Forms.ComboBox();
            this.numQueueClaimCount = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.rbClaimByClaimNo = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbClaimByPosition = new System.Windows.Forms.RadioButton();
            this.rbHighlightedClaim = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_SaveAndCloseMod,
            this.tls_CloseMod});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(406, 53);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tls_SaveAndCloseMod
            // 
            this.tls_SaveAndCloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveAndCloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveAndCloseMod.Image")));
            this.tls_SaveAndCloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveAndCloseMod.Name = "tls_SaveAndCloseMod";
            this.tls_SaveAndCloseMod.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveAndCloseMod.Text = "&Save&&Cls";
            this.tls_SaveAndCloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveAndCloseMod.ToolTipText = "Save and Close";
            this.tls_SaveAndCloseMod.Click += new System.EventHandler(this.tls_SaveAndCloseMod_Click);
            // 
            // tls_CloseMod
            // 
            this.tls_CloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_CloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_CloseMod.Image")));
            this.tls_CloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_CloseMod.Name = "tls_CloseMod";
            this.tls_CloseMod.Size = new System.Drawing.Size(43, 50);
            this.tls_CloseMod.Text = "&Close";
            this.tls_CloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_CloseMod.Click += new System.EventHandler(this.tls_CloseMod_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbSelectedClaim);
            this.panel1.Controls.Add(this.numQueueClaimCount);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.rbClaimByClaimNo);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.rbClaimByPosition);
            this.panel1.Controls.Add(this.rbHighlightedClaim);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(406, 141);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.label1.Size = new System.Drawing.Size(116, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Search Criteria";
            // 
            // cmbSelectedClaim
            // 
            this.cmbSelectedClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectedClaim.FormattingEnabled = true;
            this.cmbSelectedClaim.Location = new System.Drawing.Point(213, 35);
            this.cmbSelectedClaim.Name = "cmbSelectedClaim";
            this.cmbSelectedClaim.Size = new System.Drawing.Size(140, 22);
            this.cmbSelectedClaim.TabIndex = 2;
            this.cmbSelectedClaim.Leave += new System.EventHandler(this.cmbSelectedClaim_Leave);
            // 
            // numQueueClaimCount
            // 
            this.numQueueClaimCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.numQueueClaimCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQueueClaimCount.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numQueueClaimCount.Location = new System.Drawing.Point(213, 65);
            this.numQueueClaimCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQueueClaimCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQueueClaimCount.Name = "numQueueClaimCount";
            this.numQueueClaimCount.Size = new System.Drawing.Size(65, 22);
            this.numQueueClaimCount.TabIndex = 4;
            this.numQueueClaimCount.Tag = "Queue";
            this.numQueueClaimCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numQueueClaimCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQueueClaimCount.ValueChanged += new System.EventHandler(this.numQueueClaimCount_ValueChanged);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(402, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 133);
            this.label13.TabIndex = 3;
            // 
            // rbClaimByClaimNo
            // 
            this.rbClaimByClaimNo.AutoSize = true;
            this.rbClaimByClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClaimByClaimNo.Location = new System.Drawing.Point(41, 36);
            this.rbClaimByClaimNo.Name = "rbClaimByClaimNo";
            this.rbClaimByClaimNo.Size = new System.Drawing.Size(165, 18);
            this.rbClaimByClaimNo.TabIndex = 1;
            this.rbClaimByClaimNo.TabStop = true;
            this.rbClaimByClaimNo.Text = "Selected Claim by Claim #";
            this.rbClaimByClaimNo.UseVisualStyleBackColor = true;
            this.rbClaimByClaimNo.CheckedChanged += new System.EventHandler(this.rbClaimByClaimNo_CheckedChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 133);
            this.label14.TabIndex = 2;
            // 
            // rbClaimByPosition
            // 
            this.rbClaimByPosition.AutoSize = true;
            this.rbClaimByPosition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbClaimByPosition.Location = new System.Drawing.Point(41, 67);
            this.rbClaimByPosition.Name = "rbClaimByPosition";
            this.rbClaimByPosition.Size = new System.Drawing.Size(167, 18);
            this.rbClaimByPosition.TabIndex = 3;
            this.rbClaimByPosition.TabStop = true;
            this.rbClaimByPosition.Text = "Selected Claim by Position";
            this.rbClaimByPosition.UseVisualStyleBackColor = true;
            this.rbClaimByPosition.CheckedChanged += new System.EventHandler(this.rbClaimByPosition_CheckedChanged);
            // 
            // rbHighlightedClaim
            // 
            this.rbHighlightedClaim.AutoSize = true;
            this.rbHighlightedClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHighlightedClaim.Location = new System.Drawing.Point(41, 98);
            this.rbHighlightedClaim.Name = "rbHighlightedClaim";
            this.rbHighlightedClaim.Size = new System.Drawing.Size(117, 18);
            this.rbHighlightedClaim.TabIndex = 5;
            this.rbHighlightedClaim.TabStop = true;
            this.rbHighlightedClaim.Text = "Highlighted Claim";
            this.rbHighlightedClaim.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(3, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(400, 1);
            this.label15.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(400, 1);
            this.label16.TabIndex = 0;
            // 
            // frmClaimSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(406, 194);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClaimSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jump To";
            this.Load += new System.EventHandler(this.frmClaimSearch_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQueueClaimCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
        private System.Windows.Forms.ToolStripButton tls_SaveAndCloseMod;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton rbClaimByClaimNo;
        private System.Windows.Forms.RadioButton rbClaimByPosition;
        private System.Windows.Forms.NumericUpDown numQueueClaimCount;
        private System.Windows.Forms.ComboBox cmbSelectedClaim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbHighlightedClaim;
    }
}