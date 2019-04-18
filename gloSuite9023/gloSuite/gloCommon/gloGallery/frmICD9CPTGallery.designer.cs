namespace gloGallery
{
    partial class frmICD9CPTGallery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmICD9CPTGallery));
            this.frmMain = new System.Windows.Forms.Panel();
            this.pnlTab = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnRemoveCode = new System.Windows.Forms.Button();
            this.btnAddCode = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.gloUCMaster = new gloGallery.gloUC_TreeView();
            this.ImgICD9CPT = new System.Windows.Forms.ImageList(this.components);
            this.pnlCategory = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.cmbSpeciality = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btnUnSelectAllMasterCodes = new System.Windows.Forms.Button();
            this.btnSelectAllMasterCodes = new System.Windows.Forms.Button();
            this.lblMaster = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.gloUCGallery = new gloGallery.gloUC_TreeView();
            this.pnlActivationDatesFilter = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbActivationDate = new System.Windows.Forms.ComboBox();
            this.lblActivationDate = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.pnlSortBy = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbDescription = new System.Windows.Forms.RadioButton();
            this.rbCode = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMapping = new System.Windows.Forms.Panel();
            this.chkUnsusedOnly = new System.Windows.Forms.CheckBox();
            this.chkMapping = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbICD9Gallery = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.btnUnSelectGalleryCode = new System.Windows.Forms.Button();
            this.btnSelectAllGalleryCode = new System.Windows.Forms.Button();
            this.lblGalleryHeader = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.pnlICD9bottom = new System.Windows.Forms.Panel();
            this.pnlLeftbottom = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblBlank = new System.Windows.Forms.Label();
            this.PicBlank = new System.Windows.Forms.PictureBox();
            this.lblrevise = new System.Windows.Forms.Label();
            this.PicRevise = new System.Windows.Forms.PictureBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.PicNew = new System.Windows.Forms.PictureBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.tlICD9CptGallery = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClose = new System.Windows.Forms.ToolStripButton();
            this.Label35 = new System.Windows.Forms.Label();
            this.tlbImportCPT = new System.Windows.Forms.ToolStripButton();
            this.tlbImportIDC9 = new System.Windows.Forms.ToolStripButton();
            this.tlbClearAll = new System.Windows.Forms.ToolStripButton();
            this.tlbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlbCPTGallery = new System.Windows.Forms.ToolStripButton();
            this.tlbICD9Gallery = new System.Windows.Forms.ToolStripButton();
            this.tlbCurrentCPT = new System.Windows.Forms.ToolStripButton();
            this.tlbCurrentICD9 = new System.Windows.Forms.ToolStripButton();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.frmMain.SuspendLayout();
            this.pnlTab.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel12.SuspendLayout();
            this.pnlCategory.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel17.SuspendLayout();
            this.pnlActivationDatesFilter.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlSortBy.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMapping.SuspendLayout();
            this.pnlIndicator.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel19.SuspendLayout();
            this.pnlICD9bottom.SuspendLayout();
            this.pnlLeftbottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRevise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicNew)).BeginInit();
            this.pnlTOP.SuspendLayout();
            this.tlICD9CptGallery.SuspendLayout();
            this.SuspendLayout();
            // 
            // frmMain
            // 
            this.frmMain.Controls.Add(this.pnlTab);
            this.frmMain.Controls.Add(this.pnlICD9bottom);
            this.frmMain.Controls.Add(this.pnlTOP);
            this.frmMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmMain.Location = new System.Drawing.Point(0, 0);
            this.frmMain.Name = "frmMain";
            this.frmMain.Size = new System.Drawing.Size(925, 772);
            this.frmMain.TabIndex = 0;
            // 
            // pnlTab
            // 
            this.pnlTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTab.Controls.Add(this.panel7);
            this.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab.Location = new System.Drawing.Point(0, 53);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTab.Size = new System.Drawing.Size(925, 693);
            this.pnlTab.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Controls.Add(this.panel12);
            this.panel7.Controls.Add(this.panel17);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(919, 687);
            this.panel7.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnRemoveCode);
            this.panel10.Controls.Add(this.btnAddCode);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(400, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(119, 687);
            this.panel10.TabIndex = 1;
            // 
            // btnRemoveCode
            // 
            this.btnRemoveCode.AutoSize = true;
            this.btnRemoveCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRemoveCode.FlatAppearance.BorderSize = 0;
            this.btnRemoveCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCode.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveCode.Image")));
            this.btnRemoveCode.Location = new System.Drawing.Point(37, 331);
            this.btnRemoveCode.Name = "btnRemoveCode";
            this.btnRemoveCode.Size = new System.Drawing.Size(36, 36);
            this.btnRemoveCode.TabIndex = 3;
            this.btnRemoveCode.UseVisualStyleBackColor = true;
            this.btnRemoveCode.Click += new System.EventHandler(this.btnRemoveCode_Click);
            this.btnRemoveCode.MouseHover += new System.EventHandler(this.btnRemoveCode_MouseHover);
            // 
            // btnAddCode
            // 
            this.btnAddCode.AutoSize = true;
            this.btnAddCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddCode.FlatAppearance.BorderSize = 0;
            this.btnAddCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAddCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAddCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCode.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCode.Image")));
            this.btnAddCode.Location = new System.Drawing.Point(37, 278);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new System.Drawing.Size(36, 36);
            this.btnAddCode.TabIndex = 2;
            this.btnAddCode.UseVisualStyleBackColor = true;
            this.btnAddCode.Click += new System.EventHandler(this.btnAddCode_Click);
            this.btnAddCode.MouseHover += new System.EventHandler(this.btnAddCode_MouseHover);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.gloUCMaster);
            this.panel12.Controls.Add(this.pnlCategory);
            this.panel12.Controls.Add(this.pnlSpeciality);
            this.panel12.Controls.Add(this.panel15);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel12.Location = new System.Drawing.Point(519, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(400, 687);
            this.panel12.TabIndex = 1;
            // 
            // gloUCMaster
            // 
            this.gloUCMaster.AutoSize = true;
            this.gloUCMaster.BackColor = System.Drawing.Color.Transparent;
            this.gloUCMaster.CheckBoxes = false;
            this.gloUCMaster.CodeMember = null;
            this.gloUCMaster.Comment = null;
            this.gloUCMaster.ConceptID = null;
            this.gloUCMaster.CPTActivationDate = null;
            this.gloUCMaster.CPTDeactivationDate = null;
            this.gloUCMaster.DescriptionMember = null;
            this.gloUCMaster.DisplayType = gloGallery.gloUC_TreeView.enumDisplayType.Code_Description;
            this.gloUCMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUCMaster.DrugFlag = ((short)(16));
            this.gloUCMaster.DrugFormMember = null;
            this.gloUCMaster.DrugQtyQualifierMember = null;
            this.gloUCMaster.DurationMember = null;
            this.gloUCMaster.FrequencyMember = null;
            this.gloUCMaster.ImageIndex = 0;
            this.gloUCMaster.ImageList = this.ImgICD9CPT;
            this.gloUCMaster.ImageObject = null;
            this.gloUCMaster.Indicator = null;
            this.gloUCMaster.IsDrug = false;
            this.gloUCMaster.IsNarcoticsMember = null;
            this.gloUCMaster.Location = new System.Drawing.Point(0, 76);
            this.gloUCMaster.MaximumNodes = 500;
            this.gloUCMaster.Name = "gloUCMaster";
            this.gloUCMaster.NDCCodeMember = null;
            this.gloUCMaster.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.gloUCMaster.ParentImageIndex = 0;
            this.gloUCMaster.ParentMember = null;
            this.gloUCMaster.RouteMember = null;
            this.gloUCMaster.Search = gloGallery.gloUC_TreeView.enumSearchType.Instring;
            this.gloUCMaster.SearchBox = true;
            this.gloUCMaster.SearchText = null;
            this.gloUCMaster.SelectedImageIndex = 0;
            this.gloUCMaster.SelectedNode = null;
            this.gloUCMaster.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("gloUCMaster.SelectedNodeIDs")));
            this.gloUCMaster.SelectedParentImageIndex = 0;
            this.gloUCMaster.Size = new System.Drawing.Size(400, 611);
            this.gloUCMaster.Sort = gloGallery.gloUC_TreeView.enumSortType.ByDescription;
            this.gloUCMaster.TabIndex = 2;
            this.gloUCMaster.Tag = null;
            this.gloUCMaster.UnitMember = null;
            this.gloUCMaster.ValueMember = null;
            // 
            // ImgICD9CPT
            // 
            this.ImgICD9CPT.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgICD9CPT.ImageStream")));
            this.ImgICD9CPT.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgICD9CPT.Images.SetKeyName(0, "ICD 09.ico");
            this.ImgICD9CPT.Images.SetKeyName(1, "CPT.ico");
            this.ImgICD9CPT.Images.SetKeyName(2, "ICD9Gallery_01.ico");
            this.ImgICD9CPT.Images.SetKeyName(3, "CPTGallery_01.ico");
            this.ImgICD9CPT.Images.SetKeyName(4, "Bullet06.ico");
            this.ImgICD9CPT.Images.SetKeyName(5, "Specialty.ico");
            this.ImgICD9CPT.Images.SetKeyName(6, "Category_New.ico");
            this.ImgICD9CPT.Images.SetKeyName(7, "Blank_02.ico");
            this.ImgICD9CPT.Images.SetKeyName(8, "News_01.ico");
            this.ImgICD9CPT.Images.SetKeyName(9, "Revied_04.ico");
            this.ImgICD9CPT.Images.SetKeyName(10, "Small Speciality.ico");
            this.ImgICD9CPT.Images.SetKeyName(11, "Category_01.ico");
            this.ImgICD9CPT.Images.SetKeyName(12, "ICD 10.ico");
            this.ImgICD9CPT.Images.SetKeyName(13, "ICD-New.ico");
            this.ImgICD9CPT.Images.SetKeyName(14, "Delete.ico");
            // 
            // pnlCategory
            // 
            this.pnlCategory.Controls.Add(this.cmbCategory);
            this.pnlCategory.Controls.Add(this.label10);
            this.pnlCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCategory.Location = new System.Drawing.Point(0, 51);
            this.pnlCategory.Name = "pnlCategory";
            this.pnlCategory.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlCategory.Size = new System.Drawing.Size(400, 25);
            this.pnlCategory.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "All",
            "New",
            "Revised",
            "No Change"});
            this.cmbCategory.Location = new System.Drawing.Point(76, 3);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(324, 22);
            this.cmbCategory.TabIndex = 0;
            this.cmbCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbCategory_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 22);
            this.label10.TabIndex = 6;
            this.label10.Text = "Category :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSpeciality
            // 
            this.pnlSpeciality.Controls.Add(this.cmbSpeciality);
            this.pnlSpeciality.Controls.Add(this.label11);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 28);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Size = new System.Drawing.Size(400, 23);
            this.pnlSpeciality.TabIndex = 0;
            // 
            // cmbSpeciality
            // 
            this.cmbSpeciality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeciality.FormattingEnabled = true;
            this.cmbSpeciality.Location = new System.Drawing.Point(76, 0);
            this.cmbSpeciality.Name = "cmbSpeciality";
            this.cmbSpeciality.Size = new System.Drawing.Size(324, 22);
            this.cmbSpeciality.TabIndex = 0;
            this.cmbSpeciality.SelectionChangeCommitted += new System.EventHandler(this.cmbSpeciality_SelectionChangeCommitted);
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 23);
            this.label11.TabIndex = 5;
            this.label11.Text = "Specialty :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.panel16);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel15.Size = new System.Drawing.Size(400, 28);
            this.panel15.TabIndex = 3;
            // 
            // panel16
            // 
            this.panel16.BackgroundImage = global::gloGallery.Properties.Resources.Img_Blue2007;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.btnUnSelectAllMasterCodes);
            this.panel16.Controls.Add(this.btnSelectAllMasterCodes);
            this.panel16.Controls.Add(this.lblMaster);
            this.panel16.Controls.Add(this.label15);
            this.panel16.Controls.Add(this.label21);
            this.panel16.Controls.Add(this.label27);
            this.panel16.Controls.Add(this.label28);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel16.ForeColor = System.Drawing.Color.White;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(400, 25);
            this.panel16.TabIndex = 0;
            // 
            // btnUnSelectAllMasterCodes
            // 
            this.btnUnSelectAllMasterCodes.BackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectAllMasterCodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUnSelectAllMasterCodes.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUnSelectAllMasterCodes.FlatAppearance.BorderSize = 0;
            this.btnUnSelectAllMasterCodes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectAllMasterCodes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectAllMasterCodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnSelectAllMasterCodes.Image = ((System.Drawing.Image)(resources.GetObject("btnUnSelectAllMasterCodes.Image")));
            this.btnUnSelectAllMasterCodes.Location = new System.Drawing.Point(349, 1);
            this.btnUnSelectAllMasterCodes.Name = "btnUnSelectAllMasterCodes";
            this.btnUnSelectAllMasterCodes.Size = new System.Drawing.Size(25, 23);
            this.btnUnSelectAllMasterCodes.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.btnUnSelectAllMasterCodes, "Deselect All");
            this.btnUnSelectAllMasterCodes.UseVisualStyleBackColor = false;
            this.btnUnSelectAllMasterCodes.Visible = false;
            this.btnUnSelectAllMasterCodes.Click += new System.EventHandler(this.btnUnSelectAllMasterCodes_Click);
            // 
            // btnSelectAllMasterCodes
            // 
            this.btnSelectAllMasterCodes.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllMasterCodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelectAllMasterCodes.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAllMasterCodes.FlatAppearance.BorderSize = 0;
            this.btnSelectAllMasterCodes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllMasterCodes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllMasterCodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllMasterCodes.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllMasterCodes.Image")));
            this.btnSelectAllMasterCodes.Location = new System.Drawing.Point(374, 1);
            this.btnSelectAllMasterCodes.Name = "btnSelectAllMasterCodes";
            this.btnSelectAllMasterCodes.Size = new System.Drawing.Size(25, 23);
            this.btnSelectAllMasterCodes.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.btnSelectAllMasterCodes, "Select All");
            this.btnSelectAllMasterCodes.UseVisualStyleBackColor = false;
            this.btnSelectAllMasterCodes.Click += new System.EventHandler(this.btnSelectAllMasterCodes_Click);
            // 
            // lblMaster
            // 
            this.lblMaster.BackColor = System.Drawing.Color.Transparent;
            this.lblMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaster.Location = new System.Drawing.Point(1, 1);
            this.lblMaster.Name = "lblMaster";
            this.lblMaster.Size = new System.Drawing.Size(398, 23);
            this.lblMaster.TabIndex = 1;
            this.lblMaster.Text = "Master";
            this.lblMaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(1, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(398, 1);
            this.label15.TabIndex = 8;
            this.label15.Text = "label2";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Location = new System.Drawing.Point(0, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 24);
            this.label21.TabIndex = 7;
            this.label21.Text = "label4";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(399, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 24);
            this.label27.TabIndex = 6;
            this.label27.Text = "label3";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(400, 1);
            this.label28.TabIndex = 5;
            this.label28.Text = "label1";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.gloUCGallery);
            this.panel17.Controls.Add(this.pnlActivationDatesFilter);
            this.panel17.Controls.Add(this.pnlSortBy);
            this.panel17.Controls.Add(this.panel1);
            this.panel17.Controls.Add(this.pnlIndicator);
            this.panel17.Controls.Add(this.panel18);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(400, 687);
            this.panel17.TabIndex = 0;
            // 
            // gloUCGallery
            // 
            this.gloUCGallery.BackColor = System.Drawing.Color.Transparent;
            this.gloUCGallery.CheckBoxes = true;
            this.gloUCGallery.CodeMember = null;
            this.gloUCGallery.Comment = null;
            this.gloUCGallery.ConceptID = null;
            this.gloUCGallery.CPTActivationDate = null;
            this.gloUCGallery.CPTDeactivationDate = null;
            this.gloUCGallery.DescriptionMember = null;
            this.gloUCGallery.DisplayType = gloGallery.gloUC_TreeView.enumDisplayType.Code_Description;
            this.gloUCGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloUCGallery.DrugFlag = ((short)(16));
            this.gloUCGallery.DrugFormMember = null;
            this.gloUCGallery.DrugQtyQualifierMember = null;
            this.gloUCGallery.DurationMember = null;
            this.gloUCGallery.FrequencyMember = null;
            this.gloUCGallery.ImageIndex = 0;
            this.gloUCGallery.ImageList = this.ImgICD9CPT;
            this.gloUCGallery.ImageObject = null;
            this.gloUCGallery.Indicator = null;
            this.gloUCGallery.IsDrug = false;
            this.gloUCGallery.IsNarcoticsMember = null;
            this.gloUCGallery.Location = new System.Drawing.Point(0, 152);
            this.gloUCGallery.MaximumNodes = 500;
            this.gloUCGallery.Name = "gloUCGallery";
            this.gloUCGallery.NDCCodeMember = null;
            this.gloUCGallery.ParentImageIndex = 0;
            this.gloUCGallery.ParentMember = null;
            this.gloUCGallery.RouteMember = null;
            this.gloUCGallery.Search = gloGallery.gloUC_TreeView.enumSearchType.Instring;
            this.gloUCGallery.SearchBox = true;
            this.gloUCGallery.SearchText = null;
            this.gloUCGallery.SelectedImageIndex = 0;
            this.gloUCGallery.SelectedNode = null;
            this.gloUCGallery.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("gloUCGallery.SelectedNodeIDs")));
            this.gloUCGallery.SelectedParentImageIndex = 0;
            this.gloUCGallery.Size = new System.Drawing.Size(400, 535);
            this.gloUCGallery.Sort = gloGallery.gloUC_TreeView.enumSortType.ByDescription;
            this.gloUCGallery.TabIndex = 4;
            this.gloUCGallery.Tag = null;
            this.gloUCGallery.UnitMember = null;
            this.gloUCGallery.ValueMember = null;
            // 
            // pnlActivationDatesFilter
            // 
            this.pnlActivationDatesFilter.Controls.Add(this.panel5);
            this.pnlActivationDatesFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActivationDatesFilter.Location = new System.Drawing.Point(0, 121);
            this.pnlActivationDatesFilter.Name = "pnlActivationDatesFilter";
            this.pnlActivationDatesFilter.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlActivationDatesFilter.Size = new System.Drawing.Size(400, 31);
            this.pnlActivationDatesFilter.TabIndex = 6;
            this.pnlActivationDatesFilter.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.cmbActivationDate);
            this.panel5.Controls.Add(this.lblActivationDate);
            this.panel5.Controls.Add(this.label26);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Controls.Add(this.label34);
            this.panel5.Controls.Add(this.label36);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ForeColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(400, 28);
            this.panel5.TabIndex = 3;
            // 
            // cmbActivationDate
            // 
            this.cmbActivationDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivationDate.ForeColor = System.Drawing.Color.Black;
            this.cmbActivationDate.FormattingEnabled = true;
            this.cmbActivationDate.Items.AddRange(new object[] {
            "All",
            "New",
            "Revised",
            "No Change"});
            this.cmbActivationDate.Location = new System.Drawing.Point(114, 3);
            this.cmbActivationDate.Name = "cmbActivationDate";
            this.cmbActivationDate.Size = new System.Drawing.Size(283, 22);
            this.cmbActivationDate.TabIndex = 11;
            this.cmbActivationDate.SelectedIndexChanged += new System.EventHandler(this.cmbActivationDate_SelectedIndexChanged);
            // 
            // lblActivationDate
            // 
            this.lblActivationDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblActivationDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblActivationDate.Location = new System.Drawing.Point(1, 1);
            this.lblActivationDate.Name = "lblActivationDate";
            this.lblActivationDate.Size = new System.Drawing.Size(118, 26);
            this.lblActivationDate.TabIndex = 9;
            this.lblActivationDate.Text = "Activation Date :";
            this.lblActivationDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Location = new System.Drawing.Point(1, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(398, 1);
            this.label26.TabIndex = 8;
            this.label26.Text = "label26";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Left;
            this.label29.Location = new System.Drawing.Point(0, 1);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 27);
            this.label29.TabIndex = 7;
            this.label29.Text = "label4";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Right;
            this.label34.Location = new System.Drawing.Point(399, 1);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 27);
            this.label34.TabIndex = 6;
            this.label34.Text = "label3";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(400, 1);
            this.label36.TabIndex = 5;
            this.label36.Text = "label1";
            // 
            // pnlSortBy
            // 
            this.pnlSortBy.Controls.Add(this.panel4);
            this.pnlSortBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSortBy.Location = new System.Drawing.Point(0, 90);
            this.pnlSortBy.Name = "pnlSortBy";
            this.pnlSortBy.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlSortBy.Size = new System.Drawing.Size(400, 31);
            this.pnlSortBy.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.rbDescription);
            this.panel4.Controls.Add(this.rbCode);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 28);
            this.panel4.TabIndex = 3;
            // 
            // rbDescription
            // 
            this.rbDescription.AutoSize = true;
            this.rbDescription.Checked = true;
            this.rbDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbDescription.Location = new System.Drawing.Point(146, 6);
            this.rbDescription.Name = "rbDescription";
            this.rbDescription.Size = new System.Drawing.Size(85, 18);
            this.rbDescription.TabIndex = 1;
            this.rbDescription.TabStop = true;
            this.rbDescription.Text = "Description";
            this.rbDescription.UseVisualStyleBackColor = true;
            this.rbDescription.CheckedChanged += new System.EventHandler(this.rbCode_CheckedChanged);
            // 
            // rbCode
            // 
            this.rbCode.AutoSize = true;
            this.rbCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbCode.Location = new System.Drawing.Point(83, 6);
            this.rbCode.Name = "rbCode";
            this.rbCode.Size = new System.Drawing.Size(53, 18);
            this.rbCode.TabIndex = 0;
            this.rbCode.Text = "Code";
            this.rbCode.UseVisualStyleBackColor = true;
            this.rbCode.CheckedChanged += new System.EventHandler(this.rbCode_CheckedChanged);
            // 
            // label24
            // 
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(1, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 26);
            this.label24.TabIndex = 9;
            this.label24.Text = "Order By :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(1, 27);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(398, 1);
            this.label19.TabIndex = 8;
            this.label19.Text = "label19";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Location = new System.Drawing.Point(0, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 27);
            this.label20.TabIndex = 7;
            this.label20.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(399, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 27);
            this.label22.TabIndex = 6;
            this.label22.Text = "label3";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(400, 1);
            this.label23.TabIndex = 5;
            this.label23.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlMapping);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel1.Size = new System.Drawing.Size(400, 31);
            this.panel1.TabIndex = 2;
            // 
            // pnlMapping
            // 
            this.pnlMapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMapping.Controls.Add(this.chkUnsusedOnly);
            this.pnlMapping.Controls.Add(this.chkMapping);
            this.pnlMapping.Controls.Add(this.label2);
            this.pnlMapping.Controls.Add(this.label3);
            this.pnlMapping.Controls.Add(this.label4);
            this.pnlMapping.Controls.Add(this.label5);
            this.pnlMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapping.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMapping.ForeColor = System.Drawing.Color.White;
            this.pnlMapping.Location = new System.Drawing.Point(0, 0);
            this.pnlMapping.Name = "pnlMapping";
            this.pnlMapping.Size = new System.Drawing.Size(400, 28);
            this.pnlMapping.TabIndex = 2;
            // 
            // chkUnsusedOnly
            // 
            this.chkUnsusedOnly.AutoSize = true;
            this.chkUnsusedOnly.Checked = true;
            this.chkUnsusedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUnsusedOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkUnsusedOnly.Location = new System.Drawing.Point(12, 6);
            this.chkUnsusedOnly.Name = "chkUnsusedOnly";
            this.chkUnsusedOnly.Size = new System.Drawing.Size(149, 18);
            this.chkUnsusedOnly.TabIndex = 0;
            this.chkUnsusedOnly.Text = "Show unused codes";
            this.chkUnsusedOnly.UseVisualStyleBackColor = true;
            this.chkUnsusedOnly.CheckedChanged += new System.EventHandler(this.chkUnsusedOnly_CheckedChanged);
            // 
            // chkMapping
            // 
            this.chkMapping.AutoSize = true;
            this.chkMapping.Checked = true;
            this.chkMapping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMapping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkMapping.Location = new System.Drawing.Point(232, 6);
            this.chkMapping.Name = "chkMapping";
            this.chkMapping.Size = new System.Drawing.Size(153, 18);
            this.chkMapping.TabIndex = 1;
            this.chkMapping.Text = "Show mapped codes";
            this.chkMapping.UseVisualStyleBackColor = true;
            this.chkMapping.CheckedChanged += new System.EventHandler(this.chkMapping_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(1, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(398, 1);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(399, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "label3";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(400, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label1";
            // 
            // pnlIndicator
            // 
            this.pnlIndicator.Controls.Add(this.panel3);
            this.pnlIndicator.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIndicator.Location = new System.Drawing.Point(0, 28);
            this.pnlIndicator.Name = "pnlIndicator";
            this.pnlIndicator.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlIndicator.Size = new System.Drawing.Size(400, 31);
            this.pnlIndicator.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbICD9Gallery);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 28);
            this.panel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(1, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(398, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // cmbICD9Gallery
            // 
            this.cmbICD9Gallery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbICD9Gallery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbICD9Gallery.ForeColor = System.Drawing.Color.Black;
            this.cmbICD9Gallery.FormattingEnabled = true;
            this.cmbICD9Gallery.Location = new System.Drawing.Point(9, 3);
            this.cmbICD9Gallery.Name = "cmbICD9Gallery";
            this.cmbICD9Gallery.Size = new System.Drawing.Size(384, 22);
            this.cmbICD9Gallery.TabIndex = 0;
            this.cmbICD9Gallery.SelectionChangeCommitted += new System.EventHandler(this.cmbICD9Gallery_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 27);
            this.label6.TabIndex = 7;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(399, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 27);
            this.label7.TabIndex = 6;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(400, 1);
            this.label8.TabIndex = 5;
            this.label8.Text = "label1";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.panel19);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel18.Size = new System.Drawing.Size(400, 28);
            this.panel18.TabIndex = 5;
            // 
            // panel19
            // 
            this.panel19.BackgroundImage = global::gloGallery.Properties.Resources.Img_Blue2007;
            this.panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel19.Controls.Add(this.btnUnSelectGalleryCode);
            this.panel19.Controls.Add(this.btnSelectAllGalleryCode);
            this.panel19.Controls.Add(this.lblGalleryHeader);
            this.panel19.Controls.Add(this.label30);
            this.panel19.Controls.Add(this.label31);
            this.panel19.Controls.Add(this.label32);
            this.panel19.Controls.Add(this.label33);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel19.ForeColor = System.Drawing.Color.White;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(400, 25);
            this.panel19.TabIndex = 5;
            // 
            // btnUnSelectGalleryCode
            // 
            this.btnUnSelectGalleryCode.BackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectGalleryCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUnSelectGalleryCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUnSelectGalleryCode.FlatAppearance.BorderSize = 0;
            this.btnUnSelectGalleryCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectGalleryCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUnSelectGalleryCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnSelectGalleryCode.Image = ((System.Drawing.Image)(resources.GetObject("btnUnSelectGalleryCode.Image")));
            this.btnUnSelectGalleryCode.Location = new System.Drawing.Point(349, 1);
            this.btnUnSelectGalleryCode.Name = "btnUnSelectGalleryCode";
            this.btnUnSelectGalleryCode.Size = new System.Drawing.Size(25, 23);
            this.btnUnSelectGalleryCode.TabIndex = 0;
            this.ToolTip1.SetToolTip(this.btnUnSelectGalleryCode, "Deselect All");
            this.btnUnSelectGalleryCode.UseVisualStyleBackColor = false;
            this.btnUnSelectGalleryCode.Visible = false;
            this.btnUnSelectGalleryCode.Click += new System.EventHandler(this.btnUnSelectGalleryCode_Click);
            // 
            // btnSelectAllGalleryCode
            // 
            this.btnSelectAllGalleryCode.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllGalleryCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelectAllGalleryCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAllGalleryCode.FlatAppearance.BorderSize = 0;
            this.btnSelectAllGalleryCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllGalleryCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAllGalleryCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllGalleryCode.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllGalleryCode.Image")));
            this.btnSelectAllGalleryCode.Location = new System.Drawing.Point(374, 1);
            this.btnSelectAllGalleryCode.Name = "btnSelectAllGalleryCode";
            this.btnSelectAllGalleryCode.Size = new System.Drawing.Size(25, 23);
            this.btnSelectAllGalleryCode.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.btnSelectAllGalleryCode, "Select All");
            this.btnSelectAllGalleryCode.UseVisualStyleBackColor = false;
            this.btnSelectAllGalleryCode.Click += new System.EventHandler(this.btnSelectAllGalleryCode_Click);
            // 
            // lblGalleryHeader
            // 
            this.lblGalleryHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblGalleryHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGalleryHeader.Location = new System.Drawing.Point(1, 1);
            this.lblGalleryHeader.Name = "lblGalleryHeader";
            this.lblGalleryHeader.Size = new System.Drawing.Size(398, 23);
            this.lblGalleryHeader.TabIndex = 1;
            this.lblGalleryHeader.Text = "Gallery";
            this.lblGalleryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.Location = new System.Drawing.Point(1, 24);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(398, 1);
            this.label30.TabIndex = 8;
            this.label30.Text = "label2";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(0, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 24);
            this.label31.TabIndex = 7;
            this.label31.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(399, 1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 24);
            this.label32.TabIndex = 6;
            this.label32.Text = "label3";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(400, 1);
            this.label33.TabIndex = 5;
            this.label33.Text = "label1";
            // 
            // pnlICD9bottom
            // 
            this.pnlICD9bottom.Controls.Add(this.pnlLeftbottom);
            this.pnlICD9bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlICD9bottom.Location = new System.Drawing.Point(0, 746);
            this.pnlICD9bottom.Name = "pnlICD9bottom";
            this.pnlICD9bottom.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlICD9bottom.Size = new System.Drawing.Size(925, 26);
            this.pnlICD9bottom.TabIndex = 100;
            this.pnlICD9bottom.TabStop = true;
            // 
            // pnlLeftbottom
            // 
            this.pnlLeftbottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLeftbottom.Controls.Add(this.label18);
            this.pnlLeftbottom.Controls.Add(this.pictureBox1);
            this.pnlLeftbottom.Controls.Add(this.lblBlank);
            this.pnlLeftbottom.Controls.Add(this.PicBlank);
            this.pnlLeftbottom.Controls.Add(this.lblrevise);
            this.pnlLeftbottom.Controls.Add(this.PicRevise);
            this.pnlLeftbottom.Controls.Add(this.lblNew);
            this.pnlLeftbottom.Controls.Add(this.PicNew);
            this.pnlLeftbottom.Controls.Add(this.Label16);
            this.pnlLeftbottom.Controls.Add(this.label12);
            this.pnlLeftbottom.Controls.Add(this.label13);
            this.pnlLeftbottom.Controls.Add(this.label14);
            this.pnlLeftbottom.Controls.Add(this.label17);
            this.pnlLeftbottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftbottom.Location = new System.Drawing.Point(3, 0);
            this.pnlLeftbottom.Name = "pnlLeftbottom";
            this.pnlLeftbottom.Size = new System.Drawing.Size(919, 23);
            this.pnlLeftbottom.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(284, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 21);
            this.label18.TabIndex = 30;
            this.label18.Text = "Deleted";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label18.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(267, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lblBlank
            // 
            this.lblBlank.BackColor = System.Drawing.Color.Transparent;
            this.lblBlank.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBlank.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlank.Location = new System.Drawing.Point(177, 1);
            this.lblBlank.Name = "lblBlank";
            this.lblBlank.Size = new System.Drawing.Size(90, 21);
            this.lblBlank.TabIndex = 26;
            this.lblBlank.Text = "No Change";
            this.lblBlank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicBlank
            // 
            this.PicBlank.BackColor = System.Drawing.Color.Transparent;
            this.PicBlank.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBlank.Image = ((System.Drawing.Image)(resources.GetObject("PicBlank.Image")));
            this.PicBlank.Location = new System.Drawing.Point(159, 1);
            this.PicBlank.Name = "PicBlank";
            this.PicBlank.Size = new System.Drawing.Size(18, 21);
            this.PicBlank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBlank.TabIndex = 25;
            this.PicBlank.TabStop = false;
            // 
            // lblrevise
            // 
            this.lblrevise.BackColor = System.Drawing.Color.Transparent;
            this.lblrevise.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblrevise.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrevise.Location = new System.Drawing.Point(89, 1);
            this.lblrevise.Name = "lblrevise";
            this.lblrevise.Size = new System.Drawing.Size(70, 21);
            this.lblrevise.TabIndex = 24;
            this.lblrevise.Text = "Revised";
            this.lblrevise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicRevise
            // 
            this.PicRevise.BackColor = System.Drawing.Color.Transparent;
            this.PicRevise.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicRevise.Image = ((System.Drawing.Image)(resources.GetObject("PicRevise.Image")));
            this.PicRevise.Location = new System.Drawing.Point(71, 1);
            this.PicRevise.Name = "PicRevise";
            this.PicRevise.Size = new System.Drawing.Size(18, 21);
            this.PicRevise.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicRevise.TabIndex = 23;
            this.PicRevise.TabStop = false;
            // 
            // lblNew
            // 
            this.lblNew.BackColor = System.Drawing.Color.Transparent;
            this.lblNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew.Location = new System.Drawing.Point(24, 1);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(47, 21);
            this.lblNew.TabIndex = 22;
            this.lblNew.Text = "New    ";
            this.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicNew
            // 
            this.PicNew.BackColor = System.Drawing.Color.Transparent;
            this.PicNew.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicNew.Image = ((System.Drawing.Image)(resources.GetObject("PicNew.Image")));
            this.PicNew.Location = new System.Drawing.Point(7, 1);
            this.PicNew.Name = "PicNew";
            this.PicNew.Size = new System.Drawing.Size(17, 21);
            this.PicNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicNew.TabIndex = 21;
            this.PicNew.TabStop = false;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(1, 1);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(6, 21);
            this.Label16.TabIndex = 13;
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(1, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(917, 1);
            this.label12.TabIndex = 12;
            this.label12.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 22);
            this.label13.TabIndex = 11;
            this.label13.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(918, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 22);
            this.label14.TabIndex = 10;
            this.label14.Text = "label3";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(919, 1);
            this.label17.TabIndex = 9;
            this.label17.Text = "label1";
            // 
            // pnlTOP
            // 
            this.pnlTOP.AutoSize = true;
            this.pnlTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTOP.Controls.Add(this.lblCopyRight);
            this.pnlTOP.Controls.Add(this.tlICD9CptGallery);
            this.pnlTOP.Controls.Add(this.Label35);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 0);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Size = new System.Drawing.Size(925, 53);
            this.pnlTOP.TabIndex = 1;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyRight.Location = new System.Drawing.Point(518, 21);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(397, 14);
            this.lblCopyRight.TabIndex = 11;
            this.lblCopyRight.Text = "CPT™ copyright  2012 American Medical Association. All rights reserved";
            // 
            // tlICD9CptGallery
            // 
            this.tlICD9CptGallery.BackgroundImage = global::gloGallery.Properties.Resources.Img_Toolstrip;
            this.tlICD9CptGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlICD9CptGallery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlICD9CptGallery.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlICD9CptGallery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbClose});
            this.tlICD9CptGallery.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlICD9CptGallery.Location = new System.Drawing.Point(0, 0);
            this.tlICD9CptGallery.Name = "tlICD9CptGallery";
            this.tlICD9CptGallery.Size = new System.Drawing.Size(925, 53);
            this.tlICD9CptGallery.TabIndex = 10;
            this.tlICD9CptGallery.Text = "ToolStrip1";
            this.tlICD9CptGallery.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlICD9CptGallery_ItemClicked);
            // 
            // tlbClose
            // 
            this.tlbClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbClose.Image = ((System.Drawing.Image)(resources.GetObject("tlbClose.Image")));
            this.tlbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClose.Name = "tlbClose";
            this.tlbClose.Size = new System.Drawing.Size(59, 50);
            this.tlbClose.Tag = "Close";
            this.tlbClose.Text = "  &Close  ";
            this.tlbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbClose.ToolTipText = "Close  ";
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.Transparent;
            this.Label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(522, 26);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(397, 14);
            this.Label35.TabIndex = 9;
            this.Label35.Text = "CPT™ copyright  2010 American Medical Association. All rights reserved";
            // 
            // tlbImportCPT
            // 
            this.tlbImportCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbImportCPT.Image = ((System.Drawing.Image)(resources.GetObject("tlbImportCPT.Image")));
            this.tlbImportCPT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbImportCPT.Name = "tlbImportCPT";
            this.tlbImportCPT.Size = new System.Drawing.Size(81, 50);
            this.tlbImportCPT.Tag = "ImportCPT";
            this.tlbImportCPT.Text = "Import CPT";
            this.tlbImportCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbImportCPT.ToolTipText = "Import CPT";
            this.tlbImportCPT.Visible = false;
            // 
            // tlbImportIDC9
            // 
            this.tlbImportIDC9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbImportIDC9.Image = ((System.Drawing.Image)(resources.GetObject("tlbImportIDC9.Image")));
            this.tlbImportIDC9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbImportIDC9.Name = "tlbImportIDC9";
            this.tlbImportIDC9.Size = new System.Drawing.Size(88, 50);
            this.tlbImportIDC9.Tag = "ImportICD9";
            this.tlbImportIDC9.Text = "Import ICD9";
            this.tlbImportIDC9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbImportIDC9.ToolTipText = "Import ICD9";
            this.tlbImportIDC9.Visible = false;
            // 
            // tlbClearAll
            // 
            this.tlbClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tlbClearAll.Image")));
            this.tlbClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClearAll.Name = "tlbClearAll";
            this.tlbClearAll.Size = new System.Drawing.Size(60, 50);
            this.tlbClearAll.Tag = "ClearAll";
            this.tlbClearAll.Text = "Clear All";
            this.tlbClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbClearAll.ToolTipText = "Clear All";
            this.tlbClearAll.Visible = false;
            // 
            // tlbSelectAll
            // 
            this.tlbSelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tlbSelectAll.Image")));
            this.tlbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbSelectAll.Name = "tlbSelectAll";
            this.tlbSelectAll.Size = new System.Drawing.Size(67, 50);
            this.tlbSelectAll.Tag = "SelectAll";
            this.tlbSelectAll.Text = "Select All";
            this.tlbSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbSelectAll.ToolTipText = "Select All";
            this.tlbSelectAll.Visible = false;
            // 
            // tlbCPTGallery
            // 
            this.tlbCPTGallery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbCPTGallery.Image = ((System.Drawing.Image)(resources.GetObject("tlbCPTGallery.Image")));
            this.tlbCPTGallery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbCPTGallery.Name = "tlbCPTGallery";
            this.tlbCPTGallery.Size = new System.Drawing.Size(78, 50);
            this.tlbCPTGallery.Tag = "CPTGallery";
            this.tlbCPTGallery.Text = "CPT Gallery";
            this.tlbCPTGallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbCPTGallery.ToolTipText = "CPT Gallery";
            this.tlbCPTGallery.Visible = false;
            // 
            // tlbICD9Gallery
            // 
            this.tlbICD9Gallery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbICD9Gallery.Image = ((System.Drawing.Image)(resources.GetObject("tlbICD9Gallery.Image")));
            this.tlbICD9Gallery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbICD9Gallery.Name = "tlbICD9Gallery";
            this.tlbICD9Gallery.Size = new System.Drawing.Size(85, 50);
            this.tlbICD9Gallery.Tag = "ICD9Gallery";
            this.tlbICD9Gallery.Text = "ICD9 Gallery";
            this.tlbICD9Gallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbICD9Gallery.ToolTipText = "ICD9 Gallery";
            this.tlbICD9Gallery.Visible = false;
            // 
            // tlbCurrentCPT
            // 
            this.tlbCurrentCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbCurrentCPT.Image = ((System.Drawing.Image)(resources.GetObject("tlbCurrentCPT.Image")));
            this.tlbCurrentCPT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbCurrentCPT.Name = "tlbCurrentCPT";
            this.tlbCurrentCPT.Size = new System.Drawing.Size(85, 50);
            this.tlbCurrentCPT.Tag = "CurrentCPT";
            this.tlbCurrentCPT.Text = "Current CPT";
            this.tlbCurrentCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbCurrentCPT.ToolTipText = "Current CPT";
            this.tlbCurrentCPT.Visible = false;
            // 
            // tlbCurrentICD9
            // 
            this.tlbCurrentICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbCurrentICD9.Image = ((System.Drawing.Image)(resources.GetObject("tlbCurrentICD9.Image")));
            this.tlbCurrentICD9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbCurrentICD9.Name = "tlbCurrentICD9";
            this.tlbCurrentICD9.Size = new System.Drawing.Size(92, 50);
            this.tlbCurrentICD9.Tag = "CurrentICD9";
            this.tlbCurrentICD9.Text = "Current ICD9";
            this.tlbCurrentICD9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbCurrentICD9.ToolTipText = "Current ICD9";
            this.tlbCurrentICD9.Visible = false;
            // 
            // frmICD9CPTGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(925, 772);
            this.Controls.Add(this.frmMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmICD9CPTGallery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ICD9-CPT Gallery";
            this.Load += new System.EventHandler(this.frmICD9CPTGallery_Load);
            this.frmMain.ResumeLayout(false);
            this.frmMain.PerformLayout();
            this.pnlTab.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.pnlCategory.ResumeLayout(false);
            this.pnlSpeciality.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.pnlActivationDatesFilter.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlSortBy.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlMapping.ResumeLayout(false);
            this.pnlMapping.PerformLayout();
            this.pnlIndicator.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.pnlICD9bottom.ResumeLayout(false);
            this.pnlLeftbottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBlank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicRevise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicNew)).EndInit();
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.tlICD9CptGallery.ResumeLayout(false);
            this.tlICD9CptGallery.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Panel frmMain;
        internal System.Windows.Forms.Panel pnlTOP;
        internal System.Windows.Forms.Panel pnlTab;
        internal System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.ToolTip ToolTip1;
        #endregion
        internal System.Windows.Forms.ImageList ImgICD9CPT;
        internal gloGlobal.gloToolStripIgnoreFocus tlICD9CptGallery;
        internal System.Windows.Forms.ToolStripButton tlbImportCPT;
        internal System.Windows.Forms.ToolStripButton tlbImportIDC9;
        internal System.Windows.Forms.ToolStripButton tlbClearAll;
        internal System.Windows.Forms.ToolStripButton tlbSelectAll;
        internal System.Windows.Forms.ToolStripButton tlbCPTGallery;
        internal System.Windows.Forms.ToolStripButton tlbICD9Gallery;
        internal System.Windows.Forms.ToolStripButton tlbCurrentCPT;
        internal System.Windows.Forms.ToolStripButton tlbCurrentICD9;
        internal System.Windows.Forms.ToolStripButton tlbClose;
        internal System.Windows.Forms.Label lblCopyRight;
        internal System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.Button btnRemoveCode;
        internal System.Windows.Forms.Button btnAddCode;
        internal System.Windows.Forms.Panel panel12;
        internal gloUC_TreeView gloUCMaster;
        internal System.Windows.Forms.Panel pnlCategory;
        internal System.Windows.Forms.ComboBox cmbCategory;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Panel pnlSpeciality;
        internal System.Windows.Forms.ComboBox cmbSpeciality;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Panel panel15;
        internal System.Windows.Forms.Panel panel16;
        internal System.Windows.Forms.Button btnUnSelectAllMasterCodes;
        internal System.Windows.Forms.Button btnSelectAllMasterCodes;
        internal System.Windows.Forms.Label lblMaster;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        internal System.Windows.Forms.Panel panel17;
        internal gloUC_TreeView gloUCGallery;
        internal System.Windows.Forms.Panel panel18;
        internal System.Windows.Forms.Panel panel19;
        internal System.Windows.Forms.Button btnUnSelectGalleryCode;
        internal System.Windows.Forms.Button btnSelectAllGalleryCode;
        internal System.Windows.Forms.Label lblGalleryHeader;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel pnlMapping;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cmbICD9Gallery;
        internal System.Windows.Forms.Panel pnlIndicator;
        internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Panel pnlICD9bottom;
        internal System.Windows.Forms.Panel pnlLeftbottom;
        internal System.Windows.Forms.Label lblBlank;
        internal System.Windows.Forms.PictureBox PicBlank;
        internal System.Windows.Forms.Label lblrevise;
        internal System.Windows.Forms.PictureBox PicRevise;
        internal System.Windows.Forms.Label lblNew;
        internal System.Windows.Forms.PictureBox PicNew;
        internal System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkUnsusedOnly;
        private System.Windows.Forms.CheckBox chkMapping;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel pnlSortBy;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.RadioButton rbDescription;
        private System.Windows.Forms.RadioButton rbCode;
        internal System.Windows.Forms.Panel pnlActivationDatesFilter;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Label lblActivationDate;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.ComboBox cmbActivationDate;
    }
}