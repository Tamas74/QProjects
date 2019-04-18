namespace gloEmdeonInterface.Forms
{
    partial class frmViewSpirometryRace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewSpirometryRace));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnPost = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnReview = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnclose = new System.Windows.Forms.ToolStripButton();
            this.pnlc1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.c1SpiroRace = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.pnlc1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SpiroRace)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(613, 54);
            this.pnlToolStrip.TabIndex = 44;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.tlbbtnPost,
            this.tlbbtnReview,
            this.tlbbtnclose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(613, 53);
            this.ts_LabMain.TabIndex = 39;
            this.ts_LabMain.Text = "toolStrip1";
            this.ts_LabMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_LabMain_ItemClicked);
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(37, 50);
            this.tlbbtnNew.Tag = "New";
            this.tlbbtnNew.Text = "&New";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.ToolTipText = "New";
            // 
            // tlbbtnPost
            // 
            this.tlbbtnPost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnPost.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnPost.Image")));
            this.tlbbtnPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnPost.Name = "tlbbtnPost";
            this.tlbbtnPost.Size = new System.Drawing.Size(53, 50);
            this.tlbbtnPost.Tag = "Modify";
            this.tlbbtnPost.Text = "&Modify";
            this.tlbbtnPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnPost.ToolTipText = "Modify";
            // 
            // tlbbtnReview
            // 
            this.tlbbtnReview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnReview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnReview.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnReview.Image")));
            this.tlbbtnReview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnReview.Name = "tlbbtnReview";
            this.tlbbtnReview.Size = new System.Drawing.Size(50, 50);
            this.tlbbtnReview.Tag = "Delete";
            this.tlbbtnReview.Text = "&Delete";
            this.tlbbtnReview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnReview.ToolTipText = "Delete";
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
            // pnlc1
            // 
            this.pnlc1.Controls.Add(this.label2);
            this.pnlc1.Controls.Add(this.label6);
            this.pnlc1.Controls.Add(this.label1);
            this.pnlc1.Controls.Add(this.label21);
            this.pnlc1.Controls.Add(this.c1SpiroRace);
            this.pnlc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1.Location = new System.Drawing.Point(0, 54);
            this.pnlc1.Name = "pnlc1";
            this.pnlc1.Padding = new System.Windows.Forms.Padding(3);
            this.pnlc1.Size = new System.Drawing.Size(613, 493);
            this.pnlc1.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(605, 1);
            this.label2.TabIndex = 109;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(605, 1);
            this.label6.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 487);
            this.label1.TabIndex = 107;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 487);
            this.label21.TabIndex = 106;
            // 
            // c1SpiroRace
            // 
            this.c1SpiroRace.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SpiroRace.AllowEditing = false;
            this.c1SpiroRace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SpiroRace.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SpiroRace.ColumnInfo = "9,0,0,0,0,90,Columns:";
            this.c1SpiroRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SpiroRace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SpiroRace.Location = new System.Drawing.Point(3, 3);
            this.c1SpiroRace.Margin = new System.Windows.Forms.Padding(2);
            this.c1SpiroRace.Name = "c1SpiroRace";
            this.c1SpiroRace.Rows.DefaultSize = 18;
            this.c1SpiroRace.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SpiroRace.Size = new System.Drawing.Size(607, 487);
            this.c1SpiroRace.StyleInfo = resources.GetString("c1SpiroRace.StyleInfo");
            this.c1SpiroRace.TabIndex = 44;
            this.c1SpiroRace.Tag = "Print";
            this.c1SpiroRace.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1SpiroRace.Tree.NodeImageExpanded")));
            this.c1SpiroRace.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            // 
            // frmViewSpirometryRace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(613, 547);
            this.Controls.Add(this.pnlc1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewSpirometryRace";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Spirometry Race";
            this.Load += new System.EventHandler(this.frmVWSpiroRace_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlc1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SpiroRace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        private System.Windows.Forms.ToolStripButton tlbbtnPost;
        private System.Windows.Forms.ToolStripButton tlbbtnReview;
        private System.Windows.Forms.ToolStripButton tlbbtnclose;
        private System.Windows.Forms.Panel pnlc1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SpiroRace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
    }
}