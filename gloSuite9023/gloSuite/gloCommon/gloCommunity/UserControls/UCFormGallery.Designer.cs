namespace gloCommunity.UserControls
{
    partial class UCFormGallery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFormGallery));
            this.panel4 = new System.Windows.Forms.Panel();
            this.trvcptassoc = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.trvcpt = new System.Windows.Forms.TreeView();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trvformglry = new gloUserControlLibrary.gloUC_TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlleft = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlleft.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.trvcptassoc);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(485, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel4.Size = new System.Drawing.Size(536, 549);
            this.panel4.TabIndex = 31;
            // 
            // trvcptassoc
            // 
            this.trvcptassoc.BackColor = System.Drawing.Color.White;
            this.trvcptassoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvcptassoc.CheckBoxes = true;
            this.trvcptassoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvcptassoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvcptassoc.ForeColor = System.Drawing.Color.Black;
            this.trvcptassoc.HideSelection = false;
            this.trvcptassoc.ImageIndex = 0;
            this.trvcptassoc.ImageList = this.imageList;
            this.trvcptassoc.Indent = 20;
            this.trvcptassoc.ItemHeight = 20;
            this.trvcptassoc.Location = new System.Drawing.Point(11, 14);
            this.trvcptassoc.Name = "trvcptassoc";
            this.trvcptassoc.SelectedImageIndex = 0;
            this.trvcptassoc.ShowLines = false;
            this.trvcptassoc.ShowNodeToolTips = true;
            this.trvcptassoc.ShowRootLines = false;
            this.trvcptassoc.Size = new System.Drawing.Size(521, 531);
            this.trvcptassoc.TabIndex = 28;
            this.trvcptassoc.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvcptassoc_AfterCheck);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "CPT Association.ico");
            this.imageList.Images.SetKeyName(1, "CPT.ico");
            this.imageList.Images.SetKeyName(2, "Template.ico");
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(11, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(521, 10);
            this.label15.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 541);
            this.label16.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(1, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(531, 1);
            this.label4.TabIndex = 16;
            this.label4.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(1, 545);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(531, 1);
            this.label3.TabIndex = 15;
            this.label3.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(532, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 543);
            this.label2.TabIndex = 14;
            this.label2.Text = "label4";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 543);
            this.label1.TabIndex = 13;
            this.label1.Text = "label4";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.trvcpt);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(258, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel2.Size = new System.Drawing.Size(224, 549);
            this.panel2.TabIndex = 30;
            // 
            // trvcpt
            // 
            this.trvcpt.BackColor = System.Drawing.Color.White;
            this.trvcpt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvcpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvcpt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvcpt.ForeColor = System.Drawing.Color.Black;
            this.trvcpt.HideSelection = false;
            this.trvcpt.ImageIndex = 0;
            this.trvcpt.ImageList = this.imageList;
            this.trvcpt.Indent = 20;
            this.trvcpt.ItemHeight = 20;
            this.trvcpt.Location = new System.Drawing.Point(11, 14);
            this.trvcpt.Name = "trvcpt";
            this.trvcpt.SelectedImageIndex = 0;
            this.trvcpt.ShowLines = false;
            this.trvcpt.ShowNodeToolTips = true;
            this.trvcpt.ShowRootLines = false;
            this.trvcpt.Size = new System.Drawing.Size(212, 531);
            this.trvcpt.TabIndex = 1;
            this.trvcpt.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvcpt_NodeMouseDoubleClick);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(11, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(212, 10);
            this.label14.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 541);
            this.label12.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(223, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 541);
            this.label11.TabIndex = 20;
            this.label11.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(1, 545);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(223, 1);
            this.label10.TabIndex = 19;
            this.label10.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(1, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(223, 1);
            this.label8.TabIndex = 18;
            this.label8.Text = "label2";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 543);
            this.label9.TabIndex = 17;
            this.label9.Text = "label4";
            // 
            // trvformglry
            // 
            this.trvformglry.BackColor = System.Drawing.Color.White;
            this.trvformglry.CheckBoxes = false;
            this.trvformglry.CodeMember = null;
            this.trvformglry.Comment = null;
            this.trvformglry.ConceptID = null;
            this.trvformglry.DDIDMember = null;
            this.trvformglry.DescriptionMember = null;
            this.trvformglry.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
            this.trvformglry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvformglry.DrugFlag = ((short)(16));
            this.trvformglry.DrugFormMember = null;
            this.trvformglry.DrugQtyQualifierMember = null;
            this.trvformglry.DurationMember = null;
            this.trvformglry.FrequencyMember = null;
            this.trvformglry.ImageIndex = 0;
            this.trvformglry.ImageObject = null;
            this.trvformglry.Indicator = null;
            this.trvformglry.IsDrug = false;
            this.trvformglry.IsNarcoticsMember = null;
            this.trvformglry.IsSystemCategory = null;
            this.trvformglry.Location = new System.Drawing.Point(0, 0);
            this.trvformglry.MaximumNodes = 1000;
            this.trvformglry.Name = "trvformglry";
            this.trvformglry.NDCCodeMember = null;
            this.trvformglry.ParentImageIndex = 0;
            this.trvformglry.ParentMember = null;
            this.trvformglry.RouteMember = null;
            this.trvformglry.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
            this.trvformglry.SearchBox = true;
            this.trvformglry.SearchText = null;
            this.trvformglry.SelectedImageIndex = 0;
            this.trvformglry.SelectedNode = null;
            this.trvformglry.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("trvformglry.SelectedNodeIDs")));
            this.trvformglry.SelectedParentImageIndex = 0;
            this.trvformglry.Size = new System.Drawing.Size(224, 543);
            this.trvformglry.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription;
            this.trvformglry.TabIndex = 24;
            this.trvformglry.Tag = null;
            this.trvformglry.UnitMember = null;
            this.trvformglry.ValueMember = null;
            this.trvformglry.NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TreeView.NodeMouseDoubleClickEventHandler(this.trvformglry_NodeMouseDoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trvformglry);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(224, 546);
            this.panel3.TabIndex = 25;
            // 
            // pnlleft
            // 
            this.pnlleft.Controls.Add(this.panel3);
            this.pnlleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlleft.Location = new System.Drawing.Point(28, 0);
            this.pnlleft.Name = "pnlleft";
            this.pnlleft.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlleft.Size = new System.Drawing.Size(230, 549);
            this.pnlleft.TabIndex = 29;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(482, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 549);
            this.splitter1.TabIndex = 32;
            this.splitter1.TabStop = false;
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label24);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 549);
            this.pnltls.TabIndex = 105;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 545);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 1);
            this.label24.TabIndex = 144;
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
            this.tlsgloCommunity.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 523);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 545);
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
            this.label53.Size = new System.Drawing.Size(1, 546);
            this.label53.TabIndex = 143;
            // 
            // UCFormGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlleft);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCFormGallery";
            this.Size = new System.Drawing.Size(1021, 549);
            this.Load += new System.EventHandler(this.UCFormGallery_Load);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlleft.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TreeView trvcpt;
        public gloUserControlLibrary.gloUC_TreeView trvformglry;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel pnlleft;
        public System.Windows.Forms.TreeView trvcptassoc;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label24;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
    }
}
