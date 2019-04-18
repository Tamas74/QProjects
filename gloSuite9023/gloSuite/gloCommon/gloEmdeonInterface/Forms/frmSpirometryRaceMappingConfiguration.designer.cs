namespace gloEmdeonInterface.Forms
{
    partial class frmSpirometryRaceMappingConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpirometryRaceMappingConfiguration));
            this.ts_LabMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnPost = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnclose = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlcontrol = new System.Windows.Forms.Panel();
            this.c1RaceConfiguration = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ts_LabMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlcontrol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RaceConfiguration)).BeginInit();
            this.SuspendLayout();
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnDelete,
            this.tlbbtnPost,
            this.tlbbtnclose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(613, 53);
            this.ts_LabMain.TabIndex = 41;
            this.ts_LabMain.Text = "toolStrip1";
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(99, 50);
            this.toolStripButton1.Tag = "AddNew";
            this.toolStripButton1.Text = "&Add New Race";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Add New Race";
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 50);
            this.btnDelete.Tag = "Delete";
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.ToolTipText = "Delete";
            // 
            // tlbbtnPost
            // 
            this.tlbbtnPost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnPost.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnPost.Image")));
            this.tlbbtnPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnPost.Name = "tlbbtnPost";
            this.tlbbtnPost.Size = new System.Drawing.Size(66, 50);
            this.tlbbtnPost.Tag = "Save&Close";
            this.tlbbtnPost.Text = "&Save&&Cls";
            this.tlbbtnPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnPost.ToolTipText = "Save & Close";
            // 
            // tlbbtnclose
            // 
            this.tlbbtnclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnclose.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnclose.Image")));
            this.tlbbtnclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnclose.Name = "tlbbtnclose";
            this.tlbbtnclose.Size = new System.Drawing.Size(43, 50);
            this.tlbbtnclose.Tag = "Close";
            this.tlbbtnclose.Text = "&Close";
            this.tlbbtnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnclose.ToolTipText = "Close";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlcontrol);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(613, 494);
            this.pnlMain.TabIndex = 46;
            // 
            // pnlcontrol
            // 
            this.pnlcontrol.Controls.Add(this.c1RaceConfiguration);
            this.pnlcontrol.Controls.Add(this.label4);
            this.pnlcontrol.Controls.Add(this.label3);
            this.pnlcontrol.Controls.Add(this.label2);
            this.pnlcontrol.Controls.Add(this.label1);
            this.pnlcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcontrol.Location = new System.Drawing.Point(0, 0);
            this.pnlcontrol.Name = "pnlcontrol";
            this.pnlcontrol.Padding = new System.Windows.Forms.Padding(3);
            this.pnlcontrol.Size = new System.Drawing.Size(613, 494);
            this.pnlcontrol.TabIndex = 42;
            // 
            // c1RaceConfiguration
            // 
            this.c1RaceConfiguration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RaceConfiguration.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RaceConfiguration.ColumnInfo = "9,0,0,0,0,90,Columns:";
            this.c1RaceConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RaceConfiguration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RaceConfiguration.Location = new System.Drawing.Point(4, 4);
            this.c1RaceConfiguration.Margin = new System.Windows.Forms.Padding(2);
            this.c1RaceConfiguration.Name = "c1RaceConfiguration";
            this.c1RaceConfiguration.Rows.DefaultSize = 18;
            this.c1RaceConfiguration.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1RaceConfiguration.Size = new System.Drawing.Size(605, 486);
            this.c1RaceConfiguration.StyleInfo = resources.GetString("c1RaceConfiguration.StyleInfo");
            this.c1RaceConfiguration.TabIndex = 48;
            this.c1RaceConfiguration.Tag = "Print";
            this.c1RaceConfiguration.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1RaceConfiguration.Tree.NodeImageExpanded")));
            this.c1RaceConfiguration.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1RaceConfiguration.EnterCell += new System.EventHandler(this.c1RaceConfiguration_EnterCell);
            this.c1RaceConfiguration.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RaceConfiguration_AfterEdit);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(609, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 486);
            this.label4.TabIndex = 47;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 486);
            this.label3.TabIndex = 46;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 490);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(607, 1);
            this.label2.TabIndex = 45;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 1);
            this.label1.TabIndex = 44;
            this.label1.Text = "label1";
            // 
            // frmSpirometryRaceMappingConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(613, 547);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ts_LabMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpirometryRaceMappingConfiguration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Race";
            this.Load += new System.EventHandler(this.frmSpiroTest_RaceConfiguration_Load);
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlcontrol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1RaceConfiguration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnPost;
        private System.Windows.Forms.ToolStripButton tlbbtnclose;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlcontrol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1RaceConfiguration;
        internal System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}