namespace gloOffice
{
    partial class frmSetupTemplateGalleryAssociation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTemplateGalleryAssociation));
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Default = new System.Windows.Forms.ToolStripButton();
            this.tsb_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_Clear = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbAssociates = new System.Windows.Forms.ComboBox();
            this.lblAssociationType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlTreeView = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trvCategoryTemplates = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnl_ToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.ts_Commands);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(540, 54);
            this.pnl_ToolStrip.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Default,
            this.tsb_SelectAll,
            this.tsb_Clear,
            this.tsb_Delete,
            this.tsb_Save,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Padding = new System.Windows.Forms.Padding(0);
            this.ts_Commands.Size = new System.Drawing.Size(540, 53);
            this.ts_Commands.TabIndex = 9;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Default
            // 
            this.tsb_Default.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Default.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Default.Image")));
            this.tsb_Default.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Default.Name = "tsb_Default";
            this.tsb_Default.Size = new System.Drawing.Size(56, 50);
            this.tsb_Default.Tag = "Save";
            this.tsb_Default.Text = "De&fault";
            this.tsb_Default.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Default.ToolTipText = "Default";
            this.tsb_Default.Visible = false;
            this.tsb_Default.Click += new System.EventHandler(this.tsb_Default_Click);
            // 
            // tsb_SelectAll
            // 
            this.tsb_SelectAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SelectAll.Image")));
            this.tsb_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectAll.Name = "tsb_SelectAll";
            this.tsb_SelectAll.Size = new System.Drawing.Size(67, 50);
            this.tsb_SelectAll.Tag = "Select All";
            this.tsb_SelectAll.Text = "&Select All";
            this.tsb_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectAll.Click += new System.EventHandler(this.tsb_SelectAll_Click);
            // 
            // tsb_Clear
            // 
            this.tsb_Clear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Clear.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Clear.Image")));
            this.tsb_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Clear.Name = "tsb_Clear";
            this.tsb_Clear.Size = new System.Drawing.Size(88, 50);
            this.tsb_Clear.Tag = "Clear";
            this.tsb_Clear.Text = "De-Select &All";
            this.tsb_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Clear.Click += new System.EventHandler(this.tsb_Clear_Click);
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
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "Sa&ve&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.panel1.Size = new System.Drawing.Size(540, 29);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.cmbAssociates);
            this.panel3.Controls.Add(this.lblAssociationType);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(534, 24);
            this.panel3.TabIndex = 0;
            // 
            // cmbAssociates
            // 
            this.cmbAssociates.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbAssociates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssociates.ForeColor = System.Drawing.Color.Black;
            this.cmbAssociates.FormattingEnabled = true;
            this.cmbAssociates.Location = new System.Drawing.Point(64, 1);
            this.cmbAssociates.Name = "cmbAssociates";
            this.cmbAssociates.Size = new System.Drawing.Size(220, 22);
            this.cmbAssociates.TabIndex = 1;
            this.cmbAssociates.SelectedIndexChanged += new System.EventHandler(this.cmbAssociates_SelectedIndexChanged);
            // 
            // lblAssociationType
            // 
            this.lblAssociationType.BackColor = System.Drawing.Color.Transparent;
            this.lblAssociationType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAssociationType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssociationType.Location = new System.Drawing.Point(1, 1);
            this.lblAssociationType.Name = "lblAssociationType";
            this.lblAssociationType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAssociationType.Size = new System.Drawing.Size(63, 22);
            this.lblAssociationType.TabIndex = 6;
            this.lblAssociationType.Text = "Type : ";
            this.lblAssociationType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(532, 1);
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
            this.label3.Location = new System.Drawing.Point(533, 1);
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
            this.label4.Size = new System.Drawing.Size(534, 1);
            this.label4.TabIndex = 5;
            this.label4.Text = "label1";
            // 
            // pnlTreeView
            // 
            this.pnlTreeView.Controls.Add(this.label8);
            this.pnlTreeView.Controls.Add(this.label7);
            this.pnlTreeView.Controls.Add(this.label6);
            this.pnlTreeView.Controls.Add(this.label5);
            this.pnlTreeView.Controls.Add(this.trvCategoryTemplates);
            this.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTreeView.Location = new System.Drawing.Point(0, 83);
            this.pnlTreeView.Name = "pnlTreeView";
            this.pnlTreeView.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTreeView.Size = new System.Drawing.Size(540, 469);
            this.pnlTreeView.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(532, 1);
            this.label8.TabIndex = 11;
            this.label8.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(4, 465);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(532, 1);
            this.label7.TabIndex = 10;
            this.label7.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(536, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 466);
            this.label6.TabIndex = 9;
            this.label6.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 466);
            this.label5.TabIndex = 8;
            this.label5.Text = "label4";
            // 
            // trvCategoryTemplates
            // 
            this.trvCategoryTemplates.BackColor = System.Drawing.Color.White;
            this.trvCategoryTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategoryTemplates.CheckBoxes = true;
            this.trvCategoryTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategoryTemplates.ForeColor = System.Drawing.Color.Black;
            this.trvCategoryTemplates.FullRowSelect = true;
            this.trvCategoryTemplates.HideSelection = false;
            this.trvCategoryTemplates.ImageIndex = 0;
            this.trvCategoryTemplates.ImageList = this.imageList1;
            this.trvCategoryTemplates.Indent = 20;
            this.trvCategoryTemplates.ItemHeight = 20;
            this.trvCategoryTemplates.Location = new System.Drawing.Point(3, 0);
            this.trvCategoryTemplates.Name = "trvCategoryTemplates";
            this.trvCategoryTemplates.SelectedImageIndex = 0;
            this.trvCategoryTemplates.ShowNodeToolTips = true;
            this.trvCategoryTemplates.Size = new System.Drawing.Size(534, 466);
            this.trvCategoryTemplates.TabIndex = 0;
            this.trvCategoryTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCategoryTemplates_AfterCheck);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Bullet06.ico");
            // 
            // frmSetupTemplateGalleryAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(540, 552);
            this.Controls.Add(this.pnlTreeView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupTemplateGalleryAssociation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Template Association";
            this.Load += new System.EventHandler(this.frmSetupTemplateGalleryAssociation_Load);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlTreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Clear;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbAssociates;
        private System.Windows.Forms.Label lblAssociationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlTreeView;
        private System.Windows.Forms.TreeView trvCategoryTemplates;
        private System.Windows.Forms.ImageList imageList1;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_SelectAll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ToolStripButton tsb_Default;
    }
}