namespace DMSImport
{
    partial class DMSImportReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DMSImportReport));
            this.pnlFill = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.C1Flex = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtvalue = new System.Windows.Forms.TextBox();
            this.lblColumName = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tlpViewReport = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlpRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlpClose = new System.Windows.Forms.ToolStripButton();
            this.pnlFill.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.tlpViewReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.pnlGrid);
            this.pnlFill.Controls.Add(this.pnlTop);
            this.pnlFill.Controls.Add(this.Panel1);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(1010, 728);
            this.pnlFill.TabIndex = 1;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.C1Flex);
            this.pnlGrid.Controls.Add(this.label1);
            this.pnlGrid.Controls.Add(this.label2);
            this.pnlGrid.Controls.Add(this.label4);
            this.pnlGrid.Controls.Add(this.label5);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 93);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlGrid.Size = new System.Drawing.Size(1010, 635);
            this.pnlGrid.TabIndex = 1;
            // 
            // C1Flex
            // 
            this.C1Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1Flex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Flex.ColumnInfo = resources.GetString("C1Flex.ColumnInfo");
            this.C1Flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Flex.ExtendLastCol = true;
            this.C1Flex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Flex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Flex.Location = new System.Drawing.Point(4, 1);
            this.C1Flex.Name = "C1Flex";
            this.C1Flex.Rows.DefaultSize = 19;
            this.C1Flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Flex.Size = new System.Drawing.Size(1002, 630);
            this.C1Flex.StyleInfo = resources.GetString("C1Flex.StyleInfo");
            this.C1Flex.TabIndex = 2;
            this.C1Flex.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1Flex_AfterSort);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(4, 631);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1002, 1);
            this.label1.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1002, 1);
            this.label2.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1006, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 632);
            this.label4.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 632);
            this.label5.TabIndex = 22;
            // 
            // pnlTop
            // 
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.label8);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.txtvalue);
            this.pnlTop.Controls.Add(this.lblColumName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 54);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTop.Size = new System.Drawing.Size(1010, 39);
            this.pnlTop.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(4, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1002, 1);
            this.label8.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1002, 1);
            this.label7.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(1006, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 33);
            this.label6.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 33);
            this.label3.TabIndex = 22;
            // 
            // txtvalue
            // 
            this.txtvalue.ForeColor = System.Drawing.Color.Black;
            this.txtvalue.Location = new System.Drawing.Point(207, 9);
            this.txtvalue.Name = "txtvalue";
            this.txtvalue.Size = new System.Drawing.Size(277, 22);
            this.txtvalue.TabIndex = 1;
            this.txtvalue.TextChanged += new System.EventHandler(this.txtvalue_TextChanged);
            // 
            // lblColumName
            // 
            this.lblColumName.BackColor = System.Drawing.Color.Transparent;
            this.lblColumName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblColumName.Location = new System.Drawing.Point(84, 13);
            this.lblColumName.Name = "lblColumName";
            this.lblColumName.Size = new System.Drawing.Size(116, 14);
            this.lblColumName.TabIndex = 0;
            this.lblColumName.Text = "Date :";
            this.lblColumName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel1
            // 
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.tlpViewReport);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1010, 54);
            this.Panel1.TabIndex = 30;
            // 
            // tlpViewReport
            // 
            this.tlpViewReport.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tlpViewReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlpViewReport.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlpViewReport.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlpViewReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlpRefresh,
            this.tlpClose});
            this.tlpViewReport.Location = new System.Drawing.Point(0, 0);
            this.tlpViewReport.Name = "tlpViewReport";
            this.tlpViewReport.Size = new System.Drawing.Size(1010, 53);
            this.tlpViewReport.TabIndex = 0;
            this.tlpViewReport.Text = "ToolStrip1";
            // 
            // tlpRefresh
            // 
            this.tlpRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tlpRefresh.Image")));
            this.tlpRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpRefresh.Name = "tlpRefresh";
            this.tlpRefresh.Size = new System.Drawing.Size(58, 50);
            this.tlpRefresh.Text = "&Refresh";
            this.tlpRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlpRefresh.Click += new System.EventHandler(this.tlpRefresh_Click);
            // 
            // tlpClose
            // 
            this.tlpClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpClose.Image = ((System.Drawing.Image)(resources.GetObject("tlpClose.Image")));
            this.tlpClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpClose.Name = "tlpClose";
            this.tlpClose.Size = new System.Drawing.Size(43, 50);
            this.tlpClose.Text = "&Close";
            this.tlpClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlpClose.Click += new System.EventHandler(this.tlpClose_Click);
            // 
            // DMSImportReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1010, 728);
            this.Controls.Add(this.pnlFill);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DMSImportReport";
            this.Text = "View Report";
            this.Load += new System.EventHandler(this.DMSImportReport_Load);
            this.pnlFill.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.tlpViewReport.ResumeLayout(false);
            this.tlpViewReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlFill;
        internal System.Windows.Forms.Panel pnlGrid;
        internal C1.Win.C1FlexGrid.C1FlexGrid C1Flex;
        internal System.Windows.Forms.Panel pnlTop;
        internal System.Windows.Forms.TextBox txtvalue;
        internal System.Windows.Forms.Label lblColumName;
        internal System.Windows.Forms.Panel Panel1;
        internal gloGlobal.gloToolStripIgnoreFocus tlpViewReport;
        internal System.Windows.Forms.ToolStripButton tlpRefresh;
        internal System.Windows.Forms.ToolStripButton tlpClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}