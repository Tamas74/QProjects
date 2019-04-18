namespace gloOffice
{
    partial class frmViewTemplateGallery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewTemplateGallery));
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ADD = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsbCatTemplates = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_GetData = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.lbl_pnlBaseBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseLeftBrd = new System.Windows.Forms.Label();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.imgCommon = new System.Windows.Forms.ImageList(this.components);
            this.lbl_pnlBaseRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlBaseTopBrd = new System.Windows.Forms.Label();
            this.pnltrvSerchVWTempateGallery = new System.Windows.Forms.Panel();
            this.trvSerchVWTempateGallery = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnltrvSerchAppBookBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookRightBrd = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1TemplateGallery = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTemplateSearch = new System.Windows.Forms.TextBox();
            this.lblTemplateSearch = new System.Windows.Forms.Label();
            this.cmbProviderSearch = new System.Windows.Forms.ComboBox();
            this.lblProviderSearch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_ToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.pnltrvSerchVWTempateGallery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TemplateGallery)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.ts_Commands);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(856, 54);
            this.pnl_ToolStrip.TabIndex = 14;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ADD,
            this.tsb_Modify,
            this.tsb_Delete,
            this.tsb_Refresh,
            this.tsbCatTemplates,
            this.tsb_GetData,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Padding = new System.Windows.Forms.Padding(0);
            this.ts_Commands.Size = new System.Drawing.Size(856, 53);
            this.ts_Commands.TabIndex = 9;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_ADD
            // 
            this.tsb_ADD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ADD.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ADD.Image")));
            this.tsb_ADD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_ADD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ADD.Name = "tsb_ADD";
            this.tsb_ADD.Size = new System.Drawing.Size(37, 50);
            this.tsb_ADD.Tag = "New";
            this.tsb_ADD.Text = "&New";
            this.tsb_ADD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ADD.ToolTipText = "New";
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tsb_Modify.Tag = "Modify";
            this.tsb_Modify.Text = "&Modify";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbCatTemplates
            // 
            this.tsbCatTemplates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbCatTemplates.Image = ((System.Drawing.Image)(resources.GetObject("tsbCatTemplates.Image")));
            this.tsbCatTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCatTemplates.Name = "tsbCatTemplates";
            this.tsbCatTemplates.Size = new System.Drawing.Size(82, 50);
            this.tsbCatTemplates.Tag = "Templates";
            this.tsbCatTemplates.Text = "Templates";
            this.tsbCatTemplates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCatTemplates.Visible = false;
            // 
            // tsb_GetData
            // 
            this.tsb_GetData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_GetData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_GetData.Image")));
            this.tsb_GetData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_GetData.Name = "tsb_GetData";
            this.tsb_GetData.Size = new System.Drawing.Size(65, 50);
            this.tsb_GetData.Tag = "GetData";
            this.tsb_GetData.Text = "&Get Data";
            this.tsb_GetData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_GetData.Visible = false;
            this.tsb_GetData.Click += new System.EventHandler(this.tsb_GetData_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlBase);
            this.pnlLeft.Controls.Add(this.pnltrvSerchVWTempateGallery);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 54);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(199, 593);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlBase.Controls.Add(this.lbl_pnlBaseBottomBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseLeftBrd);
            this.pnlBase.Controls.Add(this.trvCategory);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseRightBrd);
            this.pnlBase.Controls.Add(this.lbl_pnlBaseTopBrd);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlBase.Location = new System.Drawing.Point(0, 28);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlBase.Size = new System.Drawing.Size(199, 565);
            this.pnlBase.TabIndex = 1;
            // 
            // lbl_pnlBaseBottomBrd
            // 
            this.lbl_pnlBaseBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBaseBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseBottomBrd.Location = new System.Drawing.Point(4, 561);
            this.lbl_pnlBaseBottomBrd.Name = "lbl_pnlBaseBottomBrd";
            this.lbl_pnlBaseBottomBrd.Size = new System.Drawing.Size(194, 1);
            this.lbl_pnlBaseBottomBrd.TabIndex = 4;
            this.lbl_pnlBaseBottomBrd.Text = "label2";
            // 
            // lbl_pnlBaseLeftBrd
            // 
            this.lbl_pnlBaseLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlBaseLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlBaseLeftBrd.Name = "lbl_pnlBaseLeftBrd";
            this.lbl_pnlBaseLeftBrd.Size = new System.Drawing.Size(1, 558);
            this.lbl_pnlBaseLeftBrd.TabIndex = 3;
            this.lbl_pnlBaseLeftBrd.Text = "label4";
            // 
            // trvCategory
            // 
            this.trvCategory.BackColor = System.Drawing.Color.White;
            this.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvCategory.ForeColor = System.Drawing.Color.Black;
            this.trvCategory.HideSelection = false;
            this.trvCategory.ImageIndex = 0;
            this.trvCategory.ImageList = this.imgCommon;
            this.trvCategory.Indent = 20;
            this.trvCategory.ItemHeight = 20;
            this.trvCategory.Location = new System.Drawing.Point(3, 4);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.SelectedImageIndex = 0;
            this.trvCategory.ShowLines = false;
            this.trvCategory.ShowNodeToolTips = true;
            this.trvCategory.ShowRootLines = false;
            this.trvCategory.Size = new System.Drawing.Size(195, 558);
            this.trvCategory.TabIndex = 0;
            this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
            // 
            // imgCommon
            // 
            this.imgCommon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgCommon.ImageStream")));
            this.imgCommon.TransparentColor = System.Drawing.Color.Transparent;
            this.imgCommon.Images.SetKeyName(0, "Bullet06.ico");
            // 
            // lbl_pnlBaseRightBrd
            // 
            this.lbl_pnlBaseRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlBaseRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlBaseRightBrd.Location = new System.Drawing.Point(198, 4);
            this.lbl_pnlBaseRightBrd.Name = "lbl_pnlBaseRightBrd";
            this.lbl_pnlBaseRightBrd.Size = new System.Drawing.Size(1, 558);
            this.lbl_pnlBaseRightBrd.TabIndex = 2;
            this.lbl_pnlBaseRightBrd.Text = "label3";
            // 
            // lbl_pnlBaseTopBrd
            // 
            this.lbl_pnlBaseTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBaseTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlBaseTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlBaseTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlBaseTopBrd.Name = "lbl_pnlBaseTopBrd";
            this.lbl_pnlBaseTopBrd.Size = new System.Drawing.Size(196, 1);
            this.lbl_pnlBaseTopBrd.TabIndex = 0;
            this.lbl_pnlBaseTopBrd.Text = "label1";
            // 
            // pnltrvSerchVWTempateGallery
            // 
            this.pnltrvSerchVWTempateGallery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.trvSerchVWTempateGallery);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.PicBx_Search);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_pnltrvSerchAppBookBottomBrd);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_pnltrvSerchAppBookTopBrd);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_pnltrvSerchAppBookLeftBrd);
            this.pnltrvSerchVWTempateGallery.Controls.Add(this.lbl_pnltrvSerchAppBookRightBrd);
            this.pnltrvSerchVWTempateGallery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltrvSerchVWTempateGallery.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltrvSerchVWTempateGallery.ForeColor = System.Drawing.Color.Black;
            this.pnltrvSerchVWTempateGallery.Location = new System.Drawing.Point(0, 0);
            this.pnltrvSerchVWTempateGallery.Name = "pnltrvSerchVWTempateGallery";
            this.pnltrvSerchVWTempateGallery.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnltrvSerchVWTempateGallery.Size = new System.Drawing.Size(199, 28);
            this.pnltrvSerchVWTempateGallery.TabIndex = 0;
            this.pnltrvSerchVWTempateGallery.Visible = false;
            // 
            // trvSerchVWTempateGallery
            // 
            this.trvSerchVWTempateGallery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvSerchVWTempateGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSerchVWTempateGallery.ForeColor = System.Drawing.Color.Black;
            this.trvSerchVWTempateGallery.Location = new System.Drawing.Point(32, 8);
            this.trvSerchVWTempateGallery.Name = "trvSerchVWTempateGallery";
            this.trvSerchVWTempateGallery.Size = new System.Drawing.Size(166, 15);
            this.trvSerchVWTempateGallery.TabIndex = 0;
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(32, 4);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(166, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(32, 22);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(166, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(4, 4);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 20);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnltrvSerchAppBookBottomBrd
            // 
            this.lbl_pnltrvSerchAppBookBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnltrvSerchAppBookBottomBrd.Location = new System.Drawing.Point(4, 24);
            this.lbl_pnltrvSerchAppBookBottomBrd.Name = "lbl_pnltrvSerchAppBookBottomBrd";
            this.lbl_pnltrvSerchAppBookBottomBrd.Size = new System.Drawing.Size(194, 1);
            this.lbl_pnltrvSerchAppBookBottomBrd.TabIndex = 35;
            this.lbl_pnltrvSerchAppBookBottomBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookTopBrd
            // 
            this.lbl_pnltrvSerchAppBookTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnltrvSerchAppBookTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnltrvSerchAppBookTopBrd.Name = "lbl_pnltrvSerchAppBookTopBrd";
            this.lbl_pnltrvSerchAppBookTopBrd.Size = new System.Drawing.Size(194, 1);
            this.lbl_pnltrvSerchAppBookTopBrd.TabIndex = 36;
            this.lbl_pnltrvSerchAppBookTopBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookLeftBrd
            // 
            this.lbl_pnltrvSerchAppBookLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnltrvSerchAppBookLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnltrvSerchAppBookLeftBrd.Name = "lbl_pnltrvSerchAppBookLeftBrd";
            this.lbl_pnltrvSerchAppBookLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnltrvSerchAppBookLeftBrd.TabIndex = 39;
            this.lbl_pnltrvSerchAppBookLeftBrd.Text = "label4";
            // 
            // lbl_pnltrvSerchAppBookRightBrd
            // 
            this.lbl_pnltrvSerchAppBookRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnltrvSerchAppBookRightBrd.Location = new System.Drawing.Point(198, 3);
            this.lbl_pnltrvSerchAppBookRightBrd.Name = "lbl_pnltrvSerchAppBookRightBrd";
            this.lbl_pnltrvSerchAppBookRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnltrvSerchAppBookRightBrd.TabIndex = 40;
            this.lbl_pnltrvSerchAppBookRightBrd.Text = "label4";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(199, 54);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 593);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(202, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(654, 593);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.lbl_BottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_LeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_RightBrd);
            this.pnlSearch.Controls.Add(this.lbl_TopBrd);
            this.pnlSearch.Controls.Add(this.c1TemplateGallery);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 30);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(654, 563);
            this.pnlSearch.TabIndex = 0;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 559);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_BottomBrd.TabIndex = 26;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 559);
            this.lbl_LeftBrd.TabIndex = 25;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(650, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 559);
            this.lbl_RightBrd.TabIndex = 24;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(651, 1);
            this.lbl_TopBrd.TabIndex = 23;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1TemplateGallery
            // 
            this.c1TemplateGallery.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1TemplateGallery.AllowEditing = false;
            this.c1TemplateGallery.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1TemplateGallery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1TemplateGallery.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1TemplateGallery.ColumnInfo = "0,0,0,0,0,105,Columns:";
            this.c1TemplateGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TemplateGallery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1TemplateGallery.Location = new System.Drawing.Point(0, 0);
            this.c1TemplateGallery.Name = "c1TemplateGallery";
            this.c1TemplateGallery.Rows.Count = 1;
            this.c1TemplateGallery.Rows.DefaultSize = 21;
            this.c1TemplateGallery.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1TemplateGallery.Size = new System.Drawing.Size(651, 560);
            this.c1TemplateGallery.StyleInfo = resources.GetString("c1TemplateGallery.StyleInfo");
            this.c1TemplateGallery.TabIndex = 22;
            this.c1TemplateGallery.DoubleClick += new System.EventHandler(this.c1TemplateGallery_DoubleClick);
            this.c1TemplateGallery.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1TemplateGallery_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel1.Size = new System.Drawing.Size(654, 30);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.txtTemplateSearch);
            this.panel3.Controls.Add(this.lblTemplateSearch);
            this.panel3.Controls.Add(this.cmbProviderSearch);
            this.panel3.Controls.Add(this.lblProviderSearch);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(651, 24);
            this.panel3.TabIndex = 0;
            // 
            // txtTemplateSearch
            // 
            this.txtTemplateSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateSearch.ForeColor = System.Drawing.Color.Black;
            this.txtTemplateSearch.Location = new System.Drawing.Point(480, 1);
            this.txtTemplateSearch.Name = "txtTemplateSearch";
            this.txtTemplateSearch.Size = new System.Drawing.Size(170, 22);
            this.txtTemplateSearch.TabIndex = 2;
            this.txtTemplateSearch.TextChanged += new System.EventHandler(this.txtTemplateSearch_TextChanged);
            // 
            // lblTemplateSearch
            // 
            this.lblTemplateSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTemplateSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateSearch.Location = new System.Drawing.Point(324, 1);
            this.lblTemplateSearch.Name = "lblTemplateSearch";
            this.lblTemplateSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTemplateSearch.Size = new System.Drawing.Size(156, 22);
            this.lblTemplateSearch.TabIndex = 17;
            this.lblTemplateSearch.Text = "Template Search :";
            this.lblTemplateSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProviderSearch
            // 
            this.cmbProviderSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbProviderSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviderSearch.FormattingEnabled = true;
            this.cmbProviderSearch.Location = new System.Drawing.Point(95, 1);
            this.cmbProviderSearch.Name = "cmbProviderSearch";
            this.cmbProviderSearch.Size = new System.Drawing.Size(229, 22);
            this.cmbProviderSearch.TabIndex = 0;
            this.cmbProviderSearch.SelectionChangeCommitted += new System.EventHandler(this.cmbProviderSearch_SelectionChangeCommitted);
            // 
            // lblProviderSearch
            // 
            this.lblProviderSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblProviderSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProviderSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderSearch.Location = new System.Drawing.Point(1, 1);
            this.lblProviderSearch.Name = "lblProviderSearch";
            this.lblProviderSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblProviderSearch.Size = new System.Drawing.Size(94, 22);
            this.lblProviderSearch.TabIndex = 6;
            this.lblProviderSearch.Text = "Provider : ";
            this.lblProviderSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(649, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(650, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(651, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // frmViewTemplateGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(856, 647);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewTemplateGallery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Template Gallery";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmViewTemplateGallery_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewTemplateGallery_FormClosed);
            this.Load += new System.EventHandler(this.frmViewTemplateGallery_Load);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlBase.ResumeLayout(false);
            this.pnltrvSerchVWTempateGallery.ResumeLayout(false);
            this.pnltrvSerchVWTempateGallery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TemplateGallery)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_ADD;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Label lbl_pnlBaseBottomBrd;
        private System.Windows.Forms.Label lbl_pnlBaseLeftBrd;
        private System.Windows.Forms.TreeView trvCategory;
        private System.Windows.Forms.Label lbl_pnlBaseRightBrd;
        private System.Windows.Forms.Label lbl_pnlBaseTopBrd;
        internal System.Windows.Forms.Panel pnltrvSerchVWTempateGallery;
        private System.Windows.Forms.TextBox trvSerchVWTempateGallery;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookBottomBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookTopBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookLeftBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookRightBrd;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid c1TemplateGallery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTemplateSearch;
        private System.Windows.Forms.Label lblTemplateSearch;
        private System.Windows.Forms.ComboBox cmbProviderSearch;
        private System.Windows.Forms.Label lblProviderSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripDropDownButton tsbCatTemplates;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.ToolStripButton tsb_GetData;
        private System.Windows.Forms.ImageList imgCommon;
    }
}