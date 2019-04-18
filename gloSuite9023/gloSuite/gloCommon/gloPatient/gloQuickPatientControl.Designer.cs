namespace gloPatient
{
    partial class gloQuickPatientControl
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
                
                

                //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
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
                    if (oPatientDemo != null)
                    {
                        oPatientDemo.Dispose();
                        oPatientDemo = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (_PatientInsurance != null)
                    {
                        _PatientInsurance.Dispose();
                        _PatientInsurance = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oAccount != null)
                    {
                        oAccount.Dispose();
                        oAccount = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oPatientAccount != null)
                    {
                        oPatientAccount.Dispose();
                        oPatientAccount = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloQuickPatientControl));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnl_AddressDetails = new System.Windows.Forms.Panel();
            this.pnl_InsuranceDetails = new System.Windows.Forms.Panel();
            this.cmbGenInfoInsurance = new System.Windows.Forms.ComboBox();
            this.btnClrInsurance = new System.Windows.Forms.Button();
            this.btnInsurInfo = new System.Windows.Forms.Button();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label46 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlAddDetails = new System.Windows.Forms.Panel();
            this.pnlAddresControl = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.lblAddressDetails = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pnlConDetails = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbSendAPIInvitation = new System.Windows.Forms.CheckBox();
            this.pnlPortalInvitaitonEmail = new System.Windows.Forms.Panel();
            this.cbSendPatientPortalActivationEmail = new System.Windows.Forms.CheckBox();
            this.mtxtPAFax = new gloMaskControl.gloMaskBox();
            this.mtxtPAMobile = new gloMaskControl.gloMaskBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPAEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblContactDetails = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlPadDetails = new System.Windows.Forms.Panel();
            this.txtPatientPrefix = new System.Windows.Forms.TextBox();
            this.txtPACode = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAlert = new System.Windows.Forms.TextBox();
            this.lblAlert = new System.Windows.Forms.Label();
            this.txtPAFax = new System.Windows.Forms.TextBox();
            this.txtmPASSN = new gloMaskControl.gloMaskBox();
            this.mtxtPAPhone = new gloMaskControl.gloMaskBox();
            this.cmb_Providers = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnMoreLess = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtmPADOB = new System.Windows.Forms.MaskedTextBox();
            this.txtPAMName = new System.Windows.Forms.TextBox();
            this.txtPALName = new System.Windows.Forms.TextBox();
            this.txtPAFname = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbPAGender = new System.Windows.Forms.GroupBox();
            this.rbGender3 = new System.Windows.Forms.RadioButton();
            this.rbGender2 = new System.Windows.Forms.RadioButton();
            this.rbGender1 = new System.Windows.Forms.RadioButton();
            this.lblPALName = new System.Windows.Forms.Label();
            this.lblPAMName = new System.Windows.Forms.Label();
            this.lblPAFName = new System.Windows.Forms.Label();
            this.lblPatientSSN = new System.Windows.Forms.Label();
            this.lbPatientDOB = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.lblPersonalInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlHeaderLabel = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblQuickRegistration = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnl_AddressDetails.SuspendLayout();
            this.pnl_InsuranceDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlAddDetails.SuspendLayout();
            this.pnlConDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPortalInvitaitonEmail.SuspendLayout();
            this.pnlPadDetails.SuspendLayout();
            this.gbPAGender.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlHeaderLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnl_AddressDetails);
            this.pnlMain.Controls.Add(this.pnlPadDetails);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(588, 573);
            this.pnlMain.TabIndex = 0;
            // 
            // pnl_AddressDetails
            // 
            this.pnl_AddressDetails.Controls.Add(this.pnl_InsuranceDetails);
            this.pnl_AddressDetails.Controls.Add(this.panel2);
            this.pnl_AddressDetails.Controls.Add(this.pnlAddDetails);
            this.pnl_AddressDetails.Controls.Add(this.pnlConDetails);
            this.pnl_AddressDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_AddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_AddressDetails.Location = new System.Drawing.Point(0, 211);
            this.pnl_AddressDetails.Name = "pnl_AddressDetails";
            this.pnl_AddressDetails.Size = new System.Drawing.Size(588, 362);
            this.pnl_AddressDetails.TabIndex = 1;
            this.pnl_AddressDetails.Visible = false;
            // 
            // pnl_InsuranceDetails
            // 
            this.pnl_InsuranceDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_InsuranceDetails.Controls.Add(this.cmbGenInfoInsurance);
            this.pnl_InsuranceDetails.Controls.Add(this.btnClrInsurance);
            this.pnl_InsuranceDetails.Controls.Add(this.btnInsurInfo);
            this.pnl_InsuranceDetails.Controls.Add(this.lblInsurance);
            this.pnl_InsuranceDetails.Controls.Add(this.label4);
            this.pnl_InsuranceDetails.Controls.Add(this.label6);
            this.pnl_InsuranceDetails.Controls.Add(this.label7);
            this.pnl_InsuranceDetails.Controls.Add(this.label10);
            this.pnl_InsuranceDetails.Controls.Add(this.label11);
            this.pnl_InsuranceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_InsuranceDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_InsuranceDetails.Location = new System.Drawing.Point(0, 287);
            this.pnl_InsuranceDetails.Name = "pnl_InsuranceDetails";
            this.pnl_InsuranceDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_InsuranceDetails.Size = new System.Drawing.Size(588, 50);
            this.pnl_InsuranceDetails.TabIndex = 2;
            // 
            // cmbGenInfoInsurance
            // 
            this.cmbGenInfoInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGenInfoInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenInfoInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenInfoInsurance.ForeColor = System.Drawing.Color.Black;
            this.cmbGenInfoInsurance.FormattingEnabled = true;
            this.cmbGenInfoInsurance.Location = new System.Drawing.Point(126, 17);
            this.cmbGenInfoInsurance.Name = "cmbGenInfoInsurance";
            this.cmbGenInfoInsurance.Size = new System.Drawing.Size(236, 22);
            this.cmbGenInfoInsurance.TabIndex = 0;
            // 
            // btnClrInsurance
            // 
            this.btnClrInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClrInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClrInsurance.BackgroundImage")));
            this.btnClrInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClrInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClrInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClrInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClrInsurance.Image")));
            this.btnClrInsurance.Location = new System.Drawing.Point(391, 16);
            this.btnClrInsurance.Name = "btnClrInsurance";
            this.btnClrInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClrInsurance.TabIndex = 2;
            this.btnClrInsurance.UseVisualStyleBackColor = false;
            this.btnClrInsurance.Click += new System.EventHandler(this.btnClrInsurance_Click);
            this.btnClrInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClrInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnInsurInfo
            // 
            this.btnInsurInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnInsurInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsurInfo.BackgroundImage")));
            this.btnInsurInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsurInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnInsurInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsurInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInsurInfo.Image")));
            this.btnInsurInfo.Location = new System.Drawing.Point(366, 16);
            this.btnInsurInfo.Name = "btnInsurInfo";
            this.btnInsurInfo.Size = new System.Drawing.Size(22, 22);
            this.btnInsurInfo.TabIndex = 1;
            this.btnInsurInfo.UseVisualStyleBackColor = false;
            this.btnInsurInfo.Click += new System.EventHandler(this.btnInsurInfo_Click_1);
            this.btnInsurInfo.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnInsurInfo.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblInsurance
            // 
            this.lblInsurance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsurance.AutoEllipsis = true;
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsurance.Location = new System.Drawing.Point(56, 21);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(68, 14);
            this.lblInsurance.TabIndex = 64;
            this.lblInsurance.Text = "Insurance :";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(580, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Insurance Details";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(4, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(580, 1);
            this.label6.TabIndex = 68;
            this.label6.Text = "label2";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 43);
            this.label7.TabIndex = 67;
            this.label7.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(584, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 43);
            this.label10.TabIndex = 66;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(582, 1);
            this.label11.TabIndex = 65;
            this.label11.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label46);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Controls.Add(this.label48);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 337);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(588, 25);
            this.panel2.TabIndex = 71;
            // 
            // label46
            // 
            this.label46.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label46.AutoEllipsis = true;
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(28, 5);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(86, 14);
            this.label46.TabIndex = 31;
            this.label46.Text = "Required fields";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label42.Location = new System.Drawing.Point(4, 21);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(580, 1);
            this.label42.TabIndex = 12;
            this.label42.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(3, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 21);
            this.label43.TabIndex = 11;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(584, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 21);
            this.label44.TabIndex = 10;
            this.label44.Text = "label3";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(3, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(582, 1);
            this.label45.TabIndex = 9;
            this.label45.Text = "label1";
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
            this.label48.Location = new System.Drawing.Point(18, 2);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(14, 14);
            this.label48.TabIndex = 33;
            this.label48.Text = "*";
            // 
            // pnlAddDetails
            // 
            this.pnlAddDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAddDetails.Controls.Add(this.pnlAddresControl);
            this.pnlAddDetails.Controls.Add(this.label16);
            this.pnlAddDetails.Controls.Add(this.lblAddressDetails);
            this.pnlAddDetails.Controls.Add(this.label2);
            this.pnlAddDetails.Controls.Add(this.label17);
            this.pnlAddDetails.Controls.Add(this.label24);
            this.pnlAddDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddDetails.Location = new System.Drawing.Point(0, 126);
            this.pnlAddDetails.Name = "pnlAddDetails";
            this.pnlAddDetails.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlAddDetails.Size = new System.Drawing.Size(588, 161);
            this.pnlAddDetails.TabIndex = 1;
            // 
            // pnlAddresControl
            // 
            this.pnlAddresControl.Location = new System.Drawing.Point(47, 21);
            this.pnlAddresControl.Name = "pnlAddresControl";
            this.pnlAddresControl.Size = new System.Drawing.Size(396, 136);
            this.pnlAddresControl.TabIndex = 112;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(4, 160);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(580, 1);
            this.label16.TabIndex = 26;
            // 
            // lblAddressDetails
            // 
            this.lblAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressDetails.Location = new System.Drawing.Point(4, 4);
            this.lblAddressDetails.Name = "lblAddressDetails";
            this.lblAddressDetails.Size = new System.Drawing.Size(580, 14);
            this.lblAddressDetails.TabIndex = 0;
            this.lblAddressDetails.Text = " Address Details";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 157);
            this.label2.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(581, 1);
            this.label17.TabIndex = 27;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(584, 3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 158);
            this.label24.TabIndex = 37;
            // 
            // pnlConDetails
            // 
            this.pnlConDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlConDetails.Controls.Add(this.panel3);
            this.pnlConDetails.Controls.Add(this.pnlPortalInvitaitonEmail);
            this.pnlConDetails.Controls.Add(this.mtxtPAFax);
            this.pnlConDetails.Controls.Add(this.mtxtPAMobile);
            this.pnlConDetails.Controls.Add(this.label22);
            this.pnlConDetails.Controls.Add(this.txtPAEmail);
            this.pnlConDetails.Controls.Add(this.lblEmail);
            this.pnlConDetails.Controls.Add(this.lblFax);
            this.pnlConDetails.Controls.Add(this.lblMobile);
            this.pnlConDetails.Controls.Add(this.lblContactDetails);
            this.pnlConDetails.Controls.Add(this.label8);
            this.pnlConDetails.Controls.Add(this.label23);
            this.pnlConDetails.Controls.Add(this.label25);
            this.pnlConDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlConDetails.Name = "pnlConDetails";
            this.pnlConDetails.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlConDetails.Size = new System.Drawing.Size(588, 126);
            this.pnlConDetails.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbSendAPIInvitation);
            this.panel3.Location = new System.Drawing.Point(348, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(224, 24);
            this.panel3.TabIndex = 85;
            // 
            // cbSendAPIInvitation
            // 
            this.cbSendAPIInvitation.AutoSize = true;
            this.cbSendAPIInvitation.Location = new System.Drawing.Point(48, 4);
            this.cbSendAPIInvitation.Name = "cbSendAPIInvitation";
            this.cbSendAPIInvitation.Size = new System.Drawing.Size(103, 18);
            this.cbSendAPIInvitation.TabIndex = 0;
            this.cbSendAPIInvitation.Text = "API Activation";
            this.cbSendAPIInvitation.UseVisualStyleBackColor = true;
            // 
            // pnlPortalInvitaitonEmail
            // 
            this.pnlPortalInvitaitonEmail.Controls.Add(this.cbSendPatientPortalActivationEmail);
            this.pnlPortalInvitaitonEmail.Location = new System.Drawing.Point(79, 96);
            this.pnlPortalInvitaitonEmail.Name = "pnlPortalInvitaitonEmail";
            this.pnlPortalInvitaitonEmail.Size = new System.Drawing.Size(263, 24);
            this.pnlPortalInvitaitonEmail.TabIndex = 84;
            this.pnlPortalInvitaitonEmail.Visible = false;
            // 
            // cbSendPatientPortalActivationEmail
            // 
            this.cbSendPatientPortalActivationEmail.AutoSize = true;
            this.cbSendPatientPortalActivationEmail.Location = new System.Drawing.Point(48, 4);
            this.cbSendPatientPortalActivationEmail.Name = "cbSendPatientPortalActivationEmail";
            this.cbSendPatientPortalActivationEmail.Size = new System.Drawing.Size(187, 18);
            this.cbSendPatientPortalActivationEmail.TabIndex = 0;
            this.cbSendPatientPortalActivationEmail.Text = "Send Patient Portal Invitation";
            this.cbSendPatientPortalActivationEmail.UseVisualStyleBackColor = true;
            // 
            // mtxtPAFax
            // 
            this.mtxtPAFax.AllowValidate = true;
            this.mtxtPAFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.mtxtPAFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPAFax.IncludeLiteralsAndPrompts = false;
            this.mtxtPAFax.Location = new System.Drawing.Point(126, 45);
            this.mtxtPAFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.mtxtPAFax.Name = "mtxtPAFax";
            this.mtxtPAFax.ReadOnly = false;
            this.mtxtPAFax.Size = new System.Drawing.Size(174, 22);
            this.mtxtPAFax.TabIndex = 38;
            // 
            // mtxtPAMobile
            // 
            this.mtxtPAMobile.AllowValidate = true;
            this.mtxtPAMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPAMobile.IncludeLiteralsAndPrompts = false;
            this.mtxtPAMobile.Location = new System.Drawing.Point(126, 21);
            this.mtxtPAMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxtPAMobile.Name = "mtxtPAMobile";
            this.mtxtPAMobile.ReadOnly = false;
            this.mtxtPAMobile.Size = new System.Drawing.Size(174, 22);
            this.mtxtPAMobile.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(4, 125);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(580, 1);
            this.label22.TabIndex = 28;
            // 
            // txtPAEmail
            // 
            this.txtPAEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAEmail.ForeColor = System.Drawing.Color.Black;
            this.txtPAEmail.Location = new System.Drawing.Point(126, 71);
            this.txtPAEmail.MaxLength = 50;
            this.txtPAEmail.Name = "txtPAEmail";
            this.txtPAEmail.Size = new System.Drawing.Size(347, 22);
            this.txtPAEmail.TabIndex = 2;
            this.txtPAEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtPAEmail_Validating);
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoEllipsis = true;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(82, 75);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 14);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email :";
            // 
            // lblFax
            // 
            this.lblFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFax.AutoEllipsis = true;
            this.lblFax.AutoSize = true;
            this.lblFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFax.Location = new System.Drawing.Point(91, 48);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(33, 14);
            this.lblFax.TabIndex = 4;
            this.lblFax.Text = "Fax :";
            // 
            // lblMobile
            // 
            this.lblMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMobile.AutoEllipsis = true;
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobile.Location = new System.Drawing.Point(75, 25);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(49, 14);
            this.lblMobile.TabIndex = 2;
            this.lblMobile.Text = "Mobile :";
            // 
            // lblContactDetails
            // 
            this.lblContactDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContactDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactDetails.Location = new System.Drawing.Point(4, 4);
            this.lblContactDetails.Name = "lblContactDetails";
            this.lblContactDetails.Size = new System.Drawing.Size(580, 17);
            this.lblContactDetails.TabIndex = 8;
            this.lblContactDetails.Text = " Contact Details";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(584, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 122);
            this.label8.TabIndex = 27;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(581, 1);
            this.label23.TabIndex = 29;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 123);
            this.label25.TabIndex = 37;
            // 
            // pnlPadDetails
            // 
            this.pnlPadDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPadDetails.Controls.Add(this.label18);
            this.pnlPadDetails.Controls.Add(this.txtPatientPrefix);
            this.pnlPadDetails.Controls.Add(this.txtPACode);
            this.pnlPadDetails.Controls.Add(this.label13);
            this.pnlPadDetails.Controls.Add(this.txtAlert);
            this.pnlPadDetails.Controls.Add(this.lblAlert);
            this.pnlPadDetails.Controls.Add(this.txtPAFax);
            this.pnlPadDetails.Controls.Add(this.txtmPASSN);
            this.pnlPadDetails.Controls.Add(this.mtxtPAPhone);
            this.pnlPadDetails.Controls.Add(this.cmb_Providers);
            this.pnlPadDetails.Controls.Add(this.label5);
            this.pnlPadDetails.Controls.Add(this.btnMoreLess);
            this.pnlPadDetails.Controls.Add(this.label14);
            this.pnlPadDetails.Controls.Add(this.txtmPADOB);
            this.pnlPadDetails.Controls.Add(this.txtPAMName);
            this.pnlPadDetails.Controls.Add(this.txtPALName);
            this.pnlPadDetails.Controls.Add(this.txtPAFname);
            this.pnlPadDetails.Controls.Add(this.lblPhone);
            this.pnlPadDetails.Controls.Add(this.label1);
            this.pnlPadDetails.Controls.Add(this.gbPAGender);
            this.pnlPadDetails.Controls.Add(this.lblPALName);
            this.pnlPadDetails.Controls.Add(this.lblPAMName);
            this.pnlPadDetails.Controls.Add(this.lblPAFName);
            this.pnlPadDetails.Controls.Add(this.lblPatientSSN);
            this.pnlPadDetails.Controls.Add(this.lbPatientDOB);
            this.pnlPadDetails.Controls.Add(this.lblPatientName);
            this.pnlPadDetails.Controls.Add(this.lblPatientCode);
            this.pnlPadDetails.Controls.Add(this.lblPersonalInfo);
            this.pnlPadDetails.Controls.Add(this.label3);
            this.pnlPadDetails.Controls.Add(this.label9);
            this.pnlPadDetails.Controls.Add(this.label15);
            this.pnlPadDetails.Controls.Add(this.label19);
            this.pnlPadDetails.Controls.Add(this.label12);
            this.pnlPadDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPadDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPadDetails.Location = new System.Drawing.Point(0, 31);
            this.pnlPadDetails.Name = "pnlPadDetails";
            this.pnlPadDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlPadDetails.Size = new System.Drawing.Size(588, 180);
            this.pnlPadDetails.TabIndex = 0;
            // 
            // txtPatientPrefix
            // 
            this.txtPatientPrefix.Location = new System.Drawing.Point(562, 17);
            this.txtPatientPrefix.Name = "txtPatientPrefix";
            this.txtPatientPrefix.Size = new System.Drawing.Size(10, 22);
            this.txtPatientPrefix.TabIndex = 74;
            this.txtPatientPrefix.Visible = false;
            // 
            // txtPACode
            // 
            this.txtPACode.Location = new System.Drawing.Point(126, 17);
            this.txtPACode.Mask = "AAA-AAAAAAAAAA";
            this.txtPACode.Name = "txtPACode";
            this.txtPACode.PromptChar = ' ';
            this.txtPACode.ShortcutsEnabled = false;
            this.txtPACode.Size = new System.Drawing.Size(174, 22);
            this.txtPACode.TabIndex = 0;
            this.txtPACode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtPACode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPACode_KeyDown);
            this.txtPACode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPACode_KeyPress);
            this.txtPACode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPACode_KeyUp);
            this.txtPACode.Validating += new System.ComponentModel.CancelEventHandler(this.txtPACode_Validating);
            this.txtPACode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(312, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(10, 13);
            this.label13.TabIndex = 71;
            this.label13.Text = "*";
            // 
            // txtAlert
            // 
            this.txtAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlert.ForeColor = System.Drawing.Color.Black;
            this.txtAlert.Location = new System.Drawing.Point(126, 148);
            this.txtAlert.Name = "txtAlert";
            this.txtAlert.Size = new System.Drawing.Size(446, 22);
            this.txtAlert.TabIndex = 9;
            // 
            // lblAlert
            // 
            this.lblAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlert.AutoEllipsis = true;
            this.lblAlert.AutoSize = true;
            this.lblAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.Location = new System.Drawing.Point(83, 152);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(41, 14);
            this.lblAlert.TabIndex = 70;
            this.lblAlert.Text = "Alert :";
            // 
            // txtPAFax
            // 
            this.txtPAFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAFax.ForeColor = System.Drawing.Color.Black;
            this.txtPAFax.Location = new System.Drawing.Point(548, 42);
            this.txtPAFax.MaxLength = 50;
            this.txtPAFax.Name = "txtPAFax";
            this.txtPAFax.Size = new System.Drawing.Size(30, 22);
            this.txtPAFax.TabIndex = 1;
            this.txtPAFax.Visible = false;
            // 
            // txtmPASSN
            // 
            this.txtmPASSN.AllowValidate = true;
            this.txtmPASSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmPASSN.IncludeLiteralsAndPrompts = false;
            this.txtmPASSN.Location = new System.Drawing.Point(344, 17);
            this.txtmPASSN.MaskType = gloMaskControl.gloMaskType.SSN;
            this.txtmPASSN.Name = "txtmPASSN";
            this.txtmPASSN.ReadOnly = false;
            this.txtmPASSN.Size = new System.Drawing.Size(174, 22);
            this.txtmPASSN.TabIndex = 1;
            // 
            // mtxtPAPhone
            // 
            this.mtxtPAPhone.AllowValidate = true;
            this.mtxtPAPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPAPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtPAPhone.Location = new System.Drawing.Point(126, 123);
            this.mtxtPAPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtPAPhone.Name = "mtxtPAPhone";
            this.mtxtPAPhone.ReadOnly = false;
            this.mtxtPAPhone.Size = new System.Drawing.Size(174, 22);
            this.mtxtPAPhone.TabIndex = 7;
            // 
            // cmb_Providers
            // 
            this.cmb_Providers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmb_Providers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Providers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Providers.ForeColor = System.Drawing.Color.Black;
            this.cmb_Providers.FormattingEnabled = true;
            this.cmb_Providers.Location = new System.Drawing.Point(381, 122);
            this.cmb_Providers.Name = "cmb_Providers";
            this.cmb_Providers.Size = new System.Drawing.Size(191, 22);
            this.cmb_Providers.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(320, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 14);
            this.label5.TabIndex = 66;
            this.label5.Text = "Provider :";
            // 
            // btnMoreLess
            // 
            this.btnMoreLess.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnMoreLess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoreLess.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnMoreLess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoreLess.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoreLess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnMoreLess.Location = new System.Drawing.Point(578, 120);
            this.btnMoreLess.Name = "btnMoreLess";
            this.btnMoreLess.Size = new System.Drawing.Size(89, 24);
            this.btnMoreLess.TabIndex = 9;
            this.btnMoreLess.Text = "More Details >>";
            this.btnMoreLess.UseVisualStyleBackColor = true;
            this.btnMoreLess.Click += new System.EventHandler(this.btnMoreLess_Click);
            this.btnMoreLess.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnMoreLess.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 179);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(580, 1);
            this.label14.TabIndex = 28;
            // 
            // txtmPADOB
            // 
            this.txtmPADOB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtmPADOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmPADOB.ForeColor = System.Drawing.Color.Black;
            this.txtmPADOB.Location = new System.Drawing.Point(126, 80);
            this.txtmPADOB.Mask = "00/00/0000";
            this.txtmPADOB.Name = "txtmPADOB";
            this.txtmPADOB.Size = new System.Drawing.Size(174, 22);
            this.txtmPADOB.TabIndex = 5;
            this.txtmPADOB.ValidatingType = typeof(System.DateTime);
            this.txtmPADOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.txtmPADOB.Validating += new System.ComponentModel.CancelEventHandler(this.txtmPADOB_Validating);
            // 
            // txtPAMName
            // 
            this.txtPAMName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAMName.ForeColor = System.Drawing.Color.Black;
            this.txtPAMName.Location = new System.Drawing.Point(310, 42);
            this.txtPAMName.MaxLength = 35;
            this.txtPAMName.Name = "txtPAMName";
            this.txtPAMName.Size = new System.Drawing.Size(26, 22);
            this.txtPAMName.TabIndex = 3;
            // 
            // txtPALName
            // 
            this.txtPALName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPALName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPALName.ForeColor = System.Drawing.Color.Black;
            this.txtPALName.Location = new System.Drawing.Point(344, 42);
            this.txtPALName.MaxLength = 50;
            this.txtPALName.Name = "txtPALName";
            this.txtPALName.Size = new System.Drawing.Size(174, 22);
            this.txtPALName.TabIndex = 4;
            // 
            // txtPAFname
            // 
            this.txtPAFname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAFname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAFname.ForeColor = System.Drawing.Color.Black;
            this.txtPAFname.Location = new System.Drawing.Point(126, 42);
            this.txtPAFname.MaxLength = 50;
            this.txtPAFname.Name = "txtPAFname";
            this.txtPAFname.Size = new System.Drawing.Size(174, 22);
            this.txtPAFname.TabIndex = 2;
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhone.AutoEllipsis = true;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(74, 127);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(50, 14);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "Phone :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label1.Location = new System.Drawing.Point(169, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "(mm/dd/yyyy)";
            // 
            // gbPAGender
            // 
            this.gbPAGender.Controls.Add(this.rbGender3);
            this.gbPAGender.Controls.Add(this.rbGender2);
            this.gbPAGender.Controls.Add(this.rbGender1);
            this.gbPAGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPAGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbPAGender.Location = new System.Drawing.Point(308, 78);
            this.gbPAGender.Name = "gbPAGender";
            this.gbPAGender.Size = new System.Drawing.Size(264, 38);
            this.gbPAGender.TabIndex = 6;
            this.gbPAGender.TabStop = false;
            this.gbPAGender.Text = "Gender";
            // 
            // rbGender3
            // 
            this.rbGender3.AutoSize = true;
            this.rbGender3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender3.Location = new System.Drawing.Point(187, 15);
            this.rbGender3.Name = "rbGender3";
            this.rbGender3.Size = new System.Drawing.Size(57, 18);
            this.rbGender3.TabIndex = 3;
            this.rbGender3.Text = "Other";
            this.rbGender3.UseVisualStyleBackColor = true;
            this.rbGender3.CheckedChanged += new System.EventHandler(this.rbGender3_CheckedChanged);
            // 
            // rbGender2
            // 
            this.rbGender2.AutoSize = true;
            this.rbGender2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender2.Location = new System.Drawing.Point(93, 15);
            this.rbGender2.Name = "rbGender2";
            this.rbGender2.Size = new System.Drawing.Size(63, 18);
            this.rbGender2.TabIndex = 1;
            this.rbGender2.Text = "Female";
            this.rbGender2.UseVisualStyleBackColor = true;
            this.rbGender2.CheckedChanged += new System.EventHandler(this.rbGender2_CheckedChanged);
            // 
            // rbGender1
            // 
            this.rbGender1.AutoSize = true;
            this.rbGender1.Checked = true;
            this.rbGender1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGender1.Location = new System.Drawing.Point(9, 15);
            this.rbGender1.Name = "rbGender1";
            this.rbGender1.Size = new System.Drawing.Size(53, 18);
            this.rbGender1.TabIndex = 0;
            this.rbGender1.TabStop = true;
            this.rbGender1.Text = "Male";
            this.rbGender1.UseVisualStyleBackColor = true;
            this.rbGender1.CheckedChanged += new System.EventHandler(this.rbGender1_CheckedChanged);
            // 
            // lblPALName
            // 
            this.lblPALName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPALName.AutoEllipsis = true;
            this.lblPALName.AutoSize = true;
            this.lblPALName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPALName.Location = new System.Drawing.Point(392, 65);
            this.lblPALName.Name = "lblPALName";
            this.lblPALName.Size = new System.Drawing.Size(58, 12);
            this.lblPALName.TabIndex = 11;
            this.lblPALName.Text = "(Last Name)";
            // 
            // lblPAMName
            // 
            this.lblPAMName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPAMName.AutoEllipsis = true;
            this.lblPAMName.AutoSize = true;
            this.lblPAMName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPAMName.Location = new System.Drawing.Point(307, 65);
            this.lblPAMName.Name = "lblPAMName";
            this.lblPAMName.Size = new System.Drawing.Size(25, 12);
            this.lblPAMName.TabIndex = 10;
            this.lblPAMName.Text = "(MI)";
            // 
            // lblPAFName
            // 
            this.lblPAFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPAFName.AutoEllipsis = true;
            this.lblPAFName.AutoSize = true;
            this.lblPAFName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPAFName.Location = new System.Drawing.Point(169, 68);
            this.lblPAFName.Name = "lblPAFName";
            this.lblPAFName.Size = new System.Drawing.Size(60, 12);
            this.lblPAFName.TabIndex = 9;
            this.lblPAFName.Text = "(First Name)";
            // 
            // lblPatientSSN
            // 
            this.lblPatientSSN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientSSN.AutoEllipsis = true;
            this.lblPatientSSN.AutoSize = true;
            this.lblPatientSSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSSN.Location = new System.Drawing.Point(306, 21);
            this.lblPatientSSN.Name = "lblPatientSSN";
            this.lblPatientSSN.Size = new System.Drawing.Size(37, 14);
            this.lblPatientSSN.TabIndex = 3;
            this.lblPatientSSN.Text = "SSN :";
            // 
            // lbPatientDOB
            // 
            this.lbPatientDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientDOB.AutoEllipsis = true;
            this.lbPatientDOB.AutoSize = true;
            this.lbPatientDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientDOB.Location = new System.Drawing.Point(39, 84);
            this.lbPatientDOB.Name = "lbPatientDOB";
            this.lbPatientDOB.Size = new System.Drawing.Size(85, 14);
            this.lbPatientDOB.TabIndex = 12;
            this.lbPatientDOB.Text = "Date of Birth :";
            // 
            // lblPatientName
            // 
            this.lblPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientName.AutoEllipsis = true;
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(35, 46);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(89, 14);
            this.lblPatientName.TabIndex = 5;
            this.lblPatientName.Text = "Patient Name :";
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientCode.AutoEllipsis = true;
            this.lblPatientCode.AutoSize = true;
            this.lblPatientCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCode.Location = new System.Drawing.Point(38, 21);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(86, 14);
            this.lblPatientCode.TabIndex = 1;
            this.lblPatientCode.Text = "Patient Code :";
            // 
            // lblPersonalInfo
            // 
            this.lblPersonalInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPersonalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPersonalInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonalInfo.Location = new System.Drawing.Point(4, 1);
            this.lblPersonalInfo.Name = "lblPersonalInfo";
            this.lblPersonalInfo.Size = new System.Drawing.Size(580, 14);
            this.lblPersonalInfo.TabIndex = 0;
            this.lblPersonalInfo.Text = " Personal Information";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 179);
            this.label3.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(584, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 179);
            this.label9.TabIndex = 27;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(582, 1);
            this.label15.TabIndex = 29;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoEllipsis = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(29, 85);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(10, 23);
            this.label19.TabIndex = 67;
            this.label19.Text = "*";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(25, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 23);
            this.label12.TabIndex = 68;
            this.label12.Text = "*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlHeaderLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(588, 31);
            this.panel1.TabIndex = 8;
            // 
            // pnlHeaderLabel
            // 
            this.pnlHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeaderLabel.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlHeaderLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeaderLabel.Controls.Add(this.lbl_BottomBrd);
            this.pnlHeaderLabel.Controls.Add(this.lbl_LeftBrd);
            this.pnlHeaderLabel.Controls.Add(this.lbl_RightBrd);
            this.pnlHeaderLabel.Controls.Add(this.lbl_TopBrd);
            this.pnlHeaderLabel.Controls.Add(this.lblQuickRegistration);
            this.pnlHeaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlHeaderLabel.Location = new System.Drawing.Point(3, 3);
            this.pnlHeaderLabel.Name = "pnlHeaderLabel";
            this.pnlHeaderLabel.Size = new System.Drawing.Size(582, 25);
            this.pnlHeaderLabel.TabIndex = 5;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(580, 1);
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
            this.lbl_RightBrd.Location = new System.Drawing.Point(581, 1);
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
            this.lbl_TopBrd.Size = new System.Drawing.Size(582, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblQuickRegistration
            // 
            this.lblQuickRegistration.BackColor = System.Drawing.Color.Transparent;
            this.lblQuickRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuickRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblQuickRegistration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickRegistration.ForeColor = System.Drawing.Color.White;
            this.lblQuickRegistration.Location = new System.Drawing.Point(0, 0);
            this.lblQuickRegistration.Name = "lblQuickRegistration";
            this.lblQuickRegistration.Size = new System.Drawing.Size(582, 25);
            this.lblQuickRegistration.TabIndex = 0;
            this.lblQuickRegistration.Text = "  Quick Patient Registration";
            this.lblQuickRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(27, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 14);
            this.label18.TabIndex = 75;
            this.label18.Text = "*";
            // 
            // gloQuickPatientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloQuickPatientControl";
            this.Size = new System.Drawing.Size(588, 573);
            this.Load += new System.EventHandler(this.gloQuickPatientControl_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnl_AddressDetails.ResumeLayout(false);
            this.pnl_InsuranceDetails.ResumeLayout(false);
            this.pnl_InsuranceDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlAddDetails.ResumeLayout(false);
            this.pnlConDetails.ResumeLayout(false);
            this.pnlConDetails.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPortalInvitaitonEmail.ResumeLayout(false);
            this.pnlPortalInvitaitonEmail.PerformLayout();
            this.pnlPadDetails.ResumeLayout(false);
            this.pnlPadDetails.PerformLayout();
            this.gbPAGender.ResumeLayout(false);
            this.gbPAGender.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlHeaderLabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeaderLabel;
        private System.Windows.Forms.Label lblQuickRegistration;
        private System.Windows.Forms.Panel pnlPadDetails;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.MaskedTextBox txtmPADOB;
        internal System.Windows.Forms.TextBox txtPAMName;
        internal System.Windows.Forms.TextBox txtPALName;
        internal System.Windows.Forms.TextBox txtPAFname;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox gbPAGender;
        private System.Windows.Forms.RadioButton rbGender3;
        private System.Windows.Forms.RadioButton rbGender2;
        private System.Windows.Forms.RadioButton rbGender1;
        private System.Windows.Forms.Label lblPALName;
        private System.Windows.Forms.Label lblPAMName;
        private System.Windows.Forms.Label lblPAFName;
        private System.Windows.Forms.Label lblPatientSSN;
        private System.Windows.Forms.Label lbPatientDOB;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label lblPersonalInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlConDetails;
        private System.Windows.Forms.Label label22;
       
        private System.Windows.Forms.TextBox txtPAFax;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblContactDetails;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnMoreLess;
        private System.Windows.Forms.Panel pnl_InsuranceDetails;
        private System.Windows.Forms.ComboBox cmbGenInfoInsurance;
        private System.Windows.Forms.Button btnClrInsurance;
        private System.Windows.Forms.Button btnInsurInfo;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Providers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnl_AddressDetails;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label19;
        internal gloMaskControl.gloMaskBox txtmPASSN;  //private to internal to access in quick patient registration, 00000349 changes 
        private gloMaskControl.gloMaskBox mtxtPAMobile;
        private gloMaskControl.gloMaskBox mtxtPAPhone;
        private System.Windows.Forms.TextBox txtAlert;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlAddDetails;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblAddressDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel pnlAddresControl;
        internal System.Windows.Forms.MaskedTextBox txtPACode;
        internal System.Windows.Forms.TextBox txtPatientPrefix;
        private gloMaskControl.gloMaskBox mtxtPAFax;
        private System.Windows.Forms.Panel pnlPortalInvitaitonEmail;
        private System.Windows.Forms.CheckBox cbSendPatientPortalActivationEmail;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbSendAPIInvitation;
        internal System.Windows.Forms.TextBox txtPAEmail;
        private System.Windows.Forms.Label label18;

    }
}
