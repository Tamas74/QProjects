namespace gloPatient
{
    partial class gloPatientOccupationControl
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpRetirementDate };
            System.Windows.Forms.Control[] cntControls = { dtpRetirementDate };
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
                if (oPatientOccu != null)
                {
                    oPatientOccu.Dispose();
                    oPatientOccu = null;
                }
            }
            base.Dispose(disposing);

 

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientOccupationControl));
            this.pnlOccuInfo = new System.Windows.Forms.Panel();
            this.pnlContactDetails = new System.Windows.Forms.Panel();
            this.mskOIMobile = new gloMaskControl.gloMaskBox();
            this.mskOIPhone = new gloMaskControl.gloMaskBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOIEmail = new System.Windows.Forms.TextBox();
            this.txtOIFax = new gloMaskControl.gloMaskBox();
            this.lblOIEmail = new System.Windows.Forms.Label();
            this.lblOIFax = new System.Windows.Forms.Label();
            this.lblOIMobile = new System.Windows.Forms.Label();
            this.lblOIPhone = new System.Windows.Forms.Label();
            this.lblContactDetail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.pnlAddressDetails = new System.Windows.Forms.Panel();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.cmbWState = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtwZip = new System.Windows.Forms.TextBox();
            this.txtCounty = new System.Windows.Forms.TextBox();
            this.txtwCity = new System.Windows.Forms.TextBox();
            this.txtwAddress2 = new System.Windows.Forms.TextBox();
            this.txtwAddress1 = new System.Windows.Forms.TextBox();
            this.lblOICounty = new System.Windows.Forms.Label();
            this.lblOIZip = new System.Windows.Forms.Label();
            this.lblOIState = new System.Windows.Forms.Label();
            this.lblOICity = new System.Windows.Forms.Label();
            this.lblOIAddressLine2 = new System.Windows.Forms.Label();
            this.lblOIAddressLine1 = new System.Windows.Forms.Label();
            this.lblAddressDetailHeader = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlEmploymentDetails = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.mtxtPARetirementDate = new System.Windows.Forms.MaskedTextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblEmploymentType = new System.Windows.Forms.Label();
            this.lblRetirementDate = new System.Windows.Forms.Label();
            this.dtpRetirementDate = new System.Windows.Forms.DateTimePicker();
            this.txtEmployerName = new System.Windows.Forms.TextBox();
            this.lblEmployerName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWLocation = new System.Windows.Forms.TextBox();
            this.txtwOccupation = new System.Windows.Forms.TextBox();
            this.lblOccuPlaceofEmployment = new System.Windows.Forms.Label();
            this.cmbwEmpStatus = new System.Windows.Forms.ComboBox();
            this.lblOIOccupation = new System.Windows.Forms.Label();
            this.lblOIEmpStatus = new System.Windows.Forms.Label();
            this.lblEmploymentDetails = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlOccuInfoHeader = new System.Windows.Forms.Panel();
            this.lblOccupationInfo = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlOccuInfo.SuspendLayout();
            this.pnlContactDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlAddressDetails.SuspendLayout();
            this.pnlEmploymentDetails.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlOccuInfoHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOccuInfo
            // 
            this.pnlOccuInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlOccuInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOccuInfo.Controls.Add(this.pnlContactDetails);
            this.pnlOccuInfo.Controls.Add(this.panel2);
            this.pnlOccuInfo.Controls.Add(this.pnlAddressDetails);
            this.pnlOccuInfo.Controls.Add(this.pnlEmploymentDetails);
            this.pnlOccuInfo.Controls.Add(this.pnlTOP);
            this.pnlOccuInfo.Controls.Add(this.pnlHeader);
            this.pnlOccuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOccuInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlOccuInfo.Name = "pnlOccuInfo";
            this.pnlOccuInfo.Size = new System.Drawing.Size(750, 637);
            this.pnlOccuInfo.TabIndex = 11;
            // 
            // pnlContactDetails
            // 
            this.pnlContactDetails.Controls.Add(this.mskOIMobile);
            this.pnlContactDetails.Controls.Add(this.mskOIPhone);
            this.pnlContactDetails.Controls.Add(this.label4);
            this.pnlContactDetails.Controls.Add(this.txtOIEmail);
            this.pnlContactDetails.Controls.Add(this.txtOIFax);
            this.pnlContactDetails.Controls.Add(this.lblOIEmail);
            this.pnlContactDetails.Controls.Add(this.lblOIFax);
            this.pnlContactDetails.Controls.Add(this.lblOIMobile);
            this.pnlContactDetails.Controls.Add(this.lblOIPhone);
            this.pnlContactDetails.Controls.Add(this.lblContactDetail);
            this.pnlContactDetails.Controls.Add(this.label1);
            this.pnlContactDetails.Controls.Add(this.label2);
            this.pnlContactDetails.Controls.Add(this.label3);
            this.pnlContactDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContactDetails.Location = new System.Drawing.Point(0, 486);
            this.pnlContactDetails.Name = "pnlContactDetails";
            this.pnlContactDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlContactDetails.Size = new System.Drawing.Size(750, 126);
            this.pnlContactDetails.TabIndex = 14;
            // 
            // mskOIMobile
            // 
            this.mskOIMobile.AllowValidate = true;
            this.mskOIMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskOIMobile.IncludeLiteralsAndPrompts = false;
            this.mskOIMobile.Location = new System.Drawing.Point(144, 54);
            this.mskOIMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mskOIMobile.Name = "mskOIMobile";
            this.mskOIMobile.ReadOnly = false;
            this.mskOIMobile.Size = new System.Drawing.Size(104, 22);
            this.mskOIMobile.TabIndex = 16;
            // 
            // mskOIPhone
            // 
            this.mskOIPhone.AllowValidate = true;
            this.mskOIPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskOIPhone.IncludeLiteralsAndPrompts = false;
            this.mskOIPhone.Location = new System.Drawing.Point(144, 27);
            this.mskOIPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mskOIPhone.Name = "mskOIPhone";
            this.mskOIPhone.ReadOnly = false;
            this.mskOIPhone.Size = new System.Drawing.Size(104, 22);
            this.mskOIPhone.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(742, 1);
            this.label4.TabIndex = 12;
            // 
            // txtOIEmail
            // 
            this.txtOIEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOIEmail.Location = new System.Drawing.Point(144, 109);
            this.txtOIEmail.Name = "txtOIEmail";
            this.txtOIEmail.Size = new System.Drawing.Size(347, 22);
            this.txtOIEmail.TabIndex = 18;
            this.txtOIEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtOIEmail_Validating);
            // 
            // txtOIFax
            // 
            this.txtOIFax.AllowValidate = true;
            this.txtOIFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOIFax.ForeColor = System.Drawing.Color.Black;
            this.txtOIFax.IncludeLiteralsAndPrompts = false;
            this.txtOIFax.Location = new System.Drawing.Point(144, 81);
            this.txtOIFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtOIFax.Name = "txtOIFax";
            this.txtOIFax.ReadOnly = false;
            this.txtOIFax.Size = new System.Drawing.Size(104, 22);
            this.txtOIFax.TabIndex = 17;
            // 
            // lblOIEmail
            // 
            this.lblOIEmail.AutoSize = true;
            this.lblOIEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIEmail.Location = new System.Drawing.Point(99, 113);
            this.lblOIEmail.Name = "lblOIEmail";
            this.lblOIEmail.Size = new System.Drawing.Size(42, 14);
            this.lblOIEmail.TabIndex = 7;
            this.lblOIEmail.Text = "Email :";
            // 
            // lblOIFax
            // 
            this.lblOIFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIFax.AutoEllipsis = true;
            this.lblOIFax.AutoSize = true;
            this.lblOIFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIFax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIFax.Location = new System.Drawing.Point(108, 85);
            this.lblOIFax.Name = "lblOIFax";
            this.lblOIFax.Size = new System.Drawing.Size(33, 14);
            this.lblOIFax.TabIndex = 5;
            this.lblOIFax.Text = "Fax :";
            // 
            // lblOIMobile
            // 
            this.lblOIMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIMobile.AutoEllipsis = true;
            this.lblOIMobile.AutoSize = true;
            this.lblOIMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIMobile.Location = new System.Drawing.Point(92, 58);
            this.lblOIMobile.Name = "lblOIMobile";
            this.lblOIMobile.Size = new System.Drawing.Size(49, 14);
            this.lblOIMobile.TabIndex = 3;
            this.lblOIMobile.Text = "Mobile :";
            // 
            // lblOIPhone
            // 
            this.lblOIPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIPhone.AutoEllipsis = true;
            this.lblOIPhone.AutoSize = true;
            this.lblOIPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIPhone.Location = new System.Drawing.Point(91, 31);
            this.lblOIPhone.Name = "lblOIPhone";
            this.lblOIPhone.Size = new System.Drawing.Size(50, 14);
            this.lblOIPhone.TabIndex = 1;
            this.lblOIPhone.Text = "Phone :";
            // 
            // lblContactDetail
            // 
            this.lblContactDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContactDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContactDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblContactDetail.Location = new System.Drawing.Point(4, 2);
            this.lblContactDetail.Name = "lblContactDetail";
            this.lblContactDetail.Size = new System.Drawing.Size(742, 18);
            this.lblContactDetail.TabIndex = 0;
            this.lblContactDetail.Text = " Contact Details :";
            this.lblContactDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 121);
            this.label1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(746, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 121);
            this.label2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(744, 1);
            this.label3.TabIndex = 11;
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
            this.panel2.Location = new System.Drawing.Point(0, 612);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(750, 25);
            this.panel2.TabIndex = 71;
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
            this.label48.Location = new System.Drawing.Point(15, 4);
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
            this.label46.Location = new System.Drawing.Point(28, 4);
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
            this.label42.Location = new System.Drawing.Point(4, 21);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(742, 1);
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
            this.label44.Location = new System.Drawing.Point(746, 1);
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
            this.label45.Size = new System.Drawing.Size(744, 1);
            this.label45.TabIndex = 9;
            this.label45.Text = "label1";
            // 
            // pnlAddressDetails
            // 
            this.pnlAddressDetails.Controls.Add(this.pnlAddresssControl);
            this.pnlAddressDetails.Controls.Add(this.cmbCountry);
            this.pnlAddressDetails.Controls.Add(this.label49);
            this.pnlAddressDetails.Controls.Add(this.cmbWState);
            this.pnlAddressDetails.Controls.Add(this.label8);
            this.pnlAddressDetails.Controls.Add(this.txtwZip);
            this.pnlAddressDetails.Controls.Add(this.txtCounty);
            this.pnlAddressDetails.Controls.Add(this.txtwCity);
            this.pnlAddressDetails.Controls.Add(this.txtwAddress2);
            this.pnlAddressDetails.Controls.Add(this.txtwAddress1);
            this.pnlAddressDetails.Controls.Add(this.lblOICounty);
            this.pnlAddressDetails.Controls.Add(this.lblOIZip);
            this.pnlAddressDetails.Controls.Add(this.lblOIState);
            this.pnlAddressDetails.Controls.Add(this.lblOICity);
            this.pnlAddressDetails.Controls.Add(this.lblOIAddressLine2);
            this.pnlAddressDetails.Controls.Add(this.lblOIAddressLine1);
            this.pnlAddressDetails.Controls.Add(this.lblAddressDetailHeader);
            this.pnlAddressDetails.Controls.Add(this.label5);
            this.pnlAddressDetails.Controls.Add(this.label6);
            this.pnlAddressDetails.Controls.Add(this.label7);
            this.pnlAddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddressDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAddressDetails.Location = new System.Drawing.Point(0, 304);
            this.pnlAddressDetails.Name = "pnlAddressDetails";
            this.pnlAddressDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlAddressDetails.Size = new System.Drawing.Size(750, 182);
            this.pnlAddressDetails.TabIndex = 7;
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(63, 32);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(325, 132);
            this.pnlAddresssControl.TabIndex = 108;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "US"});
            this.cmbCountry.Location = new System.Drawing.Point(128, 139);
            this.cmbCountry.MaxDropDownItems = 3;
            this.cmbCountry.MaxLength = 20;
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(151, 22);
            this.cmbCountry.TabIndex = 14;
            this.cmbCountry.Visible = false;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(67, 142);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 28;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // cmbWState
            // 
            this.cmbWState.FormattingEnabled = true;
            this.cmbWState.Location = new System.Drawing.Point(330, 84);
            this.cmbWState.MaxLength = 20;
            this.cmbWState.Name = "cmbWState";
            this.cmbWState.Size = new System.Drawing.Size(94, 22);
            this.cmbWState.TabIndex = 12;
            this.cmbWState.Visible = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(742, 1);
            this.label8.TabIndex = 16;
            // 
            // txtwZip
            // 
            this.txtwZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwZip.ForeColor = System.Drawing.Color.Black;
            this.txtwZip.Location = new System.Drawing.Point(128, 111);
            this.txtwZip.MaxLength = 10;
            this.txtwZip.Name = "txtwZip";
            this.txtwZip.Size = new System.Drawing.Size(74, 22);
            this.txtwZip.TabIndex = 10;
            this.txtwZip.Visible = false;
            this.txtwZip.Leave += new System.EventHandler(this.txtwZip_Leave);
            this.txtwZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtwZip_KeyPress);
            // 
            // txtCounty
            // 
            this.txtCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounty.ForeColor = System.Drawing.Color.Black;
            this.txtCounty.Location = new System.Drawing.Point(266, 111);
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(159, 22);
            this.txtCounty.TabIndex = 13;
            this.txtCounty.Visible = false;
            // 
            // txtwCity
            // 
            this.txtwCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwCity.ForeColor = System.Drawing.Color.Black;
            this.txtwCity.Location = new System.Drawing.Point(128, 84);
            this.txtwCity.Name = "txtwCity";
            this.txtwCity.Size = new System.Drawing.Size(151, 22);
            this.txtwCity.TabIndex = 11;
            // 
            // txtwAddress2
            // 
            this.txtwAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtwAddress2.Location = new System.Drawing.Point(128, 58);
            this.txtwAddress2.Name = "txtwAddress2";
            this.txtwAddress2.Size = new System.Drawing.Size(296, 22);
            this.txtwAddress2.TabIndex = 9;
            this.txtwAddress2.Visible = false;
            // 
            // txtwAddress1
            // 
            this.txtwAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwAddress1.ForeColor = System.Drawing.Color.Black;
            this.txtwAddress1.Location = new System.Drawing.Point(128, 32);
            this.txtwAddress1.Name = "txtwAddress1";
            this.txtwAddress1.Size = new System.Drawing.Size(297, 22);
            this.txtwAddress1.TabIndex = 8;
            this.txtwAddress1.Visible = false;
            // 
            // lblOICounty
            // 
            this.lblOICounty.AutoSize = true;
            this.lblOICounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOICounty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOICounty.Location = new System.Drawing.Point(208, 116);
            this.lblOICounty.Name = "lblOICounty";
            this.lblOICounty.Size = new System.Drawing.Size(54, 14);
            this.lblOICounty.TabIndex = 11;
            this.lblOICounty.Text = "County :";
            this.lblOICounty.Visible = false;
            // 
            // lblOIZip
            // 
            this.lblOIZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIZip.AutoEllipsis = true;
            this.lblOIZip.AutoSize = true;
            this.lblOIZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIZip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIZip.Location = new System.Drawing.Point(94, 116);
            this.lblOIZip.Name = "lblOIZip";
            this.lblOIZip.Size = new System.Drawing.Size(31, 14);
            this.lblOIZip.TabIndex = 9;
            this.lblOIZip.Text = "Zip :";
            this.lblOIZip.Visible = false;
            // 
            // lblOIState
            // 
            this.lblOIState.AutoSize = true;
            this.lblOIState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIState.Location = new System.Drawing.Point(288, 89);
            this.lblOIState.Name = "lblOIState";
            this.lblOIState.Size = new System.Drawing.Size(45, 14);
            this.lblOIState.TabIndex = 7;
            this.lblOIState.Text = "State :";
            // 
            // lblOICity
            // 
            this.lblOICity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOICity.AutoEllipsis = true;
            this.lblOICity.AutoSize = true;
            this.lblOICity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOICity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOICity.Location = new System.Drawing.Point(94, 89);
            this.lblOICity.Name = "lblOICity";
            this.lblOICity.Size = new System.Drawing.Size(31, 14);
            this.lblOICity.TabIndex = 5;
            this.lblOICity.Text = "City:";
            this.lblOICity.Visible = false;
            // 
            // lblOIAddressLine2
            // 
            this.lblOIAddressLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine2.AutoEllipsis = true;
            this.lblOIAddressLine2.AutoSize = true;
            this.lblOIAddressLine2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine2.Location = new System.Drawing.Point(30, 63);
            this.lblOIAddressLine2.Name = "lblOIAddressLine2";
            this.lblOIAddressLine2.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine2.TabIndex = 3;
            this.lblOIAddressLine2.Text = "Address Line 2 :";
            this.lblOIAddressLine2.Visible = false;
            // 
            // lblOIAddressLine1
            // 
            this.lblOIAddressLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIAddressLine1.AutoEllipsis = true;
            this.lblOIAddressLine1.AutoSize = true;
            this.lblOIAddressLine1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIAddressLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIAddressLine1.Location = new System.Drawing.Point(30, 37);
            this.lblOIAddressLine1.Name = "lblOIAddressLine1";
            this.lblOIAddressLine1.Size = new System.Drawing.Size(95, 14);
            this.lblOIAddressLine1.TabIndex = 1;
            this.lblOIAddressLine1.Text = "Address Line 1 :";
            this.lblOIAddressLine1.Visible = false;
            // 
            // lblAddressDetailHeader
            // 
            this.lblAddressDetailHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddressDetailHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddressDetailHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressDetailHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAddressDetailHeader.Location = new System.Drawing.Point(4, 2);
            this.lblAddressDetailHeader.Name = "lblAddressDetailHeader";
            this.lblAddressDetailHeader.Size = new System.Drawing.Size(742, 18);
            this.lblAddressDetailHeader.TabIndex = 0;
            this.lblAddressDetailHeader.Text = " Address Details :";
            this.lblAddressDetailHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 177);
            this.label5.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(746, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 177);
            this.label6.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(744, 1);
            this.label7.TabIndex = 15;
            // 
            // pnlEmploymentDetails
            // 
            this.pnlEmploymentDetails.Controls.Add(this.btnModify);
            this.pnlEmploymentDetails.Controls.Add(this.btnAdd);
            this.pnlEmploymentDetails.Controls.Add(this.mtxtPARetirementDate);
            this.pnlEmploymentDetails.Controls.Add(this.cmbStatus);
            this.pnlEmploymentDetails.Controls.Add(this.lblEmploymentType);
            this.pnlEmploymentDetails.Controls.Add(this.lblRetirementDate);
            this.pnlEmploymentDetails.Controls.Add(this.dtpRetirementDate);
            this.pnlEmploymentDetails.Controls.Add(this.txtEmployerName);
            this.pnlEmploymentDetails.Controls.Add(this.lblEmployerName);
            this.pnlEmploymentDetails.Controls.Add(this.label9);
            this.pnlEmploymentDetails.Controls.Add(this.txtWLocation);
            this.pnlEmploymentDetails.Controls.Add(this.txtwOccupation);
            this.pnlEmploymentDetails.Controls.Add(this.lblOccuPlaceofEmployment);
            this.pnlEmploymentDetails.Controls.Add(this.cmbwEmpStatus);
            this.pnlEmploymentDetails.Controls.Add(this.lblOIOccupation);
            this.pnlEmploymentDetails.Controls.Add(this.lblOIEmpStatus);
            this.pnlEmploymentDetails.Controls.Add(this.lblEmploymentDetails);
            this.pnlEmploymentDetails.Controls.Add(this.label10);
            this.pnlEmploymentDetails.Controls.Add(this.label11);
            this.pnlEmploymentDetails.Controls.Add(this.label12);
            this.pnlEmploymentDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEmploymentDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlEmploymentDetails.Location = new System.Drawing.Point(0, 86);
            this.pnlEmploymentDetails.Name = "pnlEmploymentDetails";
            this.pnlEmploymentDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlEmploymentDetails.Size = new System.Drawing.Size(750, 218);
            this.pnlEmploymentDetails.TabIndex = 0;
            // 
            // btnModify
            // 
            this.btnModify.AutoEllipsis = true;
            this.btnModify.BackColor = System.Drawing.Color.Transparent;
            this.btnModify.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModify.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.Location = new System.Drawing.Point(521, 90);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(22, 22);
            this.btnModify.TabIndex = 41;
            this.toolTip1.SetToolTip(this.btnModify, "Modify Occupation");
            this.btnModify.UseVisualStyleBackColor = false;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(495, 90);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(22, 22);
            this.btnAdd.TabIndex = 40;
            this.toolTip1.SetToolTip(this.btnAdd, "Add Occupation");
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // mtxtPARetirementDate
            // 
            this.mtxtPARetirementDate.Location = new System.Drawing.Point(145, 175);
            this.mtxtPARetirementDate.Mask = "00/00/0000";
            this.mtxtPARetirementDate.Name = "mtxtPARetirementDate";
            this.mtxtPARetirementDate.Size = new System.Drawing.Size(103, 21);
            this.mtxtPARetirementDate.TabIndex = 36;
            this.mtxtPARetirementDate.ValidatingType = typeof(System.DateTime);
            this.mtxtPARetirementDate.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtPARetirementDate_Validating);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(145, 34);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(212, 22);
            this.cmbStatus.TabIndex = 1;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblEmploymentType
            // 
            this.lblEmploymentType.AutoSize = true;
            this.lblEmploymentType.BackColor = System.Drawing.Color.Transparent;
            this.lblEmploymentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmploymentType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEmploymentType.Location = new System.Drawing.Point(27, 65);
            this.lblEmploymentType.Name = "lblEmploymentType";
            this.lblEmploymentType.Size = new System.Drawing.Size(115, 14);
            this.lblEmploymentType.TabIndex = 35;
            this.lblEmploymentType.Text = "Employment Type :";
            // 
            // lblRetirementDate
            // 
            this.lblRetirementDate.AutoSize = true;
            this.lblRetirementDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetirementDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRetirementDate.Location = new System.Drawing.Point(36, 177);
            this.lblRetirementDate.Name = "lblRetirementDate";
            this.lblRetirementDate.Size = new System.Drawing.Size(106, 14);
            this.lblRetirementDate.TabIndex = 33;
            this.lblRetirementDate.Text = "Retirement Date :";
            // 
            // dtpRetirementDate
            // 
            this.dtpRetirementDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRetirementDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRetirementDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRetirementDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRetirementDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRetirementDate.CustomFormat = "MM/dd/yyyy";
            this.dtpRetirementDate.Enabled = false;
            this.dtpRetirementDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRetirementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRetirementDate.Location = new System.Drawing.Point(411, 175);
            this.dtpRetirementDate.Name = "dtpRetirementDate";
            this.dtpRetirementDate.Size = new System.Drawing.Size(103, 22);
            this.dtpRetirementDate.TabIndex = 6;
            this.dtpRetirementDate.Visible = false;
            // 
            // txtEmployerName
            // 
            this.txtEmployerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtEmployerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtEmployerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployerName.ForeColor = System.Drawing.Color.Black;
            this.txtEmployerName.Location = new System.Drawing.Point(145, 90);
            this.txtEmployerName.Name = "txtEmployerName";
            this.txtEmployerName.Size = new System.Drawing.Size(346, 22);
            this.txtEmployerName.TabIndex = 3;
            this.txtEmployerName.Validated += new System.EventHandler(this.txtEmployerName_Validated);
            this.txtEmployerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmployerName_KeyPress);
            // 
            // lblEmployerName
            // 
            this.lblEmployerName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmployerName.AutoEllipsis = true;
            this.lblEmployerName.AutoSize = true;
            this.lblEmployerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEmployerName.Location = new System.Drawing.Point(77, 93);
            this.lblEmployerName.Name = "lblEmployerName";
            this.lblEmployerName.Size = new System.Drawing.Size(65, 14);
            this.lblEmployerName.TabIndex = 17;
            this.lblEmployerName.Text = "Employer :";
            this.lblEmployerName.Click += new System.EventHandler(this.lblEmployerName_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(742, 1);
            this.label9.TabIndex = 13;
            // 
            // txtWLocation
            // 
            this.txtWLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWLocation.ForeColor = System.Drawing.Color.Black;
            this.txtWLocation.Location = new System.Drawing.Point(145, 147);
            this.txtWLocation.Name = "txtWLocation";
            this.txtWLocation.Size = new System.Drawing.Size(346, 22);
            this.txtWLocation.TabIndex = 5;
            // 
            // txtwOccupation
            // 
            this.txtwOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtwOccupation.ForeColor = System.Drawing.Color.Black;
            this.txtwOccupation.Location = new System.Drawing.Point(145, 118);
            this.txtwOccupation.Name = "txtwOccupation";
            this.txtwOccupation.Size = new System.Drawing.Size(346, 22);
            this.txtwOccupation.TabIndex = 4;
            // 
            // lblOccuPlaceofEmployment
            // 
            this.lblOccuPlaceofEmployment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOccuPlaceofEmployment.AutoEllipsis = true;
            this.lblOccuPlaceofEmployment.AutoSize = true;
            this.lblOccuPlaceofEmployment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccuPlaceofEmployment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOccuPlaceofEmployment.Location = new System.Drawing.Point(12, 150);
            this.lblOccuPlaceofEmployment.Name = "lblOccuPlaceofEmployment";
            this.lblOccuPlaceofEmployment.Size = new System.Drawing.Size(130, 14);
            this.lblOccuPlaceofEmployment.TabIndex = 5;
            this.lblOccuPlaceofEmployment.Text = "Place of Employment :";
            // 
            // cmbwEmpStatus
            // 
            this.cmbwEmpStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbwEmpStatus.Enabled = false;
            this.cmbwEmpStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbwEmpStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbwEmpStatus.FormattingEnabled = true;
            this.cmbwEmpStatus.Location = new System.Drawing.Point(145, 62);
            this.cmbwEmpStatus.Name = "cmbwEmpStatus";
            this.cmbwEmpStatus.Size = new System.Drawing.Size(212, 22);
            this.cmbwEmpStatus.TabIndex = 2;
            this.cmbwEmpStatus.SelectedIndexChanged += new System.EventHandler(this.cmbwEmpStatus_SelectedIndexChanged);
            // 
            // lblOIOccupation
            // 
            this.lblOIOccupation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIOccupation.AutoEllipsis = true;
            this.lblOIOccupation.AutoSize = true;
            this.lblOIOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIOccupation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIOccupation.Location = new System.Drawing.Point(65, 121);
            this.lblOIOccupation.Name = "lblOIOccupation";
            this.lblOIOccupation.Size = new System.Drawing.Size(77, 14);
            this.lblOIOccupation.TabIndex = 3;
            this.lblOIOccupation.Text = "Occupation :";
            // 
            // lblOIEmpStatus
            // 
            this.lblOIEmpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOIEmpStatus.AutoEllipsis = true;
            this.lblOIEmpStatus.AutoSize = true;
            this.lblOIEmpStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOIEmpStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOIEmpStatus.Location = new System.Drawing.Point(20, 37);
            this.lblOIEmpStatus.Name = "lblOIEmpStatus";
            this.lblOIEmpStatus.Size = new System.Drawing.Size(122, 14);
            this.lblOIEmpStatus.TabIndex = 1;
            this.lblOIEmpStatus.Text = "Employment Status :";
            // 
            // lblEmploymentDetails
            // 
            this.lblEmploymentDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEmploymentDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblEmploymentDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmploymentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEmploymentDetails.Location = new System.Drawing.Point(4, 2);
            this.lblEmploymentDetails.Name = "lblEmploymentDetails";
            this.lblEmploymentDetails.Size = new System.Drawing.Size(742, 18);
            this.lblEmploymentDetails.TabIndex = 0;
            this.lblEmploymentDetails.Text = " Employment Details :";
            this.lblEmploymentDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 213);
            this.label10.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(746, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 213);
            this.label11.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(744, 1);
            this.label12.TabIndex = 16;
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Location = new System.Drawing.Point(0, 30);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(750, 56);
            this.pnlTOP.TabIndex = 26;
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
            this.ts_Commands.Padding = new System.Windows.Forms.Padding(0);
            this.ts_Commands.Size = new System.Drawing.Size(744, 53);
            this.ts_Commands.TabIndex = 21;
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
            this.tsb_OK.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            this.tsb_OK.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
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
            this.tsb_Cancel.MouseHover += new System.EventHandler(this.tsb_MouseHover);
            this.tsb_Cancel.MouseLeave += new System.EventHandler(this.tsb_MouseLeave);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.pnlOccuInfoHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlHeader.Size = new System.Drawing.Size(750, 30);
            this.pnlHeader.TabIndex = 27;
            // 
            // pnlOccuInfoHeader
            // 
            this.pnlOccuInfoHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlOccuInfoHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlOccuInfoHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlOccuInfoHeader.Controls.Add(this.lblOccupationInfo);
            this.pnlOccuInfoHeader.Controls.Add(this.label18);
            this.pnlOccuInfoHeader.Controls.Add(this.label14);
            this.pnlOccuInfoHeader.Controls.Add(this.label17);
            this.pnlOccuInfoHeader.Controls.Add(this.label16);
            this.pnlOccuInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOccuInfoHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlOccuInfoHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlOccuInfoHeader.Name = "pnlOccuInfoHeader";
            this.pnlOccuInfoHeader.Size = new System.Drawing.Size(744, 24);
            this.pnlOccuInfoHeader.TabIndex = 0;
            // 
            // lblOccupationInfo
            // 
            this.lblOccupationInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblOccupationInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOccupationInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOccupationInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupationInfo.ForeColor = System.Drawing.Color.White;
            this.lblOccupationInfo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblOccupationInfo.Location = new System.Drawing.Point(1, 1);
            this.lblOccupationInfo.Margin = new System.Windows.Forms.Padding(3);
            this.lblOccupationInfo.Name = "lblOccupationInfo";
            this.lblOccupationInfo.Size = new System.Drawing.Size(742, 21);
            this.lblOccupationInfo.TabIndex = 0;
            this.lblOccupationInfo.Text = "  Occupation Information";
            this.lblOccupationInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 22);
            this.label18.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(743, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 22);
            this.label14.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(0, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(744, 1);
            this.label17.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(744, 1);
            this.label16.TabIndex = 25;
            // 
            // gloPatientOccupationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlOccuInfo);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientOccupationControl";
            this.Size = new System.Drawing.Size(750, 637);
            this.Load += new System.EventHandler(this.gloPatientOccupation_Load);
            this.pnlOccuInfo.ResumeLayout(false);
            this.pnlContactDetails.ResumeLayout(false);
            this.pnlContactDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlAddressDetails.ResumeLayout(false);
            this.pnlAddressDetails.PerformLayout();
            this.pnlEmploymentDetails.ResumeLayout(false);
            this.pnlEmploymentDetails.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlOccuInfoHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOccuInfo;
        private System.Windows.Forms.Panel pnlContactDetails;
        private System.Windows.Forms.TextBox txtOIEmail;
        private  gloMaskControl.gloMaskBox  txtOIFax;
        private System.Windows.Forms.Label lblOIEmail;
        private System.Windows.Forms.Label lblOIFax;
        private System.Windows.Forms.Label lblOIMobile;
        private System.Windows.Forms.Label lblOIPhone;
        private System.Windows.Forms.Label lblContactDetail;
        private System.Windows.Forms.Panel pnlAddressDetails;
        private System.Windows.Forms.TextBox txtwZip;
        private System.Windows.Forms.TextBox txtCounty;
        private System.Windows.Forms.TextBox txtwCity;
        private System.Windows.Forms.TextBox txtwAddress2;
        private System.Windows.Forms.TextBox txtwAddress1;
        private System.Windows.Forms.Label lblOICounty;
        private System.Windows.Forms.Label lblOIZip;
        private System.Windows.Forms.Label lblOIState;
        private System.Windows.Forms.Label lblOICity;
        private System.Windows.Forms.Label lblOIAddressLine2;
        private System.Windows.Forms.Label lblOIAddressLine1;
        private System.Windows.Forms.Label lblAddressDetailHeader;
        private System.Windows.Forms.Panel pnlEmploymentDetails;
        private System.Windows.Forms.TextBox txtWLocation;
        private System.Windows.Forms.TextBox txtwOccupation;
        private System.Windows.Forms.Label lblOccuPlaceofEmployment;
        private System.Windows.Forms.ComboBox cmbwEmpStatus;
        private System.Windows.Forms.Label lblOIOccupation;
        private System.Windows.Forms.Label lblOIEmpStatus;
        private System.Windows.Forms.Label lblEmploymentDetails;
        private System.Windows.Forms.Panel pnlOccuInfoHeader;
        private System.Windows.Forms.Label lblOccupationInfo;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtEmployerName;
        private System.Windows.Forms.Label lblEmployerName;
        private System.Windows.Forms.Label lblRetirementDate;
        private System.Windows.Forms.DateTimePicker dtpRetirementDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblEmploymentType;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.MaskedTextBox mtxtPARetirementDate;
        private gloMaskControl.gloMaskBox mskOIMobile;
        private gloMaskControl.gloMaskBox mskOIPhone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbWState;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
