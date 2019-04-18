namespace gloEmdeonInterface.Forms
{
    partial class frmHL7_MessageQueue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHL7_MessageQueue));
            this.pnlFill = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlregistration = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblProcessInformation = new System.Windows.Forms.Label();
            this.C1Flex = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cmbServiceName = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtvalue = new System.Windows.Forms.TextBox();
            this.lblColumName = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.lblPage = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tlpViewReport = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.tlpRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlpDeleteLog = new System.Windows.Forms.ToolStripButton();
            this.tlpExport = new System.Windows.Forms.ToolStripButton();
            this.tlpClose = new System.Windows.Forms.ToolStripButton();
            this.pnlFill.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlregistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.tlpViewReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.Label2);
            this.pnlFill.Controls.Add(this.pnlGrid);
            this.pnlFill.Controls.Add(this.pnlTop);
            this.pnlFill.Controls.Add(this.Label1);
            this.pnlFill.Controls.Add(this.Label5);
            this.pnlFill.Controls.Add(this.Label3);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 54);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Padding = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.pnlFill.Size = new System.Drawing.Size(1120, 812);
            this.pnlFill.TabIndex = 33;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(4, 809);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1112, 1);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "3";
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.pnlregistration);
            this.pnlGrid.Controls.Add(this.C1Flex);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(4, 46);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1112, 764);
            this.pnlGrid.TabIndex = 1;
            // 
            // pnlregistration
            // 
            this.pnlregistration.BackColor = System.Drawing.Color.White;
            this.pnlregistration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlregistration.BackgroundImage")));
            this.pnlregistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlregistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlregistration.Controls.Add(this.lblPleaseWait);
            this.pnlregistration.Controls.Add(this.lblProcessInformation);
            this.pnlregistration.Location = new System.Drawing.Point(325, 333);
            this.pnlregistration.Name = "pnlregistration";
            this.pnlregistration.Size = new System.Drawing.Size(459, 92);
            this.pnlregistration.TabIndex = 44;
            this.pnlregistration.Visible = false;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(27, 13);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(119, 19);
            this.lblPleaseWait.TabIndex = 0;
            this.lblPleaseWait.Text = "Please wait...";
            // 
            // lblProcessInformation
            // 
            this.lblProcessInformation.AutoSize = true;
            this.lblProcessInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblProcessInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProcessInformation.Location = new System.Drawing.Point(28, 48);
            this.lblProcessInformation.Name = "lblProcessInformation";
            this.lblProcessInformation.Size = new System.Drawing.Size(174, 14);
            this.lblProcessInformation.TabIndex = 0;
            this.lblProcessInformation.Text = "Request is being processed";
            // 
            // C1Flex
            // 
            this.C1Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1Flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1Flex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Flex.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.C1Flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Flex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Flex.Location = new System.Drawing.Point(0, 0);
            this.C1Flex.Name = "C1Flex";
            this.C1Flex.Rows.DefaultSize = 19;
            this.C1Flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Flex.ShowCellLabels = true;
            this.C1Flex.Size = new System.Drawing.Size(1112, 764);
            this.C1Flex.StyleInfo = resources.GetString("C1Flex.StyleInfo");
            this.C1Flex.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.cmbServiceName);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.txtSearch);
            this.pnlTop.Controls.Add(this.label12);
            this.pnlTop.Controls.Add(this.btnViewReport);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.txtvalue);
            this.pnlTop.Controls.Add(this.lblColumName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(4, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1112, 42);
            this.pnlTop.TabIndex = 0;
            // 
            // cmbServiceName
            // 
            this.cmbServiceName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbServiceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceName.FormattingEnabled = true;
            this.cmbServiceName.Items.AddRange(new object[] {
            "HL7"});
            this.cmbServiceName.Location = new System.Drawing.Point(332, 9);
            this.cmbServiceName.Name = "cmbServiceName";
            this.cmbServiceName.Size = new System.Drawing.Size(285, 22);
            this.cmbServiceName.TabIndex = 26;
            this.cmbServiceName.SelectedIndexChanged += new System.EventHandler(this.cmbServiceName_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(275, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 14);
            this.label9.TabIndex = 25;
            this.label9.Text = "Service :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1112, 1);
            this.label13.TabIndex = 24;
            this.label13.Text = "3";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(75, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(166, 22);
            this.txtSearch.TabIndex = 23;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtPatientCode_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 14);
            this.label12.TabIndex = 22;
            this.label12.Text = "Search :";
            // 
            // btnViewReport
            // 
            this.btnViewReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewReport.BackgroundImage")));
            this.btnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReport.Location = new System.Drawing.Point(1598, 15);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(14, 29);
            this.btnViewReport.TabIndex = 9;
            this.btnViewReport.Text = "&View Report";
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(1477, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(14, 29);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Visible = false;
            // 
            // txtvalue
            // 
            this.txtvalue.Location = new System.Drawing.Point(1431, 18);
            this.txtvalue.Name = "txtvalue";
            this.txtvalue.Size = new System.Drawing.Size(12, 22);
            this.txtvalue.TabIndex = 1;
            this.txtvalue.Visible = false;
            // 
            // lblColumName
            // 
            this.lblColumName.AutoSize = true;
            this.lblColumName.BackColor = System.Drawing.Color.Transparent;
            this.lblColumName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumName.Location = new System.Drawing.Point(1358, 19);
            this.lblColumName.Name = "lblColumName";
            this.lblColumName.Size = new System.Drawing.Size(48, 14);
            this.lblColumName.TabIndex = 0;
            this.lblColumName.Text = "Name :";
            this.lblColumName.Visible = false;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(4, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1112, 1);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "3";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(3, 3);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(1, 807);
            this.Label5.TabIndex = 17;
            this.Label5.Text = "3";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(1116, 3);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 807);
            this.Label3.TabIndex = 18;
            this.Label3.Text = "3";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel2.Location = new System.Drawing.Point(0, 866);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Panel2.Size = new System.Drawing.Size(1120, 30);
            this.Panel2.TabIndex = 34;
            // 
            // Panel3
            // 
            this.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel3.Controls.Add(this.lblPage);
            this.Panel3.Controls.Add(this.Label8);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.Label6);
            this.Panel3.Controls.Add(this.Label4);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(3, 0);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(1114, 27);
            this.Panel3.TabIndex = 0;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage.Location = new System.Drawing.Point(30, 4);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(57, 14);
            this.lblPage.TabIndex = 18;
            this.lblPage.Text = "Page No";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(1, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1112, 1);
            this.Label8.TabIndex = 21;
            this.Label8.Text = "3";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(1, 26);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1112, 1);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "3";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(1113, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 27);
            this.Label6.TabIndex = 19;
            this.Label6.Text = "3";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 27);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "3";
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.tlpViewReport);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1120, 54);
            this.Panel1.TabIndex = 35;
            // 
            // tlpViewReport
            // 
            this.tlpViewReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlpViewReport.BackgroundImage")));
            this.tlpViewReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlpViewReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpViewReport.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlpViewReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevious,
            this.btnNext,
            this.tlpRefresh,
            this.tlpDeleteLog,
            this.tlpExport,
            this.tlpClose});
            this.tlpViewReport.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlpViewReport.Location = new System.Drawing.Point(0, 0);
            this.tlpViewReport.Name = "tlpViewReport";
            this.tlpViewReport.Size = new System.Drawing.Size(1120, 53);
            this.tlpViewReport.TabIndex = 0;
            this.tlpViewReport.Text = "ToolStrip1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(63, 50);
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(39, 50);
            this.btnNext.Text = "&Next";
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            // tlpDeleteLog
            // 
            this.tlpDeleteLog.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpDeleteLog.Image = ((System.Drawing.Image)(resources.GetObject("tlpDeleteLog.Image")));
            this.tlpDeleteLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpDeleteLog.Name = "tlpDeleteLog";
            this.tlpDeleteLog.Size = new System.Drawing.Size(133, 50);
            this.tlpDeleteLog.Text = "&Delete Message Log";
            this.tlpDeleteLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlpDeleteLog.Visible = false;
            // 
            // tlpExport
            // 
            this.tlpExport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpExport.Image = ((System.Drawing.Image)(resources.GetObject("tlpExport.Image")));
            this.tlpExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlpExport.Name = "tlpExport";
            this.tlpExport.Size = new System.Drawing.Size(114, 50);
            this.tlpExport.Text = " &Export to Excel";
            this.tlpExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlpExport.Visible = false;
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
            // frmHL7_MessageQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1120, 896);
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHL7_MessageQueue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Queue";
            this.Load += new System.EventHandler(this.frmHL7_MessageQueue_Load);
            this.Shown += new System.EventHandler(this.frmHL7_MessageQueue_Shown);
            this.pnlFill.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlregistration.ResumeLayout(false);
            this.pnlregistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.tlpViewReport.ResumeLayout(false);
            this.tlpViewReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlFill;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlregistration;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.Label lblProcessInformation;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Flex;
        internal System.Windows.Forms.Panel pnlTop;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Button btnViewReport;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.TextBox txtvalue;
        internal System.Windows.Forms.Label lblColumName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Label lblPage;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Panel Panel1;
        internal gloGlobal.gloToolStripIgnoreFocus tlpViewReport;
        internal System.Windows.Forms.ToolStripButton btnPrevious;
        internal System.Windows.Forms.ToolStripButton btnNext;
        internal System.Windows.Forms.ToolStripButton tlpRefresh;
        internal System.Windows.Forms.ToolStripButton tlpDeleteLog;
        internal System.Windows.Forms.ToolStripButton tlpExport;
        internal System.Windows.Forms.ToolStripButton tlpClose;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ComboBox cmbServiceName;
    }
}