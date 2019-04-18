namespace gloBilling
{
    partial class frmRpt_ClaimStatusReportNew
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_ClaimStatusReportNew));
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnl_Treeview = new System.Windows.Forms.Panel();
            this.trv_viewRemittance = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.tls_Strip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlTemplateDetails = new System.Windows.Forms.Panel();
            this.pnl_Filter = new System.Windows.Forms.Panel();
            this.pnlTransDate = new System.Windows.Forms.Panel();
            this.lbl_datefilter = new System.Windows.Forms.Label();
            this.cmb_datefilter = new System.Windows.Forms.ComboBox();
            this.pnlDates = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnl_View = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.c1Discrepancy = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnl_Main.SuspendLayout();
            this.pnl_Treeview.SuspendLayout();
            this.tls_Strip.SuspendLayout();
            this.pnlTemplateDetails.SuspendLayout();
            this.pnl_Filter.SuspendLayout();
            this.pnlTransDate.SuspendLayout();
            this.pnlDates.SuspendLayout();
            this.pnl_View.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Discrepancy)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Main
            // 
            this.pnl_Main.Controls.Add(this.pnlTemplateDetails);
            this.pnl_Main.Controls.Add(this.splitter1);
            this.pnl_Main.Controls.Add(this.pnl_Treeview);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 53);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnl_Main.Size = new System.Drawing.Size(904, 484);
            this.pnl_Main.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(217, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 481);
            this.splitter1.TabIndex = 84;
            this.splitter1.TabStop = false;
            // 
            // pnl_Treeview
            // 
            this.pnl_Treeview.Controls.Add(this.trv_viewRemittance);
            this.pnl_Treeview.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnl_Treeview.Controls.Add(this.Label5);
            this.pnl_Treeview.Controls.Add(this.Label6);
            this.pnl_Treeview.Controls.Add(this.Label7);
            this.pnl_Treeview.Controls.Add(this.Label8);
            this.pnl_Treeview.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Treeview.Location = new System.Drawing.Point(0, 3);
            this.pnl_Treeview.Name = "pnl_Treeview";
            this.pnl_Treeview.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_Treeview.Size = new System.Drawing.Size(217, 481);
            this.pnl_Treeview.TabIndex = 1;
            // 
            // trv_viewRemittance
            // 
            this.trv_viewRemittance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trv_viewRemittance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_viewRemittance.ForeColor = System.Drawing.Color.Black;
            this.trv_viewRemittance.HideSelection = false;
            this.trv_viewRemittance.ImageIndex = 0;
            this.trv_viewRemittance.ImageList = this.imageList1;
            this.trv_viewRemittance.Indent = 20;
            this.trv_viewRemittance.ItemHeight = 20;
            this.trv_viewRemittance.Location = new System.Drawing.Point(4, 5);
            this.trv_viewRemittance.Name = "trv_viewRemittance";
            this.trv_viewRemittance.SelectedImageKey = "Bullet06.png";
            this.trv_viewRemittance.ShowLines = false;
            this.trv_viewRemittance.ShowPlusMinus = false;
            this.trv_viewRemittance.ShowRootLines = false;
            this.trv_viewRemittance.Size = new System.Drawing.Size(212, 472);
            this.trv_viewRemittance.TabIndex = 0;
            this.trv_viewRemittance.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_viewRemittance_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Bullet06.png");
            this.imageList1.Images.SetKeyName(1, "Discrepancy.ico");
            this.imageList1.Images.SetKeyName(2, "Claim Status.ico");
            this.imageList1.Images.SetKeyName(3, "Error Remits.ico");
            this.imageList1.Images.SetKeyName(4, "Process Remits.ico");
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(4, 1);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(212, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 38;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 477);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(212, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 477);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(216, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 477);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(214, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // tls_Strip
            // 
            this.tls_Strip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Strip.BackgroundImage")));
            this.tls_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Strip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnClose});
            this.tls_Strip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Strip.Location = new System.Drawing.Point(0, 0);
            this.tls_Strip.Name = "tls_Strip";
            this.tls_Strip.Size = new System.Drawing.Size(904, 53);
            this.tls_Strip.TabIndex = 2;
            this.tls_Strip.Text = "toolStrip1";
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
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 481);
            this.panel2.TabIndex = 85;
            // 
            // pnlTemplateDetails
            // 
            this.pnlTemplateDetails.Controls.Add(this.pnl_View);
            this.pnlTemplateDetails.Controls.Add(this.pnl_Filter);
            this.pnlTemplateDetails.Controls.Add(this.panel2);
            this.pnlTemplateDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateDetails.Location = new System.Drawing.Point(220, 3);
            this.pnlTemplateDetails.Name = "pnlTemplateDetails";
            this.pnlTemplateDetails.Size = new System.Drawing.Size(684, 481);
            this.pnlTemplateDetails.TabIndex = 86;
            // 
            // pnl_Filter
            // 
            this.pnl_Filter.Controls.Add(this.pnlTransDate);
            this.pnl_Filter.Controls.Add(this.pnlDates);
            this.pnl_Filter.Controls.Add(this.label9);
            this.pnl_Filter.Controls.Add(this.label10);
            this.pnl_Filter.Controls.Add(this.label11);
            this.pnl_Filter.Controls.Add(this.label12);
            this.pnl_Filter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Filter.Location = new System.Drawing.Point(0, 0);
            this.pnl_Filter.Name = "pnl_Filter";
            this.pnl_Filter.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnl_Filter.Size = new System.Drawing.Size(684, 38);
            this.pnl_Filter.TabIndex = 86;
            this.pnl_Filter.Visible = false;
            // 
            // pnlTransDate
            // 
            this.pnlTransDate.Controls.Add(this.lbl_datefilter);
            this.pnlTransDate.Controls.Add(this.cmb_datefilter);
            this.pnlTransDate.Location = new System.Drawing.Point(10, 3);
            this.pnlTransDate.Name = "pnlTransDate";
            this.pnlTransDate.Size = new System.Drawing.Size(275, 29);
            this.pnlTransDate.TabIndex = 207;
            // 
            // lbl_datefilter
            // 
            this.lbl_datefilter.AutoSize = true;
            this.lbl_datefilter.Location = new System.Drawing.Point(5, 7);
            this.lbl_datefilter.Name = "lbl_datefilter";
            this.lbl_datefilter.Size = new System.Drawing.Size(108, 14);
            this.lbl_datefilter.TabIndex = 216;
            this.lbl_datefilter.Text = "Transaction Date :";
            // 
            // cmb_datefilter
            // 
            this.cmb_datefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_datefilter.FormattingEnabled = true;
            this.cmb_datefilter.Location = new System.Drawing.Point(117, 3);
            this.cmb_datefilter.Name = "cmb_datefilter";
            this.cmb_datefilter.Size = new System.Drawing.Size(151, 22);
            this.cmb_datefilter.TabIndex = 1;
            this.cmb_datefilter.SelectedIndexChanged += new System.EventHandler(this.cmb_datefilter_SelectedIndexChanged);
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.dtpEndDate);
            this.pnlDates.Controls.Add(this.lblStartDate);
            this.pnlDates.Controls.Add(this.lblEndDate);
            this.pnlDates.Controls.Add(this.dtpStartDate);
            this.pnlDates.Location = new System.Drawing.Point(291, 3);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(356, 28);
            this.pnlDates.TabIndex = 208;
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
            this.dtpEndDate.Location = new System.Drawing.Point(255, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 22);
            this.dtpEndDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(6, 7);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(184, 7);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Text = "End Date :";
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
            this.dtpStartDate.Location = new System.Drawing.Point(83, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(95, 22);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(682, 1);
            this.label9.TabIndex = 27;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 34);
            this.label10.TabIndex = 26;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(683, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 34);
            this.label11.TabIndex = 25;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(684, 1);
            this.label12.TabIndex = 24;
            this.label12.Text = "label1";
            // 
            // pnl_View
            // 
            this.pnl_View.Controls.Add(this.label1);
            this.pnl_View.Controls.Add(this.label2);
            this.pnl_View.Controls.Add(this.label3);
            this.pnl_View.Controls.Add(this.label4);
            this.pnl_View.Controls.Add(this.c1Discrepancy);
            this.pnl_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_View.Location = new System.Drawing.Point(0, 38);
            this.pnl_View.Name = "pnl_View";
            this.pnl_View.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnl_View.Size = new System.Drawing.Size(684, 443);
            this.pnl_View.TabIndex = 87;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 439);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(682, 1);
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
            this.label2.Size = new System.Drawing.Size(1, 439);
            this.label2.TabIndex = 26;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(683, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 439);
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
            this.label4.Size = new System.Drawing.Size(684, 1);
            this.label4.TabIndex = 24;
            this.label4.Text = "label1";
            // 
            // c1Discrepancy
            // 
            this.c1Discrepancy.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Discrepancy.AllowEditing = false;
            this.c1Discrepancy.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Discrepancy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Discrepancy.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Discrepancy.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1Discrepancy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Discrepancy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Discrepancy.Location = new System.Drawing.Point(0, 0);
            this.c1Discrepancy.Name = "c1Discrepancy";
            this.c1Discrepancy.Rows.Count = 1;
            this.c1Discrepancy.Rows.DefaultSize = 19;
            this.c1Discrepancy.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Discrepancy.Size = new System.Drawing.Size(684, 440);
            this.c1Discrepancy.StyleInfo = resources.GetString("c1Discrepancy.StyleInfo");
            this.c1Discrepancy.TabIndex = 23;
            // 
            // frmRpt_ClaimStatusReportNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(904, 537);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.tls_Strip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRpt_ClaimStatusReportNew";
            this.ShowInTaskbar = false;
            this.Text = "Claim Status";
            this.Load += new System.EventHandler(this.frmRpt_ClaimStatusReportNew_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRpt_ClaimStatusReportNew_FormClosed);
            this.pnl_Main.ResumeLayout(false);
            this.pnl_Treeview.ResumeLayout(false);
            this.tls_Strip.ResumeLayout(false);
            this.tls_Strip.PerformLayout();
            this.pnlTemplateDetails.ResumeLayout(false);
            this.pnl_Filter.ResumeLayout(false);
            this.pnlTransDate.ResumeLayout(false);
            this.pnlTransDate.PerformLayout();
            this.pnlDates.ResumeLayout(false);
            this.pnlDates.PerformLayout();
            this.pnl_View.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Discrepancy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Strip;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnl_Treeview;
        private System.Windows.Forms.TreeView trv_viewRemittance;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.Panel pnlTemplateDetails;
        private System.Windows.Forms.Panel pnl_View;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Discrepancy;
        private System.Windows.Forms.Panel pnl_Filter;
        private System.Windows.Forms.Panel pnlTransDate;
        private System.Windows.Forms.Label lbl_datefilter;
        private System.Windows.Forms.ComboBox cmb_datefilter;
        private System.Windows.Forms.Panel pnlDates;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
    }
}