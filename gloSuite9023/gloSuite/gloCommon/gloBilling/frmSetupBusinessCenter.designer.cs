namespace gloBilling
{
    partial class frmSetupBusinessCenter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupBusinessCenter));
            this.pnlText = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_pnlDataGridListBottomBrd = new System.Windows.Forms.Label();
            this.lblPayTray = new System.Windows.Forms.Label();
            this.pnlDataGridList = new System.Windows.Forms.Panel();
            this.C1BusinessCenter_UserAssociation = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_pnlDataGridListTopBrd = new System.Windows.Forms.Label();
            this.btnClearStmtDisplaySettings = new System.Windows.Forms.Button();
            this.btnBrowseStmtDisplaySettings = new System.Windows.Forms.Button();
            this.txtDefaultStmtDisplaySettings = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.btnBrowseUsers = new System.Windows.Forms.Button();
            this.btnClearUsers = new System.Windows.Forms.Button();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tls_SetupBusinessCenter = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Saveclose = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlText.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlDataGridList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1BusinessCenter_UserAssociation)).BeginInit();
            this.tls_SetupBusinessCenter.SuspendLayout();
            this.pnltlsStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.panel1);
            this.pnlText.Controls.Add(this.pnlDataGridList);
            this.pnlText.Controls.Add(this.btnClearStmtDisplaySettings);
            this.pnlText.Controls.Add(this.btnBrowseStmtDisplaySettings);
            this.pnlText.Controls.Add(this.txtDefaultStmtDisplaySettings);
            this.pnlText.Controls.Add(this.label6);
            this.pnlText.Controls.Add(this.cmbUsers);
            this.pnlText.Controls.Add(this.btnBrowseUsers);
            this.pnlText.Controls.Add(this.btnClearUsers);
            this.pnlText.Controls.Add(this.rbInactive);
            this.pnlText.Controls.Add(this.rbActive);
            this.pnlText.Controls.Add(this.label5);
            this.pnlText.Controls.Add(this.label4);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Controls.Add(this.txtDescription);
            this.pnlText.Controls.Add(this.lblName);
            this.pnlText.Controls.Add(this.txtCode);
            this.pnlText.Controls.Add(this.lblCode);
            this.pnlText.Controls.Add(this.label19);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 54);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(594, 294);
            this.pnlText.TabIndex = 0;
            this.pnlText.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlText_Paint);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.lbl_pnlDataGridListBottomBrd);
            this.panel1.Controls.Add(this.lblPayTray);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 25);
            this.panel1.TabIndex = 243;
            // 
            // lbl_pnlDataGridListBottomBrd
            // 
            this.lbl_pnlDataGridListBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListBottomBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlDataGridListBottomBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlDataGridListBottomBrd.Name = "lbl_pnlDataGridListBottomBrd";
            this.lbl_pnlDataGridListBottomBrd.Size = new System.Drawing.Size(586, 1);
            this.lbl_pnlDataGridListBottomBrd.TabIndex = 242;
            // 
            // lblPayTray
            // 
            this.lblPayTray.AutoSize = true;
            this.lblPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayTray.Location = new System.Drawing.Point(5, 5);
            this.lblPayTray.Name = "lblPayTray";
            this.lblPayTray.Size = new System.Drawing.Size(43, 14);
            this.lblPayTray.TabIndex = 241;
            this.lblPayTray.Text = "Users ";
            this.lblPayTray.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDataGridList
            // 
            this.pnlDataGridList.Controls.Add(this.C1BusinessCenter_UserAssociation);
            this.pnlDataGridList.Controls.Add(this.lbl_pnlDataGridListTopBrd);
            this.pnlDataGridList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDataGridList.Location = new System.Drawing.Point(4, 131);
            this.pnlDataGridList.Name = "pnlDataGridList";
            this.pnlDataGridList.Size = new System.Drawing.Size(586, 159);
            this.pnlDataGridList.TabIndex = 242;
            // 
            // C1BusinessCenter_UserAssociation
            // 
            this.C1BusinessCenter_UserAssociation.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1BusinessCenter_UserAssociation.AllowEditing = false;
            this.C1BusinessCenter_UserAssociation.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.C1BusinessCenter_UserAssociation.BackColor = System.Drawing.Color.White;
            this.C1BusinessCenter_UserAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1BusinessCenter_UserAssociation.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1BusinessCenter_UserAssociation.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.C1BusinessCenter_UserAssociation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1BusinessCenter_UserAssociation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1BusinessCenter_UserAssociation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1BusinessCenter_UserAssociation.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.C1BusinessCenter_UserAssociation.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1BusinessCenter_UserAssociation.Location = new System.Drawing.Point(0, 1);
            this.C1BusinessCenter_UserAssociation.Name = "C1BusinessCenter_UserAssociation";
            this.C1BusinessCenter_UserAssociation.Padding = new System.Windows.Forms.Padding(3);
            this.C1BusinessCenter_UserAssociation.Rows.Count = 1;
            this.C1BusinessCenter_UserAssociation.Rows.DefaultSize = 19;
            this.C1BusinessCenter_UserAssociation.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.C1BusinessCenter_UserAssociation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1BusinessCenter_UserAssociation.Size = new System.Drawing.Size(586, 158);
            this.C1BusinessCenter_UserAssociation.StyleInfo = resources.GetString("C1BusinessCenter_UserAssociation.StyleInfo");
            this.C1BusinessCenter_UserAssociation.TabIndex = 33;
            this.C1BusinessCenter_UserAssociation.TabStop = false;
            this.C1BusinessCenter_UserAssociation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1BusinessCenter_UserAssociation_MouseMove);
            // 
            // lbl_pnlDataGridListTopBrd
            // 
            this.lbl_pnlDataGridListTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlDataGridListTopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlDataGridListTopBrd.Name = "lbl_pnlDataGridListTopBrd";
            this.lbl_pnlDataGridListTopBrd.Size = new System.Drawing.Size(586, 1);
            this.lbl_pnlDataGridListTopBrd.TabIndex = 16;
            // 
            // btnClearStmtDisplaySettings
            // 
            this.btnClearStmtDisplaySettings.AutoEllipsis = true;
            this.btnClearStmtDisplaySettings.BackColor = System.Drawing.Color.Transparent;
            this.btnClearStmtDisplaySettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearStmtDisplaySettings.BackgroundImage")));
            this.btnClearStmtDisplaySettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearStmtDisplaySettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearStmtDisplaySettings.Image = ((System.Drawing.Image)(resources.GetObject("btnClearStmtDisplaySettings.Image")));
            this.btnClearStmtDisplaySettings.Location = new System.Drawing.Point(549, 75);
            this.btnClearStmtDisplaySettings.Name = "btnClearStmtDisplaySettings";
            this.btnClearStmtDisplaySettings.Size = new System.Drawing.Size(21, 21);
            this.btnClearStmtDisplaySettings.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnClearStmtDisplaySettings, "Clear Statement Display Settings");
            this.btnClearStmtDisplaySettings.UseVisualStyleBackColor = false;
            this.btnClearStmtDisplaySettings.Click += new System.EventHandler(this.btnClearStmtDisplaySettings_Click);
            // 
            // btnBrowseStmtDisplaySettings
            // 
            this.btnBrowseStmtDisplaySettings.AutoEllipsis = true;
            this.btnBrowseStmtDisplaySettings.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseStmtDisplaySettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseStmtDisplaySettings.BackgroundImage")));
            this.btnBrowseStmtDisplaySettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseStmtDisplaySettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseStmtDisplaySettings.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseStmtDisplaySettings.Image")));
            this.btnBrowseStmtDisplaySettings.Location = new System.Drawing.Point(524, 75);
            this.btnBrowseStmtDisplaySettings.Name = "btnBrowseStmtDisplaySettings";
            this.btnBrowseStmtDisplaySettings.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseStmtDisplaySettings.TabIndex = 4;
            this.btnBrowseStmtDisplaySettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnBrowseStmtDisplaySettings, "Browse Statement Display Settings");
            this.btnBrowseStmtDisplaySettings.UseVisualStyleBackColor = false;
            this.btnBrowseStmtDisplaySettings.Click += new System.EventHandler(this.btnBrowseStmtDisplaySettings_Click);
            // 
            // txtDefaultStmtDisplaySettings
            // 
            this.txtDefaultStmtDisplaySettings.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultStmtDisplaySettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultStmtDisplaySettings.ForeColor = System.Drawing.Color.Black;
            this.txtDefaultStmtDisplaySettings.Location = new System.Drawing.Point(188, 74);
            this.txtDefaultStmtDisplaySettings.MaxLength = 255;
            this.txtDefaultStmtDisplaySettings.Name = "txtDefaultStmtDisplaySettings";
            this.txtDefaultStmtDisplaySettings.ReadOnly = true;
            this.txtDefaultStmtDisplaySettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDefaultStmtDisplaySettings.Size = new System.Drawing.Size(331, 22);
            this.txtDefaultStmtDisplaySettings.TabIndex = 3;
            this.txtDefaultStmtDisplaySettings.MouseEnter += new System.EventHandler(this.txtDefaultStmtDisplaySettings_MouseEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 14);
            this.label6.TabIndex = 233;
            this.label6.Text = "Statement Display Settings :";
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(83, 170);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(331, 22);
            this.cmbUsers.TabIndex = 6;
            // 
            // btnBrowseUsers
            // 
            this.btnBrowseUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseUsers.BackgroundImage")));
            this.btnBrowseUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseUsers.Image")));
            this.btnBrowseUsers.Location = new System.Drawing.Point(419, 171);
            this.btnBrowseUsers.Name = "btnBrowseUsers";
            this.btnBrowseUsers.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseUsers.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnBrowseUsers, "Browse Users");
            this.btnBrowseUsers.UseVisualStyleBackColor = false;
            this.btnBrowseUsers.Click += new System.EventHandler(this.btnBrowseUsers_Click);
            // 
            // btnClearUsers
            // 
            this.btnClearUsers.BackColor = System.Drawing.Color.Transparent;
            this.btnClearUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearUsers.BackgroundImage")));
            this.btnClearUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearUsers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnClearUsers.Image")));
            this.btnClearUsers.Location = new System.Drawing.Point(444, 171);
            this.btnClearUsers.Name = "btnClearUsers";
            this.btnClearUsers.Size = new System.Drawing.Size(21, 21);
            this.btnClearUsers.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnClearUsers, "Clear Users");
            this.btnClearUsers.UseVisualStyleBackColor = false;
            this.btnClearUsers.Click += new System.EventHandler(this.btnClearUsers_Click);
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Location = new System.Drawing.Point(267, 105);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(68, 18);
            this.rbInactive.TabIndex = 10;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.Visible = false;
            this.rbInactive.CheckedChanged += new System.EventHandler(this.rbInactive_CheckedChanged);
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActive.Location = new System.Drawing.Point(190, 105);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(63, 18);
            this.rbActive.TabIndex = 9;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(135, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 14);
            this.label5.TabIndex = 130;
            this.label5.Text = "Status :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(127, 48);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 111;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(586, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(586, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(590, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 288);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 288);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(188, 44);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(266, 22);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.MouseEnter += new System.EventHandler(this.txtDescription_MouseEnter);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(139, 47);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 14);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name :";
            // 
            // txtCode
            // 
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(188, 14);
            this.txtCode.MaxLength = 4;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(63, 22);
            this.txtCode.TabIndex = 1;
            this.txtCode.MouseEnter += new System.EventHandler(this.txtCode_MouseEnter);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(142, 17);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(43, 14);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Code :";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(130, 18);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tls_SetupBusinessCenter
            // 
            this.tls_SetupBusinessCenter.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupBusinessCenter.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupBusinessCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupBusinessCenter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupBusinessCenter.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupBusinessCenter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_Saveclose,
            this.tsb_close});
            this.tls_SetupBusinessCenter.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupBusinessCenter.Name = "tls_SetupBusinessCenter";
            this.tls_SetupBusinessCenter.Size = new System.Drawing.Size(594, 53);
            this.tls_SetupBusinessCenter.TabIndex = 5;
            this.tls_SetupBusinessCenter.TabStop = true;
            this.tls_SetupBusinessCenter.Text = "toolStrip1";
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
            // tsb_Saveclose
            // 
            this.tsb_Saveclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Saveclose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Saveclose.Image")));
            this.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Saveclose.Name = "tsb_Saveclose";
            this.tsb_Saveclose.Size = new System.Drawing.Size(66, 50);
            this.tsb_Saveclose.Tag = "OK";
            this.tsb_Saveclose.Text = "Sa&ve&&Cls";
            this.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Saveclose.ToolTipText = "Save and Close";
            this.tsb_Saveclose.Click += new System.EventHandler(this.tsb_Saveclose_Click);
            // 
            // tsb_close
            // 
            this.tsb_close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_close.Image")));
            this.tsb_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_close.Name = "tsb_close";
            this.tsb_close.Size = new System.Drawing.Size(43, 50);
            this.tsb_close.Tag = "Cancel";
            this.tsb_close.Text = "&Close";
            this.tsb_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_close.Click += new System.EventHandler(this.tsb_close_Click);
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.Controls.Add(this.tls_SetupBusinessCenter);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(594, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupBusinessCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(594, 348);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupBusinessCenter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Business Center";
            this.Load += new System.EventHandler(this.frmSetupBusinessCenter_Load);
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlDataGridList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1BusinessCenter_UserAssociation)).EndInit();
            this.tls_SetupBusinessCenter.ResumeLayout(false);
            this.tls_SetupBusinessCenter.PerformLayout();
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label19;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupBusinessCenter;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Saveclose;
        private System.Windows.Forms.ToolStripButton tsb_close;
        private System.Windows.Forms.Panel pnltlsStrip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearStmtDisplaySettings;
        private System.Windows.Forms.Button btnBrowseStmtDisplaySettings;
        private System.Windows.Forms.TextBox txtDefaultStmtDisplaySettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Label lblPayTray;
        internal System.Windows.Forms.Button btnBrowseUsers;
        internal System.Windows.Forms.Button btnClearUsers;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pnlDataGridList;
        private System.Windows.Forms.Label lbl_pnlDataGridListTopBrd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_pnlDataGridListBottomBrd;
        private C1.Win.C1FlexGrid.C1FlexGrid C1BusinessCenter_UserAssociation;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;


    }
}