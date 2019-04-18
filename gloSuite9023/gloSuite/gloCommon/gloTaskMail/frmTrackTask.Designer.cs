namespace gloTaskMail
{
    partial class frmTrackTask
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
        /// 
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrackTask));
            this.pnlGridAssign = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1TaskRequest = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Reassign = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnToDelete = new System.Windows.Forms.Button();
            this.btnToBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbToUsers = new System.Windows.Forms.ComboBox();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlGridAssign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TaskRequest)).BeginInit();
            this.pnl_ToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGridAssign
            // 
            this.pnlGridAssign.Controls.Add(this.label4);
            this.pnlGridAssign.Controls.Add(this.label3);
            this.pnlGridAssign.Controls.Add(this.label2);
            this.pnlGridAssign.Controls.Add(this.label1);
            this.pnlGridAssign.Controls.Add(this.c1TaskRequest);
            this.pnlGridAssign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridAssign.Location = new System.Drawing.Point(0, 54);
            this.pnlGridAssign.Name = "pnlGridAssign";
            this.pnlGridAssign.Padding = new System.Windows.Forms.Padding(3);
            this.pnlGridAssign.Size = new System.Drawing.Size(754, 504);
            this.pnlGridAssign.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(750, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 496);
            this.label4.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 496);
            this.label3.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 500);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(748, 1);
            this.label2.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(748, 1);
            this.label1.TabIndex = 27;
            // 
            // c1TaskRequest
            // 
            this.c1TaskRequest.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1TaskRequest.AllowEditing = false;
            this.c1TaskRequest.AutoGenerateColumns = false;
            this.c1TaskRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1TaskRequest.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1TaskRequest.ColumnInfo = resources.GetString("c1TaskRequest.ColumnInfo");
            this.c1TaskRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TaskRequest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1TaskRequest.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.c1TaskRequest.Location = new System.Drawing.Point(3, 3);
            this.c1TaskRequest.Name = "c1TaskRequest";
            this.c1TaskRequest.Rows.Count = 1;
            this.c1TaskRequest.Rows.DefaultSize = 21;
            this.c1TaskRequest.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1TaskRequest.Size = new System.Drawing.Size(748, 498);
            this.c1TaskRequest.StyleInfo = resources.GetString("c1TaskRequest.StyleInfo");
            this.c1TaskRequest.TabIndex = 1;
            this.c1TaskRequest.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1TaskRequest_AfterSort);
            this.c1TaskRequest.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.c1TaskRequest_OwnerDrawCell);
            this.c1TaskRequest.Click += new System.EventHandler(this.c1TaskRequest_Click);
            this.c1TaskRequest.DoubleClick += new System.EventHandler(this.c1TaskRequest_DoubleClick);
            this.c1TaskRequest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1TaskRequest_MouseMove);
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.ts_Commands);
            this.pnl_ToolStrip.Controls.Add(this.panel1);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(754, 54);
            this.pnl_ToolStrip.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Reassign,
            this.tsb_Modify,
            this.tsb_Delete,
            this.tsb_Refresh,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(390, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Reassign
            // 
            this.tsb_Reassign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Reassign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Reassign.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reassign.Image")));
            this.tsb_Reassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Reassign.Name = "tsb_Reassign";
            this.tsb_Reassign.Size = new System.Drawing.Size(69, 50);
            this.tsb_Reassign.Tag = "Add";
            this.tsb_Reassign.Text = " &Reassign";
            this.tsb_Reassign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Reassign.ToolTipText = " Reassign Task";
            this.tsb_Reassign.Visible = false;
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(36, 50);
            this.tsb_Modify.Tag = "Modify";
            this.tsb_Modify.Text = "&Edit";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnToDelete);
            this.panel1.Controls.Add(this.btnToBrowse);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbToUsers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(390, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 54);
            this.panel1.TabIndex = 1;
            // 
            // btnToDelete
            // 
            this.btnToDelete.AutoEllipsis = true;
            this.btnToDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnToDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToDelete.BackgroundImage")));
            this.btnToDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnToDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnToDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnToDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnToDelete.Image")));
            this.btnToDelete.Location = new System.Drawing.Point(301, 17);
            this.btnToDelete.Name = "btnToDelete";
            this.btnToDelete.Size = new System.Drawing.Size(22, 22);
            this.btnToDelete.TabIndex = 4;
            this.btnToDelete.UseVisualStyleBackColor = false;
            this.btnToDelete.Click += new System.EventHandler(this.btnToDelete_Click);
            // 
            // btnToBrowse
            // 
            this.btnToBrowse.AutoEllipsis = true;
            this.btnToBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnToBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToBrowse.BackgroundImage")));
            this.btnToBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnToBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnToBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnToBrowse.Image")));
            this.btnToBrowse.Location = new System.Drawing.Point(275, 17);
            this.btnToBrowse.Name = "btnToBrowse";
            this.btnToBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnToBrowse.TabIndex = 3;
            this.btnToBrowse.UseVisualStyleBackColor = false;
            this.btnToBrowse.Click += new System.EventHandler(this.btnToBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(17, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "To :";
            // 
            // cmbToUsers
            // 
            this.cmbToUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToUsers.FormattingEnabled = true;
            this.cmbToUsers.Location = new System.Drawing.Point(49, 17);
            this.cmbToUsers.Name = "cmbToUsers";
            this.cmbToUsers.Size = new System.Drawing.Size(222, 22);
            this.cmbToUsers.TabIndex = 0;
            this.cmbToUsers.SelectedIndexChanged += new System.EventHandler(this.cmbToUsers_SelectedIndexChanged);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmTrackTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(754, 558);
            this.Controls.Add(this.pnlGridAssign);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrackTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Track Task";
            this.Load += new System.EventHandler(this.frmTrackTask_Load);
            this.Shown += new System.EventHandler(this.frmTrackTask_Shown);
            this.pnlGridAssign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TaskRequest)).EndInit();
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGridAssign;
        private System.Windows.Forms.Panel pnl_ToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Reassign;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private C1.Win.C1FlexGrid.C1FlexGrid c1TaskRequest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbToUsers;
        private System.Windows.Forms.Button btnToDelete;
        private System.Windows.Forms.Button btnToBrowse;

    }
}