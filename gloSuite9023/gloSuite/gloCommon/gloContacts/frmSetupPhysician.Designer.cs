namespace gloContacts
{
    partial class frmSetupPhysician
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPhysician));
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.gboxContact_detail = new System.Windows.Forms.GroupBox();
            this.c1Template = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.grpBoxSecureMsg = new System.Windows.Forms.GroupBox();
            this.txtClinicName = new System.Windows.Forms.TextBox();
            this.txtSpecialtyType = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.txtSPI = new System.Windows.Forms.TextBox();
            this.txtDirectAddress = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.GroupBoxTaxonomy = new System.Windows.Forms.GroupBox();
            this.btnClearTaxonomy = new System.Windows.Forms.Button();
            this.btnBrowseTaxonomy = new System.Windows.Forms.Button();
            this.txtNPI = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtUPIN = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtTaxID = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtTaxonomy = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.GBox_Pracadrs = new System.Windows.Forms.GroupBox();
            this.pnlBPracAddresssControl = new System.Windows.Forms.Panel();
            this.mtxt_Prac_Phone = new gloMaskControl.gloMaskBox();
            this.txt_Prac_State = new System.Windows.Forms.TextBox();
            this.chBoxPracAdds = new System.Windows.Forms.CheckBox();
            this.txt_Prac_Fax = new gloMaskControl.gloMaskBox();
            this.txt_Prac_Zip = new System.Windows.Forms.TextBox();
            this.txt_Prac_AddressLine2 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txt_Prac_Email = new System.Windows.Forms.TextBox();
            this.txt_Prac_AddressLine1 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_Prac_URL = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_Prac_City = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.GBox_BMadrs = new System.Windows.Forms.GroupBox();
            this.pnlBMAddresssControl = new System.Windows.Forms.Panel();
            this.mtxt_BM_Phone = new gloMaskControl.gloMaskBox();
            this.txt_BM_State = new System.Windows.Forms.TextBox();
            this.chBoxBMAds = new System.Windows.Forms.CheckBox();
            this.txt_BM_Fax = new gloMaskControl.gloMaskBox();
            this.txt_BM_Zip = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_BM_Email = new System.Windows.Forms.TextBox();
            this.txt_BM_AddressLine2 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_BM_URL = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txt_BM_AddressLine1 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_BM_City = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlComAddresssControl = new System.Windows.Forms.Panel();
            this.mtxt_Comp_mobile = new gloMaskControl.gloMaskBox();
            this.mtxt_Comp_Phone = new gloMaskControl.gloMaskBox();
            this.txt_Comp_State = new System.Windows.Forms.TextBox();
            this.txt_Comp_Fax = new gloMaskControl.gloMaskBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_Comp_Email = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Comp_URL = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Comp_AddressLine2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Comp_Pager = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_comp_ZIP = new System.Windows.Forms.TextBox();
            this.txt_Comp_City = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Comp_AddressLine1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtlName = new System.Windows.Forms.TextBox();
            this.txtmName = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtfName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbGender2 = new System.Windows.Forms.RadioButton();
            this.rbGender1 = new System.Windows.Forms.RadioButton();
            this.label44 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.rbUsePlanSetting = new System.Windows.Forms.RadioButton();
            this.rbAlways = new System.Windows.Forms.RadioButton();
            this.cmbSpecialty = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.GroupBox10 = new System.Windows.Forms.GroupBox();
            this.lvwHospitals = new System.Windows.Forms.ListView();
            this.btnAddHospital = new System.Windows.Forms.Button();
            this.btnRemoveHospital = new System.Windows.Forms.Button();
            this.btnRemAllHospital = new System.Windows.Forms.Button();
            this.GroupBox9 = new System.Windows.Forms.GroupBox();
            this.lvwInsurances = new System.Windows.Forms.ListView();
            this.btnRemoveInsurance = new System.Windows.Forms.Button();
            this.btnRemAllInsurance = new System.Windows.Forms.Button();
            this.btnAddInsurance = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Panel15 = new System.Windows.Forms.Panel();
            this.Label98 = new System.Windows.Forms.Label();
            this.Label97 = new System.Windows.Forms.Label();
            this.Label96 = new System.Windows.Forms.Label();
            this.Label74 = new System.Windows.Forms.Label();
            this.c1PhysicianAdditionalIDs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Panel11 = new System.Windows.Forms.Panel();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.Label92 = new System.Windows.Forms.Label();
            this.Label91 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.gboxContact_detail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.grpBoxSecureMsg.SuspendLayout();
            this.GroupBoxTaxonomy.SuspendLayout();
            this.GBox_Pracadrs.SuspendLayout();
            this.GBox_BMadrs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.GroupBox10.SuspendLayout();
            this.GroupBox9.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.Panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PhysicianAdditionalIDs)).BeginInit();
            this.Panel11.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlTopToolStrip.Size = new System.Drawing.Size(774, 53);
            this.pnlTopToolStrip.TabIndex = 0;
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnSave,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(774, 53);
            this.TopToolStrip.TabIndex = 0;
            this.TopToolStrip.Text = "toolStrip1";
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
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(0, 850);
            this.panel1.Controls.Add(this.TabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(774, 770);
            this.panel1.TabIndex = 1;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.tabPage3);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(3, 3);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(751, 847);
            this.TabControl1.TabIndex = 0;
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.TabPage1.Controls.Add(this.gboxContact_detail);
            this.TabPage1.Controls.Add(this.groupBox2);
            this.TabPage1.Controls.Add(this.grpBoxSecureMsg);
            this.TabPage1.Controls.Add(this.GroupBoxTaxonomy);
            this.TabPage1.Controls.Add(this.GBox_Pracadrs);
            this.TabPage1.Controls.Add(this.GBox_BMadrs);
            this.TabPage1.Controls.Add(this.groupBox1);
            this.TabPage1.Controls.Add(this.panel2);
            this.TabPage1.Controls.Add(this.label44);
            this.TabPage1.Controls.Add(this.label47);
            this.TabPage1.Controls.Add(this.label48);
            this.TabPage1.Controls.Add(this.label49);
            this.TabPage1.Controls.Add(this.btnNext);
            this.TabPage1.Location = new System.Drawing.Point(4, 23);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(743, 820);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Tag = "";
            this.TabPage1.Text = "Contact Information";
            // 
            // gboxContact_detail
            // 
            this.gboxContact_detail.Controls.Add(this.c1Template);
            this.gboxContact_detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gboxContact_detail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxContact_detail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gboxContact_detail.Location = new System.Drawing.Point(1, 640);
            this.gboxContact_detail.Name = "gboxContact_detail";
            this.gboxContact_detail.Size = new System.Drawing.Size(741, 179);
            this.gboxContact_detail.TabIndex = 7;
            this.gboxContact_detail.TabStop = false;
            this.gboxContact_detail.Text = "Additional Provider Identification";
            // 
            // c1Template
            // 
            this.c1Template.AllowDelete = true;
            this.c1Template.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Template.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Template.AutoGenerateColumns = false;
            this.c1Template.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Template.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Template.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Template.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Template.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Template.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Template.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Template.Location = new System.Drawing.Point(3, 18);
            this.c1Template.Name = "c1Template";
            this.c1Template.Rows.Count = 1;
            this.c1Template.Rows.DefaultSize = 19;
            this.c1Template.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Template.Size = new System.Drawing.Size(735, 158);
            this.c1Template.StyleInfo = resources.GetString("c1Template.StyleInfo");
            this.c1Template.TabIndex = 7;
            this.c1Template.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Template_BeforeEdit);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.txtNotes);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox2.Location = new System.Drawing.Point(1, 577);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(741, 63);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(111, 14);
            this.txtNotes.MaxLength = 250;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(574, 41);
            this.txtNotes.TabIndex = 0;
            this.txtNotes.Text = "";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(62, 16);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(47, 14);
            this.label36.TabIndex = 12;
            this.label36.Text = "Notes :";
            // 
            // grpBoxSecureMsg
            // 
            this.grpBoxSecureMsg.BackColor = System.Drawing.Color.Transparent;
            this.grpBoxSecureMsg.Controls.Add(this.txtClinicName);
            this.grpBoxSecureMsg.Controls.Add(this.txtSpecialtyType);
            this.grpBoxSecureMsg.Controls.Add(this.label53);
            this.grpBoxSecureMsg.Controls.Add(this.label55);
            this.grpBoxSecureMsg.Controls.Add(this.txtSPI);
            this.grpBoxSecureMsg.Controls.Add(this.txtDirectAddress);
            this.grpBoxSecureMsg.Controls.Add(this.label54);
            this.grpBoxSecureMsg.Controls.Add(this.label56);
            this.grpBoxSecureMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBoxSecureMsg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxSecureMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpBoxSecureMsg.Location = new System.Drawing.Point(1, 504);
            this.grpBoxSecureMsg.Name = "grpBoxSecureMsg";
            this.grpBoxSecureMsg.Size = new System.Drawing.Size(741, 73);
            this.grpBoxSecureMsg.TabIndex = 5;
            this.grpBoxSecureMsg.TabStop = false;
            this.grpBoxSecureMsg.Text = "Secure Message";
            // 
            // txtClinicName
            // 
            this.txtClinicName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClinicName.Location = new System.Drawing.Point(112, 46);
            this.txtClinicName.Name = "txtClinicName";
            this.txtClinicName.Size = new System.Drawing.Size(246, 22);
            this.txtClinicName.TabIndex = 22;
            // 
            // txtSpecialtyType
            // 
            this.txtSpecialtyType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecialtyType.Location = new System.Drawing.Point(469, 46);
            this.txtSpecialtyType.Name = "txtSpecialtyType";
            this.txtSpecialtyType.Size = new System.Drawing.Size(216, 22);
            this.txtSpecialtyType.TabIndex = 19;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(371, 50);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(95, 14);
            this.label53.TabIndex = 21;
            this.label53.Text = "Specialty Type :";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(36, 50);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(76, 14);
            this.label55.TabIndex = 20;
            this.label55.Text = "Clinic Name :";
            // 
            // txtSPI
            // 
            this.txtSPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSPI.Location = new System.Drawing.Point(469, 19);
            this.txtSPI.MaxLength = 9;
            this.txtSPI.Name = "txtSPI";
            this.txtSPI.Size = new System.Drawing.Size(216, 22);
            this.txtSPI.TabIndex = 1;
            // 
            // txtDirectAddress
            // 
            this.txtDirectAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectAddress.Location = new System.Drawing.Point(112, 19);
            this.txtDirectAddress.Name = "txtDirectAddress";
            this.txtDirectAddress.Size = new System.Drawing.Size(246, 22);
            this.txtDirectAddress.TabIndex = 0;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(18, 23);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(94, 14);
            this.label54.TabIndex = 17;
            this.label54.Text = "Direct Address :";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(433, 23);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(33, 14);
            this.label56.TabIndex = 13;
            this.label56.Text = "SPI :";
            // 
            // GroupBoxTaxonomy
            // 
            this.GroupBoxTaxonomy.BackColor = System.Drawing.Color.Transparent;
            this.GroupBoxTaxonomy.Controls.Add(this.btnClearTaxonomy);
            this.GroupBoxTaxonomy.Controls.Add(this.btnBrowseTaxonomy);
            this.GroupBoxTaxonomy.Controls.Add(this.txtNPI);
            this.GroupBoxTaxonomy.Controls.Add(this.label27);
            this.GroupBoxTaxonomy.Controls.Add(this.txtUPIN);
            this.GroupBoxTaxonomy.Controls.Add(this.label28);
            this.GroupBoxTaxonomy.Controls.Add(this.txtTaxID);
            this.GroupBoxTaxonomy.Controls.Add(this.label25);
            this.GroupBoxTaxonomy.Controls.Add(this.txtTaxonomy);
            this.GroupBoxTaxonomy.Controls.Add(this.label26);
            this.GroupBoxTaxonomy.Dock = System.Windows.Forms.DockStyle.Top;
            this.GroupBoxTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxTaxonomy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GroupBoxTaxonomy.Location = new System.Drawing.Point(1, 436);
            this.GroupBoxTaxonomy.Name = "GroupBoxTaxonomy";
            this.GroupBoxTaxonomy.Size = new System.Drawing.Size(741, 68);
            this.GroupBoxTaxonomy.TabIndex = 4;
            this.GroupBoxTaxonomy.TabStop = false;
            this.GroupBoxTaxonomy.Text = "Taxonomy";
            // 
            // btnClearTaxonomy
            // 
            this.btnClearTaxonomy.BackColor = System.Drawing.Color.Transparent;
            this.btnClearTaxonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearTaxonomy.BackgroundImage")));
            this.btnClearTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearTaxonomy.Image = ((System.Drawing.Image)(resources.GetObject("btnClearTaxonomy.Image")));
            this.btnClearTaxonomy.Location = new System.Drawing.Point(384, 16);
            this.btnClearTaxonomy.Name = "btnClearTaxonomy";
            this.btnClearTaxonomy.Size = new System.Drawing.Size(20, 20);
            this.btnClearTaxonomy.TabIndex = 2;
            this.btnClearTaxonomy.UseVisualStyleBackColor = false;
            this.btnClearTaxonomy.Click += new System.EventHandler(this.btnClearTaxonomy_Click);
            this.btnClearTaxonomy.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClearTaxonomy.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnBrowseTaxonomy
            // 
            this.btnBrowseTaxonomy.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowseTaxonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseTaxonomy.BackgroundImage")));
            this.btnBrowseTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseTaxonomy.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseTaxonomy.Image")));
            this.btnBrowseTaxonomy.Location = new System.Drawing.Point(361, 16);
            this.btnBrowseTaxonomy.Name = "btnBrowseTaxonomy";
            this.btnBrowseTaxonomy.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseTaxonomy.TabIndex = 1;
            this.btnBrowseTaxonomy.UseVisualStyleBackColor = false;
            this.btnBrowseTaxonomy.Click += new System.EventHandler(this.btnBrowseTaxonomy_Click);
            this.btnBrowseTaxonomy.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnBrowseTaxonomy.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // txtNPI
            // 
            this.txtNPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNPI.Location = new System.Drawing.Point(469, 41);
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new System.Drawing.Size(216, 22);
            this.txtNPI.TabIndex = 5;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(432, 45);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 14);
            this.label27.TabIndex = 20;
            this.label27.Text = "NPI :";
            // 
            // txtUPIN
            // 
            this.txtUPIN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPIN.Location = new System.Drawing.Point(469, 16);
            this.txtUPIN.Name = "txtUPIN";
            this.txtUPIN.Size = new System.Drawing.Size(216, 22);
            this.txtUPIN.TabIndex = 3;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(424, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(42, 14);
            this.label28.TabIndex = 17;
            this.label28.Text = "UPIN :";
            // 
            // txtTaxID
            // 
            this.txtTaxID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxID.Location = new System.Drawing.Point(112, 41);
            this.txtTaxID.MaxLength = 9;
            this.txtTaxID.Name = "txtTaxID";
            this.txtTaxID.Size = new System.Drawing.Size(246, 22);
            this.txtTaxID.TabIndex = 4;
            this.txtTaxID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxID_KeyPress);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(58, 45);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(51, 14);
            this.label25.TabIndex = 16;
            this.label25.Text = "Tax ID :";
            // 
            // txtTaxonomy
            // 
            this.txtTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxonomy.Location = new System.Drawing.Point(112, 16);
            this.txtTaxonomy.Name = "txtTaxonomy";
            this.txtTaxonomy.ReadOnly = true;
            this.txtTaxonomy.Size = new System.Drawing.Size(246, 22);
            this.txtTaxonomy.TabIndex = 0;
            this.txtTaxonomy.TabStop = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(37, 20);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 14);
            this.label26.TabIndex = 13;
            this.label26.Text = "Taxonomy :";
            // 
            // GBox_Pracadrs
            // 
            this.GBox_Pracadrs.Controls.Add(this.pnlBPracAddresssControl);
            this.GBox_Pracadrs.Controls.Add(this.mtxt_Prac_Phone);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_State);
            this.GBox_Pracadrs.Controls.Add(this.chBoxPracAdds);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_Fax);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_Zip);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_AddressLine2);
            this.GBox_Pracadrs.Controls.Add(this.label39);
            this.GBox_Pracadrs.Controls.Add(this.label18);
            this.GBox_Pracadrs.Controls.Add(this.label40);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_Email);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_AddressLine1);
            this.GBox_Pracadrs.Controls.Add(this.label38);
            this.GBox_Pracadrs.Controls.Add(this.label21);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_URL);
            this.GBox_Pracadrs.Controls.Add(this.label37);
            this.GBox_Pracadrs.Controls.Add(this.txt_Prac_City);
            this.GBox_Pracadrs.Controls.Add(this.label33);
            this.GBox_Pracadrs.Controls.Add(this.label34);
            this.GBox_Pracadrs.Controls.Add(this.label35);
            this.GBox_Pracadrs.Dock = System.Windows.Forms.DockStyle.Top;
            this.GBox_Pracadrs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBox_Pracadrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GBox_Pracadrs.Location = new System.Drawing.Point(1, 311);
            this.GBox_Pracadrs.Name = "GBox_Pracadrs";
            this.GBox_Pracadrs.Size = new System.Drawing.Size(741, 125);
            this.GBox_Pracadrs.TabIndex = 3;
            this.GBox_Pracadrs.TabStop = false;
            this.GBox_Pracadrs.Text = "Business Practice Location Address Information";
            // 
            // pnlBPracAddresssControl
            // 
            this.pnlBPracAddresssControl.Location = new System.Drawing.Point(21, 14);
            this.pnlBPracAddresssControl.Name = "pnlBPracAddresssControl";
            this.pnlBPracAddresssControl.Size = new System.Drawing.Size(325, 105);
            this.pnlBPracAddresssControl.TabIndex = 114;
            // 
            // mtxt_Prac_Phone
            // 
            this.mtxt_Prac_Phone.AllowValidate = true;
            this.mtxt_Prac_Phone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxt_Prac_Phone.IncludeLiteralsAndPrompts = false;
            this.mtxt_Prac_Phone.Location = new System.Drawing.Point(439, 35);
            this.mtxt_Prac_Phone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxt_Prac_Phone.Name = "mtxt_Prac_Phone";
            this.mtxt_Prac_Phone.ReadOnly = false;
            this.mtxt_Prac_Phone.Size = new System.Drawing.Size(110, 24);
            this.mtxt_Prac_Phone.TabIndex = 7;
            // 
            // txt_Prac_State
            // 
            this.txt_Prac_State.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_State.Location = new System.Drawing.Point(88, 53);
            this.txt_Prac_State.MaxLength = 8;
            this.txt_Prac_State.Name = "txt_Prac_State";
            this.txt_Prac_State.Size = new System.Drawing.Size(113, 22);
            this.txt_Prac_State.TabIndex = 5;
            this.txt_Prac_State.Visible = false;
            // 
            // chBoxPracAdds
            // 
            this.chBoxPracAdds.AutoSize = true;
            this.chBoxPracAdds.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chBoxPracAdds.Location = new System.Drawing.Point(439, 14);
            this.chBoxPracAdds.Name = "chBoxPracAdds";
            this.chBoxPracAdds.Size = new System.Drawing.Size(78, 18);
            this.chBoxPracAdds.TabIndex = 0;
            this.chBoxPracAdds.Text = "As Above";
            this.chBoxPracAdds.UseVisualStyleBackColor = true;
            this.chBoxPracAdds.CheckedChanged += new System.EventHandler(this.chBoxPracAdds_CheckedChanged);
            // 
            // txt_Prac_Fax
            // 
            this.txt_Prac_Fax.AllowValidate = true;
            this.txt_Prac_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_Fax.IncludeLiteralsAndPrompts = false;
            this.txt_Prac_Fax.Location = new System.Drawing.Point(592, 36);
            this.txt_Prac_Fax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txt_Prac_Fax.Name = "txt_Prac_Fax";
            this.txt_Prac_Fax.ReadOnly = false;
            this.txt_Prac_Fax.Size = new System.Drawing.Size(93, 22);
            this.txt_Prac_Fax.TabIndex = 6;
            // 
            // txt_Prac_Zip
            // 
            this.txt_Prac_Zip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_Zip.Location = new System.Drawing.Point(94, 79);
            this.txt_Prac_Zip.MaxLength = 10;
            this.txt_Prac_Zip.Name = "txt_Prac_Zip";
            this.txt_Prac_Zip.Size = new System.Drawing.Size(90, 22);
            this.txt_Prac_Zip.TabIndex = 3;
            this.txt_Prac_Zip.Visible = false;
            this.txt_Prac_Zip.TextChanged += new System.EventHandler(this.txt_Prac_Zip_TextChanged);
            this.txt_Prac_Zip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Prac_Zip_KeyPress);
            // 
            // txt_Prac_AddressLine2
            // 
            this.txt_Prac_AddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_AddressLine2.Location = new System.Drawing.Point(111, 23);
            this.txt_Prac_AddressLine2.Name = "txt_Prac_AddressLine2";
            this.txt_Prac_AddressLine2.Size = new System.Drawing.Size(206, 22);
            this.txt_Prac_AddressLine2.TabIndex = 2;
            this.txt_Prac_AddressLine2.Visible = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(556, 40);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(33, 14);
            this.label39.TabIndex = 6;
            this.label39.Text = "Fax :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(21, 27);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 14);
            this.label18.TabIndex = 12;
            this.label18.Text = "AddressLine2 :";
            this.label18.Visible = false;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(62, 83);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(31, 14);
            this.label40.TabIndex = 18;
            this.label40.Text = "Zip :";
            this.label40.Visible = false;
            // 
            // txt_Prac_Email
            // 
            this.txt_Prac_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_Email.Location = new System.Drawing.Point(439, 62);
            this.txt_Prac_Email.Name = "txt_Prac_Email";
            this.txt_Prac_Email.Size = new System.Drawing.Size(246, 22);
            this.txt_Prac_Email.TabIndex = 8;
            this.txt_Prac_Email.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Prac_Email_Validating);
            // 
            // txt_Prac_AddressLine1
            // 
            this.txt_Prac_AddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_AddressLine1.Location = new System.Drawing.Point(94, 19);
            this.txt_Prac_AddressLine1.Name = "txt_Prac_AddressLine1";
            this.txt_Prac_AddressLine1.Size = new System.Drawing.Size(206, 22);
            this.txt_Prac_AddressLine1.TabIndex = 1;
            this.txt_Prac_AddressLine1.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(394, 66);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(42, 14);
            this.label38.TabIndex = 12;
            this.label38.Text = "Email :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(87, 14);
            this.label21.TabIndex = 0;
            this.label21.Text = "AddressLine1 :";
            this.label21.Visible = false;
            // 
            // txt_Prac_URL
            // 
            this.txt_Prac_URL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_URL.Location = new System.Drawing.Point(439, 88);
            this.txt_Prac_URL.Name = "txt_Prac_URL";
            this.txt_Prac_URL.Size = new System.Drawing.Size(246, 22);
            this.txt_Prac_URL.TabIndex = 9;
            this.txt_Prac_URL.Validating += new System.ComponentModel.CancelEventHandler(this.txt_URL_Validating);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(400, 92);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(36, 14);
            this.label37.TabIndex = 14;
            this.label37.Text = "URL :";
            // 
            // txt_Prac_City
            // 
            this.txt_Prac_City.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txt_Prac_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Prac_City.Location = new System.Drawing.Point(94, 49);
            this.txt_Prac_City.Name = "txt_Prac_City";
            this.txt_Prac_City.Size = new System.Drawing.Size(107, 22);
            this.txt_Prac_City.TabIndex = 4;
            this.txt_Prac_City.Visible = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(38, 57);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(45, 14);
            this.label33.TabIndex = 2;
            this.label33.Text = "State :";
            this.label33.Visible = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(56, 53);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(35, 14);
            this.label34.TabIndex = 1;
            this.label34.Text = "City :";
            this.label34.Visible = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(386, 40);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 14);
            this.label35.TabIndex = 0;
            this.label35.Text = "Phone :";
            // 
            // GBox_BMadrs
            // 
            this.GBox_BMadrs.Controls.Add(this.pnlBMAddresssControl);
            this.GBox_BMadrs.Controls.Add(this.mtxt_BM_Phone);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_State);
            this.GBox_BMadrs.Controls.Add(this.chBoxBMAds);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_Fax);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_Zip);
            this.GBox_BMadrs.Controls.Add(this.label31);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_Email);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_AddressLine2);
            this.GBox_BMadrs.Controls.Add(this.label42);
            this.GBox_BMadrs.Controls.Add(this.label30);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_URL);
            this.GBox_BMadrs.Controls.Add(this.label29);
            this.GBox_BMadrs.Controls.Add(this.label43);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_AddressLine1);
            this.GBox_BMadrs.Controls.Add(this.label46);
            this.GBox_BMadrs.Controls.Add(this.label14);
            this.GBox_BMadrs.Controls.Add(this.label15);
            this.GBox_BMadrs.Controls.Add(this.label19);
            this.GBox_BMadrs.Controls.Add(this.txt_BM_City);
            this.GBox_BMadrs.Dock = System.Windows.Forms.DockStyle.Top;
            this.GBox_BMadrs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBox_BMadrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GBox_BMadrs.Location = new System.Drawing.Point(1, 187);
            this.GBox_BMadrs.Name = "GBox_BMadrs";
            this.GBox_BMadrs.Size = new System.Drawing.Size(741, 124);
            this.GBox_BMadrs.TabIndex = 2;
            this.GBox_BMadrs.TabStop = false;
            this.GBox_BMadrs.Text = "Business Mailing Address Information";
            // 
            // pnlBMAddresssControl
            // 
            this.pnlBMAddresssControl.Location = new System.Drawing.Point(22, 14);
            this.pnlBMAddresssControl.Name = "pnlBMAddresssControl";
            this.pnlBMAddresssControl.Size = new System.Drawing.Size(325, 105);
            this.pnlBMAddresssControl.TabIndex = 113;
            // 
            // mtxt_BM_Phone
            // 
            this.mtxt_BM_Phone.AllowValidate = true;
            this.mtxt_BM_Phone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxt_BM_Phone.IncludeLiteralsAndPrompts = false;
            this.mtxt_BM_Phone.Location = new System.Drawing.Point(439, 37);
            this.mtxt_BM_Phone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxt_BM_Phone.Name = "mtxt_BM_Phone";
            this.mtxt_BM_Phone.ReadOnly = false;
            this.mtxt_BM_Phone.Size = new System.Drawing.Size(111, 24);
            this.mtxt_BM_Phone.TabIndex = 7;
            // 
            // txt_BM_State
            // 
            this.txt_BM_State.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_State.Location = new System.Drawing.Point(122, 46);
            this.txt_BM_State.Name = "txt_BM_State";
            this.txt_BM_State.Size = new System.Drawing.Size(113, 22);
            this.txt_BM_State.TabIndex = 5;
            this.txt_BM_State.Visible = false;
            // 
            // chBoxBMAds
            // 
            this.chBoxBMAds.AutoSize = true;
            this.chBoxBMAds.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chBoxBMAds.Location = new System.Drawing.Point(439, 14);
            this.chBoxBMAds.Name = "chBoxBMAds";
            this.chBoxBMAds.Size = new System.Drawing.Size(78, 18);
            this.chBoxBMAds.TabIndex = 0;
            this.chBoxBMAds.Text = "As Above";
            this.chBoxBMAds.UseVisualStyleBackColor = true;
            this.chBoxBMAds.CheckedChanged += new System.EventHandler(this.chBoxBMAds_CheckedChanged);
            // 
            // txt_BM_Fax
            // 
            this.txt_BM_Fax.AllowValidate = true;
            this.txt_BM_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_Fax.IncludeLiteralsAndPrompts = false;
            this.txt_BM_Fax.Location = new System.Drawing.Point(592, 38);
            this.txt_BM_Fax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txt_BM_Fax.Name = "txt_BM_Fax";
            this.txt_BM_Fax.ReadOnly = false;
            this.txt_BM_Fax.Size = new System.Drawing.Size(93, 22);
            this.txt_BM_Fax.TabIndex = 6;
            // 
            // txt_BM_Zip
            // 
            this.txt_BM_Zip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_Zip.Location = new System.Drawing.Point(94, 70);
            this.txt_BM_Zip.MaxLength = 10;
            this.txt_BM_Zip.Name = "txt_BM_Zip";
            this.txt_BM_Zip.Size = new System.Drawing.Size(107, 22);
            this.txt_BM_Zip.TabIndex = 3;
            this.txt_BM_Zip.Visible = false;
            this.txt_BM_Zip.TextChanged += new System.EventHandler(this.txt_BM_Zip_TextChanged);
            this.txt_BM_Zip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_BM_Zip_KeyPress);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(556, 42);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(33, 14);
            this.label31.TabIndex = 6;
            this.label31.Text = "Fax :";
            // 
            // txt_BM_Email
            // 
            this.txt_BM_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_Email.Location = new System.Drawing.Point(439, 66);
            this.txt_BM_Email.Name = "txt_BM_Email";
            this.txt_BM_Email.Size = new System.Drawing.Size(246, 22);
            this.txt_BM_Email.TabIndex = 8;
            this.txt_BM_Email.Validating += new System.ComponentModel.CancelEventHandler(this.txt_BM_Email_Validating);
            // 
            // txt_BM_AddressLine2
            // 
            this.txt_BM_AddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_AddressLine2.Location = new System.Drawing.Point(111, 14);
            this.txt_BM_AddressLine2.Name = "txt_BM_AddressLine2";
            this.txt_BM_AddressLine2.Size = new System.Drawing.Size(206, 22);
            this.txt_BM_AddressLine2.TabIndex = 2;
            this.txt_BM_AddressLine2.Visible = false;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(60, 78);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(31, 14);
            this.label42.TabIndex = 16;
            this.label42.Text = "Zip :";
            this.label42.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(394, 70);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(42, 14);
            this.label30.TabIndex = 12;
            this.label30.Text = "Email :";
            // 
            // txt_BM_URL
            // 
            this.txt_BM_URL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_URL.Location = new System.Drawing.Point(439, 93);
            this.txt_BM_URL.Name = "txt_BM_URL";
            this.txt_BM_URL.Size = new System.Drawing.Size(246, 22);
            this.txt_BM_URL.TabIndex = 9;
            this.txt_BM_URL.Validating += new System.ComponentModel.CancelEventHandler(this.txt_URL_Validating);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(400, 97);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(36, 14);
            this.label29.TabIndex = 14;
            this.label29.Text = "URL :";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(21, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(87, 14);
            this.label43.TabIndex = 12;
            this.label43.Text = "AddressLine2 :";
            this.label43.Visible = false;
            // 
            // txt_BM_AddressLine1
            // 
            this.txt_BM_AddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_AddressLine1.Location = new System.Drawing.Point(94, 14);
            this.txt_BM_AddressLine1.Name = "txt_BM_AddressLine1";
            this.txt_BM_AddressLine1.Size = new System.Drawing.Size(206, 22);
            this.txt_BM_AddressLine1.TabIndex = 1;
            this.txt_BM_AddressLine1.Visible = false;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(4, 22);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(87, 14);
            this.label46.TabIndex = 0;
            this.label46.Text = "AddressLine1 :";
            this.label46.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(386, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "Phone :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(56, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 14);
            this.label15.TabIndex = 1;
            this.label15.Text = "City :";
            this.label15.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(75, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "State :";
            this.label19.Visible = false;
            // 
            // txt_BM_City
            // 
            this.txt_BM_City.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txt_BM_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BM_City.Location = new System.Drawing.Point(94, 42);
            this.txt_BM_City.Name = "txt_BM_City";
            this.txt_BM_City.Size = new System.Drawing.Size(107, 22);
            this.txt_BM_City.TabIndex = 4;
            this.txt_BM_City.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlComAddresssControl);
            this.groupBox1.Controls.Add(this.mtxt_Comp_mobile);
            this.groupBox1.Controls.Add(this.mtxt_Comp_Phone);
            this.groupBox1.Controls.Add(this.txt_Comp_State);
            this.groupBox1.Controls.Add(this.txt_Comp_Fax);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txt_Comp_Email);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txt_Comp_URL);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_Comp_AddressLine2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_Comp_Pager);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txt_comp_ZIP);
            this.groupBox1.Controls.Add(this.txt_Comp_City);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_Comp_AddressLine1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(1, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Company Address Information";
            // 
            // pnlComAddresssControl
            // 
            this.pnlComAddresssControl.Location = new System.Drawing.Point(22, 14);
            this.pnlComAddresssControl.Name = "pnlComAddresssControl";
            this.pnlComAddresssControl.Size = new System.Drawing.Size(325, 105);
            this.pnlComAddresssControl.TabIndex = 112;
            // 
            // mtxt_Comp_mobile
            // 
            this.mtxt_Comp_mobile.AllowValidate = true;
            this.mtxt_Comp_mobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxt_Comp_mobile.IncludeLiteralsAndPrompts = false;
            this.mtxt_Comp_mobile.Location = new System.Drawing.Point(592, 18);
            this.mtxt_Comp_mobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxt_Comp_mobile.Name = "mtxt_Comp_mobile";
            this.mtxt_Comp_mobile.ReadOnly = false;
            this.mtxt_Comp_mobile.Size = new System.Drawing.Size(93, 22);
            this.mtxt_Comp_mobile.TabIndex = 3;
            // 
            // mtxt_Comp_Phone
            // 
            this.mtxt_Comp_Phone.AllowValidate = true;
            this.mtxt_Comp_Phone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxt_Comp_Phone.IncludeLiteralsAndPrompts = false;
            this.mtxt_Comp_Phone.Location = new System.Drawing.Point(439, 18);
            this.mtxt_Comp_Phone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxt_Comp_Phone.Name = "mtxt_Comp_Phone";
            this.mtxt_Comp_Phone.ReadOnly = false;
            this.mtxt_Comp_Phone.Size = new System.Drawing.Size(93, 22);
            this.mtxt_Comp_Phone.TabIndex = 8;
            // 
            // txt_Comp_State
            // 
            this.txt_Comp_State.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_State.Location = new System.Drawing.Point(92, 48);
            this.txt_Comp_State.Name = "txt_Comp_State";
            this.txt_Comp_State.Size = new System.Drawing.Size(101, 22);
            this.txt_Comp_State.TabIndex = 6;
            this.txt_Comp_State.Visible = false;
            // 
            // txt_Comp_Fax
            // 
            this.txt_Comp_Fax.AllowValidate = true;
            this.txt_Comp_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_Fax.IncludeLiteralsAndPrompts = false;
            this.txt_Comp_Fax.Location = new System.Drawing.Point(439, 44);
            this.txt_Comp_Fax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txt_Comp_Fax.Name = "txt_Comp_Fax";
            this.txt_Comp_Fax.ReadOnly = false;
            this.txt_Comp_Fax.Size = new System.Drawing.Size(93, 22);
            this.txt_Comp_Fax.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(405, 48);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(33, 14);
            this.label22.TabIndex = 6;
            this.label22.Text = "Fax :";
            // 
            // txt_Comp_Email
            // 
            this.txt_Comp_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_Email.Location = new System.Drawing.Point(439, 70);
            this.txt_Comp_Email.Name = "txt_Comp_Email";
            this.txt_Comp_Email.Size = new System.Drawing.Size(246, 22);
            this.txt_Comp_Email.TabIndex = 10;
            this.txt_Comp_Email.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Comp_Email_Validating);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(396, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 14);
            this.label16.TabIndex = 12;
            this.label16.Text = "Email :";
            // 
            // txt_Comp_URL
            // 
            this.txt_Comp_URL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_URL.Location = new System.Drawing.Point(439, 96);
            this.txt_Comp_URL.Name = "txt_Comp_URL";
            this.txt_Comp_URL.Size = new System.Drawing.Size(246, 22);
            this.txt_Comp_URL.TabIndex = 11;
            this.txt_Comp_URL.Validating += new System.ComponentModel.CancelEventHandler(this.txt_URL_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(402, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 14);
            this.label13.TabIndex = 14;
            this.label13.Text = "URL :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(538, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 14);
            this.label12.TabIndex = 19;
            this.label12.Text = "Mobile :";
            // 
            // txt_Comp_AddressLine2
            // 
            this.txt_Comp_AddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_AddressLine2.Location = new System.Drawing.Point(92, 17);
            this.txt_Comp_AddressLine2.Name = "txt_Comp_AddressLine2";
            this.txt_Comp_AddressLine2.Size = new System.Drawing.Size(216, 22);
            this.txt_Comp_AddressLine2.TabIndex = 1;
            this.txt_Comp_AddressLine2.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "AddressLine2 :";
            this.label8.Visible = false;
            // 
            // txt_Comp_Pager
            // 
            this.txt_Comp_Pager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_Pager.Location = new System.Drawing.Point(592, 44);
            this.txt_Comp_Pager.Name = "txt_Comp_Pager";
            this.txt_Comp_Pager.Size = new System.Drawing.Size(93, 22);
            this.txt_Comp_Pager.TabIndex = 9;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(541, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 14);
            this.label17.TabIndex = 8;
            this.label17.Text = "Pager :";
            // 
            // txt_comp_ZIP
            // 
            this.txt_comp_ZIP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comp_ZIP.Location = new System.Drawing.Point(92, 76);
            this.txt_comp_ZIP.MaxLength = 10;
            this.txt_comp_ZIP.Name = "txt_comp_ZIP";
            this.txt_comp_ZIP.Size = new System.Drawing.Size(109, 22);
            this.txt_comp_ZIP.TabIndex = 4;
            this.txt_comp_ZIP.Visible = false;
            this.txt_comp_ZIP.TextChanged += new System.EventHandler(this.txt_comp_ZIP_TextChanged);
            this.txt_comp_ZIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comp_ZIP_KeyPress);
            // 
            // txt_Comp_City
            // 
            this.txt_Comp_City.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txt_Comp_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_City.Location = new System.Drawing.Point(92, 45);
            this.txt_Comp_City.Name = "txt_Comp_City";
            this.txt_Comp_City.Size = new System.Drawing.Size(109, 22);
            this.txt_Comp_City.TabIndex = 5;
            this.txt_Comp_City.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(60, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "Zip :";
            this.label4.Visible = false;
            // 
            // txt_Comp_AddressLine1
            // 
            this.txt_Comp_AddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comp_AddressLine1.Location = new System.Drawing.Point(94, 16);
            this.txt_Comp_AddressLine1.Name = "txt_Comp_AddressLine1";
            this.txt_Comp_AddressLine1.Size = new System.Drawing.Size(206, 22);
            this.txt_Comp_AddressLine1.TabIndex = 0;
            this.txt_Comp_AddressLine1.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(56, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 1;
            this.label10.Text = "City :";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "AddressLine1 :";
            this.label11.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(46, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "State :";
            this.label9.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(388, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(50, 14);
            this.label23.TabIndex = 0;
            this.label23.Text = "Phone :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.GroupBox3);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(741, 62);
            this.panel2.TabIndex = 138;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.txtSuffix);
            this.GroupBox3.Controls.Add(this.label45);
            this.GroupBox3.Controls.Add(this.txtPrefix);
            this.GroupBox3.Controls.Add(this.lblPrefix);
            this.GroupBox3.Controls.Add(this.txtlName);
            this.GroupBox3.Controls.Add(this.txtmName);
            this.GroupBox3.Controls.Add(this.Label5);
            this.GroupBox3.Controls.Add(this.Label6);
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.Controls.Add(this.Label7);
            this.GroupBox3.Controls.Add(this.txtfName);
            this.GroupBox3.Controls.Add(this.label20);
            this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GroupBox3.Location = new System.Drawing.Point(0, 0);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(569, 62);
            this.GroupBox3.TabIndex = 0;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "General Information";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSuffix.ForeColor = System.Drawing.Color.Black;
            this.txtSuffix.Location = new System.Drawing.Point(495, 18);
            this.txtSuffix.MaxLength = 20;
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(59, 22);
            this.txtSuffix.TabIndex = 4;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(507, 45);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(34, 11);
            this.label45.TabIndex = 117;
            this.label45.Text = "(Suffix)";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrefix.ForeColor = System.Drawing.Color.Black;
            this.txtPrefix.Location = new System.Drawing.Point(115, 18);
            this.txtPrefix.MaxLength = 20;
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(59, 22);
            this.txtPrefix.TabIndex = 0;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrefix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPrefix.Location = new System.Drawing.Point(127, 44);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(34, 11);
            this.lblPrefix.TabIndex = 115;
            this.lblPrefix.Text = "(Prefix)";
            // 
            // txtlName
            // 
            this.txtlName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlName.ForeColor = System.Drawing.Color.Black;
            this.txtlName.Location = new System.Drawing.Point(356, 18);
            this.txtlName.MaxLength = 50;
            this.txtlName.Name = "txtlName";
            this.txtlName.Size = new System.Drawing.Size(136, 22);
            this.txtlName.TabIndex = 3;
            // 
            // txtmName
            // 
            this.txtmName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmName.ForeColor = System.Drawing.Color.Black;
            this.txtmName.Location = new System.Drawing.Point(317, 18);
            this.txtmName.MaxLength = 2;
            this.txtmName.Name = "txtmName";
            this.txtmName.Size = new System.Drawing.Size(34, 22);
            this.txtmName.TabIndex = 2;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(400, 44);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(49, 11);
            this.Label5.TabIndex = 15;
            this.Label5.Text = "Last Name";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(326, 43);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(17, 11);
            this.Label6.TabIndex = 14;
            this.Label6.Text = "MI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "Physician Name :";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(221, 44);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(51, 11);
            this.Label7.TabIndex = 12;
            this.Label7.Text = "First Name";
            // 
            // txtfName
            // 
            this.txtfName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfName.ForeColor = System.Drawing.Color.Black;
            this.txtfName.Location = new System.Drawing.Point(178, 18);
            this.txtfName.MaxLength = 50;
            this.txtfName.Name = "txtfName";
            this.txtfName.Size = new System.Drawing.Size(136, 22);
            this.txtfName.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoEllipsis = true;
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(209, 42);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label20.Size = new System.Drawing.Size(14, 14);
            this.label20.TabIndex = 112;
            this.label20.Text = "*";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.rbGender2);
            this.groupBox4.Controls.Add(this.rbGender1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox4.Location = new System.Drawing.Point(569, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(172, 62);
            this.groupBox4.TabIndex = 114;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Gender ";
            // 
            // rbGender2
            // 
            this.rbGender2.AutoSize = true;
            this.rbGender2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender2.Location = new System.Drawing.Point(97, 26);
            this.rbGender2.Name = "rbGender2";
            this.rbGender2.Size = new System.Drawing.Size(63, 18);
            this.rbGender2.TabIndex = 6;
            this.rbGender2.Text = "Female";
            this.rbGender2.CheckedChanged += new System.EventHandler(this.rbGender2_CheckedChanged);
            // 
            // rbGender1
            // 
            this.rbGender1.AutoSize = true;
            this.rbGender1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender1.Location = new System.Drawing.Point(29, 26);
            this.rbGender1.Name = "rbGender1";
            this.rbGender1.Size = new System.Drawing.Size(49, 18);
            this.rbGender1.TabIndex = 5;
            this.rbGender1.Text = "Male";
            this.rbGender1.CheckedChanged += new System.EventHandler(this.rbGender1_CheckedChanged);
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(1, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(741, 1);
            this.label44.TabIndex = 137;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(1, 819);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(741, 1);
            this.label47.TabIndex = 136;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(742, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 820);
            this.label48.TabIndex = 135;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Left;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 820);
            this.label49.TabIndex = 134;
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(810, 677);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 26);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "&Next";
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.btnNext.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnNext.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.TabPage2.Controls.Add(this.label3);
            this.TabPage2.Controls.Add(this.label24);
            this.TabPage2.Controls.Add(this.label32);
            this.TabPage2.Controls.Add(this.label41);
            this.TabPage2.Controls.Add(this.groupBox5);
            this.TabPage2.Controls.Add(this.cmbSpecialty);
            this.TabPage2.Controls.Add(this.Label2);
            this.TabPage2.Controls.Add(this.GroupBox10);
            this.TabPage2.Controls.Add(this.GroupBox9);
            this.TabPage2.Controls.Add(this.btnPrevious);
            this.TabPage2.Location = new System.Drawing.Point(4, 23);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(743, 867);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Other Information";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(741, 1);
            this.label3.TabIndex = 137;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(1, 866);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(741, 1);
            this.label24.TabIndex = 136;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(742, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 867);
            this.label32.TabIndex = 135;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Left;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1, 867);
            this.label41.TabIndex = 134;
            // 
            // groupBox5
            // 
            this.groupBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox5.Controls.Add(this.rbNo);
            this.groupBox5.Controls.Add(this.rbUsePlanSetting);
            this.groupBox5.Controls.Add(this.rbAlways);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox5.Location = new System.Drawing.Point(17, 518);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(727, 61);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Default Prior Authorization Required";
            this.groupBox5.Visible = false;
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNo.Location = new System.Drawing.Point(371, 30);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(40, 18);
            this.rbNo.TabIndex = 2;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            // 
            // rbUsePlanSetting
            // 
            this.rbUsePlanSetting.AutoSize = true;
            this.rbUsePlanSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUsePlanSetting.Location = new System.Drawing.Point(141, 30);
            this.rbUsePlanSetting.Name = "rbUsePlanSetting";
            this.rbUsePlanSetting.Size = new System.Drawing.Size(204, 18);
            this.rbUsePlanSetting.TabIndex = 1;
            this.rbUsePlanSetting.TabStop = true;
            this.rbUsePlanSetting.Text = "Yes - Use Insurance Plan Setting";
            this.rbUsePlanSetting.UseVisualStyleBackColor = true;
            // 
            // rbAlways
            // 
            this.rbAlways.AutoSize = true;
            this.rbAlways.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAlways.Location = new System.Drawing.Point(31, 30);
            this.rbAlways.Name = "rbAlways";
            this.rbAlways.Size = new System.Drawing.Size(94, 18);
            this.rbAlways.TabIndex = 0;
            this.rbAlways.TabStop = true;
            this.rbAlways.Text = "Yes - Always";
            this.rbAlways.UseVisualStyleBackColor = true;
            // 
            // cmbSpecialty
            // 
            this.cmbSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecialty.Location = new System.Drawing.Point(105, 12);
            this.cmbSpecialty.Name = "cmbSpecialty";
            this.cmbSpecialty.Size = new System.Drawing.Size(228, 22);
            this.cmbSpecialty.TabIndex = 0;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(32, 16);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(70, 14);
            this.Label2.TabIndex = 25;
            this.Label2.Text = "Specialty :";
            // 
            // GroupBox10
            // 
            this.GroupBox10.Controls.Add(this.lvwHospitals);
            this.GroupBox10.Controls.Add(this.btnAddHospital);
            this.GroupBox10.Controls.Add(this.btnRemoveHospital);
            this.GroupBox10.Controls.Add(this.btnRemAllHospital);
            this.GroupBox10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GroupBox10.Location = new System.Drawing.Point(366, 43);
            this.GroupBox10.Name = "GroupBox10";
            this.GroupBox10.Size = new System.Drawing.Size(378, 473);
            this.GroupBox10.TabIndex = 2;
            this.GroupBox10.TabStop = false;
            this.GroupBox10.Text = "Hospital Affiliation";
            // 
            // lvwHospitals
            // 
            this.lvwHospitals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwHospitals.ForeColor = System.Drawing.Color.Black;
            this.lvwHospitals.FullRowSelect = true;
            this.lvwHospitals.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwHospitals.Location = new System.Drawing.Point(35, 25);
            this.lvwHospitals.MultiSelect = false;
            this.lvwHospitals.Name = "lvwHospitals";
            this.lvwHospitals.Size = new System.Drawing.Size(306, 398);
            this.lvwHospitals.TabIndex = 0;
            this.lvwHospitals.UseCompatibleStateImageBehavior = false;
            this.lvwHospitals.View = System.Windows.Forms.View.Details;
            // 
            // btnAddHospital
            // 
            this.btnAddHospital.BackColor = System.Drawing.Color.Transparent;
            this.btnAddHospital.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddHospital.BackgroundImage")));
            this.btnAddHospital.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddHospital.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddHospital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHospital.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddHospital.Location = new System.Drawing.Point(60, 435);
            this.btnAddHospital.Name = "btnAddHospital";
            this.btnAddHospital.Size = new System.Drawing.Size(78, 25);
            this.btnAddHospital.TabIndex = 1;
            this.btnAddHospital.Text = "Add";
            this.btnAddHospital.UseVisualStyleBackColor = false;
            this.btnAddHospital.Click += new System.EventHandler(this.btnAddHospital_Click);
            this.btnAddHospital.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnAddHospital.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnRemoveHospital
            // 
            this.btnRemoveHospital.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveHospital.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveHospital.BackgroundImage")));
            this.btnRemoveHospital.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveHospital.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemoveHospital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveHospital.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveHospital.Location = new System.Drawing.Point(154, 435);
            this.btnRemoveHospital.Name = "btnRemoveHospital";
            this.btnRemoveHospital.Size = new System.Drawing.Size(78, 25);
            this.btnRemoveHospital.TabIndex = 2;
            this.btnRemoveHospital.Text = "Remove";
            this.btnRemoveHospital.UseVisualStyleBackColor = false;
            this.btnRemoveHospital.Click += new System.EventHandler(this.btnRemoveHospital_Click);
            this.btnRemoveHospital.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRemoveHospital.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnRemAllHospital
            // 
            this.btnRemAllHospital.BackColor = System.Drawing.Color.Transparent;
            this.btnRemAllHospital.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemAllHospital.BackgroundImage")));
            this.btnRemAllHospital.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemAllHospital.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemAllHospital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemAllHospital.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemAllHospital.Location = new System.Drawing.Point(248, 435);
            this.btnRemAllHospital.Name = "btnRemAllHospital";
            this.btnRemAllHospital.Size = new System.Drawing.Size(78, 25);
            this.btnRemAllHospital.TabIndex = 3;
            this.btnRemAllHospital.Text = "Remove All";
            this.btnRemAllHospital.UseVisualStyleBackColor = false;
            this.btnRemAllHospital.Click += new System.EventHandler(this.btnRemAllHospital_Click);
            this.btnRemAllHospital.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRemAllHospital.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // GroupBox9
            // 
            this.GroupBox9.Controls.Add(this.lvwInsurances);
            this.GroupBox9.Controls.Add(this.btnRemoveInsurance);
            this.GroupBox9.Controls.Add(this.btnRemAllInsurance);
            this.GroupBox9.Controls.Add(this.btnAddInsurance);
            this.GroupBox9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GroupBox9.Location = new System.Drawing.Point(17, 43);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(343, 473);
            this.GroupBox9.TabIndex = 1;
            this.GroupBox9.TabStop = false;
            this.GroupBox9.Text = "Insurance Information";
            // 
            // lvwInsurances
            // 
            this.lvwInsurances.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwInsurances.ForeColor = System.Drawing.Color.Black;
            this.lvwInsurances.FullRowSelect = true;
            this.lvwInsurances.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwInsurances.Location = new System.Drawing.Point(10, 23);
            this.lvwInsurances.MultiSelect = false;
            this.lvwInsurances.Name = "lvwInsurances";
            this.lvwInsurances.Size = new System.Drawing.Size(306, 400);
            this.lvwInsurances.TabIndex = 0;
            this.lvwInsurances.UseCompatibleStateImageBehavior = false;
            this.lvwInsurances.View = System.Windows.Forms.View.Details;
            // 
            // btnRemoveInsurance
            // 
            this.btnRemoveInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveInsurance.BackgroundImage")));
            this.btnRemoveInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemoveInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveInsurance.Location = new System.Drawing.Point(126, 435);
            this.btnRemoveInsurance.Name = "btnRemoveInsurance";
            this.btnRemoveInsurance.Size = new System.Drawing.Size(78, 25);
            this.btnRemoveInsurance.TabIndex = 2;
            this.btnRemoveInsurance.Text = "Remove";
            this.btnRemoveInsurance.UseVisualStyleBackColor = false;
            this.btnRemoveInsurance.Click += new System.EventHandler(this.btnRemoveInsurance_Click);
            this.btnRemoveInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRemoveInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnRemAllInsurance
            // 
            this.btnRemAllInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnRemAllInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemAllInsurance.BackgroundImage")));
            this.btnRemAllInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemAllInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemAllInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemAllInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemAllInsurance.Location = new System.Drawing.Point(221, 435);
            this.btnRemAllInsurance.Name = "btnRemAllInsurance";
            this.btnRemAllInsurance.Size = new System.Drawing.Size(78, 25);
            this.btnRemAllInsurance.TabIndex = 2;
            this.btnRemAllInsurance.Text = "Remove All";
            this.btnRemAllInsurance.UseVisualStyleBackColor = false;
            this.btnRemAllInsurance.Click += new System.EventHandler(this.btnRemAllInsurance_Click);
            this.btnRemAllInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnRemAllInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnAddInsurance
            // 
            this.btnAddInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnAddInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddInsurance.BackgroundImage")));
            this.btnAddInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInsurance.Location = new System.Drawing.Point(31, 435);
            this.btnAddInsurance.Name = "btnAddInsurance";
            this.btnAddInsurance.Size = new System.Drawing.Size(78, 25);
            this.btnAddInsurance.TabIndex = 1;
            this.btnAddInsurance.Text = "Add";
            this.btnAddInsurance.UseVisualStyleBackColor = false;
            this.btnAddInsurance.Click += new System.EventHandler(this.btnAddInsurance_Click);
            this.btnAddInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnAddInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrevious.BackgroundImage")));
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Location = new System.Drawing.Point(810, 677);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 26);
            this.btnPrevious.TabIndex = 33;
            this.btnPrevious.Text = "&Previous";
            this.btnPrevious.Visible = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            this.btnPrevious.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnPrevious.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage3.Controls.Add(this.Panel15);
            this.tabPage3.Controls.Add(this.Panel11);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(743, 867);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Billing IDs";
            // 
            // Panel15
            // 
            this.Panel15.Controls.Add(this.Label98);
            this.Panel15.Controls.Add(this.Label97);
            this.Panel15.Controls.Add(this.Label96);
            this.Panel15.Controls.Add(this.Label74);
            this.Panel15.Controls.Add(this.c1PhysicianAdditionalIDs);
            this.Panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel15.Location = new System.Drawing.Point(0, 23);
            this.Panel15.Name = "Panel15";
            this.Panel15.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Panel15.Size = new System.Drawing.Size(743, 844);
            this.Panel15.TabIndex = 10;
            // 
            // Label98
            // 
            this.Label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label98.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label98.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label98.Location = new System.Drawing.Point(1, 3);
            this.Label98.Name = "Label98";
            this.Label98.Size = new System.Drawing.Size(741, 1);
            this.Label98.TabIndex = 132;
            // 
            // Label97
            // 
            this.Label97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label97.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label97.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label97.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label97.Location = new System.Drawing.Point(1, 843);
            this.Label97.Name = "Label97";
            this.Label97.Size = new System.Drawing.Size(741, 1);
            this.Label97.TabIndex = 131;
            // 
            // Label96
            // 
            this.Label96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label96.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label96.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label96.Location = new System.Drawing.Point(742, 3);
            this.Label96.Name = "Label96";
            this.Label96.Size = new System.Drawing.Size(1, 841);
            this.Label96.TabIndex = 128;
            // 
            // Label74
            // 
            this.Label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label74.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label74.Location = new System.Drawing.Point(0, 3);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(1, 841);
            this.Label74.TabIndex = 127;
            // 
            // c1PhysicianAdditionalIDs
            // 
            this.c1PhysicianAdditionalIDs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PhysicianAdditionalIDs.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1PhysicianAdditionalIDs.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PhysicianAdditionalIDs.AutoGenerateColumns = false;
            this.c1PhysicianAdditionalIDs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PhysicianAdditionalIDs.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PhysicianAdditionalIDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PhysicianAdditionalIDs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PhysicianAdditionalIDs.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.c1PhysicianAdditionalIDs.Location = new System.Drawing.Point(0, 3);
            this.c1PhysicianAdditionalIDs.Name = "c1PhysicianAdditionalIDs";
            this.c1PhysicianAdditionalIDs.Rows.Count = 1;
            this.c1PhysicianAdditionalIDs.Rows.DefaultSize = 19;
            this.c1PhysicianAdditionalIDs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.c1PhysicianAdditionalIDs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1PhysicianAdditionalIDs.Size = new System.Drawing.Size(743, 841);
            this.c1PhysicianAdditionalIDs.StyleInfo = resources.GetString("c1PhysicianAdditionalIDs.StyleInfo");
            this.c1PhysicianAdditionalIDs.TabIndex = 0;
            this.c1PhysicianAdditionalIDs.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ProviderIdentification_StartEdit);
            this.c1PhysicianAdditionalIDs.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ProviderIdentification_SetupEditor);
            this.c1PhysicianAdditionalIDs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1ProviderIdentification_MouseMove);
            // 
            // Panel11
            // 
            this.Panel11.BackgroundImage = global::gloContacts.Properties.Resources.Img_Button;
            this.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel11.Controls.Add(this.label52);
            this.Panel11.Controls.Add(this.label51);
            this.Panel11.Controls.Add(this.label50);
            this.Panel11.Controls.Add(this.Label92);
            this.Panel11.Controls.Add(this.Label91);
            this.Panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel11.Location = new System.Drawing.Point(0, 0);
            this.Panel11.Name = "Panel11";
            this.Panel11.Size = new System.Drawing.Size(743, 23);
            this.Panel11.TabIndex = 129;
            this.Panel11.TabStop = true;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Location = new System.Drawing.Point(1, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(741, 1);
            this.label52.TabIndex = 133;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Right;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(742, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1, 22);
            this.label51.TabIndex = 132;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(0, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 22);
            this.label50.TabIndex = 131;
            // 
            // Label92
            // 
            this.Label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label92.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label92.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label92.Location = new System.Drawing.Point(0, 22);
            this.Label92.Name = "Label92";
            this.Label92.Size = new System.Drawing.Size(743, 1);
            this.Label92.TabIndex = 130;
            // 
            // Label91
            // 
            this.Label91.BackColor = System.Drawing.Color.Transparent;
            this.Label91.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label91.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label91.Location = new System.Drawing.Point(0, 0);
            this.Label91.Name = "Label91";
            this.Label91.Size = new System.Drawing.Size(743, 23);
            this.Label91.TabIndex = 129;
            this.Label91.Text = "  Additional Billing IDs";
            this.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupPhysician
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(774, 823);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPhysician";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Physician";
            this.Load += new System.EventHandler(this.frmSetupPhysician_Load);
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.gboxContact_detail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpBoxSecureMsg.ResumeLayout(false);
            this.grpBoxSecureMsg.PerformLayout();
            this.GroupBoxTaxonomy.ResumeLayout(false);
            this.GroupBoxTaxonomy.PerformLayout();
            this.GBox_Pracadrs.ResumeLayout(false);
            this.GBox_Pracadrs.PerformLayout();
            this.GBox_BMadrs.ResumeLayout(false);
            this.GBox_BMadrs.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.GroupBox10.ResumeLayout(false);
            this.GroupBox9.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.Panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PhysicianAdditionalIDs)).EndInit();
            this.Panel11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.GroupBox GroupBoxTaxonomy;
        internal System.Windows.Forms.Button btnClearTaxonomy;
        internal System.Windows.Forms.Button btnBrowseTaxonomy;
        internal System.Windows.Forms.TextBox txtNPI;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.TextBox txtUPIN;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.TextBox txtTaxID;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.TextBox txtTaxonomy;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.TextBox txtlName;
        internal System.Windows.Forms.TextBox txtmName;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtfName;
        internal System.Windows.Forms.Button btnNext;
        internal System.Windows.Forms.TabPage TabPage2;
        internal System.Windows.Forms.ComboBox cmbSpecialty;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.GroupBox GroupBox10;
        private System.Windows.Forms.ListView lvwHospitals;
        private System.Windows.Forms.Button btnAddHospital;
        private System.Windows.Forms.Button btnRemoveHospital;
        private System.Windows.Forms.Button btnRemAllHospital;
        internal System.Windows.Forms.GroupBox GroupBox9;
        private System.Windows.Forms.ListView lvwInsurances;
        private System.Windows.Forms.Button btnRemoveInsurance;
        private System.Windows.Forms.Button btnRemAllInsurance;
        private System.Windows.Forms.Button btnAddInsurance;
        internal System.Windows.Forms.Button btnPrevious;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox txt_comp_ZIP;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txt_Comp_AddressLine2;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_Comp_City;
        internal System.Windows.Forms.TextBox txt_Comp_AddressLine1;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.GroupBox GBox_Pracadrs;
        internal System.Windows.Forms.TextBox txt_Prac_Zip;
        internal System.Windows.Forms.TextBox txt_Prac_AddressLine2;
        internal System.Windows.Forms.Label label40;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.TextBox txt_Prac_AddressLine1;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox txt_Comp_URL;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txt_Comp_Email;
        internal System.Windows.Forms.TextBox txt_Comp_Pager;
        internal System.Windows.Forms.Label label16;
        internal gloMaskControl.gloMaskBox txt_Comp_Fax;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.GroupBox GBox_BMadrs;
        internal System.Windows.Forms.TextBox txt_BM_Zip;
        internal System.Windows.Forms.TextBox txt_BM_AddressLine2;
        internal System.Windows.Forms.Label label42;
        internal System.Windows.Forms.Label label43;
        internal System.Windows.Forms.TextBox txt_BM_AddressLine1;
        internal System.Windows.Forms.Label label46;
        internal gloMaskControl.gloMaskBox txt_Prac_Fax;
        internal System.Windows.Forms.Label label39;
        internal System.Windows.Forms.TextBox txt_Prac_Email;
        internal System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox txt_Prac_URL;
        internal System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_Prac_City;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label label34;
        internal System.Windows.Forms.Label label35;
        internal gloMaskControl.gloMaskBox txt_BM_Fax;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.TextBox txt_BM_Email;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox txt_BM_URL;
        internal System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label15;
        internal System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_BM_City;
        private System.Windows.Forms.CheckBox chBoxPracAdds;
        private System.Windows.Forms.CheckBox chBoxBMAds;
        internal System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtNotes;
        internal System.Windows.Forms.Label label36;
        internal System.Windows.Forms.TextBox txt_Comp_State;
        internal System.Windows.Forms.TextBox txt_BM_State;
        internal System.Windows.Forms.TextBox txt_Prac_State;
        private System.Windows.Forms.GroupBox gboxContact_detail;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Template;
        private System.Windows.Forms.Label label20;
        private gloMaskControl.gloMaskBox mtxt_Comp_Phone;
        private gloMaskControl.gloMaskBox mtxt_Comp_mobile;
        private gloMaskControl.gloMaskBox mtxt_BM_Phone;
        private gloMaskControl.gloMaskBox mtxt_Prac_Phone;
        internal System.Windows.Forms.Panel pnlComAddresssControl;
        internal System.Windows.Forms.Panel pnlBMAddresssControl;
        internal System.Windows.Forms.Panel pnlBPracAddresssControl;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        internal System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.RadioButton rbUsePlanSetting;
        private System.Windows.Forms.RadioButton rbAlways;
        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.RadioButton rbGender2;
        internal System.Windows.Forms.RadioButton rbGender1;
        internal System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.Panel Panel15;
        internal System.Windows.Forms.Label Label98;
        internal System.Windows.Forms.Label Label97;
        internal System.Windows.Forms.Label Label96;
        internal System.Windows.Forms.Label Label74;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PhysicianAdditionalIDs;
        internal System.Windows.Forms.Label label44;
        internal System.Windows.Forms.Label label47;
        internal System.Windows.Forms.Label label48;
        internal System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label41;
        internal System.Windows.Forms.Panel Panel11;
        internal System.Windows.Forms.Label label52;
        internal System.Windows.Forms.Label label51;
        internal System.Windows.Forms.Label label50;
        internal System.Windows.Forms.Label Label92;
        internal System.Windows.Forms.Label Label91;
        private System.Windows.Forms.GroupBox grpBoxSecureMsg;
        internal System.Windows.Forms.TextBox txtSPI;
        internal System.Windows.Forms.TextBox txtDirectAddress;
        internal System.Windows.Forms.Label label54;
        internal System.Windows.Forms.Label label56;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.TextBox txtSpecialtyType;
        internal System.Windows.Forms.Label label53;
        internal System.Windows.Forms.Label label55;
        internal System.Windows.Forms.TextBox txtClinicName;
    }
}

