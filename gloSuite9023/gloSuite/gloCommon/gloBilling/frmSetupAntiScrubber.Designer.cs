namespace gloBilling
{
    partial class frmSetupAntiScrubber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAntiScrubber));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPOS = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbTOS = new System.Windows.Forms.ComboBox();
            this.pnlCPTGrid = new System.Windows.Forms.Panel();
            this.c1CPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.btnDeleteCPT = new System.Windows.Forms.Button();
            this.btnSelectCPT = new System.Windows.Forms.Button();
            this.c1CPTSearchGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblCPTCode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlCPTGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTSearchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(685, 55);
            this.panel2.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(685, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_save
            // 
            this.tsb_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save.Image")));
            this.tsb_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save.Name = "tsb_save";
            this.tsb_save.Size = new System.Drawing.Size(40, 50);
            this.tsb_save.Text = "&Save";
            this.tsb_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_save.ToolTipText = "Save";
            this.tsb_save.Click += new System.EventHandler(this.tsb_save_Click);
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
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.label39);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.cmbPOS);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.cmbTOS);
            this.pnlMain.Controls.Add(this.pnlCPTGrid);
            this.pnlMain.Controls.Add(this.pnlInternalControl);
            this.pnlMain.Controls.Add(this.btnDeleteCPT);
            this.pnlMain.Controls.Add(this.btnSelectCPT);
            this.pnlMain.Controls.Add(this.c1CPTSearchGrid);
            this.pnlMain.Controls.Add(this.lblCPTCode);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtDescription);
            this.pnlMain.Controls.Add(this.txtCode);
            this.pnlMain.Controls.Add(this.label19);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(685, 538);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(11, 56);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(14, 14);
            this.label39.TabIndex = 129;
            this.label39.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(53, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 14);
            this.label8.TabIndex = 127;
            this.label8.Text = "TOS :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPOS
            // 
            this.cmbPOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOS.ForeColor = System.Drawing.Color.Black;
            this.cmbPOS.Location = new System.Drawing.Point(306, 20);
            this.cmbPOS.Name = "cmbPOS";
            this.cmbPOS.Size = new System.Drawing.Size(175, 22);
            this.cmbPOS.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(265, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 14);
            this.label10.TabIndex = 128;
            this.label10.Text = "POS :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTOS
            // 
            this.cmbTOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTOS.ForeColor = System.Drawing.Color.Black;
            this.cmbTOS.Location = new System.Drawing.Point(95, 20);
            this.cmbTOS.Name = "cmbTOS";
            this.cmbTOS.Size = new System.Drawing.Size(153, 22);
            this.cmbTOS.TabIndex = 0;
            // 
            // pnlCPTGrid
            // 
            this.pnlCPTGrid.Controls.Add(this.c1CPT);
            this.pnlCPTGrid.Controls.Add(this.label15);
            this.pnlCPTGrid.Controls.Add(this.label14);
            this.pnlCPTGrid.Controls.Add(this.label11);
            this.pnlCPTGrid.Controls.Add(this.label9);
            this.pnlCPTGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlCPTGrid.Location = new System.Drawing.Point(10, 84);
            this.pnlCPTGrid.Name = "pnlCPTGrid";
            this.pnlCPTGrid.Size = new System.Drawing.Size(663, 442);
            this.pnlCPTGrid.TabIndex = 4;
            // 
            // c1CPT
            // 
            this.c1CPT.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1CPT.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1CPT.AutoGenerateColumns = false;
            this.c1CPT.AutoResize = false;
            this.c1CPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1CPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CPT.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1CPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1CPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1CPT.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1CPT.Location = new System.Drawing.Point(1, 1);
            this.c1CPT.Name = "c1CPT";
            this.c1CPT.Rows.Count = 1;
            this.c1CPT.Rows.DefaultSize = 19;
            this.c1CPT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1CPT.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1CPT.ShowCellLabels = true;
            this.c1CPT.Size = new System.Drawing.Size(661, 440);
            this.c1CPT.StyleInfo = resources.GetString("c1CPT.StyleInfo");
            this.c1CPT.TabIndex = 0;
            this.c1CPT.TabStop = false;
            this.c1CPT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1CPT_MouseMove);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1, 441);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(661, 1);
            this.label15.TabIndex = 52;
            this.label15.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(661, 1);
            this.label14.TabIndex = 51;
            this.label14.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(662, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 442);
            this.label11.TabIndex = 50;
            this.label11.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 442);
            this.label9.TabIndex = 49;
            this.label9.Text = "label4";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlInternalControl.Location = new System.Drawing.Point(10, 84);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(668, 445);
            this.pnlInternalControl.TabIndex = 5;
            this.pnlInternalControl.Visible = false;
            // 
            // btnDeleteCPT
            // 
            this.btnDeleteCPT.AutoEllipsis = true;
            this.btnDeleteCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteCPT.BackgroundImage")));
            this.btnDeleteCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCPT.Image")));
            this.btnDeleteCPT.Location = new System.Drawing.Point(226, 52);
            this.btnDeleteCPT.Name = "btnDeleteCPT";
            this.btnDeleteCPT.Size = new System.Drawing.Size(22, 22);
            this.btnDeleteCPT.TabIndex = 3;
            this.btnDeleteCPT.UseVisualStyleBackColor = false;
            this.btnDeleteCPT.Click += new System.EventHandler(this.btnDeleteCPT_Click);
            // 
            // btnSelectCPT
            // 
            this.btnSelectCPT.AutoEllipsis = true;
            this.btnSelectCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectCPT.BackgroundImage")));
            this.btnSelectCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCPT.Image")));
            this.btnSelectCPT.Location = new System.Drawing.Point(199, 52);
            this.btnSelectCPT.Name = "btnSelectCPT";
            this.btnSelectCPT.Size = new System.Drawing.Size(22, 22);
            this.btnSelectCPT.TabIndex = 2;
            this.btnSelectCPT.UseVisualStyleBackColor = false;
            this.btnSelectCPT.Click += new System.EventHandler(this.btnSelectCPT_Click);
            // 
            // c1CPTSearchGrid
            // 
            this.c1CPTSearchGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1CPTSearchGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1CPTSearchGrid.AutoResize = false;
            this.c1CPTSearchGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1CPTSearchGrid.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1CPTSearchGrid.Location = new System.Drawing.Point(95, 53);
            this.c1CPTSearchGrid.Name = "c1CPTSearchGrid";
            this.c1CPTSearchGrid.Rows.Count = 1;
            this.c1CPTSearchGrid.Rows.DefaultSize = 19;
            this.c1CPTSearchGrid.Rows.Fixed = 0;
            this.c1CPTSearchGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1CPTSearchGrid.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1CPTSearchGrid.Size = new System.Drawing.Size(97, 21);
            this.c1CPTSearchGrid.StyleInfo = resources.GetString("c1CPTSearchGrid.StyleInfo");
            this.c1CPTSearchGrid.TabIndex = 2;
            this.c1CPTSearchGrid.ChangeEdit += new System.EventHandler(this.c1CPTSearchGrid_ChangeEdit);
            this.c1CPTSearchGrid.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1CPTSearchGrid_StartEdit);
            this.c1CPTSearchGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1CPTSearchGrid_KeyUp);
            // 
            // lblCPTCode
            // 
            this.lblCPTCode.AutoSize = true;
            this.lblCPTCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTCode.Location = new System.Drawing.Point(23, 56);
            this.lblCPTCode.Name = "lblCPTCode";
            this.lblCPTCode.Size = new System.Drawing.Size(69, 14);
            this.lblCPTCode.TabIndex = 122;
            this.lblCPTCode.Text = "CPT Code :";
            this.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(197, 417);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 14);
            this.label5.TabIndex = 116;
            this.label5.Text = "Description :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(203, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 14);
            this.label6.TabIndex = 115;
            this.label6.Text = "CPT Code :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(272, 412);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(161, 22);
            this.txtDescription.TabIndex = 114;
            // 
            // txtCode
            // 
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(272, 382);
            this.txtCode.MaxLength = 50;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(161, 22);
            this.txtCode.TabIndex = 113;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(184, 419);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 117;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(191, 387);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 118;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 531);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(681, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 531);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(679, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(679, 1);
            this.label1.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(679, 2);
            this.label12.TabIndex = 14;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupAntiScrubber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(685, 593);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupAntiScrubber";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Claim Scrubber";
            this.Load += new System.EventHandler(this.frmSetupAntiScrubber_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlCPTGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CPTSearchGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Panel pnlCPTGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CPT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnDeleteCPT;
        private System.Windows.Forms.Button btnSelectCPT;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CPTSearchGrid;
        internal System.Windows.Forms.Label lblCPTCode;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox cmbPOS;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.ComboBox cmbTOS;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        internal System.Windows.Forms.ToolStripButton tsb_save;
        private System.Windows.Forms.Label label39;
    }
}