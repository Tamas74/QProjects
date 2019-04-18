namespace gloBilling
{
    partial class frmRpt_ReferralProviderPatients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRpt_ReferralProviderPatients));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcelOpen = new System.Windows.Forms.ToolStripButton();
            this.tls_btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlCriteria = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnClearReferrals = new System.Windows.Forms.Button();
            this.btnBrowseReferrals = new System.Windows.Forms.Button();
            this.lblReferral = new System.Windows.Forms.Label();
            this.cmbReferral = new System.Windows.Forms.ComboBox();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.btnClearProvider = new System.Windows.Forms.Button();
            this.btnBrowseProvider = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.pnlPatientDetails = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.C1Patients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlCriteria.SuspendLayout();
            this.pnlPatientDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(781, 55);
            this.pnlToolStrip.TabIndex = 89;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnExportToExcelOpen,
            this.tls_btnExportToExcel,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(781, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(93, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Show Report";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tls_btnExportToExcelOpen
            // 
            this.tls_btnExportToExcelOpen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcelOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcelOpen.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcelOpen.Image")));
            this.tls_btnExportToExcelOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcelOpen.Name = "tls_btnExportToExcelOpen";
            this.tls_btnExportToExcelOpen.Size = new System.Drawing.Size(154, 50);
            this.tls_btnExportToExcelOpen.Tag = "ExportnOpen";
            this.tls_btnExportToExcelOpen.Text = "Export To Excel && Open";
            this.tls_btnExportToExcelOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcelOpen.ToolTipText = "Export To Excel and Open";
            this.tls_btnExportToExcelOpen.Click += new System.EventHandler(this.tls_btnExportToExcelOpen_Click);
            // 
            // tls_btnExportToExcel
            // 
            this.tls_btnExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnExportToExcel.Image")));
            this.tls_btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnExportToExcel.Name = "tls_btnExportToExcel";
            this.tls_btnExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tls_btnExportToExcel.Tag = "Export";
            this.tls_btnExportToExcel.Text = "Export To Excel";
            this.tls_btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnExportToExcel.Click += new System.EventHandler(this.tls_btnExportToExcel_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.Controls.Add(this.label14);
            this.pnlCriteria.Controls.Add(this.label15);
            this.pnlCriteria.Controls.Add(this.label16);
            this.pnlCriteria.Controls.Add(this.label17);
            this.pnlCriteria.Controls.Add(this.cmbState);
            this.pnlCriteria.Controls.Add(this.lblState);
            this.pnlCriteria.Controls.Add(this.btnClearReferrals);
            this.pnlCriteria.Controls.Add(this.btnBrowseReferrals);
            this.pnlCriteria.Controls.Add(this.lblReferral);
            this.pnlCriteria.Controls.Add(this.cmbReferral);
            this.pnlCriteria.Controls.Add(this.btnClearInsurance);
            this.pnlCriteria.Controls.Add(this.btnBrowseInsurance);
            this.pnlCriteria.Controls.Add(this.label10);
            this.pnlCriteria.Controls.Add(this.cmbInsurance);
            this.pnlCriteria.Controls.Add(this.label6);
            this.pnlCriteria.Controls.Add(this.cmbGender);
            this.pnlCriteria.Controls.Add(this.label3);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.txtCity);
            this.pnlCriteria.Controls.Add(this.txtZipCode);
            this.pnlCriteria.Controls.Add(this.btnClearProvider);
            this.pnlCriteria.Controls.Add(this.btnBrowseProvider);
            this.pnlCriteria.Controls.Add(this.label5);
            this.pnlCriteria.Controls.Add(this.cmbProvider);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 55);
            this.pnlCriteria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCriteria.Size = new System.Drawing.Size(781, 76);
            this.pnlCriteria.TabIndex = 91;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label14.Location = new System.Drawing.Point(4, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(773, 1);
            this.label14.TabIndex = 248;
            this.label14.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 71);
            this.label15.TabIndex = 247;
            this.label15.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label16.Location = new System.Drawing.Point(777, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 71);
            this.label16.TabIndex = 246;
            this.label16.Text = "label3";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(775, 1);
            this.label17.TabIndex = 245;
            this.label17.Text = "label1";
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(527, 41);
            this.cmbState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(100, 22);
            this.cmbState.TabIndex = 244;
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoEllipsis = true;
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblState.Location = new System.Drawing.Point(479, 45);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 14);
            this.lblState.TabIndex = 243;
            this.lblState.Text = "State :";
            // 
            // btnClearReferrals
            // 
            this.btnClearReferrals.BackColor = System.Drawing.Color.Transparent;
            this.btnClearReferrals.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearReferrals.BackgroundImage")));
            this.btnClearReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearReferrals.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnClearReferrals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnClearReferrals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnClearReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReferrals.Image = ((System.Drawing.Image)(resources.GetObject("btnClearReferrals.Image")));
            this.btnClearReferrals.Location = new System.Drawing.Point(262, 41);
            this.btnClearReferrals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearReferrals.Name = "btnClearReferrals";
            this.btnClearReferrals.Size = new System.Drawing.Size(22, 22);
            this.btnClearReferrals.TabIndex = 242;
            this.btnClearReferrals.UseVisualStyleBackColor = false;
            this.btnClearReferrals.Click += new System.EventHandler(this.btnClearReferrals_Click);
            // 
            // btnBrowseReferrals
            // 
            this.btnBrowseReferrals.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseReferrals.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseReferrals.BackgroundImage")));
            this.btnBrowseReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseReferrals.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnBrowseReferrals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseReferrals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseReferrals.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseReferrals.Image")));
            this.btnBrowseReferrals.Location = new System.Drawing.Point(237, 41);
            this.btnBrowseReferrals.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseReferrals.Name = "btnBrowseReferrals";
            this.btnBrowseReferrals.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseReferrals.TabIndex = 241;
            this.btnBrowseReferrals.UseVisualStyleBackColor = false;
            this.btnBrowseReferrals.Click += new System.EventHandler(this.btnBrowseReferrals_Click);
            // 
            // lblReferral
            // 
            this.lblReferral.AutoSize = true;
            this.lblReferral.Location = new System.Drawing.Point(20, 44);
            this.lblReferral.Name = "lblReferral";
            this.lblReferral.Size = new System.Drawing.Size(60, 14);
            this.lblReferral.TabIndex = 240;
            this.lblReferral.Text = "Referral : ";
            // 
            // cmbReferral
            // 
            this.cmbReferral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferral.ForeColor = System.Drawing.Color.Black;
            this.cmbReferral.FormattingEnabled = true;
            this.cmbReferral.Location = new System.Drawing.Point(83, 41);
            this.cmbReferral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbReferral.Name = "cmbReferral";
            this.cmbReferral.Size = new System.Drawing.Size(149, 22);
            this.cmbReferral.TabIndex = 239;
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(553, 11);
            this.btnClearInsurance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsurance.TabIndex = 228;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseInsurance
            // 
            this.btnBrowseInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.BackgroundImage")));
            this.btnBrowseInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseInsurance.Image")));
            this.btnBrowseInsurance.Location = new System.Drawing.Point(528, 11);
            this.btnBrowseInsurance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 227;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(302, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 14);
            this.label10.TabIndex = 226;
            this.label10.Text = "Insurance :";
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(374, 12);
            this.cmbInsurance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(149, 22);
            this.cmbInsurance.TabIndex = 225;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(590, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 211;
            this.label6.Text = "Gender :";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.ForeColor = System.Drawing.Color.Black;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(651, 12);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(93, 22);
            this.cmbGender.TabIndex = 209;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(650, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 208;
            this.label3.Text = "Zip :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 206;
            this.label1.Text = "City :";
            // 
            // txtCity
            // 
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(374, 41);
            this.txtCity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(100, 22);
            this.txtCity.TabIndex = 201;
            // 
            // txtZipCode
            // 
            this.txtZipCode.ForeColor = System.Drawing.Color.Black;
            this.txtZipCode.Location = new System.Drawing.Point(684, 41);
            this.txtZipCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(60, 22);
            this.txtZipCode.TabIndex = 200;
            // 
            // btnClearProvider
            // 
            this.btnClearProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnClearProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.BackgroundImage")));
            this.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnClearProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnClearProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnClearProvider.Image")));
            this.btnClearProvider.Location = new System.Drawing.Point(262, 11);
            this.btnClearProvider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearProvider.Name = "btnClearProvider";
            this.btnClearProvider.Size = new System.Drawing.Size(22, 22);
            this.btnClearProvider.TabIndex = 187;
            this.btnClearProvider.UseVisualStyleBackColor = false;
            this.btnClearProvider.Click += new System.EventHandler(this.btnClearProvider_Click);
            // 
            // btnBrowseProvider
            // 
            this.btnBrowseProvider.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.BackgroundImage")));
            this.btnBrowseProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnBrowseProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnBrowseProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnBrowseProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseProvider.Image")));
            this.btnBrowseProvider.Location = new System.Drawing.Point(237, 11);
            this.btnBrowseProvider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowseProvider.Name = "btnBrowseProvider";
            this.btnBrowseProvider.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseProvider.TabIndex = 186;
            this.btnBrowseProvider.UseVisualStyleBackColor = false;
            this.btnBrowseProvider.Click += new System.EventHandler(this.btnBrowseProvider_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 185;
            this.label5.Text = "Provider :";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(83, 12);
            this.cmbProvider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(149, 22);
            this.cmbProvider.TabIndex = 184;
            // 
            // pnlPatientDetails
            // 
            this.pnlPatientDetails.Controls.Add(this.label2);
            this.pnlPatientDetails.Controls.Add(this.label4);
            this.pnlPatientDetails.Controls.Add(this.label7);
            this.pnlPatientDetails.Controls.Add(this.label8);
            this.pnlPatientDetails.Controls.Add(this.C1Patients);
            this.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientDetails.Location = new System.Drawing.Point(0, 157);
            this.pnlPatientDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlPatientDetails.Name = "pnlPatientDetails";
            this.pnlPatientDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.pnlPatientDetails.Size = new System.Drawing.Size(781, 478);
            this.pnlPatientDetails.TabIndex = 217;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(4, 475);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(773, 1);
            this.label2.TabIndex = 91;
            this.label2.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 474);
            this.label4.TabIndex = 90;
            this.label4.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(777, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 474);
            this.label7.TabIndex = 89;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(775, 1);
            this.label8.TabIndex = 88;
            this.label8.Text = "label1";
            // 
            // C1Patients
            // 
            this.C1Patients.AllowEditing = false;
            this.C1Patients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1Patients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1Patients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1Patients.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.C1Patients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1Patients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1Patients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1Patients.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1Patients.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.C1Patients.Location = new System.Drawing.Point(3, 1);
            this.C1Patients.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.C1Patients.Name = "C1Patients";
            this.C1Patients.Rows.Count = 1;
            this.C1Patients.Rows.DefaultSize = 19;
            this.C1Patients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1Patients.ShowCellLabels = true;
            this.C1Patients.Size = new System.Drawing.Size(775, 475);
            this.C1Patients.StyleInfo = resources.GetString("C1Patients.StyleInfo");
            this.C1Patients.TabIndex = 87;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 23);
            this.panel2.TabIndex = 86;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(773, 1);
            this.label9.TabIndex = 12;
            this.label9.Text = "label2";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 22);
            this.label11.TabIndex = 11;
            this.label11.Text = "label4";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label12.Location = new System.Drawing.Point(774, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 22);
            this.label12.TabIndex = 10;
            this.label12.Text = "label3";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(775, 1);
            this.label13.TabIndex = 9;
            this.label13.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(775, 23);
            this.label18.TabIndex = 240;
            this.label18.Text = "  Referral ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 131);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.panel1.Size = new System.Drawing.Size(781, 26);
            this.panel1.TabIndex = 218;
            // 
            // frmRpt_ReferralProviderPatients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(781, 635);
            this.Controls.Add(this.pnlPatientDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmRpt_ReferralProviderPatients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Provider Referral Patients Report";
            this.Load += new System.EventHandler(this.frmRpt_ReferralProviderPatients_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            this.pnlPatientDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1Patients)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcel;
        private System.Windows.Forms.Panel pnlCriteria;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbInsurance;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtZipCode;
        internal System.Windows.Forms.Button btnClearProvider;
        internal System.Windows.Forms.Button btnBrowseProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Panel pnlPatientDetails;
        private C1.Win.C1FlexGrid.C1FlexGrid C1Patients;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Button btnClearReferrals;
        internal System.Windows.Forms.Button btnBrowseReferrals;
        private System.Windows.Forms.Label lblReferral;
        private System.Windows.Forms.ComboBox cmbReferral;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripButton tls_btnExportToExcelOpen;

    }
}