namespace gloBilling
{
    partial class frmSetupFacility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupFacility));
            this.tbFacilitySetup = new System.Windows.Forms.TabControl();
            this.tbPgFacility = new System.Windows.Forms.TabPage();
            this.pnl_treeview = new System.Windows.Forms.Panel();
            this.trv_Locations = new System.Windows.Forms.TreeView();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblMammogramNo = new System.Windows.Forms.Label();
            this.txtMammogramCertNo = new System.Windows.Forms.TextBox();
            this.chkReportPatientAddress = new System.Windows.Forms.CheckBox();
            this.txtFacilityCode = new System.Windows.Forms.TextBox();
            this.lblFacilityCode = new System.Windows.Forms.Label();
            this.Label74 = new System.Windows.Forms.Label();
            this.txtTaxonomy = new System.Windows.Forms.TextBox();
            this.btn_ClearCmpTaxonomy = new System.Windows.Forms.Button();
            this.btn_BrowseCmpTaxonomy = new System.Windows.Forms.Button();
            this.txtPhoneNo = new gloMaskControl.gloMaskBox();
            this.txtFax = new gloMaskControl.gloMaskBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFacilityCLIANo = new System.Windows.Forms.Label();
            this.txtFacilityCLIANo = new System.Windows.Forms.TextBox();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.cmbFacilityType = new System.Windows.Forms.ComboBox();
            this.pnlAddresControl = new System.Windows.Forms.Panel();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblTaxId = new System.Windows.Forms.Label();
            this.txtTaxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblZip = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblFacilityName = new System.Windows.Forms.Label();
            this.txtFacilityName = new System.Windows.Forms.TextBox();
            this.lblNPI = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.txtNPI = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.cmbPOS = new System.Windows.Forms.ComboBox();
            this.lblPOS = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Panel17 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mskPLFax = new gloMaskControl.gloMaskBox();
            this.mskPLPager = new gloMaskControl.gloMaskBox();
            this.Label102 = new System.Windows.Forms.Label();
            this.Label103 = new System.Windows.Forms.Label();
            this.Label104 = new System.Windows.Forms.Label();
            this.pnlPLAddresssControl = new System.Windows.Forms.Panel();
            this.Label105 = new System.Windows.Forms.Label();
            this.maskedPLPhno = new gloMaskControl.gloMaskBox();
            this.Label106 = new System.Windows.Forms.Label();
            this.txtPLContactName = new System.Windows.Forms.TextBox();
            this.Label107 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label108 = new System.Windows.Forms.Label();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Label109 = new System.Windows.Forms.Label();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.Label110 = new System.Windows.Forms.Label();
            this.TextBox5 = new System.Windows.Forms.TextBox();
            this.txtPLUrl = new System.Windows.Forms.TextBox();
            this.Label111 = new System.Windows.Forms.Label();
            this.txtPLEMail = new System.Windows.Forms.TextBox();
            this.Label112 = new System.Windows.Forms.Label();
            this.Label113 = new System.Windows.Forms.Label();
            this.Label114 = new System.Windows.Forms.Label();
            this.Label115 = new System.Windows.Forms.Label();
            this.TextBox8 = new System.Windows.Forms.TextBox();
            this.Label116 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.c1FacilityQF = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Panel18 = new System.Windows.Forms.Panel();
            this.Label99 = new System.Windows.Forms.Label();
            this.Label101 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.tbFacilitySetup.SuspendLayout();
            this.tbPgFacility.SuspendLayout();
            this.pnl_treeview.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.Panel17.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FacilityQF)).BeginInit();
            this.Panel18.SuspendLayout();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFacilitySetup
            // 
            this.tbFacilitySetup.Controls.Add(this.tbPgFacility);
            this.tbFacilitySetup.Controls.Add(this.tabPage2);
            this.tbFacilitySetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFacilitySetup.Location = new System.Drawing.Point(0, 54);
            this.tbFacilitySetup.Name = "tbFacilitySetup";
            this.tbFacilitySetup.SelectedIndex = 0;
            this.tbFacilitySetup.Size = new System.Drawing.Size(707, 504);
            this.tbFacilitySetup.TabIndex = 115;
            this.tbFacilitySetup.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbFacilitySetup_Deselecting);
            // 
            // tbPgFacility
            // 
            this.tbPgFacility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbPgFacility.Controls.Add(this.pnl_treeview);
            this.tbPgFacility.Controls.Add(this.pnlMain);
            this.tbPgFacility.Location = new System.Drawing.Point(4, 23);
            this.tbPgFacility.Name = "tbPgFacility";
            this.tbPgFacility.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgFacility.Size = new System.Drawing.Size(699, 477);
            this.tbPgFacility.TabIndex = 0;
            this.tbPgFacility.Text = "Facility";
            this.tbPgFacility.UseVisualStyleBackColor = true;
            // 
            // pnl_treeview
            // 
            this.pnl_treeview.Controls.Add(this.trv_Locations);
            this.pnl_treeview.Controls.Add(this.label8);
            this.pnl_treeview.Controls.Add(this.label7);
            this.pnl_treeview.Controls.Add(this.label2);
            this.pnl_treeview.Controls.Add(this.label3);
            this.pnl_treeview.Controls.Add(this.label4);
            this.pnl_treeview.Controls.Add(this.label5);
            this.pnl_treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_treeview.Location = new System.Drawing.Point(413, 3);
            this.pnl_treeview.Name = "pnl_treeview";
            this.pnl_treeview.Padding = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.pnl_treeview.Size = new System.Drawing.Size(283, 471);
            this.pnl_treeview.TabIndex = 2;
            this.pnl_treeview.TabStop = true;
            // 
            // trv_Locations
            // 
            this.trv_Locations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trv_Locations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_Locations.ForeColor = System.Drawing.Color.Black;
            this.trv_Locations.Indent = 20;
            this.trv_Locations.ItemHeight = 20;
            this.trv_Locations.Location = new System.Drawing.Point(5, 8);
            this.trv_Locations.Name = "trv_Locations";
            this.trv_Locations.Size = new System.Drawing.Size(274, 459);
            this.trv_Locations.TabIndex = 0;
            this.trv_Locations.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trv_Locations_AfterCheck);
            this.trv_Locations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_Locations_AfterSelect);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(274, 4);
            this.label8.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(3, 463);
            this.label7.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(2, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 1);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 464);
            this.label3.TabIndex = 7;
            this.label3.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(279, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 464);
            this.label4.TabIndex = 6;
            this.label4.Text = "label3";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(279, 1);
            this.label5.TabIndex = 5;
            this.label5.Text = "label1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblMammogramNo);
            this.pnlMain.Controls.Add(this.txtMammogramCertNo);
            this.pnlMain.Controls.Add(this.chkReportPatientAddress);
            this.pnlMain.Controls.Add(this.txtFacilityCode);
            this.pnlMain.Controls.Add(this.lblFacilityCode);
            this.pnlMain.Controls.Add(this.Label74);
            this.pnlMain.Controls.Add(this.txtTaxonomy);
            this.pnlMain.Controls.Add(this.btn_ClearCmpTaxonomy);
            this.pnlMain.Controls.Add(this.btn_BrowseCmpTaxonomy);
            this.pnlMain.Controls.Add(this.txtPhoneNo);
            this.pnlMain.Controls.Add(this.txtFax);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.lblFacilityCLIANo);
            this.pnlMain.Controls.Add(this.txtFacilityCLIANo);
            this.pnlMain.Controls.Add(this.lblFax);
            this.pnlMain.Controls.Add(this.lblPhone);
            this.pnlMain.Controls.Add(this.cmbFacilityType);
            this.pnlMain.Controls.Add(this.pnlAddresControl);
            this.pnlMain.Controls.Add(this.cmbState);
            this.pnlMain.Controls.Add(this.lblTaxId);
            this.pnlMain.Controls.Add(this.txtTaxID);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.txtAddressLine2);
            this.pnlMain.Controls.Add(this.lbl_BottomBrd);
            this.pnlMain.Controls.Add(this.lbl_LeftBrd);
            this.pnlMain.Controls.Add(this.lbl_RightBrd);
            this.pnlMain.Controls.Add(this.lbl_TopBrd);
            this.pnlMain.Controls.Add(this.txtCity);
            this.pnlMain.Controls.Add(this.lblZip);
            this.pnlMain.Controls.Add(this.lblCity);
            this.pnlMain.Controls.Add(this.lblFacilityName);
            this.pnlMain.Controls.Add(this.txtFacilityName);
            this.pnlMain.Controls.Add(this.lblNPI);
            this.pnlMain.Controls.Add(this.lblState);
            this.pnlMain.Controls.Add(this.txtNPI);
            this.pnlMain.Controls.Add(this.txtZip);
            this.pnlMain.Controls.Add(this.lblAddress);
            this.pnlMain.Controls.Add(this.cmbPOS);
            this.pnlMain.Controls.Add(this.lblPOS);
            this.pnlMain.Controls.Add(this.label19);
            this.pnlMain.Controls.Add(this.txtAddressLine1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(410, 471);
            this.pnlMain.TabIndex = 3;
            // 
            // lblMammogramNo
            // 
            this.lblMammogramNo.AutoSize = true;
            this.lblMammogramNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMammogramNo.Location = new System.Drawing.Point(26, 427);
            this.lblMammogramNo.Name = "lblMammogramNo";
            this.lblMammogramNo.Size = new System.Drawing.Size(94, 14);
            this.lblMammogramNo.TabIndex = 143;
            this.lblMammogramNo.Text = "Mamm. Cert # :";
            // 
            // txtMammogramCertNo
            // 
            this.txtMammogramCertNo.Location = new System.Drawing.Point(122, 423);
            this.txtMammogramCertNo.MaxLength = 25;
            this.txtMammogramCertNo.Name = "txtMammogramCertNo";
            this.txtMammogramCertNo.Size = new System.Drawing.Size(234, 22);
            this.txtMammogramCertNo.TabIndex = 142;
            this.txtMammogramCertNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMammogramCertNo_KeyPress);
            // 
            // chkReportPatientAddress
            // 
            this.chkReportPatientAddress.AutoSize = true;
            this.chkReportPatientAddress.Location = new System.Drawing.Point(118, 158);
            this.chkReportPatientAddress.Name = "chkReportPatientAddress";
            this.chkReportPatientAddress.Size = new System.Drawing.Size(210, 18);
            this.chkReportPatientAddress.TabIndex = 141;
            this.chkReportPatientAddress.Text = "Report Patient\'s Address on Claim";
            this.chkReportPatientAddress.UseVisualStyleBackColor = true;
            // 
            // txtFacilityCode
            // 
            this.txtFacilityCode.Location = new System.Drawing.Point(117, 481);
            this.txtFacilityCode.Name = "txtFacilityCode";
            this.txtFacilityCode.Size = new System.Drawing.Size(234, 22);
            this.txtFacilityCode.TabIndex = 16;
            this.txtFacilityCode.Visible = false;
            // 
            // lblFacilityCode
            // 
            this.lblFacilityCode.AutoSize = true;
            this.lblFacilityCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacilityCode.Location = new System.Drawing.Point(71, 485);
            this.lblFacilityCode.Name = "lblFacilityCode";
            this.lblFacilityCode.Size = new System.Drawing.Size(43, 14);
            this.lblFacilityCode.TabIndex = 13;
            this.lblFacilityCode.Text = "Code :";
            this.lblFacilityCode.Visible = false;
            // 
            // Label74
            // 
            this.Label74.AutoSize = true;
            this.Label74.BackColor = System.Drawing.Color.Transparent;
            this.Label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label74.Location = new System.Drawing.Point(42, 109);
            this.Label74.Name = "Label74";
            this.Label74.Size = new System.Drawing.Size(72, 14);
            this.Label74.TabIndex = 140;
            this.Label74.Text = "Taxonomy :";
            // 
            // txtTaxonomy
            // 
            this.txtTaxonomy.BackColor = System.Drawing.Color.White;
            this.txtTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxonomy.ForeColor = System.Drawing.Color.Black;
            this.txtTaxonomy.Location = new System.Drawing.Point(117, 105);
            this.txtTaxonomy.MaxLength = 99;
            this.txtTaxonomy.Name = "txtTaxonomy";
            this.txtTaxonomy.ReadOnly = true;
            this.txtTaxonomy.Size = new System.Drawing.Size(181, 22);
            this.txtTaxonomy.TabIndex = 17;
            // 
            // btn_ClearCmpTaxonomy
            // 
            this.btn_ClearCmpTaxonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearCmpTaxonomy.BackgroundImage")));
            this.btn_ClearCmpTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearCmpTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_ClearCmpTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearCmpTaxonomy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_ClearCmpTaxonomy.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearCmpTaxonomy.Image")));
            this.btn_ClearCmpTaxonomy.Location = new System.Drawing.Point(329, 105);
            this.btn_ClearCmpTaxonomy.Name = "btn_ClearCmpTaxonomy";
            this.btn_ClearCmpTaxonomy.Size = new System.Drawing.Size(22, 22);
            this.btn_ClearCmpTaxonomy.TabIndex = 19;
            this.btn_ClearCmpTaxonomy.UseVisualStyleBackColor = true;
            this.btn_ClearCmpTaxonomy.Click += new System.EventHandler(this.btn_ClearCmpTaxonomy_Click);
            // 
            // btn_BrowseCmpTaxonomy
            // 
            this.btn_BrowseCmpTaxonomy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_BrowseCmpTaxonomy.BackgroundImage")));
            this.btn_BrowseCmpTaxonomy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BrowseCmpTaxonomy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_BrowseCmpTaxonomy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseCmpTaxonomy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BrowseCmpTaxonomy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_BrowseCmpTaxonomy.Image = ((System.Drawing.Image)(resources.GetObject("btn_BrowseCmpTaxonomy.Image")));
            this.btn_BrowseCmpTaxonomy.Location = new System.Drawing.Point(304, 105);
            this.btn_BrowseCmpTaxonomy.Name = "btn_BrowseCmpTaxonomy";
            this.btn_BrowseCmpTaxonomy.Size = new System.Drawing.Size(22, 22);
            this.btn_BrowseCmpTaxonomy.TabIndex = 18;
            this.btn_BrowseCmpTaxonomy.UseVisualStyleBackColor = true;
            this.btn_BrowseCmpTaxonomy.Click += new System.EventHandler(this.btn_BrowseCmpTaxonomy_Click);
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.AllowValidate = true;
            this.txtPhoneNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNo.IncludeLiteralsAndPrompts = false;
            this.txtPhoneNo.Location = new System.Drawing.Point(122, 311);
            this.txtPhoneNo.MaskType = gloMaskControl.gloMaskType.Phone;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.ReadOnly = false;
            this.txtPhoneNo.Size = new System.Drawing.Size(234, 25);
            this.txtPhoneNo.TabIndex = 115;
            this.txtPhoneNo.ErrorMessageInvoked += new gloMaskControl.gloMaskBox.ErrorMessage(this.txtPhoneNo_ErrorMessageInvoked);
            // 
            // txtFax
            // 
            this.txtFax.AllowValidate = true;
            this.txtFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.IncludeLiteralsAndPrompts = false;
            this.txtFax.Location = new System.Drawing.Point(122, 340);
            this.txtFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtFax.Name = "txtFax";
            this.txtFax.ReadOnly = false;
            this.txtFax.Size = new System.Drawing.Size(234, 25);
            this.txtFax.TabIndex = 114;
            this.txtFax.ErrorMessageInvoked += new gloMaskControl.gloMaskBox.ErrorMessage(this.txtFax_ErrorMessageInvoked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(41, 370);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "Default Fee :";
            // 
            // lblFacilityCLIANo
            // 
            this.lblFacilityCLIANo.AutoSize = true;
            this.lblFacilityCLIANo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacilityCLIANo.Location = new System.Drawing.Point(60, 397);
            this.lblFacilityCLIANo.Name = "lblFacilityCLIANo";
            this.lblFacilityCLIANo.Size = new System.Drawing.Size(59, 14);
            this.lblFacilityCLIANo.TabIndex = 13;
            this.lblFacilityCLIANo.Text = "CLIA No :";
            // 
            // txtFacilityCLIANo
            // 
            this.txtFacilityCLIANo.Location = new System.Drawing.Point(122, 395);
            this.txtFacilityCLIANo.Name = "txtFacilityCLIANo";
            this.txtFacilityCLIANo.Size = new System.Drawing.Size(234, 22);
            this.txtFacilityCLIANo.TabIndex = 13;
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.BackColor = System.Drawing.Color.Transparent;
            this.lblFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFax.Location = new System.Drawing.Point(86, 343);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(33, 14);
            this.lblFax.TabIndex = 23;
            this.lblFax.Text = "Fax :";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.Transparent;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(67, 316);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(52, 14);
            this.lblPhone.TabIndex = 22;
            this.lblPhone.Text = "Ph. No :";
            // 
            // cmbFacilityType
            // 
            this.cmbFacilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFacilityType.FormattingEnabled = true;
            this.cmbFacilityType.Location = new System.Drawing.Point(122, 369);
            this.cmbFacilityType.Name = "cmbFacilityType";
            this.cmbFacilityType.Size = new System.Drawing.Size(234, 22);
            this.cmbFacilityType.TabIndex = 12;
            // 
            // pnlAddresControl
            // 
            this.pnlAddresControl.Location = new System.Drawing.Point(39, 178);
            this.pnlAddresControl.Name = "pnlAddresControl";
            this.pnlAddresControl.Size = new System.Drawing.Size(361, 136);
            this.pnlAddresControl.TabIndex = 8;
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(122, 261);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(221, 22);
            this.cmbState.TabIndex = 9;
            this.cmbState.TabStop = false;
            this.cmbState.Visible = false;
            // 
            // lblTaxId
            // 
            this.lblTaxId.AutoSize = true;
            this.lblTaxId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxId.Location = new System.Drawing.Point(63, 81);
            this.lblTaxId.Name = "lblTaxId";
            this.lblTaxId.Size = new System.Drawing.Size(51, 14);
            this.lblTaxId.TabIndex = 33;
            this.lblTaxId.Text = "Tax ID :";
            // 
            // txtTaxID
            // 
            this.txtTaxID.Location = new System.Drawing.Point(117, 77);
            this.txtTaxID.MaxLength = 9;
            this.txtTaxID.Name = "txtTaxID";
            this.txtTaxID.Size = new System.Drawing.Size(234, 22);
            this.txtTaxID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 14);
            this.label1.TabIndex = 31;
            this.label1.Text = "Address Line 2 :";
            this.label1.Visible = false;
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.Location = new System.Drawing.Point(122, 207);
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(221, 22);
            this.txtAddressLine2.TabIndex = 6;
            this.txtAddressLine2.TabStop = false;
            this.txtAddressLine2.Visible = false;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 467);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(402, 1);
            this.lbl_BottomBrd.TabIndex = 29;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 464);
            this.lbl_LeftBrd.TabIndex = 28;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(406, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 464);
            this.lbl_RightBrd.TabIndex = 27;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(404, 1);
            this.lbl_TopBrd.TabIndex = 26;
            this.lbl_TopBrd.Text = "label1";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(122, 234);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(221, 22);
            this.txtCity.TabIndex = 8;
            this.txtCity.TabStop = false;
            this.txtCity.Visible = false;
            // 
            // lblZip
            // 
            this.lblZip.AutoSize = true;
            this.lblZip.BackColor = System.Drawing.Color.Transparent;
            this.lblZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZip.Location = new System.Drawing.Point(86, 292);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(33, 14);
            this.lblZip.TabIndex = 25;
            this.lblZip.Text = "ZIP :";
            this.lblZip.Visible = false;
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.BackColor = System.Drawing.Color.Transparent;
            this.lblCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(84, 238);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 14);
            this.lblCity.TabIndex = 24;
            this.lblCity.Text = "City :";
            this.lblCity.Visible = false;
            // 
            // lblFacilityName
            // 
            this.lblFacilityName.AutoSize = true;
            this.lblFacilityName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacilityName.Location = new System.Drawing.Point(68, 25);
            this.lblFacilityName.Name = "lblFacilityName";
            this.lblFacilityName.Size = new System.Drawing.Size(46, 14);
            this.lblFacilityName.TabIndex = 14;
            this.lblFacilityName.Text = "Name :";
            // 
            // txtFacilityName
            // 
            this.txtFacilityName.Location = new System.Drawing.Point(117, 21);
            this.txtFacilityName.Name = "txtFacilityName";
            this.txtFacilityName.Size = new System.Drawing.Size(234, 22);
            this.txtFacilityName.TabIndex = 1;
            // 
            // lblNPI
            // 
            this.lblNPI.AutoSize = true;
            this.lblNPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNPI.Location = new System.Drawing.Point(80, 53);
            this.lblNPI.Name = "lblNPI";
            this.lblNPI.Size = new System.Drawing.Size(34, 14);
            this.lblNPI.TabIndex = 15;
            this.lblNPI.Text = "NPI :";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.BackColor = System.Drawing.Color.Transparent;
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(74, 265);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 14);
            this.lblState.TabIndex = 21;
            this.lblState.Text = "State :";
            this.lblState.Visible = false;
            // 
            // txtNPI
            // 
            this.txtNPI.Location = new System.Drawing.Point(117, 49);
            this.txtNPI.Name = "txtNPI";
            this.txtNPI.Size = new System.Drawing.Size(234, 22);
            this.txtNPI.TabIndex = 2;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(122, 288);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(221, 22);
            this.txtZip.TabIndex = 7;
            this.txtZip.TabStop = false;
            this.txtZip.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(24, 184);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(95, 14);
            this.lblAddress.TabIndex = 20;
            this.lblAddress.Text = "Address Line 1 :";
            this.lblAddress.Visible = false;
            // 
            // cmbPOS
            // 
            this.cmbPOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOS.FormattingEnabled = true;
            this.cmbPOS.Location = new System.Drawing.Point(117, 133);
            this.cmbPOS.Name = "cmbPOS";
            this.cmbPOS.Size = new System.Drawing.Size(234, 22);
            this.cmbPOS.TabIndex = 4;
            this.cmbPOS.SelectionChangeCommitted += new System.EventHandler(this.cmbPOS_SelectionChangeCommitted);
            // 
            // lblPOS
            // 
            this.lblPOS.AutoSize = true;
            this.lblPOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS.Location = new System.Drawing.Point(11, 137);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(103, 14);
            this.lblPOS.TabIndex = 19;
            this.lblPOS.Text = "Place Of Service :";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(56, 26);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.Location = new System.Drawing.Point(122, 180);
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(221, 22);
            this.txtAddressLine1.TabIndex = 5;
            this.txtAddressLine1.TabStop = false;
            this.txtAddressLine1.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage2.Controls.Add(this.Panel17);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(699, 477);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Billing IDs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Panel17
            // 
            this.Panel17.Controls.Add(this.panel1);
            this.Panel17.Controls.Add(this.mskPLFax);
            this.Panel17.Controls.Add(this.mskPLPager);
            this.Panel17.Controls.Add(this.Label102);
            this.Panel17.Controls.Add(this.Label103);
            this.Panel17.Controls.Add(this.Label104);
            this.Panel17.Controls.Add(this.pnlPLAddresssControl);
            this.Panel17.Controls.Add(this.Label105);
            this.Panel17.Controls.Add(this.maskedPLPhno);
            this.Panel17.Controls.Add(this.Label106);
            this.Panel17.Controls.Add(this.txtPLContactName);
            this.Panel17.Controls.Add(this.Label107);
            this.Panel17.Controls.Add(this.TextBox2);
            this.Panel17.Controls.Add(this.Label108);
            this.Panel17.Controls.Add(this.TextBox3);
            this.Panel17.Controls.Add(this.Label109);
            this.Panel17.Controls.Add(this.TextBox4);
            this.Panel17.Controls.Add(this.Label110);
            this.Panel17.Controls.Add(this.TextBox5);
            this.Panel17.Controls.Add(this.txtPLUrl);
            this.Panel17.Controls.Add(this.Label111);
            this.Panel17.Controls.Add(this.txtPLEMail);
            this.Panel17.Controls.Add(this.Label112);
            this.Panel17.Controls.Add(this.Label113);
            this.Panel17.Controls.Add(this.Label114);
            this.Panel17.Controls.Add(this.Label115);
            this.Panel17.Controls.Add(this.TextBox8);
            this.Panel17.Controls.Add(this.Label116);
            this.Panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel17.Location = new System.Drawing.Point(3, 197);
            this.Panel17.Name = "Panel17";
            this.Panel17.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Panel17.Size = new System.Drawing.Size(693, 233);
            this.Panel17.TabIndex = 221;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 23);
            this.panel1.TabIndex = 133;
            this.panel1.TabStop = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.label11.Size = new System.Drawing.Size(178, 17);
            this.label11.TabIndex = 131;
            this.label11.Text = "  Physical Location Address";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(0, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(691, 1);
            this.label12.TabIndex = 130;
            // 
            // mskPLFax
            // 
            this.mskPLFax.AllowValidate = true;
            this.mskPLFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskPLFax.IncludeLiteralsAndPrompts = false;
            this.mskPLFax.Location = new System.Drawing.Point(414, 187);
            this.mskPLFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.mskPLFax.Name = "mskPLFax";
            this.mskPLFax.ReadOnly = false;
            this.mskPLFax.Size = new System.Drawing.Size(92, 25);
            this.mskPLFax.TabIndex = 5;
            this.mskPLFax.ErrorMessageInvoked += new gloMaskControl.gloMaskBox.ErrorMessage(this.mskPLFax_ErrorMessageInvoked);
            // 
            // mskPLPager
            // 
            this.mskPLPager.AllowValidate = true;
            this.mskPLPager.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskPLPager.IncludeLiteralsAndPrompts = false;
            this.mskPLPager.Location = new System.Drawing.Point(414, 162);
            this.mskPLPager.MaskType = gloMaskControl.gloMaskType.Pager;
            this.mskPLPager.Name = "mskPLPager";
            this.mskPLPager.ReadOnly = false;
            this.mskPLPager.Size = new System.Drawing.Size(92, 25);
            this.mskPLPager.TabIndex = 3;
            this.mskPLPager.ErrorMessageInvoked += new gloMaskControl.gloMaskBox.ErrorMessage(this.mskPLPager_ErrorMessageInvoked);
            // 
            // Label102
            // 
            this.Label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label102.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label102.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label102.Location = new System.Drawing.Point(692, 4);
            this.Label102.Name = "Label102";
            this.Label102.Size = new System.Drawing.Size(1, 228);
            this.Label102.TabIndex = 127;
            // 
            // Label103
            // 
            this.Label103.AutoSize = true;
            this.Label103.BackColor = System.Drawing.Color.Transparent;
            this.Label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label103.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label103.Location = new System.Drawing.Point(363, 166);
            this.Label103.Name = "Label103";
            this.Label103.Size = new System.Drawing.Size(46, 14);
            this.Label103.TabIndex = 119;
            this.Label103.Text = "Pager :";
            // 
            // Label104
            // 
            this.Label104.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label104.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label104.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label104.Location = new System.Drawing.Point(0, 4);
            this.Label104.Name = "Label104";
            this.Label104.Size = new System.Drawing.Size(1, 228);
            this.Label104.TabIndex = 126;
            // 
            // pnlPLAddresssControl
            // 
            this.pnlPLAddresssControl.Location = new System.Drawing.Point(120, 55);
            this.pnlPLAddresssControl.Name = "pnlPLAddresssControl";
            this.pnlPLAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlPLAddresssControl.TabIndex = 2;
            this.pnlPLAddresssControl.TabStop = true;
            // 
            // Label105
            // 
            this.Label105.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label105.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label105.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label105.Location = new System.Drawing.Point(0, 232);
            this.Label105.Name = "Label105";
            this.Label105.Size = new System.Drawing.Size(693, 1);
            this.Label105.TabIndex = 125;
            // 
            // maskedPLPhno
            // 
            this.maskedPLPhno.AllowValidate = true;
            this.maskedPLPhno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedPLPhno.IncludeLiteralsAndPrompts = false;
            this.maskedPLPhno.Location = new System.Drawing.Point(202, 187);
            this.maskedPLPhno.MaskType = gloMaskControl.gloMaskType.Phone;
            this.maskedPLPhno.Name = "maskedPLPhno";
            this.maskedPLPhno.ReadOnly = false;
            this.maskedPLPhno.Size = new System.Drawing.Size(92, 25);
            this.maskedPLPhno.TabIndex = 4;
            this.maskedPLPhno.ErrorMessageInvoked += new gloMaskControl.gloMaskBox.ErrorMessage(this.maskedPLPhno_ErrorMessageInvoked);
            // 
            // Label106
            // 
            this.Label106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label106.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label106.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label106.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label106.Location = new System.Drawing.Point(0, 3);
            this.Label106.Name = "Label106";
            this.Label106.Size = new System.Drawing.Size(693, 1);
            this.Label106.TabIndex = 124;
            // 
            // txtPLContactName
            // 
            this.txtPLContactName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPLContactName.ForeColor = System.Drawing.Color.Black;
            this.txtPLContactName.Location = new System.Drawing.Point(202, 31);
            this.txtPLContactName.MaxLength = 99;
            this.txtPLContactName.Name = "txtPLContactName";
            this.txtPLContactName.Size = new System.Drawing.Size(302, 22);
            this.txtPLContactName.TabIndex = 0;
            // 
            // Label107
            // 
            this.Label107.AutoSize = true;
            this.Label107.BackColor = System.Drawing.Color.Transparent;
            this.Label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label107.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label107.Location = new System.Drawing.Point(139, 35);
            this.Label107.Name = "Label107";
            this.Label107.Size = new System.Drawing.Size(58, 14);
            this.Label107.TabIndex = 121;
            this.Label107.Text = "Contact :";
            // 
            // TextBox2
            // 
            this.TextBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox2.ForeColor = System.Drawing.Color.Black;
            this.TextBox2.Location = new System.Drawing.Point(351, 102);
            this.TextBox2.MaxLength = 10;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(22, 22);
            this.TextBox2.TabIndex = 3;
            this.TextBox2.TabStop = false;
            this.TextBox2.Visible = false;
            // 
            // Label108
            // 
            this.Label108.AutoSize = true;
            this.Label108.BackColor = System.Drawing.Color.Transparent;
            this.Label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label108.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label108.Location = new System.Drawing.Point(376, 190);
            this.Label108.Name = "Label108";
            this.Label108.Size = new System.Drawing.Size(33, 14);
            this.Label108.TabIndex = 116;
            this.Label108.Text = "Fax :";
            // 
            // TextBox3
            // 
            this.TextBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox3.ForeColor = System.Drawing.Color.Black;
            this.TextBox3.Location = new System.Drawing.Point(304, 99);
            this.TextBox3.MaxLength = 99;
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(14, 22);
            this.TextBox3.TabIndex = 4;
            this.TextBox3.TabStop = false;
            this.TextBox3.Visible = false;
            // 
            // Label109
            // 
            this.Label109.AutoSize = true;
            this.Label109.BackColor = System.Drawing.Color.Transparent;
            this.Label109.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label109.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label109.Location = new System.Drawing.Point(157, 216);
            this.Label109.Name = "Label109";
            this.Label109.Size = new System.Drawing.Size(42, 14);
            this.Label109.TabIndex = 117;
            this.Label109.Text = "Email :";
            // 
            // TextBox4
            // 
            this.TextBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox4.ForeColor = System.Drawing.Color.Black;
            this.TextBox4.Location = new System.Drawing.Point(367, 96);
            this.TextBox4.MaxLength = 99;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(13, 22);
            this.TextBox4.TabIndex = 5;
            this.TextBox4.Visible = false;
            // 
            // Label110
            // 
            this.Label110.AutoSize = true;
            this.Label110.BackColor = System.Drawing.Color.Transparent;
            this.Label110.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label110.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label110.Location = new System.Drawing.Point(130, 191);
            this.Label110.Name = "Label110";
            this.Label110.Size = new System.Drawing.Size(69, 14);
            this.Label110.TabIndex = 111;
            this.Label110.Text = "Phone No :";
            // 
            // TextBox5
            // 
            this.TextBox5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox5.ForeColor = System.Drawing.Color.Black;
            this.TextBox5.Location = new System.Drawing.Point(363, 85);
            this.TextBox5.MaxLength = 99;
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new System.Drawing.Size(10, 22);
            this.TextBox5.TabIndex = 2;
            this.TextBox5.TabStop = false;
            this.TextBox5.Visible = false;
            // 
            // txtPLUrl
            // 
            this.txtPLUrl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPLUrl.ForeColor = System.Drawing.Color.Black;
            this.txtPLUrl.Location = new System.Drawing.Point(202, 238);
            this.txtPLUrl.MaxLength = 99;
            this.txtPLUrl.Name = "txtPLUrl";
            this.txtPLUrl.Size = new System.Drawing.Size(304, 22);
            this.txtPLUrl.TabIndex = 7;
            // 
            // Label111
            // 
            this.Label111.AutoSize = true;
            this.Label111.BackColor = System.Drawing.Color.Transparent;
            this.Label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label111.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label111.Location = new System.Drawing.Point(246, 91);
            this.Label111.Name = "Label111";
            this.Label111.Size = new System.Drawing.Size(33, 14);
            this.Label111.TabIndex = 94;
            this.Label111.Text = "ZIP :";
            this.Label111.Visible = false;
            // 
            // txtPLEMail
            // 
            this.txtPLEMail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPLEMail.ForeColor = System.Drawing.Color.Black;
            this.txtPLEMail.Location = new System.Drawing.Point(202, 213);
            this.txtPLEMail.MaxLength = 99;
            this.txtPLEMail.Name = "txtPLEMail";
            this.txtPLEMail.Size = new System.Drawing.Size(304, 22);
            this.txtPLEMail.TabIndex = 6;
            // 
            // Label112
            // 
            this.Label112.AutoSize = true;
            this.Label112.BackColor = System.Drawing.Color.Transparent;
            this.Label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label112.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label112.Location = new System.Drawing.Point(301, 127);
            this.Label112.Name = "Label112";
            this.Label112.Size = new System.Drawing.Size(45, 14);
            this.Label112.TabIndex = 93;
            this.Label112.Text = "State :";
            this.Label112.Visible = false;
            // 
            // Label113
            // 
            this.Label113.AutoSize = true;
            this.Label113.BackColor = System.Drawing.Color.Transparent;
            this.Label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label113.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label113.Location = new System.Drawing.Point(163, 241);
            this.Label113.Name = "Label113";
            this.Label113.Size = new System.Drawing.Size(36, 14);
            this.Label113.TabIndex = 118;
            this.Label113.Text = "URL :";
            // 
            // Label114
            // 
            this.Label114.AutoSize = true;
            this.Label114.BackColor = System.Drawing.Color.Transparent;
            this.Label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label114.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label114.Location = new System.Drawing.Point(266, 102);
            this.Label114.Name = "Label114";
            this.Label114.Size = new System.Drawing.Size(35, 14);
            this.Label114.TabIndex = 92;
            this.Label114.Text = "City :";
            this.Label114.Visible = false;
            // 
            // Label115
            // 
            this.Label115.AutoSize = true;
            this.Label115.BackColor = System.Drawing.Color.Transparent;
            this.Label115.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label115.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label115.Location = new System.Drawing.Point(292, 88);
            this.Label115.Name = "Label115";
            this.Label115.Size = new System.Drawing.Size(65, 14);
            this.Label115.TabIndex = 91;
            this.Label115.Text = "Address2 :";
            this.Label115.Visible = false;
            // 
            // TextBox8
            // 
            this.TextBox8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox8.ForeColor = System.Drawing.Color.Black;
            this.TextBox8.Location = new System.Drawing.Point(189, 69);
            this.TextBox8.MaxLength = 99;
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new System.Drawing.Size(47, 22);
            this.TextBox8.TabIndex = 1;
            this.TextBox8.TabStop = false;
            this.TextBox8.Visible = false;
            // 
            // Label116
            // 
            this.Label116.AutoSize = true;
            this.Label116.BackColor = System.Drawing.Color.Transparent;
            this.Label116.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label116.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label116.Location = new System.Drawing.Point(234, 72);
            this.Label116.Name = "Label116";
            this.Label116.Size = new System.Drawing.Size(65, 14);
            this.Label116.TabIndex = 90;
            this.Label116.Text = "Address1 :";
            this.Label116.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.c1FacilityQF);
            this.panel4.Controls.Add(this.Panel18);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(693, 194);
            this.panel4.TabIndex = 220;
            // 
            // c1FacilityQF
            // 
            this.c1FacilityQF.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FacilityQF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FacilityQF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1FacilityQF.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FacilityQF.ColumnInfo = "0,0,0,0,0,110,Columns:";
            this.c1FacilityQF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FacilityQF.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FacilityQF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FacilityQF.Location = new System.Drawing.Point(1, 24);
            this.c1FacilityQF.Name = "c1FacilityQF";
            this.c1FacilityQF.Rows.Count = 0;
            this.c1FacilityQF.Rows.DefaultSize = 22;
            this.c1FacilityQF.Rows.Fixed = 0;
            this.c1FacilityQF.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FacilityQF.Size = new System.Drawing.Size(691, 169);
            this.c1FacilityQF.StyleInfo = resources.GetString("c1FacilityQF.StyleInfo");
            this.c1FacilityQF.TabIndex = 8;
            this.c1FacilityQF.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FacilityQF_StartEdit);
            this.c1FacilityQF.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FacilityQF_SetupEditor);
            this.c1FacilityQF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1FacilityQF_MouseMove);
            // 
            // Panel18
            // 
            this.Panel18.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel18.Controls.Add(this.Label99);
            this.Panel18.Controls.Add(this.Label101);
            this.Panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel18.Location = new System.Drawing.Point(1, 1);
            this.Panel18.Name = "Panel18";
            this.Panel18.Size = new System.Drawing.Size(691, 23);
            this.Panel18.TabIndex = 134;
            this.Panel18.TabStop = true;
            // 
            // Label99
            // 
            this.Label99.AutoSize = true;
            this.Label99.BackColor = System.Drawing.Color.Transparent;
            this.Label99.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label99.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label99.Location = new System.Drawing.Point(0, 0);
            this.Label99.Name = "Label99";
            this.Label99.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.Label99.Size = new System.Drawing.Size(147, 17);
            this.Label99.TabIndex = 131;
            this.Label99.Text = "  Additional Billing IDs";
            // 
            // Label101
            // 
            this.Label101.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label101.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label101.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label101.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label101.Location = new System.Drawing.Point(0, 22);
            this.Label101.Name = "Label101";
            this.Label101.Size = new System.Drawing.Size(691, 1);
            this.Label101.TabIndex = 130;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 192);
            this.label17.TabIndex = 7;
            this.label17.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label18.Location = new System.Drawing.Point(692, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 192);
            this.label18.TabIndex = 6;
            this.label18.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label20.Location = new System.Drawing.Point(0, 193);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(693, 1);
            this.label20.TabIndex = 8;
            this.label20.Text = "label2";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(693, 1);
            this.label15.TabIndex = 5;
            this.label15.Text = "label1";
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnltlsStrip.BackgroundImage")));
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(707, 54);
            this.pnltlsStrip.TabIndex = 116;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.toolStripButton1,
            this.toolStripButton2});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(707, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            this.tls_SetupResource.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SetupResource_ItemClicked);
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
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 50);
            this.toolStripButton1.Tag = "OK";
            this.toolStripButton1.Text = "Sa&ve&&Cls";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Save and Close";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupFacility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(707, 558);
            this.Controls.Add(this.tbFacilitySetup);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupFacility";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facility";
            this.Load += new System.EventHandler(this.frmSetupFacility_Load);
            this.tbFacilitySetup.ResumeLayout(false);
            this.tbPgFacility.ResumeLayout(false);
            this.pnl_treeview.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.Panel17.ResumeLayout(false);
            this.Panel17.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FacilityQF)).EndInit();
            this.Panel18.ResumeLayout(false);
            this.Panel18.PerformLayout();
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbFacilitySetup;
        private System.Windows.Forms.TabPage tbPgFacility;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnl_treeview;
        private System.Windows.Forms.TreeView trv_Locations;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlAddresControl;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label lblTaxId;
        private System.Windows.Forms.TextBox txtTaxID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddressLine2;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFacilityCLIANo;
        private System.Windows.Forms.Label lblFacilityCode;
        internal System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.TextBox txtFacilityCLIANo;
        private System.Windows.Forms.TextBox txtFacilityCode;
        internal System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblFacilityName;
        internal System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.TextBox txtFacilityName;
        internal System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblNPI;
        internal System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtNPI;
        internal System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.ComboBox cmbFacilityType;
        private System.Windows.Forms.ComboBox cmbPOS;
        private System.Windows.Forms.Label lblPOS;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtAddressLine1;
        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FacilityQF;
        internal System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Panel Panel18;
        internal System.Windows.Forms.Label Label99;
        internal System.Windows.Forms.Label Label101;
        internal System.Windows.Forms.Panel Panel17;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label12;
        internal gloMaskControl.gloMaskBox mskPLFax;
        internal gloMaskControl.gloMaskBox mskPLPager;
        internal System.Windows.Forms.Label Label102;
        internal System.Windows.Forms.Label Label103;
        internal System.Windows.Forms.Label Label104;
        internal System.Windows.Forms.Panel pnlPLAddresssControl;
        internal System.Windows.Forms.Label Label105;
        internal gloMaskControl.gloMaskBox maskedPLPhno;
        internal System.Windows.Forms.Label Label106;
        internal System.Windows.Forms.TextBox txtPLContactName;
        internal System.Windows.Forms.Label Label107;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Label Label108;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.Label Label109;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.Label Label110;
        internal System.Windows.Forms.TextBox TextBox5;
        internal System.Windows.Forms.TextBox txtPLUrl;
        internal System.Windows.Forms.Label Label111;
        internal System.Windows.Forms.TextBox txtPLEMail;
        internal System.Windows.Forms.Label Label112;
        internal System.Windows.Forms.Label Label113;
        internal System.Windows.Forms.Label Label114;
        internal System.Windows.Forms.Label Label115;
        internal System.Windows.Forms.TextBox TextBox8;
        internal System.Windows.Forms.Label Label116;
        internal gloMaskControl.gloMaskBox txtFax;
        internal gloMaskControl.gloMaskBox txtPhoneNo;
        internal System.Windows.Forms.Label Label74;
        internal System.Windows.Forms.TextBox txtTaxonomy;
        private System.Windows.Forms.Button btn_ClearCmpTaxonomy;
        private System.Windows.Forms.Button btn_BrowseCmpTaxonomy;
        private System.Windows.Forms.CheckBox chkReportPatientAddress;
        private System.Windows.Forms.Label lblMammogramNo;
        private System.Windows.Forms.TextBox txtMammogramCertNo;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}