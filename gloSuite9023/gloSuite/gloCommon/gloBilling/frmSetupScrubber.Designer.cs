namespace gloBilling
{
    partial class frmSetupScrubber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupScrubber));
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteDx = new System.Windows.Forms.Button();
            this.btnSelectDx = new System.Windows.Forms.Button();
            this.btnDeleteMod = new System.Windows.Forms.Button();
            this.btnSelectMod = new System.Windows.Forms.Button();
            this.pnlDiagnosis = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.c1DiagnosisSelected = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlModifiers = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.c1ModifiersSelected = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Diagnosis = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.c1Modifier = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.cmbPOS = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbTOS = new System.Windows.Forms.ComboBox();
            this.txtCPTCodeTo = new System.Windows.Forms.TextBox();
            this.txtCPTCodeFrom = new System.Windows.Forms.TextBox();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_Toolstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlDiagnosis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DiagnosisSelected)).BeginInit();
            this.pnlModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ModifiersSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Diagnosis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Modifier)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(761, 54);
            this.pnl_Toolstrip.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(761, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
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
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnDeleteDx);
            this.panel1.Controls.Add(this.btnSelectDx);
            this.panel1.Controls.Add(this.btnDeleteMod);
            this.panel1.Controls.Add(this.btnSelectMod);
            this.panel1.Controls.Add(this.pnlDiagnosis);
            this.panel1.Controls.Add(this.pnlModifiers);
            this.panel1.Controls.Add(this.c1Diagnosis);
            this.panel1.Controls.Add(this.c1Modifier);
            this.panel1.Controls.Add(this.lbl_BottomBrd);
            this.panel1.Controls.Add(this.lbl_LeftBrd);
            this.panel1.Controls.Add(this.lbl_RightBrd);
            this.panel1.Controls.Add(this.lbl_TopBrd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.cmbPOS);
            this.panel1.Controls.Add(this.Label4);
            this.panel1.Controls.Add(this.cmbTOS);
            this.panel1.Controls.Add(this.txtCPTCodeTo);
            this.panel1.Controls.Add(this.txtCPTCodeFrom);
            this.panel1.Controls.Add(this.pnlInternalControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(761, 457);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnDeleteDx
            // 
            this.btnDeleteDx.AutoEllipsis = true;
            this.btnDeleteDx.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDx.BackgroundImage")));
            this.btnDeleteDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDx.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDx.Image")));
            this.btnDeleteDx.Location = new System.Drawing.Point(649, 81);
            this.btnDeleteDx.Name = "btnDeleteDx";
            this.btnDeleteDx.Size = new System.Drawing.Size(22, 22);
            this.btnDeleteDx.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnDeleteDx, "Remove Diagnosis");
            this.btnDeleteDx.UseVisualStyleBackColor = false;
            this.btnDeleteDx.Click += new System.EventHandler(this.btnDeleteDx_Click);
            this.btnDeleteDx.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDeleteDx.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnSelectDx
            // 
            this.btnSelectDx.AutoEllipsis = true;
            this.btnSelectDx.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectDx.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectDx.BackgroundImage")));
            this.btnSelectDx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectDx.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectDx.Image")));
            this.btnSelectDx.Location = new System.Drawing.Point(622, 81);
            this.btnSelectDx.Name = "btnSelectDx";
            this.btnSelectDx.Size = new System.Drawing.Size(22, 22);
            this.btnSelectDx.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnSelectDx, "Add Diagnosis");
            this.btnSelectDx.UseVisualStyleBackColor = false;
            this.btnSelectDx.Click += new System.EventHandler(this.btnSelectDx_Click);
            this.btnSelectDx.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnSelectDx.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnDeleteMod
            // 
            this.btnDeleteMod.AutoEllipsis = true;
            this.btnDeleteMod.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteMod.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteMod.BackgroundImage")));
            this.btnDeleteMod.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteMod.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteMod.Image")));
            this.btnDeleteMod.Location = new System.Drawing.Point(222, 81);
            this.btnDeleteMod.Name = "btnDeleteMod";
            this.btnDeleteMod.Size = new System.Drawing.Size(22, 22);
            this.btnDeleteMod.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnDeleteMod, "Remove Modifier");
            this.btnDeleteMod.UseVisualStyleBackColor = false;
            this.btnDeleteMod.Click += new System.EventHandler(this.btnDeleteMod_Click);
            this.btnDeleteMod.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDeleteMod.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnSelectMod
            // 
            this.btnSelectMod.AutoEllipsis = true;
            this.btnSelectMod.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectMod.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectMod.BackgroundImage")));
            this.btnSelectMod.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectMod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectMod.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMod.Image")));
            this.btnSelectMod.Location = new System.Drawing.Point(196, 81);
            this.btnSelectMod.Name = "btnSelectMod";
            this.btnSelectMod.Size = new System.Drawing.Size(22, 22);
            this.btnSelectMod.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnSelectMod, "Add Modifier");
            this.btnSelectMod.UseVisualStyleBackColor = false;
            this.btnSelectMod.Click += new System.EventHandler(this.btnSelectMod_Click);
            this.btnSelectMod.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnSelectMod.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlDiagnosis
            // 
            this.pnlDiagnosis.Controls.Add(this.label15);
            this.pnlDiagnosis.Controls.Add(this.label14);
            this.pnlDiagnosis.Controls.Add(this.label11);
            this.pnlDiagnosis.Controls.Add(this.label9);
            this.pnlDiagnosis.Controls.Add(this.c1DiagnosisSelected);
            this.pnlDiagnosis.Location = new System.Drawing.Point(392, 115);
            this.pnlDiagnosis.Name = "pnlDiagnosis";
            this.pnlDiagnosis.Size = new System.Drawing.Size(352, 328);
            this.pnlDiagnosis.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1, 327);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(350, 1);
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
            this.label14.Size = new System.Drawing.Size(350, 1);
            this.label14.TabIndex = 51;
            this.label14.Text = "label1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(351, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 328);
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
            this.label9.Size = new System.Drawing.Size(1, 328);
            this.label9.TabIndex = 49;
            this.label9.Text = "label4";
            // 
            // c1DiagnosisSelected
            // 
            this.c1DiagnosisSelected.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1DiagnosisSelected.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1DiagnosisSelected.AutoGenerateColumns = false;
            this.c1DiagnosisSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1DiagnosisSelected.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1DiagnosisSelected.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1DiagnosisSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DiagnosisSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1DiagnosisSelected.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1DiagnosisSelected.Location = new System.Drawing.Point(0, 0);
            this.c1DiagnosisSelected.Name = "c1DiagnosisSelected";
            this.c1DiagnosisSelected.Rows.Count = 1;
            this.c1DiagnosisSelected.Rows.DefaultSize = 21;
            this.c1DiagnosisSelected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1DiagnosisSelected.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1DiagnosisSelected.ShowCellLabels = true;
            this.c1DiagnosisSelected.Size = new System.Drawing.Size(352, 328);
            this.c1DiagnosisSelected.StyleInfo = resources.GetString("c1DiagnosisSelected.StyleInfo");
            this.c1DiagnosisSelected.TabIndex = 0;
            this.c1DiagnosisSelected.TabStop = false;
            this.c1DiagnosisSelected.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1DiagnosisSelected_MouseMove);
            // 
            // pnlModifiers
            // 
            this.pnlModifiers.Controls.Add(this.label13);
            this.pnlModifiers.Controls.Add(this.label12);
            this.pnlModifiers.Controls.Add(this.label8);
            this.pnlModifiers.Controls.Add(this.label7);
            this.pnlModifiers.Controls.Add(this.c1ModifiersSelected);
            this.pnlModifiers.Location = new System.Drawing.Point(14, 115);
            this.pnlModifiers.Name = "pnlModifiers";
            this.pnlModifiers.Size = new System.Drawing.Size(352, 328);
            this.pnlModifiers.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 327);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(350, 1);
            this.label13.TabIndex = 52;
            this.label13.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(350, 1);
            this.label12.TabIndex = 51;
            this.label12.Text = "label1";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(351, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 328);
            this.label8.TabIndex = 50;
            this.label8.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 328);
            this.label7.TabIndex = 49;
            this.label7.Text = "label4";
            // 
            // c1ModifiersSelected
            // 
            this.c1ModifiersSelected.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ModifiersSelected.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1ModifiersSelected.AutoGenerateColumns = false;
            this.c1ModifiersSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ModifiersSelected.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ModifiersSelected.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1ModifiersSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ModifiersSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ModifiersSelected.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ModifiersSelected.Location = new System.Drawing.Point(0, 0);
            this.c1ModifiersSelected.Name = "c1ModifiersSelected";
            this.c1ModifiersSelected.Rows.Count = 1;
            this.c1ModifiersSelected.Rows.DefaultSize = 21;
            this.c1ModifiersSelected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1ModifiersSelected.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ModifiersSelected.ShowCellLabels = true;
            this.c1ModifiersSelected.Size = new System.Drawing.Size(352, 328);
            this.c1ModifiersSelected.StyleInfo = resources.GetString("c1ModifiersSelected.StyleInfo");
            this.c1ModifiersSelected.TabIndex = 0;
            this.c1ModifiersSelected.TabStop = false;
            this.c1ModifiersSelected.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ModifiersSelected_MouseMove);
            // 
            // c1Diagnosis
            // 
            this.c1Diagnosis.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Diagnosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Diagnosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1Diagnosis.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1Diagnosis.Location = new System.Drawing.Point(523, 82);
            this.c1Diagnosis.Name = "c1Diagnosis";
            this.c1Diagnosis.Rows.Count = 1;
            this.c1Diagnosis.Rows.DefaultSize = 21;
            this.c1Diagnosis.Rows.Fixed = 0;
            this.c1Diagnosis.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1Diagnosis.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Diagnosis.Size = new System.Drawing.Size(93, 21);
            this.c1Diagnosis.StyleInfo = resources.GetString("c1Diagnosis.StyleInfo");
            this.c1Diagnosis.TabIndex = 8;
            this.c1Diagnosis.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Diagnosis_StartEdit);
            this.c1Diagnosis.ChangeEdit += new System.EventHandler(this.c1Diagnosis_ChangeEdit);
            this.c1Diagnosis.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Diagnosis_KeyUp);
            // 
            // c1Modifier
            // 
            this.c1Modifier.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1Modifier.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Modifier.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.c1Modifier.ColumnInfo = "1,0,0,0,0,105,Columns:";
            this.c1Modifier.Location = new System.Drawing.Point(93, 82);
            this.c1Modifier.Name = "c1Modifier";
            this.c1Modifier.Rows.Count = 1;
            this.c1Modifier.Rows.DefaultSize = 21;
            this.c1Modifier.Rows.Fixed = 0;
            this.c1Modifier.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1Modifier.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1Modifier.Size = new System.Drawing.Size(97, 21);
            this.c1Modifier.StyleInfo = resources.GetString("c1Modifier.StyleInfo");
            this.c1Modifier.TabIndex = 5;
            this.c1Modifier.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Modifier_StartEdit);
            this.c1Modifier.ChangeEdit += new System.EventHandler(this.c1Modifier_ChangeEdit);
            this.c1Modifier.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1Modifier_KeyUp);
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 453);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(753, 1);
            this.lbl_BottomBrd.TabIndex = 49;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 450);
            this.lbl_LeftBrd.TabIndex = 48;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(757, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 450);
            this.lbl_RightBrd.TabIndex = 47;
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
            this.lbl_TopBrd.TabIndex = 46;
            this.lbl_TopBrd.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(456, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 14);
            this.label2.TabIndex = 26;
            this.label2.Text = "Diagnosis :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(32, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 14);
            this.label10.TabIndex = 26;
            this.label10.Text = "Modifier :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(356, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 11);
            this.label6.TabIndex = 11;
            this.label6.Text = "(To)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(152, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 11);
            this.label5.TabIndex = 11;
            this.label5.Text = "(From)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Location = new System.Drawing.Point(20, 19);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 14);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "CPT Code :";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Location = new System.Drawing.Point(50, 59);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(39, 14);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "TOS :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPOS
            // 
            this.cmbPOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOS.ForeColor = System.Drawing.Color.Black;
            this.cmbPOS.Location = new System.Drawing.Point(292, 55);
            this.cmbPOS.Name = "cmbPOS";
            this.cmbPOS.Size = new System.Drawing.Size(151, 22);
            this.cmbPOS.TabIndex = 4;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Location = new System.Drawing.Point(250, 59);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(38, 14);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "POS :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTOS
            // 
            this.cmbTOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTOS.ForeColor = System.Drawing.Color.Black;
            this.cmbTOS.Location = new System.Drawing.Point(93, 55);
            this.cmbTOS.Name = "cmbTOS";
            this.cmbTOS.Size = new System.Drawing.Size(151, 22);
            this.cmbTOS.TabIndex = 3;
            // 
            // txtCPTCodeTo
            // 
            this.txtCPTCodeTo.BackColor = System.Drawing.Color.White;
            this.txtCPTCodeTo.ForeColor = System.Drawing.Color.Black;
            this.txtCPTCodeTo.Location = new System.Drawing.Point(292, 15);
            this.txtCPTCodeTo.MaxLength = 50;
            this.txtCPTCodeTo.Name = "txtCPTCodeTo";
            this.txtCPTCodeTo.Size = new System.Drawing.Size(151, 22);
            this.txtCPTCodeTo.TabIndex = 2;
            this.txtCPTCodeTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPTCodeTo_KeyPress);
            // 
            // txtCPTCodeFrom
            // 
            this.txtCPTCodeFrom.BackColor = System.Drawing.Color.White;
            this.txtCPTCodeFrom.ForeColor = System.Drawing.Color.Black;
            this.txtCPTCodeFrom.Location = new System.Drawing.Point(93, 15);
            this.txtCPTCodeFrom.MaxLength = 50;
            this.txtCPTCodeFrom.Name = "txtCPTCodeFrom";
            this.txtCPTCodeFrom.Size = new System.Drawing.Size(151, 22);
            this.txtCPTCodeFrom.TabIndex = 1;
            this.txtCPTCodeFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPTCodeFrom_KeyPress);
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(14, 115);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(352, 331);
            this.pnlInternalControl.TabIndex = 50;
            this.pnlInternalControl.Visible = false;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupScrubber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(761, 511);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupScrubber";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scrubber";
            this.Load += new System.EventHandler(this.frmSetupScrubber_Load);
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlDiagnosis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DiagnosisSelected)).EndInit();
            this.pnlModifiers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ModifiersSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Diagnosis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Modifier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Toolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox cmbPOS;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbTOS;
        internal System.Windows.Forms.TextBox txtCPTCodeTo;
        internal System.Windows.Forms.TextBox txtCPTCodeFrom;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlInternalControl;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Modifier;
        private System.Windows.Forms.Panel pnlModifiers;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ModifiersSelected;
        private System.Windows.Forms.Panel pnlDiagnosis;
        private C1.Win.C1FlexGrid.C1FlexGrid c1DiagnosisSelected;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Diagnosis;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDeleteDx;
        private System.Windows.Forms.Button btnSelectDx;
        private System.Windows.Forms.Button btnDeleteMod;
        private System.Windows.Forms.Button btnSelectMod;
        private System.Windows.Forms.ToolTip toolTip1;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
    }
}