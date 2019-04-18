namespace gloPatient
{
    partial class gloPatientAddAccountControl
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
                    if (oToolTip1 != null)
                    {
                        oToolTip1.Dispose();
                        oToolTip1 = null;
                    }
                }
                catch
                {
                }
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
                    if (oPatientGuardian != null)
                    {
                        oPatientGuardian.Dispose();
                        oPatientGuardian = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (oPatientDemographicsDetails != null)
                    {
                        oPatientDemographicsDetails.Dispose();
                        oPatientDemographicsDetails = null;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientAddAccountControl));
            this.pnlAddAccInfo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtAccountDescription = new System.Windows.Forms.TextBox();
            this.chkExcludefromStatement = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.chkSetToCollection = new System.Windows.Forms.CheckBox();
            this.txtAccGuarantor = new System.Windows.Forms.TextBox();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.btnGuarantorExistingPatientBrowse = new System.Windows.Forms.Button();
            this.btnNewGuarantor = new System.Windows.Forms.Button();
            this.cmbSameAsGuardian = new System.Windows.Forms.ComboBox();
            this.lblSameAsGuardian = new System.Windows.Forms.Label();
            this.lblGuarantorDetails = new System.Windows.Forms.Label();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCenterCaption = new System.Windows.Forms.Label();
            this.lblBusinessCenter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label64 = new System.Windows.Forms.Label();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.rbExisting = new System.Windows.Forms.RadioButton();
            this.label59 = new System.Windows.Forms.Label();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.btnExistingAccountDelete = new System.Windows.Forms.Button();
            this.btnExistingAccountSelect = new System.Windows.Forms.Button();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlAddAccInfo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAddAccInfo
            // 
            this.pnlAddAccInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAddAccInfo.Controls.Add(this.panel2);
            this.pnlAddAccInfo.Controls.Add(this.pnlBusinessCenter);
            this.pnlAddAccInfo.Controls.Add(this.panel1);
            this.pnlAddAccInfo.Controls.Add(this.lbl_BottomBrd);
            this.pnlAddAccInfo.Controls.Add(this.lbl_LeftBrd);
            this.pnlAddAccInfo.Controls.Add(this.lbl_RightBrd);
            this.pnlAddAccInfo.Controls.Add(this.lbl_TopBrd);
            this.pnlAddAccInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddAccInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddAccInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlAddAccInfo.Name = "pnlAddAccInfo";
            this.pnlAddAccInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pnlAddAccInfo.Size = new System.Drawing.Size(435, 225);
            this.pnlAddAccInfo.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtAccountDescription);
            this.panel2.Controls.Add(this.chkExcludefromStatement);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblAccountDescription);
            this.panel2.Controls.Add(this.chkSetToCollection);
            this.panel2.Controls.Add(this.txtAccGuarantor);
            this.panel2.Controls.Add(this.lblGuarantor);
            this.panel2.Controls.Add(this.btnGuarantorExistingPatientBrowse);
            this.panel2.Controls.Add(this.btnNewGuarantor);
            this.panel2.Controls.Add(this.cmbSameAsGuardian);
            this.panel2.Controls.Add(this.lblSameAsGuardian);
            this.panel2.Controls.Add(this.lblGuarantorDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(427, 125);
            this.panel2.TabIndex = 101;
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.AcceptsReturn = true;
            this.txtAccountDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountDescription.Location = new System.Drawing.Point(112, 3);
            this.txtAccountDescription.MaxLength = 255;
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.Size = new System.Drawing.Size(217, 22);
            this.txtAccountDescription.TabIndex = 86;
            // 
            // chkExcludefromStatement
            // 
            this.chkExcludefromStatement.AutoSize = true;
            this.chkExcludefromStatement.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExcludefromStatement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkExcludefromStatement.Location = new System.Drawing.Point(112, 83);
            this.chkExcludefromStatement.Name = "chkExcludefromStatement";
            this.chkExcludefromStatement.Size = new System.Drawing.Size(158, 18);
            this.chkExcludefromStatement.TabIndex = 74;
            this.chkExcludefromStatement.Text = "Exclude from statement";
            this.chkExcludefromStatement.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 96;
            this.label1.Text = "*";
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountDescription.AutoEllipsis = true;
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAccountDescription.Location = new System.Drawing.Point(34, 7);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(74, 14);
            this.lblAccountDescription.TabIndex = 85;
            this.lblAccountDescription.Text = "Acct. Desc :";
            // 
            // chkSetToCollection
            // 
            this.chkSetToCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSetToCollection.AutoSize = true;
            this.chkSetToCollection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSetToCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkSetToCollection.Location = new System.Drawing.Point(278, 83);
            this.chkSetToCollection.Name = "chkSetToCollection";
            this.chkSetToCollection.Size = new System.Drawing.Size(123, 18);
            this.chkSetToCollection.TabIndex = 75;
            this.chkSetToCollection.Text = "Sent to collection";
            this.chkSetToCollection.UseVisualStyleBackColor = true;
            // 
            // txtAccGuarantor
            // 
            this.txtAccGuarantor.AcceptsReturn = true;
            this.txtAccGuarantor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccGuarantor.Location = new System.Drawing.Point(112, 28);
            this.txtAccGuarantor.MaxLength = 255;
            this.txtAccGuarantor.Name = "txtAccGuarantor";
            this.txtAccGuarantor.ReadOnly = true;
            this.txtAccGuarantor.Size = new System.Drawing.Size(217, 22);
            this.txtAccGuarantor.TabIndex = 94;
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantor.AutoEllipsis = true;
            this.lblGuarantor.AutoSize = true;
            this.lblGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGuarantor.Location = new System.Drawing.Point(34, 32);
            this.lblGuarantor.MaximumSize = new System.Drawing.Size(74, 14);
            this.lblGuarantor.MinimumSize = new System.Drawing.Size(74, 14);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(74, 14);
            this.lblGuarantor.TabIndex = 76;
            this.lblGuarantor.Text = "Guarantor :";
            this.lblGuarantor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuarantorExistingPatientBrowse
            // 
            this.btnGuarantorExistingPatientBrowse.AutoEllipsis = true;
            this.btnGuarantorExistingPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.BackgroundImage")));
            this.btnGuarantorExistingPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorExistingPatientBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnGuarantorExistingPatientBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorExistingPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.Image")));
            this.btnGuarantorExistingPatientBrowse.Location = new System.Drawing.Point(334, 28);
            this.btnGuarantorExistingPatientBrowse.Name = "btnGuarantorExistingPatientBrowse";
            this.btnGuarantorExistingPatientBrowse.Size = new System.Drawing.Size(21, 22);
            this.btnGuarantorExistingPatientBrowse.TabIndex = 78;
            this.btnGuarantorExistingPatientBrowse.UseVisualStyleBackColor = false;
            this.btnGuarantorExistingPatientBrowse.Click += new System.EventHandler(this.btnGuarantorExistingPatientBrowse_Click);
            // 
            // btnNewGuarantor
            // 
            this.btnNewGuarantor.AutoEllipsis = true;
            this.btnNewGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.BackgroundImage")));
            this.btnNewGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGuarantor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnNewGuarantor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.Image")));
            this.btnNewGuarantor.Location = new System.Drawing.Point(358, 28);
            this.btnNewGuarantor.Name = "btnNewGuarantor";
            this.btnNewGuarantor.Size = new System.Drawing.Size(21, 22);
            this.btnNewGuarantor.TabIndex = 79;
            this.btnNewGuarantor.UseVisualStyleBackColor = false;
            this.btnNewGuarantor.Click += new System.EventHandler(this.btnNewGuarantor_Click);
            // 
            // cmbSameAsGuardian
            // 
            this.cmbSameAsGuardian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbSameAsGuardian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSameAsGuardian.FormattingEnabled = true;
            this.cmbSameAsGuardian.Location = new System.Drawing.Point(112, 54);
            this.cmbSameAsGuardian.Name = "cmbSameAsGuardian";
            this.cmbSameAsGuardian.Size = new System.Drawing.Size(217, 22);
            this.cmbSameAsGuardian.TabIndex = 89;
            this.cmbSameAsGuardian.SelectedIndexChanged += new System.EventHandler(this.cmbSameAsGuardian_SelectedIndexChanged);
            // 
            // lblSameAsGuardian
            // 
            this.lblSameAsGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSameAsGuardian.AutoEllipsis = true;
            this.lblSameAsGuardian.AutoSize = true;
            this.lblSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSameAsGuardian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSameAsGuardian.Location = new System.Drawing.Point(48, 58);
            this.lblSameAsGuardian.Name = "lblSameAsGuardian";
            this.lblSameAsGuardian.Size = new System.Drawing.Size(60, 14);
            this.lblSameAsGuardian.TabIndex = 88;
            this.lblSameAsGuardian.Text = "Same as :";
            // 
            // lblGuarantorDetails
            // 
            this.lblGuarantorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantorDetails.AutoEllipsis = true;
            this.lblGuarantorDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantorDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGuarantorDetails.Location = new System.Drawing.Point(112, 32);
            this.lblGuarantorDetails.Name = "lblGuarantorDetails";
            this.lblGuarantorDetails.Size = new System.Drawing.Size(221, 68);
            this.lblGuarantorDetails.TabIndex = 87;
            this.lblGuarantorDetails.Text = "GuarantorDetails";
            this.lblGuarantorDetails.Visible = false;
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenterCaption);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(4, 70);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(427, 26);
            this.pnlBusinessCenter.TabIndex = 100;
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(112, 2);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(217, 22);
            this.cmbBusinessCenter.TabIndex = 5;
            this.cmbBusinessCenter.SelectedIndexChanged += new System.EventHandler(this.cmbBusinessCenter_SelectedIndexChanged);
            this.cmbBusinessCenter.MouseEnter += new System.EventHandler(this.cmbBusinessCenter_MouseEnter);
            // 
            // lblBusinessCenterCaption
            // 
            this.lblBusinessCenterCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCenterCaption.AutoEllipsis = true;
            this.lblBusinessCenterCaption.AutoSize = true;
            this.lblBusinessCenterCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenterCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenterCaption.Location = new System.Drawing.Point(5, 6);
            this.lblBusinessCenterCaption.Name = "lblBusinessCenterCaption";
            this.lblBusinessCenterCaption.Size = new System.Drawing.Size(101, 14);
            this.lblBusinessCenterCaption.TabIndex = 97;
            this.lblBusinessCenterCaption.Text = "Business Center :";
            // 
            // lblBusinessCenter
            // 
            this.lblBusinessCenter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusinessCenter.AutoEllipsis = true;
            this.lblBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter.Location = new System.Drawing.Point(108, 6);
            this.lblBusinessCenter.Name = "lblBusinessCenter";
            this.lblBusinessCenter.Size = new System.Drawing.Size(270, 13);
            this.lblBusinessCenter.TabIndex = 99;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label64);
            this.panel1.Controls.Add(this.lblAccountNo);
            this.panel1.Controls.Add(this.txtAccountNo);
            this.panel1.Controls.Add(this.rbExisting);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.rbNew);
            this.panel1.Controls.Add(this.btnExistingAccountDelete);
            this.panel1.Controls.Add(this.btnExistingAccountSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 66);
            this.panel1.TabIndex = 97;
            // 
            // label64
            // 
            this.label64.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label64.AutoEllipsis = true;
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Location = new System.Drawing.Point(7, 6);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(62, 14);
            this.label64.TabIndex = 80;
            this.label64.Text = "Account ";
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountNo.AutoEllipsis = true;
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAccountNo.Location = new System.Drawing.Point(54, 45);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(53, 14);
            this.lblAccountNo.TabIndex = 83;
            this.lblAccountNo.Text = "Acct.# :";
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtAccountNo.Location = new System.Drawing.Point(112, 41);
            this.txtAccountNo.MaxLength = 50;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(217, 22);
            this.txtAccountNo.TabIndex = 84;
            // 
            // rbExisting
            // 
            this.rbExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbExisting.AutoSize = true;
            this.rbExisting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExisting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbExisting.Location = new System.Drawing.Point(171, 19);
            this.rbExisting.Name = "rbExisting";
            this.rbExisting.Size = new System.Drawing.Size(66, 18);
            this.rbExisting.TabIndex = 82;
            this.rbExisting.Text = "Existing";
            this.rbExisting.UseVisualStyleBackColor = true;
            this.rbExisting.CheckedChanged += new System.EventHandler(this.rbExisting_CheckedChanged);
            // 
            // label59
            // 
            this.label59.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label59.AutoEllipsis = true;
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.Red;
            this.label59.Location = new System.Drawing.Point(43, 45);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(14, 14);
            this.label59.TabIndex = 92;
            this.label59.Text = "*";
            // 
            // rbNew
            // 
            this.rbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNew.AutoSize = true;
            this.rbNew.Checked = true;
            this.rbNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rbNew.Location = new System.Drawing.Point(113, 19);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(50, 18);
            this.rbNew.TabIndex = 81;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "New";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // btnExistingAccountDelete
            // 
            this.btnExistingAccountDelete.AutoEllipsis = true;
            this.btnExistingAccountDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExistingAccountDelete.BackgroundImage")));
            this.btnExistingAccountDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExistingAccountDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnExistingAccountDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExistingAccountDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingAccountDelete.Image")));
            this.btnExistingAccountDelete.Location = new System.Drawing.Point(359, 41);
            this.btnExistingAccountDelete.Name = "btnExistingAccountDelete";
            this.btnExistingAccountDelete.Size = new System.Drawing.Size(22, 22);
            this.btnExistingAccountDelete.TabIndex = 91;
            this.btnExistingAccountDelete.UseVisualStyleBackColor = false;
            this.btnExistingAccountDelete.Visible = false;
            this.btnExistingAccountDelete.Click += new System.EventHandler(this.btnExistingAccountDelete_Click);
            // 
            // btnExistingAccountSelect
            // 
            this.btnExistingAccountSelect.AutoEllipsis = true;
            this.btnExistingAccountSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountSelect.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnExistingAccountSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExistingAccountSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnExistingAccountSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExistingAccountSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingAccountSelect.Image")));
            this.btnExistingAccountSelect.Location = new System.Drawing.Point(334, 41);
            this.btnExistingAccountSelect.Name = "btnExistingAccountSelect";
            this.btnExistingAccountSelect.Size = new System.Drawing.Size(22, 22);
            this.btnExistingAccountSelect.TabIndex = 90;
            this.btnExistingAccountSelect.UseVisualStyleBackColor = false;
            this.btnExistingAccountSelect.Visible = false;
            this.btnExistingAccountSelect.Click += new System.EventHandler(this.btnExistingAccountSelect_Click);
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 221);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(427, 1);
            this.lbl_BottomBrd.TabIndex = 30;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 218);
            this.lbl_LeftBrd.TabIndex = 29;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(431, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 218);
            this.lbl_RightBrd.TabIndex = 28;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(429, 1);
            this.lbl_TopBrd.TabIndex = 27;
            this.lbl_TopBrd.Text = "label1";
            // 
            // gloPatientAddAccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnlAddAccInfo);
            this.Name = "gloPatientAddAccountControl";
            this.Size = new System.Drawing.Size(435, 225);
            this.Load += new System.EventHandler(this.gloPatientAddAccountControl_Load);
            this.pnlAddAccInfo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAddAccInfo;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button btnExistingAccountDelete;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Button btnExistingAccountSelect;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.ComboBox cmbSameAsGuardian;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lblSameAsGuardian;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Label lblGuarantorDetails;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Button btnNewGuarantor;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.Button btnGuarantorExistingPatientBrowse;
        private System.Windows.Forms.RadioButton rbExisting;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.CheckBox chkSetToCollection;
        private System.Windows.Forms.TextBox txtAccountDescription;
        private System.Windows.Forms.Label lblAccountDescription;
        private System.Windows.Forms.CheckBox chkExcludefromStatement;
        private System.Windows.Forms.TextBox txtAccGuarantor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenter;
        private System.Windows.Forms.ComboBox cmbBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenterCaption;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
