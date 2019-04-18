namespace gloTaskMail
{
    partial class frmTask
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpReminderDate, dtpReminderTime, dtp_EndDate, dtp_StartDate };
            System.Windows.Forms.Control[] cntControls = { dtpReminderDate, dtpReminderTime, dtp_EndDate, dtp_StartDate };
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTask));
            this.pnlFields = new System.Windows.Forms.Panel();
            this.pnlRtxtBox = new System.Windows.Forms.Panel();
            this.rtxtDescription = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlReminder = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpReminderDate = new System.Windows.Forms.DateTimePicker();
            this.chkReminder = new System.Windows.Forms.CheckBox();
            this.lblReminderDate = new System.Windows.Forms.Label();
            this.lblReminderTime = new System.Windows.Forms.Label();
            this.dtpReminderTime = new System.Windows.Forms.DateTimePicker();
            this.pnlTaskDetails = new System.Windows.Forms.Panel();
            this.ChkCompleteTaskforallUsers = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmb_FollowUp = new System.Windows.Forms.ComboBox();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.lblFollowUp = new System.Windows.Forms.Label();
            this.numComplete = new System.Windows.Forms.NumericUpDown();
            this.lblOwner = new System.Windows.Forms.Label();
            this.lblComplete = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.btn_Patient = new System.Windows.Forms.Button();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.btn_Providers = new System.Windows.Forms.Button();
            this.lblProvider = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.pnl_AssignTo = new System.Windows.Forms.Panel();
            this.btnreply = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.lbl_From = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.btnTo = new System.Windows.Forms.Button();
            this.cmb_To = new System.Windows.Forms.ComboBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.btn_ToDel = new System.Windows.Forms.Button();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsbbtn_OnlySave = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_ReviewPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_AssignTo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowOrder = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowLab = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowExam = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowDoc = new System.Windows.Forms.ToolStripButton();
            this.tsb_AcceptTask = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeclineTask = new System.Windows.Forms.ToolStripButton();
            this.tsb_MatchPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowCCD = new System.Windows.Forms.ToolStripButton();
            this.tsb_FlowSheet = new System.Windows.Forms.ToolStripButton();
            this.tsb_Drugs = new System.Windows.Forms.ToolStripButton();
            this.tsb_LabOrder = new System.Windows.Forms.ToolStripButton();
            this.tsb_Vitals = new System.Windows.Forms.ToolStripButton();
            this.tsb_LabReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewMessage = new System.Windows.Forms.ToolStripButton();
            this.tsb_Reply = new System.Windows.Forms.ToolStripButton();
            this.tsb_Forward = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientPayment = new System.Windows.Forms.ToolStripButton();
            this.tsb_RxMeds = new System.Windows.Forms.ToolStripButton();
            this.tsb_Reconcile = new System.Windows.Forms.ToolStripButton();
            this.tsb_MergeOrder = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowHistory = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowROS = new System.Windows.Forms.ToolStripButton();
            this.tsb_ReviewPHI = new System.Windows.Forms.ToolStripButton();
            this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.uiPnlPatientTask = new Janus.Windows.UI.Dock.UIPanel();
            this.uiPnlPatientTaskContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.C1PatTask = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlselectall = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.btncomp = new System.Windows.Forms.Button();
            this.chkselectall = new System.Windows.Forms.CheckBox();
            this.uiPnlTaskDetails = new Janus.Windows.UI.Dock.UIPanel();
            this.uiPnlTaskDetailsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlFields.SuspendLayout();
            this.pnlRtxtBox.SuspendLayout();
            this.pnlReminder.SuspendLayout();
            this.pnlTaskDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numComplete)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_AssignTo.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnlPatientTask)).BeginInit();
            this.uiPnlPatientTask.SuspendLayout();
            this.uiPnlPatientTaskContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1PatTask)).BeginInit();
            this.pnlselectall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnlTaskDetails)).BeginInit();
            this.uiPnlTaskDetails.SuspendLayout();
            this.uiPnlTaskDetailsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFields
            // 
            this.pnlFields.AutoScroll = true;
            this.pnlFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlFields.Controls.Add(this.pnlRtxtBox);
            this.pnlFields.Controls.Add(this.pnlReminder);
            this.pnlFields.Controls.Add(this.pnlTaskDetails);
            this.pnlFields.Controls.Add(this.panel2);
            this.pnlFields.Controls.Add(this.panel1);
            this.pnlFields.Controls.Add(this.pnl_AssignTo);
            this.pnlFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFields.Location = new System.Drawing.Point(0, 0);
            this.pnlFields.Name = "pnlFields";
            this.pnlFields.Size = new System.Drawing.Size(761, 581);
            this.pnlFields.TabIndex = 0;
            // 
            // pnlRtxtBox
            // 
            this.pnlRtxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlRtxtBox.Controls.Add(this.rtxtDescription);
            this.pnlRtxtBox.Controls.Add(this.label5);
            this.pnlRtxtBox.Controls.Add(this.label3);
            this.pnlRtxtBox.Controls.Add(this.label16);
            this.pnlRtxtBox.Controls.Add(this.label17);
            this.pnlRtxtBox.Controls.Add(this.label18);
            this.pnlRtxtBox.Controls.Add(this.label19);
            this.pnlRtxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRtxtBox.Location = new System.Drawing.Point(0, 300);
            this.pnlRtxtBox.Name = "pnlRtxtBox";
            this.pnlRtxtBox.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlRtxtBox.Size = new System.Drawing.Size(761, 281);
            this.pnlRtxtBox.TabIndex = 6;
            // 
            // rtxtDescription
            // 
            this.rtxtDescription.BackColor = System.Drawing.Color.White;
            this.rtxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtDescription.ForeColor = System.Drawing.Color.Black;
            this.rtxtDescription.Location = new System.Drawing.Point(8, 5);
            this.rtxtDescription.MaxLength = 1000;
            this.rtxtDescription.Name = "rtxtDescription";
            this.rtxtDescription.Size = new System.Drawing.Size(749, 272);
            this.rtxtDescription.TabIndex = 0;
            this.rtxtDescription.Text = "";
            this.rtxtDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtDescription_LinkClicked);
            this.rtxtDescription.TextChanged += new System.EventHandler(this.rtxtDescription_TextChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(749, 4);
            this.label5.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(4, 276);
            this.label3.TabIndex = 51;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(4, 277);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(753, 1);
            this.label16.TabIndex = 50;
            this.label16.Text = "label2";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 277);
            this.label17.TabIndex = 49;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(757, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 277);
            this.label18.TabIndex = 48;
            this.label18.Text = "label3";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(755, 1);
            this.label19.TabIndex = 47;
            this.label19.Text = "label1";
            // 
            // pnlReminder
            // 
            this.pnlReminder.Controls.Add(this.label12);
            this.pnlReminder.Controls.Add(this.label13);
            this.pnlReminder.Controls.Add(this.label14);
            this.pnlReminder.Controls.Add(this.label15);
            this.pnlReminder.Controls.Add(this.dtpReminderDate);
            this.pnlReminder.Controls.Add(this.chkReminder);
            this.pnlReminder.Controls.Add(this.lblReminderDate);
            this.pnlReminder.Controls.Add(this.lblReminderTime);
            this.pnlReminder.Controls.Add(this.dtpReminderTime);
            this.pnlReminder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReminder.Location = new System.Drawing.Point(0, 259);
            this.pnlReminder.Name = "pnlReminder";
            this.pnlReminder.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlReminder.Size = new System.Drawing.Size(761, 41);
            this.pnlReminder.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(4, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(753, 1);
            this.label12.TabIndex = 24;
            this.label12.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 37);
            this.label13.TabIndex = 23;
            this.label13.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(757, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 37);
            this.label14.TabIndex = 22;
            this.label14.Text = "label3";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(755, 1);
            this.label15.TabIndex = 21;
            this.label15.Text = "label1";
            // 
            // dtpReminderDate
            // 
            this.dtpReminderDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReminderDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReminderDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReminderDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReminderDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReminderDate.Checked = false;
            this.dtpReminderDate.CustomFormat = "MM/dd/yyyy";
            this.dtpReminderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReminderDate.Location = new System.Drawing.Point(110, 8);
            this.dtpReminderDate.Name = "dtpReminderDate";
            this.dtpReminderDate.Size = new System.Drawing.Size(171, 22);
            this.dtpReminderDate.TabIndex = 0;
            this.dtpReminderDate.ValueChanged += new System.EventHandler(this.dtpReminderDate_ValueChanged);
            // 
            // chkReminder
            // 
            this.chkReminder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReminder.AutoSize = true;
            this.chkReminder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReminder.Location = new System.Drawing.Point(579, 10);
            this.chkReminder.Name = "chkReminder";
            this.chkReminder.Size = new System.Drawing.Size(77, 18);
            this.chkReminder.TabIndex = 2;
            this.chkReminder.Text = "Reminder";
            this.chkReminder.UseVisualStyleBackColor = true;
            // 
            // lblReminderDate
            // 
            this.lblReminderDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReminderDate.AutoSize = true;
            this.lblReminderDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReminderDate.Location = new System.Drawing.Point(12, 12);
            this.lblReminderDate.Name = "lblReminderDate";
            this.lblReminderDate.Size = new System.Drawing.Size(96, 14);
            this.lblReminderDate.TabIndex = 18;
            this.lblReminderDate.Text = "Reminder Date :";
            // 
            // lblReminderTime
            // 
            this.lblReminderTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReminderTime.AutoSize = true;
            this.lblReminderTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReminderTime.Location = new System.Drawing.Point(389, 12);
            this.lblReminderTime.Name = "lblReminderTime";
            this.lblReminderTime.Size = new System.Drawing.Size(97, 14);
            this.lblReminderTime.TabIndex = 19;
            this.lblReminderTime.Text = "Reminder Time :";
            // 
            // dtpReminderTime
            // 
            this.dtpReminderTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpReminderTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReminderTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReminderTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReminderTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReminderTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReminderTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpReminderTime.CustomFormat = "hh:mm tt";
            this.dtpReminderTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReminderTime.Location = new System.Drawing.Point(490, 8);
            this.dtpReminderTime.Name = "dtpReminderTime";
            this.dtpReminderTime.ShowUpDown = true;
            this.dtpReminderTime.Size = new System.Drawing.Size(80, 22);
            this.dtpReminderTime.TabIndex = 1;
            this.dtpReminderTime.ValueChanged += new System.EventHandler(this.dtpReminderTime_ValueChanged);
            // 
            // pnlTaskDetails
            // 
            this.pnlTaskDetails.Controls.Add(this.ChkCompleteTaskforallUsers);
            this.pnlTaskDetails.Controls.Add(this.label1);
            this.pnlTaskDetails.Controls.Add(this.label2);
            this.pnlTaskDetails.Controls.Add(this.label10);
            this.pnlTaskDetails.Controls.Add(this.label11);
            this.pnlTaskDetails.Controls.Add(this.dtp_EndDate);
            this.pnlTaskDetails.Controls.Add(this.lblStartDate);
            this.pnlTaskDetails.Controls.Add(this.lblEndDate);
            this.pnlTaskDetails.Controls.Add(this.dtp_StartDate);
            this.pnlTaskDetails.Controls.Add(this.lblStatus);
            this.pnlTaskDetails.Controls.Add(this.lblPriority);
            this.pnlTaskDetails.Controls.Add(this.cmbStatus);
            this.pnlTaskDetails.Controls.Add(this.cmb_FollowUp);
            this.pnlTaskDetails.Controls.Add(this.cmbPriority);
            this.pnlTaskDetails.Controls.Add(this.lblFollowUp);
            this.pnlTaskDetails.Controls.Add(this.numComplete);
            this.pnlTaskDetails.Controls.Add(this.lblOwner);
            this.pnlTaskDetails.Controls.Add(this.lblComplete);
            this.pnlTaskDetails.Controls.Add(this.txtOwner);
            this.pnlTaskDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTaskDetails.Location = new System.Drawing.Point(0, 163);
            this.pnlTaskDetails.Name = "pnlTaskDetails";
            this.pnlTaskDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTaskDetails.Size = new System.Drawing.Size(761, 96);
            this.pnlTaskDetails.TabIndex = 4;
            // 
            // ChkCompleteTaskforallUsers
            // 
            this.ChkCompleteTaskforallUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkCompleteTaskforallUsers.AutoSize = true;
            this.ChkCompleteTaskforallUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCompleteTaskforallUsers.Location = new System.Drawing.Point(579, 67);
            this.ChkCompleteTaskforallUsers.Name = "ChkCompleteTaskforallUsers";
            this.ChkCompleteTaskforallUsers.Size = new System.Drawing.Size(173, 18);
            this.ChkCompleteTaskforallUsers.TabIndex = 6;
            this.ChkCompleteTaskforallUsers.Text = "Complete Task for all Users";
            this.ChkCompleteTaskforallUsers.UseVisualStyleBackColor = true;
            this.ChkCompleteTaskforallUsers.CheckedChanged += new System.EventHandler(this.chkCompleteAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(753, 1);
            this.label1.TabIndex = 55;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 92);
            this.label2.TabIndex = 54;
            this.label2.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(757, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 92);
            this.label10.TabIndex = 53;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(755, 1);
            this.label11.TabIndex = 52;
            this.label11.Text = "label1";
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtp_EndDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtp_EndDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtp_EndDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtp_EndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtp_EndDate.Checked = false;
            this.dtp_EndDate.CustomFormat = "MM/dd/yyyy";
            this.dtp_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndDate.Location = new System.Drawing.Point(110, 36);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(171, 22);
            this.dtp_EndDate.TabIndex = 1;
            this.dtp_EndDate.ValueChanged += new System.EventHandler(this.dtp_EndDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(36, 10);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(41, 39);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(67, 14);
            this.lblEndDate.TabIndex = 5;
            this.lblEndDate.Text = "Due Date :";
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtp_StartDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtp_StartDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtp_StartDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtp_StartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtp_StartDate.Checked = false;
            this.dtp_StartDate.CustomFormat = "MM/dd/yyyy";
            this.dtp_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_StartDate.Location = new System.Drawing.Point(110, 6);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(171, 22);
            this.dtp_StartDate.TabIndex = 0;
            this.dtp_StartDate.ValueChanged += new System.EventHandler(this.dtp_StartDate_ValueChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(436, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 14);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status :";
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.Location = new System.Drawing.Point(434, 40);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(52, 14);
            this.lblPriority.TabIndex = 9;
            this.lblPriority.Text = "Priority :";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(490, 6);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(204, 22);
            this.cmbStatus.TabIndex = 3;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // cmb_FollowUp
            // 
            this.cmb_FollowUp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmb_FollowUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FollowUp.ForeColor = System.Drawing.Color.Black;
            this.cmb_FollowUp.FormattingEnabled = true;
            this.cmb_FollowUp.Location = new System.Drawing.Point(110, 66);
            this.cmb_FollowUp.Name = "cmb_FollowUp";
            this.cmb_FollowUp.Size = new System.Drawing.Size(171, 22);
            this.cmb_FollowUp.TabIndex = 2;
            this.cmb_FollowUp.SelectedIndexChanged += new System.EventHandler(this.cmb_FollowUp_SelectedIndexChanged);
            // 
            // cmbPriority
            // 
            this.cmbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPriority.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.ForeColor = System.Drawing.Color.Black;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(490, 36);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(204, 22);
            this.cmbPriority.TabIndex = 4;
            this.cmbPriority.SelectedIndexChanged += new System.EventHandler(this.cmbPriority_SelectedIndexChanged);
            // 
            // lblFollowUp
            // 
            this.lblFollowUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFollowUp.AutoSize = true;
            this.lblFollowUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFollowUp.Location = new System.Drawing.Point(44, 70);
            this.lblFollowUp.Name = "lblFollowUp";
            this.lblFollowUp.Size = new System.Drawing.Size(64, 14);
            this.lblFollowUp.TabIndex = 34;
            this.lblFollowUp.Text = "FollowUp :";
            // 
            // numComplete
            // 
            this.numComplete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numComplete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.numComplete.ForeColor = System.Drawing.Color.Black;
            this.numComplete.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numComplete.Location = new System.Drawing.Point(490, 66);
            this.numComplete.Name = "numComplete";
            this.numComplete.ReadOnly = true;
            this.numComplete.Size = new System.Drawing.Size(80, 22);
            this.numComplete.TabIndex = 5;
            this.numComplete.ValueChanged += new System.EventHandler(this.numComplete_ValueChanged);
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOwner.Location = new System.Drawing.Point(45, 205);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(43, 14);
            this.lblOwner.TabIndex = 32;
            this.lblOwner.Text = "User  :";
            this.lblOwner.Visible = false;
            // 
            // lblComplete
            // 
            this.lblComplete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComplete.AutoSize = true;
            this.lblComplete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComplete.Location = new System.Drawing.Point(403, 70);
            this.lblComplete.Name = "lblComplete";
            this.lblComplete.Size = new System.Drawing.Size(83, 14);
            this.lblComplete.TabIndex = 10;
            this.lblComplete.Text = "% Complete :";
            // 
            // txtOwner
            // 
            this.txtOwner.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtOwner.Enabled = false;
            this.txtOwner.ForeColor = System.Drawing.Color.Black;
            this.txtOwner.Location = new System.Drawing.Point(92, 211);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.ReadOnly = true;
            this.txtOwner.Size = new System.Drawing.Size(514, 22);
            this.txtOwner.TabIndex = 33;
            this.txtOwner.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.txtProvider);
            this.panel2.Controls.Add(this.btn_Patient);
            this.panel2.Controls.Add(this.txtPatient);
            this.panel2.Controls.Add(this.lblPatient);
            this.panel2.Controls.Add(this.btn_Providers);
            this.panel2.Controls.Add(this.lblProvider);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(761, 64);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(4, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(753, 1);
            this.label4.TabIndex = 44;
            this.label4.Text = "label2";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 60);
            this.label20.TabIndex = 43;
            this.label20.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Right;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(757, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 60);
            this.label21.TabIndex = 42;
            this.label21.Text = "label3";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(755, 1);
            this.label22.TabIndex = 41;
            this.label22.Text = "label1";
            // 
            // txtProvider
            // 
            this.txtProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProvider.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtProvider.Enabled = false;
            this.txtProvider.ForeColor = System.Drawing.Color.Black;
            this.txtProvider.Location = new System.Drawing.Point(110, 4);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.ReadOnly = true;
            this.txtProvider.Size = new System.Drawing.Size(584, 22);
            this.txtProvider.TabIndex = 10;
            this.txtProvider.TextChanged += new System.EventHandler(this.txtProvider_TextChanged);
            // 
            // btn_Patient
            // 
            this.btn_Patient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Patient.BackColor = System.Drawing.Color.Transparent;
            this.btn_Patient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Patient.BackgroundImage")));
            this.btn_Patient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Patient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Patient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Patient.Image = ((System.Drawing.Image)(resources.GetObject("btn_Patient.Image")));
            this.btn_Patient.Location = new System.Drawing.Point(700, 32);
            this.btn_Patient.Name = "btn_Patient";
            this.btn_Patient.Size = new System.Drawing.Size(22, 22);
            this.btn_Patient.TabIndex = 1;
            this.btn_Patient.UseVisualStyleBackColor = false;
            this.btn_Patient.Click += new System.EventHandler(this.btn_Patient_Click);
            this.btn_Patient.MouseLeave += new System.EventHandler(this.btn_Patient_MouseLeave);
            this.btn_Patient.MouseHover += new System.EventHandler(this.btn_Patient_MouseHover);
            // 
            // txtPatient
            // 
            this.txtPatient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatient.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPatient.Enabled = false;
            this.txtPatient.ForeColor = System.Drawing.Color.Black;
            this.txtPatient.Location = new System.Drawing.Point(110, 32);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.ReadOnly = true;
            this.txtPatient.Size = new System.Drawing.Size(584, 22);
            this.txtPatient.TabIndex = 30;
            this.txtPatient.TextChanged += new System.EventHandler(this.txtPatient_TextChanged);
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.Location = new System.Drawing.Point(54, 36);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(54, 14);
            this.lblPatient.TabIndex = 29;
            this.lblPatient.Text = "Patient :";
            // 
            // btn_Providers
            // 
            this.btn_Providers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Providers.BackColor = System.Drawing.Color.Transparent;
            this.btn_Providers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Providers.BackgroundImage")));
            this.btn_Providers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Providers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Providers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Providers.Image = ((System.Drawing.Image)(resources.GetObject("btn_Providers.Image")));
            this.btn_Providers.Location = new System.Drawing.Point(700, 4);
            this.btn_Providers.Name = "btn_Providers";
            this.btn_Providers.Size = new System.Drawing.Size(22, 22);
            this.btn_Providers.TabIndex = 0;
            this.btn_Providers.UseVisualStyleBackColor = false;
            this.btn_Providers.Click += new System.EventHandler(this.btn_Providers_Click);
            this.btn_Providers.MouseLeave += new System.EventHandler(this.btn_Providers_MouseLeave);
            this.btn_Providers.MouseHover += new System.EventHandler(this.btn_Providers_MouseHover);
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(49, 8);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(59, 14);
            this.lblProvider.TabIndex = 14;
            this.lblProvider.Text = "Provider :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtSubject);
            this.panel1.Controls.Add(this.lblSubject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(761, 33);
            this.panel1.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(39, 8);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(14, 14);
            this.label24.TabIndex = 45;
            this.label24.Text = "*";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(4, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(753, 1);
            this.label6.TabIndex = 44;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 29);
            this.label7.TabIndex = 43;
            this.label7.Text = "label4";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(757, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 29);
            this.label8.TabIndex = 42;
            this.label8.Text = "label3";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(755, 1);
            this.label9.TabIndex = 41;
            this.label9.Text = "label1";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSubject.ForeColor = System.Drawing.Color.Black;
            this.txtSubject.Location = new System.Drawing.Point(110, 4);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(0);
            this.txtSubject.MaxLength = 200;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSubject.Size = new System.Drawing.Size(584, 22);
            this.txtSubject.TabIndex = 0;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(51, 8);
            this.lblSubject.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(57, 14);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = "Subject :";
            this.lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_AssignTo
            // 
            this.pnl_AssignTo.Controls.Add(this.btnreply);
            this.pnl_AssignTo.Controls.Add(this.label25);
            this.pnl_AssignTo.Controls.Add(this.txtFrom);
            this.pnl_AssignTo.Controls.Add(this.lbl_From);
            this.pnl_AssignTo.Controls.Add(this.lbl_BottomBrd);
            this.pnl_AssignTo.Controls.Add(this.lbl_LeftBrd);
            this.pnl_AssignTo.Controls.Add(this.lbl_RightBrd);
            this.pnl_AssignTo.Controls.Add(this.lbl_TopBrd);
            this.pnl_AssignTo.Controls.Add(this.btnTo);
            this.pnl_AssignTo.Controls.Add(this.cmb_To);
            this.pnl_AssignTo.Controls.Add(this.lblTo);
            this.pnl_AssignTo.Controls.Add(this.btn_ToDel);
            this.pnl_AssignTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_AssignTo.Location = new System.Drawing.Point(0, 0);
            this.pnl_AssignTo.Name = "pnl_AssignTo";
            this.pnl_AssignTo.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_AssignTo.Size = new System.Drawing.Size(761, 66);
            this.pnl_AssignTo.TabIndex = 1;
            // 
            // btnreply
            // 
            this.btnreply.Location = new System.Drawing.Point(4, 8);
            this.btnreply.Name = "btnreply";
            this.btnreply.Size = new System.Drawing.Size(55, 23);
            this.btnreply.TabIndex = 48;
            this.btnreply.Text = "Reply";
            this.btnreply.UseVisualStyleBackColor = true;
            this.btnreply.Click += new System.EventHandler(this.btnreply_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(60, 12);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(14, 14);
            this.label25.TabIndex = 47;
            this.label25.Text = "*";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFrom.Enabled = false;
            this.txtFrom.ForeColor = System.Drawing.Color.Black;
            this.txtFrom.Location = new System.Drawing.Point(110, 35);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(0);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFrom.Size = new System.Drawing.Size(584, 22);
            this.txtFrom.TabIndex = 3;
            this.txtFrom.TextChanged += new System.EventHandler(this.txtFrom_TextChanged);
            // 
            // lbl_From
            // 
            this.lbl_From.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_From.AutoSize = true;
            this.lbl_From.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_From.Location = new System.Drawing.Point(66, 39);
            this.lbl_From.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_From.Name = "lbl_From";
            this.lbl_From.Size = new System.Drawing.Size(42, 14);
            this.lbl_From.TabIndex = 46;
            this.lbl_From.Text = "From :";
            this.lbl_From.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_From.Click += new System.EventHandler(this.lbl_From_Click);
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 62);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(753, 1);
            this.lbl_BottomBrd.TabIndex = 44;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 59);
            this.lbl_LeftBrd.TabIndex = 43;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(757, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 59);
            this.lbl_RightBrd.TabIndex = 42;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(755, 1);
            this.lbl_TopBrd.TabIndex = 41;
            this.lbl_TopBrd.Text = "label1";
            // 
            // btnTo
            // 
            this.btnTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTo.BackColor = System.Drawing.Color.Transparent;
            this.btnTo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTo.BackgroundImage")));
            this.btnTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTo.Image = ((System.Drawing.Image)(resources.GetObject("btnTo.Image")));
            this.btnTo.Location = new System.Drawing.Point(700, 8);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(22, 22);
            this.btnTo.TabIndex = 0;
            this.btnTo.UseVisualStyleBackColor = false;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            this.btnTo.MouseLeave += new System.EventHandler(this.btnTo_MouseLeave);
            this.btnTo.MouseHover += new System.EventHandler(this.btnTo_MouseHover);
            // 
            // cmb_To
            // 
            this.cmb_To.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_To.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_To.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_To.ForeColor = System.Drawing.Color.Black;
            this.cmb_To.FormattingEnabled = true;
            this.cmb_To.Location = new System.Drawing.Point(110, 8);
            this.cmb_To.Name = "cmb_To";
            this.cmb_To.Size = new System.Drawing.Size(584, 22);
            this.cmb_To.TabIndex = 2;
            this.cmb_To.SelectedIndexChanged += new System.EventHandler(this.cmb_To_SelectedIndexChanged);
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(74, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(34, 14);
            this.lblTo.TabIndex = 38;
            this.lblTo.Text = "To  :";
            // 
            // btn_ToDel
            // 
            this.btn_ToDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ToDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_ToDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ToDel.BackgroundImage")));
            this.btn_ToDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ToDel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_ToDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ToDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_ToDel.Image")));
            this.btn_ToDel.Location = new System.Drawing.Point(726, 8);
            this.btn_ToDel.Name = "btn_ToDel";
            this.btn_ToDel.Size = new System.Drawing.Size(22, 22);
            this.btn_ToDel.TabIndex = 1;
            this.btn_ToDel.UseVisualStyleBackColor = false;
            this.btn_ToDel.Click += new System.EventHandler(this.btn_ToDel_Click);
            this.btn_ToDel.MouseLeave += new System.EventHandler(this.btn_ToDel_MouseLeave);
            this.btn_ToDel.MouseHover += new System.EventHandler(this.btn_ToDel_MouseHover);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1012, 53);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsbbtn_OnlySave,
            this.tsb_Cancel,
            this.tsb_ReviewPatient,
            this.tsb_ViewReport,
            this.tsb_AssignTo,
            this.toolStripButton1,
            this.tsb_ShowOrder,
            this.tsb_ShowLab,
            this.tsb_ShowExam,
            this.tsb_ShowDoc,
            this.tsb_AcceptTask,
            this.tsb_DeclineTask,
            this.tsb_MatchPatient,
            this.tsb_ShowCCD,
            this.tsb_FlowSheet,
            this.tsb_Drugs,
            this.tsb_LabOrder,
            this.tsb_Vitals,
            this.tsb_LabReport,
            this.tsb_ViewMessage,
            this.tsb_Reply,
            this.tsb_Forward,
            this.tsb_PatientPayment,
            this.tsb_RxMeds,
            this.tsb_Reconcile,
            this.tsb_MergeOrder,
            this.tsb_ShowHistory,
            this.tsb_ShowROS,
            this.tsb_ReviewPHI});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1012, 53);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsbbtn_OnlySave
            // 
            this.tsbbtn_OnlySave.BackColor = System.Drawing.Color.Transparent;
            this.tsbbtn_OnlySave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsbbtn_OnlySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbbtn_OnlySave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbbtn_OnlySave.Image = ((System.Drawing.Image)(resources.GetObject("tsbbtn_OnlySave.Image")));
            this.tsbbtn_OnlySave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbbtn_OnlySave.Name = "tsbbtn_OnlySave";
            this.tsbbtn_OnlySave.Size = new System.Drawing.Size(40, 50);
            this.tsbbtn_OnlySave.Tag = "Save";
            this.tsbbtn_OnlySave.Text = "Sa&ve";
            this.tsbbtn_OnlySave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbbtn_OnlySave.ToolTipText = "Save";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_ReviewPatient
            // 
            this.tsb_ReviewPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ReviewPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ReviewPatient.Image")));
            this.tsb_ReviewPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ReviewPatient.Name = "tsb_ReviewPatient";
            this.tsb_ReviewPatient.Size = new System.Drawing.Size(104, 50);
            this.tsb_ReviewPatient.Tag = "ReviewPatient";
            this.tsb_ReviewPatient.Text = "&Review Patient";
            this.tsb_ReviewPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ReviewPatient.ToolTipText = "Review Portal Patient";
            this.tsb_ReviewPatient.Visible = false;
            // 
            // tsb_ViewReport
            // 
            this.tsb_ViewReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewReport.Image")));
            this.tsb_ViewReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewReport.Name = "tsb_ViewReport";
            this.tsb_ViewReport.Size = new System.Drawing.Size(87, 50);
            this.tsb_ViewReport.Tag = "ViewReport";
            this.tsb_ViewReport.Text = "&View Report";
            this.tsb_ViewReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewReport.Visible = false;
            // 
            // tsb_AssignTo
            // 
            this.tsb_AssignTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AssignTo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AssignTo.Image")));
            this.tsb_AssignTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AssignTo.Name = "tsb_AssignTo";
            this.tsb_AssignTo.Size = new System.Drawing.Size(70, 50);
            this.tsb_AssignTo.Tag = "AssignTo";
            this.tsb_AssignTo.Text = "&Assign To";
            this.tsb_AssignTo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsb_AssignTo.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_AssignTo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AssignTo.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 50);
            this.toolStripButton1.Text = "&Help";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Visible = false;
            // 
            // tsb_ShowOrder
            // 
            this.tsb_ShowOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowOrder.Image")));
            this.tsb_ShowOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowOrder.Name = "tsb_ShowOrder";
            this.tsb_ShowOrder.Size = new System.Drawing.Size(150, 50);
            this.tsb_ShowOrder.Tag = "ShowOrder";
            this.tsb_ShowOrder.Text = "&Show Order Templates";
            this.tsb_ShowOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowOrder.Visible = false;
            // 
            // tsb_ShowLab
            // 
            this.tsb_ShowLab.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowLab.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowLab.Image")));
            this.tsb_ShowLab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowLab.Name = "tsb_ShowLab";
            this.tsb_ShowLab.Size = new System.Drawing.Size(82, 50);
            this.tsb_ShowLab.Tag = "ShowLab";
            this.tsb_ShowLab.Text = "&Order Entry";
            this.tsb_ShowLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowLab.Visible = false;
            // 
            // tsb_ShowExam
            // 
            this.tsb_ShowExam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowExam.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowExam.Image")));
            this.tsb_ShowExam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowExam.Name = "tsb_ShowExam";
            this.tsb_ShowExam.Size = new System.Drawing.Size(82, 50);
            this.tsb_ShowExam.Tag = "ShowExam";
            this.tsb_ShowExam.Text = "Show &Exam";
            this.tsb_ShowExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowExam.ToolTipText = "Show Exam";
            this.tsb_ShowExam.Visible = false;
            // 
            // tsb_ShowDoc
            // 
            this.tsb_ShowDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowDoc.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowDoc.Image")));
            this.tsb_ShowDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowDoc.Name = "tsb_ShowDoc";
            this.tsb_ShowDoc.Size = new System.Drawing.Size(79, 50);
            this.tsb_ShowDoc.Tag = "ShowDocs";
            this.tsb_ShowDoc.Text = "Show &Docs";
            this.tsb_ShowDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowDoc.ToolTipText = "Show Documents";
            this.tsb_ShowDoc.Visible = false;
            // 
            // tsb_AcceptTask
            // 
            this.tsb_AcceptTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AcceptTask.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AcceptTask.Image")));
            this.tsb_AcceptTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AcceptTask.Name = "tsb_AcceptTask";
            this.tsb_AcceptTask.Size = new System.Drawing.Size(84, 50);
            this.tsb_AcceptTask.Tag = "AcceptTask";
            this.tsb_AcceptTask.Text = "Accept &Task";
            this.tsb_AcceptTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AcceptTask.Visible = false;
            // 
            // tsb_DeclineTask
            // 
            this.tsb_DeclineTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DeclineTask.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DeclineTask.Image")));
            this.tsb_DeclineTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DeclineTask.Name = "tsb_DeclineTask";
            this.tsb_DeclineTask.Size = new System.Drawing.Size(85, 50);
            this.tsb_DeclineTask.Tag = "DeclineTask";
            this.tsb_DeclineTask.Text = "&Decline Task";
            this.tsb_DeclineTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DeclineTask.Visible = false;
            // 
            // tsb_MatchPatient
            // 
            this.tsb_MatchPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_MatchPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MatchPatient.Image")));
            this.tsb_MatchPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MatchPatient.Name = "tsb_MatchPatient";
            this.tsb_MatchPatient.Size = new System.Drawing.Size(98, 50);
            this.tsb_MatchPatient.Tag = "MatchPatient";
            this.tsb_MatchPatient.Text = "&Match Patient";
            this.tsb_MatchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MatchPatient.Visible = false;
            // 
            // tsb_ShowCCD
            // 
            this.tsb_ShowCCD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ShowCCD.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowCCD.Image")));
            this.tsb_ShowCCD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowCCD.Name = "tsb_ShowCCD";
            this.tsb_ShowCCD.Size = new System.Drawing.Size(109, 50);
            this.tsb_ShowCCD.Tag = "ShowCCD";
            this.tsb_ShowCCD.Text = "Show &CCD-CCR ";
            this.tsb_ShowCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowCCD.ToolTipText = "Show CCD";
            this.tsb_ShowCCD.Visible = false;
            // 
            // tsb_FlowSheet
            // 
            this.tsb_FlowSheet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_FlowSheet.Image = ((System.Drawing.Image)(resources.GetObject("tsb_FlowSheet.Image")));
            this.tsb_FlowSheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_FlowSheet.Name = "tsb_FlowSheet";
            this.tsb_FlowSheet.Size = new System.Drawing.Size(114, 50);
            this.tsb_FlowSheet.Tag = "ShowFlowSheet";
            this.tsb_FlowSheet.Text = "Show &FlowSheet";
            this.tsb_FlowSheet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_FlowSheet.ToolTipText = "Show FlowSheet";
            this.tsb_FlowSheet.Visible = false;
            // 
            // tsb_Drugs
            // 
            this.tsb_Drugs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Drugs.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Drugs.Image")));
            this.tsb_Drugs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Drugs.Name = "tsb_Drugs";
            this.tsb_Drugs.Size = new System.Drawing.Size(86, 50);
            this.tsb_Drugs.Tag = "ShowDrugs";
            this.tsb_Drugs.Text = "Show &Drugs";
            this.tsb_Drugs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Drugs.ToolTipText = "Show Drugs";
            this.tsb_Drugs.Visible = false;
            // 
            // tsb_LabOrder
            // 
            this.tsb_LabOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_LabOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsb_LabOrder.Image")));
            this.tsb_LabOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_LabOrder.Name = "tsb_LabOrder";
            this.tsb_LabOrder.Size = new System.Drawing.Size(71, 50);
            this.tsb_LabOrder.Tag = "LabOrder";
            this.tsb_LabOrder.Text = "&Lab Order";
            this.tsb_LabOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_LabOrder.Visible = false;
            // 
            // tsb_Vitals
            // 
            this.tsb_Vitals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Vitals.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Vitals.Image")));
            this.tsb_Vitals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Vitals.Name = "tsb_Vitals";
            this.tsb_Vitals.Size = new System.Drawing.Size(83, 50);
            this.tsb_Vitals.Tag = "ShowVitals";
            this.tsb_Vitals.Text = "Show &Vitals";
            this.tsb_Vitals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Vitals.ToolTipText = "Vitals";
            this.tsb_Vitals.Visible = false;
            // 
            // tsb_LabReport
            // 
            this.tsb_LabReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_LabReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_LabReport.Image")));
            this.tsb_LabReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_LabReport.Name = "tsb_LabReport";
            this.tsb_LabReport.Size = new System.Drawing.Size(80, 50);
            this.tsb_LabReport.Tag = "LabReport";
            this.tsb_LabReport.Text = "Lab &Report";
            this.tsb_LabReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_LabReport.Visible = false;
            // 
            // tsb_ViewMessage
            // 
            this.tsb_ViewMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ViewMessage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewMessage.Image")));
            this.tsb_ViewMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewMessage.Name = "tsb_ViewMessage";
            this.tsb_ViewMessage.Size = new System.Drawing.Size(96, 50);
            this.tsb_ViewMessage.Tag = "ViewMessage";
            this.tsb_ViewMessage.Text = "View &Message";
            this.tsb_ViewMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewMessage.Visible = false;
            // 
            // tsb_Reply
            // 
            this.tsb_Reply.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Reply.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reply.Image")));
            this.tsb_Reply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Reply.Name = "tsb_Reply";
            this.tsb_Reply.Size = new System.Drawing.Size(45, 50);
            this.tsb_Reply.Tag = "ReplyMessage";
            this.tsb_Reply.Text = "&Reply";
            this.tsb_Reply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Reply.Visible = false;
            // 
            // tsb_Forward
            // 
            this.tsb_Forward.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Forward.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Forward.Image")));
            this.tsb_Forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Forward.Name = "tsb_Forward";
            this.tsb_Forward.Size = new System.Drawing.Size(61, 50);
            this.tsb_Forward.Tag = "ForwardMessage";
            this.tsb_Forward.Text = "&Forward";
            this.tsb_Forward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Forward.Visible = false;
            // 
            // tsb_PatientPayment
            // 
            this.tsb_PatientPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_PatientPayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientPayment.Image")));
            this.tsb_PatientPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientPayment.Name = "tsb_PatientPayment";
            this.tsb_PatientPayment.Size = new System.Drawing.Size(94, 50);
            this.tsb_PatientPayment.Tag = "PatientPayment";
            this.tsb_PatientPayment.Text = "&Pat. Payment";
            this.tsb_PatientPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientPayment.ToolTipText = "Patient Payment";
            this.tsb_PatientPayment.Visible = false;
            this.tsb_PatientPayment.Click += new System.EventHandler(this.tsb_PatientPayment_Click);
            // 
            // tsb_RxMeds
            // 
            this.tsb_RxMeds.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_RxMeds.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RxMeds.Image")));
            this.tsb_RxMeds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RxMeds.Name = "tsb_RxMeds";
            this.tsb_RxMeds.Size = new System.Drawing.Size(64, 50);
            this.tsb_RxMeds.Tag = "RxMeds";
            this.tsb_RxMeds.Text = "&Rx-Meds";
            this.tsb_RxMeds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RxMeds.ToolTipText = "Rx Meds";
            this.tsb_RxMeds.Visible = false;
            // 
            // tsb_Reconcile
            // 
            this.tsb_Reconcile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Reconcile.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Reconcile.Image")));
            this.tsb_Reconcile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Reconcile.Name = "tsb_Reconcile";
            this.tsb_Reconcile.Size = new System.Drawing.Size(68, 50);
            this.tsb_Reconcile.Tag = "Reconcile";
            this.tsb_Reconcile.Text = "&Reconcile";
            this.tsb_Reconcile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Reconcile.ToolTipText = "Reconcile";
            this.tsb_Reconcile.Visible = false;
            // 
            // tsb_MergeOrder
            // 
            this.tsb_MergeOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_MergeOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MergeOrder.Image")));
            this.tsb_MergeOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MergeOrder.Name = "tsb_MergeOrder";
            this.tsb_MergeOrder.Size = new System.Drawing.Size(87, 50);
            this.tsb_MergeOrder.Tag = "MergeOrder";
            this.tsb_MergeOrder.Text = "&Merge Order";
            this.tsb_MergeOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MergeOrder.ToolTipText = "Merge Order";
            this.tsb_MergeOrder.Visible = false;
            // 
            // tsb_ShowHistory
            // 
            this.tsb_ShowHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ShowHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowHistory.Image")));
            this.tsb_ShowHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHistory.Name = "tsb_ShowHistory";
            this.tsb_ShowHistory.Size = new System.Drawing.Size(94, 50);
            this.tsb_ShowHistory.Tag = "ShowHistory";
            this.tsb_ShowHistory.Text = "Show &History";
            this.tsb_ShowHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowHistory.Visible = false;
            // 
            // tsb_ShowROS
            // 
            this.tsb_ShowROS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowROS.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowROS.Image")));
            this.tsb_ShowROS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowROS.Name = "tsb_ShowROS";
            this.tsb_ShowROS.Size = new System.Drawing.Size(76, 50);
            this.tsb_ShowROS.Tag = "ShowROS";
            this.tsb_ShowROS.Text = "Show ROS";
            this.tsb_ShowROS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowROS.Visible = false;
            // 
            // tsb_ReviewPHI
            // 
            this.tsb_ReviewPHI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ReviewPHI.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ReviewPHI.Image")));
            this.tsb_ReviewPHI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ReviewPHI.Name = "tsb_ReviewPHI";
            this.tsb_ReviewPHI.Size = new System.Drawing.Size(81, 50);
            this.tsb_ReviewPHI.Tag = "ReviewPHI";
            this.tsb_ReviewPHI.Text = "Review PHI";
            this.tsb_ReviewPHI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ReviewPHI.ToolTipText = "Review Personal health information (PHI)";
            this.tsb_ReviewPHI.Visible = false;
            // 
            // uiPanelManager1
            // 
            this.uiPanelManager1.ContainerControl = this;
            this.uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            this.uiPnlPatientTask.Id = new System.Guid("df6338a5-b272-43e1-81cc-93ceadfe12f9");
            this.uiPanelManager1.Panels.Add(this.uiPnlPatientTask);
            this.uiPnlTaskDetails.Id = new System.Guid("f70c8c22-b59a-43e4-ba59-3f286ee21442");
            this.uiPanelManager1.Panels.Add(this.uiPnlTaskDetails);
            // 
            // Design Time Panel Info:
            // 
            this.uiPanelManager1.BeginPanelInfo();
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("df6338a5-b272-43e1-81cc-93ceadfe12f9"), Janus.Windows.UI.Dock.PanelDockStyle.Left, new System.Drawing.Size(243, 605), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("f70c8c22-b59a-43e4-ba59-3f286ee21442"), Janus.Windows.UI.Dock.PanelDockStyle.Fill, new System.Drawing.Size(763, 605), true);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("f70c8c22-b59a-43e4-ba59-3f286ee21442"), new System.Drawing.Point(25, 25), new System.Drawing.Size(500, 500), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("df6338a5-b272-43e1-81cc-93ceadfe12f9"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.EndPanelInfo();
            // 
            // uiPnlPatientTask
            // 
            this.uiPnlPatientTask.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.uiPnlPatientTask.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPnlPatientTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiPnlPatientTask.Icon = ((System.Drawing.Icon)(resources.GetObject("uiPnlPatientTask.Icon")));
            this.uiPnlPatientTask.InfoTextFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.InnerContainer = this.uiPnlPatientTaskContainer;
            this.uiPnlPatientTask.Location = new System.Drawing.Point(3, 56);
            this.uiPnlPatientTask.Name = "uiPnlPatientTask";
            this.uiPnlPatientTask.Size = new System.Drawing.Size(243, 605);
            this.uiPnlPatientTask.TabIndex = 1;
            this.uiPnlPatientTask.TabStateStyles.DisabledFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.TabStateStyles.FormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.TabStateStyles.HotFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.TabStateStyles.SelectedFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlPatientTask.Text = "All Patient Task";
            // 
            // uiPnlPatientTaskContainer
            // 
            this.uiPnlPatientTaskContainer.AutoScroll = true;
            this.uiPnlPatientTaskContainer.Controls.Add(this.panel3);
            this.uiPnlPatientTaskContainer.Location = new System.Drawing.Point(1, 23);
            this.uiPnlPatientTaskContainer.Name = "uiPnlPatientTaskContainer";
            this.uiPnlPatientTaskContainer.Size = new System.Drawing.Size(237, 581);
            this.uiPnlPatientTaskContainer.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.C1PatTask);
            this.panel3.Controls.Add(this.pnlselectall);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(237, 581);
            this.panel3.TabIndex = 8;
            // 
            // C1PatTask
            // 
            this.C1PatTask.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1PatTask.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1PatTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1PatTask.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1PatTask.ColumnInfo = "20,0,0,0,0,100,Columns:0{StyleFixed:\"TextAlign:CenterCenter;ImageAlign:CenterCent" +
    "er;\";}\t";
            this.C1PatTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1PatTask.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1PatTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1PatTask.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1PatTask.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1PatTask.Location = new System.Drawing.Point(0, 35);
            this.C1PatTask.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.C1PatTask.Name = "C1PatTask";
            this.C1PatTask.Rows.Count = 1;
            this.C1PatTask.Rows.DefaultSize = 20;
            this.C1PatTask.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1PatTask.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.C1PatTask.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None;
            this.C1PatTask.Size = new System.Drawing.Size(237, 546);
            this.C1PatTask.StyleInfo = resources.GetString("C1PatTask.StyleInfo");
            this.C1PatTask.TabIndex = 7;
            this.C1PatTask.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1PatTask_BeforeSelChange);
            this.C1PatTask.RowColChange += new System.EventHandler(this.C1PatTask_RowColChange);
            this.C1PatTask.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.C1PatTask_AfterEdit);
            this.C1PatTask.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1PatTask_MouseMove);
            // 
            // pnlselectall
            // 
            this.pnlselectall.Controls.Add(this.label23);
            this.pnlselectall.Controls.Add(this.btncomp);
            this.pnlselectall.Controls.Add(this.chkselectall);
            this.pnlselectall.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlselectall.Location = new System.Drawing.Point(0, 0);
            this.pnlselectall.Name = "pnlselectall";
            this.pnlselectall.Size = new System.Drawing.Size(237, 35);
            this.pnlselectall.TabIndex = 0;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(0, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(237, 1);
            this.label23.TabIndex = 45;
            this.label23.Text = "label2";
            // 
            // btncomp
            // 
            this.btncomp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btncomp.BackgroundImage = global::gloTaskMail.Properties.Resources.Img_Button;
            this.btncomp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncomp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncomp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncomp.Location = new System.Drawing.Point(90, 5);
            this.btncomp.Name = "btncomp";
            this.btncomp.Size = new System.Drawing.Size(133, 24);
            this.btncomp.TabIndex = 1;
            this.btncomp.Text = "Complete Selected Task";
            this.btncomp.UseVisualStyleBackColor = true;
            this.btncomp.Click += new System.EventHandler(this.btncomp_Click);
            // 
            // chkselectall
            // 
            this.chkselectall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chkselectall.AutoSize = true;
            this.chkselectall.BackColor = System.Drawing.Color.Transparent;
            this.chkselectall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkselectall.Location = new System.Drawing.Point(9, 8);
            this.chkselectall.Name = "chkselectall";
            this.chkselectall.Size = new System.Drawing.Size(76, 18);
            this.chkselectall.TabIndex = 0;
            this.chkselectall.Text = "Select All";
            this.chkselectall.UseVisualStyleBackColor = false;
            this.chkselectall.CheckedChanged += new System.EventHandler(this.chkselectall_CheckedChanged);
            // 
            // uiPnlTaskDetails
            // 
            this.uiPnlTaskDetails.ActiveCaptionMode = Janus.Windows.UI.Dock.ActiveCaptionMode.Never;
            this.uiPnlTaskDetails.AllowPanelDrag = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPnlTaskDetails.AllowPanelDrop = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPnlTaskDetails.AutoHideButtonVisible = Janus.Windows.UI.InheritableBoolean.True;
            this.uiPnlTaskDetails.CaptionDoubleClickAction = Janus.Windows.UI.Dock.CaptionDoubleClickAction.None;
            this.uiPnlTaskDetails.CaptionFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.uiPnlTaskDetails.CaptionVisible = Janus.Windows.UI.InheritableBoolean.True;
            this.uiPnlTaskDetails.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uiPnlTaskDetails.FloatingLocation = new System.Drawing.Point(25, 25);
            this.uiPnlTaskDetails.FloatingSize = new System.Drawing.Size(500, 500);
            this.uiPnlTaskDetails.Icon = ((System.Drawing.Icon)(resources.GetObject("uiPnlTaskDetails.Icon")));
            this.uiPnlTaskDetails.InnerContainer = this.uiPnlTaskDetailsContainer;
            this.uiPnlTaskDetails.Location = new System.Drawing.Point(246, 56);
            this.uiPnlTaskDetails.Name = "uiPnlTaskDetails";
            this.uiPnlTaskDetails.Size = new System.Drawing.Size(763, 605);
            this.uiPnlTaskDetails.TabIndex = 0;
            this.uiPnlTaskDetails.TabStateStyles.DisabledFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.TabStateStyles.FormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.TabStateStyles.HotFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.TabStateStyles.PressedFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.TabStateStyles.SelectedFormatStyle.FontBold = Janus.Windows.UI.TriState.True;
            this.uiPnlTaskDetails.Text = "Selected Task";
            // 
            // uiPnlTaskDetailsContainer
            // 
            this.uiPnlTaskDetailsContainer.Controls.Add(this.pnlFields);
            this.uiPnlTaskDetailsContainer.Location = new System.Drawing.Point(1, 23);
            this.uiPnlTaskDetailsContainer.Name = "uiPnlTaskDetailsContainer";
            this.uiPnlTaskDetailsContainer.Size = new System.Drawing.Size(761, 581);
            this.uiPnlTaskDetailsContainer.TabIndex = 0;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1012, 664);
            this.Controls.Add(this.uiPnlTaskDetails);
            this.Controls.Add(this.uiPnlPatientTask);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTask_FormClosing);
            this.Load += new System.EventHandler(this.frmTask_Load);
            this.pnlFields.ResumeLayout(false);
            this.pnlRtxtBox.ResumeLayout(false);
            this.pnlReminder.ResumeLayout(false);
            this.pnlReminder.PerformLayout();
            this.pnlTaskDetails.ResumeLayout(false);
            this.pnlTaskDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numComplete)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_AssignTo.ResumeLayout(false);
            this.pnl_AssignTo.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnlPatientTask)).EndInit();
            this.uiPnlPatientTask.ResumeLayout(false);
            this.uiPnlPatientTaskContainer.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1PatTask)).EndInit();
            this.pnlselectall.ResumeLayout(false);
            this.pnlselectall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPnlTaskDetails)).EndInit();
            this.uiPnlTaskDetails.ResumeLayout(false);
            this.uiPnlTaskDetailsContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_AssignTo;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel pnlFields;
        private System.Windows.Forms.Panel pnl_AssignTo;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.ComboBox cmb_To;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btn_ToDel;
        private System.Windows.Forms.Panel pnlRtxtBox;
        public  System.Windows.Forms.RichTextBox rtxtDescription;
        private System.Windows.Forms.Panel pnlReminder;
        private System.Windows.Forms.DateTimePicker dtpReminderDate;
        private System.Windows.Forms.CheckBox chkReminder;
        private System.Windows.Forms.Label lblReminderDate;
        private System.Windows.Forms.Label lblReminderTime;
        private System.Windows.Forms.DateTimePicker dtpReminderTime;
        private System.Windows.Forms.Panel pnlTaskDetails;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        public  System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btn_Providers;
        private System.Windows.Forms.ComboBox cmb_FollowUp;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.Label lblFollowUp;
        private System.Windows.Forms.Button btn_Patient;
        private System.Windows.Forms.NumericUpDown numComplete;
        private System.Windows.Forms.Label lblOwner;
        private System.Windows.Forms.Label lblComplete;
        private System.Windows.Forms.TextBox txtOwner;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.ToolStripButton tsb_ShowOrder;
        internal System.Windows.Forms.ToolStripButton tsb_ShowLab;
        internal System.Windows.Forms.ToolStripButton tsb_ShowDoc;
        internal System.Windows.Forms.ToolStripButton tsb_AcceptTask;
        internal System.Windows.Forms.ToolStripButton tsb_DeclineTask;
        internal System.Windows.Forms.ToolStripButton tsb_ShowExam;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label lbl_From;
        internal System.Windows.Forms.ToolStripButton tsb_MatchPatient;
        private System.Windows.Forms.ToolStripButton tsb_ShowCCD;
        private System.Windows.Forms.ToolStripButton tsb_FlowSheet;
        private System.Windows.Forms.ToolStripButton tsb_Drugs;
        private System.Windows.Forms.CheckBox ChkCompleteTaskforallUsers;
        internal System.Windows.Forms.ToolStripButton tsb_LabOrder;
        private System.Windows.Forms.ToolStripButton tsb_ViewReport;
        private System.Windows.Forms.ToolStripButton tsb_ReviewPatient;
        internal System.Windows.Forms.ToolStripButton tsb_Vitals;
        internal System.Windows.Forms.ToolStripButton tsb_LabReport;
        internal System.Windows.Forms.ToolStripButton tsb_ViewMessage;
        internal System.Windows.Forms.ToolStripButton tsb_Reply;
        internal System.Windows.Forms.ToolStripButton tsb_Forward;
        internal System.Windows.Forms.ToolStripButton tsb_PatientPayment;
        public System.Windows.Forms.ToolStripButton tsb_RxMeds;
        internal System.Windows.Forms.ToolStripButton tsb_Reconcile;
        private System.Windows.Forms.ToolStripButton tsb_MergeOrder;
        private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
        private Janus.Windows.UI.Dock.UIPanel uiPnlTaskDetails;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPnlTaskDetailsContainer;
        private Janus.Windows.UI.Dock.UIPanel uiPnlPatientTask;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPnlPatientTaskContainer;
        internal C1.Win.C1FlexGrid.C1FlexGrid C1PatTask;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ToolStripButton tsbbtn_OnlySave;
        private System.Windows.Forms.Button btncomp;
        private System.Windows.Forms.CheckBox chkselectall;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlselectall;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolStripButton tsb_ShowHistory;
        private System.Windows.Forms.Button btnreply;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ToolStripButton tsb_ShowROS;
        private System.Windows.Forms.ToolStripButton tsb_ReviewPHI;
    }
}
