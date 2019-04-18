namespace gloPMGeneral.gloAppointmentsChargesLinking
{
    partial class frmPatientAppointments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientAppointments));
            this.c1Appointments = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAppointmentHeader = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTransactionOther2 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_collection = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_SaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.tls_Close = new System.Windows.Forms.ToolStripButton();
            this.tls_ShowCheckInApointment = new System.Windows.Forms.ToolStripButton();
            this.tls_ShowCheckOutAppointment = new System.Windows.Forms.ToolStripButton();
            this.tls_ShowAllAppointment = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTransactionOther2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.ts_collection.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Appointments
            // 
            this.c1Appointments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows;
            this.c1Appointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Appointments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Appointments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Appointments.ColumnInfo = resources.GetString("c1Appointments.ColumnInfo");
            this.c1Appointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Appointments.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove;
            this.c1Appointments.DropMode = C1.Win.C1FlexGrid.DropModeEnum.Automatic;
            this.c1Appointments.ExtendLastCol = true;
            this.c1Appointments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Appointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Appointments.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.c1Appointments.Location = new System.Drawing.Point(3, 1);
            this.c1Appointments.Name = "c1Appointments";
            this.c1Appointments.Rows.Count = 1;
            this.c1Appointments.Rows.DefaultSize = 18;
            this.c1Appointments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Appointments.Size = new System.Drawing.Size(885, 499);
            this.c1Appointments.StyleInfo = resources.GetString("c1Appointments.StyleInfo");
            this.c1Appointments.TabIndex = 24;
            this.c1Appointments.BeforeSelChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Appointments_BeforeSelChange);
            this.c1Appointments.EnterCell += new System.EventHandler(this.c1Appointments_EnterCell);
            this.c1Appointments.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Appointments_AfterEdit);
            this.c1Appointments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Appointments_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.c1Appointments);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(891, 504);
            this.panel1.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(887, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 499);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 499);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 500);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(885, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(885, 1);
            this.label1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.ts_collection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(891, 53);
            this.panel2.TabIndex = 26;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(891, 33);
            this.panel3.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lblAppointmentHeader);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(885, 27);
            this.panel4.TabIndex = 0;
            // 
            // lblAppointmentHeader
            // 
            this.lblAppointmentHeader.AutoSize = true;
            this.lblAppointmentHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentHeader.Location = new System.Drawing.Point(9, 6);
            this.lblAppointmentHeader.Name = "lblAppointmentHeader";
            this.lblAppointmentHeader.Size = new System.Drawing.Size(228, 14);
            this.lblAppointmentHeader.TabIndex = 7;
            this.lblAppointmentHeader.Text = "Select appointment to inherit detail";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(1, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(883, 1);
            this.label8.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(883, 1);
            this.label7.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(884, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 27);
            this.label6.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 27);
            this.label5.TabIndex = 3;
            // 
            // pnlTransactionOther2
            // 
            this.pnlTransactionOther2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionOther2.Controls.Add(this.label30);
            this.pnlTransactionOther2.Controls.Add(this.label31);
            this.pnlTransactionOther2.Controls.Add(this.label34);
            this.pnlTransactionOther2.Controls.Add(this.label33);
            this.pnlTransactionOther2.Controls.Add(this.label32);
            this.pnlTransactionOther2.Controls.Add(this.label13);
            this.pnlTransactionOther2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTransactionOther2.Location = new System.Drawing.Point(0, 590);
            this.pnlTransactionOther2.Name = "pnlTransactionOther2";
            this.pnlTransactionOther2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTransactionOther2.Size = new System.Drawing.Size(891, 25);
            this.pnlTransactionOther2.TabIndex = 28;
            this.pnlTransactionOther2.TabStop = true;
            this.pnlTransactionOther2.Tag = "pnlTransactionOther2";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(53, 2);
            this.label30.Name = "label30";
            this.label30.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label30.Size = new System.Drawing.Size(43, 16);
            this.label30.TabIndex = 13;
            this.label30.Text = "- Save";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Maroon;
            this.label31.Location = new System.Drawing.Point(4, 2);
            this.label31.Name = "label31";
            this.label31.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label31.Size = new System.Drawing.Size(49, 16);
            this.label31.TabIndex = 12;
            this.label31.Text = "Ctrl + S";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(4, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(883, 1);
            this.label34.TabIndex = 16;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(4, 1);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(883, 1);
            this.label33.TabIndex = 0;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(887, 1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 21);
            this.label32.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 1);
            this.label13.Name = "label13";
            this.label13.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label13.Size = new System.Drawing.Size(1, 21);
            this.label13.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(891, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Tag = "menuStrip1";
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling_Save});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            this.mnuBilling.Visible = false;
            // 
            // mnuBilling_Save
            // 
            this.mnuBilling_Save.Name = "mnuBilling_Save";
            this.mnuBilling_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuBilling_Save.Size = new System.Drawing.Size(138, 22);
            this.mnuBilling_Save.Text = "Save";
            this.mnuBilling_Save.Click += new System.EventHandler(this.mnuBilling_Save_Click);
            // 
            // ts_collection
            // 
            this.ts_collection.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.ts_collection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_collection.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_collection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_SaveAndClose,
            this.tls_Close,
            this.tls_ShowCheckInApointment,
            this.tls_ShowCheckOutAppointment,
            this.tls_ShowAllAppointment});
            this.ts_collection.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_collection.Location = new System.Drawing.Point(0, 0);
            this.ts_collection.Name = "ts_collection";
            this.ts_collection.Size = new System.Drawing.Size(891, 53);
            this.ts_collection.TabIndex = 3;
            this.ts_collection.TabStop = true;
            this.ts_collection.Text = "toolStrip2";
            // 
            // tls_SaveAndClose
            // 
            this.tls_SaveAndClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_SaveAndClose.Image")));
            this.tls_SaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_SaveAndClose.Name = "tls_SaveAndClose";
            this.tls_SaveAndClose.Size = new System.Drawing.Size(66, 50);
            this.tls_SaveAndClose.Text = "Sa&ve&&Cls";
            this.tls_SaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_SaveAndClose.ToolTipText = "Save and Close";
            this.tls_SaveAndClose.Click += new System.EventHandler(this.tls_SaveAndClose_Click);
            // 
            // tls_Close
            // 
            this.tls_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Close.Image = ((System.Drawing.Image)(resources.GetObject("tls_Close.Image")));
            this.tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_Close.Name = "tls_Close";
            this.tls_Close.Size = new System.Drawing.Size(43, 50);
            this.tls_Close.Text = "&Close";
            this.tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_Close.Click += new System.EventHandler(this.tls_Close_Click);
            // 
            // tls_ShowCheckInApointment
            // 
            this.tls_ShowCheckInApointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ShowCheckInApointment.Image = ((System.Drawing.Image)(resources.GetObject("tls_ShowCheckInApointment.Image")));
            this.tls_ShowCheckInApointment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ShowCheckInApointment.Name = "tls_ShowCheckInApointment";
            this.tls_ShowCheckInApointment.Size = new System.Drawing.Size(182, 50);
            this.tls_ShowCheckInApointment.Text = "Show Check-In Apointment";
            this.tls_ShowCheckInApointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ShowCheckInApointment.ToolTipText = "Save and Close";
            this.tls_ShowCheckInApointment.Visible = false;
            // 
            // tls_ShowCheckOutAppointment
            // 
            this.tls_ShowCheckOutAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ShowCheckOutAppointment.Image = ((System.Drawing.Image)(resources.GetObject("tls_ShowCheckOutAppointment.Image")));
            this.tls_ShowCheckOutAppointment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ShowCheckOutAppointment.Name = "tls_ShowCheckOutAppointment";
            this.tls_ShowCheckOutAppointment.Size = new System.Drawing.Size(192, 50);
            this.tls_ShowCheckOutAppointment.Text = "Show Check-Out Apointment";
            this.tls_ShowCheckOutAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ShowCheckOutAppointment.ToolTipText = "Save and Close";
            this.tls_ShowCheckOutAppointment.Visible = false;
            // 
            // tls_ShowAllAppointment
            // 
            this.tls_ShowAllAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_ShowAllAppointment.Image = ((System.Drawing.Image)(resources.GetObject("tls_ShowAllAppointment.Image")));
            this.tls_ShowAllAppointment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_ShowAllAppointment.Name = "tls_ShowAllAppointment";
            this.tls_ShowAllAppointment.Size = new System.Drawing.Size(151, 50);
            this.tls_ShowAllAppointment.Text = "Show All Appointment";
            this.tls_ShowAllAppointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_ShowAllAppointment.ToolTipText = "Save and Close";
            this.tls_ShowAllAppointment.Visible = false;
            // 
            // frmPatientAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(891, 615);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlTransactionOther2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientAppointments";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Appointments";
            this.Load += new System.EventHandler(this.frmPatientAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Appointments)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTransactionOther2.ResumeLayout(false);
            this.pnlTransactionOther2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ts_collection.ResumeLayout(false);
            this.ts_collection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus ts_collection;
        private System.Windows.Forms.ToolStripButton tls_SaveAndClose;
        private System.Windows.Forms.ToolStripButton tls_Close;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Appointments;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ToolStripButton tls_ShowCheckInApointment;
        private System.Windows.Forms.ToolStripButton tls_ShowCheckOutAppointment;
        private System.Windows.Forms.ToolStripButton tls_ShowAllAppointment;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAppointmentHeader;
        private System.Windows.Forms.Panel pnlTransactionOther2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_Save;


    }
}