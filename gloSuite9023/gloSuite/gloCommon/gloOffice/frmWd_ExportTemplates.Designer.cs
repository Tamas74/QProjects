namespace gloOffice
{
    partial class frmWd_ExportTemplates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWd_ExportTemplates));
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.imgtrvCategory = new System.Windows.Forms.ImageList(this.components);
            this.pnlc1Templates = new System.Windows.Forms.Panel();
            this.c1Templates = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalTemplates = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlProviderSearchHeader = new System.Windows.Forms.Panel();
            this.pnlProviderSearch = new System.Windows.Forms.Panel();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblProviderSearch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnltrvCategory = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnltxtDirectoryPathHeader = new System.Windows.Forms.Panel();
            this.pnltxtDirectoryPath = new System.Windows.Forms.Panel();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_SelectAllCategory = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearAllCategory = new System.Windows.Forms.ToolStripButton();
            this.tsb_SelectAllDocument = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearAllDocument = new System.Windows.Forms.ToolStripButton();
            this.tsb_ExportTemplates = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnltsCommands = new System.Windows.Forms.Panel();
            this.sptLefttrvCategory = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlc1Templates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Templates)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlProviderSearchHeader.SuspendLayout();
            this.pnlProviderSearch.SuspendLayout();
            this.pnltrvCategory.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnltxtDirectoryPathHeader.SuspendLayout();
            this.pnltxtDirectoryPath.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnltsCommands.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvCategory
            // 
            this.trvCategory.BackColor = System.Drawing.Color.White;
            this.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategory.CheckBoxes = true;
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.ForeColor = System.Drawing.Color.Black;
            this.trvCategory.HideSelection = false;
            this.trvCategory.ImageIndex = 0;
            this.trvCategory.ImageList = this.imgtrvCategory;
            this.trvCategory.Indent = 20;
            this.trvCategory.ItemHeight = 20;
            this.trvCategory.Location = new System.Drawing.Point(3, 1);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.SelectedImageIndex = 0;
            this.trvCategory.ShowLines = false;
            this.trvCategory.ShowNodeToolTips = true;
            this.trvCategory.ShowRootLines = false;
            this.trvCategory.Size = new System.Drawing.Size(271, 620);
            this.trvCategory.TabIndex = 0;
            this.trvCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterCheck);
            // 
            // imgtrvCategory
            // 
            this.imgtrvCategory.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgtrvCategory.ImageStream")));
            this.imgtrvCategory.TransparentColor = System.Drawing.Color.Transparent;
            this.imgtrvCategory.Images.SetKeyName(0, "Bullet06.ico");
            this.imgtrvCategory.Images.SetKeyName(1, "Small Arrow.ico");
            // 
            // pnlc1Templates
            // 
            this.pnlc1Templates.Controls.Add(this.c1Templates);
            this.pnlc1Templates.Controls.Add(this.label14);
            this.pnlc1Templates.Controls.Add(this.label15);
            this.pnlc1Templates.Controls.Add(this.label16);
            this.pnlc1Templates.Controls.Add(this.label17);
            this.pnlc1Templates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1Templates.Location = new System.Drawing.Point(278, 116);
            this.pnlc1Templates.Name = "pnlc1Templates";
            this.pnlc1Templates.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlc1Templates.Size = new System.Drawing.Size(750, 595);
            this.pnlc1Templates.TabIndex = 2;
            // 
            // c1Templates
            // 
            this.c1Templates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Templates.AllowEditing = false;
            this.c1Templates.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Templates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Templates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Templates.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1Templates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Templates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Templates.Location = new System.Drawing.Point(1, 1);
            this.c1Templates.Name = "c1Templates";
            this.c1Templates.Rows.Count = 1;
            this.c1Templates.Rows.DefaultSize = 19;
            this.c1Templates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Templates.Size = new System.Drawing.Size(745, 590);
            this.c1Templates.StyleInfo = resources.GetString("c1Templates.StyleInfo");
            this.c1Templates.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(1, 591);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(745, 1);
            this.label14.TabIndex = 216;
            this.label14.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 591);
            this.label15.TabIndex = 215;
            this.label15.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(746, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 591);
            this.label16.TabIndex = 214;
            this.label16.Text = "label3";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(747, 1);
            this.label17.TabIndex = 213;
            this.label17.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblTotalTemplates);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 26);
            this.panel2.TabIndex = 218;
            // 
            // lblTotalTemplates
            // 
            this.lblTotalTemplates.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTemplates.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalTemplates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTemplates.Location = new System.Drawing.Point(1, 1);
            this.lblTotalTemplates.Name = "lblTotalTemplates";
            this.lblTotalTemplates.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTotalTemplates.Size = new System.Drawing.Size(154, 24);
            this.lblTotalTemplates.TabIndex = 6;
            this.lblTotalTemplates.Text = "  Total Templates : 0";
            this.lblTotalTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(1, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(745, 1);
            this.label25.TabIndex = 8;
            this.label25.Text = "label2";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 25);
            this.label26.TabIndex = 7;
            this.label26.Text = "label4";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.Location = new System.Drawing.Point(746, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 25);
            this.label27.TabIndex = 6;
            this.label27.Text = "label27";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(747, 1);
            this.label28.TabIndex = 5;
            this.label28.Text = "label1";
            // 
            // pnlProviderSearchHeader
            // 
            this.pnlProviderSearchHeader.Controls.Add(this.pnlProviderSearch);
            this.pnlProviderSearchHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderSearchHeader.Location = new System.Drawing.Point(278, 86);
            this.pnlProviderSearchHeader.Name = "pnlProviderSearchHeader";
            this.pnlProviderSearchHeader.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlProviderSearchHeader.Size = new System.Drawing.Size(750, 30);
            this.pnlProviderSearchHeader.TabIndex = 3;
            // 
            // pnlProviderSearch
            // 
            this.pnlProviderSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlProviderSearch.BackgroundImage")));
            this.pnlProviderSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProviderSearch.Controls.Add(this.cmbProvider);
            this.pnlProviderSearch.Controls.Add(this.lblProviderSearch);
            this.pnlProviderSearch.Controls.Add(this.label1);
            this.pnlProviderSearch.Controls.Add(this.label2);
            this.pnlProviderSearch.Controls.Add(this.label3);
            this.pnlProviderSearch.Controls.Add(this.label4);
            this.pnlProviderSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProviderSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlProviderSearch.Name = "pnlProviderSearch";
            this.pnlProviderSearch.Size = new System.Drawing.Size(747, 27);
            this.pnlProviderSearch.TabIndex = 0;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(88, 2);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(266, 22);
            this.cmbProvider.TabIndex = 0;
            this.cmbProvider.SelectionChangeCommitted += new System.EventHandler(this.cmbProvider_SelectionChangeCommitted);
            // 
            // lblProviderSearch
            // 
            this.lblProviderSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblProviderSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProviderSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderSearch.Location = new System.Drawing.Point(1, 1);
            this.lblProviderSearch.Name = "lblProviderSearch";
            this.lblProviderSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblProviderSearch.Size = new System.Drawing.Size(82, 25);
            this.lblProviderSearch.TabIndex = 6;
            this.lblProviderSearch.Text = "Provider : ";
            this.lblProviderSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(745, 1);
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
            this.label2.Size = new System.Drawing.Size(1, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(746, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 26);
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
            this.label4.Size = new System.Drawing.Size(747, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // pnltrvCategory
            // 
            this.pnltrvCategory.Controls.Add(this.panel4);
            this.pnltrvCategory.Controls.Add(this.panel3);
            this.pnltrvCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltrvCategory.Location = new System.Drawing.Point(0, 86);
            this.pnltrvCategory.Name = "pnltrvCategory";
            this.pnltrvCategory.Size = new System.Drawing.Size(275, 654);
            this.pnltrvCategory.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.trvCategory);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel4.Size = new System.Drawing.Size(275, 624);
            this.panel4.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(4, 620);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(270, 1);
            this.label10.TabIndex = 8;
            this.label10.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 620);
            this.label11.TabIndex = 7;
            this.label11.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(274, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 620);
            this.label12.TabIndex = 6;
            this.label12.Text = "label3";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(272, 1);
            this.label13.TabIndex = 5;
            this.label13.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(275, 30);
            this.panel3.TabIndex = 40;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 27);
            this.panel1.TabIndex = 39;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1, 1);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label19.Size = new System.Drawing.Size(154, 25);
            this.label19.TabIndex = 6;
            this.label19.Text = " Template Catagories";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label20.Location = new System.Drawing.Point(1, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(270, 1);
            this.label20.TabIndex = 8;
            this.label20.Text = "label2";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(0, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 26);
            this.label21.TabIndex = 7;
            this.label21.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(271, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 26);
            this.label22.TabIndex = 6;
            this.label22.Text = "label22";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(272, 1);
            this.label23.TabIndex = 5;
            this.label23.Text = "label1";
            // 
            // pnltxtDirectoryPathHeader
            // 
            this.pnltxtDirectoryPathHeader.Controls.Add(this.pnltxtDirectoryPath);
            this.pnltxtDirectoryPathHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltxtDirectoryPathHeader.Location = new System.Drawing.Point(0, 54);
            this.pnltxtDirectoryPathHeader.Name = "pnltxtDirectoryPathHeader";
            this.pnltxtDirectoryPathHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnltxtDirectoryPathHeader.Size = new System.Drawing.Size(1028, 32);
            this.pnltxtDirectoryPathHeader.TabIndex = 0;
            // 
            // pnltxtDirectoryPath
            // 
            this.pnltxtDirectoryPath.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.pnltxtDirectoryPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltxtDirectoryPath.Controls.Add(this.btnBrowsePath);
            this.pnltxtDirectoryPath.Controls.Add(this.txtDirectoryPath);
            this.pnltxtDirectoryPath.Controls.Add(this.label5);
            this.pnltxtDirectoryPath.Controls.Add(this.label6);
            this.pnltxtDirectoryPath.Controls.Add(this.label7);
            this.pnltxtDirectoryPath.Controls.Add(this.label8);
            this.pnltxtDirectoryPath.Controls.Add(this.label9);
            this.pnltxtDirectoryPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltxtDirectoryPath.Location = new System.Drawing.Point(3, 3);
            this.pnltxtDirectoryPath.Name = "pnltxtDirectoryPath";
            this.pnltxtDirectoryPath.Size = new System.Drawing.Size(1022, 26);
            this.pnltxtDirectoryPath.TabIndex = 0;
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowsePath.FlatAppearance.BorderSize = 0;
            this.btnBrowsePath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePath.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowsePath.Image")));
            this.btnBrowsePath.Location = new System.Drawing.Point(863, 2);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(27, 21);
            this.btnBrowsePath.TabIndex = 1;
            this.btnBrowsePath.UseVisualStyleBackColor = false;
            this.btnBrowsePath.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            this.btnBrowsePath.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtDirectoryPath
            // 
            this.txtDirectoryPath.BackColor = System.Drawing.Color.White;
            this.txtDirectoryPath.Location = new System.Drawing.Point(102, 2);
            this.txtDirectoryPath.Name = "txtDirectoryPath";
            this.txtDirectoryPath.Size = new System.Drawing.Size(756, 22);
            this.txtDirectoryPath.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(98, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Folder Path : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(1, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1020, 1);
            this.label6.TabIndex = 8;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 25);
            this.label7.TabIndex = 7;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(1021, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 25);
            this.label8.TabIndex = 6;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1022, 1);
            this.label9.TabIndex = 5;
            this.label9.Text = "label1";
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_SelectAllCategory,
            this.tsb_ClearAllCategory,
            this.tsb_SelectAllDocument,
            this.tsb_ClearAllDocument,
            this.tsb_ExportTemplates,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Padding = new System.Windows.Forms.Padding(0);
            this.ts_Commands.Size = new System.Drawing.Size(1028, 53);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_SelectAllCategory
            // 
            this.tsb_SelectAllCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SelectAllCategory.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SelectAllCategory.Image")));
            this.tsb_SelectAllCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectAllCategory.Name = "tsb_SelectAllCategory";
            this.tsb_SelectAllCategory.Size = new System.Drawing.Size(96, 50);
            this.tsb_SelectAllCategory.Tag = "SelectAllCat";
            this.tsb_SelectAllCategory.Text = "&Select All Cat.";
            this.tsb_SelectAllCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectAllCategory.ToolTipText = "Select All Categories";
            // 
            // tsb_ClearAllCategory
            // 
            this.tsb_ClearAllCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClearAllCategory.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClearAllCategory.Image")));
            this.tsb_ClearAllCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearAllCategory.Name = "tsb_ClearAllCategory";
            this.tsb_ClearAllCategory.Size = new System.Drawing.Size(89, 50);
            this.tsb_ClearAllCategory.Tag = "ClearAllCat";
            this.tsb_ClearAllCategory.Text = "Clear &All Cat.";
            this.tsb_ClearAllCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClearAllCategory.ToolTipText = "Clear All Categories";
            this.tsb_ClearAllCategory.Visible = false;
            // 
            // tsb_SelectAllDocument
            // 
            this.tsb_SelectAllDocument.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SelectAllDocument.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SelectAllDocument.Image")));
            this.tsb_SelectAllDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectAllDocument.Name = "tsb_SelectAllDocument";
            this.tsb_SelectAllDocument.Size = new System.Drawing.Size(98, 50);
            this.tsb_SelectAllDocument.Tag = "SelectAllDoc";
            this.tsb_SelectAllDocument.Text = "&Select All Doc.";
            this.tsb_SelectAllDocument.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectAllDocument.ToolTipText = "Select All Documents";
            this.tsb_SelectAllDocument.Visible = false;
            // 
            // tsb_ClearAllDocument
            // 
            this.tsb_ClearAllDocument.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClearAllDocument.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClearAllDocument.Image")));
            this.tsb_ClearAllDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearAllDocument.Name = "tsb_ClearAllDocument";
            this.tsb_ClearAllDocument.Size = new System.Drawing.Size(91, 50);
            this.tsb_ClearAllDocument.Tag = "ClearAllDoc";
            this.tsb_ClearAllDocument.Text = "Clear All &Doc.";
            this.tsb_ClearAllDocument.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClearAllDocument.ToolTipText = "Clear All Documents";
            this.tsb_ClearAllDocument.Visible = false;
            // 
            // tsb_ExportTemplates
            // 
            this.tsb_ExportTemplates.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ExportTemplates.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ExportTemplates.Image")));
            this.tsb_ExportTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ExportTemplates.Name = "tsb_ExportTemplates";
            this.tsb_ExportTemplates.Size = new System.Drawing.Size(118, 50);
            this.tsb_ExportTemplates.Tag = "Export";
            this.tsb_ExportTemplates.Text = "&Export Templates";
            this.tsb_ExportTemplates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // pnltsCommands
            // 
            this.pnltsCommands.Controls.Add(this.ts_Commands);
            this.pnltsCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltsCommands.Location = new System.Drawing.Point(0, 0);
            this.pnltsCommands.Name = "pnltsCommands";
            this.pnltsCommands.Size = new System.Drawing.Size(1028, 54);
            this.pnltsCommands.TabIndex = 213;
            // 
            // sptLefttrvCategory
            // 
            this.sptLefttrvCategory.Location = new System.Drawing.Point(275, 86);
            this.sptLefttrvCategory.Name = "sptLefttrvCategory";
            this.sptLefttrvCategory.Size = new System.Drawing.Size(3, 654);
            this.sptLefttrvCategory.TabIndex = 214;
            this.sptLefttrvCategory.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(278, 711);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(750, 29);
            this.panel5.TabIndex = 215;
            // 
            // frmWd_ExportTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1028, 740);
            this.Controls.Add(this.pnlc1Templates);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlProviderSearchHeader);
            this.Controls.Add(this.sptLefttrvCategory);
            this.Controls.Add(this.pnltrvCategory);
            this.Controls.Add(this.pnltxtDirectoryPathHeader);
            this.Controls.Add(this.pnltsCommands);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWd_ExportTemplates";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Templates";
            this.Load += new System.EventHandler(this.frmWd_ExportTemplates_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWd_ExportTemplates_FormClosed);
            this.pnlc1Templates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Templates)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlProviderSearchHeader.ResumeLayout(false);
            this.pnlProviderSearch.ResumeLayout(false);
            this.pnltrvCategory.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnltxtDirectoryPathHeader.ResumeLayout(false);
            this.pnltxtDirectoryPath.ResumeLayout(false);
            this.pnltxtDirectoryPath.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnltsCommands.ResumeLayout(false);
            this.pnltsCommands.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_SelectAllCategory;
        internal System.Windows.Forms.ToolStripButton tsb_ExportTemplates;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.TreeView trvCategory;
        private System.Windows.Forms.Panel pnlc1Templates;
        private System.Windows.Forms.Panel pnlProviderSearchHeader;
        private System.Windows.Forms.Panel pnlProviderSearch;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label lblProviderSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Templates;
        private System.Windows.Forms.Panel pnltrvCategory;
        private System.Windows.Forms.Panel pnltxtDirectoryPathHeader;
        private System.Windows.Forms.Panel pnltxtDirectoryPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDirectoryPath;
        internal System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnltsCommands;
        private System.Windows.Forms.Splitter sptLefttrvCategory;
        internal System.Windows.Forms.ToolStripButton tsb_SelectAllDocument;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalTemplates;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        internal System.Windows.Forms.ToolStripButton tsb_ClearAllCategory;
        internal System.Windows.Forms.ToolStripButton tsb_ClearAllDocument;
        internal System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ImageList imgtrvCategory;
    }
}