namespace gloContacts
{
    partial class frmInsuranceMultipleCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsuranceMultipleCopy));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnCopy = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlInsurance = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.c1ViewContacts = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSearchMain = new System.Windows.Forms.Panel();
            this.pnl_Search = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Label77 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.picProgress = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_Search = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnlHeaderData = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlInsurance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewContacts)).BeginInit();
            this.pnlSearchMain.SuspendLayout();
            this.pnl_Search.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).BeginInit();
            this.pnlHeaderData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.TopToolStrip);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(760, 55);
            this.pnlTop.TabIndex = 1;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnCopy,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(760, 53);
            this.TopToolStrip.TabIndex = 1;
            this.TopToolStrip.TabStop = true;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnCopy
            // 
            this.ts_btnCopy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ts_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCopy.Image")));
            this.ts_btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCopy.Name = "ts_btnCopy";
            this.ts_btnCopy.Size = new System.Drawing.Size(42, 50);
            this.ts_btnCopy.Tag = "Copy";
            this.ts_btnCopy.Text = "Co&py";
            this.ts_btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCopy.Click += new System.EventHandler(this.ts_btnCopy_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlInsurance);
            this.pnlMain.Controls.Add(this.pnlSearchMain);
            this.pnlMain.Controls.Add(this.pnlHeaderData);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(760, 653);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlInsurance
            // 
            this.pnlInsurance.Controls.Add(this.label10);
            this.pnlInsurance.Controls.Add(this.c1ViewContacts);
            this.pnlInsurance.Controls.Add(this.label6);
            this.pnlInsurance.Controls.Add(this.label5);
            this.pnlInsurance.Controls.Add(this.label3);
            this.pnlInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsurance.Location = new System.Drawing.Point(0, 99);
            this.pnlInsurance.Name = "pnlInsurance";
            this.pnlInsurance.Padding = new System.Windows.Forms.Padding(3);
            this.pnlInsurance.Size = new System.Drawing.Size(760, 554);
            this.pnlInsurance.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(4, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(752, 1);
            this.label10.TabIndex = 27;
            this.label10.Text = "label2";
            // 
            // c1ViewContacts
            // 
            this.c1ViewContacts.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ViewContacts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ViewContacts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ViewContacts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ViewContacts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ViewContacts.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1ViewContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ViewContacts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ViewContacts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ViewContacts.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ViewContacts.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ViewContacts.Location = new System.Drawing.Point(4, 3);
            this.c1ViewContacts.Name = "c1ViewContacts";
            this.c1ViewContacts.Rows.Count = 1;
            this.c1ViewContacts.Rows.DefaultSize = 19;
            this.c1ViewContacts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ViewContacts.ShowCellLabels = true;
            this.c1ViewContacts.Size = new System.Drawing.Size(752, 547);
            this.c1ViewContacts.StyleInfo = resources.GetString("c1ViewContacts.StyleInfo");
            this.c1ViewContacts.TabIndex = 13;
            this.c1ViewContacts.TabStop = false;
            this.c1ViewContacts.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ViewContacts_AfterEdit);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 547);
            this.label6.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(756, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 547);
            this.label5.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 550);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(754, 1);
            this.label3.TabIndex = 10;
            // 
            // pnlSearchMain
            // 
            this.pnlSearchMain.Controls.Add(this.pnl_Search);
            this.pnlSearchMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchMain.Location = new System.Drawing.Point(0, 75);
            this.pnlSearchMain.Name = "pnlSearchMain";
            this.pnlSearchMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlSearchMain.Size = new System.Drawing.Size(760, 24);
            this.pnlSearchMain.TabIndex = 1;
            // 
            // pnl_Search
            // 
            this.pnl_Search.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.pnl_Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Search.Controls.Add(this.panel3);
            this.pnl_Search.Controls.Add(this.lbl_Search);
            this.pnl_Search.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Search.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Search.Controls.Add(this.lbl_RightBrd);
            this.pnl_Search.Controls.Add(this.lbl_TopBrd);
            this.pnl_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Search.Location = new System.Drawing.Point(3, 0);
            this.pnl_Search.Name = "pnl_Search";
            this.pnl_Search.Size = new System.Drawing.Size(754, 24);
            this.pnl_Search.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.Label77);
            this.panel3.Controls.Add(this.txt_search);
            this.panel3.Controls.Add(this.picProgress);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.btnClearSearch);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(146, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(342, 22);
            this.panel3.TabIndex = 3;
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(5, 17);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(291, 5);
            this.Label77.TabIndex = 43;
            // 
            // txt_search
            // 
            this.txt_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_search.Location = new System.Drawing.Point(5, 3);
            this.txt_search.MaxLength = 100;
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(291, 15);
            this.txt_search.TabIndex = 4;
            this.txt_search.Tag = "0";
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // picProgress
            // 
            this.picProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.picProgress.Image = global::gloContacts.Properties.Resources.ajax_loader_2_;
            this.picProgress.Location = new System.Drawing.Point(296, 3);
            this.picProgress.Name = "picProgress";
            this.picProgress.Size = new System.Drawing.Size(20, 19);
            this.picProgress.TabIndex = 45;
            this.picProgress.TabStop = false;
            this.picProgress.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(311, 3);
            this.label8.TabIndex = 37;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.White;
            this.btnClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearch.Image")));
            this.btnClearSearch.Location = new System.Drawing.Point(316, 0);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(25, 22);
            this.btnClearSearch.TabIndex = 30;
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(4, 22);
            this.label9.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 22);
            this.label12.TabIndex = 39;
            this.label12.Text = "label4";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(341, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 22);
            this.label13.TabIndex = 40;
            this.label13.Text = "label4";
            // 
            // lbl_Search
            // 
            this.lbl_Search.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Search.Location = new System.Drawing.Point(1, 1);
            this.lbl_Search.Name = "lbl_Search";
            this.lbl_Search.Size = new System.Drawing.Size(145, 22);
            this.lbl_Search.TabIndex = 21;
            this.lbl_Search.Text = "Search : ";
            this.lbl_Search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 23);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(752, 1);
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
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_LeftBrd.TabIndex = 25;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(753, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 23);
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
            this.lbl_TopBrd.Size = new System.Drawing.Size(754, 1);
            this.lbl_TopBrd.TabIndex = 23;
            this.lbl_TopBrd.Text = "label1";
            // 
            // pnlHeaderData
            // 
            this.pnlHeaderData.Controls.Add(this.label14);
            this.pnlHeaderData.Controls.Add(this.label11);
            this.pnlHeaderData.Controls.Add(this.label7);
            this.pnlHeaderData.Controls.Add(this.label4);
            this.pnlHeaderData.Controls.Add(this.cmbInsurance);
            this.pnlHeaderData.Controls.Add(this.txtSuffix);
            this.pnlHeaderData.Controls.Add(this.label2);
            this.pnlHeaderData.Controls.Add(this.label1);
            this.pnlHeaderData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderData.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaderData.Name = "pnlHeaderData";
            this.pnlHeaderData.Padding = new System.Windows.Forms.Padding(3);
            this.pnlHeaderData.Size = new System.Drawing.Size(760, 75);
            this.pnlHeaderData.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(4, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(752, 1);
            this.label14.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(4, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(752, 1);
            this.label11.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(756, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 69);
            this.label7.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 69);
            this.label4.TabIndex = 13;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInsurance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(149, 41);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(341, 22);
            this.cmbInsurance.TabIndex = 1;
            this.cmbInsurance.SelectedIndexChanged += new System.EventHandler(this.cmbInsurance_SelectedIndexChanged);
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(149, 12);
            this.txtSuffix.MaxLength = 50;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(340, 22);
            this.txtSuffix.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Default settings from :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Suffix :";
            // 
            // frmInsuranceMultipleCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(760, 708);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsuranceMultipleCopy";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Multiple Copy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInsuranceMultipleCopy_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlInsurance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ViewContacts)).EndInit();
            this.pnlSearchMain.ResumeLayout(false);
            this.pnl_Search.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).EndInit();
            this.pnlHeaderData.ResumeLayout(false);
            this.pnlHeaderData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlInsurance;
        private System.Windows.Forms.Panel pnlHeaderData;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnCopy;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_Search;
        internal System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.PictureBox picProgress;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label Label77;
        private System.Windows.Forms.Button btnClearSearch;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        internal System.Windows.Forms.Label lbl_Search;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ViewContacts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlSearchMain;
    }
}