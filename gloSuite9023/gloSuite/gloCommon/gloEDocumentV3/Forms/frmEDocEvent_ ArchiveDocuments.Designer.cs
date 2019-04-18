namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent__ArchiveDocuments
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
                if (oList != null)
                {
                    oList.Dispose();
                    oList = null;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent__ArchiveDocuments));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Retrive = new System.Windows.Forms.ToolStripButton();
            this.tsb_SelectAllDocuments = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.C1ArchiveList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlTop.SuspendLayout();
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1ArchiveList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.tls_MaintainDoc);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(532, 54);
            this.pnlTop.TabIndex = 0;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Retrive,
            this.tsb_SelectAllDocuments,
            this.tsb_Close});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(532, 53);
            this.tls_MaintainDoc.TabIndex = 16;
            this.tls_MaintainDoc.TabStop = true;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tsb_Retrive
            // 
            this.tsb_Retrive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Retrive.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Retrive.Image")));
            this.tsb_Retrive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Retrive.Name = "tsb_Retrive";
            this.tsb_Retrive.Size = new System.Drawing.Size(59, 50);
            this.tsb_Retrive.Tag = "Restore";
            this.tsb_Retrive.Text = "&Restore";
            this.tsb_Retrive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Retrive.ToolTipText = "Restore Documents";
            this.tsb_Retrive.Click += new System.EventHandler(this.tsb_Retrive_Click);
            // 
            // tsb_SelectAllDocuments
            // 
            this.tsb_SelectAllDocuments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SelectAllDocuments.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SelectAllDocuments.Image")));
            this.tsb_SelectAllDocuments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectAllDocuments.Name = "tsb_SelectAllDocuments";
            this.tsb_SelectAllDocuments.Size = new System.Drawing.Size(67, 50);
            this.tsb_SelectAllDocuments.Tag = "SelectAll";
            this.tsb_SelectAllDocuments.Text = "Select &All";
            this.tsb_SelectAllDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectAllDocuments.ToolTipText = "Select All Documents";
            this.tsb_SelectAllDocuments.Click += new System.EventHandler(this.tsb_SelectAllDocuments_Click);
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
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.ToolTipText = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label18);
            this.pnlMain.Controls.Add(this.C1ArchiveList);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(532, 368);
            this.pnlMain.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 364);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(524, 1);
            this.label3.TabIndex = 19;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(524, 1);
            this.label2.TabIndex = 18;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(528, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 362);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(3, 3);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 362);
            this.label18.TabIndex = 16;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // C1ArchiveList
            // 
            this.C1ArchiveList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1ArchiveList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1ArchiveList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1ArchiveList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1ArchiveList.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:\"TextAlign:CenterCenter;ImageAlign:CenterCenter" +
                ";\";}\t";
            this.C1ArchiveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1ArchiveList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1ArchiveList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1ArchiveList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1ArchiveList.Location = new System.Drawing.Point(3, 3);
            this.C1ArchiveList.Name = "C1ArchiveList";
            this.C1ArchiveList.Rows.Count = 1;
            this.C1ArchiveList.Rows.DefaultSize = 19;
            this.C1ArchiveList.Rows.Fixed = 0;
            this.C1ArchiveList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1ArchiveList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1ArchiveList.ShowCellLabels = true;
            this.C1ArchiveList.ShowSort = false;
            this.C1ArchiveList.Size = new System.Drawing.Size(526, 362);
            this.C1ArchiveList.StyleInfo = resources.GetString("C1ArchiveList.StyleInfo");
            this.C1ArchiveList.TabIndex = 1;
            this.C1ArchiveList.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("C1ArchiveList.Tree.NodeImageCollapsed")));
            this.C1ArchiveList.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("C1ArchiveList.Tree.NodeImageExpanded")));
            this.C1ArchiveList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1ArchiveList_AfterEdit);
            this.C1ArchiveList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1ArchiveList_CellChanged);
            // 
            // frmEDocEvent__ArchiveDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(532, 422);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent__ArchiveDocuments";
            this.ShowInTaskbar = false;
            this.Text = "Restore Documents";
            this.Load += new System.EventHandler(this.frmEDocEvent__ArchiveDocuments_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1ArchiveList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
        private System.Windows.Forms.ToolStripButton tsb_SelectAllDocuments;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        internal C1.Win.C1FlexGrid.C1FlexGrid C1ArchiveList;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripButton tsb_Retrive;
    }
}