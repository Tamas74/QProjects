namespace gloPatientPortal
{
    partial class frmHealthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHealthForm));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.Label8 = new System.Windows.Forms.Label();
            this.tls_DyHealthForm_2 = new System.Windows.Forms.ToolStrip();
            this.ts_New = new System.Windows.Forms.ToolStripButton();
            this.ts_Modify = new System.Windows.Forms.ToolStripButton();
            this.ts_Save = new System.Windows.Forms.ToolStripButton();
            this.ts_SaveandPreview = new System.Windows.Forms.ToolStripButton();
            this.ts_Publish = new System.Windows.Forms.ToolStripButton();
            this.ts_close = new System.Windows.Forms.ToolStripButton();
            this.Label29 = new System.Windows.Forms.Label();
            this.tls_DyHealthForm = new System.Windows.Forms.ToolStrip();
            this.ts_Groups = new System.Windows.Forms.ToolStripButton();
            this.ts_Questions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_BuildForm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlToolStrip.SuspendLayout();
            this.tls_DyHealthForm_2.SuspendLayout();
            this.tls_DyHealthForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.Label8);
            this.pnlToolStrip.Controls.Add(this.tls_DyHealthForm_2);
            this.pnlToolStrip.Controls.Add(this.Label29);
            this.pnlToolStrip.Controls.Add(this.tls_DyHealthForm);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1105, 108);
            this.pnlToolStrip.TabIndex = 15;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(0, 107);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1105, 1);
            this.Label8.TabIndex = 6;
            this.Label8.Text = "label1";
            // 
            // tls_DyHealthForm_2
            // 
            this.tls_DyHealthForm_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_DyHealthForm_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_DyHealthForm_2.BackgroundImage")));
            this.tls_DyHealthForm_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_DyHealthForm_2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_DyHealthForm_2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_DyHealthForm_2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_DyHealthForm_2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_New,
            this.ts_Modify,
            this.ts_Save,
            this.ts_SaveandPreview,
            this.ts_Publish,
            this.ts_close});
            this.tls_DyHealthForm_2.Location = new System.Drawing.Point(0, 54);
            this.tls_DyHealthForm_2.Name = "tls_DyHealthForm_2";
            this.tls_DyHealthForm_2.Size = new System.Drawing.Size(1105, 53);
            this.tls_DyHealthForm_2.TabIndex = 7;
            this.tls_DyHealthForm_2.Text = "ToolStrip1";
            // 
            // ts_New
            // 
            this.ts_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_New.Image = ((System.Drawing.Image)(resources.GetObject("ts_New.Image")));
            this.ts_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_New.Name = "ts_New";
            this.ts_New.Size = new System.Drawing.Size(37, 50);
            this.ts_New.Tag = "";
            this.ts_New.Text = "&New";
            this.ts_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_New.Click += new System.EventHandler(this.ts_New_Click);
            // 
            // ts_Modify
            // 
            this.ts_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Modify.Image = ((System.Drawing.Image)(resources.GetObject("ts_Modify.Image")));
            this.ts_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Modify.Name = "ts_Modify";
            this.ts_Modify.Size = new System.Drawing.Size(53, 50);
            this.ts_Modify.Tag = "Modify";
            this.ts_Modify.Text = "&Modify";
            this.ts_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Modify.ToolTipText = "Modify";
            this.ts_Modify.Click += new System.EventHandler(this.ts_Modify_Click);
            // 
            // ts_Save
            // 
            this.ts_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_Save.Image = ((System.Drawing.Image)(resources.GetObject("ts_Save.Image")));
            this.ts_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Save.Name = "ts_Save";
            this.ts_Save.Size = new System.Drawing.Size(40, 50);
            this.ts_Save.Tag = "";
            this.ts_Save.Text = "&Save";
            this.ts_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Save.Visible = false;
            this.ts_Save.Click += new System.EventHandler(this.ts_Save_Click);
            // 
            // ts_SaveandPreview
            // 
            this.ts_SaveandPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_SaveandPreview.Image = ((System.Drawing.Image)(resources.GetObject("ts_SaveandPreview.Image")));
            this.ts_SaveandPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SaveandPreview.Name = "ts_SaveandPreview";
            this.ts_SaveandPreview.Size = new System.Drawing.Size(105, 50);
            this.ts_SaveandPreview.Tag = "";
            this.ts_SaveandPreview.Text = "Save && &Preview";
            this.ts_SaveandPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_SaveandPreview.ToolTipText = "Save & Preview";
            this.ts_SaveandPreview.Visible = false;
            this.ts_SaveandPreview.Click += new System.EventHandler(this.ts_SaveandPreview_Click);
            // 
            // ts_Publish
            // 
            this.ts_Publish.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_Publish.Image = ((System.Drawing.Image)(resources.GetObject("ts_Publish.Image")));
            this.ts_Publish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Publish.Name = "ts_Publish";
            this.ts_Publish.Size = new System.Drawing.Size(55, 50);
            this.ts_Publish.Tag = "";
            this.ts_Publish.Text = "Publish";
            this.ts_Publish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Publish.Visible = false;
            this.ts_Publish.Click += new System.EventHandler(this.ts_Publish_Click);
            // 
            // ts_close
            // 
            this.ts_close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_close.Image = ((System.Drawing.Image)(resources.GetObject("ts_close.Image")));
            this.ts_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_close.Name = "ts_close";
            this.ts_close.Size = new System.Drawing.Size(43, 50);
            this.ts_close.Tag = "Close";
            this.ts_close.Text = "C&lose";
            this.ts_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_close.Visible = false;
            this.ts_close.Click += new System.EventHandler(this.ts_close_Click);
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label29.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label29.Location = new System.Drawing.Point(0, 53);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(1105, 1);
            this.Label29.TabIndex = 11;
            this.Label29.Text = "label2";
            // 
            // tls_DyHealthForm
            // 
            this.tls_DyHealthForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_DyHealthForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_DyHealthForm.BackgroundImage")));
            this.tls_DyHealthForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_DyHealthForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_DyHealthForm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_DyHealthForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_DyHealthForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_Groups,
            this.ts_Questions,
            this.toolStripSeparator2,
            this.ts_BuildForm,
            this.toolStripSeparator1,
            this.ts_btnClose});
            this.tls_DyHealthForm.Location = new System.Drawing.Point(0, 0);
            this.tls_DyHealthForm.Name = "tls_DyHealthForm";
            this.tls_DyHealthForm.Size = new System.Drawing.Size(1105, 53);
            this.tls_DyHealthForm.TabIndex = 0;
            this.tls_DyHealthForm.Text = "ToolStrip1";
            // 
            // ts_Groups
            // 
            this.ts_Groups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_Groups.Image = ((System.Drawing.Image)(resources.GetObject("ts_Groups.Image")));
            this.ts_Groups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Groups.Name = "ts_Groups";
            this.ts_Groups.Size = new System.Drawing.Size(54, 50);
            this.ts_Groups.Tag = "Dynamic Patient form";
            this.ts_Groups.Text = "&Groups";
            this.ts_Groups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Groups.Click += new System.EventHandler(this.ts_Groups_Click);
            // 
            // ts_Questions
            // 
            this.ts_Questions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_Questions.Image = ((System.Drawing.Image)(resources.GetObject("ts_Questions.Image")));
            this.ts_Questions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Questions.Name = "ts_Questions";
            this.ts_Questions.Size = new System.Drawing.Size(72, 50);
            this.ts_Questions.Tag = "Dynamic Patient form";
            this.ts_Questions.Text = "&Questions";
            this.ts_Questions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Questions.Click += new System.EventHandler(this.ts_Questions_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 53);
            // 
            // ts_BuildForm
            // 
            this.ts_BuildForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_BuildForm.Image = ((System.Drawing.Image)(resources.GetObject("ts_BuildForm.Image")));
            this.ts_BuildForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_BuildForm.Name = "ts_BuildForm";
            this.ts_BuildForm.Size = new System.Drawing.Size(138, 50);
            this.ts_BuildForm.Tag = "Patient forms";
            this.ts_BuildForm.Text = "&Online Patient Forms";
            this.ts_BuildForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_BuildForm.Click += new System.EventHandler(this.ts_BuildForm_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            this.toolStripSeparator1.Visible = false;
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Visible = false;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 108);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1105, 667);
            this.pnlMain.TabIndex = 16;
            // 
            // frmHealthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1105, 775);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHealthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Patient Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHealthForm_FormClosing);
            this.Load += new System.EventHandler(this.frmHealthForm_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_DyHealthForm_2.ResumeLayout(false);
            this.tls_DyHealthForm_2.PerformLayout();
            this.tls_DyHealthForm.ResumeLayout(false);
            this.tls_DyHealthForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        internal System.Windows.Forms.ToolStrip tls_DyHealthForm;
        internal System.Windows.Forms.ToolStripButton ts_Questions;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.ToolStripButton ts_BuildForm;
        internal System.Windows.Forms.ToolStripButton ts_Groups;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStrip tls_DyHealthForm_2;
        internal System.Windows.Forms.ToolStripButton ts_New;
        internal System.Windows.Forms.ToolStripButton ts_Modify;
        internal System.Windows.Forms.ToolStripButton ts_Save;
        internal System.Windows.Forms.ToolStripButton ts_SaveandPreview;
        internal System.Windows.Forms.ToolStripButton ts_Publish;
        internal System.Windows.Forms.ToolStripButton ts_close;
        private System.Windows.Forms.Label Label29;
    }
}