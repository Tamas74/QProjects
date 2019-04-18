namespace gloBilling
{
    partial class frmBillingPatientviewExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillingPatientviewExam));
            this.pnlOnBillingHold = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1PateintExams = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlOnToolstrip = new System.Windows.Forms.Panel();
            this.toolStrip2 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_Load = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.pnlOnBillingHold.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PateintExams)).BeginInit();
            this.pnlOnToolstrip.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOnBillingHold
            // 
            this.pnlOnBillingHold.Controls.Add(this.panel1);
            this.pnlOnBillingHold.Controls.Add(this.pnlOnToolstrip);
            this.pnlOnBillingHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOnBillingHold.Location = new System.Drawing.Point(0, 0);
            this.pnlOnBillingHold.Name = "pnlOnBillingHold";
            this.pnlOnBillingHold.Size = new System.Drawing.Size(779, 289);
            this.pnlOnBillingHold.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1PateintExams);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(779, 235);
            this.panel1.TabIndex = 2;
            // 
            // c1PateintExams
            // 
            this.c1PateintExams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PateintExams.AllowEditing = false;
            this.c1PateintExams.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PateintExams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PateintExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1PateintExams.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PateintExams.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PateintExams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PateintExams.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PateintExams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PateintExams.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PateintExams.Location = new System.Drawing.Point(4, 4);
            this.c1PateintExams.Name = "c1PateintExams";
            this.c1PateintExams.Padding = new System.Windows.Forms.Padding(2);
            this.c1PateintExams.Rows.Count = 1;
            this.c1PateintExams.Rows.DefaultSize = 19;
            this.c1PateintExams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PateintExams.Size = new System.Drawing.Size(771, 227);
            this.c1PateintExams.StyleInfo = resources.GetString("c1PateintExams.StyleInfo");
            this.c1PateintExams.TabIndex = 4;
            this.c1PateintExams.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1PateintExams_MouseDoubleClick);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(775, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 227);
            this.label13.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 227);
            this.label14.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(3, 231);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(773, 1);
            this.label15.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(773, 1);
            this.label16.TabIndex = 0;
            // 
            // pnlOnToolstrip
            // 
            this.pnlOnToolstrip.Controls.Add(this.toolStrip2);
            this.pnlOnToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOnToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlOnToolstrip.Name = "pnlOnToolstrip";
            this.pnlOnToolstrip.Size = new System.Drawing.Size(779, 54);
            this.pnlOnToolstrip.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_Load,
            this.tls_CloseMod});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(779, 53);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tls_Load
            // 
            this.tls_Load.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Load.Image = ((System.Drawing.Image)(resources.GetObject("tls_Load.Image")));
            this.tls_Load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Load.Name = "tls_Load";
            this.tls_Load.Size = new System.Drawing.Size(40, 50);
            this.tls_Load.Text = "&View";
            this.tls_Load.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Load.Click += new System.EventHandler(this.tls_Load_Click);
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
            // frmBillingPatientviewExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(779, 289);
            this.Controls.Add(this.pnlOnBillingHold);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBillingPatientviewExam";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Exam Note";
            this.Load += new System.EventHandler(this.frmBillingPatientviewExam_Load);
            this.pnlOnBillingHold.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PateintExams)).EndInit();
            this.pnlOnToolstrip.ResumeLayout(false);
            this.pnlOnToolstrip.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOnBillingHold;
        private System.Windows.Forms.Panel pnlOnToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip2;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolStripButton tls_Load;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PateintExams;
    }
}