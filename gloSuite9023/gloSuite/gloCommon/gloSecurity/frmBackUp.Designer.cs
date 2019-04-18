namespace gloSecurity
{
    partial class frmBackUp
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { DTPEndDate, DTPStartDate, tmScheduleTime };
            System.Windows.Forms.Control[] cntControls  = { DTPEndDate, DTPStartDate, tmScheduleTime };
            if (disposing && (components != null))
            {
               
                try
                {
                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
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
                try
                {
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (folderBrowserDialog1 != null)
                    {

                        folderBrowserDialog1.Dispose();
                        folderBrowserDialog1 = null;
                    }
                }
                catch
                {
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackUp));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pnlBackupType = new System.Windows.Forms.Panel();
            this.optDifferential = new System.Windows.Forms.RadioButton();
            this.optComplete = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnlSelectNS = new System.Windows.Forms.Panel();
            this.optSchedule = new System.Windows.Forms.RadioButton();
            this.optNow = new System.Windows.Forms.RadioButton();
            this.txtBackUpFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pnlScheduling = new System.Windows.Forms.Panel();
            this.lblDailyEvery = new System.Windows.Forms.Label();
            this.lblDaily = new System.Windows.Forms.Label();
            this.numWeekFreq = new System.Windows.Forms.NumericUpDown();
            this.pnlTimeInfo = new System.Windows.Forms.Panel();
            this.rdBtnDateEnd = new System.Windows.Forms.RadioButton();
            this.rdBtnNoEndDate = new System.Windows.Forms.RadioButton();
            this.tmScheduleTime = new System.Windows.Forms.DateTimePicker();
            this.DTPEndDate = new System.Windows.Forms.DateTimePicker();
            this.DTPStartDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlStartEndDate = new System.Windows.Forms.Panel();
            this.rdBtnMonthly = new System.Windows.Forms.RadioButton();
            this.rdBtnWeekly = new System.Windows.Forms.RadioButton();
            this.rdbtnDaily = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlScheduleHeader = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtScheduleName = new System.Windows.Forms.TextBox();
            this.pnlMonthly = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.numMonth = new System.Windows.Forms.NumericUpDown();
            this.lblJobOccursValue = new System.Windows.Forms.Label();
            this.numDayOfMonth = new System.Windows.Forms.NumericUpDown();
            this.lblJobOccursHead = new System.Windows.Forms.Label();
            this.pnlWeekly = new System.Windows.Forms.Panel();
            this.chkEveryDay = new System.Windows.Forms.CheckBox();
            this.chkSun = new System.Windows.Forms.CheckBox();
            this.chkSat = new System.Windows.Forms.CheckBox();
            this.chkFri = new System.Windows.Forms.CheckBox();
            this.chkThu = new System.Windows.Forms.CheckBox();
            this.chkWed = new System.Windows.Forms.CheckBox();
            this.chkTue = new System.Windows.Forms.CheckBox();
            this.chkMon = new System.Windows.Forms.CheckBox();
            this.pnl_Toolstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.pnlBackupType.SuspendLayout();
            this.pnlSelectNS.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlScheduling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekFreq)).BeginInit();
            this.pnlTimeInfo.SuspendLayout();
            this.pnlScheduleHeader.SuspendLayout();
            this.pnlMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDayOfMonth)).BeginInit();
            this.pnlWeekly.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(535, 54);
            this.pnl_Toolstrip.TabIndex = 21;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(535, 54);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(36, 49);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = " OK";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(46, 49);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " Cancel";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel2.BackgroundImage")));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.lblStatus);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel2.Location = new System.Drawing.Point(0, 486);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(535, 26);
            this.Panel2.TabIndex = 22;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(3, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(323, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Visible = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(12, 58);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(91, 13);
            this.Label3.TabIndex = 9;
            this.Label3.Text = "Backup &Location :";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtLocation.Enabled = false;
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(118, 51);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(327, 21);
            this.txtLocation.TabIndex = 10;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(448, 51);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 20);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pnlBackupType
            // 
            this.pnlBackupType.Controls.Add(this.optDifferential);
            this.pnlBackupType.Controls.Add(this.optComplete);
            this.pnlBackupType.Location = new System.Drawing.Point(119, 9);
            this.pnlBackupType.Name = "pnlBackupType";
            this.pnlBackupType.Size = new System.Drawing.Size(276, 23);
            this.pnlBackupType.TabIndex = 14;
            // 
            // optDifferential
            // 
            this.optDifferential.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDifferential.Location = new System.Drawing.Point(95, 3);
            this.optDifferential.Name = "optDifferential";
            this.optDifferential.Size = new System.Drawing.Size(129, 19);
            this.optDifferential.TabIndex = 4;
            this.optDifferential.Text = "Differential";
            // 
            // optComplete
            // 
            this.optComplete.Checked = true;
            this.optComplete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optComplete.Location = new System.Drawing.Point(4, 3);
            this.optComplete.Name = "optComplete";
            this.optComplete.Size = new System.Drawing.Size(119, 19);
            this.optComplete.TabIndex = 3;
            this.optComplete.TabStop = true;
            this.optComplete.Text = "Complete";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(29, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Backup &Type :";
            // 
            // pnlSelectNS
            // 
            this.pnlSelectNS.Controls.Add(this.optSchedule);
            this.pnlSelectNS.Controls.Add(this.optNow);
            this.pnlSelectNS.Location = new System.Drawing.Point(119, 124);
            this.pnlSelectNS.Name = "pnlSelectNS";
            this.pnlSelectNS.Size = new System.Drawing.Size(282, 26);
            this.pnlSelectNS.TabIndex = 16;
            // 
            // optSchedule
            // 
            this.optSchedule.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSchedule.Location = new System.Drawing.Point(76, 4);
            this.optSchedule.Name = "optSchedule";
            this.optSchedule.Size = new System.Drawing.Size(115, 17);
            this.optSchedule.TabIndex = 15;
            this.optSchedule.Text = "Schedule";
            this.optSchedule.CheckedChanged += new System.EventHandler(this.optSchedule_CheckedChanged);
            // 
            // optNow
            // 
            this.optNow.Checked = true;
            this.optNow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNow.Location = new System.Drawing.Point(2, 4);
            this.optNow.Name = "optNow";
            this.optNow.Size = new System.Drawing.Size(68, 18);
            this.optNow.TabIndex = 14;
            this.optNow.TabStop = true;
            this.optNow.Text = "Now";
            this.optNow.CheckedChanged += new System.EventHandler(this.optNow_CheckedChanged);
            // 
            // txtBackUpFileName
            // 
            this.txtBackUpFileName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackUpFileName.Location = new System.Drawing.Point(118, 89);
            this.txtBackUpFileName.Name = "txtBackUpFileName";
            this.txtBackUpFileName.Size = new System.Drawing.Size(180, 21);
            this.txtBackUpFileName.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "BackUp FileName :";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.txtBackUpFileName);
            this.Panel1.Controls.Add(this.pnlSelectNS);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.pnlBackupType);
            this.Panel1.Controls.Add(this.btnBrowse);
            this.Panel1.Controls.Add(this.txtLocation);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Location = new System.Drawing.Point(0, 57);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(569, 168);
            this.Panel1.TabIndex = 5;
            // 
            // pnlScheduling
            // 
            this.pnlScheduling.Controls.Add(this.lblDailyEvery);
            this.pnlScheduling.Controls.Add(this.lblDaily);
            this.pnlScheduling.Controls.Add(this.numWeekFreq);
            this.pnlScheduling.Controls.Add(this.pnlTimeInfo);
            this.pnlScheduling.Controls.Add(this.rdBtnMonthly);
            this.pnlScheduling.Controls.Add(this.rdBtnWeekly);
            this.pnlScheduling.Controls.Add(this.rdbtnDaily);
            this.pnlScheduling.Controls.Add(this.label2);
            this.pnlScheduling.Controls.Add(this.pnlScheduleHeader);
            this.pnlScheduling.Controls.Add(this.txtScheduleName);
            this.pnlScheduling.Controls.Add(this.pnlMonthly);
            this.pnlScheduling.Controls.Add(this.pnlWeekly);
            this.pnlScheduling.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlScheduling.Location = new System.Drawing.Point(0, 225);
            this.pnlScheduling.Name = "pnlScheduling";
            this.pnlScheduling.Size = new System.Drawing.Size(535, 261);
            this.pnlScheduling.TabIndex = 23;
            this.pnlScheduling.Visible = false;
            // 
            // lblDailyEvery
            // 
            this.lblDailyEvery.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDailyEvery.Location = new System.Drawing.Point(462, 76);
            this.lblDailyEvery.Name = "lblDailyEvery";
            this.lblDailyEvery.Size = new System.Drawing.Size(62, 14);
            this.lblDailyEvery.TabIndex = 27;
            this.lblDailyEvery.Text = "Every :";
            // 
            // lblDaily
            // 
            this.lblDaily.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaily.Location = new System.Drawing.Point(462, 118);
            this.lblDaily.Name = "lblDaily";
            this.lblDaily.Size = new System.Drawing.Size(62, 16);
            this.lblDaily.TabIndex = 26;
            this.lblDaily.Text = "day(s)";
            // 
            // numWeekFreq
            // 
            this.numWeekFreq.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeekFreq.Location = new System.Drawing.Point(465, 93);
            this.numWeekFreq.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numWeekFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWeekFreq.Name = "numWeekFreq";
            this.numWeekFreq.Size = new System.Drawing.Size(40, 22);
            this.numWeekFreq.TabIndex = 21;
            this.numWeekFreq.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pnlTimeInfo
            // 
            this.pnlTimeInfo.Controls.Add(this.rdBtnDateEnd);
            this.pnlTimeInfo.Controls.Add(this.rdBtnNoEndDate);
            this.pnlTimeInfo.Controls.Add(this.tmScheduleTime);
            this.pnlTimeInfo.Controls.Add(this.DTPEndDate);
            this.pnlTimeInfo.Controls.Add(this.DTPStartDate);
            this.pnlTimeInfo.Controls.Add(this.label8);
            this.pnlTimeInfo.Controls.Add(this.label6);
            this.pnlTimeInfo.Controls.Add(this.label7);
            this.pnlTimeInfo.Controls.Add(this.pnlStartEndDate);
            this.pnlTimeInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimeInfo.Location = new System.Drawing.Point(0, 187);
            this.pnlTimeInfo.Name = "pnlTimeInfo";
            this.pnlTimeInfo.Size = new System.Drawing.Size(535, 74);
            this.pnlTimeInfo.TabIndex = 23;
            // 
            // rdBtnDateEnd
            // 
            this.rdBtnDateEnd.AutoSize = true;
            this.rdBtnDateEnd.Checked = true;
            this.rdBtnDateEnd.Location = new System.Drawing.Point(300, 16);
            this.rdBtnDateEnd.Name = "rdBtnDateEnd";
            this.rdBtnDateEnd.Size = new System.Drawing.Size(14, 13);
            this.rdBtnDateEnd.TabIndex = 18;
            this.rdBtnDateEnd.TabStop = true;
            this.rdBtnDateEnd.UseVisualStyleBackColor = true;
            this.rdBtnDateEnd.CheckedChanged += new System.EventHandler(this.rdBtnWeekly_CheckedChanged);
            // 
            // rdBtnNoEndDate
            // 
            this.rdBtnNoEndDate.AutoSize = true;
            this.rdBtnNoEndDate.Location = new System.Drawing.Point(300, 42);
            this.rdBtnNoEndDate.Name = "rdBtnNoEndDate";
            this.rdBtnNoEndDate.Size = new System.Drawing.Size(85, 17);
            this.rdBtnNoEndDate.TabIndex = 17;
            this.rdBtnNoEndDate.Text = "No End Date";
            this.rdBtnNoEndDate.UseVisualStyleBackColor = true;
            this.rdBtnNoEndDate.CheckedChanged += new System.EventHandler(this.rdBtnWeekly_CheckedChanged);
            // 
            // tmScheduleTime
            // 
            this.tmScheduleTime.CustomFormat = "HHmmss";
            this.tmScheduleTime.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tmScheduleTime.Location = new System.Drawing.Point(101, 42);
            this.tmScheduleTime.Name = "tmScheduleTime";
            this.tmScheduleTime.ShowUpDown = true;
            this.tmScheduleTime.Size = new System.Drawing.Size(110, 22);
            this.tmScheduleTime.TabIndex = 16;
            // 
            // DTPEndDate
            // 
            this.DTPEndDate.CustomFormat = "yyyyMMdd";
            this.DTPEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPEndDate.Location = new System.Drawing.Point(372, 12);
            this.DTPEndDate.Name = "DTPEndDate";
            this.DTPEndDate.Size = new System.Drawing.Size(163, 21);
            this.DTPEndDate.TabIndex = 14;
            // 
            // DTPStartDate
            // 
            this.DTPStartDate.CustomFormat = "yyyyMMdd";
            this.DTPStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPStartDate.Location = new System.Drawing.Point(101, 12);
            this.DTPStartDate.Name = "DTPStartDate";
            this.DTPStartDate.Size = new System.Drawing.Size(174, 21);
            this.DTPStartDate.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Shedule Time\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "StartDate";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(316, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "End Date";
            // 
            // pnlStartEndDate
            // 
            this.pnlStartEndDate.Location = new System.Drawing.Point(300, 8);
            this.pnlStartEndDate.Name = "pnlStartEndDate";
            this.pnlStartEndDate.Size = new System.Drawing.Size(14, 56);
            this.pnlStartEndDate.TabIndex = 19;
            // 
            // rdBtnMonthly
            // 
            this.rdBtnMonthly.AutoSize = true;
            this.rdBtnMonthly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnMonthly.Location = new System.Drawing.Point(25, 148);
            this.rdBtnMonthly.Name = "rdBtnMonthly";
            this.rdBtnMonthly.Size = new System.Drawing.Size(71, 17);
            this.rdBtnMonthly.TabIndex = 22;
            this.rdBtnMonthly.Text = "Monthly";
            this.rdBtnMonthly.UseVisualStyleBackColor = true;
            this.rdBtnMonthly.CheckedChanged += new System.EventHandler(this.rdBtnWeekly_CheckedChanged);
            // 
            // rdBtnWeekly
            // 
            this.rdBtnWeekly.AutoSize = true;
            this.rdBtnWeekly.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnWeekly.Location = new System.Drawing.Point(25, 116);
            this.rdBtnWeekly.Name = "rdBtnWeekly";
            this.rdBtnWeekly.Size = new System.Drawing.Size(67, 17);
            this.rdBtnWeekly.TabIndex = 21;
            this.rdBtnWeekly.Text = "Weekly";
            this.rdBtnWeekly.UseVisualStyleBackColor = true;
            this.rdBtnWeekly.CheckedChanged += new System.EventHandler(this.rdBtnWeekly_CheckedChanged);
            // 
            // rdbtnDaily
            // 
            this.rdbtnDaily.AutoSize = true;
            this.rdbtnDaily.Checked = true;
            this.rdbtnDaily.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnDaily.Location = new System.Drawing.Point(25, 84);
            this.rdbtnDaily.Name = "rdbtnDaily";
            this.rdbtnDaily.Size = new System.Drawing.Size(53, 17);
            this.rdbtnDaily.TabIndex = 20;
            this.rdbtnDaily.TabStop = true;
            this.rdbtnDaily.Text = "Daily";
            this.rdbtnDaily.UseVisualStyleBackColor = true;
            this.rdbtnDaily.CheckedChanged += new System.EventHandler(this.rdBtnWeekly_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Schedule Name";
            // 
            // pnlScheduleHeader
            // 
            this.pnlScheduleHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlScheduleHeader.Controls.Add(this.label5);
            this.pnlScheduleHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScheduleHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlScheduleHeader.Name = "pnlScheduleHeader";
            this.pnlScheduleHeader.Size = new System.Drawing.Size(535, 32);
            this.pnlScheduleHeader.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Scheduling Information";
            // 
            // txtScheduleName
            // 
            this.txtScheduleName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScheduleName.Location = new System.Drawing.Point(116, 41);
            this.txtScheduleName.Name = "txtScheduleName";
            this.txtScheduleName.Size = new System.Drawing.Size(249, 21);
            this.txtScheduleName.TabIndex = 18;
            // 
            // pnlMonthly
            // 
            this.pnlMonthly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMonthly.Controls.Add(this.lblMonth);
            this.pnlMonthly.Controls.Add(this.numMonth);
            this.pnlMonthly.Controls.Add(this.lblJobOccursValue);
            this.pnlMonthly.Controls.Add(this.numDayOfMonth);
            this.pnlMonthly.Controls.Add(this.lblJobOccursHead);
            this.pnlMonthly.Location = new System.Drawing.Point(116, 82);
            this.pnlMonthly.Name = "pnlMonthly";
            this.pnlMonthly.Size = new System.Drawing.Size(340, 87);
            this.pnlMonthly.TabIndex = 25;
            this.pnlMonthly.Visible = false;
            // 
            // lblMonth
            // 
            this.lblMonth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(244, 33);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(64, 16);
            this.lblMonth.TabIndex = 16;
            this.lblMonth.Text = "month(s)";
            this.lblMonth.Visible = false;
            // 
            // numMonth
            // 
            this.numMonth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMonth.Location = new System.Drawing.Point(194, 31);
            this.numMonth.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonth.Name = "numMonth";
            this.numMonth.Size = new System.Drawing.Size(44, 22);
            this.numMonth.TabIndex = 15;
            this.numMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblJobOccursValue
            // 
            this.lblJobOccursValue.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobOccursValue.Location = new System.Drawing.Point(131, 32);
            this.lblJobOccursValue.Name = "lblJobOccursValue";
            this.lblJobOccursValue.Size = new System.Drawing.Size(66, 14);
            this.lblJobOccursValue.TabIndex = 14;
            this.lblJobOccursValue.Text = "of every";
            // 
            // numDayOfMonth
            // 
            this.numDayOfMonth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDayOfMonth.Location = new System.Drawing.Point(57, 31);
            this.numDayOfMonth.Maximum = new decimal(new int[] {
            366,
            0,
            0,
            0});
            this.numDayOfMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDayOfMonth.Name = "numDayOfMonth";
            this.numDayOfMonth.Size = new System.Drawing.Size(58, 22);
            this.numDayOfMonth.TabIndex = 13;
            this.numDayOfMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblJobOccursHead
            // 
            this.lblJobOccursHead.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobOccursHead.Location = new System.Drawing.Point(13, 31);
            this.lblJobOccursHead.Name = "lblJobOccursHead";
            this.lblJobOccursHead.Size = new System.Drawing.Size(66, 16);
            this.lblJobOccursHead.TabIndex = 12;
            this.lblJobOccursHead.Text = "Day :";
            // 
            // pnlWeekly
            // 
            this.pnlWeekly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWeekly.Controls.Add(this.chkEveryDay);
            this.pnlWeekly.Controls.Add(this.chkSun);
            this.pnlWeekly.Controls.Add(this.chkSat);
            this.pnlWeekly.Controls.Add(this.chkFri);
            this.pnlWeekly.Controls.Add(this.chkThu);
            this.pnlWeekly.Controls.Add(this.chkWed);
            this.pnlWeekly.Controls.Add(this.chkTue);
            this.pnlWeekly.Controls.Add(this.chkMon);
            this.pnlWeekly.Location = new System.Drawing.Point(116, 82);
            this.pnlWeekly.Name = "pnlWeekly";
            this.pnlWeekly.Size = new System.Drawing.Size(340, 87);
            this.pnlWeekly.TabIndex = 24;
            this.pnlWeekly.Visible = false;
            // 
            // chkEveryDay
            // 
            this.chkEveryDay.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEveryDay.Location = new System.Drawing.Point(98, 59);
            this.chkEveryDay.Name = "chkEveryDay";
            this.chkEveryDay.Size = new System.Drawing.Size(152, 18);
            this.chkEveryDay.TabIndex = 20;
            this.chkEveryDay.Tag = "ALL";
            this.chkEveryDay.Text = "Each Day of Week";
            // 
            // chkSun
            // 
            this.chkSun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSun.Location = new System.Drawing.Point(36, 59);
            this.chkSun.Name = "chkSun";
            this.chkSun.Size = new System.Drawing.Size(50, 18);
            this.chkSun.TabIndex = 19;
            this.chkSun.Tag = "SUN";
            this.chkSun.Text = "Sun";
            // 
            // chkSat
            // 
            this.chkSat.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSat.Location = new System.Drawing.Point(144, 35);
            this.chkSat.Name = "chkSat";
            this.chkSat.Size = new System.Drawing.Size(50, 18);
            this.chkSat.TabIndex = 18;
            this.chkSat.Tag = "SAT";
            this.chkSat.Text = "Sat";
            // 
            // chkFri
            // 
            this.chkFri.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFri.Location = new System.Drawing.Point(98, 35);
            this.chkFri.Name = "chkFri";
            this.chkFri.Size = new System.Drawing.Size(50, 18);
            this.chkFri.TabIndex = 17;
            this.chkFri.Tag = "FRI";
            this.chkFri.Text = "Fri";
            // 
            // chkThu
            // 
            this.chkThu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThu.Location = new System.Drawing.Point(36, 35);
            this.chkThu.Name = "chkThu";
            this.chkThu.Size = new System.Drawing.Size(50, 18);
            this.chkThu.TabIndex = 16;
            this.chkThu.Tag = "THU";
            this.chkThu.Text = "Thu";
            // 
            // chkWed
            // 
            this.chkWed.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWed.Location = new System.Drawing.Point(144, 11);
            this.chkWed.Name = "chkWed";
            this.chkWed.Size = new System.Drawing.Size(62, 18);
            this.chkWed.TabIndex = 15;
            this.chkWed.Tag = "WED";
            this.chkWed.Text = "Wed";
            // 
            // chkTue
            // 
            this.chkTue.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTue.Location = new System.Drawing.Point(98, 11);
            this.chkTue.Name = "chkTue";
            this.chkTue.Size = new System.Drawing.Size(50, 18);
            this.chkTue.TabIndex = 14;
            this.chkTue.Tag = "TUE";
            this.chkTue.Text = "Tue";
            // 
            // chkMon
            // 
            this.chkMon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMon.Location = new System.Drawing.Point(35, 11);
            this.chkMon.Name = "chkMon";
            this.chkMon.Size = new System.Drawing.Size(57, 18);
            this.chkMon.TabIndex = 13;
            this.chkMon.Tag = "MON";
            this.chkMon.Text = "Mon";
            // 
            // frmBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(535, 512);
            this.Controls.Add(this.pnlScheduling);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBackUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "gloPMS - Database BackUp";
            this.Load += new System.EventHandler(this.frmBackUp_Load);
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.pnlBackupType.ResumeLayout(false);
            this.pnlSelectNS.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnlScheduling.ResumeLayout(false);
            this.pnlScheduling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekFreq)).EndInit();
            this.pnlTimeInfo.ResumeLayout(false);
            this.pnlTimeInfo.PerformLayout();
            this.pnlScheduleHeader.ResumeLayout(false);
            this.pnlScheduleHeader.PerformLayout();
            this.pnlMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDayOfMonth)).EndInit();
            this.pnlWeekly.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel pnl_Toolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtLocation;
        internal System.Windows.Forms.Button btnBrowse;
        internal System.Windows.Forms.Panel pnlBackupType;
        internal System.Windows.Forms.RadioButton optDifferential;
        internal System.Windows.Forms.RadioButton optComplete;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel pnlSelectNS;
        internal System.Windows.Forms.RadioButton optSchedule;
        internal System.Windows.Forms.RadioButton optNow;
        internal System.Windows.Forms.TextBox txtBackUpFileName;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel pnlScheduling;
        private System.Windows.Forms.RadioButton rdBtnMonthly;
        private System.Windows.Forms.RadioButton rdBtnWeekly;
        private System.Windows.Forms.RadioButton rdbtnDaily;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlScheduleHeader;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtScheduleName;
        private System.Windows.Forms.Panel pnlTimeInfo;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker DTPEndDate;
        private System.Windows.Forms.DateTimePicker DTPStartDate;
        private System.Windows.Forms.RadioButton rdBtnNoEndDate;
        internal System.Windows.Forms.DateTimePicker tmScheduleTime;
        private System.Windows.Forms.Panel pnlWeekly;
        internal System.Windows.Forms.CheckBox chkEveryDay;
        internal System.Windows.Forms.CheckBox chkSun;
        internal System.Windows.Forms.CheckBox chkSat;
        internal System.Windows.Forms.CheckBox chkFri;
        internal System.Windows.Forms.CheckBox chkThu;
        internal System.Windows.Forms.CheckBox chkWed;
        internal System.Windows.Forms.CheckBox chkTue;
        internal System.Windows.Forms.CheckBox chkMon;
        private System.Windows.Forms.Panel pnlMonthly;
        internal System.Windows.Forms.Label lblMonth;
        internal System.Windows.Forms.NumericUpDown numMonth;
        internal System.Windows.Forms.Label lblJobOccursValue;
        internal System.Windows.Forms.NumericUpDown numDayOfMonth;
        internal System.Windows.Forms.Label lblJobOccursHead;
        internal System.Windows.Forms.NumericUpDown numWeekFreq;
        internal System.Windows.Forms.Label lblDailyEvery;
        internal System.Windows.Forms.Label lblDaily;
        private System.Windows.Forms.RadioButton rdBtnDateEnd;
        private System.Windows.Forms.Panel pnlStartEndDate;
    }
}