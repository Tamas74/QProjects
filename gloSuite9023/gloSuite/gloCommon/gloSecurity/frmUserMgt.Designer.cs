namespace gloSecurity
{
    partial class frmUserMgt
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
                System.Windows.Forms.DateTimePicker[] cntdtControls = { dtDOB };
                System.Windows.Forms.Control[] cntControls = { dtDOB };

                try
                {
                    components.Dispose();
                   
                      
                        if (cntdtControls != null)
                        {
                            if (cntdtControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                            }
                        }
                       
                    
                }
                catch
                {
                }

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserMgt));
            this.chkCoSign = new System.Windows.Forms.CheckBox();
            this.trvUserRights = new System.Windows.Forms.TreeView();
            this.lstGroups = new System.Windows.Forms.CheckedListBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.chkAccessDenied = new System.Windows.Forms.CheckBox();
            this.chkAuditTrails = new System.Windows.Forms.CheckBox();
            this.chkgloPMSAdmin = new System.Windows.Forms.CheckBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.txtNickName = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtn_UserDetails = new System.Windows.Forms.RadioButton();
            this.rbtn_otherInfo = new System.Windows.Forms.RadioButton();
            this.pnlUserDetails = new System.Windows.Forms.Panel();
            this.chkApplyPwdSettings = new System.Windows.Forms.CheckBox();
            this.btnBlock = new System.Windows.Forms.Button();
            this.btnExpandAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_UserHeader = new System.Windows.Forms.Panel();
            this.lblUserHeader = new System.Windows.Forms.Label();
            this.pnl_tlsStrip = new System.Windows.Forms.Panel();
            this.pnl_OtherInfo = new System.Windows.Forms.Panel();
            this.pnlSignature = new System.Windows.Forms.Panel();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.picSignature = new System.Windows.Forms.PictureBox();
            this.optSignaturePad = new System.Windows.Forms.RadioButton();
            this.optBrowse = new System.Windows.Forms.RadioButton();
            this.grpBrowse = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSignatureImage = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbMaritualStatus = new System.Windows.Forms.ComboBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtMidleName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.Label39 = new System.Windows.Forms.Label();
            this.Label40 = new System.Windows.Forms.Label();
            this.Label41 = new System.Windows.Forms.Label();
            this.Label42 = new System.Windows.Forms.Label();
            this.Label43 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label34 = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtSSN = new System.Windows.Forms.MaskedTextBox();
            this.txtMobileNo = new System.Windows.Forms.MaskedTextBox();
            this.txtPhoneNo = new System.Windows.Forms.MaskedTextBox();
            this.dtDOB = new System.Windows.Forms.DateTimePicker();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label36 = new System.Windows.Forms.Label();
            this.Label37 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.dlgOpenSignature = new System.Windows.Forms.OpenFileDialog();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlUserDetails.SuspendLayout();
            this.pnl_UserHeader.SuspendLayout();
            this.pnl_tlsStrip.SuspendLayout();
            this.pnl_OtherInfo.SuspendLayout();
            this.pnlSignature.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).BeginInit();
            this.grpBrowse.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCoSign
            // 
            this.chkCoSign.AutoSize = true;
            this.chkCoSign.BackColor = System.Drawing.Color.Transparent;
            this.chkCoSign.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCoSign.Location = new System.Drawing.Point(434, 197);
            this.chkCoSign.Name = "chkCoSign";
            this.chkCoSign.Size = new System.Drawing.Size(133, 17);
            this.chkCoSign.TabIndex = 9;
            this.chkCoSign.Text = "Co- Signature Enabled";
            this.chkCoSign.UseVisualStyleBackColor = false;
            // 
            // trvUserRights
            // 
            this.trvUserRights.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvUserRights.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvUserRights.Location = new System.Drawing.Point(364, 255);
            this.trvUserRights.Name = "trvUserRights";
            this.trvUserRights.Size = new System.Drawing.Size(289, 277);
            this.trvUserRights.TabIndex = 42;
            // 
            // lstGroups
            // 
            this.lstGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstGroups.CheckOnClick = true;
            this.lstGroups.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstGroups.Location = new System.Drawing.Point(19, 255);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new System.Drawing.Size(291, 258);
            this.lstGroups.TabIndex = 40;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(364, 228);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(62, 13);
            this.Label5.TabIndex = 43;
            this.Label5.Text = "User &Rights";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(19, 234);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(69, 13);
            this.Label4.TabIndex = 36;
            this.Label4.Text = "User &Groups ";
            // 
            // chkAccessDenied
            // 
            this.chkAccessDenied.AutoSize = true;
            this.chkAccessDenied.BackColor = System.Drawing.Color.Transparent;
            this.chkAccessDenied.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAccessDenied.Location = new System.Drawing.Point(255, 197);
            this.chkAccessDenied.Name = "chkAccessDenied";
            this.chkAccessDenied.Size = new System.Drawing.Size(95, 17);
            this.chkAccessDenied.TabIndex = 8;
            this.chkAccessDenied.Text = "Access Denied";
            this.chkAccessDenied.UseVisualStyleBackColor = false;
            // 
            // chkAuditTrails
            // 
            this.chkAuditTrails.AutoSize = true;
            this.chkAuditTrails.BackColor = System.Drawing.Color.Transparent;
            this.chkAuditTrails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAuditTrails.Location = new System.Drawing.Point(50, 197);
            this.chkAuditTrails.Name = "chkAuditTrails";
            this.chkAuditTrails.Size = new System.Drawing.Size(120, 17);
            this.chkAuditTrails.TabIndex = 7;
            this.chkAuditTrails.Text = "Audit Trails Enabled";
            this.chkAuditTrails.UseVisualStyleBackColor = false;
            // 
            // chkgloPMSAdmin
            // 
            this.chkgloPMSAdmin.AutoSize = true;
            this.chkgloPMSAdmin.BackColor = System.Drawing.Color.Transparent;
            this.chkgloPMSAdmin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgloPMSAdmin.Location = new System.Drawing.Point(50, 167);
            this.chkgloPMSAdmin.Name = "chkgloPMSAdmin";
            this.chkgloPMSAdmin.Size = new System.Drawing.Size(127, 17);
            this.chkgloPMSAdmin.TabIndex = 6;
            this.chkgloPMSAdmin.Text = "gloPMS Administrator";
            this.chkgloPMSAdmin.UseVisualStyleBackColor = false;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(69, 130);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(63, 13);
            this.Label23.TabIndex = 45;
            this.Label23.Text = "Nick Name :";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(26, 100);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(100, 13);
            this.Label3.TabIndex = 44;
            this.Label3.Text = "Confirm Pass&word :";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(72, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(60, 13);
            this.Label2.TabIndex = 41;
            this.Label2.Text = "&Password :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(62, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(69, 13);
            this.Label1.TabIndex = 39;
            this.Label1.Text = "&Login Name :";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginName.Location = new System.Drawing.Point(173, 37);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(220, 21);
            this.txtLoginName.TabIndex = 1;
            // 
            // txtNickName
            // 
            this.txtNickName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNickName.Location = new System.Drawing.Point(173, 127);
            this.txtNickName.Name = "txtNickName";
            this.txtNickName.Size = new System.Drawing.Size(220, 21);
            this.txtNickName.TabIndex = 5;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(174, 97);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(220, 21);
            this.txtConfirmPassword.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(173, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(220, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(701, 53);
            this.ts_Commands.TabIndex = 32;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(40, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtn_UserDetails);
            this.panel1.Controls.Add(this.rbtn_otherInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 655);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 33);
            this.panel1.TabIndex = 17;
            // 
            // rbtn_UserDetails
            // 
            this.rbtn_UserDetails.AutoSize = true;
            this.rbtn_UserDetails.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_UserDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtn_UserDetails.Location = new System.Drawing.Point(489, 0);
            this.rbtn_UserDetails.Name = "rbtn_UserDetails";
            this.rbtn_UserDetails.Size = new System.Drawing.Size(88, 33);
            this.rbtn_UserDetails.TabIndex = 1;
            this.rbtn_UserDetails.Text = "User Details";
            this.rbtn_UserDetails.UseVisualStyleBackColor = false;
            this.rbtn_UserDetails.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChange);
            // 
            // rbtn_otherInfo
            // 
            this.rbtn_otherInfo.AutoSize = true;
            this.rbtn_otherInfo.BackColor = System.Drawing.Color.Transparent;
            this.rbtn_otherInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbtn_otherInfo.Location = new System.Drawing.Point(577, 0);
            this.rbtn_otherInfo.Name = "rbtn_otherInfo";
            this.rbtn_otherInfo.Size = new System.Drawing.Size(124, 33);
            this.rbtn_otherInfo.TabIndex = 12;
            this.rbtn_otherInfo.Text = "Other Information";
            this.rbtn_otherInfo.UseVisualStyleBackColor = false;
            this.rbtn_otherInfo.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChange);
            // 
            // pnlUserDetails
            // 
            this.pnlUserDetails.Controls.Add(this.chkApplyPwdSettings);
            this.pnlUserDetails.Controls.Add(this.btnBlock);
            this.pnlUserDetails.Controls.Add(this.btnExpandAll);
            this.pnlUserDetails.Controls.Add(this.btnSelectAll);
            this.pnlUserDetails.Controls.Add(this.btnNewGroup);
            this.pnlUserDetails.Controls.Add(this.cmbProvider);
            this.pnlUserDetails.Controls.Add(this.label8);
            this.pnlUserDetails.Controls.Add(this.pnl_UserHeader);
            this.pnlUserDetails.Controls.Add(this.txtPassword);
            this.pnlUserDetails.Controls.Add(this.Label3);
            this.pnlUserDetails.Controls.Add(this.txtConfirmPassword);
            this.pnlUserDetails.Controls.Add(this.Label1);
            this.pnlUserDetails.Controls.Add(this.txtNickName);
            this.pnlUserDetails.Controls.Add(this.Label2);
            this.pnlUserDetails.Controls.Add(this.txtLoginName);
            this.pnlUserDetails.Controls.Add(this.Label23);
            this.pnlUserDetails.Controls.Add(this.chkCoSign);
            this.pnlUserDetails.Controls.Add(this.trvUserRights);
            this.pnlUserDetails.Controls.Add(this.chkgloPMSAdmin);
            this.pnlUserDetails.Controls.Add(this.lstGroups);
            this.pnlUserDetails.Controls.Add(this.chkAuditTrails);
            this.pnlUserDetails.Controls.Add(this.Label5);
            this.pnlUserDetails.Controls.Add(this.chkAccessDenied);
            this.pnlUserDetails.Controls.Add(this.Label4);
            this.pnlUserDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlUserDetails.Name = "pnlUserDetails";
            this.pnlUserDetails.Size = new System.Drawing.Size(701, 688);
            this.pnlUserDetails.TabIndex = 18;
            // 
            // chkApplyPwdSettings
            // 
            this.chkApplyPwdSettings.AutoSize = true;
            this.chkApplyPwdSettings.BackColor = System.Drawing.Color.Transparent;
            this.chkApplyPwdSettings.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApplyPwdSettings.Location = new System.Drawing.Point(434, 98);
            this.chkApplyPwdSettings.Name = "chkApplyPwdSettings";
            this.chkApplyPwdSettings.Size = new System.Drawing.Size(144, 17);
            this.chkApplyPwdSettings.TabIndex = 4;
            this.chkApplyPwdSettings.Text = "Apply Password Settings";
            this.chkApplyPwdSettings.UseVisualStyleBackColor = false;
            // 
            // btnBlock
            // 
            this.btnBlock.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlock.Location = new System.Drawing.Point(542, 551);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(111, 26);
            this.btnBlock.TabIndex = 11;
            this.btnBlock.Text = "Block";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Visible = false;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.Location = new System.Drawing.Point(561, 223);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(92, 26);
            this.btnExpandAll.TabIndex = 58;
            this.btnExpandAll.Text = "Expand All";
            this.btnExpandAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(463, 223);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(94, 26);
            this.btnSelectAll.TabIndex = 57;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGroup.Location = new System.Drawing.Point(196, 223);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(101, 26);
            this.btnNewGroup.TabIndex = 56;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DisplayMember = "1";
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(132, 538);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(178, 21);
            this.cmbProvider.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(26, 542);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Provider Name :";
            // 
            // pnl_UserHeader
            // 
            this.pnl_UserHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_UserHeader.Controls.Add(this.lblUserHeader);
            this.pnl_UserHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_UserHeader.Location = new System.Drawing.Point(0, 0);
            this.pnl_UserHeader.Name = "pnl_UserHeader";
            this.pnl_UserHeader.Size = new System.Drawing.Size(701, 26);
            this.pnl_UserHeader.TabIndex = 53;
            // 
            // lblUserHeader
            // 
            this.lblUserHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserHeader.Location = new System.Drawing.Point(0, 0);
            this.lblUserHeader.Name = "lblUserHeader";
            this.lblUserHeader.Size = new System.Drawing.Size(131, 24);
            this.lblUserHeader.TabIndex = 0;
            this.lblUserHeader.Text = "User Details";
            this.lblUserHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_tlsStrip
            // 
            this.pnl_tlsStrip.Controls.Add(this.ts_Commands);
            this.pnl_tlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsStrip.Name = "pnl_tlsStrip";
            this.pnl_tlsStrip.Size = new System.Drawing.Size(701, 54);
            this.pnl_tlsStrip.TabIndex = 19;
            // 
            // pnl_OtherInfo
            // 
            this.pnl_OtherInfo.Controls.Add(this.pnlSignature);
            this.pnl_OtherInfo.Controls.Add(this.label26);
            this.pnl_OtherInfo.Controls.Add(this.label27);
            this.pnl_OtherInfo.Controls.Add(this.label28);
            this.pnl_OtherInfo.Controls.Add(this.cmbMaritualStatus);
            this.pnl_OtherInfo.Controls.Add(this.cmbGender);
            this.pnl_OtherInfo.Controls.Add(this.txtFax);
            this.pnl_OtherInfo.Controls.Add(this.txtEmail);
            this.pnl_OtherInfo.Controls.Add(this.txtCity);
            this.pnl_OtherInfo.Controls.Add(this.txtState);
            this.pnl_OtherInfo.Controls.Add(this.txtStreet);
            this.pnl_OtherInfo.Controls.Add(this.txtAddress2);
            this.pnl_OtherInfo.Controls.Add(this.label7);
            this.pnl_OtherInfo.Controls.Add(this.txtAddress1);
            this.pnl_OtherInfo.Controls.Add(this.txtMidleName);
            this.pnl_OtherInfo.Controls.Add(this.txtLastName);
            this.pnl_OtherInfo.Controls.Add(this.txtFirstName);
            this.pnl_OtherInfo.Controls.Add(this.Label39);
            this.pnl_OtherInfo.Controls.Add(this.Label40);
            this.pnl_OtherInfo.Controls.Add(this.Label41);
            this.pnl_OtherInfo.Controls.Add(this.Label42);
            this.pnl_OtherInfo.Controls.Add(this.Label43);
            this.pnl_OtherInfo.Controls.Add(this.Label44);
            this.pnl_OtherInfo.Controls.Add(this.Label30);
            this.pnl_OtherInfo.Controls.Add(this.Label31);
            this.pnl_OtherInfo.Controls.Add(this.Label32);
            this.pnl_OtherInfo.Controls.Add(this.Label33);
            this.pnl_OtherInfo.Controls.Add(this.Label34);
            this.pnl_OtherInfo.Controls.Add(this.txtZip);
            this.pnl_OtherInfo.Controls.Add(this.txtSSN);
            this.pnl_OtherInfo.Controls.Add(this.txtMobileNo);
            this.pnl_OtherInfo.Controls.Add(this.txtPhoneNo);
            this.pnl_OtherInfo.Controls.Add(this.dtDOB);
            this.pnl_OtherInfo.Controls.Add(this.Label35);
            this.pnl_OtherInfo.Controls.Add(this.Label36);
            this.pnl_OtherInfo.Controls.Add(this.Label37);
            this.pnl_OtherInfo.Controls.Add(this.panel3);
            this.pnl_OtherInfo.Controls.Add(this.lbl_BottomBrd);
            this.pnl_OtherInfo.Controls.Add(this.lbl_LeftBrd);
            this.pnl_OtherInfo.Controls.Add(this.lbl_RightBrd);
            this.pnl_OtherInfo.Controls.Add(this.lbl_TopBrd);
            this.pnl_OtherInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_OtherInfo.Location = new System.Drawing.Point(0, 54);
            this.pnl_OtherInfo.Name = "pnl_OtherInfo";
            this.pnl_OtherInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_OtherInfo.Size = new System.Drawing.Size(701, 601);
            this.pnl_OtherInfo.TabIndex = 20;
            // 
            // pnlSignature
            // 
            this.pnlSignature.BackColor = System.Drawing.Color.Transparent;
            this.pnlSignature.Controls.Add(this.btnClearImage);
            this.pnlSignature.Controls.Add(this.picSignature);
            this.pnlSignature.Controls.Add(this.optSignaturePad);
            this.pnlSignature.Controls.Add(this.optBrowse);
            this.pnlSignature.Controls.Add(this.grpBrowse);
            this.pnlSignature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSignature.Location = new System.Drawing.Point(22, 401);
            this.pnlSignature.Name = "pnlSignature";
            this.pnlSignature.Size = new System.Drawing.Size(644, 187);
            this.pnlSignature.TabIndex = 107;
            // 
            // btnClearImage
            // 
            this.btnClearImage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearImage.Location = new System.Drawing.Point(501, 65);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(74, 25);
            this.btnClearImage.TabIndex = 19;
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.UseVisualStyleBackColor = true;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // picSignature
            // 
            this.picSignature.Location = new System.Drawing.Point(8, 41);
            this.picSignature.Name = "picSignature";
            this.picSignature.Size = new System.Drawing.Size(487, 69);
            this.picSignature.TabIndex = 12;
            this.picSignature.TabStop = false;
            // 
            // optSignaturePad
            // 
            this.optSignaturePad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSignaturePad.Location = new System.Drawing.Point(206, 11);
            this.optSignaturePad.Name = "optSignaturePad";
            this.optSignaturePad.Size = new System.Drawing.Size(174, 19);
            this.optSignaturePad.TabIndex = 1;
            this.optSignaturePad.Text = "Signature Pad";
            // 
            // optBrowse
            // 
            this.optBrowse.Checked = true;
            this.optBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBrowse.Location = new System.Drawing.Point(19, 11);
            this.optBrowse.Name = "optBrowse";
            this.optBrowse.Size = new System.Drawing.Size(169, 19);
            this.optBrowse.TabIndex = 0;
            this.optBrowse.TabStop = true;
            this.optBrowse.Text = "Browse From File";
            // 
            // grpBrowse
            // 
            this.grpBrowse.Controls.Add(this.btnBrowse);
            this.grpBrowse.Controls.Add(this.txtSignatureImage);
            this.grpBrowse.Controls.Add(this.Label38);
            this.grpBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpBrowse.Location = new System.Drawing.Point(9, 114);
            this.grpBrowse.Name = "grpBrowse";
            this.grpBrowse.Size = new System.Drawing.Size(572, 60);
            this.grpBrowse.TabIndex = 13;
            this.grpBrowse.TabStop = false;
            this.grpBrowse.Text = "Browse from File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(432, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(23, 23);
            this.btnBrowse.TabIndex = 31;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSignatureImage
            // 
            this.txtSignatureImage.Location = new System.Drawing.Point(86, 21);
            this.txtSignatureImage.Name = "txtSignatureImage";
            this.txtSignatureImage.Size = new System.Drawing.Size(341, 22);
            this.txtSignatureImage.TabIndex = 17;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label38.Location = new System.Drawing.Point(17, 25);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(67, 14);
            this.Label38.TabIndex = 16;
            this.Label38.Text = "File Name :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(478, 78);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(55, 11);
            this.label26.TabIndex = 106;
            this.label26.Text = "(Last Name)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(313, 78);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(66, 11);
            this.label27.TabIndex = 105;
            this.label27.Text = "(Middle Name)";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(156, 78);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(57, 11);
            this.label28.TabIndex = 104;
            this.label28.Text = "(First Name)";
            // 
            // cmbMaritualStatus
            // 
            this.cmbMaritualStatus.FormattingEnabled = true;
            this.cmbMaritualStatus.Items.AddRange(new object[] {
            "Married",
            "UnMarried",
            "Single",
            "Widowed",
            " Divorced"});
            this.cmbMaritualStatus.Location = new System.Drawing.Point(468, 357);
            this.cmbMaritualStatus.Name = "cmbMaritualStatus";
            this.cmbMaritualStatus.Size = new System.Drawing.Size(129, 22);
            this.cmbMaritualStatus.TabIndex = 30;
            // 
            // cmbGender
            // 
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(117, 360);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(86, 22);
            this.cmbGender.TabIndex = 29;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(117, 284);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(193, 22);
            this.txtFax.TabIndex = 24;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(468, 283);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(129, 22);
            this.txtEmail.TabIndex = 25;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(468, 170);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(129, 22);
            this.txtCity.TabIndex = 19;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(117, 208);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(194, 22);
            this.txtState.TabIndex = 20;
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(117, 170);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(194, 22);
            this.txtStreet.TabIndex = 18;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(117, 132);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(286, 22);
            this.txtAddress2.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 14);
            this.label7.TabIndex = 95;
            this.label7.Text = "Address 2 :";
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(117, 94);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(286, 22);
            this.txtAddress1.TabIndex = 16;
            // 
            // txtMidleName
            // 
            this.txtMidleName.Location = new System.Drawing.Point(278, 56);
            this.txtMidleName.Name = "txtMidleName";
            this.txtMidleName.Size = new System.Drawing.Size(144, 22);
            this.txtMidleName.TabIndex = 14;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(453, 56);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(144, 22);
            this.txtLastName.TabIndex = 15;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(117, 56);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(144, 22);
            this.txtFirstName.TabIndex = 13;
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.BackColor = System.Drawing.Color.Transparent;
            this.Label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label39.Location = new System.Drawing.Point(423, 287);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(42, 14);
            this.Label39.TabIndex = 87;
            this.Label39.Text = "Email :";
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.BackColor = System.Drawing.Color.Transparent;
            this.Label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label40.Location = new System.Drawing.Point(397, 250);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(68, 14);
            this.Label40.TabIndex = 86;
            this.Label40.Text = "Mobile No :";
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.BackColor = System.Drawing.Color.Transparent;
            this.Label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(432, 213);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(33, 14);
            this.Label41.TabIndex = 85;
            this.Label41.Text = "ZIP :";
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.BackColor = System.Drawing.Color.Transparent;
            this.Label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label42.Location = new System.Drawing.Point(430, 176);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(35, 14);
            this.Label42.TabIndex = 84;
            this.Label42.Text = "City :";
            // 
            // Label43
            // 
            this.Label43.AutoSize = true;
            this.Label43.BackColor = System.Drawing.Color.Transparent;
            this.Label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label43.Location = new System.Drawing.Point(377, 361);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(88, 14);
            this.Label43.TabIndex = 89;
            this.Label43.Text = "Marital Status :";
            // 
            // Label44
            // 
            this.Label44.AutoSize = true;
            this.Label44.BackColor = System.Drawing.Color.Transparent;
            this.Label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label44.Location = new System.Drawing.Point(380, 324);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(85, 14);
            this.Label44.TabIndex = 88;
            this.Label44.Text = "Date of Birth :";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.BackColor = System.Drawing.Color.Transparent;
            this.Label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(78, 288);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(36, 14);
            this.Label30.TabIndex = 81;
            this.Label30.Text = "FAX :";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.BackColor = System.Drawing.Color.Transparent;
            this.Label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label31.Location = new System.Drawing.Point(62, 250);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(52, 14);
            this.Label31.TabIndex = 80;
            this.Label31.Text = "Ph. No :";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.Transparent;
            this.Label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label32.Location = new System.Drawing.Point(69, 213);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(45, 14);
            this.Label32.TabIndex = 79;
            this.Label32.Text = "State :";
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(64, 174);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(50, 14);
            this.Label33.TabIndex = 78;
            this.Label33.Text = "Street :";
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.BackColor = System.Drawing.Color.Transparent;
            this.Label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.Location = new System.Drawing.Point(45, 98);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(69, 14);
            this.Label34.TabIndex = 77;
            this.Label34.Text = "Address 1 :";
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(468, 210);
            this.txtZip.MaxLength = 10;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(129, 22);
            this.txtZip.TabIndex = 21;
            // 
            // txtSSN
            // 
            this.txtSSN.Location = new System.Drawing.Point(117, 322);
            this.txtSSN.Mask = "000-00-0000";
            this.txtSSN.Name = "txtSSN";
            this.txtSSN.Size = new System.Drawing.Size(193, 22);
            this.txtSSN.TabIndex = 26;
            this.txtSSN.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Location = new System.Drawing.Point(468, 247);
            this.txtMobileNo.Mask = "(999) 000-0000";
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(129, 22);
            this.txtMobileNo.TabIndex = 23;
            this.txtMobileNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.Location = new System.Drawing.Point(117, 246);
            this.txtPhoneNo.Mask = "(999) 000-0000";
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Size = new System.Drawing.Size(193, 22);
            this.txtPhoneNo.TabIndex = 22;
            this.txtPhoneNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // dtDOB
            // 
            this.dtDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtDOB.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDOB.Location = new System.Drawing.Point(468, 320);
            this.dtDOB.Name = "dtDOB";
            this.dtDOB.Size = new System.Drawing.Size(129, 22);
            this.dtDOB.TabIndex = 28;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.BackColor = System.Drawing.Color.Transparent;
            this.Label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.Location = new System.Drawing.Point(59, 364);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(55, 14);
            this.Label35.TabIndex = 83;
            this.Label35.Text = "Gender :";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.BackColor = System.Drawing.Color.Transparent;
            this.Label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.Location = new System.Drawing.Point(58, 326);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(56, 14);
            this.Label36.TabIndex = 82;
            this.Label36.Text = "SSN No :";
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.BackColor = System.Drawing.Color.Transparent;
            this.Label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label37.Location = new System.Drawing.Point(68, 60);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(46, 14);
            this.Label37.TabIndex = 76;
            this.Label37.Text = "Name :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(693, 26);
            this.panel3.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(693, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "  Other Information";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 597);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(693, 1);
            this.lbl_BottomBrd.TabIndex = 111;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 594);
            this.lbl_LeftBrd.TabIndex = 110;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(697, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 594);
            this.lbl_RightBrd.TabIndex = 109;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(695, 1);
            this.lbl_TopBrd.TabIndex = 108;
            this.lbl_TopBrd.Text = "label1";
            // 
            // dlgOpenSignature
            // 
            this.dlgOpenSignature.FileName = "openFileDialog1";
            // 
            // frmUserMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(701, 688);
            this.Controls.Add(this.pnl_OtherInfo);
            this.Controls.Add(this.pnl_tlsStrip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlUserDetails);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserMgt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.frmUserMgt_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlUserDetails.ResumeLayout(false);
            this.pnlUserDetails.PerformLayout();
            this.pnl_UserHeader.ResumeLayout(false);
            this.pnl_tlsStrip.ResumeLayout(false);
            this.pnl_tlsStrip.PerformLayout();
            this.pnl_OtherInfo.ResumeLayout(false);
            this.pnl_OtherInfo.PerformLayout();
            this.pnlSignature.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).EndInit();
            this.grpBrowse.ResumeLayout(false);
            this.grpBrowse.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtNickName;
        private System.Windows.Forms.TextBox txtLoginName;
        internal System.Windows.Forms.CheckBox chkCoSign;
        internal System.Windows.Forms.TreeView trvUserRights;
        internal System.Windows.Forms.CheckedListBox lstGroups;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.CheckBox chkAccessDenied;
        internal System.Windows.Forms.CheckBox chkAuditTrails;
        internal System.Windows.Forms.CheckBox chkgloPMSAdmin;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtn_otherInfo;
        private System.Windows.Forms.Panel pnlUserDetails;
        private System.Windows.Forms.Panel pnl_tlsStrip;
        private System.Windows.Forms.Panel pnl_UserHeader;
        private System.Windows.Forms.Label lblUserHeader;
        private System.Windows.Forms.Panel pnl_OtherInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtMidleName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        internal System.Windows.Forms.Label Label39;
        internal System.Windows.Forms.Label Label40;
        internal System.Windows.Forms.Label Label41;
        internal System.Windows.Forms.Label Label42;
        internal System.Windows.Forms.Label Label43;
        internal System.Windows.Forms.Label Label44;
        internal System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Label Label31;
        internal System.Windows.Forms.Label Label32;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.TextBox txtZip;
        internal System.Windows.Forms.MaskedTextBox txtSSN;
        internal System.Windows.Forms.MaskedTextBox txtMobileNo;
        internal System.Windows.Forms.MaskedTextBox txtPhoneNo;
        internal System.Windows.Forms.DateTimePicker dtDOB;
        internal System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.Label Label36;
        internal System.Windows.Forms.Label Label37;
        private System.Windows.Forms.ComboBox cmbMaritualStatus;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtFax;
        internal System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.TextBox txtAddress2;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.Label label28;
        private System.Windows.Forms.RadioButton rbtn_UserDetails;
        private System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnExpandAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnNewGroup;
        internal System.Windows.Forms.Panel pnlSignature;
        internal System.Windows.Forms.PictureBox picSignature;
        internal System.Windows.Forms.RadioButton optSignaturePad;
        internal System.Windows.Forms.RadioButton optBrowse;
        internal System.Windows.Forms.GroupBox grpBrowse;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSignatureImage;
        internal System.Windows.Forms.Label Label38;
        private System.Windows.Forms.OpenFileDialog dlgOpenSignature;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnBlock;
        internal System.Windows.Forms.CheckBox chkApplyPwdSettings;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;

    }
}