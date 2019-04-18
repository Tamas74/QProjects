namespace gloCardScanning
{
    partial class frmScanCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanCard));
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axms_InsuranceCard = new AxMEDICSDKLib.AxMedicSdk();
            this.ts_Commands = new System.Windows.Forms.ToolStrip();
            this.tsb_InsuranceCard = new System.Windows.Forms.ToolStripButton();
            this.tsb_DriversLicense = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cheque = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearData = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.pnlInsuranceDetails = new System.Windows.Forms.Panel();
            this.txtIns_SSN = new gloMaskControl.gloMaskBox();
            this.label75 = new System.Windows.Forms.Label();
            this.txtCopay = new System.Windows.Forms.TextBox();
            this.txtIns_ExpiryDate = new System.Windows.Forms.MaskedTextBox();
            this.txtIns_EffectiveDate = new System.Windows.Forms.MaskedTextBox();
            this.txtIns_DOB = new System.Windows.Forms.MaskedTextBox();
            this.txtInsCounty = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInsOther = new System.Windows.Forms.RadioButton();
            this.rbInsFemale = new System.Windows.Forms.RadioButton();
            this.rbInsMale = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIns_PatientCode = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtIns_PatientLastName = new System.Windows.Forms.TextBox();
            this.txtIns_PatientFirstName = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmb_Ins_Providers = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtIns_CopayOV = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtIns_CopayER = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtIns_CopaySP = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtIns_CopayUC = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txtIns_Zip = new System.Windows.Forms.TextBox();
            this.txtIns_State = new System.Windows.Forms.TextBox();
            this.txtIns_City = new System.Windows.Forms.TextBox();
            this.txtIns_Address = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.txtIns_PayerID = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtIns_MemberID = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtIns_GroupNo = new System.Windows.Forms.TextBox();
            this.txtIns_ContractNo = new System.Windows.Forms.TextBox();
            this.txtIns_MemberName = new System.Windows.Forms.TextBox();
            this.txtIns_PlanProvider = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.pnlLicenseDetails = new System.Windows.Forms.Panel();
            this.txtLicDOB = new System.Windows.Forms.MaskedTextBox();
            this.txtLicCounty = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.gbPAGender = new System.Windows.Forms.GroupBox();
            this.rbLicOthers = new System.Windows.Forms.RadioButton();
            this.rbLicFemale = new System.Windows.Forms.RadioButton();
            this.rbLicMale = new System.Windows.Forms.RadioButton();
            this.txtLicMiddleName = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtLic_PatientCode = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cmb_DLID_Providers = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLicSSn = new gloMaskControl.gloMaskBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLicNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbFaceImage = new System.Windows.Forms.PictureBox();
            this.txtLicZip = new System.Windows.Forms.TextBox();
            this.txtLicState = new System.Windows.Forms.TextBox();
            this.txtLicCity = new System.Windows.Forms.TextBox();
            this.txtLicAddress = new System.Windows.Forms.TextBox();
            this.txtLicLastName = new System.Windows.Forms.TextBox();
            this.txtLicFirstName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLicID = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlTemplateDetailsHeader = new System.Windows.Forms.Panel();
            this.lblCardDetails = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlCardLeft = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.pb_FrontSide = new System.Windows.Forms.PictureBox();
            this.pb_BackSide = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.pnlCheck = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.txtRoutingNo = new System.Windows.Forms.TextBox();
            this.lblRoutingNumber = new System.Windows.Forms.Label();
            this.txtCheckNo = new System.Windows.Forms.TextBox();
            this.lblCheckNumber = new System.Windows.Forms.Label();
            this.txtMICR = new System.Windows.Forms.TextBox();
            this.lblMICR = new System.Windows.Forms.Label();
            this.txtIssuingBank = new System.Windows.Forms.TextBox();
            this.lblIssuingBank = new System.Windows.Forms.Label();
            this.txtIssuingCompany = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.txtCheckAmount = new System.Windows.Forms.TextBox();
            this.lblCheckAmount = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pb_ChequeImage = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axms_InsuranceCard)).BeginInit();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlInsuranceDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlLicenseDetails.SuspendLayout();
            this.gbPAGender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFaceImage)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlTemplateDetailsHeader.SuspendLayout();
            this.pnlCardLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlCheck.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ChequeImage)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.Location = new System.Drawing.Point(326, 24);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(277, 22);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.Text = "F:\\Developer Working Folder\\Temp\\Sample.bmp";
            this.txtFilePath.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axms_InsuranceCard);
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 54);
            this.panel1.TabIndex = 12;
            // 
            // axms_InsuranceCard
            // 
            this.axms_InsuranceCard.Enabled = true;
            this.axms_InsuranceCard.Location = new System.Drawing.Point(579, 11);
            this.axms_InsuranceCard.Name = "axms_InsuranceCard";
            this.axms_InsuranceCard.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axms_InsuranceCard.OcxState")));
            this.axms_InsuranceCard.Size = new System.Drawing.Size(35, 35);
            this.axms_InsuranceCard.TabIndex = 1;
            this.axms_InsuranceCard.Visible = false;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_InsuranceCard,
            this.tsb_DriversLicense,
            this.tsb_Cheque,
            this.tsb_ClearData,
            this.tsb_Print,
            this.tsb_Save,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(738, 53);
            this.ts_Commands.TabIndex = 16;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_InsuranceCard
            // 
            this.tsb_InsuranceCard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_InsuranceCard.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsuranceCard.Image")));
            this.tsb_InsuranceCard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsuranceCard.Name = "tsb_InsuranceCard";
            this.tsb_InsuranceCard.Size = new System.Drawing.Size(103, 50);
            this.tsb_InsuranceCard.Tag = "Insurance";
            this.tsb_InsuranceCard.Text = "&Insurance Card";
            this.tsb_InsuranceCard.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_InsuranceCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsuranceCard.Click += new System.EventHandler(this.tsb_InsuranceCard_Click);
            // 
            // tsb_DriversLicense
            // 
            this.tsb_DriversLicense.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DriversLicense.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DriversLicense.Image")));
            this.tsb_DriversLicense.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DriversLicense.Name = "tsb_DriversLicense";
            this.tsb_DriversLicense.Size = new System.Drawing.Size(80, 50);
            this.tsb_DriversLicense.Tag = "Driving";
            this.tsb_DriversLicense.Text = "&DL/ID Card";
            this.tsb_DriversLicense.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_DriversLicense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DriversLicense.Click += new System.EventHandler(this.tsb_DriversLicense_Click);
            // 
            // tsb_Cheque
            // 
            this.tsb_Cheque.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cheque.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cheque.Image")));
            this.tsb_Cheque.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cheque.Name = "tsb_Cheque";
            this.tsb_Cheque.Size = new System.Drawing.Size(57, 50);
            this.tsb_Cheque.Tag = "Cheque";
            this.tsb_Cheque.Text = "Che&que";
            this.tsb_Cheque.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cheque.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cheque.Click += new System.EventHandler(this.tsb_Cheque_Click);
            // 
            // tsb_ClearData
            // 
            this.tsb_ClearData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClearData.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClearData.Image")));
            this.tsb_ClearData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearData.Name = "tsb_ClearData";
            this.tsb_ClearData.Size = new System.Drawing.Size(41, 50);
            this.tsb_ClearData.Tag = "Clear";
            this.tsb_ClearData.Text = "Cl&ear";
            this.tsb_ClearData.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ClearData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClearData.Click += new System.EventHandler(this.tsb_ClearData_Click);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlDetails);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.pnlCardLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(738, 575);
            this.pnlMain.TabIndex = 8;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.pnlInsuranceDetails);
            this.pnlDetails.Controls.Add(this.pnlLicenseDetails);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(358, 29);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(380, 546);
            this.pnlDetails.TabIndex = 110;
            // 
            // pnlInsuranceDetails
            // 
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_SSN);
            this.pnlInsuranceDetails.Controls.Add(this.label75);
            this.pnlInsuranceDetails.Controls.Add(this.txtCopay);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_ExpiryDate);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_EffectiveDate);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_DOB);
            this.pnlInsuranceDetails.Controls.Add(this.txtInsCounty);
            this.pnlInsuranceDetails.Controls.Add(this.label64);
            this.pnlInsuranceDetails.Controls.Add(this.label58);
            this.pnlInsuranceDetails.Controls.Add(this.groupBox1);
            this.pnlInsuranceDetails.Controls.Add(this.label2);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_PatientCode);
            this.pnlInsuranceDetails.Controls.Add(this.label57);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_PatientLastName);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_PatientFirstName);
            this.pnlInsuranceDetails.Controls.Add(this.label26);
            this.pnlInsuranceDetails.Controls.Add(this.label24);
            this.pnlInsuranceDetails.Controls.Add(this.label25);
            this.pnlInsuranceDetails.Controls.Add(this.cmb_Ins_Providers);
            this.pnlInsuranceDetails.Controls.Add(this.label23);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_CopayOV);
            this.pnlInsuranceDetails.Controls.Add(this.label49);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_CopayER);
            this.pnlInsuranceDetails.Controls.Add(this.label48);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_CopaySP);
            this.pnlInsuranceDetails.Controls.Add(this.label47);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_CopayUC);
            this.pnlInsuranceDetails.Controls.Add(this.label46);
            this.pnlInsuranceDetails.Controls.Add(this.label41);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_Zip);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_State);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_City);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_Address);
            this.pnlInsuranceDetails.Controls.Add(this.label42);
            this.pnlInsuranceDetails.Controls.Add(this.label43);
            this.pnlInsuranceDetails.Controls.Add(this.label44);
            this.pnlInsuranceDetails.Controls.Add(this.label45);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_PayerID);
            this.pnlInsuranceDetails.Controls.Add(this.label28);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_MemberID);
            this.pnlInsuranceDetails.Controls.Add(this.label27);
            this.pnlInsuranceDetails.Controls.Add(this.label14);
            this.pnlInsuranceDetails.Controls.Add(this.label15);
            this.pnlInsuranceDetails.Controls.Add(this.label16);
            this.pnlInsuranceDetails.Controls.Add(this.label17);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_GroupNo);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_ContractNo);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_MemberName);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_PlanProvider);
            this.pnlInsuranceDetails.Controls.Add(this.label35);
            this.pnlInsuranceDetails.Controls.Add(this.label36);
            this.pnlInsuranceDetails.Controls.Add(this.label37);
            this.pnlInsuranceDetails.Controls.Add(this.label38);
            this.pnlInsuranceDetails.Controls.Add(this.label39);
            this.pnlInsuranceDetails.Controls.Add(this.label40);
            this.pnlInsuranceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsuranceDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlInsuranceDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlInsuranceDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlInsuranceDetails.Name = "pnlInsuranceDetails";
            this.pnlInsuranceDetails.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlInsuranceDetails.Size = new System.Drawing.Size(380, 546);
            this.pnlInsuranceDetails.TabIndex = 0;
            // 
            // txtIns_SSN
            // 
            this.txtIns_SSN.AllowValidate = true;
            this.txtIns_SSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_SSN.IncludeLiteralsAndPrompts = false;
            this.txtIns_SSN.Location = new System.Drawing.Point(106, 176);
            this.txtIns_SSN.MaskType = gloMaskControl.gloMaskType.SSN;
            this.txtIns_SSN.Name = "txtIns_SSN";
            this.txtIns_SSN.ReadOnly = false;
            this.txtIns_SSN.Size = new System.Drawing.Size(94, 22);
            this.txtIns_SSN.TabIndex = 7;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Location = new System.Drawing.Point(224, 288);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(59, 14);
            this.label75.TabIndex = 157;
            this.label75.Tag = "";
            this.label75.Text = "CopayUC:";
            this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label75.Visible = false;
            // 
            // txtCopay
            // 
            this.txtCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopay.ForeColor = System.Drawing.Color.Black;
            this.txtCopay.Location = new System.Drawing.Point(106, 338);
            this.txtCopay.Name = "txtCopay";
            this.txtCopay.Size = new System.Drawing.Size(109, 22);
            this.txtCopay.TabIndex = 15;
            this.txtCopay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtIns_ExpiryDate
            // 
            this.txtIns_ExpiryDate.Location = new System.Drawing.Point(106, 310);
            this.txtIns_ExpiryDate.Mask = "00/00/0000";
            this.txtIns_ExpiryDate.Name = "txtIns_ExpiryDate";
            this.txtIns_ExpiryDate.Size = new System.Drawing.Size(109, 22);
            this.txtIns_ExpiryDate.TabIndex = 14;
            this.txtIns_ExpiryDate.ValidatingType = typeof(System.DateTime);
            this.txtIns_ExpiryDate.Validating += new System.ComponentModel.CancelEventHandler(this.txtIns_ExpiryDate_Validating);
            this.txtIns_ExpiryDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIns_DOB_MouseClick);
            // 
            // txtIns_EffectiveDate
            // 
            this.txtIns_EffectiveDate.Location = new System.Drawing.Point(106, 285);
            this.txtIns_EffectiveDate.Mask = "00/00/0000";
            this.txtIns_EffectiveDate.Name = "txtIns_EffectiveDate";
            this.txtIns_EffectiveDate.Size = new System.Drawing.Size(109, 22);
            this.txtIns_EffectiveDate.TabIndex = 12;
            this.txtIns_EffectiveDate.ValidatingType = typeof(System.DateTime);
            this.txtIns_EffectiveDate.Validating += new System.ComponentModel.CancelEventHandler(this.txtIns_EffectiveDate_Validating);
            this.txtIns_EffectiveDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIns_DOB_MouseClick);
            // 
            // txtIns_DOB
            // 
            this.txtIns_DOB.Location = new System.Drawing.Point(106, 204);
            this.txtIns_DOB.Mask = "00/00/0000";
            this.txtIns_DOB.Name = "txtIns_DOB";
            this.txtIns_DOB.Size = new System.Drawing.Size(109, 22);
            this.txtIns_DOB.TabIndex = 9;
            this.txtIns_DOB.ValidatingType = typeof(System.DateTime);
            this.txtIns_DOB.Validating += new System.ComponentModel.CancelEventHandler(this.txtIns_DOB_Validating);
            this.txtIns_DOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIns_DOB_MouseClick);
            this.txtIns_DOB.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtIns_DOB_MaskInputRejected);
            this.txtIns_DOB.TextChanged += new System.EventHandler(this.txtIns_DOB_TextChanged);
            // 
            // txtInsCounty
            // 
            this.txtInsCounty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtInsCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsCounty.Location = new System.Drawing.Point(254, 422);
            this.txtInsCounty.Name = "txtInsCounty";
            this.txtInsCounty.Size = new System.Drawing.Size(83, 22);
            this.txtInsCounty.TabIndex = 20;
            // 
            // label64
            // 
            this.label64.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label64.AutoEllipsis = true;
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(201, 426);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(54, 14);
            this.label64.TabIndex = 151;
            this.label64.Text = "County :";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Location = new System.Drawing.Point(218, 209);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(63, 13);
            this.label58.TabIndex = 150;
            this.label58.Text = "(mm/dd/yy)";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbInsOther);
            this.groupBox1.Controls.Add(this.rbInsFemale);
            this.groupBox1.Controls.Add(this.rbInsMale);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(106, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 30);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // rbInsOther
            // 
            this.rbInsOther.AutoSize = true;
            this.rbInsOther.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsOther.Location = new System.Drawing.Point(136, 10);
            this.rbInsOther.Name = "rbInsOther";
            this.rbInsOther.Size = new System.Drawing.Size(57, 18);
            this.rbInsOther.TabIndex = 0;
            this.rbInsOther.Text = "Other";
            this.rbInsOther.UseVisualStyleBackColor = true;
            this.rbInsOther.CheckedChanged += new System.EventHandler(this.rbInsOther_CheckedChanged);
            // 
            // rbInsFemale
            // 
            this.rbInsFemale.AutoSize = true;
            this.rbInsFemale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsFemale.Location = new System.Drawing.Point(65, 10);
            this.rbInsFemale.Name = "rbInsFemale";
            this.rbInsFemale.Size = new System.Drawing.Size(63, 18);
            this.rbInsFemale.TabIndex = 1;
            this.rbInsFemale.Text = "Female";
            this.rbInsFemale.UseVisualStyleBackColor = true;
            this.rbInsFemale.CheckedChanged += new System.EventHandler(this.rbInsFemale_CheckedChanged);
            // 
            // rbInsMale
            // 
            this.rbInsMale.AutoSize = true;
            this.rbInsMale.Checked = true;
            this.rbInsMale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsMale.Location = new System.Drawing.Point(5, 10);
            this.rbInsMale.Name = "rbInsMale";
            this.rbInsMale.Size = new System.Drawing.Size(53, 18);
            this.rbInsMale.TabIndex = 0;
            this.rbInsMale.TabStop = true;
            this.rbInsMale.Text = "Male";
            this.rbInsMale.UseVisualStyleBackColor = true;
            this.rbInsMale.CheckedChanged += new System.EventHandler(this.rbInsMale_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(68, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 125;
            this.label2.Text = "Sex :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIns_PatientCode
            // 
            this.txtIns_PatientCode.Enabled = false;
            this.txtIns_PatientCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_PatientCode.ForeColor = System.Drawing.Color.Black;
            this.txtIns_PatientCode.Location = new System.Drawing.Point(253, 176);
            this.txtIns_PatientCode.Name = "txtIns_PatientCode";
            this.txtIns_PatientCode.Size = new System.Drawing.Size(83, 22);
            this.txtIns_PatientCode.TabIndex = 8;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Location = new System.Drawing.Point(212, 180);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(43, 14);
            this.label57.TabIndex = 149;
            this.label57.Text = "Code :";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIns_PatientLastName
            // 
            this.txtIns_PatientLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_PatientLastName.ForeColor = System.Drawing.Color.Black;
            this.txtIns_PatientLastName.Location = new System.Drawing.Point(106, 149);
            this.txtIns_PatientLastName.Name = "txtIns_PatientLastName";
            this.txtIns_PatientLastName.Size = new System.Drawing.Size(230, 22);
            this.txtIns_PatientLastName.TabIndex = 6;
            // 
            // txtIns_PatientFirstName
            // 
            this.txtIns_PatientFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_PatientFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtIns_PatientFirstName.Location = new System.Drawing.Point(106, 122);
            this.txtIns_PatientFirstName.Name = "txtIns_PatientFirstName";
            this.txtIns_PatientFirstName.Size = new System.Drawing.Size(230, 22);
            this.txtIns_PatientFirstName.TabIndex = 5;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(31, 126);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 14);
            this.label26.TabIndex = 145;
            this.label26.Text = "First Name :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(66, 180);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 14);
            this.label24.TabIndex = 143;
            this.label24.Text = "SSN :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Location = new System.Drawing.Point(31, 153);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 14);
            this.label25.TabIndex = 146;
            this.label25.Text = "Last Name :";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_Ins_Providers
            // 
            this.cmb_Ins_Providers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmb_Ins_Providers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Ins_Providers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Ins_Providers.ForeColor = System.Drawing.Color.Black;
            this.cmb_Ins_Providers.FormattingEnabled = true;
            this.cmb_Ins_Providers.Location = new System.Drawing.Point(106, 480);
            this.cmb_Ins_Providers.Name = "cmb_Ins_Providers";
            this.cmb_Ins_Providers.Size = new System.Drawing.Size(231, 22);
            this.cmb_Ins_Providers.TabIndex = 22;
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoEllipsis = true;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(44, 484);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 14);
            this.label23.TabIndex = 142;
            this.label23.Text = "Provider :";
            // 
            // txtIns_CopayOV
            // 
            this.txtIns_CopayOV.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_CopayOV.ForeColor = System.Drawing.Color.Black;
            this.txtIns_CopayOV.Location = new System.Drawing.Point(338, 306);
            this.txtIns_CopayOV.Name = "txtIns_CopayOV";
            this.txtIns_CopayOV.Size = new System.Drawing.Size(29, 22);
            this.txtIns_CopayOV.TabIndex = 13;
            this.txtIns_CopayOV.Visible = false;
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Location = new System.Drawing.Point(305, 310);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(32, 14);
            this.label49.TabIndex = 139;
            this.label49.Text = "CopayOV  :";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label49.Visible = false;
            // 
            // txtIns_CopayER
            // 
            this.txtIns_CopayER.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_CopayER.ForeColor = System.Drawing.Color.Black;
            this.txtIns_CopayER.Location = new System.Drawing.Point(296, 334);
            this.txtIns_CopayER.Name = "txtIns_CopayER";
            this.txtIns_CopayER.Size = new System.Drawing.Size(81, 22);
            this.txtIns_CopayER.TabIndex = 15;
            this.txtIns_CopayER.Visible = false;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Location = new System.Drawing.Point(229, 338);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(66, 14);
            this.label48.TabIndex = 137;
            this.label48.Text = "Copay ER :";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label48.Visible = false;
            // 
            // txtIns_CopaySP
            // 
            this.txtIns_CopaySP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_CopaySP.ForeColor = System.Drawing.Color.Black;
            this.txtIns_CopaySP.Location = new System.Drawing.Point(285, 307);
            this.txtIns_CopaySP.Name = "txtIns_CopaySP";
            this.txtIns_CopaySP.Size = new System.Drawing.Size(10, 22);
            this.txtIns_CopaySP.TabIndex = 14;
            this.txtIns_CopaySP.Visible = false;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Location = new System.Drawing.Point(224, 310);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(62, 14);
            this.label47.TabIndex = 135;
            this.label47.Text = "CopaySP :";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label47.Visible = false;
            // 
            // txtIns_CopayUC
            // 
            this.txtIns_CopayUC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_CopayUC.ForeColor = System.Drawing.Color.Black;
            this.txtIns_CopayUC.Location = new System.Drawing.Point(289, 285);
            this.txtIns_CopayUC.Name = "txtIns_CopayUC";
            this.txtIns_CopayUC.Size = new System.Drawing.Size(25, 22);
            this.txtIns_CopayUC.TabIndex = 13;
            this.txtIns_CopayUC.Visible = false;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Location = new System.Drawing.Point(40, 342);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(48, 14);
            this.label46.TabIndex = 133;
            this.label46.Tag = "CopayUC";
            this.label46.Text = "Copay :";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Location = new System.Drawing.Point(64, 207);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(39, 14);
            this.label41.TabIndex = 131;
            this.label41.Text = "DOB :";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIns_Zip
            // 
            this.txtIns_Zip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_Zip.ForeColor = System.Drawing.Color.Black;
            this.txtIns_Zip.Location = new System.Drawing.Point(106, 423);
            this.txtIns_Zip.Name = "txtIns_Zip";
            this.txtIns_Zip.Size = new System.Drawing.Size(93, 22);
            this.txtIns_Zip.TabIndex = 17;
            this.txtIns_Zip.Leave += new System.EventHandler(this.txtPAZip_Leave);
            this.txtIns_Zip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAZip_KeyPress);
            // 
            // txtIns_State
            // 
            this.txtIns_State.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_State.ForeColor = System.Drawing.Color.Black;
            this.txtIns_State.Location = new System.Drawing.Point(254, 395);
            this.txtIns_State.Name = "txtIns_State";
            this.txtIns_State.Size = new System.Drawing.Size(83, 22);
            this.txtIns_State.TabIndex = 19;
            // 
            // txtIns_City
            // 
            this.txtIns_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_City.ForeColor = System.Drawing.Color.Black;
            this.txtIns_City.Location = new System.Drawing.Point(106, 395);
            this.txtIns_City.Name = "txtIns_City";
            this.txtIns_City.Size = new System.Drawing.Size(93, 22);
            this.txtIns_City.TabIndex = 18;
            // 
            // txtIns_Address
            // 
            this.txtIns_Address.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_Address.ForeColor = System.Drawing.Color.Black;
            this.txtIns_Address.Location = new System.Drawing.Point(106, 368);
            this.txtIns_Address.Name = "txtIns_Address";
            this.txtIns_Address.Size = new System.Drawing.Size(231, 22);
            this.txtIns_Address.TabIndex = 16;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Location = new System.Drawing.Point(73, 427);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(31, 14);
            this.label42.TabIndex = 125;
            this.label42.Text = "Zip :";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Location = new System.Drawing.Point(207, 399);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(45, 14);
            this.label43.TabIndex = 126;
            this.label43.Text = "State :";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Location = new System.Drawing.Point(69, 399);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(35, 14);
            this.label44.TabIndex = 124;
            this.label44.Text = "City :";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Location = new System.Drawing.Point(45, 372);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(58, 14);
            this.label45.TabIndex = 123;
            this.label45.Text = "Address :";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIns_PayerID
            // 
            this.txtIns_PayerID.AcceptsTab = true;
            this.txtIns_PayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_PayerID.ForeColor = System.Drawing.Color.Black;
            this.txtIns_PayerID.HideSelection = false;
            this.txtIns_PayerID.Location = new System.Drawing.Point(106, 14);
            this.txtIns_PayerID.Name = "txtIns_PayerID";
            this.txtIns_PayerID.Size = new System.Drawing.Size(230, 22);
            this.txtIns_PayerID.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(42, 18);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(61, 14);
            this.label28.TabIndex = 121;
            this.label28.Text = "Payer ID :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIns_MemberID
            // 
            this.txtIns_MemberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_MemberID.ForeColor = System.Drawing.Color.Black;
            this.txtIns_MemberID.Location = new System.Drawing.Point(106, 68);
            this.txtIns_MemberID.Name = "txtIns_MemberID";
            this.txtIns_MemberID.Size = new System.Drawing.Size(230, 22);
            this.txtIns_MemberID.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(28, 72);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(75, 14);
            this.label27.TabIndex = 119;
            this.label27.Text = "Member ID :";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(1, 542);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(375, 1);
            this.label14.TabIndex = 118;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(375, 1);
            this.label15.TabIndex = 117;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(376, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 543);
            this.label16.TabIndex = 116;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 543);
            this.label17.TabIndex = 115;
            // 
            // txtIns_GroupNo
            // 
            this.txtIns_GroupNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_GroupNo.ForeColor = System.Drawing.Color.Black;
            this.txtIns_GroupNo.Location = new System.Drawing.Point(106, 257);
            this.txtIns_GroupNo.Name = "txtIns_GroupNo";
            this.txtIns_GroupNo.Size = new System.Drawing.Size(230, 22);
            this.txtIns_GroupNo.TabIndex = 11;
            // 
            // txtIns_ContractNo
            // 
            this.txtIns_ContractNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_ContractNo.ForeColor = System.Drawing.Color.Black;
            this.txtIns_ContractNo.Location = new System.Drawing.Point(106, 452);
            this.txtIns_ContractNo.Name = "txtIns_ContractNo";
            this.txtIns_ContractNo.Size = new System.Drawing.Size(231, 22);
            this.txtIns_ContractNo.TabIndex = 21;
            // 
            // txtIns_MemberName
            // 
            this.txtIns_MemberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_MemberName.ForeColor = System.Drawing.Color.Black;
            this.txtIns_MemberName.Location = new System.Drawing.Point(106, 95);
            this.txtIns_MemberName.Name = "txtIns_MemberName";
            this.txtIns_MemberName.Size = new System.Drawing.Size(230, 22);
            this.txtIns_MemberName.TabIndex = 4;
            // 
            // txtIns_PlanProvider
            // 
            this.txtIns_PlanProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtIns_PlanProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtIns_PlanProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_PlanProvider.ForeColor = System.Drawing.Color.Black;
            this.txtIns_PlanProvider.Location = new System.Drawing.Point(106, 41);
            this.txtIns_PlanProvider.Name = "txtIns_PlanProvider";
            this.txtIns_PlanProvider.Size = new System.Drawing.Size(230, 22);
            this.txtIns_PlanProvider.TabIndex = 2;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Location = new System.Drawing.Point(26, 315);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 14);
            this.label35.TabIndex = 5;
            this.label35.Text = "Expiry Date :";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Location = new System.Drawing.Point(10, 288);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(93, 14);
            this.label36.TabIndex = 4;
            this.label36.Text = "Effective Date :";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(36, 261);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(67, 14);
            this.label37.TabIndex = 3;
            this.label37.Text = "Group No :";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(22, 456);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(81, 14);
            this.label38.TabIndex = 2;
            this.label38.Text = "Contract No :";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(9, 99);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(94, 14);
            this.label39.TabIndex = 1;
            this.label39.Text = "Member Name :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Location = new System.Drawing.Point(18, 45);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(85, 14);
            this.label40.TabIndex = 0;
            this.label40.Text = "Plan Provider :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLicenseDetails
            // 
            this.pnlLicenseDetails.Controls.Add(this.txtLicDOB);
            this.pnlLicenseDetails.Controls.Add(this.txtLicCounty);
            this.pnlLicenseDetails.Controls.Add(this.lblCountry);
            this.pnlLicenseDetails.Controls.Add(this.gbPAGender);
            this.pnlLicenseDetails.Controls.Add(this.txtLicMiddleName);
            this.pnlLicenseDetails.Controls.Add(this.label59);
            this.pnlLicenseDetails.Controls.Add(this.txtLic_PatientCode);
            this.pnlLicenseDetails.Controls.Add(this.label56);
            this.pnlLicenseDetails.Controls.Add(this.cmb_DLID_Providers);
            this.pnlLicenseDetails.Controls.Add(this.label22);
            this.pnlLicenseDetails.Controls.Add(this.label50);
            this.pnlLicenseDetails.Controls.Add(this.label10);
            this.pnlLicenseDetails.Controls.Add(this.label11);
            this.pnlLicenseDetails.Controls.Add(this.label12);
            this.pnlLicenseDetails.Controls.Add(this.label13);
            this.pnlLicenseDetails.Controls.Add(this.label4);
            this.pnlLicenseDetails.Controls.Add(this.txtLicSSn);
            this.pnlLicenseDetails.Controls.Add(this.label3);
            this.pnlLicenseDetails.Controls.Add(this.txtLicNo);
            this.pnlLicenseDetails.Controls.Add(this.label1);
            this.pnlLicenseDetails.Controls.Add(this.pbFaceImage);
            this.pnlLicenseDetails.Controls.Add(this.txtLicZip);
            this.pnlLicenseDetails.Controls.Add(this.txtLicState);
            this.pnlLicenseDetails.Controls.Add(this.txtLicCity);
            this.pnlLicenseDetails.Controls.Add(this.txtLicAddress);
            this.pnlLicenseDetails.Controls.Add(this.txtLicLastName);
            this.pnlLicenseDetails.Controls.Add(this.txtLicFirstName);
            this.pnlLicenseDetails.Controls.Add(this.label9);
            this.pnlLicenseDetails.Controls.Add(this.txtLicID);
            this.pnlLicenseDetails.Controls.Add(this.label29);
            this.pnlLicenseDetails.Controls.Add(this.label30);
            this.pnlLicenseDetails.Controls.Add(this.label31);
            this.pnlLicenseDetails.Controls.Add(this.label32);
            this.pnlLicenseDetails.Controls.Add(this.label33);
            this.pnlLicenseDetails.Controls.Add(this.label34);
            this.pnlLicenseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLicenseDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLicenseDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLicenseDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlLicenseDetails.Name = "pnlLicenseDetails";
            this.pnlLicenseDetails.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlLicenseDetails.Size = new System.Drawing.Size(380, 546);
            this.pnlLicenseDetails.TabIndex = 0;
            // 
            // txtLicDOB
            // 
            this.txtLicDOB.Location = new System.Drawing.Point(88, 365);
            this.txtLicDOB.Mask = "00/00/0000";
            this.txtLicDOB.Name = "txtLicDOB";
            this.txtLicDOB.Size = new System.Drawing.Size(111, 22);
            this.txtLicDOB.TabIndex = 7;
            this.txtLicDOB.ValidatingType = typeof(System.DateTime);
            this.txtLicDOB.Validating += new System.ComponentModel.CancelEventHandler(this.txtLicDOB_Validating);
            this.txtLicDOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIns_DOB_MouseClick);
            // 
            // txtLicCounty
            // 
            this.txtLicCounty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtLicCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicCounty.Location = new System.Drawing.Point(242, 473);
            this.txtLicCounty.Name = "txtLicCounty";
            this.txtLicCounty.Size = new System.Drawing.Size(89, 22);
            this.txtLicCounty.TabIndex = 13;
            this.txtLicCounty.Tag = "";
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountry.AutoEllipsis = true;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(189, 477);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(54, 14);
            this.lblCountry.TabIndex = 125;
            this.lblCountry.Text = "County :";
            // 
            // gbPAGender
            // 
            this.gbPAGender.Controls.Add(this.rbLicOthers);
            this.gbPAGender.Controls.Add(this.rbLicFemale);
            this.gbPAGender.Controls.Add(this.rbLicMale);
            this.gbPAGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPAGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbPAGender.Location = new System.Drawing.Point(88, 386);
            this.gbPAGender.Name = "gbPAGender";
            this.gbPAGender.Size = new System.Drawing.Size(282, 30);
            this.gbPAGender.TabIndex = 8;
            this.gbPAGender.TabStop = false;
            // 
            // rbLicOthers
            // 
            this.rbLicOthers.AutoSize = true;
            this.rbLicOthers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLicOthers.Location = new System.Drawing.Point(139, 10);
            this.rbLicOthers.Name = "rbLicOthers";
            this.rbLicOthers.Size = new System.Drawing.Size(57, 18);
            this.rbLicOthers.TabIndex = 2;
            this.rbLicOthers.Text = "Other";
            this.rbLicOthers.UseVisualStyleBackColor = true;
            this.rbLicOthers.CheckedChanged += new System.EventHandler(this.rbLicOthers_CheckedChanged);
            // 
            // rbLicFemale
            // 
            this.rbLicFemale.AutoSize = true;
            this.rbLicFemale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLicFemale.Location = new System.Drawing.Point(69, 10);
            this.rbLicFemale.Name = "rbLicFemale";
            this.rbLicFemale.Size = new System.Drawing.Size(63, 18);
            this.rbLicFemale.TabIndex = 1;
            this.rbLicFemale.Text = "Female";
            this.rbLicFemale.UseVisualStyleBackColor = true;
            this.rbLicFemale.CheckedChanged += new System.EventHandler(this.rbLicFemale_CheckedChanged);
            // 
            // rbLicMale
            // 
            this.rbLicMale.AutoSize = true;
            this.rbLicMale.Checked = true;
            this.rbLicMale.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLicMale.Location = new System.Drawing.Point(11, 10);
            this.rbLicMale.Name = "rbLicMale";
            this.rbLicMale.Size = new System.Drawing.Size(53, 18);
            this.rbLicMale.TabIndex = 0;
            this.rbLicMale.TabStop = true;
            this.rbLicMale.Text = "Male";
            this.rbLicMale.UseVisualStyleBackColor = true;
            this.rbLicMale.CheckedChanged += new System.EventHandler(this.rbLicMale_CheckedChanged);
            // 
            // txtLicMiddleName
            // 
            this.txtLicMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicMiddleName.ForeColor = System.Drawing.Color.Black;
            this.txtLicMiddleName.Location = new System.Drawing.Point(88, 315);
            this.txtLicMiddleName.Name = "txtLicMiddleName";
            this.txtLicMiddleName.Size = new System.Drawing.Size(278, 22);
            this.txtLicMiddleName.TabIndex = 5;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(2, 319);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(84, 14);
            this.label59.TabIndex = 122;
            this.label59.Text = "Middle Name :";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLic_PatientCode
            // 
            this.txtLic_PatientCode.Enabled = false;
            this.txtLic_PatientCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLic_PatientCode.ForeColor = System.Drawing.Color.Black;
            this.txtLic_PatientCode.Location = new System.Drawing.Point(88, 237);
            this.txtLic_PatientCode.Name = "txtLic_PatientCode";
            this.txtLic_PatientCode.Size = new System.Drawing.Size(136, 22);
            this.txtLic_PatientCode.TabIndex = 2;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(43, 241);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(43, 14);
            this.label56.TabIndex = 121;
            this.label56.Text = "Code :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_DLID_Providers
            // 
            this.cmb_DLID_Providers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmb_DLID_Providers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DLID_Providers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_DLID_Providers.ForeColor = System.Drawing.Color.Black;
            this.cmb_DLID_Providers.FormattingEnabled = true;
            this.cmb_DLID_Providers.Location = new System.Drawing.Point(88, 501);
            this.cmb_DLID_Providers.Name = "cmb_DLID_Providers";
            this.cmb_DLID_Providers.Size = new System.Drawing.Size(278, 22);
            this.cmb_DLID_Providers.TabIndex = 14;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoEllipsis = true;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(25, 505);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 14);
            this.label22.TabIndex = 68;
            this.label22.Text = "Provider :";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(47, 368);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(39, 14);
            this.label50.TabIndex = 119;
            this.label50.Text = "DOB :";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 542);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(375, 1);
            this.label10.TabIndex = 118;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(375, 1);
            this.label11.TabIndex = 117;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(376, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 543);
            this.label12.TabIndex = 116;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 543);
            this.label13.TabIndex = 115;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(51, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 113;
            this.label4.Text = "Sex :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicSSn
            // 
            this.txtLicSSn.AllowValidate = true;
            this.txtLicSSn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicSSn.IncludeLiteralsAndPrompts = false;
            this.txtLicSSn.Location = new System.Drawing.Point(88, 263);
            this.txtLicSSn.MaskType = gloMaskControl.gloMaskType.SSN;
            this.txtLicSSn.Name = "txtLicSSn";
            this.txtLicSSn.ReadOnly = false;
            this.txtLicSSn.Size = new System.Drawing.Size(94, 22);
            this.txtLicSSn.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(49, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 14);
            this.label3.TabIndex = 111;
            this.label3.Text = "SSN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicNo
            // 
            this.txtLicNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicNo.ForeColor = System.Drawing.Color.Black;
            this.txtLicNo.Location = new System.Drawing.Point(88, 210);
            this.txtLicNo.Name = "txtLicNo";
            this.txtLicNo.Size = new System.Drawing.Size(278, 22);
            this.txtLicNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(31, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "License :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbFaceImage
            // 
            this.pbFaceImage.BackgroundImage = global::gloCardScanning.Properties.Resources.HumanNew;
            this.pbFaceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbFaceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFaceImage.Location = new System.Drawing.Point(119, 12);
            this.pbFaceImage.Name = "pbFaceImage";
            this.pbFaceImage.Size = new System.Drawing.Size(142, 159);
            this.pbFaceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbFaceImage.TabIndex = 108;
            this.pbFaceImage.TabStop = false;
            // 
            // txtLicZip
            // 
            this.txtLicZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicZip.ForeColor = System.Drawing.Color.Black;
            this.txtLicZip.Location = new System.Drawing.Point(88, 474);
            this.txtLicZip.Name = "txtLicZip";
            this.txtLicZip.Size = new System.Drawing.Size(95, 22);
            this.txtLicZip.TabIndex = 10;
            this.txtLicZip.Leave += new System.EventHandler(this.txtPAZip_Leave);
            this.txtLicZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAZip_KeyPress);
            // 
            // txtLicState
            // 
            this.txtLicState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicState.ForeColor = System.Drawing.Color.Black;
            this.txtLicState.Location = new System.Drawing.Point(242, 448);
            this.txtLicState.Name = "txtLicState";
            this.txtLicState.Size = new System.Drawing.Size(53, 22);
            this.txtLicState.TabIndex = 12;
            // 
            // txtLicCity
            // 
            this.txtLicCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicCity.ForeColor = System.Drawing.Color.Black;
            this.txtLicCity.Location = new System.Drawing.Point(88, 448);
            this.txtLicCity.Name = "txtLicCity";
            this.txtLicCity.Size = new System.Drawing.Size(94, 22);
            this.txtLicCity.TabIndex = 11;
            // 
            // txtLicAddress
            // 
            this.txtLicAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicAddress.ForeColor = System.Drawing.Color.Black;
            this.txtLicAddress.Location = new System.Drawing.Point(88, 421);
            this.txtLicAddress.Name = "txtLicAddress";
            this.txtLicAddress.Size = new System.Drawing.Size(278, 22);
            this.txtLicAddress.TabIndex = 9;
            // 
            // txtLicLastName
            // 
            this.txtLicLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicLastName.ForeColor = System.Drawing.Color.Black;
            this.txtLicLastName.Location = new System.Drawing.Point(88, 339);
            this.txtLicLastName.Name = "txtLicLastName";
            this.txtLicLastName.Size = new System.Drawing.Size(278, 22);
            this.txtLicLastName.TabIndex = 6;
            // 
            // txtLicFirstName
            // 
            this.txtLicFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtLicFirstName.Location = new System.Drawing.Point(88, 290);
            this.txtLicFirstName.Name = "txtLicFirstName";
            this.txtLicFirstName.Size = new System.Drawing.Size(278, 22);
            this.txtLicFirstName.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(55, 478);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Zip :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicID
            // 
            this.txtLicID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicID.ForeColor = System.Drawing.Color.Black;
            this.txtLicID.Location = new System.Drawing.Point(88, 184);
            this.txtLicID.Name = "txtLicID";
            this.txtLicID.Size = new System.Drawing.Size(278, 22);
            this.txtLicID.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(195, 452);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(45, 14);
            this.label29.TabIndex = 5;
            this.label29.Text = "State :";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(51, 452);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(35, 14);
            this.label30.TabIndex = 4;
            this.label30.Text = "City :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(28, 425);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(58, 14);
            this.label31.TabIndex = 3;
            this.label31.Text = "Address :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(14, 343);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(72, 14);
            this.label32.TabIndex = 2;
            this.label32.Text = "Last Name :";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(14, 294);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(72, 14);
            this.label33.TabIndex = 1;
            this.label33.Text = "First Name :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Location = new System.Drawing.Point(59, 188);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(27, 14);
            this.label34.TabIndex = 0;
            this.label34.Text = "ID :";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlTemplateDetailsHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(358, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel2.Size = new System.Drawing.Size(380, 29);
            this.panel2.TabIndex = 115;
            // 
            // pnlTemplateDetailsHeader
            // 
            this.pnlTemplateDetailsHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTemplateDetailsHeader.BackgroundImage")));
            this.pnlTemplateDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetailsHeader.Controls.Add(this.lblCardDetails);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label21);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label20);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label19);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label18);
            this.pnlTemplateDetailsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateDetailsHeader.Location = new System.Drawing.Point(0, 3);
            this.pnlTemplateDetailsHeader.Name = "pnlTemplateDetailsHeader";
            this.pnlTemplateDetailsHeader.Size = new System.Drawing.Size(377, 23);
            this.pnlTemplateDetailsHeader.TabIndex = 1;
            // 
            // lblCardDetails
            // 
            this.lblCardDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblCardDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardDetails.ForeColor = System.Drawing.Color.White;
            this.lblCardDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCardDetails.Location = new System.Drawing.Point(1, 1);
            this.lblCardDetails.Name = "lblCardDetails";
            this.lblCardDetails.Size = new System.Drawing.Size(375, 21);
            this.lblCardDetails.TabIndex = 117;
            this.lblCardDetails.Text = "  Card Details";
            this.lblCardDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(1, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(375, 1);
            this.label21.TabIndex = 116;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(1, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(375, 1);
            this.label20.TabIndex = 115;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Location = new System.Drawing.Point(376, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 23);
            this.label19.TabIndex = 113;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 23);
            this.label18.TabIndex = 112;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(355, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 575);
            this.splitter1.TabIndex = 113;
            this.splitter1.TabStop = false;
            // 
            // pnlCardLeft
            // 
            this.pnlCardLeft.Controls.Add(this.panel3);
            this.pnlCardLeft.Controls.Add(this.panel5);
            this.pnlCardLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCardLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlCardLeft.Name = "pnlCardLeft";
            this.pnlCardLeft.Size = new System.Drawing.Size(355, 575);
            this.pnlCardLeft.TabIndex = 112;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label63);
            this.panel3.Controls.Add(this.label62);
            this.panel3.Controls.Add(this.label61);
            this.panel3.Controls.Add(this.label60);
            this.panel3.Controls.Add(this.pb_FrontSide);
            this.panel3.Controls.Add(this.pb_BackSide);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 29);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(355, 546);
            this.panel3.TabIndex = 116;
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Location = new System.Drawing.Point(4, 542);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(350, 1);
            this.label63.TabIndex = 118;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Location = new System.Drawing.Point(4, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(350, 1);
            this.label62.TabIndex = 117;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Location = new System.Drawing.Point(354, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 543);
            this.label61.TabIndex = 114;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Location = new System.Drawing.Point(3, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 543);
            this.label60.TabIndex = 113;
            // 
            // pb_FrontSide
            // 
            this.pb_FrontSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_FrontSide.BackgroundImage")));
            this.pb_FrontSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_FrontSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_FrontSide.Location = new System.Drawing.Point(14, 45);
            this.pb_FrontSide.Name = "pb_FrontSide";
            this.pb_FrontSide.Size = new System.Drawing.Size(325, 192);
            this.pb_FrontSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_FrontSide.TabIndex = 107;
            this.pb_FrontSide.TabStop = false;
            // 
            // pb_BackSide
            // 
            this.pb_BackSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_BackSide.BackgroundImage")));
            this.pb_BackSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_BackSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_BackSide.Location = new System.Drawing.Point(14, 253);
            this.pb_BackSide.Name = "pb_BackSide";
            this.pb_BackSide.Size = new System.Drawing.Size(325, 192);
            this.pb_BackSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_BackSide.TabIndex = 109;
            this.pb_BackSide.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel5.Size = new System.Drawing.Size(355, 29);
            this.panel5.TabIndex = 117;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label51);
            this.panel6.Controls.Add(this.label52);
            this.panel6.Controls.Add(this.label53);
            this.panel6.Controls.Add(this.label54);
            this.panel6.Controls.Add(this.label55);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(352, 23);
            this.panel6.TabIndex = 1;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.White;
            this.label51.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label51.Location = new System.Drawing.Point(1, 1);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(350, 21);
            this.label51.TabIndex = 117;
            this.label51.Text = "  Card Images";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Location = new System.Drawing.Point(1, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(350, 1);
            this.label52.TabIndex = 116;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label53.Location = new System.Drawing.Point(1, 22);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(350, 1);
            this.label53.TabIndex = 115;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Location = new System.Drawing.Point(351, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 23);
            this.label54.TabIndex = 113;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(0, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 23);
            this.label55.TabIndex = 112;
            // 
            // pnlCheck
            // 
            this.pnlCheck.Controls.Add(this.panel10);
            this.pnlCheck.Controls.Add(this.panel9);
            this.pnlCheck.Controls.Add(this.panel7);
            this.pnlCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCheck.Location = new System.Drawing.Point(0, 54);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new System.Drawing.Size(738, 575);
            this.pnlCheck.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.txtAccountNo);
            this.panel10.Controls.Add(this.lblAccountNo);
            this.panel10.Controls.Add(this.txtRoutingNo);
            this.panel10.Controls.Add(this.lblRoutingNumber);
            this.panel10.Controls.Add(this.txtCheckNo);
            this.panel10.Controls.Add(this.lblCheckNumber);
            this.panel10.Controls.Add(this.txtMICR);
            this.panel10.Controls.Add(this.lblMICR);
            this.panel10.Controls.Add(this.txtIssuingBank);
            this.panel10.Controls.Add(this.lblIssuingBank);
            this.panel10.Controls.Add(this.txtIssuingCompany);
            this.panel10.Controls.Add(this.label74);
            this.panel10.Controls.Add(this.txtCheckAmount);
            this.panel10.Controls.Add(this.lblCheckAmount);
            this.panel10.Controls.Add(this.label70);
            this.panel10.Controls.Add(this.label71);
            this.panel10.Controls.Add(this.label72);
            this.panel10.Controls.Add(this.label73);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 29);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel10.Size = new System.Drawing.Size(738, 237);
            this.panel10.TabIndex = 0;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.ForeColor = System.Drawing.Color.Black;
            this.txtAccountNo.Location = new System.Drawing.Point(132, 39);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(278, 22);
            this.txtAccountNo.TabIndex = 1;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAccountNo.Location = new System.Drawing.Point(18, 43);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(108, 14);
            this.lblAccountNo.TabIndex = 131;
            this.lblAccountNo.Text = "Account Number :";
            this.lblAccountNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRoutingNo
            // 
            this.txtRoutingNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoutingNo.ForeColor = System.Drawing.Color.Black;
            this.txtRoutingNo.Location = new System.Drawing.Point(132, 205);
            this.txtRoutingNo.Name = "txtRoutingNo";
            this.txtRoutingNo.Size = new System.Drawing.Size(278, 22);
            this.txtRoutingNo.TabIndex = 6;
            // 
            // lblRoutingNumber
            // 
            this.lblRoutingNumber.AutoSize = true;
            this.lblRoutingNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoutingNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRoutingNumber.Location = new System.Drawing.Point(22, 209);
            this.lblRoutingNumber.Name = "lblRoutingNumber";
            this.lblRoutingNumber.Size = new System.Drawing.Size(104, 14);
            this.lblRoutingNumber.TabIndex = 129;
            this.lblRoutingNumber.Text = "Routing Number :";
            this.lblRoutingNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckNo.ForeColor = System.Drawing.Color.Black;
            this.txtCheckNo.Location = new System.Drawing.Point(132, 11);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Size = new System.Drawing.Size(278, 22);
            this.txtCheckNo.TabIndex = 0;
            // 
            // lblCheckNumber
            // 
            this.lblCheckNumber.AutoSize = true;
            this.lblCheckNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckNumber.Location = new System.Drawing.Point(31, 15);
            this.lblCheckNumber.Name = "lblCheckNumber";
            this.lblCheckNumber.Size = new System.Drawing.Size(95, 14);
            this.lblCheckNumber.TabIndex = 127;
            this.lblCheckNumber.Text = "Check Number :";
            this.lblCheckNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMICR
            // 
            this.txtMICR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMICR.ForeColor = System.Drawing.Color.Black;
            this.txtMICR.Location = new System.Drawing.Point(132, 66);
            this.txtMICR.Name = "txtMICR";
            this.txtMICR.Size = new System.Drawing.Size(278, 22);
            this.txtMICR.TabIndex = 2;
            // 
            // lblMICR
            // 
            this.lblMICR.AutoSize = true;
            this.lblMICR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMICR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblMICR.Location = new System.Drawing.Point(84, 70);
            this.lblMICR.Name = "lblMICR";
            this.lblMICR.Size = new System.Drawing.Size(42, 14);
            this.lblMICR.TabIndex = 125;
            this.lblMICR.Text = "MICR :";
            this.lblMICR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIssuingBank
            // 
            this.txtIssuingBank.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssuingBank.ForeColor = System.Drawing.Color.Black;
            this.txtIssuingBank.Location = new System.Drawing.Point(132, 120);
            this.txtIssuingBank.Name = "txtIssuingBank";
            this.txtIssuingBank.Size = new System.Drawing.Size(414, 22);
            this.txtIssuingBank.TabIndex = 4;
            // 
            // lblIssuingBank
            // 
            this.lblIssuingBank.AutoSize = true;
            this.lblIssuingBank.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssuingBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblIssuingBank.Location = new System.Drawing.Point(44, 124);
            this.lblIssuingBank.Name = "lblIssuingBank";
            this.lblIssuingBank.Size = new System.Drawing.Size(82, 14);
            this.lblIssuingBank.TabIndex = 123;
            this.lblIssuingBank.Text = "Issuing Bank :";
            this.lblIssuingBank.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIssuingCompany
            // 
            this.txtIssuingCompany.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssuingCompany.ForeColor = System.Drawing.Color.Black;
            this.txtIssuingCompany.Location = new System.Drawing.Point(132, 148);
            this.txtIssuingCompany.Multiline = true;
            this.txtIssuingCompany.Name = "txtIssuingCompany";
            this.txtIssuingCompany.Size = new System.Drawing.Size(414, 52);
            this.txtIssuingCompany.TabIndex = 5;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Location = new System.Drawing.Point(20, 148);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(106, 14);
            this.label74.TabIndex = 121;
            this.label74.Text = "Issuing Company :";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCheckAmount
            // 
            this.txtCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckAmount.ForeColor = System.Drawing.Color.Black;
            this.txtCheckAmount.Location = new System.Drawing.Point(132, 94);
            this.txtCheckAmount.Name = "txtCheckAmount";
            this.txtCheckAmount.Size = new System.Drawing.Size(138, 22);
            this.txtCheckAmount.TabIndex = 3;
            this.txtCheckAmount.Text = "0.00";
            this.txtCheckAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheckAmount_KeyPress);
            // 
            // lblCheckAmount
            // 
            this.lblCheckAmount.AutoSize = true;
            this.lblCheckAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCheckAmount.Location = new System.Drawing.Point(67, 98);
            this.lblCheckAmount.Name = "lblCheckAmount";
            this.lblCheckAmount.Size = new System.Drawing.Size(59, 14);
            this.lblCheckAmount.TabIndex = 119;
            this.lblCheckAmount.Text = "Amount :";
            this.lblCheckAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label70.Location = new System.Drawing.Point(4, 233);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(730, 1);
            this.label70.TabIndex = 118;
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Location = new System.Drawing.Point(4, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(730, 1);
            this.label71.TabIndex = 117;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Right;
            this.label72.Location = new System.Drawing.Point(734, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(1, 234);
            this.label72.TabIndex = 114;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Left;
            this.label73.Location = new System.Drawing.Point(3, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1, 234);
            this.label73.TabIndex = 113;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.pb_ChequeImage);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 266);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel9.Size = new System.Drawing.Size(738, 309);
            this.panel9.TabIndex = 1;
            // 
            // pb_ChequeImage
            // 
            this.pb_ChequeImage.BackColor = System.Drawing.Color.Transparent;
            this.pb_ChequeImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_ChequeImage.BackgroundImage")));
            this.pb_ChequeImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pb_ChequeImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_ChequeImage.Location = new System.Drawing.Point(3, 0);
            this.pb_ChequeImage.Name = "pb_ChequeImage";
            this.pb_ChequeImage.Size = new System.Drawing.Size(735, 306);
            this.pb_ChequeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_ChequeImage.TabIndex = 119;
            this.pb_ChequeImage.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel7.Size = new System.Drawing.Size(738, 29);
            this.panel7.TabIndex = 118;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label65);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(735, 23);
            this.panel8.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(733, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "  Check Detalis";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(733, 1);
            this.label6.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(1, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(733, 1);
            this.label7.TabIndex = 115;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(734, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 113;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Left;
            this.label65.Location = new System.Drawing.Point(0, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1, 23);
            this.label65.TabIndex = 112;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // frmScanCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(738, 629);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlCheck);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScanCard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card Scanning";
            this.Load += new System.EventHandler(this.frmCardScanning_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScanCard_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axms_InsuranceCard)).EndInit();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlInsuranceDetails.ResumeLayout(false);
            this.pnlInsuranceDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlLicenseDetails.ResumeLayout(false);
            this.pnlLicenseDetails.PerformLayout();
            this.gbPAGender.ResumeLayout(false);
            this.gbPAGender.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFaceImage)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pnlTemplateDetailsHeader.ResumeLayout(false);
            this.pnlCardLeft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlCheck.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_ChequeImage)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private AxMEDICSDKLib.AxMedicSdk axms_InsuranceCard;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripButton tsb_InsuranceCard;
        internal System.Windows.Forms.ToolStripButton tsb_DriversLicense;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pb_BackSide;
        private System.Windows.Forms.PictureBox pbFaceImage;
        private System.Windows.Forms.PictureBox pb_FrontSide;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlInsuranceDetails;
        private System.Windows.Forms.TextBox txtIns_GroupNo;
        private System.Windows.Forms.TextBox txtIns_ContractNo;
        private System.Windows.Forms.TextBox txtIns_MemberName;
        private System.Windows.Forms.TextBox txtIns_PlanProvider;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel pnlLicenseDetails;
        private System.Windows.Forms.TextBox txtLicState;
        private System.Windows.Forms.TextBox txtLicCity;
        private System.Windows.Forms.TextBox txtLicAddress;
        private System.Windows.Forms.TextBox txtLicLastName;
        private System.Windows.Forms.TextBox txtLicFirstName;
        private System.Windows.Forms.TextBox txtLicID;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtLicNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlCardLeft;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox txtLicZip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlTemplateDetailsHeader;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCardDetails;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtIns_MemberID;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtIns_PayerID;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtIns_Zip;
        private System.Windows.Forms.TextBox txtIns_State;
        private System.Windows.Forms.TextBox txtIns_City;
        private System.Windows.Forms.TextBox txtIns_Address;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtIns_CopayER;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtIns_CopaySP;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtIns_CopayUC;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txtIns_CopayOV;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ComboBox cmb_DLID_Providers;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmb_Ins_Providers;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtIns_PatientLastName;
        private System.Windows.Forms.TextBox txtIns_PatientFirstName;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.ToolStripButton tsb_ClearData;
        private System.Windows.Forms.TextBox txtLic_PatientCode;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtIns_PatientCode;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TextBox txtLicMiddleName;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.GroupBox gbPAGender;
        private System.Windows.Forms.RadioButton rbLicOthers;
        private System.Windows.Forms.RadioButton rbLicFemale;
        private System.Windows.Forms.RadioButton rbLicMale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbInsOther;
        private System.Windows.Forms.RadioButton rbInsFemale;
        private System.Windows.Forms.RadioButton rbInsMale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txtInsCounty;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txtLicCounty;
        private System.Windows.Forms.Label lblCountry;
        internal System.Windows.Forms.ToolStripButton tsb_Cheque;
        private System.Windows.Forms.Panel pnlCheck;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox pb_ChequeImage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txtCheckAmount;
        private System.Windows.Forms.Label lblCheckAmount;
        private System.Windows.Forms.TextBox txtIssuingCompany;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.TextBox txtRoutingNo;
        private System.Windows.Forms.Label lblRoutingNumber;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.Label lblCheckNumber;
        private System.Windows.Forms.TextBox txtMICR;
        private System.Windows.Forms.Label lblMICR;
        private System.Windows.Forms.TextBox txtIssuingBank;
        private System.Windows.Forms.Label lblIssuingBank;
        private System.Windows.Forms.MaskedTextBox txtIns_DOB;
        private System.Windows.Forms.MaskedTextBox txtLicDOB;
        private System.Windows.Forms.MaskedTextBox txtIns_ExpiryDate;
        private System.Windows.Forms.MaskedTextBox txtIns_EffectiveDate;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox txtCopay;
        private gloMaskControl.gloMaskBox txtLicSSn;
        private gloMaskControl.gloMaskBox txtIns_SSN;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDoc;


    }
}