namespace gloAppointmentBook
{
    partial class frmSetupTemplateAllocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTemplateAllocation));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Recurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_RemoveRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_ApplyRecurrence = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.c1Template = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.pnlTemplateAllocation = new System.Windows.Forms.Panel();
            this.pnlTemplate = new System.Windows.Forms.Panel();
            this.CalendarTemplate = new Janus.Windows.Schedule.Schedule();
            this.lbl_pnlTemplateTopBrd = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cmbTemplates = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbSimpleAllocation = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_pnlTemplateAllocationBottomBrd = new System.Windows.Forms.Label();
            this.pnlTemplateDetailsHeader = new System.Windows.Forms.Panel();
            this.lbl_pnlTemplateDetailsBottomBrd = new System.Windows.Forms.Label();
            this.lblTemplateDetails = new System.Windows.Forms.Label();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.rbRecurrence = new System.Windows.Forms.RadioButton();
            this.lbl_pnlTemplateAllocationLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlTemplateAllocationRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlTemplateAllocationTopBrd = new System.Windows.Forms.Label();
            this.pnlReccurance = new System.Windows.Forms.Panel();
            this.pnlc1RecurrnceCriteria = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.c1RecurrnceCriteria = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_pnlc1RecurrnceCriteriaTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlc1RecurrnceCriteriaRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd = new System.Windows.Forms.Label();
            this.gbRecurrncePattern = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkLast = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.chkFourth = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.chkThird = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chkSecond = new System.Windows.Forms.CheckBox();
            this.chkFirst = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_EndDate = new System.Windows.Forms.Label();
            this.lbl_StartDate = new System.Windows.Forms.Label();
            this.numToYear = new System.Windows.Forms.NumericUpDown();
            this.numFromMonth = new System.Windows.Forms.NumericUpDown();
            this.numToMonth = new System.Windows.Forms.NumericUpDown();
            this.numFromYear = new System.Windows.Forms.NumericUpDown();
            this.pnlRecMain = new System.Windows.Forms.Panel();
            this.pnlTemplateReccuranceDeatailsHeader = new System.Windows.Forms.Panel();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.lbl_TemplateReccuranceDetails = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.pnlTemplateAllocation.SuspendLayout();
            this.pnlTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).BeginInit();
            this.gbSimpleAllocation.SuspendLayout();
            this.pnlTemplateDetailsHeader.SuspendLayout();
            this.pnlReccurance.SuspendLayout();
            this.pnlc1RecurrnceCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RecurrnceCriteria)).BeginInit();
            this.gbRecurrncePattern.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromYear)).BeginInit();
            this.pnlRecMain.SuspendLayout();
            this.pnlTemplateReccuranceDeatailsHeader.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Recurrence,
            this.tsb_RemoveRecurrence,
            this.tsb_ApplyRecurrence,
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(794, 53);
            this.ts_Commands.TabIndex = 15;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Recurrence
            // 
            this.tsb_Recurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Recurrence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Recurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Recurrence.Image")));
            this.tsb_Recurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Recurrence.Name = "tsb_Recurrence";
            this.tsb_Recurrence.Size = new System.Drawing.Size(79, 50);
            this.tsb_Recurrence.Tag = "ShowRecurrence";
            this.tsb_Recurrence.Text = "&Recurrence";
            this.tsb_Recurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Recurrence.ToolTipText = "Recurrence";
            // 
            // tsb_RemoveRecurrence
            // 
            this.tsb_RemoveRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RemoveRecurrence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_RemoveRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RemoveRecurrence.Image")));
            this.tsb_RemoveRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RemoveRecurrence.Name = "tsb_RemoveRecurrence";
            this.tsb_RemoveRecurrence.Size = new System.Drawing.Size(132, 50);
            this.tsb_RemoveRecurrence.Tag = "RemoveRecurrence";
            this.tsb_RemoveRecurrence.Text = "R&emove Recurrence";
            this.tsb_RemoveRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RemoveRecurrence.Visible = false;
            // 
            // tsb_ApplyRecurrence
            // 
            this.tsb_ApplyRecurrence.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ApplyRecurrence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ApplyRecurrence.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ApplyRecurrence.Image")));
            this.tsb_ApplyRecurrence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ApplyRecurrence.Name = "tsb_ApplyRecurrence";
            this.tsb_ApplyRecurrence.Size = new System.Drawing.Size(118, 50);
            this.tsb_ApplyRecurrence.Tag = "ApplyRecurrence";
            this.tsb_ApplyRecurrence.Text = "&Apply Recurrence";
            this.tsb_ApplyRecurrence.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ApplyRecurrence.Visible = false;
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
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
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.c1Template);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Location = new System.Drawing.Point(0, 89);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.pnlMain.Size = new System.Drawing.Size(302, 673);
            this.pnlMain.TabIndex = 16;
            // 
            // c1Template
            // 
            this.c1Template.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Template.AllowEditing = false;
            this.c1Template.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Template.AutoGenerateColumns = false;
            this.c1Template.AutoResize = false;
            this.c1Template.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Template.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Template.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Template.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Template.ForeColor = System.Drawing.Color.DarkBlue;
            this.c1Template.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Template.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Template.Location = new System.Drawing.Point(3, 56);
            this.c1Template.Name = "c1Template";
            this.c1Template.Rows.Count = 1;
            this.c1Template.Rows.DefaultSize = 19;
            this.c1Template.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Template.ShowCursor = true;
            this.c1Template.Size = new System.Drawing.Size(296, 614);
            this.c1Template.StyleInfo = resources.GetString("c1Template.StyleInfo");
            this.c1Template.TabIndex = 6;
            this.c1Template.DoubleClick += new System.EventHandler(this.c1Template_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.btnRemove);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(296, 27);
            this.panel3.TabIndex = 28;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(149, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 27);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseHover += new System.EventHandler(this.btnAdd_MouseHover);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(221, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(3, 27);
            this.label14.TabIndex = 27;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(224, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(72, 27);
            this.btnRemove.TabIndex = 26;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.MouseLeave += new System.EventHandler(this.btnRemove_MouseLeave);
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.MouseHover += new System.EventHandler(this.btnRemove_MouseHover);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(296, 27);
            this.panel2.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(296, 27);
            this.label10.TabIndex = 1;
            this.label10.Text = "   Allocated Templates";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 670);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(296, 1);
            this.label7.TabIndex = 24;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(296, 1);
            this.label6.TabIndex = 23;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(299, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 670);
            this.label5.TabIndex = 22;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(2, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 670);
            this.label4.TabIndex = 21;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(87, 35);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(268, 22);
            this.cmbProvider.TabIndex = 1;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(794, 54);
            this.pnlToolStrip.TabIndex = 30;
            // 
            // pnlTemplateAllocation
            // 
            this.pnlTemplateAllocation.Controls.Add(this.pnlTemplate);
            this.pnlTemplateAllocation.Controls.Add(this.label15);
            this.pnlTemplateAllocation.Controls.Add(this.lblProvider);
            this.pnlTemplateAllocation.Controls.Add(this.cmbTemplates);
            this.pnlTemplateAllocation.Controls.Add(this.btnCancel);
            this.pnlTemplateAllocation.Controls.Add(this.cmbProvider);
            this.pnlTemplateAllocation.Controls.Add(this.btnSave);
            this.pnlTemplateAllocation.Controls.Add(this.gbSimpleAllocation);
            this.pnlTemplateAllocation.Controls.Add(this.lblTemplate);
            this.pnlTemplateAllocation.Controls.Add(this.label19);
            this.pnlTemplateAllocation.Controls.Add(this.lbl_pnlTemplateAllocationBottomBrd);
            this.pnlTemplateAllocation.Controls.Add(this.pnlTemplateDetailsHeader);
            this.pnlTemplateAllocation.Controls.Add(this.lbl_pnlTemplateAllocationLeftBrd);
            this.pnlTemplateAllocation.Controls.Add(this.lbl_pnlTemplateAllocationRightBrd);
            this.pnlTemplateAllocation.Controls.Add(this.lbl_pnlTemplateAllocationTopBrd);
            this.pnlTemplateAllocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateAllocation.Location = new System.Drawing.Point(0, 54);
            this.pnlTemplateAllocation.Name = "pnlTemplateAllocation";
            this.pnlTemplateAllocation.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateAllocation.Size = new System.Drawing.Size(794, 566);
            this.pnlTemplateAllocation.TabIndex = 27;
            // 
            // pnlTemplate
            // 
            this.pnlTemplate.Controls.Add(this.CalendarTemplate);
            this.pnlTemplate.Controls.Add(this.lbl_pnlTemplateTopBrd);
            this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTemplate.Location = new System.Drawing.Point(4, 103);
            this.pnlTemplate.Name = "pnlTemplate";
            this.pnlTemplate.Size = new System.Drawing.Size(786, 459);
            this.pnlTemplate.TabIndex = 138;
            // 
            // CalendarTemplate
            // 
            this.CalendarTemplate.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None;
            this.CalendarTemplate.AllowEdit = false;
            this.CalendarTemplate.BorderStyle = Janus.Windows.Schedule.BorderStyle.None;
            this.CalendarTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarTemplate.HorizontalScrollPosition = 0;
            this.CalendarTemplate.Location = new System.Drawing.Point(0, 1);
            this.CalendarTemplate.Name = "CalendarTemplate";
            this.CalendarTemplate.Size = new System.Drawing.Size(786, 458);
            this.CalendarTemplate.TabIndex = 5;
            this.CalendarTemplate.VerticalScrollPosition = 16;
            this.CalendarTemplate.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007;
            this.CalendarTemplate.AppointmentChanged += new Janus.Windows.Schedule.AppointmentChangeEventHandler(this.CalendarTemplate_AppointmentChanged);
            // 
            // lbl_pnlTemplateTopBrd
            // 
            this.lbl_pnlTemplateTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTemplateTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTemplateTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlTemplateTopBrd.Name = "lbl_pnlTemplateTopBrd";
            this.lbl_pnlTemplateTopBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_pnlTemplateTopBrd.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(12, 39);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(14, 14);
            this.label15.TabIndex = 140;
            this.label15.Text = "*";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.Location = new System.Drawing.Point(25, 39);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(59, 14);
            this.lblProvider.TabIndex = 137;
            this.lblProvider.Text = "Provider :";
            // 
            // cmbTemplates
            // 
            this.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplates.ForeColor = System.Drawing.Color.Black;
            this.cmbTemplates.FormattingEnabled = true;
            this.cmbTemplates.Location = new System.Drawing.Point(442, 35);
            this.cmbTemplates.Name = "cmbTemplates";
            this.cmbTemplates.Size = new System.Drawing.Size(268, 22);
            this.cmbTemplates.TabIndex = 2;
            this.cmbTemplates.SelectedIndexChanged += new System.EventHandler(this.cmbTemplates_SelectedIndexChanged);
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
            this.btnCancel.Location = new System.Drawing.Point(174, 635);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseHover += new System.EventHandler(this.btnCancel_MouseHover);
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
            this.btnSave.Location = new System.Drawing.Point(92, 635);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 25);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseHover += new System.EventHandler(this.btnSave_MouseHover);
            // 
            // gbSimpleAllocation
            // 
            this.gbSimpleAllocation.Controls.Add(this.dtpEndDate);
            this.gbSimpleAllocation.Controls.Add(this.lblEndDate);
            this.gbSimpleAllocation.Controls.Add(this.dtpStartDate);
            this.gbSimpleAllocation.Controls.Add(this.lblStartDate);
            this.gbSimpleAllocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSimpleAllocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbSimpleAllocation.Location = new System.Drawing.Point(28, 58);
            this.gbSimpleAllocation.Name = "gbSimpleAllocation";
            this.gbSimpleAllocation.Size = new System.Drawing.Size(681, 40);
            this.gbSimpleAllocation.TabIndex = 134;
            this.gbSimpleAllocation.TabStop = false;
            this.gbSimpleAllocation.Text = "Simple";
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
            this.dtpEndDate.Location = new System.Drawing.Point(359, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(119, 22);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(290, 16);
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
            this.dtpStartDate.Location = new System.Drawing.Point(137, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(119, 22);
            this.dtpStartDate.TabIndex = 3;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(60, 16);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplate.Location = new System.Drawing.Point(371, 39);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(67, 14);
            this.lblTemplate.TabIndex = 1;
            this.lblTemplate.Text = "Template :";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(360, 40);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 139;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_pnlTemplateAllocationBottomBrd
            // 
            this.lbl_pnlTemplateAllocationBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTemplateAllocationBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlTemplateAllocationBottomBrd.Location = new System.Drawing.Point(4, 562);
            this.lbl_pnlTemplateAllocationBottomBrd.Name = "lbl_pnlTemplateAllocationBottomBrd";
            this.lbl_pnlTemplateAllocationBottomBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_pnlTemplateAllocationBottomBrd.TabIndex = 42;
            // 
            // pnlTemplateDetailsHeader
            // 
            this.pnlTemplateDetailsHeader.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Blue2007;
            this.pnlTemplateDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetailsHeader.Controls.Add(this.lbl_pnlTemplateDetailsBottomBrd);
            this.pnlTemplateDetailsHeader.Controls.Add(this.lblTemplateDetails);
            this.pnlTemplateDetailsHeader.Controls.Add(this.rbSimple);
            this.pnlTemplateDetailsHeader.Controls.Add(this.rbRecurrence);
            this.pnlTemplateDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetailsHeader.Location = new System.Drawing.Point(4, 4);
            this.pnlTemplateDetailsHeader.Name = "pnlTemplateDetailsHeader";
            this.pnlTemplateDetailsHeader.Size = new System.Drawing.Size(786, 23);
            this.pnlTemplateDetailsHeader.TabIndex = 0;
            // 
            // lbl_pnlTemplateDetailsBottomBrd
            // 
            this.lbl_pnlTemplateDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlTemplateDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlTemplateDetailsBottomBrd.Location = new System.Drawing.Point(0, 22);
            this.lbl_pnlTemplateDetailsBottomBrd.Name = "lbl_pnlTemplateDetailsBottomBrd";
            this.lbl_pnlTemplateDetailsBottomBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_pnlTemplateDetailsBottomBrd.TabIndex = 137;
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
            this.lblTemplateDetails.Size = new System.Drawing.Size(786, 23);
            this.lblTemplateDetails.TabIndex = 1;
            this.lblTemplateDetails.Text = " Template Details";
            this.lblTemplateDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbSimple
            // 
            this.rbSimple.AutoSize = true;
            this.rbSimple.Checked = true;
            this.rbSimple.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSimple.Location = new System.Drawing.Point(667, 5);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(55, 17);
            this.rbSimple.TabIndex = 135;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Simple";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.CheckedChanged += new System.EventHandler(this.rbSimple_CheckedChanged);
            // 
            // rbRecurrence
            // 
            this.rbRecurrence.AutoSize = true;
            this.rbRecurrence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRecurrence.Location = new System.Drawing.Point(589, 5);
            this.rbRecurrence.Name = "rbRecurrence";
            this.rbRecurrence.Size = new System.Drawing.Size(61, 17);
            this.rbRecurrence.TabIndex = 136;
            this.rbRecurrence.Text = "Pattern";
            this.rbRecurrence.UseVisualStyleBackColor = true;
            // 
            // lbl_pnlTemplateAllocationLeftBrd
            // 
            this.lbl_pnlTemplateAllocationLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTemplateAllocationLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlTemplateAllocationLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlTemplateAllocationLeftBrd.Name = "lbl_pnlTemplateAllocationLeftBrd";
            this.lbl_pnlTemplateAllocationLeftBrd.Size = new System.Drawing.Size(1, 559);
            this.lbl_pnlTemplateAllocationLeftBrd.TabIndex = 40;
            // 
            // lbl_pnlTemplateAllocationRightBrd
            // 
            this.lbl_pnlTemplateAllocationRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTemplateAllocationRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlTemplateAllocationRightBrd.Location = new System.Drawing.Point(790, 4);
            this.lbl_pnlTemplateAllocationRightBrd.Name = "lbl_pnlTemplateAllocationRightBrd";
            this.lbl_pnlTemplateAllocationRightBrd.Size = new System.Drawing.Size(1, 559);
            this.lbl_pnlTemplateAllocationRightBrd.TabIndex = 43;
            // 
            // lbl_pnlTemplateAllocationTopBrd
            // 
            this.lbl_pnlTemplateAllocationTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.lbl_pnlTemplateAllocationTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTemplateAllocationTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlTemplateAllocationTopBrd.Name = "lbl_pnlTemplateAllocationTopBrd";
            this.lbl_pnlTemplateAllocationTopBrd.Size = new System.Drawing.Size(788, 1);
            this.lbl_pnlTemplateAllocationTopBrd.TabIndex = 41;
            // 
            // pnlReccurance
            // 
            this.pnlReccurance.Controls.Add(this.pnlc1RecurrnceCriteria);
            this.pnlReccurance.Controls.Add(this.gbRecurrncePattern);
            this.pnlReccurance.Controls.Add(this.panel1);
            this.pnlReccurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReccurance.Location = new System.Drawing.Point(0, 28);
            this.pnlReccurance.Name = "pnlReccurance";
            this.pnlReccurance.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlReccurance.Size = new System.Drawing.Size(794, 538);
            this.pnlReccurance.TabIndex = 139;
            // 
            // pnlc1RecurrnceCriteria
            // 
            this.pnlc1RecurrnceCriteria.Controls.Add(this.label20);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.c1RecurrnceCriteria);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.label12);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.lbl_pnlc1RecurrnceCriteriaTopBrd);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.lbl_pnlc1RecurrnceCriteriaBottomBrd);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.lbl_pnlc1RecurrnceCriteriaRightBrd);
            this.pnlc1RecurrnceCriteria.Controls.Add(this.lbl_pnlc1RecurrnceCriteriaLeftBrd);
            this.pnlc1RecurrnceCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1RecurrnceCriteria.Location = new System.Drawing.Point(3, 103);
            this.pnlc1RecurrnceCriteria.Name = "pnlc1RecurrnceCriteria";
            this.pnlc1RecurrnceCriteria.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlc1RecurrnceCriteria.Size = new System.Drawing.Size(788, 432);
            this.pnlc1RecurrnceCriteria.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(1, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(786, 1);
            this.label20.TabIndex = 142;
            // 
            // c1RecurrnceCriteria
            // 
            this.c1RecurrnceCriteria.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1RecurrnceCriteria.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1RecurrnceCriteria.AutoGenerateColumns = false;
            this.c1RecurrnceCriteria.AutoResize = false;
            this.c1RecurrnceCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RecurrnceCriteria.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RecurrnceCriteria.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1RecurrnceCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RecurrnceCriteria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1RecurrnceCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RecurrnceCriteria.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1RecurrnceCriteria.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1RecurrnceCriteria.Location = new System.Drawing.Point(1, 28);
            this.c1RecurrnceCriteria.Name = "c1RecurrnceCriteria";
            this.c1RecurrnceCriteria.Rows.Count = 1;
            this.c1RecurrnceCriteria.Rows.DefaultSize = 19;
            this.c1RecurrnceCriteria.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1RecurrnceCriteria.ShowCursor = true;
            this.c1RecurrnceCriteria.Size = new System.Drawing.Size(786, 403);
            this.c1RecurrnceCriteria.StyleInfo = resources.GetString("c1RecurrnceCriteria.StyleInfo");
            this.c1RecurrnceCriteria.TabIndex = 11;
            this.c1RecurrnceCriteria.Click += new System.EventHandler(this.c1RecurrnceCriteria_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 4);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(5);
            this.label12.Size = new System.Drawing.Size(66, 24);
            this.label12.TabIndex = 141;
            this.label12.Text = "  Step 3";
            // 
            // lbl_pnlc1RecurrnceCriteriaTopBrd
            // 
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.Location = new System.Drawing.Point(1, 3);
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.Name = "lbl_pnlc1RecurrnceCriteriaTopBrd";
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_pnlc1RecurrnceCriteriaTopBrd.TabIndex = 12;
            // 
            // lbl_pnlc1RecurrnceCriteriaBottomBrd
            // 
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.Location = new System.Drawing.Point(1, 431);
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.Name = "lbl_pnlc1RecurrnceCriteriaBottomBrd";
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.Size = new System.Drawing.Size(786, 1);
            this.lbl_pnlc1RecurrnceCriteriaBottomBrd.TabIndex = 11;
            // 
            // lbl_pnlc1RecurrnceCriteriaRightBrd
            // 
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.Location = new System.Drawing.Point(787, 3);
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.Name = "lbl_pnlc1RecurrnceCriteriaRightBrd";
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.Size = new System.Drawing.Size(1, 429);
            this.lbl_pnlc1RecurrnceCriteriaRightBrd.TabIndex = 10;
            // 
            // lbl_pnlc1RecurrnceCriteriaLeftBrd
            // 
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.Location = new System.Drawing.Point(0, 3);
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.Name = "lbl_pnlc1RecurrnceCriteriaLeftBrd";
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.Size = new System.Drawing.Size(1, 429);
            this.lbl_pnlc1RecurrnceCriteriaLeftBrd.TabIndex = 9;
            // 
            // gbRecurrncePattern
            // 
            this.gbRecurrncePattern.Controls.Add(this.label11);
            this.gbRecurrncePattern.Controls.Add(this.chkAll);
            this.gbRecurrncePattern.Controls.Add(this.label13);
            this.gbRecurrncePattern.Controls.Add(this.chkLast);
            this.gbRecurrncePattern.Controls.Add(this.label16);
            this.gbRecurrncePattern.Controls.Add(this.chkFourth);
            this.gbRecurrncePattern.Controls.Add(this.label17);
            this.gbRecurrncePattern.Controls.Add(this.chkThird);
            this.gbRecurrncePattern.Controls.Add(this.label18);
            this.gbRecurrncePattern.Controls.Add(this.chkSecond);
            this.gbRecurrncePattern.Controls.Add(this.chkFirst);
            this.gbRecurrncePattern.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRecurrncePattern.Location = new System.Drawing.Point(3, 49);
            this.gbRecurrncePattern.Name = "gbRecurrncePattern";
            this.gbRecurrncePattern.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.gbRecurrncePattern.Size = new System.Drawing.Size(788, 54);
            this.gbRecurrncePattern.TabIndex = 142;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1, 4);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(5);
            this.label11.Size = new System.Drawing.Size(66, 24);
            this.label11.TabIndex = 140;
            this.label11.Text = "  Step 2";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.Location = new System.Drawing.Point(327, 29);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(38, 18);
            this.chkAll.TabIndex = 10;
            this.chkAll.Tag = "ALL";
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(787, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 49);
            this.label13.TabIndex = 139;
            this.label13.Text = "label13";
            // 
            // chkLast
            // 
            this.chkLast.AutoSize = true;
            this.chkLast.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLast.Location = new System.Drawing.Point(273, 29);
            this.chkLast.Name = "chkLast";
            this.chkLast.Size = new System.Drawing.Size(48, 18);
            this.chkLast.TabIndex = 9;
            this.chkLast.Tag = "5";
            this.chkLast.Text = "Last";
            this.chkLast.UseVisualStyleBackColor = true;
            this.chkLast.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(1, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(787, 1);
            this.label16.TabIndex = 138;
            this.label16.Text = "label2";
            // 
            // chkFourth
            // 
            this.chkFourth.AutoSize = true;
            this.chkFourth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFourth.Location = new System.Drawing.Point(205, 29);
            this.chkFourth.Name = "chkFourth";
            this.chkFourth.Size = new System.Drawing.Size(62, 18);
            this.chkFourth.TabIndex = 8;
            this.chkFourth.Tag = "4";
            this.chkFourth.Text = "Fourth";
            this.chkFourth.UseVisualStyleBackColor = true;
            this.chkFourth.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(787, 1);
            this.label17.TabIndex = 137;
            this.label17.Text = "label1";
            // 
            // chkThird
            // 
            this.chkThird.AutoSize = true;
            this.chkThird.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThird.Location = new System.Drawing.Point(145, 29);
            this.chkThird.Name = "chkThird";
            this.chkThird.Size = new System.Drawing.Size(54, 18);
            this.chkThird.TabIndex = 7;
            this.chkThird.Tag = "3";
            this.chkThird.Text = "Third";
            this.chkThird.UseVisualStyleBackColor = true;
            this.chkThird.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 51);
            this.label18.TabIndex = 136;
            this.label18.Text = "label4";
            // 
            // chkSecond
            // 
            this.chkSecond.AutoSize = true;
            this.chkSecond.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecond.Location = new System.Drawing.Point(72, 29);
            this.chkSecond.Name = "chkSecond";
            this.chkSecond.Size = new System.Drawing.Size(67, 18);
            this.chkSecond.TabIndex = 6;
            this.chkSecond.Tag = "2";
            this.chkSecond.Text = "Second";
            this.chkSecond.UseVisualStyleBackColor = true;
            this.chkSecond.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkFirst
            // 
            this.chkFirst.AutoSize = true;
            this.chkFirst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFirst.Location = new System.Drawing.Point(18, 29);
            this.chkFirst.Name = "chkFirst";
            this.chkFirst.Size = new System.Drawing.Size(48, 18);
            this.chkFirst.TabIndex = 5;
            this.chkFirst.Tag = "1";
            this.chkFirst.Text = "First";
            this.chkFirst.UseVisualStyleBackColor = true;
            this.chkFirst.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_EndDate);
            this.panel1.Controls.Add(this.lbl_StartDate);
            this.panel1.Controls.Add(this.numToYear);
            this.panel1.Controls.Add(this.numFromMonth);
            this.panel1.Controls.Add(this.numToMonth);
            this.panel1.Controls.Add(this.numFromYear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 49);
            this.panel1.TabIndex = 141;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1, 1);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(5);
            this.label9.Size = new System.Drawing.Size(70, 24);
            this.label9.TabIndex = 140;
            this.label9.Text = "  Step 1 ";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(787, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 47);
            this.label3.TabIndex = 139;
            this.label3.Text = "label3";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(787, 1);
            this.label1.TabIndex = 138;
            this.label1.Text = "label2";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(1, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(787, 1);
            this.Label8.TabIndex = 137;
            this.Label8.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 49);
            this.label2.TabIndex = 136;
            this.label2.Text = "label4";
            // 
            // lbl_EndDate
            // 
            this.lbl_EndDate.AutoSize = true;
            this.lbl_EndDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EndDate.Location = new System.Drawing.Point(227, 25);
            this.lbl_EndDate.Name = "lbl_EndDate";
            this.lbl_EndDate.Size = new System.Drawing.Size(66, 14);
            this.lbl_EndDate.TabIndex = 6;
            this.lbl_EndDate.Text = "End Date :";
            // 
            // lbl_StartDate
            // 
            this.lbl_StartDate.AutoSize = true;
            this.lbl_StartDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_StartDate.Location = new System.Drawing.Point(15, 25);
            this.lbl_StartDate.Name = "lbl_StartDate";
            this.lbl_StartDate.Size = new System.Drawing.Size(72, 14);
            this.lbl_StartDate.TabIndex = 4;
            this.lbl_StartDate.Text = "Start Date :";
            // 
            // numToYear
            // 
            this.numToYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numToYear.ForeColor = System.Drawing.Color.Black;
            this.numToYear.Location = new System.Drawing.Point(350, 21);
            this.numToYear.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.numToYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numToYear.Name = "numToYear";
            this.numToYear.Size = new System.Drawing.Size(79, 22);
            this.numToYear.TabIndex = 4;
            this.numToYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numToYear.ValueChanged += new System.EventHandler(this.numFromYear_ValueChanged);
            // 
            // numFromMonth
            // 
            this.numFromMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFromMonth.ForeColor = System.Drawing.Color.Black;
            this.numFromMonth.Location = new System.Drawing.Point(93, 21);
            this.numFromMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numFromMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromMonth.Name = "numFromMonth";
            this.numFromMonth.Size = new System.Drawing.Size(43, 22);
            this.numFromMonth.TabIndex = 1;
            this.numFromMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromMonth.ValueChanged += new System.EventHandler(this.numFromYear_ValueChanged);
            // 
            // numToMonth
            // 
            this.numToMonth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numToMonth.ForeColor = System.Drawing.Color.Black;
            this.numToMonth.Location = new System.Drawing.Point(301, 21);
            this.numToMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numToMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToMonth.Name = "numToMonth";
            this.numToMonth.Size = new System.Drawing.Size(43, 22);
            this.numToMonth.TabIndex = 3;
            this.numToMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numToMonth.ValueChanged += new System.EventHandler(this.numFromYear_ValueChanged);
            // 
            // numFromYear
            // 
            this.numFromYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFromYear.ForeColor = System.Drawing.Color.Black;
            this.numFromYear.Location = new System.Drawing.Point(142, 21);
            this.numFromYear.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.numFromYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numFromYear.Name = "numFromYear";
            this.numFromYear.Size = new System.Drawing.Size(79, 22);
            this.numFromYear.TabIndex = 2;
            this.numFromYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numFromYear.ValueChanged += new System.EventHandler(this.numFromYear_ValueChanged);
            // 
            // pnlRecMain
            // 
            this.pnlRecMain.Controls.Add(this.pnlReccurance);
            this.pnlRecMain.Controls.Add(this.pnlTemplateReccuranceDeatailsHeader);
            this.pnlRecMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRecMain.Location = new System.Drawing.Point(0, 54);
            this.pnlRecMain.Name = "pnlRecMain";
            this.pnlRecMain.Size = new System.Drawing.Size(794, 566);
            this.pnlRecMain.TabIndex = 28;
            // 
            // pnlTemplateReccuranceDeatailsHeader
            // 
            this.pnlTemplateReccuranceDeatailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateReccuranceDeatailsHeader.Controls.Add(this.Panel8);
            this.pnlTemplateReccuranceDeatailsHeader.Controls.Add(this.radioButton1);
            this.pnlTemplateReccuranceDeatailsHeader.Controls.Add(this.radioButton2);
            this.pnlTemplateReccuranceDeatailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateReccuranceDeatailsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTemplateReccuranceDeatailsHeader.Name = "pnlTemplateReccuranceDeatailsHeader";
            this.pnlTemplateReccuranceDeatailsHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateReccuranceDeatailsHeader.Size = new System.Drawing.Size(794, 28);
            this.pnlTemplateReccuranceDeatailsHeader.TabIndex = 0;
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Transparent;
            this.Panel8.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Blue2007;
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.lbl_TemplateReccuranceDetails);
            this.Panel8.Controls.Add(this.Label24);
            this.Panel8.Controls.Add(this.Label25);
            this.Panel8.Controls.Add(this.Label26);
            this.Panel8.Controls.Add(this.Label27);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel8.Location = new System.Drawing.Point(3, 3);
            this.Panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(788, 22);
            this.Panel8.TabIndex = 137;
            // 
            // lbl_TemplateReccuranceDetails
            // 
            this.lbl_TemplateReccuranceDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TemplateReccuranceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TemplateReccuranceDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TemplateReccuranceDetails.ForeColor = System.Drawing.Color.White;
            this.lbl_TemplateReccuranceDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_TemplateReccuranceDetails.Location = new System.Drawing.Point(1, 1);
            this.lbl_TemplateReccuranceDetails.Name = "lbl_TemplateReccuranceDetails";
            this.lbl_TemplateReccuranceDetails.Size = new System.Drawing.Size(786, 20);
            this.lbl_TemplateReccuranceDetails.TabIndex = 1;
            this.lbl_TemplateReccuranceDetails.Text = "  Template Reccurance Details";
            this.lbl_TemplateReccuranceDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label24.Location = new System.Drawing.Point(1, 21);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(786, 1);
            this.Label24.TabIndex = 8;
            this.Label24.Text = "label2";
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(0, 1);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1, 21);
            this.Label25.TabIndex = 7;
            this.Label25.Text = "label4";
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label26.Location = new System.Drawing.Point(787, 1);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(1, 21);
            this.Label26.TabIndex = 6;
            this.Label26.Text = "label3";
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(0, 0);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(788, 1);
            this.Label27.TabIndex = 5;
            this.Label27.Text = "label1";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.radioButton1.Location = new System.Drawing.Point(670, 8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 18);
            this.radioButton1.TabIndex = 135;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Simple";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.radioButton2.Location = new System.Drawing.Point(592, 8);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(66, 18);
            this.radioButton2.TabIndex = 136;
            this.radioButton2.Text = "Pattern";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // frmSetupTemplateAllocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(794, 620);
            this.Controls.Add(this.pnlTemplateAllocation);
            this.Controls.Add(this.pnlRecMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupTemplateAllocation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Template Allocation";
            this.Load += new System.EventHandler(this.frmSetupTemplateAllocation_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.pnlTemplateAllocation.ResumeLayout(false);
            this.pnlTemplateAllocation.PerformLayout();
            this.pnlTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTemplate)).EndInit();
            this.gbSimpleAllocation.ResumeLayout(false);
            this.gbSimpleAllocation.PerformLayout();
            this.pnlTemplateDetailsHeader.ResumeLayout(false);
            this.pnlTemplateDetailsHeader.PerformLayout();
            this.pnlReccurance.ResumeLayout(false);
            this.pnlc1RecurrnceCriteria.ResumeLayout(false);
            this.pnlc1RecurrnceCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RecurrnceCriteria)).EndInit();
            this.gbRecurrncePattern.ResumeLayout(false);
            this.gbRecurrncePattern.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromYear)).EndInit();
            this.pnlRecMain.ResumeLayout(false);
            this.pnlTemplateReccuranceDeatailsHeader.ResumeLayout(false);
            this.pnlTemplateReccuranceDeatailsHeader.PerformLayout();
            this.Panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Template;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlTemplateAllocation;
        private System.Windows.Forms.Label lbl_pnlTemplateAllocationBottomBrd;
        private System.Windows.Forms.GroupBox gbSimpleAllocation;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbTemplates;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Panel pnlTemplateDetailsHeader;
        private System.Windows.Forms.Label lblTemplateDetails;
        private System.Windows.Forms.Label lbl_pnlTemplateAllocationLeftBrd;
        private System.Windows.Forms.Label lbl_pnlTemplateAllocationRightBrd;
        private System.Windows.Forms.Label lbl_pnlTemplateAllocationTopBrd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripButton tsb_Recurrence;
        internal System.Windows.Forms.ToolStripButton tsb_RemoveRecurrence;
        internal System.Windows.Forms.ToolStripButton tsb_ApplyRecurrence;
        private System.Windows.Forms.RadioButton rbRecurrence;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.RadioButton rbSimple;
        private System.Windows.Forms.Panel pnlRecMain;
        private System.Windows.Forms.Panel pnlTemplateReccuranceDeatailsHeader;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel pnlReccurance;
        private System.Windows.Forms.Panel pnlc1RecurrnceCriteria;
        private System.Windows.Forms.Label lbl_pnlc1RecurrnceCriteriaTopBrd;
        private System.Windows.Forms.Label lbl_pnlc1RecurrnceCriteriaBottomBrd;
        private System.Windows.Forms.Label lbl_pnlc1RecurrnceCriteriaRightBrd;
        private System.Windows.Forms.Label lbl_pnlc1RecurrnceCriteriaLeftBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid c1RecurrnceCriteria;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkLast;
        private System.Windows.Forms.CheckBox chkFourth;
        private System.Windows.Forms.CheckBox chkThird;
        private System.Windows.Forms.CheckBox chkSecond;
        private System.Windows.Forms.CheckBox chkFirst;
        private System.Windows.Forms.NumericUpDown numToYear;
        private System.Windows.Forms.NumericUpDown numToMonth;
        private System.Windows.Forms.NumericUpDown numFromYear;
        private System.Windows.Forms.NumericUpDown numFromMonth;
        private System.Windows.Forms.Label lbl_EndDate;
        private System.Windows.Forms.Label lbl_StartDate;
        private System.Windows.Forms.Panel pnlTemplate;
        private Janus.Windows.Schedule.Schedule CalendarTemplate;
        private System.Windows.Forms.Label lbl_pnlTemplateTopBrd;
        private System.Windows.Forms.Label lbl_TemplateReccuranceDetails;
        private System.Windows.Forms.Label lbl_pnlTemplateDetailsBottomBrd;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.Panel Panel8;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        private System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel gbRecurrncePattern;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label20;
    }
}