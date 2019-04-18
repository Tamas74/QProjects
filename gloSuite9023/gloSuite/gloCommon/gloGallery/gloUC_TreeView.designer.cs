namespace gloGallery
{
    partial class gloUC_TreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloUC_TreeView));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTree = new System.Windows.Forms.Panel();
            this.trvMain = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Label33 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnlRadioButtons = new System.Windows.Forms.Panel();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnUnassociated = new System.Windows.Forms.RadioButton();
            this.rbtnAssociated = new System.Windows.Forms.RadioButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlTree.SuspendLayout();
            this.pnlRadioButtons.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlTree);
            this.pnlMain.Controls.Add(this.pnlRadioButtons);
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(241, 332);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlTree
            // 
            this.pnlTree.Controls.Add(this.trvMain);
            this.pnlTree.Controls.Add(this.Label33);
            this.pnlTree.Controls.Add(this.Label31);
            this.pnlTree.Controls.Add(this.Label5);
            this.pnlTree.Controls.Add(this.Label6);
            this.pnlTree.Controls.Add(this.Label7);
            this.pnlTree.Controls.Add(this.Label8);
            this.pnlTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTree.Location = new System.Drawing.Point(0, 53);
            this.pnlTree.Name = "pnlTree";
            this.pnlTree.Size = new System.Drawing.Size(241, 279);
            this.pnlTree.TabIndex = 1;
            // 
            // trvMain
            // 
            this.trvMain.BackColor = System.Drawing.Color.White;
            this.trvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvMain.ForeColor = System.Drawing.Color.Black;
            this.trvMain.HideSelection = false;
            this.trvMain.ImageIndex = 0;
            this.trvMain.ImageList = this.ImageList1;
            this.trvMain.Indent = 20;
            this.trvMain.ItemHeight = 20;
            this.trvMain.Location = new System.Drawing.Point(5, 5);
            this.trvMain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trvMain.Name = "trvMain";
            this.trvMain.SelectedImageIndex = 0;
            this.trvMain.ShowLines = false;
            this.trvMain.ShowNodeToolTips = true;
            this.trvMain.Size = new System.Drawing.Size(235, 273);
            this.trvMain.TabIndex = 1;
            this.trvMain.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvMain_AfterCheck);
            this.trvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMain_AfterSelect);
            this.trvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvMain_NodeMouseClick);
            this.trvMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvMain_NodeMouseDoubleClick);
            this.trvMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvMain_KeyPress);
            this.trvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvMain_MouseDown);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Bullet06.ico");
            // 
            // Label33
            // 
            this.Label33.BackColor = System.Drawing.Color.White;
            this.Label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label33.Location = new System.Drawing.Point(1, 5);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(4, 273);
            this.Label33.TabIndex = 39;
            // 
            // Label31
            // 
            this.Label31.BackColor = System.Drawing.Color.White;
            this.Label31.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label31.Location = new System.Drawing.Point(1, 1);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(239, 4);
            this.Label31.TabIndex = 38;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(1, 278);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(239, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(0, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 278);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(240, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 278);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(0, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(241, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // pnlRadioButtons
            // 
            this.pnlRadioButtons.Controls.Add(this.rbtnAll);
            this.pnlRadioButtons.Controls.Add(this.rbtnUnassociated);
            this.pnlRadioButtons.Controls.Add(this.rbtnAssociated);
            this.pnlRadioButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRadioButtons.Location = new System.Drawing.Point(0, 26);
            this.pnlRadioButtons.Name = "pnlRadioButtons";
            this.pnlRadioButtons.Size = new System.Drawing.Size(241, 27);
            this.pnlRadioButtons.TabIndex = 2;
            this.pnlRadioButtons.Visible = false;
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Location = new System.Drawing.Point(187, 5);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(36, 17);
            this.rbtnAll.TabIndex = 2;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnUnassociated
            // 
            this.rbtnUnassociated.AutoSize = true;
            this.rbtnUnassociated.Location = new System.Drawing.Point(89, 5);
            this.rbtnUnassociated.Name = "rbtnUnassociated";
            this.rbtnUnassociated.Size = new System.Drawing.Size(90, 17);
            this.rbtnUnassociated.TabIndex = 1;
            this.rbtnUnassociated.TabStop = true;
            this.rbtnUnassociated.Text = "Unassociated";
            this.rbtnUnassociated.UseVisualStyleBackColor = true;
            // 
            // rbtnAssociated
            // 
            this.rbtnAssociated.AutoSize = true;
            this.rbtnAssociated.Location = new System.Drawing.Point(5, 6);
            this.rbtnAssociated.Name = "rbtnAssociated";
            this.rbtnAssociated.Size = new System.Drawing.Size(77, 17);
            this.rbtnAssociated.TabIndex = 0;
            this.rbtnAssociated.TabStop = true;
            this.rbtnAssociated.Text = "Associated";
            this.rbtnAssociated.UseVisualStyleBackColor = true;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.txtsearch);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.PicBx_Search);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlSearch.Size = new System.Drawing.Size(241, 26);
            this.pnlSearch.TabIndex = 0;
            // 
            // txtsearch
            // 
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.ForeColor = System.Drawing.Color.Black;
            this.txtsearch.Location = new System.Drawing.Point(29, 4);
            this.txtsearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(190, 15);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            this.txtsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtsearch_KeyPress);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(29, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(190, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(29, 17);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(190, 5);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(219, 1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 21);
            this.btnClear.TabIndex = 41;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(1, 1);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 21);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(1, 22);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(239, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(1, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(239, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(240, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // gloUC_TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "gloUC_TreeView";
            this.Size = new System.Drawing.Size(241, 332);
            this.pnlMain.ResumeLayout(false);
            this.pnlTree.ResumeLayout(false);
            this.pnlRadioButtons.ResumeLayout(false);
            this.pnlRadioButtons.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Panel pnlTree;
        internal System.Windows.Forms.TreeView trvMain;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label31;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Panel pnlRadioButtons;
        internal System.Windows.Forms.RadioButton rbtnAll;
        internal System.Windows.Forms.RadioButton rbtnUnassociated;
        internal System.Windows.Forms.RadioButton rbtnAssociated;
        internal System.Windows.Forms.Panel pnlSearch;
        public System.Windows.Forms.TextBox txtsearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        internal System.Windows.Forms.ImageList ImageList1;
        internal System.Windows.Forms.ToolTip ToolTip1;
    }
}
