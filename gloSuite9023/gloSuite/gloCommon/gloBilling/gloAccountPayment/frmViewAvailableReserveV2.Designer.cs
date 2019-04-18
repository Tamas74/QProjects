namespace gloAccountsV2
{
    partial class frmViewAvailableReserveV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewAvailableReserveV2));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.c1FlexGridAvailResrv = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label49 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.pnlTotalRsrv = new System.Windows.Forms.Panel();
            this.c1FlexGridTotalAvailResrv = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label57 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridAvailResrv)).BeginInit();
            this.panel22.SuspendLayout();
            this.pnlTotalRsrv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridTotalAvailResrv)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSize = true;
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1034, 53);
            this.pnlToolStrip.TabIndex = 6;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1034, 53);
            this.tls_Top.TabIndex = 0;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnClose.Image")));
            this.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tls_btnClose.Tag = "Close";
            this.tls_btnClose.Text = "&Close";
            this.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel8.Controls.Add(this.panel21);
            this.panel8.Controls.Add(this.pnlTotalRsrv);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel8.Location = new System.Drawing.Point(0, 53);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel8.Size = new System.Drawing.Size(1034, 462);
            this.panel8.TabIndex = 8;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.c1FlexGridAvailResrv);
            this.panel21.Controls.Add(this.label49);
            this.panel21.Controls.Add(this.panel22);
            this.panel21.Controls.Add(this.label52);
            this.panel21.Controls.Add(this.label53);
            this.panel21.Controls.Add(this.label54);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(0, 0);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel21.Size = new System.Drawing.Size(1034, 440);
            this.panel21.TabIndex = 42;
            // 
            // c1FlexGridAvailResrv
            // 
            this.c1FlexGridAvailResrv.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGridAvailResrv.AllowEditing = false;
            this.c1FlexGridAvailResrv.AutoGenerateColumns = false;
            this.c1FlexGridAvailResrv.BackColor = System.Drawing.Color.White;
            this.c1FlexGridAvailResrv.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGridAvailResrv.ColumnInfo = resources.GetString("c1FlexGridAvailResrv.ColumnInfo");
            this.c1FlexGridAvailResrv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGridAvailResrv.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexGridAvailResrv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexGridAvailResrv.Location = new System.Drawing.Point(4, 26);
            this.c1FlexGridAvailResrv.Name = "c1FlexGridAvailResrv";
            this.c1FlexGridAvailResrv.Padding = new System.Windows.Forms.Padding(3);
            this.c1FlexGridAvailResrv.Rows.Count = 1;
            this.c1FlexGridAvailResrv.Rows.DefaultSize = 19;
            this.c1FlexGridAvailResrv.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGridAvailResrv.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlexGridAvailResrv.ShowCellLabels = true;
            this.c1FlexGridAvailResrv.Size = new System.Drawing.Size(1026, 413);
            this.c1FlexGridAvailResrv.StyleInfo = resources.GetString("c1FlexGridAvailResrv.StyleInfo");
            this.c1FlexGridAvailResrv.TabIndex = 35;
            this.c1FlexGridAvailResrv.Tag = "ClosePeriod";
            this.c1FlexGridAvailResrv.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexGridAvailResrv_MouseMove);
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(4, 439);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1026, 1);
            this.label49.TabIndex = 113;
            this.label49.Text = "label1";
            // 
            // panel22
            // 
            this.panel22.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel22.Controls.Add(this.label50);
            this.panel22.Controls.Add(this.label10);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Location = new System.Drawing.Point(4, 4);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(1026, 22);
            this.panel22.TabIndex = 6;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(0, 21);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1026, 1);
            this.label50.TabIndex = 58;
            this.label50.Text = "label1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1026, 23);
            this.label10.TabIndex = 40;
            this.label10.Text = "  Available Reserves";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label52.Location = new System.Drawing.Point(3, 4);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 436);
            this.label52.TabIndex = 111;
            this.label52.Text = "label2";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label53.Location = new System.Drawing.Point(1030, 4);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 436);
            this.label53.TabIndex = 112;
            this.label53.Text = "label2";
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Top;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(3, 3);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1028, 1);
            this.label54.TabIndex = 114;
            this.label54.Text = "label1";
            // 
            // pnlTotalRsrv
            // 
            this.pnlTotalRsrv.BackColor = System.Drawing.Color.Transparent;
            this.pnlTotalRsrv.Controls.Add(this.c1FlexGridTotalAvailResrv);
            this.pnlTotalRsrv.Controls.Add(this.label57);
            this.pnlTotalRsrv.Controls.Add(this.label55);
            this.pnlTotalRsrv.Controls.Add(this.label51);
            this.pnlTotalRsrv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotalRsrv.Location = new System.Drawing.Point(0, 440);
            this.pnlTotalRsrv.Name = "pnlTotalRsrv";
            this.pnlTotalRsrv.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlTotalRsrv.Size = new System.Drawing.Size(1034, 19);
            this.pnlTotalRsrv.TabIndex = 43;
            // 
            // c1FlexGridTotalAvailResrv
            // 
            this.c1FlexGridTotalAvailResrv.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGridTotalAvailResrv.AllowEditing = false;
            this.c1FlexGridTotalAvailResrv.AutoGenerateColumns = false;
            this.c1FlexGridTotalAvailResrv.BackColor = System.Drawing.Color.White;
            this.c1FlexGridTotalAvailResrv.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGridTotalAvailResrv.ColumnInfo = resources.GetString("c1FlexGridTotalAvailResrv.ColumnInfo");
            this.c1FlexGridTotalAvailResrv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGridTotalAvailResrv.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexGridTotalAvailResrv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexGridTotalAvailResrv.Location = new System.Drawing.Point(4, 0);
            this.c1FlexGridTotalAvailResrv.Name = "c1FlexGridTotalAvailResrv";
            this.c1FlexGridTotalAvailResrv.Padding = new System.Windows.Forms.Padding(3);
            this.c1FlexGridTotalAvailResrv.Rows.Count = 1;
            this.c1FlexGridTotalAvailResrv.Rows.DefaultSize = 19;
            this.c1FlexGridTotalAvailResrv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1FlexGridTotalAvailResrv.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1FlexGridTotalAvailResrv.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlexGridTotalAvailResrv.ShowCellLabels = true;
            this.c1FlexGridTotalAvailResrv.Size = new System.Drawing.Size(1026, 18);
            this.c1FlexGridTotalAvailResrv.StyleInfo = resources.GetString("c1FlexGridTotalAvailResrv.StyleInfo");
            this.c1FlexGridTotalAvailResrv.TabIndex = 117;
            this.c1FlexGridTotalAvailResrv.Tag = "ClosePeriod";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(4, 18);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1026, 1);
            this.label57.TabIndex = 116;
            this.label57.Text = "label1";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Right;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label55.Location = new System.Drawing.Point(1030, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 19);
            this.label55.TabIndex = 113;
            this.label55.Text = "label2";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Left;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label51.Location = new System.Drawing.Point(3, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 19);
            this.label51.TabIndex = 112;
            this.label51.Text = "label2";
            // 
            // frmViewAvailableReserveV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1034, 515);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewAvailableReserveV2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Available Reserve";
            this.Load += new System.EventHandler(this.frmViewAvailableReserveV2_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridAvailResrv)).EndInit();
            this.panel22.ResumeLayout(false);
            this.pnlTotalRsrv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridTotalAvailResrv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel21;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridAvailResrv;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Panel pnlTotalRsrv;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridTotalAvailResrv;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label51;
    }
}