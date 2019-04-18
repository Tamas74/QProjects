namespace gloBilling
{
    partial class frmAssociateBusinessCenter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssociateBusinessCenter));
            this.pnlText = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mskEnddate = new System.Windows.Forms.MaskedTextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mskStartDate = new System.Windows.Forms.MaskedTextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cbx_BusCenByCPT = new System.Windows.Forms.CheckBox();
            this.cbx_AdvanceBusCenRule = new System.Windows.Forms.CheckBox();
            this.cbx_BusCenByFacility = new System.Windows.Forms.CheckBox();
            this.cbx_BusCenByDoctor = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TopToolStrip = new System.Windows.Forms.ToolStrip();
            this.ts_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Saveclose = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.c1BusinessCenter = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.ts_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnl_Shortkeys = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_KeyClose = new System.Windows.Forms.Label();
            this.lbl_shrtctKeyClose = new System.Windows.Forms.Label();
            this.lbl_KeySave = new System.Windows.Forms.Label();
            this.lbl_shrtctKeySave = new System.Windows.Forms.Label();
            this.lbl_Keyremoveline = new System.Windows.Forms.Label();
            this.lbl_lshrtctKeyremoveline = new System.Windows.Forms.Label();
            this.lbl_KeyAddline = new System.Windows.Forms.Label();
            this.lbl_shrtctKeyAddline = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlText.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BusinessCenter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnl_Shortkeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.label8);
            this.pnlText.Controls.Add(this.label9);
            this.pnlText.Controls.Add(this.panel4);
            this.pnlText.Controls.Add(this.panel3);
            this.pnlText.Controls.Add(this.cbx_BusCenByCPT);
            this.pnlText.Controls.Add(this.cbx_AdvanceBusCenRule);
            this.pnlText.Controls.Add(this.cbx_BusCenByFacility);
            this.pnlText.Controls.Add(this.cbx_BusCenByDoctor);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlText.Location = new System.Drawing.Point(0, 54);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(816, 99);
            this.pnlText.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(222, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 14);
            this.label8.TabIndex = 17635;
            this.label8.Text = "End Date :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 14);
            this.label9.TabIndex = 17634;
            this.label9.Text = "Start Date :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mskEnddate);
            this.panel4.Controls.Add(this.dtpEndDate);
            this.panel4.Location = new System.Drawing.Point(294, 40);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(104, 23);
            this.panel4.TabIndex = 17636;
            this.panel4.TabStop = true;
            // 
            // mskEnddate
            // 
            this.mskEnddate.Dock = System.Windows.Forms.DockStyle.Left;
            this.mskEnddate.Location = new System.Drawing.Point(0, 0);
            this.mskEnddate.Mask = "00/00/0000";
            this.mskEnddate.Name = "mskEnddate";
            this.mskEnddate.Size = new System.Drawing.Size(74, 22);
            this.mskEnddate.TabIndex = 1;
            this.mskEnddate.ValidatingType = typeof(System.DateTime);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpEndDate.Location = new System.Drawing.Point(0, 0);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpEndDate.Size = new System.Drawing.Size(104, 22);
            this.dtpEndDate.TabIndex = 2011;
            this.dtpEndDate.CloseUp += new System.EventHandler(this.dtpEndDate_CloseUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mskStartDate);
            this.panel3.Controls.Add(this.dtpStartDate);
            this.panel3.Location = new System.Drawing.Point(101, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(104, 23);
            this.panel3.TabIndex = 17633;
            this.panel3.TabStop = true;
            // 
            // mskStartDate
            // 
            this.mskStartDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.mskStartDate.Location = new System.Drawing.Point(0, 0);
            this.mskStartDate.Mask = "00/00/0000";
            this.mskStartDate.Name = "mskStartDate";
            this.mskStartDate.Size = new System.Drawing.Size(74, 22);
            this.mskStartDate.TabIndex = 0;
            this.mskStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpStartDate.Location = new System.Drawing.Point(0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpStartDate.Size = new System.Drawing.Size(104, 22);
            this.dtpStartDate.TabIndex = 1011;
            this.dtpStartDate.CloseUp += new System.EventHandler(this.dtpStartDate_CloseUp);
            // 
            // cbx_BusCenByCPT
            // 
            this.cbx_BusCenByCPT.AutoSize = true;
            this.cbx_BusCenByCPT.Checked = true;
            this.cbx_BusCenByCPT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_BusCenByCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_BusCenByCPT.Location = new System.Drawing.Point(594, 66);
            this.cbx_BusCenByCPT.Name = "cbx_BusCenByCPT";
            this.cbx_BusCenByCPT.Size = new System.Drawing.Size(155, 18);
            this.cbx_BusCenByCPT.TabIndex = 134;
            this.cbx_BusCenByCPT.Text = "Busines Centers by CPT";
            this.cbx_BusCenByCPT.UseVisualStyleBackColor = true;
            this.cbx_BusCenByCPT.Visible = false;
            this.cbx_BusCenByCPT.CheckedChanged += new System.EventHandler(this.cbx_BusCenByCPT_CheckedChanged);
            // 
            // cbx_AdvanceBusCenRule
            // 
            this.cbx_AdvanceBusCenRule.AutoSize = true;
            this.cbx_AdvanceBusCenRule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_AdvanceBusCenRule.Location = new System.Drawing.Point(593, 44);
            this.cbx_AdvanceBusCenRule.Name = "cbx_AdvanceBusCenRule";
            this.cbx_AdvanceBusCenRule.Size = new System.Drawing.Size(202, 18);
            this.cbx_AdvanceBusCenRule.TabIndex = 133;
            this.cbx_AdvanceBusCenRule.Text = "Advanced Business Center Rules";
            this.cbx_AdvanceBusCenRule.UseVisualStyleBackColor = true;
            this.cbx_AdvanceBusCenRule.Visible = false;
            // 
            // cbx_BusCenByFacility
            // 
            this.cbx_BusCenByFacility.AutoSize = true;
            this.cbx_BusCenByFacility.Checked = true;
            this.cbx_BusCenByFacility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_BusCenByFacility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_BusCenByFacility.Location = new System.Drawing.Point(593, 26);
            this.cbx_BusCenByFacility.Name = "cbx_BusCenByFacility";
            this.cbx_BusCenByFacility.Size = new System.Drawing.Size(168, 18);
            this.cbx_BusCenByFacility.TabIndex = 132;
            this.cbx_BusCenByFacility.Text = "Busines Centers by Facility";
            this.cbx_BusCenByFacility.UseVisualStyleBackColor = true;
            this.cbx_BusCenByFacility.Visible = false;
            this.cbx_BusCenByFacility.CheckedChanged += new System.EventHandler(this.cbx_BusCenByFacility_CheckedChanged);
            // 
            // cbx_BusCenByDoctor
            // 
            this.cbx_BusCenByDoctor.AutoSize = true;
            this.cbx_BusCenByDoctor.Checked = true;
            this.cbx_BusCenByDoctor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_BusCenByDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_BusCenByDoctor.Location = new System.Drawing.Point(593, 7);
            this.cbx_BusCenByDoctor.Name = "cbx_BusCenByDoctor";
            this.cbx_BusCenByDoctor.Size = new System.Drawing.Size(175, 18);
            this.cbx_BusCenByDoctor.TabIndex = 131;
            this.cbx_BusCenByDoctor.Text = "Business Centers by Doctor";
            this.cbx_BusCenByDoctor.UseVisualStyleBackColor = true;
            this.cbx_BusCenByDoctor.Visible = false;
            this.cbx_BusCenByDoctor.CheckedChanged += new System.EventHandler(this.cbx_BusCenByDoctor_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(808, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(808, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(812, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 93);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 93);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TopToolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 54);
            this.panel1.TabIndex = 1;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnAddLine,
            this.tsb_btnRemoveLine,
            this.tsb_Save,
            this.tsb_Saveclose,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(816, 53);
            this.TopToolStrip.TabIndex = 8;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnAddLine
            // 
            this.ts_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnAddLine.Image")));
            this.ts_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnAddLine.Name = "ts_btnAddLine";
            this.ts_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.ts_btnAddLine.Tag = "AddLine";
            this.ts_btnAddLine.Text = "&Add Line";
            this.ts_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnAddLine.Click += new System.EventHandler(this.ts_btnAddLine_Click);
            // 
            // tsb_btnRemoveLine
            // 
            this.tsb_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnRemoveLine.Image")));
            this.tsb_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_btnRemoveLine.Name = "tsb_btnRemoveLine";
            this.tsb_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.tsb_btnRemoveLine.Tag = "RemoveLine";
            this.tsb_btnRemoveLine.Text = "Re&move Line";
            this.tsb_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnRemoveLine.Click += new System.EventHandler(this.tsb_btnRemoveLine_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Saveclose
            // 
            this.tsb_Saveclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Saveclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Saveclose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Saveclose.Image")));
            this.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Saveclose.Name = "tsb_Saveclose";
            this.tsb_Saveclose.Size = new System.Drawing.Size(66, 50);
            this.tsb_Saveclose.Tag = "SaveFeeSchedule";
            this.tsb_Saveclose.Text = "Sa&ve&&Cls";
            this.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Saveclose.ToolTipText = "Save and Close";
            this.tsb_Saveclose.Click += new System.EventHandler(this.tsb_Saveclose_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.ToolTipText = "Close";
            this.ts_btnClose.Click += new System.EventHandler(this.tsb_close_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.pnlInternalControl);
            this.pnlDetails.Controls.Add(this.c1BusinessCenter);
            this.pnlDetails.Controls.Add(this.label102);
            this.pnlDetails.Controls.Add(this.label103);
            this.pnlDetails.Controls.Add(this.label104);
            this.pnlDetails.Controls.Add(this.label105);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 153);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDetails.Size = new System.Drawing.Size(816, 377);
            this.pnlDetails.TabIndex = 223;
            this.pnlDetails.TabStop = true;
            this.pnlDetails.Leave += new System.EventHandler(this.pnlDetails_Leave);
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(196, 47);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(337, 211);
            this.pnlInternalControl.TabIndex = 9;
            this.pnlInternalControl.Visible = false;
            // 
            // c1BusinessCenter
            // 
            this.c1BusinessCenter.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1BusinessCenter.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1BusinessCenter.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1BusinessCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1BusinessCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1BusinessCenter.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1BusinessCenter.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1BusinessCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1BusinessCenter.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1BusinessCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1BusinessCenter.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1BusinessCenter.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1BusinessCenter.Location = new System.Drawing.Point(4, 1);
            this.c1BusinessCenter.Name = "c1BusinessCenter";
            this.c1BusinessCenter.Rows.Count = 1;
            this.c1BusinessCenter.Rows.DefaultSize = 22;
            this.c1BusinessCenter.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1BusinessCenter.Size = new System.Drawing.Size(808, 372);
            this.c1BusinessCenter.StyleInfo = resources.GetString("c1BusinessCenter.StyleInfo");
            this.c1BusinessCenter.TabIndex = 8;
            this.c1BusinessCenter.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1BusinessCenter_AfterScroll);
            this.c1BusinessCenter.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1BusinessCenter_AfterRowColChange);
            this.c1BusinessCenter.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BillingTaxonomy_StartEdit);
            this.c1BusinessCenter.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BusinessCenter_AfterEdit);
            this.c1BusinessCenter.LeaveEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BusinessCenter_LeaveEdit);
            this.c1BusinessCenter.ChangeEdit += new System.EventHandler(this.c1BusinessCenter_ChangeEdit);
            this.c1BusinessCenter.KeyDownEdit += new C1.Win.C1FlexGrid.KeyEditEventHandler(this.c1BusinessCenter_KeyDownEdit);
            this.c1BusinessCenter.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1BusinessCenter_KeyPressEdit);
            this.c1BusinessCenter.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1BusinessCenter_CellChanged);
            this.c1BusinessCenter.Click += new System.EventHandler(this.c1BusinessCenter_Click);
            this.c1BusinessCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1BusinessCenter_KeyDown);
            this.c1BusinessCenter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1BusinessCenter_KeyUp);
            this.c1BusinessCenter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1BusinessCenter_MouseMove);
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(4, 373);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(808, 1);
            this.label102.TabIndex = 7;
            this.label102.Text = "label1";
            // 
            // label103
            // 
            this.label103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Dock = System.Windows.Forms.DockStyle.Top;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(4, 0);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(808, 1);
            this.label103.TabIndex = 6;
            this.label103.Text = "label1";
            // 
            // label104
            // 
            this.label104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label104.Dock = System.Windows.Forms.DockStyle.Right;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(812, 0);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(1, 374);
            this.label104.TabIndex = 5;
            this.label104.Text = "label4";
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Dock = System.Windows.Forms.DockStyle.Left;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(3, 0);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(1, 374);
            this.label105.TabIndex = 4;
            this.label105.Text = "label4";
            // 
            // ts_btnRemoveLine
            // 
            this.ts_btnRemoveLine.Name = "ts_btnRemoveLine";
            this.ts_btnRemoveLine.Size = new System.Drawing.Size(23, 50);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.pnl_Shortkeys);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 504);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlFooter.Size = new System.Drawing.Size(816, 26);
            this.pnlFooter.TabIndex = 334;
            // 
            // pnl_Shortkeys
            // 
            this.pnl_Shortkeys.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnl_Shortkeys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Shortkeys.Controls.Add(this.label4);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeyClose);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeyClose);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeySave);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeySave);
            this.pnl_Shortkeys.Controls.Add(this.lbl_Keyremoveline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_lshrtctKeyremoveline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_KeyAddline);
            this.pnl_Shortkeys.Controls.Add(this.lbl_shrtctKeyAddline);
            this.pnl_Shortkeys.Controls.Add(this.label5);
            this.pnl_Shortkeys.Controls.Add(this.label6);
            this.pnl_Shortkeys.Controls.Add(this.label7);
            this.pnl_Shortkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Shortkeys.Location = new System.Drawing.Point(3, 0);
            this.pnl_Shortkeys.Name = "pnl_Shortkeys";
            this.pnl_Shortkeys.Size = new System.Drawing.Size(810, 23);
            this.pnl_Shortkeys.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(809, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 21);
            this.label4.TabIndex = 68;
            this.label4.Text = "label4";
            // 
            // lbl_KeyClose
            // 
            this.lbl_KeyClose.AutoSize = true;
            this.lbl_KeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyClose.Location = new System.Drawing.Point(445, 1);
            this.lbl_KeyClose.Name = "lbl_KeyClose";
            this.lbl_KeyClose.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeyClose.Size = new System.Drawing.Size(52, 18);
            this.lbl_KeyClose.TabIndex = 63;
            this.lbl_KeyClose.Text = "- Close";
            // 
            // lbl_shrtctKeyClose
            // 
            this.lbl_shrtctKeyClose.AutoSize = true;
            this.lbl_shrtctKeyClose.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyClose.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyClose.Location = new System.Drawing.Point(384, 1);
            this.lbl_shrtctKeyClose.Name = "lbl_shrtctKeyClose";
            this.lbl_shrtctKeyClose.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeyClose.Size = new System.Drawing.Size(61, 18);
            this.lbl_shrtctKeyClose.TabIndex = 64;
            this.lbl_shrtctKeyClose.Text = "Alt + F4";
            // 
            // lbl_KeySave
            // 
            this.lbl_KeySave.AutoSize = true;
            this.lbl_KeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeySave.Location = new System.Drawing.Point(286, 1);
            this.lbl_KeySave.Name = "lbl_KeySave";
            this.lbl_KeySave.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeySave.Size = new System.Drawing.Size(98, 18);
            this.lbl_KeySave.TabIndex = 56;
            this.lbl_KeySave.Text = "- Save && Close";
            // 
            // lbl_shrtctKeySave
            // 
            this.lbl_shrtctKeySave.AutoSize = true;
            this.lbl_shrtctKeySave.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeySave.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeySave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeySave.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeySave.Location = new System.Drawing.Point(227, 1);
            this.lbl_shrtctKeySave.Name = "lbl_shrtctKeySave";
            this.lbl_shrtctKeySave.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeySave.Size = new System.Drawing.Size(59, 18);
            this.lbl_shrtctKeySave.TabIndex = 55;
            this.lbl_shrtctKeySave.Text = "Ctrl + S";
            // 
            // lbl_Keyremoveline
            // 
            this.lbl_Keyremoveline.AutoSize = true;
            this.lbl_Keyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Keyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Keyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Keyremoveline.Location = new System.Drawing.Point(129, 1);
            this.lbl_Keyremoveline.Name = "lbl_Keyremoveline";
            this.lbl_Keyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_Keyremoveline.Size = new System.Drawing.Size(98, 18);
            this.lbl_Keyremoveline.TabIndex = 46;
            this.lbl_Keyremoveline.Text = "- Remove Line";
            // 
            // lbl_lshrtctKeyremoveline
            // 
            this.lbl_lshrtctKeyremoveline.AutoSize = true;
            this.lbl_lshrtctKeyremoveline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_lshrtctKeyremoveline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_lshrtctKeyremoveline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lshrtctKeyremoveline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_lshrtctKeyremoveline.Location = new System.Drawing.Point(104, 1);
            this.lbl_lshrtctKeyremoveline.Name = "lbl_lshrtctKeyremoveline";
            this.lbl_lshrtctKeyremoveline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_lshrtctKeyremoveline.Size = new System.Drawing.Size(25, 18);
            this.lbl_lshrtctKeyremoveline.TabIndex = 45;
            this.lbl_lshrtctKeyremoveline.Text = "F3";
            // 
            // lbl_KeyAddline
            // 
            this.lbl_KeyAddline.AutoSize = true;
            this.lbl_KeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_KeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_KeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_KeyAddline.Location = new System.Drawing.Point(30, 1);
            this.lbl_KeyAddline.Name = "lbl_KeyAddline";
            this.lbl_KeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_KeyAddline.Size = new System.Drawing.Size(74, 18);
            this.lbl_KeyAddline.TabIndex = 44;
            this.lbl_KeyAddline.Text = "- Add Line";
            // 
            // lbl_shrtctKeyAddline
            // 
            this.lbl_shrtctKeyAddline.AutoSize = true;
            this.lbl_shrtctKeyAddline.BackColor = System.Drawing.Color.Transparent;
            this.lbl_shrtctKeyAddline.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_shrtctKeyAddline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shrtctKeyAddline.ForeColor = System.Drawing.Color.Maroon;
            this.lbl_shrtctKeyAddline.Location = new System.Drawing.Point(1, 1);
            this.lbl_shrtctKeyAddline.Name = "lbl_shrtctKeyAddline";
            this.lbl_shrtctKeyAddline.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_shrtctKeyAddline.Size = new System.Drawing.Size(29, 18);
            this.lbl_shrtctKeyAddline.TabIndex = 44;
            this.lbl_shrtctKeyAddline.Text = "F2 ";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(1, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(809, 1);
            this.label5.TabIndex = 65;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 22);
            this.label6.TabIndex = 67;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(810, 1);
            this.label7.TabIndex = 69;
            this.label7.Text = "label7";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmAssociateBusinessCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(816, 530);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssociateBusinessCenter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Business Center";
            this.Load += new System.EventHandler(this.frmAssociateBusinessCenter_Load);
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1BusinessCenter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnl_Shortkeys.ResumeLayout(false);
            this.pnl_Shortkeys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbx_AdvanceBusCenRule;
        private System.Windows.Forms.CheckBox cbx_BusCenByFacility;
        private System.Windows.Forms.CheckBox cbx_BusCenByDoctor;
        internal System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlInternalControl;
        private C1.Win.C1FlexGrid.C1FlexGrid c1BusinessCenter;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.ToolStripButton ts_btnRemoveLine;
        private System.Windows.Forms.ToolStrip TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnAddLine;
        private System.Windows.Forms.ToolStripButton tsb_btnRemoveLine;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Saveclose;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.CheckBox cbx_BusCenByCPT;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnl_Shortkeys;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_KeyClose;
        private System.Windows.Forms.Label lbl_shrtctKeyClose;
        private System.Windows.Forms.Label lbl_KeySave;
        private System.Windows.Forms.Label lbl_shrtctKeySave;
        private System.Windows.Forms.Label lbl_Keyremoveline;
        private System.Windows.Forms.Label lbl_lshrtctKeyremoveline;
        private System.Windows.Forms.Label lbl_KeyAddline;
        private System.Windows.Forms.Label lbl_shrtctKeyAddline;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MaskedTextBox mskEnddate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MaskedTextBox mskStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;


    }
}