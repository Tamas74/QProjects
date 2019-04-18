namespace gloSecurity
{
    partial class frmViewUsers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewUsers));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ADD = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlUserTypes = new System.Windows.Forms.Panel();
            this.trvMasters = new System.Windows.Forms.TreeView();
            this.pnlViewUsers = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgUsers = new System.Windows.Forms.DataGridView();
            this.pnl_toolstrip = new System.Windows.Forms.Panel();
            this.pnl_Grid = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ts_Commands.SuspendLayout();
            this.pnlUserTypes.SuspendLayout();
            this.pnlViewUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).BeginInit();
            this.pnl_toolstrip.SuspendLayout();
            this.pnl_Grid.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloSecurity.Properties.Resources.ImgButton;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ADD,
            this.tsb_Modify,
            this.tsb_Delete,
            this.tsb_Refresh,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(835, 57);
            this.ts_Commands.TabIndex = 11;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_ADD
            // 
            this.tsb_ADD.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ADD.Image")));
            this.tsb_ADD.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsb_ADD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ADD.Name = "tsb_ADD";
            this.tsb_ADD.Size = new System.Drawing.Size(37, 49);
            this.tsb_ADD.Tag = "Add";
            this.tsb_ADD.Text = " &New";
            this.tsb_ADD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(36, 49);
            this.tsb_Modify.Tag = "Modify";
            this.tsb_Modify.Text = "&Edit";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(48, 49);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(55, 49);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Close
            // 
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(41, 49);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlUserTypes
            // 
            this.pnlUserTypes.Controls.Add(this.trvMasters);
            this.pnlUserTypes.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUserTypes.Location = new System.Drawing.Point(0, 57);
            this.pnlUserTypes.Name = "pnlUserTypes";
            this.pnlUserTypes.Size = new System.Drawing.Size(190, 584);
            this.pnlUserTypes.TabIndex = 12;
            // 
            // trvMasters
            // 
            this.trvMasters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvMasters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMasters.HideSelection = false;
            this.trvMasters.Indent = 22;
            this.trvMasters.Location = new System.Drawing.Point(0, 0);
            this.trvMasters.Name = "trvMasters";
            this.trvMasters.ShowNodeToolTips = true;
            this.trvMasters.Size = new System.Drawing.Size(190, 584);
            this.trvMasters.TabIndex = 13;
            this.trvMasters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMasters_AfterSelect);
            // 
            // pnlViewUsers
            // 
            this.pnlViewUsers.BackgroundImage = global::gloSecurity.Properties.Resources.ImgSearchBackground;
            this.pnlViewUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlViewUsers.Controls.Add(this.lblSearch);
            this.pnlViewUsers.Controls.Add(this.txtSearch);
            this.pnlViewUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlViewUsers.Location = new System.Drawing.Point(192, 57);
            this.pnlViewUsers.Name = "pnlViewUsers";
            this.pnlViewUsers.Size = new System.Drawing.Size(643, 29);
            this.pnlViewUsers.TabIndex = 13;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSearch.Location = new System.Drawing.Point(6, 7);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(69, 13);
            this.lblSearch.TabIndex = 77;
            this.lblSearch.Text = "Login Name :";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(81, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(174, 21);
            this.txtSearch.TabIndex = 20;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgUsers
            // 
            this.dgUsers.AllowUserToAddRows = false;
            this.dgUsers.AllowUserToDeleteRows = false;
            this.dgUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Brown;
            this.dgUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(195)))), ((int)(((byte)(234)))));
            this.dgUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(170)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(68)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUsers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUsers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgUsers.EnableHeadersVisualStyles = false;
            this.dgUsers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(205)))), ((int)(((byte)(211)))));
            this.dgUsers.Location = new System.Drawing.Point(0, 0);
            this.dgUsers.MultiSelect = false;
            this.dgUsers.Name = "dgUsers";
            this.dgUsers.ReadOnly = true;
            this.dgUsers.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(248)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(215)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Maroon;
            this.dgUsers.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUsers.Size = new System.Drawing.Size(643, 555);
            this.dgUsers.TabIndex = 19;
            this.dgUsers.TabStop = false;
            this.dgUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUsers_CellClick);
            // 
            // pnl_toolstrip
            // 
            this.pnl_toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_toolstrip.Name = "pnl_toolstrip";
            this.pnl_toolstrip.Size = new System.Drawing.Size(835, 57);
            this.pnl_toolstrip.TabIndex = 14;
            // 
            // pnl_Grid
            // 
            this.pnl_Grid.Controls.Add(this.dgUsers);
            this.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Grid.Location = new System.Drawing.Point(192, 86);
            this.pnl_Grid.Name = "pnl_Grid";
            this.pnl_Grid.Size = new System.Drawing.Size(643, 555);
            this.pnl_Grid.TabIndex = 15;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.splitter1.Location = new System.Drawing.Point(190, 57);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 584);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmViewUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(835, 641);
            this.Controls.Add(this.pnl_Grid);
            this.Controls.Add(this.pnlViewUsers);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlUserTypes);
            this.Controls.Add(this.pnl_toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmViewUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Users";
            this.Load += new System.EventHandler(this.ViewUsers_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlUserTypes.ResumeLayout(false);
            this.pnlViewUsers.ResumeLayout(false);
            this.pnlViewUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUsers)).EndInit();
            this.pnl_toolstrip.ResumeLayout(false);
            this.pnl_toolstrip.PerformLayout();
            this.pnl_Grid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStripButton tsb_ADD;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlUserTypes;
        private System.Windows.Forms.TreeView trvMasters;
        private System.Windows.Forms.Panel pnlViewUsers;
        private System.Windows.Forms.DataGridView dgUsers;
        private System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnl_toolstrip;
        private gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        private System.Windows.Forms.Panel pnl_Grid;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageList1;
    }
}