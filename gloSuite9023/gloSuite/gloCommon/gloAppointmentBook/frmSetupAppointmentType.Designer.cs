namespace gloAppointmentBook
{
    partial class frmSetupAppointmentType
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
                    if (clDlg != null)
                    {

                        clDlg.Dispose();
                        clDlg = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupAppointmentType));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSelectedProblemtype = new System.Windows.Forms.Label();
            this.lblProblemtype = new System.Windows.Forms.Label();
            this.pnlAppointmentTypeDetails = new System.Windows.Forms.Panel();
            this.chkTurnOffReminders = new System.Windows.Forms.CheckBox();
            this.chkPriorAuthorizationRequired = new System.Windows.Forms.CheckBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.numAppDurationMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAppointmentType = new System.Windows.Forms.ComboBox();
            this.lblmin = new System.Windows.Forms.Label();
            this.numAppDurationHour = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowseAppColor = new System.Windows.Forms.Button();
            this.lblAppColor = new System.Windows.Forms.Label();
            this.txtAppColor = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.lblAppType = new System.Windows.Forms.Label();
            this.lbl_B = new System.Windows.Forms.Label();
            this.lbl_T = new System.Windows.Forms.Label();
            this.lbl_R = new System.Windows.Forms.Label();
            this.lbl_L = new System.Windows.Forms.Label();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.trvSelectedResources = new System.Windows.Forms.TreeView();
            this.trvResources = new System.Windows.Forms.TreeView();
            this.txtAppType = new System.Windows.Forms.TextBox();
            this.clDlg = new System.Windows.Forms.ColorDialog();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ts_Commands.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlAppointmentTypeDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAppDurationMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAppDurationHour)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(424, 53);
            this.ts_Commands.TabIndex = 13;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
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
            this.tsb_Cancel.Size = new System.Drawing.Size(47, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " &Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.ToolTipText = "Close";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.lblSelectedProblemtype);
            this.pnlSearch.Controls.Add(this.lblProblemtype);
            this.pnlSearch.Controls.Add(this.pnlAppointmentTypeDetails);
            this.pnlSearch.Controls.Add(this.panel1);
            this.pnlSearch.Controls.Add(this.lbl_B);
            this.pnlSearch.Controls.Add(this.lbl_T);
            this.pnlSearch.Controls.Add(this.lbl_R);
            this.pnlSearch.Controls.Add(this.lbl_L);
            this.pnlSearch.Controls.Add(this.btnRemoveAll);
            this.pnlSearch.Controls.Add(this.btnRemove);
            this.pnlSearch.Controls.Add(this.btnAddAll);
            this.pnlSearch.Controls.Add(this.btnAdd);
            this.pnlSearch.Controls.Add(this.trvSelectedResources);
            this.pnlSearch.Controls.Add(this.trvResources);
            this.pnlSearch.Controls.Add(this.txtAppType);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(424, 401);
            this.pnlSearch.TabIndex = 0;
            // 
            // lblSelectedProblemtype
            // 
            this.lblSelectedProblemtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedProblemtype.AutoSize = true;
            this.lblSelectedProblemtype.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedProblemtype.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedProblemtype.Location = new System.Drawing.Point(234, 160);
            this.lblSelectedProblemtype.Name = "lblSelectedProblemtype";
            this.lblSelectedProblemtype.Size = new System.Drawing.Size(154, 14);
            this.lblSelectedProblemtype.TabIndex = 50;
            this.lblSelectedProblemtype.Text = "Selected Problem Type :";
            this.lblSelectedProblemtype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProblemtype
            // 
            this.lblProblemtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProblemtype.AutoSize = true;
            this.lblProblemtype.BackColor = System.Drawing.Color.Transparent;
            this.lblProblemtype.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProblemtype.Location = new System.Drawing.Point(17, 160);
            this.lblProblemtype.Name = "lblProblemtype";
            this.lblProblemtype.Size = new System.Drawing.Size(98, 14);
            this.lblProblemtype.TabIndex = 50;
            this.lblProblemtype.Text = "Problem Type :";
            this.lblProblemtype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAppointmentTypeDetails
            // 
            this.pnlAppointmentTypeDetails.Controls.Add(this.chkTurnOffReminders);
            this.pnlAppointmentTypeDetails.Controls.Add(this.chkPriorAuthorizationRequired);
            this.pnlAppointmentTypeDetails.Controls.Add(this.lblDuration);
            this.pnlAppointmentTypeDetails.Controls.Add(this.numAppDurationMin);
            this.pnlAppointmentTypeDetails.Controls.Add(this.label2);
            this.pnlAppointmentTypeDetails.Controls.Add(this.cmbAppointmentType);
            this.pnlAppointmentTypeDetails.Controls.Add(this.lblmin);
            this.pnlAppointmentTypeDetails.Controls.Add(this.numAppDurationHour);
            this.pnlAppointmentTypeDetails.Controls.Add(this.label1);
            this.pnlAppointmentTypeDetails.Controls.Add(this.btnBrowseAppColor);
            this.pnlAppointmentTypeDetails.Controls.Add(this.lblAppColor);
            this.pnlAppointmentTypeDetails.Controls.Add(this.txtAppColor);
            this.pnlAppointmentTypeDetails.Location = new System.Drawing.Point(77, 43);
            this.pnlAppointmentTypeDetails.Name = "pnlAppointmentTypeDetails";
            this.pnlAppointmentTypeDetails.Size = new System.Drawing.Size(337, 114);
            this.pnlAppointmentTypeDetails.TabIndex = 2;
            // 
            // chkTurnOffReminders
            // 
            this.chkTurnOffReminders.AutoSize = true;
            this.chkTurnOffReminders.Location = new System.Drawing.Point(82, 93);
            this.chkTurnOffReminders.Name = "chkTurnOffReminders";
            this.chkTurnOffReminders.Size = new System.Drawing.Size(131, 18);
            this.chkTurnOffReminders.TabIndex = 6;
            this.chkTurnOffReminders.Text = "Turn off Reminders";
            this.chkTurnOffReminders.UseVisualStyleBackColor = true;
            // 
            // chkPriorAuthorizationRequired
            // 
            this.chkPriorAuthorizationRequired.AutoSize = true;
            this.chkPriorAuthorizationRequired.Location = new System.Drawing.Point(82, 71);
            this.chkPriorAuthorizationRequired.Name = "chkPriorAuthorizationRequired";
            this.chkPriorAuthorizationRequired.Size = new System.Drawing.Size(221, 18);
            this.chkPriorAuthorizationRequired.TabIndex = 5;
            this.chkPriorAuthorizationRequired.Text = "Default Prior Authorization Required";
            this.chkPriorAuthorizationRequired.UseVisualStyleBackColor = true;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(16, 7);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(61, 14);
            this.lblDuration.TabIndex = 19;
            this.lblDuration.Text = "Duration :";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numAppDurationMin
            // 
            this.numAppDurationMin.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAppDurationMin.ForeColor = System.Drawing.Color.Black;
            this.numAppDurationMin.Location = new System.Drawing.Point(133, 3);
            this.numAppDurationMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numAppDurationMin.Name = "numAppDurationMin";
            this.numAppDurationMin.Size = new System.Drawing.Size(42, 22);
            this.numAppDurationMin.TabIndex = 1;
            this.numAppDurationMin.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numAppDurationMin.ValueChanged += new System.EventHandler(this.numAppDurationMin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 49;
            this.label2.Text = "Type :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAppointmentType
            // 
            this.cmbAppointmentType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbAppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppointmentType.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmbAppointmentType.FormattingEnabled = true;
            this.cmbAppointmentType.Location = new System.Drawing.Point(245, 3);
            this.cmbAppointmentType.Name = "cmbAppointmentType";
            this.cmbAppointmentType.Size = new System.Drawing.Size(87, 22);
            this.cmbAppointmentType.TabIndex = 2;
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.BackColor = System.Drawing.Color.Transparent;
            this.lblmin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmin.Location = new System.Drawing.Point(143, 26);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(23, 13);
            this.lblmin.TabIndex = 20;
            this.lblmin.Text = "mm";
            this.lblmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numAppDurationHour
            // 
            this.numAppDurationHour.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numAppDurationHour.ForeColor = System.Drawing.Color.Black;
            this.numAppDurationHour.Location = new System.Drawing.Point(82, 3);
            this.numAppDurationHour.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numAppDurationHour.Name = "numAppDurationHour";
            this.numAppDurationHour.Size = new System.Drawing.Size(43, 22);
            this.numAppDurationHour.TabIndex = 0;
            this.numAppDurationHour.ValueChanged += new System.EventHandler(this.numAppDurationHour_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "hh";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnBrowseAppColor
            // 
            this.btnBrowseAppColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppColor.BackgroundImage")));
            this.btnBrowseAppColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseAppColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseAppColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAppColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseAppColor.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseAppColor.Image")));
            this.btnBrowseAppColor.Location = new System.Drawing.Point(112, 41);
            this.btnBrowseAppColor.Name = "btnBrowseAppColor";
            this.btnBrowseAppColor.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseAppColor.TabIndex = 3;
            this.btnBrowseAppColor.UseVisualStyleBackColor = true;
            this.btnBrowseAppColor.Click += new System.EventHandler(this.btnBrowseAppColor_Click);
            this.btnBrowseAppColor.MouseLeave += new System.EventHandler(this.btnBrowseAppColor_MouseLeave);
            this.btnBrowseAppColor.MouseHover += new System.EventHandler(this.btnBrowseAppColor_MouseHover);
            // 
            // lblAppColor
            // 
            this.lblAppColor.AutoSize = true;
            this.lblAppColor.BackColor = System.Drawing.Color.Transparent;
            this.lblAppColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppColor.Location = new System.Drawing.Point(34, 45);
            this.lblAppColor.Name = "lblAppColor";
            this.lblAppColor.Size = new System.Drawing.Size(42, 14);
            this.lblAppColor.TabIndex = 21;
            this.lblAppColor.Text = "Color :";
            this.lblAppColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAppColor
            // 
            this.txtAppColor.BackColor = System.Drawing.Color.White;
            this.txtAppColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppColor.ForeColor = System.Drawing.Color.Black;
            this.txtAppColor.Location = new System.Drawing.Point(82, 41);
            this.txtAppColor.Name = "txtAppColor";
            this.txtAppColor.ReadOnly = true;
            this.txtAppColor.Size = new System.Drawing.Size(26, 22);
            this.txtAppColor.TabIndex = 4;
            this.txtAppColor.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblAppType);
            this.panel1.Location = new System.Drawing.Point(14, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 19);
            this.panel1.TabIndex = 108;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(7, 0);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 107;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAppType
            // 
            this.lblAppType.AutoEllipsis = true;
            this.lblAppType.AutoSize = true;
            this.lblAppType.BackColor = System.Drawing.Color.Transparent;
            this.lblAppType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAppType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppType.Location = new System.Drawing.Point(21, 0);
            this.lblAppType.Name = "lblAppType";
            this.lblAppType.Size = new System.Drawing.Size(119, 14);
            this.lblAppType.TabIndex = 6;
            this.lblAppType.Text = "Appointment Type :";
            this.lblAppType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_B
            // 
            this.lbl_B.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_B.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_B.Location = new System.Drawing.Point(4, 397);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(416, 1);
            this.lbl_B.TabIndex = 33;
            // 
            // lbl_T
            // 
            this.lbl_T.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_T.Location = new System.Drawing.Point(4, 3);
            this.lbl_T.Name = "lbl_T";
            this.lbl_T.Size = new System.Drawing.Size(416, 1);
            this.lbl_T.TabIndex = 32;
            // 
            // lbl_R
            // 
            this.lbl_R.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_R.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_R.Location = new System.Drawing.Point(420, 3);
            this.lbl_R.Name = "lbl_R";
            this.lbl_R.Size = new System.Drawing.Size(1, 395);
            this.lbl_R.TabIndex = 31;
            // 
            // lbl_L
            // 
            this.lbl_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_L.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_L.Location = new System.Drawing.Point(3, 3);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(1, 395);
            this.lbl_L.TabIndex = 30;
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveAll.BackgroundImage")));
            this.btnRemoveAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveAll.Image")));
            this.btnRemoveAll.Location = new System.Drawing.Point(202, 325);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(22, 22);
            this.btnRemoveAll.TabIndex = 7;
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            this.btnRemoveAll.MouseLeave += new System.EventHandler(this.btnRemoveAll_MouseLeave);
            this.btnRemoveAll.MouseHover += new System.EventHandler(this.btnRemoveAll_MouseHover);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(202, 298);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(22, 22);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.MouseLeave += new System.EventHandler(this.btnRemove_MouseLeave);
            this.btnRemove.MouseHover += new System.EventHandler(this.btnRemove_MouseHover);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddAll.BackgroundImage")));
            this.btnAddAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAll.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAll.Image")));
            this.btnAddAll.Location = new System.Drawing.Point(202, 243);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(22, 22);
            this.btnAddAll.TabIndex = 5;
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            this.btnAddAll.MouseLeave += new System.EventHandler(this.btnAddAll_MouseLeave);
            this.btnAddAll.MouseHover += new System.EventHandler(this.btnAddAll_MouseHover);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(202, 216);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(22, 22);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            this.btnAdd.MouseHover += new System.EventHandler(this.btnAdd_MouseHover);
            // 
            // trvSelectedResources
            // 
            this.trvSelectedResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvSelectedResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvSelectedResources.ForeColor = System.Drawing.Color.Black;
            this.trvSelectedResources.Location = new System.Drawing.Point(235, 177);
            this.trvSelectedResources.Name = "trvSelectedResources";
            this.trvSelectedResources.Size = new System.Drawing.Size(174, 209);
            this.trvSelectedResources.TabIndex = 8;
            // 
            // trvResources
            // 
            this.trvResources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvResources.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvResources.ForeColor = System.Drawing.Color.Black;
            this.trvResources.Location = new System.Drawing.Point(16, 177);
            this.trvResources.Name = "trvResources";
            this.trvResources.Size = new System.Drawing.Size(174, 209);
            this.trvResources.TabIndex = 3;
            // 
            // txtAppType
            // 
            this.txtAppType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppType.ForeColor = System.Drawing.Color.Black;
            this.txtAppType.Location = new System.Drawing.Point(158, 18);
            this.txtAppType.Name = "txtAppType";
            this.txtAppType.Size = new System.Drawing.Size(252, 22);
            this.txtAppType.TabIndex = 1;
            this.txtAppType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAppType_KeyPress);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(424, 53);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Appointment type_01.ico");
            this.imageList1.Images.SetKeyName(1, "Procedure.ico");
            // 
            // frmSetupAppointmentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(424, 454);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupAppointmentType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appointment Type";
            this.Load += new System.EventHandler(this.frmSetupAppointmentType_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlAppointmentTypeDetails.ResumeLayout(false);
            this.pnlAppointmentTypeDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAppDurationMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAppDurationHour)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtAppType;
        private System.Windows.Forms.Label lblAppType;
        private System.Windows.Forms.Label lblmin;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown numAppDurationMin;
        private System.Windows.Forms.Label lblAppColor;
        private System.Windows.Forms.Button btnBrowseAppColor;
        private System.Windows.Forms.TextBox txtAppColor;
        private System.Windows.Forms.ColorDialog clDlg;
        private System.Windows.Forms.TreeView trvSelectedResources;
        private System.Windows.Forms.TreeView trvResources;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Label lbl_B;
        private System.Windows.Forms.Label lbl_T;
        private System.Windows.Forms.Label lbl_R;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.NumericUpDown numAppDurationHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAppointmentType;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlAppointmentTypeDetails;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.Label lblSelectedProblemtype;
        private System.Windows.Forms.Label lblProblemtype;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkPriorAuthorizationRequired;
        private System.Windows.Forms.CheckBox chkTurnOffReminders;
    }
}