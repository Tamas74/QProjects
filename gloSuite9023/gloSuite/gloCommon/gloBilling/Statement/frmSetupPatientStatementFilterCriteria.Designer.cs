namespace gloBilling
{
    partial class frmSetupPatientStatementFilterCriteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPatientStatementFilterCriteria));
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnOK = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.nmWaitFordays = new System.Windows.Forms.NumericUpDown();
            this.cmbNameTo = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.cmbNameFrom = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.txtWaitFordays = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblCPT = new System.Windows.Forms.Label();
            this.cmbCPT = new System.Windows.Forms.ComboBox();
            this.btnClearCPT = new System.Windows.Forms.Button();
            this.btnBrowseCPT = new System.Windows.Forms.Button();
            this.btnClearInsurance = new System.Windows.Forms.Button();
            this.btnBrowseInsurance = new System.Windows.Forms.Button();
            this.txtDueAmount = new System.Windows.Forms.TextBox();
            this.rbGreaterthen = new System.Windows.Forms.RadioButton();
            this.rbLessThen = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtZipCode = new System.Windows.Forms.TextBox();
            this.cmbFacility = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPaymentTray = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbChargesTray = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbInsurance = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imgtabControl1 = new System.Windows.Forms.ImageList(this.components);
            this.Panel21 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.txtStatementCriteriaName = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.Label47 = new System.Windows.Forms.Label();
            this.Panel20 = new System.Windows.Forms.Panel();
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaitFordays)).BeginInit();
            this.Panel21.SuspendLayout();
            this.Panel20.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.Controls.Add(this.tstrip);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(433, 54);
            this.pnl_tlsp_Top.TabIndex = 18;
            // 
            // tstrip
            // 
            this.tstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tstrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstrip.BackgroundImage")));
            this.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.btnOK,
            this.btnClose});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.Size = new System.Drawing.Size(433, 53);
            this.tstrip.TabIndex = 0;
            this.tstrip.Text = "ToolStrip1";
            this.tstrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "&Save";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 50);
            this.btnOK.Tag = "Save&Close";
            this.btnOK.Text = "Sa&ve&&Cls";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOK.ToolTipText = "Save and Close";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 50);
            this.btnClose.Tag = "Close";
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label62);
            this.panel1.Controls.Add(this.label61);
            this.panel1.Controls.Add(this.nmWaitFordays);
            this.panel1.Controls.Add(this.cmbNameTo);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.label60);
            this.panel1.Controls.Add(this.cmbNameFrom);
            this.panel1.Controls.Add(this.label58);
            this.panel1.Controls.Add(this.label57);
            this.panel1.Controls.Add(this.txtWaitFordays);
            this.panel1.Controls.Add(this.label56);
            this.panel1.Controls.Add(this.label55);
            this.panel1.Controls.Add(this.label54);
            this.panel1.Controls.Add(this.label53);
            this.panel1.Controls.Add(this.lblCPT);
            this.panel1.Controls.Add(this.cmbCPT);
            this.panel1.Controls.Add(this.btnClearCPT);
            this.panel1.Controls.Add(this.btnBrowseCPT);
            this.panel1.Controls.Add(this.btnClearInsurance);
            this.panel1.Controls.Add(this.btnBrowseInsurance);
            this.panel1.Controls.Add(this.txtDueAmount);
            this.panel1.Controls.Add(this.rbGreaterthen);
            this.panel1.Controls.Add(this.rbLessThen);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtZipCode);
            this.panel1.Controls.Add(this.cmbFacility);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbPaymentTray);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbChargesTray);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbInsurance);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(433, 102);
            this.panel1.TabIndex = 82;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(230, 38);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(32, 14);
            this.label62.TabIndex = 320;
            this.label62.Text = "Days";
            this.label62.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label61.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Location = new System.Drawing.Point(68, 38);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(59, 14);
            this.label61.TabIndex = 319;
            this.label61.Text = "Wait for :";
            this.label61.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // nmWaitFordays
            // 
            this.nmWaitFordays.Location = new System.Drawing.Point(132, 34);
            this.nmWaitFordays.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmWaitFordays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmWaitFordays.Name = "nmWaitFordays";
            this.nmWaitFordays.Size = new System.Drawing.Size(92, 22);
            this.nmWaitFordays.TabIndex = 4;
            this.nmWaitFordays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbNameTo
            // 
            this.cmbNameTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameTo.FormattingEnabled = true;
            this.cmbNameTo.Location = new System.Drawing.Point(248, 64);
            this.cmbNameTo.Name = "cmbNameTo";
            this.cmbNameTo.Size = new System.Drawing.Size(46, 22);
            this.cmbNameTo.TabIndex = 6;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(220, 68);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(26, 14);
            this.label59.TabIndex = 316;
            this.label59.Text = "To ";
            this.label59.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label60.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Location = new System.Drawing.Point(132, 68);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(38, 14);
            this.label60.TabIndex = 317;
            this.label60.Text = "From ";
            this.label60.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbNameFrom
            // 
            this.cmbNameFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNameFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNameFrom.FormattingEnabled = true;
            this.cmbNameFrom.Location = new System.Drawing.Point(170, 64);
            this.cmbNameFrom.Name = "cmbNameFrom";
            this.cmbNameFrom.Size = new System.Drawing.Size(46, 22);
            this.cmbNameFrom.TabIndex = 5;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(12, 68);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(115, 14);
            this.label58.TabIndex = 313;
            this.label58.Text = "Patient Last Name :";
            this.label58.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(56, 366);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(90, 14);
            this.label57.TabIndex = 312;
            this.label57.Text = "Wait For Days :";
            this.label57.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label57.Visible = false;
            // 
            // txtWaitFordays
            // 
            this.txtWaitFordays.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtWaitFordays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtWaitFordays.Location = new System.Drawing.Point(147, 359);
            this.txtWaitFordays.MaxLength = 10;
            this.txtWaitFordays.Name = "txtWaitFordays";
            this.txtWaitFordays.Size = new System.Drawing.Size(58, 22);
            this.txtWaitFordays.TabIndex = 311;
            this.txtWaitFordays.Visible = false;
            this.txtWaitFordays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWaitFordays_KeyPress);
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Location = new System.Drawing.Point(429, 1);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 97);
            this.label56.TabIndex = 310;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(3, 1);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 97);
            this.label55.TabIndex = 309;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Top;
            this.label54.Location = new System.Drawing.Point(3, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(427, 1);
            this.label54.TabIndex = 308;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label53.Location = new System.Drawing.Point(3, 98);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(427, 1);
            this.label53.TabIndex = 307;
            // 
            // lblCPT
            // 
            this.lblCPT.AutoSize = true;
            this.lblCPT.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPT.Location = new System.Drawing.Point(105, 396);
            this.lblCPT.Name = "lblCPT";
            this.lblCPT.Size = new System.Drawing.Size(37, 14);
            this.lblCPT.TabIndex = 250;
            this.lblCPT.Text = "CPT :";
            this.lblCPT.Visible = false;
            // 
            // cmbCPT
            // 
            this.cmbCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPT.ForeColor = System.Drawing.Color.Black;
            this.cmbCPT.FormattingEnabled = true;
            this.cmbCPT.Location = new System.Drawing.Point(147, 392);
            this.cmbCPT.Name = "cmbCPT";
            this.cmbCPT.Size = new System.Drawing.Size(301, 22);
            this.cmbCPT.TabIndex = 5;
            this.cmbCPT.Visible = false;
            // 
            // btnClearCPT
            // 
            this.btnClearCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnClearCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.BackgroundImage")));
            this.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCPT.Image")));
            this.btnClearCPT.Location = new System.Drawing.Point(479, 392);
            this.btnClearCPT.Name = "btnClearCPT";
            this.btnClearCPT.Size = new System.Drawing.Size(22, 22);
            this.btnClearCPT.TabIndex = 7;
            this.btnClearCPT.UseVisualStyleBackColor = false;
            this.btnClearCPT.Visible = false;
            this.btnClearCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearCPT.Click += new System.EventHandler(this.btnClearCPT_Click);
            this.btnClearCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseCPT
            // 
            this.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseCPT.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.BackgroundImage")));
            this.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseCPT.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseCPT.Image")));
            this.btnBrowseCPT.Location = new System.Drawing.Point(453, 392);
            this.btnBrowseCPT.Name = "btnBrowseCPT";
            this.btnBrowseCPT.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseCPT.TabIndex = 6;
            this.btnBrowseCPT.UseVisualStyleBackColor = false;
            this.btnBrowseCPT.Visible = false;
            this.btnBrowseCPT.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseCPT.Click += new System.EventHandler(this.btnBrowseCPT_Click);
            this.btnBrowseCPT.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClearInsurance
            // 
            this.btnClearInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClearInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.BackgroundImage")));
            this.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClearInsurance.Image")));
            this.btnClearInsurance.Location = new System.Drawing.Point(479, 423);
            this.btnClearInsurance.Name = "btnClearInsurance";
            this.btnClearInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClearInsurance.TabIndex = 10;
            this.btnClearInsurance.UseVisualStyleBackColor = false;
            this.btnClearInsurance.Visible = false;
            this.btnClearInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearInsurance.Click += new System.EventHandler(this.btnClearInsurance_Click);
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
            this.btnBrowseInsurance.Location = new System.Drawing.Point(453, 423);
            this.btnBrowseInsurance.Name = "btnBrowseInsurance";
            this.btnBrowseInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnBrowseInsurance.TabIndex = 9;
            this.btnBrowseInsurance.UseVisualStyleBackColor = false;
            this.btnBrowseInsurance.Visible = false;
            this.btnBrowseInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseInsurance.Click += new System.EventHandler(this.btnBrowseInsurance_Click);
            this.btnBrowseInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtDueAmount
            // 
            this.txtDueAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtDueAmount.Location = new System.Drawing.Point(132, 6);
            this.txtDueAmount.MaxLength = 20;
            this.txtDueAmount.Name = "txtDueAmount";
            this.txtDueAmount.Size = new System.Drawing.Size(92, 22);
            this.txtDueAmount.TabIndex = 3;
            this.txtDueAmount.Text = "0.00";
            this.txtDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDueAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDueAmount_KeyPress);
            // 
            // rbGreaterthen
            // 
            this.rbGreaterthen.AutoSize = true;
            this.rbGreaterthen.Checked = true;
            this.rbGreaterthen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGreaterthen.Location = new System.Drawing.Point(213, 363);
            this.rbGreaterthen.Name = "rbGreaterthen";
            this.rbGreaterthen.Size = new System.Drawing.Size(104, 18);
            this.rbGreaterthen.TabIndex = 4;
            this.rbGreaterthen.TabStop = true;
            this.rbGreaterthen.Text = "Greater Than";
            this.rbGreaterthen.UseVisualStyleBackColor = true;
            this.rbGreaterthen.Visible = false;
            // 
            // rbLessThen
            // 
            this.rbLessThen.AutoSize = true;
            this.rbLessThen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLessThen.Location = new System.Drawing.Point(329, 363);
            this.rbLessThen.Name = "rbLessThen";
            this.rbLessThen.Size = new System.Drawing.Size(80, 18);
            this.rbLessThen.TabIndex = 4;
            this.rbLessThen.Text = "Less Than";
            this.rbLessThen.UseVisualStyleBackColor = true;
            this.rbLessThen.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(71, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 246;
            this.label1.Text = "Balance :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(79, 551);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 244;
            this.label11.Text = "Zip Code :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label11.Visible = false;
            // 
            // txtZipCode
            // 
            this.txtZipCode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtZipCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtZipCode.Location = new System.Drawing.Point(147, 547);
            this.txtZipCode.MaxLength = 10;
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(301, 22);
            this.txtZipCode.TabIndex = 14;
            this.txtZipCode.Visible = false;
            // 
            // cmbFacility
            // 
            this.cmbFacility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacility.ForeColor = System.Drawing.Color.Black;
            this.cmbFacility.FormattingEnabled = true;
            this.cmbFacility.Location = new System.Drawing.Point(147, 516);
            this.cmbFacility.Name = "cmbFacility";
            this.cmbFacility.Size = new System.Drawing.Size(301, 22);
            this.cmbFacility.TabIndex = 13;
            this.cmbFacility.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(92, 520);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 14);
            this.label9.TabIndex = 239;
            this.label9.Text = "Facility :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label9.Visible = false;
            // 
            // cmbPaymentTray
            // 
            this.cmbPaymentTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentTray.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentTray.FormattingEnabled = true;
            this.cmbPaymentTray.Location = new System.Drawing.Point(147, 485);
            this.cmbPaymentTray.Name = "cmbPaymentTray";
            this.cmbPaymentTray.Size = new System.Drawing.Size(301, 22);
            this.cmbPaymentTray.TabIndex = 12;
            this.cmbPaymentTray.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(51, 489);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 237;
            this.label7.Text = "Payment Tray :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label7.Visible = false;
            // 
            // cmbChargesTray
            // 
            this.cmbChargesTray.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargesTray.ForeColor = System.Drawing.Color.Black;
            this.cmbChargesTray.FormattingEnabled = true;
            this.cmbChargesTray.Location = new System.Drawing.Point(147, 454);
            this.cmbChargesTray.Name = "cmbChargesTray";
            this.cmbChargesTray.Size = new System.Drawing.Size(301, 22);
            this.cmbChargesTray.TabIndex = 11;
            this.cmbChargesTray.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(61, 458);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 14);
            this.label6.TabIndex = 235;
            this.label6.Text = "Charge Tray :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label6.Visible = false;
            // 
            // cmbInsurance
            // 
            this.cmbInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbInsurance.FormattingEnabled = true;
            this.cmbInsurance.Location = new System.Drawing.Point(147, 423);
            this.cmbInsurance.Name = "cmbInsurance";
            this.cmbInsurance.Size = new System.Drawing.Size(301, 22);
            this.cmbInsurance.TabIndex = 8;
            this.cmbInsurance.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(74, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 14);
            this.label3.TabIndex = 231;
            this.label3.Text = "Insurance :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label3.Visible = false;
            // 
            // imgtabControl1
            // 
            this.imgtabControl1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgtabControl1.ImageStream")));
            this.imgtabControl1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgtabControl1.Images.SetKeyName(0, "Filter Criteria.ico");
            this.imgtabControl1.Images.SetKeyName(1, "Display.ico");
            // 
            // Panel21
            // 
            this.Panel21.BackColor = System.Drawing.Color.Transparent;
            this.Panel21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel21.BackgroundImage")));
            this.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel21.Controls.Add(this.label19);
            this.Panel21.Controls.Add(this.chkDefault);
            this.Panel21.Controls.Add(this.txtStatementCriteriaName);
            this.Panel21.Controls.Add(this.Label23);
            this.Panel21.Controls.Add(this.Label44);
            this.Panel21.Controls.Add(this.Label45);
            this.Panel21.Controls.Add(this.Label46);
            this.Panel21.Controls.Add(this.Label47);
            this.Panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel21.Location = new System.Drawing.Point(3, 3);
            this.Panel21.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel21.Name = "Panel21";
            this.Panel21.Size = new System.Drawing.Size(427, 24);
            this.Panel21.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(63, 5);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 111;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(350, 3);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(65, 18);
            this.chkDefault.TabIndex = 2;
            this.chkDefault.Text = "Default";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // txtStatementCriteriaName
            // 
            this.txtStatementCriteriaName.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtStatementCriteriaName.Location = new System.Drawing.Point(126, 1);
            this.txtStatementCriteriaName.MaxLength = 50;
            this.txtStatementCriteriaName.Name = "txtStatementCriteriaName";
            this.txtStatementCriteriaName.Size = new System.Drawing.Size(218, 22);
            this.txtStatementCriteriaName.TabIndex = 1;
            // 
            // Label23
            // 
            this.Label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1, 1);
            this.Label23.Name = "Label23";
            this.Label23.Padding = new System.Windows.Forms.Padding(3);
            this.Label23.Size = new System.Drawing.Size(125, 22);
            this.Label23.TabIndex = 10;
            this.Label23.Text = "  Name :";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label44.Location = new System.Drawing.Point(1, 23);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(425, 1);
            this.Label44.TabIndex = 8;
            this.Label44.Text = "label2";
            // 
            // Label45
            // 
            this.Label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label45.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(0, 1);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(1, 23);
            this.Label45.TabIndex = 7;
            this.Label45.Text = "label4";
            // 
            // Label46
            // 
            this.Label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label46.Location = new System.Drawing.Point(426, 1);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(1, 23);
            this.Label46.TabIndex = 6;
            this.Label46.Text = "label3";
            // 
            // Label47
            // 
            this.Label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label47.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.Location = new System.Drawing.Point(0, 0);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(427, 1);
            this.Label47.TabIndex = 5;
            this.Label47.Text = "label1";
            // 
            // Panel20
            // 
            this.Panel20.Controls.Add(this.Panel21);
            this.Panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel20.Location = new System.Drawing.Point(0, 54);
            this.Panel20.Name = "Panel20";
            this.Panel20.Padding = new System.Windows.Forms.Padding(3);
            this.Panel20.Size = new System.Drawing.Size(433, 30);
            this.Panel20.TabIndex = 81;
            // 
            // frmSetupPatientStatementFilterCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(433, 186);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel20);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPatientStatementFilterCriteria";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Statement Filter Setting";
            this.Load += new System.EventHandler(this.frmSetupPatientStatementFilterCriteria_Load);
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmWaitFordays)).EndInit();
            this.Panel21.ResumeLayout(false);
            this.Panel21.PerformLayout();
            this.Panel20.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstrip;
        internal System.Windows.Forms.ToolStripButton btnOK;
        internal System.Windows.Forms.ToolStripButton btnClose;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imgtabControl1;
        internal System.Windows.Forms.Panel Panel21;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.TextBox txtStatementCriteriaName;
        internal System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label44;
        private System.Windows.Forms.Label Label45;
        private System.Windows.Forms.Label Label46;
        private System.Windows.Forms.Label Label47;
        internal System.Windows.Forms.Panel Panel20;
        internal System.Windows.Forms.Label label62;
        internal System.Windows.Forms.Label label61;
        private System.Windows.Forms.NumericUpDown nmWaitFordays;
        private System.Windows.Forms.ComboBox cmbNameTo;
        internal System.Windows.Forms.Label label59;
        internal System.Windows.Forms.Label label60;
        private System.Windows.Forms.ComboBox cmbNameFrom;
        internal System.Windows.Forms.Label label58;
        internal System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtWaitFordays;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label lblCPT;
        private System.Windows.Forms.ComboBox cmbCPT;
        internal System.Windows.Forms.Button btnClearCPT;
        internal System.Windows.Forms.Button btnBrowseCPT;
        internal System.Windows.Forms.Button btnClearInsurance;
        internal System.Windows.Forms.Button btnBrowseInsurance;
        private System.Windows.Forms.TextBox txtDueAmount;
        private System.Windows.Forms.RadioButton rbGreaterthen;
        private System.Windows.Forms.RadioButton rbLessThen;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtZipCode;
        internal System.Windows.Forms.ComboBox cmbFacility;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ComboBox cmbPaymentTray;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox cmbChargesTray;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ComboBox cmbInsurance;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label19;
    }
}