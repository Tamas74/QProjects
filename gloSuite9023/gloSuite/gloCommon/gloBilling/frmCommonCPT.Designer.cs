namespace gloBilling
{
    partial class frmCommonCPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonCPT));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tls_CPT = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.c1FlexCommon = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCloseSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.Label77 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlToolstrip.SuspendLayout();
            this.tls_CPT.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexCommon)).BeginInit();
            this.pnlCloseSearch.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tls_CPT);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(869, 53);
            this.pnlToolstrip.TabIndex = 3;
            this.pnlToolstrip.TabStop = true;
            // 
            // tls_CPT
            // 
            this.tls_CPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_CPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_CPT.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_CPT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Close});
            this.tls_CPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_CPT.Location = new System.Drawing.Point(0, 0);
            this.tls_CPT.Name = "tls_CPT";
            this.tls_CPT.Size = new System.Drawing.Size(869, 53);
            this.tls_CPT.TabIndex = 0;
            this.tls_CPT.TabStop = true;
            this.tls_CPT.Text = "toolStrip1";
            this.tls_CPT.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_CPT_ItemClicked);
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "Sa&ve&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            // 
            // tlb_Close
            // 
            this.tlb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Close.Image")));
            this.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Close.Name = "tlb_Close";
            this.tlb_Close.Size = new System.Drawing.Size(43, 50);
            this.tlb_Close.Tag = "Cancel";
            this.tlb_Close.Text = "&Close";
            this.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Close.ToolTipText = "Close";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.c1FlexCommon);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 82);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(869, 595);
            this.pnlMain.TabIndex = 4;
            // 
            // c1FlexCommon
            // 
            this.c1FlexCommon.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexCommon.AllowEditing = false;
            this.c1FlexCommon.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1FlexCommon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FlexCommon.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexCommon.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1FlexCommon.Cursor = System.Windows.Forms.Cursors.Default;
            this.c1FlexCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexCommon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexCommon.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.c1FlexCommon.Location = new System.Drawing.Point(4, 1);
            this.c1FlexCommon.Name = "c1FlexCommon";
            this.c1FlexCommon.Rows.Count = 1;
            this.c1FlexCommon.Rows.DefaultSize = 21;
            this.c1FlexCommon.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexCommon.ShowCellLabels = true;
            this.c1FlexCommon.Size = new System.Drawing.Size(861, 590);
            this.c1FlexCommon.StyleInfo = resources.GetString("c1FlexCommon.StyleInfo");
            this.c1FlexCommon.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(865, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 590);
            this.label4.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 590);
            this.label3.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 591);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(863, 1);
            this.label2.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(863, 1);
            this.label1.TabIndex = 25;
            // 
            // pnlCloseSearch
            // 
            this.pnlCloseSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlCloseSearch.Controls.Add(this.txtSearch);
            this.pnlCloseSearch.Controls.Add(this.Label77);
            this.pnlCloseSearch.Controls.Add(this.label7);
            this.pnlCloseSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlCloseSearch.Controls.Add(this.btnClear);
            this.pnlCloseSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlCloseSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlCloseSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCloseSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCloseSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlCloseSearch.Location = new System.Drawing.Point(61, 1);
            this.pnlCloseSearch.Name = "pnlCloseSearch";
            this.pnlCloseSearch.Size = new System.Drawing.Size(241, 21);
            this.pnlCloseSearch.TabIndex = 45;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(5, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(214, 15);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(5, 16);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(214, 5);
            this.Label77.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(214, 3);
            this.label7.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(1, 0);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(4, 21);
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
            this.btnClear.Location = new System.Drawing.Point(219, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 21);
            this.btnClear.TabIndex = 41;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(240, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 53);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(869, 29);
            this.panel4.TabIndex = 46;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.pnlCloseSearch);
            this.panel5.Controls.Add(this.lblSearch);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(863, 23);
            this.panel5.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(1, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(60, 17);
            this.lblSearch.TabIndex = 9;
            this.lblSearch.Text = "  Search :";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(1, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(861, 1);
            this.label12.TabIndex = 8;
            this.label12.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 22);
            this.label13.TabIndex = 7;
            this.label13.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(862, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 22);
            this.label14.TabIndex = 6;
            this.label14.Text = "label14";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(863, 1);
            this.label15.TabIndex = 5;
            this.label15.Text = "label1";
            // 
            // frmCommonCPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(869, 677);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCommonCPT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Common CPT";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCommonCPT_FormClosed);
            this.Load += new System.EventHandler(this.frmCommonCPT_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tls_CPT.ResumeLayout(false);
            this.tls_CPT.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexCommon)).EndInit();
            this.pnlCloseSearch.ResumeLayout(false);
            this.pnlCloseSearch.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_CPT;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Close;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel pnlCloseSearch;
        private System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label Label77;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblSearch;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexCommon;

    }
}