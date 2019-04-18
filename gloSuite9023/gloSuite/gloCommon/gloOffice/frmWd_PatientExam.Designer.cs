namespace gloOffice
{
    partial class frmWd_PatientExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWd_PatientExam));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.wdExam = new AxDSOFramer.AxFramerControl();
            this.pnlwdExam = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnltsCommands = new System.Windows.Forms.Panel();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.ts_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdExam)).BeginInit();
            this.pnlwdExam.SuspendLayout();
            this.pnltsCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Print,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(978, 53);
            this.ts_Commands.TabIndex = 12;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Close
            // 
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(51, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = " &Close ";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // wdExam
            // 
            this.wdExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdExam.Enabled = true;
            this.wdExam.Location = new System.Drawing.Point(4, 4);
            this.wdExam.Name = "wdExam";
            this.wdExam.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdExam.OcxState")));
            this.wdExam.Size = new System.Drawing.Size(970, 651);
            this.wdExam.TabIndex = 0;
            this.wdExam.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdExam_OnDocumentOpened);
            this.wdExam.OnDocumentClosed += new System.EventHandler(this.wdExam_OnDocumentClosed);
            // 
            // pnlwdExam
            // 
            this.pnlwdExam.Controls.Add(this.wdExam);
            this.pnlwdExam.Controls.Add(this.label2);
            this.pnlwdExam.Controls.Add(this.label20);
            this.pnlwdExam.Controls.Add(this.label1);
            this.pnlwdExam.Controls.Add(this.label11);
            this.pnlwdExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlwdExam.Location = new System.Drawing.Point(0, 54);
            this.pnlwdExam.Name = "pnlwdExam";
            this.pnlwdExam.Padding = new System.Windows.Forms.Padding(3);
            this.pnlwdExam.Size = new System.Drawing.Size(978, 659);
            this.pnlwdExam.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(970, 1);
            this.label2.TabIndex = 17;
            this.label2.Text = "label2";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label20.Location = new System.Drawing.Point(4, 655);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(970, 1);
            this.label20.TabIndex = 16;
            this.label20.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(974, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 653);
            this.label1.TabIndex = 15;
            this.label1.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 653);
            this.label11.TabIndex = 14;
            this.label11.Text = "label4";
            // 
            // pnltsCommands
            // 
            this.pnltsCommands.Controls.Add(this.ts_Commands);
            this.pnltsCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltsCommands.Location = new System.Drawing.Point(0, 0);
            this.pnltsCommands.Name = "pnltsCommands";
            this.pnltsCommands.Size = new System.Drawing.Size(978, 54);
            this.pnltsCommands.TabIndex = 1;
            // 
            // tsb_Print
            // 
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(45, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = " &Print";
            this.tsb_Print.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // frmWd_PatientExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(978, 713);
            this.Controls.Add(this.pnlwdExam);
            this.Controls.Add(this.pnltsCommands);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWd_PatientExam";
            this.Text = "Patient Exam";
            this.Load += new System.EventHandler(this.frmWd_PatientExam_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdExam)).EndInit();
            this.pnlwdExam.ResumeLayout(false);
            this.pnltsCommands.ResumeLayout(false);
            this.pnltsCommands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private AxDSOFramer.AxFramerControl wdExam;
        private System.Windows.Forms.Panel pnlwdExam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel pnltsCommands;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
    }
}