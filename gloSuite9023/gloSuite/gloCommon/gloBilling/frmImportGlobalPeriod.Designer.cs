namespace gloBilling
{
    partial class frmImportGlobalPeriod
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
                    if (dlgBrowseFile != null)
                    {

                        dlgBrowseFile.Dispose();
                        dlgBrowseFile = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportGlobalPeriod));
            this.pnlFeeSchedule = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.c1RVUSchedule = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlSpeciality = new System.Windows.Forms.Panel();
            this.btnClearFile = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbSpecificInsCompany = new System.Windows.Forms.RadioButton();
            this.rbAllInsCompany = new System.Windows.Forms.RadioButton();
            this.cmbInsCompany = new System.Windows.Forms.ComboBox();
            this.btnBrowseInsCompany = new System.Windows.Forms.Button();
            this.btnClearInsCompany = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtImportFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CPTStatus = new System.Windows.Forms.StatusStrip();
            this.CPTProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSaveCls = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.dlgBrowseFile = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tooltip_Billing = new System.Windows.Forms.ToolTip(this.components);
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlFeeSchedule.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1RVUSchedule)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlSpeciality.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.CPTStatus.SuspendLayout();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFeeSchedule
            // 
            this.pnlFeeSchedule.Controls.Add(this.pnlDetails);
            this.pnlFeeSchedule.Controls.Add(this.pnlSearch);
            this.pnlFeeSchedule.Controls.Add(this.pnlSpeciality);
            this.pnlFeeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeeSchedule.Location = new System.Drawing.Point(0, 54);
            this.pnlFeeSchedule.Name = "pnlFeeSchedule";
            this.pnlFeeSchedule.Size = new System.Drawing.Size(606, 576);
            this.pnlFeeSchedule.TabIndex = 0;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.label20);
            this.pnlDetails.Controls.Add(this.label19);
            this.pnlDetails.Controls.Add(this.label18);
            this.pnlDetails.Controls.Add(this.label17);
            this.pnlDetails.Controls.Add(this.c1RVUSchedule);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 195);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlDetails.Size = new System.Drawing.Size(606, 381);
            this.pnlDetails.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(602, 1);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 376);
            this.label20.TabIndex = 20;
            this.label20.Text = "label20";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(3, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 376);
            this.label19.TabIndex = 0;
            this.label19.Text = "label19";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(3, 377);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(600, 1);
            this.label18.TabIndex = 18;
            this.label18.Text = "label18";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(600, 1);
            this.label17.TabIndex = 0;
            this.label17.Text = "label17";
            // 
            // c1RVUSchedule
            // 
            this.c1RVUSchedule.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1RVUSchedule.AutoGenerateColumns = false;
            this.c1RVUSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1RVUSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1RVUSchedule.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1RVUSchedule.ColumnInfo = resources.GetString("c1RVUSchedule.ColumnInfo");
            this.c1RVUSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1RVUSchedule.ExtendLastCol = true;
            this.c1RVUSchedule.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1RVUSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1RVUSchedule.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1RVUSchedule.Location = new System.Drawing.Point(3, 0);
            this.c1RVUSchedule.Name = "c1RVUSchedule";
            this.c1RVUSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.c1RVUSchedule.Rows.Count = 1;
            this.c1RVUSchedule.Rows.DefaultSize = 19;
            this.c1RVUSchedule.Rows.GlyphRow = 0;
            this.c1RVUSchedule.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1RVUSchedule.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1RVUSchedule.Size = new System.Drawing.Size(600, 378);
            this.c1RVUSchedule.StyleInfo = resources.GetString("c1RVUSchedule.StyleInfo");
            this.c1RVUSchedule.TabIndex = 0;
            this.c1RVUSchedule.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_SetupEditor);
            this.c1RVUSchedule.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1RVUSchedule_KeyPressEdit);
            this.c1RVUSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1RVUSchedule_MouseMove);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.panel4);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 168);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(606, 27);
            this.pnlSearch.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.txtSearch);
            this.panel4.Controls.Add(this.lblSearch);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(600, 24);
            this.panel4.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(65, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(108, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(1, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(64, 17);
            this.lblSearch.TabIndex = 6;
            this.lblSearch.Text = "  Search :";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(1, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(598, 1);
            this.label7.TabIndex = 8;
            this.label7.Text = "label2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "label4";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(599, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 23);
            this.label9.TabIndex = 6;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(600, 1);
            this.label10.TabIndex = 5;
            this.label10.Text = "label1";
            // 
            // pnlSpeciality
            // 
            this.pnlSpeciality.Controls.Add(this.btnClearFile);
            this.pnlSpeciality.Controls.Add(this.panel3);
            this.pnlSpeciality.Controls.Add(this.panel1);
            this.pnlSpeciality.Controls.Add(this.label21);
            this.pnlSpeciality.Controls.Add(this.label15);
            this.pnlSpeciality.Controls.Add(this.txtImportFile);
            this.pnlSpeciality.Controls.Add(this.label5);
            this.pnlSpeciality.Controls.Add(this.btn_Browse);
            this.pnlSpeciality.Controls.Add(this.label6);
            this.pnlSpeciality.Controls.Add(this.label14);
            this.pnlSpeciality.Controls.Add(this.label16);
            this.pnlSpeciality.Controls.Add(this.label13);
            this.pnlSpeciality.Controls.Add(this.label12);
            this.pnlSpeciality.Controls.Add(this.label11);
            this.pnlSpeciality.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeciality.Location = new System.Drawing.Point(0, 0);
            this.pnlSpeciality.Name = "pnlSpeciality";
            this.pnlSpeciality.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSpeciality.Size = new System.Drawing.Size(606, 168);
            this.pnlSpeciality.TabIndex = 1;
            // 
            // btnClearFile
            // 
            this.btnClearFile.BackColor = System.Drawing.Color.Transparent;
            this.btnClearFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearFile.BackgroundImage")));
            this.btnClearFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFile.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFile.Image")));
            this.btnClearFile.Location = new System.Drawing.Point(454, 134);
            this.btnClearFile.Name = "btnClearFile";
            this.btnClearFile.Size = new System.Drawing.Size(22, 22);
            this.btnClearFile.TabIndex = 132;
            this.c1SuperTooltip1.SetToolTip(this.btnClearFile, "Clear All");
            this.btnClearFile.UseVisualStyleBackColor = false;
            this.btnClearFile.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbCustom);
            this.panel3.Controls.Add(this.rbStandard);
            this.panel3.Location = new System.Drawing.Point(153, 107);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(298, 22);
            this.panel3.TabIndex = 134;
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Checked = true;
            this.rbCustom.Dock = System.Windows.Forms.DockStyle.Left;
            this.rbCustom.Location = new System.Drawing.Point(0, 0);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(119, 22);
            this.rbCustom.TabIndex = 120;
            this.rbCustom.TabStop = true;
            this.rbCustom.Text = "CMS PPRRVU.xlsx";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbStandard.Location = new System.Drawing.Point(150, 0);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(148, 22);
            this.rbStandard.TabIndex = 118;
            this.rbStandard.Text = "gloStream Layout .xlsx";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbSpecificInsCompany);
            this.panel1.Controls.Add(this.rbAllInsCompany);
            this.panel1.Controls.Add(this.cmbInsCompany);
            this.panel1.Controls.Add(this.btnBrowseInsCompany);
            this.panel1.Controls.Add(this.btnClearInsCompany);
            this.panel1.Location = new System.Drawing.Point(153, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 62);
            this.panel1.TabIndex = 133;
            // 
            // rbSpecificInsCompany
            // 
            this.rbSpecificInsCompany.AutoSize = true;
            this.rbSpecificInsCompany.Location = new System.Drawing.Point(5, 34);
            this.rbSpecificInsCompany.Name = "rbSpecificInsCompany";
            this.rbSpecificInsCompany.Size = new System.Drawing.Size(177, 18);
            this.rbSpecificInsCompany.TabIndex = 130;
            this.rbSpecificInsCompany.Text = "Specific Insurance Company";
            this.rbSpecificInsCompany.UseVisualStyleBackColor = true;
            this.rbSpecificInsCompany.CheckedChanged += new System.EventHandler(this.rbSpecificInsCompany_CheckedChanged);
            // 
            // rbAllInsCompany
            // 
            this.rbAllInsCompany.AutoSize = true;
            this.rbAllInsCompany.Checked = true;
            this.rbAllInsCompany.Location = new System.Drawing.Point(5, 10);
            this.rbAllInsCompany.Name = "rbAllInsCompany";
            this.rbAllInsCompany.Size = new System.Drawing.Size(156, 18);
            this.rbAllInsCompany.TabIndex = 131;
            this.rbAllInsCompany.TabStop = true;
            this.rbAllInsCompany.Text = "All Insurance Companies";
            this.rbAllInsCompany.UseVisualStyleBackColor = true;
            this.rbAllInsCompany.CheckedChanged += new System.EventHandler(this.rbAllInsCompany_CheckedChanged);
            // 
            // cmbInsCompany
            // 
            this.cmbInsCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsCompany.FormattingEnabled = true;
            this.cmbInsCompany.Location = new System.Drawing.Point(204, 32);
            this.cmbInsCompany.Name = "cmbInsCompany";
            this.cmbInsCompany.Size = new System.Drawing.Size(177, 22);
            this.cmbInsCompany.TabIndex = 126;
            this.cmbInsCompany.SelectedIndexChanged += new System.EventHandler(this.cmbInsCompany_SelectedIndexChanged);
            this.cmbInsCompany.MouseEnter += new System.EventHandler(this.cmbInsCompany_MouseEnter);
            this.cmbInsCompany.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbInsCompany_MouseMove);
            // 
            // btnBrowseInsCompany
            // 
            this.btnBrowseInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsCompany.BackgroundImage")));
            this.btnBrowseInsCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsCompany.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsCompany.Image")));
            this.btnBrowseInsCompany.Location = new System.Drawing.Point(384, 32);
            this.btnBrowseInsCompany.Name = "btnBrowseInsCompany";
            this.btnBrowseInsCompany.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsCompany.TabIndex = 127;
            this.c1SuperTooltip1.SetToolTip(this.btnBrowseInsCompany, "Browse Insurance Comapny");
            this.btnBrowseInsCompany.UseVisualStyleBackColor = false;
            this.btnBrowseInsCompany.Click += new System.EventHandler(this.btnBrowseInsCompany_Click);
            // 
            // btnClearInsCompany
            // 
            this.btnClearInsCompany.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsCompany.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.BackgroundImage")));
            this.btnClearInsCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsCompany.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsCompany.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsCompany.Image")));
            this.btnClearInsCompany.Location = new System.Drawing.Point(409, 32);
            this.btnClearInsCompany.Name = "btnClearInsCompany";
            this.btnClearInsCompany.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsCompany.TabIndex = 128;
            this.c1SuperTooltip1.SetToolTip(this.btnClearInsCompany, "Clear All");
            this.btnClearInsCompany.UseVisualStyleBackColor = false;
            this.btnClearInsCompany.Click += new System.EventHandler(this.btnClearInsCompany_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(22, 51);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 14);
            this.label21.TabIndex = 132;
            this.label21.Text = "Settings will apply to :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(476, 14);
            this.label15.TabIndex = 129;
            this.label15.Text = "CPT settings will be updated for each CPT mentioned in selected Import file.";
            // 
            // txtImportFile
            // 
            this.txtImportFile.BackColor = System.Drawing.Color.White;
            this.txtImportFile.ForeColor = System.Drawing.Color.Black;
            this.txtImportFile.Location = new System.Drawing.Point(151, 134);
            this.txtImportFile.Name = "txtImportFile";
            this.txtImportFile.ReadOnly = true;
            this.txtImportFile.Size = new System.Drawing.Size(275, 22);
            this.txtImportFile.TabIndex = 122;
            this.txtImportFile.TabStop = false;
            this.txtImportFile.MouseEnter += new System.EventHandler(this.txtImportFile_MouseEnter);
            this.txtImportFile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtImportFile_MouseMove);
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(69, 138);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 123;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btn_Browse
            // 
            this.btn_Browse.AutoEllipsis = true;
            this.btn_Browse.BackColor = System.Drawing.Color.Transparent;
            this.btn_Browse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Browse.BackgroundImage")));
            this.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Browse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Browse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Image = ((System.Drawing.Image)(resources.GetObject("btn_Browse.Image")));
            this.btn_Browse.Location = new System.Drawing.Point(429, 134);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(22, 22);
            this.btn_Browse.TabIndex = 119;
            this.c1SuperTooltip1.SetToolTip(this.btn_Browse, "Import");
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 121;
            this.label6.Text = "File Name :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(56, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 14);
            this.label14.TabIndex = 124;
            this.label14.Text = "Data File Type :";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(602, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 160);
            this.label16.TabIndex = 18;
            this.label16.Text = "label16";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(599, 1);
            this.label13.TabIndex = 17;
            this.label13.Text = "label13";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(4, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(599, 1);
            this.label12.TabIndex = 16;
            this.label12.Text = "label12";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 162);
            this.label11.TabIndex = 15;
            this.label11.Text = "label11";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.CPTStatus);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 630);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(606, 29);
            this.panel6.TabIndex = 23;
            // 
            // CPTStatus
            // 
            this.CPTStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CPTProgressBar});
            this.CPTStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.CPTStatus.Location = new System.Drawing.Point(3, 4);
            this.CPTStatus.Name = "CPTStatus";
            this.CPTStatus.Size = new System.Drawing.Size(600, 22);
            this.CPTStatus.TabIndex = 21;
            this.CPTStatus.Text = "statusStrip1";
            // 
            // CPTProgressBar
            // 
            this.CPTProgressBar.Name = "CPTProgressBar";
            this.CPTProgressBar.Size = new System.Drawing.Size(575, 16);
            // 
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.TopToolStrip);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(606, 54);
            this.pnlTopToolStrip.TabIndex = 3;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSaveCls,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(606, 53);
            this.TopToolStrip.TabIndex = 7;
            this.TopToolStrip.TabStop = true;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // ts_btnSaveCls
            // 
            this.ts_btnSaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnSaveCls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnSaveCls.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveCls.Image")));
            this.ts_btnSaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveCls.Name = "ts_btnSaveCls";
            this.ts_btnSaveCls.Size = new System.Drawing.Size(66, 50);
            this.ts_btnSaveCls.Tag = "SaveFeeSchedule";
            this.ts_btnSaveCls.Text = "Sa&ve&&Cls";
            this.ts_btnSaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnSaveCls.ToolTipText = "Save and Close";
            this.ts_btnSaveCls.Click += new System.EventHandler(this.ts_btnSaveCls_Click);
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
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // dlgBrowseFile
            // 
            this.dlgBrowseFile.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlFeeSchedule);
            this.panel2.Controls.Add(this.pnlTopToolStrip);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(606, 659);
            this.panel2.TabIndex = 21;
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmImportGlobalPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(606, 659);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportGlobalPeriod";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Global Period";
            this.Load += new System.EventHandler(this.frmImportGlobalPeriod_Load);
            this.pnlFeeSchedule.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1RVUSchedule)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlSpeciality.ResumeLayout(false);
            this.pnlSpeciality.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.CPTStatus.ResumeLayout(false);
            this.CPTStatus.PerformLayout();
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFeeSchedule;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel pnlSpeciality;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSaveCls;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtImportFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.OpenFileDialog dlgBrowseFile;
        private C1.Win.C1FlexGrid.C1FlexGrid c1RVUSchedule;
        internal System.Windows.Forms.Button btnBrowseInsCompany;
        internal System.Windows.Forms.Button btnClearInsCompany;
        private System.Windows.Forms.ComboBox cmbInsCompany;
        private System.Windows.Forms.RadioButton rbSpecificInsCompany;
        private System.Windows.Forms.RadioButton rbAllInsCompany;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Button btnClearFile;
        private System.Windows.Forms.ToolTip tooltip_Billing;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
        private System.Windows.Forms.StatusStrip CPTStatus;
        private System.Windows.Forms.ToolStripProgressBar CPTProgressBar;
        private System.Windows.Forms.Panel panel6;
    }
}