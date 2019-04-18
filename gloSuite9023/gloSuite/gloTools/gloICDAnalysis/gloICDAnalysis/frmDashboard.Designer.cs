namespace gloICDAnalysis
{
    partial class frmDashboard
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
                    if (dtpStartDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);
                        }
                        catch
                        {
                        }
                        dtpStartDate.Dispose();
                        dtpStartDate = null;
                    }
                }
                catch
                {
                }


                try
                {
                    if (dtpEndDate != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);
                        }
                        catch
                        {
                        }
                        dtpEndDate.Dispose();
                        dtpEndDate = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlDateRanges = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbByExam = new System.Windows.Forms.RadioButton();
            this.rbByClaim = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbICD10 = new System.Windows.Forms.RadioButton();
            this.rbICD9 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlICD9Usage = new System.Windows.Forms.Panel();
            this.c1UsageGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlPleasewait = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlCDuedate = new System.Windows.Forms.Panel();
            this.lblRecord = new System.Windows.Forms.Label();
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.slblDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.slblServerName = new System.Windows.Forms.ToolStripStatusLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tspButtons = new gloGlobal.gloToolStripIgnoreFocus();
            this.tslb_SelectAll = new System.Windows.Forms.ToolStripButton();
            this.tlBack = new System.Windows.Forms.ToolStripButton();
            this.tsSaveICD10Codes = new System.Windows.Forms.ToolStripButton();
            this.tsImportICD10Codes = new System.Windows.Forms.ToolStripButton();
            this.tsShowReport = new System.Windows.Forms.ToolStripButton();
            this.tsConnectDB = new System.Windows.Forms.ToolStripButton();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsExit = new System.Windows.Forms.ToolStripButton();
            this.pnlImport10Codes = new System.Windows.Forms.Panel();
            this.c1Import10Codes = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.C1SuperTooltip2 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlDateRanges.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlICD9Usage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1UsageGrid)).BeginInit();
            this.pnlPleasewait.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tspButtons.SuspendLayout();
            this.pnlImport10Codes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Import10Codes)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDateRanges
            // 
            this.pnlDateRanges.Controls.Add(this.panel2);
            this.pnlDateRanges.Controls.Add(this.groupBox2);
            this.pnlDateRanges.Controls.Add(this.label4);
            this.pnlDateRanges.Controls.Add(this.panel1);
            this.pnlDateRanges.Controls.Add(this.label3);
            this.pnlDateRanges.Controls.Add(this.label2);
            this.pnlDateRanges.Controls.Add(this.cmbProvider);
            this.pnlDateRanges.Controls.Add(this.label1);
            this.pnlDateRanges.Controls.Add(this.label9);
            this.pnlDateRanges.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDateRanges.Location = new System.Drawing.Point(0, 53);
            this.pnlDateRanges.Name = "pnlDateRanges";
            this.pnlDateRanges.Padding = new System.Windows.Forms.Padding(3);
            this.pnlDateRanges.Size = new System.Drawing.Size(1219, 117);
            this.pnlDateRanges.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbByExam);
            this.panel2.Controls.Add(this.rbByClaim);
            this.panel2.Location = new System.Drawing.Point(40, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 31);
            this.panel2.TabIndex = 8;
            // 
            // rbByExam
            // 
            this.rbByExam.AutoSize = true;
            this.rbByExam.Checked = true;
            this.rbByExam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbByExam.Location = new System.Drawing.Point(22, 7);
            this.rbByExam.Name = "rbByExam";
            this.rbByExam.Size = new System.Drawing.Size(71, 18);
            this.rbByExam.TabIndex = 0;
            this.rbByExam.TabStop = true;
            this.rbByExam.Text = "By Exam";
            this.rbByExam.UseVisualStyleBackColor = true;
            this.rbByExam.CheckedChanged += new System.EventHandler(this.rbByExam_CheckedChanged);
            // 
            // rbByClaim
            // 
            this.rbByClaim.AutoSize = true;
            this.rbByClaim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbByClaim.Location = new System.Drawing.Point(116, 7);
            this.rbByClaim.Name = "rbByClaim";
            this.rbByClaim.Size = new System.Drawing.Size(69, 18);
            this.rbByClaim.TabIndex = 1;
            this.rbByClaim.Text = "By Claim";
            this.rbByClaim.UseVisualStyleBackColor = true;
            this.rbByClaim.CheckedChanged += new System.EventHandler(this.rbByClaim_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpEndDate);
            this.groupBox2.Controls.Add(this.dtpStartDate);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(549, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 87);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date Range";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndDate.CustomFormat = " MM/dd/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(136, 54);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(121, 22);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartDate.CustomFormat = " MM/dd/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(136, 24);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(121, 22);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(61, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 14);
            this.label10.TabIndex = 4;
            this.label10.Text = "Start Date :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(67, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 14);
            this.label11.TabIndex = 4;
            this.label11.Text = "End Date :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1215, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 109);
            this.label4.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbICD10);
            this.panel1.Controls.Add(this.rbICD9);
            this.panel1.Location = new System.Drawing.Point(252, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 31);
            this.panel1.TabIndex = 5;
            // 
            // rbICD10
            // 
            this.rbICD10.AutoSize = true;
            this.rbICD10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD10.Location = new System.Drawing.Point(105, 6);
            this.rbICD10.Name = "rbICD10";
            this.rbICD10.Size = new System.Drawing.Size(58, 18);
            this.rbICD10.TabIndex = 6;
            this.rbICD10.Text = "ICD10";
            this.rbICD10.UseVisualStyleBackColor = true;
            this.rbICD10.CheckedChanged += new System.EventHandler(this.rbICD10_CheckedChanged);
            // 
            // rbICD9
            // 
            this.rbICD9.AutoSize = true;
            this.rbICD9.Checked = true;
            this.rbICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD9.Location = new System.Drawing.Point(28, 6);
            this.rbICD9.Name = "rbICD9";
            this.rbICD9.Size = new System.Drawing.Size(51, 18);
            this.rbICD9.TabIndex = 5;
            this.rbICD9.TabStop = true;
            this.rbICD9.Text = "ICD9";
            this.rbICD9.UseVisualStyleBackColor = true;
            this.rbICD9.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 109);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1213, 1);
            this.label2.TabIndex = 1;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(75, 71);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(438, 22);
            this.cmbProvider.TabIndex = 2;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1213, 1);
            this.label1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "Provider :";
            // 
            // pnlICD9Usage
            // 
            this.pnlICD9Usage.Controls.Add(this.c1UsageGrid);
            this.pnlICD9Usage.Controls.Add(this.label5);
            this.pnlICD9Usage.Controls.Add(this.label6);
            this.pnlICD9Usage.Controls.Add(this.label7);
            this.pnlICD9Usage.Controls.Add(this.label8);
            this.pnlICD9Usage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlICD9Usage.Location = new System.Drawing.Point(0, 170);
            this.pnlICD9Usage.Name = "pnlICD9Usage";
            this.pnlICD9Usage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlICD9Usage.Size = new System.Drawing.Size(1219, 328);
            this.pnlICD9Usage.TabIndex = 3;
            // 
            // c1UsageGrid
            // 
            this.c1UsageGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1UsageGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1UsageGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1UsageGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1UsageGrid.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1UsageGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1UsageGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1UsageGrid.Location = new System.Drawing.Point(4, 1);
            this.c1UsageGrid.Name = "c1UsageGrid";
            this.c1UsageGrid.Rows.Count = 1;
            this.c1UsageGrid.Rows.DefaultSize = 21;
            this.c1UsageGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1UsageGrid.Size = new System.Drawing.Size(1211, 326);
            this.c1UsageGrid.StyleInfo = resources.GetString("c1UsageGrid.StyleInfo");
            this.c1UsageGrid.TabIndex = 239;
            this.C1SuperTooltip2.SetToolTip(this.c1UsageGrid, "False");
            this.c1UsageGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1UsageGrid_MouseMove);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1215, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 326);
            this.label5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 326);
            this.label6.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1213, 1);
            this.label7.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1213, 1);
            this.label8.TabIndex = 0;
            // 
            // pnlPleasewait
            // 
            this.pnlPleasewait.Controls.Add(this.label45);
            this.pnlPleasewait.Controls.Add(this.label44);
            this.pnlPleasewait.Controls.Add(this.label43);
            this.pnlPleasewait.Controls.Add(this.label42);
            this.pnlPleasewait.Controls.Add(this.label41);
            this.pnlPleasewait.Controls.Add(this.label25);
            this.pnlPleasewait.Controls.Add(this.pnlCDuedate);
            this.pnlPleasewait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPleasewait.Location = new System.Drawing.Point(0, 170);
            this.pnlPleasewait.Name = "pnlPleasewait";
            this.pnlPleasewait.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPleasewait.Size = new System.Drawing.Size(1219, 328);
            this.pnlPleasewait.TabIndex = 34;
            this.pnlPleasewait.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.White;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Baskerville Old Face", 48F);
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(4, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1211, 326);
            this.label45.TabIndex = 238;
            this.label45.Text = "Please wait...";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Left;
            this.label44.Location = new System.Drawing.Point(3, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 326);
            this.label44.TabIndex = 36;
            this.label44.Text = "label44";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Location = new System.Drawing.Point(1215, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 326);
            this.label43.TabIndex = 35;
            this.label43.Text = "label43";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(3, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1213, 1);
            this.label42.TabIndex = 34;
            this.label42.Text = "label42";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 327);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1213, 1);
            this.label41.TabIndex = 33;
            this.label41.Text = "label41";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(52, 90);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(108, 14);
            this.label25.TabIndex = 234;
            this.label25.Text = "Transaction Date :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label25.Visible = false;
            // 
            // pnlCDuedate
            // 
            this.pnlCDuedate.Location = new System.Drawing.Point(615, 57);
            this.pnlCDuedate.Name = "pnlCDuedate";
            this.pnlCDuedate.Size = new System.Drawing.Size(418, 51);
            this.pnlCDuedate.TabIndex = 236;
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.BackColor = System.Drawing.Color.Transparent;
            this.lblRecord.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(7, 7);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblRecord.Size = new System.Drawing.Size(46, 16);
            this.lblRecord.TabIndex = 35;
            this.lblRecord.Text = "Total :";
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.Controls.Add(this.lblRecord);
            this.pnlStatusBar.Controls.Add(this.statusStrip);
            this.pnlStatusBar.Controls.Add(this.label16);
            this.pnlStatusBar.Controls.Add(this.label17);
            this.pnlStatusBar.Controls.Add(this.label18);
            this.pnlStatusBar.Controls.Add(this.label19);
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 498);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Padding = new System.Windows.Forms.Padding(3);
            this.pnlStatusBar.Size = new System.Drawing.Size(1219, 30);
            this.pnlStatusBar.TabIndex = 36;
            // 
            // statusStrip
            // 
            this.statusStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("statusStrip.BackgroundImage")));
            this.statusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblDatabase,
            this.slblServerName});
            this.statusStrip.Location = new System.Drawing.Point(4, 4);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(1211, 22);
            this.statusStrip.TabIndex = 37;
            this.statusStrip.Text = "StatusStrip";
            // 
            // slblDatabase
            // 
            this.slblDatabase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slblDatabase.Image = ((System.Drawing.Image)(resources.GetObject("slblDatabase.Image")));
            this.slblDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.slblDatabase.Name = "slblDatabase";
            this.slblDatabase.Size = new System.Drawing.Size(80, 17);
            this.slblDatabase.Text = "Database";
            this.slblDatabase.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // slblServerName
            // 
            this.slblServerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.slblServerName.Image = ((System.Drawing.Image)(resources.GetObject("slblServerName.Image")));
            this.slblServerName.Name = "slblServerName";
            this.slblServerName.Size = new System.Drawing.Size(62, 17);
            this.slblServerName.Text = "Server";
            this.slblServerName.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(1215, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 22);
            this.label16.TabIndex = 3;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(3, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 22);
            this.label17.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(3, 26);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1213, 1);
            this.label18.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(3, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1213, 1);
            this.label19.TabIndex = 0;
            // 
            // tspButtons
            // 
            this.tspButtons.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tspButtons.BackgroundImage")));
            this.tspButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tspButtons.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspButtons.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tspButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslb_SelectAll,
            this.tlBack,
            this.tsSaveICD10Codes,
            this.tsImportICD10Codes,
            this.tsShowReport,
            this.tsConnectDB,
            this.tsRefresh,
            this.btnPrint,
            this.tsExit});
            this.tspButtons.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tspButtons.Location = new System.Drawing.Point(0, 0);
            this.tspButtons.Name = "tspButtons";
            this.tspButtons.Size = new System.Drawing.Size(1219, 53);
            this.tspButtons.TabIndex = 6;
            this.tspButtons.Text = "ToolStrip";
            // 
            // tslb_SelectAll
            // 
            this.tslb_SelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslb_SelectAll.Image = global::gloICDAnalysis.Properties.Resources.Select_All;
            this.tslb_SelectAll.ImageTransparentColor = System.Drawing.Color.Black;
            this.tslb_SelectAll.Name = "tslb_SelectAll";
            this.tslb_SelectAll.Size = new System.Drawing.Size(63, 49);
            this.tslb_SelectAll.Text = "Select All";
            this.tslb_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tslb_SelectAll.Click += new System.EventHandler(this.tslb_SelectAll_Click);
            // 
            // tlBack
            // 
            this.tlBack.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlBack.Image = ((System.Drawing.Image)(resources.GetObject("tlBack.Image")));
            this.tlBack.ImageTransparentColor = System.Drawing.Color.Black;
            this.tlBack.Name = "tlBack";
            this.tlBack.Size = new System.Drawing.Size(38, 49);
            this.tlBack.Text = "Back";
            this.tlBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlBack.Visible = false;
            this.tlBack.Click += new System.EventHandler(this.tlBack_Click);
            // 
            // tsSaveICD10Codes
            // 
            this.tsSaveICD10Codes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsSaveICD10Codes.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveICD10Codes.Image")));
            this.tsSaveICD10Codes.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsSaveICD10Codes.Name = "tsSaveICD10Codes";
            this.tsSaveICD10Codes.Size = new System.Drawing.Size(81, 49);
            this.tsSaveICD10Codes.Text = "Save ICD-10";
            this.tsSaveICD10Codes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSaveICD10Codes.Visible = false;
            this.tsSaveICD10Codes.Click += new System.EventHandler(this.tsSaveICD10Codes_Click);
            // 
            // tsImportICD10Codes
            // 
            this.tsImportICD10Codes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsImportICD10Codes.Image = ((System.Drawing.Image)(resources.GetObject("tsImportICD10Codes.Image")));
            this.tsImportICD10Codes.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsImportICD10Codes.Name = "tsImportICD10Codes";
            this.tsImportICD10Codes.Size = new System.Drawing.Size(93, 49);
            this.tsImportICD10Codes.Text = "Import ICD-10";
            this.tsImportICD10Codes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsImportICD10Codes.Click += new System.EventHandler(this.tsImportICD10Codes_Click);
            // 
            // tsShowReport
            // 
            this.tsShowReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsShowReport.Image = ((System.Drawing.Image)(resources.GetObject("tsShowReport.Image")));
            this.tsShowReport.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsShowReport.Name = "tsShowReport";
            this.tsShowReport.Size = new System.Drawing.Size(83, 49);
            this.tsShowReport.Text = "Show ICD-10";
            this.tsShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsShowReport.Click += new System.EventHandler(this.tsShowReport_Click);
            // 
            // tsConnectDB
            // 
            this.tsConnectDB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsConnectDB.Image = ((System.Drawing.Image)(resources.GetObject("tsConnectDB.Image")));
            this.tsConnectDB.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsConnectDB.Name = "tsConnectDB";
            this.tsConnectDB.Size = new System.Drawing.Size(57, 49);
            this.tsConnectDB.Text = "Connect";
            this.tsConnectDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsConnectDB.Click += new System.EventHandler(this.tsConnectDB_Click);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsRefresh.Image")));
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(55, 49);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(41, 50);
            this.btnPrint.Text = "&Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tsExit
            // 
            this.tsExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsExit.Image = ((System.Drawing.Image)(resources.GetObject("tsExit.Image")));
            this.tsExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(43, 50);
            this.tsExit.Text = "&Close";
            this.tsExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // pnlImport10Codes
            // 
            this.pnlImport10Codes.Controls.Add(this.c1Import10Codes);
            this.pnlImport10Codes.Controls.Add(this.label12);
            this.pnlImport10Codes.Controls.Add(this.label13);
            this.pnlImport10Codes.Controls.Add(this.label14);
            this.pnlImport10Codes.Controls.Add(this.label15);
            this.pnlImport10Codes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImport10Codes.Location = new System.Drawing.Point(0, 170);
            this.pnlImport10Codes.Name = "pnlImport10Codes";
            this.pnlImport10Codes.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlImport10Codes.Size = new System.Drawing.Size(1219, 328);
            this.pnlImport10Codes.TabIndex = 241;
            this.pnlImport10Codes.Visible = false;
            // 
            // c1Import10Codes
            // 
            this.c1Import10Codes.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Import10Codes.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Import10Codes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Import10Codes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Import10Codes.ColumnInfo = "5,1,0,0,0,105,Columns:";
            this.c1Import10Codes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Import10Codes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Import10Codes.Location = new System.Drawing.Point(4, 1);
            this.c1Import10Codes.Name = "c1Import10Codes";
            this.c1Import10Codes.Rows.Count = 1;
            this.c1Import10Codes.Rows.DefaultSize = 21;
            this.c1Import10Codes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Import10Codes.Size = new System.Drawing.Size(1211, 326);
            this.c1Import10Codes.StyleInfo = resources.GetString("c1Import10Codes.StyleInfo");
            this.c1Import10Codes.TabIndex = 239;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(1215, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 326);
            this.label12.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 326);
            this.label13.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(3, 327);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1213, 1);
            this.label14.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1213, 1);
            this.label15.TabIndex = 0;
            // 
            // C1SuperTooltip2
            // 
            this.C1SuperTooltip2.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1219, 528);
            this.Controls.Add(this.pnlPleasewait);
            this.Controls.Add(this.pnlICD9Usage);
            this.Controls.Add(this.pnlImport10Codes);
            this.Controls.Add(this.pnlStatusBar);
            this.Controls.Add(this.pnlDateRanges);
            this.Controls.Add(this.tspButtons);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICD-9 Code Usage Report with ICD-10 Mapping Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDashboard_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.frmDashboard_Shown);
            this.pnlDateRanges.ResumeLayout(false);
            this.pnlDateRanges.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlICD9Usage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1UsageGrid)).EndInit();
            this.pnlPleasewait.ResumeLayout(false);
            this.pnlStatusBar.ResumeLayout(false);
            this.pnlStatusBar.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tspButtons.ResumeLayout(false);
            this.tspButtons.PerformLayout();
            this.pnlImport10Codes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Import10Codes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tspButtons;
        //private System.Windows.Forms.ToolStripStatusLabel slblServerName;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripButton tsShowReport;
        private System.Windows.Forms.Panel pnlDateRanges;
        private System.Windows.Forms.RadioButton rbByExam;
        private System.Windows.Forms.RadioButton rbByClaim;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlICD9Usage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ToolStripButton tsConnectDB;
        //private System.Windows.Forms.ToolStripStatusLabel slblDatabase;
        private System.Windows.Forms.Panel pnlPleasewait;
        internal System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel pnlCDuedate;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolStripButton tslb_SelectAll;
        private C1.Win.C1FlexGrid.C1FlexGrid c1UsageGrid;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel slblServerName;
        private System.Windows.Forms.ToolStripStatusLabel slblDatabase;
        private System.Windows.Forms.ToolStripButton tsRefresh;
        private System.Windows.Forms.ToolStripButton tlBack;
        private System.Windows.Forms.ToolStripButton tsSaveICD10Codes;
        private System.Windows.Forms.ToolStripButton tsImportICD10Codes;
        private System.Windows.Forms.Panel pnlImport10Codes;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Import10Codes;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rbICD10;
        private System.Windows.Forms.RadioButton rbICD9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripButton btnPrint;
        internal System.Windows.Forms.ToolStripButton tsExit;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip2;
    }
}



