namespace gloPatientStripControl
{
    partial class frmPatientAlerts
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
                try
                {
                    if (colorDialog1 != null)
                    {

                        colorDialog1.Dispose();
                        colorDialog1 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientAlerts));
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.rdbInActive = new System.Windows.Forms.RadioButton();
            this.txtAlertName = new System.Windows.Forms.TextBox();
            this.rdbActive = new System.Windows.Forms.RadioButton();
            this.lblAlertName = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAlertType = new System.Windows.Forms.ComboBox();
            this.lbl_ColorContainer = new System.Windows.Forms.Label();
            this.btn_ClearColor = new System.Windows.Forms.Button();
            this.lbl_Color = new System.Windows.Forms.Label();
            this.btn_Color = new System.Windows.Forms.Button();
            this.lblAlertType = new System.Windows.Forms.Label();
            this.pnl_PatientAlertGrid = new System.Windows.Forms.Panel();
            this.c1PatientAlerts = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label70 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlToolstrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnl_PatientAlertGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolstrip.Controls.Add(this.TopToolStrip);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(595, 53);
            this.pnlToolstrip.TabIndex = 0;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_New,
            this.tsb_Modify,
            this.tsb_Delete,
            this.tls_btnSave,
            this.ts_btnSave,
            this.ts_btnClose,
            this.ts_btnCancel});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(593, 53);
            this.TopToolStrip.TabIndex = 1;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // tsb_New
            // 
            this.tsb_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(37, 50);
            this.tsb_New.Tag = "Add";
            this.tsb_New.Text = "&New";
            this.tsb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(36, 50);
            this.tsb_Modify.Tag = "Modify";
            this.tsb_Modify.Text = "&Edit";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tls_btnSave
            // 
            this.tls_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSave.Image")));
            this.tls_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSave.Name = "tls_btnSave";
            this.tls_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tls_btnSave.Tag = "Save";
            this.tls_btnSave.Text = "&Save";
            this.tls_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSave.ToolTipText = "Save";
            this.tls_btnSave.Click += new System.EventHandler(this.tls_btnSave_Click);
            // 
            // ts_btnSave
            // 
            this.ts_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSave.Image")));
            this.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSave.Name = "ts_btnSave";
            this.ts_btnSave.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSave.Tag = "Save";
            this.ts_btnSave.Text = "Sa&ve&&Cls";
            this.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSave.ToolTipText = "Save and Close";
            this.ts_btnSave.Click += new System.EventHandler(this.ts_btnSave_Click);
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
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Cancel";
            this.ts_btnCancel.Text = "Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.ts_btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.rdbInActive);
            this.pnlMain.Controls.Add(this.txtAlertName);
            this.pnlMain.Controls.Add(this.rdbActive);
            this.pnlMain.Controls.Add(this.lblAlertName);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(589, 57);
            this.pnlMain.TabIndex = 1;
            // 
            // rdbInActive
            // 
            this.rdbInActive.AutoSize = true;
            this.rdbInActive.Location = new System.Drawing.Point(501, 28);
            this.rdbInActive.Name = "rdbInActive";
            this.rdbInActive.Size = new System.Drawing.Size(68, 18);
            this.rdbInActive.TabIndex = 0;
            this.rdbInActive.Text = "Inactive";
            this.rdbInActive.UseVisualStyleBackColor = true;
            this.rdbInActive.CheckedChanged += new System.EventHandler(this.rdbInActive_CheckedChanged);
            // 
            // txtAlertName
            // 
            this.txtAlertName.Location = new System.Drawing.Point(96, 3);
            this.txtAlertName.MaxLength = 250;
            this.txtAlertName.Multiline = true;
            this.txtAlertName.Name = "txtAlertName";
            this.txtAlertName.Size = new System.Drawing.Size(387, 50);
            this.txtAlertName.TabIndex = 20;
            // 
            // rdbActive
            // 
            this.rdbActive.AutoSize = true;
            this.rdbActive.Checked = true;
            this.rdbActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbActive.Location = new System.Drawing.Point(501, 4);
            this.rdbActive.Name = "rdbActive";
            this.rdbActive.Size = new System.Drawing.Size(63, 18);
            this.rdbActive.TabIndex = 0;
            this.rdbActive.TabStop = true;
            this.rdbActive.Text = "Active";
            this.rdbActive.UseVisualStyleBackColor = true;
            this.rdbActive.CheckedChanged += new System.EventHandler(this.rdbActive_CheckedChanged);
            // 
            // lblAlertName
            // 
            this.lblAlertName.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAlertName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAlertName.Location = new System.Drawing.Point(1, 1);
            this.lblAlertName.Name = "lblAlertName";
            this.lblAlertName.Size = new System.Drawing.Size(95, 55);
            this.lblAlertName.TabIndex = 21;
            this.lblAlertName.Text = "  Description :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(587, 1);
            this.label10.TabIndex = 179;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(588, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 56);
            this.label9.TabIndex = 178;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 56);
            this.label8.TabIndex = 177;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(589, 1);
            this.label7.TabIndex = 176;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(323, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 38);
            this.groupBox1.TabIndex = 180;
            this.groupBox1.TabStop = false;
            // 
            // cmbAlertType
            // 
            this.cmbAlertType.FormattingEnabled = true;
            this.cmbAlertType.Location = new System.Drawing.Point(290, 94);
            this.cmbAlertType.Name = "cmbAlertType";
            this.cmbAlertType.Size = new System.Drawing.Size(33, 22);
            this.cmbAlertType.TabIndex = 175;
            this.cmbAlertType.Visible = false;
            // 
            // lbl_ColorContainer
            // 
            this.lbl_ColorContainer.BackColor = System.Drawing.Color.White;
            this.lbl_ColorContainer.Location = new System.Drawing.Point(364, 95);
            this.lbl_ColorContainer.Name = "lbl_ColorContainer";
            this.lbl_ColorContainer.Size = new System.Drawing.Size(18, 20);
            this.lbl_ColorContainer.TabIndex = 173;
            this.lbl_ColorContainer.Visible = false;
            // 
            // btn_ClearColor
            // 
            this.btn_ClearColor.BackColor = System.Drawing.Color.Transparent;
            this.btn_ClearColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearColor.BackgroundImage")));
            this.btn_ClearColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_ClearColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearColor.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearColor.Image")));
            this.btn_ClearColor.Location = new System.Drawing.Point(403, 96);
            this.btn_ClearColor.Name = "btn_ClearColor";
            this.btn_ClearColor.Size = new System.Drawing.Size(16, 19);
            this.btn_ClearColor.TabIndex = 174;
            this.btn_ClearColor.UseVisualStyleBackColor = false;
            this.btn_ClearColor.Visible = false;
            this.btn_ClearColor.Click += new System.EventHandler(this.btnApp_ClearColor_Click);
            // 
            // lbl_Color
            // 
            this.lbl_Color.AutoSize = true;
            this.lbl_Color.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Color.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Color.Location = new System.Drawing.Point(326, 95);
            this.lbl_Color.Name = "lbl_Color";
            this.lbl_Color.Size = new System.Drawing.Size(42, 14);
            this.lbl_Color.TabIndex = 171;
            this.lbl_Color.Text = "Color :";
            this.lbl_Color.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Color.Visible = false;
            // 
            // btn_Color
            // 
            this.btn_Color.BackColor = System.Drawing.Color.Transparent;
            this.btn_Color.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Color.BackgroundImage")));
            this.btn_Color.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Color.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Color.Image = ((System.Drawing.Image)(resources.GetObject("btn_Color.Image")));
            this.btn_Color.Location = new System.Drawing.Point(381, 96);
            this.btn_Color.Name = "btn_Color";
            this.btn_Color.Size = new System.Drawing.Size(16, 19);
            this.btn_Color.TabIndex = 172;
            this.btn_Color.UseVisualStyleBackColor = false;
            this.btn_Color.Visible = false;
            this.btn_Color.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // lblAlertType
            // 
            this.lblAlertType.AutoSize = true;
            this.lblAlertType.Location = new System.Drawing.Point(239, 95);
            this.lblAlertType.Name = "lblAlertType";
            this.lblAlertType.Size = new System.Drawing.Size(43, 14);
            this.lblAlertType.TabIndex = 21;
            this.lblAlertType.Text = "Type :";
            this.lblAlertType.Visible = false;
            // 
            // pnl_PatientAlertGrid
            // 
            this.pnl_PatientAlertGrid.Controls.Add(this.c1PatientAlerts);
            this.pnl_PatientAlertGrid.Controls.Add(this.lbl_BottomBrd);
            this.pnl_PatientAlertGrid.Controls.Add(this.lbl_LeftBrd);
            this.pnl_PatientAlertGrid.Controls.Add(this.lbl_RightBrd);
            this.pnl_PatientAlertGrid.Controls.Add(this.lbl_TopBrd);
            this.pnl_PatientAlertGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PatientAlertGrid.Location = new System.Drawing.Point(0, 144);
            this.pnl_PatientAlertGrid.Name = "pnl_PatientAlertGrid";
            this.pnl_PatientAlertGrid.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnl_PatientAlertGrid.Size = new System.Drawing.Size(595, 280);
            this.pnl_PatientAlertGrid.TabIndex = 19;
            // 
            // c1PatientAlerts
            // 
            this.c1PatientAlerts.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1PatientAlerts.AutoGenerateColumns = false;
            this.c1PatientAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientAlerts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientAlerts.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1PatientAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientAlerts.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientAlerts.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientAlerts.Location = new System.Drawing.Point(4, 2);
            this.c1PatientAlerts.Name = "c1PatientAlerts";
            this.c1PatientAlerts.Rows.Count = 1;
            this.c1PatientAlerts.Rows.DefaultSize = 21;
            this.c1PatientAlerts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientAlerts.Size = new System.Drawing.Size(587, 274);
            this.c1PatientAlerts.StyleInfo = resources.GetString("c1PatientAlerts.StyleInfo");
            this.c1PatientAlerts.TabIndex = 10;
            this.c1PatientAlerts.SelChange += new System.EventHandler(this.c1PatientAlerts_SelChange);
            this.c1PatientAlerts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientAlerts_MouseMove);
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 276);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(587, 1);
            this.lbl_BottomBrd.TabIndex = 14;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 275);
            this.lbl_LeftBrd.TabIndex = 13;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(591, 2);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 275);
            this.lbl_RightBrd.TabIndex = 12;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(589, 1);
            this.lbl_TopBrd.TabIndex = 11;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel7.Location = new System.Drawing.Point(0, 116);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.panel7.Size = new System.Drawing.Size(595, 28);
            this.panel7.TabIndex = 19;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label70);
            this.panel8.Controls.Add(this.label17);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(589, 24);
            this.panel8.TabIndex = 0;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.Transparent;
            this.label70.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(1, 1);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(587, 22);
            this.label70.TabIndex = 10;
            this.label70.Text = "  Patient Alerts :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(1, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(587, 1);
            this.label17.TabIndex = 8;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 23);
            this.label18.TabIndex = 7;
            this.label18.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(588, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 23);
            this.label19.TabIndex = 6;
            this.label19.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(589, 1);
            this.label20.TabIndex = 5;
            this.label20.Text = "label1";
            // 
            // panelEditor
            // 
            this.panelEditor.Controls.Add(this.pnlMain);
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEditor.Location = new System.Drawing.Point(0, 53);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Padding = new System.Windows.Forms.Padding(3);
            this.panelEditor.Size = new System.Drawing.Size(595, 63);
            this.panelEditor.TabIndex = 181;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPatientAlerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(595, 424);
            this.Controls.Add(this.pnl_PatientAlertGrid);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlToolstrip);
            this.Controls.Add(this.cmbAlertType);
            this.Controls.Add(this.lblAlertType);
            this.Controls.Add(this.lbl_ColorContainer);
            this.Controls.Add(this.btn_Color);
            this.Controls.Add(this.btn_ClearColor);
            this.Controls.Add(this.lbl_Color);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientAlerts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Alerts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPatientAlerts_FormClosed);
            this.Load += new System.EventHandler(this.frmPatientAlerts_Load);
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnl_PatientAlertGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panelEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolstrip;
        private System.Windows.Forms.Panel pnlMain;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnl_PatientAlertGrid;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientAlerts;
        private System.Windows.Forms.RadioButton rdbInActive;
        private System.Windows.Forms.RadioButton rdbActive;
        private System.Windows.Forms.Label lblAlertName;
        private System.Windows.Forms.TextBox txtAlertName;
        internal System.Windows.Forms.Label lbl_ColorContainer;
        internal System.Windows.Forms.Button btn_ClearColor;
        private System.Windows.Forms.Label lbl_Color;
        internal System.Windows.Forms.Button btn_Color;
        private System.Windows.Forms.ComboBox cmbAlertType;
        private System.Windows.Forms.Label lblAlertType;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ToolStripButton tsb_New;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ToolStripButton tls_btnSave;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}