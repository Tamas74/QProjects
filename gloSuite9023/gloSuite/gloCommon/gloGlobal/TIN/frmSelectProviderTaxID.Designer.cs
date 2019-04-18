namespace gloGlobal
{
    partial class frmSelectProviderTaxID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectProviderTaxID));
            this.ts_collection = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveAndCloseMod = new System.Windows.Forms.ToolStripButton();
            this.tls_CloseMod = new System.Windows.Forms.ToolStripButton();
            this.c1SelectProviderTaxIDs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.ts_collection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SelectProviderTaxIDs)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_collection
            // 
            this.ts_collection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_collection.BackgroundImage")));
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
            // c1SelectProviderTaxIDs
            // 
            this.c1SelectProviderTaxIDs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1SelectProviderTaxIDs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SelectProviderTaxIDs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1SelectProviderTaxIDs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SelectProviderTaxIDs.ColumnInfo = resources.GetString("c1SelectProviderTaxIDs.ColumnInfo");
            this.c1SelectProviderTaxIDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SelectProviderTaxIDs.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1SelectProviderTaxIDs.ExtendLastCol = true;
            this.c1SelectProviderTaxIDs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SelectProviderTaxIDs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SelectProviderTaxIDs.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1SelectProviderTaxIDs.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SelectProviderTaxIDs.Location = new System.Drawing.Point(4, 3);
            this.c1SelectProviderTaxIDs.Name = "c1SelectProviderTaxIDs";
            this.c1SelectProviderTaxIDs.Rows.Count = 1;
            this.c1SelectProviderTaxIDs.Rows.DefaultSize = 18;
            this.c1SelectProviderTaxIDs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1SelectProviderTaxIDs.Size = new System.Drawing.Size(413, 144);
            this.c1SelectProviderTaxIDs.StyleInfo = resources.GetString("c1SelectProviderTaxIDs.StyleInfo");
            this.c1SelectProviderTaxIDs.TabIndex = 23;
            this.c1SelectProviderTaxIDs.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SelectProviderTaxIDs_CellChecked);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.lbl_pnlBottom);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.c1SelectProviderTaxIDs);
            this.pnlMain.Controls.Add(this.lbl_pnlLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(420, 150);
            this.pnlMain.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(412, 1);
            this.label2.TabIndex = 16;
            this.label2.Text = "label2";
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 146);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(412, 1);
            this.lbl_pnlBottom.TabIndex = 15;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(416, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 144);
            this.label1.TabIndex = 14;
            this.label1.Text = "label4";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 144);
            this.lbl_pnlLeft.TabIndex = 13;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.ts_collection);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(420, 53);
            this.pnlToolstrip.TabIndex = 25;
            // 
            // frmSelectProviderTaxID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(420, 203);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectProviderTaxID";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Provider TaxID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectProviderTaxID_FormClosing);
            this.Load += new System.EventHandler(this.frmSelectProviderTaxID_Load);
            this.ts_collection.ResumeLayout(false);
            this.ts_collection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SelectProviderTaxIDs)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus ts_collection;
        private System.Windows.Forms.ToolStripButton tls_SaveAndCloseMod;
        private System.Windows.Forms.ToolStripButton tls_CloseMod;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SelectProviderTaxIDs;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Panel pnlToolstrip;
    }
}