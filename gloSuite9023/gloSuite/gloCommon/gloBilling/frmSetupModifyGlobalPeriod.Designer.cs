namespace gloBilling
{
    partial class frmSetupModifyGlobalPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupModifyGlobalPeriod));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblCreatedCaption = new System.Windows.Forms.Label();
            this.panel30 = new System.Windows.Forms.Panel();
            this.c1FlexGridChargesClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label59 = new System.Windows.Forms.Label();
            this.panel31 = new System.Windows.Forms.Panel();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBillingReminder = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbPeriodDays = new System.Windows.Forms.ComboBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblDaysCaption = new System.Windows.Forms.Label();
            this.lblEndDateCaption = new System.Windows.Forms.Label();
            this.lblCPTDesc = new System.Windows.Forms.Label();
            this.txtCpt = new System.Windows.Forms.TextBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblPatientCaption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel1.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridChargesClaims)).BeginInit();
            this.panel31.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 54);
            this.panel1.TabIndex = 136;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Modify,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(850, 53);
            this.ts_Commands.TabIndex = 137;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(106, 50);
            this.tsb_Modify.Text = "M&odify Charges";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "Sa&ve&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // lblCreated
            // 
            this.lblCreated.AutoSize = true;
            this.lblCreated.BackColor = System.Drawing.Color.Transparent;
            this.lblCreated.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreated.Location = new System.Drawing.Point(89, 258);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(0, 14);
            this.lblCreated.TabIndex = 146;
            this.lblCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreatedCaption
            // 
            this.lblCreatedCaption.AutoSize = true;
            this.lblCreatedCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCreatedCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCreatedCaption.Location = new System.Drawing.Point(15, 258);
            this.lblCreatedCaption.Name = "lblCreatedCaption";
            this.lblCreatedCaption.Size = new System.Drawing.Size(76, 14);
            this.lblCreatedCaption.TabIndex = 145;
            this.lblCreatedCaption.Text = "Created on :";
            this.lblCreatedCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel30
            // 
            this.panel30.Controls.Add(this.c1FlexGridChargesClaims);
            this.panel30.Controls.Add(this.label59);
            this.panel30.Controls.Add(this.panel31);
            this.panel30.Controls.Add(this.label64);
            this.panel30.Controls.Add(this.label84);
            this.panel30.Controls.Add(this.label85);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel30.Location = new System.Drawing.Point(0, 337);
            this.panel30.Name = "panel30";
            this.panel30.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel30.Size = new System.Drawing.Size(850, 261);
            this.panel30.TabIndex = 114;
            // 
            // c1FlexGridChargesClaims
            // 
            this.c1FlexGridChargesClaims.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FlexGridChargesClaims.AllowEditing = false;
            this.c1FlexGridChargesClaims.AutoGenerateColumns = false;
            this.c1FlexGridChargesClaims.BackColor = System.Drawing.Color.White;
            this.c1FlexGridChargesClaims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FlexGridChargesClaims.ColumnInfo = resources.GetString("c1FlexGridChargesClaims.ColumnInfo");
            this.c1FlexGridChargesClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGridChargesClaims.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FlexGridChargesClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FlexGridChargesClaims.Location = new System.Drawing.Point(4, 27);
            this.c1FlexGridChargesClaims.Name = "c1FlexGridChargesClaims";
            this.c1FlexGridChargesClaims.Padding = new System.Windows.Forms.Padding(3);
            this.c1FlexGridChargesClaims.Rows.Count = 1;
            this.c1FlexGridChargesClaims.Rows.DefaultSize = 19;
            this.c1FlexGridChargesClaims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGridChargesClaims.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1FlexGridChargesClaims.Size = new System.Drawing.Size(842, 230);
            this.c1FlexGridChargesClaims.StyleInfo = resources.GetString("c1FlexGridChargesClaims.StyleInfo");
            this.c1FlexGridChargesClaims.TabIndex = 113;
            this.c1FlexGridChargesClaims.Tag = "ClosePeriod";
            this.c1FlexGridChargesClaims.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1FlexGridChargesClaims_AfterSort);
            this.c1FlexGridChargesClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GlobalPeriods_MouseDoubleClick);
            this.c1FlexGridChargesClaims.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FlexGridChargesClaims_MouseMove);
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(4, 257);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(842, 1);
            this.label59.TabIndex = 111;
            this.label59.Text = "label4";
            // 
            // panel31
            // 
            this.panel31.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel31.Controls.Add(this.label62);
            this.panel31.Controls.Add(this.label63);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel31.Location = new System.Drawing.Point(4, 1);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(842, 26);
            this.panel31.TabIndex = 5;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(842, 25);
            this.label62.TabIndex = 6;
            this.label62.Text = "  Charges";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label63.Location = new System.Drawing.Point(0, 25);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(842, 1);
            this.label63.TabIndex = 5;
            this.label63.Text = "label2";
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Left;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label64.Location = new System.Drawing.Point(3, 1);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(1, 257);
            this.label64.TabIndex = 109;
            this.label64.Text = "label2";
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Top;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(3, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(843, 1);
            this.label84.TabIndex = 112;
            this.label84.Text = "label1";
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Dock = System.Windows.Forms.DockStyle.Right;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label85.Location = new System.Drawing.Point(846, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(1, 258);
            this.label85.TabIndex = 110;
            this.label85.Text = "label2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtBillingReminder);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.cmbPeriodDays);
            this.panel3.Controls.Add(this.lblCreated);
            this.panel3.Controls.Add(this.lblEndDate);
            this.panel3.Controls.Add(this.lblDaysCaption);
            this.panel3.Controls.Add(this.lblEndDateCaption);
            this.panel3.Controls.Add(this.lblCPTDesc);
            this.panel3.Controls.Add(this.txtCpt);
            this.panel3.Controls.Add(this.cmbProvider);
            this.panel3.Controls.Add(this.cmbInsurance);
            this.panel3.Controls.Add(this.lblPatient);
            this.panel3.Controls.Add(this.lblPatientCaption);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtNote);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.dtpStartDate);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lbl_LeftBrd);
            this.panel3.Controls.Add(this.lblCreatedCaption);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel3.Size = new System.Drawing.Size(850, 280);
            this.panel3.TabIndex = 147;
            // 
            // txtBillingReminder
            // 
            this.txtBillingReminder.BackColor = System.Drawing.Color.Transparent;
            this.txtBillingReminder.Location = new System.Drawing.Point(97, 96);
            this.txtBillingReminder.Name = "txtBillingReminder";
            this.txtBillingReminder.Size = new System.Drawing.Size(327, 155);
            this.txtBillingReminder.TabIndex = 174;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(605, 68);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 14);
            this.label10.TabIndex = 173;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(41, 69);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 14);
            this.label13.TabIndex = 172;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPeriodDays
            // 
            this.cmbPeriodDays.FormattingEnabled = true;
            this.cmbPeriodDays.Location = new System.Drawing.Point(658, 66);
            this.cmbPeriodDays.MaxLength = 4;
            this.cmbPeriodDays.Name = "cmbPeriodDays";
            this.cmbPeriodDays.Size = new System.Drawing.Size(63, 22);
            this.cmbPeriodDays.TabIndex = 171;
            this.cmbPeriodDays.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodDays_SelectedIndexChanged);
            this.cmbPeriodDays.TextChanged += new System.EventHandler(this.cmbPeriodDays_TextChanged);
            this.cmbPeriodDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPeriodDays_KeyPress);
            this.cmbPeriodDays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbPeriodDays_MouseDown);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDate.Location = new System.Drawing.Point(758, 69);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(73, 14);
            this.lblEndDate.TabIndex = 164;
            this.lblEndDate.Text = "09/14/2011";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDaysCaption
            // 
            this.lblDaysCaption.AutoSize = true;
            this.lblDaysCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblDaysCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDaysCaption.Location = new System.Drawing.Point(616, 69);
            this.lblDaysCaption.Name = "lblDaysCaption";
            this.lblDaysCaption.Size = new System.Drawing.Size(40, 14);
            this.lblDaysCaption.TabIndex = 156;
            this.lblDaysCaption.Text = "Days :";
            this.lblDaysCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDateCaption
            // 
            this.lblEndDateCaption.AutoSize = true;
            this.lblEndDateCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDateCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEndDateCaption.Location = new System.Drawing.Point(723, 69);
            this.lblEndDateCaption.Name = "lblEndDateCaption";
            this.lblEndDateCaption.Size = new System.Drawing.Size(36, 14);
            this.lblEndDateCaption.TabIndex = 157;
            this.lblEndDateCaption.Text = "End :";
            this.lblEndDateCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCPTDesc
            // 
            this.lblCPTDesc.AutoEllipsis = true;
            this.lblCPTDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPTDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTDesc.Location = new System.Drawing.Point(176, 69);
            this.lblCPTDesc.Name = "lblCPTDesc";
            this.lblCPTDesc.Size = new System.Drawing.Size(248, 15);
            this.lblCPTDesc.TabIndex = 170;
            this.lblCPTDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCpt
            // 
            this.txtCpt.Location = new System.Drawing.Point(95, 65);
            this.txtCpt.MaxLength = 10;
            this.txtCpt.Name = "txtCpt";
            this.txtCpt.Size = new System.Drawing.Size(79, 22);
            this.txtCpt.TabIndex = 169;
            this.txtCpt.TextChanged += new System.EventHandler(this.txtCpt_TextChanged);
            this.txtCpt.Enter += new System.EventHandler(this.txtCpt_Enter);
            this.txtCpt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCpt_KeyUp);
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(491, 37);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(340, 22);
            this.cmbProvider.TabIndex = 168;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.Location = new System.Drawing.Point(95, 37);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(329, 22);
            this.cmbInsurance.TabIndex = 167;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatient.Location = new System.Drawing.Point(95, 17);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(0, 14);
            this.lblPatient.TabIndex = 166;
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientCaption
            // 
            this.lblPatientCaption.AutoSize = true;
            this.lblPatientCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatientCaption.Location = new System.Drawing.Point(37, 17);
            this.lblPatientCaption.Name = "lblPatientCaption";
            this.lblPatientCaption.Size = new System.Drawing.Size(54, 14);
            this.lblPatientCaption.TabIndex = 165;
            this.lblPatientCaption.Text = "Patient :";
            this.lblPatientCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(23, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.TabIndex = 153;
            this.label1.Text = "Insurance :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(54, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 154;
            this.label2.Text = "CPT :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(446, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 155;
            this.label3.Text = "Start :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(429, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 14);
            this.label6.TabIndex = 158;
            this.label6.Text = "Provider :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(491, 96);
            this.txtNote.MaxLength = 255;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(327, 155);
            this.txtNote.TabIndex = 163;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(25, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 159;
            this.label7.Text = "Reminder :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(441, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 160;
            this.label8.Text = "Note :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(491, 65);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(111, 22);
            this.dtpStartDate.TabIndex = 161;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 279);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(842, 1);
            this.label9.TabIndex = 150;
            this.label9.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(842, 1);
            this.label5.TabIndex = 149;
            this.label5.Text = "label1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 277);
            this.label4.TabIndex = 148;
            this.label4.Text = "label4";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(846, 3);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 277);
            this.lbl_LeftBrd.TabIndex = 147;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 334);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(850, 3);
            this.splitter1.TabIndex = 148;
            this.splitter1.TabStop = false;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(94, 145);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(329, 192);
            this.pnlInternalControl.TabIndex = 172;
            this.pnlInternalControl.Visible = false;
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupModifyGlobalPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(850, 598);
            this.Controls.Add(this.panel30);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlInternalControl);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupModifyGlobalPeriod";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify Global Period";
            this.Load += new System.EventHandler(this.frmSetupModifyGlobalPeriod_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel30.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGridChargesClaims)).EndInit();
            this.panel31.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel panel31;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        internal System.Windows.Forms.Label lblCreated;
        internal System.Windows.Forms.Label lblCreatedCaption;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGridChargesClaims;
        private System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.Label lblEndDate;
        internal System.Windows.Forms.Label lblDaysCaption;
        internal System.Windows.Forms.Label lblEndDateCaption;
        internal System.Windows.Forms.Label lblCPTDesc;
        private System.Windows.Forms.TextBox txtCpt;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Label lblPatient;
        internal System.Windows.Forms.Label lblPatientCaption;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNote;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.ComboBox cmbPeriodDays;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label txtBillingReminder;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
    }
}