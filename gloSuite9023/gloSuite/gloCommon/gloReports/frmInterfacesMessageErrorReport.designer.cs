namespace gloReports
{
    partial class frmInterfacesMessageErrorReport
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
                    if (dtpToDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate);
                        }
                        catch
                        {
                        }
                        dtpToDate.Dispose();
                        dtpToDate = null;
                    }
                }
                catch
                {
                }

                try
                {
                    if (dtpFromDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate);
                        }
                        catch
                        {
                        }
                        dtpFromDate.Dispose();
                        dtpFromDate = null;
                    }
                }
                catch
                {
                }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInterfacesMessageErrorReport));
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.C1Flex = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.pnlregistration = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblProcessInformation = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtvalue = new System.Windows.Forms.TextBox();
            this.lblColumName = new System.Windows.Forms.Label();
            this.tabCntFilterReport = new System.Windows.Forms.TabControl();
            this.FilterCriteria = new System.Windows.Forms.TabPage();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnLast = new System.Windows.Forms.Button();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.BtnPrev = new System.Windows.Forms.Button();
            this.BtnFirst = new System.Windows.Forms.Button();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ChkUseDateRange = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.cmbServiceName = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
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
            this.tlpExport = new System.Windows.Forms.ToolStripButton();
            this.tlpDeleteLog = new System.Windows.Forms.ToolStripButton();
            this.tspViewHL7Message = new System.Windows.Forms.ToolStripButton();
            this.tlpRefresh = new System.Windows.Forms.ToolStripButton();
            this.tlpClose = new System.Windows.Forms.ToolStripButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbHL7Clients = new System.Windows.Forms.ComboBox();
            this.lblHL7Clients = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.miniToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).BeginInit();
            this.pnlregistration.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tabCntFilterReport.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.tlpViewReport.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.Label2);
            this.pnlGrid.Controls.Add(this.C1Flex);
            this.pnlGrid.Controls.Add(this.Label1);
            this.pnlGrid.Controls.Add(this.Label5);
            this.pnlGrid.Controls.Add(this.Label3);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 218);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.pnlGrid.Size = new System.Drawing.Size(1120, 678);
            this.pnlGrid.TabIndex = 33;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(4, 675);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1112, 1);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "3";
            // 
            // C1Flex
            // 
            this.C1Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1Flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1Flex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Flex.ColumnInfo = "10,1,0,0,0,105,Columns:";
            this.C1Flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Flex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Flex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Flex.Location = new System.Drawing.Point(4, 4);
            this.C1Flex.Name = "C1Flex";
            this.C1Flex.Rows.DefaultSize = 21;
            this.C1Flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Flex.ShowCellLabels = true;
            this.C1Flex.Size = new System.Drawing.Size(1112, 672);
            this.C1Flex.StyleInfo = resources.GetString("C1Flex.StyleInfo");
            this.C1Flex.TabIndex = 0;
            this.C1Flex.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1Flex_AfterSort);
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
            this.Label5.Size = new System.Drawing.Size(1, 673);
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
            this.Label3.Size = new System.Drawing.Size(1, 673);
            this.Label3.TabIndex = 18;
            this.Label3.Text = "3";
            // 
            // pnlregistration
            // 
            this.pnlregistration.BackColor = System.Drawing.Color.White;
            this.pnlregistration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlregistration.BackgroundImage")));
            this.pnlregistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlregistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlregistration.Controls.Add(this.lblPleaseWait);
            this.pnlregistration.Controls.Add(this.lblProcessInformation);
            this.pnlregistration.Location = new System.Drawing.Point(331, 402);
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
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.label13);
            this.pnlTop.Controls.Add(this.btnViewReport);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.txtvalue);
            this.pnlTop.Controls.Add(this.lblColumName);
            this.pnlTop.Location = new System.Drawing.Point(116, 261);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1112, 15);
            this.pnlTop.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1112, 1);
            this.label13.TabIndex = 24;
            this.label13.Text = "3";
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
            // tabCntFilterReport
            // 
            this.tabCntFilterReport.Controls.Add(this.FilterCriteria);
            this.tabCntFilterReport.Location = new System.Drawing.Point(308, 261);
            this.tabCntFilterReport.Name = "tabCntFilterReport";
            this.tabCntFilterReport.SelectedIndex = 0;
            this.tabCntFilterReport.Size = new System.Drawing.Size(86, 22);
            this.tabCntFilterReport.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabCntFilterReport.TabIndex = 0;
            // 
            // FilterCriteria
            // 
            this.FilterCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.FilterCriteria.Location = new System.Drawing.Point(4, 23);
            this.FilterCriteria.Name = "FilterCriteria";
            this.FilterCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.FilterCriteria.Size = new System.Drawing.Size(78, 0);
            this.FilterCriteria.TabIndex = 0;
            this.FilterCriteria.Text = "Filter Criteria";
            // 
            // chkTo
            // 
            this.chkTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTo.AutoSize = true;
            this.chkTo.Location = new System.Drawing.Point(546, 68);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(15, 14);
            this.chkTo.TabIndex = 6;
            this.chkTo.UseVisualStyleBackColor = true;
            this.chkTo.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.BtnLast);
            this.panel4.Controls.Add(this.Btn_Next);
            this.panel4.Controls.Add(this.lblSelected);
            this.panel4.Controls.Add(this.BtnPrev);
            this.panel4.Controls.Add(this.BtnFirst);
            this.panel4.Controls.Add(this.cmbPageSize);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1114, 26);
            this.panel4.TabIndex = 74;
            // 
            // BtnLast
            // 
            this.BtnLast.BackColor = System.Drawing.Color.Transparent;
            this.BtnLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnLast.BackgroundImage")));
            this.BtnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnLast.FlatAppearance.BorderSize = 0;
            this.BtnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLast.Location = new System.Drawing.Point(380, 2);
            this.BtnLast.Name = "BtnLast";
            this.BtnLast.Size = new System.Drawing.Size(24, 23);
            this.BtnLast.TabIndex = 5;
            this.BtnLast.UseVisualStyleBackColor = false;
            this.BtnLast.Click += new System.EventHandler(this.BtnLast_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Next.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Next.BackgroundImage")));
            this.Btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Btn_Next.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_Next.FlatAppearance.BorderSize = 0;
            this.Btn_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.Btn_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.Btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Next.Location = new System.Drawing.Point(356, 2);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(24, 23);
            this.Btn_Next.TabIndex = 4;
            this.Btn_Next.UseVisualStyleBackColor = false;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblSelected.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelected.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSelected.Location = new System.Drawing.Point(275, 2);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblSelected.Size = new System.Drawing.Size(81, 19);
            this.lblSelected.TabIndex = 4;
            this.lblSelected.Text = "Page 1 of 1";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPrev
            // 
            this.BtnPrev.BackColor = System.Drawing.Color.Transparent;
            this.BtnPrev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnPrev.BackgroundImage")));
            this.BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnPrev.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnPrev.FlatAppearance.BorderSize = 0;
            this.BtnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrev.Location = new System.Drawing.Point(251, 2);
            this.BtnPrev.Name = "BtnPrev";
            this.BtnPrev.Size = new System.Drawing.Size(24, 23);
            this.BtnPrev.TabIndex = 3;
            this.BtnPrev.UseVisualStyleBackColor = false;
            this.BtnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // BtnFirst
            // 
            this.BtnFirst.BackColor = System.Drawing.Color.Transparent;
            this.BtnFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnFirst.BackgroundImage")));
            this.BtnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnFirst.FlatAppearance.BorderSize = 0;
            this.BtnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            this.BtnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirst.Location = new System.Drawing.Point(227, 2);
            this.BtnFirst.Name = "BtnFirst";
            this.BtnFirst.Size = new System.Drawing.Size(24, 23);
            this.BtnFirst.TabIndex = 2;
            this.BtnFirst.UseVisualStyleBackColor = false;
            this.BtnFirst.Click += new System.EventHandler(this.BtnFirst_Click);
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(131, 2);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(96, 22);
            this.cmbPageSize.TabIndex = 1;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.cmbPageSize_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 2);
            this.label15.Name = "label15";
            this.label15.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label15.Size = new System.Drawing.Size(131, 17);
            this.label15.TabIndex = 10;
            this.label15.Text = "     Records per Page :";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1114, 2);
            this.label26.TabIndex = 72;
            this.label26.Text = "3";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 25);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1114, 1);
            this.label27.TabIndex = 73;
            this.label27.Text = "3";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(239, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 14);
            this.label11.TabIndex = 73;
            this.label11.Text = ":";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(61, 34);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(86, 26);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "View Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All",
            "Error",
            "Failed",
            "Success",
            "Error/Failed"});
            this.cmbStatus.Location = new System.Drawing.Point(546, 35);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(128, 22);
            this.cmbStatus.TabIndex = 2;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(494, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "Status :";
            // 
            // ChkUseDateRange
            // 
            this.ChkUseDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkUseDateRange.AutoSize = true;
            this.ChkUseDateRange.Location = new System.Drawing.Point(250, 68);
            this.ChkUseDateRange.Name = "ChkUseDateRange";
            this.ChkUseDateRange.Size = new System.Drawing.Size(15, 14);
            this.ChkUseDateRange.TabIndex = 4;
            this.ChkUseDateRange.UseVisualStyleBackColor = true;
            this.ChkUseDateRange.CheckedChanged += new System.EventHandler(this.ChkUseDateRange_CheckedChanged);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(434, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 14);
            this.label20.TabIndex = 61;
            this.label20.Text = "File Processed To :";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpToDate.CustomFormat = "";
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(565, 64);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(109, 22);
            this.dtpToDate.TabIndex = 7;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(127, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "File Processed From :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpFromDate.CustomFormat = "";
            this.dtpFromDate.Enabled = false;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(269, 64);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(109, 22);
            this.dtpFromDate.TabIndex = 5;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // cmbServiceName
            // 
            this.cmbServiceName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbServiceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceName.FormattingEnabled = true;
            this.cmbServiceName.Items.AddRange(new object[] {
            "HL7"});
            this.cmbServiceName.Location = new System.Drawing.Point(251, 35);
            this.cmbServiceName.Name = "cmbServiceName";
            this.cmbServiceName.Size = new System.Drawing.Size(127, 22);
            this.cmbServiceName.TabIndex = 1;
            this.cmbServiceName.SelectedIndexChanged += new System.EventHandler(this.cmbServiceName_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(252, 95);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(184, 22);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtPatientCode_TextChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(195, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Service :";
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(25, 99);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(216, 18);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Patient Code";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Location = new System.Drawing.Point(0, 866);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Panel2.Size = new System.Drawing.Size(1120, 30);
            this.Panel2.TabIndex = 34;
            this.Panel2.Visible = false;
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
            this.lblPage.Location = new System.Drawing.Point(15, 6);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(57, 14);
            this.lblPage.TabIndex = 18;
            this.lblPage.Text = "Page No";
            this.lblPage.Visible = false;
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
            this.Panel1.Size = new System.Drawing.Size(1120, 56);
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
            this.tlpExport,
            this.tlpDeleteLog,
            this.tspViewHL7Message,
            this.tlpRefresh,
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
            this.btnPrevious.Visible = false;
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
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            this.tlpExport.Click += new System.EventHandler(this.tlpExport_Click);
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
            this.tlpDeleteLog.Click += new System.EventHandler(this.tlpDeleteLog_Click_1);
            // 
            // tspViewHL7Message
            // 
            this.tspViewHL7Message.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspViewHL7Message.Image = ((System.Drawing.Image)(resources.GetObject("tspViewHL7Message.Image")));
            this.tspViewHL7Message.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspViewHL7Message.Name = "tspViewHL7Message";
            this.tspViewHL7Message.Size = new System.Drawing.Size(96, 50);
            this.tspViewHL7Message.Text = "&View Message";
            this.tspViewHL7Message.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tspViewHL7Message.Click += new System.EventHandler(this.tspViewHL7Message_Click);
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
            // panel5
            // 
            this.panel5.Controls.Add(this.cmbHL7Clients);
            this.panel5.Controls.Add(this.lblHL7Clients);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.chkTo);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.cmbStatus);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.ChkUseDateRange);
            this.panel5.Controls.Add(this.lblSearch);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.dtpToDate);
            this.panel5.Controls.Add(this.txtSearch);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.cmbServiceName);
            this.panel5.Controls.Add(this.dtpFromDate);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 56);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel5.Size = new System.Drawing.Size(1120, 135);
            this.panel5.TabIndex = 36;
            // 
            // cmbHL7Clients
            // 
            this.cmbHL7Clients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHL7Clients.ForeColor = System.Drawing.Color.Black;
            this.cmbHL7Clients.FormattingEnabled = true;
            this.cmbHL7Clients.Location = new System.Drawing.Point(546, 99);
            this.cmbHL7Clients.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbHL7Clients.Name = "cmbHL7Clients";
            this.cmbHL7Clients.Size = new System.Drawing.Size(204, 22);
            this.cmbHL7Clients.TabIndex = 78;
            // 
            // lblHL7Clients
            // 
            this.lblHL7Clients.AutoSize = true;
            this.lblHL7Clients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHL7Clients.Location = new System.Drawing.Point(468, 103);
            this.lblHL7Clients.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHL7Clients.Name = "lblHL7Clients";
            this.lblHL7Clients.Size = new System.Drawing.Size(75, 14);
            this.lblHL7Clients.TabIndex = 77;
            this.lblHL7Clients.Text = "HL7 Clients :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label25);
            this.panel9.Controls.Add(this.btnReport);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(916, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(200, 108);
            this.panel9.TabIndex = 76;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 108);
            this.label25.TabIndex = 39;
            this.label25.Text = "3";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(4, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1112, 1);
            this.label16.TabIndex = 39;
            this.label16.Text = "3";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(4, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1112, 25);
            this.panel6.TabIndex = 36;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1112, 24);
            this.label21.TabIndex = 0;
            this.label21.Text = "  Filter Criteria";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1112, 1);
            this.label17.TabIndex = 1;
            this.label17.Text = "3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 134);
            this.label12.TabIndex = 37;
            this.label12.Text = "3";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1116, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 134);
            this.label14.TabIndex = 38;
            this.label14.Text = "3";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1114, 1);
            this.label18.TabIndex = 40;
            this.label18.Text = "3";
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label22);
            this.panel7.Controls.Add(this.panel4);
            this.panel7.Controls.Add(this.label23);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 191);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel7.Size = new System.Drawing.Size(1120, 27);
            this.panel7.TabIndex = 37;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(1116, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 26);
            this.label24.TabIndex = 42;
            this.label24.Text = "3";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 26);
            this.label22.TabIndex = 0;
            this.label22.Text = "3";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(3, 26);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1114, 1);
            this.label23.TabIndex = 40;
            this.label23.Text = "3";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("miniToolStrip.BackgroundImage")));
            this.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.miniToolStrip.Location = new System.Drawing.Point(101, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(404, 59);
            this.miniToolStrip.TabIndex = 2;
            // 
            // frmInterfacesMessageErrorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1120, 896);
            this.Controls.Add(this.pnlregistration);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.tabCntFilterReport);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInterfacesMessageErrorReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface Reports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHL7_MessageQueue_Load);
            this.Shown += new System.EventHandler(this.frmHL7_MessageQueue_Shown);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Flex)).EndInit();
            this.pnlregistration.ResumeLayout(false);
            this.pnlregistration.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabCntFilterReport.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.tlpViewReport.ResumeLayout(false);
            this.tlpViewReport.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlGrid;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Panel pnlregistration;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.Label lblProcessInformation;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Flex;
        internal System.Windows.Forms.Panel pnlTop;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearch;
        internal System.Windows.Forms.Label lblSearch;
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
        private System.Windows.Forms.TabControl tabCntFilterReport;
        private System.Windows.Forms.TabPage FilterCriteria;
        internal System.Windows.Forms.ComboBox cmbStatus;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ChkUseDateRange;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button BtnFirst;
        private System.Windows.Forms.Button BtnLast;
        private System.Windows.Forms.Button BtnPrev;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnReport;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel9;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label label27;
        internal gloGlobal.gloToolStripIgnoreFocus miniToolStrip;
        internal System.Windows.Forms.ComboBox cmbHL7Clients;
        internal System.Windows.Forms.Label lblHL7Clients;
        internal System.Windows.Forms.ToolStripButton tspViewHL7Message;
    }
}