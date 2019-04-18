namespace gloBilling
{
    partial class frmSetupCPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupCPT));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.lblCPTCode = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtCPTCode = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbSpecialty = new System.Windows.Forms.ComboBox();
            this.pnl_Toolstrip = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInactive = new System.Windows.Forms.Panel();
            this.chkNonServiceCode = new System.Windows.Forms.CheckBox();
            this.chkDefaultSelf = new System.Windows.Forms.CheckBox();
            this.txt_Rate2 = new System.Windows.Forms.TextBox();
            this.cbx_IsTaxable = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbx_Inactive = new System.Windows.Forms.CheckBox();
            this.pnlrevenue = new System.Windows.Forms.Panel();
            this.cmbRevenueCode = new System.Windows.Forms.ComboBox();
            this.txt_Rate1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblRevenueCode = new System.Windows.Forms.Label();
            this.pnlEPSDT = new System.Windows.Forms.Panel();
            this.chkEpsdtFamPlan = new System.Windows.Forms.CheckBox();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.chkMammogram = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.rbtUnit = new System.Windows.Forms.RadioButton();
            this.rbtVisit = new System.Windows.Forms.RadioButton();
            this.rbtPatient = new System.Windows.Forms.RadioButton();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.mskCPTInactivationDate = new System.Windows.Forms.MaskedTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.mskCPTActivationDate = new System.Windows.Forms.MaskedTextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lblFacilityCLIANo = new System.Windows.Forms.Label();
            this.lblCPTFrom = new System.Windows.Forms.Label();
            this.txtCPTFrom = new System.Windows.Forms.TextBox();
            this.txtCptCLIANo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCPTTO = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStatementdesc = new System.Windows.Forms.TextBox();
            this.lblCPTStatementdesc = new System.Windows.Forms.Label();
            this.txtCPTTo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Mod1Code = new System.Windows.Forms.TextBox();
            this.txt_Mod2Code = new System.Windows.Forms.TextBox();
            this.txt_ClinicFee = new System.Windows.Forms.TextBox();
            this.txt_Mod3Code = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Mod4Code = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_DeafultUnits = new System.Windows.Forms.TextBox();
            this.txtProductCost = new System.Windows.Forms.TextBox();
            this.txt_Charges = new System.Windows.Forms.TextBox();
            this.cbx_IsCPTdrug = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_Ndccode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.dgMasters = new System.Windows.Forms.DataGridView();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabCPTMaster = new System.Windows.Forms.TabControl();
            this.tpCPT = new System.Windows.Forms.TabPage();
            this.tpGlobalPeriods = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.c1GPInsurance = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbPeriodDays = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBillingReminder = new System.Windows.Forms.TextBox();
            this.lblGPtriggers = new System.Windows.Forms.Label();
            this.cmbGPtriggers = new System.Windows.Forms.ComboBox();
            this.lblPeriodDays = new System.Windows.Forms.Label();
            this.lblBillingReminder = new System.Windows.Forms.Label();
            this.tpAnesthesia = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.chkAnesthesia = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtBaseUnits = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.ts_Commands.SuspendLayout();
            this.pnl_Toolstrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlInactive.SuspendLayout();
            this.pnlrevenue.SuspendLayout();
            this.pnlEPSDT.SuspendLayout();
            this.pnlmain.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMasters)).BeginInit();
            this.Panel2.SuspendLayout();
            this.tabCPTMaster.SuspendLayout();
            this.tpCPT.SuspendLayout();
            this.tpGlobalPeriods.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GPInsurance)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tpAnesthesia.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
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
            this.ts_Commands.Size = new System.Drawing.Size(534, 53);
            this.ts_Commands.TabIndex = 20;
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
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // lblCPTCode
            // 
            this.lblCPTCode.AutoSize = true;
            this.lblCPTCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTCode.Location = new System.Drawing.Point(92, 8);
            this.lblCPTCode.MaximumSize = new System.Drawing.Size(69, 14);
            this.lblCPTCode.MinimumSize = new System.Drawing.Size(69, 14);
            this.lblCPTCode.Name = "lblCPTCode";
            this.lblCPTCode.Size = new System.Drawing.Size(69, 14);
            this.lblCPTCode.TabIndex = 6;
            this.lblCPTCode.Text = "CPT Code :";
            this.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblDescription.Location = new System.Drawing.Point(16, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(78, 14);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description :";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDescription.Click += new System.EventHandler(this.lblDescription_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Location = new System.Drawing.Point(97, 120);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(64, 14);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "Category :";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Location = new System.Drawing.Point(98, 92);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(63, 14);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "Specialty :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // txtCPTCode
            // 
            this.txtCPTCode.ForeColor = System.Drawing.Color.Black;
            this.txtCPTCode.Location = new System.Drawing.Point(166, 4);
            this.txtCPTCode.MaxLength = 50;
            this.txtCPTCode.Name = "txtCPTCode";
            this.txtCPTCode.Size = new System.Drawing.Size(321, 22);
            this.txtCPTCode.TabIndex = 7;
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(166, 32);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(321, 22);
            this.txtDescription.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.Location = new System.Drawing.Point(166, 116);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(321, 22);
            this.cmbCategory.TabIndex = 4;
            // 
            // cmbSpecialty
            // 
            this.cmbSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecialty.ForeColor = System.Drawing.Color.Black;
            this.cmbSpecialty.Location = new System.Drawing.Point(166, 88);
            this.cmbSpecialty.Name = "cmbSpecialty";
            this.cmbSpecialty.Size = new System.Drawing.Size(321, 22);
            this.cmbSpecialty.TabIndex = 3;
            // 
            // pnl_Toolstrip
            // 
            this.pnl_Toolstrip.Controls.Add(this.ts_Commands);
            this.pnl_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_Toolstrip.Name = "pnl_Toolstrip";
            this.pnl_Toolstrip.Size = new System.Drawing.Size(534, 55);
            this.pnl_Toolstrip.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pnlInactive);
            this.panel1.Controls.Add(this.pnlrevenue);
            this.panel1.Controls.Add(this.pnlEPSDT);
            this.panel1.Controls.Add(this.pnlmain);
            this.panel1.Controls.Add(this.lbl_BottomBrd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 523);
            this.panel1.TabIndex = 1;
            // 
            // pnlInactive
            // 
            this.pnlInactive.Controls.Add(this.chkNonServiceCode);
            this.pnlInactive.Controls.Add(this.chkDefaultSelf);
            this.pnlInactive.Controls.Add(this.txt_Rate2);
            this.pnlInactive.Controls.Add(this.cbx_IsTaxable);
            this.pnlInactive.Controls.Add(this.label14);
            this.pnlInactive.Controls.Add(this.cbx_Inactive);
            this.pnlInactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInactive.Location = new System.Drawing.Point(0, 454);
            this.pnlInactive.Name = "pnlInactive";
            this.pnlInactive.Size = new System.Drawing.Size(518, 68);
            this.pnlInactive.TabIndex = 21;
            // 
            // chkNonServiceCode
            // 
            this.chkNonServiceCode.AutoSize = true;
            this.chkNonServiceCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkNonServiceCode.Location = new System.Drawing.Point(167, 44);
            this.chkNonServiceCode.Name = "chkNonServiceCode";
            this.chkNonServiceCode.Size = new System.Drawing.Size(248, 18);
            this.chkNonServiceCode.TabIndex = 4;
            this.chkNonServiceCode.Text = "Non-service communication to Insurance";
            this.c1SuperTooltip1.SetToolTip(this.chkNonServiceCode, "Select when no insurance remittance is expected");
            this.chkNonServiceCode.UseVisualStyleBackColor = true;
            // 
            // chkDefaultSelf
            // 
            this.chkDefaultSelf.AutoSize = true;
            this.chkDefaultSelf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkDefaultSelf.Location = new System.Drawing.Point(167, 24);
            this.chkDefaultSelf.Name = "chkDefaultSelf";
            this.chkDefaultSelf.Size = new System.Drawing.Size(128, 18);
            this.chkDefaultSelf.TabIndex = 3;
            this.chkDefaultSelf.Text = "Set Default to Self";
            this.c1SuperTooltip1.SetToolTip(this.chkDefaultSelf, "Set Default to Self");
            this.chkDefaultSelf.UseVisualStyleBackColor = true;
            // 
            // txt_Rate2
            // 
            this.txt_Rate2.ForeColor = System.Drawing.Color.Black;
            this.txt_Rate2.Location = new System.Drawing.Point(431, 3);
            this.txt_Rate2.MaxLength = 10;
            this.txt_Rate2.Name = "txt_Rate2";
            this.txt_Rate2.ShortcutsEnabled = false;
            this.txt_Rate2.Size = new System.Drawing.Size(56, 22);
            this.txt_Rate2.TabIndex = 2;
            this.txt_Rate2.Visible = false;
            this.txt_Rate2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Rate2_KeyPress);
            this.txt_Rate2.Leave += new System.EventHandler(this.txt_Amount_Leave);
            // 
            // cbx_IsTaxable
            // 
            this.cbx_IsTaxable.AutoSize = true;
            this.cbx_IsTaxable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_IsTaxable.Location = new System.Drawing.Point(243, 4);
            this.cbx_IsTaxable.Name = "cbx_IsTaxable";
            this.cbx_IsTaxable.Size = new System.Drawing.Size(81, 18);
            this.cbx_IsTaxable.TabIndex = 1;
            this.cbx_IsTaxable.Text = "Is Taxable";
            this.cbx_IsTaxable.UseVisualStyleBackColor = true;
            this.cbx_IsTaxable.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(368, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 14);
            this.label14.TabIndex = 30;
            this.label14.Text = "Rate ($) :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Visible = false;
            // 
            // cbx_Inactive
            // 
            this.cbx_Inactive.AutoSize = true;
            this.cbx_Inactive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_Inactive.Location = new System.Drawing.Point(167, 4);
            this.cbx_Inactive.Name = "cbx_Inactive";
            this.cbx_Inactive.Size = new System.Drawing.Size(69, 18);
            this.cbx_Inactive.TabIndex = 0;
            this.cbx_Inactive.Text = "Inactive";
            this.cbx_Inactive.UseVisualStyleBackColor = true;
            // 
            // pnlrevenue
            // 
            this.pnlrevenue.Controls.Add(this.cmbRevenueCode);
            this.pnlrevenue.Controls.Add(this.txt_Rate1);
            this.pnlrevenue.Controls.Add(this.label13);
            this.pnlrevenue.Controls.Add(this.lblRevenueCode);
            this.pnlrevenue.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlrevenue.Location = new System.Drawing.Point(0, 430);
            this.pnlrevenue.Name = "pnlrevenue";
            this.pnlrevenue.Size = new System.Drawing.Size(518, 24);
            this.pnlrevenue.TabIndex = 20;
            // 
            // cmbRevenueCode
            // 
            this.cmbRevenueCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRevenueCode.ForeColor = System.Drawing.Color.Black;
            this.cmbRevenueCode.Location = new System.Drawing.Point(165, 1);
            this.cmbRevenueCode.Name = "cmbRevenueCode";
            this.cmbRevenueCode.Size = new System.Drawing.Size(155, 22);
            this.cmbRevenueCode.TabIndex = 0;
            // 
            // txt_Rate1
            // 
            this.txt_Rate1.ForeColor = System.Drawing.Color.Black;
            this.txt_Rate1.Location = new System.Drawing.Point(431, 1);
            this.txt_Rate1.MaxLength = 10;
            this.txt_Rate1.Name = "txt_Rate1";
            this.txt_Rate1.ShortcutsEnabled = false;
            this.txt_Rate1.Size = new System.Drawing.Size(56, 22);
            this.txt_Rate1.TabIndex = 1;
            this.txt_Rate1.Visible = false;
            this.txt_Rate1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Rate1_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(363, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 14);
            this.label13.TabIndex = 26;
            this.label13.Text = "Rate (%) :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Visible = false;
            // 
            // lblRevenueCode
            // 
            this.lblRevenueCode.AutoSize = true;
            this.lblRevenueCode.BackColor = System.Drawing.Color.Transparent;
            this.lblRevenueCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRevenueCode.Location = new System.Drawing.Point(68, 4);
            this.lblRevenueCode.Name = "lblRevenueCode";
            this.lblRevenueCode.Size = new System.Drawing.Size(95, 14);
            this.lblRevenueCode.TabIndex = 119;
            this.lblRevenueCode.Text = "Revenue Code :";
            this.lblRevenueCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlEPSDT
            // 
            this.pnlEPSDT.Controls.Add(this.chkEpsdtFamPlan);
            this.pnlEPSDT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEPSDT.Location = new System.Drawing.Point(0, 409);
            this.pnlEPSDT.Name = "pnlEPSDT";
            this.pnlEPSDT.Size = new System.Drawing.Size(518, 21);
            this.pnlEPSDT.TabIndex = 19;
            // 
            // chkEpsdtFamPlan
            // 
            this.chkEpsdtFamPlan.AutoSize = true;
            this.chkEpsdtFamPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkEpsdtFamPlan.Location = new System.Drawing.Point(167, 3);
            this.chkEpsdtFamPlan.Name = "chkEpsdtFamPlan";
            this.chkEpsdtFamPlan.Size = new System.Drawing.Size(220, 18);
            this.chkEpsdtFamPlan.TabIndex = 118;
            this.chkEpsdtFamPlan.Text = "Prompt for EPSDT / Family Planning";
            this.chkEpsdtFamPlan.UseVisualStyleBackColor = true;
            // 
            // pnlmain
            // 
            this.pnlmain.Controls.Add(this.chkMammogram);
            this.pnlmain.Controls.Add(this.panel9);
            this.pnlmain.Controls.Add(this.label38);
            this.pnlmain.Controls.Add(this.label37);
            this.pnlmain.Controls.Add(this.mskCPTInactivationDate);
            this.pnlmain.Controls.Add(this.label35);
            this.pnlmain.Controls.Add(this.mskCPTActivationDate);
            this.pnlmain.Controls.Add(this.label34);
            this.pnlmain.Controls.Add(this.lblFacilityCLIANo);
            this.pnlmain.Controls.Add(this.lblCPTFrom);
            this.pnlmain.Controls.Add(this.txtCPTFrom);
            this.pnlmain.Controls.Add(this.txtCptCLIANo);
            this.pnlmain.Controls.Add(this.panel3);
            this.pnlmain.Controls.Add(this.label5);
            this.pnlmain.Controls.Add(this.txtStatementdesc);
            this.pnlmain.Controls.Add(this.cmbSpecialty);
            this.pnlmain.Controls.Add(this.lblCPTStatementdesc);
            this.pnlmain.Controls.Add(this.Label4);
            this.pnlmain.Controls.Add(this.cmbCategory);
            this.pnlmain.Controls.Add(this.txtCPTCode);
            this.pnlmain.Controls.Add(this.Label3);
            this.pnlmain.Controls.Add(this.txtCPTTo);
            this.pnlmain.Controls.Add(this.txtDescription);
            this.pnlmain.Controls.Add(this.lblCPTCode);
            this.pnlmain.Controls.Add(this.label10);
            this.pnlmain.Controls.Add(this.txt_Mod1Code);
            this.pnlmain.Controls.Add(this.txt_Mod2Code);
            this.pnlmain.Controls.Add(this.txt_ClinicFee);
            this.pnlmain.Controls.Add(this.txt_Mod3Code);
            this.pnlmain.Controls.Add(this.label17);
            this.pnlmain.Controls.Add(this.txt_Mod4Code);
            this.pnlmain.Controls.Add(this.label11);
            this.pnlmain.Controls.Add(this.txt_DeafultUnits);
            this.pnlmain.Controls.Add(this.txtProductCost);
            this.pnlmain.Controls.Add(this.txt_Charges);
            this.pnlmain.Controls.Add(this.cbx_IsCPTdrug);
            this.pnlmain.Controls.Add(this.label15);
            this.pnlmain.Controls.Add(this.txt_Ndccode);
            this.pnlmain.Controls.Add(this.label12);
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlmain.Location = new System.Drawing.Point(0, 0);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(518, 409);
            this.pnlmain.TabIndex = 121;
            // 
            // chkMammogram
            // 
            this.chkMammogram.AutoSize = true;
            this.chkMammogram.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkMammogram.Location = new System.Drawing.Point(167, 388);
            this.chkMammogram.Name = "chkMammogram";
            this.chkMammogram.Size = new System.Drawing.Size(95, 18);
            this.chkMammogram.TabIndex = 31;
            this.chkMammogram.Text = "Mammogram";
            this.c1SuperTooltip1.SetToolTip(this.chkMammogram, "Select when no insurance remittance is expected");
            this.chkMammogram.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.rbtUnit);
            this.panel9.Controls.Add(this.rbtVisit);
            this.panel9.Controls.Add(this.rbtPatient);
            this.panel9.Location = new System.Drawing.Point(165, 364);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(224, 25);
            this.panel9.TabIndex = 17;
            // 
            // rbtUnit
            // 
            this.rbtUnit.AutoSize = true;
            this.rbtUnit.Location = new System.Drawing.Point(151, 3);
            this.rbtUnit.Name = "rbtUnit";
            this.rbtUnit.Size = new System.Drawing.Size(47, 18);
            this.rbtUnit.TabIndex = 2;
            this.rbtUnit.TabStop = true;
            this.rbtUnit.Text = "Unit";
            this.rbtUnit.UseVisualStyleBackColor = true;
            // 
            // rbtVisit
            // 
            this.rbtVisit.AutoSize = true;
            this.rbtVisit.Location = new System.Drawing.Point(85, 3);
            this.rbtVisit.Name = "rbtVisit";
            this.rbtVisit.Size = new System.Drawing.Size(47, 18);
            this.rbtVisit.TabIndex = 1;
            this.rbtVisit.TabStop = true;
            this.rbtVisit.Text = "Visit";
            this.rbtVisit.UseVisualStyleBackColor = true;
            // 
            // rbtPatient
            // 
            this.rbtPatient.AutoSize = true;
            this.rbtPatient.Location = new System.Drawing.Point(2, 3);
            this.rbtPatient.Name = "rbtPatient";
            this.rbtPatient.Size = new System.Drawing.Size(64, 18);
            this.rbtPatient.TabIndex = 0;
            this.rbtPatient.TabStop = true;
            this.rbtPatient.Text = "Patient";
            this.rbtPatient.UseVisualStyleBackColor = true;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(100, 366);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(61, 14);
            this.label38.TabIndex = 124;
            this.label38.Text = "Cost per :";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(75, 344);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(86, 14);
            this.label37.TabIndex = 123;
            this.label37.Text = "Product Cost :";
            // 
            // mskCPTInactivationDate
            // 
            this.mskCPTInactivationDate.Location = new System.Drawing.Point(166, 312);
            this.mskCPTInactivationDate.Mask = "00/00/0000";
            this.mskCPTInactivationDate.Name = "mskCPTInactivationDate";
            this.mskCPTInactivationDate.Size = new System.Drawing.Size(155, 22);
            this.mskCPTInactivationDate.TabIndex = 16;
            this.mskCPTInactivationDate.ValidatingType = typeof(System.DateTime);
            this.mskCPTInactivationDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCPTActivationDate_MouseClick);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(53, 316);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(108, 14);
            this.label35.TabIndex = 120;
            this.label35.Text = "Inactivation Date :";
            // 
            // mskCPTActivationDate
            // 
            this.mskCPTActivationDate.Location = new System.Drawing.Point(166, 284);
            this.mskCPTActivationDate.Mask = "00/00/0000";
            this.mskCPTActivationDate.Name = "mskCPTActivationDate";
            this.mskCPTActivationDate.Size = new System.Drawing.Size(155, 22);
            this.mskCPTActivationDate.TabIndex = 15;
            this.mskCPTActivationDate.ValidatingType = typeof(System.DateTime);
            this.mskCPTActivationDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mskCPTActivationDate_MouseClick);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(62, 288);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(99, 14);
            this.label34.TabIndex = 118;
            this.label34.Text = "Activation Date :";
            // 
            // lblFacilityCLIANo
            // 
            this.lblFacilityCLIANo.AutoSize = true;
            this.lblFacilityCLIANo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacilityCLIANo.Location = new System.Drawing.Point(78, 260);
            this.lblFacilityCLIANo.Name = "lblFacilityCLIANo";
            this.lblFacilityCLIANo.Size = new System.Drawing.Size(83, 14);
            this.lblFacilityCLIANo.TabIndex = 23;
            this.lblFacilityCLIANo.Text = "Default CLIA :";
            // 
            // lblCPTFrom
            // 
            this.lblCPTFrom.AutoSize = true;
            this.lblCPTFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTFrom.Location = new System.Drawing.Point(92, 8);
            this.lblCPTFrom.MaximumSize = new System.Drawing.Size(69, 14);
            this.lblCPTFrom.MinimumSize = new System.Drawing.Size(69, 14);
            this.lblCPTFrom.Name = "lblCPTFrom";
            this.lblCPTFrom.Size = new System.Drawing.Size(69, 14);
            this.lblCPTFrom.TabIndex = 114;
            this.lblCPTFrom.Text = "CPT From :";
            this.lblCPTFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCPTFrom
            // 
            this.txtCPTFrom.ForeColor = System.Drawing.Color.Black;
            this.txtCPTFrom.Location = new System.Drawing.Point(166, 4);
            this.txtCPTFrom.MaxLength = 50;
            this.txtCPTFrom.Name = "txtCPTFrom";
            this.txtCPTFrom.Size = new System.Drawing.Size(321, 22);
            this.txtCPTFrom.TabIndex = 0;
            this.txtCPTFrom.Visible = false;
            this.txtCPTFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPTFrom_KeyPress);
            this.txtCPTFrom.Validated += new System.EventHandler(this.txtCPTFrom_Validated);
            // 
            // txtCptCLIANo
            // 
            this.txtCptCLIANo.Location = new System.Drawing.Point(166, 256);
            this.txtCptCLIANo.MaxLength = 50;
            this.txtCptCLIANo.Name = "txtCptCLIANo";
            this.txtCptCLIANo.Size = new System.Drawing.Size(155, 22);
            this.txtCptCLIANo.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.lblDescription);
            this.panel3.Controls.Add(this.lblCPTTO);
            this.panel3.Location = new System.Drawing.Point(9, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(152, 14);
            this.panel3.TabIndex = 117;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label19.Location = new System.Drawing.Point(6, 0);
            this.label19.MaximumSize = new System.Drawing.Size(10, 10);
            this.label19.MinimumSize = new System.Drawing.Size(10, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(10, 10);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            // 
            // lblCPTTO
            // 
            this.lblCPTTO.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTTO.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCPTTO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTTO.Location = new System.Drawing.Point(94, 0);
            this.lblCPTTO.MaximumSize = new System.Drawing.Size(75, 14);
            this.lblCPTTO.Name = "lblCPTTO";
            this.lblCPTTO.Size = new System.Drawing.Size(58, 14);
            this.lblCPTTO.TabIndex = 113;
            this.lblCPTTO.Text = "CPT To :";
            this.lblCPTTO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCPTTO.Visible = false;
            this.lblCPTTO.Click += new System.EventHandler(this.lblCPTTO_Click);
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(81, 8);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStatementdesc
            // 
            this.txtStatementdesc.ForeColor = System.Drawing.Color.Black;
            this.txtStatementdesc.Location = new System.Drawing.Point(166, 60);
            this.txtStatementdesc.MaxLength = 50;
            this.txtStatementdesc.Name = "txtStatementdesc";
            this.txtStatementdesc.Size = new System.Drawing.Size(321, 22);
            this.txtStatementdesc.TabIndex = 2;
            // 
            // lblCPTStatementdesc
            // 
            this.lblCPTStatementdesc.AutoSize = true;
            this.lblCPTStatementdesc.BackColor = System.Drawing.Color.Transparent;
            this.lblCPTStatementdesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCPTStatementdesc.Location = new System.Drawing.Point(23, 64);
            this.lblCPTStatementdesc.Name = "lblCPTStatementdesc";
            this.lblCPTStatementdesc.Size = new System.Drawing.Size(138, 14);
            this.lblCPTStatementdesc.TabIndex = 116;
            this.lblCPTStatementdesc.Text = "Statement Description :";
            this.lblCPTStatementdesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCPTStatementdesc.Click += new System.EventHandler(this.lblCPTStatementdesc_Click);
            // 
            // txtCPTTo
            // 
            this.txtCPTTo.ForeColor = System.Drawing.Color.Black;
            this.txtCPTTo.Location = new System.Drawing.Point(166, 32);
            this.txtCPTTo.MaxLength = 50;
            this.txtCPTTo.Name = "txtCPTTo";
            this.txtCPTTo.Size = new System.Drawing.Size(321, 22);
            this.txtCPTTo.TabIndex = 1;
            this.txtCPTTo.Visible = false;
            this.txtCPTTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPTTo_KeyPress);
            this.txtCPTTo.Validated += new System.EventHandler(this.txtCPTTo_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(61, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 14);
            this.label10.TabIndex = 26;
            this.label10.Text = "Default Modifier :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // txt_Mod1Code
            // 
            this.txt_Mod1Code.ForeColor = System.Drawing.Color.Black;
            this.txt_Mod1Code.Location = new System.Drawing.Point(166, 144);
            this.txt_Mod1Code.MaxLength = 2;
            this.txt_Mod1Code.Name = "txt_Mod1Code";
            this.txt_Mod1Code.Size = new System.Drawing.Size(74, 22);
            this.txt_Mod1Code.TabIndex = 5;
            this.txt_Mod1Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ModCode_KeyPress);
            // 
            // txt_Mod2Code
            // 
            this.txt_Mod2Code.ForeColor = System.Drawing.Color.Black;
            this.txt_Mod2Code.Location = new System.Drawing.Point(248, 144);
            this.txt_Mod2Code.MaxLength = 2;
            this.txt_Mod2Code.Name = "txt_Mod2Code";
            this.txt_Mod2Code.Size = new System.Drawing.Size(74, 22);
            this.txt_Mod2Code.TabIndex = 6;
            this.txt_Mod2Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ModCode_KeyPress);
            // 
            // txt_ClinicFee
            // 
            this.txt_ClinicFee.ForeColor = System.Drawing.Color.Black;
            this.txt_ClinicFee.Location = new System.Drawing.Point(412, 200);
            this.txt_ClinicFee.MaxLength = 10;
            this.txt_ClinicFee.Name = "txt_ClinicFee";
            this.txt_ClinicFee.ShortcutsEnabled = false;
            this.txt_ClinicFee.Size = new System.Drawing.Size(74, 22);
            this.txt_ClinicFee.TabIndex = 11;
            this.txt_ClinicFee.Visible = false;
            this.txt_ClinicFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ClinicFee_KeyPress_1);
            this.txt_ClinicFee.Leave += new System.EventHandler(this.txt_Amount_Leave);
            // 
            // txt_Mod3Code
            // 
            this.txt_Mod3Code.ForeColor = System.Drawing.Color.Black;
            this.txt_Mod3Code.Location = new System.Drawing.Point(330, 144);
            this.txt_Mod3Code.MaxLength = 2;
            this.txt_Mod3Code.Name = "txt_Mod3Code";
            this.txt_Mod3Code.Size = new System.Drawing.Size(74, 22);
            this.txt_Mod3Code.TabIndex = 7;
            this.txt_Mod3Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ModCode_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(345, 204);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 14);
            this.label17.TabIndex = 21;
            this.label17.Text = "Clinic Fee :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Visible = false;
            // 
            // txt_Mod4Code
            // 
            this.txt_Mod4Code.ForeColor = System.Drawing.Color.Black;
            this.txt_Mod4Code.Location = new System.Drawing.Point(412, 144);
            this.txt_Mod4Code.MaxLength = 2;
            this.txt_Mod4Code.Name = "txt_Mod4Code";
            this.txt_Mod4Code.Size = new System.Drawing.Size(74, 22);
            this.txt_Mod4Code.TabIndex = 8;
            this.txt_Mod4Code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ModCode_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(76, 204);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 14);
            this.label11.TabIndex = 31;
            this.label11.Text = "Default Units :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // txt_DeafultUnits
            // 
            this.txt_DeafultUnits.ForeColor = System.Drawing.Color.Black;
            this.txt_DeafultUnits.Location = new System.Drawing.Point(166, 200);
            this.txt_DeafultUnits.MaxLength = 8;
            this.txt_DeafultUnits.Name = "txt_DeafultUnits";
            this.txt_DeafultUnits.ShortcutsEnabled = false;
            this.txt_DeafultUnits.Size = new System.Drawing.Size(155, 22);
            this.txt_DeafultUnits.TabIndex = 10;
            this.txt_DeafultUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_DeafultUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_DeafultUnits_KeyPress);
            this.txt_DeafultUnits.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_DeafultUnits_KeyUp);
            // 
            // txtProductCost
            // 
            this.txtProductCost.ForeColor = System.Drawing.Color.Black;
            this.txtProductCost.Location = new System.Drawing.Point(166, 340);
            this.txtProductCost.MaxLength = 10;
            this.txtProductCost.Name = "txtProductCost";
            this.txtProductCost.ShortcutsEnabled = false;
            this.txtProductCost.Size = new System.Drawing.Size(155, 22);
            this.txtProductCost.TabIndex = 9;
            this.txtProductCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtProductCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductCost_KeyPress);
            this.txtProductCost.Leave += new System.EventHandler(this.txt_Amount_Leave);
            // 
            // txt_Charges
            // 
            this.txt_Charges.ForeColor = System.Drawing.Color.Black;
            this.txt_Charges.Location = new System.Drawing.Point(166, 172);
            this.txt_Charges.MaxLength = 10;
            this.txt_Charges.Name = "txt_Charges";
            this.txt_Charges.ShortcutsEnabled = false;
            this.txt_Charges.Size = new System.Drawing.Size(155, 22);
            this.txt_Charges.TabIndex = 9;
            this.txt_Charges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Charges.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Charges_KeyPress);
            this.txt_Charges.Leave += new System.EventHandler(this.txt_Amount_Leave);
            // 
            // cbx_IsCPTdrug
            // 
            this.cbx_IsCPTdrug.AutoSize = true;
            this.cbx_IsCPTdrug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cbx_IsCPTdrug.Location = new System.Drawing.Point(328, 230);
            this.cbx_IsCPTdrug.Name = "cbx_IsCPTdrug";
            this.cbx_IsCPTdrug.Size = new System.Drawing.Size(112, 18);
            this.cbx_IsCPTdrug.TabIndex = 13;
            this.cbx_IsCPTdrug.Text = "Prompt for NDC";
            this.cbx_IsCPTdrug.UseVisualStyleBackColor = true;
            this.cbx_IsCPTdrug.CheckedChanged += new System.EventHandler(this.cbx_IsCPTdrug_CheckedChanged_1);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(60, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 14);
            this.label15.TabIndex = 41;
            this.label15.Text = "Default Charges :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // txt_Ndccode
            // 
            this.txt_Ndccode.ForeColor = System.Drawing.Color.Black;
            this.txt_Ndccode.Location = new System.Drawing.Point(166, 228);
            this.txt_Ndccode.MaxLength = 11;
            this.txt_Ndccode.Name = "txt_Ndccode";
            
            this.txt_Ndccode.Size = new System.Drawing.Size(155, 22);
            this.txt_Ndccode.TabIndex = 12;
            this.txt_Ndccode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Ndccode_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(80, 232);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 14);
            this.label12.TabIndex = 35;
            this.label12.Text = "Default NDC :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(0, 522);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(518, 1);
            this.lbl_BottomBrd.TabIndex = 49;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // dgMasters
            // 
            this.dgMasters.AllowUserToAddRows = false;
            this.dgMasters.AllowUserToDeleteRows = false;
            this.dgMasters.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMasters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMasters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgMasters.BackgroundColor = System.Drawing.Color.White;
            this.dgMasters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgMasters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMasters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMasters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMasters.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgMasters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMasters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgMasters.EnableHeadersVisualStyles = false;
            this.dgMasters.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgMasters.Location = new System.Drawing.Point(0, 3);
            this.dgMasters.MultiSelect = false;
            this.dgMasters.Name = "dgMasters";
            this.dgMasters.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMasters.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMasters.RowHeadersVisible = false;
            this.dgMasters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgMasters.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgMasters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMasters.Size = new System.Drawing.Size(518, 0);
            this.dgMasters.TabIndex = 112;
            this.dgMasters.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.label31);
            this.Panel2.Controls.Add(this.dgMasters);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(4, 527);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Panel2.Size = new System.Drawing.Size(518, 0);
            this.Panel2.TabIndex = 20;
            this.Panel2.Visible = false;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Top;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label31.Location = new System.Drawing.Point(0, 3);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(518, 1);
            this.label31.TabIndex = 113;
            this.label31.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 525);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(518, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 3);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 523);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(522, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 523);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(518, 1);
            this.label2.TabIndex = 5;
            this.label2.Text = "label1";
            // 
            // tabCPTMaster
            // 
            this.tabCPTMaster.Controls.Add(this.tpCPT);
            this.tabCPTMaster.Controls.Add(this.tpGlobalPeriods);
            this.tabCPTMaster.Controls.Add(this.tpAnesthesia);
            this.tabCPTMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCPTMaster.Location = new System.Drawing.Point(0, 55);
            this.tabCPTMaster.Name = "tabCPTMaster";
            this.tabCPTMaster.SelectedIndex = 0;
            this.tabCPTMaster.Size = new System.Drawing.Size(534, 556);
            this.tabCPTMaster.TabIndex = 21;
            this.tabCPTMaster.SelectedIndexChanged += new System.EventHandler(this.tabCPTMaster_SelectedIndexChanged);
            // 
            // tpCPT
            // 
            this.tpCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tpCPT.Controls.Add(this.label1);
            this.tpCPT.Controls.Add(this.Panel2);
            this.tpCPT.Controls.Add(this.panel1);
            this.tpCPT.Controls.Add(this.label2);
            this.tpCPT.Controls.Add(this.Label7);
            this.tpCPT.Controls.Add(this.Label6);
            this.tpCPT.Location = new System.Drawing.Point(4, 23);
            this.tpCPT.Name = "tpCPT";
            this.tpCPT.Padding = new System.Windows.Forms.Padding(3);
            this.tpCPT.Size = new System.Drawing.Size(526, 529);
            this.tpCPT.TabIndex = 0;
            this.tpCPT.Text = "CPT";
            // 
            // tpGlobalPeriods
            // 
            this.tpGlobalPeriods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tpGlobalPeriods.Controls.Add(this.panel7);
            this.tpGlobalPeriods.Controls.Add(this.panel5);
            this.tpGlobalPeriods.Controls.Add(this.panel4);
            this.tpGlobalPeriods.Location = new System.Drawing.Point(4, 23);
            this.tpGlobalPeriods.Name = "tpGlobalPeriods";
            this.tpGlobalPeriods.Padding = new System.Windows.Forms.Padding(3);
            this.tpGlobalPeriods.Size = new System.Drawing.Size(526, 529);
            this.tpGlobalPeriods.TabIndex = 1;
            this.tpGlobalPeriods.Text = "Global Period";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.c1GPInsurance);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.label29);
            this.panel7.Controls.Add(this.label30);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 189);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(520, 337);
            this.panel7.TabIndex = 221;
            this.panel7.TabStop = true;
            // 
            // c1GPInsurance
            // 
            this.c1GPInsurance.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1GPInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1GPInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1GPInsurance.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1GPInsurance.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1GPInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1GPInsurance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1GPInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1GPInsurance.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1GPInsurance.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1GPInsurance.Location = new System.Drawing.Point(1, 1);
            this.c1GPInsurance.Name = "c1GPInsurance";
            this.c1GPInsurance.Rows.Count = 1;
            this.c1GPInsurance.Rows.DefaultSize = 22;
            this.c1GPInsurance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1GPInsurance.Size = new System.Drawing.Size(518, 335);
            this.c1GPInsurance.StyleInfo = resources.GetString("c1GPInsurance.StyleInfo");
            this.c1GPInsurance.TabIndex = 8;
            this.c1GPInsurance.EnterCell += new System.EventHandler(this.c1GPInsurance_EnterCell);
            this.c1GPInsurance.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1GPInsurance_StartEdit);
            this.c1GPInsurance.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1GPInsurance_SetupEditor);
            this.c1GPInsurance.KeyPressEdit += new C1.Win.C1FlexGrid.KeyPressEditEventHandler(this.c1GPInsurance_KeyPressEdit);
            this.c1GPInsurance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1GPInsurance_KeyPress);
            this.c1GPInsurance.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1GPInsurance_MouseMove);
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(1, 336);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(518, 1);
            this.label27.TabIndex = 7;
            this.label27.Text = "label1";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(1, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(518, 1);
            this.label28.TabIndex = 6;
            this.label28.Text = "label1";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(519, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(1, 337);
            this.label29.TabIndex = 5;
            this.label29.Text = "label4";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 337);
            this.label30.TabIndex = 4;
            this.label30.Text = "label4";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel5.Location = new System.Drawing.Point(3, 159);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.panel5.Size = new System.Drawing.Size(520, 30);
            this.panel5.TabIndex = 220;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label26);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(0, 3);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(520, 24);
            this.panel6.TabIndex = 19;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(6, 5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(128, 14);
            this.label22.TabIndex = 190;
            this.label22.Text = "Insurance Overrides";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 22);
            this.label23.TabIndex = 7;
            this.label23.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(519, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 22);
            this.label24.TabIndex = 6;
            this.label24.Text = "label3";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(520, 1);
            this.label25.TabIndex = 5;
            this.label25.Text = "label1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label26.Location = new System.Drawing.Point(0, 23);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(520, 1);
            this.label26.TabIndex = 8;
            this.label26.Text = "label2";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbPeriodDays);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.txtBillingReminder);
            this.panel4.Controls.Add(this.lblGPtriggers);
            this.panel4.Controls.Add(this.cmbGPtriggers);
            this.panel4.Controls.Add(this.lblPeriodDays);
            this.panel4.Controls.Add(this.lblBillingReminder);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(520, 156);
            this.panel4.TabIndex = 130;
            // 
            // cmbPeriodDays
            // 
            this.cmbPeriodDays.Location = new System.Drawing.Point(188, 33);
            this.cmbPeriodDays.MaxLength = 4;
            this.cmbPeriodDays.Name = "cmbPeriodDays";
            this.cmbPeriodDays.Size = new System.Drawing.Size(72, 22);
            this.cmbPeriodDays.TabIndex = 1;
            this.cmbPeriodDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPeriodDays_KeyPress);
            this.cmbPeriodDays.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbPeriodDays_MouseDown);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label20.Location = new System.Drawing.Point(1, 155);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(518, 1);
            this.label20.TabIndex = 132;
            this.label20.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(519, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 155);
            this.label18.TabIndex = 131;
            this.label18.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(519, 1);
            this.label21.TabIndex = 130;
            this.label21.Text = "label2";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 156);
            this.label9.TabIndex = 127;
            this.label9.Text = "label4";
            // 
            // txtBillingReminder
            // 
            this.txtBillingReminder.Location = new System.Drawing.Point(188, 61);
            this.txtBillingReminder.MaxLength = 255;
            this.txtBillingReminder.Multiline = true;
            this.txtBillingReminder.Name = "txtBillingReminder";
            this.txtBillingReminder.Size = new System.Drawing.Size(296, 85);
            this.txtBillingReminder.TabIndex = 2;
            // 
            // lblGPtriggers
            // 
            this.lblGPtriggers.AutoSize = true;
            this.lblGPtriggers.Location = new System.Drawing.Point(19, 11);
            this.lblGPtriggers.Name = "lblGPtriggers";
            this.lblGPtriggers.Size = new System.Drawing.Size(165, 14);
            this.lblGPtriggers.TabIndex = 0;
            this.lblGPtriggers.Text = "CPT triggers a global period :";
            // 
            // cmbGPtriggers
            // 
            this.cmbGPtriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGPtriggers.ForeColor = System.Drawing.Color.Black;
            this.cmbGPtriggers.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.cmbGPtriggers.Location = new System.Drawing.Point(188, 7);
            this.cmbGPtriggers.Name = "cmbGPtriggers";
            this.cmbGPtriggers.Size = new System.Drawing.Size(72, 22);
            this.cmbGPtriggers.TabIndex = 0;
            this.cmbGPtriggers.SelectedIndexChanged += new System.EventHandler(this.cmbGPtriggers_SelectedIndexChanged);
            // 
            // lblPeriodDays
            // 
            this.lblPeriodDays.AutoSize = true;
            this.lblPeriodDays.Location = new System.Drawing.Point(106, 37);
            this.lblPeriodDays.Name = "lblPeriodDays";
            this.lblPeriodDays.Size = new System.Drawing.Size(78, 14);
            this.lblPeriodDays.TabIndex = 122;
            this.lblPeriodDays.Text = "Period Days :";
            // 
            // lblBillingReminder
            // 
            this.lblBillingReminder.AutoSize = true;
            this.lblBillingReminder.Location = new System.Drawing.Point(85, 61);
            this.lblBillingReminder.Name = "lblBillingReminder";
            this.lblBillingReminder.Size = new System.Drawing.Size(99, 14);
            this.lblBillingReminder.TabIndex = 124;
            this.lblBillingReminder.Text = "Billing Reminder :";
            // 
            // tpAnesthesia
            // 
            this.tpAnesthesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tpAnesthesia.Controls.Add(this.panel8);
            this.tpAnesthesia.Location = new System.Drawing.Point(4, 23);
            this.tpAnesthesia.Name = "tpAnesthesia";
            this.tpAnesthesia.Padding = new System.Windows.Forms.Padding(3);
            this.tpAnesthesia.Size = new System.Drawing.Size(526, 529);
            this.tpAnesthesia.TabIndex = 2;
            this.tpAnesthesia.Text = "Anesthesia";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.chkAnesthesia);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label16);
            this.panel8.Controls.Add(this.label32);
            this.panel8.Controls.Add(this.label33);
            this.panel8.Controls.Add(this.txtBaseUnits);
            this.panel8.Controls.Add(this.label36);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(520, 523);
            this.panel8.TabIndex = 0;
            // 
            // chkAnesthesia
            // 
            this.chkAnesthesia.AutoSize = true;
            this.chkAnesthesia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkAnesthesia.Location = new System.Drawing.Point(99, 13);
            this.chkAnesthesia.Name = "chkAnesthesia";
            this.chkAnesthesia.Size = new System.Drawing.Size(85, 18);
            this.chkAnesthesia.TabIndex = 0;
            this.chkAnesthesia.Text = "Anesthesia";
            this.chkAnesthesia.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.Location = new System.Drawing.Point(1, 522);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(518, 1);
            this.label8.TabIndex = 132;
            this.label8.Text = "label2";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(519, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 522);
            this.label16.TabIndex = 131;
            this.label16.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label32.Location = new System.Drawing.Point(1, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(519, 1);
            this.label32.TabIndex = 130;
            this.label32.Text = "label2";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 523);
            this.label33.TabIndex = 127;
            this.label33.Text = "label4";
            // 
            // txtBaseUnits
            // 
            this.txtBaseUnits.Location = new System.Drawing.Point(99, 36);
            this.txtBaseUnits.MaxLength = 13;
            this.txtBaseUnits.Name = "txtBaseUnits";
            this.txtBaseUnits.Size = new System.Drawing.Size(102, 22);
            this.txtBaseUnits.TabIndex = 1;
            this.txtBaseUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBaseUnits_KeyPress);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(26, 39);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(71, 14);
            this.label36.TabIndex = 124;
            this.label36.Text = "Base Units :";
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupCPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(534, 611);
            this.Controls.Add(this.tabCPTMaster);
            this.Controls.Add(this.pnl_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupCPT";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CPT";
            this.Load += new System.EventHandler(this.frmSetupCPT_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_Toolstrip.ResumeLayout(false);
            this.pnl_Toolstrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlInactive.ResumeLayout(false);
            this.pnlInactive.PerformLayout();
            this.pnlrevenue.ResumeLayout(false);
            this.pnlrevenue.PerformLayout();
            this.pnlEPSDT.ResumeLayout(false);
            this.pnlEPSDT.PerformLayout();
            this.pnlmain.ResumeLayout(false);
            this.pnlmain.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMasters)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.tabCPTMaster.ResumeLayout(false);
            this.tpCPT.ResumeLayout(false);
            this.tpGlobalPeriods.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1GPInsurance)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tpAnesthesia.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.Label lblCPTCode;
        internal System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtCPTCode;
        internal System.Windows.Forms.TextBox txtDescription;
        internal System.Windows.Forms.ComboBox cmbCategory;
        internal System.Windows.Forms.ComboBox cmbSpecialty;
        private System.Windows.Forms.Panel pnl_Toolstrip;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox txt_Rate2;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txt_Rate1;
        private System.Windows.Forms.CheckBox cbx_IsTaxable;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox txt_Ndccode;
        private System.Windows.Forms.CheckBox cbx_IsCPTdrug;
        internal System.Windows.Forms.TextBox txt_DeafultUnits;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txt_Mod4Code;
        internal System.Windows.Forms.TextBox txt_Mod3Code;
        internal System.Windows.Forms.TextBox txt_Mod2Code;
        internal System.Windows.Forms.TextBox txt_Mod1Code;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbx_Inactive;
        internal System.Windows.Forms.TextBox txt_ClinicFee;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.TextBox txt_Charges;
        internal System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtCPTFrom;
        internal System.Windows.Forms.TextBox txtCPTTo;
        private System.Windows.Forms.DataGridView dgMasters;
        internal System.Windows.Forms.Label lblCPTFrom;
        internal System.Windows.Forms.Label lblCPTTO;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.TextBox txtStatementdesc;
        internal System.Windows.Forms.Label lblCPTStatementdesc;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label lblRevenueCode;
        internal System.Windows.Forms.ComboBox cmbRevenueCode;
        private System.Windows.Forms.Panel pnlInactive;
        private System.Windows.Forms.Panel pnlrevenue;
        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.TabControl tabCPTMaster;
        private System.Windows.Forms.TabPage tpCPT;
        private System.Windows.Forms.TabPage tpGlobalPeriods;
        private System.Windows.Forms.Label lblGPtriggers;
        internal System.Windows.Forms.ComboBox cmbGPtriggers;
        private System.Windows.Forms.Label lblPeriodDays;
        private System.Windows.Forms.Label lblBillingReminder;
        private System.Windows.Forms.TextBox txtBillingReminder;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Panel panel7;
        private C1.Win.C1FlexGrid.C1FlexGrid c1GPInsurance;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
        private System.Windows.Forms.ComboBox cmbPeriodDays;
        private System.Windows.Forms.CheckBox chkEpsdtFamPlan;
        private System.Windows.Forms.Panel pnlEPSDT;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TabPage tpAnesthesia;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox chkAnesthesia;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtBaseUnits;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblFacilityCLIANo;
        private System.Windows.Forms.TextBox txtCptCLIANo;
        private System.Windows.Forms.CheckBox chkDefaultSelf;
        private System.Windows.Forms.CheckBox chkNonServiceCode;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton rbtUnit;
        private System.Windows.Forms.RadioButton rbtVisit;
        private System.Windows.Forms.RadioButton rbtPatient;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.MaskedTextBox mskCPTInactivationDate;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.MaskedTextBox mskCPTActivationDate;
        private System.Windows.Forms.Label label34;
        internal System.Windows.Forms.TextBox txtProductCost;
        private System.Windows.Forms.CheckBox chkMammogram;
    }
}