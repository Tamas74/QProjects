namespace gloEmdeonInterface.Forms
{
    partial class frmViewOrderTests
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewOrderTests));
            this.pnl_tlsp = new System.Windows.Forms.Panel();
            this.tlsp_OrderTests = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.C1SplitOrderTests = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkEmdeon = new System.Windows.Forms.CheckBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_tlsp.SuspendLayout();
            this.tlsp_OrderTests.SuspendLayout();
            this.pnlmain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1SplitOrderTests)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp
            // 
            this.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp.Controls.Add(this.tlsp_OrderTests);
            this.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp.Name = "pnl_tlsp";
            this.pnl_tlsp.Size = new System.Drawing.Size(654, 54);
            this.pnl_tlsp.TabIndex = 16;
            // 
            // tlsp_OrderTests
            // 
            this.tlsp_OrderTests.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_OrderTests.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_OrderTests.BackgroundImage")));
            this.tlsp_OrderTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_OrderTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_OrderTests.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_OrderTests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSave,
            this.ts_btnClose});
            this.tlsp_OrderTests.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_OrderTests.Location = new System.Drawing.Point(0, 0);
            this.tlsp_OrderTests.Name = "tlsp_OrderTests";
            this.tlsp_OrderTests.Size = new System.Drawing.Size(654, 53);
            this.tlsp_OrderTests.TabIndex = 0;
            this.tlsp_OrderTests.Text = "toolStrip1";
            this.tlsp_OrderTests.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsp_OrderTests_ItemClicked);
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "&Save&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlmain
            // 
            this.pnlmain.Controls.Add(this.label10);
            this.pnlmain.Controls.Add(this.C1SplitOrderTests);
            this.pnlmain.Controls.Add(this.label9);
            this.pnlmain.Controls.Add(this.label5);
            this.pnlmain.Controls.Add(this.label6);
            this.pnlmain.Controls.Add(this.label7);
            this.pnlmain.Controls.Add(this.label8);
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmain.Location = new System.Drawing.Point(0, 94);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlmain.Size = new System.Drawing.Size(654, 484);
            this.pnlmain.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(4, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(646, 1);
            this.label10.TabIndex = 19;
            this.label10.Text = "label1";
            // 
            // C1SplitOrderTests
            // 
            this.C1SplitOrderTests.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1SplitOrderTests.AllowEditing = false;
            this.C1SplitOrderTests.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this.C1SplitOrderTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1SplitOrderTests.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1SplitOrderTests.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.C1SplitOrderTests.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.C1SplitOrderTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1SplitOrderTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1SplitOrderTests.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1SplitOrderTests.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1SplitOrderTests.Location = new System.Drawing.Point(4, 45);
            this.C1SplitOrderTests.Name = "C1SplitOrderTests";
            this.C1SplitOrderTests.Rows.Count = 1;
            this.C1SplitOrderTests.Rows.DefaultSize = 19;
            this.C1SplitOrderTests.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1SplitOrderTests.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1SplitOrderTests.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None;
            this.C1SplitOrderTests.Size = new System.Drawing.Size(646, 435);
            this.C1SplitOrderTests.StyleInfo = resources.GetString("C1SplitOrderTests.StyleInfo");
            this.C1SplitOrderTests.TabIndex = 17;
            this.C1SplitOrderTests.Tree.LineColor = System.Drawing.Color.LightGray;
            this.C1SplitOrderTests.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("C1SplitOrderTests.Tree.NodeImageCollapsed")));
            this.C1SplitOrderTests.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("C1SplitOrderTests.Tree.NodeImageExpanded")));
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(226, 14);
            this.label9.TabIndex = 18;
            this.label9.Text = "Select tests for creating new order.";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(646, 1);
            this.label5.TabIndex = 11;
            this.label5.Text = "label1";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(650, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 480);
            this.label6.TabIndex = 10;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 480);
            this.label7.TabIndex = 9;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(3, 480);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(648, 1);
            this.label8.TabIndex = 8;
            this.label8.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkEmdeon);
            this.panel2.Controls.Add(this.Label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(654, 40);
            this.panel2.TabIndex = 18;
            // 
            // chkEmdeon
            // 
            this.chkEmdeon.AutoSize = true;
            this.chkEmdeon.BackColor = System.Drawing.Color.Transparent;
            this.chkEmdeon.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmdeon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkEmdeon.Location = new System.Drawing.Point(26, 11);
            this.chkEmdeon.Name = "chkEmdeon";
            this.chkEmdeon.Size = new System.Drawing.Size(243, 18);
            this.chkEmdeon.TabIndex = 45;
            this.chkEmdeon.Text = "Create the new order through Emdeon";
            this.chkEmdeon.UseVisualStyleBackColor = false;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Location = new System.Drawing.Point(4, 36);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(646, 1);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 33);
            this.label2.TabIndex = 7;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(647, 1);
            this.label3.TabIndex = 6;
            this.label3.Text = "label1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(650, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "label3";
            // 
            // frmViewOrderTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(654, 578);
            this.Controls.Add(this.pnlmain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_tlsp);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewOrderTests";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Split Order";
            this.Load += new System.EventHandler(this.frmViewOrderTests_Load);
            this.pnl_tlsp.ResumeLayout(false);
            this.pnl_tlsp.PerformLayout();
            this.tlsp_OrderTests.ResumeLayout(false);
            this.tlsp_OrderTests.PerformLayout();
            this.pnlmain.ResumeLayout(false);
            this.pnlmain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1SplitOrderTests)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_OrderTests;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal C1.Win.C1FlexGrid.C1FlexGrid C1SplitOrderTests;
        internal System.Windows.Forms.CheckBox chkEmdeon;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}