namespace gloCommunity.UserControls
{
    partial class UCSmartCPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSmartCPT));
            this.Panel5 = new System.Windows.Forms.Panel();
            this.trvCPTAssociation = new System.Windows.Forms.TreeView();
            this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pnlgloUC_TreeView2 = new System.Windows.Forms.Panel();
            this.GloUC_trvCPT = new gloUserControlLibrary.gloUC_TreeView();
            this.pnl_btnICD9 = new System.Windows.Forms.Panel();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.Panel5.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlgloUC_TreeView2.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel5
            // 
            this.Panel5.Controls.Add(this.trvCPTAssociation);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Controls.Add(this.Label7);
            this.Panel5.Controls.Add(this.Label8);
            this.Panel5.Controls.Add(this.Label9);
            this.Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel5.Location = new System.Drawing.Point(313, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Panel5.Size = new System.Drawing.Size(582, 734);
            this.Panel5.TabIndex = 6;
            // 
            // trvCPTAssociation
            // 
            this.trvCPTAssociation.BackColor = System.Drawing.Color.White;
            this.trvCPTAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCPTAssociation.CheckBoxes = true;
            this.trvCPTAssociation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCPTAssociation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvCPTAssociation.ForeColor = System.Drawing.Color.Black;
            this.trvCPTAssociation.HideSelection = false;
            this.trvCPTAssociation.ImageIndex = 0;
            this.trvCPTAssociation.ImageList = this.imgTreeView;
            this.trvCPTAssociation.Indent = 21;
            this.trvCPTAssociation.ItemHeight = 20;
            this.trvCPTAssociation.Location = new System.Drawing.Point(1, 4);
            this.trvCPTAssociation.Name = "trvCPTAssociation";
            this.trvCPTAssociation.SelectedImageIndex = 0;
            this.trvCPTAssociation.ShowLines = false;
            this.trvCPTAssociation.Size = new System.Drawing.Size(577, 726);
            this.trvCPTAssociation.TabIndex = 0;
            // 
            // imgTreeView
            // 
            this.imgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeView.ImageStream")));
            this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTreeView.Images.SetKeyName(0, "Bullet06.ico");
            this.imgTreeView.Images.SetKeyName(1, "CPT_01.ico");
            this.imgTreeView.Images.SetKeyName(2, "ICD 9_01.ico");
            this.imgTreeView.Images.SetKeyName(3, "Drugs.ico");
            this.imgTreeView.Images.SetKeyName(4, "Tag.ico");
            this.imgTreeView.Images.SetKeyName(5, "Patient Education.ico");
            this.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico");
            this.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico");
            this.imgTreeView.Images.SetKeyName(8, "CPT-ICD9 Association.ico");
            this.imgTreeView.Images.SetKeyName(9, "CPT Association.ico");
            this.imgTreeView.Images.SetKeyName(10, "FLow sheet.ico");
            this.imgTreeView.Images.SetKeyName(11, "Lab orders.ico");
            this.imgTreeView.Images.SetKeyName(12, "Radiology Orders.ico");
            this.imgTreeView.Images.SetKeyName(13, "Refferal.ico");
            this.imgTreeView.Images.SetKeyName(14, "Tempate Category_new.ico");
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label6.Location = new System.Drawing.Point(1, 730);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(577, 1);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "label2";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(0, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 727);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "label4";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label8.Location = new System.Drawing.Point(578, 4);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1, 727);
            this.Label8.TabIndex = 10;
            this.Label8.Text = "label3";
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(0, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(579, 1);
            this.Label9.TabIndex = 9;
            this.Label9.Text = "label1";
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(308, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(5, 734);
            this.Splitter1.TabIndex = 7;
            this.Splitter1.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.pnlgloUC_TreeView2);
            this.Panel1.Controls.Add(this.pnl_btnICD9);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(28, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(280, 734);
            this.Panel1.TabIndex = 8;
            // 
            // pnlgloUC_TreeView2
            // 
            this.pnlgloUC_TreeView2.Controls.Add(this.GloUC_trvCPT);
            this.pnlgloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloUC_TreeView2.Location = new System.Drawing.Point(0, 10);
            this.pnlgloUC_TreeView2.Name = "pnlgloUC_TreeView2";
            this.pnlgloUC_TreeView2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlgloUC_TreeView2.Size = new System.Drawing.Size(280, 724);
            this.pnlgloUC_TreeView2.TabIndex = 24;
            // 
            // GloUC_trvCPT
            // 
            this.GloUC_trvCPT.BackColor = System.Drawing.Color.White;
            this.GloUC_trvCPT.CheckBoxes = false;
            this.GloUC_trvCPT.CodeMember = null;
            this.GloUC_trvCPT.Comment = null;
            this.GloUC_trvCPT.ConceptID = null;
            this.GloUC_trvCPT.DDIDMember = null;
            this.GloUC_trvCPT.DescriptionMember = null;
            this.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
            this.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GloUC_trvCPT.DrugFlag = ((short)(16));
            this.GloUC_trvCPT.DrugFormMember = null;
            this.GloUC_trvCPT.DrugQtyQualifierMember = null;
            this.GloUC_trvCPT.DurationMember = null;
            this.GloUC_trvCPT.FrequencyMember = null;
            this.GloUC_trvCPT.ImageIndex = 0;
            this.GloUC_trvCPT.ImageList = this.imgTreeView;
            this.GloUC_trvCPT.ImageObject = null;
            this.GloUC_trvCPT.Indicator = null;
            this.GloUC_trvCPT.IsDrug = false;
            this.GloUC_trvCPT.IsNarcoticsMember = null;
            this.GloUC_trvCPT.IsSystemCategory = null;
            this.GloUC_trvCPT.Location = new System.Drawing.Point(3, 0);
            this.GloUC_trvCPT.MaximumNodes = 1000;
            this.GloUC_trvCPT.Name = "GloUC_trvCPT";
            this.GloUC_trvCPT.NDCCodeMember = null;
            this.GloUC_trvCPT.ParentImageIndex = 0;
            this.GloUC_trvCPT.ParentMember = null;
            this.GloUC_trvCPT.RouteMember = null;
            this.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
            this.GloUC_trvCPT.SearchBox = true;
            this.GloUC_trvCPT.SearchText = null;
            this.GloUC_trvCPT.SelectedImageIndex = 0;
            this.GloUC_trvCPT.SelectedNode = null;
            this.GloUC_trvCPT.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs")));
            this.GloUC_trvCPT.SelectedParentImageIndex = 0;
            this.GloUC_trvCPT.Size = new System.Drawing.Size(277, 721);
            this.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription;
            this.GloUC_trvCPT.TabIndex = 23;
            this.GloUC_trvCPT.Tag = null;
            this.GloUC_trvCPT.UnitMember = null;
            this.GloUC_trvCPT.ValueMember = null;
            this.GloUC_trvCPT.NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TreeView.NodeMouseDoubleClickEventHandler(this.GloUC_trvCPT_NodeMouseDoubleClick);
            // 
            // pnl_btnICD9
            // 
            this.pnl_btnICD9.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnICD9.Location = new System.Drawing.Point(0, 0);
            this.pnl_btnICD9.Name = "pnl_btnICD9";
            this.pnl_btnICD9.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnl_btnICD9.Size = new System.Drawing.Size(280, 10);
            this.pnl_btnICD9.TabIndex = 0;
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
            this.pnltls.Size = new System.Drawing.Size(28, 734);
            this.pnltls.TabIndex = 101;
            this.pnltls.Visible = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 730);
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
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 708);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 730);
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
            this.label53.Size = new System.Drawing.Size(1, 731);
            this.label53.TabIndex = 143;
            // 
            // UCSmartCPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCSmartCPT";
            this.Size = new System.Drawing.Size(895, 734);
            this.Load += new System.EventHandler(this.UCSmartCPT_Load);
            this.Panel5.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.pnlgloUC_TreeView2.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel5;
        public System.Windows.Forms.TreeView trvCPTAssociation;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel pnlgloUC_TreeView2;
        private gloUserControlLibrary.gloUC_TreeView GloUC_trvCPT;
        internal System.Windows.Forms.Panel pnl_btnICD9;
        internal System.Windows.Forms.ImageList imgTreeView;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label19;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
    }
}
