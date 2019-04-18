namespace gloContacts
{
    partial class frmSetupHospital
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupHospital));
            this.pnlTopToolStrip = new System.Windows.Forms.Panel();
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnSave = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.txtPager = new System.Windows.Forms.TextBox();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.GBox_Companyadrs = new System.Windows.Forms.GroupBox();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.gBoxComContact = new System.Windows.Forms.GroupBox();
            this.mtxtPager = new gloMaskControl.gloMaskBox();
            this.mtxtMobile = new gloMaskControl.gloMaskBox();
            this.mtxtPhone = new gloMaskControl.gloMaskBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtFax = new gloMaskControl.gloMaskBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.GBox_GeneralInfo = new System.Windows.Forms.GroupBox();
            this.txtNPI = new System.Windows.Forms.TextBox();
            this.lblNPI = new System.Windows.Forms.Label();
            this.txtcontact = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnlTopToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.GBox_Companyadrs.SuspendLayout();
            this.gBoxComContact.SuspendLayout();
            this.GBox_GeneralInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopToolStrip
            // 
            this.pnlTopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTopToolStrip.Controls.Add(this.TopToolStrip);
            this.pnlTopToolStrip.Controls.Add(this.txtPager);
            this.pnlTopToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTopToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTopToolStrip.Name = "pnlTopToolStrip";
            this.pnlTopToolStrip.Size = new System.Drawing.Size(471, 53);
            this.pnlTopToolStrip.TabIndex = 1;
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
            this.TopToolStrip.Size = new System.Drawing.Size(471, 53);
            this.TopToolStrip.TabIndex = 0;
            this.TopToolStrip.TabStop = true;
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
            // txtPager
            // 
            this.txtPager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPager.Location = new System.Drawing.Point(295, 25);
            this.txtPager.Name = "txtPager";
            this.txtPager.Size = new System.Drawing.Size(140, 22);
            this.txtPager.TabIndex = 3;
            this.txtPager.TabStop = false;
            this.txtPager.Visible = false;
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Base.Controls.Add(this.GBox_Companyadrs);
            this.pnl_Base.Controls.Add(this.gBoxComContact);
            this.pnl_Base.Controls.Add(this.GBox_GeneralInfo);
            this.pnl_Base.Controls.Add(this.lbl_BottomBrd);
            this.pnl_Base.Controls.Add(this.lbl_LeftBrd);
            this.pnl_Base.Controls.Add(this.lbl_RightBrd);
            this.pnl_Base.Controls.Add(this.lbl_TopBrd);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 53);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_Base.Size = new System.Drawing.Size(471, 432);
            this.pnl_Base.TabIndex = 0;
            // 
            // GBox_Companyadrs
            // 
            this.GBox_Companyadrs.Controls.Add(this.pnlAddresssControl);
            this.GBox_Companyadrs.Controls.Add(this.txtState);
            this.GBox_Companyadrs.Controls.Add(this.txtZip);
            this.GBox_Companyadrs.Controls.Add(this.label38);
            this.GBox_Companyadrs.Controls.Add(this.txtAddressLine2);
            this.GBox_Companyadrs.Controls.Add(this.label30);
            this.GBox_Companyadrs.Controls.Add(this.txtCity);
            this.GBox_Companyadrs.Controls.Add(this.txtAddressLine1);
            this.GBox_Companyadrs.Controls.Add(this.label33);
            this.GBox_Companyadrs.Controls.Add(this.label32);
            this.GBox_Companyadrs.Controls.Add(this.label31);
            this.GBox_Companyadrs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBox_Companyadrs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GBox_Companyadrs.Location = new System.Drawing.Point(20, 131);
            this.GBox_Companyadrs.Name = "GBox_Companyadrs";
            this.GBox_Companyadrs.Size = new System.Drawing.Size(416, 132);
            this.GBox_Companyadrs.TabIndex = 1;
            this.GBox_Companyadrs.TabStop = false;
            this.GBox_Companyadrs.Text = "Address Information";
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(5, 21);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 105);
            this.pnlAddresssControl.TabIndex = 108;
            // 
            // txtState
            // 
            this.txtState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.ForeColor = System.Drawing.Color.Black;
            this.txtState.Location = new System.Drawing.Point(299, 81);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(81, 22);
            this.txtState.TabIndex = 4;
            this.txtState.Visible = false;
            // 
            // txtZip
            // 
            this.txtZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZip.ForeColor = System.Drawing.Color.Black;
            this.txtZip.Location = new System.Drawing.Point(101, 110);
            this.txtZip.MaxLength = 8;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(124, 22);
            this.txtZip.TabIndex = 2;
            this.txtZip.Visible = false;
            this.txtZip.Leave += new System.EventHandler(this.txtZip_Leave);
            this.txtZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(67, 114);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(31, 14);
            this.label38.TabIndex = 14;
            this.label38.Text = "Zip :";
            this.label38.Visible = false;
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressLine2.ForeColor = System.Drawing.Color.Black;
            this.txtAddressLine2.Location = new System.Drawing.Point(101, 53);
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(279, 22);
            this.txtAddressLine2.TabIndex = 1;
            this.txtAddressLine2.Visible = false;
            this.txtAddressLine2.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(12, 57);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(87, 14);
            this.label30.TabIndex = 12;
            this.label30.Text = "AddressLine2 :";
            this.label30.Visible = false;
            // 
            // txtCity
            // 
            this.txtCity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.ForeColor = System.Drawing.Color.Black;
            this.txtCity.Location = new System.Drawing.Point(101, 81);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(124, 22);
            this.txtCity.TabIndex = 3;
            this.txtCity.Visible = false;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddressLine1.ForeColor = System.Drawing.Color.Black;
            this.txtAddressLine1.Location = new System.Drawing.Point(101, 25);
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(279, 22);
            this.txtAddressLine1.TabIndex = 0;
            this.txtAddressLine1.Visible = false;
            this.txtAddressLine1.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(12, 29);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(87, 14);
            this.label33.TabIndex = 0;
            this.label33.Text = "AddressLine1 :";
            this.label33.Visible = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(64, 85);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 14);
            this.label32.TabIndex = 1;
            this.label32.Text = "City :";
            this.label32.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(251, 85);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(45, 14);
            this.label31.TabIndex = 2;
            this.label31.Text = "State :";
            // 
            // gBoxComContact
            // 
            this.gBoxComContact.Controls.Add(this.mtxtPager);
            this.gBoxComContact.Controls.Add(this.mtxtMobile);
            this.gBoxComContact.Controls.Add(this.mtxtPhone);
            this.gBoxComContact.Controls.Add(this.Label14);
            this.gBoxComContact.Controls.Add(this.txtURL);
            this.gBoxComContact.Controls.Add(this.label34);
            this.gBoxComContact.Controls.Add(this.txtEmail);
            this.gBoxComContact.Controls.Add(this.label35);
            this.gBoxComContact.Controls.Add(this.txtFax);
            this.gBoxComContact.Controls.Add(this.Label15);
            this.gBoxComContact.Controls.Add(this.label36);
            this.gBoxComContact.Controls.Add(this.label37);
            this.gBoxComContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxComContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gBoxComContact.Location = new System.Drawing.Point(19, 269);
            this.gBoxComContact.Name = "gBoxComContact";
            this.gBoxComContact.Size = new System.Drawing.Size(416, 150);
            this.gBoxComContact.TabIndex = 2;
            this.gBoxComContact.TabStop = false;
            this.gBoxComContact.Text = "Contact Information";
            // 
            // mtxtPager
            // 
            this.mtxtPager.AllowValidate = true;
            this.mtxtPager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPager.IncludeLiteralsAndPrompts = false;
            this.mtxtPager.Location = new System.Drawing.Point(286, 48);
            this.mtxtPager.MaskType = gloMaskControl.gloMaskType.Pager;
            this.mtxtPager.Name = "mtxtPager";
            this.mtxtPager.ReadOnly = false;
            this.mtxtPager.Size = new System.Drawing.Size(99, 24);
            this.mtxtPager.TabIndex = 20;
            // 
            // mtxtMobile
            // 
            this.mtxtMobile.AllowValidate = true;
            this.mtxtMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtMobile.IncludeLiteralsAndPrompts = false;
            this.mtxtMobile.Location = new System.Drawing.Point(286, 17);
            this.mtxtMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxtMobile.Name = "mtxtMobile";
            this.mtxtMobile.ReadOnly = false;
            this.mtxtMobile.Size = new System.Drawing.Size(99, 24);
            this.mtxtMobile.TabIndex = 1;
            // 
            // mtxtPhone
            // 
            this.mtxtPhone.AllowValidate = true;
            this.mtxtPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtPhone.Location = new System.Drawing.Point(84, 17);
            this.mtxtPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtPhone.Name = "mtxtPhone";
            this.mtxtPhone.ReadOnly = false;
            this.mtxtPhone.Size = new System.Drawing.Size(101, 24);
            this.mtxtPhone.TabIndex = 0;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(234, 22);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(49, 14);
            this.Label14.TabIndex = 19;
            this.Label14.Text = "Mobile :";
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.Location = new System.Drawing.Point(84, 106);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(302, 22);
            this.txtURL.TabIndex = 5;
            this.txtURL.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            this.txtURL.Validating += new System.ComponentModel.CancelEventHandler(this.txtURL_Validating);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(45, 110);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(36, 14);
            this.label34.TabIndex = 14;
            this.label34.Text = "URL :";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(84, 77);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(302, 22);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            this.txtEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmail_Validating);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(39, 81);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(42, 14);
            this.label35.TabIndex = 12;
            this.label35.Text = "Email :";
            // 
            // txtFax
            // 
            this.txtFax.AllowValidate = true;
            this.txtFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.IncludeLiteralsAndPrompts = false;
            this.txtFax.Location = new System.Drawing.Point(84, 48);
            this.txtFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtFax.Name = "txtFax";
            this.txtFax.ReadOnly = false;
            this.txtFax.Size = new System.Drawing.Size(101, 22);
            this.txtFax.TabIndex = 2;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(237, 52);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(46, 14);
            this.Label15.TabIndex = 8;
            this.Label15.Text = "Pager :";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(48, 52);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(33, 14);
            this.label36.TabIndex = 6;
            this.label36.Text = "Fax :";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(31, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(50, 14);
            this.label37.TabIndex = 0;
            this.label37.Text = "Phone :";
            // 
            // GBox_GeneralInfo
            // 
            this.GBox_GeneralInfo.Controls.Add(this.txtNPI);
            this.GBox_GeneralInfo.Controls.Add(this.lblNPI);
            this.GBox_GeneralInfo.Controls.Add(this.txtcontact);
            this.GBox_GeneralInfo.Controls.Add(this.Label3);
            this.GBox_GeneralInfo.Controls.Add(this.txtname);
            this.GBox_GeneralInfo.Controls.Add(this.Label1);
            this.GBox_GeneralInfo.Controls.Add(this.label20);
            this.GBox_GeneralInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBox_GeneralInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.GBox_GeneralInfo.Location = new System.Drawing.Point(20, 16);
            this.GBox_GeneralInfo.Name = "GBox_GeneralInfo";
            this.GBox_GeneralInfo.Size = new System.Drawing.Size(416, 109);
            this.GBox_GeneralInfo.TabIndex = 0;
            this.GBox_GeneralInfo.TabStop = false;
            this.GBox_GeneralInfo.Text = "General Information";
            // 
            // txtNPI
            // 
            this.txtNPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNPI.ForeColor = System.Drawing.Color.Black;
            this.txtNPI.Location = new System.Drawing.Point(84, 79);
            this.txtNPI.MaxLength = 12;
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new System.Drawing.Size(301, 22);
            this.txtNPI.TabIndex = 2;
            this.txtNPI.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // lblNPI
            // 
            this.lblNPI.AutoSize = true;
            this.lblNPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNPI.Location = new System.Drawing.Point(47, 82);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new System.Drawing.Size(34, 14);
            this.lblNPI.TabIndex = 2;
            this.lblNPI.Text = "NPI :";
            // 
            // txtcontact
            // 
            this.txtcontact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontact.ForeColor = System.Drawing.Color.Black;
            this.txtcontact.Location = new System.Drawing.Point(84, 49);
            this.txtcontact.Name = "txtcontact";
            this.txtcontact.Size = new System.Drawing.Size(301, 22);
            this.txtcontact.TabIndex = 1;
            this.txtcontact.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(23, 51);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(58, 14);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Contact :";
            // 
            // txtname
            // 
            this.txtname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.ForeColor = System.Drawing.Color.Black;
            this.txtname.Location = new System.Drawing.Point(84, 21);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(301, 22);
            this.txtname.TabIndex = 0;
            this.txtname.TextChanged += new System.EventHandler(this.AllControlValueChanged_Event);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(35, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(46, 14);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Name :";
            // 
            // label20
            // 
            this.label20.AutoEllipsis = true;
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(24, 26);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label20.Size = new System.Drawing.Size(14, 14);
            this.label20.TabIndex = 113;
            this.label20.Text = "*";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 428);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(463, 1);
            this.lbl_BottomBrd.TabIndex = 4;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 425);
            this.lbl_LeftBrd.TabIndex = 3;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(467, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 425);
            this.lbl_RightBrd.TabIndex = 2;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(465, 1);
            this.lbl_TopBrd.TabIndex = 0;
            this.lbl_TopBrd.Text = "label1";
            // 
            // frmSetupHospital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(471, 485);
            this.Controls.Add(this.pnl_Base);
            this.Controls.Add(this.pnlTopToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupHospital";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Hospital";
            this.Load += new System.EventHandler(this.frmSetupHospital_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupHospital_FormClosing);
            this.pnlTopToolStrip.ResumeLayout(false);
            this.pnlTopToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.pnl_Base.ResumeLayout(false);
            this.GBox_Companyadrs.ResumeLayout(false);
            this.GBox_Companyadrs.PerformLayout();
            this.gBoxComContact.ResumeLayout(false);
            this.gBoxComContact.PerformLayout();
            this.GBox_GeneralInfo.ResumeLayout(false);
            this.GBox_GeneralInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton ts_btnSave;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.GroupBox gBoxComContact;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox txtURL;
        internal System.Windows.Forms.Label label34;
        internal System.Windows.Forms.TextBox txtEmail;
        internal System.Windows.Forms.TextBox txtPager;
        internal System.Windows.Forms.Label label35;
        internal gloMaskControl.gloMaskBox  txtFax;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label label36;
        internal System.Windows.Forms.Label label37;
        internal System.Windows.Forms.GroupBox GBox_GeneralInfo;
        internal System.Windows.Forms.TextBox txtcontact;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtname;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.GroupBox GBox_Companyadrs;
        internal System.Windows.Forms.TextBox txtZip;
        internal System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox txtAddressLine2;
        internal System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtCity;
        internal System.Windows.Forms.TextBox txtAddressLine1;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label20;
        private gloMaskControl.gloMaskBox mtxtMobile;
        private gloMaskControl.gloMaskBox mtxtPhone;
        internal System.Windows.Forms.TextBox txtNPI;
        internal System.Windows.Forms.Label lblNPI;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private gloMaskControl.gloMaskBox mtxtPager;
    }
}