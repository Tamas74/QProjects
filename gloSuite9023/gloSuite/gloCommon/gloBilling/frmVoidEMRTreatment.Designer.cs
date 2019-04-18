namespace gloBilling
{
    partial class frmVoidEMRTreatment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVoidEMRTreatment));
            this.components = new System.ComponentModel.Container();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.c1PatientEMRExams = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.chkGeneralSearch = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tls_SetupResource.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientEMRExams)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
       
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_SaveClose,
            this.tsb_Close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Padding = new System.Windows.Forms.Padding(0);
            this.tls_SetupResource.Size = new System.Drawing.Size(1154, 53);
            this.tls_SetupResource.TabIndex = 1;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "Sa&ve";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_SaveClose
            // 
            this.tsb_SaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveClose.Image")));
            this.tsb_SaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveClose.Name = "tsb_SaveClose";
            this.tsb_SaveClose.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveClose.Tag = "OK";
            this.tsb_SaveClose.Text = "&Save&&Cls";
            this.tsb_SaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveClose.ToolTipText = "Save and Close";
            this.tsb_SaveClose.Click += new System.EventHandler(this.tsb_SaveClose_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Cancel";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.c1PatientEMRExams);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label59);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 86);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(1154, 641);
            this.pnlMain.TabIndex = 2;
            // 
            // c1PatientEMRExams
            // 
            this.c1PatientEMRExams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientEMRExams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientEMRExams.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientEMRExams.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1PatientEMRExams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientEMRExams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientEMRExams.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1PatientEMRExams.Location = new System.Drawing.Point(4, 1);
            this.c1PatientEMRExams.Name = "c1PatientEMRExams";
            this.c1PatientEMRExams.Padding = new System.Windows.Forms.Padding(3);
            this.c1PatientEMRExams.Rows.Count = 1;
            this.c1PatientEMRExams.Rows.DefaultSize = 21;
            this.c1PatientEMRExams.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PatientEMRExams.Size = new System.Drawing.Size(1146, 636);
            this.c1PatientEMRExams.StyleInfo = resources.GetString("c1PatientEMRExams.StyleInfo");
            this.c1PatientEMRExams.TabIndex = 65;
            this.c1PatientEMRExams.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientEMRExams_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(1150, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 636);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1147, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 637);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 637);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1148, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 54);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(1154, 32);
            this.panel4.TabIndex = 75;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.cbSelectAll);
            this.panel3.Controls.Add(this.chkGeneralSearch);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.lblSearch);
            this.panel3.Controls.Add(this.label57);
            this.panel3.Controls.Add(this.label60);
            this.panel3.Controls.Add(this.label61);
            this.panel3.Controls.Add(this.label62);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1148, 26);
            this.panel3.TabIndex = 73;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.ForeColor = System.Drawing.Color.White;
            this.cbSelectAll.Location = new System.Drawing.Point(1033, 4);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(76, 18);
            this.cbSelectAll.TabIndex = 65;
            this.cbSelectAll.Text = "Select All";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            this.cbSelectAll.CheckedChanged += new System.EventHandler(this.cbSelectAll_CheckedChanged);
            // 
            // chkGeneralSearch
            // 
            this.chkGeneralSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGeneralSearch.AutoSize = true;
            this.chkGeneralSearch.ForeColor = System.Drawing.Color.White;
            this.chkGeneralSearch.Location = new System.Drawing.Point(420, 1);
            this.chkGeneralSearch.Name = "chkGeneralSearch";
            this.chkGeneralSearch.Padding = new System.Windows.Forms.Padding(3);
            this.chkGeneralSearch.Size = new System.Drawing.Size(114, 24);
            this.chkGeneralSearch.TabIndex = 64;
            this.chkGeneralSearch.Text = "General Search";
            this.chkGeneralSearch.UseVisualStyleBackColor = true;
            this.chkGeneralSearch.CheckedChanged += new System.EventHandler(this.chkGeneralSearch_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(108, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(307, 22);
            this.txtSearch.TabIndex = 63;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1124, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 24);
            this.btnClose.TabIndex = 29;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(1, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(107, 24);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "  Search : ";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Left;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(0, 1);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1, 24);
            this.label57.TabIndex = 7;
            this.label57.Text = "label4";
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Right;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.Location = new System.Drawing.Point(1147, 1);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 24);
            this.label60.TabIndex = 6;
            this.label60.Text = "label3";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1148, 1);
            this.label61.TabIndex = 5;
            this.label61.Text = "label1";
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.Location = new System.Drawing.Point(0, 25);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(1148, 1);
            this.label62.TabIndex = 8;
            this.label62.Text = "label2";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_SetupResource);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1154, 54);
            this.pnlToolStrip.TabIndex = 66;
            // 
            // frmVoidEMRTreatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1154, 727);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVoidEMRTreatment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "No Charge";
            this.Load += new System.EventHandler(this.frmVoidEMRTreatment_Load);
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientEMRExams)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_SaveClose;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientEMRExams;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkGeneralSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}