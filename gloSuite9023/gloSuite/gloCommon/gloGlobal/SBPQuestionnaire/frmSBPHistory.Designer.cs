namespace gloGlobal.SBPQuestionnaire
{
    partial class frmSBPHistory
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSBPHistory));
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.c1SBPHistory = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_pnlBottom = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.lbl_pnlRight = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.tlsSBPHistory = new gloGlobal.gloToolStripIgnoreFocus();
            this.btn_tls_Close = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SBPHistory)).BeginInit();
            this.Panel2.SuspendLayout();
            this.tlsSBPHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.c1SBPHistory);
            this.pnl_Base.Controls.Add(this.lbl_pnlBottom);
            this.pnl_Base.Controls.Add(this.lbl_pnlLeft);
            this.pnl_Base.Controls.Add(this.lbl_pnlRight);
            this.pnl_Base.Controls.Add(this.lbl_pnlTop);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 53);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(734, 450);
            this.pnl_Base.TabIndex = 9;
            // 
            // c1SBPHistory
            // 
            this.c1SBPHistory.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SBPHistory.AllowEditing = false;
            this.c1SBPHistory.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1SBPHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SBPHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SBPHistory.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1SBPHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SBPHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1SBPHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SBPHistory.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SBPHistory.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1SBPHistory.Location = new System.Drawing.Point(4, 4);
            this.c1SBPHistory.Name = "c1SBPHistory";
            this.c1SBPHistory.Rows.DefaultSize = 19;
            this.c1SBPHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SBPHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1SBPHistory.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None;
            this.c1SBPHistory.Size = new System.Drawing.Size(726, 442);
            this.c1SBPHistory.StyleInfo = resources.GetString("c1SBPHistory.StyleInfo");
            this.c1SBPHistory.TabIndex = 3;
            this.c1SBPHistory.DoubleClick += new System.EventHandler(this.c1SBPHistory_DoubleClick);
            this.c1SBPHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1SBPHistory_MouseMove);
            // 
            // lbl_pnlBottom
            // 
            this.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlBottom.Location = new System.Drawing.Point(4, 446);
            this.lbl_pnlBottom.Name = "lbl_pnlBottom";
            this.lbl_pnlBottom.Size = new System.Drawing.Size(726, 1);
            this.lbl_pnlBottom.TabIndex = 4;
            this.lbl_pnlBottom.Text = "label2";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 443);
            this.lbl_pnlLeft.TabIndex = 3;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // lbl_pnlRight
            // 
            this.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlRight.Location = new System.Drawing.Point(730, 4);
            this.lbl_pnlRight.Name = "lbl_pnlRight";
            this.lbl_pnlRight.Size = new System.Drawing.Size(1, 443);
            this.lbl_pnlRight.TabIndex = 2;
            this.lbl_pnlRight.Text = "label3";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(728, 1);
            this.lbl_pnlTop.TabIndex = 0;
            this.lbl_pnlTop.Text = "label1";
            // 
            // Panel2
            // 
            this.Panel2.AutoSize = true;
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.tlsSBPHistory);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(734, 53);
            this.Panel2.TabIndex = 10;
            // 
            // tlsSBPHistory
            // 
            this.tlsSBPHistory.BackColor = System.Drawing.Color.Transparent;
            this.tlsSBPHistory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsSBPHistory.BackgroundImage")));
            this.tlsSBPHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsSBPHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsSBPHistory.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsSBPHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_tls_Close});
            this.tlsSBPHistory.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsSBPHistory.Location = new System.Drawing.Point(0, 0);
            this.tlsSBPHistory.Name = "tlsSBPHistory";
            this.tlsSBPHistory.Size = new System.Drawing.Size(734, 53);
            this.tlsSBPHistory.TabIndex = 1;
            this.tlsSBPHistory.Text = "toolStrip1";
            // 
            // btn_tls_Close
            // 
            this.btn_tls_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Close.Image")));
            this.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Close.Name = "btn_tls_Close";
            this.btn_tls_Close.Size = new System.Drawing.Size(59, 50);
            this.btn_tls_Close.Tag = "Close";
            this.btn_tls_Close.Text = "  &Close  ";
            this.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Close.ToolTipText = "Close";
            this.btn_tls_Close.Click += new System.EventHandler(this.btn_tls_Close_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSBPHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(734, 503);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSBPHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Social, Psychological, and Behavioral Audit Data";
            this.Load += new System.EventHandler(this.frmSBPHistory_Load);
            this.pnl_Base.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SBPHistory)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.tlsSBPHistory.ResumeLayout(false);
            this.tlsSBPHistory.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_pnlBottom;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label lbl_pnlRight;
        private System.Windows.Forms.Label lbl_pnlTop;
        internal System.Windows.Forms.Panel Panel2;
        private gloToolStripIgnoreFocus tlsSBPHistory;
        internal System.Windows.Forms.ToolStripButton btn_tls_Close;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1SBPHistory;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}