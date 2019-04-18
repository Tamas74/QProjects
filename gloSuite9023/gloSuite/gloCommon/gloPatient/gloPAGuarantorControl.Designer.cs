namespace gloPatient
{
    partial class gloPAGuarantorControl
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
                    if (oPatientGuarantors != null)
                    {
                        oPatientGuarantors.Dispose();
                        oPatientGuarantors = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oListControl != null)
                    {
                        oListControl.Dispose();
                        oListControl = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (objgloAccount != null)
                    {
                        objgloAccount.Dispose();
                        objgloAccount = null;
                    }
                }
                catch
                {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPAGuarantorControl));
            this.pnlGaurantorInfo = new System.Windows.Forms.Panel();
            this.pnlGIContactDetails = new System.Windows.Forms.Panel();
            this.mskGIMobile = new gloMaskControl.gloMaskBox();
            this.mskGIPhone = new gloMaskControl.gloMaskBox();
            this.txtGIEmail = new System.Windows.Forms.TextBox();
            this.txtGIFax = new gloMaskControl.gloMaskBox();
            this.lblGIEmail = new System.Windows.Forms.Label();
            this.lblGIFax = new System.Windows.Forms.Label();
            this.lblGIMobile = new System.Windows.Forms.Label();
            this.lblGIPhone = new System.Windows.Forms.Label();
            this.lblGIContactDetails = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.pnlGIAddressDetails = new System.Windows.Forms.Panel();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.cmbGICountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.txtGICounty = new System.Windows.Forms.TextBox();
            this.txtGIZip = new System.Windows.Forms.TextBox();
            this.txtGICity = new System.Windows.Forms.TextBox();
            this.txtGIAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtGIAddressLine1 = new System.Windows.Forms.TextBox();
            this.lblGIAddressDetails = new System.Windows.Forms.Label();
            this.lblGIZip = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblGIState = new System.Windows.Forms.Label();
            this.lblGICity = new System.Windows.Forms.Label();
            this.lblGIAddressLine2 = new System.Windows.Forms.Label();
            this.lblGIAddressLine1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlGIPersonalDetails = new System.Windows.Forms.Panel();
            this.pnlPersonalGuarantor = new System.Windows.Forms.Panel();
            this.lblGIRelation = new System.Windows.Forms.Label();
            this.cmbGIRelation = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.mtxtGISSN = new gloMaskControl.gloMaskBox();
            this.lblGIPatName = new System.Windows.Forms.Label();
            this.lblGIDOB = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblGIFName = new System.Windows.Forms.Label();
            this.lblGIPatMName = new System.Windows.Forms.Label();
            this.lblGIPatLName = new System.Windows.Forms.Label();
            this.grpboxGIGender = new System.Windows.Forms.GroupBox();
            this.radbtnGIOthers = new System.Windows.Forms.RadioButton();
            this.radbtnGIFemale = new System.Windows.Forms.RadioButton();
            this.radbtnGIMale = new System.Windows.Forms.RadioButton();
            this.txtGIPatFName = new System.Windows.Forms.TextBox();
            this.mskGIDOB = new System.Windows.Forms.MaskedTextBox();
            this.txtGIPatLName = new System.Windows.Forms.TextBox();
            this.txtGIMName = new System.Windows.Forms.TextBox();
            this.pnlCommercialGuarantor = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.lblCommercialName = new System.Windows.Forms.Label();
            this.txtCommercialName = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCommercialGuarantor = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.rbSecondary = new System.Windows.Forms.RadioButton();
            this.rbPrimary = new System.Windows.Forms.RadioButton();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.rbTertiary = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTreeView = new System.Windows.Forms.Panel();
            this.trvGuarantors = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsb_SelectGuarantor = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGIHeader = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblGIHeader = new System.Windows.Forms.Label();
            this.pnlGaurantorInfo.SuspendLayout();
            this.pnlGIContactDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlGIAddressDetails.SuspendLayout();
            this.pnlGIPersonalDetails.SuspendLayout();
            this.pnlPersonalGuarantor.SuspendLayout();
            this.grpboxGIGender.SuspendLayout();
            this.pnlCommercialGuarantor.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlTreeView.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlGIHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGaurantorInfo
            // 
            this.pnlGaurantorInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlGaurantorInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGaurantorInfo.Controls.Add(this.pnlGIContactDetails);
            this.pnlGaurantorInfo.Controls.Add(this.panel2);
            this.pnlGaurantorInfo.Controls.Add(this.pnlGIAddressDetails);
            this.pnlGaurantorInfo.Controls.Add(this.pnlGIPersonalDetails);
            this.pnlGaurantorInfo.Controls.Add(this.splitter1);
            this.pnlGaurantorInfo.Controls.Add(this.pnlTreeView);
            this.pnlGaurantorInfo.Controls.Add(this.pnlTOP);
            this.pnlGaurantorInfo.Controls.Add(this.panel1);
            this.pnlGaurantorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGaurantorInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlGaurantorInfo.Name = "pnlGaurantorInfo";
            this.pnlGaurantorInfo.Size = new System.Drawing.Size(756, 653);
            this.pnlGaurantorInfo.TabIndex = 12;
            this.pnlGaurantorInfo.Visible = false;
            // 
            // pnlGIContactDetails
            // 
            this.pnlGIContactDetails.Controls.Add(this.mskGIMobile);
            this.pnlGIContactDetails.Controls.Add(this.mskGIPhone);
            this.pnlGIContactDetails.Controls.Add(this.txtGIEmail);
            this.pnlGIContactDetails.Controls.Add(this.txtGIFax);
            this.pnlGIContactDetails.Controls.Add(this.lblGIEmail);
            this.pnlGIContactDetails.Controls.Add(this.lblGIFax);
            this.pnlGIContactDetails.Controls.Add(this.lblGIMobile);
            this.pnlGIContactDetails.Controls.Add(this.lblGIPhone);
            this.pnlGIContactDetails.Controls.Add(this.lblGIContactDetails);
            this.pnlGIContactDetails.Controls.Add(this.lbl_BottomBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_LeftBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_RightBrd);
            this.pnlGIContactDetails.Controls.Add(this.lbl_TopBrd);
            this.pnlGIContactDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGIContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIContactDetails.Location = new System.Drawing.Point(3, 447);
            this.pnlGIContactDetails.Name = "pnlGIContactDetails";
            this.pnlGIContactDetails.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlGIContactDetails.Size = new System.Drawing.Size(753, 179);
            this.pnlGIContactDetails.TabIndex = 3;
            // 
            // mskGIMobile
            // 
            this.mskGIMobile.AllowValidate = true;
            this.mskGIMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGIMobile.IncludeLiteralsAndPrompts = false;
            this.mskGIMobile.Location = new System.Drawing.Point(178, 56);
            this.mskGIMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mskGIMobile.Name = "mskGIMobile";
            this.mskGIMobile.ReadOnly = false;
            this.mskGIMobile.Size = new System.Drawing.Size(96, 22);
            this.mskGIMobile.TabIndex = 24;
            // 
            // mskGIPhone
            // 
            this.mskGIPhone.AllowValidate = true;
            this.mskGIPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskGIPhone.IncludeLiteralsAndPrompts = false;
            this.mskGIPhone.Location = new System.Drawing.Point(178, 27);
            this.mskGIPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskGIPhone.Name = "mskGIPhone";
            this.mskGIPhone.ReadOnly = false;
            this.mskGIPhone.Size = new System.Drawing.Size(96, 22);
            this.mskGIPhone.TabIndex = 23;
            // 
            // txtGIEmail
            // 
            this.txtGIEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIEmail.ForeColor = System.Drawing.Color.Black;
            this.txtGIEmail.Location = new System.Drawing.Point(178, 114);
            this.txtGIEmail.Name = "txtGIEmail";
            this.txtGIEmail.Size = new System.Drawing.Size(341, 22);
            this.txtGIEmail.TabIndex = 26;
            this.txtGIEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtPAEmail_Validating);
            // 
            // txtGIFax
            // 
            this.txtGIFax.AllowValidate = true;
            this.txtGIFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIFax.ForeColor = System.Drawing.Color.Black;
            this.txtGIFax.IncludeLiteralsAndPrompts = false;
            this.txtGIFax.Location = new System.Drawing.Point(178, 85);
            this.txtGIFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtGIFax.Name = "txtGIFax";
            this.txtGIFax.ReadOnly = false;
            this.txtGIFax.Size = new System.Drawing.Size(96, 22);
            this.txtGIFax.TabIndex = 25;
            // 
            // lblGIEmail
            // 
            this.lblGIEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIEmail.AutoEllipsis = true;
            this.lblGIEmail.AutoSize = true;
            this.lblGIEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIEmail.Location = new System.Drawing.Point(131, 118);
            this.lblGIEmail.Name = "lblGIEmail";
            this.lblGIEmail.Size = new System.Drawing.Size(42, 14);
            this.lblGIEmail.TabIndex = 20;
            this.lblGIEmail.Text = "Email :";
            // 
            // lblGIFax
            // 
            this.lblGIFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIFax.AutoEllipsis = true;
            this.lblGIFax.AutoSize = true;
            this.lblGIFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIFax.Location = new System.Drawing.Point(140, 89);
            this.lblGIFax.Name = "lblGIFax";
            this.lblGIFax.Size = new System.Drawing.Size(33, 14);
            this.lblGIFax.TabIndex = 19;
            this.lblGIFax.Text = "Fax :";
            // 
            // lblGIMobile
            // 
            this.lblGIMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIMobile.AutoEllipsis = true;
            this.lblGIMobile.AutoSize = true;
            this.lblGIMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIMobile.Location = new System.Drawing.Point(124, 60);
            this.lblGIMobile.Name = "lblGIMobile";
            this.lblGIMobile.Size = new System.Drawing.Size(49, 14);
            this.lblGIMobile.TabIndex = 22;
            this.lblGIMobile.Text = "Mobile :";
            // 
            // lblGIPhone
            // 
            this.lblGIPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIPhone.AutoEllipsis = true;
            this.lblGIPhone.AutoSize = true;
            this.lblGIPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIPhone.Location = new System.Drawing.Point(123, 31);
            this.lblGIPhone.Name = "lblGIPhone";
            this.lblGIPhone.Size = new System.Drawing.Size(50, 14);
            this.lblGIPhone.TabIndex = 21;
            this.lblGIPhone.Text = "Phone :";
            // 
            // lblGIContactDetails
            // 
            this.lblGIContactDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGIContactDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGIContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIContactDetails.Location = new System.Drawing.Point(1, 1);
            this.lblGIContactDetails.Name = "lblGIContactDetails";
            this.lblGIContactDetails.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGIContactDetails.Size = new System.Drawing.Size(748, 18);
            this.lblGIContactDetails.TabIndex = 18;
            this.lblGIContactDetails.Text = " Contact Details :";
            this.lblGIContactDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 175);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(748, 1);
            this.lbl_BottomBrd.TabIndex = 30;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 175);
            this.lbl_LeftBrd.TabIndex = 29;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(749, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 175);
            this.lbl_RightBrd.TabIndex = 28;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(750, 1);
            this.lbl_TopBrd.TabIndex = 27;
            this.lbl_TopBrd.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label48);
            this.panel2.Controls.Add(this.label46);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 626);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(753, 27);
            this.panel2.TabIndex = 69;
            // 
            // label48
            // 
            this.label48.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Red;
            this.label48.Location = new System.Drawing.Point(12, 4);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(14, 14);
            this.label48.TabIndex = 33;
            this.label48.Text = "*";
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.AutoEllipsis = true;
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(25, 4);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(90, 14);
            this.label46.TabIndex = 31;
            this.label46.Text = "Required fields ";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label42.Location = new System.Drawing.Point(1, 23);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(748, 1);
            this.label42.TabIndex = 12;
            this.label42.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(0, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 23);
            this.label43.TabIndex = 11;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(749, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 23);
            this.label44.TabIndex = 10;
            this.label44.Text = "label3";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(750, 1);
            this.label45.TabIndex = 9;
            this.label45.Text = "label1";
            // 
            // pnlGIAddressDetails
            // 
            this.pnlGIAddressDetails.Controls.Add(this.pnlAddresssControl);
            this.pnlGIAddressDetails.Controls.Add(this.cmbGICountry);
            this.pnlGIAddressDetails.Controls.Add(this.label49);
            this.pnlGIAddressDetails.Controls.Add(this.cmbState);
            this.pnlGIAddressDetails.Controls.Add(this.txtGICounty);
            this.pnlGIAddressDetails.Controls.Add(this.txtGIZip);
            this.pnlGIAddressDetails.Controls.Add(this.txtGICity);
            this.pnlGIAddressDetails.Controls.Add(this.txtGIAddressLine2);
            this.pnlGIAddressDetails.Controls.Add(this.txtGIAddressLine1);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressDetails);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIZip);
            this.pnlGIAddressDetails.Controls.Add(this.label22);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIState);
            this.pnlGIAddressDetails.Controls.Add(this.lblGICity);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressLine2);
            this.pnlGIAddressDetails.Controls.Add(this.lblGIAddressLine1);
            this.pnlGIAddressDetails.Controls.Add(this.label1);
            this.pnlGIAddressDetails.Controls.Add(this.label2);
            this.pnlGIAddressDetails.Controls.Add(this.label3);
            this.pnlGIAddressDetails.Controls.Add(this.label25);
            this.pnlGIAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIAddressDetails.Location = new System.Drawing.Point(3, 268);
            this.pnlGIAddressDetails.Name = "pnlGIAddressDetails";
            this.pnlGIAddressDetails.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlGIAddressDetails.Size = new System.Drawing.Size(753, 179);
            this.pnlGIAddressDetails.TabIndex = 2;
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(96, 25);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlAddresssControl.TabIndex = 108;
            // 
            // cmbGICountry
            // 
            this.cmbGICountry.FormattingEnabled = true;
            this.cmbGICountry.Items.AddRange(new object[] {
            "US"});
            this.cmbGICountry.Location = new System.Drawing.Point(175, 144);
            this.cmbGICountry.MaxDropDownItems = 3;
            this.cmbGICountry.MaxLength = 20;
            this.cmbGICountry.Name = "cmbGICountry";
            this.cmbGICountry.Size = new System.Drawing.Size(118, 22);
            this.cmbGICountry.TabIndex = 22;
            this.cmbGICountry.Visible = false;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(117, 148);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 28;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(357, 89);
            this.cmbState.MaxLength = 20;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(98, 22);
            this.cmbState.TabIndex = 20;
            this.cmbState.Visible = false;
            // 
            // txtGICounty
            // 
            this.txtGICounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGICounty.ForeColor = System.Drawing.Color.Black;
            this.txtGICounty.Location = new System.Drawing.Point(357, 117);
            this.txtGICounty.MaxLength = 20;
            this.txtGICounty.Name = "txtGICounty";
            this.txtGICounty.Size = new System.Drawing.Size(98, 22);
            this.txtGICounty.TabIndex = 21;
            this.txtGICounty.Tag = "";
            this.txtGICounty.Visible = false;
            this.txtGICounty.Leave += new System.EventHandler(this.txtGIZip_Leave);
            // 
            // txtGIZip
            // 
            this.txtGIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIZip.ForeColor = System.Drawing.Color.Black;
            this.txtGIZip.Location = new System.Drawing.Point(175, 117);
            this.txtGIZip.MaxLength = 10;
            this.txtGIZip.Name = "txtGIZip";
            this.txtGIZip.Size = new System.Drawing.Size(118, 22);
            this.txtGIZip.TabIndex = 18;
            this.txtGIZip.Tag = "";
            this.txtGIZip.Visible = false;
            this.txtGIZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGIZip_KeyPress);
            this.txtGIZip.Leave += new System.EventHandler(this.txtGIZip_Leave);
            // 
            // txtGICity
            // 
            this.txtGICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGICity.ForeColor = System.Drawing.Color.Black;
            this.txtGICity.Location = new System.Drawing.Point(175, 91);
            this.txtGICity.Name = "txtGICity";
            this.txtGICity.Size = new System.Drawing.Size(118, 22);
            this.txtGICity.TabIndex = 19;
            this.txtGICity.Tag = "19";
            this.txtGICity.Visible = false;
            // 
            // txtGIAddressLine2
            // 
            this.txtGIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIAddressLine2.ForeColor = System.Drawing.Color.Black;
            this.txtGIAddressLine2.Location = new System.Drawing.Point(175, 64);
            this.txtGIAddressLine2.Name = "txtGIAddressLine2";
            this.txtGIAddressLine2.Size = new System.Drawing.Size(279, 22);
            this.txtGIAddressLine2.TabIndex = 17;
            this.txtGIAddressLine2.Visible = false;
            // 
            // txtGIAddressLine1
            // 
            this.txtGIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIAddressLine1.ForeColor = System.Drawing.Color.Black;
            this.txtGIAddressLine1.Location = new System.Drawing.Point(175, 40);
            this.txtGIAddressLine1.Name = "txtGIAddressLine1";
            this.txtGIAddressLine1.Size = new System.Drawing.Size(280, 22);
            this.txtGIAddressLine1.TabIndex = 16;
            this.txtGIAddressLine1.Visible = false;
            // 
            // lblGIAddressDetails
            // 
            this.lblGIAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGIAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGIAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressDetails.Location = new System.Drawing.Point(1, 1);
            this.lblGIAddressDetails.Name = "lblGIAddressDetails";
            this.lblGIAddressDetails.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblGIAddressDetails.Size = new System.Drawing.Size(748, 24);
            this.lblGIAddressDetails.TabIndex = 0;
            this.lblGIAddressDetails.Text = " Address Details :";
            this.lblGIAddressDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGIZip
            // 
            this.lblGIZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIZip.AutoEllipsis = true;
            this.lblGIZip.AutoSize = true;
            this.lblGIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIZip.Location = new System.Drawing.Point(140, 120);
            this.lblGIZip.Name = "lblGIZip";
            this.lblGIZip.Size = new System.Drawing.Size(31, 14);
            this.lblGIZip.TabIndex = 6;
            this.lblGIZip.Text = "Zip :";
            this.lblGIZip.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(303, 120);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 14);
            this.label22.TabIndex = 5;
            this.label22.Text = "County :";
            this.label22.Visible = false;
            // 
            // lblGIState
            // 
            this.lblGIState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIState.AutoEllipsis = true;
            this.lblGIState.AutoSize = true;
            this.lblGIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIState.Location = new System.Drawing.Point(312, 94);
            this.lblGIState.Name = "lblGIState";
            this.lblGIState.Size = new System.Drawing.Size(45, 14);
            this.lblGIState.TabIndex = 5;
            this.lblGIState.Text = "State :";
            this.lblGIState.Visible = false;
            // 
            // lblGICity
            // 
            this.lblGICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGICity.AutoEllipsis = true;
            this.lblGICity.AutoSize = true;
            this.lblGICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGICity.Location = new System.Drawing.Point(136, 93);
            this.lblGICity.Name = "lblGICity";
            this.lblGICity.Size = new System.Drawing.Size(35, 14);
            this.lblGICity.TabIndex = 10;
            this.lblGICity.Text = "City :";
            this.lblGICity.Visible = false;
            // 
            // lblGIAddressLine2
            // 
            this.lblGIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIAddressLine2.AutoEllipsis = true;
            this.lblGIAddressLine2.AutoSize = true;
            this.lblGIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressLine2.Location = new System.Drawing.Point(76, 67);
            this.lblGIAddressLine2.Name = "lblGIAddressLine2";
            this.lblGIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblGIAddressLine2.TabIndex = 9;
            this.lblGIAddressLine2.Text = "Address Line 2 :";
            this.lblGIAddressLine2.Visible = false;
            // 
            // lblGIAddressLine1
            // 
            this.lblGIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIAddressLine1.AutoEllipsis = true;
            this.lblGIAddressLine1.AutoSize = true;
            this.lblGIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIAddressLine1.Location = new System.Drawing.Point(76, 43);
            this.lblGIAddressLine1.Name = "lblGIAddressLine1";
            this.lblGIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblGIAddressLine1.TabIndex = 8;
            this.lblGIAddressLine1.Text = "Address Line 1 :";
            this.lblGIAddressLine1.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(748, 1);
            this.label1.TabIndex = 25;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 175);
            this.label2.TabIndex = 24;
            this.label2.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(749, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 175);
            this.label3.TabIndex = 23;
            this.label3.Text = "label3";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(750, 1);
            this.label25.TabIndex = 109;
            this.label25.Text = "label2";
            // 
            // pnlGIPersonalDetails
            // 
            this.pnlGIPersonalDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlGIPersonalDetails.Controls.Add(this.pnlPersonalGuarantor);
            this.pnlGIPersonalDetails.Controls.Add(this.pnlCommercialGuarantor);
            this.pnlGIPersonalDetails.Controls.Add(this.panel4);
            this.pnlGIPersonalDetails.Controls.Add(this.label5);
            this.pnlGIPersonalDetails.Controls.Add(this.label6);
            this.pnlGIPersonalDetails.Controls.Add(this.label7);
            this.pnlGIPersonalDetails.Controls.Add(this.label8);
            this.pnlGIPersonalDetails.Controls.Add(this.pnlStatus);
            this.pnlGIPersonalDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIPersonalDetails.Location = new System.Drawing.Point(3, 85);
            this.pnlGIPersonalDetails.Name = "pnlGIPersonalDetails";
            this.pnlGIPersonalDetails.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlGIPersonalDetails.Size = new System.Drawing.Size(753, 183);
            this.pnlGIPersonalDetails.TabIndex = 0;
            this.pnlGIPersonalDetails.TabStop = true;
            // 
            // pnlPersonalGuarantor
            // 
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIRelation);
            this.pnlPersonalGuarantor.Controls.Add(this.cmbGIRelation);
            this.pnlPersonalGuarantor.Controls.Add(this.label23);
            this.pnlPersonalGuarantor.Controls.Add(this.mtxtGISSN);
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIPatName);
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIDOB);
            this.pnlPersonalGuarantor.Controls.Add(this.label19);
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIFName);
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIPatMName);
            this.pnlPersonalGuarantor.Controls.Add(this.lblGIPatLName);
            this.pnlPersonalGuarantor.Controls.Add(this.grpboxGIGender);
            this.pnlPersonalGuarantor.Controls.Add(this.txtGIPatFName);
            this.pnlPersonalGuarantor.Controls.Add(this.mskGIDOB);
            this.pnlPersonalGuarantor.Controls.Add(this.txtGIPatLName);
            this.pnlPersonalGuarantor.Controls.Add(this.txtGIMName);
            this.pnlPersonalGuarantor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPersonalGuarantor.Location = new System.Drawing.Point(1, 32);
            this.pnlPersonalGuarantor.Name = "pnlPersonalGuarantor";
            this.pnlPersonalGuarantor.Size = new System.Drawing.Size(748, 147);
            this.pnlPersonalGuarantor.TabIndex = 1;
            // 
            // lblGIRelation
            // 
            this.lblGIRelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGIRelation.AutoEllipsis = true;
            this.lblGIRelation.AutoSize = true;
            this.lblGIRelation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIRelation.Location = new System.Drawing.Point(116, 120);
            this.lblGIRelation.Name = "lblGIRelation";
            this.lblGIRelation.Size = new System.Drawing.Size(58, 14);
            this.lblGIRelation.TabIndex = 37;
            this.lblGIRelation.Text = "Relation :";
            // 
            // cmbGIRelation
            // 
            this.cmbGIRelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbGIRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGIRelation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGIRelation.ForeColor = System.Drawing.Color.Black;
            this.cmbGIRelation.FormattingEnabled = true;
            this.cmbGIRelation.Location = new System.Drawing.Point(178, 116);
            this.cmbGIRelation.Name = "cmbGIRelation";
            this.cmbGIRelation.Size = new System.Drawing.Size(169, 22);
            this.cmbGIRelation.TabIndex = 10;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(116, 17);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(14, 14);
            this.label23.TabIndex = 34;
            this.label23.Text = "*";
            // 
            // mtxtGISSN
            // 
            this.mtxtGISSN.AllowValidate = true;
            this.mtxtGISSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtGISSN.IncludeLiteralsAndPrompts = false;
            this.mtxtGISSN.Location = new System.Drawing.Point(177, 86);
            this.mtxtGISSN.MaskType = gloMaskControl.gloMaskType.SSN;
            this.mtxtGISSN.Name = "mtxtGISSN";
            this.mtxtGISSN.ReadOnly = false;
            this.mtxtGISSN.Size = new System.Drawing.Size(96, 22);
            this.mtxtGISSN.TabIndex = 5;
            // 
            // lblGIPatName
            // 
            this.lblGIPatName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIPatName.AutoEllipsis = true;
            this.lblGIPatName.AutoSize = true;
            this.lblGIPatName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIPatName.Location = new System.Drawing.Point(128, 17);
            this.lblGIPatName.Name = "lblGIPatName";
            this.lblGIPatName.Size = new System.Drawing.Size(46, 14);
            this.lblGIPatName.TabIndex = 4;
            this.lblGIPatName.Text = "Name :";
            // 
            // lblGIDOB
            // 
            this.lblGIDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIDOB.AutoEllipsis = true;
            this.lblGIDOB.AutoSize = true;
            this.lblGIDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIDOB.Location = new System.Drawing.Point(89, 60);
            this.lblGIDOB.Name = "lblGIDOB";
            this.lblGIDOB.Size = new System.Drawing.Size(85, 14);
            this.lblGIDOB.TabIndex = 8;
            this.lblGIDOB.Text = "Date of Birth :";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(137, 90);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 14);
            this.label19.TabIndex = 13;
            this.label19.Text = "SSN :";
            // 
            // lblGIFName
            // 
            this.lblGIFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIFName.AutoEllipsis = true;
            this.lblGIFName.AutoSize = true;
            this.lblGIFName.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIFName.Location = new System.Drawing.Point(237, 37);
            this.lblGIFName.Name = "lblGIFName";
            this.lblGIFName.Size = new System.Drawing.Size(57, 11);
            this.lblGIFName.TabIndex = 9;
            this.lblGIFName.Text = "(First Name)";
            // 
            // lblGIPatMName
            // 
            this.lblGIPatMName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIPatMName.AutoEllipsis = true;
            this.lblGIPatMName.AutoSize = true;
            this.lblGIPatMName.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIPatMName.Location = new System.Drawing.Point(355, 37);
            this.lblGIPatMName.Name = "lblGIPatMName";
            this.lblGIPatMName.Size = new System.Drawing.Size(23, 11);
            this.lblGIPatMName.TabIndex = 10;
            this.lblGIPatMName.Text = "(MI)";
            // 
            // lblGIPatLName
            // 
            this.lblGIPatLName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGIPatLName.AutoEllipsis = true;
            this.lblGIPatLName.AutoSize = true;
            this.lblGIPatLName.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIPatLName.Location = new System.Drawing.Point(441, 37);
            this.lblGIPatLName.Name = "lblGIPatLName";
            this.lblGIPatLName.Size = new System.Drawing.Size(55, 11);
            this.lblGIPatLName.TabIndex = 11;
            this.lblGIPatLName.Text = "(Last Name)";
            // 
            // grpboxGIGender
            // 
            this.grpboxGIGender.Controls.Add(this.radbtnGIOthers);
            this.grpboxGIGender.Controls.Add(this.radbtnGIFemale);
            this.grpboxGIGender.Controls.Add(this.radbtnGIMale);
            this.grpboxGIGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpboxGIGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpboxGIGender.Location = new System.Drawing.Point(292, 51);
            this.grpboxGIGender.Name = "grpboxGIGender";
            this.grpboxGIGender.Size = new System.Drawing.Size(205, 57);
            this.grpboxGIGender.TabIndex = 6;
            this.grpboxGIGender.TabStop = false;
            this.grpboxGIGender.Text = "Gender";
            // 
            // radbtnGIOthers
            // 
            this.radbtnGIOthers.AutoSize = true;
            this.radbtnGIOthers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIOthers.Location = new System.Drawing.Point(137, 23);
            this.radbtnGIOthers.Name = "radbtnGIOthers";
            this.radbtnGIOthers.Size = new System.Drawing.Size(57, 18);
            this.radbtnGIOthers.TabIndex = 9;
            this.radbtnGIOthers.TabStop = true;
            this.radbtnGIOthers.Text = "Other";
            this.radbtnGIOthers.UseVisualStyleBackColor = true;
            this.radbtnGIOthers.CheckedChanged += new System.EventHandler(this.radbtnGIOthers_CheckedChanged);
            // 
            // radbtnGIFemale
            // 
            this.radbtnGIFemale.AutoSize = true;
            this.radbtnGIFemale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIFemale.Location = new System.Drawing.Point(68, 23);
            this.radbtnGIFemale.Name = "radbtnGIFemale";
            this.radbtnGIFemale.Size = new System.Drawing.Size(63, 18);
            this.radbtnGIFemale.TabIndex = 8;
            this.radbtnGIFemale.TabStop = true;
            this.radbtnGIFemale.Text = "Female";
            this.radbtnGIFemale.UseVisualStyleBackColor = true;
            this.radbtnGIFemale.CheckedChanged += new System.EventHandler(this.radbtnGIFemale_CheckedChanged);
            // 
            // radbtnGIMale
            // 
            this.radbtnGIMale.AutoSize = true;
            this.radbtnGIMale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtnGIMale.Location = new System.Drawing.Point(12, 23);
            this.radbtnGIMale.Name = "radbtnGIMale";
            this.radbtnGIMale.Size = new System.Drawing.Size(49, 18);
            this.radbtnGIMale.TabIndex = 7;
            this.radbtnGIMale.TabStop = true;
            this.radbtnGIMale.Text = "Male";
            this.radbtnGIMale.UseVisualStyleBackColor = true;
            this.radbtnGIMale.CheckedChanged += new System.EventHandler(this.radbtnGIMale_CheckedChanged);
            // 
            // txtGIPatFName
            // 
            this.txtGIPatFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIPatFName.ForeColor = System.Drawing.Color.Black;
            this.txtGIPatFName.Location = new System.Drawing.Point(177, 13);
            this.txtGIPatFName.Name = "txtGIPatFName";
            this.txtGIPatFName.Size = new System.Drawing.Size(168, 22);
            this.txtGIPatFName.TabIndex = 1;
            // 
            // mskGIDOB
            // 
            this.mskGIDOB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.mskGIDOB.ForeColor = System.Drawing.Color.Black;
            this.mskGIDOB.Location = new System.Drawing.Point(177, 56);
            this.mskGIDOB.Mask = "00/00/0000";
            this.mskGIDOB.Name = "mskGIDOB";
            this.mskGIDOB.Size = new System.Drawing.Size(96, 22);
            this.mskGIDOB.TabIndex = 4;
            this.mskGIDOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mskGIDOB.Validating += new System.ComponentModel.CancelEventHandler(this.mskGIDOB_Validating);
            // 
            // txtGIPatLName
            // 
            this.txtGIPatLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIPatLName.ForeColor = System.Drawing.Color.Black;
            this.txtGIPatLName.Location = new System.Drawing.Point(384, 13);
            this.txtGIPatLName.Name = "txtGIPatLName";
            this.txtGIPatLName.Size = new System.Drawing.Size(159, 22);
            this.txtGIPatLName.TabIndex = 3;
            // 
            // txtGIMName
            // 
            this.txtGIMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGIMName.ForeColor = System.Drawing.Color.Black;
            this.txtGIMName.Location = new System.Drawing.Point(355, 13);
            this.txtGIMName.Name = "txtGIMName";
            this.txtGIMName.Size = new System.Drawing.Size(23, 22);
            this.txtGIMName.TabIndex = 2;
            // 
            // pnlCommercialGuarantor
            // 
            this.pnlCommercialGuarantor.Controls.Add(this.label24);
            this.pnlCommercialGuarantor.Controls.Add(this.lblCommercialName);
            this.pnlCommercialGuarantor.Controls.Add(this.txtCommercialName);
            this.pnlCommercialGuarantor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCommercialGuarantor.Location = new System.Drawing.Point(1, 32);
            this.pnlCommercialGuarantor.Name = "pnlCommercialGuarantor";
            this.pnlCommercialGuarantor.Size = new System.Drawing.Size(748, 147);
            this.pnlCommercialGuarantor.TabIndex = 1;
            this.pnlCommercialGuarantor.Visible = false;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoEllipsis = true;
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(114, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(14, 14);
            this.label24.TabIndex = 34;
            this.label24.Text = "*";
            // 
            // lblCommercialName
            // 
            this.lblCommercialName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommercialName.AutoEllipsis = true;
            this.lblCommercialName.AutoSize = true;
            this.lblCommercialName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommercialName.Location = new System.Drawing.Point(126, 16);
            this.lblCommercialName.Name = "lblCommercialName";
            this.lblCommercialName.Size = new System.Drawing.Size(46, 14);
            this.lblCommercialName.TabIndex = 44;
            this.lblCommercialName.Text = "Name :";
            // 
            // txtCommercialName
            // 
            this.txtCommercialName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommercialName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommercialName.ForeColor = System.Drawing.Color.Black;
            this.txtCommercialName.Location = new System.Drawing.Point(176, 12);
            this.txtCommercialName.Name = "txtCommercialName";
            this.txtCommercialName.Size = new System.Drawing.Size(362, 22);
            this.txtCommercialName.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.chkCommercialGuarantor);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(748, 28);
            this.panel4.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 28);
            this.label4.TabIndex = 2;
            this.label4.Text = " Guarantor Details :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkCommercialGuarantor
            // 
            this.chkCommercialGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCommercialGuarantor.AutoSize = true;
            this.chkCommercialGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkCommercialGuarantor.Location = new System.Drawing.Point(541, 5);
            this.chkCommercialGuarantor.Name = "chkCommercialGuarantor";
            this.chkCommercialGuarantor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkCommercialGuarantor.Size = new System.Drawing.Size(161, 18);
            this.chkCommercialGuarantor.TabIndex = 1;
            this.chkCommercialGuarantor.Text = "Commercial Guarantor";
            this.chkCommercialGuarantor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCommercialGuarantor.UseVisualStyleBackColor = true;
            this.chkCommercialGuarantor.CheckedChanged += new System.EventHandler(this.chkCommercialGuarantor_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(1, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(748, 1);
            this.label5.TabIndex = 20;
            this.label5.Text = "label2";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 176);
            this.label6.TabIndex = 19;
            this.label6.Text = "label4";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label7.Location = new System.Drawing.Point(749, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 176);
            this.label7.TabIndex = 18;
            this.label7.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(750, 1);
            this.label8.TabIndex = 17;
            this.label8.Text = "label1";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.rbSecondary);
            this.pnlStatus.Controls.Add(this.rbPrimary);
            this.pnlStatus.Controls.Add(this.rbInactive);
            this.pnlStatus.Controls.Add(this.rbTertiary);
            this.pnlStatus.Controls.Add(this.label18);
            this.pnlStatus.Location = new System.Drawing.Point(1, 73);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(748, 30);
            this.pnlStatus.TabIndex = 35;
            this.pnlStatus.Visible = false;
            // 
            // rbSecondary
            // 
            this.rbSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbSecondary.AutoSize = true;
            this.rbSecondary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSecondary.ForeColor = System.Drawing.Color.OrangeRed;
            this.rbSecondary.Location = new System.Drawing.Point(257, 5);
            this.rbSecondary.Name = "rbSecondary";
            this.rbSecondary.Size = new System.Drawing.Size(82, 18);
            this.rbSecondary.TabIndex = 1;
            this.rbSecondary.Text = "Secondary";
            this.rbSecondary.UseVisualStyleBackColor = true;
            // 
            // rbPrimary
            // 
            this.rbPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbPrimary.AutoSize = true;
            this.rbPrimary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPrimary.ForeColor = System.Drawing.Color.DarkRed;
            this.rbPrimary.Location = new System.Drawing.Point(176, 5);
            this.rbPrimary.Name = "rbPrimary";
            this.rbPrimary.Size = new System.Drawing.Size(64, 18);
            this.rbPrimary.TabIndex = 0;
            this.rbPrimary.Text = "Primary";
            this.rbPrimary.UseVisualStyleBackColor = true;
            // 
            // rbInactive
            // 
            this.rbInactive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbInactive.AutoSize = true;
            this.rbInactive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInactive.Location = new System.Drawing.Point(443, 5);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(74, 18);
            this.rbInactive.TabIndex = 3;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            // 
            // rbTertiary
            // 
            this.rbTertiary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbTertiary.AutoSize = true;
            this.rbTertiary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTertiary.ForeColor = System.Drawing.Color.ForestGreen;
            this.rbTertiary.Location = new System.Drawing.Point(357, 5);
            this.rbTertiary.Name = "rbTertiary";
            this.rbTertiary.Size = new System.Drawing.Size(67, 18);
            this.rbTertiary.TabIndex = 2;
            this.rbTertiary.Text = "Tertiary";
            this.rbTertiary.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(122, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 14);
            this.label18.TabIndex = 37;
            this.label18.Text = "Status :";
            // 
            // splitter1
            // 
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 85);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 568);
            this.splitter1.TabIndex = 28;
            this.splitter1.TabStop = false;
            // 
            // pnlTreeView
            // 
            this.pnlTreeView.Controls.Add(this.trvGuarantors);
            this.pnlTreeView.Controls.Add(this.label21);
            this.pnlTreeView.Controls.Add(this.label20);
            this.pnlTreeView.Controls.Add(this.label14);
            this.pnlTreeView.Controls.Add(this.label15);
            this.pnlTreeView.Controls.Add(this.label16);
            this.pnlTreeView.Controls.Add(this.label17);
            this.pnlTreeView.Location = new System.Drawing.Point(0, 85);
            this.pnlTreeView.Name = "pnlTreeView";
            this.pnlTreeView.Padding = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.pnlTreeView.Size = new System.Drawing.Size(232, 568);
            this.pnlTreeView.TabIndex = 3;
            this.pnlTreeView.TabStop = true;
            // 
            // trvGuarantors
            // 
            this.trvGuarantors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvGuarantors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvGuarantors.ForeColor = System.Drawing.Color.Black;
            this.trvGuarantors.ImageIndex = 0;
            this.trvGuarantors.ImageList = this.imageList1;
            this.trvGuarantors.Indent = 20;
            this.trvGuarantors.ItemHeight = 20;
            this.trvGuarantors.Location = new System.Drawing.Point(8, 8);
            this.trvGuarantors.Name = "trvGuarantors";
            this.trvGuarantors.SelectedImageIndex = 0;
            this.trvGuarantors.ShowLines = false;
            this.trvGuarantors.Size = new System.Drawing.Size(222, 556);
            this.trvGuarantors.TabIndex = 0;
            this.trvGuarantors.TabStop = false;
            this.trvGuarantors.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvGuarantors_BeforeSelect);
            this.trvGuarantors.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvGuarantors_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Gaurantor.ico");
            this.imageList1.Images.SetKeyName(1, "Bullet06.png");
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(8, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(222, 4);
            this.label21.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(4, 560);
            this.label20.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 564);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(226, 1);
            this.label14.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(4, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(226, 1);
            this.label15.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(230, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 562);
            this.label16.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 562);
            this.label17.TabIndex = 26;
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 30);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(756, 55);
            this.pnlTOP.TabIndex = 25;
            this.pnlTOP.TabStop = true;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.tsb_SelectGuarantor,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(750, 53);
            this.ts_Commands.TabIndex = 21;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(60, 50);
            this.toolStripButton3.Tag = "Remove";
            this.toolStripButton3.Text = "&Remove";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_SelectGuarantor
            // 
            this.tsb_SelectGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_SelectGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SelectGuarantor.Image")));
            this.tsb_SelectGuarantor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SelectGuarantor.Name = "tsb_SelectGuarantor";
            this.tsb_SelectGuarantor.Size = new System.Drawing.Size(118, 50);
            this.tsb_SelectGuarantor.Tag = "Select Guarantor";
            this.tsb_SelectGuarantor.Text = "Other &Guarantors";
            this.tsb_SelectGuarantor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SelectGuarantor.ToolTipText = "Select Patient Other Guarantors";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            this.tsb_OK.MouseHover += new System.EventHandler(this.tsb_MouseHover);
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
            this.tsb_Cancel.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            this.tsb_Cancel.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlGIHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(756, 30);
            this.panel1.TabIndex = 26;
            this.panel1.TabStop = true;
            // 
            // pnlGIHeader
            // 
            this.pnlGIHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlGIHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGIHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGIHeader.Controls.Add(this.label9);
            this.pnlGIHeader.Controls.Add(this.label10);
            this.pnlGIHeader.Controls.Add(this.label11);
            this.pnlGIHeader.Controls.Add(this.label12);
            this.pnlGIHeader.Controls.Add(this.lblGIHeader);
            this.pnlGIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlGIHeader.Name = "pnlGIHeader";
            this.pnlGIHeader.Size = new System.Drawing.Size(750, 24);
            this.pnlGIHeader.TabIndex = 0;
            this.pnlGIHeader.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            this.pnlGIHeader.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(1, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(748, 1);
            this.label9.TabIndex = 8;
            this.label9.Text = "label2";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 7;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(749, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 6;
            this.label11.Text = "label3";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(750, 1);
            this.label12.TabIndex = 5;
            this.label12.Text = "label1";
            // 
            // lblGIHeader
            // 
            this.lblGIHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIHeader.ForeColor = System.Drawing.Color.White;
            this.lblGIHeader.Location = new System.Drawing.Point(0, 0);
            this.lblGIHeader.Name = "lblGIHeader";
            this.lblGIHeader.Size = new System.Drawing.Size(750, 24);
            this.lblGIHeader.TabIndex = 0;
            this.lblGIHeader.Text = "  Guarantor Information";
            this.lblGIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gloPAGuarantorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlGaurantorInfo);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPAGuarantorControl";
            this.Size = new System.Drawing.Size(756, 653);
            this.Load += new System.EventHandler(this.gloPatientGuarantor_Load);
            this.pnlGaurantorInfo.ResumeLayout(false);
            this.pnlGIContactDetails.ResumeLayout(false);
            this.pnlGIContactDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlGIAddressDetails.ResumeLayout(false);
            this.pnlGIAddressDetails.PerformLayout();
            this.pnlGIPersonalDetails.ResumeLayout(false);
            this.pnlPersonalGuarantor.ResumeLayout(false);
            this.pnlPersonalGuarantor.PerformLayout();
            this.grpboxGIGender.ResumeLayout(false);
            this.grpboxGIGender.PerformLayout();
            this.pnlCommercialGuarantor.ResumeLayout(false);
            this.pnlCommercialGuarantor.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.pnlTreeView.ResumeLayout(false);
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlGIHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGaurantorInfo;
        private System.Windows.Forms.Panel pnlGIContactDetails;
        private System.Windows.Forms.TextBox txtGIEmail;
        private gloMaskControl.gloMaskBox txtGIFax;
        private System.Windows.Forms.Label lblGIEmail;
        private System.Windows.Forms.Label lblGIFax;
        private System.Windows.Forms.Label lblGIMobile;
        private System.Windows.Forms.Label lblGIPhone;
        private System.Windows.Forms.Label lblGIContactDetails;
        private System.Windows.Forms.Panel pnlGIAddressDetails;
        private System.Windows.Forms.Label lblGIAddressDetails;
        private System.Windows.Forms.TextBox txtGIZip;
        private System.Windows.Forms.TextBox txtGICity;
        private System.Windows.Forms.TextBox txtGIAddressLine2;
        private System.Windows.Forms.TextBox txtGIAddressLine1;
        private System.Windows.Forms.Label lblGIZip;
        private System.Windows.Forms.Label lblGIState;
        private System.Windows.Forms.Label lblGICity;
        private System.Windows.Forms.Label lblGIAddressLine2;
        private System.Windows.Forms.Label lblGIAddressLine1;
        private System.Windows.Forms.Panel pnlGIPersonalDetails;
        private System.Windows.Forms.GroupBox grpboxGIGender;
        private System.Windows.Forms.RadioButton radbtnGIOthers;
        private System.Windows.Forms.RadioButton radbtnGIFemale;
        private System.Windows.Forms.RadioButton radbtnGIMale;
        private System.Windows.Forms.Label lblGIPatLName;
        private System.Windows.Forms.Label lblGIPatMName;
        private System.Windows.Forms.Label lblGIFName;
        private System.Windows.Forms.MaskedTextBox mskGIDOB;
        private System.Windows.Forms.TextBox txtGIMName;
        private System.Windows.Forms.TextBox txtGIPatLName;
        private System.Windows.Forms.TextBox txtGIPatFName;
        private System.Windows.Forms.Label lblGIDOB;
        private System.Windows.Forms.Label lblGIPatName;
        private System.Windows.Forms.Panel pnlGIHeader;
        private System.Windows.Forms.Label lblGIHeader;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlTreeView;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TreeView trvGuarantors;
        private System.Windows.Forms.Label label19;
        private gloMaskControl.gloMaskBox mskGIMobile;
        private gloMaskControl.gloMaskBox mskGIPhone;
        private gloMaskControl.gloMaskBox mtxtGISSN;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.ComboBox cmbGICountry;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtGICounty;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private System.Windows.Forms.Panel pnlPersonalGuarantor;
        private System.Windows.Forms.Panel pnlCommercialGuarantor;
        private System.Windows.Forms.Label lblCommercialName;
        private System.Windows.Forms.TextBox txtCommercialName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox chkCommercialGuarantor;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripButton tsb_SelectGuarantor;
        internal System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.RadioButton rbSecondary;
        private System.Windows.Forms.RadioButton rbPrimary;
        private System.Windows.Forms.RadioButton rbInactive;
        private System.Windows.Forms.RadioButton rbTertiary;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblGIRelation;
        private System.Windows.Forms.ComboBox cmbGIRelation;
        private System.Windows.Forms.Label label4;
    }
}