namespace gloAppointmentScheduling
{
    partial class frmViewApptHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewApptHistory));
            this.grdApptLog = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlCalendar = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdApptLog)).BeginInit();
            this.panel5.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdApptLog
            // 
            this.grdApptLog.AllowEditing = false;
            this.grdApptLog.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.grdApptLog.ColumnInfo = resources.GetString("grdApptLog.ColumnInfo");
            this.grdApptLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdApptLog.ExtendLastCol = true;
            this.grdApptLog.Location = new System.Drawing.Point(4, 4);
            this.grdApptLog.Name = "grdApptLog";
            this.grdApptLog.Rows.DefaultSize = 21;
            this.grdApptLog.Size = new System.Drawing.Size(992, 376);
            this.grdApptLog.StyleInfo = resources.GetString("grdApptLog.StyleInfo");
            this.grdApptLog.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ts_Commands);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(997, 55);
            this.panel5.TabIndex = 95;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(997, 53);
            this.ts_Commands.TabIndex = 9;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(47, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = " &Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.ToolTipText = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlCalendar
            // 
            this.pnlCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCalendar.Controls.Add(this.grdApptLog);
            this.pnlCalendar.Controls.Add(this.label18);
            this.pnlCalendar.Controls.Add(this.label7);
            this.pnlCalendar.Controls.Add(this.label14);
            this.pnlCalendar.Controls.Add(this.label17);
            this.pnlCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalendar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCalendar.Location = new System.Drawing.Point(0, 55);
            this.pnlCalendar.Name = "pnlCalendar";
            this.pnlCalendar.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlCalendar.Size = new System.Drawing.Size(997, 384);
            this.pnlCalendar.TabIndex = 96;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(4, 380);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(992, 1);
            this.label18.TabIndex = 97;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 377);
            this.label7.TabIndex = 91;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(996, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 377);
            this.label14.TabIndex = 94;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(994, 1);
            this.label17.TabIndex = 96;
            // 
            // frmViewApptHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(997, 439);
            this.Controls.Add(this.pnlCalendar);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewApptHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Appointment History";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewApptHistory_FormClosed);
            this.Load += new System.EventHandler(this.frmViewApptHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdApptLog)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlCalendar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdApptLog;
        private System.Windows.Forms.Panel panel5;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlCalendar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
       
    }
}