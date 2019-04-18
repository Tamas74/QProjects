namespace gloBilling
{
    partial class frmSelectCollectionAgency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectCollectionAgency));
            this.ts_collection = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.c1CollectionAgency = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ts_collection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CollectionAgency)).BeginInit();
            this.SuspendLayout();
            // 
            // ts_collection
            // 
            this.ts_collection.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_collection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_collection.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_collection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_SaveAndCloseMod,
            this.tls_CloseMod});
            this.ts_collection.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_collection.Location = new System.Drawing.Point(0, 0);
            this.ts_collection.Name = "ts_collection";
            this.ts_collection.Size = new System.Drawing.Size(420, 53);
            this.ts_collection.TabIndex = 2;
            this.ts_collection.TabStop = true;
            this.ts_collection.Text = "toolStrip2";
            // 
            // tls_SaveAndCloseMod
            // 
            this.tls_SaveAndCloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveAndCloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveAndCloseMod.Image")));
            this.tls_SaveAndCloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveAndCloseMod.Name = "tls_SaveAndCloseMod";
            this.tls_SaveAndCloseMod.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveAndCloseMod.Text = "Sa&ve&&Cls";
            this.tls_SaveAndCloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveAndCloseMod.ToolTipText = "Save and Close";
            this.tls_SaveAndCloseMod.Click += new System.EventHandler(this.tls_SaveAndCloseMod_Click);
            // 
            // tls_CloseMod
            // 
            this.tls_CloseMod.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_CloseMod.Image = ((System.Drawing.Image)(resources.GetObject("tls_CloseMod.Image")));
            this.tls_CloseMod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_CloseMod.Name = "tls_CloseMod";
            this.tls_CloseMod.Size = new System.Drawing.Size(43, 50);
            this.tls_CloseMod.Text = "&Close";
            this.tls_CloseMod.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_CloseMod.Click += new System.EventHandler(this.tls_CloseMod_Click);
            // 
            // c1CollectionAgency
            // 
            this.c1CollectionAgency.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1CollectionAgency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1CollectionAgency.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1CollectionAgency.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CollectionAgency.ColumnInfo = "5,0,0,0,0,90,Columns:0{AllowSorting:False;Name:\"colSelect\";}\t1{AllowSorting:False" +
    ";Name:\"colId\";Visible:False;}\t2{AllowSorting:False;Name:\"colName\";}\t3{Name:\"colF" +
    "eeType\";}\t4{Name:\"ColFee\";}\t";
            this.c1CollectionAgency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1CollectionAgency.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1CollectionAgency.ExtendLastCol = true;
            this.c1CollectionAgency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1CollectionAgency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1CollectionAgency.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1CollectionAgency.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1CollectionAgency.Location = new System.Drawing.Point(0, 53);
            this.c1CollectionAgency.Name = "c1CollectionAgency";
            this.c1CollectionAgency.Rows.Count = 1;
            this.c1CollectionAgency.Rows.DefaultSize = 18;
            this.c1CollectionAgency.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1CollectionAgency.Size = new System.Drawing.Size(420, 150);
            this.c1CollectionAgency.StyleInfo = resources.GetString("c1CollectionAgency.StyleInfo");
            this.c1CollectionAgency.TabIndex = 23;
            this.c1CollectionAgency.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CollectionAgency_CellChecked);
            // 
            // frmSelectCollectionAgency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(420, 203);
            this.Controls.Add(this.c1CollectionAgency);
            this.Controls.Add(this.ts_collection);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectCollectionAgency";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collection Agency";
            this.Load += new System.EventHandler(this.frmSelectCollectionAgency_Load);
            this.ts_collection.ResumeLayout(false);
            this.ts_collection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CollectionAgency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus ts_collection;
        private System.Windows.Forms.ToolStripButton tls_SaveAndCloseMod;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CollectionAgency;
    }
}