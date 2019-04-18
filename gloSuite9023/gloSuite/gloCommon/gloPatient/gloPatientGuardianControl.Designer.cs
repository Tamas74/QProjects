namespace gloPatient
{
    partial class gloPatientGuardianControl
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
               // _PatientGuardian.Dispose();
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
                }
            }
            base.Dispose(disposing);
        }
       

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientGuardianControl));
            this.pnlGuardianInfo = new System.Windows.Forms.Panel();
            this.pnlGaurInfo = new System.Windows.Forms.Panel();
            this.cmbGauInfoRelation = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlGuaiAdds = new System.Windows.Forms.Panel();
            this.cmbGauInfoCountry = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbGauInfoState = new System.Windows.Forms.ComboBox();
            this.mskGauInfoMobile = new gloMaskControl.gloMaskBox();
            this.mskGauInfoPhone = new gloMaskControl.gloMaskBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGauInfoFax = new gloMaskControl.gloMaskBox();
            this.cb_AddrforGuardian = new System.Windows.Forms.CheckBox();
            this.txtGauInfoMName = new System.Windows.Forms.TextBox();
            this.txtGauInfoAddress2 = new System.Windows.Forms.TextBox();
            this.txtGauInfoCounty = new System.Windows.Forms.TextBox();
            this.lblGauInfoPhone = new System.Windows.Forms.Label();
            this.txtGauInfoZip = new System.Windows.Forms.TextBox();
            this.lblGauInfoMobile = new System.Windows.Forms.Label();
            this.lblGauInfoFax = new System.Windows.Forms.Label();
            this.txtGauInfoEmail = new System.Windows.Forms.TextBox();
            this.txtGauInfoCity = new System.Windows.Forms.TextBox();
            this.txtGauInfoAddress1 = new System.Windows.Forms.TextBox();
            this.txtGauInfoLName = new System.Windows.Forms.TextBox();
            this.txtGauInfoFName = new System.Windows.Forms.TextBox();
            this.lblGauInfoAddress2 = new System.Windows.Forms.Label();
            this.lblGauInfoCounty = new System.Windows.Forms.Label();
            this.lblGauInfoZip = new System.Windows.Forms.Label();
            this.lblGauInfoState = new System.Windows.Forms.Label();
            this.lblGauInoEmail = new System.Windows.Forms.Label();
            this.lblGauInfoCity = new System.Windows.Forms.Label();
            this.lblGauInfoAddress1 = new System.Windows.Forms.Label();
            this.lblGauInfoFName = new System.Windows.Forms.Label();
            this.lblGauIGaurdInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlFathersInfo = new System.Windows.Forms.Panel();
            this.pnlFthersAdds = new System.Windows.Forms.Panel();
            this.cmbFCountry = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFState = new System.Windows.Forms.ComboBox();
            this.mtxtFatherMobile = new gloMaskControl.gloMaskBox();
            this.mskGauIFatherPhone = new gloMaskControl.gloMaskBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtGauIFatherFax = new gloMaskControl.gloMaskBox();
            this.chkGauIFatherAddress = new System.Windows.Forms.CheckBox();
            this.txtGauIFatherMName = new System.Windows.Forms.TextBox();
            this.txtGauIFatherAddress2 = new System.Windows.Forms.TextBox();
            this.txtGauIFatherCounty = new System.Windows.Forms.TextBox();
            this.lblGauIFathetPhone = new System.Windows.Forms.Label();
            this.txtGauIFatherZip = new System.Windows.Forms.TextBox();
            this.lblGauIFatherMobile = new System.Windows.Forms.Label();
            this.lblGauIFatherFax = new System.Windows.Forms.Label();
            this.txtGauIFatherEmail = new System.Windows.Forms.TextBox();
            this.txtGauIFatherCity = new System.Windows.Forms.TextBox();
            this.txtGauIFatherAddress1 = new System.Windows.Forms.TextBox();
            this.txtGauIFatherLName = new System.Windows.Forms.TextBox();
            this.txtGauIFatherfName = new System.Windows.Forms.TextBox();
            this.lblGauIFatherAddress2 = new System.Windows.Forms.Label();
            this.lblGauIFatherCounty = new System.Windows.Forms.Label();
            this.lblGauIFatherZip = new System.Windows.Forms.Label();
            this.lblGIFatherState = new System.Windows.Forms.Label();
            this.lblGauIFatherEmail = new System.Windows.Forms.Label();
            this.lblGauIFatherCity = new System.Windows.Forms.Label();
            this.lblGauIFatherAdd1 = new System.Windows.Forms.Label();
            this.lblGauIFatherFName = new System.Windows.Forms.Label();
            this.lblGauIFatherInfo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlMotherInfo = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtGauIMotherMaidenMName = new System.Windows.Forms.TextBox();
            this.txtGauIMotherMaidenLName = new System.Windows.Forms.TextBox();
            this.txtGauIMotherMaidenFName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlMthersAdds = new System.Windows.Forms.Panel();
            this.cmbGauIMCountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbGauIMState = new System.Windows.Forms.ComboBox();
            this.mskGauIMMobile = new gloMaskControl.gloMaskBox();
            this.mskGauIMPhone = new gloMaskControl.gloMaskBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtGauIMFax = new gloMaskControl.gloMaskBox();
            this.chkGauIAddrforMother = new System.Windows.Forms.CheckBox();
            this.txtGauIMotherMName = new System.Windows.Forms.TextBox();
            this.txtGauIMotherAddress2 = new System.Windows.Forms.TextBox();
            this.txtGauIMCounty = new System.Windows.Forms.TextBox();
            this.lblGauIMPhone = new System.Windows.Forms.Label();
            this.txtGauIMotherZip = new System.Windows.Forms.TextBox();
            this.lblGauIMMobile = new System.Windows.Forms.Label();
            this.lblGauIMFax = new System.Windows.Forms.Label();
            this.txtGauiMEmail = new System.Windows.Forms.TextBox();
            this.txtGauIMotherCity = new System.Windows.Forms.TextBox();
            this.txtGauIMotherAddress1 = new System.Windows.Forms.TextBox();
            this.txtGauIMotherLName = new System.Windows.Forms.TextBox();
            this.txtGauIMotherfName = new System.Windows.Forms.TextBox();
            this.lblGauIAddressLine2 = new System.Windows.Forms.Label();
            this.lblGauICounty = new System.Windows.Forms.Label();
            this.lblGauIMZip = new System.Windows.Forms.Label();
            this.lblGauIState = new System.Windows.Forms.Label();
            this.lblGauiMEmail = new System.Windows.Forms.Label();
            this.lblGauICity = new System.Windows.Forms.Label();
            this.lblGauIAddressLine1 = new System.Windows.Forms.Label();
            this.lblGauIFName = new System.Windows.Forms.Label();
            this.lblGauIMotherInfoHeader = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.pnlFInternalControl = new System.Windows.Forms.Panel();
            this.pnlMInternalControl = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGI = new System.Windows.Forms.Panel();
            this.pnlGInternalControl = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblGaurIHeader = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label23 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.pnlGuardianInfo.SuspendLayout();
            this.pnlGaurInfo.SuspendLayout();
            this.pnlFathersInfo.SuspendLayout();
            this.pnlMotherInfo.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlGI.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGuardianInfo
            // 
            this.pnlGuardianInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlGuardianInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGuardianInfo.Controls.Add(this.pnlGaurInfo);
            this.pnlGuardianInfo.Controls.Add(this.pnlFathersInfo);
            this.pnlGuardianInfo.Controls.Add(this.pnlMotherInfo);
            this.pnlGuardianInfo.Controls.Add(this.pnlTOP);
            this.pnlGuardianInfo.Controls.Add(this.panel1);
            this.pnlGuardianInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGuardianInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGuardianInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlGuardianInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlGuardianInfo.Name = "pnlGuardianInfo";
            this.pnlGuardianInfo.Size = new System.Drawing.Size(750, 773);
            this.pnlGuardianInfo.TabIndex = 13;
            // 
            // pnlGaurInfo
            // 
            this.pnlGaurInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlGaurInfo.Controls.Add(this.label20);
            this.pnlGaurInfo.Controls.Add(this.cmbGauInfoRelation);
            this.pnlGaurInfo.Controls.Add(this.label21);
            this.pnlGaurInfo.Controls.Add(this.label15);
            this.pnlGaurInfo.Controls.Add(this.label32);
            this.pnlGaurInfo.Controls.Add(this.label33);
            this.pnlGaurInfo.Controls.Add(this.pnlGuaiAdds);
            this.pnlGaurInfo.Controls.Add(this.label34);
            this.pnlGaurInfo.Controls.Add(this.cmbGauInfoCountry);
            this.pnlGaurInfo.Controls.Add(this.label10);
            this.pnlGaurInfo.Controls.Add(this.cmbGauInfoState);
            this.pnlGaurInfo.Controls.Add(this.mskGauInfoMobile);
            this.pnlGaurInfo.Controls.Add(this.mskGauInfoPhone);
            this.pnlGaurInfo.Controls.Add(this.label3);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoFax);
            this.pnlGaurInfo.Controls.Add(this.cb_AddrforGuardian);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoMName);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoAddress2);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoCounty);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoPhone);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoZip);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoMobile);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoFax);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoEmail);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoCity);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoAddress1);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoLName);
            this.pnlGaurInfo.Controls.Add(this.txtGauInfoFName);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoAddress2);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoCounty);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoZip);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoState);
            this.pnlGaurInfo.Controls.Add(this.lblGauInoEmail);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoCity);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoAddress1);
            this.pnlGaurInfo.Controls.Add(this.lblGauInfoFName);
            this.pnlGaurInfo.Controls.Add(this.lblGauIGaurdInfo);
            this.pnlGaurInfo.Controls.Add(this.label6);
            this.pnlGaurInfo.Controls.Add(this.label1);
            this.pnlGaurInfo.Controls.Add(this.label2);
            this.pnlGaurInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGaurInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGaurInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlGaurInfo.Location = new System.Drawing.Point(0, 530);
            this.pnlGaurInfo.Name = "pnlGaurInfo";
            this.pnlGaurInfo.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlGaurInfo.Size = new System.Drawing.Size(750, 243);
            this.pnlGaurInfo.TabIndex = 3;
            // 
            // cmbGauInfoRelation
            // 
            this.cmbGauInfoRelation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGauInfoRelation.ForeColor = System.Drawing.Color.Black;
            this.cmbGauInfoRelation.FormattingEnabled = true;
            this.cmbGauInfoRelation.Location = new System.Drawing.Point(105, 63);
            this.cmbGauInfoRelation.Name = "cmbGauInfoRelation";
            this.cmbGauInfoRelation.Size = new System.Drawing.Size(240, 22);
            this.cmbGauInfoRelation.TabIndex = 34;
            this.cmbGauInfoRelation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGauInfoRelation_KeyDown);
            this.cmbGauInfoRelation.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbGauInfoRelation_PreviewKeyDown);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(46, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 14);
            this.label15.TabIndex = 35;
            this.label15.Text = "Relation :";
            // 
            // pnlGuaiAdds
            // 
            this.pnlGuaiAdds.Location = new System.Drawing.Point(24, 88);
            this.pnlGuaiAdds.Name = "pnlGuaiAdds";
            this.pnlGuaiAdds.Size = new System.Drawing.Size(325, 132);
            this.pnlGuaiAdds.TabIndex = 4;
            // 
            // cmbGauInfoCountry
            // 
            this.cmbGauInfoCountry.FormattingEnabled = true;
            this.cmbGauInfoCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbGauInfoCountry.Location = new System.Drawing.Point(105, 186);
            this.cmbGauInfoCountry.MaxDropDownItems = 3;
            this.cmbGauInfoCountry.MaxLength = 20;
            this.cmbGauInfoCountry.Name = "cmbGauInfoCountry";
            this.cmbGauInfoCountry.Size = new System.Drawing.Size(130, 22);
            this.cmbGauInfoCountry.TabIndex = 10;
            this.cmbGauInfoCountry.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(47, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 14);
            this.label10.TabIndex = 31;
            this.label10.Text = "Country :";
            this.label10.Visible = false;
            // 
            // cmbGauInfoState
            // 
            this.cmbGauInfoState.FormattingEnabled = true;
            this.cmbGauInfoState.Location = new System.Drawing.Point(289, 137);
            this.cmbGauInfoState.MaxLength = 20;
            this.cmbGauInfoState.Name = "cmbGauInfoState";
            this.cmbGauInfoState.Size = new System.Drawing.Size(61, 22);
            this.cmbGauInfoState.TabIndex = 8;
            this.cmbGauInfoState.Visible = false;
            // 
            // mskGauInfoMobile
            // 
            this.mskGauInfoMobile.AllowValidate = true;
            this.mskGauInfoMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGauInfoMobile.IncludeLiteralsAndPrompts = false;
            this.mskGauInfoMobile.Location = new System.Drawing.Point(448, 110);
            this.mskGauInfoMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mskGauInfoMobile.Name = "mskGauInfoMobile";
            this.mskGauInfoMobile.ReadOnly = false;
            this.mskGauInfoMobile.Size = new System.Drawing.Size(193, 22);
            this.mskGauInfoMobile.TabIndex = 12;
            // 
            // mskGauInfoPhone
            // 
            this.mskGauInfoPhone.AllowValidate = true;
            this.mskGauInfoPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGauInfoPhone.IncludeLiteralsAndPrompts = false;
            this.mskGauInfoPhone.Location = new System.Drawing.Point(448, 85);
            this.mskGauInfoPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskGauInfoPhone.Name = "mskGauInfoPhone";
            this.mskGauInfoPhone.ReadOnly = false;
            this.mskGauInfoPhone.Size = new System.Drawing.Size(193, 22);
            this.mskGauInfoPhone.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(742, 1);
            this.label3.TabIndex = 30;
            // 
            // txtGauInfoFax
            // 
            this.txtGauInfoFax.AllowValidate = true;
            this.txtGauInfoFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoFax.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoFax.IncludeLiteralsAndPrompts = false;
            this.txtGauInfoFax.Location = new System.Drawing.Point(448, 135);
            this.txtGauInfoFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtGauInfoFax.Name = "txtGauInfoFax";
            this.txtGauInfoFax.ReadOnly = false;
            this.txtGauInfoFax.Size = new System.Drawing.Size(193, 22);
            this.txtGauInfoFax.TabIndex = 13;
            // 
            // cb_AddrforGuardian
            // 
            this.cb_AddrforGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_AddrforGuardian.AutoSize = true;
            this.cb_AddrforGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_AddrforGuardian.Location = new System.Drawing.Point(355, 65);
            this.cb_AddrforGuardian.Name = "cb_AddrforGuardian";
            this.cb_AddrforGuardian.Size = new System.Drawing.Size(161, 18);
            this.cb_AddrforGuardian.TabIndex = 3;
            this.cb_AddrforGuardian.Text = "Same as Patient Address";
            this.cb_AddrforGuardian.UseVisualStyleBackColor = true;
            this.cb_AddrforGuardian.CheckedChanged += new System.EventHandler(this.cb_AddrforGuardian_CheckedChanged);
            // 
            // txtGauInfoMName
            // 
            this.txtGauInfoMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoMName.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoMName.Location = new System.Drawing.Point(349, 24);
            this.txtGauInfoMName.MaxLength = 2;
            this.txtGauInfoMName.Name = "txtGauInfoMName";
            this.txtGauInfoMName.Size = new System.Drawing.Size(92, 22);
            this.txtGauInfoMName.TabIndex = 1;
            // 
            // txtGauInfoAddress2
            // 
            this.txtGauInfoAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoAddress2.Location = new System.Drawing.Point(105, 113);
            this.txtGauInfoAddress2.Name = "txtGauInfoAddress2";
            this.txtGauInfoAddress2.Size = new System.Drawing.Size(245, 22);
            this.txtGauInfoAddress2.TabIndex = 5;
            this.txtGauInfoAddress2.Visible = false;
            // 
            // txtGauInfoCounty
            // 
            this.txtGauInfoCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoCounty.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoCounty.Location = new System.Drawing.Point(223, 161);
            this.txtGauInfoCounty.Name = "txtGauInfoCounty";
            this.txtGauInfoCounty.Size = new System.Drawing.Size(127, 22);
            this.txtGauInfoCounty.TabIndex = 9;
            this.txtGauInfoCounty.Visible = false;
            // 
            // lblGauInfoPhone
            // 
            this.lblGauInfoPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoPhone.AutoEllipsis = true;
            this.lblGauInfoPhone.AutoSize = true;
            this.lblGauInfoPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoPhone.Location = new System.Drawing.Point(396, 92);
            this.lblGauInfoPhone.Name = "lblGauInfoPhone";
            this.lblGauInfoPhone.Size = new System.Drawing.Size(50, 14);
            this.lblGauInfoPhone.TabIndex = 21;
            this.lblGauInfoPhone.Text = "Phone :";
            // 
            // txtGauInfoZip
            // 
            this.txtGauInfoZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoZip.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoZip.Location = new System.Drawing.Point(105, 161);
            this.txtGauInfoZip.MaxLength = 8;
            this.txtGauInfoZip.Name = "txtGauInfoZip";
            this.txtGauInfoZip.Size = new System.Drawing.Size(59, 22);
            this.txtGauInfoZip.TabIndex = 6;
            this.txtGauInfoZip.Visible = false;
            this.txtGauInfoZip.TextChanged += new System.EventHandler(this.GuardianZip_TextChanged);
            this.txtGauInfoZip.Enter += new System.EventHandler(this.GuardianZip_GotFocus);
            this.txtGauInfoZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GuardianZip_KeyDown);
            this.txtGauInfoZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GuardianZip_KeyPress);
            this.txtGauInfoZip.Leave += new System.EventHandler(this.GuardianZip_LostFocus);
            // 
            // lblGauInfoMobile
            // 
            this.lblGauInfoMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoMobile.AutoEllipsis = true;
            this.lblGauInfoMobile.AutoSize = true;
            this.lblGauInfoMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoMobile.Location = new System.Drawing.Point(397, 116);
            this.lblGauInfoMobile.Name = "lblGauInfoMobile";
            this.lblGauInfoMobile.Size = new System.Drawing.Size(49, 14);
            this.lblGauInfoMobile.TabIndex = 23;
            this.lblGauInfoMobile.Text = "Mobile :";
            // 
            // lblGauInfoFax
            // 
            this.lblGauInfoFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoFax.AutoEllipsis = true;
            this.lblGauInfoFax.AutoSize = true;
            this.lblGauInfoFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoFax.Location = new System.Drawing.Point(413, 140);
            this.lblGauInfoFax.Name = "lblGauInfoFax";
            this.lblGauInfoFax.Size = new System.Drawing.Size(33, 14);
            this.lblGauInfoFax.TabIndex = 25;
            this.lblGauInfoFax.Text = "Fax :";
            // 
            // txtGauInfoEmail
            // 
            this.txtGauInfoEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoEmail.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoEmail.Location = new System.Drawing.Point(448, 160);
            this.txtGauInfoEmail.Name = "txtGauInfoEmail";
            this.txtGauInfoEmail.Size = new System.Drawing.Size(193, 22);
            this.txtGauInfoEmail.TabIndex = 14;
            this.txtGauInfoEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtGauInfoEmail_Validating);
            // 
            // txtGauInfoCity
            // 
            this.txtGauInfoCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoCity.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoCity.Location = new System.Drawing.Point(105, 137);
            this.txtGauInfoCity.Name = "txtGauInfoCity";
            this.txtGauInfoCity.Size = new System.Drawing.Size(135, 22);
            this.txtGauInfoCity.TabIndex = 7;
            this.txtGauInfoCity.Visible = false;
            // 
            // txtGauInfoAddress1
            // 
            this.txtGauInfoAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoAddress1.Location = new System.Drawing.Point(105, 89);
            this.txtGauInfoAddress1.Name = "txtGauInfoAddress1";
            this.txtGauInfoAddress1.Size = new System.Drawing.Size(245, 22);
            this.txtGauInfoAddress1.TabIndex = 4;
            this.txtGauInfoAddress1.Visible = false;
            // 
            // txtGauInfoLName
            // 
            this.txtGauInfoLName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtGauInfoLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoLName.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoLName.Location = new System.Drawing.Point(445, 24);
            this.txtGauInfoLName.Name = "txtGauInfoLName";
            this.txtGauInfoLName.Size = new System.Drawing.Size(193, 22);
            this.txtGauInfoLName.TabIndex = 2;
            // 
            // txtGauInfoFName
            // 
            this.txtGauInfoFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauInfoFName.ForeColor = System.Drawing.Color.Black;
            this.txtGauInfoFName.Location = new System.Drawing.Point(105, 24);
            this.txtGauInfoFName.Name = "txtGauInfoFName";
            this.txtGauInfoFName.Size = new System.Drawing.Size(240, 22);
            this.txtGauInfoFName.TabIndex = 0;
            // 
            // lblGauInfoAddress2
            // 
            this.lblGauInfoAddress2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoAddress2.AutoEllipsis = true;
            this.lblGauInfoAddress2.AutoSize = true;
            this.lblGauInfoAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoAddress2.Location = new System.Drawing.Point(8, 117);
            this.lblGauInfoAddress2.Name = "lblGauInfoAddress2";
            this.lblGauInfoAddress2.Size = new System.Drawing.Size(95, 14);
            this.lblGauInfoAddress2.TabIndex = 9;
            this.lblGauInfoAddress2.Text = "Address Line 2 :";
            this.lblGauInfoAddress2.Visible = false;
            // 
            // lblGauInfoCounty
            // 
            this.lblGauInfoCounty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoCounty.AutoEllipsis = true;
            this.lblGauInfoCounty.AutoSize = true;
            this.lblGauInfoCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoCounty.Location = new System.Drawing.Point(167, 165);
            this.lblGauInfoCounty.Name = "lblGauInfoCounty";
            this.lblGauInfoCounty.Size = new System.Drawing.Size(54, 14);
            this.lblGauInfoCounty.TabIndex = 17;
            this.lblGauInfoCounty.Text = "County :";
            this.lblGauInfoCounty.Visible = false;
            // 
            // lblGauInfoZip
            // 
            this.lblGauInfoZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoZip.AutoEllipsis = true;
            this.lblGauInfoZip.AutoSize = true;
            this.lblGauInfoZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoZip.Location = new System.Drawing.Point(72, 165);
            this.lblGauInfoZip.Name = "lblGauInfoZip";
            this.lblGauInfoZip.Size = new System.Drawing.Size(31, 14);
            this.lblGauInfoZip.TabIndex = 15;
            this.lblGauInfoZip.Text = "Zip :";
            this.lblGauInfoZip.Visible = false;
            // 
            // lblGauInfoState
            // 
            this.lblGauInfoState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoState.AutoEllipsis = true;
            this.lblGauInfoState.AutoSize = true;
            this.lblGauInfoState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoState.Location = new System.Drawing.Point(244, 141);
            this.lblGauInfoState.Name = "lblGauInfoState";
            this.lblGauInfoState.Size = new System.Drawing.Size(45, 14);
            this.lblGauInfoState.TabIndex = 13;
            this.lblGauInfoState.Text = "State :";
            this.lblGauInfoState.Visible = false;
            // 
            // lblGauInoEmail
            // 
            this.lblGauInoEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInoEmail.AutoEllipsis = true;
            this.lblGauInoEmail.AutoSize = true;
            this.lblGauInoEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInoEmail.Location = new System.Drawing.Point(404, 164);
            this.lblGauInoEmail.Name = "lblGauInoEmail";
            this.lblGauInoEmail.Size = new System.Drawing.Size(42, 14);
            this.lblGauInoEmail.TabIndex = 19;
            this.lblGauInoEmail.Text = "Email :";
            // 
            // lblGauInfoCity
            // 
            this.lblGauInfoCity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoCity.AutoEllipsis = true;
            this.lblGauInfoCity.AutoSize = true;
            this.lblGauInfoCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoCity.Location = new System.Drawing.Point(68, 141);
            this.lblGauInfoCity.Name = "lblGauInfoCity";
            this.lblGauInfoCity.Size = new System.Drawing.Size(35, 14);
            this.lblGauInfoCity.TabIndex = 11;
            this.lblGauInfoCity.Text = "City :";
            this.lblGauInfoCity.Visible = false;
            // 
            // lblGauInfoAddress1
            // 
            this.lblGauInfoAddress1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoAddress1.AutoEllipsis = true;
            this.lblGauInfoAddress1.AutoSize = true;
            this.lblGauInfoAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoAddress1.Location = new System.Drawing.Point(8, 93);
            this.lblGauInfoAddress1.Name = "lblGauInfoAddress1";
            this.lblGauInfoAddress1.Size = new System.Drawing.Size(95, 14);
            this.lblGauInfoAddress1.TabIndex = 6;
            this.lblGauInfoAddress1.Text = "Address Line 1 :";
            this.lblGauInfoAddress1.Visible = false;
            // 
            // lblGauInfoFName
            // 
            this.lblGauInfoFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauInfoFName.AutoEllipsis = true;
            this.lblGauInfoFName.AutoSize = true;
            this.lblGauInfoFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauInfoFName.Location = new System.Drawing.Point(57, 28);
            this.lblGauInfoFName.Name = "lblGauInfoFName";
            this.lblGauInfoFName.Size = new System.Drawing.Size(46, 14);
            this.lblGauInfoFName.TabIndex = 0;
            this.lblGauInfoFName.Text = "Name :";
            // 
            // lblGauIGaurdInfo
            // 
            this.lblGauIGaurdInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGauIGaurdInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIGaurdInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGauIGaurdInfo.Location = new System.Drawing.Point(4, 2);
            this.lblGauIGaurdInfo.Name = "lblGauIGaurdInfo";
            this.lblGauIGaurdInfo.Size = new System.Drawing.Size(742, 19);
            this.lblGauIGaurdInfo.TabIndex = 0;
            this.lblGauIGaurdInfo.Text = " Guardian\'s Information :";
            this.lblGauIGaurdInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 238);
            this.label6.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(746, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 238);
            this.label1.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(744, 1);
            this.label2.TabIndex = 29;
            // 
            // pnlFathersInfo
            // 
            this.pnlFathersInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlFathersInfo.Controls.Add(this.label18);
            this.pnlFathersInfo.Controls.Add(this.pnlFthersAdds);
            this.pnlFathersInfo.Controls.Add(this.label19);
            this.pnlFathersInfo.Controls.Add(this.cmbFCountry);
            this.pnlFathersInfo.Controls.Add(this.label29);
            this.pnlFathersInfo.Controls.Add(this.label7);
            this.pnlFathersInfo.Controls.Add(this.label30);
            this.pnlFathersInfo.Controls.Add(this.label31);
            this.pnlFathersInfo.Controls.Add(this.cmbFState);
            this.pnlFathersInfo.Controls.Add(this.mtxtFatherMobile);
            this.pnlFathersInfo.Controls.Add(this.mskGauIFatherPhone);
            this.pnlFathersInfo.Controls.Add(this.label14);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherFax);
            this.pnlFathersInfo.Controls.Add(this.chkGauIFatherAddress);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherMName);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherAddress2);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherCounty);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFathetPhone);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherZip);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherMobile);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherFax);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherEmail);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherCity);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherAddress1);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherLName);
            this.pnlFathersInfo.Controls.Add(this.txtGauIFatherfName);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherAddress2);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherCounty);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherZip);
            this.pnlFathersInfo.Controls.Add(this.lblGIFatherState);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherEmail);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherCity);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherAdd1);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherFName);
            this.pnlFathersInfo.Controls.Add(this.lblGauIFatherInfo);
            this.pnlFathersInfo.Controls.Add(this.label4);
            this.pnlFathersInfo.Controls.Add(this.label8);
            this.pnlFathersInfo.Controls.Add(this.label12);
            this.pnlFathersInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFathersInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFathersInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlFathersInfo.Location = new System.Drawing.Point(0, 325);
            this.pnlFathersInfo.Name = "pnlFathersInfo";
            this.pnlFathersInfo.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlFathersInfo.Size = new System.Drawing.Size(750, 205);
            this.pnlFathersInfo.TabIndex = 1;
            // 
            // pnlFthersAdds
            // 
            this.pnlFthersAdds.Location = new System.Drawing.Point(25, 66);
            this.pnlFthersAdds.Name = "pnlFthersAdds";
            this.pnlFthersAdds.Size = new System.Drawing.Size(325, 132);
            this.pnlFthersAdds.TabIndex = 4;
            // 
            // cmbFCountry
            // 
            this.cmbFCountry.FormattingEnabled = true;
            this.cmbFCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbFCountry.Location = new System.Drawing.Point(103, 163);
            this.cmbFCountry.MaxDropDownItems = 3;
            this.cmbFCountry.MaxLength = 20;
            this.cmbFCountry.Name = "cmbFCountry";
            this.cmbFCountry.Size = new System.Drawing.Size(130, 22);
            this.cmbFCountry.TabIndex = 10;
            this.cmbFCountry.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 14);
            this.label7.TabIndex = 35;
            this.label7.Text = "Country :";
            this.label7.Visible = false;
            // 
            // cmbFState
            // 
            this.cmbFState.FormattingEnabled = true;
            this.cmbFState.Location = new System.Drawing.Point(289, 114);
            this.cmbFState.MaxLength = 20;
            this.cmbFState.Name = "cmbFState";
            this.cmbFState.Size = new System.Drawing.Size(61, 22);
            this.cmbFState.TabIndex = 8;
            this.cmbFState.Visible = false;
            // 
            // mtxtFatherMobile
            // 
            this.mtxtFatherMobile.AllowValidate = true;
            this.mtxtFatherMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtFatherMobile.IncludeLiteralsAndPrompts = false;
            this.mtxtFatherMobile.Location = new System.Drawing.Point(448, 112);
            this.mtxtFatherMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxtFatherMobile.Name = "mtxtFatherMobile";
            this.mtxtFatherMobile.ReadOnly = false;
            this.mtxtFatherMobile.Size = new System.Drawing.Size(193, 22);
            this.mtxtFatherMobile.TabIndex = 12;
            // 
            // mskGauIFatherPhone
            // 
            this.mskGauIFatherPhone.AllowValidate = true;
            this.mskGauIFatherPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGauIFatherPhone.IncludeLiteralsAndPrompts = false;
            this.mskGauIFatherPhone.Location = new System.Drawing.Point(448, 86);
            this.mskGauIFatherPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskGauIFatherPhone.Name = "mskGauIFatherPhone";
            this.mskGauIFatherPhone.ReadOnly = false;
            this.mskGauIFatherPhone.Size = new System.Drawing.Size(193, 22);
            this.mskGauIFatherPhone.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 201);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(742, 1);
            this.label14.TabIndex = 34;
            // 
            // txtGauIFatherFax
            // 
            this.txtGauIFatherFax.AllowValidate = true;
            this.txtGauIFatherFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherFax.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherFax.IncludeLiteralsAndPrompts = false;
            this.txtGauIFatherFax.Location = new System.Drawing.Point(448, 138);
            this.txtGauIFatherFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtGauIFatherFax.Name = "txtGauIFatherFax";
            this.txtGauIFatherFax.ReadOnly = false;
            this.txtGauIFatherFax.Size = new System.Drawing.Size(193, 22);
            this.txtGauIFatherFax.TabIndex = 13;
            // 
            // chkGauIFatherAddress
            // 
            this.chkGauIFatherAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGauIFatherAddress.AutoSize = true;
            this.chkGauIFatherAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGauIFatherAddress.Location = new System.Drawing.Point(352, 69);
            this.chkGauIFatherAddress.Name = "chkGauIFatherAddress";
            this.chkGauIFatherAddress.Size = new System.Drawing.Size(161, 18);
            this.chkGauIFatherAddress.TabIndex = 3;
            this.chkGauIFatherAddress.Text = "Same as Patient Address";
            this.chkGauIFatherAddress.UseVisualStyleBackColor = true;
            this.chkGauIFatherAddress.CheckedChanged += new System.EventHandler(this.chkGauIFatherAddress_CheckedChanged);
            // 
            // txtGauIFatherMName
            // 
            this.txtGauIFatherMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherMName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherMName.Location = new System.Drawing.Point(349, 24);
            this.txtGauIFatherMName.MaxLength = 2;
            this.txtGauIFatherMName.Name = "txtGauIFatherMName";
            this.txtGauIFatherMName.Size = new System.Drawing.Size(92, 22);
            this.txtGauIFatherMName.TabIndex = 1;
            // 
            // txtGauIFatherAddress2
            // 
            this.txtGauIFatherAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherAddress2.Location = new System.Drawing.Point(104, 90);
            this.txtGauIFatherAddress2.Name = "txtGauIFatherAddress2";
            this.txtGauIFatherAddress2.Size = new System.Drawing.Size(246, 22);
            this.txtGauIFatherAddress2.TabIndex = 5;
            this.txtGauIFatherAddress2.Visible = false;
            // 
            // txtGauIFatherCounty
            // 
            this.txtGauIFatherCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherCounty.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherCounty.Location = new System.Drawing.Point(223, 138);
            this.txtGauIFatherCounty.Name = "txtGauIFatherCounty";
            this.txtGauIFatherCounty.Size = new System.Drawing.Size(127, 22);
            this.txtGauIFatherCounty.TabIndex = 9;
            this.txtGauIFatherCounty.Visible = false;
            // 
            // lblGauIFathetPhone
            // 
            this.lblGauIFathetPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFathetPhone.AutoEllipsis = true;
            this.lblGauIFathetPhone.AutoSize = true;
            this.lblGauIFathetPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFathetPhone.Location = new System.Drawing.Point(396, 92);
            this.lblGauIFathetPhone.Name = "lblGauIFathetPhone";
            this.lblGauIFathetPhone.Size = new System.Drawing.Size(50, 14);
            this.lblGauIFathetPhone.TabIndex = 21;
            this.lblGauIFathetPhone.Text = "Phone :";
            // 
            // txtGauIFatherZip
            // 
            this.txtGauIFatherZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherZip.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherZip.Location = new System.Drawing.Point(104, 138);
            this.txtGauIFatherZip.MaxLength = 8;
            this.txtGauIFatherZip.Name = "txtGauIFatherZip";
            this.txtGauIFatherZip.Size = new System.Drawing.Size(59, 22);
            this.txtGauIFatherZip.TabIndex = 6;
            this.txtGauIFatherZip.Visible = false;
            this.txtGauIFatherZip.TextChanged += new System.EventHandler(this.FatherZip_TextChanged);
            this.txtGauIFatherZip.Enter += new System.EventHandler(this.FatherZip_GotFocus);
            this.txtGauIFatherZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FatherZip_KeyDown);
            this.txtGauIFatherZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FatherZip_KeyPress);
            this.txtGauIFatherZip.Leave += new System.EventHandler(this.FatherZip_LostFocus);
            // 
            // lblGauIFatherMobile
            // 
            this.lblGauIFatherMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherMobile.AutoEllipsis = true;
            this.lblGauIFatherMobile.AutoSize = true;
            this.lblGauIFatherMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherMobile.Location = new System.Drawing.Point(397, 115);
            this.lblGauIFatherMobile.Name = "lblGauIFatherMobile";
            this.lblGauIFatherMobile.Size = new System.Drawing.Size(49, 14);
            this.lblGauIFatherMobile.TabIndex = 23;
            this.lblGauIFatherMobile.Text = "Mobile :";
            // 
            // lblGauIFatherFax
            // 
            this.lblGauIFatherFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherFax.AutoEllipsis = true;
            this.lblGauIFatherFax.AutoSize = true;
            this.lblGauIFatherFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherFax.Location = new System.Drawing.Point(413, 141);
            this.lblGauIFatherFax.Name = "lblGauIFatherFax";
            this.lblGauIFatherFax.Size = new System.Drawing.Size(33, 14);
            this.lblGauIFatherFax.TabIndex = 25;
            this.lblGauIFatherFax.Text = "Fax :";
            // 
            // txtGauIFatherEmail
            // 
            this.txtGauIFatherEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherEmail.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherEmail.Location = new System.Drawing.Point(448, 164);
            this.txtGauIFatherEmail.Name = "txtGauIFatherEmail";
            this.txtGauIFatherEmail.Size = new System.Drawing.Size(193, 22);
            this.txtGauIFatherEmail.TabIndex = 14;
            this.txtGauIFatherEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtGauIFatherEmail_Validating);
            // 
            // txtGauIFatherCity
            // 
            this.txtGauIFatherCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherCity.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherCity.Location = new System.Drawing.Point(104, 114);
            this.txtGauIFatherCity.Name = "txtGauIFatherCity";
            this.txtGauIFatherCity.Size = new System.Drawing.Size(135, 22);
            this.txtGauIFatherCity.TabIndex = 7;
            this.txtGauIFatherCity.Visible = false;
            // 
            // txtGauIFatherAddress1
            // 
            this.txtGauIFatherAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherAddress1.Location = new System.Drawing.Point(104, 66);
            this.txtGauIFatherAddress1.Name = "txtGauIFatherAddress1";
            this.txtGauIFatherAddress1.Size = new System.Drawing.Size(246, 22);
            this.txtGauIFatherAddress1.TabIndex = 4;
            this.txtGauIFatherAddress1.Visible = false;
            // 
            // txtGauIFatherLName
            // 
            this.txtGauIFatherLName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtGauIFatherLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherLName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherLName.Location = new System.Drawing.Point(445, 24);
            this.txtGauIFatherLName.Name = "txtGauIFatherLName";
            this.txtGauIFatherLName.Size = new System.Drawing.Size(193, 22);
            this.txtGauIFatherLName.TabIndex = 2;
            // 
            // txtGauIFatherfName
            // 
            this.txtGauIFatherfName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIFatherfName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIFatherfName.Location = new System.Drawing.Point(105, 24);
            this.txtGauIFatherfName.Name = "txtGauIFatherfName";
            this.txtGauIFatherfName.Size = new System.Drawing.Size(240, 22);
            this.txtGauIFatherfName.TabIndex = 0;
            // 
            // lblGauIFatherAddress2
            // 
            this.lblGauIFatherAddress2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherAddress2.AutoEllipsis = true;
            this.lblGauIFatherAddress2.AutoSize = true;
            this.lblGauIFatherAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherAddress2.Location = new System.Drawing.Point(8, 94);
            this.lblGauIFatherAddress2.Name = "lblGauIFatherAddress2";
            this.lblGauIFatherAddress2.Size = new System.Drawing.Size(95, 14);
            this.lblGauIFatherAddress2.TabIndex = 9;
            this.lblGauIFatherAddress2.Text = "Address Line 2 :";
            this.lblGauIFatherAddress2.Visible = false;
            // 
            // lblGauIFatherCounty
            // 
            this.lblGauIFatherCounty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherCounty.AutoEllipsis = true;
            this.lblGauIFatherCounty.AutoSize = true;
            this.lblGauIFatherCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherCounty.Location = new System.Drawing.Point(166, 142);
            this.lblGauIFatherCounty.Name = "lblGauIFatherCounty";
            this.lblGauIFatherCounty.Size = new System.Drawing.Size(54, 14);
            this.lblGauIFatherCounty.TabIndex = 17;
            this.lblGauIFatherCounty.Text = "County :";
            // 
            // lblGauIFatherZip
            // 
            this.lblGauIFatherZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherZip.AutoEllipsis = true;
            this.lblGauIFatherZip.AutoSize = true;
            this.lblGauIFatherZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherZip.Location = new System.Drawing.Point(72, 142);
            this.lblGauIFatherZip.Name = "lblGauIFatherZip";
            this.lblGauIFatherZip.Size = new System.Drawing.Size(31, 14);
            this.lblGauIFatherZip.TabIndex = 15;
            this.lblGauIFatherZip.Text = "Zip :";
            this.lblGauIFatherZip.Visible = false;
            // 
            // lblGIFatherState
            // 
            this.lblGIFatherState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIFatherState.AutoEllipsis = true;
            this.lblGIFatherState.AutoSize = true;
            this.lblGIFatherState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIFatherState.Location = new System.Drawing.Point(244, 118);
            this.lblGIFatherState.Name = "lblGIFatherState";
            this.lblGIFatherState.Size = new System.Drawing.Size(45, 14);
            this.lblGIFatherState.TabIndex = 13;
            this.lblGIFatherState.Text = "State :";
            // 
            // lblGauIFatherEmail
            // 
            this.lblGauIFatherEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherEmail.AutoEllipsis = true;
            this.lblGauIFatherEmail.AutoSize = true;
            this.lblGauIFatherEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherEmail.Location = new System.Drawing.Point(404, 168);
            this.lblGauIFatherEmail.Name = "lblGauIFatherEmail";
            this.lblGauIFatherEmail.Size = new System.Drawing.Size(42, 14);
            this.lblGauIFatherEmail.TabIndex = 19;
            this.lblGauIFatherEmail.Text = "Email :";
            // 
            // lblGauIFatherCity
            // 
            this.lblGauIFatherCity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherCity.AutoEllipsis = true;
            this.lblGauIFatherCity.AutoSize = true;
            this.lblGauIFatherCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherCity.Location = new System.Drawing.Point(68, 118);
            this.lblGauIFatherCity.Name = "lblGauIFatherCity";
            this.lblGauIFatherCity.Size = new System.Drawing.Size(35, 14);
            this.lblGauIFatherCity.TabIndex = 11;
            this.lblGauIFatherCity.Text = "City :";
            this.lblGauIFatherCity.Visible = false;
            // 
            // lblGauIFatherAdd1
            // 
            this.lblGauIFatherAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherAdd1.AutoEllipsis = true;
            this.lblGauIFatherAdd1.AutoSize = true;
            this.lblGauIFatherAdd1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherAdd1.Location = new System.Drawing.Point(8, 70);
            this.lblGauIFatherAdd1.Name = "lblGauIFatherAdd1";
            this.lblGauIFatherAdd1.Size = new System.Drawing.Size(95, 14);
            this.lblGauIFatherAdd1.TabIndex = 6;
            this.lblGauIFatherAdd1.Text = "Address Line 1 :";
            this.lblGauIFatherAdd1.Visible = false;
            // 
            // lblGauIFatherFName
            // 
            this.lblGauIFatherFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFatherFName.AutoEllipsis = true;
            this.lblGauIFatherFName.AutoSize = true;
            this.lblGauIFatherFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherFName.Location = new System.Drawing.Point(57, 28);
            this.lblGauIFatherFName.Name = "lblGauIFatherFName";
            this.lblGauIFatherFName.Size = new System.Drawing.Size(46, 14);
            this.lblGauIFatherFName.TabIndex = 0;
            this.lblGauIFatherFName.Text = "Name :";
            // 
            // lblGauIFatherInfo
            // 
            this.lblGauIFatherInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGauIFatherInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFatherInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGauIFatherInfo.Location = new System.Drawing.Point(4, 2);
            this.lblGauIFatherInfo.Name = "lblGauIFatherInfo";
            this.lblGauIFatherInfo.Size = new System.Drawing.Size(742, 19);
            this.lblGauIFatherInfo.TabIndex = 0;
            this.lblGauIFatherInfo.Text = " Father\'s Information :";
            this.lblGauIFatherInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(742, 1);
            this.label4.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 201);
            this.label8.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(746, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 201);
            this.label12.TabIndex = 33;
            // 
            // pnlMotherInfo
            // 
            this.pnlMotherInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlMotherInfo.Controls.Add(this.label17);
            this.pnlMotherInfo.Controls.Add(this.label23);
            this.pnlMotherInfo.Controls.Add(this.label27);
            this.pnlMotherInfo.Controls.Add(this.label28);
            this.pnlMotherInfo.Controls.Add(this.label22);
            this.pnlMotherInfo.Controls.Add(this.label26);
            this.pnlMotherInfo.Controls.Add(this.label25);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherMaidenMName);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherMaidenLName);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherMaidenFName);
            this.pnlMotherInfo.Controls.Add(this.label24);
            this.pnlMotherInfo.Controls.Add(this.pnlMthersAdds);
            this.pnlMotherInfo.Controls.Add(this.cmbGauIMCountry);
            this.pnlMotherInfo.Controls.Add(this.label49);
            this.pnlMotherInfo.Controls.Add(this.cmbGauIMState);
            this.pnlMotherInfo.Controls.Add(this.mskGauIMMobile);
            this.pnlMotherInfo.Controls.Add(this.mskGauIMPhone);
            this.pnlMotherInfo.Controls.Add(this.label13);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMFax);
            this.pnlMotherInfo.Controls.Add(this.chkGauIAddrforMother);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherMName);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherAddress2);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMCounty);
            this.pnlMotherInfo.Controls.Add(this.lblGauIMPhone);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherZip);
            this.pnlMotherInfo.Controls.Add(this.lblGauIMMobile);
            this.pnlMotherInfo.Controls.Add(this.lblGauIMFax);
            this.pnlMotherInfo.Controls.Add(this.txtGauiMEmail);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherCity);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherAddress1);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherLName);
            this.pnlMotherInfo.Controls.Add(this.txtGauIMotherfName);
            this.pnlMotherInfo.Controls.Add(this.lblGauIAddressLine2);
            this.pnlMotherInfo.Controls.Add(this.lblGauICounty);
            this.pnlMotherInfo.Controls.Add(this.lblGauIMZip);
            this.pnlMotherInfo.Controls.Add(this.lblGauIState);
            this.pnlMotherInfo.Controls.Add(this.lblGauiMEmail);
            this.pnlMotherInfo.Controls.Add(this.lblGauICity);
            this.pnlMotherInfo.Controls.Add(this.lblGauIAddressLine1);
            this.pnlMotherInfo.Controls.Add(this.lblGauIFName);
            this.pnlMotherInfo.Controls.Add(this.lblGauIMotherInfoHeader);
            this.pnlMotherInfo.Controls.Add(this.label5);
            this.pnlMotherInfo.Controls.Add(this.label9);
            this.pnlMotherInfo.Controls.Add(this.label11);
            this.pnlMotherInfo.Controls.Add(this.label16);
            this.pnlMotherInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMotherInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMotherInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMotherInfo.Location = new System.Drawing.Point(0, 87);
            this.pnlMotherInfo.Name = "pnlMotherInfo";
            this.pnlMotherInfo.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlMotherInfo.Size = new System.Drawing.Size(750, 238);
            this.pnlMotherInfo.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label22.Location = new System.Drawing.Point(193, 86);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 12);
            this.label22.TabIndex = 45;
            this.label22.Text = "(First Name)";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoEllipsis = true;
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label26.Location = new System.Drawing.Point(382, 86);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(25, 12);
            this.label26.TabIndex = 44;
            this.label26.Text = "(MI)";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoEllipsis = true;
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label25.Location = new System.Drawing.Point(512, 86);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 12);
            this.label25.TabIndex = 43;
            this.label25.Text = "(Last Name)";
            // 
            // txtGauIMotherMaidenMName
            // 
            this.txtGauIMotherMaidenMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherMaidenMName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherMaidenMName.Location = new System.Drawing.Point(349, 62);
            this.txtGauIMotherMaidenMName.MaxLength = 2;
            this.txtGauIMotherMaidenMName.Name = "txtGauIMotherMaidenMName";
            this.txtGauIMotherMaidenMName.Size = new System.Drawing.Size(92, 22);
            this.txtGauIMotherMaidenMName.TabIndex = 4;
            // 
            // txtGauIMotherMaidenLName
            // 
            this.txtGauIMotherMaidenLName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtGauIMotherMaidenLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherMaidenLName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherMaidenLName.Location = new System.Drawing.Point(445, 62);
            this.txtGauIMotherMaidenLName.Name = "txtGauIMotherMaidenLName";
            this.txtGauIMotherMaidenLName.Size = new System.Drawing.Size(193, 22);
            this.txtGauIMotherMaidenLName.TabIndex = 5;
            // 
            // txtGauIMotherMaidenFName
            // 
            this.txtGauIMotherMaidenFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherMaidenFName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherMaidenFName.Location = new System.Drawing.Point(105, 62);
            this.txtGauIMotherMaidenFName.Name = "txtGauIMotherMaidenFName";
            this.txtGauIMotherMaidenFName.Size = new System.Drawing.Size(240, 22);
            this.txtGauIMotherMaidenFName.TabIndex = 3;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoEllipsis = true;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(16, 66);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(88, 14);
            this.label24.TabIndex = 37;
            this.label24.Text = "Maiden Name :";
            // 
            // pnlMthersAdds
            // 
            this.pnlMthersAdds.Location = new System.Drawing.Point(25, 99);
            this.pnlMthersAdds.Name = "pnlMthersAdds";
            this.pnlMthersAdds.Size = new System.Drawing.Size(325, 132);
            this.pnlMthersAdds.TabIndex = 6;
            // 
            // cmbGauIMCountry
            // 
            this.cmbGauIMCountry.FormattingEnabled = true;
            this.cmbGauIMCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbGauIMCountry.Location = new System.Drawing.Point(103, 194);
            this.cmbGauIMCountry.MaxDropDownItems = 3;
            this.cmbGauIMCountry.MaxLength = 20;
            this.cmbGauIMCountry.Name = "cmbGauIMCountry";
            this.cmbGauIMCountry.Size = new System.Drawing.Size(130, 22);
            this.cmbGauIMCountry.TabIndex = 10;
            this.cmbGauIMCountry.Visible = false;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(45, 197);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 34;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // cmbGauIMState
            // 
            this.cmbGauIMState.FormattingEnabled = true;
            this.cmbGauIMState.Location = new System.Drawing.Point(289, 145);
            this.cmbGauIMState.MaxLength = 20;
            this.cmbGauIMState.Name = "cmbGauIMState";
            this.cmbGauIMState.Size = new System.Drawing.Size(61, 22);
            this.cmbGauIMState.TabIndex = 8;
            this.cmbGauIMState.Visible = false;
            // 
            // mskGauIMMobile
            // 
            this.mskGauIMMobile.AllowValidate = true;
            this.mskGauIMMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGauIMMobile.IncludeLiteralsAndPrompts = false;
            this.mskGauIMMobile.Location = new System.Drawing.Point(448, 148);
            this.mskGauIMMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mskGauIMMobile.Name = "mskGauIMMobile";
            this.mskGauIMMobile.ReadOnly = false;
            this.mskGauIMMobile.Size = new System.Drawing.Size(193, 22);
            this.mskGauIMMobile.TabIndex = 9;
            // 
            // mskGauIMPhone
            // 
            this.mskGauIMPhone.AllowValidate = true;
            this.mskGauIMPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGauIMPhone.IncludeLiteralsAndPrompts = false;
            this.mskGauIMPhone.Location = new System.Drawing.Point(448, 121);
            this.mskGauIMPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskGauIMPhone.Name = "mskGauIMPhone";
            this.mskGauIMPhone.ReadOnly = false;
            this.mskGauIMPhone.Size = new System.Drawing.Size(193, 22);
            this.mskGauIMPhone.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 234);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(742, 1);
            this.label13.TabIndex = 33;
            // 
            // txtGauIMFax
            // 
            this.txtGauIMFax.AllowValidate = true;
            this.txtGauIMFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMFax.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMFax.IncludeLiteralsAndPrompts = false;
            this.txtGauIMFax.Location = new System.Drawing.Point(448, 172);
            this.txtGauIMFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtGauIMFax.Name = "txtGauIMFax";
            this.txtGauIMFax.ReadOnly = false;
            this.txtGauIMFax.Size = new System.Drawing.Size(193, 22);
            this.txtGauIMFax.TabIndex = 10;
            // 
            // chkGauIAddrforMother
            // 
            this.chkGauIAddrforMother.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGauIAddrforMother.AutoSize = true;
            this.chkGauIAddrforMother.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGauIAddrforMother.Location = new System.Drawing.Point(350, 99);
            this.chkGauIAddrforMother.Name = "chkGauIAddrforMother";
            this.chkGauIAddrforMother.Size = new System.Drawing.Size(161, 18);
            this.chkGauIAddrforMother.TabIndex = 7;
            this.chkGauIAddrforMother.Text = "Same as Patient Address";
            this.chkGauIAddrforMother.UseVisualStyleBackColor = true;
            this.chkGauIAddrforMother.CheckedChanged += new System.EventHandler(this.chkGauIAddrforMother_CheckedChanged);
            // 
            // txtGauIMotherMName
            // 
            this.txtGauIMotherMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherMName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherMName.Location = new System.Drawing.Point(349, 24);
            this.txtGauIMotherMName.MaxLength = 2;
            this.txtGauIMotherMName.Name = "txtGauIMotherMName";
            this.txtGauIMotherMName.Size = new System.Drawing.Size(92, 22);
            this.txtGauIMotherMName.TabIndex = 1;
            // 
            // txtGauIMotherAddress2
            // 
            this.txtGauIMotherAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherAddress2.Location = new System.Drawing.Point(103, 121);
            this.txtGauIMotherAddress2.Name = "txtGauIMotherAddress2";
            this.txtGauIMotherAddress2.Size = new System.Drawing.Size(247, 22);
            this.txtGauIMotherAddress2.TabIndex = 5;
            this.txtGauIMotherAddress2.Visible = false;
            // 
            // txtGauIMCounty
            // 
            this.txtGauIMCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMCounty.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMCounty.Location = new System.Drawing.Point(222, 169);
            this.txtGauIMCounty.Name = "txtGauIMCounty";
            this.txtGauIMCounty.Size = new System.Drawing.Size(128, 22);
            this.txtGauIMCounty.TabIndex = 9;
            this.txtGauIMCounty.Visible = false;
            // 
            // lblGauIMPhone
            // 
            this.lblGauIMPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIMPhone.AutoEllipsis = true;
            this.lblGauIMPhone.AutoSize = true;
            this.lblGauIMPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIMPhone.Location = new System.Drawing.Point(396, 125);
            this.lblGauIMPhone.Name = "lblGauIMPhone";
            this.lblGauIMPhone.Size = new System.Drawing.Size(50, 14);
            this.lblGauIMPhone.TabIndex = 21;
            this.lblGauIMPhone.Text = "Phone :";
            // 
            // txtGauIMotherZip
            // 
            this.txtGauIMotherZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherZip.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherZip.Location = new System.Drawing.Point(103, 169);
            this.txtGauIMotherZip.MaxLength = 8;
            this.txtGauIMotherZip.Name = "txtGauIMotherZip";
            this.txtGauIMotherZip.Size = new System.Drawing.Size(59, 22);
            this.txtGauIMotherZip.TabIndex = 6;
            this.txtGauIMotherZip.Visible = false;
            this.txtGauIMotherZip.TextChanged += new System.EventHandler(this.MotherZip_TextChanged);
            this.txtGauIMotherZip.Enter += new System.EventHandler(this.MotherZip_GotFocus);
            this.txtGauIMotherZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MotherZip_KeyDown);
            this.txtGauIMotherZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MotherZip_KeyPress);
            this.txtGauIMotherZip.Leave += new System.EventHandler(this.MotherZip_LostFocus);
            // 
            // lblGauIMMobile
            // 
            this.lblGauIMMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIMMobile.AutoEllipsis = true;
            this.lblGauIMMobile.AutoSize = true;
            this.lblGauIMMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIMMobile.Location = new System.Drawing.Point(397, 149);
            this.lblGauIMMobile.Name = "lblGauIMMobile";
            this.lblGauIMMobile.Size = new System.Drawing.Size(49, 14);
            this.lblGauIMMobile.TabIndex = 23;
            this.lblGauIMMobile.Text = "Mobile :";
            // 
            // lblGauIMFax
            // 
            this.lblGauIMFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIMFax.AutoEllipsis = true;
            this.lblGauIMFax.AutoSize = true;
            this.lblGauIMFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIMFax.Location = new System.Drawing.Point(413, 176);
            this.lblGauIMFax.Name = "lblGauIMFax";
            this.lblGauIMFax.Size = new System.Drawing.Size(33, 14);
            this.lblGauIMFax.TabIndex = 25;
            this.lblGauIMFax.Text = "Fax :";
            // 
            // txtGauiMEmail
            // 
            this.txtGauiMEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauiMEmail.ForeColor = System.Drawing.Color.Black;
            this.txtGauiMEmail.Location = new System.Drawing.Point(448, 196);
            this.txtGauiMEmail.Name = "txtGauiMEmail";
            this.txtGauiMEmail.Size = new System.Drawing.Size(193, 22);
            this.txtGauiMEmail.TabIndex = 11;
            this.txtGauiMEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtGauiMEmail_Validating);
            // 
            // txtGauIMotherCity
            // 
            this.txtGauIMotherCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherCity.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherCity.Location = new System.Drawing.Point(103, 145);
            this.txtGauIMotherCity.Name = "txtGauIMotherCity";
            this.txtGauIMotherCity.Size = new System.Drawing.Size(135, 22);
            this.txtGauIMotherCity.TabIndex = 7;
            this.txtGauIMotherCity.Visible = false;
            // 
            // txtGauIMotherAddress1
            // 
            this.txtGauIMotherAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherAddress1.Location = new System.Drawing.Point(103, 97);
            this.txtGauIMotherAddress1.Name = "txtGauIMotherAddress1";
            this.txtGauIMotherAddress1.Size = new System.Drawing.Size(247, 22);
            this.txtGauIMotherAddress1.TabIndex = 4;
            this.txtGauIMotherAddress1.Visible = false;
            // 
            // txtGauIMotherLName
            // 
            this.txtGauIMotherLName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.txtGauIMotherLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherLName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherLName.Location = new System.Drawing.Point(445, 24);
            this.txtGauIMotherLName.Name = "txtGauIMotherLName";
            this.txtGauIMotherLName.Size = new System.Drawing.Size(193, 22);
            this.txtGauIMotherLName.TabIndex = 2;
            // 
            // txtGauIMotherfName
            // 
            this.txtGauIMotherfName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGauIMotherfName.ForeColor = System.Drawing.Color.Black;
            this.txtGauIMotherfName.Location = new System.Drawing.Point(105, 24);
            this.txtGauIMotherfName.Name = "txtGauIMotherfName";
            this.txtGauIMotherfName.Size = new System.Drawing.Size(240, 22);
            this.txtGauIMotherfName.TabIndex = 0;
            // 
            // lblGauIAddressLine2
            // 
            this.lblGauIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIAddressLine2.AutoEllipsis = true;
            this.lblGauIAddressLine2.AutoSize = true;
            this.lblGauIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIAddressLine2.Location = new System.Drawing.Point(8, 125);
            this.lblGauIAddressLine2.Name = "lblGauIAddressLine2";
            this.lblGauIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblGauIAddressLine2.TabIndex = 9;
            this.lblGauIAddressLine2.Text = "Address Line 2 :";
            this.lblGauIAddressLine2.Visible = false;
            // 
            // lblGauICounty
            // 
            this.lblGauICounty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauICounty.AutoEllipsis = true;
            this.lblGauICounty.AutoSize = true;
            this.lblGauICounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauICounty.Location = new System.Drawing.Point(165, 173);
            this.lblGauICounty.Name = "lblGauICounty";
            this.lblGauICounty.Size = new System.Drawing.Size(54, 14);
            this.lblGauICounty.TabIndex = 17;
            this.lblGauICounty.Text = "County :";
            this.lblGauICounty.Visible = false;
            // 
            // lblGauIMZip
            // 
            this.lblGauIMZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIMZip.AutoEllipsis = true;
            this.lblGauIMZip.AutoSize = true;
            this.lblGauIMZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIMZip.Location = new System.Drawing.Point(72, 173);
            this.lblGauIMZip.Name = "lblGauIMZip";
            this.lblGauIMZip.Size = new System.Drawing.Size(31, 14);
            this.lblGauIMZip.TabIndex = 15;
            this.lblGauIMZip.Text = "Zip :";
            this.lblGauIMZip.Visible = false;
            // 
            // lblGauIState
            // 
            this.lblGauIState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIState.AutoEllipsis = true;
            this.lblGauIState.AutoSize = true;
            this.lblGauIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIState.Location = new System.Drawing.Point(245, 149);
            this.lblGauIState.Name = "lblGauIState";
            this.lblGauIState.Size = new System.Drawing.Size(45, 14);
            this.lblGauIState.TabIndex = 13;
            this.lblGauIState.Text = "State :";
            this.lblGauIState.Visible = false;
            // 
            // lblGauiMEmail
            // 
            this.lblGauiMEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauiMEmail.AutoEllipsis = true;
            this.lblGauiMEmail.AutoSize = true;
            this.lblGauiMEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauiMEmail.Location = new System.Drawing.Point(404, 200);
            this.lblGauiMEmail.Name = "lblGauiMEmail";
            this.lblGauiMEmail.Size = new System.Drawing.Size(42, 14);
            this.lblGauiMEmail.TabIndex = 19;
            this.lblGauiMEmail.Text = "Email :";
            // 
            // lblGauICity
            // 
            this.lblGauICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauICity.AutoEllipsis = true;
            this.lblGauICity.AutoSize = true;
            this.lblGauICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauICity.Location = new System.Drawing.Point(68, 149);
            this.lblGauICity.Name = "lblGauICity";
            this.lblGauICity.Size = new System.Drawing.Size(35, 14);
            this.lblGauICity.TabIndex = 11;
            this.lblGauICity.Text = "City :";
            this.lblGauICity.Visible = false;
            // 
            // lblGauIAddressLine1
            // 
            this.lblGauIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIAddressLine1.AutoEllipsis = true;
            this.lblGauIAddressLine1.AutoSize = true;
            this.lblGauIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIAddressLine1.Location = new System.Drawing.Point(8, 101);
            this.lblGauIAddressLine1.Name = "lblGauIAddressLine1";
            this.lblGauIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblGauIAddressLine1.TabIndex = 6;
            this.lblGauIAddressLine1.Text = "Address Line 1 :";
            this.lblGauIAddressLine1.Visible = false;
            // 
            // lblGauIFName
            // 
            this.lblGauIFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGauIFName.AutoEllipsis = true;
            this.lblGauIFName.AutoSize = true;
            this.lblGauIFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIFName.Location = new System.Drawing.Point(58, 28);
            this.lblGauIFName.Name = "lblGauIFName";
            this.lblGauIFName.Size = new System.Drawing.Size(46, 14);
            this.lblGauIFName.TabIndex = 0;
            this.lblGauIFName.Text = "Name :";
            // 
            // lblGauIMotherInfoHeader
            // 
            this.lblGauIMotherInfoHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGauIMotherInfoHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGauIMotherInfoHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGauIMotherInfoHeader.Location = new System.Drawing.Point(4, 2);
            this.lblGauIMotherInfoHeader.Name = "lblGauIMotherInfoHeader";
            this.lblGauIMotherInfoHeader.Size = new System.Drawing.Size(742, 19);
            this.lblGauIMotherInfoHeader.TabIndex = 0;
            this.lblGauIMotherInfoHeader.Text = " Mother\'s Information :";
            this.lblGauIMotherInfoHeader.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(742, 1);
            this.label5.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 234);
            this.label9.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(746, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 234);
            this.label11.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoEllipsis = true;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(182, 48);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "*";
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.pnlFInternalControl);
            this.pnlTOP.Controls.Add(this.pnlMInternalControl);
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTOP.Location = new System.Drawing.Point(0, 31);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(750, 56);
            this.pnlTOP.TabIndex = 26;
            // 
            // pnlFInternalControl
            // 
            this.pnlFInternalControl.Location = new System.Drawing.Point(514, 18);
            this.pnlFInternalControl.Name = "pnlFInternalControl";
            this.pnlFInternalControl.Size = new System.Drawing.Size(41, 32);
            this.pnlFInternalControl.TabIndex = 105;
            this.pnlFInternalControl.Visible = false;
            // 
            // pnlMInternalControl
            // 
            this.pnlMInternalControl.Location = new System.Drawing.Point(518, 15);
            this.pnlMInternalControl.Name = "pnlMInternalControl";
            this.pnlMInternalControl.Size = new System.Drawing.Size(41, 32);
            this.pnlMInternalControl.TabIndex = 104;
            this.pnlMInternalControl.Visible = false;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(744, 53);
            this.ts_Commands.TabIndex = 14;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlGI);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(750, 31);
            this.panel1.TabIndex = 31;
            // 
            // pnlGI
            // 
            this.pnlGI.BackColor = System.Drawing.Color.Transparent;
            this.pnlGI.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGI.Controls.Add(this.pnlGInternalControl);
            this.pnlGI.Controls.Add(this.lbl_BottomBrd);
            this.pnlGI.Controls.Add(this.lbl_LeftBrd);
            this.pnlGI.Controls.Add(this.lbl_RightBrd);
            this.pnlGI.Controls.Add(this.lbl_TopBrd);
            this.pnlGI.Controls.Add(this.lblGaurIHeader);
            this.pnlGI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlGI.Location = new System.Drawing.Point(3, 3);
            this.pnlGI.Name = "pnlGI";
            this.pnlGI.Size = new System.Drawing.Size(744, 25);
            this.pnlGI.TabIndex = 0;
            this.pnlGI.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGI_Paint);
            // 
            // pnlGInternalControl
            // 
            this.pnlGInternalControl.Location = new System.Drawing.Point(440, 8);
            this.pnlGInternalControl.Name = "pnlGInternalControl";
            this.pnlGInternalControl.Size = new System.Drawing.Size(306, 135);
            this.pnlGInternalControl.TabIndex = 106;
            this.pnlGInternalControl.Visible = false;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(742, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(743, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(744, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblGaurIHeader
            // 
            this.lblGaurIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGaurIHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGaurIHeader.ForeColor = System.Drawing.Color.White;
            this.lblGaurIHeader.Location = new System.Drawing.Point(0, 0);
            this.lblGaurIHeader.Name = "lblGaurIHeader";
            this.lblGaurIHeader.Size = new System.Drawing.Size(744, 25);
            this.lblGaurIHeader.TabIndex = 0;
            this.lblGaurIHeader.Text = "  Guardian Information";
            this.lblGaurIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Tag = "Save";
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label23.Location = new System.Drawing.Point(193, 48);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 12);
            this.label23.TabIndex = 48;
            this.label23.Text = "(First Name)";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoEllipsis = true;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label27.Location = new System.Drawing.Point(383, 48);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(25, 12);
            this.label27.TabIndex = 47;
            this.label27.Text = "(MI)";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoEllipsis = true;
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label28.Location = new System.Drawing.Point(512, 48);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(58, 12);
            this.label28.TabIndex = 46;
            this.label28.Text = "(Last Name)";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoEllipsis = true;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(503, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 49;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(488, 50);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 54;
            this.label18.Text = "*";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label19.Location = new System.Drawing.Point(177, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 12);
            this.label19.TabIndex = 53;
            this.label19.Text = "(First Name)";
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label29.Location = new System.Drawing.Point(383, 50);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(25, 12);
            this.label29.TabIndex = 52;
            this.label29.Text = "(MI)";
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoEllipsis = true;
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label30.Location = new System.Drawing.Point(512, 50);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(58, 12);
            this.label30.TabIndex = 51;
            this.label30.Text = "(Last Name)";
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoEllipsis = true;
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(166, 50);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(11, 12);
            this.label31.TabIndex = 50;
            this.label31.Text = "*";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoEllipsis = true;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(484, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 59;
            this.label20.Text = "*";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoEllipsis = true;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label21.Location = new System.Drawing.Point(173, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(60, 12);
            this.label21.TabIndex = 58;
            this.label21.Text = "(First Name)";
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoEllipsis = true;
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label32.Location = new System.Drawing.Point(383, 49);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(25, 12);
            this.label32.TabIndex = 57;
            this.label32.Text = "(MI)";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoEllipsis = true;
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label33.Location = new System.Drawing.Point(512, 49);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(58, 12);
            this.label33.TabIndex = 56;
            this.label33.Text = "(Last Name)";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoEllipsis = true;
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label34.ForeColor = System.Drawing.Color.Red;
            this.label34.Location = new System.Drawing.Point(162, 49);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(11, 12);
            this.label34.TabIndex = 55;
            this.label34.Text = "*";
            // 
            // gloPatientGuardianControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlGuardianInfo);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientGuardianControl";
            this.Size = new System.Drawing.Size(750, 773);
            this.Load += new System.EventHandler(this.gloPatientGuardian_Load);
            this.pnlGuardianInfo.ResumeLayout(false);
            this.pnlGaurInfo.ResumeLayout(false);
            this.pnlGaurInfo.PerformLayout();
            this.pnlFathersInfo.ResumeLayout(false);
            this.pnlFathersInfo.PerformLayout();
            this.pnlMotherInfo.ResumeLayout(false);
            this.pnlMotherInfo.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlGI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGuardianInfo;
        private System.Windows.Forms.Panel pnlGaurInfo;
        private gloMaskControl.gloMaskBox  txtGauInfoFax;
        private System.Windows.Forms.CheckBox cb_AddrforGuardian;
        private System.Windows.Forms.TextBox txtGauInfoMName;
        private System.Windows.Forms.TextBox txtGauInfoAddress2;
        private System.Windows.Forms.TextBox txtGauInfoCounty;
        private System.Windows.Forms.Label lblGauInfoPhone;
        private System.Windows.Forms.TextBox txtGauInfoZip;
        private System.Windows.Forms.Label lblGauInfoMobile;
        private System.Windows.Forms.Label lblGauInfoFax;
        private System.Windows.Forms.TextBox txtGauInfoEmail;
        private System.Windows.Forms.TextBox txtGauInfoCity;
        private System.Windows.Forms.TextBox txtGauInfoAddress1;
        private System.Windows.Forms.TextBox txtGauInfoLName;
        private System.Windows.Forms.TextBox txtGauInfoFName;
        private System.Windows.Forms.Label lblGauInfoAddress2;
        private System.Windows.Forms.Label lblGauInfoCounty;
        private System.Windows.Forms.Label lblGauInfoZip;
        private System.Windows.Forms.Label lblGauInfoState;
        private System.Windows.Forms.Label lblGauInoEmail;
        private System.Windows.Forms.Label lblGauInfoCity;
        private System.Windows.Forms.Label lblGauInfoAddress1;
        private System.Windows.Forms.Label lblGauInfoFName;
        private System.Windows.Forms.Label lblGauIGaurdInfo;
        private System.Windows.Forms.Panel pnlFathersInfo;
        private gloMaskControl.gloMaskBox  txtGauIFatherFax;
        private System.Windows.Forms.CheckBox chkGauIFatherAddress;
        private System.Windows.Forms.TextBox txtGauIFatherMName;
        private System.Windows.Forms.TextBox txtGauIFatherAddress2;
        private System.Windows.Forms.TextBox txtGauIFatherCounty;
        private System.Windows.Forms.Label lblGauIFathetPhone;
        private System.Windows.Forms.TextBox txtGauIFatherZip;
        private System.Windows.Forms.Label lblGauIFatherMobile;
        private System.Windows.Forms.Label lblGauIFatherFax;
        private System.Windows.Forms.TextBox txtGauIFatherEmail;
        private System.Windows.Forms.TextBox txtGauIFatherCity;
        private System.Windows.Forms.TextBox txtGauIFatherAddress1;
        private System.Windows.Forms.TextBox txtGauIFatherLName;
        private System.Windows.Forms.TextBox txtGauIFatherfName;
        private System.Windows.Forms.Label lblGauIFatherAddress2;
        private System.Windows.Forms.Label lblGauIFatherCounty;
        private System.Windows.Forms.Label lblGauIFatherZip;
        private System.Windows.Forms.Label lblGIFatherState;
        private System.Windows.Forms.Label lblGauIFatherEmail;
        private System.Windows.Forms.Label lblGauIFatherCity;
        private System.Windows.Forms.Label lblGauIFatherAdd1;
        private System.Windows.Forms.Label lblGauIFatherFName;
        private System.Windows.Forms.Label lblGauIFatherInfo;
        private System.Windows.Forms.Panel pnlMotherInfo;
        private System.Windows.Forms.CheckBox chkGauIAddrforMother;
        private System.Windows.Forms.TextBox txtGauIMotherMName;
        private System.Windows.Forms.TextBox txtGauIMotherAddress2;
        private System.Windows.Forms.TextBox txtGauIMCounty;
        private System.Windows.Forms.Label lblGauIMPhone;
        private System.Windows.Forms.TextBox txtGauIMotherZip;
        private System.Windows.Forms.Label lblGauIMMobile;
        private System.Windows.Forms.Label lblGauIMFax;
        private System.Windows.Forms.TextBox txtGauiMEmail;
        private System.Windows.Forms.TextBox txtGauIMotherCity;
        private System.Windows.Forms.TextBox txtGauIMotherAddress1;
        private System.Windows.Forms.TextBox txtGauIMotherLName;
        private System.Windows.Forms.TextBox txtGauIMotherfName;
        private System.Windows.Forms.Label lblGauIAddressLine2;
        private System.Windows.Forms.Label lblGauICounty;
        private System.Windows.Forms.Label lblGauIMZip;
        private System.Windows.Forms.Label lblGauIState;
        private System.Windows.Forms.Label lblGauiMEmail;
        private System.Windows.Forms.Label lblGauICity;
        private System.Windows.Forms.Label lblGauIAddressLine1;
        private System.Windows.Forms.Label lblGauIFName;
        private System.Windows.Forms.Label lblGauIMotherInfoHeader;
        private System.Windows.Forms.Panel pnlGI;
        private System.Windows.Forms.Label lblGaurIHeader;
        //internal System.Windows.Forms.ToolStripButton tsb_OK;
        //internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private gloMaskControl.gloMaskBox mskGauIMMobile;
        private gloMaskControl.gloMaskBox mskGauIMPhone;
        private gloMaskControl.gloMaskBox mtxtFatherMobile;
        private gloMaskControl.gloMaskBox mskGauIFatherPhone;
        private gloMaskControl.gloMaskBox mskGauInfoMobile;
        private gloMaskControl.gloMaskBox mskGauInfoPhone;
        private System.Windows.Forms.ComboBox cmbGauInfoState;
        private System.Windows.Forms.ComboBox cmbFState;
        private System.Windows.Forms.ComboBox cmbGauIMState;
        private System.Windows.Forms.ComboBox cmbGauInfoCountry;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbFCountry;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGauIMCountry;
        private System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Panel pnlFInternalControl;
        internal System.Windows.Forms.Panel pnlMInternalControl;
        internal System.Windows.Forms.Panel pnlGInternalControl;
        private gloMaskControl.gloMaskBox txtGauIMFax;
        internal System.Windows.Forms.Panel pnlMthersAdds;
        internal System.Windows.Forms.Panel pnlFthersAdds;
        internal System.Windows.Forms.Panel pnlGuaiAdds;
        private System.Windows.Forms.ComboBox cmbGauInfoRelation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtGauIMotherMaidenMName;
        private System.Windows.Forms.TextBox txtGauIMotherMaidenLName;
        private System.Windows.Forms.TextBox txtGauIMotherMaidenFName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
       
        
    }
}
