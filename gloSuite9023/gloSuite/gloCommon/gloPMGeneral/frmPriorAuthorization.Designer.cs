namespace gloPMGeneral
{
    partial class frmPriorAuthorization
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtAuthorizationDate, dtAuthorizationThroughDate, dtAuthorizationStatusDate };
            System.Windows.Forms.Control[] cntControls = { dtAuthorizationDate, dtAuthorizationThroughDate, dtAuthorizationStatusDate };

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


            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPriorAuthorization));
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_Clear = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmb_Insurances = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtVisitsMade = new System.Windows.Forms.TextBox();
            this.lblVisitsLeft = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtAuthorizationStatusDate = new System.Windows.Forms.DateTimePicker();
            this.cbAuthorizationStatus = new System.Windows.Forms.ComboBox();
            this.dtAuthorizationThroughDate = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbAppointmentType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTotalVisits = new System.Windows.Forms.TextBox();
            this.txtAuthorizationNumber = new System.Windows.Forms.TextBox();
            this.dtAuthorizationDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_PriorAuthorizations = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1PriorAuthorization = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.cmnu_PriorAuthorization = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modiFyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_tlspTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_PriorAuthorizations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PriorAuthorization)).BeginInit();
            this.cmnu_PriorAuthorization.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.ts_Commands);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(647, 54);
            this.pnl_tlspTOP.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsb_Clear,
            this.tsb_Save,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(647, 53);
            this.ts_Commands.TabIndex = 8;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 50);
            this.toolStripButton1.Tag = "CheckIn";
            this.toolStripButton1.Text = "Check&In";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Patient CheckIn ";
            this.toolStripButton1.Visible = false;
            // 
            // tsb_Clear
            // 
            this.tsb_Clear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Clear.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Clear.Image")));
            this.tsb_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Clear.Name = "tsb_Clear";
            this.tsb_Clear.Size = new System.Drawing.Size(41, 50);
            this.tsb_Clear.Tag = "Clear";
            this.tsb_Clear.Text = "C&lear";
            this.tsb_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.cmb_Insurances);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.dtAuthorizationStatusDate);
            this.panel3.Controls.Add(this.cbAuthorizationStatus);
            this.panel3.Controls.Add(this.dtAuthorizationThroughDate);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.cmbAppointmentType);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.txtTotalVisits);
            this.panel3.Controls.Add(this.txtAuthorizationNumber);
            this.panel3.Controls.Add(this.dtAuthorizationDate);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(647, 164);
            this.panel3.TabIndex = 0;
            // 
            // cmb_Insurances
            // 
            this.cmb_Insurances.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Insurances.ForeColor = System.Drawing.Color.Black;
            this.cmb_Insurances.FormattingEnabled = true;
            this.cmb_Insurances.Location = new System.Drawing.Point(500, 129);
            this.cmb_Insurances.Name = "cmb_Insurances";
            this.cmb_Insurances.Size = new System.Drawing.Size(128, 22);
            this.cmb_Insurances.TabIndex = 8;
            this.cmb_Insurances.SelectionChangeCommitted += new System.EventHandler(this.cmb_Insurances_SelectionChangeCommitted);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(424, 133);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 14);
            this.label18.TabIndex = 18;
            this.label18.Text = "Insurances :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.txtVisitsMade);
            this.panel1.Controls.Add(this.lblVisitsLeft);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(16, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 30);
            this.panel1.TabIndex = 0;
            // 
            // txtVisitsMade
            // 
            this.txtVisitsMade.ForeColor = System.Drawing.Color.Black;
            this.txtVisitsMade.Location = new System.Drawing.Point(92, 4);
            this.txtVisitsMade.MaxLength = 3;
            this.txtVisitsMade.Name = "txtVisitsMade";
            this.txtVisitsMade.Size = new System.Drawing.Size(38, 22);
            this.txtVisitsMade.TabIndex = 0;
            this.txtVisitsMade.TextChanged += new System.EventHandler(this.txtVisitsMade_TextChanged);
            this.txtVisitsMade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVisitsMade_KeyPress);
            // 
            // lblVisitsLeft
            // 
            this.lblVisitsLeft.BackColor = System.Drawing.Color.White;
            this.lblVisitsLeft.ForeColor = System.Drawing.Color.Black;
            this.lblVisitsLeft.Location = new System.Drawing.Point(208, 3);
            this.lblVisitsLeft.Name = "lblVisitsLeft";
            this.lblVisitsLeft.Size = new System.Drawing.Size(31, 23);
            this.lblVisitsLeft.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(142, 8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 14);
            this.label14.TabIndex = 9;
            this.label14.Text = "Visit left :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(7, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "Visits Made :";
            // 
            // dtAuthorizationStatusDate
            // 
            this.dtAuthorizationStatusDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtAuthorizationStatusDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtAuthorizationStatusDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtAuthorizationStatusDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtAuthorizationStatusDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtAuthorizationStatusDate.CustomFormat = "MM/dd/yyyy";
            this.dtAuthorizationStatusDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAuthorizationStatusDate.Location = new System.Drawing.Point(172, 129);
            this.dtAuthorizationStatusDate.Name = "dtAuthorizationStatusDate";
            this.dtAuthorizationStatusDate.Size = new System.Drawing.Size(128, 22);
            this.dtAuthorizationStatusDate.TabIndex = 4;
            // 
            // cbAuthorizationStatus
            // 
            this.cbAuthorizationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthorizationStatus.ForeColor = System.Drawing.Color.Black;
            this.cbAuthorizationStatus.FormattingEnabled = true;
            this.cbAuthorizationStatus.Location = new System.Drawing.Point(500, 100);
            this.cbAuthorizationStatus.Name = "cbAuthorizationStatus";
            this.cbAuthorizationStatus.Size = new System.Drawing.Size(128, 22);
            this.cbAuthorizationStatus.TabIndex = 7;
            // 
            // dtAuthorizationThroughDate
            // 
            this.dtAuthorizationThroughDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtAuthorizationThroughDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtAuthorizationThroughDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtAuthorizationThroughDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtAuthorizationThroughDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtAuthorizationThroughDate.CustomFormat = "MM/dd/yyyy";
            this.dtAuthorizationThroughDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAuthorizationThroughDate.Location = new System.Drawing.Point(172, 71);
            this.dtAuthorizationThroughDate.Name = "dtAuthorizationThroughDate";
            this.dtAuthorizationThroughDate.Size = new System.Drawing.Size(128, 22);
            this.dtAuthorizationThroughDate.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(13, 133);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(156, 14);
            this.label17.TabIndex = 14;
            this.label17.Text = "Authorization Status Date :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Location = new System.Drawing.Point(371, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(126, 14);
            this.label16.TabIndex = 12;
            this.label16.Text = "Authorization Status :";
            // 
            // cmbAppointmentType
            // 
            this.cmbAppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbAppointmentType.FormattingEnabled = true;
            this.cmbAppointmentType.Location = new System.Drawing.Point(172, 100);
            this.cmbAppointmentType.Name = "cmbAppointmentType";
            this.cmbAppointmentType.Size = new System.Drawing.Size(193, 22);
            this.cmbAppointmentType.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(50, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 14);
            this.label15.TabIndex = 10;
            this.label15.Text = "Appointment Type :";
            // 
            // txtTotalVisits
            // 
            this.txtTotalVisits.ForeColor = System.Drawing.Color.Black;
            this.txtTotalVisits.Location = new System.Drawing.Point(500, 71);
            this.txtTotalVisits.MaxLength = 3;
            this.txtTotalVisits.Name = "txtTotalVisits";
            this.txtTotalVisits.Size = new System.Drawing.Size(53, 22);
            this.txtTotalVisits.TabIndex = 6;
            this.txtTotalVisits.TextChanged += new System.EventHandler(this.txtTotalVisits_TextChanged);
            this.txtTotalVisits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalVisits_KeyPress);
            // 
            // txtAuthorizationNumber
            // 
            this.txtAuthorizationNumber.ForeColor = System.Drawing.Color.Black;
            this.txtAuthorizationNumber.Location = new System.Drawing.Point(500, 42);
            this.txtAuthorizationNumber.MaxLength = 20;
            this.txtAuthorizationNumber.Name = "txtAuthorizationNumber";
            this.txtAuthorizationNumber.Size = new System.Drawing.Size(128, 22);
            this.txtAuthorizationNumber.TabIndex = 5;
            // 
            // dtAuthorizationDate
            // 
            this.dtAuthorizationDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtAuthorizationDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtAuthorizationDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtAuthorizationDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtAuthorizationDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtAuthorizationDate.CustomFormat = "MM/dd/yyyy";
            this.dtAuthorizationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAuthorizationDate.Location = new System.Drawing.Point(172, 42);
            this.dtAuthorizationDate.Name = "dtAuthorizationDate";
            this.dtAuthorizationDate.Size = new System.Drawing.Size(128, 22);
            this.dtAuthorizationDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(313, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = " Number of Authorization Visits :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(363, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Authorization Number :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(31, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Authorization Through :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(37, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date of Authorization :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(4, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(639, 1);
            this.label5.TabIndex = 4;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 157);
            this.label6.TabIndex = 3;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(643, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 157);
            this.label7.TabIndex = 2;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(641, 1);
            this.label8.TabIndex = 0;
            this.label8.Text = "label1";
            // 
            // pnl_PriorAuthorizations
            // 
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_BottomBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_LeftBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_RightBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.lbl_TopBrd);
            this.pnl_PriorAuthorizations.Controls.Add(this.c1PriorAuthorization);
            this.pnl_PriorAuthorizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PriorAuthorizations.Location = new System.Drawing.Point(0, 218);
            this.pnl_PriorAuthorizations.Name = "pnl_PriorAuthorizations";
            this.pnl_PriorAuthorizations.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_PriorAuthorizations.Size = new System.Drawing.Size(647, 187);
            this.pnl_PriorAuthorizations.TabIndex = 1;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 183);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(639, 1);
            this.lbl_BottomBrd.TabIndex = 14;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 183);
            this.lbl_LeftBrd.TabIndex = 13;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(643, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 183);
            this.lbl_RightBrd.TabIndex = 12;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(641, 1);
            this.lbl_TopBrd.TabIndex = 11;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1PriorAuthorization
            // 
            this.c1PriorAuthorization.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PriorAuthorization.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1PriorAuthorization.AutoGenerateColumns = false;
            this.c1PriorAuthorization.AutoResize = false;
            this.c1PriorAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PriorAuthorization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PriorAuthorization.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PriorAuthorization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PriorAuthorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PriorAuthorization.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PriorAuthorization.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PriorAuthorization.Location = new System.Drawing.Point(3, 0);
            this.c1PriorAuthorization.Name = "c1PriorAuthorization";
            this.c1PriorAuthorization.Rows.Count = 1;
            this.c1PriorAuthorization.Rows.DefaultSize = 19;
            this.c1PriorAuthorization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PriorAuthorization.Size = new System.Drawing.Size(641, 184);
            this.c1PriorAuthorization.StyleInfo = resources.GetString("c1PriorAuthorization.StyleInfo");
            this.c1PriorAuthorization.TabIndex = 0;
            this.c1PriorAuthorization.Click += new System.EventHandler(this.c1PriorAuthorization_Click);
            this.c1PriorAuthorization.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PriorAuthorization_MouseDown);
            this.c1PriorAuthorization.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PriorAuthorization_MouseMove);
            this.c1PriorAuthorization.DoubleClick += new System.EventHandler(this.c1PriorAuthorization_DoubleClick);
            // 
            // cmnu_PriorAuthorization
            // 
            this.cmnu_PriorAuthorization.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_PriorAuthorization.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modiFyToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmnu_PriorAuthorization.Name = "cmnu_Appointment";
            this.cmnu_PriorAuthorization.Size = new System.Drawing.Size(118, 48);
            // 
            // modiFyToolStripMenuItem
            // 
            this.modiFyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("modiFyToolStripMenuItem.Image")));
            this.modiFyToolStripMenuItem.Name = "modiFyToolStripMenuItem";
            this.modiFyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.modiFyToolStripMenuItem.Text = "Modify";
            this.modiFyToolStripMenuItem.Click += new System.EventHandler(this.modiFyToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPriorAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(647, 405);
            this.Controls.Add(this.pnl_PriorAuthorizations);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPriorAuthorization";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prior Authorization";
            this.Load += new System.EventHandler(this.frmPriorAuthorization_Load);
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_PriorAuthorizations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PriorAuthorization)).EndInit();
            this.cmnu_PriorAuthorization.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlspTOP;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtAuthorizationDate;
        private System.Windows.Forms.TextBox txtTotalVisits;
        private System.Windows.Forms.TextBox txtAuthorizationNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblVisitsLeft;
        private System.Windows.Forms.Label label14;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.TextBox txtVisitsMade;
        private System.Windows.Forms.ComboBox cmbAppointmentType;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DateTimePicker dtAuthorizationThroughDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtAuthorizationStatusDate;
        private System.Windows.Forms.ComboBox cbAuthorizationStatus;
        private System.Windows.Forms.Panel pnl_PriorAuthorizations;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PriorAuthorization;
        private System.Windows.Forms.ComboBox cmb_Insurances;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.ToolStripButton tsb_Clear;
        private System.Windows.Forms.ContextMenuStrip cmnu_PriorAuthorization;
        private System.Windows.Forms.ToolStripMenuItem modiFyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}