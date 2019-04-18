namespace gloBilling
{
    partial class frmRpt_DuplicateClaimReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_DuplicateClaimReport));
            this.tls_Strip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnPrint = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCharge = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnl_View = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.c1Claim = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.tls_Strip.SuspendLayout();
            this.pnl_View.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Claim)).BeginInit();
            this.pnlCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Strip
            // 
            this.tls_Strip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Strip.BackgroundImage")));
            this.tls_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Strip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnPrint,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnCharge,
            this.tls_btnClose});
            this.tls_Strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Strip.Location = new System.Drawing.Point(0, 0);
            this.tls_Strip.Name = "tls_Strip";
            this.tls_Strip.Size = new System.Drawing.Size(904, 53);
            this.tls_Strip.TabIndex = 3;
            this.tls_Strip.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(93, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Show Report";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tls_btnExportToExcelOpen
            // 
            this.tls_btnExportToExcelOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcelOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcelOpen.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcelOpen.Image")));
            this.tls_btnExportToExcelOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcelOpen.Name = "tls_btnExportToExcelOpen";
            this.tls_btnExportToExcelOpen.Size = new System.Drawing.Size(154, 50);
            this.tls_btnExportToExcelOpen.Tag = "ExportnOpen";
            this.tls_btnExportToExcelOpen.Text = "Export To Excel && Open";
            this.tls_btnExportToExcelOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcelOpen.ToolTipText = "Export To Excel and Open";
            this.tls_btnExportToExcelOpen.Click += new System.EventHandler(this.tls_btnExportToExcelOpen_Click);
            // 
            // tls_btnExportToExcel
            // 
            this.tls_btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcel.Image")));
            this.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcel.Name = "tls_btnExportToExcel";
            this.tls_btnExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tls_btnExportToExcel.Tag = "Export";
            this.tls_btnExportToExcel.Text = "Export To Excel";
            this.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcel.Click += new System.EventHandler(this.tls_btnExportToExcel_Click);
            // 
            // tls_btnPrint
            // 
            this.tls_btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnPrint.Image")));
            this.tls_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnPrint.Name = "tls_btnPrint";
            this.tls_btnPrint.Size = new System.Drawing.Size(41, 50);
            this.tls_btnPrint.Tag = "Print";
            this.tls_btnPrint.Text = "&Print";
            this.tls_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnPrint.ToolTipText = "Print";
            this.tls_btnPrint.Click += new System.EventHandler(this.tls_btnPrint_Click);
            // 
            // tls_btnCharge
            // 
            this.tls_btnCharge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCharge.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCharge.Image")));
            this.tls_btnCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCharge.Name = "tls_btnCharge";
            this.tls_btnCharge.Size = new System.Drawing.Size(106, 50);
            this.tls_btnCharge.Tag = "OpenCharge";
            this.tls_btnCharge.Text = "&Modify Charges";
            this.tls_btnCharge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCharge.ToolTipText = "Modify Charges";
            this.tls_btnCharge.Click += new System.EventHandler(this.tls_btnCharge_Click);
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
            // pnl_View
            // 
            this.pnl_View.Controls.Add(this.label1);
            this.pnl_View.Controls.Add(this.label2);
            this.pnl_View.Controls.Add(this.label3);
            this.pnl_View.Controls.Add(this.label4);
            this.pnl_View.Controls.Add(this.c1Claim);
            this.pnl_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_View.Location = new System.Drawing.Point(0, 113);
            this.pnl_View.Name = "pnl_View";
            this.pnl_View.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnl_View.Size = new System.Drawing.Size(904, 424);
            this.pnl_View.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(899, 1);
            this.label1.TabIndex = 27;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 420);
            this.label2.TabIndex = 26;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(900, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 420);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(901, 1);
            this.label4.TabIndex = 24;
            this.label4.Text = "label1";
            // 
            // c1Claim
            // 
            this.c1Claim.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Claim.AllowEditing = false;
            this.c1Claim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Claim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Claim.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Claim.ColumnInfo = "0,0,0,0,0,105,Columns:";
            this.c1Claim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Claim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Claim.Location = new System.Drawing.Point(0, 0);
            this.c1Claim.Name = "c1Claim";
            this.c1Claim.Rows.Count = 1;
            this.c1Claim.Rows.DefaultSize = 21;
            this.c1Claim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Claim.Size = new System.Drawing.Size(901, 421);
            this.c1Claim.StyleInfo = resources.GetString("c1Claim.StyleInfo");
            this.c1Claim.TabIndex = 23;
            this.c1Claim.DoubleClick += new System.EventHandler(this.c1Claim_DoubleClick);
            this.c1Claim.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Claim_MouseDoubleClick);
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.dtpEndDate);
            this.pnlCriteria.Controls.Add(this.label9);
            this.pnlCriteria.Controls.Add(this.lblEndDate);
            this.pnlCriteria.Controls.Add(this.label10);
            this.pnlCriteria.Controls.Add(this.label11);
            this.pnlCriteria.Controls.Add(this.dtpStartDate);
            this.pnlCriteria.Controls.Add(this.label12);
            this.pnlCriteria.Controls.Add(this.lblStartDate);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 53);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCriteria.Size = new System.Drawing.Size(904, 60);
            this.pnlCriteria.TabIndex = 92;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "MM/dd/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(301, 20);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(101, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(896, 1);
            this.label9.TabIndex = 99;
            this.label9.Text = "label4";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(233, 24);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(60, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "To Date :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(896, 1);
            this.label10.TabIndex = 98;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(900, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 54);
            this.label11.TabIndex = 97;
            this.label11.Text = "label4";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(108, 20);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(102, 22);
            this.dtpStartDate.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 54);
            this.label12.TabIndex = 96;
            this.label12.Text = "label4";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(31, 24);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "From Date :";
            // 
            // frmRpt_DuplicateClaimReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(904, 537);
            this.Controls.Add(this.pnl_View);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.tls_Strip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_DuplicateClaimReport";
            this.ShowInTaskbar = false;
            this.Text = "Duplicate Claim Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_DuplicateClaimReport_FormClosed);
            this.Load += new System.EventHandler(this.frmRpt_DuplicateClaimReport_Load);
            this.tls_Strip.ResumeLayout(false);
            this.tls_Strip.PerformLayout();
            this.pnl_View.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Claim)).EndInit();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Strip;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel pnl_View;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Claim;
        private System.Windows.Forms.Panel pnlCriteria;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCharge;
        internal System.Windows.Forms.ToolStripButton tls_btnPrint;
    }
}