namespace gloCommunity.UserControls
{
    partial class UCSmartDX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCSmartDX));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pnlgloUC_TreeView2 = new System.Windows.Forms.Panel();
            this.gloUC_TreeView2 = new gloUserControlLibrary.gloUC_TreeView();
            this.imgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.trvsmartdiag = new System.Windows.Forms.TreeView();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.pnlgloUC_TreeView2.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.pnlgloUC_TreeView2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(28, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(240, 734);
            this.Panel1.TabIndex = 2;
            // 
            // pnlgloUC_TreeView2
            // 
            this.pnlgloUC_TreeView2.Controls.Add(this.gloUC_TreeView2);
            this.pnlgloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloUC_TreeView2.Location = new System.Drawing.Point(0, 0);
            this.pnlgloUC_TreeView2.Name = "pnlgloUC_TreeView2";
            this.pnlgloUC_TreeView2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlgloUC_TreeView2.Size = new System.Drawing.Size(240, 734);
            this.pnlgloUC_TreeView2.TabIndex = 24;
            // 
            // gloUC_TreeView2
            // 
            this.gloUC_TreeView2.BackColor = System.Drawing.Color.White;
            this.gloUC_TreeView2.CheckBoxes = false;
            this.gloUC_TreeView2.CodeMember = null;
            this.gloUC_TreeView2.Comment = null;
            this.gloUC_TreeView2.ConceptID = null;
            this.gloUC_TreeView2.DDIDMember = null;
            this.gloUC_TreeView2.DescriptionMember = null;
            this.gloUC_TreeView2.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
            this.gloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUC_TreeView2.DrugFlag = ((short)(16));
            this.gloUC_TreeView2.DrugFormMember = null;
            this.gloUC_TreeView2.DrugQtyQualifierMember = null;
            this.gloUC_TreeView2.DurationMember = null;
            this.gloUC_TreeView2.FrequencyMember = null;
            this.gloUC_TreeView2.ImageIndex = 0;
            this.gloUC_TreeView2.ImageList = this.imgTreeView;
            this.gloUC_TreeView2.ImageObject = null;
            this.gloUC_TreeView2.Indicator = null;
            this.gloUC_TreeView2.IsDrug = false;
            this.gloUC_TreeView2.IsNarcoticsMember = null;
            this.gloUC_TreeView2.IsSystemCategory = null;
            this.gloUC_TreeView2.Location = new System.Drawing.Point(3, 0);
            this.gloUC_TreeView2.MaximumNodes = 1000;
            this.gloUC_TreeView2.Name = "gloUC_TreeView2";
            this.gloUC_TreeView2.NDCCodeMember = null;
            this.gloUC_TreeView2.ParentImageIndex = 0;
            this.gloUC_TreeView2.ParentMember = null;
            this.gloUC_TreeView2.RouteMember = null;
            this.gloUC_TreeView2.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
            this.gloUC_TreeView2.SearchBox = true;
            this.gloUC_TreeView2.SearchText = null;
            this.gloUC_TreeView2.SelectedImageIndex = 0;
            this.gloUC_TreeView2.SelectedNode = null;
            this.gloUC_TreeView2.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("gloUC_TreeView2.SelectedNodeIDs")));
            this.gloUC_TreeView2.SelectedParentImageIndex = 0;
            this.gloUC_TreeView2.Size = new System.Drawing.Size(237, 731);
            this.gloUC_TreeView2.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription;
            this.gloUC_TreeView2.TabIndex = 23;
            this.gloUC_TreeView2.Tag = null;
            this.gloUC_TreeView2.UnitMember = null;
            this.gloUC_TreeView2.ValueMember = null;
            this.gloUC_TreeView2.NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TreeView.NodeMouseDoubleClickEventHandler(this.gloUC_TreeView2_NodeMouseDoubleClick);
            this.gloUC_TreeView2.Click += new System.EventHandler(this.gloUC_TreeView2_Click);
            this.gloUC_TreeView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gloUC_TreeView2_MouseDoubleClick);
            // 
            // imgTreeView
            // 
            this.imgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTreeView.ImageStream")));
            this.imgTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTreeView.Images.SetKeyName(0, "Bullet06.ico");
            this.imgTreeView.Images.SetKeyName(1, "ICD 9_01.ico");
            this.imgTreeView.Images.SetKeyName(2, "CPT_01.ico");
            this.imgTreeView.Images.SetKeyName(3, "Drugs.ico");
            this.imgTreeView.Images.SetKeyName(4, "Tag.ico");
            this.imgTreeView.Images.SetKeyName(5, "Pat Education.ico");
            this.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico");
            this.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico");
            this.imgTreeView.Images.SetKeyName(8, "FLow sheet.ico");
            this.imgTreeView.Images.SetKeyName(9, "Lab orders.ico");
            this.imgTreeView.Images.SetKeyName(10, "Radiology Orders.ico");
            this.imgTreeView.Images.SetKeyName(11, "Refferal.ico");
            this.imgTreeView.Images.SetKeyName(12, "Template.ico");
            this.imgTreeView.Images.SetKeyName(13, "Tempate Category_new.ico");
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(268, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(4, 734);
            this.Splitter1.TabIndex = 4;
            this.Splitter1.TabStop = false;
            // 
            // Panel5
            // 
            this.Panel5.Controls.Add(this.trvsmartdiag);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Controls.Add(this.Label7);
            this.Panel5.Controls.Add(this.Label8);
            this.Panel5.Controls.Add(this.Label9);
            this.Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel5.Location = new System.Drawing.Point(272, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Panel5.Size = new System.Drawing.Size(623, 734);
            this.Panel5.TabIndex = 5;
            // 
            // trvsmartdiag
            // 
            this.trvsmartdiag.BackColor = System.Drawing.Color.White;
            this.trvsmartdiag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvsmartdiag.CheckBoxes = true;
            this.trvsmartdiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvsmartdiag.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvsmartdiag.ForeColor = System.Drawing.Color.Black;
            this.trvsmartdiag.HideSelection = false;
            this.trvsmartdiag.ImageIndex = 0;
            this.trvsmartdiag.ImageList = this.imgTreeView;
            this.trvsmartdiag.Indent = 21;
            this.trvsmartdiag.ItemHeight = 20;
            this.trvsmartdiag.Location = new System.Drawing.Point(1, 4);
            this.trvsmartdiag.Name = "trvsmartdiag";
            this.trvsmartdiag.SelectedImageIndex = 0;
            this.trvsmartdiag.ShowLines = false;
            this.trvsmartdiag.Size = new System.Drawing.Size(618, 726);
            this.trvsmartdiag.TabIndex = 0;
            this.trvsmartdiag.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvsmartdiag_AfterCheck);
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label6.Location = new System.Drawing.Point(1, 730);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(618, 1);
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
            this.Label8.Location = new System.Drawing.Point(619, 4);
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
            this.Label9.Size = new System.Drawing.Size(620, 1);
            this.Label9.TabIndex = 9;
            this.Label9.Text = "label1";
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
            // UCSmartDX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCSmartDX";
            this.Size = new System.Drawing.Size(895, 734);
            this.Load += new System.EventHandler(this.UCSmartDX_Load);
            this.Panel1.ResumeLayout(false);
            this.pnlgloUC_TreeView2.ResumeLayout(false);
            this.Panel5.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

    //    private gloUserControlLibrary.gloUC_TreeView gloUC_TreeView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel Panel5;
        public  System.Windows.Forms.TreeView trvsmartdiag;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label9;
        private gloUserControlLibrary.gloUC_TreeView gloUC_TreeView2;
        private System.Windows.Forms.Panel pnlgloUC_TreeView2;
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
