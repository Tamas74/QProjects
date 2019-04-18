namespace gloReports
{
    partial class frmDeleteOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteOptions));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_Settings = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_DeleteLog = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.RecordCount = new System.Windows.Forms.NumericUpDown();
            this.rb_DeleteSelected = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.rb_DeleteAll = new System.Windows.Forms.RadioButton();
            this.pnlToolStrip.SuspendLayout();
            this.tlsp_Settings.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tlsp_Settings);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(349, 54);
            this.pnlToolStrip.TabIndex = 16;
            // 
            // tlsp_Settings
            // 
            this.tlsp_Settings.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_Settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_Settings.BackgroundImage")));
            this.tlsp_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Settings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Settings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Settings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_DeleteLog,
            this.ts_btnCancel});
            this.tlsp_Settings.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Settings.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Settings.Name = "tlsp_Settings";
            this.tlsp_Settings.Size = new System.Drawing.Size(349, 53);
            this.tlsp_Settings.TabIndex = 24;
            this.tlsp_Settings.TabStop = true;
            this.tlsp_Settings.Text = "toolStrip1";
            // 
            // tlsp_DeleteLog
            // 
            this.tlsp_DeleteLog.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_DeleteLog.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_DeleteLog.Image")));
            this.tlsp_DeleteLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_DeleteLog.Name = "tlsp_DeleteLog";
            this.tlsp_DeleteLog.Size = new System.Drawing.Size(77, 50);
            this.tlsp_DeleteLog.Text = "Delete Log";
            this.tlsp_DeleteLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_DeleteLog.Click += new System.EventHandler(this.tlsp_DeleteLog_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Close";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.ts_btnCancel_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.lblLogCount);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label10);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.RecordCount);
            this.Panel1.Controls.Add(this.rb_DeleteSelected);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.rb_DeleteAll);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 54);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(349, 135);
            this.Panel1.TabIndex = 17;
            // 
            // lblLogCount
            // 
            this.lblLogCount.AutoSize = true;
            this.lblLogCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogCount.Location = new System.Drawing.Point(194, 99);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(15, 14);
            this.lblLogCount.TabIndex = 13;
            this.lblLogCount.Text = "0";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(12, 99);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(181, 14);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "Current Database Log Records :";
            // 
            // Label9
            // 
            this.Label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(4, 4);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(341, 31);
            this.Label9.TabIndex = 4;
            this.Label9.Text = "  Select Number of Records to Delete";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label4.Location = new System.Drawing.Point(345, 4);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 127);
            this.Label4.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label3.Location = new System.Drawing.Point(3, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 127);
            this.Label3.TabIndex = 2;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(273, 69);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(47, 14);
            this.Label10.TabIndex = 11;
            this.Label10.Text = "records";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label2.Location = new System.Drawing.Point(3, 131);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(343, 1);
            this.Label2.TabIndex = 1;
            // 
            // RecordCount
            // 
            this.RecordCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordCount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.RecordCount.Location = new System.Drawing.Point(146, 65);
            this.RecordCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RecordCount.Name = "RecordCount";
            this.RecordCount.Size = new System.Drawing.Size(122, 22);
            this.RecordCount.TabIndex = 9;
            this.RecordCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.RecordCount.ValueChanged += new System.EventHandler(this.RecordCount_ValueChanged);
            this.RecordCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RecordCount_KeyUp);
            this.RecordCount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RecordCount_MouseClick);
            // 
            // rb_DeleteSelected
            // 
            this.rb_DeleteSelected.AutoSize = true;
            this.rb_DeleteSelected.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_DeleteSelected.Location = new System.Drawing.Point(55, 67);
            this.rb_DeleteSelected.Name = "rb_DeleteSelected";
            this.rb_DeleteSelected.Size = new System.Drawing.Size(85, 18);
            this.rb_DeleteSelected.TabIndex = 8;
            this.rb_DeleteSelected.TabStop = true;
            this.rb_DeleteSelected.Text = "Delete old ";
            this.rb_DeleteSelected.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Location = new System.Drawing.Point(3, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(343, 1);
            this.Label1.TabIndex = 0;
            // 
            // rb_DeleteAll
            // 
            this.rb_DeleteAll.AutoSize = true;
            this.rb_DeleteAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_DeleteAll.Location = new System.Drawing.Point(55, 41);
            this.rb_DeleteAll.Name = "rb_DeleteAll";
            this.rb_DeleteAll.Size = new System.Drawing.Size(77, 18);
            this.rb_DeleteAll.TabIndex = 7;
            this.rb_DeleteAll.TabStop = true;
            this.rb_DeleteAll.Text = "Delete All";
            this.rb_DeleteAll.UseVisualStyleBackColor = true;
            // 
            // frmDeleteOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(349, 189);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDeleteOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Options";
            this.Load += new System.EventHandler(this.frmDeleteOptions_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tlsp_Settings.ResumeLayout(false);
            this.tlsp_Settings.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_Settings;
        internal System.Windows.Forms.ToolStripButton tlsp_DeleteLog;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label lblLogCount;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.NumericUpDown RecordCount;
        internal System.Windows.Forms.RadioButton rb_DeleteSelected;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.RadioButton rb_DeleteAll;
    }
}