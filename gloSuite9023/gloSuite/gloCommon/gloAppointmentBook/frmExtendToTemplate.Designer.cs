namespace gloAppointmentBook
{
    partial class frmExtendToTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtendToTemplate));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTemplateAllocation = new System.Windows.Forms.Panel();
            this.pnlTemplate = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CalendarTemplate = new Janus.Windows.Schedule.Schedule();
            this.CalendarTemplate_1 = new Janus.Windows.Schedule.Schedule();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.lblProvider = new System.Windows.Forms.Label();
            this.gbSimpleAllocation = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbTemplates = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbl_pnlTemplateAllocationBottomBrd = new System.Windows.Forms.Label();
            this.pnlTemplateDetailsHeader = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblTemplateDetails = new System.Windows.Forms.Label();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.rbRecurrence = new System.Windows.Forms.RadioButton();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlTemplateAllocation.SuspendLayout();
            this.pnlTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate_1)).BeginInit();
            this.panel2.SuspendLayout();
            this.gbSimpleAllocation.SuspendLayout();
            this.pnlTemplateDetailsHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(945, 54);
            this.pnlToolStrip.TabIndex = 18;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(945, 53);
            this.ts_Commands.TabIndex = 15;
            this.ts_Commands.Text = "ToolStrip1";
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
            this.tsb_OK.Text = "&Save&&Cls";
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
            this.tsb_Cancel.ToolTipText = "Close";
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlTemplateAllocation
            // 
            this.pnlTemplateAllocation.Controls.Add(this.pnlTemplate);
            this.pnlTemplateAllocation.Controls.Add(this.panel2);
            this.pnlTemplateAllocation.Controls.Add(this.btnCancel);
            this.pnlTemplateAllocation.Controls.Add(this.btnSave);
            this.pnlTemplateAllocation.Controls.Add(this.lbl_pnlTemplateAllocationBottomBrd);
            this.pnlTemplateAllocation.Controls.Add(this.pnlTemplateDetailsHeader);
            this.pnlTemplateAllocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateAllocation.Location = new System.Drawing.Point(0, 54);
            this.pnlTemplateAllocation.Name = "pnlTemplateAllocation";
            this.pnlTemplateAllocation.Size = new System.Drawing.Size(945, 756);
            this.pnlTemplateAllocation.TabIndex = 28;
            // 
            // pnlTemplate
            // 
            this.pnlTemplate.Controls.Add(this.label1);
            this.pnlTemplate.Controls.Add(this.label2);
            this.pnlTemplate.Controls.Add(this.label3);
            this.pnlTemplate.Controls.Add(this.label4);
            this.pnlTemplate.Controls.Add(this.CalendarTemplate);
            this.pnlTemplate.Controls.Add(this.CalendarTemplate_1);
            this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplate.Location = new System.Drawing.Point(0, 153);
            this.pnlTemplate.Name = "pnlTemplate";
            this.pnlTemplate.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTemplate.Size = new System.Drawing.Size(945, 602);
            this.pnlTemplate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 598);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(937, 1);
            this.label1.TabIndex = 19;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 598);
            this.label2.TabIndex = 18;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(941, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 598);
            this.label3.TabIndex = 17;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(939, 1);
            this.label4.TabIndex = 16;
            this.label4.Text = "label1";
            // 
            // CalendarTemplate
            // 
            this.CalendarTemplate.AcceptsTab = false;
            this.CalendarTemplate.AddNewMode = Janus.Windows.Schedule.AddNewMode.Manual;
            this.CalendarTemplate.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None;
            this.CalendarTemplate.AllowEdit = false;
            this.CalendarTemplate.BorderStyle = Janus.Windows.Schedule.BorderStyle.None;
            this.CalendarTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarTemplate.HorizontalScrollPosition = 0;
            this.CalendarTemplate.Location = new System.Drawing.Point(3, 0);
            this.CalendarTemplate.Name = "CalendarTemplate";
            this.CalendarTemplate.Size = new System.Drawing.Size(939, 599);
            this.CalendarTemplate.TabIndex = 15;
            this.CalendarTemplate.VerticalScrollPosition = 16;
            this.CalendarTemplate.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            // 
            // CalendarTemplate_1
            // 
            this.CalendarTemplate_1.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None;
            this.CalendarTemplate_1.AllowEdit = false;
            this.CalendarTemplate_1.BorderStyle = Janus.Windows.Schedule.BorderStyle.None;
            this.CalendarTemplate_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarTemplate_1.HorizontalScrollPosition = 0;
            this.CalendarTemplate_1.Location = new System.Drawing.Point(3, 0);
            this.CalendarTemplate_1.Name = "CalendarTemplate_1";
            this.CalendarTemplate_1.Size = new System.Drawing.Size(939, 599);
            this.CalendarTemplate_1.TabIndex = 1;
            this.CalendarTemplate_1.VerticalScrollPosition = 16;
            this.CalendarTemplate_1.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.cmbProvider);
            this.panel2.Controls.Add(this.lblTemplate);
            this.panel2.Controls.Add(this.lblProvider);
            this.panel2.Controls.Add(this.gbSimpleAllocation);
            this.panel2.Controls.Add(this.cmbTemplates);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(945, 125);
            this.panel2.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(4, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(937, 1);
            this.label9.TabIndex = 141;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 121);
            this.label10.TabIndex = 140;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(941, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 121);
            this.label11.TabIndex = 139;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(939, 1);
            this.label12.TabIndex = 138;
            this.label12.Text = "label1";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(77, 14);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(312, 23);
            this.cmbProvider.TabIndex = 0;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplate.Location = new System.Drawing.Point(398, 21);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(67, 14);
            this.lblTemplate.TabIndex = 1;
            this.lblTemplate.Text = "Template :";
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(15, 18);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(59, 14);
            this.lblProvider.TabIndex = 137;
            this.lblProvider.Text = "Provider :";
            // 
            // gbSimpleAllocation
            // 
            this.gbSimpleAllocation.Controls.Add(this.dtpEndDate);
            this.gbSimpleAllocation.Controls.Add(this.lblEndDate);
            this.gbSimpleAllocation.Controls.Add(this.dtpStartDate);
            this.gbSimpleAllocation.Controls.Add(this.lblStartDate);
            this.gbSimpleAllocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSimpleAllocation.Location = new System.Drawing.Point(15, 45);
            this.gbSimpleAllocation.Name = "gbSimpleAllocation";
            this.gbSimpleAllocation.Size = new System.Drawing.Size(765, 50);
            this.gbSimpleAllocation.TabIndex = 2;
            this.gbSimpleAllocation.TabStop = false;
            this.gbSimpleAllocation.Text = "Date";
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
            this.dtpEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(351, 18);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(138, 22);
            this.dtpEndDate.TabIndex = 2;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(282, 22);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(66, 14);
            this.lblEndDate.TabIndex = 2;
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
            this.dtpStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(97, 18);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(138, 22);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(22, 22);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date :";
            // 
            // cmbTemplates
            // 
            this.cmbTemplates.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplates.ForeColor = System.Drawing.Color.Black;
            this.cmbTemplates.FormattingEnabled = true;
            this.cmbTemplates.Location = new System.Drawing.Point(468, 17);
            this.cmbTemplates.Name = "cmbTemplates";
            this.cmbTemplates.Size = new System.Drawing.Size(312, 23);
            this.cmbTemplates.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(203, 684);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 27);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(107, 684);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 27);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lbl_pnlTemplateAllocationBottomBrd
            // 
            this.lbl_pnlTemplateAllocationBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTemplateAllocationBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlTemplateAllocationBottomBrd.Location = new System.Drawing.Point(0, 755);
            this.lbl_pnlTemplateAllocationBottomBrd.Name = "lbl_pnlTemplateAllocationBottomBrd";
            this.lbl_pnlTemplateAllocationBottomBrd.Size = new System.Drawing.Size(945, 1);
            this.lbl_pnlTemplateAllocationBottomBrd.TabIndex = 42;
            // 
            // pnlTemplateDetailsHeader
            // 
            this.pnlTemplateDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetailsHeader.Controls.Add(this.panel1);
            this.pnlTemplateDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetailsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTemplateDetailsHeader.Name = "pnlTemplateDetailsHeader";
            this.pnlTemplateDetailsHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateDetailsHeader.Size = new System.Drawing.Size(945, 28);
            this.pnlTemplateDetailsHeader.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Blue2007;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.Label5);
            this.panel1.Controls.Add(this.Label6);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.Label8);
            this.panel1.Controls.Add(this.lblTemplateDetails);
            this.panel1.Controls.Add(this.rbSimple);
            this.panel1.Controls.Add(this.rbRecurrence);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 22);
            this.panel1.TabIndex = 137;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(1, 21);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(937, 1);
            this.Label5.TabIndex = 140;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(0, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 21);
            this.Label6.TabIndex = 139;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(938, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 21);
            this.Label7.TabIndex = 138;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(0, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(939, 1);
            this.Label8.TabIndex = 137;
            this.Label8.Text = "label1";
            // 
            // lblTemplateDetails
            // 
            this.lblTemplateDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemplateDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateDetails.ForeColor = System.Drawing.Color.White;
            this.lblTemplateDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTemplateDetails.Location = new System.Drawing.Point(0, 0);
            this.lblTemplateDetails.Name = "lblTemplateDetails";
            this.lblTemplateDetails.Size = new System.Drawing.Size(939, 22);
            this.lblTemplateDetails.TabIndex = 1;
            this.lblTemplateDetails.Text = " Template Details";
            this.lblTemplateDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Checked = true;
            this.rbSimple.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSimple.Location = new System.Drawing.Point(778, 5);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(55, 17);
            this.rbSimple.TabIndex = 135;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Simple";
            this.rbSimple.UseVisualStyleBackColor = true;
            // 
            // rbRecurrence
            // 
            this.rbRecurrence.AutoSize = true;
            this.rbRecurrence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRecurrence.Location = new System.Drawing.Point(687, 5);
            this.rbRecurrence.Name = "rbRecurrence";
            this.rbRecurrence.Size = new System.Drawing.Size(61, 17);
            this.rbRecurrence.TabIndex = 136;
            this.rbRecurrence.Text = "Pattern";
            this.rbRecurrence.UseVisualStyleBackColor = true;
            // 
            // frmExtendToTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(945, 810);
            this.Controls.Add(this.pnlTemplateAllocation);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExtendToTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extend Template";
            this.Load += new System.EventHandler(this.frmExtendToTemplate_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTemplateAllocation.ResumeLayout(false);
            this.pnlTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate_1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbSimpleAllocation.ResumeLayout(false);
            this.gbSimpleAllocation.PerformLayout();
            this.pnlTemplateDetailsHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlTemplateAllocation;
        private System.Windows.Forms.Panel pnlTemplate;
        private Janus.Windows.Schedule.Schedule CalendarTemplate_1;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbTemplates;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbSimpleAllocation;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Label lbl_pnlTemplateAllocationBottomBrd;
        private System.Windows.Forms.Panel pnlTemplateDetailsHeader;
        private System.Windows.Forms.Label lblTemplateDetails;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.RadioButton rbRecurrence;
        private Janus.Windows.Schedule.Schedule CalendarTemplate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;

    }
}