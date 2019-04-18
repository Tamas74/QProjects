namespace gloCommunity.UserControls
{
	partial class UC_CVSetup
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_CVSetup));
            this.pnlgloUC_TreeView2 = new System.Windows.Forms.Panel();
            this.trvCVSetup = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlCVSetup = new System.Windows.Forms.Panel();
            this.splitCVsetup = new System.Windows.Forms.SplitContainer();
            this.pnlCVCriteria = new System.Windows.Forms.Panel();
            this.flxCVSetup = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCVSetupSummary = new System.Windows.Forms.Panel();
            this.txt_summary = new System.Windows.Forms.RichTextBox();
            this.pnlMainMain = new System.Windows.Forms.Panel();
            this.SplitterMain = new System.Windows.Forms.Splitter();
            this.pnlRepository = new System.Windows.Forms.Panel();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlgloUC_TreeView2.SuspendLayout();
            this.pnlCVSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVsetup)).BeginInit();
            this.splitCVsetup.Panel1.SuspendLayout();
            this.splitCVsetup.Panel2.SuspendLayout();
            this.splitCVsetup.SuspendLayout();
            this.pnlCVCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxCVSetup)).BeginInit();
            this.pnlCVSetupSummary.SuspendLayout();
            this.pnlMainMain.SuspendLayout();
            this.pnlRepository.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlgloUC_TreeView2
            // 
            this.pnlgloUC_TreeView2.Controls.Add(this.trvCVSetup);
            this.pnlgloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloUC_TreeView2.Location = new System.Drawing.Point(0, 0);
            this.pnlgloUC_TreeView2.Name = "pnlgloUC_TreeView2";
            this.pnlgloUC_TreeView2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlgloUC_TreeView2.Size = new System.Drawing.Size(261, 844);
            this.pnlgloUC_TreeView2.TabIndex = 59;
            // 
            // trvCVSetup
            // 
            this.trvCVSetup.BackColor = System.Drawing.Color.White;
            this.trvCVSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvCVSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCVSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvCVSetup.ForeColor = System.Drawing.Color.Black;
            this.trvCVSetup.HideSelection = false;
            this.trvCVSetup.ImageIndex = 0;
            this.trvCVSetup.ImageList = this.ImageList1;
            this.trvCVSetup.Indent = 20;
            this.trvCVSetup.ItemHeight = 20;
            this.trvCVSetup.Location = new System.Drawing.Point(3, 0);
            this.trvCVSetup.Name = "trvCVSetup";
            this.trvCVSetup.SelectedImageIndex = 0;
            this.trvCVSetup.ShowLines = false;
            this.trvCVSetup.ShowNodeToolTips = true;
            this.trvCVSetup.ShowRootLines = false;
            this.trvCVSetup.Size = new System.Drawing.Size(258, 841);
            this.trvCVSetup.TabIndex = 27;
            this.trvCVSetup.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvCVSetup_NodeMouseClick);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Bullet06.ico");
            this.ImageList1.Images.SetKeyName(1, "Pat Demographic.ico");
            this.ImageList1.Images.SetKeyName(2, "History.ico");
            this.ImageList1.Images.SetKeyName(3, "ICD 09.ico");
            this.ImageList1.Images.SetKeyName(4, "Drugs.ico");
            this.ImageList1.Images.SetKeyName(5, "CPT.ico");
            this.ImageList1.Images.SetKeyName(6, "Lab.ico");
            this.ImageList1.Images.SetKeyName(7, "Radiology.ico");
            this.ImageList1.Images.SetKeyName(8, "Guideline Template.ico");
            this.ImageList1.Images.SetKeyName(9, "RX.ico");
            this.ImageList1.Images.SetKeyName(10, "PatientDetails.ico");
            this.ImageList1.Images.SetKeyName(11, "Small Arrow.ico");
            this.ImageList1.Images.SetKeyName(12, "Immunization_Old.ico");
            this.ImageList1.Images.SetKeyName(13, "Tempate Category_new.ico");
            // 
            // pnlCVSetup
            // 
            this.pnlCVSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCVSetup.Controls.Add(this.splitCVsetup);
            this.pnlCVSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCVSetup.Location = new System.Drawing.Point(292, 0);
            this.pnlCVSetup.Name = "pnlCVSetup";
            this.pnlCVSetup.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlCVSetup.Size = new System.Drawing.Size(693, 844);
            this.pnlCVSetup.TabIndex = 2;
            // 
            // splitCVsetup
            // 
            this.splitCVsetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCVsetup.Location = new System.Drawing.Point(0, 0);
            this.splitCVsetup.Name = "splitCVsetup";
            // 
            // splitCVsetup.Panel1
            // 
            this.splitCVsetup.Panel1.Controls.Add(this.pnlCVCriteria);
            // 
            // splitCVsetup.Panel2
            // 
            this.splitCVsetup.Panel2.Controls.Add(this.pnlCVSetupSummary);
            this.splitCVsetup.Size = new System.Drawing.Size(690, 844);
            this.splitCVsetup.SplitterDistance = 258;
            this.splitCVsetup.TabIndex = 0;
            // 
            // pnlCVCriteria
            // 
            this.pnlCVCriteria.Controls.Add(this.flxCVSetup);
            this.pnlCVCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCVCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnlCVCriteria.Name = "pnlCVCriteria";
            this.pnlCVCriteria.Size = new System.Drawing.Size(258, 844);
            this.pnlCVCriteria.TabIndex = 0;
            // 
            // flxCVSetup
            // 
            this.flxCVSetup.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxCVSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxCVSetup.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxCVSetup.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxCVSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxCVSetup.ExtendLastCol = true;
            this.flxCVSetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxCVSetup.Location = new System.Drawing.Point(0, 0);
            this.flxCVSetup.Name = "flxCVSetup";
            this.flxCVSetup.Rows.Count = 1;
            this.flxCVSetup.Rows.DefaultSize = 21;
            this.flxCVSetup.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxCVSetup.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxCVSetup.Size = new System.Drawing.Size(258, 844);
            this.flxCVSetup.StyleInfo = resources.GetString("flxCVSetup.StyleInfo");
            this.flxCVSetup.TabIndex = 4;
            this.flxCVSetup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.flxCVSetup_MouseDoubleClick);
            this.flxCVSetup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxCVSetup_MouseDown);
            // 
            // pnlCVSetupSummary
            // 
            this.pnlCVSetupSummary.Controls.Add(this.txt_summary);
            this.pnlCVSetupSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCVSetupSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlCVSetupSummary.Name = "pnlCVSetupSummary";
            this.pnlCVSetupSummary.Size = new System.Drawing.Size(428, 844);
            this.pnlCVSetupSummary.TabIndex = 0;
            // 
            // txt_summary
            // 
            this.txt_summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_summary.Location = new System.Drawing.Point(0, 0);
            this.txt_summary.Name = "txt_summary";
            this.txt_summary.ReadOnly = true;
            this.txt_summary.Size = new System.Drawing.Size(428, 844);
            this.txt_summary.TabIndex = 0;
            this.txt_summary.Text = "";
            // 
            // pnlMainMain
            // 
            this.pnlMainMain.Controls.Add(this.pnlCVSetup);
            this.pnlMainMain.Controls.Add(this.SplitterMain);
            this.pnlMainMain.Controls.Add(this.pnlRepository);
            this.pnlMainMain.Controls.Add(this.pnltls);
            this.pnlMainMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMainMain.Name = "pnlMainMain";
            this.pnlMainMain.Size = new System.Drawing.Size(985, 844);
            this.pnlMainMain.TabIndex = 5;
            // 
            // SplitterMain
            // 
            this.SplitterMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.SplitterMain.Location = new System.Drawing.Point(289, 0);
            this.SplitterMain.Name = "SplitterMain";
            this.SplitterMain.Size = new System.Drawing.Size(3, 844);
            this.SplitterMain.TabIndex = 3;
            this.SplitterMain.TabStop = false;
            // 
            // pnlRepository
            // 
            this.pnlRepository.Controls.Add(this.pnlgloUC_TreeView2);
            this.pnlRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRepository.Location = new System.Drawing.Point(28, 0);
            this.pnlRepository.Name = "pnlRepository";
            this.pnlRepository.Size = new System.Drawing.Size(261, 844);
            this.pnlRepository.TabIndex = 0;
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label19);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 844);
            this.pnltls.TabIndex = 101;
            this.pnltls.Visible = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 840);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 1);
            this.label19.TabIndex = 144;
            // 
            // tlsgloCommunity
            // 
            this.tlsgloCommunity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tlsgloCommunity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsgloCommunity.BackgroundImage")));
            this.tlsgloCommunity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsgloCommunity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsgloCommunity.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsgloCommunity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbClinicRepository,
            this.tlbGlobalRepository});
            this.tlsgloCommunity.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tlsgloCommunity.Location = new System.Drawing.Point(4, 23);
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 818);
            this.tlsgloCommunity.TabIndex = 21;
            this.tlsgloCommunity.Text = "toolStrip1";
            this.tlsgloCommunity.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tlbClinicRepository
            // 
            this.tlbClinicRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbClinicRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbClinicRepository.Image")));
            this.tlbClinicRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClinicRepository.Name = "tlbClinicRepository";
            this.tlbClinicRepository.Size = new System.Drawing.Size(21, 154);
            this.tlbClinicRepository.Text = "  Practice Repository";
            this.tlbClinicRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbClinicRepository.Click += new System.EventHandler(this.tlbClinicRepository_Click);
            // 
            // tlbGlobalRepository
            // 
            this.tlbGlobalRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbGlobalRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbGlobalRepository.Image")));
            this.tlbGlobalRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbGlobalRepository.Name = "tlbGlobalRepository";
            this.tlbGlobalRepository.Size = new System.Drawing.Size(21, 143);
            this.tlbGlobalRepository.Text = "  Global Repository";
            this.tlbGlobalRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbGlobalRepository.Click += new System.EventHandler(this.tlbGlobalRepository_Click);
            // 
            // btn_Right1
            // 
            this.btn_Right1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Right1.BackgroundImage")));
            this.btn_Right1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right1.FlatAppearance.BorderSize = 0;
            this.btn_Right1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Right1.Location = new System.Drawing.Point(4, 1);
            this.btn_Right1.Name = "btn_Right1";
            this.btn_Right1.Size = new System.Drawing.Size(23, 22);
            this.btn_Right1.TabIndex = 16;
            this.btn_Right1.UseVisualStyleBackColor = false;
            // 
            // lbl_pnlSmallStripLeftBrd
            // 
            this.lbl_pnlSmallStripLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSmallStripLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlSmallStripLeftBrd.Name = "lbl_pnlSmallStripLeftBrd";
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 840);
            this.lbl_pnlSmallStripLeftBrd.TabIndex = 9;
            // 
            // lbl_pnlSmallStripTopBrd
            // 
            this.lbl_pnlSmallStripTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSmallStripTopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSmallStripTopBrd.Name = "lbl_pnlSmallStripTopBrd";
            this.lbl_pnlSmallStripTopBrd.Size = new System.Drawing.Size(24, 1);
            this.lbl_pnlSmallStripTopBrd.TabIndex = 141;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(27, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 841);
            this.label53.TabIndex = 143;
            // 
            // UC_CVSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlMainMain);
            this.Name = "UC_CVSetup";
            this.Size = new System.Drawing.Size(985, 844);
            this.Load += new System.EventHandler(this.UC_CVSetup_Load);
            this.pnlgloUC_TreeView2.ResumeLayout(false);
            this.pnlCVSetup.ResumeLayout(false);
            this.splitCVsetup.Panel1.ResumeLayout(false);
            this.splitCVsetup.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCVsetup)).EndInit();
            this.splitCVsetup.ResumeLayout(false);
            this.pnlCVCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxCVSetup)).EndInit();
            this.pnlCVSetupSummary.ResumeLayout(false);
            this.pnlMainMain.ResumeLayout(false);
            this.pnlRepository.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel pnlgloUC_TreeView2;
        internal System.Windows.Forms.Panel pnlCVSetup;
        internal System.Windows.Forms.Panel pnlMainMain;
        internal System.Windows.Forms.Splitter SplitterMain;
        internal System.Windows.Forms.Panel pnlRepository;
        private System.Windows.Forms.SplitContainer splitCVsetup;
        private System.Windows.Forms.Panel pnlCVCriteria;
        private System.Windows.Forms.Panel pnlCVSetupSummary;
        public C1.Win.C1FlexGrid.C1FlexGrid flxCVSetup;
        private System.Windows.Forms.RichTextBox txt_summary;
        internal System.Windows.Forms.ImageList ImageList1;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label19;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.TreeView trvCVSetup;
	}
}
